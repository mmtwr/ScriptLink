using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetUserEmailAddressByUserId(string facility, string userId)
        {
            string commandString = @"SELECT staff_info_more_pr.email
                                       FROM ROSE.staff_info_more_pr
                                      WHERE staff_info_more_pr.FACILITY=?
                                        AND staff_info_more_pr.USERID=?";

            try
            {
                return GetUserString(_connectionStringCollection.PM, commandString, facility, userId);
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetUserEmailAddressByUserId: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
