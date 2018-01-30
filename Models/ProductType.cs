using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
    author: Kevin Haggerty
    purpose: ProductType model schema for BANGAZON_DB
*/
namespace BangazonAPI.Models
{
  public class ProductType
  {
    [Key]
    public int ProductTypeId { get; set; }

    [Required]
    public string ProductCategory { get; set; }

    public ICollection<Product> Product;

  }
}