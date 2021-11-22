namespace AppFatureClient
{
    partial class FormLimiteCredito
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLimiteCredito));
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCANCELAR = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.campoTexto1NOMEFANTASIA = new AppLib.Windows.CampoTexto();
            this.label21 = new System.Windows.Forms.Label();
            this.campoTexto1CODCFO = new AppLib.Windows.CampoTexto();
            this.label28 = new System.Windows.Forms.Label();
            this.campoDecimal1 = new AppLib.Windows.CampoDecimal();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.lblValorAberto = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButtonOK);
            this.panel1.Controls.Add(this.simpleButtonCANCELAR);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 310);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 61);
            this.panel1.TabIndex = 13;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK.Location = new System.Drawing.Point(407, 18);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCANCELAR
            // 
            this.simpleButtonCANCELAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR.Location = new System.Drawing.Point(497, 18);
            this.simpleButtonCANCELAR.Name = "simpleButtonCANCELAR";
            this.simpleButtonCANCELAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR.TabIndex = 3;
            this.simpleButtonCANCELAR.Text = "Cancelar";
            this.simpleButtonCANCELAR.Click += new System.EventHandler(this.simpleButtonCANCELAR_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(594, 310);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblSaldo);
            this.tabPage1.Controls.Add(this.lblValorAberto);
            this.tabPage1.Controls.Add(this.label60);
            this.tabPage1.Controls.Add(this.label59);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.campoTexto1NOMEFANTASIA);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.campoTexto1CODCFO);
            this.tabPage1.Controls.Add(this.label28);
            this.tabPage1.Controls.Add(this.campoDecimal1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(586, 284);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Alterar Limite de Crédito";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(114, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 97;
            this.label1.Text = "Nome Fantasia";
            // 
            // campoTexto1NOMEFANTASIA
            // 
            this.campoTexto1NOMEFANTASIA.Campo = "NOMEFANTASIA";
            this.campoTexto1NOMEFANTASIA.Default = null;
            this.campoTexto1NOMEFANTASIA.Edita = true;
            this.campoTexto1NOMEFANTASIA.Enabled = false;
            this.campoTexto1NOMEFANTASIA.Location = new System.Drawing.Point(117, 32);
            this.campoTexto1NOMEFANTASIA.MaximoCaracteres = null;
            this.campoTexto1NOMEFANTASIA.Name = "campoTexto1NOMEFANTASIA";
            this.campoTexto1NOMEFANTASIA.Query = 0;
            this.campoTexto1NOMEFANTASIA.Size = new System.Drawing.Size(302, 20);
            this.campoTexto1NOMEFANTASIA.Tabela = null;
            this.campoTexto1NOMEFANTASIA.TabIndex = 95;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(8, 16);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(46, 13);
            this.label21.TabIndex = 96;
            this.label21.Text = "Código";
            // 
            // campoTexto1CODCFO
            // 
            this.campoTexto1CODCFO.Campo = "CODCFO";
            this.campoTexto1CODCFO.Default = null;
            this.campoTexto1CODCFO.Edita = true;
            this.campoTexto1CODCFO.Enabled = false;
            this.campoTexto1CODCFO.Location = new System.Drawing.Point(11, 32);
            this.campoTexto1CODCFO.MaximoCaracteres = null;
            this.campoTexto1CODCFO.Name = "campoTexto1CODCFO";
            this.campoTexto1CODCFO.Query = 0;
            this.campoTexto1CODCFO.Size = new System.Drawing.Size(100, 20);
            this.campoTexto1CODCFO.Tabela = "FCFO";
            this.campoTexto1CODCFO.TabIndex = 94;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 59);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(85, 13);
            this.label28.TabIndex = 93;
            this.label28.Text = "Limite de Crédito";
            // 
            // campoDecimal1
            // 
            this.campoDecimal1.Campo = "";
            this.campoDecimal1.Decimais = 2;
            this.campoDecimal1.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimal1.Edita = true;
            this.campoDecimal1.Location = new System.Drawing.Point(11, 75);
            this.campoDecimal1.Name = "campoDecimal1";
            this.campoDecimal1.Query = 0;
            this.campoDecimal1.Size = new System.Drawing.Size(100, 20);
            this.campoDecimal1.Tabela = "";
            this.campoDecimal1.TabIndex = 91;
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldo.Location = new System.Drawing.Point(260, 85);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(51, 13);
            this.lblSaldo.TabIndex = 102;
            this.lblSaldo.Text = "lblSaldo";
            this.lblSaldo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblValorAberto
            // 
            this.lblValorAberto.AutoSize = true;
            this.lblValorAberto.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorAberto.Location = new System.Drawing.Point(260, 62);
            this.lblValorAberto.Name = "lblValorAberto";
            this.lblValorAberto.Size = new System.Drawing.Size(88, 13);
            this.lblValorAberto.TabIndex = 101;
            this.lblValorAberto.Text = "lblValorAberto";
            this.lblValorAberto.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(165, 85);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(37, 13);
            this.label60.TabIndex = 100;
            this.label60.Text = "Saldo:";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(165, 62);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(85, 13);
            this.label59.TabIndex = 99;
            this.label59.Text = "Valor em Aberto:";
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(117, 75);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 23);
            this.button2.TabIndex = 98;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormLimiteCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 371);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormLimiteCredito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alterar Limite de Crédito";
            this.Load += new System.EventHandler(this.FormLimiteCredito_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCANCELAR;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label28;
        private AppLib.Windows.CampoDecimal campoDecimal1;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoTexto campoTexto1NOMEFANTASIA;
        private System.Windows.Forms.Label label21;
        private AppLib.Windows.CampoTexto campoTexto1CODCFO;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.Label lblValorAberto;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Button button2;

    }
}