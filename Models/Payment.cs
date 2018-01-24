using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class OrderProduct
  {
    [Key]
    public int PaymentTypeId { get; set; }

    [Required]
    public int CustomerId { get; set; }
    public CustomerId CustomerId { get; set; }

    [Required]
    public string PaymentType { get; set; }
    public PaymentType PaymentType { get; set; }

    [Required]
    public int AccountNumber { get; set; }
    public AccountNumber AccountNumber { get; set; }
  }
}