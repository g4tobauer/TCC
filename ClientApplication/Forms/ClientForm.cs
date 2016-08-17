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
using System.Threading;
using CommonData;

namespace ClientApplication.Forms
{
    public partial class ClientForm : Form
    {
        #region
        private Client Client;
        private GameInstance GameInstance;
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
            Client = new Client();
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
                                    GameInstance = TrianglePlayerToGameInstance(playerName);
                                    break;
                                case "Quadrado":
                                    Client.CreateConnection(serverIp, serverPort, localSenderPort, receiverPort);
                                    GameInstance = QuadPlayerToGameInstance(playerName);
                                    break;
                            }

                            if (Client.Join(GameInstance))
                            {
                                new Thread(Client.Receive).Start();
                                _clientOpenGLScreen = new ClientOpenGLScreen(Client);
                                _clientOpenGLScreen.MakeGameInstance(GameInstance);
                                _clientOpenGLScreen.AddGameInstanceToList(GameInstance);
                                isCreated = true;
                            }
                            else
                            {
                                Client.Close();
                            }
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

        private Player MakeQuadPlayer(string playerName)
        {
            return new Player(MeshType.Quad, playerName);
        }
        private Player MakeTrianglePlayer(string playerName)
        {
            return new Player(MeshType.Triangle, playerName);
        }

        public GameInstance QuadPlayerToGameInstance(string playerName)
        {
            return new GameInstance { Player = MakeQuadPlayer(playerName) };
        }
        public GameInstance TrianglePlayerToGameInstance(string playerName)
        {
            return new GameInstance { Player = MakeTrianglePlayer(playerName) };
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            _clientOpenGLScreen.Close();
        }
    }
}
