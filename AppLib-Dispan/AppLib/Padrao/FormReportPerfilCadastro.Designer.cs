namespace AppLib.Padrao
{
    partial class FormReportPerfilCadastro
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
            AppLib.Windows.CodigoNome codigoNome1 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome2 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome3 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome4 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome5 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome6 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.campoLista3 = new AppLib.Windows.CampoLista();
            this.label3 = new System.Windows.Forms.Label();
            this.campoLista2 = new AppLib.Windows.CampoLista();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.campoUSUARIOALTERACAO = new AppLib.Windows.CampoTexto();
            this.campoDATAALTERACAO = new AppLib.Windows.CampoDataHora();
            this.campoUSUARIOCRIACAO = new AppLib.Windows.CampoTexto();
            this.campoDATACRIACAO = new AppLib.Windows.CampoDataHora();
            this.label9 = new System.Windows.Forms.Label();
            this.campoLista1 = new AppLib.Windows.CampoLista();
            this.label2 = new System.Windows.Forms.Label();
            this.campoLookupCODPERFIL = new AppLib.Windows.CampoLookup();
            this.label1 = new System.Windows.Forms.Label();
            this.campoLookupIDREPORT = new AppLib.Windows.CampoLookup();
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
            this.tabControl1.Size = new System.Drawing.Size(711, 337);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.campoLista3);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.campoLista2);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.campoUSUARIOALTERACAO);
            this.tabPage1.Controls.Add(this.campoDATAALTERACAO);
            this.tabPage1.Controls.Add(this.campoUSUARIOCRIACAO);
            this.tabPage1.Controls.Add(this.campoDATACRIACAO);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.campoLista1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.campoLookupCODPERFIL);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.campoLookupIDREPORT);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(703, 311);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(130, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Formartar";
            // 
            // campoLista3
            // 
            this.campoLista3.Campo = "FORMATAR";
            this.campoLista3.Default = null;
            this.campoLista3.Edita = true;
            codigoNome1.Codigo = "1";
            codigoNome1.Nome = "Sim";
            codigoNome2.Codigo = "0";
            codigoNome2.Nome = "Não";
            this.campoLista3.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2};
            this.campoLista3.Location = new System.Drawing.Point(130, 139);
            this.campoLista3.Name = "campoLista3";
            this.campoLista3.Query = 0;
            this.campoLista3.Size = new System.Drawing.Size(100, 21);
            this.campoLista3.Tabela = "ZREPORTPERFIL";
            this.campoLista3.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Visualizar";
            // 
            // campoLista2
            // 
            this.campoLista2.Campo = "VISUALIZAR";
            this.campoLista2.Default = null;
            this.campoLista2.Edita = true;
            codigoNome3.Codigo = "1";
            codigoNome3.Nome = "Sim";
            codigoNome4.Codigo = "0";
            codigoNome4.Nome = "Não";
            this.campoLista2.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome3,
        codigoNome4};
            this.campoLista2.Location = new System.Drawing.Point(21, 139);
            this.campoLista2.Name = "campoLista2";
            this.campoLista2.Query = 0;
            this.campoLista2.Size = new System.Drawing.Size(100, 21);
            this.campoLista2.Tabela = "ZREPORTPERFIL";
            this.campoLista2.TabIndex = 54;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(557, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 53;
            this.label7.Text = "Usuário Alteração";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(339, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 52;
            this.label6.Text = "Data Alteração";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 51;
            this.label5.Text = "Usuário Criação";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 240);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Data Criação";
            // 
            // campoUSUARIOALTERACAO
            // 
            this.campoUSUARIOALTERACAO.Campo = "USUARIOALTERACAO";
            this.campoUSUARIOALTERACAO.Default = null;
            this.campoUSUARIOALTERACAO.Edita = false;
            this.campoUSUARIOALTERACAO.Enabled = false;
            this.campoUSUARIOALTERACAO.Location = new System.Drawing.Point(557, 256);
            this.campoUSUARIOALTERACAO.MaximoCaracteres = null;
            this.campoUSUARIOALTERACAO.Name = "campoUSUARIOALTERACAO";
            this.campoUSUARIOALTERACAO.Query = 0;
            this.campoUSUARIOALTERACAO.Size = new System.Drawing.Size(100, 20);
            this.campoUSUARIOALTERACAO.Tabela = "ZREPORTPERFIL";
            this.campoUSUARIOALTERACAO.TabIndex = 49;
            // 
            // campoDATAALTERACAO
            // 
            this.campoDATAALTERACAO.Campo = "DATAALTERACAO";
            this.campoDATAALTERACAO.Default = null;
            this.campoDATAALTERACAO.Edita = false;
            this.campoDATAALTERACAO.Enabled = false;
            this.campoDATAALTERACAO.Location = new System.Drawing.Point(342, 256);
            this.campoDATAALTERACAO.Name = "campoDATAALTERACAO";
            this.campoDATAALTERACAO.Query = 0;
            this.campoDATAALTERACAO.Size = new System.Drawing.Size(209, 20);
            this.campoDATAALTERACAO.Tabela = "ZREPORTPERFIL";
            this.campoDATAALTERACAO.TabIndex = 48;
            // 
            // campoUSUARIOCRIACAO
            // 
            this.campoUSUARIOCRIACAO.Campo = "USUARIOCRIACAO";
            this.campoUSUARIOCRIACAO.Default = null;
            this.campoUSUARIOCRIACAO.Edita = false;
            this.campoUSUARIOCRIACAO.Enabled = false;
            this.campoUSUARIOCRIACAO.Location = new System.Drawing.Point(236, 256);
            this.campoUSUARIOCRIACAO.MaximoCaracteres = null;
            this.campoUSUARIOCRIACAO.Name = "campoUSUARIOCRIACAO";
            this.campoUSUARIOCRIACAO.Query = 0;
            this.campoUSUARIOCRIACAO.Size = new System.Drawing.Size(100, 20);
            this.campoUSUARIOCRIACAO.Tabela = "ZREPORTPERFIL";
            this.campoUSUARIOCRIACAO.TabIndex = 47;
            // 
            // campoDATACRIACAO
            // 
            this.campoDATACRIACAO.Campo = "DATACRIACAO";
            this.campoDATACRIACAO.Default = null;
            this.campoDATACRIACAO.Edita = false;
            this.campoDATACRIACAO.Enabled = false;
            this.campoDATACRIACAO.Location = new System.Drawing.Point(21, 256);
            this.campoDATACRIACAO.Name = "campoDATACRIACAO";
            this.campoDATACRIACAO.Query = 0;
            this.campoDATACRIACAO.Size = new System.Drawing.Size(209, 20);
            this.campoDATACRIACAO.Tabela = "ZREPORTPERFIL";
            this.campoDATACRIACAO.TabIndex = 46;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 181);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "Ativo";
            // 
            // campoLista1
            // 
            this.campoLista1.Campo = "ATIVO";
            this.campoLista1.Default = null;
            this.campoLista1.Edita = true;
            codigoNome5.Codigo = "1";
            codigoNome5.Nome = "Sim";
            codigoNome6.Codigo = "0";
            codigoNome6.Nome = "Não";
            this.campoLista1.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome5,
        codigoNome6};
            this.campoLista1.Location = new System.Drawing.Point(21, 200);
            this.campoLista1.Name = "campoLista1";
            this.campoLista1.Query = 0;
            this.campoLista1.Size = new System.Drawing.Size(100, 21);
            this.campoLista1.Tabela = "ZREPORTPERFIL";
            this.campoLista1.TabIndex = 44;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Perfil";
            // 
            // campoLookupCODPERFIL
            // 
            this.campoLookupCODPERFIL.AutoSize = true;
            this.campoLookupCODPERFIL.Campo = "CODPERFIL";
            this.campoLookupCODPERFIL.ColunaCodigo = "CODPERFIL";
            this.campoLookupCODPERFIL.ColunaDescricao = "NOME";
            this.campoLookupCODPERFIL.ColunaIdentificador = null;
            this.campoLookupCODPERFIL.ColunaTabela = "ZPERFIL";
            this.campoLookupCODPERFIL.Conexao = "Start";
            this.campoLookupCODPERFIL.Default = null;
            this.campoLookupCODPERFIL.Edita = true;
            this.campoLookupCODPERFIL.EditaLookup = false;
            this.campoLookupCODPERFIL.Location = new System.Drawing.Point(21, 88);
            this.campoLookupCODPERFIL.MaximoCaracteres = null;
            this.campoLookupCODPERFIL.Name = "campoLookupCODPERFIL";
            this.campoLookupCODPERFIL.NomeGrid = null;
            this.campoLookupCODPERFIL.Query = 0;
            this.campoLookupCODPERFIL.Size = new System.Drawing.Size(597, 24);
            this.campoLookupCODPERFIL.Tabela = "ZREPORTPERFIL";
            this.campoLookupCODPERFIL.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Report";
            // 
            // campoLookupIDREPORT
            // 
            this.campoLookupIDREPORT.AutoSize = true;
            this.campoLookupIDREPORT.Campo = "IDREPORT";
            this.campoLookupIDREPORT.ColunaCodigo = "IDREPORT";
            this.campoLookupIDREPORT.ColunaDescricao = "NOME";
            this.campoLookupIDREPORT.ColunaIdentificador = null;
            this.campoLookupIDREPORT.ColunaTabela = "ZREPORT";
            this.campoLookupIDREPORT.Conexao = "Start";
            this.campoLookupIDREPORT.Default = null;
            this.campoLookupIDREPORT.Edita = true;
            this.campoLookupIDREPORT.EditaLookup = false;
            this.campoLookupIDREPORT.Location = new System.Drawing.Point(21, 36);
            this.campoLookupIDREPORT.MaximoCaracteres = null;
            this.campoLookupIDREPORT.Name = "campoLookupIDREPORT";
            this.campoLookupIDREPORT.NomeGrid = null;
            this.campoLookupIDREPORT.Query = 0;
            this.campoLookupIDREPORT.Size = new System.Drawing.Size(597, 24);
            this.campoLookupIDREPORT.Tabela = "ZREPORTPERFIL";
            this.campoLookupIDREPORT.TabIndex = 40;
            // 
            // FormReportPerfilCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 437);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormReportPerfilCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZREPORTPERFIL",
        "WHERE IDREPORT = ?",
        "  AND CODPERFIL = ?"};
            query1.Parametros = new string[] {
        "IDREPORT",
        "CODPERFIL"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZREPORTPERFIL";
            this.Text = "Cadastro de Report/Perfil";
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormReportPerfilCadastro_AntesSalvar);
            this.Load += new System.EventHandler(this.FormReportPerfilCadastro_Load);
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
        private System.Windows.Forms.Label label4;
        private Windows.CampoLista campoLista3;
        private System.Windows.Forms.Label label3;
        private Windows.CampoLista campoLista2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private Windows.CampoTexto campoUSUARIOALTERACAO;
        private Windows.CampoDataHora campoDATAALTERACAO;
        private Windows.CampoTexto campoUSUARIOCRIACAO;
        private Windows.CampoDataHora campoDATACRIACAO;
        private System.Windows.Forms.Label label9;
        private Windows.CampoLista campoLista1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public Windows.CampoLookup campoLookupCODPERFIL;
        public Windows.CampoLookup campoLookupIDREPORT;
    }
}