using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class Orders
  {
    [Key]
    public int OrdersId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated { get; set; }

    [Required]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int? PaymentTypeId { get; set;} // ? means that the variable can be null
    public PaymentType PaymentType { get; set; }

    public ICollection<OrderProduct> OrderProduct;

  }
}