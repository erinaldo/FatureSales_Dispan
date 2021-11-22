namespace AppFatureClient
{
    partial class FormAgendaCadastro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAgendaCadastro));
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.campoTextoCIDADE = new AppLib.Windows.CampoTexto();
            this.campoLookup2 = new AppLib.Windows.CampoLookup();
            this.campoLookup1 = new AppLib.Windows.CampoLookup();
            this.campoMemoOBS = new AppLib.Windows.CampoMemo();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoEMAIL = new AppLib.Windows.CampoTexto();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoFAX = new AppLib.Windows.CampoTexto();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoCELULAR = new AppLib.Windows.CampoTexto();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoTELEFONE1 = new AppLib.Windows.CampoTexto();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoTELEFONE = new AppLib.Windows.CampoTexto();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoCEP = new AppLib.Windows.CampoTexto();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoENDERECO = new AppLib.Windows.CampoTexto();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoAPELIDO = new AppLib.Windows.CampoTexto();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoNOME = new AppLib.Windows.CampoTexto();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroIDAGENDA = new AppLib.Windows.CampoInteiro();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 462);
            this.panel1.Size = new System.Drawing.Size(681, 61);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(681, 423);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoTextoCIDADE);
            this.tabPage1.Controls.Add(this.campoLookup2);
            this.tabPage1.Controls.Add(this.campoLookup1);
            this.tabPage1.Controls.Add(this.campoMemoOBS);
            this.tabPage1.Controls.Add(this.labelControl13);
            this.tabPage1.Controls.Add(this.campoTextoEMAIL);
            this.tabPage1.Controls.Add(this.labelControl12);
            this.tabPage1.Controls.Add(this.campoTextoFAX);
            this.tabPage1.Controls.Add(this.labelControl11);
            this.tabPage1.Controls.Add(this.campoTextoCELULAR);
            this.tabPage1.Controls.Add(this.labelControl10);
            this.tabPage1.Controls.Add(this.campoTextoTELEFONE1);
            this.tabPage1.Controls.Add(this.labelControl9);
            this.tabPage1.Controls.Add(this.campoTextoTELEFONE);
            this.tabPage1.Controls.Add(this.labelControl8);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.campoTextoCEP);
            this.tabPage1.Controls.Add(this.labelControl7);
            this.tabPage1.Controls.Add(this.campoTextoENDERECO);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.campoTextoAPELIDO);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.campoTextoNOME);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.campoInteiroIDAGENDA);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(673, 397);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // campoTextoCIDADE
            // 
            this.campoTextoCIDADE.Campo = "CIDADE";
            this.campoTextoCIDADE.Default = null;
            this.campoTextoCIDADE.Edita = true;
            this.campoTextoCIDADE.Location = new System.Drawing.Point(650, 3);
            this.campoTextoCIDADE.MaximoCaracteres = 10;
            this.campoTextoCIDADE.Name = "campoTextoCIDADE";
            this.campoTextoCIDADE.Query = 0;
            this.campoTextoCIDADE.Size = new System.Drawing.Size(10, 20);
            this.campoTextoCIDADE.Tabela = "ZAGENDA";
            this.campoTextoCIDADE.TabIndex = 32;
            this.campoTextoCIDADE.Visible = false;
            // 
            // campoLookup2
            // 
            this.campoLookup2.Campo = "ESTADO";
            this.campoLookup2.ColunaCodigo = "CODETD";
            this.campoLookup2.ColunaDescricao = "NOME";
            this.campoLookup2.ColunaIdentificador = null;
            this.campoLookup2.ColunaTabela = "GETD";
            this.campoLookup2.Conexao = "Start";
            this.campoLookup2.Default = null;
            this.campoLookup2.Edita = true;
            this.campoLookup2.EditaLookup = false;
            this.campoLookup2.Location = new System.Drawing.Point(9, 129);
            this.campoLookup2.MaximoCaracteres = null;
            this.campoLookup2.Name = "campoLookup2";
            this.campoLookup2.NomeGrid = null;
            this.campoLookup2.Query = 0;
            this.campoLookup2.Size = new System.Drawing.Size(240, 21);
            this.campoLookup2.Tabela = "ZAGENDA";
            this.campoLookup2.TabIndex = 4;
            this.campoLookup2.AposSelecao += new AppLib.Windows.CampoLookup.AposSelecaoHandler(this.campoLookup2_AposSelecao);
            this.campoLookup2.Leave += new System.EventHandler(this.campoLookup2_Leave);
            // 
            // campoLookup1
            // 
            this.campoLookup1.Campo = "CODMUNICIPIO";
            this.campoLookup1.ColunaCodigo = "CODMUNICIPIO";
            this.campoLookup1.ColunaDescricao = "NOMEMUNICIPIO";
            this.campoLookup1.ColunaIdentificador = null;
            this.campoLookup1.ColunaTabela = "GMUNICIPIO";
            this.campoLookup1.Conexao = "Start";
            this.campoLookup1.Default = null;
            this.campoLookup1.Edita = true;
            this.campoLookup1.EditaLookup = false;
            this.campoLookup1.Enabled = false;
            this.campoLookup1.Location = new System.Drawing.Point(255, 129);
            this.campoLookup1.MaximoCaracteres = null;
            this.campoLookup1.Name = "campoLookup1";
            this.campoLookup1.NomeGrid = null;
            this.campoLookup1.Query = 0;
            this.campoLookup1.Size = new System.Drawing.Size(405, 21);
            this.campoLookup1.Tabela = "ZAGENDA";
            this.campoLookup1.TabIndex = 6;
            this.campoLookup1.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookup1_SetFormConsulta);
            this.campoLookup1.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookup1_SetDescricao);
            this.campoLookup1.AposSelecao += new AppLib.Windows.CampoLookup.AposSelecaoHandler(this.campoLookup1_AposSelecao);
            this.campoLookup1.Leave += new System.EventHandler(this.campoLookup1_Leave);
            // 
            // campoMemoOBS
            // 
            this.campoMemoOBS.Campo = "OBS";
            this.campoMemoOBS.Default = null;
            this.campoMemoOBS.Edita = true;
            this.campoMemoOBS.Location = new System.Drawing.Point(9, 290);
            this.campoMemoOBS.MaximoCaracteres = 255;
            this.campoMemoOBS.Name = "campoMemoOBS";
            this.campoMemoOBS.Query = 0;
            this.campoMemoOBS.Size = new System.Drawing.Size(651, 96);
            this.campoMemoOBS.Tabela = "ZAGENDA";
            this.campoMemoOBS.TabIndex = 31;
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(9, 270);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(20, 13);
            this.labelControl13.TabIndex = 28;
            this.labelControl13.Text = "OBS";
            // 
            // campoTextoEMAIL
            // 
            this.campoTextoEMAIL.Campo = "EMAIL";
            this.campoTextoEMAIL.Default = null;
            this.campoTextoEMAIL.Edita = true;
            this.campoTextoEMAIL.Location = new System.Drawing.Point(9, 230);
            this.campoTextoEMAIL.MaximoCaracteres = 100;
            this.campoTextoEMAIL.Name = "campoTextoEMAIL";
            this.campoTextoEMAIL.Query = 0;
            this.campoTextoEMAIL.Size = new System.Drawing.Size(433, 20);
            this.campoTextoEMAIL.Tabela = "ZAGENDA";
            this.campoTextoEMAIL.TabIndex = 11;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(9, 211);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(34, 13);
            this.labelControl12.TabIndex = 26;
            this.labelControl12.Text = "E-MAIL";
            // 
            // campoTextoFAX
            // 
            this.campoTextoFAX.Campo = "FAX";
            this.campoTextoFAX.Default = null;
            this.campoTextoFAX.Edita = true;
            this.campoTextoFAX.Location = new System.Drawing.Point(450, 230);
            this.campoTextoFAX.MaximoCaracteres = 14;
            this.campoTextoFAX.Name = "campoTextoFAX";
            this.campoTextoFAX.Query = 0;
            this.campoTextoFAX.Size = new System.Drawing.Size(210, 20);
            this.campoTextoFAX.Tabela = "ZAGENDA";
            this.campoTextoFAX.TabIndex = 10;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(450, 211);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(19, 13);
            this.labelControl11.TabIndex = 24;
            this.labelControl11.Text = "FAX";
            // 
            // campoTextoCELULAR
            // 
            this.campoTextoCELULAR.Campo = "CELULAR";
            this.campoTextoCELULAR.Default = null;
            this.campoTextoCELULAR.Edita = true;
            this.campoTextoCELULAR.Location = new System.Drawing.Point(450, 176);
            this.campoTextoCELULAR.MaximoCaracteres = 15;
            this.campoTextoCELULAR.Name = "campoTextoCELULAR";
            this.campoTextoCELULAR.Query = 0;
            this.campoTextoCELULAR.Size = new System.Drawing.Size(210, 20);
            this.campoTextoCELULAR.Tabela = "ZAGENDA";
            this.campoTextoCELULAR.TabIndex = 9;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(450, 157);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(44, 13);
            this.labelControl10.TabIndex = 22;
            this.labelControl10.Text = "CELULAR";
            // 
            // campoTextoTELEFONE1
            // 
            this.campoTextoTELEFONE1.Campo = "TELEFONE1";
            this.campoTextoTELEFONE1.Default = null;
            this.campoTextoTELEFONE1.Edita = true;
            this.campoTextoTELEFONE1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.campoTextoTELEFONE1.Location = new System.Drawing.Point(232, 176);
            this.campoTextoTELEFONE1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.campoTextoTELEFONE1.MaximoCaracteres = 14;
            this.campoTextoTELEFONE1.Name = "campoTextoTELEFONE1";
            this.campoTextoTELEFONE1.Query = 0;
            this.campoTextoTELEFONE1.Size = new System.Drawing.Size(210, 20);
            this.campoTextoTELEFONE1.Tabela = "ZAGENDA";
            this.campoTextoTELEFONE1.TabIndex = 8;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(232, 156);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(56, 13);
            this.labelControl9.TabIndex = 20;
            this.labelControl9.Text = "TELEFONE1";
            // 
            // campoTextoTELEFONE
            // 
            this.campoTextoTELEFONE.Campo = "TELEFONE";
            this.campoTextoTELEFONE.Default = null;
            this.campoTextoTELEFONE.Edita = true;
            this.campoTextoTELEFONE.Font = new System.Drawing.Font("Tahoma", 22F);
            this.campoTextoTELEFONE.Location = new System.Drawing.Point(9, 176);
            this.campoTextoTELEFONE.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.campoTextoTELEFONE.MaximoCaracteres = 14;
            this.campoTextoTELEFONE.Name = "campoTextoTELEFONE";
            this.campoTextoTELEFONE.Query = 0;
            this.campoTextoTELEFONE.Size = new System.Drawing.Size(210, 20);
            this.campoTextoTELEFONE.Tabela = "ZAGENDA";
            this.campoTextoTELEFONE.TabIndex = 7;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(9, 156);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(50, 13);
            this.labelControl8.TabIndex = 18;
            this.labelControl8.Text = "TELEFONE";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(9, 110);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(40, 13);
            this.labelControl6.TabIndex = 17;
            this.labelControl6.Text = "ESTADO";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(255, 110);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(38, 13);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "CIDADE";
            // 
            // campoTextoCEP
            // 
            this.campoTextoCEP.Campo = "CEP";
            this.campoTextoCEP.Default = null;
            this.campoTextoCEP.Edita = true;
            this.campoTextoCEP.Location = new System.Drawing.Point(474, 86);
            this.campoTextoCEP.MaximoCaracteres = 10;
            this.campoTextoCEP.Name = "campoTextoCEP";
            this.campoTextoCEP.Query = 0;
            this.campoTextoCEP.Size = new System.Drawing.Size(186, 20);
            this.campoTextoCEP.Tabela = "ZAGENDA";
            this.campoTextoCEP.TabIndex = 5;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(474, 67);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(19, 13);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "CEP";
            // 
            // campoTextoENDERECO
            // 
            this.campoTextoENDERECO.Campo = "ENDERECO";
            this.campoTextoENDERECO.Default = null;
            this.campoTextoENDERECO.Edita = true;
            this.campoTextoENDERECO.Location = new System.Drawing.Point(9, 86);
            this.campoTextoENDERECO.MaximoCaracteres = 100;
            this.campoTextoENDERECO.Name = "campoTextoENDERECO";
            this.campoTextoENDERECO.Query = 0;
            this.campoTextoENDERECO.Size = new System.Drawing.Size(459, 20);
            this.campoTextoENDERECO.Tabela = "ZAGENDA";
            this.campoTextoENDERECO.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(9, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(54, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "ENDERECO";
            // 
            // campoTextoAPELIDO
            // 
            this.campoTextoAPELIDO.Campo = "APELIDO";
            this.campoTextoAPELIDO.Default = null;
            this.campoTextoAPELIDO.Edita = true;
            this.campoTextoAPELIDO.Location = new System.Drawing.Point(474, 39);
            this.campoTextoAPELIDO.MaximoCaracteres = 100;
            this.campoTextoAPELIDO.Name = "campoTextoAPELIDO";
            this.campoTextoAPELIDO.Query = 0;
            this.campoTextoAPELIDO.Size = new System.Drawing.Size(186, 20);
            this.campoTextoAPELIDO.Tabela = "ZAGENDA";
            this.campoTextoAPELIDO.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(474, 20);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(43, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "APELIDO";
            // 
            // campoTextoNOME
            // 
            this.campoTextoNOME.Campo = "NOME";
            this.campoTextoNOME.Default = null;
            this.campoTextoNOME.Edita = true;
            this.campoTextoNOME.Location = new System.Drawing.Point(115, 39);
            this.campoTextoNOME.MaximoCaracteres = 100;
            this.campoTextoNOME.Name = "campoTextoNOME";
            this.campoTextoNOME.Query = 0;
            this.campoTextoNOME.Size = new System.Drawing.Size(353, 20);
            this.campoTextoNOME.Tabela = "ZAGENDA";
            this.campoTextoNOME.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(115, 20);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "NOME";
            // 
            // campoInteiroIDAGENDA
            // 
            this.campoInteiroIDAGENDA.Campo = "IDAGENDA";
            this.campoInteiroIDAGENDA.Default = null;
            this.campoInteiroIDAGENDA.Edita = false;
            this.campoInteiroIDAGENDA.Location = new System.Drawing.Point(9, 39);
            this.campoInteiroIDAGENDA.Name = "campoInteiroIDAGENDA";
            this.campoInteiroIDAGENDA.Query = 0;
            this.campoInteiroIDAGENDA.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroIDAGENDA.Tabela = "ZAGENDA";
            this.campoInteiroIDAGENDA.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "IDAGENDA";
            // 
            // FormAgendaCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 523);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAgendaCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT * FROM ZAGENDA WHERE IDAGENDA = ?"};
            query1.Parametros = new string[] {
        "IDAGENDA"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZAGENDA";
            this.Text = "Agenda Telefônica";
            this.ValidarExcluir += new AppLib.Windows.FormCadastroData.ValidarExcluirHandler(this.FormAgendaCadastro_ValidarExcluir);
            this.AntesNovo += new AppLib.Windows.FormCadastroData.AntesNovoHandler(this.FormAgendaCadastro_AntesNovo);
            this.AposNovo += new AppLib.Windows.FormCadastroData.AposNovoHandler(this.FormAgendaCadastro_AposNovo);
            this.AntesExcluir += new AppLib.Windows.FormCadastroData.AntesExcluirHandler(this.FormAgendaCadastro_AntesExcluir);
            this.Load += new System.EventHandler(this.FormAgendaCadastro_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private AppLib.Windows.CampoTexto campoTextoCEP;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private AppLib.Windows.CampoTexto campoTextoENDERECO;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private AppLib.Windows.CampoTexto campoTextoAPELIDO;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private AppLib.Windows.CampoTexto campoTextoNOME;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private AppLib.Windows.CampoInteiro campoInteiroIDAGENDA;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private AppLib.Windows.CampoTexto campoTextoEMAIL;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private AppLib.Windows.CampoTexto campoTextoFAX;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private AppLib.Windows.CampoTexto campoTextoCELULAR;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private AppLib.Windows.CampoTexto campoTextoTELEFONE1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private AppLib.Windows.CampoTexto campoTextoTELEFONE;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private AppLib.Windows.CampoMemo campoMemoOBS;
        private AppLib.Windows.CampoLookup campoLookup2;
        private AppLib.Windows.CampoLookup campoLookup1;
        private AppLib.Windows.CampoTexto campoTextoCIDADE;
    }
}