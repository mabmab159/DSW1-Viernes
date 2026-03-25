using System.ComponentModel.DataAnnotations;

namespace Semana2.Models
{
    public class Producto
    {
        [Display(Name = "Id Producto")] public int id { get; set; }
        [Display(Name = "Descripcion")] public string descripcion { get; set; }
        [Display(Name = "Marca")] public string marca { get; set; }
        [Display(Name = "Precio c/u")] public decimal precio { get; set; }
        [Display(Name = "Cantidad")] public int stock { get; set; }
    }
}
