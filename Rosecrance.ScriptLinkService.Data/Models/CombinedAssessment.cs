using System;
using Rosecrance.ScriptLinkService.Data.Helpers;

namespace Rosecrance.ScriptLinkService.Data.Models
{
    public class CombinedAssessment
    {
        public CombinedAssessment()
        {
            Doc1DateUsed = new DateTime().ToString();

        }

        public string Facility { get; set; }
        public string PatientId { get; set; }
        public double EpisodeNumber { get; set; }

        public string PppeProblem { get; set; }
        public string PppeDuration { get; set; }
        public string PppeSeverity { get; set; }
        public string PppeOthersSxCo { get; set; }
        public string PppeSa { get; set; }
        public string PppeSaMessage
        {
            get
            {
                if (PppeSa == "1")
                    return "Client reports a history of substance use.";
                else if (PppeSa == "2")
                    return "Client denies a history of substance use.";
                return "";
            }
        }
        public string PppeMh { get; set; }
        public string PppeMhMessage
        {
            get
            {
                if (PppeMh == "1")
                    return "Client has a history of mental health symptoms.";
                else if (PppeMh == "2")
                    return "Client denies a history of mental health symptoms or diagnoses.";
                return "";
            }
        }

        public string MhsFamCo { get; set; }

        public string DocSupport { get; set; }
        public string DocSupportMessage
        {
            get
            {
                if (DocSupport == "1")
                    return "Client reports having support from family and friends.";
                else if (DocSupport == "2")
                    return "Client denies having support from family and friends.";
                return "";
            }
        }
        public string DocFamPerception { get; set; }
        public string DocGenSubUseHx { get; set; }
        public string DocStageofChangeValue { get; set; }
        public string Doc1DocValue { get; set; }
        public string SsDoc1IAValue { get; set; }
        public string Doc1Message
        {
            get
            {
                string formattedString;
                if (Facility == "5")
                {
                    formattedString = RoseConvert.FormattedValue(SsDoc1IAValue);
                    return formattedString;
                }
                else
                {
                    formattedString = RoseConvert.FormattedValue(Doc1DocValue);
                    return formattedString;
                }
            }
        }
        public string Doc1Age1Use { get; set; }
        public string Doc1DateUsed { get; set; }
        public string Doc1FreqValue { get; set; }
        public string Doc1RouteValue { get; set; }
            
        public string Doc1RouteMessage
        {
            get
            {
                string formattedString;
                formattedString = RoseConvert.FormattedValue(Doc1RouteValue);
                return formattedString;
            }

        }
        public string Doc1SxValue { get; set; }
        public string Doc1SxMessage
        {
            get
            {
                string multiSelectString = RoseConvert.MultiSelectFormat(Doc1SxValue);  //F13
                return multiSelectString;
            }
        }
        public string WithdrawalAuditoryValue { get; set; }
        public string WithdrawalAuditoryMessage
        {
            get
            {
                string formattedString;
                formattedString = RoseConvert.FormattedValue(WithdrawalAuditoryValue);
                return formattedString;
            }
        }
        public string WithdrawalDiaphoresisValue { get; set; }
        public string WithdrawalDiaphoresisMessage
        {
            get
            {
                string formattedString;
                formattedString = RoseConvert.FormattedValue(WithdrawalDiaphoresisValue);
                return formattedString;
            }
        }
        public string WithdrawalDiarrheaValue { get; set; }
        public string WithdrawalDiarrheaMessage
        {
            get
            {
                string formattedString;
                formattedString = RoseConvert.FormattedValue(WithdrawalDiarrheaValue);
                return formattedString;
            }
        }
        public string WithdrawalNauseaVomitingValue { get; set; }
        public string WithdrawalNauseaVomitingMessage 
        {
            get
            {
                string formattedString;
                formattedString = RoseConvert.FormattedValue(WithdrawalNauseaVomitingValue);
                return formattedString;
            }
        }
        public string WithdrawalSeizureValue { get; set; }
        public string WithdrawalSeizureMessage 
        {
            get
            {
                string formattedString;
                formattedString = RoseConvert.FormattedValue(WithdrawalSeizureValue);
                return formattedString;
            }
        }
        public string WithdrawalTactileValue { get; set; }
        public string WithdrawalTactileMessage 
        {
            get
            {
                string formattedString;
                formattedString = RoseConvert.FormattedValue(WithdrawalTactileValue);
                return formattedString;
            }
        }
        public string WithdrawalTremorsValue { get; set; }
        public string WithdrawalTremorsMessage
        {
            get
            {
                string formattedString;
                formattedString = RoseConvert.FormattedValue(WithdrawalTremorsValue);
                return formattedString;
            }
        }
        public string WithdrawalVisualValue { get; set; }
        public string WithdrawalVisualMessage
        {
            get
            {
                string formattedString;
                formattedString = RoseConvert.FormattedValue(WithdrawalVisualValue);
                return formattedString;
            }
        }
        public string WithdrawalMsOrientedValue { get; set; }
        public string WithdrawalMsOrientedMessage
        {
            get
            {
                string multiSelectString = RoseConvert.MultiSelectFormat(WithdrawalMsOrientedValue); //O9
                return multiSelectString;
            }
        }
        public string WithdrawalMsSymptomsValue { get; set; }
        public string WithdrawalMsSymptomsMessage
        {
            get
            {
                string multiSelectString = RoseConvert.MultiSelectFormat(WithdrawalMsSymptomsValue); //O10
                return multiSelectString;
            }
        }

