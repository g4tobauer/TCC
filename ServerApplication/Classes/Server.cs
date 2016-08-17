//using ClientApplication.Common;
using CommonData;
using Newtonsoft.Json;
using ServerApplication.Classes.TCP;
using ServerApplication.Classes.UDP;
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
        #region TESTE
        #endregion

        #region Proprierties
        private UdpReceive Receiver;
        private MulticastSender Sender;
        private Dictionary<string, UdpState> _dicUdpState;
        private Forms.ServerForm ServerForm;
        private List<UdpState> _lstUdpState;
        private Map _map;        
        private bool IsOK;
        #endregion


        public Server(Forms.ServerForm Form)
        {
            ServerForm = Form;
            int serverPort;
            if (ServerForm.ServerPort(out serverPort))
            {
                _lstUdpState = new List<UdpState>();
                Receiver = new UdpReceive(ref _lstUdpState, new IPEndPoint(IPAddress.Any, serverPort));
                Sender = new MulticastSender();

                _dicUdpState = new Dictionary<string, UdpState>();
                _map = new Map();
                IsOK = true;
            }
        }

        private void teste()
        {
            new Thread(new TcpServer(ref _dicUdpState).Start).Start();
        }

        #region PublicMethods
        public bool Start()
        {
            if (IsOK && !Receiver.IsRunning)
            {
                StartThreads();
                //teste();
                return true;
                //return true;
            }
            return false;
        }
        public void ShutDown()
        {
            if (IsOK && Receiver.IsRunning)
                Receiver.EndReceive();
        }
        #endregion

        #region PrivateMethods
        private void StartThreads()
        {
            teste();
            Thread ThreadReceive = new Thread(Receiver.BeginReceive);
            ThreadReceive.Start();

            Thread ThreadUpdateUdpState = new Thread(UpdateUdpState);
            ThreadUpdateUdpState.Start();
            
            Thread ThreadUpdateList = new Thread(UpdateList);
            ThreadUpdateList.Start();

            Thread ThreadSend = new Thread(Sender.Run);
            ThreadSend.Start();
        }

        private void UpdateUdpState()
        {
            while (Receiver.IsRunning)
            {
                Thread.Sleep(1);
                lock (_lstUdpState)
                {
                    foreach (var UdpState in _lstUdpState)
                    {
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
            while (Receiver.IsRunning)
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

                lock (Sender.Message)
                {
                    Sender.Message = json;
                }
            }
        }
        #endregion
    }
}
