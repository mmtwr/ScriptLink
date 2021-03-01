using System;
using System.Collections.Generic;
using System.Text;

namespace Rosecrance.ScriptLinkService.Data.Models
{
    public class Client
    {
        public string FACILITY { get; set; }
        public double EPISODE_NUMBER { get; set; }
        public string PATID { get; set; }
        public string UnitCode { get; set; }
        public string UnitValue { get; set; }
        public string Room { get; set; }
        public string Bed { get; set; }
        public int Age { get; set; }
    }
}
