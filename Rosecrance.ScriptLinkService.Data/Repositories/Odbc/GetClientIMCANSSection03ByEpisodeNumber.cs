using Rosecrance.ScriptLinkService.Data.Models;
using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        // Selects the most recent
        // See also https://cedocs.intersystems.com/latest/csp/docbook/DocBook.UI.Page.cls?KEY=RSQL_totimestamp
        public IMCANSSection03 GetClientIMCANSSection03ByEpisodeNumber(string facility, string patientId, double episodeNumber)
        {
            string commandString = @"SELECT TOP (1) a.BE_Depression,
                                                    a.BE_Anxiety,
                                                    a.BE_Eating,
                                                    a.TR_Adjustment_Trauma,
                                                    a.BE_Regulatory,
                                                    a.BE_Thrive,
                                                    a.BE_Atypical,
                                                    a.BE_Oppositional,
                                                    a.BE_Inpulsivity,
                                                    a.BE_Anger,
                                                    a.BE_Substance_use,
                                                    a.BE_Psychosis,
                                                    a.BE_Conduct,
                                                    a.BE_Interpersonal,
                                                    a.Mania,
                                                    a.Somatization,
                                                    a.TR_Emotional,
                                                    a.TR_intrusion,
                                                    a.TR_Hyperarousal,
                                                    a.BE_Attachment,
                                                    a.TR_Grief,
                                                    a.TR_Numbing,
                                                    a.TR_Dissociation,
                                                    a.TR_Avoidance,
                                                    a.L_Family,
                                                    a.L_Living,
                                                    a.L_residential_stability,
                                                    a.L_Social,
                                                    a.L_Recreation,
                                                    a.L_Developmental,
                                                    a.L_Communication,
                                                    a.L_Medical,
                                                    a.L_Medication,
                                                    a.C_Transportation,
                                                    a.L_Sleep,
                                                    a.L_Motor,
                                                    a.L_Sensory,
                                                    a.L_Curiosity,
                                                    a.L_Feeding,
                                                    a.L_School,
                                                    a.L_Judgment,
                                                    a.L_Legal,
                                                    a.L_Sexual,
                                                    a.L_Job,
                                                    a.L_Parental,
                                                    a.L_Independent_Living,
                                                    a.L_Intimate,
                                                    a.L_Daily_Living,
                                                    a.L_Routiens,
                                                    a.L_Functinal,
                                                    a.l_Loneliness,
                                                    a.D_Cognitive,
                                                    a.D_Developmental,
                                                    a.D_Selfcare,
                                                    a.D_Autism,
                                                    a.D_Sensory,
                                                    a.D_Motor,
                                                    a.D_Regulatory,
                                                    a.S_Behavior,
                                                    a.S_Achievement,
                                                    a.S_Attendance,
                                                    a.S_Relationships,
                                                    a.S_Preschool,
                                                    a.V_Career,
                                                    a.V_Job,
                                                    a.V_Attendance,
                                                    a.V_Performance,
                                                    a.V_Relations,
                                                    a.V_Skills,
                                                    a.P_Knowledge,
                                                    a.P_Supervision,
                                                    a.P_Involvement,
                                                    a.P_Organization,
                                                    a.P_Marital,
                                                    a.DL_Meal,
                                                    a.DL_Shopping,
                                                    a.DL_housework,
                                                    a.DL_Money,
                                                    a.DL_Communication,
                                                    a.DL_Housing,
                                                    a.P_Supporting,
                                                    -- For Testing and Validation
                                                    a.FACILITY,
                                                    a.PATID,
                                                    a.EPISODE_NUMBER,
                                                    a.GI_Type, a.GI_Type_Value,
                                                    a.Data_Entry_Date,
                                                    a.Data_Entry_Time
                                       FROM SYSTEM.AP_CWS_CAN_Comp_Assess1 a 
                                      WHERE a.FACILITY=?
                                        AND a.PATID=?
                                        AND a.EPISODE_NUMBER=?
                                      ORDER BY a.Data_Entry_Date DESC,
                                               TO_TIMESTAMP(a.Data_Entry_Time,'HH:MI:SS.FF') DESC";

            IMCANSSection03 imCans = new IMCANSSection03();
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
                                imCans.Section03a.Depression = IMCANSGetRating(GetStringValue(reader, "BE_Depression"));
                                imCans.Section03a.Anxiety = IMCANSGetRating(GetStringValue(reader, "BE_Anxiety"));
                                imCans.Section03a.EatingDisturbance = IMCANSGetRating(GetStringValue(reader, "BE_Eating"));
                                imCans.Section03a.AdjustmentToTrauma = IMCANSGetRating(GetStringValue(reader, "TR_Adjustment_Trauma"));
                                imCans.Section03a.Regulatory = IMCANSGetRating(GetStringValue(reader, "BE_Regulatory"));
                                imCans.Section03a.FailureToThrive = IMCANSGetRating(GetStringValue(reader, "BE_Thrive"));
                                imCans.Section03a.AtypicalRepetitiveBehaviors = IMCANSGetRating(GetStringValue(reader, "BE_Atypical"));
                                imCans.Section03a.Oppositional = IMCANSGetRating(GetStringValue(reader, "BE_Oppositional"));
                                imCans.Section03a.ImpulsivityHyperactivity = IMCANSGetRating(GetStringValue(reader, "BE_Inpulsivity"));
                                imCans.Section03a.AngerControlFrustrationTolerance = IMCANSGetRating(GetStringValue(reader, "BE_Anger"));
                                imCans.Section03a.SubstanceUse = IMCANSGetRating(GetStringValue(reader, "BE_Substance_use"));
                                imCans.Section03a.Psychosis = IMCANSGetRating(GetStringValue(reader, "BE_Psychosis"));
                                imCans.Section03a.ConductAntisocialBehavior = IMCANSGetRating(GetStringValue(reader, "BE_Conduct"));
                                imCans.Section03a.InterpersonalProblems = IMCANSGetRating(GetStringValue(reader, "BE_Interpersonal"));
                                imCans.Section03a.Mania = IMCANSGetRating(GetStringValue(reader, "Mania"));
                                imCans.Section03a.Somatization = IMCANSGetRating(GetStringValue(reader, "Somatization"));

                                imCans.Section03a.TssmEmotionalPhysicalDysregulation = IMCANSGetRating(GetStringValue(reader, "TR_Emotional"));
                                imCans.Section03a.TssmIntrusionReexperiencing = IMCANSGetRating(GetStringValue(reader, "TR_intrusion"));
                                imCans.Section03a.TssmHyperarousal = IMCANSGetRating(GetStringValue(reader, "TR_Hyperarousal"));
                                imCans.Section03a.TssmAttachmentDifficulties = IMCANSGetRating(GetStringValue(reader, "BE_Attachment"));
                                imCans.Section03a.TssmTraumaticGriefSeparation = IMCANSGetRating(GetStringValue(reader, "TR_Grief"));
                                imCans.Section03a.TssmNumbing = IMCANSGetRating(GetStringValue(reader, "TR_Numbing"));
                                imCans.Section03a.TssmDissociation = IMCANSGetRating(GetStringValue(reader, "TR_Dissociation"));
                                imCans.Section03a.TssmAvoidance = IMCANSGetRating(GetStringValue(reader, "TR_Avoidance"));

                                imCans.Section03b.FamilyFunctioning = IMCANSGetRating(GetStringValue(reader, "L_Family"));
                                imCans.Section03b.LivingSituation = IMCANSGetRating(GetStringValue(reader, "L_Living"));
                                imCans.Section03b.ResidentialStability = IMCANSGetRating(GetStringValue(reader, "L_residential_stability"));
                                imCans.Section03b.SocialFunctioning = IMCANSGetRating(GetStringValue(reader, "L_Social"));
                                imCans.Section03b.RecreationPlay = IMCANSGetRating(GetStringValue(reader, "L_Recreation"));
                                imCans.Section03b.DevelopmentalIntellectual = IMCANSGetRating(GetStringValue(reader, "L_Developmental"));
                                imCans.Section03b.Communication = IMCANSGetRating(GetStringValue(reader, "L_Communication"));
                                imCans.Section03b.MedicalPhysical = IMCANSGetRating(GetStringValue(reader, "L_Medical"));
                                imCans.Section03b.MedicationCompliance = IMCANSGetRating(GetStringValue(reader, "L_Medication"));
                                imCans.Section03b.Transportation = IMCANSGetRating(GetStringValue(reader, "C_Transportation"));
                                imCans.Section03b.Sleep = IMCANSGetRating(GetStringValue(reader, "L_Sleep"));
                                imCans.Section03b.Motor = IMCANSGetRating(GetStringValue(reader, "L_Motor"));
                                imCans.Section03b.Sensory = IMCANSGetRating(GetStringValue(reader, "L_Sensory"));
                                imCans.Section03b.PersistenceCuriosityAdaptibility = IMCANSGetRating(GetStringValue(reader, "L_Curiosity"));
                                imCans.Section03b.Elimination = IMCANSGetRating(GetStringValue(reader, "L_Feeding"));
                                imCans.Section03b.SchoolPreschoolDaycare = IMCANSGetRating(GetStringValue(reader, "L_School"));
                                imCans.Section03b.DecisionMaking = IMCANSGetRating(GetStringValue(reader, "L_Judgment"));
                                imCans.Section03b.Legal = IMCANSGetRating(GetStringValue(reader, "L_Legal"));
                                imCans.Section03b.SexualDevelopment = IMCANSGetRating(GetStringValue(reader, "L_Sexual"));
                                imCans.Section03b.JobFunctioningEmployment = IMCANSGetRating(GetStringValue(reader, "L_Job"));
                                imCans.Section03b.ParentalCaregiveringRole = IMCANSGetRating(GetStringValue(reader, "L_Parental"));
                                imCans.Section03b.IndependentLivingSkills = IMCANSGetRating(GetStringValue(reader, "L_Independent_Living"));
                                imCans.Section03b.IntimateRelationships = IMCANSGetRating(GetStringValue(reader, "L_Intimate"));
                                imCans.Section03b.BasicActivitiesOfDailyLiving = IMCANSGetRating(GetStringValue(reader, "L_Daily_Living"));
                                imCans.Section03b.Routines = IMCANSGetRating(GetStringValue(reader, "L_Routiens"));
                                imCans.Section03b.FunctionalCommunication = IMCANSGetRating(GetStringValue(reader, "L_Functinal"));
                                imCans.Section03b.Loneliness = IMCANSGetRating(GetStringValue(reader, "l_Loneliness"));

                                imCans.Section03b.DdCognitive = IMCANSGetRating(GetStringValue(reader, "D_Cognitive"));
                                imCans.Section03b.DdDevelopmental = IMCANSGetRating(GetStringValue(reader, "D_Developmental"));
                                imCans.Section03b.DdSelfCareDailyLivingSkills = IMCANSGetRating(GetStringValue(reader, "D_Selfcare"));
                                imCans.Section03b.DdAutismSpectrum = IMCANSGetRating(GetStringValue(reader, "D_Autism"));
                                imCans.Section03b.DdSensory = IMCANSGetRating(GetStringValue(reader, "D_Sensory"));
                                imCans.Section03b.DdMotor = IMCANSGetRating(GetStringValue(reader, "D_Motor"));
                                imCans.Section03b.DdRegulatory = IMCANSGetRating(GetStringValue(reader, "D_Regulatory"));

                                imCans.Section03b.SpdcBehavior = IMCANSGetRating(GetStringValue(reader, "S_Behavior"));
                                imCans.Section03b.SpdcAchievement = IMCANSGetRating(GetStringValue(reader, "S_Achievement"));
                                imCans.Section03b.SpdcAttendance = IMCANSGetRating(GetStringValue(reader, "S_Attendance"));
                                imCans.Section03b.SpdcRelationshipsWithTeachers = IMCANSGetRating(GetStringValue(reader, "S_Relationships"));
                                imCans.Section03b.SpdcPreschoolDaycareQuality = IMCANSGetRating(GetStringValue(reader, "S_Preschool"));

                                imCans.Section03b.VcCareerAspirations = IMCANSGetRating(GetStringValue(reader, "V_Career"));
                                imCans.Section03b.VcJobTime = IMCANSGetRating(GetStringValue(reader, "V_Job"));
                                imCans.Section03b.VcJobAttendance = IMCANSGetRating(GetStringValue(reader, "V_Attendance"));
                                imCans.Section03b.VcJobPerformance = IMCANSGetRating(GetStringValue(reader, "V_Performance"));
                                imCans.Section03b.VcJobRelations = IMCANSGetRating(GetStringValue(reader, "V_Relations"));
                                imCans.Section03b.VcJobSkills = IMCANSGetRating(GetStringValue(reader, "V_Skills"));

                                imCans.Section03b.PcKnowledgeOfNeeds = IMCANSGetRating(GetStringValue(reader, "P_Knowledge"));
                                imCans.Section03b.PcSupervision = IMCANSGetRating(GetStringValue(reader, "P_Supervision"));
                                imCans.Section03b.PcInvolvementWithCare = IMCANSGetRating(GetStringValue(reader, "P_Involvement"));
                                imCans.Section03b.PcOrganization = IMCANSGetRating(GetStringValue(reader, "P_Organization"));
                                imCans.Section03b.PcMaritalPartnerViolenceInTheHome = IMCANSGetRating(GetStringValue(reader, "P_Marital"));

                                imCans.Section03b.IadlMealPreparation = IMCANSGetRating(GetStringValue(reader, "DL_Meal"));
                                imCans.Section03b.IadlShopping = IMCANSGetRating(GetStringValue(reader, "DL_Shopping"));
                                imCans.Section03b.IadlHousework = IMCANSGetRating(GetStringValue(reader, "DL_housework"));
                                imCans.Section03b.IadlMoneyManagement = IMCANSGetRating(GetStringValue(reader, "DL_Money"));
                                imCans.Section03b.IadlCommunicationDeviceUse = IMCANSGetRating(GetStringValue(reader, "DL_Communication"));
                                imCans.Section03b.IadlHousingSafety = IMCANSGetRating(GetStringValue(reader, "DL_Housing"));

                                imCans.SupportingInformation = GetStringValue(reader, "P_Supporting");
                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetClientIMCANSSection03ByEpisodeNumber: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", _connectionStringCollection.PM, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetClientIMCANSSection03ByEpisodeNumber: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }
            return imCans;
        }
    }
}
