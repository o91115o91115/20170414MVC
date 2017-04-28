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
        /// <summary>
        /// 一開始Index網頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            List<SelectListItem> employeedata = new List<SelectListItem>();
            Models.orderService employee = new orderService();
            employeedata.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = ""
            });
            foreach (var item in (List<Models.Employees>)employee.GetEmployeeData()) {
                employeedata.Add(new SelectListItem()
                {
                    Text = item.FirstName + item.LastName,
                    Value = Convert.ToString(item.EmployeeID)
                });
            }

            List<SelectListItem> shipperdata = new List<SelectListItem>();
            Models.orderService shipper = new orderService();
            shipperdata.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = ""
            });
            foreach (var item in (List<Models.Shippers>)shipper.GetShipperData())
            {
                shipperdata.Add(new SelectListItem()
                {
                    Text = item.CompanyName,
                    Value = Convert.ToString(item.ShipperID)
                });
            }

            ViewBag.edata = employeedata;
            ViewBag.sdata = shipperdata;
            ViewBag.data = null;
            return View();
        }
        /// <summary>
        /// 顯示搜尋後結果
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(OrderSearch data)
        {
            List<SelectListItem> employeedata = new List<SelectListItem>();
            Models.orderService employee = new orderService();
            employeedata.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = ""
            });
            foreach (var item in (List<Models.Employees>)employee.GetEmployeeData())
            {
                employeedata.Add(new SelectListItem()
                {
                    Text = item.FirstName + item.LastName,
                    Value = Convert.ToString(item.EmployeeID)
                });
            }

            List<SelectListItem> shipperdata = new List<SelectListItem>();
            Models.orderService shipper = new orderService();
            shipperdata.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = ""
            });
            foreach (var item in (List<Models.Shippers>)shipper.GetShipperData())
            {
                shipperdata.Add(new SelectListItem()
                {
                    Text = item.CompanyName,
                    Value = Convert.ToString(item.ShipperID)
                });
            }

            ViewBag.edata = employeedata;
            ViewBag.sdata = shipperdata;

            Models.orderService order = new Models.orderService();
            ViewBag.data = order.GetOrderById(data);
            return View();
        }

        /// <summary>
        /// 顯示Install視窗
        /// </summary>
        /// <returns></returns>
        public ActionResult Install()
        {
            List<SelectListItem> customerdata = new List<SelectListItem>();
            Models.orderService customer = new orderService();
            foreach (var item in (List<Models.Customers>)customer.GetCustomerData())
            {
                customerdata.Add(new SelectListItem()
                {
                    Text = item.CompanyName,
                    Value = Convert.ToString(item.CustomerID)
                });
            }

            List<SelectListItem> employeedata = new List<SelectListItem>();
            Models.orderService employee = new orderService();
            foreach (var item in (List<Models.Employees>)employee.GetEmployeeData())
            {
                employeedata.Add(new SelectListItem()
                {
                    Text = item.FirstName + item.LastName,
                    Value = Convert.ToString(item.EmployeeID)
                });
            }

            List<SelectListItem> shipperdata = new List<SelectListItem>();
            Models.orderService shipper = new orderService();
            foreach (var item in (List<Models.Shippers>)shipper.GetShipperData())
            {
                shipperdata.Add(new SelectListItem()
                {
                    Text = item.CompanyName,
                    Value = Convert.ToString(item.ShipperID)
                });
            }

            Models.orderService product = new orderService();
            ViewBag.productdata = product.GetProductData();

            List<SelectListItem> productdata = new List<SelectListItem>();
            foreach (var item in (List<Models.Products>)product.GetProductData())
            {
                shipperdata.Add(new SelectListItem()
                {
                    Text = item.ProductName,
                    Value = Convert.ToString(item.ProductID)
                });
            }

            ViewBag.cdata = customerdata;
            ViewBag.edata = employeedata;
            ViewBag.sdata = shipperdata;
            ViewBag.pdata = productdata;
            TempData["id"] = "";
            return View();
        }
        public ActionResult Updata(string updataid)
        {

            ViewBag.id = updataid;
            return View();
        }
    }
}