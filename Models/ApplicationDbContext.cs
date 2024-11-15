using Microsoft.EntityFrameworkCore;

namespace TestFinal5.Models
{
   public class ApplicationDbContext : DbContext
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {

      }

      public DbSet<Student> Students { get; set; }

   }

}
