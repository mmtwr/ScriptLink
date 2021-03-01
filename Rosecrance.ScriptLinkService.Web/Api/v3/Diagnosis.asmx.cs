using System.Web.Services;
using NLog;
using RarelySimple.AvatarScriptLink.Objects;
using Rosecrance.Diagnosis.Web.Commands;
using Rosecrance.Diagnosis.Web.Factories;

namespace Rosecrance.Diagnosis.Web.Api.v3
{
    /// <summary>
    /// This is the <see cref="OptionObject2015"/> version of the Diagnosis.
    /// <para>Note: No edits should be required here.</para>
    /// </summary>
    [WebService(Namespace = "http://rosecrance.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Diagnosis : WebService
    {          
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        [WebMethod]
        public string GetVersion()
        {
            var command = new GetVersionCommand(new OptionObject2015());
            if (command == null)
            {
                logger.Error("A valid GetVersion command was not retrieved.");
                return "";
            }
            return command.Execute();
        }

        [WebMethod]
        public OptionObject2015 RunScript(OptionObject2015 optionObject2015, string parameter)
        {
            IRunScriptCommand command = CommandSelector.GetCommand(optionObject2015, parameter);
            if (command == null)
            {
                logger.Error("A valid RunScript command was not retrieved.");
                return optionObject2015;
            }
            return command.Execute();
        }
    }
}
