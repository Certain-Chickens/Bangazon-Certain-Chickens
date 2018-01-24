using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class Computer
  {
    [Key]
    public int ComputerId { get; set; };

    [Required]
    public string PurchaseDate { get; set; ;}

    public string DecommissionDate { get; set; };

    public ICollection<EmployeesComputers> EmployeesComputers;
  }
}