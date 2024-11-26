using WebPush;

namespace ServidorPUSH.Services
{
    public class NotificationService
    {
        WebPushClient pushClient = new();
        VapidDetails vapid;

        public NotificationService(IConfiguration configuration)
        {
            vapid = new VapidDetails()
            {
                Subject = configuration["Vapid:subject"],
                PrivateKey = configuration["Vapid:privateKey"],
                PublicKey = configuration["Vapid:publicKey"]
            };
        }


       
        public async Task EnviarNotificacion(PushSubscription cliente, string mensaje)
        {
            await pushClient.SendNotificationAsync
                (cliente, mensaje, vapid);
        }



    }
}
