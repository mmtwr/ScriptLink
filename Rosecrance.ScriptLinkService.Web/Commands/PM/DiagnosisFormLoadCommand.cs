using NLog;
using RarelySimple.AvatarScriptLink.Objects;
using Rosecrance.ScriptLinkService.Data.Models;
using Rosecrance.ScriptLinkService.Data.Repositories;
using System;
using System.Data.Odbc;
using System.Linq;

namespace Rosecrance.Diagnosis.Web.Commands
{
    public class DiagnosisFormLoadCommand : IRunScriptCommand
    {
        // ***********************************************************************************************
        //  Diagnosis Form Load routine
        //  
        //  02-02-2021 Matt Becker - https://jira.r.local/browse/SCRIPTLINK-72
        // ***********************************************************************************************

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly OptionObject2015 _optionObject2015;
        private readonly IGetDataRepository _repository;

        public DiagnosisFormLoadCommand(OptionObject2015 optionObject2015, IGetDataRepository repository) 
        {
            _optionObject2015 = optionObject2015;
            _repository = repository;
        }

        public OptionObject2015 Execute()
        {
            string facility = _optionObject2015.Facility;
            string userID = _optionObject2015.OptionUserId;
            string userRole = _repository.GetUserRoleByUserId(facility, userID);
            //string draftFinalFieldNumber = "295.3";
            //string draftFinal = _optionObject2015.GetFieldValue(draftFinalFieldNumber);
            //string dispositionFieldNumber = "295.14";
            //string disposition = _optionObject2015.GetFieldValue(dispositionFieldNumber);            
            string returnMessage = "";



            //https://jira.r.local/browse/SCRIPTLINK-70 requires Is this patient enrolled in a program that must meet 42 CFR Part 2 Regulations and defaults to 'Yes'
            string is42CFRPart2RegulationsFieldNumber = "36017";
            if (_optionObject2015.IsFieldPresent(is42CFRPart2RegulationsFieldNumber))
            {
                _optionObject2015.SetRequiredField(is42CFRPart2RegulationsFieldNumber);
                _optionObject2015.SetFieldValue(is42CFRPart2RegulationsFieldNumber, "1");
                //logger.Debug($"Treatment setting: {treatmentSetting}");
                //if (treatmentSetting == "I")
                //{
                //    logger.Debug("Setting discharge date to required.");
                //    _optionObject2015.SetRequiredField(expectedDischargeDateFieldNumber);
                //}
                //else
                //{
                //    _optionObject2015.SetOptionalField(expectedDischargeDateFieldNumber);
                //}
                //logger.Debug($"Treatment setting: {treatmentSetting}");
            }

            // default Social Security field to 999-99-9999 at initial contact
            //string ssn = "6";
            //if (_optionObject2015.IsFieldPresent(ssn) && string.IsNullOrEmpty(_optionObject2015.GetFieldValue(ssn)) && setUnknown)
            //{
            //    logger.Debug("Setting default Social Security field to 999-99-9999 at initial contact.");
            //    string unknown = "999-99-9999";
            //    _optionObject2015.SetFieldValue(ssn, unknown);
            //}

            //logger.Debug($"facility: {facility} userID: {userID} userRole: {userRole} draftFinal: {draftFinal}");
            //if (!string.IsNullOrEmpty(userRole) && !userRole.Contains("FinanceBilling") && draftFinal == "F")
            //{
            //    _optionObject2015.ErrorCode = 1;  // this makes it a hard stop
            //    returnMessage = "Only users with the Finance Billing role can finalize Event Corrections.";
            //    return _optionObject2015.ToReturnOptionObject(ErrorCode.Error, returnMessage);
            //}

            //logger.Debug($"facility: {facility} userID: {userID} userRole: {userRole} draftFinal: {draftFinal} disposition: {disposition}");
            //if (!string.IsNullOrEmpty(userRole) && userRole.Contains("FinanceBilling") && draftFinal == "F" && string.IsNullOrEmpty(disposition))
            //{
            //    _optionObject2015.ErrorCode = 1;  // this makes it a hard stop
            //    returnMessage = "Disposition needs to be selected before finalizing.";
            //    return _optionObject2015.ToReturnOptionObject(ErrorCode.Error, returnMessage);
            //}

            return _optionObject2015.ToReturnOptionObject();
        }
    }
}