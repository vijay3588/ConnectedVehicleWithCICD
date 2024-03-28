/// <summary>
/// Interface for Vehicle 
/// </summary>
/// 
using ConnectVehicle.Data.Models;

namespace ConnectVehicle.Data.Interfaces
{
    public interface IVehicleRepository
    {
        /// <summary>
        /// Get list of vehicles without any contraints
        /// </summary>
        /// <returns>returns list of vehicles</returns>
        List<Customer> GetAllCustomer(string connStr);

        /// <summary>
        /// Get list of vehicles who belongs to
        /// </summary>
        /// <param name="customerID">passing parameter customer id</param>
        /// <returns>returns list of vehicles</returns>
        List<Vehicle>? GetVehicles(int customerID, string connStr);

        /// <summary>
        /// Get list of vehicles based on status
        /// </summary>
        /// <param name="customerID">passing parameter customer id</param>
        /// <param name="status">passing parameter status</param>
        /// <returns>returns list of vehicles</returns>
        List<Vehicle>? GetVehicles(int customerID, int status, string connStr);
    }
}
