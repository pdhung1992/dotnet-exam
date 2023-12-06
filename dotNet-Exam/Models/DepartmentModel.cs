using System.ComponentModel.DataAnnotations;

namespace dotNet_Exam.Models;

public class DepartmentModel
{
    public int id { get; set; }
    
    [Required(ErrorMessage = "Department name is required.")]
    [MinLength(5, ErrorMessage = "Department name must at least 5 characters.")]
    public string name { get; set; }
    
}