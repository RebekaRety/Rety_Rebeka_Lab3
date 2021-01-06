using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rety_Rebeka_Lab2.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int ParfumeID { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public Parfume Parfume { get; set; }
    }
}
