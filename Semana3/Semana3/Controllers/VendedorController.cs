using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Semana3.Models;

namespace Semana3.Controllers
{
    public class VendedorController : Controller
    {
        private readonly IConfiguration configuration;

        public VendedorController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        IEnumerable<Vendedor> TotalidadVendedores()
        {
            List<Vendedor> vendedores = new List<Vendedor>();
            using(SqlConnection sqlConnection = new SqlConnection(configuration["ConnectionStrings:database"]))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("usp_vendedores", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Vendedor vendedor = new Vendedor()
                    {
                        id = sqlDataReader.GetInt32(0),
                        nombre = sqlDataReader.GetString(1),
                        direccion = sqlDataReader.GetString(2),
                        email = sqlDataReader.GetString(3),
                        ciudad = sqlDataReader.GetString(4)
                    };
                    vendedores.Add(vendedor);
                }
                sqlDataReader.Close();
            }
            return vendedores;
        }

        string guardarVendedor(Vendedor vendedor)
        {
            using(SqlConnection sqlConnection = new SqlConnection(configuration["ConnectionStrings:database"]))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("usp_crear_vendedor", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", vendedor.id);
                sqlCommand.Parameters.AddWithValue("@nombre", vendedor.nombre);
                sqlCommand.Parameters.AddWithValue("@direccion", vendedor.direccion);
                sqlCommand.Parameters.AddWithValue("@email", vendedor.email);
                sqlCommand.Parameters.AddWithValue("@ciudad", vendedor.ciudad);
                int cantidadFilas = sqlCommand.ExecuteNonQuery();
                string mensaje = cantidadFilas > 0 ? "Vendedor guardado" : "Error al guardar el vendedor";
                return mensaje;
            }
        }

        public IActionResult Index()
        {
            return View(TotalidadVendedores());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Vendedor vendedor)
        {
            string mensaje = guardarVendedor(vendedor);
            ViewBag.Mensaje = mensaje;
            ModelState.Clear();
            return View(new Vendedor());
        }
    }
}
