using SalesManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace SalesManagement.Data
{
    public class SaleContext : DbContext
    {
        public SaleContext(DbContextOptions<SaleContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleDetail>()
           .HasOne(p => p.SaleMaster)
           .WithMany(b => b.SaleDetails)
           .OnDelete(DeleteBehavior.Cascade);

 modelBuilder.Entity<SaleMasterReport>().HasNoKey();
            //modelBuilder.Entity<SaleMaster>()
            //      .MapToStoredProcedures();

        }
        public DbSet<SaleMaster> SaleMaster { get; set; }
        public DbSet<SaleDetail> SaleDetail { get; set; }
        public DbSet<SaleMasterReport> SaleMasterReport { get; set; }

        public async Task<List<SaleMasterReport>> GetSalesAsync(DateTime? date)
        {
            List<SaleMasterReport> sm = new List<SaleMasterReport>();
            
            if (date.HasValue)
            {
                var dateParam = new SqlParameter("@date", System.Data.SqlDbType.DateTime);
                dateParam.Value = (date.Value.ToString("dd MMM yyyy"));

                sm = await this.SaleMasterReport
                    .FromSqlRaw("exec SaleReport @date",
                              dateParam).ToListAsync();
            }
            else {               
                sm = await this.SaleMasterReport
                    .FromSqlRaw("exec SaleReport").ToListAsync();
            }
           


                if (sm.Count > 0)
                {
                    return sm;
                }


            

            return sm;
        }



    }
}