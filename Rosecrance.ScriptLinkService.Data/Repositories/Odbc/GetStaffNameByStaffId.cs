using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetStaffNameByStaffId(string facility, string staffId)
        {
            string commandString = @"SELECT staff_enrollment_history.staff_name
                                       FROM SYSTEM.staff_enrollment_history
                                      WHERE staff_enrollment_history.FACILITY=?
                                        AND staff_enrollment_history.STAFFID=?";

            try
            {
                return GetStaffString(_connectionStringCollection.PM, commandString, facility, staffId);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetStaffNameByStaffId: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
