using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
                
        }
        public DbSet<Student> Students { get; set; }
    }
}
