using System;
using System.Collections.Generic;
using System.Text;

namespace Rosecrance.ScriptLinkService.Data.Models
{
    public class Age
    {
        private readonly DateTime _birthDate;
        private readonly DateTime _asOfDate;
        private int age;
        public Age(DateTime birthDate)
        {
            _birthDate = birthDate;
            _asOfDate = DateTime.Now;
        }

        public Age(DateTime birthDate, DateTime asOfDate)
        {
            _birthDate = birthDate;
            _asOfDate = asOfDate;
        }

        /// <summary>
        /// Returns the age in years.
        /// <para>Based on previous CalculateAge() method.</para>
        /// </summary>
        public int Years
        {
            get
            {
                age = _asOfDate.Year - _birthDate.Year;
                
                if ((_asOfDate.Month < _birthDate.Month) || (_asOfDate.Month == _birthDate.Month && _asOfDate.Day < _birthDate.Day))
                {
                    age--;
                    return age;
                }
                if (age < 0)
                { 
                    return 0;
                }
                else
                { 
                    return age;
                }
            }
            set { age = value; }
        }
    }
}
