namespace AppFatureClient
{
    partial class FormProdutoCompostoCadastro
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
            this.campoInteiro1 = new AppLib.Windows.CampoInteiro();
            this.campoDataHora3 = new AppLib.Windows.CampoDataHora();
            this.campoLookup1 = new AppLib.Windows.CampoLookup();
            this.label4 = new System.Windows.Forms.Label();
            this.campoDecimalQUANTIDADE = new AppLib.Windows.CampoDecimal();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.campoLookupPRODUTO = new AppLib.Windows.CampoLookup();
            this.campoTexto14 = new AppLib.Windows.CampoTexto();
            this.campoTextoIDPRD = new AppLib.Windows.CampoTexto();
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
            this.tabControl1.Size = new System.Drawing.Size(575, 194);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoInteiro1);
            this.tabPage1.Controls.Add(this.campoDataHora3);
            this.tabPage1.Controls.Add(this.campoLookup1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.campoDecimalQUANTIDADE);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.campoLookupPRODUTO);
            this.tabPage1.Controls.Add(this.campoTexto14);
            this.tabPage1.Controls.Add(this.campoTextoIDPRD);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(567, 168);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "NUMEROSEQUENCIAL";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Edita = true;
            this.campoInteiro1.Location = new System.Drawing.Point(465, 90);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(84, 20);
            this.campoInteiro1.Tabela = "TPRDCOMPOSTO";
            this.campoInteiro1.TabIndex = 3;
            this.campoInteiro1.Visible = false;
            // 
            // campoDataHora3
            // 
            this.campoDataHora3.Campo = "RECMODIFIEDON";
            this.campoDataHora3.Default = null;
            this.campoDataHora3.Edita = true;
            this.campoDataHora3.Location = new System.Drawing.Point(465, 64);
            this.campoDataHora3.Name = "campoDataHora3";
            this.campoDataHora3.Query = 0;
            this.campoDataHora3.Size = new System.Drawing.Size(84, 20);
            this.campoDataHora3.Tabela = "TPRDCOMPOSTO";
            this.campoDataHora3.TabIndex = 2;
            this.campoDataHora3.Visible = false;
            // 
            // campoLookup1
            // 
            this.campoLookup1.AutoSize = true;
            this.campoLookup1.Campo = "CODUND";
            this.campoLookup1.ColunaCodigo = "CODUND";
            this.campoLookup1.ColunaDescricao = "DESCRICAO";
            this.campoLookup1.ColunaIdentificador = null;
            this.campoLookup1.ColunaTabela = "TUND";
            this.campoLookup1.Conexao = "Start";
            this.campoLookup1.Default = null;
            this.campoLookup1.Edita = true;
            this.campoLookup1.Location = new System.Drawing.Point(8, 74);
            this.campoLookup1.MaximoCaracteres = null;
            this.campoLookup1.Name = "campoLookup1";
            this.campoLookup1.NomeGrid = null;
            this.campoLookup1.Query = 0;
            this.campoLookup1.Size = new System.Drawing.Size(451, 24);
            this.campoLookup1.Tabela = "TPRDCOMPOSTO";
            this.campoLookup1.TabIndex = 5;
            this.campoLookup1.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookup1_SetFormConsulta);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "Unidade Medida";
            // 
            // campoDecimalQUANTIDADE
            // 
            this.campoDecimalQUANTIDADE.Campo = "QUANTIDADE";
            this.campoDecimalQUANTIDADE.Decimais = 4;
            this.campoDecimalQUANTIDADE.Default = null;
            this.campoDecimalQUANTIDADE.Edita = true;
            this.campoDecimalQUANTIDADE.Location = new System.Drawing.Point(8, 116);
            this.campoDecimalQUANTIDADE.Name = "campoDecimalQUANTIDADE";
            this.campoDecimalQUANTIDADE.Query = 0;
            this.campoDecimalQUANTIDADE.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalQUANTIDADE.Tabela = "TPRDCOMPOSTO";
            this.campoDecimalQUANTIDADE.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 59;
            this.label2.Text = "Quantidade";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Produto";
            // 
            // campoLookupPRODUTO
            // 
            this.campoLookupPRODUTO.AutoSize = true;
            this.campoLookupPRODUTO.Campo = "IDPRDCOMPONENTE";
            this.campoLookupPRODUTO.ColunaCodigo = "IDPRD";
            this.campoLookupPRODUTO.ColunaDescricao = "NOME";
            this.campoLookupPRODUTO.ColunaIdentificador = null;
            this.campoLookupPRODUTO.ColunaTabela = "TPRD";
            this.campoLookupPRODUTO.Conexao = "Start";
            this.campoLookupPRODUTO.Default = null;
            this.campoLookupPRODUTO.Edita = true;
            this.campoLookupPRODUTO.Location = new System.Drawing.Point(8, 35);
            this.campoLookupPRODUTO.MaximoCaracteres = null;
            this.campoLookupPRODUTO.Name = "campoLookupPRODUTO";
            this.campoLookupPRODUTO.NomeGrid = null;
            this.campoLookupPRODUTO.Query = 0;
            this.campoLookupPRODUTO.Size = new System.Drawing.Size(451, 24);
            this.campoLookupPRODUTO.Tabela = "TPRDCOMPOSTO";
            this.campoLookupPRODUTO.TabIndex = 4;
            this.campoLookupPRODUTO.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupPRODUTO_SetFormConsulta);
            this.campoLookupPRODUTO.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupPRODUTO_SetDescricao);
            // 
            // campoTexto14
            // 
            this.campoTexto14.Campo = "CODCOLIGADA";
            this.campoTexto14.Default = "";
            this.campoTexto14.Edita = true;
            this.campoTexto14.Location = new System.Drawing.Point(466, 12);
            this.campoTexto14.MaximoCaracteres = null;
            this.campoTexto14.Name = "campoTexto14";
            this.campoTexto14.Query = 0;
            this.campoTexto14.Size = new System.Drawing.Size(83, 20);
            this.campoTexto14.Tabela = "TPRDCOMPOSTO";
            this.campoTexto14.TabIndex = 0;
            this.campoTexto14.Visible = false;
            // 
            // campoTextoIDPRD
            // 
            this.campoTextoIDPRD.Campo = "IDPRD";
            this.campoTextoIDPRD.Default = "";
            this.campoTextoIDPRD.Edita = true;
            this.campoTextoIDPRD.Location = new System.Drawing.Point(465, 38);
            this.campoTextoIDPRD.MaximoCaracteres = null;
            this.campoTextoIDPRD.Name = "campoTextoIDPRD";
            this.campoTextoIDPRD.Query = 0;
            this.campoTextoIDPRD.Size = new System.Drawing.Size(84, 20);
            this.campoTextoIDPRD.Tabela = "TPRDCOMPOSTO";
            this.campoTextoIDPRD.TabIndex = 1;
            this.campoTextoIDPRD.Visible = false;
            // 
            // FormProdutoCompostoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 294);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormProdutoCompostoCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT * ",
        "FROM TPRDCOMPOSTO",
        "WHERE CODCOLIGADA = ? AND IDPRD = ? AND IDPRDCOMPONENTE = ?"};
            query1.Parametros = new string[] {
        "CODCOLIGADA",
        "IDPRD",
        "IDPRDCOMPONENTE"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "TPRDCOMPOSTO";
            this.Text = "Composição do Produto";
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormProdutoCompostoCadastro_AntesSalvar);
            this.Load += new System.EventHandler(this.FormProdutoCompostoCadastro_Load);
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
        private AppLib.Windows.CampoLookup campoLookup1;
        private System.Windows.Forms.Label label4;
        private AppLib.Windows.CampoDecimal campoDecimalQUANTIDADE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoLookup campoLookupPRODUTO;
        private AppLib.Windows.CampoDataHora campoDataHora3;
        private AppLib.Windows.CampoInteiro campoInteiro1;
        public AppLib.Windows.CampoTexto campoTexto14;
        public AppLib.Windows.CampoTexto campoTextoIDPRD;
    }
}