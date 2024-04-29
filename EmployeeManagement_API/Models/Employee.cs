using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement_API.Models
{
  public class Employee
  {
    [Key]
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(6)] //male or female max length is 6
    public string Gender { get; set; }

    public bool IsFirstAppointment { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public string? Notes { get; set; }

    [ForeignKey("Job")]
    public int JobId { get; set; }
    //Navigation Property
    public Job Job { get; set; }
  }
}
