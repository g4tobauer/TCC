using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ServerApplication.Classes;

namespace ServerApplication.Forms
{
    public partial class ServerForm : Form
    {
        #region Proprierties
        private Server Server;
        #endregion Proprierties


        public ServerForm()
        {
            InitializeComponent();
            Server = new Server(this);
        }

        #region EventMethods
        private void btn_Start_Click(object sender, EventArgs e)
        {
            Server.Start();
        }
        private void btn_ShutDown_Click(object sender, EventArgs e)
        {
            Server.ShutDown();
        }
        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Server.ShutDown();
        }
        #endregion

        #region PublicMethods
        public bool ServerPort(out int serverPort)
        {
            return int.TryParse(txt_ServerPort.Text, out serverPort);
        }

        public void DefiniTexto(string texto)
        {
            if (this.txt_Resposta.InvokeRequired)
            {
                SetTextCallBack d = new SetTextCallBack(DefinirTexto);
                this.Invoke(d, new object[] { texto });
            }
        }
        #endregion

        #region PrivateMethods
        private void DefinirTexto(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
                this.txt_Resposta.AppendText(texto + "\n");
        }
        #endregion

        delegate void SetTextCallBack(string teste);

    }
}
