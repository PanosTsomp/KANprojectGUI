using System;
using System.Text.RegularExpressions;
using Npgsql;

namespace KANprojectGUI.Managers;

public class DbManager : Singleton<DbManager>
{
    private NpgsqlConnection? _conn;

    public void ConnectToDatabase()
    {
        var connString = Program.Configuration["ConnectionStrings:Default"];

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
        Console.Write("Connected to databse");
    }

    public void AddUser(string name, string password, string email)
    {
        using var cmd = new NpgsqlCommand(
            "INSERT INTO public.users (name, email, password) VALUES (@name, @email, @password);",
            _conn
        );

        // Console writelines should become error messages in the future
        // Basic validation
        if (
            string.IsNullOrWhiteSpace(name)
            || string.IsNullOrWhiteSpace(password)
            || string.IsNullOrWhiteSpace(email)
        )
        {
            Console.WriteLine("Name, password, and email cannot be empty.");
            return;
        }

        // Simple email-format check
        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
        {
            Console.WriteLine("Invalid email format.");
            return;
        }

        // Hash the passowrd
        var hash = BCrypt.Net.BCrypt.HashPassword(password);

        // Add values to the cmd
        cmd.Parameters.AddWithValue("name", name);
        cmd.Parameters.AddWithValue("email", email);
        cmd.Parameters.AddWithValue("password", hash);

        var result = cmd.ExecuteNonQuery();

        Console.WriteLine(result > 0 ? "Insert successful." : "Insert failed - no rows affected");
    }

    public bool UserExists(string name, string password, string email)
    {
        using var cmd = new NpgsqlCommand(
            "SELECT COUNT(*) FROM public.users WHERE name = @name AND password = @password AND email = @email;",
            _conn
        );

        cmd.Parameters.AddWithValue("name", name);
        cmd.Parameters.AddWithValue("password", password);
        cmd.Parameters.AddWithValue("email", email);

        Console.WriteLine(name);
        Console.WriteLine(email);
        Console.WriteLine(password);

        var count = (long)(cmd.ExecuteScalar());

        return count > 0;
    }
}
