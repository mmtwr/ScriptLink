using Rosecrance.ScriptLinkService.Data.Helpers;
using System;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        private int IMCANSGetRating(string fieldValue)
        {
            try
            {
                switch (fieldValue)
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                        return RoseConvert.ToInt(fieldValue);
                    default:
                        return -1;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error occurred getting IMCANS rating from Field Value {fieldValue}. Error: {errorMessage}.", fieldValue, ex.Message);
                throw;
            }
        }
    }
}
