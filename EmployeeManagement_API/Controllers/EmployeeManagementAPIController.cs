using EmployeeManagement_API.Data;
using EmployeeManagement_API.Dto;
using EmployeeManagement_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeeManagementAPIController : ControllerBase
  {
    private readonly EmployeeManagemendtDbContext _context;

    public EmployeeManagementAPIController(EmployeeManagemendtDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult GetEmployees()
    {
      List<Employee> employees = _context.Employees.Include(e => e.Job).ToList();
      List<EmployeeWithJobTitleDto> employeeWithJobTitleDtos = new List<EmployeeWithJobTitleDto>();
      foreach (Employee employee in employees)
      {
        employeeWithJobTitleDtos.Add(EmployeeWithJobTitleDto(employee));
      }

      return Ok(employeeWithJobTitleDtos);
    }


    [HttpGet("{id}")]
    public IActionResult GetEmployee(int id)
    {
      Employee employee = FitchEmployee(id);

      if (employee == null)
      {
        return NotFound();
      }
      return Ok(EmployeeWithJobTitleDto(employee));
    }



    [HttpGet("JobTitles")]
    public IActionResult GetJobTitles()
    {
      List<string> jobTitles = _context.Jobs.Select(j => j.JobTitle).ToList();
      return Ok(jobTitles);
    }


    [HttpPut("{id}")]

    public IActionResult PutEmployee(int id, EmployeeWithJobTitleDto employeeWithJobTitleDto)
    {

      if (!EmployeeExists(id))
      {
        return NotFound();
      }
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      Employee DataBaseEmployee = FitchEmployee(id);
      Employee employee = EmployeeReMap(employeeWithJobTitleDto);
      UpdateEmployee(DataBaseEmployee, employee);
      _context.SaveChanges();
      return Ok(employee);

    }



    [HttpPost]
    public IActionResult PostEmployee(EmployeeWithJobTitleDto employeeWithJobTitleDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Employees.Add(EmployeeReMap(employeeWithJobTitleDto));
      _context.SaveChanges();

      return CreatedAtAction("GetEmployee", new { id = employeeWithJobTitleDto.EmployeeId }, employeeWithJobTitleDto);

    }


    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {

      Employee employee = FitchEmployee(id);
      if (employee == null)
      {
        return NotFound();
      }

      _context.Employees.Remove(employee);
      _context.SaveChanges();

      return NoContent();
    }

    private bool EmployeeExists(int id)
    {
      return _context.Employees.Any(e => e.EmployeeId == id);
    }
    private void UpdateEmployee(Employee DataBaseEmployee, Employee employee)
    {
      DataBaseEmployee.Name = employee.Name;
      DataBaseEmployee.JobId = employee.JobId;
      DataBaseEmployee.IsFirstAppointment = employee.IsFirstAppointment;
      DataBaseEmployee.StartDate = employee.StartDate;
      DataBaseEmployee.Notes = employee.Notes;

    }
    private Employee FitchEmployee(int id)
    {
      return _context.Employees.Include(e => e.Job).FirstOrDefault(e => e.EmployeeId == id);
    }
    private EmployeeWithJobTitleDto EmployeeWithJobTitleDto(Employee employee)
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
    private Employee EmployeeReMap(EmployeeWithJobTitleDto employeeWithJobTitleDto)
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
  }
}
