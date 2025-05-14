using System;
using Npgsql;

namespace KANprojectGUI.Managers;

public class DbManager : Singleton<DbManager>
{
    private NpgsqlConnection? _conn;

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
            _conn = new NpgsqlConnection(connString);
            _conn.Open();
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

    public void AddUser(string name, string password, string email)
    {
        using var cmd = new NpgsqlCommand(
            "INSERT INTO public.users (name, email, password) VALUES (@name, @email, @password);",
            _conn
        );

        cmd.Parameters.AddWithValue("name", name);
        cmd.Parameters.AddWithValue("email", email);
        cmd.Parameters.AddWithValue("password", password);

        var reuslt = cmd.ExecuteNonQuery();

        if (reuslt > 0)
            Console.WriteLine("Insert successful.");
        else
            Console.WriteLine("Insert failed - no rows affected");
    }
}
