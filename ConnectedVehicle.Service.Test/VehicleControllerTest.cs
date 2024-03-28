using ConnectedVehicle_API.Service.Secrets;
using ConnectVehicle.Data.Interfaces;
using ConnectVehicle.Data.Models;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ConnectedVehicle.Service.Test
{
    public class VehicleControllerTest
    {
        private IVehicleRepository _vehicleRepository = null!;
        private IOptions<MyConfig> config;

        [SetUp]
        public void Setup(IOptions<MyConfig> config)
        {
            this.config = config;
            List<Vehicle> vehicles = new List<Vehicle>()
                {
                    new Vehicle{ CustomerID = 1, VIN = "YS2R4X20005399401", RegNo = "ABC123", Status = 1},
                    new Vehicle{ CustomerID = 1, VIN = "VLUR4X20009093588", RegNo = "DEF456", Status = 0},
                    new Vehicle{ CustomerID = 1, VIN = "VLUR4X20009048066", RegNo = "GHI789", Status = 1},


                    new Vehicle{ CustomerID = 2, VIN = "YS2R4X20005388011", RegNo = "JKL012", Status = 1},
                    new Vehicle{ CustomerID = 2, VIN = "YS2R4X20005387949", RegNo = "MNO345", Status = 0},

                    new Vehicle{ CustomerID = 3, VIN = "YS2R4X20005387765", RegNo = "PQR678", Status = 1},
                    new Vehicle{ CustomerID = 3, VIN = "YS2R4X20005387055", RegNo = "STU901", Status = 0}
                };

            List<Customer> customers = new List<Customer>()
                {
                    new Customer{ CustomerID = 1, Name = "Kalles Grustransporter AB", Address = "Cementvägen 8", PostCode = "111 11", City ="Södertälje"},
                    new Customer{ CustomerID = 2, Name = "Johans Bulk AB", Address = "Balkvägen 12", PostCode = "222 22", City ="Stockholm"},
                    new Customer{ CustomerID = 3, Name = "Haralds Värdetransporter AB", Address = "Budgetvägen 1", PostCode = "333 33", City ="Uppsala"}
                };
            var repMock = new Mock<IVehicleRepository>();
            repMock.Setup(r => r.GetAllCustomer(this.config.Value.AZURE_SQL_CONNECTIONSTRING)).Returns(customers);
            repMock.Setup(r => r.GetVehicles(1, this.config.Value.AZURE_SQL_CONNECTIONSTRING)).Returns(vehicles);
            repMock.Setup(r => r.GetVehicles(2, this.config.Value.AZURE_SQL_CONNECTIONSTRING)).Returns(vehicles);
            repMock.Setup(r => r.GetVehicles(3, this.config.Value.AZURE_SQL_CONNECTIONSTRING)).Returns(vehicles);
            repMock.Setup(r => r.GetVehicles(1, "1")).Returns(vehicles);
            repMock.Setup(r => r.GetVehicles(2, "1")).Returns(vehicles);
            _vehicleRepository = repMock.Object;
        }

        [Test]
        public void GetAllCustomer_Test()
        {
            // Assign
            List<Customer> customer = new List<Customer>();

            // Act
            foreach (var item in _vehicleRepository.GetAllCustomer(this.config.Value.AZURE_SQL_CONNECTIONSTRING))
            {
                customer.Add(item);
            }

            // Assert
            Assert.IsNotNull(customer);
            Assert.Greater(customer.Count, 0);
        }

        [Test]
        public void GetVehicles_Test()
        {
            // Assign
            int customerId = 1;

            // Act
            var customer = _vehicleRepository.GetVehicles(customerId, this.config.Value.AZURE_SQL_CONNECTIONSTRING);

            // Assert
            Assert.IsNotNull(customer);
        }

        [Test]
        public void GetVehiclesWithStatus_Test()
        {
            // Assign
            int customerId = 1;
            int status = 1;

            // Act
            var customer = _vehicleRepository.GetVehicles(customerId, status, this.config.Value.AZURE_SQL_CONNECTIONSTRING);

            // Assert
            Assert.IsNotNull(customer);
        }
    }
}