using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBBurkhardt_GUI.Common
{
    static class Tools
    {        


        /// <summary>
        ///  Translate Time. String Format iso8601 to String Format HH:mm
        /// </summary>
        /// <param name="isoTime">String Formatted in iso8601 standard</param>
        /// <returns></returns>
        public static string toHours(string isoTime)
        {
            DateTime newTime = DateTime.Parse(isoTime);
            return newTime.ToString("HH:mm");
        }
    }
}
