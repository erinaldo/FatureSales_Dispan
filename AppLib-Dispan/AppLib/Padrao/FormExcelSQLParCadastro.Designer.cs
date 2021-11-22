namespace AppLib.Padrao
{
    partial class FormExcelSQLParCadastro
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
            this.campoTextoCELULACOLUNA = new AppLib.Windows.CampoTexto();
            this.campoTextoCELULAABA = new AppLib.Windows.CampoTexto();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiro3 = new AppLib.Windows.CampoInteiro();
            this.campoInteiro2 = new AppLib.Windows.CampoInteiro();
            this.campoInteiro1 = new AppLib.Windows.CampoInteiro();
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
            this.tabControl1.Size = new System.Drawing.Size(618, 326);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoTextoCELULACOLUNA);
            this.tabPage1.Controls.Add(this.campoTextoCELULAABA);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoInteiro3);
            this.tabPage1.Controls.Add(this.campoInteiro2);
            this.tabPage1.Controls.Add(this.campoInteiro1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(610, 300);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // campoTextoCELULACOLUNA
            // 
            this.campoTextoCELULACOLUNA.Campo = "CELULACOLUNA";
            this.campoTextoCELULACOLUNA.Default = null;
            this.campoTextoCELULACOLUNA.Edita = true;
            this.campoTextoCELULACOLUNA.Location = new System.Drawing.Point(19, 190);
            this.campoTextoCELULACOLUNA.MaximoCaracteres = null;
            this.campoTextoCELULACOLUNA.Name = "campoTextoCELULACOLUNA";
            this.campoTextoCELULACOLUNA.Query = 0;
            this.campoTextoCELULACOLUNA.Size = new System.Drawing.Size(100, 20);
            this.campoTextoCELULACOLUNA.Tabela = "ZPLANILHASQLPAR";
            this.campoTextoCELULACOLUNA.TabIndex = 3;
            // 
            // campoTextoCELULAABA
            // 
            this.campoTextoCELULAABA.Campo = "CELULAABA";
            this.campoTextoCELULAABA.Default = null;
            this.campoTextoCELULAABA.Edita = true;
            this.campoTextoCELULAABA.Location = new System.Drawing.Point(19, 141);
            this.campoTextoCELULAABA.MaximoCaracteres = null;
            this.campoTextoCELULAABA.Name = "campoTextoCELULAABA";
            this.campoTextoCELULAABA.Query = 0;
            this.campoTextoCELULAABA.Size = new System.Drawing.Size(560, 20);
            this.campoTextoCELULAABA.Tabela = "ZPLANILHASQLPAR";
            this.campoTextoCELULAABA.TabIndex = 2;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(19, 219);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(25, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Linha";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(19, 171);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(33, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Coluna";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(19, 122);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(19, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Aba";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(19, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Sequêncial";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Identificador";
            // 
            // campoInteiro3
            // 
            this.campoInteiro3.Campo = "CELULALINHA";
            this.campoInteiro3.Default = null;
            this.campoInteiro3.Edita = true;
            this.campoInteiro3.Location = new System.Drawing.Point(19, 238);
            this.campoInteiro3.Name = "campoInteiro3";
            this.campoInteiro3.Query = 0;
            this.campoInteiro3.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro3.Tabela = "ZPLANILHASQLPAR";
            this.campoInteiro3.TabIndex = 4;
            // 
            // campoInteiro2
            // 
            this.campoInteiro2.Campo = "PARAMETRO";
            this.campoInteiro2.Default = null;
            this.campoInteiro2.Edita = true;
            this.campoInteiro2.Location = new System.Drawing.Point(19, 87);
            this.campoInteiro2.Name = "campoInteiro2";
            this.campoInteiro2.Query = 0;
            this.campoInteiro2.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro2.Tabela = "ZPLANILHASQLPAR";
            this.campoInteiro2.TabIndex = 1;
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "IDPLANILHASQL";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Edita = false;
            this.campoInteiro1.Location = new System.Drawing.Point(19, 38);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro1.Tabela = "ZPLANILHASQLPAR";
            this.campoInteiro1.TabIndex = 0;
            // 
            // FormExcelSQLParCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BotaoExcluir = true;
            this.BotaoNovo = true;
            this.ClientSize = new System.Drawing.Size(618, 426);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormExcelSQLParCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZPLANILHASQLPAR",
        "WHERE IDPLANILHASQL = ?",
        " AND PARAMETRO = ?"};
            query1.Parametros = new string[] {
        "IDPLANILHASQL",
        "PARAMETRO"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZPLANILHASQLPAR";
            this.Text = "Configuração dos Parâmetros";
            this.Load += new System.EventHandler(this.FormExcelSQLPar_Load);
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
        private Windows.CampoInteiro campoInteiro3;
        private Windows.CampoInteiro campoInteiro2;
        private Windows.CampoInteiro campoInteiro1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Windows.CampoTexto campoTextoCELULACOLUNA;
        private Windows.CampoTexto campoTextoCELULAABA;
    }
}