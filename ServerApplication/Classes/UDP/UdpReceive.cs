using CommonData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication.Classes.UDP
{
    class UdpReceive
    {
        private List<UdpState> _lstUdpState;
        private UdpClient UdpClient;
        private IPEndPoint IPEndPoint;
        public bool IsRunning { get; set; }

        public UdpReceive(ref List<UdpState> lstUdpState, IPEndPoint IPEndPoint)
        {
            this.IPEndPoint = IPEndPoint;
            _lstUdpState = lstUdpState;
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            UdpState UdpState = ((UdpState)(ar.AsyncState));
            UdpClient UdpClient = UdpState.UdpClient;
            IPEndPoint IPEndPoint = UdpState.IPEndPoint;
            try
            {
                Byte[] receiveBytes = UdpClient.EndReceive(ar, ref IPEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                //Console.WriteLine(returnData);
                GameInstance deserializedGameInstance = JsonConvert.DeserializeObject<GameInstance>(returnData);
                UdpState.IPEndPoint = IPEndPoint;
                UdpState.GameInstance = deserializedGameInstance;

                lock (_lstUdpState)
                {
                    if(_lstUdpState.Count < 15)
                        _lstUdpState.Add(UdpState);
                }
                //ServerForm.DefiniTexto(returnData);

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

        public void BeginReceive()
        {
            IsRunning = true;
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

        public void EndReceive()
        {
            UdpClient.Close();
        }
    }
}
