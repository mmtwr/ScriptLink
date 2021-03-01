using NLog;
using RarelySimple.AvatarScriptLink.Objects;
using Rosecrance.ScriptLinkService.Data.Models;
using Rosecrance.ScriptLinkService.Data.Repositories.Odbc;
using Rosecrance.Diagnosis.Web.Commands;
using Rosecrance.Diagnosis.Web.Services.Smtp;

namespace Rosecrance.Diagnosis.Web.Factories
{
    public static class CommandSelector
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Returns the desired command based on the provided <see cref="OptionObject2015"/> and parameter.
        /// </summary>
        /// <param name="optionObject"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static IRunScriptCommand GetCommand(OptionObject2015 optionObject, string parameter)
        {
            if (optionObject == null)
            {
                logger.Error("The provided {object} is null", nameof(OptionObject2015));
                return new DefaultCommand(optionObject, parameter);
            }

            // Determine ScriptName
            logger.Debug("Parameter for scriptName is:  " + parameter);
            string scriptName = parameter != null ? parameter.Split(',')[0] : "";
            logger.Debug("Script '" + scriptName + "' requested.");

            // Get Dependencies
            // Replace with this line when ready to do ODBC from SBOX and BLD
            //ConnectionStringCollection odbcConnectionStrings = ConnectionStringSelector.GetConnectionStringCollection(optionObject.Facility, optionObject.NamespaceName);
            ConnectionStringCollection odbcConnectionStrings = ConnectionStringSelector.GetConnectionStringCollection(optionObject.Facility);
            logger.Debug("Facility is: " + optionObject.Facility);
            var repository = new GetOdbcDataRepository(odbcConnectionStrings);
            logger.Debug("Repository is: " + repository.ToString());
            var smtpService = new SmtpService();
            logger.Debug("SMTP is: " + smtpService.ToString());

            // Select Command, Not Case Sensitive
            switch (scriptName.ToUpperInvariant().Trim())
            {
                // General Commands
                case "ADDDURATIONANDTIME":
                    logger.Debug(nameof(AddDurationAndTimeCommand) + " selected.");
                    return new AddDurationAndTimeCommand(optionObject, parameter);

                case "CHKCHRS":
                    logger.Debug(nameof(ChkChrsCommand) + " selected.");
                    return new ChkChrsCommand(optionObject, parameter);

                // PM Commands
                case "DIAGNOSISFORMLOAD":
                    logger.Debug(nameof(DiagnosisFormLoadCommand) + " selected.");
                    return new DiagnosisFormLoadCommand(optionObject, repository);

                // CWS Commands

                // Testing and Sample ("Utility") Commands
                case "GETODBCDATA":
                    logger.Debug(nameof(GetOdbcDataCommand) + " selected.");
                    return new GetOdbcDataCommand(optionObject, repository);

                case "HELLOWORLD":
                    logger.Debug(nameof(HelloWorldCommand) + " selected.");
                    return new HelloWorldCommand(optionObject);

                case "SENDTESTEMAIL":
                    logger.Debug(nameof(SendTestEmailCommand) + " selected.");
                    return new SendTestEmailCommand(optionObject, parameter, smtpService);

                // If nothing matches
                default:
                    logger.Warn(nameof(DefaultCommand) + " selected because ScriptName '" + scriptName + "' does not match an existing command.");
                    return new DefaultCommand(optionObject, parameter);
            }
        }
    }
}