using EmployeeManagement_API.Models;
using EmployeeManagement_API.Repository;

namespace EmployeeManagement_API.Service
{
  public class ServiceJob : IServiceJob
  {
    private readonly IRepository<Job> _jobs;
    public ServiceJob(IRepository<Job> jobs)
    {
      _jobs = jobs;
    }
    public List<string> GetJobs()
    {
      return _jobs.GetAll().Select(j => j.JobTitle).ToList();
    }
  }
}
