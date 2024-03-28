namespace ConnectedVehicle_API.Service.Secrets
{
    public class MyConfig
    {
        public string AZURE_SQL_CONNECTIONSTRING { get; set; }
    }

    public class KeyVaultConfig
    {
        public string ConnectionStringConnectedVehicle { get; set; }
    }
}
