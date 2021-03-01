using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public bool HasDischargeDiagnosisByEpisodeNumber(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT CASE WHEN COUNT(dr.PATID)>0 THEN '1' ELSE '0' END AS HasDischargeDiagnosis
                                       FROM SYSTEM.client_diagnosis_record dr
                                            LEFT OUTER JOIN
                                            SYSTEM.client_diagnosis_entry de
                                            ON dr.ID = de.DiagnosisRecord
                                      WHERE dr.FACILITY=?
                                        AND dr.PATID=?
                                        AND dr.EPISODE_NUMBER=?
                                        AND dr.diagnosis_type_code='D'";

            try
            {
                return GetPatientBool(_connectionStringCollection.PM, commandString, facility, patientId, episodeNumber);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "HasDischargeDiagnosisByEpisodeNumber: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
