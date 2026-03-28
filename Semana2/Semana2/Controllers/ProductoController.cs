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

        IEnumerable<Producto> getProductosFilterByName(string descripcion)
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:database"]))
            {
                //1. Abrir la conexión
                sqlConnection.Open();
                //2. Crear el comando SQL | Llamar al store procedure (query, cadena de conexion)
                SqlCommand sqlCommand = new SqlCommand("usp_productos_filtrar", sqlConnection);
                //3. Indicar el tipo de comando (store procedure)
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //4. Agregar los parámetros al comando (como llega al SP, como lo estas enviando)
                sqlCommand.Parameters.AddWithValue("@descripcion", descripcion);
                //5. Ejecutar el comando
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Producto producto = new Producto
                    {
                        id = sqlDataReader.GetInt32(0),
                        descripcion = sqlDataReader.GetString(1),
                        marca = sqlDataReader.GetString(2),
                        precio = sqlDataReader.GetDecimal(3),
                        stock = sqlDataReader.GetInt32(4),
                    };
                    productos.Add(producto);
                }
            }
            return productos;
        }

        IEnumerable<Producto> getAllProductosPaginacion()
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:database"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("usp_productos", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
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

        public IActionResult Filtrar(string? descripcion)
        {
            if (string.IsNullOrEmpty(descripcion))
            {
                return View(new List<Producto>());
            }
            return View(getProductosFilterByName(descripcion));
        }

        public IActionResult Paginacion(int pagina = 0)
        {
            IEnumerable<Producto> productos = getAllProductosPaginacion();

            int cantidadRegistrosPorPagina = 10;
            int cantidadTotalRegistros = productos.Count(); //100:10  -> 10 paginas| 104:10 -> 10 paginas + 1 pagina (4 elementos)
            int cantidadTotalPaginas = cantidadTotalRegistros % cantidadRegistrosPorPagina == 0 ?
                                        cantidadTotalRegistros / cantidadRegistrosPorPagina :
                                        cantidadTotalRegistros / cantidadRegistrosPorPagina + 1;

            ViewBag.PaginaActual = pagina;
            ViewBag.CantidadTotalPaginas = cantidadTotalPaginas;
            return View(productos.Skip(cantidadRegistrosPorPagina * pagina).Take(cantidadRegistrosPorPagina));
        }
    }
}
