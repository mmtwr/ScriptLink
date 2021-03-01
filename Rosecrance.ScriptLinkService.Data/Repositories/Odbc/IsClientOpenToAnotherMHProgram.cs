using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public bool IsClientOpenToAnotherMHProgram(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT CASE WHEN COUNT(mi.program) > 0 THEN '1' ELSE '0' END AS HasOtherOpenMHPrograms
                                       FROM view_episode_summary_current vesc
                                            LEFT OUTER JOIN
                                            (SELECT FACILITY, Program, division
                                               FROM ROSE.dbadmin_program_def_more_mi
                                              WHERE PATID = '2') AS mi
                                            ON vesc.FACILITY = mi.FACILITY AND
                                               vesc.program_code = mi.Program
                                      WHERE vesc.FACILITY=?
                                        AND vesc.PATID=?
                                        AND vesc.EPISODE_NUMBER<>?
                                        AND mi.division = '2'";

            try
            {
                return GetPatientBool(_connectionStringCollection.PM, commandString, facility, patientId, episodeNumber);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "IsClientOpenToAnotherMHProgram: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
