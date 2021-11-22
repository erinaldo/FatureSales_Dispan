namespace AppLib.Padrao
{
    partial class FormMenuFluxoVisaoCadastro
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
            this.campoLookup2 = new AppLib.Windows.CampoLookup();
            this.campoLookup1 = new AppLib.Windows.CampoLookup();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
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
            this.tabControl1.Size = new System.Drawing.Size(718, 283);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoLookup2);
            this.tabPage1.Controls.Add(this.campoLookup1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(710, 257);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(39, 84);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(26, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Fluxo";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(39, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(26, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Menu";
            // 
            // campoLookup2
            // 
            this.campoLookup2.Campo = "FLUXO";
            this.campoLookup2.ColunaCodigo = "NOME";
            this.campoLookup2.ColunaDescricao = "DESCRICAO";
            this.campoLookup2.ColunaIdentificador = null;
            this.campoLookup2.ColunaTabela = "ZFLUXO";
            this.campoLookup2.Conexao = "Start";
            this.campoLookup2.Default = null;
            this.campoLookup2.Edita = true;
            this.campoLookup2.Location = new System.Drawing.Point(39, 104);
            this.campoLookup2.MaximoCaracteres = null;
            this.campoLookup2.Name = "campoLookup2";
            this.campoLookup2.NomeGrid = "ZFLUXO";
            this.campoLookup2.Query = 0;
            this.campoLookup2.Size = new System.Drawing.Size(595, 21);
            this.campoLookup2.Tabela = "ZMENUFLUXOVISAO";
            this.campoLookup2.TabIndex = 1;
            // 
            // campoLookup1
            // 
            this.campoLookup1.Campo = "CODMENU";
            this.campoLookup1.ColunaCodigo = "CODMENU";
            this.campoLookup1.ColunaDescricao = "NOME";
            this.campoLookup1.ColunaIdentificador = null;
            this.campoLookup1.ColunaTabela = "ZMENU";
            this.campoLookup1.Conexao = "Start";
            this.campoLookup1.Default = null;
            this.campoLookup1.Edita = false;
            this.campoLookup1.Location = new System.Drawing.Point(39, 45);
            this.campoLookup1.MaximoCaracteres = null;
            this.campoLookup1.Name = "campoLookup1";
            this.campoLookup1.NomeGrid = "ZMENU";
            this.campoLookup1.Query = 0;
            this.campoLookup1.Size = new System.Drawing.Size(595, 21);
            this.campoLookup1.Tabela = "ZMENUFLUXOVISAO";
            this.campoLookup1.TabIndex = 0;
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "NOME";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(39, 165);
            this.campoTexto1.MaximoCaracteres = null;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(595, 20);
            this.campoTexto1.Tabela = "ZMENUFLUXOVISAO";
            this.campoTexto1.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(39, 146);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(88, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Nome do Processo";
            // 
            // FormMenuFluxoVisaoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 383);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormMenuFluxoVisaoCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZMENUFLUXOVISAO",
        "WHERE CODMENU = ?",
        "  AND FLUXO = ?"};
            query1.Parametros = new string[] {
        "CODMENU",
        "FLUXO"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZMENUFLUXOVISAO";
            this.Text = "Cadastro de Fluxo de Visão";
            this.Load += new System.EventHandler(this.FormMenuFluxoVisaoCadastro_Load);
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
        private Windows.CampoLookup campoLookup2;
        private Windows.CampoLookup campoLookup1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Windows.CampoTexto campoTexto1;
    }
}