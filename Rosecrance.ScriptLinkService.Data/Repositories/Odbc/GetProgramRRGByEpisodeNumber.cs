using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetProgramRRGByEpisodeNumber(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT view_episode_summary_admit.program_X_RRG_value
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
                logger.Debug(ex, "GetProgramRRGByEpisodeNumber: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
