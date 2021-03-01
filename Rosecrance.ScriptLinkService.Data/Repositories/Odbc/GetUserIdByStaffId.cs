using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetUserIdByStaffId(string facility, string staffId)
        {
            string commandString = @"SELECT RADPlus_users.USERID
                                       FROM SYSTEM.RADPlus_users
                                      WHERE RADPlus_users.FACILITY=?
                                        AND RADPlus_users.staff_member_id=?";

            try
            {
                return GetUserString(_connectionStringCollection.PM, commandString, facility, staffId);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetUserIdByStaffId: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
