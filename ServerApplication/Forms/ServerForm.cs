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
using System.Threading;

namespace ServerApplication.Forms
{
    public partial class ServerForm : Form
    {
        #region Proprierties
        Thread ThreadLog;
        private Server Server;
        private string log;
        private bool isPrintingLog;
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
            ThreadLog = new Thread(Print);
            isPrintingLog = true;
            ThreadLog.Start();
        }
        private void btn_ShutDown_Click(object sender, EventArgs e)
        {
            ShutDown();
        }
        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutDown();
        }
        #endregion

        #region PublicMethods
        public bool ServerPort(out int serverPort)
        {
            return int.TryParse(txt_ServerPort.Text, out serverPort);
        }

        public void DefiniTexto(string texto)
        {
            log = texto;
        }

        #endregion

        #region PrivateMethods
        private void DefinirTexto()
        {
            //if (!string.IsNullOrEmpty(log))
            this.txt_Resposta.AppendText(log + "\n");
        }
        private void Print()
        {
            while (isPrintingLog)
            {
                Thread.Sleep(100);
                if (this.txt_Resposta.InvokeRequired)
                {
                    SetTextCallBack d = new SetTextCallBack(DefinirTexto);
                    this.Invoke(d);
                }
            }
            log = string.Empty;
            DefinirTexto();
        }
        private void ShutDown()
        {
            isPrintingLog = false;
            Server.ShutDown();
            if (ThreadLog != null)
                ThreadLog.Abort();
        }
        #endregion

        delegate void SetTextCallBack();

    }
}
