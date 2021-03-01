using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetPMUserDefinedDictionaryValue(string facility, string fieldNumber, string dictionaryCode)
        {
            string commandString = @"SELECT d.dictionary_value
                                       FROM SYSTEM.dictionaries_userdefined d
                                      WHERE d.facility=?
                                        AND d.field_number=?
                                        AND d.dictionary_code=?";

            string stringResult = "";
            using (OdbcConnection connection = new OdbcConnection(_connectionStringCollection.PM))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("FIELDNUMBER", fieldNumber));
                command.Parameters.Add(new OdbcParameter("DICTIONARYCODE", dictionaryCode));

                try
                {
                    connection.Open();
                    object obj = command.ExecuteScalar();
                    if (obj != null && !DBNull.Value.Equals(obj))
                        stringResult = (string)obj;
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetPMUserDefinedDictionaryValue: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", _connectionStringCollection.PM, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetPMUserDefinedDictionaryValue: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }
            return stringResult;
        }
    }
}
