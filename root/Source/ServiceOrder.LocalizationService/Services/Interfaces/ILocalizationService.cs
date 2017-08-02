using ServiceOrder.LocalizationService.Models;

namespace ServiceOrder.LocalizationService.Services.Interfaces
{
    public interface ILocalizationService : IService<LocalizationPhrase>
    {
        LocalizationPhrase GetByLocalizationKeyAndType(int key, string type);
    }
}
