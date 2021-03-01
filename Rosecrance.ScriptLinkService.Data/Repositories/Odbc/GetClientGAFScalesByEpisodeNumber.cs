using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public int GetClientGAFScalesByEpisodeNumber(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT TOP (1) s.nu_gafCgas,
                                                    -- For Testing
                                                    s.FACILITY,
                                                    s.PATID,
                                                    s.EPISODE_NUMBER,
                                                    s.data_entry_date,
                                                    s.data_entry_time
                                       FROM ROSE.rose_asmnt_mh_scales s
                                      WHERE s.FACILITY=?
                                        AND s.PATID=?
                                        AND s.EPISODE_NUMBER=?
                                      ORDER BY s.Data_Entry_Date DESC,
                                               TO_TIMESTAMP(s.Data_Entry_Time,'HH:MI:SS.FF') DESC";

            try
            {
                return GetPatientInt(_connectionStringCollection.CWS, commandString, facility, patientId, episodeNumber);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetClientGAFScalesByEpisodeNumber: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
