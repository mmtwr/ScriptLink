using System;

namespace Rosecrance.ScriptLinkService.Data.Helpers
{
    public static class RoseConvert
    {
        public static bool ToBool(string boolString)
        {
            if (boolString != null)
            {
                switch (boolString.ToUpperInvariant())
                {
                    case "1":
                    case "T":
                    case "TRUE":
                    case "Y":
                    case "YES":
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }

        public static DateTime ToDateTime(string dateTimeString)
        {
            if (DateTime.TryParse(dateTimeString, out DateTime dateTime))
                return dateTime;
            return new DateTime();
        }

        public static int ToInt(string intString)
        {
            if (int.TryParse(intString, out int returnInt))
                return returnInt;
            return 0;
        }

        public static string MultiSelectFormat(string dirtyString)
        {
            string multiSelectString;
            if (!string.IsNullOrEmpty(dirtyString))
            {            
                if (dirtyString.Length > 1)
                {
                     multiSelectString = dirtyString.Substring(1, dirtyString.Length - 2).Replace("&", ",");
                    return multiSelectString;
                }
                else
                {
                    multiSelectString = dirtyString.Substring(0, dirtyString.Length - 1).Replace("&", ",");
                    return multiSelectString;
                }
            }
            else
            {
                return dirtyString;
            }            
        }
        public static string FormattedValue(string dirtyString)
        {
            string[] formattedString;
            if (!string.IsNullOrEmpty(dirtyString))
            {
                if (dirtyString.Length > 1)
                {
                    if (dirtyString.Contains("-"))
                    {
                        formattedString = dirtyString.Split('-');
                        return formattedString[1];
                    }
                    else
                    {
                        return dirtyString;
                    }
                }
                else
                {
                    return dirtyString;
                }
            }

            return dirtyString;
        }
    }
}
