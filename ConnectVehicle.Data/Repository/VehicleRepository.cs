using ConnectVehicle.Data.Interfaces;
using ConnectVehicle.Data.Models;
using System.Data;
using System.Data.SqlClient;

namespace ConnectVehicle.Data.Repository
{
    public class VehicleRepository : IVehicleRepository
    {   
        #region Data
        /// <summary>
        /// Assign value
        /// </summary>
        public List<Vehicle> vehicles = new List<Vehicle>()
        {
            new Vehicle{ CustomerID = 1, VIN = "YS2R4X20005399401", RegNo = "ABC123", Status = 1},
            new Vehicle{ CustomerID = 1, VIN = "VLUR4X20009093588", RegNo = "DEF456", Status = 0},
            new Vehicle{ CustomerID = 1, VIN = "VLUR4X20009048066", RegNo = "GHI789", Status = 1},


            new Vehicle{ CustomerID = 2, VIN = "YS2R4X20005388011", RegNo = "JKL012", Status = 1},
            new Vehicle{ CustomerID = 2, VIN = "YS2R4X20005387949", RegNo = "MNO345", Status = 0},

            new Vehicle{ CustomerID = 3, VIN = "YS2R4X20005387765", RegNo = "PQR678", Status = 1},
            new Vehicle{ CustomerID = 3, VIN = "YS2R4X20005387055", RegNo = "STU901", Status = 0}
        };

        public List<Customer> customers = new List<Customer>()
        {
            new Customer{ CustomerID = 1, Name = "Kalles Grustransporter AB", Address = "Cementvägen 8", PostCode = "111 11", City ="Södertälje"},
            new Customer{ CustomerID = 2, Name = "Johans Bulk AB", Address = "Balkvägen 12", PostCode = "222 22", City ="Stockholm"},
            new Customer{ CustomerID = 3, Name = "Haralds Värdetransporter AB", Address = "Budgetvägen 1", PostCode = "333 33", City ="Uppsala"}
        };

        #endregion

        /// <summary>
        /// Get list of vehicles without any contraints
        /// </summary>
        /// <returns>returns list of vehicles</returns>
        public List<Customer> GetAllCustomer(string connStr)
        {
            return GetCustomerDataFromSql(connStr);
            //return AssignVehicleList();
        }

        /// <summary>
        /// Get list of vehicles who belongs to
        /// </summary>
        /// <param name="customerID">passing parameter customer id</param>
        /// <returns>returns list of vehicles</returns>
        public List<Vehicle>? GetVehicles(int customerID, string connStr)
        {
            return GetVehicleDataFromSql(customerID, connStr);
        }

        /// <summary>
        /// Get list of vehicles based on status
        /// </summary>
        /// <param name="customerID">passing parameter customer id</param>
        /// <param name="status">passing parameter status</param>
        /// <returns>returns list of vehicles</returns>
        public List<Vehicle>? GetVehicles(int customerID, int status, string connStr)
        {
            List<Vehicle> customers = GetVehicleDataFromSql(customerID, connStr);

            var vehicle = customers.Where(v => v.Status == status).ToList();

            return vehicle;
        }

        private List<Customer> GetCustomerDataFromSql(string connStr)
        {
            string sqlQuery = "Select *  FROM [dbo].[tblCustomer]";
            List<Customer> customer = SQLDBConnect.GetCustomerData(sqlQuery, connStr);
            return customer;
        }

        private List<Vehicle> GetVehicleDataFromSql(int customerID, string connStr)
        {
            string sqlQuery = "Select *  FROM [dbo].[tblVehicle]";
            List<Vehicle> vehicleList = SQLDBConnect.GetVehicleData(sqlQuery, customerID, connStr);
            return vehicleList;
        }
    }
}
