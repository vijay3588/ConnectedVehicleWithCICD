using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using ConnectedVehicle_API.Service.Secrets;
using ConnectVehicle.Data.Interfaces;
using ConnectVehicle.Data.Models;
using ConnectVehicle.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;

namespace ConnectedVehicleAPI.Service.Controllers
{
    [Authorize(Roles = "API.ReadWrite")]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IOptions<MyConfig> config;
        private readonly IConfiguration configuration;
        private readonly IVehicleRepository _vehicleRepository;
        
        //var credential = new ClientSecretCredential(this.keyVaultConfig, clientId, clientSecret);
        //var client = new SecretClient(new Uri(kvURL), credential);

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="vehicleRepository"></param>
        public VehicleController(IVehicleRepository vehicleRepository, IOptions<MyConfig> config, IConfiguration configuration)
        {
            _vehicleRepository = vehicleRepository;
            this.config = config;
            this.configuration = configuration;
        }

        /// <summary>
        /// Get all customers and vehicle details
        /// </summary>
        /// <returns>return list of customers</returns>
        [HttpGet("GetAllCustomers")]
        public ActionResult<List<Customer>> Get()
        {
            // Get all customer along with respective vehicla details
            Logger.Info("Get all customer along with respective vehicla details");
            var vehicleList = _vehicleRepository.GetAllCustomer(this.configuration["ConnectedVehicleConnectionString"]);

            if (vehicleList == null)
            {
                Logger.Info("Not Found");
                return NotFound();
            }

            Logger.Info("Respond 'OK'. Got list of customer and vehicle");
            return Ok(vehicleList);
        }

        /// <summary>
        /// Get vehicle details and filtering with customer id and vehicle status
        /// </summary>
        /// <param name="customerID">passing customer id</param>
        /// <param name="status">passing vehicle status</param>
        /// <returns>return list of vehicle</returns>
        [HttpGet("GetVehicles/{customerID}/{status}")]
        public ActionResult<List<Vehicle>> GetVehicles(int customerID, int status)
        {
            Logger.Info("Request GetVehicles. Paasing Params Customer ID - " + customerID + "; Status- " + status);
            var vehicleList = _vehicleRepository.GetVehicles(customerID, status, this.config.Value.AZURE_SQL_CONNECTIONSTRING);

            if (vehicleList == null)
            {
                Logger.Info("Not Found");
                return NotFound();
            }

            Logger.Info("Respond 'OK'. Got list of customer and vehicle");
            return Ok(vehicleList);
        }

        /// <summary>
        /// Get vehicle details filtering by customer id
        /// </summary>
        /// <param name="customerID">passing customer id</param>
        /// <returns>return list of vehicle</returns>
        [HttpGet("GetVehicles/{customerID}")]
        public ActionResult<List<Vehicle>> GetVehicles(int customerID)
        {
            Logger.Info("Request GetVehicles. Paasing Params Customer ID - " + customerID);
            var vehicleList = _vehicleRepository.GetVehicles(customerID, this.config.Value.AZURE_SQL_CONNECTIONSTRING);

            if (vehicleList == null)
            {
                Logger.Info("Not Found");
                return NotFound();
            }

            Logger.Info("Respond 'OK'. Got list of customer and vehicle");
            return Ok(vehicleList);
        }
    }
}
