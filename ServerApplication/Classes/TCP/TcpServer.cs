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
                new Thread(new ConnectionBuilder(tcpListener.AcceptTcpClient()).Run).Start();
                Console.WriteLine("Conexao Aceita");
            }
        }
    }

    class ConnectionBuilder
    {
        private TcpClient _tcpClient;
        public ConnectionBuilder(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
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
            TcpClient mscClient;
            string mstrMessage;
            string mstrResponse;
            byte[] bytesSent;
            // Handle the message received and  
            // send a response back to the client.
            mstrMessage = Encoding.ASCII.GetString(bytesReceived, 0, bytesReceived.Length);
            mscClient = _tcpClient;
            mstrMessage = mstrMessage.Substring(0, 5);
            if (mstrMessage.Equals("Hello"))
            {
                mstrResponse = "Goodbye";
            }
            else
            {
                mstrResponse = "What?";
            }
            bytesSent = Encoding.ASCII.GetBytes(mstrResponse);
            stream.Write(bytesSent, 0, bytesSent.Length);
        }
    }
}
