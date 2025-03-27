using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InterviewSamples
{
    public class Sample1Repository
    {
        private readonly string _connectionString;

        public Sample1Repository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AppDatabase"].ConnectionString;
        }

        public async Task<Sample1User> Get(string name)
        {
            Sample1User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Name = " + name, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new Sample1User
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"]
                        };
                    }
                }
            }

            return user;
        }
    }

    public class Sample1User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
