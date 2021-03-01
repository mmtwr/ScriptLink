using NLog;
using Rosecrance.ScriptLinkService.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public List<Client> GetBedAssignmentAgeCheck(string facility, string unitCode, string room)
        {
            string commandString;
            //string patientIDParameter = "";
            commandString = $@"SELECT bed.FACILITY
                                    , bed.EPISODE_NUMBER
                                    , bed.PATID                                    
                                    , bed.unit_code
                                    , bed.unit_value
                                    , bed.room
                                    , bed.bed
                                    , bed.date_of_bed_assignment
                                    , bed.end_date_of_bed_assignment
                                    , cur.c_age 
                                    FROM   SYSTEM.history_bed_assignment bed
                                    INNER JOIN SYSTEM.view_episode_summary_current cur ON bed.FACILITY = cur.FACILITY 
                                    AND bed.PATID = cur.PATID
                                    AND bed.EPISODE_NUMBER = cur.EPISODE_NUMBER
                                    WHERE  bed.FACILITY= ?                                     
                                    AND bed.unit_code = ?
                                    AND bed.room = ?
                                    AND bed.end_date_of_bed_assignment IS NULL";


            //Age age = new Age(DateTime.Now);
            Client client = new Client();
            List<Client> clients = new List<Client>();
            using (OdbcConnection connection = new OdbcConnection(_connectionStringCollection.PM))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("unit_code", unitCode));
                command.Parameters.Add(new OdbcParameter("room", room));
                
                //if (!string.IsNullOrEmpty(patientID))
                //{
                //    patientIDParameter = $"AND bed.PATID = {patientID}";
                //}
                //else
                //{
                //    patientIDParameter = "";
                //}


                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        logger.Debug($"Reader has rows: {reader.HasRows}");
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int result;
                                double number;
                                client.FACILITY = reader["FACILITY"].ToString();                                
                                logger.Debug($"FACILITY: {client.FACILITY}");
                                client.EPISODE_NUMBER = double.TryParse(reader["EPISODE_NUMBER"].ToString(), out number) ? number : 99;
                                logger.Debug($"EPISODE_NUMBER: {client.EPISODE_NUMBER}");
                                var clientAge = reader["c_age"].ToString();
                                logger.Debug($"Reader value for clientAge: {clientAge}");
                                client.Age = double.TryParse(reader["c_age"].ToString(), out number) ? Convert.ToInt32(number) : 99;
                                logger.Debug($"Line 55 Age {client.Age}");
                                client.PATID = reader["PATID"].ToString();
                                client.UnitValue = reader["unit_value"].ToString();
                                client.Room = reader["room"].ToString();
                                logger.Debug($"Age is: {client.Age}");
                                clients.Add(client);                                
                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetBedAssignmentByAgeCheck: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", "_connectionStringCollection.CWS", ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetBedAssignmentByAgeCheck: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }

            return clients;
        }
        public Client GetBedAssignmentAgeCheck(string facility, string patientID)
        {
            string commandString;
            string patientIDParameter = "";
            commandString = $@"SELECT bed.FACILITY
                                    , bed.EPISODE_NUMBER
                                    , bed.PATID                                    
                                    , bed.unit_code
                                    , bed.unit_value
                                    , bed.room
                                    , bed.bed
                                    , bed.date_of_bed_assignment
                                    , bed.end_date_of_bed_assignment
                                    , cur.c_age 
                                    FROM   SYSTEM.history_bed_assignment bed
                                    INNER JOIN SYSTEM.view_episode_summary_current cur ON bed.FACILITY = cur.FACILITY 
                                    AND bed.PATID = cur.PATID
                                    AND bed.EPISODE_NUMBER = cur.EPISODE_NUMBER
                                    WHERE  bed.FACILITY= ?   
                                    AND bed.PATID = ?                                   
                                    AND bed.end_date_of_bed_assignment IS NULL";


            //Age age = new Age(DateTime.Now);
            Client client = new Client();
            //List<Client> clients = new List<Client>();
            using (OdbcConnection connection = new OdbcConnection(_connectionStringCollection.PM))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("PATID", patientID));                

                //if (!string.IsNullOrEmpty(patientID))
                //{
                //    patientIDParameter = $"AND bed.PATID = {patientID}";
                //}
                //else
                //{
                //    patientIDParameter = "";
                //}


                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        logger.Debug($"Reader has rows: {reader.HasRows}");
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int result;
                                double number;
                                client.FACILITY = reader["FACILITY"].ToString();
                                logger.Debug($"FACILITY: {client.FACILITY}");
                                client.EPISODE_NUMBER = double.TryParse(reader["EPISODE_NUMBER"].ToString(), out number) ? number : 99;
                                logger.Debug($"EPISODE_NUMBER: {client.EPISODE_NUMBER}");
                                var clientAge = reader["c_age"].ToString();
                                logger.Debug($"Reader value for clientAge: {clientAge}");
                                client.Age = double.TryParse(reader["c_age"].ToString(), out number) ? Convert.ToInt32(number) : 99;
                                logger.Debug($"Line 162 Age {client.Age}");
                                client.PATID = reader["PATID"].ToString();
                                client.UnitValue = reader["unit_value"].ToString();
                                client.Room = reader["room"].ToString();
                                logger.Debug($"Age is: {client.Age}");
                                //clients.Add(client);
                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetBedAssignmentByAgeCheck: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", "_connectionStringCollection.CWS", ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetBedAssignmentByAgeCheck: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }

            return client;
        }
    }
}
