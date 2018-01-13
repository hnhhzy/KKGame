using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKServer.Common
{
    public static class Util
    {
        public static string ToHexString(byte[] bytes)
        {
            string byteStr = string.Empty;
            if (bytes != null || bytes.Length > 0)
            {
                foreach (var item in bytes)
                {
                    byteStr += string.Format("{0:X2}", item);
                }
            }
            return byteStr;
        }
    }
}
