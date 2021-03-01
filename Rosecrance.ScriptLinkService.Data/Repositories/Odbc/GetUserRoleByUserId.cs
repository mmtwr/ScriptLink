using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetUserRoleByUserId(string facility, string userId)
        {
            string commandString = @"SELECT RADPlus_users.USERROLE
                                       FROM SYSTEM.RADPlus_users
                                      WHERE RADPlus_users.FACILITY=?
                                        AND RADPlus_users.USERID=?";

            try
            {
                return GetUserString(_connectionStringCollection.PM, commandString, facility, userId);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetUserRoleByUserId: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
}
    }
}
