using Rosecrance.ScriptLinkService.Data.Helpers;
using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        private bool GetBoolValue(OdbcDataReader reader, int column)
        {
            try
            {
                if (reader.IsDBNull(column))
                {
                    logger.Debug("Reader column {column} is DbNull. Returning default value.", column);
                    return false;
                }
                else
                {
                    return RoseConvert.ToBool(reader.GetString(column));
                }
            }
            catch (Exception ex)
            {
                logger.Error("An unexpected error occurred while processing reader column {column}. Error: {errorMessage}", column, ex.Message);
                throw;
            }
        }
        private bool GetBoolValue(OdbcDataReader reader, string column)
        {
            try
            {
                if (reader.IsDBNull(reader.GetOrdinal(column)))
                {
                    logger.Debug("Reader column {column} is DbNull. Returning default value.", column);
                    return false;
                }
                else
                {
                    return RoseConvert.ToBool(reader.GetString(reader.GetOrdinal(column)));
                }
            }
            catch (Exception ex)
            {
                logger.Error("An unexpected error occurred while processing reader column {column}. Error: {errorMessage}", column, ex.Message);
                throw;
            }
        }
    }
}
