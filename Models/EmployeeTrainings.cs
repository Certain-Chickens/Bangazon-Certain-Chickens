using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class EmployeeTrainings
  {
    [Key]
    public int EmployeeTrainingsId { get; set; }

    [Required]
    public int TrainingProgramId { get; set; }
    public int EmployeeId { get; set; }

  }
}