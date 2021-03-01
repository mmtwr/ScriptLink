using Rosecrance.ScriptLinkService.Data.Helpers;
using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        private DateTime GetDateTimeValue(OdbcDataReader reader, int column)
        {
            try
            {
                if (reader.IsDBNull(column))
                {
                    logger.Debug("Reader column {column} is DbNull. Returning default value.", column);
                    return new DateTime();
                }
                else
                {
                    return reader.GetDateTime(column);
                }
            }
            catch (InvalidCastException ex)
            {
                string columnValue = reader.GetString(column);
                logger.Info(ex, "Could not cast column {column} value {value} as DateTime. returning default value.", column, columnValue);
                return new DateTime();
            }
            catch (Exception ex)
            {
                logger.Error("An unexpected error occurred while processing reader column {column}. Error: {errorMessage}", column, ex.Message);
                throw;
            }
        }
        private DateTime GetDateTimeValue(OdbcDataReader reader, string column)
        {
            try
            {
                if (reader.IsDBNull(reader.GetOrdinal(column)))
                {
                    logger.Debug("Reader column {column} is DbNull. Returning default value.", column);
                    return new DateTime();
                }
                else
                {
                    return reader.GetDateTime(reader.GetOrdinal(column));
                }
            }
            catch (InvalidCastException ex)
            {
                string columnValue = reader.GetString(reader.GetOrdinal(column));
                logger.Info(ex, "Could not cast column {column} value {value} as DateTime. returning default value.", column, columnValue);
                return new DateTime();
            }
            catch (Exception ex)
            {
                logger.Error("An unexpected error occurred while processing reader column {column}. Error: {errorMessage}", column, ex.Message);
                throw;
            }
        }
    }
}
