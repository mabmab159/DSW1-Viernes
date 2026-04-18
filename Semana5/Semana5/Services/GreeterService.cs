using Grpc.Core;

namespace Semana5.Services
{
    public class GreeterService(ILogger<GreeterService> logger) : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            logger.LogInformation("The message is received from {Name}", request.Name);
            string mensaje = string.Concat(request.Code, ";", request.Name, ";", request.Surname);
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + mensaje,
            });
        }
    }
}
