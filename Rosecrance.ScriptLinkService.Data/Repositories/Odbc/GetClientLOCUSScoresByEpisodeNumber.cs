using Rosecrance.ScriptLinkService.Data.Models;
using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public LOCUSScores GetClientLOCUSScoresByEpisodeNumber(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT TOP (1) s.nu_d1_score,
                                                    s.nu_d2_score,
                                                    s.nu_d3_score,
                                                    s.nu_d4a_score,
                                                    s.nu_d4b_score,
                                                    s.nu_d5_score,
                                                    s.nu_d6_score,
                                                    -- For Testing
                                                    s.FACILITY,
                                                    s.PATID,
                                                    s.EPISODE_NUMBER,
                                                    s.data_entry_date,
                                                    s.data_entry_time
                                       FROM ROSE.rose_asmnt_mh_scales s
                                      WHERE s.FACILITY=?
                                        AND s.PATID=?
                                        AND s.EPISODE_NUMBER=?
                                      ORDER BY s.Data_Entry_Date DESC,
                                               TO_TIMESTAMP(s.Data_Entry_Time,'HH:MI:SS.FF') DESC";

            LOCUSScores locusScores = new LOCUSScores();
            using (OdbcConnection connection = new OdbcConnection(_connectionStringCollection.CWS))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("PATID", patientId));
                command.Parameters.Add(new OdbcParameter("EPISODENUMBER", episodeNumber));

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                locusScores.D1Score = GetIntValue(reader, "nu_d1_score"); logger.Debug("LOCUS D1: {score}", GetIntValue(reader, "nu_d1_score"));
                                locusScores.D2Score = GetIntValue(reader, "nu_d2_score"); logger.Debug("LOCUS D2: {score}", GetIntValue(reader, "nu_d2_score"));
                                locusScores.D3Score = GetIntValue(reader, "nu_d3_score"); logger.Debug("LOCUS D3: {score}", GetIntValue(reader, "nu_d3_score"));
                                locusScores.D4aScore = GetIntValue(reader, "nu_d4a_score"); logger.Debug("LOCUS D4a: {score}", GetIntValue(reader, "nu_d4a_score"));
                                locusScores.D4bScore = GetIntValue(reader, "nu_d4b_score"); logger.Debug("LOCUS D4b: {score}", GetIntValue(reader, "nu_d4b_score"));
                                locusScores.D5Score = GetIntValue(reader, "nu_d5_score"); logger.Debug("LOCUS D5: {score}", GetIntValue(reader, "nu_d5_score"));
                                locusScores.D6Score = GetIntValue(reader, "nu_d6_score"); logger.Debug("LOCUS D6: {score}", GetIntValue(reader, "nu_d6_score"));
                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetClientLOCUSScoresByEpisodeNumber: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", _connectionStringCollection.CWS, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetClientLOCUSScoresByEpisodeNumber: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }
            return locusScores;
        }
    }
}
