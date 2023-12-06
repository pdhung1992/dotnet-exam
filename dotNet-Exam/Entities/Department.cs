using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNet_Exam.Entities;

[Table("departments")]
public class Department
{
    [Key]
    public int id { get; set; }
    
    [Required(ErrorMessage = "Department name is required.")]
    public string name { get; set; }
}