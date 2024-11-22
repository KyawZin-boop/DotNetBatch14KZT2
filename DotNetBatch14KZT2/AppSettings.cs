using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14KZT2.ConsoleApp1;

public static class AppSettings
{
    public static SqlConnectionStringBuilder sqlConnectionString { get; } = new SqlConnectionStringBuilder()
    {
        DataSource = "DESKTOP-C0JBC3O\\MSSQLSERVER2022",
        InitialCatalog = "test_db",
        UserID = "sa",
        Password = "Kyawzin@123",
        TrustServerCertificate = true,
    };
}
