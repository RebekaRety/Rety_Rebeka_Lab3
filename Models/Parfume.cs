using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rety_Rebeka_Lab2.Models
{
    public class Parfume
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
