using StocksApp.Dtos.Email;

namespace StocksApp.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailRegistration(EmailDto request);
    }
}
