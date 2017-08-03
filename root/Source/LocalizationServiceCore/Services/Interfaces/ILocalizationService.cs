using LocalizationServiceCore.Models;

namespace LocalizationServiceCore.Services.Interfaces
{
    public interface ILocalizationService : IService<LocalizationPhrase>
    {
        LocalizationPhrase GetByLocalizationKeyAndType(int key, string type);
    }
}
