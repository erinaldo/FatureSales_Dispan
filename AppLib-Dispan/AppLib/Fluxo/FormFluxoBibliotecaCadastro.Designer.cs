namespace AppLib.Fluxo
{
    partial class FormFluxoBibliotecaCadastro
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto2 = new AppLib.Windows.CampoTexto();
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
            this.tabControl1.Size = new System.Drawing.Size(579, 239);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoTexto2);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(571, 213);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(32, 93);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(109, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Assembly (arquivo .dll)";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(32, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Nome";
            // 
            // campoTexto2
            // 
            this.campoTexto2.Campo = "ASSEMBLY";
            this.campoTexto2.Default = null;
            this.campoTexto2.Edita = true;
            this.campoTexto2.Location = new System.Drawing.Point(32, 113);
            this.campoTexto2.MaximoCaracteres = null;
            this.campoTexto2.Name = "campoTexto2";
            this.campoTexto2.Query = 0;
            this.campoTexto2.Size = new System.Drawing.Size(485, 20);
            this.campoTexto2.Tabela = "ZFLUXOBIBLIOTECA";
            this.campoTexto2.TabIndex = 1;
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "NOME";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(32, 45);
            this.campoTexto1.MaximoCaracteres = null;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(485, 20);
            this.campoTexto1.Tabela = "ZFLUXOBIBLIOTECA";
            this.campoTexto1.TabIndex = 0;
            // 
            // FormFluxoBibliotecaCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 339);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormFluxoBibliotecaCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZFLUXOBIBLIOTECA",
        "WHERE NOME = ?"};
            query1.Parametros = new string[] {
        "NOME"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZFLUXOBIBLIOTECA";
            this.Text = "Cadastro de Biblioteca";
            this.Load += new System.EventHandler(this.FormFluxoBibliotecaCadastro_Load);
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
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Windows.CampoTexto campoTexto2;
        private Windows.CampoTexto campoTexto1;
    }
}