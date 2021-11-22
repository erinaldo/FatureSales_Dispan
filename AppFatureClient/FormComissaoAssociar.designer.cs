namespace AppFatureClient
{
    partial class FormComissaoAssociar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComissaoAssociar));
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCANCELAR = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.clConta2 = new AppLib.Windows.CampoLookup();
            this.label2 = new System.Windows.Forms.Label();
            this.clConta1 = new AppLib.Windows.CampoLookup();
            this.label1 = new System.Windows.Forms.Label();
            this.clCliente = new AppLib.Windows.CampoLookup();
            this.dthConta1 = new AppLib.Windows.CampoData();
            this.dthConta2 = new AppLib.Windows.CampoData();
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
            this.panel1.Location = new System.Drawing.Point(0, 222);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 61);
            this.panel1.TabIndex = 11;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK.Location = new System.Drawing.Point(383, 18);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCANCELAR
            // 
            this.simpleButtonCANCELAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR.Location = new System.Drawing.Point(473, 18);
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
            this.tabControl1.Size = new System.Drawing.Size(570, 222);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dthConta2);
            this.tabPage1.Controls.Add(this.dthConta1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.clConta2);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.clConta1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.clCliente);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(562, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Associar conta";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Conta 2";
            // 
            // clConta2
            // 
            this.clConta2.AutoSize = true;
            this.clConta2.Campo = null;
            this.clConta2.ColunaCodigo = "CODCONTA";
            this.clConta2.ColunaDescricao = "DESCRICAO";
            this.clConta2.ColunaIdentificador = null;
            this.clConta2.ColunaTabela = "(select * from CCONTA where CODCOLIGADA = 1 and CODCONTA like \'2.01.04.00001.%\') " +
    "Z";
            this.clConta2.Conexao = "Start";
            this.clConta2.Default = null;
            this.clConta2.Edita = true;
            this.clConta2.EditaLookup = false;
            this.clConta2.Location = new System.Drawing.Point(8, 115);
            this.clConta2.MaximoCaracteres = null;
            this.clConta2.Name = "clConta2";
            this.clConta2.NomeGrid = null;
            this.clConta2.Query = 0;
            this.clConta2.Size = new System.Drawing.Size(434, 23);
            this.clConta2.Tabela = null;
            this.clConta2.TabIndex = 18;
            this.clConta2.AposSelecao += new AppLib.Windows.CampoLookup.AposSelecaoHandler(this.clConta2_AposSelecao);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Conta 1";
            // 
            // clConta1
            // 
            this.clConta1.AutoSize = true;
            this.clConta1.Campo = null;
            this.clConta1.ColunaCodigo = "CODCONTA";
            this.clConta1.ColunaDescricao = "DESCRICAO";
            this.clConta1.ColunaIdentificador = null;
            this.clConta1.ColunaTabela = "(select * from CCONTA where CODCOLIGADA = 1 and CODCONTA like \'1.01.03.00001.%\') " +
    "Z";
            this.clConta1.Conexao = "Start";
            this.clConta1.Default = null;
            this.clConta1.Edita = true;
            this.clConta1.EditaLookup = false;
            this.clConta1.Location = new System.Drawing.Point(8, 73);
            this.clConta1.MaximoCaracteres = null;
            this.clConta1.Name = "clConta1";
            this.clConta1.NomeGrid = null;
            this.clConta1.Query = 0;
            this.clConta1.Size = new System.Drawing.Size(434, 23);
            this.clConta1.Tabela = null;
            this.clConta1.TabIndex = 16;
            this.clConta1.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.clConta1_SetDescricao);
            this.clConta1.AposSelecao += new AppLib.Windows.CampoLookup.AposSelecaoHandler(this.clConta1_AposSelecao);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Cliente";
            // 
            // clCliente
            // 
            this.clCliente.AutoSize = true;
            this.clCliente.Campo = null;
            this.clCliente.ColunaCodigo = "CODTMV";
            this.clCliente.ColunaDescricao = "NOME";
            this.clCliente.ColunaIdentificador = null;
            this.clCliente.ColunaTabela = "TTMV";
            this.clCliente.Conexao = "Start";
            this.clCliente.Default = null;
            this.clCliente.Edita = true;
            this.clCliente.EditaLookup = false;
            this.clCliente.Enabled = false;
            this.clCliente.Location = new System.Drawing.Point(8, 31);
            this.clCliente.MaximoCaracteres = null;
            this.clCliente.Name = "clCliente";
            this.clCliente.NomeGrid = null;
            this.clCliente.Query = 0;
            this.clCliente.Size = new System.Drawing.Size(434, 23);
            this.clCliente.Tabela = null;
            this.clCliente.TabIndex = 14;
            // 
            // dthConta1
            // 
            this.dthConta1.Campo = null;
            this.dthConta1.Default = null;
            this.dthConta1.Edita = true;
            this.dthConta1.Location = new System.Drawing.Point(448, 73);
            this.dthConta1.Name = "dthConta1";
            this.dthConta1.Query = 0;
            this.dthConta1.Size = new System.Drawing.Size(100, 21);
            this.dthConta1.Tabela = null;
            this.dthConta1.TabIndex = 20;
            // 
            // dthConta2
            // 
            this.dthConta2.Campo = null;
            this.dthConta2.Default = null;
            this.dthConta2.Edita = true;
            this.dthConta2.Location = new System.Drawing.Point(448, 115);
            this.dthConta2.Name = "dthConta2";
            this.dthConta2.Query = 0;
            this.dthConta2.Size = new System.Drawing.Size(100, 21);
            this.dthConta2.Tabela = null;
            this.dthConta2.TabIndex = 21;
            // 
            // FormComissaoAssociar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 283);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormComissaoAssociar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Associar Conta Contabil";
            this.Load += new System.EventHandler(this.FormComissaoAssociar_Load);
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
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoLookup clCliente;
        private System.Windows.Forms.Label label3;
        private AppLib.Windows.CampoLookup clConta2;
        private System.Windows.Forms.Label label2;
        private AppLib.Windows.CampoLookup clConta1;
        private AppLib.Windows.CampoData dthConta2;
        private AppLib.Windows.CampoData dthConta1;
    }
}