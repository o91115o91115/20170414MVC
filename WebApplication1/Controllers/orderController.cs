using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class orderController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<SelectListItem> employeedata = new List<SelectListItem>();
            Models.orderService employee = new orderService();
            foreach (var item in (List<Models.Employees>)employee.GetEmployeeData()) {
                employeedata.Add(new SelectListItem()
                {
                    Text = item.FirstName + item.LastName,
                    Value=Convert.ToString(item.EmployeeID)
                });

            }
            ViewBag.edata = employeedata;
            ViewBag.data = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(Orders data)
        {
            Models.orderService order = new Models.orderService();
            ViewBag.data = order.GetOrderById(data);
            return View();
        }
    }
}