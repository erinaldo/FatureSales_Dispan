namespace AppFatureClient
{
    partial class FormLancamentoCadastro
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
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label28 = new System.Windows.Forms.Label();
            this.campoDecimalVALORFRETE = new AppLib.Windows.CampoDecimal();
            this.label46 = new System.Windows.Forms.Label();
            this.campoLookupCODCPG = new AppLib.Windows.CampoLookup();
            this.label33 = new System.Windows.Forms.Label();
            this.campoTextoNUMEROMOV = new AppLib.Windows.CampoTexto();
            this.label35 = new System.Windows.Forms.Label();
            this.campoDataENTREGA = new AppLib.Windows.CampoData();
            this.label36 = new System.Windows.Forms.Label();
            this.campoDataEMISSAO = new AppLib.Windows.CampoData();
            this.label37 = new System.Windows.Forms.Label();
            this.campoLookupCODCFO = new AppLib.Windows.CampoLookup();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.campoTexto14 = new AppLib.Windows.CampoTexto();
            this.campoTextoIDMOV = new AppLib.Windows.CampoTexto();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(575, 230);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label28);
            this.tabPage1.Controls.Add(this.campoDecimalVALORFRETE);
            this.tabPage1.Controls.Add(this.label46);
            this.tabPage1.Controls.Add(this.campoLookupCODCPG);
            this.tabPage1.Controls.Add(this.label33);
            this.tabPage1.Controls.Add(this.campoTextoNUMEROMOV);
            this.tabPage1.Controls.Add(this.label35);
            this.tabPage1.Controls.Add(this.campoDataENTREGA);
            this.tabPage1.Controls.Add(this.label36);
            this.tabPage1.Controls.Add(this.campoDataEMISSAO);
            this.tabPage1.Controls.Add(this.label37);
            this.tabPage1.Controls.Add(this.campoLookupCODCFO);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Controls.Add(this.campoTexto14);
            this.tabPage1.Controls.Add(this.campoTextoIDMOV);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(567, 204);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(220, 136);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(70, 13);
            this.label28.TabIndex = 89;
            this.label28.Text = "Valor Original";
            // 
            // campoDecimalVALORFRETE
            // 
            this.campoDecimalVALORFRETE.Campo = "VALORORIGINAL";
            this.campoDecimalVALORFRETE.Decimais = 2;
            this.campoDecimalVALORFRETE.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimalVALORFRETE.Edita = true;
            this.campoDecimalVALORFRETE.Enabled = false;
            this.campoDecimalVALORFRETE.Location = new System.Drawing.Point(223, 152);
            this.campoDecimalVALORFRETE.Name = "campoDecimalVALORFRETE";
            this.campoDecimalVALORFRETE.Query = 0;
            this.campoDecimalVALORFRETE.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalVALORFRETE.Tabela = "FLAN";
            this.campoDecimalVALORFRETE.TabIndex = 6;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(8, 95);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(84, 13);
            this.label46.TabIndex = 87;
            this.label46.Text = "Tipo Documento";
            // 
            // campoLookupCODCPG
            // 
            this.campoLookupCODCPG.AutoSize = true;
            this.campoLookupCODCPG.Campo = "CODTDO";
            this.campoLookupCODCPG.ColunaCodigo = "CODTDO";
            this.campoLookupCODCPG.ColunaDescricao = "DESCRICAO";
            this.campoLookupCODCPG.ColunaIdentificador = null;
            this.campoLookupCODCPG.ColunaTabela = "FTDO";
            this.campoLookupCODCPG.Conexao = "Start";
            this.campoLookupCODCPG.Default = null;
            this.campoLookupCODCPG.Edita = true;
            this.campoLookupCODCPG.Enabled = false;
            this.campoLookupCODCPG.Location = new System.Drawing.Point(11, 111);
            this.campoLookupCODCPG.MaximoCaracteres = null;
            this.campoLookupCODCPG.Name = "campoLookupCODCPG";
            this.campoLookupCODCPG.NomeGrid = null;
            this.campoLookupCODCPG.Query = 0;
            this.campoLookupCODCPG.Size = new System.Drawing.Size(546, 24);
            this.campoLookupCODCPG.Tabela = "FLAN";
            this.campoLookupCODCPG.TabIndex = 3;
            this.campoLookupCODCPG.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupCODCPG_SetFormConsulta);
            this.campoLookupCODCPG.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupCODCPG_SetDescricao);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(8, 14);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(89, 13);
            this.label33.TabIndex = 85;
            this.label33.Text = "Núm. Documento";
            // 
            // campoTextoNUMEROMOV
            // 
            this.campoTextoNUMEROMOV.Campo = "NUMERODOCUMENTO";
            this.campoTextoNUMEROMOV.Default = null;
            this.campoTextoNUMEROMOV.Edita = true;
            this.campoTextoNUMEROMOV.Enabled = false;
            this.campoTextoNUMEROMOV.Location = new System.Drawing.Point(11, 30);
            this.campoTextoNUMEROMOV.MaximoCaracteres = null;
            this.campoTextoNUMEROMOV.Name = "campoTextoNUMEROMOV";
            this.campoTextoNUMEROMOV.Query = 0;
            this.campoTextoNUMEROMOV.Size = new System.Drawing.Size(100, 20);
            this.campoTextoNUMEROMOV.Tabela = "FLAN";
            this.campoTextoNUMEROMOV.TabIndex = 1;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(114, 136);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(88, 13);
            this.label35.TabIndex = 83;
            this.label35.Text = "Data Vencimento";
            // 
            // campoDataENTREGA
            // 
            this.campoDataENTREGA.Campo = "DATAVENCIMENTO";
            this.campoDataENTREGA.Default = null;
            this.campoDataENTREGA.Edita = true;
            this.campoDataENTREGA.Location = new System.Drawing.Point(117, 152);
            this.campoDataENTREGA.Name = "campoDataENTREGA";
            this.campoDataENTREGA.Query = 0;
            this.campoDataENTREGA.Size = new System.Drawing.Size(100, 20);
            this.campoDataENTREGA.Tabela = "FLAN";
            this.campoDataENTREGA.TabIndex = 5;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(8, 136);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(71, 13);
            this.label36.TabIndex = 81;
            this.label36.Text = "Data Emissão";
            // 
            // campoDataEMISSAO
            // 
            this.campoDataEMISSAO.Campo = "DATAEMISSAO";
            this.campoDataEMISSAO.Default = null;
            this.campoDataEMISSAO.Edita = true;
            this.campoDataEMISSAO.Enabled = false;
            this.campoDataEMISSAO.Location = new System.Drawing.Point(11, 152);
            this.campoDataEMISSAO.Name = "campoDataEMISSAO";
            this.campoDataEMISSAO.Query = 0;
            this.campoDataEMISSAO.Size = new System.Drawing.Size(100, 20);
            this.campoDataEMISSAO.Tabela = "FLAN";
            this.campoDataEMISSAO.TabIndex = 4;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(8, 53);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(40, 13);
            this.label37.TabIndex = 79;
            this.label37.Text = "Cliente";
            // 
            // campoLookupCODCFO
            // 
            this.campoLookupCODCFO.AutoSize = true;
            this.campoLookupCODCFO.Campo = "CODCFO";
            this.campoLookupCODCFO.ColunaCodigo = "CODCFO";
            this.campoLookupCODCFO.ColunaDescricao = "NOMEFANTASIA";
            this.campoLookupCODCFO.ColunaIdentificador = null;
            this.campoLookupCODCFO.ColunaTabela = "FCFO";
            this.campoLookupCODCFO.Conexao = "Start";
            this.campoLookupCODCFO.Default = null;
            this.campoLookupCODCFO.Edita = true;
            this.campoLookupCODCFO.Enabled = false;
            this.campoLookupCODCFO.Location = new System.Drawing.Point(11, 69);
            this.campoLookupCODCFO.MaximoCaracteres = null;
            this.campoLookupCODCFO.Name = "campoLookupCODCFO";
            this.campoLookupCODCFO.NomeGrid = null;
            this.campoLookupCODCFO.Query = 0;
            this.campoLookupCODCFO.Size = new System.Drawing.Size(547, 24);
            this.campoLookupCODCFO.Tabela = "FLAN";
            this.campoLookupCODCFO.TabIndex = 2;
            this.campoLookupCODCFO.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupCODCFO_SetFormConsulta);
            this.campoLookupCODCFO.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupCODCFO_SetDescricao);
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "IDLAN";
            this.campoTexto1.Default = "";
            this.campoTexto1.Edita = true;
            this.campoTexto1.Enabled = false;
            this.campoTexto1.Location = new System.Drawing.Point(385, 6);
            this.campoTexto1.MaximoCaracteres = null;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(84, 20);
            this.campoTexto1.Tabela = "FLAN";
            this.campoTexto1.TabIndex = 59;
            this.campoTexto1.Visible = false;
            // 
            // campoTexto14
            // 
            this.campoTexto14.Campo = "CODCOLIGADA";
            this.campoTexto14.Default = "";
            this.campoTexto14.Edita = true;
            this.campoTexto14.Enabled = false;
            this.campoTexto14.Location = new System.Drawing.Point(475, 7);
            this.campoTexto14.MaximoCaracteres = null;
            this.campoTexto14.Name = "campoTexto14";
            this.campoTexto14.Query = 0;
            this.campoTexto14.Size = new System.Drawing.Size(83, 20);
            this.campoTexto14.Tabela = "FLAN";
            this.campoTexto14.TabIndex = 58;
            this.campoTexto14.Visible = false;
            // 
            // campoTextoIDMOV
            // 
            this.campoTextoIDMOV.Campo = "IDMOV";
            this.campoTextoIDMOV.Default = "";
            this.campoTextoIDMOV.Edita = true;
            this.campoTextoIDMOV.Enabled = false;
            this.campoTextoIDMOV.Location = new System.Drawing.Point(474, 33);
            this.campoTextoIDMOV.MaximoCaracteres = null;
            this.campoTextoIDMOV.Name = "campoTextoIDMOV";
            this.campoTextoIDMOV.Query = 0;
            this.campoTextoIDMOV.Size = new System.Drawing.Size(84, 20);
            this.campoTextoIDMOV.Tabela = "FLAN";
            this.campoTextoIDMOV.TabIndex = 57;
            this.campoTextoIDMOV.Visible = false;
            // 
            // FormLancamentoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 330);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormLancamentoCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM FLAN",
        "WHERE CODCOLIGADA = ? AND IDMOV = ? AND IDLAN = ?"};
            query1.Parametros = new string[] {
        "CODCOLIGADA",
        "IDMOV",
        "IDLAN"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "FLAN";
            this.Text = "Lançamentos da Ordem de Produção";
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormLancamentoCadastro_AntesSalvar);
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
        public AppLib.Windows.CampoTexto campoTexto14;
        public AppLib.Windows.CampoTexto campoTextoIDMOV;
        public AppLib.Windows.CampoTexto campoTexto1;
        private System.Windows.Forms.Label label46;
        private AppLib.Windows.CampoLookup campoLookupCODCPG;
        private System.Windows.Forms.Label label33;
        private AppLib.Windows.CampoTexto campoTextoNUMEROMOV;
        private System.Windows.Forms.Label label35;
        private AppLib.Windows.CampoData campoDataENTREGA;
        private System.Windows.Forms.Label label36;
        private AppLib.Windows.CampoData campoDataEMISSAO;
        private System.Windows.Forms.Label label37;
        private AppLib.Windows.CampoLookup campoLookupCODCFO;
        private System.Windows.Forms.Label label28;
        private AppLib.Windows.CampoDecimal campoDecimalVALORFRETE;
    }
}