using JavidElectronics.Database.Models;

namespace JavidElectronics.Services.Abstracts
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user);
    }
}
