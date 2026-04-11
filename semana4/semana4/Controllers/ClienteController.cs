using Microsoft.AspNetCore.Mvc;
using semana4.Models;
using semana4.Repositories.DAO;

namespace semana4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly clienteDAO _clienteDao;
    
    //Inyeccion de manera correcta a traves de contexto en Program.cs
    public ClienteController(clienteDAO clienteDao)
    {
        _clienteDao = clienteDao;
    }
    
    [HttpGet("getClientes")]
    public ActionResult getClientes()
    {
        return Ok(_clienteDao.getAllClientes());
    }

    [HttpPost("saveCliente")]
    public ActionResult saveCliente(Cliente cliente)
    {
        var resultado =  _clienteDao.saveCliente(cliente);
        if(resultado > 0){
            return Created("",cliente); 
        }
        return BadRequest();
    }
}