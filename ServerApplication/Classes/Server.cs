//using ClientApplication.Common;
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

namespace ServerApplication.Classes
{
    class Server
    {
        #region
        #endregion

        #region Proprierties
        private Dictionary<string, UdpState> _dicUdpState;
        private Forms.ServerForm ServerForm;
        private List<UdpState> _lstUdpState;
        private IPEndPoint IPEndPoint;
        private UdpClient UdpClient;
        private Map _map;

        private bool IsRunning;
        private bool IsOK;
        public string Result { get; set; }
        #endregion

        public Server(Forms.ServerForm Form)
        {
            ServerForm = Form;
            int serverPort;
            if (ServerForm.ServerPort(out serverPort))
            {
                IPEndPoint = new IPEndPoint(IPAddress.Any, serverPort);
                _dicUdpState = new Dictionary<string, UdpState>();
                _lstUdpState = new List<UdpState>();
                _map = new Map();
                Result = string.Empty;
                IsOK = true;
            }
        }

        #region PublicMethods
        public void Start()
        {
            if (IsOK && !IsRunning)
            {
                StartThreads();
            }
        }
        public void ShutDown()
        {
            if (IsOK && IsRunning)
                EndReceive();
        }
        #endregion

        #region PrivateMethods
        #region SEND
        private void Send()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                IPAddress ip = IPAddress.Parse("224.5.6.7");
                try
                {
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip));
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);
                    IPEndPoint ipep = new IPEndPoint(ip, 4567);

                    socket.Connect(ipep);
                    Byte[] sendBytes;

                    while (IsRunning)
                    {
                        lock (Result)
                        {
                            sendBytes = Encoding.ASCII.GetBytes(Result);
                            socket.Send(sendBytes, sendBytes.Length, SocketFlags.None);
                        }

                        Thread.Sleep(10);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        private void Send2()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                IPAddress ip = IPAddress.Parse("224.5.6.7");
                try
                {
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip));
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);
                    IPEndPoint ipep = new IPEndPoint(ip, 4568);

                    socket.Connect(ipep);
                    Byte[] sendBytes;

                    while (IsRunning)
                    {
                        lock (Result)
                        {
                            sendBytes = Encoding.ASCII.GetBytes(Result);
                            socket.Send(sendBytes, sendBytes.Length, SocketFlags.None);
                        }

                        Thread.Sleep(10);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        #endregion
        #region RECEIVE
        private void ReceiveCallback(IAsyncResult ar)
        {
            UdpState UdpState = ((UdpState)(ar.AsyncState));
            UdpClient UdpClient = UdpState.UdpClient;
            IPEndPoint IPEndPoint = UdpState.IPEndPoint;
            try
            {
                Byte[] receiveBytes = UdpClient.EndReceive(ar, ref IPEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                Console.WriteLine(returnData);
                GameInstance deserializedGameInstance = JsonConvert.DeserializeObject<GameInstance>(returnData);
                UdpState.IPEndPoint = IPEndPoint;
                UdpState.GameInstance = deserializedGameInstance;

                lock (_lstUdpState)
                {
                    _lstUdpState.Add(UdpState);
                }
                ServerForm.DefiniTexto(returnData);

                UdpState UdpStateTeste = new UdpState();
                UdpStateTeste.IPEndPoint = IPEndPoint;
                UdpStateTeste.UdpClient = UdpClient;
                UdpClient.BeginReceive(new AsyncCallback(ReceiveCallback), UdpStateTeste);
            }
            catch (SocketException e)
            {
                // Oups, connection was closed
                IsRunning = false;
            }
            catch (ObjectDisposedException e)
            {
                // Oups, client was disposed
                IsRunning = false;
            }
        }

        private void BeginReceive()
        {
            UdpClient = new UdpClient(IPEndPoint);
            try
            {
                UdpState UdpState = new UdpState();
                UdpState.IPEndPoint = IPEndPoint;
                UdpState.UdpClient = UdpClient;
                //Console.WriteLine("listening for messages");
                UdpClient.BeginReceive(new AsyncCallback(ReceiveCallback), UdpState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EndReceive()
        {
            if (IsOK && UdpClient != null)
            {
                try
                {
                    UdpClient.Close();
                }catch(Exception)
                {

                }
            }
        }
        #endregion
        private void StartThreads()
        {
            IsRunning = true;
            Thread ThreadUpdateUdpState = new Thread(UpdateUdpState);
            Thread ThreadUpdateList = new Thread(UpdateList);
            Thread ThreadReceive = new Thread(BeginReceive);
            Thread ThreadSend2 = new Thread(Send2);
            Thread ThreadSend = new Thread(Send);
            ThreadUpdateUdpState.Start();
            ThreadUpdateList.Start();
            ThreadReceive.Start();
            ThreadSend2.Start();
            ThreadSend.Start();
        }
        private void UpdateUdpState()
        {
            while (IsRunning)
            {
                Thread.Sleep(1);
                lock (_lstUdpState)
                {
                    foreach (var UdpState in _lstUdpState)
                    {
                        var gameInstance = UdpState.GameInstance;
                        if (gameInstance != null)
                        {
                            var dicKey = UdpState.IPEndPoint.Address.ToString() + UdpState.IPEndPoint.Port + gameInstance.Player.PlayerName;
                            lock (_dicUdpState)
                            {
                                switch (gameInstance.opCode)
                                {
                                    case Operation.Join:
                                        if (!_dicUdpState.ContainsKey(dicKey))
                                        {
                                            _dicUdpState.Add(dicKey, UdpState);
                                        }
                                        break;
                                    case Operation.Update:
                                        if (_dicUdpState.ContainsKey(dicKey))
                                        {
                                            _dicUdpState[dicKey] = UdpState;
                                        }
                                        break;
                                    case Operation.Exit:
                                        if (_dicUdpState.ContainsKey(dicKey))
                                        {
                                            _dicUdpState.Remove(dicKey);
                                        }
                                        break;
                                }
                            }
                        }
                        _lstUdpState.Remove(UdpState);
                        break;
                    }
                }
            }
        }
        private void UpdateList()
        {
            while (IsRunning)
            {
                lock (_dicUdpState)
                {
                    _map.PlayerList.Clear();
                    foreach (KeyValuePair<string, UdpState> entry in _dicUdpState)
                    {
                        var player = entry.Value.GameInstance.Player;
                        _map.PlayerList.Add(player);
                    }
                }
                string json = string.Empty;
                if (_map.PlayerList.Count != 0)
                {
                    json = JsonConvert.SerializeObject(_map);
                }

                lock (Result)
                {
                    Result = json;
                }
            }
        }
        #endregion
    }
}
