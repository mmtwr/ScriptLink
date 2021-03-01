using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        private string GetProgramString(string connectionString, string commandString, string facility, string programCode)
        {
            string stringResult = "";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("PROGRAM", programCode));

                try
                {
                    connection.Open();
                    object obj = command.ExecuteScalar();
                    if (obj != null && !DBNull.Value.Equals(obj))
                        stringResult = (string)obj;
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetProgramString: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Facility: {facility}. Program Code: {programCode}. Error: {errorMessage}", connectionString, facility, programCode, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetProgramString: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }

            return stringResult;
        }
    }
}
