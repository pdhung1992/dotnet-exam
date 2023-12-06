using System.ComponentModel.DataAnnotations;
using dotNet_Exam.Entities;

namespace dotNet_Exam.Models;

public class EmployeeModel
{
    public int id { get; set; }

    [Required(ErrorMessage = "Full name is required.")]
    public string fullname { get; set; }
        
    [Required(ErrorMessage = "Email is required.")]
    public string email { get; set; }
    
    [Required(ErrorMessage = "Department ID is required.")]
    public int depId { get; set; }
    public Department Department { get; set; }
}