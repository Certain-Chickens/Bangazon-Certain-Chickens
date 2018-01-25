using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class PaymentType
  {
    [Key]
    public int PaymentTypeId { get; set; }

    [Required]
    public int CustomerId { get; set; }
    public Customer Customers { get; set; }

    [Required]
    public string PaymentName { get; set; }

    [Required]
    public int AccountNumber { get; set; }
  }
}