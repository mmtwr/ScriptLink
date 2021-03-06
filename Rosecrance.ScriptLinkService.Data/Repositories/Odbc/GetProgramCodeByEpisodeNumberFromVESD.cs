﻿using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetProgramCodeByEpisodeNumberFromVESD(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT view_episode_summary_discharge.program_code 
                                       FROM SYSTEM.view_episode_summary_discharge 
                                      WHERE view_episode_summary_discharge.FACILITY=? 
                                        AND view_episode_summary_discharge.PATID=? 
                                        AND view_episode_summary_discharge.EPISODE_NUMBER=?";

            try
            {
                return GetPatientString(_connectionStringCollection.PM, commandString, facility, patientId, episodeNumber);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetProgramCodeByEpisodeNumberFromVESD: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
