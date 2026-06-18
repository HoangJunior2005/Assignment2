using System.Threading.Tasks;

namespace LearningDocumentSystem.Business.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string messageType, object payload);
    }
}
