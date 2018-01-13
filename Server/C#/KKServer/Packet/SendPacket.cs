using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKServer.Packet
{
    public class CMD_DATAX
    {
        public short key;
        public short CMDID;

        public CMD_DATAX()
        {
            key = 0x1;
        }
    }

    public class CMD_LOGIN_FIRST
    {
        public byte[] data;
    }


    public class CMD_MESSAGE_BOX : CMD_DATAX
    {
        private int MessageLen;
        private byte SUB_CMDID;
        private string Message;

        public byte[] SetText(string text, byte sub_cmdid)
        {
            CMDID = 0x12;
            SUB_CMDID = (byte)sub_cmdid;
            Message = text;

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(Message);
            MessageLen = (short)(byteArray.Length + 2);
            byte[] ret = new byte[9 + byteArray.Length + 1];
            ret[0] = BitConverter.GetBytes(key)[1];
            ret[1] = BitConverter.GetBytes(key)[0];
            ret[2] = BitConverter.GetBytes(CMDID)[1];
            ret[3] = BitConverter.GetBytes(CMDID)[0];
            ret[4] = BitConverter.GetBytes(MessageLen)[3];
            ret[5] = BitConverter.GetBytes(MessageLen)[2];
            ret[6] = BitConverter.GetBytes(MessageLen)[1];
            ret[7] = BitConverter.GetBytes(MessageLen)[0];
            ret[8] = SUB_CMDID;

            for (int i = 0; i < byteArray.Length; i++)
            {
                ret[9 + i] = byteArray[i];
            }
            ret[9 + byteArray.Length] = 0x0;
            return ret;
        }
    }

    public class CMD_SERVER_LIST : CMD_DATAX
    {
        private int MessageLen;

        //列王紀 iKing              [33m擁擠[2;37;0m        127.0.0.1
        public byte[] SetServerStatus(string servername, string status, string speed)
        {
            CMDID = 0x7009;
            //Message = servername + 0x9 + "              " + 0x1b + status + 0x1b + speed + 0x9 + "127.0.0.1\n";

            string space1 = "      ";
            string serverip = "127.0.0.1\n";
            byte[] servernameArray = System.Text.Encoding.UTF8.GetBytes(servername);
            byte[] statusArray = System.Text.Encoding.UTF8.GetBytes(status);
            byte[] speedArray = System.Text.Encoding.UTF8.GetBytes(speed);
            byte[] space1Array = System.Text.Encoding.UTF8.GetBytes(space1);
            byte[] serveripArray = System.Text.Encoding.UTF8.GetBytes(serverip);
            MessageLen = (short)(servernameArray.Length + statusArray.Length + serveripArray.Length + speedArray.Length + space1Array.Length + 1 + 4);
            byte[] ret = new byte[8 + MessageLen];

            ret[0] = BitConverter.GetBytes(key)[1];
            ret[1] = BitConverter.GetBytes(key)[0];
            ret[2] = BitConverter.GetBytes(CMDID)[1];
            ret[3] = BitConverter.GetBytes(CMDID)[0];
            ret[4] = BitConverter.GetBytes(MessageLen)[3];
            ret[5] = BitConverter.GetBytes(MessageLen)[2];
            ret[6] = BitConverter.GetBytes(MessageLen)[1];
            ret[7] = BitConverter.GetBytes(MessageLen)[0];

            for (int i = 0; i < servernameArray.Length; i++)
            {
                ret[8 + i] = servernameArray[i];
            }
            ret[8 + servernameArray.Length] = 0x9;
            for (int i = 0; i < space1Array.Length; i++)
            {
                ret[8 + servernameArray.Length + 1 + i] = space1Array[i];
            }
            ret[8 + servernameArray.Length + space1Array.Length + 1] = 0x1b;
            for (int i = 0; i < statusArray.Length; i++)
            {
                ret[8 + servernameArray.Length + 1 + space1Array.Length + 1 + i] = statusArray[i];
            }
            ret[8 + servernameArray.Length + 1 + space1Array.Length + 1 + statusArray.Length] = 0x1b;
            for (int i = 0; i < speedArray.Length; i++)
            {
                ret[8 + servernameArray.Length + 1 + space1Array.Length + 1 + statusArray.Length + 1 + i] = speedArray[i];
            }
            ret[8 + servernameArray.Length + 1 + space1Array.Length + 1 + statusArray.Length + 1 + speedArray.Length] = 0x9;
            for (int i = 0; i < serveripArray.Length; i++)
            {
                ret[8 + servernameArray.Length + 1 + space1Array.Length + 1 + statusArray.Length + 1 + speedArray.Length + 1 + i] = serveripArray[i];
            }

            ret[8 + MessageLen - 1] = 0x0;
            return ret;
        }
    }

    public class CMD_ACCOUNT_STATUS : CMD_DATAX
    {
        private int MessageLen;
        private int unkown;
        //0x00, 0x01, 0x70, 0x01, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x7F, 0xFF, 0xff, 0x00
        public byte[] SetAccountStatus(short point, byte createNum)
        {
            CMDID = 0x7001;
            MessageLen = 0x8;
            unkown = 0x0;
            byte[] ret = new byte[8 + MessageLen];
            ret[0] = BitConverter.GetBytes(key)[1];
            ret[1] = BitConverter.GetBytes(key)[0];
            ret[2] = BitConverter.GetBytes(CMDID)[1];
            ret[3] = BitConverter.GetBytes(CMDID)[0];
            ret[4] = BitConverter.GetBytes(MessageLen)[3];
            ret[5] = BitConverter.GetBytes(MessageLen)[2];
            ret[6] = BitConverter.GetBytes(MessageLen)[1];
            ret[7] = BitConverter.GetBytes(MessageLen)[0];

            ret[8] = BitConverter.GetBytes(unkown)[3];
            ret[9] = BitConverter.GetBytes(unkown)[2];
            ret[10] = BitConverter.GetBytes(unkown)[1];
            ret[11] = BitConverter.GetBytes(unkown)[0];


            ret[12] = BitConverter.GetBytes(point)[1];
            ret[13] = BitConverter.GetBytes(point)[0];

            ret[14] = BitConverter.GetBytes(point)[0];

            ret[15] = createNum;
            return ret;
        }

    }

    public class CMD_ROLE_LIST : CMD_DATAX
    {
        private int MessageLen;
        private short num;
        public byte[] SetRoleName(string roleName)
        {
            CMDID = 0x7002;
            num = 0; //默认是数量-1，因为台服不能创建双角色，暂时无测试，都是0。估计就是角色数量
            byte[] roleNameArray = System.Text.Encoding.UTF8.GetBytes(roleName);
            MessageLen = roleNameArray.Length + 4;
            byte[] ret = new byte[8 + MessageLen];
            ret[0] = BitConverter.GetBytes(key)[1];
            ret[1] = BitConverter.GetBytes(key)[0];
            ret[2] = BitConverter.GetBytes(CMDID)[1];
            ret[3] = BitConverter.GetBytes(CMDID)[0];
            ret[4] = BitConverter.GetBytes(MessageLen)[3];
            ret[5] = BitConverter.GetBytes(MessageLen)[2];
            ret[6] = BitConverter.GetBytes(MessageLen)[1];
            ret[7] = BitConverter.GetBytes(MessageLen)[0];

            ret[8] = BitConverter.GetBytes(num)[1];
            ret[9] = BitConverter.GetBytes(num)[0];
            for (int i = 0; i < roleNameArray.Length; i++)
            {
                ret[10 + i] = roleNameArray[i];
            }
            ret[10 + roleNameArray.Length] = 0x2c; // 逗号，多角色分割
            ret[10 + roleNameArray.Length + 1] = 0x0;
            return ret;
        }


    }


}
