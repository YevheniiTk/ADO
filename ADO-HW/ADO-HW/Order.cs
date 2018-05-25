using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_HW
{
    public class Order
    {
        public DateTime ShippedDate { get; set; }
        public int OrderID { get; set; }
        public decimal Subtotal { get; set; }
        public string Year { get; set; }
    }
}