using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetClientFirstAndLastInitialByPatientId(string facility, string patientId)
        {
            string commandString = @"SELECT patient_current_demographics.patient_name_first,
                                            patient_current_demographics.patient_name_last 
                                       FROM SYSTEM.patient_current_demographics 
                                      WHERE patient_current_demographics.FACILITY=? 
                                        AND patient_current_demographics.PATID=?";

            string patientName = "";
            using (OdbcConnection connection = new OdbcConnection(_connectionStringCollection.PM))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("PATID", patientId));

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string firstName = GetStringValue(reader, "patient_name_first");
                                string lastName = GetStringValue(reader, "patient_name_last");
                                patientName = firstName + " " + lastName.Substring(0, 1);
                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetClientFirstAndLastInitialByPatientId: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", _connectionStringCollection.PM, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetClientFirstAndLastInitialByPatientId: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }
            return patientName;
        }
    }
}
