namespace SQLWebSiteManager
{
    partial class Comfiguracoes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Comfiguracoes));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxLocalAdress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxExternalAdress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxInternalUser = new System.Windows.Forms.TextBox();
            this.tbxInternalPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxPassExternal = new System.Windows.Forms.TextBox();
            this.tbxUserExternal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxConsulttable = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(318, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Salvar Configurações";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Endereço de consulta local:";
            // 
            // tbxLocalAdress
            // 
            this.tbxLocalAdress.Location = new System.Drawing.Point(173, 6);
            this.tbxLocalAdress.Name = "tbxLocalAdress";
            this.tbxLocalAdress.Size = new System.Drawing.Size(160, 20);
            this.tbxLocalAdress.TabIndex = 2;
            this.tbxLocalAdress.Text = "255.255.255.255";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Endereço de consulta externo:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tbxExternalAdress
            // 
            this.tbxExternalAdress.Location = new System.Drawing.Point(173, 105);
            this.tbxExternalAdress.Name = "tbxExternalAdress";
            this.tbxExternalAdress.Size = new System.Drawing.Size(160, 20);
            this.tbxExternalAdress.TabIndex = 4;
            this.tbxExternalAdress.Text = "255.255.255.255";
            this.tbxExternalAdress.TextChanged += new System.EventHandler(this.tbxExternalAdress_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Usuario Interno:";
            // 
            // tbxInternalUser
            // 
            this.tbxInternalUser.Location = new System.Drawing.Point(173, 33);
            this.tbxInternalUser.Name = "tbxInternalUser";
            this.tbxInternalUser.Size = new System.Drawing.Size(160, 20);
            this.tbxInternalUser.TabIndex = 6;
            this.tbxInternalUser.Text = "root";
            // 
            // tbxInternalPass
            // 
            this.tbxInternalPass.Location = new System.Drawing.Point(173, 60);
            this.tbxInternalPass.Name = "tbxInternalPass";
            this.tbxInternalPass.Size = new System.Drawing.Size(160, 20);
            this.tbxInternalPass.TabIndex = 7;
            this.tbxInternalPass.Text = "admin";
            this.tbxInternalPass.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Senha Interna:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Senha Externo:";
            // 
            // tbxPassExternal
            // 
            this.tbxPassExternal.Location = new System.Drawing.Point(174, 160);
            this.tbxPassExternal.Name = "tbxPassExternal";
            this.tbxPassExternal.Size = new System.Drawing.Size(160, 20);
            this.tbxPassExternal.TabIndex = 11;
            this.tbxPassExternal.Text = "admin";
            this.tbxPassExternal.UseSystemPasswordChar = true;
            // 
            // tbxUserExternal
            // 
            this.tbxUserExternal.Location = new System.Drawing.Point(174, 133);
            this.tbxUserExternal.Name = "tbxUserExternal";
            this.tbxUserExternal.Size = new System.Drawing.Size(160, 20);
            this.tbxUserExternal.TabIndex = 10;
            this.tbxUserExternal.Text = "root";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Usuario Externo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Tabela de consulta:";
            // 
            // tbxConsulttable
            // 
            this.tbxConsulttable.Location = new System.Drawing.Point(174, 186);
            this.tbxConsulttable.Name = "tbxConsulttable";
            this.tbxConsulttable.Size = new System.Drawing.Size(160, 20);
            this.tbxConsulttable.TabIndex = 14;
            this.tbxConsulttable.Text = "consultas_";
            // 
            // Comfiguracoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 240);
            this.Controls.Add(this.tbxConsulttable);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxPassExternal);
            this.Controls.Add(this.tbxUserExternal);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxInternalPass);
            this.Controls.Add(this.tbxInternalUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxExternalAdress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxLocalAdress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Comfiguracoes";
            this.Text = "Comfiguracoes";
            this.Load += new System.EventHandler(this.Comfiguracoes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxLocalAdress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxExternalAdress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxInternalUser;
        private System.Windows.Forms.TextBox tbxInternalPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxPassExternal;
        private System.Windows.Forms.TextBox tbxUserExternal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxConsulttable;
    }
}