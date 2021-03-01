using System.Collections.Generic;
using System.Linq;

namespace Rosecrance.ScriptLinkService.Data.Models
{
    public static class ConfidentialTerm
    {
        // *****************************************************************************************
        // CheckForConfidentialTerms
        // this routine is passed in the address of a text field.  It will look for certain terms in 
        //  the field.  The terms are defined in the ConfTerms string array.  If one is found,  
        //  "Y" is returned, else "N" is returned.
        // Created: 12-17-2018 by Helen Ostapik
        // 01-23-2019 Helen Ostapik revised the list per Renita
        // 01-31-2019 Helen Ostapik revised the list per Renita 
        // 10-22-2019 Karen Pavlik  added another category ... allow NSAIDS. The original
        //                          list is considered confidential and will be stopped. per Renita
        // 01-20-2020 Scott Olson   Changed to static class
        // *****************************************************************************************

        private static readonly List<string> _confidentialTerms = new List<string>()
        {
            "HIV", 
            "AIDS", 
            "hiv/aids", 
            " hiv ", 
            "/aids", 
            "/ aids", 
            " hiv.", 
            " hiv+",
            " aids+", 
            " aids +"
        };

        private static readonly List<string> _allowableTerms = new List<string>()
        {
            "NSAID",
            "SAID"
        };

        // Consider using Regex for this check. See https://regexr.com/
        // Bug: The presence of "NSAID" returns false even if "HIV" is present also.
        // Bug: The criteria is case sensitive.
        public static bool IncludesConfidentialTerms(string noteContent)
        {
            if (_confidentialTerms.Any(noteContent.Contains))
            {
                if (_allowableTerms.Any(noteContent.Contains))
                    return false;
                return true;
            }
            return false;
        }

        // Old Code For Reference
        /*
        // *****************************************************************************************
        // CheckForConfidentialTerms
        // this routine is passed in the address of a text field.  It will look for certain terms in 
        //  the field.  The terms are defined in the ConfTerms string array.  If one is found,  
        //  "Y" is returned, else "N" is returned.
        // Created: 12-17-2018 by Helen Ostapik
        // 01-23-2019 Helen Ostapik revised the list per Renita
        // 01-31-2019 Helen Ostapik revised the list per Renita 
        // 10-22-2019 Karen Pavlik  added another category ... allow NSAIDS. The original
        //                          list is considered confidential and will be stopped. per Renita
        // *****************************************************************************************
        private string CheckForConfidentialTerms(ref string note)
        {
            string returnFlag = "N";
            string[] ConfTerms = { "HIV", "AIDS", "hiv/aids", " hiv ", "/aids", "/ aids", " hiv.", " hiv+",
                                     " aids+", " aids +"};
            string[] Allowable = { "NSAIDS" };
            //  the term 'NSAIDS' gets caught because of the 'AIDS' 
            if (ConfTerms.Any(note.Contains))
            {
                returnFlag = "Y";
                if (Allowable.Any(note.Contains))
                    returnFlag = "N";
            }
            return returnFlag;
        }
        */
    }
}
