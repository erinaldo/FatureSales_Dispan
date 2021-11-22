namespace AppFatureClient
{
    partial class FormRelacaoParcial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRelacaoParcial));
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCANCELAR = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtCliente = new AppLib.Windows.CampoTexto();
            this.txtCodigoAuxiliar = new AppLib.Windows.CampoTexto();
            this.btnLimparLista = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.campoLookup1 = new AppLib.Windows.CampoLookup();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataFin = new AppLib.Windows.CampoData();
            this.txtDataIni = new AppLib.Windows.CampoData();
            this.panel1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButtonOK);
            this.panel1.Controls.Add(this.simpleButtonCANCELAR);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 297);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 61);
            this.panel1.TabIndex = 13;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK.Location = new System.Drawing.Point(331, 18);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCANCELAR
            // 
            this.simpleButtonCANCELAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR.Location = new System.Drawing.Point(421, 18);
            this.simpleButtonCANCELAR.Name = "simpleButtonCANCELAR";
            this.simpleButtonCANCELAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR.TabIndex = 3;
            this.simpleButtonCANCELAR.Text = "Cancelar";
            this.simpleButtonCANCELAR.Click += new System.EventHandler(this.simpleButtonCANCELAR_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(518, 297);
            this.tabControl2.TabIndex = 16;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblCliente);
            this.tabPage2.Controls.Add(this.txtCliente);
            this.tabPage2.Controls.Add(this.txtCodigoAuxiliar);
            this.tabPage2.Controls.Add(this.btnLimparLista);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.campoLookup1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtDataFin);
            this.tabPage2.Controls.Add(this.txtDataIni);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(510, 271);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Parâmetros";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(8, 143);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(39, 13);
            this.lblCliente.TabIndex = 25;
            this.lblCliente.Text = "Cliente";
            // 
            // txtCliente
            // 
            this.txtCliente.Campo = null;
            this.txtCliente.Default = "%";
            this.txtCliente.Edita = true;
            this.txtCliente.Location = new System.Drawing.Point(9, 159);
            this.txtCliente.MaximoCaracteres = null;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Query = 0;
            this.txtCliente.Size = new System.Drawing.Size(204, 20);
            this.txtCliente.Tabela = null;
            this.txtCliente.TabIndex = 24;
            // 
            // txtCodigoAuxiliar
            // 
            this.txtCodigoAuxiliar.Campo = null;
            this.txtCodigoAuxiliar.Default = null;
            this.txtCodigoAuxiliar.Edita = true;
            this.txtCodigoAuxiliar.Location = new System.Drawing.Point(9, 120);
            this.txtCodigoAuxiliar.MaximoCaracteres = null;
            this.txtCodigoAuxiliar.Name = "txtCodigoAuxiliar";
            this.txtCodigoAuxiliar.Query = 0;
            this.txtCodigoAuxiliar.Size = new System.Drawing.Size(204, 20);
            this.txtCodigoAuxiliar.Tabela = null;
            this.txtCodigoAuxiliar.TabIndex = 23;
            // 
            // btnLimparLista
            // 
            this.btnLimparLista.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimparLista.Location = new System.Drawing.Point(417, 221);
            this.btnLimparLista.Name = "btnLimparLista";
            this.btnLimparLista.Size = new System.Drawing.Size(75, 23);
            this.btnLimparLista.TabIndex = 4;
            this.btnLimparLista.Text = "Limpar Lista";
            this.btnLimparLista.Click += new System.EventHandler(this.btnLimparLista_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Representante";
            // 
            // campoLookup1
            // 
            this.campoLookup1.Campo = "CODRPR";
            this.campoLookup1.ColunaCodigo = "CODRPR";
            this.campoLookup1.ColunaDescricao = "NOMEFANTASIA";
            this.campoLookup1.ColunaIdentificador = null;
            this.campoLookup1.ColunaTabela = "TRPR";
            this.campoLookup1.Conexao = "Start";
            this.campoLookup1.Default = null;
            this.campoLookup1.Edita = true;
            this.campoLookup1.EditaLookup = false;
            this.campoLookup1.Location = new System.Drawing.Point(9, 119);
            this.campoLookup1.MaximoCaracteres = null;
            this.campoLookup1.Name = "campoLookup1";
            this.campoLookup1.NomeGrid = null;
            this.campoLookup1.Query = 0;
            this.campoLookup1.Size = new System.Drawing.Size(433, 21);
            this.campoLookup1.Tabela = "TRPR";
            this.campoLookup1.TabIndex = 21;
            this.campoLookup1.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookup1_SetFormConsulta);
            this.campoLookup1.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookup1_SetDescricao);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Data Final";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Data Inicial";
            // 
            // txtDataFin
            // 
            this.txtDataFin.Campo = null;
            this.txtDataFin.Default = null;
            this.txtDataFin.Edita = true;
            this.txtDataFin.Location = new System.Drawing.Point(9, 71);
            this.txtDataFin.Name = "txtDataFin";
            this.txtDataFin.Query = 0;
            this.txtDataFin.Size = new System.Drawing.Size(109, 21);
            this.txtDataFin.Tabela = null;
            this.txtDataFin.TabIndex = 18;
            // 
            // txtDataIni
            // 
            this.txtDataIni.Campo = null;
            this.txtDataIni.Default = null;
            this.txtDataIni.Edita = true;
            this.txtDataIni.Location = new System.Drawing.Point(9, 31);
            this.txtDataIni.Name = "txtDataIni";
            this.txtDataIni.Query = 0;
            this.txtDataIni.Size = new System.Drawing.Size(109, 21);
            this.txtDataIni.Tabela = null;
            this.txtDataIni.TabIndex = 16;
            // 
            // FormRelacaoParcial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 358);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRelacaoParcial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relação Parcial";
            this.Load += new System.EventHandler(this.FormRelacaoParcial_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCANCELAR;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage2;
        private AppLib.Windows.CampoData txtDataFin;
        private AppLib.Windows.CampoData txtDataIni;
        private System.Windows.Forms.Label label3;
        private AppLib.Windows.CampoLookup campoLookup1;
        private DevExpress.XtraEditors.SimpleButton btnLimparLista;
        private AppLib.Windows.CampoTexto txtCodigoAuxiliar;
        private System.Windows.Forms.Label lblCliente;
        private AppLib.Windows.CampoTexto txtCliente;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
    }
}