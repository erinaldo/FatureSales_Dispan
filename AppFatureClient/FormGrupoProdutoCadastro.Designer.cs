namespace AppFatureClient
{
    partial class FormGrupoProdutoCadastro
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
            this.campoInteiroCODCOLIGADA = new AppLib.Windows.CampoInteiro();
            this.label23 = new System.Windows.Forms.Label();
            this.campoTextoNOME = new AppLib.Windows.CampoTexto();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.campoTextoCODTRA = new AppLib.Windows.CampoTexto();
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
            this.tabControl1.Size = new System.Drawing.Size(565, 209);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoInteiroCODCOLIGADA);
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.campoTextoNOME);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.campoTextoCODTRA);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(557, 183);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // campoInteiroCODCOLIGADA
            // 
            this.campoInteiroCODCOLIGADA.Campo = "CODCOLIGADA";
            this.campoInteiroCODCOLIGADA.Default = null;
            this.campoInteiroCODCOLIGADA.Location = new System.Drawing.Point(225, 29);
            this.campoInteiroCODCOLIGADA.Name = "campoInteiroCODCOLIGADA";
            this.campoInteiroCODCOLIGADA.Query = 0;
            this.campoInteiroCODCOLIGADA.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroCODCOLIGADA.Tabela = "TTB3";
            this.campoInteiroCODCOLIGADA.TabIndex = 23;
            this.campoInteiroCODCOLIGADA.Visible = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(222, 12);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(94, 13);
            this.label23.TabIndex = 27;
            this.label23.Text = "Coligada (invisivel)";
            this.label23.Visible = false;
            // 
            // campoTextoNOME
            // 
            this.campoTextoNOME.Campo = "DESCRICAO";
            this.campoTextoNOME.Default = null;
            this.campoTextoNOME.Location = new System.Drawing.Point(20, 68);
            this.campoTextoNOME.MaximoCaracteres = 100;
            this.campoTextoNOME.Name = "campoTextoNOME";
            this.campoTextoNOME.Query = 0;
            this.campoTextoNOME.Size = new System.Drawing.Size(305, 20);
            this.campoTextoNOME.Tabela = "TTB3";
            this.campoTextoNOME.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Descrição";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Código";
            // 
            // campoTextoCODTRA
            // 
            this.campoTextoCODTRA.Campo = "CODTB3FAT";
            this.campoTextoCODTRA.Default = null;
            this.campoTextoCODTRA.Location = new System.Drawing.Point(20, 29);
            this.campoTextoCODTRA.MaximoCaracteres = 10;
            this.campoTextoCODTRA.Name = "campoTextoCODTRA";
            this.campoTextoCODTRA.Query = 0;
            this.campoTextoCODTRA.Size = new System.Drawing.Size(100, 20);
            this.campoTextoCODTRA.Tabela = "TTB3";
            this.campoTextoCODTRA.TabIndex = 22;
            // 
            // FormGrupoProdutoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BotaoExcluir = true;
            this.BotaoNovo = true;
            this.ClientSize = new System.Drawing.Size(565, 309);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormGrupoProdutoCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM TTB3",
        "WHERE CODCOLIGADA =?",
        "  AND CODTB3FAT = ?"};
            query1.Parametros = new string[] {
        "CODCOLIGADA",
        "CODTB3FAT"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "TTB3";
            this.Text = "Cadastro Grupo de Produto";
            this.ValidarSalvar += new AppLib.Windows.FormCadastroData.ValidarSalvarHandler(this.FormGrupoProdutoCadastro_Validar);
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormGrupoProdutoCadastro_AntesSalvar);
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
        private AppLib.Windows.CampoInteiro campoInteiroCODCOLIGADA;
        private System.Windows.Forms.Label label23;
        private AppLib.Windows.CampoTexto campoTextoNOME;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoTexto campoTextoCODTRA;
    }
}