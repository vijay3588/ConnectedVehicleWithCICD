/// <summary>
/// Model Class for Customer entity
/// </summary>
/// 

namespace ConnectVehicle.Data.Models
{
    public class Customer
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Customer Address
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Postcode
        /// </summary>
        public string? PostCode { get; set; }

        /// <summary>
        ///  Customer City
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// List of Vehicle entity
        /// </summary>
        public List<Vehicle>? Vehicles { get; set; }
    }
}
