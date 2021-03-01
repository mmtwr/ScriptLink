using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetProgramDivisionByProgramCode(string facility, string programCode)
        {
            string commandString = @"SELECT mi.division
                                       FROM ROSE.dbadmin_program_def_more_mi mi
                                      WHERE mi.FACILITY=?
                                        AND mi.program=?
                                        AND mi.PATID='2'";

            try
            {
                return GetProgramString(_connectionStringCollection.PM, commandString, facility, programCode);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetProgramDivisionByProgramCode: An error occurred attempting to get division for Facility {facility} and Program {programCode}.", facility, programCode);
                throw;
            }
        }
    }
}
