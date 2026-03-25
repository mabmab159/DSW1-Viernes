using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Semana2.Models;

namespace Semana2.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IEnumerable<Producto> getAllProductos()
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:database"]))
            {
                connection.Open();
                string query = "SELECT id, descripcion, marca, precio, stock FROM Producto";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto producto = new Producto
                            {
                                id = reader.GetInt32(0),
                                descripcion = reader.GetString(1),
                                marca = reader.GetString(2),
                                precio = reader.GetDecimal(3),
                                stock = reader.GetInt32(4)
                            };
                            productos.Add(producto);
                        }
                    }
                }
            }
            return productos;
        }

        public IActionResult Index()
        {
            return View(getAllProductos());
        }
    }
}
