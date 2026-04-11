using Microsoft.Data.SqlClient;
using semana4.Models;
using semana4.Repositories.Interfaces;

namespace semana4.Repositories.DAO;

public class paisDAO : IPais
{
    private readonly string connectionString;
    
    public paisDAO()
    {
        connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("dataBase");
    }
    
    public IEnumerable<Pais> getAllPais()
    {
        List<Pais> paises = new List<Pais>();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("Select * from Pais",  sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                paises.Add(new Pais()
                {
                    id = sqlDataReader.GetInt32(0),
                    nombre = sqlDataReader.GetString(1)
                });
            }
            return paises;
        }
    }
}