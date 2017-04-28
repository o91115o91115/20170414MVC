using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class orderService
    {
        /// <summary>
        /// 取的資料庫連接字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
        }
        /// <summary>
        /// 依訂單編號取得訂單
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Models.OrderSearch> GetOrderById(Models.OrderSearch data)
        {
            DataTable dt = new DataTable();
            String sql = @"Select OrderID,CompanyName,OrderDate,ShippedDate
                           From [Sales].[Orders] as A right join [Sales].[Customers] as B on A.CustomerID=B.CustomerID
                           Where (OrderID LIKE @orderid OR @orderid='')
                           AND (CompanyName LIKE @company OR @company='')
                           AND (EmployeeID LIKE @employeeid OR @employeeid='')
                           AND (ShipperID LIKE @shipperid OR @shipperid='')
                           AND (CONVERT(varchar,OrderDate,120) LIKE @orderdate OR @orderdate='')
                           AND (CONVERT(varchar,ShippedDate,120) LIKE @shipperdate OR @shipperdate='')
                           AND (CONVERT(varchar,RequiredDate,120) LIKE @requireddate OR @requireddate='')";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@orderid", "%" + data.OrderID + "%"));
                cmd.Parameters.Add(new SqlParameter("@company", "%" + data.CompanyName + "%"));
                cmd.Parameters.Add(new SqlParameter("@employeeid", "%" + data.EmployeeID + "%"));
                cmd.Parameters.Add(new SqlParameter("@shipperid", "%" + data.ShipperID + "%"));
                cmd.Parameters.Add(new SqlParameter("@orderdate", "%" + data.OrderDate + "%"));
                cmd.Parameters.Add(new SqlParameter("@shipperdate", "%" + data.ShippedDate + "%"));
                cmd.Parameters.Add(new SqlParameter("@requireddate", "%" + data.RequiredDate + "%"));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.OrderDataToList(dt);
        }
        /// <summary>
        /// 將取出的Order資料轉型
        /// </summary>
        /// <param name="orderdata"></param>
        /// <returns></returns>
        private List<Models.OrderSearch> OrderDataToList(DataTable orderdata)
        {

            List<Models.OrderSearch> result = new List<Models.OrderSearch>();
            foreach (DataRow row in orderdata.Rows)
            {
                result.Add(new OrderSearch()
                {
                    OrderID=Convert.ToString(row["OrderID"]),
                    CompanyName=Convert.ToString(row["CompanyName"]),
                    OrderDate=Convert.ToString(row["OrderDate"]),
                    ShippedDate=Convert.ToString(row["ShippedDate"]),

                });
            }
            return result;
        }
        /// <summary>
        /// 取出員工ID、姓名
        /// </summary>
        /// <returns></returns>
        public List<Models.Employees> GetEmployeeData()
        {
            DataTable dt = new DataTable();
            String sql = @"Select distinct  B.[EmployeeID],[FirstName],[LastName]
                           From [Sales].[Orders] A right join [HR].[Employees] B on A.EmployeeID=B.EmployeeID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.EmployeeDataToList(dt);
        }
        /// <summary>
        /// 將取出的Emplyee資料轉型
        /// </summary>
        /// <param name="orderdata"></param>
        /// <returns></returns>
        private List<Models.Employees> EmployeeDataToList(DataTable orderdata)
        {

            List<Models.Employees> result = new List<Models.Employees>();
            foreach (DataRow row in orderdata.Rows)
            {
                result.Add(new Models.Employees()
                {
                    EmployeeID=Convert.ToInt32(row["EmployeeID"]),
                    FirstName=Convert.ToString(row["FirstName"]),
                    LastName = Convert.ToString(row["LastName"]),
                });
            }
            return result;
        }
        /// <summary>
        /// 取出出貨公司ID、名稱
        /// </summary>
        /// <returns></returns>
        public List<Models.Shippers> GetShipperData()
        {
            DataTable dt = new DataTable();
            String sql = @"select distinct A.ShipperID,[CompanyName]
                           from [Sales].[Shippers] as A left join [Sales].[Orders] as B on A.ShipperID=B.ShipperID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.ShipperDataToList(dt);
        }
        /// <summary>
        /// 將取出的Shipper資料轉型
        /// </summary>
        /// <param name="orderdata"></param>
        /// <returns></returns>
        private List<Models.Shippers> ShipperDataToList(DataTable orderdata)
        {

            List<Models.Shippers> result = new List<Models.Shippers>();
            foreach (DataRow row in orderdata.Rows)
            {
                result.Add(new Models.Shippers()
                {
                    ShipperID=Convert.ToInt32(row["ShipperID"]),
                    CompanyName=Convert.ToString(row["CompanyName"]),
                });
            }
            return result;
        }
        /// <summary>
        /// 取出顧客ID、名稱
        /// </summary>
        /// <returns></returns>
        public List<Models.Customers> GetCustomerData()
        {
            DataTable dt = new DataTable();
            String sql = @"Select [CustomerID],[CompanyName]
                           From [Sales].[Customers]";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.CustomerDataToList(dt);
        }
        /// <summary>
        /// 將取出的Customer資料轉型
        /// </summary>
        /// <param name="orderdata"></param>
        /// <returns></returns>
        private List<Models.Customers> CustomerDataToList(DataTable orderdata)
        {

            List<Models.Customers> result = new List<Models.Customers>();
            foreach (DataRow row in orderdata.Rows)
            {
                result.Add(new Models.Customers()
                {
                    CustomerID=Convert.ToInt32(row["CustomerID"]),
                    CompanyName=Convert.ToString(row["CompanyName"]),
                });
            }
            return result;
        }
        /// <summary>
        /// 取出產品ID、單價、名稱
        /// </summary>
        /// <returns></returns>
        public List<Models.Products> GetProductData()
        {
            DataTable dt = new DataTable();
            String sql = @"Select [ProductID],[ProductName],[UnitPrice]
                           From [Production].[Products]";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.ProductDataToList(dt);
        }
        /// <summary>
        /// 將取出的Prouct資料轉型
        /// </summary>
        /// <param name="orderdata"></param>
        /// <returns></returns>
        private List<Models.Products> ProductDataToList(DataTable orderdata)
        {

            List<Models.Products> result = new List<Models.Products>();
            foreach (DataRow row in orderdata.Rows)
            {
                result.Add(new Models.Products()
                {
                    ProductID=Convert.ToInt32(row["ProductID"]),
                    ProductName=Convert.ToString(row["ProductName"]),
                    UnitPrice=Convert.ToInt32(row["UnitPrice"]),
                });
            }
            return result;
        }
    }
}