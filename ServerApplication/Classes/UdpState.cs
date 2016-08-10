using CommonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication.Classes
{
    public class UdpState
    {
        public IPEndPoint IPEndPoint { get; set; }
        public UdpClient UdpClient { get; set; }
        public GameInstance GameInstance { get; set; }
    }
}
