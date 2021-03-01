using NLog;
using RarelySimple.AvatarScriptLink.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Rosecrance.Diagnosis.Web.Commands
{
    public class ChkChrsCommand : IRunScriptCommand
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly OptionObject2015 _optionObject2015;
        private readonly string _parameter;

        public ChkChrsCommand(OptionObject2015 optionObject2015, string parameter)
        {
            _optionObject2015 = optionObject2015;
            _parameter = parameter;
        }

        public OptionObject2015 Execute()
        {
            string[] temp = _parameter.Split(',');
            var returnOptionObject = new OptionObject2015();
            var formObject = new FormObject();
            var verifyDataField = new FieldObject();
            string holdValidChars = temp[3];
            string fieldDesc = temp[4];
            string holdValidsAddr = "^[a-zA-Z0-9 ]*$";
            string holdValidsName = "^[a-zA-Z0-9, ]*$";
            string holdValids;

            //RegexStringValidator re = new RegexStringValidator(holdValidChars);
            if (holdValidChars == "N")
            // valid characters for a name field
            {
                holdValids = holdValidsName;
            }
            else
            // valid characters for an address field 
            {
                holdValids = holdValidsAddr;
            }

            RegexStringValidator re = new RegexStringValidator(holdValids);

            string returnMessage = "";
            try
            {
                formObject.FormId = temp[1];
                verifyDataField.FieldNumber = temp[2];

                foreach (var form in _optionObject2015.Forms)
                {
                    foreach (var field in form.CurrentRow.Fields)
                    {
                        if (field.FieldNumber == verifyDataField.FieldNumber)
                        {
                            verifyDataField.FieldValue = field.FieldValue;
                            formObject.MultipleIteration = form.MultipleIteration;
                        }
                    }
                }
                // allow regular characters

                re.Validate(verifyDataField.FieldValue);
                //var fields = new FieldObject[1];
                //fields[0] = verifyDataField;
                var currentRow = new RowObject
                {
                    Fields = new List<FieldObject>
                    {
                        verifyDataField
                    }
                };
                foreach (var form in _optionObject2015.Forms)
                {
                    if (form.FormId == formObject.FormId)
                    {
                        currentRow.RowId = form.CurrentRow.RowId;
                        currentRow.ParentRowId = form.CurrentRow.ParentRowId;
                    }
                }
            }
            catch (ArgumentException eo)
            {
                returnMessage = "There are one or more special characters in the '"
                 + fieldDesc + "' field";
                returnOptionObject.ErrorCode = 1;
                logger.Error(eo, returnMessage);
            }

            catch (Exception e)
            {
                returnMessage = e.Message + " -Validate Character Field- \n\n" +
                e.GetType().ToString() + "\n" +
                e.StackTrace;
                returnOptionObject.ErrorCode = 3;
                logger.Error(e, "An error occurred. Error: {errorMessage}.", e.Message);
            }

            returnOptionObject.ErrorMesg = returnMessage;

            returnOptionObject.EntityID = _optionObject2015.EntityID;
            returnOptionObject.EpisodeNumber = _optionObject2015.EpisodeNumber;
            returnOptionObject.Facility = _optionObject2015.Facility;
            returnOptionObject.NamespaceName = _optionObject2015.NamespaceName;
            returnOptionObject.OptionId = _optionObject2015.OptionId;
            returnOptionObject.OptionStaffId = _optionObject2015.OptionStaffId;
            returnOptionObject.OptionUserId = _optionObject2015.OptionUserId;
            returnOptionObject.ParentNamespace = _optionObject2015.ParentNamespace;
            returnOptionObject.ServerName = _optionObject2015.ServerName;
            returnOptionObject.SystemCode = _optionObject2015.SystemCode;
            returnOptionObject.SessionToken = _optionObject2015.SessionToken;

            return returnOptionObject;
        }
    }
}