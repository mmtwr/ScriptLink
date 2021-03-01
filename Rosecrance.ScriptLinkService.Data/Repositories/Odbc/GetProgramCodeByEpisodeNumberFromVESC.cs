using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetProgramCodeByEpisodeNumberFromVESC(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT view_episode_summary_current.program_code 
                                       FROM SYSTEM.view_episode_summary_current 
                                      WHERE view_episode_summary_current.FACILITY=? 
                                        AND view_episode_summary_current.PATID=? 
                                        AND view_episode_summary_current.EPISODE_NUMBER=?";

            try
            {
                return GetPatientString(_connectionStringCollection.PM, commandString, facility, patientId, episodeNumber);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetProgramCodeByEpisodeNumberFromVESC: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
