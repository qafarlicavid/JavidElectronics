using JavidElectronics.Contracts.Email;

namespace JavidElectronics.Services.Abstracts
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}
