using EmployeeManagement_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class JobAPIController : ControllerBase
  {
    IJobs jobs;
    public JobAPIController(IJobs JobsRePo)
    {
      jobs = JobsRePo;
    }

    [HttpGet]
    public IActionResult GetJobTitles()
    {
      List<string> jobTitles = jobs.GetJobs();
      return Ok(jobTitles);
    }

  }
}
