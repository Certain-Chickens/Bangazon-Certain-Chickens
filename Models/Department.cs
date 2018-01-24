using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class Department
  {
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; }
    
    public int ExpenseBudget { get; set; }
    
    // public ICollection<OrderProduct> OrderProducts;

  }
}