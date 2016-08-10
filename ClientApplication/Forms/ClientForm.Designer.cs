namespace ClientApplication.Forms
{
    partial class ClientForm
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
            this.btn_Conectar = new System.Windows.Forms.Button();
            this.btn_Desconectar = new System.Windows.Forms.Button();
            this.txt_ServerIp = new System.Windows.Forms.TextBox();
            this.lbl_ServerIp = new System.Windows.Forms.Label();
            this.lbl_ServerPort = new System.Windows.Forms.Label();
            this.txt_ServerPort = new System.Windows.Forms.TextBox();
            this.lbl_LocalPort = new System.Windows.Forms.Label();
            this.txt_LocalPort = new System.Windows.Forms.TextBox();
            this.txt_Log = new System.Windows.Forms.TextBox();
            this.lbl_MultcastPort = new System.Windows.Forms.Label();
            this.txt_MultcastPort = new System.Windows.Forms.TextBox();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.grp_PlayerType = new System.Windows.Forms.GroupBox();
            this.rb_Quad = new System.Windows.Forms.RadioButton();
            this.rb_Triangle = new System.Windows.Forms.RadioButton();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.lbl_Log = new System.Windows.Forms.Label();
            this.grp_PlayerType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Conectar
            // 
            this.btn_Conectar.Location = new System.Drawing.Point(23, 526);
            this.btn_Conectar.Name = "btn_Conectar";
            this.btn_Conectar.Size = new System.Drawing.Size(181, 109);
            this.btn_Conectar.TabIndex = 0;
            this.btn_Conectar.Text = "CONECTAR";
            this.btn_Conectar.UseVisualStyleBackColor = true;
            this.btn_Conectar.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_Desconectar
            // 
            this.btn_Desconectar.Location = new System.Drawing.Point(377, 526);
            this.btn_Desconectar.Name = "btn_Desconectar";
            this.btn_Desconectar.Size = new System.Drawing.Size(181, 109);
            this.btn_Desconectar.TabIndex = 1;
            this.btn_Desconectar.Text = "DESCONECTAR";
            this.btn_Desconectar.UseVisualStyleBackColor = true;
            this.btn_Desconectar.Click += new System.EventHandler(this.btn_Disconnect_Click);
            // 
            // txt_ServerIp
            // 
            this.txt_ServerIp.Location = new System.Drawing.Point(24, 58);
            this.txt_ServerIp.Name = "txt_ServerIp";
            this.txt_ServerIp.Size = new System.Drawing.Size(535, 20);
            this.txt_ServerIp.TabIndex = 2;
            this.txt_ServerIp.Text = "127.0.0.1";
            // 
            // lbl_ServerIp
            // 
            this.lbl_ServerIp.AutoSize = true;
            this.lbl_ServerIp.Location = new System.Drawing.Point(21, 42);
            this.lbl_ServerIp.Name = "lbl_ServerIp";
            this.lbl_ServerIp.Size = new System.Drawing.Size(76, 13);
            this.lbl_ServerIp.TabIndex = 3;
            this.lbl_ServerIp.Text = "IP SERVIDOR";
            // 
            // lbl_ServerPort
            // 
            this.lbl_ServerPort.AutoSize = true;
            this.lbl_ServerPort.Location = new System.Drawing.Point(21, 90);
            this.lbl_ServerPort.Name = "lbl_ServerPort";
            this.lbl_ServerPort.Size = new System.Drawing.Size(103, 13);
            this.lbl_ServerPort.TabIndex = 5;
            this.lbl_ServerPort.Text = "PORTA SERVIDOR";
            // 
            // txt_ServerPort
            // 
            this.txt_ServerPort.Location = new System.Drawing.Point(24, 106);
            this.txt_ServerPort.Name = "txt_ServerPort";
            this.txt_ServerPort.Size = new System.Drawing.Size(140, 20);
            this.txt_ServerPort.TabIndex = 4;
            this.txt_ServerPort.Text = "11000";
            // 
            // lbl_LocalPort
            // 
            this.lbl_LocalPort.AutoSize = true;
            this.lbl_LocalPort.Location = new System.Drawing.Point(167, 90);
            this.lbl_LocalPort.Name = "lbl_LocalPort";
            this.lbl_LocalPort.Size = new System.Drawing.Size(81, 13);
            this.lbl_LocalPort.TabIndex = 7;
            this.lbl_LocalPort.Text = "PORTA LOCAL";
            // 
            // txt_LocalPort
            // 
            this.txt_LocalPort.Location = new System.Drawing.Point(170, 106);
            this.txt_LocalPort.Name = "txt_LocalPort";
            this.txt_LocalPort.Size = new System.Drawing.Size(113, 20);
            this.txt_LocalPort.TabIndex = 6;
            this.txt_LocalPort.Text = "22000";
            // 
            // txt_Log
            // 
            this.txt_Log.Location = new System.Drawing.Point(21, 488);
            this.txt_Log.Name = "txt_Log";
            this.txt_Log.Size = new System.Drawing.Size(537, 20);
            this.txt_Log.TabIndex = 8;
            // 
            // lbl_MultcastPort
            // 
            this.lbl_MultcastPort.AutoSize = true;
            this.lbl_MultcastPort.Location = new System.Drawing.Point(21, 186);
            this.lbl_MultcastPort.Name = "lbl_MultcastPort";
            this.lbl_MultcastPort.Size = new System.Drawing.Size(98, 13);
            this.lbl_MultcastPort.TabIndex = 10;
            this.lbl_MultcastPort.Text = "PORTA RECEVER";
            // 
            // txt_MultcastPort
            // 
            this.txt_MultcastPort.Location = new System.Drawing.Point(24, 202);
            this.txt_MultcastPort.Name = "txt_MultcastPort";
            this.txt_MultcastPort.Size = new System.Drawing.Size(113, 20);
            this.txt_MultcastPort.TabIndex = 9;
            this.txt_MultcastPort.Text = "4567";
            // 
            // lbl_Name
            // 
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.Location = new System.Drawing.Point(21, 270);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(35, 13);
            this.lbl_Name.TabIndex = 13;
            this.lbl_Name.Text = "Nome";
            // 
            // grp_PlayerType
            // 
            this.grp_PlayerType.Controls.Add(this.rb_Quad);
            this.grp_PlayerType.Controls.Add(this.rb_Triangle);
            this.grp_PlayerType.Location = new System.Drawing.Point(23, 323);
            this.grp_PlayerType.Name = "grp_PlayerType";
            this.grp_PlayerType.Size = new System.Drawing.Size(536, 70);
            this.grp_PlayerType.TabIndex = 12;
            this.grp_PlayerType.TabStop = false;
            this.grp_PlayerType.Text = "Players";
            // 
            // rb_Quad
            // 
            this.rb_Quad.AutoSize = true;
            this.rb_Quad.Location = new System.Drawing.Point(6, 42);
            this.rb_Quad.Name = "rb_Quad";
            this.rb_Quad.Size = new System.Drawing.Size(72, 17);
            this.rb_Quad.TabIndex = 5;
            this.rb_Quad.TabStop = true;
            this.rb_Quad.Text = "Quadrado";
            this.rb_Quad.UseVisualStyleBackColor = true;
            // 
            // rb_Triangle
            // 
            this.rb_Triangle.AutoSize = true;
            this.rb_Triangle.Location = new System.Drawing.Point(6, 19);
            this.rb_Triangle.Name = "rb_Triangle";
            this.rb_Triangle.Size = new System.Drawing.Size(69, 17);
            this.rb_Triangle.TabIndex = 4;
            this.rb_Triangle.TabStop = true;
            this.rb_Triangle.Text = "Triangulo";
            this.rb_Triangle.UseVisualStyleBackColor = true;
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(23, 286);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(535, 20);
            this.txt_Name.TabIndex = 11;
            // 
            // lbl_Log
            // 
            this.lbl_Log.AutoSize = true;
            this.lbl_Log.Location = new System.Drawing.Point(18, 472);
            this.lbl_Log.Name = "lbl_Log";
            this.lbl_Log.Size = new System.Drawing.Size(35, 13);
            this.lbl_Log.TabIndex = 14;
            this.lbl_Log.Text = "Nome";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 657);
            this.Controls.Add(this.lbl_Log);
            this.Controls.Add(this.lbl_Name);
            this.Controls.Add(this.grp_PlayerType);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.lbl_MultcastPort);
            this.Controls.Add(this.txt_MultcastPort);
            this.Controls.Add(this.txt_Log);
            this.Controls.Add(this.lbl_LocalPort);
            this.Controls.Add(this.txt_LocalPort);
            this.Controls.Add(this.lbl_ServerPort);
            this.Controls.Add(this.txt_ServerPort);
            this.Controls.Add(this.lbl_ServerIp);
            this.Controls.Add(this.txt_ServerIp);
            this.Controls.Add(this.btn_Desconectar);
            this.Controls.Add(this.btn_Conectar);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.grp_PlayerType.ResumeLayout(false);
            this.grp_PlayerType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Conectar;
        private System.Windows.Forms.Button btn_Desconectar;
        private System.Windows.Forms.TextBox txt_ServerIp;
        private System.Windows.Forms.Label lbl_ServerIp;
        private System.Windows.Forms.Label lbl_ServerPort;
        private System.Windows.Forms.TextBox txt_ServerPort;
        private System.Windows.Forms.Label lbl_LocalPort;
        private System.Windows.Forms.TextBox txt_LocalPort;
        private System.Windows.Forms.TextBox txt_Log;
        private System.Windows.Forms.Label lbl_MultcastPort;
        private System.Windows.Forms.TextBox txt_MultcastPort;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.GroupBox grp_PlayerType;
        private System.Windows.Forms.RadioButton rb_Quad;
        private System.Windows.Forms.RadioButton rb_Triangle;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.Label lbl_Log;
    }
}