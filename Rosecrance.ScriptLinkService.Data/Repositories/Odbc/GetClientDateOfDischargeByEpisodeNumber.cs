using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public DateTime ? GetClientDateOfDischargeByEpisodeNumber(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT TO_CHAR(a.date_of_discharge, 'MM-DD-YYYY') AS date_of_discharge 
                                       FROM SYSTEM.view_episode_summary_admit a
                                      WHERE a.FACILITY=?
                                        AND a.PATID=?
                                        AND a.EPISODE_NUMBER=?";

            try
            {
                return GetPatientDateTime(_connectionStringCollection.PM, commandString, facility, patientId, episodeNumber);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetClientDateOfDischargeByEpisodeNumber: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
