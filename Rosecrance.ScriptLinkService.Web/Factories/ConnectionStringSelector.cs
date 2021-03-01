using NLog;
using Rosecrance.ScriptLinkService.Data.Models;
using System.Configuration;

namespace Rosecrance.Diagnosis.Web.Factories
{
    public static class ConnectionStringSelector
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static ConnectionStringCollection GetConnectionStringCollection(string facility)
        {
            string pmString = GetConnectionString("AVPM", "", facility);
            string cwsString = GetConnectionString("AVCWS", "", facility);
            return new ConnectionStringCollection(pmString, cwsString);
        }

        public static ConnectionStringCollection GetConnectionStringCollection(string facility, string systemCode)
        {
            string pmString = GetConnectionString("AVPM", facility, systemCode);
            string cwsString = GetConnectionString("AVCWS", facility, systemCode);
            return new ConnectionStringCollection(pmString, cwsString);
        }

        public static string GetConnectionString(string namespaceName, string systemCode, string facility)
        {
            string connectionName = GetConnectionStringName(namespaceName, systemCode, facility);
            if (connectionName != null && ConfigurationManager.ConnectionStrings[connectionName] != null)
            {
                logger.Debug("Found Connection String {connectionString} in web.config", ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);
                return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            }
            return "";
        }

        public static string GetConnectionStringName(string namespaceName, string systemCode, string facility)
        {
            string connectionName = "CacheODBC";

            switch (namespaceName)
            {
                case "AVCWS":
                    connectionName += "CWS";
                    break;
                default:
                    connectionName += "PM";
                    break;
            }

            switch (systemCode)
            {
                case "LIVE":
                case "LIVE4":
                case "LIVE5":
                    connectionName += "LIVE";
                    break;
                case "UAT":
                case "UATLIVE4":
                case "UATLIVE5":
                    connectionName += "UAT";
                    break;
                case "BLD":
                case "BLDLIVE4":    // Not sure if this is relevant
                case "BLDLIVE5":    // Not sure if this is relevant
                    connectionName += "BLD";
                    break;
                case "SBOX":
                case "SBOXLIVE4":   // Not sure if this is relevant
                case "SBOXLIVE5":   // Not sure if this is relevant
                    connectionName += "SBOX";
                    break;
                default:
                    break;
            }

            if (facility != "1")
                connectionName += facility;

            logger.Debug("Selected Connection String Name {connectionStringName}.", connectionName);
            return connectionName;
        }
    }
}