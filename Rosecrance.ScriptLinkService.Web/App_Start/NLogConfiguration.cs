using NLog;
using NLog.Config;
using NLog.Targets;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Rosecrance.Diagnosis.Web
{
    public static class NLogConfiguration
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Target is disposed elsewhere.")]
        public static void RegisterLogs()
        {
            var config = new LoggingConfiguration();

            string fileLocation = "C:\\Logs\\Rosecrance.Diagnosis\\";
            string fileFolder = "";
            string fileExtension = ".log";
            LogLevel minLogLevel = LogLevel.Info;

            // Set values based on DEBUG vs RELEASE environment
#if DEBUG
            fileFolder = "Test\\";
            minLogLevel = LogLevel.Debug;
#else
            fileFolder = "Production\\";
#endif

            // Set Logging Targets
            FileTarget apiLogfile = new FileTarget("apilogfile")
            {
                Name = "Api.Commands",
                FileName = fileLocation + fileFolder + "api" + fileExtension,
                ArchiveFileName = fileLocation + fileFolder + "Archive\\Api\\api.{#}" + fileExtension,
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Date,
                MaxArchiveFiles = 14,
                ConcurrentWrites = true,
                KeepFileOpen = false,
                Encoding = Encoding.UTF8
            };
            FileTarget apiCommandsLogfile = new FileTarget("apicommandslogfile")
            {
                Name = "Api.Commands",
                FileName = fileLocation + fileFolder + "api.commands" + fileExtension,
                ArchiveFileName = fileLocation + fileFolder + "Archive\\Api\\api.commands.{#}" + fileExtension,
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Date,
                MaxArchiveFiles = 14,
                ConcurrentWrites = true,
                KeepFileOpen = false,
                Encoding = Encoding.UTF8
            };
            FileTarget apiFactoriesLogfile = new FileTarget("apifactorieslogfile")
            {
                Name = "Api.Factories",
                FileName = fileLocation + fileFolder + "api.factories" + fileExtension,
                ArchiveFileName = fileLocation + fileFolder + "Archive\\Api\\api.factories.{#}" + fileExtension,
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Date,
                MaxArchiveFiles = 14,
                ConcurrentWrites = true,
                KeepFileOpen = false,
                Encoding = Encoding.UTF8
            };
            FileTarget dataOdbcLogfile = new FileTarget("dataodbclogfile")
            {
                Name = "Data.Odbc",
                FileName = fileLocation + fileFolder + "data.odbc" + fileExtension,
                ArchiveFileName = fileLocation + fileFolder + "Archive\\Data\\data.odbc.{#}" + fileExtension,
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Date,
                MaxArchiveFiles = 14,
                ConcurrentWrites = true,
                KeepFileOpen = false,
                Encoding = Encoding.UTF8
            };
            FileTarget dataSoapLogfile = new FileTarget("datasoaplogfile")
            {
                Name = "Data.Soap",
                FileName = fileLocation + fileFolder + "data.soap" + fileExtension,
                ArchiveFileName = fileLocation + fileFolder + "Archive\\Data\\data.soap.{#}" + fileExtension,
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Date,
                MaxArchiveFiles = 14,
                ConcurrentWrites = true,
                KeepFileOpen = false,
                Encoding = Encoding.UTF8
            };
            FileTarget dataRestLogfile = new FileTarget("datarestlogfile")
            {
                Name = "Data.Rest",
                FileName = fileLocation + fileFolder + "data.rest" + fileExtension,
                ArchiveFileName = fileLocation + fileFolder + "Archive\\Data\\data.rest.{#}" + fileExtension,
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Date,
                MaxArchiveFiles = 14,
                ConcurrentWrites = true,
                KeepFileOpen = false,
                Encoding = Encoding.UTF8
            };
            FileTarget servicesSmtpLogFile = new FileTarget("servicessmtplogfile")
            {
                Name = "Services.Smtp",
                FileName = fileLocation + fileFolder + "services.smtp" + fileExtension,
                ArchiveFileName = fileLocation + fileFolder + "Archive\\Services\\services.smtp.{#}" + fileExtension,
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Date,
                MaxArchiveFiles = 14,
                ConcurrentWrites = true,
                KeepFileOpen = false,
                Encoding = Encoding.UTF8
            };

            // Set Rules for mapping loggers to targets            
            config.AddRule(minLogLevel, LogLevel.Fatal, apiLogfile, "Rosecrance.Diagnosis.Web.Api.*");                      // Logs from .asmx files
            config.AddRule(minLogLevel, LogLevel.Fatal, apiCommandsLogfile, "Rosecrance.Diagnosis.Web.Commands.*");         // Logs from Commands
            config.AddRule(minLogLevel, LogLevel.Fatal, apiFactoriesLogfile, "Rosecrance.Diagnosis.Web.Factories.*");       // Logs from Factories
            config.AddRule(minLogLevel, LogLevel.Fatal, dataOdbcLogfile, "Rosecrance.ScriptLinkService.Data.Repositories.Odbc.*");  // Logs from ODBC-based Repository
            config.AddRule(minLogLevel, LogLevel.Fatal, dataSoapLogfile, "Rosecrance.ScriptLinkService.Data.Repositories.Soap.*");  // For future SOAP-based Repository (i.e., current generation Avatar Web Services)
            config.AddRule(minLogLevel, LogLevel.Fatal, dataRestLogfile, "Rosecrance.ScriptLinkService.Data.Repositories.Rest.*");  // For future REST-based Repository (i.e., next generation Avatar Web Services)
            config.AddRule(minLogLevel, LogLevel.Fatal, servicesSmtpLogFile, "Rosecrance.Diagnosis.Web.Services.Smtp.*");   // Logs from SMTP Services

            // Apply config           
            LogManager.Configuration = config;
        }
    }
}