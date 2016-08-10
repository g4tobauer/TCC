namespace ServerApplication.Forms
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_ShutDown = new System.Windows.Forms.Button();
            this.txt_Resposta = new System.Windows.Forms.TextBox();
            this.txt_ServerPort = new System.Windows.Forms.TextBox();
            this.lbl_ServerPort = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(12, 396);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(160, 117);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "START";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_ShutDown
            // 
            this.btn_ShutDown.Location = new System.Drawing.Point(178, 396);
            this.btn_ShutDown.Name = "btn_ShutDown";
            this.btn_ShutDown.Size = new System.Drawing.Size(160, 117);
            this.btn_ShutDown.TabIndex = 1;
            this.btn_ShutDown.Text = "SHUTDOWN";
            this.btn_ShutDown.UseVisualStyleBackColor = true;
            this.btn_ShutDown.Click += new System.EventHandler(this.btn_ShutDown_Click);
            // 
            // txt_Resposta
            // 
            this.txt_Resposta.Location = new System.Drawing.Point(12, 60);
            this.txt_Resposta.Multiline = true;
            this.txt_Resposta.Name = "txt_Resposta";
            this.txt_Resposta.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Resposta.Size = new System.Drawing.Size(988, 330);
            this.txt_Resposta.TabIndex = 2;
            // 
            // txt_ServerPort
            // 
            this.txt_ServerPort.Location = new System.Drawing.Point(12, 34);
            this.txt_ServerPort.Name = "txt_ServerPort";
            this.txt_ServerPort.Size = new System.Drawing.Size(121, 20);
            this.txt_ServerPort.TabIndex = 3;
            this.txt_ServerPort.Text = "11000";
            // 
            // lbl_ServerPort
            // 
            this.lbl_ServerPort.AutoSize = true;
            this.lbl_ServerPort.Location = new System.Drawing.Point(12, 18);
            this.lbl_ServerPort.Name = "lbl_ServerPort";
            this.lbl_ServerPort.Size = new System.Drawing.Size(84, 13);
            this.lbl_ServerPort.TabIndex = 4;
            this.lbl_ServerPort.Text = "SERVER PORT";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 533);
            this.Controls.Add(this.lbl_ServerPort);
            this.Controls.Add(this.txt_ServerPort);
            this.Controls.Add(this.txt_Resposta);
            this.Controls.Add(this.btn_ShutDown);
            this.Controls.Add(this.btn_Start);
            this.Name = "ServerForm";
            this.Text = "ServerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_ShutDown;
        private System.Windows.Forms.TextBox txt_Resposta;
        private System.Windows.Forms.TextBox txt_ServerPort;
        private System.Windows.Forms.Label lbl_ServerPort;
    }
}