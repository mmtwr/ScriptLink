using NLog;
using Rosecrance.ScriptLinkService.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public int GetAgeFromVESC(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT view_episode_summary_current.c_age 
                                       FROM SYSTEM.view_episode_summary_current 
                                      WHERE view_episode_summary_current.FACILITY=? 
                                        AND view_episode_summary_current.PATID=? 
                                        AND view_episode_summary_current.EPISODE_NUMBER=?";

            int result = 0;
            using (OdbcConnection connection = new OdbcConnection(_connectionStringCollection.PM))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("PATID", patientId));
                command.Parameters.Add(new OdbcParameter("EPISODE_NUMBER", episodeNumber));

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        logger.Debug($"Reader has rows: {reader.HasRows}");
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                
                                double number; 
                                result = double.TryParse(reader["c_age"].ToString(), out number) ? Convert.ToInt32(number) : 99;
                                logger.Debug($"Line 41 clientAge: {result}");
                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetBedAssignmentByAgeCheck: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", "_connectionStringCollection.CWS", ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetBedAssignmentByAgeCheck: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }

            return result;
        }
    }
}
