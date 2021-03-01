using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public bool HasAdmissionDiagnosisByEpisodeNumber(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT CASE WHEN COUNT(dr.PATID)>0 THEN '1' ELSE '0' END AS HasAdmissionDiagnosis
                                       FROM SYSTEM.client_diagnosis_record dr
                                            LEFT OUTER JOIN
                                            SYSTEM.client_diagnosis_entry de
                                            ON dr.ID = de.DiagnosisRecord
                                      WHERE dr.FACILITY=?
                                        AND dr.PATID=?
                                        AND dr.EPISODE_NUMBER=?
                                        AND dr.diagnosis_type_code='A'";

            try
            {
                return GetPatientBool(_connectionStringCollection.PM, commandString, facility, patientId, episodeNumber);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "HasAdmissionDiagnosisByEpisodeNumber: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
