using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
    author: Kevin Haggerty
    purpose: Department model schema for BANGAZON_DB
*/
namespace BangazonAPI.Models
{
  public class Department
  {
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    public string DepartmentName { get; set; }
    public int ExpenseBudget { get; set; }

    public ICollection<Employee> Employee;

  }
}