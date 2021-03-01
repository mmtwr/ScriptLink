using NLog;
using RarelySimple.AvatarScriptLink.Objects;
using Rosecrance.Diagnosis.Web.Services.Smtp;
using System.Net.Mail;

namespace Rosecrance.Diagnosis.Web.Commands
{
    public class SendTestEmailCommand : IRunScriptCommand
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly OptionObject2015 _optionObject2015;
        private readonly string _parameter;
        private readonly ISmtpService _smtpService;

        public SendTestEmailCommand(OptionObject2015 optionObject2015, string parameter, ISmtpService smtpService)
        {
            _optionObject2015 = optionObject2015;
            _parameter = parameter;
            _smtpService = smtpService;
        }

        public OptionObject2015 Execute()
        {
            // Get Email Address
            string emailAddress = GetEmailAddress();
            logger.Debug("Attempting to send an email to {emailAddress}.", emailAddress);

            if (string.IsNullOrEmpty(emailAddress))
            {
                logger.Error("A valid email was not provided to {command}.", nameof(SendTestEmailCommand));
            }
            else
            {
                // Create MailMessage
                MailMessage mailMessage = GetMailMessage(emailAddress);

                // Send MailMessage
                _smtpService.Send(mailMessage);
                _smtpService.Dispose();
            }

            return _optionObject2015.ToReturnOptionObject(ErrorCode.Informational, "SendTestEmailCommand executed.");
        }

        #region Helper Methods

        private string GetEmailAddress()
        {
            string[] parameters = SplitDelimitedString(_parameter);
            return parameters != null && parameters.Length >= 2 ? parameters[1] : "";
        }

        private static string[] SplitDelimitedString(string delimitedString)
        {
            return delimitedString.Split(',');
        }

        public static MailMessage GetMailMessage(string emailAddress)
        {
            string from = "donotreply@rosecrance.org";
            string subject = "Test Email (Rosecrance.ScriptLinkService)";
            string body = "This is a test email from the Rosecrance.ScriptLinkService SendTestEmailCommand.";
            return new MailMessage(from, emailAddress)
            {
                Body = body,
                IsBodyHtml = false,
                Subject = subject
            };
        }

        #endregion
    }
}