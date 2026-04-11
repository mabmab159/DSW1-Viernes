using Microsoft.AspNetCore.Mvc;
using semana4.Repositories.DAO;

namespace semana4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaisController : ControllerBase
{
    private paisDAO _paisDao;

    //No recomendado - Saturacion de objetos en memoria por construccion de objetos
    public PaisController()
    {
        _paisDao = new paisDAO();
    }
    
    [HttpGet("getAllPais")]
    public ActionResult getAllPais()
    {
        return Ok(_paisDao.getAllPais());
    }
}