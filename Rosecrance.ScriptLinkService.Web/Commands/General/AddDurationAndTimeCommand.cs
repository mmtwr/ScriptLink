using System;
using System.Collections.Generic;
using NLog;
using RarelySimple.AvatarScriptLink.Objects;

namespace Rosecrance.Diagnosis.Web.Commands
{
    public class AddDurationAndTimeCommand : IRunScriptCommand
    {
        // ***************************************************************************
        // Add Duration and Time routine to either compute end time from
        // the start time and duration, or compute the duration from
        // the start and stop time. 
        // This is executed from a field with 5 parameters passed in:
        // example: AddDurationAndTime,286,237.44,237.45,237.46,End
        //  form number,start time field number,end time field number, 
        //  duration field number, End or Dur depending on what is to be 
        //  calculated (End will calculate the end time based on start and 
        //  duration)
        // ***************************************************************************

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly OptionObject2015 _optionObject2015;
        private readonly string _parameter;

        public AddDurationAndTimeCommand(OptionObject2015 optionObject2015, string parameter)
        {
            _optionObject2015 = optionObject2015;
            _parameter = parameter;
        }

        public OptionObject2015 Execute()
        {
            string[] temp = _parameter.Split(',');
            var returnOptionObject = new OptionObject2015();
            var formObject = new FormObject();
            var startTimeField = new FieldObject();
            var endTimeField = new FieldObject();
            var durationField = new FieldObject();
            string returnMessage = "";
            try
            {
                formObject.FormId = temp[1];
                startTimeField.FieldNumber = temp[2];
                endTimeField.FieldNumber = temp[3];
                durationField.FieldNumber = temp[4];
                foreach (var form in _optionObject2015.Forms)
                {
                    foreach (var field in form.CurrentRow.Fields)
                    {
                        if (field.FieldNumber == startTimeField.FieldNumber)
                            startTimeField.FieldValue = field.FieldValue;
                        else if (field.FieldNumber == endTimeField.FieldNumber)
                            endTimeField.FieldValue = field.FieldValue;
                        else if (field.FieldNumber == durationField.FieldNumber)
                        {
                            durationField.FieldValue = field.FieldValue;
                            formObject.MultipleIteration = form.MultipleIteration;
                        }
                    }
                }
                if (temp[5] == "Dur")
                {
                    // calculate the duration from the start time and end time
                    durationField.FieldValue = ChangeDuration(DateTime.Parse(startTimeField.FieldValue), DateTime.Parse(endTimeField.FieldValue)).TotalMinutes.ToString();
                }
                else
                {
                    // calculate the end time from the start time and duration
                    endTimeField.FieldValue = ChangeEndTime(DateTime.Parse(startTimeField.FieldValue), double.Parse(durationField.FieldValue)).ToString("hh:mmtt");
                }

                if (DateTime.Parse(startTimeField.FieldValue) > DateTime.Parse(endTimeField.FieldValue))
                {
                    logger.Error("Start Time must be before End Time");
                    throw new IndexOutOfRangeException("Start Time must be before End Time");
                }

                //var fields = new FieldObject[1];
                var fields = new List<FieldObject>
                {
                    temp[5] == "Dur" ? durationField : endTimeField
                };
                var currentRow = new RowObject
                {
                    Fields = fields
                };
                foreach (var form in _optionObject2015.Forms)
                {
                    if (form.FormId == formObject.FormId)
                    {
                        currentRow.RowId = form.CurrentRow.RowId;
                        currentRow.ParentRowId = form.CurrentRow.ParentRowId;
                    }
                }
                currentRow.RowAction = "EDIT";
                formObject.CurrentRow = currentRow;
                //var forms = new FormObject[1];
                //forms[0] = formObject;
                //returnOptionObject.Forms = forms;
                returnOptionObject.Forms.Add(formObject);
            }
            catch (FormatException)
            {
                logger.Info("The start date or duration is invalid!");
                //returnMessage = "\nStop: The start date or duration is invalid! ";
                //returnOptionObject.ErrorCode = 3;
            }
            catch (ArgumentOutOfRangeException eo)
            {
                logger.Error(eo, "Error: {errorMessage}", eo.Message);
                returnMessage = eo.Message;
                returnOptionObject.ErrorCode = 1;
            }
            catch (Exception e)
            {
                returnMessage = e.Message + " -Duration/Time Error - \n\n" +
                e.GetType().ToString() + "\n" +
                e.StackTrace;
                returnOptionObject.ErrorCode = 3;
                logger.Error(e, "Error: {errorMessage}", e.Message);
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

        #region Helpers
        private DateTime ChangeEndTime(DateTime startTime, Double duration)
        {
            return startTime.AddMinutes(duration);
        }

        private TimeSpan ChangeDuration(DateTime startTime, DateTime endTime)
        {
            return endTime - startTime;
        }
        #endregion
    }
}