using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApplication.Classes;

namespace ClientApplication.Forms
{
    public partial class ClientForm : Form
    {
        #region
        private Client Client;
        private ClientOpenGLScreen _clientOpenGLScreen;
        private bool isCreated;
        private bool isPlayed;
        #endregion
        public ClientForm()
        {
            InitThings();
        }

        private void InitThings()
        {
            InitializeComponent();
            isCreated = false;
            isPlayed = false;
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            Client = new Client(this);
            _clientOpenGLScreen = new ClientOpenGLScreen(Client);
            if (!isCreated)
            {
                var playerName = txt_Name.Text;
                var serverIp = txt_ServerIp.Text;
                int serverPort, localSenderPort, receiverPort;

                if (!string.IsNullOrEmpty(playerName) && !string.IsNullOrEmpty(serverIp) && int.TryParse(txt_ServerPort.Text, out serverPort) && int.TryParse(txt_LocalPort.Text, out localSenderPort) && int.TryParse(txt_MultcastPort.Text, out receiverPort))
                {
                    var controls = grp_PlayerType.Controls;
                    foreach (var c in controls)
                    {
                        var radioButton = ((RadioButton)c);
                        if (radioButton.Checked)
                        {
                            var playerType = radioButton.Text;

                            switch (playerType)
                            {
                                case "Triangulo":
                                    Client.CreateConnection(serverIp, serverPort, localSenderPort, receiverPort);
                                    _clientOpenGLScreen.TrianglePlayer(playerName);
                                    break;
                                case "Quadrado":
                                    Client.CreateConnection(serverIp, serverPort, localSenderPort, receiverPort);
                                    _clientOpenGLScreen.QuadPlayer(playerName);
                                    break;
                            }
                            isCreated = true;
                            break;
                        }
                    }
                }
                if (isCreated && !isPlayed)
                {
                    _clientOpenGLScreen.MainLoop();
                    isPlayed = true;
                }
            }
        }

        delegate void SetTextCallBack(string texto);
        private void DefinirTexto(string texto)
        {
            this.txt_Log.Text = texto;
        }
        public void DefiniTexto(string texto)
        {
            if (!(string.IsNullOrEmpty(texto)))
            {
                if (this.txt_Log.InvokeRequired)
                {
                    SetTextCallBack d = new SetTextCallBack(DefinirTexto);
                    this.Invoke(d, new object[] { texto });
                }
            }
        }


        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            _clientOpenGLScreen.Close();
        }
    }
}
