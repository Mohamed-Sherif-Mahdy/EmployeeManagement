using EmployeeManagement_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement_API.Data
{
  public class EmployeeManagemendtDbContext : DbContext
  {
    private readonly IConfiguration _configuration;

    public EmployeeManagemendtDbContext(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Job> Jobs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      // Retrieve connection string from appsettings.json
      string connectionString = _configuration.GetConnectionString("EmployeeDbContext");

      // Configure SQL Server connection
      optionsBuilder.UseSqlServer(connectionString);
    }

  }
}
