using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_API.Dto
{
  public class EmployeeWithJobTitleDto
  {
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
    public string JobTitle { get; set; }


  }
}
