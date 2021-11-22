namespace AppLib.Padrao
{
    partial class FormExcelParamCadastro
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
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.campoLista2 = new AppLib.Windows.CampoLista();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiro2 = new AppLib.Windows.CampoInteiro();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiro1 = new AppLib.Windows.CampoInteiro();
            this.campoTexto5 = new AppLib.Windows.CampoTexto();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto6 = new AppLib.Windows.CampoTexto();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto7 = new AppLib.Windows.CampoTexto();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
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
            this.tabControl1.Size = new System.Drawing.Size(584, 390);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoTexto5);
            this.tabPage1.Controls.Add(this.labelControl9);
            this.tabPage1.Controls.Add(this.campoTexto6);
            this.tabPage1.Controls.Add(this.labelControl10);
            this.tabPage1.Controls.Add(this.campoTexto7);
            this.tabPage1.Controls.Add(this.labelControl11);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Controls.Add(this.campoLista2);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.campoInteiro2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoInteiro1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(576, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(18, 126);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Tipo de Valor";
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "ORIGDIGTEXTO";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(18, 90);
            this.campoTexto1.MaximoCaracteres = 100;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(491, 20);
            this.campoTexto1.Tabela = "ZPLANILHAPARAM";
            this.campoTexto1.TabIndex = 0;
            // 
            // campoLista2
            // 
            this.campoLista2.Campo = "ORIGDIGTIPO";
            this.campoLista2.Default = null;
            this.campoLista2.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.campoLista2.comboBox1.FormattingEnabled = true;
            codigoNome1.Codigo = "S";
            codigoNome1.Nome = "String";
            codigoNome2.Codigo = "D";
            codigoNome2.Nome = "Data";
            codigoNome3.Codigo = "I";
            codigoNome3.Nome = "Inteiro";
            codigoNome4.Codigo = "V";
            codigoNome4.Nome = "Valor (Decimal)";
            this.campoLista2.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2,
        codigoNome3,
        codigoNome4};
            this.campoLista2.Location = new System.Drawing.Point(18, 145);
            this.campoLista2.Name = "campoLista2";
            this.campoLista2.Query = 0;
            this.campoLista2.Size = new System.Drawing.Size(182, 21);
            this.campoLista2.Tabela = "ZPLANILHAPARAM";
            this.campoLista2.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(18, 71);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(28, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Texto";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(124, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "IDPLANILHA (FK)";
            this.labelControl2.Visible = false;
            // 
            // campoInteiro2
            // 
            this.campoInteiro2.Campo = "IDPLANILHA";
            this.campoInteiro2.Default = null;
            this.campoInteiro2.Edita = false;
            this.campoInteiro2.Location = new System.Drawing.Point(124, 36);
            this.campoInteiro2.Name = "campoInteiro2";
            this.campoInteiro2.Query = 0;
            this.campoInteiro2.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro2.Tabela = "ZPLANILHAPARAM";
            this.campoInteiro2.TabIndex = 1;
            this.campoInteiro2.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Identificador";
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "IDPLANILHAPARAM";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Edita = false;
            this.campoInteiro1.Location = new System.Drawing.Point(18, 36);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro1.Tabela = "ZPLANILHAPARAM";
            this.campoInteiro1.TabIndex = 0;
            // 
            // campoTexto5
            // 
            this.campoTexto5.Campo = "DESTCELLINHA";
            this.campoTexto5.Default = null;
            this.campoTexto5.Edita = true;
            this.campoTexto5.Location = new System.Drawing.Point(18, 314);
            this.campoTexto5.MaximoCaracteres = null;
            this.campoTexto5.Name = "campoTexto5";
            this.campoTexto5.Query = 0;
            this.campoTexto5.Size = new System.Drawing.Size(182, 20);
            this.campoTexto5.Tabela = "ZPLANILHAPARAM";
            this.campoTexto5.TabIndex = 22;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(18, 295);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(25, 13);
            this.labelControl9.TabIndex = 25;
            this.labelControl9.Text = "Linha";
            // 
            // campoTexto6
            // 
            this.campoTexto6.Campo = "DESTCELCOLUNA";
            this.campoTexto6.Default = null;
            this.campoTexto6.Edita = true;
            this.campoTexto6.Location = new System.Drawing.Point(18, 259);
            this.campoTexto6.MaximoCaracteres = null;
            this.campoTexto6.Name = "campoTexto6";
            this.campoTexto6.Query = 0;
            this.campoTexto6.Size = new System.Drawing.Size(182, 20);
            this.campoTexto6.Tabela = "ZPLANILHAPARAM";
            this.campoTexto6.TabIndex = 21;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(18, 240);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(33, 13);
            this.labelControl10.TabIndex = 24;
            this.labelControl10.Text = "Coluna";
            // 
            // campoTexto7
            // 
            this.campoTexto7.Campo = "DESTCELABA";
            this.campoTexto7.Default = null;
            this.campoTexto7.Edita = true;
            this.campoTexto7.Location = new System.Drawing.Point(18, 205);
            this.campoTexto7.MaximoCaracteres = null;
            this.campoTexto7.Name = "campoTexto7";
            this.campoTexto7.Query = 0;
            this.campoTexto7.Size = new System.Drawing.Size(491, 20);
            this.campoTexto7.Tabela = "ZPLANILHAPARAM";
            this.campoTexto7.TabIndex = 20;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(18, 186);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(19, 13);
            this.labelControl11.TabIndex = 23;
            this.labelControl11.Text = "Aba";
            // 
            // FormExcelParamCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BotaoExcluir = true;
            this.BotaoNovo = true;
            this.ClientSize = new System.Drawing.Size(584, 490);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormExcelParamCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZPLANILHAPARAM",
        "WHERE IDPLANILHAPARAM = ?"};
            query1.Parametros = new string[] {
        "IDPLANILHAPARAM"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZPLANILHAPARAM";
            this.Text = "Cadastro de Parâmetros";
            this.Load += new System.EventHandler(this.FormExcelParamCadastro_Load);
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
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Windows.CampoInteiro campoInteiro2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Windows.CampoInteiro campoInteiro1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Windows.CampoTexto campoTexto1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Windows.CampoLista campoLista2;
        private Windows.CampoTexto campoTexto5;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private Windows.CampoTexto campoTexto6;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private Windows.CampoTexto campoTexto7;
        private DevExpress.XtraEditors.LabelControl labelControl11;
    }
}