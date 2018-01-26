using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
  }
}
