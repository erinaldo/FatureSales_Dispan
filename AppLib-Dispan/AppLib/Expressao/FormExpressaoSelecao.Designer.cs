namespace AppLib.Expressao
{
    partial class FormExpressaoSelecao
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoVALORFIXO = new AppLib.Windows.CampoTexto();
            this.panel3 = new System.Windows.Forms.Panel();
            this.simpleButtonCANCELAR_VALORFIXO = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK_VALORFIXO = new DevExpress.XtraEditors.SimpleButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookupVARIAVEL = new AppLib.Windows.CampoLookup();
            this.panel2 = new System.Windows.Forms.Panel();
            this.simpleButtonCANCELAR_VARIAVEL = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK_VARIAVEL = new DevExpress.XtraEditors.SimpleButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookupFLUXO = new AppLib.Windows.CampoLookup();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButtonCANCELAR_FLUXO = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK_FLUXO = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(427, 318);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoTextoVALORFIXO);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(419, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Valor Fixo";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Valor";
            // 
            // campoTextoVALORFIXO
            // 
            this.campoTextoVALORFIXO.Campo = null;
            this.campoTextoVALORFIXO.Default = null;
            this.campoTextoVALORFIXO.Edita = true;
            this.campoTextoVALORFIXO.Location = new System.Drawing.Point(24, 42);
            this.campoTextoVALORFIXO.MaximoCaracteres = null;
            this.campoTextoVALORFIXO.Name = "campoTextoVALORFIXO";
            this.campoTextoVALORFIXO.Query = 0;
            this.campoTextoVALORFIXO.Size = new System.Drawing.Size(369, 20);
            this.campoTextoVALORFIXO.Tabela = null;
            this.campoTextoVALORFIXO.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.simpleButtonCANCELAR_VALORFIXO);
            this.panel3.Controls.Add(this.simpleButtonOK_VALORFIXO);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 235);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(413, 54);
            this.panel3.TabIndex = 12;
            // 
            // simpleButtonCANCELAR_VALORFIXO
            // 
            this.simpleButtonCANCELAR_VALORFIXO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR_VALORFIXO.Location = new System.Drawing.Point(315, 15);
            this.simpleButtonCANCELAR_VALORFIXO.Name = "simpleButtonCANCELAR_VALORFIXO";
            this.simpleButtonCANCELAR_VALORFIXO.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR_VALORFIXO.TabIndex = 1;
            this.simpleButtonCANCELAR_VALORFIXO.Text = "Cancelar";
            this.simpleButtonCANCELAR_VALORFIXO.Click += new System.EventHandler(this.simpleButtonCANCELAR_VALORFIXO_Click);
            // 
            // simpleButtonOK_VALORFIXO
            // 
            this.simpleButtonOK_VALORFIXO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK_VALORFIXO.Location = new System.Drawing.Point(225, 15);
            this.simpleButtonOK_VALORFIXO.Name = "simpleButtonOK_VALORFIXO";
            this.simpleButtonOK_VALORFIXO.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK_VALORFIXO.TabIndex = 0;
            this.simpleButtonOK_VALORFIXO.Text = "OK";
            this.simpleButtonOK_VALORFIXO.Click += new System.EventHandler(this.simpleButtonOK_VALORFIXO_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labelControl2);
            this.tabPage2.Controls.Add(this.campoLookupVARIAVEL);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(419, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Variável";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(38, 13);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Variável";
            // 
            // campoLookupVARIAVEL
            // 
            this.campoLookupVARIAVEL.Campo = "";
            this.campoLookupVARIAVEL.ColunaCodigo = "VARIAVEL";
            this.campoLookupVARIAVEL.ColunaDescricao = "TIPODADO";
            this.campoLookupVARIAVEL.ColunaIdentificador = null;
            this.campoLookupVARIAVEL.ColunaTabela = "ZFLUXOVARIAVEL";
            this.campoLookupVARIAVEL.Conexao = "Start";
            this.campoLookupVARIAVEL.Default = null;
            this.campoLookupVARIAVEL.Edita = true;
            this.campoLookupVARIAVEL.Location = new System.Drawing.Point(24, 42);
            this.campoLookupVARIAVEL.MaximoCaracteres = null;
            this.campoLookupVARIAVEL.Name = "campoLookupVARIAVEL";
            this.campoLookupVARIAVEL.NomeGrid = null;
            this.campoLookupVARIAVEL.Query = 0;
            this.campoLookupVARIAVEL.Size = new System.Drawing.Size(369, 21);
            this.campoLookupVARIAVEL.Tabela = "";
            this.campoLookupVARIAVEL.TabIndex = 13;
            this.campoLookupVARIAVEL.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupVARIAVEL_SetFormConsulta);
            this.campoLookupVARIAVEL.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupVARIAVEL_SetDescricao);
            this.campoLookupVARIAVEL.AposSelecao += new AppLib.Windows.CampoLookup.AposSelecaoHandler(this.campoLookupVARIAVEL_AposSelecao);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.simpleButtonCANCELAR_VARIAVEL);
            this.panel2.Controls.Add(this.simpleButtonOK_VARIAVEL);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 235);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(413, 54);
            this.panel2.TabIndex = 12;
            // 
            // simpleButtonCANCELAR_VARIAVEL
            // 
            this.simpleButtonCANCELAR_VARIAVEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR_VARIAVEL.Location = new System.Drawing.Point(315, 15);
            this.simpleButtonCANCELAR_VARIAVEL.Name = "simpleButtonCANCELAR_VARIAVEL";
            this.simpleButtonCANCELAR_VARIAVEL.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR_VARIAVEL.TabIndex = 1;
            this.simpleButtonCANCELAR_VARIAVEL.Text = "Cancelar";
            this.simpleButtonCANCELAR_VARIAVEL.Click += new System.EventHandler(this.simpleButtonCANCELAR_VARIAVEL_Click);
            // 
            // simpleButtonOK_VARIAVEL
            // 
            this.simpleButtonOK_VARIAVEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK_VARIAVEL.Location = new System.Drawing.Point(225, 15);
            this.simpleButtonOK_VARIAVEL.Name = "simpleButtonOK_VARIAVEL";
            this.simpleButtonOK_VARIAVEL.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK_VARIAVEL.TabIndex = 0;
            this.simpleButtonOK_VARIAVEL.Text = "OK";
            this.simpleButtonOK_VARIAVEL.Click += new System.EventHandler(this.simpleButtonOK_VARIAVEL_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.labelControl5);
            this.tabPage3.Controls.Add(this.campoLookupFLUXO);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(419, 292);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Fluxo";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(24, 22);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(26, 13);
            this.labelControl5.TabIndex = 20;
            this.labelControl5.Text = "Fluxo";
            // 
            // campoLookupFLUXO
            // 
            this.campoLookupFLUXO.Campo = "";
            this.campoLookupFLUXO.ColunaCodigo = "NOME";
            this.campoLookupFLUXO.ColunaDescricao = "DESCRICAO";
            this.campoLookupFLUXO.ColunaIdentificador = null;
            this.campoLookupFLUXO.ColunaTabela = "ZFLUXO";
            this.campoLookupFLUXO.Conexao = "Start";
            this.campoLookupFLUXO.Default = null;
            this.campoLookupFLUXO.Edita = true;
            this.campoLookupFLUXO.Location = new System.Drawing.Point(24, 42);
            this.campoLookupFLUXO.MaximoCaracteres = null;
            this.campoLookupFLUXO.Name = "campoLookupFLUXO";
            this.campoLookupFLUXO.NomeGrid = null;
            this.campoLookupFLUXO.Query = 0;
            this.campoLookupFLUXO.Size = new System.Drawing.Size(369, 21);
            this.campoLookupFLUXO.Tabela = "";
            this.campoLookupFLUXO.TabIndex = 19;
            this.campoLookupFLUXO.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupFLUXO_SetFormConsulta);
            this.campoLookupFLUXO.AposSelecao += new AppLib.Windows.CampoLookup.AposSelecaoHandler(this.campoLookupFLUXO_AposSelecao);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.simpleButtonCANCELAR_FLUXO);
            this.panel1.Controls.Add(this.simpleButtonOK_FLUXO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 235);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 54);
            this.panel1.TabIndex = 11;
            // 
            // simpleButtonCANCELAR_FLUXO
            // 
            this.simpleButtonCANCELAR_FLUXO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR_FLUXO.Location = new System.Drawing.Point(315, 15);
            this.simpleButtonCANCELAR_FLUXO.Name = "simpleButtonCANCELAR_FLUXO";
            this.simpleButtonCANCELAR_FLUXO.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR_FLUXO.TabIndex = 1;
            this.simpleButtonCANCELAR_FLUXO.Text = "Cancelar";
            this.simpleButtonCANCELAR_FLUXO.Click += new System.EventHandler(this.simpleButtonCANCELAR_FLUXO_Click);
            // 
            // simpleButtonOK_FLUXO
            // 
            this.simpleButtonOK_FLUXO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK_FLUXO.Location = new System.Drawing.Point(225, 15);
            this.simpleButtonOK_FLUXO.Name = "simpleButtonOK_FLUXO";
            this.simpleButtonOK_FLUXO.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK_FLUXO.TabIndex = 0;
            this.simpleButtonOK_FLUXO.Text = "OK";
            this.simpleButtonOK_FLUXO.Click += new System.EventHandler(this.simpleButtonOK_FLUXO_Click);
            // 
            // FormExpressaoSelecao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 318);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormExpressaoSelecao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleção de Expressão";
            this.Load += new System.EventHandler(this.FormExpressaoSelecao_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCANCELAR_VALORFIXO;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK_VALORFIXO;
        public System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCANCELAR_VARIAVEL;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK_VARIAVEL;
        public System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCANCELAR_FLUXO;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK_FLUXO;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public Windows.CampoTexto campoTextoVALORFIXO;
        public Windows.CampoLookup campoLookupVARIAVEL;
        public Windows.CampoLookup campoLookupFLUXO;
    }
}