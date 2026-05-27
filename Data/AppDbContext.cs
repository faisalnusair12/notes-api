using Notes_API.Models;
using Microsoft.EntityFrameworkCore;
namespace Notes_API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options)
            :base(options) 
        {
        }
        public DbSet<Note> Notes { get; set; } 
        public DbSet<User> Users { get; set; }
    }
}
