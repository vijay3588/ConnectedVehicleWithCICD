/// <summary>
/// Model Class for Vehicle entity
/// </summary>
namespace ConnectVehicle.Data.Models
{
    public class Vehicle
    {
        /// <summary>
        /// 
        /// </summary>
        public int Vehicle_ID { get; set; }

        /// <summary>
        /// Vehicle ID
        /// </summary>
        public string? VIN { get; set; }

        /// <summary>
        /// Registration Number
        /// </summary>
        public string? RegNo { get; set; }

        /// <summary>
        /// Vehicle Status
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        public int CustomerID { get; set; }
    }
}
