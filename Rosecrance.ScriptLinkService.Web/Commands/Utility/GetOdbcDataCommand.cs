using NLog;
using RarelySimple.AvatarScriptLink.Objects;
using Rosecrance.ScriptLinkService.Data.Repositories;
using System;
using System.Data.Odbc;

namespace Rosecrance.Diagnosis.Web.Commands
{
    public class GetOdbcDataCommand : IRunScriptCommand
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly OptionObject2015 _optionObject2015;
        private readonly IGetDataRepository _repository;

        public GetOdbcDataCommand(OptionObject2015 optionObject2015, IGetDataRepository repository)
        {
            _optionObject2015 = optionObject2015;
            _repository = repository;
        }

        public OptionObject2015 Execute()
        {
            double errorCode = 0;
            string errorMesg;
            try
            {
                int episodeCount = _repository.GetClientCountOfOpenMHPrograms(_optionObject2015.Facility, _optionObject2015.EntityID);
                logger.Debug("The ODBC call successfully retrieved this value {value}.", episodeCount);
                errorMesg = "The ODBC call was successful.";
            }
            catch (OdbcException ex)
            {
                logger.Error(ex, "An ODBC exception has occurred. See ODBC Data log for more detail.");
                errorCode = ErrorCode.Informational;
                errorMesg = "An ODBC exception has occurred. See the ODBC Data log on the web server for more detail.";
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An unexpected error occurred when attempting to get the ODBC data. See ODBC Data log for more detail.");
                errorCode = ErrorCode.Informational;
                errorMesg = "The ODBC connection failed unexpectedly. See ODBC Data log on the web server for more detail.";
            }

            return _optionObject2015.ToReturnOptionObject(errorCode, errorMesg);
        }
    }
}