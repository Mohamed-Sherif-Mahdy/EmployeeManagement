using EmployeeManagement_API.Dto;
using EmployeeManagement_API.Models;

namespace EmployeeManagement_API.Service
{
  public interface IServiceEmployee
  {
    public bool EmployeeExists(int id);
    public void UpdateEmployee(Employee DataBaseEmployee, Employee employee);
    public Employee FitchEmployee(int id);
    public EmployeeWithJobTitleDto EmployeeWithJobTitleDto(Employee employee);
    public Employee EmployeeReMap(EmployeeWithJobTitleDto employeeWithJobTitleDto);
    public void AddEmployee(Employee employee);
    public void DeleteEmployee(Employee employee);
    public List<Employee> GetEmployeesWithjobs();


  }
}
