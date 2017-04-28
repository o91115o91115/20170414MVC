using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderSearch
    {
        public string OrderID { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeID { get; set; }
        public string ShipperID { get; set; }
        public string OrderDate { get; set; }
        public string ShippedDate { get; set; }
        public string RequiredDate { get; set; }

    }
}