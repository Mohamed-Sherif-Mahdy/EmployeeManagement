using EmployeeManagement_API.Data;
using EmployeeManagement_API.Dto;
using EmployeeManagement_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement_API.Repository
{
  public class EmployeeRepository : IEmployeeRepository
  {
    private readonly EmployeeManagemendtDbContext _context;

    public EmployeeRepository(EmployeeManagemendtDbContext context)
    {
      _context = context;
    }

    public bool EmployeeExists(int id)
    {
      return _context.Employees.Any(e => e.EmployeeId == id);
    }
    public void UpdateEmployee(Employee DataBaseEmployee, Employee employee)
    {
      DataBaseEmployee.Name = employee.Name;
      DataBaseEmployee.JobId = employee.JobId;
      DataBaseEmployee.IsFirstAppointment = employee.IsFirstAppointment;
      DataBaseEmployee.StartDate = employee.StartDate;
      DataBaseEmployee.Notes = employee.Notes;
      _context.SaveChanges();
    }
    public Employee FitchEmployee(int id)
    {
      return _context.Employees.Include(e => e.Job).FirstOrDefault(e => e.EmployeeId == id);
    }
    public EmployeeWithJobTitleDto EmployeeWithJobTitleDto(Employee employee)
    {
      EmployeeWithJobTitleDto employeeWithJobTitleDto = new EmployeeWithJobTitleDto();
      employeeWithJobTitleDto.EmployeeId = employee.EmployeeId;
      employeeWithJobTitleDto.Name = employee.Name;
      employeeWithJobTitleDto.Gender = employee.Gender;
      employeeWithJobTitleDto.IsFirstAppointment = employee.IsFirstAppointment;
      employeeWithJobTitleDto.StartDate = employee.StartDate;
      employeeWithJobTitleDto.Notes = employee.Notes;
      employeeWithJobTitleDto.JobTitle = employee.Job.JobTitle;
      return employeeWithJobTitleDto;
    }
    public Employee EmployeeReMap(EmployeeWithJobTitleDto employeeWithJobTitleDto)
    {
      Employee employee = new Employee();
      employee.EmployeeId = employeeWithJobTitleDto.EmployeeId;
      employee.Name = employeeWithJobTitleDto.Name;
      employee.Gender = employeeWithJobTitleDto.Gender;
      employee.IsFirstAppointment = employeeWithJobTitleDto.IsFirstAppointment;
      employee.StartDate = employeeWithJobTitleDto.StartDate;
      employee.Notes = employeeWithJobTitleDto.Notes;
      employee.JobId = _context.Jobs.FirstOrDefault(j => j.JobTitle == employeeWithJobTitleDto.JobTitle).JobId;
      return employee;
    }

    public void AddEmployee(Employee employee)
    {
      _context.Employees.Add(employee);
      _context.SaveChanges();
    }

    public void DeleteEmployee(Employee employee)
    {
      _context.Employees.Remove(employee);
      _context.SaveChanges();
    }

    public List<Employee> GetEmployees()
    {
      return _context.Employees.Include(e => e.Job).ToList();
    }
    public List<string> GetJobs()
    {
      return _context.Jobs.Select(j => j.JobTitle).ToList();
    }
  }
}
