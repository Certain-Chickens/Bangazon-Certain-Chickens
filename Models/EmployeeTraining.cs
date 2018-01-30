using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
    author: Kevin Haggerty
    purpose: EmployeeTraining joiner model schema for BANGAZON_DB
*/
namespace BangazonAPI.Models
{
  public class EmployeeTraining
  {
    [Key]
    public int EmployeeTrainingId { get; set; }

    [Required]
    public int TrainingProgramId { get; set; }
    public TrainingProgram TrainingProgram { get; set; }
    
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

  }
}