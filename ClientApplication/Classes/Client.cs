//using ClientApplication.Common;
using ClientApplication.Classes.TCP;
using CommonData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApplication.Classes
{
    public class Client
    {
        #region Proprierties
        private string URI;
        private int SENDPORT;
        private int LOCALPORT;
        private int MULTICASTPORT;
        private bool IsConnected;
        private Socket MulticastSocket;
        private ClientOpenGLScreen Game;
        private TcpClientConnection TcpClientConnection;
        #endregion

        public void CreateConnection(string serverIP, int serverPort, int localPort, int multicastPort)
        {
            URI = serverIP;
            SENDPORT = serverPort;
            LOCALPORT = localPort;
            MULTICASTPORT = multicastPort;

            CreateTcpConnection();
            CreateReceiverConnection();
        }
        
        public void Close()
        {
            try
            {
                IsConnected = false;
                Game.Close();
                Thread.Sleep(100);
                if (MulticastSocket != null)
                    MulticastSocket.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        #region UDP
        private void CreateReceiverConnection()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, MULTICASTPORT);
            IPAddress ip = IPAddress.Parse("224.5.6.7");
            MulticastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            MulticastSocket.Bind(ipep);
            MulticastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));
        }
        public void Receive()
        {
            IsConnected = true;
            while (IsConnected)
            {
                byte[] b = new byte[1024];
                MulticastSocket.Receive(b);
                string str = Encoding.ASCII.GetString(b, 0, b.Length);
                str = str.Trim('\0');
                if(Game != null)
                    Game.ReceiveUpdate(str);
            }
        }
        public void Send(string msg)
        {
            using (UdpClient Sender = new UdpClient(LOCALPORT))
            {
                try
                {
                    Sender.Connect(URI, SENDPORT);
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(msg);
                    Sender.Send(sendBytes, sendBytes.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        #endregion

        #region TCP
        private void CreateTcpConnection()
        {
            TcpClientConnection = new TcpClientConnection(URI);
        }
        public bool Join(GameInstance GameInstance)
        {
            return TcpClientConnection.Join(GameInstance);
        }
        public bool Exit(GameInstance GameInstance)
        {
            return TcpClientConnection.Exit(GameInstance);
        }
        #endregion


        public bool Create(GameInstance GameInstance)
        {
            new Thread(Receive).Start();
            Game = new ClientOpenGLScreen(this);
            Game.MakeGameInstance(GameInstance);
            Game.AddGameInstanceToList(GameInstance);
            return true;
        }
        public void Start()
        {
            Game.MainLoop();
        }

    }
}
