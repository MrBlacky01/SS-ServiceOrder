using System.Threading.Tasks;

namespace AuthorizationService.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
