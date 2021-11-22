namespace AppLib.Windows
{
    partial class FormAlteraItemComposicao
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlteraItemComposicao));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNOVO = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEXCLUIR = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPRIMEIRO = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonANTERIOR = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPROXIMO = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonULTIMO = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonANEXOS = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripButtonPROCESSOS = new System.Windows.Forms.ToolStripSplitButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButtonSALVAR = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCANCELAR = new DevExpress.XtraEditors.SimpleButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.decimalQuantidade = new AppLib.Windows.CampoDecimal();
            this.label1 = new System.Windows.Forms.Label();
            this.clProduto = new AppLib.Windows.CampoLookup();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNOVO,
            this.toolStripButtonEXCLUIR,
            this.toolStripSeparator1,
            this.toolStripButtonPRIMEIRO,
            this.toolStripButtonANTERIOR,
            this.toolStripButtonPROXIMO,
            this.toolStripButtonULTIMO,
            this.toolStripSeparator2,
            this.toolStripButtonANEXOS,
            this.toolStripButtonPROCESSOS});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(782, 39);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonNOVO
            // 
            this.toolStripButtonNOVO.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNOVO.Image")));
            this.toolStripButtonNOVO.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonNOVO.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNOVO.Name = "toolStripButtonNOVO";
            this.toolStripButtonNOVO.Size = new System.Drawing.Size(62, 36);
            this.toolStripButtonNOVO.Text = "Novo";
            this.toolStripButtonNOVO.Click += new System.EventHandler(this.toolStripButtonNOVO_Click);
            // 
            // toolStripButtonEXCLUIR
            // 
            this.toolStripButtonEXCLUIR.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEXCLUIR.Image")));
            this.toolStripButtonEXCLUIR.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonEXCLUIR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEXCLUIR.Name = "toolStripButtonEXCLUIR";
            this.toolStripButtonEXCLUIR.Size = new System.Drawing.Size(70, 36);
            this.toolStripButtonEXCLUIR.Text = "Excluir";
            this.toolStripButtonEXCLUIR.Click += new System.EventHandler(this.toolStripButtonEXCLUIR_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButtonPRIMEIRO
            // 
            this.toolStripButtonPRIMEIRO.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPRIMEIRO.Image")));
            this.toolStripButtonPRIMEIRO.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonPRIMEIRO.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPRIMEIRO.Name = "toolStripButtonPRIMEIRO";
            this.toolStripButtonPRIMEIRO.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonPRIMEIRO.Click += new System.EventHandler(this.toolStripButtonPRIMEIRO_Click);
            // 
            // toolStripButtonANTERIOR
            // 
            this.toolStripButtonANTERIOR.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonANTERIOR.Image")));
            this.toolStripButtonANTERIOR.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonANTERIOR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonANTERIOR.Name = "toolStripButtonANTERIOR";
            this.toolStripButtonANTERIOR.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonANTERIOR.Click += new System.EventHandler(this.toolStripButtonANTERIOR_Click);
            // 
            // toolStripButtonPROXIMO
            // 
            this.toolStripButtonPROXIMO.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPROXIMO.Image")));
            this.toolStripButtonPROXIMO.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonPROXIMO.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPROXIMO.Name = "toolStripButtonPROXIMO";
            this.toolStripButtonPROXIMO.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonPROXIMO.Click += new System.EventHandler(this.toolStripButtonPROXIMO_Click);
            // 
            // toolStripButtonULTIMO
            // 
            this.toolStripButtonULTIMO.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonULTIMO.Image")));
            this.toolStripButtonULTIMO.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonULTIMO.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonULTIMO.Name = "toolStripButtonULTIMO";
            this.toolStripButtonULTIMO.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonULTIMO.Click += new System.EventHandler(this.toolStripButtonULTIMO_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButtonANEXOS
            // 
            this.toolStripButtonANEXOS.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonANEXOS.Image")));
            this.toolStripButtonANEXOS.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonANEXOS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonANEXOS.Name = "toolStripButtonANEXOS";
            this.toolStripButtonANEXOS.Size = new System.Drawing.Size(86, 36);
            this.toolStripButtonANEXOS.Text = "Anexos";
            // 
            // toolStripButtonPROCESSOS
            // 
            this.toolStripButtonPROCESSOS.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPROCESSOS.Image")));
            this.toolStripButtonPROCESSOS.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonPROCESSOS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPROCESSOS.Name = "toolStripButtonPROCESSOS";
            this.toolStripButtonPROCESSOS.Size = new System.Drawing.Size(99, 36);
            this.toolStripButtonPROCESSOS.Text = "Processos";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButtonSALVAR);
            this.panel1.Controls.Add(this.simpleButtonOK);
            this.panel1.Controls.Add(this.simpleButtonCANCELAR);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 136);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 61);
            this.panel1.TabIndex = 11;
            // 
            // simpleButtonSALVAR
            // 
            this.simpleButtonSALVAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonSALVAR.Location = new System.Drawing.Point(505, 18);
            this.simpleButtonSALVAR.Name = "simpleButtonSALVAR";
            this.simpleButtonSALVAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonSALVAR.TabIndex = 1;
            this.simpleButtonSALVAR.Text = "Salvar";
            this.simpleButtonSALVAR.Visible = false;
            this.simpleButtonSALVAR.Click += new System.EventHandler(this.simpleButtonSALVAR_Click);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK.Location = new System.Drawing.Point(595, 18);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCANCELAR
            // 
            this.simpleButtonCANCELAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR.Location = new System.Drawing.Point(685, 18);
            this.simpleButtonCANCELAR.Name = "simpleButtonCANCELAR";
            this.simpleButtonCANCELAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR.TabIndex = 3;
            this.simpleButtonCANCELAR.Text = "Cancelar";
            this.simpleButtonCANCELAR.Click += new System.EventHandler(this.simpleButtonCANCELAR_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.decimalQuantidade);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.clProduto);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(774, 71);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(663, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Quantidade";
            // 
            // decimalQuantidade
            // 
            this.decimalQuantidade.Campo = null;
            this.decimalQuantidade.Decimais = 2;
            this.decimalQuantidade.Default = null;
            this.decimalQuantidade.Edita = true;
            this.decimalQuantidade.Location = new System.Drawing.Point(666, 28);
            this.decimalQuantidade.Name = "decimalQuantidade";
            this.decimalQuantidade.Query = 0;
            this.decimalQuantidade.Size = new System.Drawing.Size(100, 20);
            this.decimalQuantidade.Tabela = null;
            this.decimalQuantidade.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Produto";
            // 
            // clProduto
            // 
            this.clProduto.Campo = null;
            this.clProduto.ColunaCodigo = "CODIGOPRD";
            this.clProduto.ColunaDescricao = "NOMEFANTASIA";
            this.clProduto.ColunaIdentificador = null;
            this.clProduto.ColunaTabela = null;
            this.clProduto.Conexao = "Start";
            this.clProduto.Default = null;
            this.clProduto.Edita = true;
            this.clProduto.EditaLookup = false;
            this.clProduto.Location = new System.Drawing.Point(20, 28);
            this.clProduto.MaximoCaracteres = null;
            this.clProduto.Name = "clProduto";
            this.clProduto.NomeGrid = null;
            this.clProduto.Query = 0;
            this.clProduto.Size = new System.Drawing.Size(641, 21);
            this.clProduto.Tabela = null;
            this.clProduto.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(782, 97);
            this.tabControl1.TabIndex = 12;
            // 
            // FormAlteraItemComposicao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 197);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAlteraItemComposicao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de ADC";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCadastroObject_FormClosed);
            this.Load += new System.EventHandler(this.FormCadastro2_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonNOVO;
        private System.Windows.Forms.ToolStripButton toolStripButtonEXCLUIR;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripSplitButton toolStripButtonANEXOS;
        public System.Windows.Forms.ToolStripSplitButton toolStripButtonPROCESSOS;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCANCELAR;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSALVAR;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonPRIMEIRO;
        private System.Windows.Forms.ToolStripButton toolStripButtonANTERIOR;
        private System.Windows.Forms.ToolStripButton toolStripButtonPROXIMO;
        private System.Windows.Forms.ToolStripButton toolStripButtonULTIMO;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label2;
        private CampoDecimal decimalQuantidade;
        private System.Windows.Forms.Label label1;
        private CampoLookup clProduto;
    }
}