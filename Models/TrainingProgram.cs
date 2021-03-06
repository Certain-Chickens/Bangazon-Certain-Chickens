using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
    author: Kevin Haggerty
    purpose: TrainingProgram model schema for BANGAZON_DB
*/
namespace BangazonAPI.Models
{
  public class TrainingProgram
  {
    [Key]
    public int TrainingProgramId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }
    public string EndDate { get; set; }
    public int MaxAttendees { get; set; }
    

  }
}