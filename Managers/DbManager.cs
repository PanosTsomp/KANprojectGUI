using System;
using Npgsql;

namespace KANprojectGUI.Managers;

public class DbManager : Singleton<DbManager>
{
    public void ConnectToDatabase()
    {
        var connString =
            "Host=todoeatdb.postgres.database.azure.com;"
            + "Database=postgres;"
            + "Port=5432;"
            + "User Id=todoeat;"
            + "Password=GxcH$66@Nx3HyqgP;"
            + "Ssl Mode=Require";

        try
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"PostgreSQL error: {ex.Message}");
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            Environment.Exit(1);
        }
        Console.Write("Connectd to databse");
    }
}
