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
        public List<Models.Orders> GetOrderById(Models.Orders data)
        {
            DataTable dt = new DataTable();
            String sql = @"Select * From [Sales].[Orders] Where (OrderID=@orderid OR @orderid='') AND (CustomerID=@customerid OR @customerid='') AND (EmployeeID=@employeeid OR @employeeid='') ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@orderid", data.OrderID));
                cmd.Parameters.Add(new SqlParameter("@customerid", data.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@employeeid", data.EmployeeID));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapOrderDataToList(dt);
        }
        /// <summary>
        /// 將取出的Order資料轉型
        /// </summary>
        /// <param name="orderdata"></param>
        /// <returns></returns>
        private List<Models.Orders> MapOrderDataToList(DataTable orderdata)
        {

            List<Models.Orders> result = new List<Models.Orders>();
            foreach (DataRow row in orderdata.Rows)
            {
                result.Add(new Orders()
                {
                    OrderID = Convert.ToInt32(row["OrderID"]),
                    CustomerID = Convert.ToInt32(row["CustomerID"]),
                    EmployeeID = Convert.ToInt32(row["EmployeeID"]),

                });
            }
            return result;
        }
        /// <summary>
        /// 取出員工ID，姓名
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
    }
}