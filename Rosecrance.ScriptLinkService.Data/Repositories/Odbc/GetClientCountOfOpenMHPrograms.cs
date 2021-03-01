using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public int GetClientCountOfOpenMHPrograms(string facility, string patientId)
        {
            string commandString = @"SELECT COUNT(mi.program) AS CountOfOpenMHPrograms
                                       FROM view_episode_summary_current vesc
                                            LEFT OUTER JOIN
                                            (SELECT FACILITY, Program, division
                                               FROM ROSE.dbadmin_program_def_more_mi
                                              WHERE PATID = '2') AS mi
                                            ON vesc.FACILITY = mi.FACILITY AND
                                               vesc.program_code = mi.Program
                                      WHERE vesc.FACILITY=?
                                        AND vesc.PATID=?
                                        AND mi.division = '2'";

            try
            {
                return GetPatientInt(_connectionStringCollection.PM, commandString, facility, patientId);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetClientCountOfOpenMHPrograms: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
