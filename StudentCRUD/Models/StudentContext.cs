using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace StudentCRUD.Models
{
    public class StudentContext: DbContext
    {
        // Constructor for passing options to the DbContext
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }
        public DbSet<Student> Student { get; set; }
    }
}
