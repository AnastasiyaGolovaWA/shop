using System.Data.Entity;

namespace shop.Models
{
    public class ComputerContext : DbContext
    {
     
        public DbSet < Computer > Computers { get; set; }
        public DbSet < Purchase > Purchases { get; set; }
     
    }
}