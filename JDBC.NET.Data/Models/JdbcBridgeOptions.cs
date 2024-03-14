namespace JDBC.NET.Data.Models
{
    internal sealed class JdbcBridgeOptions
    {
        public string DriverPath { get; }

        public string DriverClass { get; }

        public string[] LibraryJarFiles { get; init; }

        public string[] LibraryPath { get; set; }

        public JdbcConnectionProperties ConnectionProperties { get; init; }

        public JdbcBridgeOptions(string driverPath, string driverClass)
        {
            DriverPath = driverPath;
            DriverClass = driverClass;
        }
    }
}
