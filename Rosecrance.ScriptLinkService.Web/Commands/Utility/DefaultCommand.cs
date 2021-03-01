using NLog;
using RarelySimple.AvatarScriptLink.Objects;

namespace Rosecrance.Diagnosis.Web.Commands
{
    public class DefaultCommand : IRunScriptCommand
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly OptionObject2015 _optionObject2015;
        private readonly string _parameter;

        public DefaultCommand(OptionObject2015 optionObject2015, string parameter)
        {
            _optionObject2015 = optionObject2015;
            _parameter = parameter;
        }

        public OptionObject2015 Execute()
        {
            string message = "Error: There is no command matching the parameter '" + _parameter + "'. Please verify your settings.";
            logger.Warn(message);
            return _optionObject2015.ToReturnOptionObject(ErrorCode.Informational, message);
        }
    }
}