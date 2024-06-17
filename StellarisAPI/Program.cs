using Microsoft.EntityFrameworkCore;
using Serilog;
using StellarisAPI.Services;

namespace StellarisAPI
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync(args);
        public static void MainAsync(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

            builder.Services.AddLogging(x => x.ClearProviders().AddSerilog());
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite(configuration["Database:DatabaseData"]));
           

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();


            try
            {
                app.Run();
            }
            catch (Exception ex) 
            {
                Log.Fatal("Произошла необработанная ошибка {Exeption}", ex);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
