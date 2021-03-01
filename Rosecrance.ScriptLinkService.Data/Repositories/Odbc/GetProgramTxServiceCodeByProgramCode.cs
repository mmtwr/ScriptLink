using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetProgramTxServiceCodeByProgramCode(string facility, string programCode)
        {
            string commandString = @"SELECT SYSTEM.table_program_definition.program_X_tx_service_code 
                                       FROM SYSTEM.table_program_definition 
                                      WHERE table_program_definition.FACILITY=?
                                        AND table_program_definition.program_code=?";

            try
            {
                return GetProgramString(_connectionStringCollection.PM, commandString, facility, programCode);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetProgramTxServiceCodeByProgramCode: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
