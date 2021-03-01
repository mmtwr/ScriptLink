using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public DateTime GetClientDateOfBirthByPatientId(string facility, string patientId)
        {
            string commandString = @"SELECT TO_CHAR(d.date_of_birth, 'MM-DD-YYYY') AS date_of_birth
                                       FROM SYSTEM.patient_current_demographics d
                                      WHERE d.FACILITY=?
                                        AND d.PATID=?";

            try
            {
                return GetPatientDateTime(_connectionStringCollection.PM, commandString, facility, patientId);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetClientDateOfBirthByPatientId: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
