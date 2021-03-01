using System.Net.Mail;

namespace Rosecrance.Diagnosis.Web.Services.Smtp
{
    public interface ISmtpService
    {
        void Dispose();
        void Send(MailMessage mailMessage);
    }
}