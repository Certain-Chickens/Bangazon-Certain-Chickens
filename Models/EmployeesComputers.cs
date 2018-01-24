using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class EmployeesComputers
  {
    [Key]
    public int EmployeesComputers { get; set; }

    [Required]
    public int ComputerId { get; set; }
    public Computer Computer { get; set; }

    [Required]
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    [Required]
    public string EndDate { get; set; }
  }
}