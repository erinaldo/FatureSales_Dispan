namespace AppLib.Padrao
{
    partial class FormMenuCadastro
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
            this.label4 = new System.Windows.Forms.Label();
            this.campoTexto4 = new AppLib.Windows.CampoTexto();
            this.label3 = new System.Windows.Forms.Label();
            this.campoTexto3 = new AppLib.Windows.CampoTexto();
            this.label2 = new System.Windows.Forms.Label();
            this.campoTexto2 = new AppLib.Windows.CampoTexto();
            this.label1 = new System.Windows.Forms.Label();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridData1 = new AppLib.Windows.GridData();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(697, 369);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
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
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.campoTexto4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.campoTexto3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.campoTexto2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(689, 343);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(562, 291);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Usuário Alteração";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(347, 291);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Data Alteração";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(241, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Usuário Criação";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 291);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Data Criação";
            // 
            // campoUSUARIOALTERACAO
            // 
            this.campoUSUARIOALTERACAO.Campo = "USUARIOALTERACAO";
            this.campoUSUARIOALTERACAO.Default = null;
            this.campoUSUARIOALTERACAO.Edita = true;
            this.campoUSUARIOALTERACAO.Enabled = false;
            this.campoUSUARIOALTERACAO.Location = new System.Drawing.Point(562, 307);
            this.campoUSUARIOALTERACAO.MaximoCaracteres = null;
            this.campoUSUARIOALTERACAO.Name = "campoUSUARIOALTERACAO";
            this.campoUSUARIOALTERACAO.Query = 0;
            this.campoUSUARIOALTERACAO.Size = new System.Drawing.Size(100, 20);
            this.campoUSUARIOALTERACAO.Tabela = "ZMENU";
            this.campoUSUARIOALTERACAO.TabIndex = 19;
            // 
            // campoDATAALTERACAO
            // 
            this.campoDATAALTERACAO.Campo = "DATAALTERACAO";
            this.campoDATAALTERACAO.Default = null;
            this.campoDATAALTERACAO.Edita = true;
            this.campoDATAALTERACAO.Enabled = false;
            this.campoDATAALTERACAO.Location = new System.Drawing.Point(350, 307);
            this.campoDATAALTERACAO.Name = "campoDATAALTERACAO";
            this.campoDATAALTERACAO.Query = 0;
            this.campoDATAALTERACAO.Size = new System.Drawing.Size(206, 20);
            this.campoDATAALTERACAO.Tabela = "ZMENU";
            this.campoDATAALTERACAO.TabIndex = 18;
            // 
            // campoUSUARIOCRIACAO
            // 
            this.campoUSUARIOCRIACAO.Campo = "USUARIOCRIACAO";
            this.campoUSUARIOCRIACAO.Default = null;
            this.campoUSUARIOCRIACAO.Edita = true;
            this.campoUSUARIOCRIACAO.Enabled = false;
            this.campoUSUARIOCRIACAO.Location = new System.Drawing.Point(241, 307);
            this.campoUSUARIOCRIACAO.MaximoCaracteres = null;
            this.campoUSUARIOCRIACAO.Name = "campoUSUARIOCRIACAO";
            this.campoUSUARIOCRIACAO.Query = 0;
            this.campoUSUARIOCRIACAO.Size = new System.Drawing.Size(100, 20);
            this.campoUSUARIOCRIACAO.Tabela = "ZMENU";
            this.campoUSUARIOCRIACAO.TabIndex = 17;
            // 
            // campoDATACRIACAO
            // 
            this.campoDATACRIACAO.Campo = "DATACRIACAO";
            this.campoDATACRIACAO.Default = null;
            this.campoDATACRIACAO.Edita = true;
            this.campoDATACRIACAO.Enabled = false;
            this.campoDATACRIACAO.Location = new System.Drawing.Point(29, 307);
            this.campoDATACRIACAO.Name = "campoDATACRIACAO";
            this.campoDATACRIACAO.Query = 0;
            this.campoDATACRIACAO.Size = new System.Drawing.Size(206, 20);
            this.campoDATACRIACAO.Tabela = "ZMENU";
            this.campoDATACRIACAO.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Ativo";
            // 
            // campoLista1
            // 
            this.campoLista1.Campo = "ATIVO";
            this.campoLista1.Default = null;
            codigoNome1.Codigo = "1";
            codigoNome1.Nome = "Sim";
            codigoNome2.Codigo = "0";
            codigoNome2.Nome = "Não";
            this.campoLista1.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2};
            this.campoLista1.Location = new System.Drawing.Point(29, 251);
            this.campoLista1.Name = "campoLista1";
            this.campoLista1.Query = 0;
            this.campoLista1.Size = new System.Drawing.Size(100, 21);
            this.campoLista1.Tabela = "ZMENU";
            this.campoLista1.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Cadastro (Tabela Principal)";
            // 
            // campoTexto4
            // 
            this.campoTexto4.Campo = "TELACADASTRO";
            this.campoTexto4.Default = null;
            this.campoTexto4.Edita = true;
            this.campoTexto4.Location = new System.Drawing.Point(29, 199);
            this.campoTexto4.MaximoCaracteres = null;
            this.campoTexto4.Name = "campoTexto4";
            this.campoTexto4.Query = 0;
            this.campoTexto4.Size = new System.Drawing.Size(206, 20);
            this.campoTexto4.Tabela = "ZMENU";
            this.campoTexto4.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Visão (Nome)";
            // 
            // campoTexto3
            // 
            this.campoTexto3.Campo = "TELACONSULTA";
            this.campoTexto3.Default = null;
            this.campoTexto3.Edita = true;
            this.campoTexto3.Location = new System.Drawing.Point(29, 147);
            this.campoTexto3.MaximoCaracteres = null;
            this.campoTexto3.Name = "campoTexto3";
            this.campoTexto3.Query = 0;
            this.campoTexto3.Size = new System.Drawing.Size(206, 20);
            this.campoTexto3.Tabela = "ZMENU";
            this.campoTexto3.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Menu";
            // 
            // campoTexto2
            // 
            this.campoTexto2.Campo = "NOME";
            this.campoTexto2.Default = null;
            this.campoTexto2.Edita = true;
            this.campoTexto2.Location = new System.Drawing.Point(29, 97);
            this.campoTexto2.MaximoCaracteres = null;
            this.campoTexto2.Name = "campoTexto2";
            this.campoTexto2.Query = 0;
            this.campoTexto2.Size = new System.Drawing.Size(418, 20);
            this.campoTexto2.Tabela = "ZMENU";
            this.campoTexto2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Código";
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "CODMENU";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(29, 43);
            this.campoTexto1.MaximoCaracteres = null;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(206, 20);
            this.campoTexto1.Tabela = "ZMENU";
            this.campoTexto1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridData1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(689, 343);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Fluxo Visão";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridData1
            // 
            this.gridData1.AutoAjuste = true;
            this.gridData1.BotaoEditar = true;
            this.gridData1.BotaoExcluir = true;
            this.gridData1.BotaoNovo = true;
            this.gridData1.Conexao = "Start";
            this.gridData1.Consulta = new string[] {
        "SELECT *",
        "FROM ZMENUFLUXOVISAO",
        "WHERE CODMENU = ?"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = "ZMENUFLUXOVISAO";
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(683, 337);
            this.gridData1.TabIndex = 0;
            this.gridData1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData1_SetParametros);
            this.gridData1.Novo += new AppLib.Windows.GridData.NovoHandler(this.gridData1_Novo);
            this.gridData1.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData1_Editar);
            this.gridData1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData1_Excluir);
            // 
            // FormMenuCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 469);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormMenuCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT * FROM ZMENU WHERE CODMENU = ?"};
            query1.Parametros = new string[] {
        "CODMENU"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZMENU";
            this.Text = "Cadastro de Menu";
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormMenuCadastro_AntesSalvar);
            this.Load += new System.EventHandler(this.FormMenuCadastro_Load);
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
        private System.Windows.Forms.Label label4;
        private AppLib.Windows.CampoTexto campoTexto4;
        private System.Windows.Forms.Label label3;
        private AppLib.Windows.CampoTexto campoTexto3;
        private System.Windows.Forms.Label label2;
        private AppLib.Windows.CampoTexto campoTexto2;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoTexto campoTexto1;
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
        private System.Windows.Forms.TabPage tabPage2;
        private Windows.GridData gridData1;
    }
}