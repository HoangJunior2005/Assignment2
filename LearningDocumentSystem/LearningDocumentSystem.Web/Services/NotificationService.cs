using LearningDocumentSystem.Business.Services.Interfaces;
using LearningDocumentSystem.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LearningDocumentSystem.Web.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(string messageType, object payload)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", messageType, payload);
        }
    }
}
