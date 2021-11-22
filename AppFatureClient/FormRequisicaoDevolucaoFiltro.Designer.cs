namespace AppFatureClient
{
    partial class FormRequisicaoDevolucaoFiltro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRequisicaoDevolucaoFiltro));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.lblRepresentante = new System.Windows.Forms.Label();
            this.campoLookupCODRPR = new AppLib.Windows.CampoLookup();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.lblDataFini = new System.Windows.Forms.Label();
            this.lblDataIni = new System.Windows.Forms.Label();
            this.txtDataFin = new AppLib.Windows.CampoData();
            this.txtDataIni = new AppLib.Windows.CampoData();
            this.lblCliente = new System.Windows.Forms.Label();
            this.campoLookupCODCFO = new AppLib.Windows.CampoLookup();
            this.ceRepresentante = new DevExpress.XtraEditors.CheckEdit();
            this.ceCliente = new DevExpress.XtraEditors.CheckEdit();
            this.ceData = new DevExpress.XtraEditors.CheckEdit();
            this.ceFaturado = new DevExpress.XtraEditors.CheckEdit();
            this.ceAFaturar = new DevExpress.XtraEditors.CheckEdit();
            this.ceMultiselecao = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ceRepresentante.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceFaturado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceAFaturar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceMultiselecao.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(374, 251);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 59;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblRepresentante
            // 
            this.lblRepresentante.AutoSize = true;
            this.lblRepresentante.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepresentante.Location = new System.Drawing.Point(14, 121);
            this.lblRepresentante.Name = "lblRepresentante";
            this.lblRepresentante.Size = new System.Drawing.Size(77, 13);
            this.lblRepresentante.TabIndex = 58;
            this.lblRepresentante.Text = "Representante";
            // 
            // campoLookupCODRPR
            // 
            this.campoLookupCODRPR.AutoSize = true;
            this.campoLookupCODRPR.Campo = "CODRPR";
            this.campoLookupCODRPR.ColunaCodigo = "CODRPR";
            this.campoLookupCODRPR.ColunaDescricao = "NOMEFANTASIA";
            this.campoLookupCODRPR.ColunaIdentificador = null;
            this.campoLookupCODRPR.ColunaTabela = "(select * from TRPR where CODCOLIGADA = 1 and INATIVO = 0) Y";
            this.campoLookupCODRPR.Conexao = "Start";
            this.campoLookupCODRPR.Default = null;
            this.campoLookupCODRPR.Edita = true;
            this.campoLookupCODRPR.EditaLookup = false;
            this.campoLookupCODRPR.Enabled = false;
            this.campoLookupCODRPR.Location = new System.Drawing.Point(17, 137);
            this.campoLookupCODRPR.MaximoCaracteres = null;
            this.campoLookupCODRPR.Name = "campoLookupCODRPR";
            this.campoLookupCODRPR.NomeGrid = null;
            this.campoLookupCODRPR.Query = 0;
            this.campoLookupCODRPR.Size = new System.Drawing.Size(497, 24);
            this.campoLookupCODRPR.Tabela = "TRPR";
            this.campoLookupCODRPR.TabIndex = 57;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(455, 251);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 60;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblDataFini
            // 
            this.lblDataFini.AutoSize = true;
            this.lblDataFini.Location = new System.Drawing.Point(14, 49);
            this.lblDataFini.Name = "lblDataFini";
            this.lblDataFini.Size = new System.Drawing.Size(55, 13);
            this.lblDataFini.TabIndex = 64;
            this.lblDataFini.Text = "Data Final";
            // 
            // lblDataIni
            // 
            this.lblDataIni.AutoSize = true;
            this.lblDataIni.Location = new System.Drawing.Point(12, 9);
            this.lblDataIni.Name = "lblDataIni";
            this.lblDataIni.Size = new System.Drawing.Size(60, 13);
            this.lblDataIni.TabIndex = 63;
            this.lblDataIni.Text = "Data Inicial";
            // 
            // txtDataFin
            // 
            this.txtDataFin.Campo = null;
            this.txtDataFin.Default = null;
            this.txtDataFin.Edita = true;
            this.txtDataFin.Enabled = false;
            this.txtDataFin.Location = new System.Drawing.Point(15, 65);
            this.txtDataFin.Name = "txtDataFin";
            this.txtDataFin.Query = 0;
            this.txtDataFin.Size = new System.Drawing.Size(109, 21);
            this.txtDataFin.Tabela = null;
            this.txtDataFin.TabIndex = 62;
            // 
            // txtDataIni
            // 
            this.txtDataIni.Campo = null;
            this.txtDataIni.Default = null;
            this.txtDataIni.Edita = true;
            this.txtDataIni.Enabled = false;
            this.txtDataIni.Location = new System.Drawing.Point(15, 25);
            this.txtDataIni.Name = "txtDataIni";
            this.txtDataIni.Query = 0;
            this.txtDataIni.Size = new System.Drawing.Size(109, 21);
            this.txtDataIni.Tabela = null;
            this.txtDataIni.TabIndex = 61;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.Location = new System.Drawing.Point(14, 194);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(39, 13);
            this.lblCliente.TabIndex = 66;
            this.lblCliente.Text = "Cliente";
            // 
            // campoLookupCODCFO
            // 
            this.campoLookupCODCFO.AutoSize = true;
            this.campoLookupCODCFO.Campo = "CODCFO";
            this.campoLookupCODCFO.ColunaCodigo = "CODCFO";
            this.campoLookupCODCFO.ColunaDescricao = "NOMEFANTASIA";
            this.campoLookupCODCFO.ColunaIdentificador = null;
            this.campoLookupCODCFO.ColunaTabela = "(select * from FCFO where ATIVO = 1) Y";
            this.campoLookupCODCFO.Conexao = "Start";
            this.campoLookupCODCFO.Default = null;
            this.campoLookupCODCFO.Edita = true;
            this.campoLookupCODCFO.EditaLookup = false;
            this.campoLookupCODCFO.Enabled = false;
            this.campoLookupCODCFO.Location = new System.Drawing.Point(17, 210);
            this.campoLookupCODCFO.MaximoCaracteres = null;
            this.campoLookupCODCFO.Name = "campoLookupCODCFO";
            this.campoLookupCODCFO.NomeGrid = null;
            this.campoLookupCODCFO.Query = 0;
            this.campoLookupCODCFO.Size = new System.Drawing.Size(497, 24);
            this.campoLookupCODCFO.Tabela = "TRPR";
            this.campoLookupCODCFO.TabIndex = 65;
            // 
            // ceRepresentante
            // 
            this.ceRepresentante.Location = new System.Drawing.Point(130, 118);
            this.ceRepresentante.Name = "ceRepresentante";
            this.ceRepresentante.Properties.Caption = "";
            this.ceRepresentante.Size = new System.Drawing.Size(75, 19);
            this.ceRepresentante.TabIndex = 67;
            this.ceRepresentante.CheckedChanged += new System.EventHandler(this.ceRepresentante_CheckedChanged);
            // 
            // ceCliente
            // 
            this.ceCliente.Location = new System.Drawing.Point(130, 191);
            this.ceCliente.Name = "ceCliente";
            this.ceCliente.Properties.Caption = "";
            this.ceCliente.Size = new System.Drawing.Size(75, 19);
            this.ceCliente.TabIndex = 68;
            this.ceCliente.CheckedChanged += new System.EventHandler(this.ceCliente_CheckedChanged);
            // 
            // ceData
            // 
            this.ceData.Location = new System.Drawing.Point(130, 46);
            this.ceData.Name = "ceData";
            this.ceData.Properties.Caption = "";
            this.ceData.Size = new System.Drawing.Size(75, 19);
            this.ceData.TabIndex = 69;
            this.ceData.CheckedChanged += new System.EventHandler(this.ceData_CheckedChanged);
            // 
            // ceFaturado
            // 
            this.ceFaturado.EditValue = true;
            this.ceFaturado.Location = new System.Drawing.Point(358, 27);
            this.ceFaturado.Name = "ceFaturado";
            this.ceFaturado.Properties.Caption = "Faturado";
            this.ceFaturado.Size = new System.Drawing.Size(75, 19);
            this.ceFaturado.TabIndex = 70;
            this.ceFaturado.CheckedChanged += new System.EventHandler(this.ceFaturado_CheckedChanged);
            // 
            // ceAFaturar
            // 
            this.ceAFaturar.EditValue = true;
            this.ceAFaturar.Location = new System.Drawing.Point(439, 27);
            this.ceAFaturar.Name = "ceAFaturar";
            this.ceAFaturar.Properties.Caption = "A Faturar";
            this.ceAFaturar.Size = new System.Drawing.Size(75, 19);
            this.ceAFaturar.TabIndex = 71;
            this.ceAFaturar.CheckedChanged += new System.EventHandler(this.ceAFaturar_CheckedChanged);
            // 
            // ceMultiselecao
            // 
            this.ceMultiselecao.Location = new System.Drawing.Point(358, 52);
            this.ceMultiselecao.Name = "ceMultiselecao";
            this.ceMultiselecao.Properties.Caption = "Selecionar multiplos clientes";
            this.ceMultiselecao.Size = new System.Drawing.Size(156, 19);
            this.ceMultiselecao.TabIndex = 72;
            this.ceMultiselecao.CheckedChanged += new System.EventHandler(this.ceMultiselecao_CheckedChanged);
            // 
            // FormRequisicaoDevolucaoFiltro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 285);
            this.Controls.Add(this.ceMultiselecao);
            this.Controls.Add(this.ceAFaturar);
            this.Controls.Add(this.ceFaturado);
            this.Controls.Add(this.ceData);
            this.Controls.Add(this.ceCliente);
            this.Controls.Add(this.ceRepresentante);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.campoLookupCODCFO);
            this.Controls.Add(this.lblDataFini);
            this.Controls.Add(this.lblDataIni);
            this.Controls.Add(this.txtDataFin);
            this.Controls.Add(this.txtDataIni);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblRepresentante);
            this.Controls.Add(this.campoLookupCODRPR);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRequisicaoDevolucaoFiltro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleção de Representante";
            ((System.ComponentModel.ISupportInitialize)(this.ceRepresentante.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceFaturado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceAFaturar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceMultiselecao.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        public System.Windows.Forms.Label lblDataFini;
        public System.Windows.Forms.Label lblDataIni;
        public System.Windows.Forms.Label lblRepresentante;
        public AppLib.Windows.CampoLookup campoLookupCODRPR;
        public AppLib.Windows.CampoData txtDataFin;
        public AppLib.Windows.CampoData txtDataIni;
        public System.Windows.Forms.Label lblCliente;
        public AppLib.Windows.CampoLookup campoLookupCODCFO;
        public DevExpress.XtraEditors.CheckEdit ceRepresentante;
        public DevExpress.XtraEditors.CheckEdit ceCliente;
        public DevExpress.XtraEditors.CheckEdit ceData;
        public DevExpress.XtraEditors.CheckEdit ceFaturado;
        public DevExpress.XtraEditors.CheckEdit ceAFaturar;
        public DevExpress.XtraEditors.CheckEdit ceMultiselecao;
    }
}