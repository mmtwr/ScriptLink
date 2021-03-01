using Rosecrance.ScriptLinkService.Data.Models;
using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository 
    {
        public ILSR GetLatestILSRByEpisodeNumber(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT top 1 illinois_mh_reg.FACILITY,
                                                  illinois_mh_reg.PATID, 
                                                  illinois_mh_reg.EPISODE_NUMBER, 
                                                  illinois_mh_reg.purpose_of_filing_code,
                                                  illinois_mh_reg.purpose_of_filing_value,
                                                  illinois_mh_reg.registration_start_date
                                       FROM STATEFORM.illinois_mh_reg
                                      WHERE illinois_mh_reg.FACILITY=?
                                        AND illinois_mh_reg.PATID=?
                                        AND illinois_mh_reg.EPISODE_NUMBER=?
                                      ORDER BY registration_start_date desc";

            ILSR ilsr = new ILSR(facility, patientId, episodeNumber);
            using (OdbcConnection connection = new OdbcConnection(_connectionStringCollection.PM))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("PATID", patientId));
                command.Parameters.Add(new OdbcParameter("EPISODENUMBER", episodeNumber));

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ilsr.PurposeOfFilingCode = GetStringValue(reader, "purpose_of_filing_code");
                                ilsr.PurposeOfFilingValue = GetStringValue(reader, "purpose_of_filing_value");
                                ilsr.RegistrationStartDate = GetDateTimeValue(reader, "registration_start_date");
                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetLatestILSRByEpisodeNumber: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", _connectionStringCollection.PM, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetLatestILSRByEpisodeNumber: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }
            return ilsr;
        }
    }
}
