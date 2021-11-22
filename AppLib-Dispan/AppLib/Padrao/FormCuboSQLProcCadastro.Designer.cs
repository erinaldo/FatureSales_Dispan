namespace AppLib.Padrao
{
    partial class FormCuboSQLProcCadastro
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
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookup1 = new AppLib.Windows.CampoLookup();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiro3 = new AppLib.Windows.CampoInteiro();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroIDCUBO = new AppLib.Windows.CampoInteiro();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
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
            this.tabControl1.Size = new System.Drawing.Size(728, 308);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.campoLookup1);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.campoInteiro3);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.campoInteiroIDCUBO);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoInteiro1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(720, 282);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificador";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(28, 83);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(49, 13);
            this.labelControl5.TabIndex = 20;
            this.labelControl5.Text = "Procedure";
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "NOMEPROCEDURE";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(28, 102);
            this.campoTexto1.MaximoCaracteres = null;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(206, 20);
            this.campoTexto1.Tabela = "ZCUBOSQLPROC";
            this.campoTexto1.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(28, 200);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(50, 13);
            this.labelControl4.TabIndex = 18;
            this.labelControl4.Text = "Parâmetro";
            // 
            // campoLookup1
            // 
            this.campoLookup1.Campo = "IDCUBOPARAM";
            this.campoLookup1.ColunaCodigo = "IDCUBOPARAM";
            this.campoLookup1.ColunaDescricao = "ORIGDIGTEXTO";
            this.campoLookup1.ColunaIdentificador = null;
            this.campoLookup1.ColunaTabela = "ZCUBOPARAM";
            this.campoLookup1.Conexao = "Start";
            this.campoLookup1.Default = null;
            this.campoLookup1.Edita = true;
            this.campoLookup1.Location = new System.Drawing.Point(28, 219);
            this.campoLookup1.MaximoCaracteres = null;
            this.campoLookup1.Name = "campoLookup1";
            this.campoLookup1.NomeGrid = "ZCUBOSQLPAR";
            this.campoLookup1.Query = 0;
            this.campoLookup1.Size = new System.Drawing.Size(591, 21);
            this.campoLookup1.Tabela = "ZCUBOSQLPROC";
            this.campoLookup1.TabIndex = 4;
            this.campoLookup1.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookup1_SetFormConsulta);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(28, 141);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(51, 13);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Sequêncial";
            // 
            // campoInteiro3
            // 
            this.campoInteiro3.Campo = "PARAMETRO";
            this.campoInteiro3.Default = null;
            this.campoInteiro3.Edita = true;
            this.campoInteiro3.Location = new System.Drawing.Point(28, 160);
            this.campoInteiro3.Name = "campoInteiro3";
            this.campoInteiro3.Query = 0;
            this.campoInteiro3.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro3.Tabela = "ZCUBOSQLPROC";
            this.campoInteiro3.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(134, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(39, 13);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "IDCUBO";
            this.labelControl2.Visible = false;
            // 
            // campoInteiroIDCUBO
            // 
            this.campoInteiroIDCUBO.Campo = "IDCUBO";
            this.campoInteiroIDCUBO.Default = null;
            this.campoInteiroIDCUBO.Edita = true;
            this.campoInteiroIDCUBO.Location = new System.Drawing.Point(134, 45);
            this.campoInteiroIDCUBO.Name = "campoInteiroIDCUBO";
            this.campoInteiroIDCUBO.Query = 0;
            this.campoInteiroIDCUBO.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroIDCUBO.Tabela = "ZCUBOSQLPROC";
            this.campoInteiroIDCUBO.TabIndex = 1;
            this.campoInteiroIDCUBO.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "Identificador";
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "IDCUBOSQLPROC";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Edita = false;
            this.campoInteiro1.Location = new System.Drawing.Point(28, 45);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro1.Tabela = "ZCUBOSQLPROC";
            this.campoInteiro1.TabIndex = 0;
            // 
            // FormCuboSQLProcCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 408);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormCuboSQLProcCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZCUBOSQLPROC",
        "WHERE IDCUBOSQLPROC = ?"};
            query1.Parametros = new string[] {
        "IDCUBOSQLPROC"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZCUBOSQLPROC";
            this.Text = "Cadastro de Procedure / Parâmetro";
            this.Load += new System.EventHandler(this.FormCuboSQLProcCadastro_Load);
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
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Windows.CampoLookup campoLookup1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Windows.CampoInteiro campoInteiro3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Windows.CampoInteiro campoInteiroIDCUBO;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Windows.CampoInteiro campoInteiro1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Windows.CampoTexto campoTexto1;
    }
}