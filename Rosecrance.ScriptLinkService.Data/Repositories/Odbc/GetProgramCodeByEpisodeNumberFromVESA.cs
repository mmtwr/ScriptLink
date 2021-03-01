using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetProgramCodeByEpisodeNumberFromVESA(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT view_episode_summary_admit.program_code
                                       FROM SYSTEM.view_episode_summary_admit
                                      WHERE view_episode_summary_admit.FACILITY=?
                                        AND view_episode_summary_admit.PATID=?
                                        AND view_episode_summary_admit.EPISODE_NUMBER=?";

            try
            {
                return GetPatientString(_connectionStringCollection.PM, commandString, facility, patientId, episodeNumber);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetProgramCodeByEpisodeNumberFromVESA: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
