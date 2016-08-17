using CommonData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication.Classes.TCP
{
    public class TcpClientConnection
    {
        private string _serverIP;
        private static int SERVERPORT = 10000;

        public TcpClientConnection(string serverIP)
        {
            _serverIP = serverIP;
        }
        public bool Join(GameInstance GameInstance)
        {
            GameInstance.opCode = Operation.Join;
            var json = JsonConvert.SerializeObject(GameInstance);
            return Connect(json);
        }
        public bool Exit(GameInstance GameInstance)
        {
            GameInstance.opCode = Operation.Exit;
            var json = JsonConvert.SerializeObject(GameInstance);
            return Connect(json);
        }

        private bool Connect(string message)
        {
            string output = "";

            try
            {
                // Create a TcpClient. 
                // The client requires a TcpServer that is connected 
                // to the same address specified by the server and port 
                // combination.
                TcpClient client = new TcpClient(_serverIP, SERVERPORT);

                // Translate the passed message into ASCII and store it as a byte array.
                Byte[] data = new Byte[256];
                data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing. 
                // Stream stream = client.GetStream();
                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                output = "Sent: " + message;
                Console.WriteLine(output);

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                output = "Received: " + responseData;
                Console.WriteLine(output);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                output = "ArgumentNullException: " + e;
                Console.WriteLine(output);
            }
            catch (SocketException e)
            {
                output = "SocketException: " + e.ToString();
                Console.WriteLine(output);
            }

            return true;
        }
    }
}
