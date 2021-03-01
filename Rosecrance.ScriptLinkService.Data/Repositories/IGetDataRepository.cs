using Rosecrance.ScriptLinkService.Data.Models;
using System;
using System.Collections.Generic;

namespace Rosecrance.ScriptLinkService.Data.Repositories
{
    public interface IGetDataRepository
    {
        // Client/Patient Queries
        int GetAgeFromVESC(string facility, string patientId, double episodeNumber);
        Client GetBedAssignmentAgeCheck(string facility, string patientID);
        List<Client> GetBedAssignmentAgeCheck(string facility, string unitCode, string room);
        CombinedAssessment GetClientCombinedAssessmentInfoByPatientId(string facility, string patientId, double episodeNumber);
        //CombinedAssessment GetClientCombinedAssessmentInfoByPatientId(string facility, string patientId);
        int GetClientCountOfOpenMHPrograms(string facility, string patientId); 
        int GetClientCountOfOtherOpenMHPrograms(string facility, string patientId, double episodeNumber);
        DateTime GetClientDateOfBirthByPatientId(string facility, string patientId);
        DateTime ? GetClientDateOfDischargeByEpisodeNumber(string facility, string patientId, double episodeNumber);
        string GetClientFirstAndLastInitialByPatientId(string facility, string patientId);
        int GetClientGAFScalesByEpisodeNumber(string facility, string patientId, double episodeNumber);
        IMCANSSection03 GetClientIMCANSSection03ByEpisodeNumber(string facility, string patientId, double episodeNumber);
        IMCANSSection04a GetClientIMCANSSection04aByEpisodeNumber(string facility, string patientId, double episodeNumber);
        IMCANSSection05 GetClientIMCANSSection05ByEpisodeNumber(string facility, string patientId, double episodeNumber);
        LOCUSScores GetClientLOCUSScoresByEpisodeNumber(string facility, string patientId, double episodeNumber);
        ILSR GetLatestILSRByEpisodeNumber(string facility, string patientId, double episodeNumber);
        string GetCWSUserDefinedDictionaryValue(string facility, string fieldNumber, string dictionaryCode);
        string GetFinancialEligibility(string facility, string patientId, double episodeNumber);
        string GetPMUserDefinedDictionaryValue(string facility, string fieldNumber, string dictionaryCode);
        string GetProgramCodeByEpisodeNumberFromVESA(string facility, string patientId, double episodeNumber);
        string GetProgramCodeByEpisodeNumberFromVESC(string facility, string patientId, double episodeNumber);
        string GetProgramCodeByEpisodeNumberFromVESD(string facility, string patientId, double episodeNumber);
        string GetProgramNameByEpisodeNumberFromVESA(string facility, string patientId, double episodeNumber);
        string GetProgramNameByEpisodeNumberFromVESC(string facility, string patientId, double episodeNumber);
        string GetProgramNameByEpisodeNumberFromVESD(string facility, string patientId, double episodeNumber);
        string GetProgramRRGByEpisodeNumber(string facility, string patientId, double episodeNumber);
        string GetProgramTypeCodeByEpisodeNumber(string facility, string patientId, double episodeNumber);
        bool HasAdmissionDiagnosisByEpisodeNumber(string facility, string patientId, double episodeNumber);
        bool HasDischargeDiagnosisByEpisodeNumber(string facility, string patientId, double episodeNumber);
        bool IsClientOpenToAnotherMHProgram(string facility, string patientId, double episodeNumber);

        // Staff Queries
        string GetStaffNameByStaffId(string facility, string staffId);

        // User Queries
        string GetUserEmailAddressByUserId(string facility, string userId);
        string GetUserIdByStaffId(string facility, string staffId);
        string GetUserRoleByUserId(string facility, string userId);

        // Program Queries
        string GetProgramDiagnosisRequiredByProgramCode(string facility, string programCode);
        string GetProgramDivisionByProgramCode(string facility, string programCode);
        string GetProgramTxServiceCodeByProgramCode(string facility, string programCode);

        // Other Miscellaneous Queries
        ReferralSource GetReferralSourceInfoBySourceCode(string facility, string sourceCode);
    }
}