        public string TxHxPriSaEvaluation { get; set; }
        public string TxHxPriSaEvaluationMessage
        {
            get
            {
                if (TxHxPriSaEvaluation == "1")
                    return "Client has previously participated in an SA evaluation.";
                else if (TxHxPriSaEvaluation == "2")
                    return "Client has not previously participated in an SA evaluation.";
                return "";
            }
        }
        public string TxHxPriSaTxHx { get; set; }
        public string TxHxPriSaTxHxMessage
        {
            get
            {
                if (TxHxPriSaTxHx == "1")
                    return "Client has sought SA treatment in the past.";
                else if (TxHxPriSaTxHx == "2")
                    return "Client has not sought SA treatment in the past.";
                return "";
            }
        }
        public int TxHxPriSaTxHxNum { get; set; }
        public int NuPriorTxAdmit { get; set; }
        public int NuNonTxSAHosp { get; set; }
        public string NuNonTxSAHospMessage 
        {
            get
            {
                if (NuNonTxSAHosp > 0)
                {
                    return "Client reports having been hospitalized previously for medical reasons.";
                }
                else if (NuNonTxSAHosp == 0)
                {
                    return "Client denies any previous hospitalizations for medical reasons.";
                }
                else
                {
                    return "";
                }
            }
        }
        public string TxHxPriSaSelfHelp { get; set; }
        public string TxHxPriSaSelfHelpMessage
        {
            get
            {
                if (TxHxPriSaSelfHelp == "1")
                    return "Client reports having attended self-help groups in the past month.";
                else if (TxHxPriSaSelfHelp == "2")
                    return "Client denies having attended any self-help groups in the past month.";
                return "";
            }
        }
        public int TxHxPriSaSelfHelpNo { get; set; }
        public string TxHxPriSaSelfHelpNoMessage
        {
            get
            {                
                if (TxHxPriSaSelfHelp == "1" && TxHxPriSaSelfHelpNo != 0)
                {
                    return "How Many: " + TxHxPriSaSelfHelpNo.ToString();
                }
                else if (TxHxPriSaSelfHelp == "1" && TxHxPriSaSelfHelpNo == 0)
                {
                    return "How Many: ";                        
                }
                else
                {
                    return "";
                }
            }
        }
        public string MhTxHxPriPriorEvaluation { get; set; }
        public string MhTxHxPriPriorEvaluationMessage
        {
            get
            {
                if (MhTxHxPriPriorEvaluation == "1")
                    return "Client has participated in a psychiatric evaluation.";
                else if (MhTxHxPriPriorEvaluation == "2")
                    return "Client denies ever having participated in a psychiatric evaluation.";
                return "";
            }
        }
        public string MhTxHxPriPriorTx { get; set; }
        public string MhTxHxPriPriorTxMessage
        {
            get
            {
                if (MhTxHxPriPriorTx == "1")
                    return "Client has engaged in mental health treatment in the past.";
                else if (MhTxHxPriPriorTx == "2")
                    return "Client denies ever participating in mental health treatment.";
                return "";
            }
        }

