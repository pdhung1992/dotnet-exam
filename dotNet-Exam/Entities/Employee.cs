using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNet_Exam.Entities
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        public int id { get; set; }
        
        [Required(ErrorMessage = "Full name is required.")]
        public string fullname { get; set; }
        
        [Required(ErrorMessage = "Email is required.")]
        public string email { get; set; }
        
        public int depId { get; set; }
        [ForeignKey("depId")]
        public Department Department { get; set; }
        
    }
}

