using EmployeeManagement_API.Dto;
using EmployeeManagement_API.Models;
using EmployeeManagement_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeeManagementAPIController : ControllerBase
  {
    //IEmployeeRepository EmployeeRepository;
    //IJobs jobs;
    //public EmployeeManagementAPIController(IEmployeeRepository EmpRepo, IJobs JobsRePo)
    //{
    //  EmployeeRepository = EmpRepo;
    //  jobs = JobsRePo;
    //}
    private readonly IServiceEmployee EmployeeService;
    private readonly IServiceJob JobService;
    //private readonly IRepository<Job> jobs;
    public EmployeeManagementAPIController(IServiceEmployee employeeRepository, IServiceJob jobService)
    {
      EmployeeService = employeeRepository;
      JobService = jobService;
    }

    [HttpGet]
    public IActionResult GetEmployees()
    {
      List<Employee> employees = EmployeeService.GetEmployeesWithjobs();
      List<EmployeeWithJobTitleDto> employeeWithJobTitleDtos = new List<EmployeeWithJobTitleDto>();
      foreach (Employee employee in employees)
      {
        employeeWithJobTitleDtos.Add(EmployeeService.EmployeeWithJobTitleDto(employee));
      }

      return Ok(employeeWithJobTitleDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployee(int id)
    {
      Employee employee = EmployeeService.FitchEmployee(id);

      if (employee == null)
      {
        return NotFound();
      }
      return Ok(EmployeeService.EmployeeWithJobTitleDto(employee));
    }

    [HttpPut("{id}")]
    public IActionResult PutEmployee(int id, EmployeeWithJobTitleDto employeeWithJobTitleDto)
    {

      if (!EmployeeService.EmployeeExists(id))
      {
        return NotFound();
      }
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      Employee DataBaseEmployee = EmployeeService.FitchEmployee(id);
      Employee employee = EmployeeService.EmployeeReMap(employeeWithJobTitleDto);
      employee.EmployeeId = DataBaseEmployee.EmployeeId;
      EmployeeService.UpdateEmployee(DataBaseEmployee, employee);

      return Ok(employee);

    }



    [HttpPost]
    public IActionResult PostEmployee(EmployeeWithJobTitleDto employeeWithJobTitleDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      EmployeeService.AddEmployee(EmployeeService.EmployeeReMap(employeeWithJobTitleDto));
      return CreatedAtAction("GetEmployee", new { id = employeeWithJobTitleDto.EmployeeId }, employeeWithJobTitleDto);

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {

      Employee employee = EmployeeService.FitchEmployee(id);
      if (employee == null)
      {
        return NotFound();
      }
      EmployeeService.DeleteEmployee(employee);

      return NoContent();
    }

  }
}
