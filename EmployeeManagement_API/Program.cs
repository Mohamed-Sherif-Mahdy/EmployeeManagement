using EmployeeManagement_API.Data;
using EmployeeManagement_API.Repository;
using EmployeeManagement_API.Service;

namespace EmployeeManagement_API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();


      builder.Services.AddDbContext<EmployeeManagemendtDbContext>();
      builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
      builder.Services.AddScoped<IServiceEmployee, ServiceEmployee>();
      builder.Services.AddScoped<IServiceJob, ServiceJob>();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
