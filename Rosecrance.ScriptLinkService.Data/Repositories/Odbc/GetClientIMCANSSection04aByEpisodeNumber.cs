using Rosecrance.ScriptLinkService.Data.Models;
using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        // Selects most recent
        // See also https://cedocs.intersystems.com/latest/csp/docbook/DocBook.UI.Page.cls?KEY=RSQL_totimestamp
        public IMCANSSection04a GetClientIMCANSSection04aByEpisodeNumber(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT TOP (1) a.R_Victimization,
                                                    a.R_Self_harm2,
                                                    a.R_Flight,
                                                    a.R_Suicide,
                                                    a.R_Seeking,
                                                    a.R_Runaway,
                                                    a.R_Sexually,
                                                    a.R_Bullying,
                                                    a.R_Criminal,
                                                    a.R_Self_Mutilation,
                                                    a.R_Self_Harm,
                                                    a.R_others,
                                                    a.R_Fire,
                                                    a.R_Grave_Disability,
                                                    a.R_Hoarding,
                                                    a.RM_Frequency,
                                                    a.RM_Destination,
                                                    a.RM_Safety,
                                                    a.RM_Involvement,
                                                    a.RM_return,
                                                    a.RM_Others,
                                                    a.RM_Realistic,
                                                    a.RM_Planning,
                                                    a.S_Hyper,
                                                    a.S_High,
                                                    a.S_Masturbation,
                                                    a.S_Aggression,
                                                    a.S_Reactive,
                                                    a.SAB_Relationship,
                                                    a.SAB_Physical,
                                                    a.SAB_Planning,
                                                    a.SAB_Age_diff,
                                                    a.SAB_Power,
                                                    a.SAB_Type,
                                                    a.SAB_Response,
                                                    a.D_Hostility,
                                                    a.D_Paranoid,
                                                    a.D_Secondary,
                                                    a.D_Violent,
                                                    a.D_Intent,
                                                    a.D_Planning,
                                                    a.D_Violence,
                                                    a.RF_Violence,
                                                    a.RF_consequences,
                                                    a.RF_Self,
                                                    a.FS_Seriousness,
                                                    a.FS_History,
                                                    a.FS_Planning,
                                                    a.FS_Use_of_accelerants,
                                                    a.FS_Intention,
                                                    a.FS_Safety,
                                                    a.FS_Response,
                                                    a.FS_Remorse,
                                                    a.FS_Future,
                                                    a.FS_Supporting_Info,
                                                    a.JJ_Seriousness,
                                                    a.JJ_History,
                                                    a.JJ_Arrests,
                                                    a.JJ_Planning,
                                                    a.JJ_Safety,
                                                    a.JJ_Legal,
                                                    a.JJ_Peer,
                                                    a.JJ_Environmental,
                                                    a.CJ_Supporting,
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

            IMCANSSection04a imCans = new IMCANSSection04a();
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
                                imCans.VictimizationExploitation = IMCANSGetRating(GetStringValue(reader, "R_Victimization"));
                                imCans.SelfHarm = IMCANSGetRating(GetStringValue(reader, "R_Self_harm2"));
                                imCans.FlightRisk = IMCANSGetRating(GetStringValue(reader, "R_Flight"));
                                imCans.SuicideRisk = IMCANSGetRating(GetStringValue(reader, "R_Suicide"));
                                imCans.IntentionialMisbehavior = IMCANSGetRating(GetStringValue(reader, "R_Seeking"));
                                imCans.Runaway = IMCANSGetRating(GetStringValue(reader, "R_Runaway"));
                                imCans.SexuallyProblematicBehavior = IMCANSGetRating(GetStringValue(reader, "R_Sexually"));
                                imCans.BullyingOthers = IMCANSGetRating(GetStringValue(reader, "R_Bullying"));
                                imCans.DelinquentCriminalBehavior = IMCANSGetRating(GetStringValue(reader, "R_Criminal"));
                                imCans.NonSuicidalSelfInjurousBehavior = IMCANSGetRating(GetStringValue(reader, "R_Self_Mutilation"));
                                imCans.OtherSelfHarm = IMCANSGetRating(GetStringValue(reader, "R_Self_Harm"));
                                imCans.DangerToOthers = IMCANSGetRating(GetStringValue(reader, "R_others"));
                                imCans.FireSetting = IMCANSGetRating(GetStringValue(reader, "R_Fire"));
                                imCans.GraveDisability = IMCANSGetRating(GetStringValue(reader, "R_Grave_Disability"));
                                imCans.Hoarding = IMCANSGetRating(GetStringValue(reader, "R_Hoarding"));

                                imCans.RunawayFrequencyOfRunning = IMCANSGetRating(GetStringValue(reader, "RM_Frequency"));
                                imCans.RunawayConsistencyOfDestination = IMCANSGetRating(GetStringValue(reader, "RM_Destination"));
                                imCans.RunawaySafetyOfDestination = IMCANSGetRating(GetStringValue(reader, "RM_Safety"));
                                imCans.RunawayInvolvementInIllegalActs = IMCANSGetRating(GetStringValue(reader, "RM_Involvement"));
                                imCans.RunawayLikelihoodOfReturnOnOwn = IMCANSGetRating(GetStringValue(reader, "RM_return"));
                                imCans.RunawayInvolvementOfOthers = IMCANSGetRating(GetStringValue(reader, "RM_Others"));
                                imCans.RunawayRealisticExpectations = IMCANSGetRating(GetStringValue(reader, "RM_Realistic"));
                                imCans.RunawayPlanning = IMCANSGetRating(GetStringValue(reader, "RM_Planning"));

                                imCans.SpbHypersexuality = IMCANSGetRating(GetStringValue(reader, "S_Hyper"));
                                imCans.SpbHighRiskSexualBehavior = IMCANSGetRating(GetStringValue(reader, "S_High"));
                                imCans.SpbMasturbation = IMCANSGetRating(GetStringValue(reader, "S_Masturbation"));
                                imCans.SpbSexualAggression = IMCANSGetRating(GetStringValue(reader, "S_Aggression"));
                                imCans.SpbSexuallyReactiveBehavior = IMCANSGetRating(GetStringValue(reader, "S_Reactive"));

                                imCans.SabRelationship = IMCANSGetRating(GetStringValue(reader, "SAB_Relationship"));
                                imCans.SabPhysicalForceThreat = IMCANSGetRating(GetStringValue(reader, "SAB_Physical"));
                                imCans.SabPlanning = IMCANSGetRating(GetStringValue(reader, "SAB_Planning"));
                                imCans.SabAgeDifferential = IMCANSGetRating(GetStringValue(reader, "SAB_Age_diff"));
                                imCans.SabPowerDifferential = IMCANSGetRating(GetStringValue(reader, "SAB_Power"));
                                imCans.SabTypeOfSexAct = IMCANSGetRating(GetStringValue(reader, "SAB_Type"));
                                imCans.SabResponseToAccusation = IMCANSGetRating(GetStringValue(reader, "SAB_Response"));

                                imCans.DangerHostility = IMCANSGetRating(GetStringValue(reader, "D_Hostility"));
                                imCans.DangerParanoidThinking = IMCANSGetRating(GetStringValue(reader, "D_Paranoid"));
                                imCans.DangerSecondaryGainsFromAnger = IMCANSGetRating(GetStringValue(reader, "D_Secondary"));
                                imCans.DangerViolentThinking = IMCANSGetRating(GetStringValue(reader, "D_Violent"));
                                imCans.DangerIntent = IMCANSGetRating(GetStringValue(reader, "D_Intent"));
                                imCans.DangerPlanning = IMCANSGetRating(GetStringValue(reader, "D_Planning"));
                                imCans.DangerViolenceHistory = IMCANSGetRating(GetStringValue(reader, "D_Violence"));
                                imCans.DangerAwareOfViolencePotential = IMCANSGetRating(GetStringValue(reader, "RF_Violence"));
                                imCans.DangerResponseToConsequences = IMCANSGetRating(GetStringValue(reader, "RF_consequences"));
                                imCans.DangerCommitmentToSelfControl = IMCANSGetRating(GetStringValue(reader, "RF_Self"));

                                imCans.FireSeriousness = IMCANSGetRating(GetStringValue(reader, "FS_Seriousness"));
                                imCans.FireHistory = IMCANSGetRating(GetStringValue(reader, "FS_History"));
                                imCans.FirePlanning = IMCANSGetRating(GetStringValue(reader, "FS_Planning"));
                                imCans.FireUseOfAccelerants = IMCANSGetRating(GetStringValue(reader, "FS_Use_of_accelerants"));
                                imCans.FireIntentionToHarm = IMCANSGetRating(GetStringValue(reader, "FS_Intention"));
                                imCans.FireCommunitySafety = IMCANSGetRating(GetStringValue(reader, "FS_Safety"));
                                imCans.FireResponseToAccusation = IMCANSGetRating(GetStringValue(reader, "FS_Response"));
                                imCans.FireRemorse = IMCANSGetRating(GetStringValue(reader, "FS_Remorse"));
                                imCans.FireLikelihoodOfFutureFireSetting = IMCANSGetRating(GetStringValue(reader, "FS_Future"));

                                imCans.SupportingInformation1 = GetStringValue(reader, "FS_Supporting_Info");

                                imCans.JusticeSeriousness = IMCANSGetRating(GetStringValue(reader, "JJ_Seriousness"));
                                imCans.JusticeHistory = IMCANSGetRating(GetStringValue(reader, "JJ_History"));
                                imCans.JusticeArrests = IMCANSGetRating(GetStringValue(reader, "JJ_Arrests"));
                                imCans.JusticePlanning = IMCANSGetRating(GetStringValue(reader, "JJ_Planning"));
                                imCans.JusticeCommunitySafety = IMCANSGetRating(GetStringValue(reader, "JJ_Safety"));
                                imCans.JusticeLegalCompliance = IMCANSGetRating(GetStringValue(reader, "JJ_Legal"));
                                imCans.JusticePeerInfluences = IMCANSGetRating(GetStringValue(reader, "JJ_Peer"));
                                imCans.JusticeEnvironmentalInfluences = IMCANSGetRating(GetStringValue(reader, "JJ_Environmental"));

                                imCans.SupportingInformation2 = GetStringValue(reader, "CJ_Supporting");
                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetClientIMCANSSection04aByEpisodeNumber: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", _connectionStringCollection.PM, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetClientIMCANSSection04aByEpisodeNumber: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }
            return imCans;
        }
    }
}
