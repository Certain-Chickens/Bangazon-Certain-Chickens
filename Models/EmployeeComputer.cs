using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
    author: Kevin Haggerty
    purpose: EmployeeComputer joiner model schema for BANGAZON_DB
*/
namespace BangazonAPI.Models
{
  public class EmployeeComputer
  {
    [Key]
    public int EmployeeComputerId { get; set; }

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