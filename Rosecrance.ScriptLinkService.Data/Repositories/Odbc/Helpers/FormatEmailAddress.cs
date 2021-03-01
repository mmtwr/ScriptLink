namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        private string FormatEmailAddress(string emailAddress, string userId)
        {
            if (emailAddress == null || emailAddress == "")
            {
                return userId != null ? userId + "@rosecrance.org" : "";
            }
            return emailAddress;
        }
    }
}
