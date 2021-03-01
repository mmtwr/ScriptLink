using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetProgramDiagnosisRequiredByProgramCode(string facility, string programCode)
        {
            string commandString = @"SELECT ROSE.dbadmin_program_def_more_mi.ss_diagnosis_reqd
                                       FROM ROSE.dbadmin_program_def_more_mi
                                      WHERE dbadmin_program_def_more_mi.FACILITY=?
                                        AND dbadmin_program_def_more_mi.PATID='2'
                                        AND dbadmin_program_def_more_mi.program=?";

            try
            {
                return GetProgramString(_connectionStringCollection.PM, commandString, facility, programCode);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetProgramDiagnosisRequiredByProgramCode: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
