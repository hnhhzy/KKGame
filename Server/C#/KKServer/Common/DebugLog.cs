using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKServer.Common
{
    public static class DebugLog
    {
        public static void Show(String text)
        {
            string str = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] : ", CultureInfo.CreateSpecificCulture("en-US"));
            Console.WriteLine(str + text);
        }
    }
}
