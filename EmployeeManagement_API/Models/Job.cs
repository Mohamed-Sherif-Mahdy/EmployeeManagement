using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_API.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [StringLength(100)]
        public string JobTitle { get; set; }

        //Navigation Property
        public List<Employee> Employees { get; set; }
    }
}
