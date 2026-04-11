using semana4.Models;

namespace semana4.Repositories.Interfaces;

public interface ICliente
{
    IEnumerable<Cliente> getAllClientes();
    Cliente getClienteById(int id);
    int saveCliente(Cliente cliente);
    Cliente updateCliente(Cliente cliente);
}