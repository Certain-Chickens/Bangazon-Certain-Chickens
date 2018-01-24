using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class PaymentTypes
  {
    [Key]
    public int PaymentTypeId { get; set; }

    [Required]
    public int CustomerId { get; set; }
    public Customers Customers { get; set; }

    [Required]
    public string PaymentType { get; set; }

    [Required]
    public int AccountNumber { get; set; }
  }
}