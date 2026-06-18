using Microsoft.AspNetCore.SignalR;

namespace LearningDocumentSystem.Web.Hubs
{
    public class NotificationHub : Hub
    {
        // Hub này được sử dụng để phát thông điệp Realtime từ Server tới Client.
        // Client sẽ lắng nghe sự kiện "ReceiveNotification".
    }
}
