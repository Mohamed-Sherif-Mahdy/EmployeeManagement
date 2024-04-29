using EmployeeManagement_API.Data;

namespace EmployeeManagement_API.Repository
{
  public class JobsRepository : IJobs
  {
    private readonly EmployeeManagemendtDbContext _context;

    public JobsRepository(EmployeeManagemendtDbContext context)
    {
      _context = context;
    }

    public List<string> GetJobs()
    {
      List<string> jobTitles = _context.Jobs.Select(j => j.JobTitle).ToList();
      return jobTitles;
    }
  }
}
