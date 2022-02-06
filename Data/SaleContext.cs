using SalesManagement.Models;
using Microsoft.EntityFrameworkCore;
namespace SalesManagement.Data
{
    public class SaleContext : DbContext
    {
        public SaleContext(DbContextOptions<SaleContext> options) : base(options)
        {
        }

        public DbSet<SaleMaster> SaleMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleDetail>()
           .HasOne(p => p.SaleMaster)
           .WithMany(b => b.SaleDetails)
           .OnDelete(DeleteBehavior.Cascade); 
            
        }

        public DbSet<SalesManagement.Models.SaleDetail> SaleDetail { get; set; }
    }
}