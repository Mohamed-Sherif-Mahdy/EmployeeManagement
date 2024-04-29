using EmployeeManagement_API.Dto;
using EmployeeManagement_API.Models;
using EmployeeManagement_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeeManagementAPIController : ControllerBase
  {
    IEmployeeRepository EmployeeRepository;
    IJobs jobs;
    public EmployeeManagementAPIController(IEmployeeRepository EmpRepo, IJobs JobsRePo)
    {
      EmployeeRepository = EmpRepo;
      jobs = JobsRePo;
    }

    [HttpGet]
    public IActionResult GetEmployees()
    {
      List<Employee> employees = EmployeeRepository.GetEmployees();
      List<EmployeeWithJobTitleDto> employeeWithJobTitleDtos = new List<EmployeeWithJobTitleDto>();
      foreach (Employee employee in employees)
      {
        employeeWithJobTitleDtos.Add(EmployeeRepository.EmployeeWithJobTitleDto(employee));
      }

      return Ok(employeeWithJobTitleDtos);
    }


    [HttpGet("{id}")]
    public IActionResult GetEmployee(int id)
    {
      Employee employee = EmployeeRepository.FitchEmployee(id);

      if (employee == null)
      {
        return NotFound();
      }
      return Ok(EmployeeRepository.EmployeeWithJobTitleDto(employee));
    }



    [HttpGet("JobTitles")]
    public IActionResult GetJobTitles()
    {
      List<string> jobTitles = jobs.GetJobs();
      return Ok(jobTitles);
    }


    [HttpPut("{id}")]

    public IActionResult PutEmployee(int id, EmployeeWithJobTitleDto employeeWithJobTitleDto)
    {

      if (!EmployeeRepository.EmployeeExists(id))
      {
        return NotFound();
      }
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      Employee DataBaseEmployee = EmployeeRepository.FitchEmployee(id);
      Employee employee = EmployeeRepository.EmployeeReMap(employeeWithJobTitleDto);
      EmployeeRepository.UpdateEmployee(DataBaseEmployee, employee);

      return Ok(employee);

    }



    [HttpPost]
    public IActionResult PostEmployee(EmployeeWithJobTitleDto employeeWithJobTitleDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      EmployeeRepository.AddEmployee(EmployeeRepository.EmployeeReMap(employeeWithJobTitleDto));
      return CreatedAtAction("GetEmployee", new { id = employeeWithJobTitleDto.EmployeeId }, employeeWithJobTitleDto);

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {

      Employee employee = EmployeeRepository.FitchEmployee(id);
      if (employee == null)
      {
        return NotFound();
      }
      EmployeeRepository.DeleteEmployee(employee);

      return NoContent();
    }

  }
}
