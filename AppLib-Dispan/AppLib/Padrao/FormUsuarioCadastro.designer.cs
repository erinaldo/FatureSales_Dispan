namespace AppLib.Padrao
{
    partial class FormUsuarioCadastro
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
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.campoTexto3 = new AppLib.Windows.CampoTexto();
            this.campoTexto2 = new AppLib.Windows.CampoTexto();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.campoUSUARIOALTERACAO = new AppLib.Windows.CampoTexto();
            this.campoDATAALTERACAO = new AppLib.Windows.CampoDataHora();
            this.campoUSUARIOCRIACAO = new AppLib.Windows.CampoTexto();
            this.campoDATACRIACAO = new AppLib.Windows.CampoDataHora();
            this.label6 = new System.Windows.Forms.Label();
            this.campoLista1 = new AppLib.Windows.CampoLista();
            this.label5 = new System.Windows.Forms.Label();
            this.campoData2 = new AppLib.Windows.CampoData();
            this.label4 = new System.Windows.Forms.Label();
            this.campoData1 = new AppLib.Windows.CampoData();
            this.label3 = new System.Windows.Forms.Label();
            this.campoLookup1 = new AppLib.Windows.CampoLookup();
            this.label1 = new System.Windows.Forms.Label();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
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
            this.tabControl1.Size = new System.Drawing.Size(708, 400);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.campoTexto3);
            this.tabPage1.Controls.Add(this.campoTexto2);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.campoUSUARIOALTERACAO);
            this.tabPage1.Controls.Add(this.campoDATAALTERACAO);
            this.tabPage1.Controls.Add(this.campoUSUARIOCRIACAO);
            this.tabPage1.Controls.Add(this.campoDATACRIACAO);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.campoLista1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.campoData2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.campoData1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.campoLookup1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(700, 374);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 111);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "E-mail";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Nome do Usuário";
            // 
            // campoTexto3
            // 
            this.campoTexto3.Campo = "EMAIL";
            this.campoTexto3.Default = null;
            this.campoTexto3.Edita = true;
            this.campoTexto3.Location = new System.Drawing.Point(26, 127);
            this.campoTexto3.MaximoCaracteres = null;
            this.campoTexto3.Name = "campoTexto3";
            this.campoTexto3.Query = 0;
            this.campoTexto3.Size = new System.Drawing.Size(631, 20);
            this.campoTexto3.Tabela = "ZUSUARIO";
            this.campoTexto3.TabIndex = 2;
            // 
            // campoTexto2
            // 
            this.campoTexto2.Campo = "NOME";
            this.campoTexto2.Default = null;
            this.campoTexto2.Edita = true;
            this.campoTexto2.Location = new System.Drawing.Point(26, 80);
            this.campoTexto2.MaximoCaracteres = null;
            this.campoTexto2.Name = "campoTexto2";
            this.campoTexto2.Query = 0;
            this.campoTexto2.Size = new System.Drawing.Size(631, 20);
            this.campoTexto2.Tabela = "ZUSUARIO";
            this.campoTexto2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(565, 312);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Usuário Alteração";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 312);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Data Alteração";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(241, 312);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Usuário Criação";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 312);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Data Criação";
            // 
            // campoUSUARIOALTERACAO
            // 
            this.campoUSUARIOALTERACAO.Campo = "USUARIOALTERACAO";
            this.campoUSUARIOALTERACAO.Default = null;
            this.campoUSUARIOALTERACAO.Edita = true;
            this.campoUSUARIOALTERACAO.Enabled = false;
            this.campoUSUARIOALTERACAO.Location = new System.Drawing.Point(565, 328);
            this.campoUSUARIOALTERACAO.MaximoCaracteres = null;
            this.campoUSUARIOALTERACAO.Name = "campoUSUARIOALTERACAO";
            this.campoUSUARIOALTERACAO.Query = 0;
            this.campoUSUARIOALTERACAO.Size = new System.Drawing.Size(100, 20);
            this.campoUSUARIOALTERACAO.Tabela = "ZUSUARIO";
            this.campoUSUARIOALTERACAO.TabIndex = 10;
            // 
            // campoDATAALTERACAO
            // 
            this.campoDATAALTERACAO.Campo = "DATAALTERACAO";
            this.campoDATAALTERACAO.Default = null;
            this.campoDATAALTERACAO.Edita = true;
            this.campoDATAALTERACAO.Enabled = false;
            this.campoDATAALTERACAO.Location = new System.Drawing.Point(350, 328);
            this.campoDATAALTERACAO.Name = "campoDATAALTERACAO";
            this.campoDATAALTERACAO.Query = 0;
            this.campoDATAALTERACAO.Size = new System.Drawing.Size(209, 20);
            this.campoDATAALTERACAO.Tabela = "ZUSUARIO";
            this.campoDATAALTERACAO.TabIndex = 9;
            // 
            // campoUSUARIOCRIACAO
            // 
            this.campoUSUARIOCRIACAO.Campo = "USUARIOCRIACAO";
            this.campoUSUARIOCRIACAO.Default = null;
            this.campoUSUARIOCRIACAO.Edita = true;
            this.campoUSUARIOCRIACAO.Enabled = false;
            this.campoUSUARIOCRIACAO.Location = new System.Drawing.Point(241, 328);
            this.campoUSUARIOCRIACAO.MaximoCaracteres = null;
            this.campoUSUARIOCRIACAO.Name = "campoUSUARIOCRIACAO";
            this.campoUSUARIOCRIACAO.Query = 0;
            this.campoUSUARIOCRIACAO.Size = new System.Drawing.Size(100, 20);
            this.campoUSUARIOCRIACAO.Tabela = "ZUSUARIO";
            this.campoUSUARIOCRIACAO.TabIndex = 8;
            // 
            // campoDATACRIACAO
            // 
            this.campoDATACRIACAO.Campo = "DATACRIACAO";
            this.campoDATACRIACAO.Default = null;
            this.campoDATACRIACAO.Edita = true;
            this.campoDATACRIACAO.Enabled = false;
            this.campoDATACRIACAO.Location = new System.Drawing.Point(26, 328);
            this.campoDATACRIACAO.Name = "campoDATACRIACAO";
            this.campoDATACRIACAO.Query = 0;
            this.campoDATACRIACAO.Size = new System.Drawing.Size(209, 20);
            this.campoDATACRIACAO.Tabela = "ZUSUARIO";
            this.campoDATACRIACAO.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 261);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Ativo";
            // 
            // campoLista1
            // 
            this.campoLista1.Campo = "ATIVO";
            this.campoLista1.Default = null;
            this.campoLista1.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.campoLista1.comboBox1.FormattingEnabled = true;
            codigoNome1.Codigo = "1";
            codigoNome1.Nome = "Sim";
            codigoNome2.Codigo = "0";
            codigoNome2.Nome = "Não";
            this.campoLista1.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2};
            this.campoLista1.Location = new System.Drawing.Point(26, 280);
            this.campoLista1.Name = "campoLista1";
            this.campoLista1.Query = 0;
            this.campoLista1.Size = new System.Drawing.Size(100, 21);
            this.campoLista1.Tabela = "ZUSUARIO";
            this.campoLista1.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Ultimo Login";
            // 
            // campoData2
            // 
            this.campoData2.Campo = "ULTIMOLOGIN";
            this.campoData2.Default = null;
            this.campoData2.Edita = true;
            this.campoData2.Enabled = false;
            this.campoData2.Location = new System.Drawing.Point(135, 233);
            this.campoData2.Name = "campoData2";
            this.campoData2.Query = 0;
            this.campoData2.Size = new System.Drawing.Size(100, 21);
            this.campoData2.Tabela = "ZUSUARIO";
            this.campoData2.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Data Valiade";
            // 
            // campoData1
            // 
            this.campoData1.Campo = "DATAVALIDADE";
            this.campoData1.Default = null;
            this.campoData1.Edita = true;
            this.campoData1.Location = new System.Drawing.Point(26, 233);
            this.campoData1.Name = "campoData1";
            this.campoData1.Query = 0;
            this.campoData1.Size = new System.Drawing.Size(100, 21);
            this.campoData1.Tabela = "ZUSUARIO";
            this.campoData1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Perfil";
            // 
            // campoLookup1
            // 
            this.campoLookup1.AutoSize = true;
            this.campoLookup1.Campo = "CODPERFIL";
            this.campoLookup1.ColunaCodigo = "CODPERFIL";
            this.campoLookup1.ColunaDescricao = "NOME";
            this.campoLookup1.ColunaIdentificador = null;
            this.campoLookup1.ColunaTabela = "ZPERFIL";
            this.campoLookup1.Conexao = "Start";
            this.campoLookup1.Default = null;
            this.campoLookup1.Edita = true;
            this.campoLookup1.Location = new System.Drawing.Point(26, 180);
            this.campoLookup1.MaximoCaracteres = null;
            this.campoLookup1.Name = "campoLookup1";
            this.campoLookup1.NomeGrid = null;
            this.campoLookup1.Query = 0;
            this.campoLookup1.Size = new System.Drawing.Size(631, 24);
            this.campoLookup1.Tabela = "ZUSUARIO";
            this.campoLookup1.TabIndex = 3;
            this.campoLookup1.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookup1_SetFormConsulta);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Usuário";
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "USUARIO";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(26, 33);
            this.campoTexto1.MaximoCaracteres = null;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(100, 20);
            this.campoTexto1.Tabela = "ZUSUARIO";
            this.campoTexto1.TabIndex = 0;
            // 
            // FormUsuarioCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 500);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormUsuarioCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZUSUARIO",
        "WHERE USUARIO = ?"};
            query1.Parametros = new string[] {
        "USUARIO"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZUSUARIO";
            this.Text = "Cadastro de Usuário";
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormUsuarioCadastro_AntesSalvar);
            this.Load += new System.EventHandler(this.FormUsuarioCadastro_Load);
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private AppLib.Windows.CampoTexto campoUSUARIOALTERACAO;
        private AppLib.Windows.CampoDataHora campoDATAALTERACAO;
        private AppLib.Windows.CampoTexto campoUSUARIOCRIACAO;
        private AppLib.Windows.CampoDataHora campoDATACRIACAO;
        private System.Windows.Forms.Label label6;
        private AppLib.Windows.CampoLista campoLista1;
        private System.Windows.Forms.Label label5;
        private AppLib.Windows.CampoData campoData2;
        private System.Windows.Forms.Label label4;
        private AppLib.Windows.CampoData campoData1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoTexto campoTexto1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private Windows.CampoTexto campoTexto3;
        private Windows.CampoTexto campoTexto2;
        public Windows.CampoLookup campoLookup1;
    }
}