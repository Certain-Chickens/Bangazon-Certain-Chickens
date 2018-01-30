using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
    author: Leah Duvic
    purpose: Customer model schema for BANGAZON_DB
*/
namespace BangazonAPI.Models
{
  public class Customer
  {
    [Key]
    public int CustomerId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public ICollection<Orders> Orders;
  }
}
