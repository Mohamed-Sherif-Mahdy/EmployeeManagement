using EmployeeManagement_API.Dto;
using EmployeeManagement_API.Models;
using EmployeeManagement_API.Repository;

namespace EmployeeManagement_API.Service
{
  public class ServiceEmployee : IServiceEmployee
  {
    private readonly IRepository<Employee> _repository;
    private readonly IRepository<Job> _repositoryJob;
    public ServiceEmployee(IRepository<Employee> repository, IRepository<Job> repositoryJob)
    {
      _repository = repository;
      _repositoryJob = repositoryJob;
    }
    public void AddEmployee(Employee employee) => _repository.Insert(employee);

    public void DeleteEmployee(Employee employee) => _repository.Delete(employee);

    public bool EmployeeExists(int id) => _repository.Exists(e => e.EmployeeId == id);


    public Employee EmployeeReMap(EmployeeWithJobTitleDto employeeWithJobTitleDto)
    {
      Employee employee = new Employee();
      employee.EmployeeId = employeeWithJobTitleDto.EmployeeId;
      employee.Name = employeeWithJobTitleDto.Name;
      employee.Gender = employeeWithJobTitleDto.Gender;
      employee.IsFirstAppointment = employeeWithJobTitleDto.IsFirstAppointment;
      employee.StartDate = employeeWithJobTitleDto.StartDate;
      employee.Notes = employeeWithJobTitleDto.Notes;
      employee.JobId = _repositoryJob.GetBy(j => j.JobTitle == employeeWithJobTitleDto.JobTitle).JobId;
      return employee;
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

    public Employee FitchEmployee(int id) => GetEmployeesWithjobs().FirstOrDefault(e => e.EmployeeId == id);

    public List<Employee> GetEmployeesWithjobs() => _repository.GetWithInclude("Job");


    public void UpdateEmployee
      (Employee DataBaseEmployee, Employee employee) => _repository.Update(DataBaseEmployee, employee);

  }
}
