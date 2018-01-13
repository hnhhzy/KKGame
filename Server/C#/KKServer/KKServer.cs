using KKServer.Common;
using KKServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//203.70.8.215
namespace KKServer
{
    class KKServer
    {
        public static PullServer _server;
        static void Main(string[] args)
        {
            DebugLog.Show("KKServer 正在启动");
            try
            {
                int port = int.Parse(ConfigurationSettings.AppSettings["port"]);
                int numConnections = int.Parse(ConfigurationSettings.AppSettings["numConnections"]);
                int receiveBufferSize = int.Parse(ConfigurationSettings.AppSettings["receiveBufferSize"]);
                int overtime = int.Parse(ConfigurationSettings.AppSettings["overtime"]);

                _server = new PullServer(numConnections, receiveBufferSize, overtime, port);

                DebugLog.Show("KKServer 启动成功");

                // message.CMDID = System.BitConverter.ToInt16(buffer, 2);
            }
            catch
            {
                DebugLog.Show("KKServer 4321端口被占用，启动失败");
            }
        }

        //private static void _server_MessageReceived(object sender, SocketBase.MessageEventArgs e)
        //{
        //    DebugLog.Show("接收消息");
        //}

        //private static void _server_Connected(object sender, SocketLibrary.Connection e)
        //{
        //    string ip = e.NickName;
        //    DebugLog.Show(ip + "玩家连入");
        //    byte[] test = { 0x8e, 0x71, 0x00, 0x01, 0x70, 0x06, 0x00, 0x00, 0x00, 0x01, 0x01 };
        //    KKNetEngine.KKNet.Encode(test, test.Length, test[0]);
        //    SocketLibrary.Message message = new SocketLibrary.Message(SocketLibrary.Message.CommandType.SendMessage, test , test.Length);
        //    e.messageQueue.Enqueue(message);
        //}

        //private static void _server_ConnectionClose(object sender, SocketLibrary.SocketBase.ConCloseMessagesEventArgs e)
        //{
        //    string ip = e.ConnectionName.Split(':')[0].ToString();
        //    DebugLog.Show(ip + "玩家离线");
        //}


    }
}
