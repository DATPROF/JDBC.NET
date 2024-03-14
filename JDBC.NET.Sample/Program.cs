using System;
using System.Data.Common;
using JDBC.NET.Data;

namespace JDBC.NET.Sample
{
    internal class Program
    {
        private const string DriversPath = @"C:\Users\bb\projects\datprof-applications\Privacy\Privacy\bin\Debug\win-x64\Drivers";
        private const string DriverJar = @"C:\Users\bb\projects\datprof-applications\Privacy\Privacy\bin\Debug\win-x64\Drivers\mssql-jdbc-12.4.2.jre11.jar";
        private const string DriverClass = "com.microsoft.sqlserver.jdbc.SQLServerDriver";
        private const string JdbcUrl = "jdbc:sqlserver://dpf-sqlserver-2\\MSSQL2022:2233;integratedSecurity=true;trustServerCertificate=true";
        // private const string JdbcUrl = "jdbc:sqlserver://dpf-docker-1:1499;user=BB_T01;;password=BB_T01;integratedSecurity=false;trustServerCertificate=true;database=BB_T01";

        private static void Main(string[] args)
        {
            Console.WriteLine($"Driver Path : {DriverJar}");
            Console.WriteLine($"Driver Class : {DriverClass}");
            Console.WriteLine($"JDBC Url : {JdbcUrl}");

            var builder = new JdbcConnectionStringBuilder
            {
                FetchSize = -1,
                DriverPath = DriverJar,
                DriverClass = DriverClass,
                JdbcUrl = JdbcUrl,
                LibaryPath = new []{DriversPath}
            };

            try
            {
                Console.WriteLine("Connecting...");
                using var connection = new JdbcConnection(builder);
                connection.Open();
                Console.WriteLine("Connection established...");

                while (true)
                {
                    Console.Write("SQL > ");

                    var sql = Console.ReadLine();

                    using var command = connection.CreateCommand(sql);

                    try
                    {
                        using var reader = command.ExecuteReader();
                        ConsoleUtils.PrintResult(reader);
                    }
                    catch (DbException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Connection error: {e.Message}");
            }
        }
    }
}
