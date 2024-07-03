using MySql.Data.MySqlClient;
using System.Data;

namespace OrightApi.Db
{
    public class DbContext
    {
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("conStr");
        }
        public MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);

            try
            {
                connection.Open();

                
                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine("Database connection established successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to connect to the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }

            return connection;
        }

        }
}
