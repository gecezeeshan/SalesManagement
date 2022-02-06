using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesManagement.Models;
using System;
using System.Linq;

namespace SalesManagement.Data
{
    public static class DbInitializer
    {

        public static void Initialize(IApplicationBuilder app)
        {
            SaleContext context = app.ApplicationServices
               .CreateScope().ServiceProvider.GetRequiredService<SaleContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            string[] items = { "Bed", "Sofa", "Cabinet", "Chairs", "Side table", "Wardrobe", "Stool", "Dinning table" };

            string[] customers = { "Richard", "Jeff Prosise", "Dave McCarter", "Allen O'neill",
                        "Monica Paul", "Henry Fin", "Jeremy Fernandis", "Mark Prime","Rose Tracey", "Mike Crown" };
            Random rand = new Random();
            if (!context.SaleMaster.Any())
            {
                context.Database.EnsureCreated();
                for (int i = 0; i < 10; i++)
                {





                    int index = rand.Next(customers.Length);

                    var sales = new SaleMaster[]
                                   {
                new SaleMaster{Customer=customers[index],Date=DateTime.Now, Tax = 1, Total = 10}

                                   };
                    foreach (SaleMaster s in sales)
                    {
                        context.SaleMaster.Add(s);


                        var qty = rand.Next(10);

                        for (int j = 0; j < qty; j++)
                        {
                            var number = rand.Next(items.Length);
                            var sd = new SaleDetail
                            {
                                SaleMaster = s,
                                ItemName = items[number],
                                ItemNo = "000" + number.ToString(),
                                Price = number,
                                QTY = number,
                                Tax = 5
                            };
                            context.SaleDetail.Add(sd);
                        }

                    }
                }

                context.SaveChanges();
            }
        }
        public static void Initialize(SaleContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.SaleMaster.Any())
            {
                return;   // DB has been seeded
            }



            var sales = new SaleMaster[]
            {
                new SaleMaster{Customer="Alexander",Date=DateTime.Now, Tax = 1, Total = 10}

            };
            foreach (SaleMaster s in sales)
            {
                context.SaleMaster.Add(s);

                var saleDetails = new SaleDetail[] {

                 new SaleDetail{ SaleMaster = s, ItemName = "Abc", ItemNo = "1", Price = 2, QTY = 4, Tax = 5 },
                 new SaleDetail{ SaleMaster = s,ItemName = "Abc", ItemNo = "1", Price = 2, QTY = 4, Tax = 5 },
                 new SaleDetail{ SaleMaster = s,ItemName = "Abc", ItemNo = "1", Price = 2, QTY = 4, Tax = 5 },
                new SaleDetail{ SaleMaster = s,ItemName = "Abc", ItemNo = "1", Price = 2, QTY = 4, Tax = 5 },
                 new SaleDetail{SaleMaster = s, ItemName = "Abc", ItemNo = "1", Price = 2, QTY = 4, Tax = 5 },
                 new SaleDetail{ SaleMaster = s,ItemName = "Abc", ItemNo = "1", Price = 2, QTY = 4, Tax = 5 },
                 new SaleDetail{ SaleMaster = s,ItemName = "Abc", ItemNo = "1", Price = 2, QTY = 4, Tax = 5 }

            };

                context.SaleDetail.AddRange(saleDetails);

            }
            context.SaveChanges();
        }
    }
}
