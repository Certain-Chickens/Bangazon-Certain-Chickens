using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class TrainingProgram
  {
    [Key]
    public int TrainingProgramId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime StartDate { get; set; }
    public string EndDate { get; set; }
    public int MaxAttendees { get; set; }
    

  }
}