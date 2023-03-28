using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace webAPICore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class api : ControllerBase
    {
        private readonly string connectionString = "server=localhost;user=sqluser1;password=123456;database=membershipdb";
            [HttpPost("users")]
    public IActionResult CreateUser([FromBody] CreateUserRequest request)
    {
        using var connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            var cmd = new MySqlCommand("INSERT INTO users (username, email, password) VALUES (@username, @email, @password)",
                connection);
            cmd.Parameters.AddWithValue("@username", request.Username);
            cmd.Parameters.AddWithValue("@email", request.Email);
            cmd.Parameters.AddWithValue("@password", request.Password);
            cmd.ExecuteNonQuery();
            return Ok("User created successfully");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            return StatusCode(500, "Error creating user");
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        using var connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            var cmd = new MySqlCommand("SELECT * FROM users WHERE email = @email", connection);
            cmd.Parameters.AddWithValue("@email", request.Email);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return NotFound("User not found");
            }
            var password = reader.GetString("password");
            if (password != request.Password)
            {
                return Unauthorized("Incorrect password");
            }
            return Ok("Login successful");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            return StatusCode(500, "Error logging in");
        }
    }

    [HttpPost("update")]
    public IActionResult UpdateUser([FromBody] UpdateUserRequest request)
    {
        using var connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            var cmd = new MySqlCommand("SELECT * FROM users WHERE email = @email", connection);
            cmd.Parameters.AddWithValue("@email", request.Email);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return NotFound("User not found");
            }
            reader.Close();
            var updateCmd = new MySqlCommand("UPDATE users SET password = @password WHERE email = @email",
                connection);
            updateCmd.Parameters.AddWithValue("@password", request.Password);
            updateCmd.Parameters.AddWithValue("@email", request.Email);
            updateCmd.ExecuteNonQuery();
            Console.WriteLine($"Reset password for user with email {request.Email}");
            return Ok("Password reset successfully");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            return StatusCode(500, "Error resetting password");
        }
    }

    [HttpPost("edit")]
    public IActionResult EditUserProfile([FromBody] EditUserProfileRequest request)
    {
        using var connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            var cmd = new MySqlCommand("SELECT * FROM users WHERE email = @email", connection);
            cmd.Parameters.AddWithValue("@email", request.Email);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return NotFound("User not found");
            }
            reader.Close();
            var updateCmd = new MySqlCommand("UPDATE users SET username = @username WHERE email = @email",
                connection);
            updateCmd.Parameters.AddWithValue("@username", request.Username);
            updateCmd.Parameters.AddWithValue("@email", request.Email);
            updateCmd.ExecuteNonQuery();
            Console.WriteLine($"Username has been changed for email: {request.Email}");
            return Ok("Username has been changed");
        }
        catch (MySqlException ex)
{
Console.WriteLine($"Error occurred: {ex.Message}");
return StatusCode(500, "Error occurred while processing the request");
}
finally
{
connection.Close();
}
}
     
     [HttpPost("expenses")]
public IActionResult CreateExpense([FromBody] Expense expense)
{
    // insert expense into database
    const string cmd = "INSERT INTO amount_register (date, amount, subject) VALUES (@date, @amount, @subject)";
    const string cmd2 = "INSERT INTO amount_distribution (amount_70, amount_30) VALUES (@col1Value, @col2Value)";

    // calculate 70% and 30% of amount
    decimal col1Value = expense.Amount * 0.7M;
    decimal col2Value = expense.Amount * 0.3M;

    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        using (var transaction = connection.BeginTransaction())
        {
            try
            {
                // insert expense into amount_register table
                using (var command = new SqlCommand(cmd, connection, transaction))
                {
                    command.Parameters.AddWithValue("@date", expense.Date);
                    command.Parameters.AddWithValue("@amount", expense.Amount);
                    command.Parameters.AddWithValue("@subject", expense.Subject);
                    command.ExecuteNonQuery();
                }

                // retrieve the expense ID from amount_register table
                using (var command = new SqlCommand("SELECT @@IDENTITY", connection, transaction))
                {
                    int expenseId = Convert.ToInt32(command.ExecuteScalar());

                    // insert values into amount_distribution table
                    using (var command2 = new SqlCommand(cmd2, connection, transaction))
                    {
                        command2.Parameters.AddWithValue("@col1Value", col1Value);
                        command2.Parameters.AddWithValue("@col2Value", col2Value);
                        command2.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    Console.WriteLine($"Created expense of {expense.Amount} for {expense.Subject}");
                    Console.WriteLine($"Inserted values {col1Value} and {col2Value} into amount_distribution table with ID {expenseId}");
                    return CreatedAtRoute("GetExpense", new { id = expenseId }, expense);
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error creating expense: {ex.Message}");
                return StatusCode(500, "Error creating expense");
            }
        }
    }
}

[HttpGet("expenses")]
public IActionResult GetExpenses()
{
    const string cmd = "SELECT amount_register.date, amount_register.amount, amount_register.subject, amount_distribution.amount_70, amount_distribution.amount_30 FROM amount_register INNER JOIN amount_distribution ON amount_register.id = amount_distribution.id";

    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        using (var command = new SqlCommand(cmd, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                var expenses = new List<Expense>();
                while (reader.Read())
                {
                    expenses.Add(new Expense
                    {
                        Date = reader.GetDateTime(0),
                        Amount = reader.GetDecimal(1),
                        Subject = reader.GetString(2),
                        Amount70 = reader.GetDecimal(3),
                        Amount30 = reader.GetDecimal(4)
                    });
                }
                Console.WriteLine($"Retrieved {expenses.Count} expenses");
                return Ok(expenses);
            }
        }
    }
}


    }
}