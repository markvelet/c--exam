using Microsoft.EntityFrameworkCore;
namespace ExamOne.Models
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options) : base(options){}
        public DbSet<User> Users {get;set;}
        public DbSet<Idea> Ideas {get;set;}
        public DbSet<Like> Likes {get;set;}
    }
}