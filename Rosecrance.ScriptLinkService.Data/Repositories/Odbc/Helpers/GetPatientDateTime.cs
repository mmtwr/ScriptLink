﻿using Rosecrance.ScriptLinkService.Data.Helpers;
using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        private DateTime GetPatientDateTime(string connectionString, string commandString, string facility, string patientId)
        {
            string stringResult = "";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("PATID", patientId));

                try
                {
                    connection.Open();
                    object obj = command.ExecuteScalar();
                    if (obj != null && !DBNull.Value.Equals(obj))
                    {
                        try
                        {
                            return (DateTime)obj;
                        }
                        catch (InvalidCastException)
                        {
                            stringResult = (string)obj;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetPatientDateTime: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", connectionString, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetPatientDateTime: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }
            logger.Debug("Retrieved date string {dateString} for Facility {facility} and Patient Id {patientId}", stringResult, facility, patientId);
            return RoseConvert.ToDateTime(stringResult);
        }
        private DateTime GetPatientDateTime(string connectionString, string commandString, string facility, string patientId, double episodeNumber)
        {
            string stringResult = "";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("PATID", patientId));
                command.Parameters.Add(new OdbcParameter("EPISODENUMBER", episodeNumber));

                try
                {
                    connection.Open();
                    object obj = command.ExecuteScalar();
                    if (obj != null && !DBNull.Value.Equals(obj))
                    {
                        try
                        {
                            return (DateTime)obj;
                        }
                        catch (InvalidCastException)
                        {
                            stringResult = (string)obj;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetPatientDateTime: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", connectionString, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetPatientDateTime: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }
            logger.Debug("Retrieved date string {dateString} for Facility {facility} and Patient Id {patientId}", stringResult, facility, patientId);
            return RoseConvert.ToDateTime(stringResult);
        }
    }
}
