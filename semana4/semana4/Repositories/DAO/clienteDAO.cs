using System.Data;
using Microsoft.Data.SqlClient;
using semana4.Models;
using semana4.Repositories.Interfaces;

namespace semana4.Repositories.DAO;

public class clienteDAO : ICliente
{
    private readonly string connectionString;
    
    public clienteDAO()
    {
        connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("dataBase");
    }
    
    public IEnumerable<Cliente> getAllClientes()
    {
        List<Cliente> clientes = new List<Cliente>();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("Select * from Cliente", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                clientes.Add(new Cliente()
                {
                    id = sqlDataReader.GetInt32(0),
                    nombre = sqlDataReader.GetString(1),
                    apellido = sqlDataReader.GetString(2),
                    telefono = sqlDataReader.GetString(3),
                    direccion = sqlDataReader.GetString(4),
                    idPais =  sqlDataReader.GetInt32(5),
                });
            }
            return clientes;
        }
    }

    public Cliente getClienteById(int id)
    {
        throw new NotImplementedException();
    }

    public int saveCliente(Cliente cliente)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("usp_save_cliente", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", cliente.id);
            sqlCommand.Parameters.AddWithValue("@nombre", cliente.nombre);
            sqlCommand.Parameters.AddWithValue("@apellido", cliente.apellido);
            sqlCommand.Parameters.AddWithValue("@telefono", cliente.telefono);
            sqlCommand.Parameters.AddWithValue("@direccion", cliente.direccion);
            sqlCommand.Parameters.AddWithValue("@idPais", cliente.idPais);
            return sqlCommand.ExecuteNonQuery();
        }
    }

    public Cliente updateCliente(Cliente cliente)
    {
        throw new NotImplementedException();
    }
}