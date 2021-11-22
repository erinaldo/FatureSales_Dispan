namespace AppLib.Windows
{
    partial class FormRegraCFOPCadastro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegraCFOPCadastro));
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
            this.clRevenderSemST = new AppLib.Windows.CampoLookup();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.clConsumoSemST = new AppLib.Windows.CampoLookup();
            this.cbNaoContrinuinte = new DevExpress.XtraEditors.CheckEdit();
            this.clTipoProduto = new AppLib.Windows.CampoLookup();
            this.label7 = new System.Windows.Forms.Label();
            this.clNumeroCCF = new AppLib.Windows.CampoLookup();
            this.clAtivo = new AppLib.Windows.CampoLookup();
            this.clIndustrializar = new AppLib.Windows.CampoLookup();
            this.clRevender = new AppLib.Windows.CampoLookup();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.clConsumo = new AppLib.Windows.CampoLookup();
            this.label2 = new System.Windows.Forms.Label();
            this.clEstado = new AppLib.Windows.CampoLookup();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbNaoContrinuinte.Properties)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(518, 39);
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
            this.panel1.Location = new System.Drawing.Point(0, 442);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 49);
            this.panel1.TabIndex = 11;
            // 
            // simpleButtonSALVAR
            // 
            this.simpleButtonSALVAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonSALVAR.Location = new System.Drawing.Point(246, 14);
            this.simpleButtonSALVAR.Name = "simpleButtonSALVAR";
            this.simpleButtonSALVAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonSALVAR.TabIndex = 1;
            this.simpleButtonSALVAR.Text = "Salvar";
            this.simpleButtonSALVAR.Click += new System.EventHandler(this.simpleButtonSALVAR_Click);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK.Location = new System.Drawing.Point(336, 14);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCANCELAR
            // 
            this.simpleButtonCANCELAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR.Location = new System.Drawing.Point(426, 14);
            this.simpleButtonCANCELAR.Name = "simpleButtonCANCELAR";
            this.simpleButtonCANCELAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR.TabIndex = 3;
            this.simpleButtonCANCELAR.Text = "Cancelar";
            this.simpleButtonCANCELAR.Click += new System.EventHandler(this.simpleButtonCANCELAR_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.clRevenderSemST);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.clConsumoSemST);
            this.tabPage1.Controls.Add(this.cbNaoContrinuinte);
            this.tabPage1.Controls.Add(this.clTipoProduto);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.clNumeroCCF);
            this.tabPage1.Controls.Add(this.clAtivo);
            this.tabPage1.Controls.Add(this.clIndustrializar);
            this.tabPage1.Controls.Add(this.clRevender);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.clConsumo);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.clEstado);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(510, 377);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // clRevenderSemST
            // 
            this.clRevenderSemST.Campo = null;
            this.clRevenderSemST.ColunaCodigo = "CODNAT";
            this.clRevenderSemST.ColunaDescricao = "NOME";
            this.clRevenderSemST.ColunaIdentificador = null;
            this.clRevenderSemST.ColunaTabela = "(SELECT CODNAT, NOME FROM DCFOP WHERE CODCOLIGADA = 1 AND ATIVO = 1 AND TIPOMOVIM" +
    "ENTO = \'S\' and len(CODNAT) = 9) W";
            this.clRevenderSemST.Conexao = "Start";
            this.clRevenderSemST.Default = null;
            this.clRevenderSemST.Edita = true;
            this.clRevenderSemST.EditaLookup = false;
            this.clRevenderSemST.Location = new System.Drawing.Point(12, 270);
            this.clRevenderSemST.MaximoCaracteres = null;
            this.clRevenderSemST.Name = "clRevenderSemST";
            this.clRevenderSemST.NomeGrid = null;
            this.clRevenderSemST.Query = 0;
            this.clRevenderSemST.Size = new System.Drawing.Size(486, 21);
            this.clRevenderSemST.Tabela = null;
            this.clRevenderSemST.TabIndex = 24;
            this.clRevenderSemST.Leave += new System.EventHandler(this.clRevenderSemST_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 253);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Revender Sem ST";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Uso/Consumo Sem ST";
            // 
            // clConsumoSemST
            // 
            this.clConsumoSemST.Campo = null;
            this.clConsumoSemST.ColunaCodigo = "CODNAT";
            this.clConsumoSemST.ColunaDescricao = "NOME";
            this.clConsumoSemST.ColunaIdentificador = null;
            this.clConsumoSemST.ColunaTabela = "(SELECT CODNAT, NOME FROM DCFOP WHERE CODCOLIGADA = 1 AND ATIVO = 1 AND TIPOMOVIM" +
    "ENTO = \'S\' and len(CODNAT) = 9) W";
            this.clConsumoSemST.Conexao = "Start";
            this.clConsumoSemST.Default = null;
            this.clConsumoSemST.Edita = true;
            this.clConsumoSemST.EditaLookup = false;
            this.clConsumoSemST.Location = new System.Drawing.Point(12, 189);
            this.clConsumoSemST.MaximoCaracteres = null;
            this.clConsumoSemST.Name = "clConsumoSemST";
            this.clConsumoSemST.NomeGrid = null;
            this.clConsumoSemST.Query = 0;
            this.clConsumoSemST.Size = new System.Drawing.Size(486, 21);
            this.clConsumoSemST.Tabela = null;
            this.clConsumoSemST.TabIndex = 21;
            this.clConsumoSemST.Leave += new System.EventHandler(this.clConsumoSemST_Leave);
            // 
            // cbNaoContrinuinte
            // 
            this.cbNaoContrinuinte.Location = new System.Drawing.Point(393, 30);
            this.cbNaoContrinuinte.Name = "cbNaoContrinuinte";
            this.cbNaoContrinuinte.Properties.Caption = "Não Contribuinte";
            this.cbNaoContrinuinte.Size = new System.Drawing.Size(105, 19);
            this.cbNaoContrinuinte.TabIndex = 20;
            this.cbNaoContrinuinte.CheckedChanged += new System.EventHandler(this.cbNaoContrinuinte_CheckedChanged);
            // 
            // clTipoProduto
            // 
            this.clTipoProduto.Campo = null;
            this.clTipoProduto.ColunaCodigo = "CODTB2FAT";
            this.clTipoProduto.ColunaDescricao = "DESCRICAO";
            this.clTipoProduto.ColunaIdentificador = null;
            this.clTipoProduto.ColunaTabela = "(select CODTB2FAT, DESCRICAO from TTB2 where CODCOLIGADA = 1) W";
            this.clTipoProduto.Conexao = "Start";
            this.clTipoProduto.Default = null;
            this.clTipoProduto.Edita = true;
            this.clTipoProduto.EditaLookup = false;
            this.clTipoProduto.Location = new System.Drawing.Point(11, 109);
            this.clTipoProduto.MaximoCaracteres = null;
            this.clTipoProduto.Name = "clTipoProduto";
            this.clTipoProduto.NomeGrid = null;
            this.clTipoProduto.Query = 0;
            this.clTipoProduto.Size = new System.Drawing.Size(486, 21);
            this.clTipoProduto.Tabela = null;
            this.clTipoProduto.TabIndex = 19;
            this.clTipoProduto.Leave += new System.EventHandler(this.clTipoProduto_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Tipo produto";
            // 
            // clNumeroCCF
            // 
            this.clNumeroCCF.Campo = null;
            this.clNumeroCCF.ColunaCodigo = "NCM";
            this.clNumeroCCF.ColunaDescricao = "NCM";
            this.clNumeroCCF.ColunaIdentificador = null;
            this.clNumeroCCF.ColunaTabela = "(SELECT DISTINCT(NUMEROCCF) NCM FROM TPRODUTO WHERE CODCOLPRD = 1) W";
            this.clNumeroCCF.Conexao = "Start";
            this.clNumeroCCF.Default = null;
            this.clNumeroCCF.Edita = true;
            this.clNumeroCCF.EditaLookup = false;
            this.clNumeroCCF.Location = new System.Drawing.Point(11, 69);
            this.clNumeroCCF.MaximoCaracteres = null;
            this.clNumeroCCF.Name = "clNumeroCCF";
            this.clNumeroCCF.NomeGrid = null;
            this.clNumeroCCF.Query = 0;
            this.clNumeroCCF.Size = new System.Drawing.Size(486, 21);
            this.clNumeroCCF.Tabela = null;
            this.clNumeroCCF.TabIndex = 17;
            this.clNumeroCCF.Leave += new System.EventHandler(this.clNumeroCCF_Leave);
            // 
            // clAtivo
            // 
            this.clAtivo.Campo = null;
            this.clAtivo.ColunaCodigo = "CODNAT";
            this.clAtivo.ColunaDescricao = "NOME";
            this.clAtivo.ColunaIdentificador = null;
            this.clAtivo.ColunaTabela = "(SELECT CODNAT, NOME FROM DCFOP WHERE CODCOLIGADA = 1 AND ATIVO = 1 AND TIPOMOVIM" +
    "ENTO = \'S\' and len(CODNAT) = 9) W";
            this.clAtivo.Conexao = "Start";
            this.clAtivo.Default = null;
            this.clAtivo.Edita = true;
            this.clAtivo.EditaLookup = false;
            this.clAtivo.Location = new System.Drawing.Point(12, 350);
            this.clAtivo.MaximoCaracteres = null;
            this.clAtivo.Name = "clAtivo";
            this.clAtivo.NomeGrid = null;
            this.clAtivo.Query = 0;
            this.clAtivo.Size = new System.Drawing.Size(486, 21);
            this.clAtivo.Tabela = null;
            this.clAtivo.TabIndex = 16;
            this.clAtivo.Leave += new System.EventHandler(this.clAtivo_Leave);
            // 
            // clIndustrializar
            // 
            this.clIndustrializar.Campo = null;
            this.clIndustrializar.ColunaCodigo = "CODNAT";
            this.clIndustrializar.ColunaDescricao = "NOME";
            this.clIndustrializar.ColunaIdentificador = null;
            this.clIndustrializar.ColunaTabela = "(SELECT CODNAT, NOME FROM DCFOP WHERE CODCOLIGADA = 1 AND ATIVO = 1 AND TIPOMOVIM" +
    "ENTO = \'S\' and len(CODNAT) = 9) W";
            this.clIndustrializar.Conexao = "Start";
            this.clIndustrializar.Default = null;
            this.clIndustrializar.Edita = true;
            this.clIndustrializar.EditaLookup = false;
            this.clIndustrializar.Location = new System.Drawing.Point(12, 310);
            this.clIndustrializar.MaximoCaracteres = null;
            this.clIndustrializar.Name = "clIndustrializar";
            this.clIndustrializar.NomeGrid = null;
            this.clIndustrializar.Query = 0;
            this.clIndustrializar.Size = new System.Drawing.Size(486, 21);
            this.clIndustrializar.Tabela = null;
            this.clIndustrializar.TabIndex = 15;
            this.clIndustrializar.Leave += new System.EventHandler(this.clIndustrializar_Leave);
            // 
            // clRevender
            // 
            this.clRevender.Campo = null;
            this.clRevender.ColunaCodigo = "CODNAT";
            this.clRevender.ColunaDescricao = "NOME";
            this.clRevender.ColunaIdentificador = null;
            this.clRevender.ColunaTabela = "(SELECT CODNAT, NOME FROM DCFOP WHERE CODCOLIGADA = 1 AND ATIVO = 1 AND TIPOMOVIM" +
    "ENTO = \'S\' and len(CODNAT) = 9) W";
            this.clRevender.Conexao = "Start";
            this.clRevender.Default = null;
            this.clRevender.Edita = true;
            this.clRevender.EditaLookup = false;
            this.clRevender.Location = new System.Drawing.Point(12, 229);
            this.clRevender.MaximoCaracteres = null;
            this.clRevender.Name = "clRevender";
            this.clRevender.NomeGrid = null;
            this.clRevender.Query = 0;
            this.clRevender.Size = new System.Drawing.Size(486, 21);
            this.clRevender.Tabela = null;
            this.clRevender.TabIndex = 14;
            this.clRevender.Leave += new System.EventHandler(this.clRevender_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Ativo Imobilizado";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 294);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Industrializar";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Revender";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Uso/Consumo";
            // 
            // clConsumo
            // 
            this.clConsumo.Campo = null;
            this.clConsumo.ColunaCodigo = "CODNAT";
            this.clConsumo.ColunaDescricao = "NOME";
            this.clConsumo.ColunaIdentificador = null;
            this.clConsumo.ColunaTabela = "(SELECT CODNAT, NOME FROM DCFOP WHERE CODCOLIGADA = 1 AND ATIVO = 1 AND TIPOMOVIM" +
    "ENTO = \'S\' and len(CODNAT) = 9) W";
            this.clConsumo.Conexao = "Start";
            this.clConsumo.Default = null;
            this.clConsumo.Edita = true;
            this.clConsumo.EditaLookup = false;
            this.clConsumo.Location = new System.Drawing.Point(12, 149);
            this.clConsumo.MaximoCaracteres = null;
            this.clConsumo.Name = "clConsumo";
            this.clConsumo.NomeGrid = null;
            this.clConsumo.Query = 0;
            this.clConsumo.Size = new System.Drawing.Size(486, 21);
            this.clConsumo.Tabela = null;
            this.clConsumo.TabIndex = 3;
            this.clConsumo.Leave += new System.EventHandler(this.clConsumo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "NCM";
            // 
            // clEstado
            // 
            this.clEstado.Campo = null;
            this.clEstado.ColunaCodigo = "CODETD";
            this.clEstado.ColunaDescricao = "NOME";
            this.clEstado.ColunaIdentificador = null;
            this.clEstado.ColunaTabela = "GETD";
            this.clEstado.Conexao = "Start";
            this.clEstado.Default = null;
            this.clEstado.Edita = true;
            this.clEstado.EditaLookup = false;
            this.clEstado.Location = new System.Drawing.Point(11, 29);
            this.clEstado.MaximoCaracteres = null;
            this.clEstado.Name = "clEstado";
            this.clEstado.NomeGrid = null;
            this.clEstado.Query = 0;
            this.clEstado.Size = new System.Drawing.Size(376, 21);
            this.clEstado.Tabela = null;
            this.clEstado.TabIndex = 1;
            this.clEstado.Leave += new System.EventHandler(this.clEstado_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estado";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(518, 403);
            this.tabControl1.TabIndex = 12;
            // 
            // FormRegraCFOPCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 491);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRegraCFOPCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Regras do CFOP";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCadastroObject_FormClosed);
            this.Load += new System.EventHandler(this.FormCadastro2_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbNaoContrinuinte.Properties)).EndInit();
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private CampoLookup clConsumo;
        private System.Windows.Forms.Label label2;
        private CampoLookup clEstado;
        private System.Windows.Forms.Label label1;
        private CampoLookup clAtivo;
        private CampoLookup clIndustrializar;
        private CampoLookup clRevender;
        private CampoLookup clNumeroCCF;
        private CampoLookup clTipoProduto;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.CheckEdit cbNaoContrinuinte;
        private CampoLookup clRevenderSemST;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private CampoLookup clConsumoSemST;
    }
}