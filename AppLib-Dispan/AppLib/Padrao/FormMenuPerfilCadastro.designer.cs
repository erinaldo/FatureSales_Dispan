namespace AppLib.Padrao
{
    partial class FormMenuPerfilCadastro
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
            AppLib.Windows.CodigoNome codigoNome7 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome8 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.campoLista4 = new AppLib.Windows.CampoLista();
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
            this.campoLookup2 = new AppLib.Windows.CampoLookup();
            this.label1 = new System.Windows.Forms.Label();
            this.campoLookup1 = new AppLib.Windows.CampoLookup();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.campoMemo1 = new AppLib.Windows.CampoMemo();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 362);
            this.panel1.Size = new System.Drawing.Size(678, 61);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(678, 323);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.campoLista4);
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
            this.tabPage1.Controls.Add(this.campoLookup2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.campoLookup1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(670, 297);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(238, 123);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "Excluir";
            // 
            // campoLista4
            // 
            this.campoLista4.Campo = "EXCLUIR";
            this.campoLista4.Default = null;
            this.campoLista4.Edita = true;
            codigoNome1.Codigo = "1";
            codigoNome1.Nome = "Sim";
            codigoNome2.Codigo = "0";
            codigoNome2.Nome = "Não";
            this.campoLista4.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2};
            this.campoLista4.Location = new System.Drawing.Point(238, 142);
            this.campoLista4.Name = "campoLista4";
            this.campoLista4.Query = 0;
            this.campoLista4.Size = new System.Drawing.Size(100, 21);
            this.campoLista4.Tabela = "ZMENUPERFIL";
            this.campoLista4.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Cadastrar";
            // 
            // campoLista3
            // 
            this.campoLista3.Campo = "CADASTRAR";
            this.campoLista3.Default = null;
            this.campoLista3.Edita = true;
            codigoNome3.Codigo = "1";
            codigoNome3.Nome = "Sim";
            codigoNome4.Codigo = "0";
            codigoNome4.Nome = "Não";
            this.campoLista3.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome3,
        codigoNome4};
            this.campoLista3.Location = new System.Drawing.Point(132, 142);
            this.campoLista3.Name = "campoLista3";
            this.campoLista3.Query = 0;
            this.campoLista3.Size = new System.Drawing.Size(100, 21);
            this.campoLista3.Tabela = "ZMENUPERFIL";
            this.campoLista3.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Consultar";
            // 
            // campoLista2
            // 
            this.campoLista2.Campo = "CONSULTAR";
            this.campoLista2.Default = null;
            this.campoLista2.Edita = true;
            codigoNome5.Codigo = "1";
            codigoNome5.Nome = "Sim";
            codigoNome6.Codigo = "0";
            codigoNome6.Nome = "Não";
            this.campoLista2.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome5,
        codigoNome6};
            this.campoLista2.Location = new System.Drawing.Point(23, 142);
            this.campoLista2.Name = "campoLista2";
            this.campoLista2.Query = 0;
            this.campoLista2.Size = new System.Drawing.Size(100, 21);
            this.campoLista2.Tabela = "ZMENUPERFIL";
            this.campoLista2.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(559, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Usuário Alteração";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(341, 243);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Data Alteração";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(238, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Usuário Criação";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 243);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Data Criação";
            // 
            // campoUSUARIOALTERACAO
            // 
            this.campoUSUARIOALTERACAO.Campo = "USUARIOALTERACAO";
            this.campoUSUARIOALTERACAO.Default = null;
            this.campoUSUARIOALTERACAO.Edita = true;
            this.campoUSUARIOALTERACAO.Enabled = false;
            this.campoUSUARIOALTERACAO.Location = new System.Drawing.Point(559, 259);
            this.campoUSUARIOALTERACAO.MaximoCaracteres = null;
            this.campoUSUARIOALTERACAO.Name = "campoUSUARIOALTERACAO";
            this.campoUSUARIOALTERACAO.Query = 0;
            this.campoUSUARIOALTERACAO.Size = new System.Drawing.Size(100, 20);
            this.campoUSUARIOALTERACAO.Tabela = "ZMENUPERFIL";
            this.campoUSUARIOALTERACAO.TabIndex = 29;
            // 
            // campoDATAALTERACAO
            // 
            this.campoDATAALTERACAO.Campo = "DATAALTERACAO";
            this.campoDATAALTERACAO.Default = null;
            this.campoDATAALTERACAO.Edita = true;
            this.campoDATAALTERACAO.Enabled = false;
            this.campoDATAALTERACAO.Location = new System.Drawing.Point(344, 259);
            this.campoDATAALTERACAO.Name = "campoDATAALTERACAO";
            this.campoDATAALTERACAO.Query = 0;
            this.campoDATAALTERACAO.Size = new System.Drawing.Size(209, 20);
            this.campoDATAALTERACAO.Tabela = "ZMENUPERFIL";
            this.campoDATAALTERACAO.TabIndex = 28;
            // 
            // campoUSUARIOCRIACAO
            // 
            this.campoUSUARIOCRIACAO.Campo = "USUARIOCRIACAO";
            this.campoUSUARIOCRIACAO.Default = null;
            this.campoUSUARIOCRIACAO.Edita = true;
            this.campoUSUARIOCRIACAO.Enabled = false;
            this.campoUSUARIOCRIACAO.Location = new System.Drawing.Point(238, 259);
            this.campoUSUARIOCRIACAO.MaximoCaracteres = null;
            this.campoUSUARIOCRIACAO.Name = "campoUSUARIOCRIACAO";
            this.campoUSUARIOCRIACAO.Query = 0;
            this.campoUSUARIOCRIACAO.Size = new System.Drawing.Size(100, 20);
            this.campoUSUARIOCRIACAO.Tabela = "ZMENUPERFIL";
            this.campoUSUARIOCRIACAO.TabIndex = 27;
            // 
            // campoDATACRIACAO
            // 
            this.campoDATACRIACAO.Campo = "DATACRIACAO";
            this.campoDATACRIACAO.Default = null;
            this.campoDATACRIACAO.Edita = true;
            this.campoDATACRIACAO.Enabled = false;
            this.campoDATACRIACAO.Location = new System.Drawing.Point(23, 259);
            this.campoDATACRIACAO.Name = "campoDATACRIACAO";
            this.campoDATACRIACAO.Query = 0;
            this.campoDATACRIACAO.Size = new System.Drawing.Size(209, 20);
            this.campoDATACRIACAO.Tabela = "ZMENUPERFIL";
            this.campoDATACRIACAO.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 184);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Ativo";
            // 
            // campoLista1
            // 
            this.campoLista1.Campo = "ATIVO";
            this.campoLista1.Default = null;
            this.campoLista1.Edita = true;
            codigoNome7.Codigo = "1";
            codigoNome7.Nome = "Sim";
            codigoNome8.Codigo = "0";
            codigoNome8.Nome = "Não";
            this.campoLista1.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome7,
        codigoNome8};
            this.campoLista1.Location = new System.Drawing.Point(23, 203);
            this.campoLista1.Name = "campoLista1";
            this.campoLista1.Query = 0;
            this.campoLista1.Size = new System.Drawing.Size(100, 21);
            this.campoLista1.Tabela = "ZMENUPERFIL";
            this.campoLista1.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Perfil";
            // 
            // campoLookup2
            // 
            this.campoLookup2.AutoSize = true;
            this.campoLookup2.Campo = "CODPERFIL";
            this.campoLookup2.ColunaCodigo = "CODPERFIL";
            this.campoLookup2.ColunaDescricao = "NOME";
            this.campoLookup2.ColunaIdentificador = null;
            this.campoLookup2.ColunaTabela = "ZPERFIL";
            this.campoLookup2.Conexao = "Start";
            this.campoLookup2.Default = null;
            this.campoLookup2.Edita = true;
            this.campoLookup2.EditaLookup = false;
            this.campoLookup2.Location = new System.Drawing.Point(23, 91);
            this.campoLookup2.MaximoCaracteres = null;
            this.campoLookup2.Name = "campoLookup2";
            this.campoLookup2.NomeGrid = null;
            this.campoLookup2.Query = 0;
            this.campoLookup2.Size = new System.Drawing.Size(597, 24);
            this.campoLookup2.Tabela = "ZMENUPERFIL";
            this.campoLookup2.TabIndex = 2;
            this.campoLookup2.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookup2_SetFormConsulta);
            this.campoLookup2.Leave += new System.EventHandler(this.campoLookup2_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Menu";
            // 
            // campoLookup1
            // 
            this.campoLookup1.AutoSize = true;
            this.campoLookup1.Campo = "CODMENU";
            this.campoLookup1.ColunaCodigo = "CODMENU";
            this.campoLookup1.ColunaDescricao = "NOME";
            this.campoLookup1.ColunaIdentificador = null;
            this.campoLookup1.ColunaTabela = "ZMENU";
            this.campoLookup1.Conexao = "Start";
            this.campoLookup1.Default = null;
            this.campoLookup1.Edita = true;
            this.campoLookup1.EditaLookup = false;
            this.campoLookup1.Location = new System.Drawing.Point(23, 39);
            this.campoLookup1.MaximoCaracteres = null;
            this.campoLookup1.Name = "campoLookup1";
            this.campoLookup1.NomeGrid = null;
            this.campoLookup1.Query = 0;
            this.campoLookup1.Size = new System.Drawing.Size(597, 24);
            this.campoLookup1.Tabela = "ZMENUPERFIL";
            this.campoLookup1.TabIndex = 0;
            this.campoLookup1.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookup1_SetFormConsulta);
            this.campoLookup1.Leave += new System.EventHandler(this.campoLookup1_Leave);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.campoMemo1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(670, 297);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Filtro Estendido";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // campoMemo1
            // 
            this.campoMemo1.Campo = "FILTRO";
            this.campoMemo1.Default = null;
            this.campoMemo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.campoMemo1.Edita = true;
            this.campoMemo1.Location = new System.Drawing.Point(3, 3);
            this.campoMemo1.MaximoCaracteres = 4000;
            this.campoMemo1.Name = "campoMemo1";
            this.campoMemo1.Query = 0;
            this.campoMemo1.Size = new System.Drawing.Size(664, 291);
            this.campoMemo1.Tabela = "ZMENUPERFIL";
            this.campoMemo1.TabIndex = 0;
            // 
            // FormMenuPerfilCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 423);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormMenuPerfilCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZMENUPERFIL",
        "WHERE CODMENU = ?",
        "  AND CODPERFIL = ?"};
            query1.Parametros = new string[] {
        "CODMENU",
        "CODPERFIL"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZMENUPERFIL";
            this.Text = "Cadastro de Menu/Perfil";
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormMenuPerfilCadastro_AntesSalvar);
            this.Load += new System.EventHandler(this.FormMenuPerfilCadastro_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private AppLib.Windows.CampoTexto campoUSUARIOALTERACAO;
        private AppLib.Windows.CampoDataHora campoDATAALTERACAO;
        private AppLib.Windows.CampoTexto campoUSUARIOCRIACAO;
        private AppLib.Windows.CampoDataHora campoDATACRIACAO;
        private System.Windows.Forms.Label label9;
        private AppLib.Windows.CampoLista campoLista1;
        private System.Windows.Forms.Label label10;
        private AppLib.Windows.CampoLista campoLista4;
        private System.Windows.Forms.Label label4;
        private AppLib.Windows.CampoLista campoLista3;
        private System.Windows.Forms.Label label3;
        private AppLib.Windows.CampoLista campoLista2;
        private System.Windows.Forms.TabPage tabPage2;
        private AppLib.Windows.CampoMemo campoMemo1;
        public Windows.CampoLookup campoLookup2;
        public Windows.CampoLookup campoLookup1;
    }
}