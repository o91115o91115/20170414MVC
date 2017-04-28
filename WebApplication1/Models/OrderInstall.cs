using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderInstall
    {
        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShipperDate { get; set; }
        public string ShipperID { get; set; }
        public string Freight { get; set; }
        public string ShipCountry { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipAddress { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string UnitPrice { get; set; }
        public string Qty { get; set; }
    }
}