using NLog;
using RarelySimple.AvatarScriptLink.Objects;

namespace Rosecrance.Diagnosis.Web.Commands
{
    public class HelloWorldCommand : IRunScriptCommand
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly OptionObject2015 _optionObject2015;

        public HelloWorldCommand(OptionObject2015 optionObject2015)
        {
            _optionObject2015 = optionObject2015;
        }

        public OptionObject2015 Execute()
        {
            string message = "Hello, World!";
            logger.Debug("Returning {message}.", message);
            return _optionObject2015.ToReturnOptionObject(ErrorCode.Informational, message);
        }
    }
}