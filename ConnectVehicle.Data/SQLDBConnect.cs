using ConnectVehicle.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectVehicle.Data
{
    public static class SQLDBConnect
    {
        public static IEnumerable<T> Select<T>(this IDataReader reader,
                                       Func<IDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }

        /// <summary>
        /// Read Customer Data from Azure SQL Server
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public static List<Customer> GetCustomerData(string sqlQuery, string connStr)
        {
            using (var connection = new SqlConnection(connStr))
            {
                List<Customer> customerList = null;
                using (SqlCommand sqlcommand = new SqlCommand(sqlQuery, connection))
                {
                    //Open the connection
                    connection.Open();
                    //Execute the reader function to read the information
                    using (IDataReader reader = sqlcommand.ExecuteReader())
                    {
                        customerList = reader.Select<Customer>(cus => new Customer()
                        {
                            CustomerID = Convert.ToInt32(cus.GetInt64(0)),
                            Name = cus.GetString(1),
                            Address = cus.GetString(2),
                            City = cus.GetString(3),
                            PostCode = cus.GetString(4),
                        }).ToList();
                        return customerList;
                    }
                }
            }
        }

        /// <summary>
        /// Read Vehicle Data from Azure SQL Server
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public static List<Vehicle> GetVehicleData(string sqlQuery, int customerID, string connStr)
        {
            using (var connection = new SqlConnection(connStr))
            {
                List<Vehicle> vehicleList = null;
                using (SqlCommand sqlcommand = new SqlCommand(sqlQuery, connection))
                {
                    //Open the connection
                    connection.Open();
                    //Execute the reader function to read the information
                    using (IDataReader reader = sqlcommand.ExecuteReader())
                    {
                        vehicleList = reader.Select<Vehicle>(veh => new Vehicle()
                        {
                            Vehicle_ID = Convert.ToInt32(veh.GetInt64(0)),
                            VIN = veh.GetString(1),
                            RegNo = veh.GetString(2),
                            //Status = veh.GetInt32(3),
                            CustomerID = Convert.ToInt32(veh.GetInt64(4)),
                        }).Where<Vehicle>(c => c.CustomerID == customerID).ToList();
                        //}).ToList();
                        return vehicleList;
                    }
                }
            }
        }
    }
}
