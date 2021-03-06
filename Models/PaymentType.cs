using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/*
    author: Kevin Haggerty
    purpose: PaymentType model schema for BANGAZON_DB
*/
namespace BangazonAPI.Models
{
  public class PaymentType
  {
    [Key]
    public int PaymentTypeId {get;set;}


    [Required]
    [StringLength(12)]
    public string Description { get; set; }

    [Required]
    [StringLength(20)]
    public string AccountNumber { get; set; }
    public int CustomerId {get;set;}
    public Customer Customer {get;set;}

    public ICollection<Orders> Orders;
  }
}
