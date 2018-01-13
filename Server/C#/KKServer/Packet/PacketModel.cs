using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKServer.Packet
{
    public class CMD_DATA
    {
        public short key;
        public short CMDID;

        public CMD_DATA()
        {
            key = 0x1;
        }
    }
}
