using Microsoft.EntityFrameworkCore;

namespace RestWithAspNetCore5.Model
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        { 
        
        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options){}
        public DbSet<Person> Persons { get; set;}


    }
}
