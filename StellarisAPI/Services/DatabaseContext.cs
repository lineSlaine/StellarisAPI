using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using StellarisAPI.Models;

namespace StellarisAPI.Services;
public class DatabaseContext : DbContext
{
    public DbSet<TestModel> testModels {  get; set; }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
