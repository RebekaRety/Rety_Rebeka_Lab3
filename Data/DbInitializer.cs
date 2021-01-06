using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rety_Rebeka_Lab2.Models;

namespace Rety_Rebeka_Lab2.Data
{
    public class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            context.Database.EnsureCreated();
            if (context.Parfumes.Any())
            {
                return; // BD a fost creata anterior
            }
            var parfumes = new Parfume[]
            {
                 new Parfume{Name="Bright Crystal", Brand="Versace", Price=Decimal.Parse("260")},
                 new Parfume{Name="La Vie est Belle", Brand="Lancome", Price=Decimal.Parse("286")},
                 new Parfume{Name="Dior Addict", Brand="Dior", Price=Decimal.Parse("428")}
            };
            foreach (Parfume s in parfumes)
            {
                context.Parfumes.Add(s);
            }
            context.SaveChanges();

            var customers = new Customer[]
            {

                 new Customer{CustomerID=1050,Name="Clara Smith",Adress="Mainstreet, 1", BirthDate=DateTime.Parse("1989-07-19")},
                 new Customer{CustomerID=1045,Name="John Barn", Adress="Oxford, 34", BirthDate=DateTime.Parse("1980-07-18")},
                 new Customer{CustomerID=1047,Name="Julia Klein", Adress="Bigstreet, 98",BirthDate=DateTime.Parse("1965-09-28")},

            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
                 new Order{ParfumeID=1,CustomerID=1050},
                 new Order{ParfumeID=3,CustomerID=1045},
                 new Order{ParfumeID=1,CustomerID=1045},
                 new Order{ParfumeID=2,CustomerID=1050},
                 new Order{ParfumeID=2,CustomerID=1050},
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();
        }
    }
}
