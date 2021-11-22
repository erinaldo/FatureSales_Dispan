namespace AppLib.Padrao
{
    partial class FormSQLCadastro
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
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoUSUARIOALTERACAO = new AppLib.Windows.CampoTexto();
            this.campoDATAALTERACAO = new AppLib.Windows.CampoDataHora();
            this.campoTextoUSUARIOCRIACAO = new AppLib.Windows.CampoTexto();
            this.campoDATACRIACAO = new AppLib.Windows.CampoDataHora();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.campoListaATIVO = new AppLib.Windows.CampoLista();
            this.campoTextoCONEXAO = new AppLib.Windows.CampoTexto();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoNOME = new AppLib.Windows.CampoTexto();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroIDSQL = new AppLib.Windows.CampoInteiro();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
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
            this.tabControl1.Size = new System.Drawing.Size(676, 305);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl9);
            this.tabPage1.Controls.Add(this.labelControl8);
            this.tabPage1.Controls.Add(this.labelControl7);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.campoTextoUSUARIOALTERACAO);
            this.tabPage1.Controls.Add(this.campoDATAALTERACAO);
            this.tabPage1.Controls.Add(this.campoTextoUSUARIOCRIACAO);
            this.tabPage1.Controls.Add(this.campoDATACRIACAO);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.campoListaATIVO);
            this.tabPage1.Controls.Add(this.campoTextoCONEXAO);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.campoTextoNOME);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoInteiroIDSQL);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(668, 279);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(542, 210);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(85, 13);
            this.labelControl9.TabIndex = 54;
            this.labelControl9.Text = "Usuário Alteração";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(335, 210);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(72, 13);
            this.labelControl8.TabIndex = 53;
            this.labelControl8.Text = "Data Alteração";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(229, 210);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(75, 13);
            this.labelControl7.TabIndex = 52;
            this.labelControl7.Text = "Usuário Criação";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(22, 210);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(62, 13);
            this.labelControl6.TabIndex = 51;
            this.labelControl6.Text = "Data Criação";
            // 
            // campoTextoUSUARIOALTERACAO
            // 
            this.campoTextoUSUARIOALTERACAO.Campo = "USUARIOALTERACAO";
            this.campoTextoUSUARIOALTERACAO.Default = null;
            this.campoTextoUSUARIOALTERACAO.Edita = false;
            this.campoTextoUSUARIOALTERACAO.Location = new System.Drawing.Point(542, 229);
            this.campoTextoUSUARIOALTERACAO.MaximoCaracteres = null;
            this.campoTextoUSUARIOALTERACAO.Name = "campoTextoUSUARIOALTERACAO";
            this.campoTextoUSUARIOALTERACAO.Query = 0;
            this.campoTextoUSUARIOALTERACAO.Size = new System.Drawing.Size(100, 20);
            this.campoTextoUSUARIOALTERACAO.Tabela = "ZSQL";
            this.campoTextoUSUARIOALTERACAO.TabIndex = 7;
            // 
            // campoDATAALTERACAO
            // 
            this.campoDATAALTERACAO.Campo = "DATAALTERACAO";
            this.campoDATAALTERACAO.Default = null;
            this.campoDATAALTERACAO.Edita = false;
            this.campoDATAALTERACAO.Location = new System.Drawing.Point(335, 229);
            this.campoDATAALTERACAO.Name = "campoDATAALTERACAO";
            this.campoDATAALTERACAO.Query = 0;
            this.campoDATAALTERACAO.Size = new System.Drawing.Size(200, 20);
            this.campoDATAALTERACAO.Tabela = "ZSQL";
            this.campoDATAALTERACAO.TabIndex = 6;
            // 
            // campoTextoUSUARIOCRIACAO
            // 
            this.campoTextoUSUARIOCRIACAO.Campo = "USUARIOCRIACAO";
            this.campoTextoUSUARIOCRIACAO.Default = null;
            this.campoTextoUSUARIOCRIACAO.Edita = false;
            this.campoTextoUSUARIOCRIACAO.Location = new System.Drawing.Point(229, 229);
            this.campoTextoUSUARIOCRIACAO.MaximoCaracteres = null;
            this.campoTextoUSUARIOCRIACAO.Name = "campoTextoUSUARIOCRIACAO";
            this.campoTextoUSUARIOCRIACAO.Query = 0;
            this.campoTextoUSUARIOCRIACAO.Size = new System.Drawing.Size(100, 20);
            this.campoTextoUSUARIOCRIACAO.Tabela = "ZSQL";
            this.campoTextoUSUARIOCRIACAO.TabIndex = 5;
            // 
            // campoDATACRIACAO
            // 
            this.campoDATACRIACAO.Campo = "DATACRIACAO";
            this.campoDATACRIACAO.Default = null;
            this.campoDATACRIACAO.Edita = false;
            this.campoDATACRIACAO.Location = new System.Drawing.Point(22, 229);
            this.campoDATACRIACAO.Name = "campoDATACRIACAO";
            this.campoDATACRIACAO.Query = 0;
            this.campoDATACRIACAO.Size = new System.Drawing.Size(200, 20);
            this.campoDATACRIACAO.Tabela = "ZSQL";
            this.campoDATACRIACAO.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(22, 162);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(25, 13);
            this.labelControl5.TabIndex = 46;
            this.labelControl5.Text = "Ativo";
            // 
            // campoListaATIVO
            // 
            this.campoListaATIVO.Campo = "ATIVO";
            this.campoListaATIVO.Default = null;
            this.campoListaATIVO.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.campoListaATIVO.comboBox1.FormattingEnabled = true;
            codigoNome1.Codigo = "1";
            codigoNome1.Nome = "Sim";
            codigoNome2.Codigo = "0";
            codigoNome2.Nome = "Não";
            this.campoListaATIVO.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2};
            this.campoListaATIVO.Location = new System.Drawing.Point(22, 181);
            this.campoListaATIVO.Name = "campoListaATIVO";
            this.campoListaATIVO.Query = 0;
            this.campoListaATIVO.Size = new System.Drawing.Size(100, 21);
            this.campoListaATIVO.Tabela = "ZSQL";
            this.campoListaATIVO.TabIndex = 3;
            // 
            // campoTextoCONEXAO
            // 
            this.campoTextoCONEXAO.Campo = "CONEXAO";
            this.campoTextoCONEXAO.Default = "Start";
            this.campoTextoCONEXAO.Edita = true;
            this.campoTextoCONEXAO.Location = new System.Drawing.Point(22, 134);
            this.campoTextoCONEXAO.MaximoCaracteres = null;
            this.campoTextoCONEXAO.Name = "campoTextoCONEXAO";
            this.campoTextoCONEXAO.Query = 0;
            this.campoTextoCONEXAO.Size = new System.Drawing.Size(100, 20);
            this.campoTextoCONEXAO.Tabela = "ZSQL";
            this.campoTextoCONEXAO.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(22, 115);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(43, 13);
            this.labelControl4.TabIndex = 43;
            this.labelControl4.Text = "Conexão";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(22, 63);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(27, 13);
            this.labelControl2.TabIndex = 40;
            this.labelControl2.Text = "Nome";
            // 
            // campoTextoNOME
            // 
            this.campoTextoNOME.Campo = "NOME";
            this.campoTextoNOME.Default = null;
            this.campoTextoNOME.Edita = true;
            this.campoTextoNOME.Location = new System.Drawing.Point(22, 82);
            this.campoTextoNOME.MaximoCaracteres = 100;
            this.campoTextoNOME.Name = "campoTextoNOME";
            this.campoTextoNOME.Query = 0;
            this.campoTextoNOME.Size = new System.Drawing.Size(620, 20);
            this.campoTextoNOME.Tabela = "ZSQL";
            this.campoTextoNOME.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 38;
            this.labelControl1.Text = "Identificador";
            // 
            // campoInteiroIDSQL
            // 
            this.campoInteiroIDSQL.Campo = "IDSQL";
            this.campoInteiroIDSQL.Default = null;
            this.campoInteiroIDSQL.Edita = false;
            this.campoInteiroIDSQL.Location = new System.Drawing.Point(22, 35);
            this.campoInteiroIDSQL.Name = "campoInteiroIDSQL";
            this.campoInteiroIDSQL.Query = 0;
            this.campoInteiroIDSQL.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroIDSQL.Tabela = "ZSQL";
            this.campoInteiroIDSQL.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.richTextBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(668, 279);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Comando";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(662, 273);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // FormSQLCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BotaoExcluir = true;
            this.BotaoNovo = true;
            this.ClientSize = new System.Drawing.Size(676, 405);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormSQLCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZSQL",
        "WHERE IDSQL = ?"};
            query1.Parametros = new string[] {
        "IDSQL"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZSQL";
            this.Text = "Cadastro de Consulta SQL";
            this.AposNovo += new AppLib.Windows.FormCadastroData.AposNovoHandler(this.FormSQLCadastro_AposNovo);
            this.AposEditar += new AppLib.Windows.FormCadastroData.AposEditarHandler(this.FormSQLCadastro_AposEditar);
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormSQLCadastro_AntesSalvar);
            this.AposSalvar += new AppLib.Windows.FormCadastroData.AposSalvarHandler(this.FormSQLCadastro_AposSalvar);
            this.Load += new System.EventHandler(this.FormSQLCadastro_Load);
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
        private System.Windows.Forms.TabPage tabPage2;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private Windows.CampoTexto campoTextoUSUARIOALTERACAO;
        private Windows.CampoDataHora campoDATAALTERACAO;
        private Windows.CampoTexto campoTextoUSUARIOCRIACAO;
        private Windows.CampoDataHora campoDATACRIACAO;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Windows.CampoLista campoListaATIVO;
        private Windows.CampoTexto campoTextoCONEXAO;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Windows.CampoTexto campoTextoNOME;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Windows.CampoInteiro campoInteiroIDSQL;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}