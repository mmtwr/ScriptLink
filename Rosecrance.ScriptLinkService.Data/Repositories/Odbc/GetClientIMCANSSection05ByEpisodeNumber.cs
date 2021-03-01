using Rosecrance.ScriptLinkService.Data.Models;
using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public IMCANSSection05 GetClientIMCANSSection05ByEpisodeNumber(string facility, string patientId, double episodeNumber)
        {
            // Selects the most recent entry
            // See also https://cedocs.intersystems.com/latest/csp/docbook/DocBook.UI.Page.cls?KEY=RSQL_totimestamp
            string commandString = @"SELECT TOP (1) a.SA_Severity,
                                                    a.SA_Duration,
                                                    a.SA_Stage,
                                                    a.SA_Environmental,
                                                    a.SA_Peer,
                                                    a.SA_Parental,
                                                    a.SA_Recovery,
                                                    a.SA_Supporting,
                                                    -- For Testing and Validation
                                                    a.FACILITY,
                                                    a.PATID,
                                                    a.EPISODE_NUMBER,
                                                    a.GI_Type, a.GI_Type_Value,
                                                    a.Data_Entry_Date,
                                                    a.Data_Entry_Time
                                       FROM SYSTEM.AP_CWS_CAN_Comp_Assess2 a 
                                      WHERE a.FACILITY=?
                                        AND a.PATID=?
                                        AND a.EPISODE_NUMBER=?
                                      ORDER BY a.Data_Entry_Date DESC,
                                               TO_TIMESTAMP(a.Data_Entry_Time,'HH:MI:SS.FF') DESC";

            IMCANSSection05 imCans = new IMCANSSection05();
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
                                imCans.SeverityOfUse = IMCANSGetRating(GetStringValue(reader, "SA_Severity"));
                                imCans.DurationOfUse = IMCANSGetRating(GetStringValue(reader, "SA_Duration"));
                                imCans.StageOfRecovery = IMCANSGetRating(GetStringValue(reader, "SA_Stage"));
                                imCans.EnvironmentalInfluences = IMCANSGetRating(GetStringValue(reader, "SA_Environmental"));
                                imCans.PeerInfluences = IMCANSGetRating(GetStringValue(reader, "SA_Peer"));
                                imCans.ParentalInfluences = IMCANSGetRating(GetStringValue(reader, "SA_Parental"));
                                imCans.RecoverySupportInCommunity = IMCANSGetRating(GetStringValue(reader, "SA_Recovery"));
                                imCans.SupportingInformation = GetStringValue(reader, "SA_Supporting");
                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetClientIMCANSSection05ByEpisodeNumber: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", _connectionStringCollection.PM, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetClientIMCANSSection05ByEpisodeNumber: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }
            return imCans;
        }
    }
}