namespace AppLib.Padrao
{
    partial class FormProcessoCadastro
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
            this.label4 = new System.Windows.Forms.Label();
            this.campoUSUARIOALTERACAO = new AppLib.Windows.CampoTexto();
            this.campoDATAALTERACAO = new AppLib.Windows.CampoDataHora();
            this.campoUSUARIOCRIACAO = new AppLib.Windows.CampoTexto();
            this.campoDATACRIACAO = new AppLib.Windows.CampoDataHora();
            this.label3 = new System.Windows.Forms.Label();
            this.campoLista1 = new AppLib.Windows.CampoLista();
            this.label2 = new System.Windows.Forms.Label();
            this.campoTexto2 = new AppLib.Windows.CampoTexto();
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
            this.tabControl1.Size = new System.Drawing.Size(718, 291);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.campoUSUARIOALTERACAO);
            this.tabPage1.Controls.Add(this.campoDATAALTERACAO);
            this.tabPage1.Controls.Add(this.campoUSUARIOCRIACAO);
            this.tabPage1.Controls.Add(this.campoDATACRIACAO);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.campoLista1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.campoTexto2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(710, 265);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(548, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Usuário Alteração";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(342, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Data Alteração";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Usuário Criação";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Data Criação";
            // 
            // campoUSUARIOALTERACAO
            // 
            this.campoUSUARIOALTERACAO.Campo = "USUARIOALTERACAO";
            this.campoUSUARIOALTERACAO.Default = null;
            this.campoUSUARIOALTERACAO.Edita = true;
            this.campoUSUARIOALTERACAO.Enabled = false;
            this.campoUSUARIOALTERACAO.Location = new System.Drawing.Point(548, 204);
            this.campoUSUARIOALTERACAO.MaximoCaracteres = null;
            this.campoUSUARIOALTERACAO.Name = "campoUSUARIOALTERACAO";
            this.campoUSUARIOALTERACAO.Query = 0;
            this.campoUSUARIOALTERACAO.Size = new System.Drawing.Size(100, 20);
            this.campoUSUARIOALTERACAO.Tabela = "ZPROCESSO";
            this.campoUSUARIOALTERACAO.TabIndex = 9;
            // 
            // campoDATAALTERACAO
            // 
            this.campoDATAALTERACAO.Campo = "DATAALTERACAO";
            this.campoDATAALTERACAO.Default = null;
            this.campoDATAALTERACAO.Edita = true;
            this.campoDATAALTERACAO.Enabled = false;
            this.campoDATAALTERACAO.Location = new System.Drawing.Point(342, 204);
            this.campoDATAALTERACAO.Name = "campoDATAALTERACAO";
            this.campoDATAALTERACAO.Query = 0;
            this.campoDATAALTERACAO.Size = new System.Drawing.Size(200, 20);
            this.campoDATAALTERACAO.Tabela = "ZPROCESSO";
            this.campoDATAALTERACAO.TabIndex = 8;
            // 
            // campoUSUARIOCRIACAO
            // 
            this.campoUSUARIOCRIACAO.Campo = "USUARIOCRIACAO";
            this.campoUSUARIOCRIACAO.Default = null;
            this.campoUSUARIOCRIACAO.Edita = true;
            this.campoUSUARIOCRIACAO.Enabled = false;
            this.campoUSUARIOCRIACAO.Location = new System.Drawing.Point(236, 204);
            this.campoUSUARIOCRIACAO.MaximoCaracteres = null;
            this.campoUSUARIOCRIACAO.Name = "campoUSUARIOCRIACAO";
            this.campoUSUARIOCRIACAO.Query = 0;
            this.campoUSUARIOCRIACAO.Size = new System.Drawing.Size(100, 20);
            this.campoUSUARIOCRIACAO.Tabela = "ZPROCESSO";
            this.campoUSUARIOCRIACAO.TabIndex = 7;
            // 
            // campoDATACRIACAO
            // 
            this.campoDATACRIACAO.Campo = "DATACRIACAO";
            this.campoDATACRIACAO.Default = null;
            this.campoDATACRIACAO.Edita = true;
            this.campoDATACRIACAO.Enabled = false;
            this.campoDATACRIACAO.Location = new System.Drawing.Point(30, 204);
            this.campoDATACRIACAO.Name = "campoDATACRIACAO";
            this.campoDATACRIACAO.Query = 0;
            this.campoDATACRIACAO.Size = new System.Drawing.Size(200, 20);
            this.campoDATACRIACAO.Tabela = "ZPROCESSO";
            this.campoDATACRIACAO.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ativo";
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
            this.campoLista1.Location = new System.Drawing.Point(30, 148);
            this.campoLista1.Name = "campoLista1";
            this.campoLista1.Query = 0;
            this.campoLista1.Size = new System.Drawing.Size(100, 21);
            this.campoLista1.Tabela = "ZPROCESSO";
            this.campoLista1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nome";
            // 
            // campoTexto2
            // 
            this.campoTexto2.Campo = "NOME";
            this.campoTexto2.Default = null;
            this.campoTexto2.Edita = true;
            this.campoTexto2.Location = new System.Drawing.Point(30, 98);
            this.campoTexto2.MaximoCaracteres = null;
            this.campoTexto2.Name = "campoTexto2";
            this.campoTexto2.Query = 0;
            this.campoTexto2.Size = new System.Drawing.Size(467, 20);
            this.campoTexto2.Tabela = "ZPROCESSO";
            this.campoTexto2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Código";
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "CODPROCESSO";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(30, 46);
            this.campoTexto1.MaximoCaracteres = null;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(100, 20);
            this.campoTexto1.Tabela = "ZPROCESSO";
            this.campoTexto1.TabIndex = 0;
            // 
            // FormProcessoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BotaoExcluir = true;
            this.BotaoNovo = true;
            this.ClientSize = new System.Drawing.Size(718, 391);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormProcessoCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT * FROM ZPROCESSO WHERE CODPROCESSO = ?"};
            query1.Parametros = new string[] {
        "CODPROCESSO"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZPROCESSO";
            this.Text = "Cadastro de Processo";
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormProcessoCadastro_AntesSalvar);
            this.Load += new System.EventHandler(this.FormProcessoCadastro_Load);
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private AppLib.Windows.CampoTexto campoUSUARIOALTERACAO;
        private AppLib.Windows.CampoDataHora campoDATAALTERACAO;
        private AppLib.Windows.CampoTexto campoUSUARIOCRIACAO;
        private AppLib.Windows.CampoDataHora campoDATACRIACAO;
        private System.Windows.Forms.Label label3;
        private AppLib.Windows.CampoLista campoLista1;
        private System.Windows.Forms.Label label2;
        private AppLib.Windows.CampoTexto campoTexto2;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoTexto campoTexto1;
    }
}