using Microsoft.EntityFrameworkCore;

namespace dotNet_Exam.Entities
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    } 
}

