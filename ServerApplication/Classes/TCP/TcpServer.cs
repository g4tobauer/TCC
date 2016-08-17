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

namespace ServerApplication.Classes.TCP
{
    public class TcpServer
    {
        private Dictionary<string, UdpState> _dicUdpState;
        private static int TCPPORT = 10000;

        public TcpServer(ref Dictionary<string, UdpState> dicUdpState)
        {
            _dicUdpState = dicUdpState;
        }

        public void Start()
        {
            string output = string.Empty;
            // Create an instance of the TcpListener class.
            TcpListener tcpListener = null;
            IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            try
            {
                // Set the listener on the local IP address 
                // and specify the port.
                tcpListener = new TcpListener(ipAddress, TCPPORT);
                tcpListener.Start();
                output = "Waiting for a connection...";
            }
            catch (Exception e)
            {
                output = "Error: " + e.ToString();
                Console.WriteLine(output);
            }



            while (true)
            {
                // Always use a Sleep call in a while(true) loop 
                // to avoid locking up your CPU.
                Thread.Sleep(10);
                // Create a TCP socket. 
                // If you ran this server on the desktop, you could use 
                // Socket socket = tcpListener.AcceptSocket() 
                // for greater flexibility.

                ConnectionBuilder Builder = new ConnectionBuilder(ref _dicUdpState);
                new Thread(Builder.AcceptConnection(tcpListener.AcceptTcpClient()).Run).Start();
            }
        }
    }

    class ConnectionBuilder
    {
        private Dictionary<string, UdpState> _dicUdpState;
        private TcpClient _tcpClient;

        public ConnectionBuilder(ref Dictionary<string, UdpState> dicUdpState)
        {
            _dicUdpState = dicUdpState;
        }

        public ConnectionBuilder AcceptConnection(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            return this;
        }

        public void Run()
        {
            // Read the data stream from the client. 
            byte[] bytes = new byte[256];
            NetworkStream stream = _tcpClient.GetStream();
            stream.Read(bytes, 0, bytes.Length);
            ProcessMsg(stream, bytes);
        }
        private void ProcessMsg(NetworkStream stream, byte[] bytesReceived)
        {
            string mstrMessage = string.Empty;
            //string mstrResponse;
            byte[] bytesSent;

            bool IsOk = false;

            // Handle the message received and  
            // send a response back to the client.
            mstrMessage = Encoding.ASCII.GetString(bytesReceived, 0, bytesReceived.Length);

            GameInstance deserializedGameInstance = JsonConvert.DeserializeObject<GameInstance>(mstrMessage);
            UdpState UdpState = new UdpState
            {
                IPEndPoint = ((IPEndPoint)_tcpClient.Client.RemoteEndPoint),
                GameInstance = deserializedGameInstance
            };

            var gameInstance = UdpState.GameInstance;
            if (gameInstance != null)
            {
                //var dicKey = UdpState.IPEndPoint.Address.ToString() + UdpState.IPEndPoint.Port + gameInstance.Player.PlayerName;
                var dicKey = gameInstance.Player.PlayerName;
                lock (_dicUdpState)
                {
                    switch (gameInstance.opCode)
                    {
                        case Operation.Join:
                            if (!_dicUdpState.ContainsKey(dicKey))
                            {
                                _dicUdpState.Add(dicKey, UdpState);
                                mstrMessage = "Connectado";
                                IsOk = true;
                            }
                            break;
                        case Operation.Exit:
                            if (_dicUdpState.ContainsKey(dicKey))
                            {
                                _dicUdpState.Remove(dicKey);
                                mstrMessage = "Desconnectado";
                                IsOk = true;
                            }
                            break;
                    }
                }
            }

            if(IsOk)
            {
                bytesSent = Encoding.ASCII.GetBytes(mstrMessage);
                stream.Write(bytesSent, 0, bytesSent.Length);
            }
            else
            {
                bytesSent = Encoding.ASCII.GetBytes("Fail");
                stream.Write(bytesSent, 0, bytesSent.Length);
            }
        }
    }
}
