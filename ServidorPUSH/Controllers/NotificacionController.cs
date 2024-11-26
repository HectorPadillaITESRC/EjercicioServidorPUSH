using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServidorPUSH.Models;
using ServidorPUSH.Services;
using WebPush;

namespace ServidorPUSH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        public NotificacionController(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        private static List<PushSubscription> listaSuscriptores = new();
        private readonly NotificationService notificationService;

        [HttpPost]
        public IActionResult Suscribir(ClienteDTO cliente)
        {
            PushSubscription pushSubscription = new PushSubscription
            {
                Endpoint = cliente.Endpoint,
                P256DH = cliente.Keys.P256dh,
                Auth = cliente.Keys.Auth
            };

            listaSuscriptores.Add(pushSubscription);

            return Ok();
        }

        [HttpGet("/enviar/{message}")]
        public async Task<IActionResult> EnviarNotificacion(string message)
        {

            foreach (var item in listaSuscriptores.ToList())
            {
                await notificationService.EnviarNotificacion(item, message);
            }
            return Ok();
        }


    }
}
