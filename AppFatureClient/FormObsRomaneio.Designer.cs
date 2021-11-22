namespace AppFatureClient
{
    partial class FormObsRomaneio
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiro1 = new AppLib.Windows.CampoInteiro();
            this.campoInteiro2 = new AppLib.Windows.CampoInteiro();
            this.campoData1 = new AppLib.Windows.CampoData();
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
            this.tabControl1.Size = new System.Drawing.Size(493, 249);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoData1);
            this.tabPage1.Controls.Add(this.campoInteiro2);
            this.tabPage1.Controls.Add(this.campoInteiro1);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(485, 223);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "OBS";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(9, 45);
            this.campoTexto1.MaximoCaracteres = 255;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(458, 20);
            this.campoTexto1.Tabela = "ZROMANEIO";
            this.campoTexto1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Observação";
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "IDROMANEIO";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Edita = true;
            this.campoInteiro1.Location = new System.Drawing.Point(367, 84);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro1.Tabela = "ZROMANEIO";
            this.campoInteiro1.TabIndex = 2;
            // 
            // campoInteiro2
            // 
            this.campoInteiro2.Campo = "IDPROGRAMACAOCARREGAMENTO";
            this.campoInteiro2.Default = null;
            this.campoInteiro2.Edita = true;
            this.campoInteiro2.Location = new System.Drawing.Point(367, 110);
            this.campoInteiro2.Name = "campoInteiro2";
            this.campoInteiro2.Query = 0;
            this.campoInteiro2.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro2.Tabela = "ZROMANEIO";
            this.campoInteiro2.TabIndex = 3;
            // 
            // campoData1
            // 
            this.campoData1.Campo = "DATAAGENDAMENTO";
            this.campoData1.Default = null;
            this.campoData1.Edita = true;
            this.campoData1.Location = new System.Drawing.Point(367, 137);
            this.campoData1.Name = "campoData1";
            this.campoData1.Query = 0;
            this.campoData1.Size = new System.Drawing.Size(100, 21);
            this.campoData1.Tabela = "ZROMANEIO";
            this.campoData1.TabIndex = 4;
            // 
            // FormObsRomaneio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BarraNavegacao = false;
            this.BotaoExcluir = false;
            this.BotaoNovo = false;
            this.BotaoSalvarComo = false;
            this.ClientSize = new System.Drawing.Size(493, 349);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormObsRomaneio";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT * FROM ZROMANEIO WHERE IDROMANEIO = ?"};
            query1.Parametros = new string[] {
        "IDROMANEIO"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZROMANEIO";
            this.Text = "Cadastro de Observação do Romaneio";
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
        private AppLib.Windows.CampoTexto campoTexto1;
        private AppLib.Windows.CampoInteiro campoInteiro1;
        private AppLib.Windows.CampoInteiro campoInteiro2;
        private AppLib.Windows.CampoData campoData1;
    }
}