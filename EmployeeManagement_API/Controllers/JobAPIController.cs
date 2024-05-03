using EmployeeManagement_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class JobAPIController : ControllerBase
  {
    private readonly IServiceJob _jobs;
    public JobAPIController(IServiceJob jobs)
    {
      _jobs = jobs;
    }

    [HttpGet]
    public IActionResult GetJobTitles()
    {
      List<string> jobTitles = _jobs.GetJobs();
      return Ok(jobTitles);
    }

  }
}
