using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        private int GetIntValue(OdbcDataReader reader, int column)
        {
            try
            {
                if (reader.IsDBNull(column))
                {
                    logger.Debug("Reader column {column} is DbNull. Returning default value.", column);
                    return 0;
                }
                else
                {
                    return reader.GetInt32(column);
                }
            }
            catch (InvalidCastException ex)
            {
                string columnValue = reader.GetString(column);
                logger.Info(ex, "Could not cast column {column} value {value} as Int32. returning default value of 0.", column, columnValue);
                return 0;
            }
            catch (Exception ex)
            {
                logger.Error("An unexpected error occurred while processing reader column {column}. Error: {errorMessage}", column, ex.Message);
                throw;
            }
        }

        private int GetIntValue(OdbcDataReader reader, string column)
        {
            try
            {
                if (reader.IsDBNull(reader.GetOrdinal(column)))
                {
                    logger.Debug("Reader column {column} is DbNull. Returning default value.", column);
                    return 0;
                }
                else
                {
                    return reader.GetInt32(reader.GetOrdinal(column));
                }
            }
            catch (InvalidCastException ex)
            {
                string columnValue = reader.GetString(reader.GetOrdinal(column));
                logger.Info(ex, "Could not cast column {column} value {value} as Int32. returning default value of 0.", column, columnValue);
                return 0;
            }
            catch (Exception ex)
            {
                logger.Error("An unexpected error occurred while processing reader column {column}. Error: {errorMessage}", column, ex.Message);
                throw;
            }
        }
    }
}
