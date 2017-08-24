using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsApp.Utils
{
    public class Helpers
    {
        /// <summary>
        /// Converts unix time to DateTime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime UnixToDateTime(long time)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddSeconds(time);
        }
    }
}