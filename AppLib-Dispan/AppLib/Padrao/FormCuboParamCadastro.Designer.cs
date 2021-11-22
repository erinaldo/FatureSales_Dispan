namespace AppLib.Padrao
{
    partial class FormCuboParamCadastro
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
            this.campoTexto2 = new AppLib.Windows.CampoTexto();
            this.campoLista2 = new AppLib.Windows.CampoLista();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiro2 = new AppLib.Windows.CampoInteiro();
            this.campoInteiro1 = new AppLib.Windows.CampoInteiro();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
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
            this.tabControl1.Size = new System.Drawing.Size(620, 261);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.campoTexto2);
            this.tabPage1.Controls.Add(this.campoLista2);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.campoInteiro2);
            this.tabPage1.Controls.Add(this.campoInteiro1);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(612, 235);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(21, 131);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 13);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "Tipo de Valor";
            // 
            // campoTexto2
            // 
            this.campoTexto2.Campo = "ORIGDIGTEXTO";
            this.campoTexto2.Default = null;
            this.campoTexto2.Edita = true;
            this.campoTexto2.Location = new System.Drawing.Point(21, 95);
            this.campoTexto2.MaximoCaracteres = 100;
            this.campoTexto2.Name = "campoTexto2";
            this.campoTexto2.Query = 0;
            this.campoTexto2.Size = new System.Drawing.Size(491, 20);
            this.campoTexto2.Tabela = "ZCUBOPARAM";
            this.campoTexto2.TabIndex = 9;
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
            this.campoLista2.Location = new System.Drawing.Point(21, 150);
            this.campoLista2.Name = "campoLista2";
            this.campoLista2.Query = 0;
            this.campoLista2.Size = new System.Drawing.Size(206, 21);
            this.campoLista2.Tabela = "ZCUBOPARAM";
            this.campoLista2.TabIndex = 10;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(21, 76);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(28, 13);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "Texto";
            // 
            // campoInteiro2
            // 
            this.campoInteiro2.Campo = "IDCUBO";
            this.campoInteiro2.Default = null;
            this.campoInteiro2.Edita = false;
            this.campoInteiro2.Location = new System.Drawing.Point(127, 39);
            this.campoInteiro2.Name = "campoInteiro2";
            this.campoInteiro2.Query = 0;
            this.campoInteiro2.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro2.Tabela = "ZCUBOPARAM";
            this.campoInteiro2.TabIndex = 6;
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "IDCUBOPARAM";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Edita = false;
            this.campoInteiro1.Location = new System.Drawing.Point(21, 39);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro1.Tabela = "ZCUBOPARAM";
            this.campoInteiro1.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(127, 20);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(39, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "IDCUBO";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Identificador";
            // 
            // FormCuboParamCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 361);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormCuboParamCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZCUBOPARAM",
        "WHERE IDCUBOPARAM = ?"};
            query1.Parametros = new string[] {
        "IDCUBOPARAM"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZCUBOPARAM";
            this.Text = "Cadastro de Parâmetro Cubo";
            this.Load += new System.EventHandler(this.FormCuboParamCadastro_Load);
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
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Windows.CampoInteiro campoInteiro2;
        private Windows.CampoInteiro campoInteiro1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Windows.CampoTexto campoTexto2;
        private Windows.CampoLista campoLista2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}