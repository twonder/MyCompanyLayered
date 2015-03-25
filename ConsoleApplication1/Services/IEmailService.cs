namespace MyCompany.Domain.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string from, string body);
    }
}
