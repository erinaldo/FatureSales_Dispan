namespace AppLib.Padrao
{
    partial class FormExcelSQLProcCadastro
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
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoCELULACOLUNA = new AppLib.Windows.CampoTexto();
            this.campoTextoCELULAABA = new AppLib.Windows.CampoTexto();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiro3 = new AppLib.Windows.CampoInteiro();
            this.campoInteiro1 = new AppLib.Windows.CampoInteiro();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiro4 = new AppLib.Windows.CampoInteiro();
            this.campoTexto2 = new AppLib.Windows.CampoTexto();
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
            this.tabControl1.Size = new System.Drawing.Size(669, 376);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoTexto2);
            this.tabPage1.Controls.Add(this.labelControl7);
            this.tabPage1.Controls.Add(this.campoInteiro4);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.campoTextoCELULACOLUNA);
            this.tabPage1.Controls.Add(this.campoTextoCELULAABA);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoInteiro3);
            this.tabPage1.Controls.Add(this.campoInteiro1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(661, 350);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "NOMEPROCEDURE";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(22, 90);
            this.campoTexto1.MaximoCaracteres = null;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(560, 20);
            this.campoTexto1.Tabela = "ZPLANILHASQLPROC";
            this.campoTexto1.TabIndex = 2;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(22, 71);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(94, 13);
            this.labelControl6.TabIndex = 22;
            this.labelControl6.Text = "Nome da Procedure";
            // 
            // campoTextoCELULACOLUNA
            // 
            this.campoTextoCELULACOLUNA.Campo = "CELULACOLUNA";
            this.campoTextoCELULACOLUNA.Default = null;
            this.campoTextoCELULACOLUNA.Edita = true;
            this.campoTextoCELULACOLUNA.Location = new System.Drawing.Point(22, 244);
            this.campoTextoCELULACOLUNA.MaximoCaracteres = null;
            this.campoTextoCELULACOLUNA.Name = "campoTextoCELULACOLUNA";
            this.campoTextoCELULACOLUNA.Query = 0;
            this.campoTextoCELULACOLUNA.Size = new System.Drawing.Size(100, 20);
            this.campoTextoCELULACOLUNA.Tabela = "ZPLANILHASQLPROC";
            this.campoTextoCELULACOLUNA.TabIndex = 5;
            // 
            // campoTextoCELULAABA
            // 
            this.campoTextoCELULAABA.Campo = "CELULAABA";
            this.campoTextoCELULAABA.Default = null;
            this.campoTextoCELULAABA.Edita = true;
            this.campoTextoCELULAABA.Location = new System.Drawing.Point(22, 195);
            this.campoTextoCELULAABA.MaximoCaracteres = null;
            this.campoTextoCELULAABA.Name = "campoTextoCELULAABA";
            this.campoTextoCELULAABA.Query = 0;
            this.campoTextoCELULAABA.Size = new System.Drawing.Size(560, 20);
            this.campoTextoCELULAABA.Tabela = "ZPLANILHASQLPROC";
            this.campoTextoCELULAABA.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(22, 273);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(25, 13);
            this.labelControl5.TabIndex = 19;
            this.labelControl5.Text = "Linha";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(22, 225);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(33, 13);
            this.labelControl4.TabIndex = 18;
            this.labelControl4.Text = "Coluna";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(22, 176);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(19, 13);
            this.labelControl3.TabIndex = 17;
            this.labelControl3.Text = "Aba";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(22, 122);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "Parâmetro";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "Identificador";
            // 
            // campoInteiro3
            // 
            this.campoInteiro3.Campo = "CELULALINHA";
            this.campoInteiro3.Default = null;
            this.campoInteiro3.Edita = true;
            this.campoInteiro3.Location = new System.Drawing.Point(22, 292);
            this.campoInteiro3.Name = "campoInteiro3";
            this.campoInteiro3.Query = 0;
            this.campoInteiro3.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro3.Tabela = "ZPLANILHASQLPROC";
            this.campoInteiro3.TabIndex = 6;
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "IDPLANILHASQLPROC";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Edita = false;
            this.campoInteiro1.Location = new System.Drawing.Point(22, 40);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro1.Tabela = "ZPLANILHASQLPROC";
            this.campoInteiro1.TabIndex = 0;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(128, 21);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(101, 13);
            this.labelControl7.TabIndex = 24;
            this.labelControl7.Text = "IDPLANILHASQL (FK)";
            this.labelControl7.Visible = false;
            // 
            // campoInteiro4
            // 
            this.campoInteiro4.Campo = "IDPLANILHASQL";
            this.campoInteiro4.Default = null;
            this.campoInteiro4.Edita = false;
            this.campoInteiro4.Location = new System.Drawing.Point(128, 40);
            this.campoInteiro4.Name = "campoInteiro4";
            this.campoInteiro4.Query = 0;
            this.campoInteiro4.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro4.Tabela = "ZPLANILHASQLPROC";
            this.campoInteiro4.TabIndex = 1;
            this.campoInteiro4.Visible = false;
            // 
            // campoTexto2
            // 
            this.campoTexto2.Campo = "PARAMETRO";
            this.campoTexto2.Default = null;
            this.campoTexto2.Edita = true;
            this.campoTexto2.Location = new System.Drawing.Point(22, 141);
            this.campoTexto2.MaximoCaracteres = null;
            this.campoTexto2.Name = "campoTexto2";
            this.campoTexto2.Query = 0;
            this.campoTexto2.Size = new System.Drawing.Size(560, 20);
            this.campoTexto2.Tabela = "ZPLANILHASQLPROC";
            this.campoTexto2.TabIndex = 3;
            // 
            // FormExcelSQLProcCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BotaoExcluir = true;
            this.BotaoNovo = true;
            this.ClientSize = new System.Drawing.Size(669, 476);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormExcelSQLProcCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZPLANILHASQLPROC",
        "WHERE IDPLANILHASQLPROC = ?"};
            query1.Parametros = new string[] {
        "IDPLANILHASQLPROC"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZPLANILHASQLPROC";
            this.Text = "Cadastro de Procedure e Parâmetros";
            this.Load += new System.EventHandler(this.FormExcelSQLProcCadastro_Load);
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
        private Windows.CampoTexto campoTexto1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private Windows.CampoTexto campoTextoCELULACOLUNA;
        private Windows.CampoTexto campoTextoCELULAABA;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Windows.CampoInteiro campoInteiro3;
        private Windows.CampoInteiro campoInteiro1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private Windows.CampoInteiro campoInteiro4;
        private Windows.CampoTexto campoTexto2;
    }
}