        public string SingleFormMsHxHomicidalValue { get; set; }
        public string SingleFormMsHxHomicidalMessage
        {
            get
            {
                string multiSelectString = RoseConvert.MultiSelectFormat(SingleFormMsHxHomicidalValue); //C49
                return multiSelectString;
            }
        }
        public string SingleFormMsCurHomicidalValue { get; set; }
        public string SingleFormMsCurHomicidalMessage
        {
            get
            {
                string multiSelectString = RoseConvert.MultiSelectFormat(SingleFormMsCurHomicidalValue); //C51
                return multiSelectString;
            }
        }
        public string SaMhSelfHarmHistory { get; set; }
        public string SaMhSelfHarmHistoryMessage
        {
            get
            {
                if (SaMhSelfHarmHistory == "1")
                    return "Yes";
                else if (SaMhSelfHarmHistory == "2")
                    return "No";
                else
                    return "";
            }
        }
        public string SaMhScreenSelfHarmRiskValue { get; set; }
        public string SaMhScreenSelfHarmRiskResponse
        {
            get
            {
                switch (SaMhScreenSelfHarmRiskValue)
                {
                    case "0":
                        return "None";
                    case "1":
                        return "Low";
                    case "2":
                        return "Medium";
                    case "3":
                        return "High";
                    default:
                        return "";
                }
            }
        }
        public string PshxsrTraumaMrValue { get; set; }
        public string PshxsrTraumaMrMessage
        {
            get
            {
                string multiSelectString = RoseConvert.MultiSelectFormat(PshxsrTraumaMrValue); //G2
                return multiSelectString;
            }
        }
        public string BmsCurMeds { get; set; }
        public string BmsCurMedsMessage
        {
            get
            {
                if (BmsCurMeds == "1")
                    return "The client is currently taking medications.";
                else if (BmsCurMeds == "2")
                    return "The client denies taking any medications currently.";
                return "";
            }
        }
        public string BmsCurMedsCo { get; set; }
        public string BmsDcMeds { get; set; }
        public string BmsDcMedsMessage
        {
            get
            {
                if (BmsDcMeds == "1")
                    return "Client reports taking medications in the past.";
                else if (BmsDcMeds == "2")
                    return "Client denies any past medications.";
                return "";
            }
        }
        public string BmsHsptSr { get; set; }
        public string BmsHsptSrMessage
        {
            get
            {
                if (BmsHsptSr == "1")
                    return "Client reports having been hospitalized previously for medical reasons.";
                else if (BmsHsptSr == "2")
                    return "Client denies any previous hospitalizations for medical reasons.";
                return "";
            }
        }
        public string BmsProcedure { get; set; }
        public string BmsProcedureMessage
        {
            get
            {
                if (BmsProcedure == "1")
                    return "Client reports having a medical procedure.";
                else if (BmsProcedure == "2")
                    return "Client denies any medical procedures in the past 5 years.";
                return "";
            }
        }
        public string BmsPeTimeSrValue { get; set; }
        public string BmsPeTimeNsTxt { get; set; }
        public string BmsApptSr { get; set; }
        public string BmsApptSrMessage
        {
            get
            {
                if (BmsApptSr == "1")
                    return "Client reports an upcoming appointment.";
                else if (BmsApptSr == "2")
                    return "Client denies any upcoming appointments.";
                return "";
            }
        }
        public string BmsContagionMrValue { get; set; }
        public string BmsContagionMrMessage
        {
            get
            {
                string multiSelectString = RoseConvert.MultiSelectFormat(BmsContagionMrValue); //L7
                return multiSelectString;
            }
        }
        public string LegalHxYn { get; set; }
        public string LegalHxYnMessage
        {
            get
            {
                if (LegalHxYn == "0001")
                    return "Client currently involved in legal/court system.";
                else if (LegalHxYn == "0002")
                    return "Client denies current involvement in legal/court system.";
                return "";
            }
        }
        public string LegalHxYnLglHx { get; set; }
        public string LegalHxYnLglHxMessage
        {
            get
            {
                if (LegalHxYnLglHx == "0001")
                    return "Client has history of legal system involvement.";
                else if (LegalHxYnLglHx == "0002")
                    return "Client denies history of legal system involvement.";
                return "";
            }
        }
        public string LegalCourtOrderValue { get; set; }
        public string LegalCourtOrderMessage
        {
            get
            {
                if (LegalCourtOrderValue == "Yes")
                    return "Client has court ordered treatment.";
                else if (LegalCourtOrderValue == "No")
                    return "Client has no court ordered treatment.";
                return "";
            }
        }
        public string LegalRecordsValue { get; set; }
        public string LegalRecordsMessage
        {
            get
            {
                if (LegalRecordsValue == "Yes")
                    return "Legal records are required.";
                else if (LegalRecordsValue == "No")
                    return "No legal records are required.";
                return "";
            }
        }
    }
}
