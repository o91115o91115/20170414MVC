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