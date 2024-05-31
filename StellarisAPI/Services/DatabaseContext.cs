using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace StellarisAPI.Services;
public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DatabaseContext(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(configuration["Database:Host"], "Отсутствует Host БД");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(configuration["Database:Port"], "Отсутствует Port БД");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(configuration["Database:Database"], "Отсутствует Database БД");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(configuration["Database:Login"], "Отсутствует Login БД");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(configuration["Database:Password"], "Отсутствует Password БД");

        _configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"" +
            $"Host={_configuration["Database:Host"]};" +
            $"Port={_configuration["Database:Port"]};" +
            $"Database={_configuration["Database:Database"]};" +
            $"Username={_configuration["Database:Login"]};" +
            $"Password={_configuration["Database:Password"]}");
    }

}
