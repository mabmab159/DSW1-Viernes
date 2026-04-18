using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Semana5;

namespace Aw5.Controllers
{
    public class PruebaController : Controller
    {
        private Greeter.GreeterClient _greeter;

        [HttpPost]
        public async Task<IActionResult> Index(string code, string name, string surname) { 
            var canal = GrpcChannel.ForAddress("http://localhost:5243");
            _greeter = new Greeter.GreeterClient(canal);

            var request = new HelloRequest();
            request.Code = code;
            request.Name = name;
            request.Surname = surname;

            var mensaje = await _greeter.SayHelloAsync(request);
            ViewBag.mensaje = mensaje.Message;
            ViewBag.code  = code;
            ViewBag.name  = name;
            ViewBag.surname  = surname;
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
