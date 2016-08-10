//using ClientApplication.Common;
using CommonData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication.Classes
{
    public class Client
    {
        #region
        #endregion
        #region Proprierties
        private string URI;
        private int SENDPORT;
        private int LOCALPORT;
        private Forms.ClientForm ClientForm;
        private bool IsConnected;
        private Socket Socket;
        //private ClientOpenGLScreen ClientOpenGLScreen;
        #endregion

        public Client(Forms.ClientForm Form)
        {
            ClientForm = Form;
        }

        public void CreateConnection(string serverIP, int serverPort, int localPort, int multicastPort)
        {
            CreateReceiverConnection(multicastPort);
            URI = serverIP;
            SENDPORT = serverPort;
            LOCALPORT = localPort;
        }

        #region PublicMethods
        #region SEND
        public void send(string msg)
        {
            using (UdpClient udpClientSender = new UdpClient(LOCALPORT))
            {
                try
                {
                    udpClientSender.Connect(URI, SENDPORT);
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(msg);
                    udpClientSender.Send(sendBytes, sendBytes.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        #endregion
        #region RECEIVE
        //public void Receive()
        //{
        //    IsConnected = true;
        //    while (IsConnected)
        //    {
        //        byte[] b = new byte[1024];
        //        Socket.Receive(b);
        //        string str = Encoding.ASCII.GetString(b, 0, b.Length);
        //        str = str.Trim('\0');
        //        //ClientForm.DefiniTexto(str);
        //        Console.WriteLine(str);
        //    }
        //}
        public void Close()
        {
            try
            {
                Socket.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
        #endregion

        #region PrivateMethods
        private void CreateReceiverConnection(int receiverPort)
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 4567);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, receiverPort);
            Socket.Bind(ipep);
            IPAddress ip = IPAddress.Parse("224.5.6.7");
            Socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));
        }
        #endregion

    }
}
