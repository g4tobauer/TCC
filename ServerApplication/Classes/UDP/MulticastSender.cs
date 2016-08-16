using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApplication.Classes.UDP
{
    class MulticastSender
    {

        public bool IsRunning { get; set; }
        public string Message { get; set; }

        public void Run()
        {
            Message = string.Empty;
            IsRunning = true;
            Send();
        }
        public void Stop()
        {
            IsRunning = false;
        }

        private void Send()
        {
            new Thread(Send1).Start();
            new Thread(Send2).Start();
        }

        private void Send1()
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
                        lock (Message)
                        {
                            sendBytes = Encoding.ASCII.GetBytes(Message);
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
                        lock (Message)
                        {
                            sendBytes = Encoding.ASCII.GetBytes(Message);
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

    }
}
