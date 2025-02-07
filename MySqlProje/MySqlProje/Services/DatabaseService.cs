using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySqlProje.Models;
using System.Diagnostics.Metrics;

namespace MySqlProje.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Actor> GetActors()
        {
            var actors = new List<Actor>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM actor", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var actor = new Actor
                    {
                        ActorId = reader.GetInt32("actor_id"),
                        FirstName = reader.GetString("first_name"),
                        LastName = reader.GetString("last_name")
                    };
                    actors.Add(actor);
                }
            }
            return actors;
        }


        public List<CountrY> GetAllCountrys()
        {
            var countrys = new List<CountrY>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM country", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var country = new CountrY
                    {
                        CountryId = reader.GetInt32("country_id"),
                        Country = reader.GetString("country"),
                        
                    };
                    countrys.Add(country);
                }
            }
            return countrys;
        }

        public void AddCountry(CountrY country)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("INSERT INTO country (country_id, country) VALUES (@country_id,@country)", connection);
                command.Parameters.AddWithValue("@country_id", country.CountryId);
                command.Parameters.AddWithValue("@country", country.Country);
                
                command.ExecuteNonQuery();
            }
        }

        public bool UpdateCountry(int id, CountrY country)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("UPDATE country SET country = @country WHERE country_id = @country_id", connection);
                command.Parameters.AddWithValue("@country_id", id);
                command.Parameters.AddWithValue("@country", country.Country);

                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

        public bool DeleteCountry(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("DELETE FROM country WHERE country_id = @country_id", connection);
                command.Parameters.AddWithValue("@country_id", id);

                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

    }
}
