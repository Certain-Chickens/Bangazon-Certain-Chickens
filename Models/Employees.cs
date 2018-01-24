using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class Employees
  {
    [Key]
    public int EmployeesId { get; set; }

    [Required]
    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public bool Supervisor { get; set; }
    
    public ICollection<EmployeesComputers> EmployeesComputers;
    public ICollection<EmployeeTrainings> EmployeeTrainings;

  }
}