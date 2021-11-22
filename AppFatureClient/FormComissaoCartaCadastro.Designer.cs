namespace AppLib.Windows
{
    partial class FormComissaoCartaCadastro
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComissaoCartaCadastro));
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
            this.lblNCarta = new System.Windows.Forms.Label();
            this.simpleButtonSALVAR = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCANCELAR = new DevExpress.XtraEditors.SimpleButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCARTAANTERIOR = new AppLib.Windows.CampoTexto();
            this.label6 = new System.Windows.Forms.Label();
            this.campoDecimalDEBITOANTERIOR = new AppLib.Windows.CampoDecimal();
            this.label1 = new System.Windows.Forms.Label();
            this.txtObsDeb = new AppLib.Windows.CampoTexto();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVDeb = new AppLib.Windows.CampoDecimal();
            this.label10 = new System.Windows.Forms.Label();
            this.txtObsCred = new AppLib.Windows.CampoTexto();
            this.label12 = new System.Windows.Forms.Label();
            this.txtVCred = new AppLib.Windows.CampoDecimal();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridData3 = new AppLib.Windows.GridData();
            this.gridData1 = new AppLib.Windows.GridData();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridData4 = new AppLib.Windows.GridData();
            this.gridData2 = new AppLib.Windows.GridData();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnDeletarNF = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeletarCarta = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvarNFe = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvarCarta = new DevExpress.XtraEditors.SimpleButton();
            this.pdfViewer1 = new DevExpress.XtraPdfViewer.PdfViewer();
            this.btnVisualizaNFe = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelecionaNFe = new DevExpress.XtraEditors.SimpleButton();
            this.btnVisualizaCarta = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelecionaCarta = new DevExpress.XtraEditors.SimpleButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ofdComissao = new System.Windows.Forms.OpenFileDialog();
            this.fbdComissao = new System.Windows.Forms.FolderBrowserDialog();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(1005, 39);
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
            this.toolStripButtonEXCLUIR.Size = new System.Drawing.Size(69, 36);
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
            this.toolStripButtonANEXOS.Size = new System.Drawing.Size(85, 36);
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
            this.panel1.Controls.Add(this.lblNCarta);
            this.panel1.Controls.Add(this.simpleButtonSALVAR);
            this.panel1.Controls.Add(this.simpleButtonOK);
            this.panel1.Controls.Add(this.simpleButtonCANCELAR);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 407);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1005, 61);
            this.panel1.TabIndex = 11;
            // 
            // lblNCarta
            // 
            this.lblNCarta.AutoSize = true;
            this.lblNCarta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNCarta.Location = new System.Drawing.Point(12, 23);
            this.lblNCarta.Name = "lblNCarta";
            this.lblNCarta.Size = new System.Drawing.Size(0, 16);
            this.lblNCarta.TabIndex = 45;
            // 
            // simpleButtonSALVAR
            // 
            this.simpleButtonSALVAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonSALVAR.Location = new System.Drawing.Point(728, 18);
            this.simpleButtonSALVAR.Name = "simpleButtonSALVAR";
            this.simpleButtonSALVAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonSALVAR.TabIndex = 1;
            this.simpleButtonSALVAR.Text = "Salvar";
            this.simpleButtonSALVAR.Click += new System.EventHandler(this.simpleButtonSALVAR_Click);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK.Location = new System.Drawing.Point(818, 18);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCANCELAR
            // 
            this.simpleButtonCANCELAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR.Location = new System.Drawing.Point(908, 18);
            this.simpleButtonCANCELAR.Name = "simpleButtonCANCELAR";
            this.simpleButtonCANCELAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR.TabIndex = 3;
            this.simpleButtonCANCELAR.Text = "Cancelar";
            this.simpleButtonCANCELAR.Click += new System.EventHandler(this.simpleButtonCANCELAR_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.txtCARTAANTERIOR);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.campoDecimalDEBITOANTERIOR);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.txtObsDeb);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.txtVDeb);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.txtObsCred);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.txtVCred);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(997, 342);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Complementares";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(184, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Carta";
            // 
            // txtCARTAANTERIOR
            // 
            this.txtCARTAANTERIOR.Campo = null;
            this.txtCARTAANTERIOR.Default = null;
            this.txtCARTAANTERIOR.Edita = true;
            this.txtCARTAANTERIOR.Enabled = false;
            this.txtCARTAANTERIOR.Location = new System.Drawing.Point(182, 128);
            this.txtCARTAANTERIOR.MaximoCaracteres = null;
            this.txtCARTAANTERIOR.Name = "txtCARTAANTERIOR";
            this.txtCARTAANTERIOR.Query = 0;
            this.txtCARTAANTERIOR.Size = new System.Drawing.Size(57, 20);
            this.txtCARTAANTERIOR.Tabela = null;
            this.txtCARTAANTERIOR.TabIndex = 53;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 52;
            this.label6.Text = "Valor débito anterior";
            // 
            // campoDecimalDEBITOANTERIOR
            // 
            this.campoDecimalDEBITOANTERIOR.Campo = null;
            this.campoDecimalDEBITOANTERIOR.Decimais = 2;
            this.campoDecimalDEBITOANTERIOR.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimalDEBITOANTERIOR.Edita = true;
            this.campoDecimalDEBITOANTERIOR.Enabled = false;
            this.campoDecimalDEBITOANTERIOR.Location = new System.Drawing.Point(8, 128);
            this.campoDecimalDEBITOANTERIOR.Name = "campoDecimalDEBITOANTERIOR";
            this.campoDecimalDEBITOANTERIOR.Query = 0;
            this.campoDecimalDEBITOANTERIOR.Size = new System.Drawing.Size(126, 20);
            this.campoDecimalDEBITOANTERIOR.Tabela = null;
            this.campoDecimalDEBITOANTERIOR.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(184, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Descrição";
            // 
            // txtObsDeb
            // 
            this.txtObsDeb.Campo = null;
            this.txtObsDeb.Default = null;
            this.txtObsDeb.Edita = true;
            this.txtObsDeb.Location = new System.Drawing.Point(182, 75);
            this.txtObsDeb.MaximoCaracteres = null;
            this.txtObsDeb.Name = "txtObsDeb";
            this.txtObsDeb.Query = 0;
            this.txtObsDeb.Size = new System.Drawing.Size(546, 20);
            this.txtObsDeb.Tabela = null;
            this.txtObsDeb.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Valor Débitos";
            // 
            // txtVDeb
            // 
            this.txtVDeb.Campo = null;
            this.txtVDeb.Decimais = 2;
            this.txtVDeb.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtVDeb.Edita = true;
            this.txtVDeb.Location = new System.Drawing.Point(8, 75);
            this.txtVDeb.Name = "txtVDeb";
            this.txtVDeb.Query = 0;
            this.txtVDeb.Size = new System.Drawing.Size(126, 20);
            this.txtVDeb.Tabela = null;
            this.txtVDeb.TabIndex = 47;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(184, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 46;
            this.label10.Text = "Descrição";
            // 
            // txtObsCred
            // 
            this.txtObsCred.Campo = null;
            this.txtObsCred.Default = null;
            this.txtObsCred.Edita = true;
            this.txtObsCred.Location = new System.Drawing.Point(182, 26);
            this.txtObsCred.MaximoCaracteres = null;
            this.txtObsCred.Name = "txtObsCred";
            this.txtObsCred.Query = 0;
            this.txtObsCred.Size = new System.Drawing.Size(546, 20);
            this.txtObsCred.Tabela = null;
            this.txtObsCred.TabIndex = 45;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(5, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 44;
            this.label12.Text = "Valor Créditos";
            // 
            // txtVCred
            // 
            this.txtVCred.Campo = null;
            this.txtVCred.Decimais = 2;
            this.txtVCred.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtVCred.Edita = true;
            this.txtVCred.Location = new System.Drawing.Point(8, 26);
            this.txtVCred.Name = "txtVCred";
            this.txtVCred.Query = 0;
            this.txtVCred.Size = new System.Drawing.Size(126, 20);
            this.txtVCred.Tabela = null;
            this.txtVCred.TabIndex = 43;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridData3);
            this.tabPage2.Controls.Add(this.gridData1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(997, 342);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Débito";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridData3
            // 
            this.gridData3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData3.AutoAjuste = true;
            this.gridData3.BotaoEditar = false;
            this.gridData3.BotaoExcluir = true;
            this.gridData3.BotaoNovo = false;
            this.gridData3.Conexao = "Start";
            this.gridData3.Consulta = new string[] {
        "select TM2.IDMOV, ",
        "       TM2.NUMEROMOV, ",
        "\t   FCFO.NOMEFANTASIA,",
        "\t   TRPR.NOMEFANTASIA as \'REPRESENTANTE\',",
        "\t   TM2.VALORLIQUIDOORIG, ",
        "\t   TM2.VALORBRUTOORIG,",
        "\t   (case when TM2.STATUS = \'A\' THEN \'ABERTO\'",
        "\t    when TM2.STATUS = \'F\' THEN \'FATURADO\'",
        "\t    END) as STATUS",
        "",
        "from ZTMOVCOMISSAO ZTC",
        "",
        "inner join TMOV TM",
        "on TM.IDMOV = ZTC.IDMOV",
        "",
        "inner join TMOV TM2",
        "on TM2.NUMEROMOV = TM.NUMEROMOV",
        "and TM2.CODCOLIGADA = TM.CODCOLIGADA",
        "",
        "inner join FCFO",
        "on FCFO.CODCFO = TM2.CODCFO",
        "",
        "inner join TRPR",
        "on TRPR.CODRPR = TM2.CODRPR",
        "",
        "where TM.CODCOLIGADA = 1",
        "and TM2.CODTMV = \'2.1.20\'",
        "and ZTC.VALORPAGO <= ZTC.VALORLIQ",
        "and ZTC.TIPOPAG = \'DÉBITO\'",
        "and (ZTC.CODRPR = \'00033\' or ZTC.CODRPR = ?)",
        "and ZTC.NCARTA = ?"};
            this.gridData3.Formatacao = null;
            this.gridData3.FormPai = null;
            this.gridData3.Location = new System.Drawing.Point(3, 161);
            this.gridData3.ModoFiltro = false;
            this.gridData3.Name = "gridData3";
            this.gridData3.NomeGrid = null;
            this.gridData3.SelecaoCascata = true;
            this.gridData3.Selecionou = false;
            this.gridData3.Size = new System.Drawing.Size(991, 175);
            this.gridData3.TabIndex = 1;
            this.gridData3.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData3.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData3.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData3_Excluir);
            this.gridData3.AposAtualizar += new AppLib.Windows.GridData.AposAtualizarHandler(this.gridData3_AposAtualizar);
            // 
            // gridData1
            // 
            this.gridData1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData1.AutoAjuste = true;
            this.gridData1.BotaoEditar = true;
            this.gridData1.BotaoExcluir = false;
            this.gridData1.BotaoNovo = false;
            this.gridData1.Conexao = "Start";
            this.gridData1.Consulta = new string[] {
        "select TM2.IDMOV, ",
        "       TM2.NUMEROMOV, ",
        "\tTM2.DATAEMISSAO, ",
        "\t   FCFO.NOMEFANTASIA,",
        "\t   TRPR.NOMEFANTASIA as \'REPRESENTANTE\',",
        "\t   TM2.VALORLIQUIDOORIG, ",
        "\t   TM2.VALORBRUTOORIG,",
        "\t   (case when TM2.STATUS = \'A\' THEN \'ABERTO\'",
        "\t    when TM2.STATUS = \'F\' THEN \'FATURADO\'",
        "\t    END) as STATUS",
        "",
        "from ZTMOVCOMISSAO ZTC",
        "",
        "inner join TMOV TM",
        "on TM.IDMOV = ZTC.IDMOV",
        "",
        "inner join TMOV TM2",
        "on TM2.NUMEROMOV = TM.NUMEROMOV",
        "and TM2.CODCOLIGADA = TM.CODCOLIGADA",
        "",
        "inner join FCFO",
        "on FCFO.CODCFO = TM2.CODCFO",
        "",
        "inner join TRPR",
        "on TRPR.CODRPR = TM2.CODRPR",
        "",
        "where TM.CODCOLIGADA = 1",
        "and TM2.CODTMV = \'2.1.20\'",
        "and ZTC.VALORPAGO <= ZTC.VALORLIQ",
        "and (ZTC.CODRPR = \'00033\' or ZTC.CODRPR = ?)",
        "and ZTC.NCARTA is null"};
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = null;
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(991, 152);
            this.gridData1.TabIndex = 0;
            this.gridData1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData1_Editar);
            this.gridData1.AposSelecao += new AppLib.Windows.GridData.AposSelecaoHandler(this.gridData1_AposSelecao);
            this.gridData1.AposAtualizar += new AppLib.Windows.GridData.AposAtualizarHandler(this.gridData1_AposAtualizar);
            this.gridData1.Load += new System.EventHandler(this.gridData1_Load);
            this.gridData1.DoubleClick += new System.EventHandler(this.gridData1_DoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1005, 368);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridData4);
            this.tabPage1.Controls.Add(this.gridData2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(997, 342);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Crédito";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridData4
            // 
            this.gridData4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData4.AutoAjuste = true;
            this.gridData4.BotaoEditar = false;
            this.gridData4.BotaoExcluir = true;
            this.gridData4.BotaoNovo = false;
            this.gridData4.Conexao = "Start";
            this.gridData4.Consulta = new string[] {
        "select TM2.IDMOV, ",
        "       TM2.NUMEROMOV, ",
        "\t   FCFO.CODCFO, ",
        "\t   FCFO.NOMEFANTASIA, ",
        "\t   TRPR.CODRPR, ",
        "\t   TRPR.NOMEFANTASIA as \'NOME\', ",
        "\t   case when ZTC.VALORPAGO >= ZTC.VALORLIQ then \'Finalizado\' else \'Aberto\' end a" +
            "s \'STATUS\', ",
        "\t   ZTC.VALORPAGO as \'BAIXA\', ",
        "\t   ZTC.VALORLIQ as  \'VALORLIQUIDOORIG\',",
        "                      ZTC.PERCENTUALCOMISSAO   ",
        "",
        "from ZTMOVCOMISSAO ZTC",
        "",
        "inner join TMOV TM",
        "on TM.IDMOV = ZTC.IDMOV",
        "",
        "inner join TMOV TM2",
        "on TM2.NUMEROMOV = TM.NUMEROMOV",
        "and TM2.CODCOLIGADA = TM.CODCOLIGADA",
        "",
        "inner join FCFO",
        "on FCFO.CODCFO = TM2.CODCFO",
        "",
        "inner join TRPR",
        "on TRPR.CODRPR = TM2.CODRPR",
        "",
        "where TM.CODCOLIGADA = 1",
        "and TM2.CODTMV = \'2.1.20\'",
        "and ZTC.VALORPAGO >= ZTC.VALORLIQ",
        "and ZTC.TIPOPAG = \'CRÉDITO\'",
        "and (ZTC.CODRPR = \'00033\' or ZTC.CODRPR = ?)",
        "and ZTC.NCARTA = ?"};
            this.gridData4.Formatacao = null;
            this.gridData4.FormPai = null;
            this.gridData4.Location = new System.Drawing.Point(3, 161);
            this.gridData4.ModoFiltro = false;
            this.gridData4.Name = "gridData4";
            this.gridData4.NomeGrid = null;
            this.gridData4.SelecaoCascata = true;
            this.gridData4.Selecionou = false;
            this.gridData4.Size = new System.Drawing.Size(991, 175);
            this.gridData4.TabIndex = 1;
            this.gridData4.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData4.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData4.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData4_SetParametros);
            this.gridData4.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData4_Excluir);
            this.gridData4.AposAtualizar += new AppLib.Windows.GridData.AposAtualizarHandler(this.gridData4_AposAtualizar);
            // 
            // gridData2
            // 
            this.gridData2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData2.AutoAjuste = true;
            this.gridData2.BotaoEditar = true;
            this.gridData2.BotaoExcluir = false;
            this.gridData2.BotaoNovo = false;
            this.gridData2.Conexao = "Start";
            this.gridData2.Consulta = new string[] {
        "select TM2.IDMOV, ",
        "       TM2.NUMEROMOV, ",
        "\tTM2.DATAEMISSAO, ",
        "\t   FCFO.CODCFO, ",
        "\t   FCFO.NOMEFANTASIA, ",
        "\t   TRPR.CODRPR, ",
        "\t   TRPR.NOMEFANTASIA as \'NOME\', ",
        "\t   case when ZTC.VALORPAGO >= ZTC.VALORLIQ then \'Finalizado\' else \'Aberto\' end a" +
            "s \'STATUS\', ",
        "\t   ZTC.VALORPAGO as \'BAIXA\', ",
        "\t   ZTC.VALORLIQ as  \'VALORLIQUIDOORIG\',",
        "                      ZTC.PERCENTUALCOMISSAO  ",
        "",
        "from ZTMOVCOMISSAO ZTC",
        "",
        "inner join TMOV TM",
        "on TM.IDMOV = ZTC.IDMOV",
        "",
        "inner join TMOV TM2",
        "on TM2.NUMEROMOV = TM.NUMEROMOV",
        "and TM2.CODCOLIGADA = TM.CODCOLIGADA",
        "",
        "inner join FCFO",
        "on FCFO.CODCFO = TM2.CODCFO",
        "",
        "inner join TRPR",
        "on TRPR.CODRPR = TM2.CODRPR",
        "",
        "where TM.CODCOLIGADA = 1",
        "and TM2.CODTMV = \'2.1.20\'",
        "and ZTC.VALORPAGO >= ZTC.VALORLIQ",
        "and (ZTC.CODRPR = \'00033\' or ZTC.CODRPR = ?)",
        "and ZTC.NCARTA is null"};
            this.gridData2.Formatacao = null;
            this.gridData2.FormPai = null;
            this.gridData2.Location = new System.Drawing.Point(3, 3);
            this.gridData2.ModoFiltro = false;
            this.gridData2.Name = "gridData2";
            this.gridData2.NomeGrid = null;
            this.gridData2.SelecaoCascata = true;
            this.gridData2.Selecionou = false;
            this.gridData2.Size = new System.Drawing.Size(991, 152);
            this.gridData2.TabIndex = 0;
            this.gridData2.TabStop = false;
            this.gridData2.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData2.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData2.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData2_SetParametros);
            this.gridData2.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData2_Editar);
            this.gridData2.AposAtualizar += new AppLib.Windows.GridData.AposAtualizarHandler(this.gridData2_AposAtualizar);
            this.gridData2.Load += new System.EventHandler(this.gridData2_Load);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnDeletarNF);
            this.tabPage4.Controls.Add(this.btnDeletarCarta);
            this.tabPage4.Controls.Add(this.btnSalvarNFe);
            this.tabPage4.Controls.Add(this.btnSalvarCarta);
            this.tabPage4.Controls.Add(this.pdfViewer1);
            this.tabPage4.Controls.Add(this.btnVisualizaNFe);
            this.tabPage4.Controls.Add(this.btnSelecionaNFe);
            this.tabPage4.Controls.Add(this.btnVisualizaCarta);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.btnSelecionaCarta);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(997, 342);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Anexos";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnDeletarNF
            // 
            this.btnDeletarNF.Location = new System.Drawing.Point(190, 40);
            this.btnDeletarNF.Name = "btnDeletarNF";
            this.btnDeletarNF.Size = new System.Drawing.Size(52, 23);
            this.btnDeletarNF.TabIndex = 58;
            this.btnDeletarNF.Text = "Deletar";
            this.btnDeletarNF.Visible = false;
            this.btnDeletarNF.Click += new System.EventHandler(this.btnDeletarNF_Click);
            // 
            // btnDeletarCarta
            // 
            this.btnDeletarCarta.Location = new System.Drawing.Point(190, 11);
            this.btnDeletarCarta.Name = "btnDeletarCarta";
            this.btnDeletarCarta.Size = new System.Drawing.Size(52, 23);
            this.btnDeletarCarta.TabIndex = 57;
            this.btnDeletarCarta.Text = "Deletar";
            this.btnDeletarCarta.Visible = false;
            this.btnDeletarCarta.Click += new System.EventHandler(this.btnDeletarCarta_Click);
            // 
            // btnSalvarNFe
            // 
            this.btnSalvarNFe.Location = new System.Drawing.Point(132, 40);
            this.btnSalvarNFe.Name = "btnSalvarNFe";
            this.btnSalvarNFe.Size = new System.Drawing.Size(52, 23);
            this.btnSalvarNFe.TabIndex = 56;
            this.btnSalvarNFe.Text = "Exportar";
            this.btnSalvarNFe.Visible = false;
            this.btnSalvarNFe.Click += new System.EventHandler(this.btnSalvarNFe_Click);
            // 
            // btnSalvarCarta
            // 
            this.btnSalvarCarta.Location = new System.Drawing.Point(132, 11);
            this.btnSalvarCarta.Name = "btnSalvarCarta";
            this.btnSalvarCarta.Size = new System.Drawing.Size(52, 23);
            this.btnSalvarCarta.TabIndex = 55;
            this.btnSalvarCarta.Text = "Exportar";
            this.btnSalvarCarta.Visible = false;
            this.btnSalvarCarta.Click += new System.EventHandler(this.btnSalvarCarta_Click);
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfViewer1.Location = new System.Drawing.Point(11, 69);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(980, 273);
            this.pdfViewer1.TabIndex = 54;
            this.pdfViewer1.Visible = false;
            // 
            // btnVisualizaNFe
            // 
            this.btnVisualizaNFe.Enabled = false;
            this.btnVisualizaNFe.Location = new System.Drawing.Point(74, 40);
            this.btnVisualizaNFe.Name = "btnVisualizaNFe";
            this.btnVisualizaNFe.Size = new System.Drawing.Size(52, 23);
            this.btnVisualizaNFe.TabIndex = 53;
            this.btnVisualizaNFe.Text = "Visualizar";
            this.btnVisualizaNFe.Visible = false;
            this.btnVisualizaNFe.Click += new System.EventHandler(this.btnVisualizaNFe_Click);
            // 
            // btnSelecionaNFe
            // 
            this.btnSelecionaNFe.Location = new System.Drawing.Point(45, 40);
            this.btnSelecionaNFe.Name = "btnSelecionaNFe";
            this.btnSelecionaNFe.Size = new System.Drawing.Size(23, 23);
            this.btnSelecionaNFe.TabIndex = 52;
            this.btnSelecionaNFe.Text = "...";
            this.btnSelecionaNFe.Click += new System.EventHandler(this.btnSelecionaNFe_Click);
            // 
            // btnVisualizaCarta
            // 
            this.btnVisualizaCarta.Enabled = false;
            this.btnVisualizaCarta.Location = new System.Drawing.Point(74, 11);
            this.btnVisualizaCarta.Name = "btnVisualizaCarta";
            this.btnVisualizaCarta.Size = new System.Drawing.Size(52, 23);
            this.btnVisualizaCarta.TabIndex = 51;
            this.btnVisualizaCarta.Text = "Visualizar";
            this.btnVisualizaCarta.Visible = false;
            this.btnVisualizaCarta.Click += new System.EventHandler(this.btnVisualizaCarta_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "NF-e";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "Carta";
            // 
            // btnSelecionaCarta
            // 
            this.btnSelecionaCarta.Location = new System.Drawing.Point(45, 11);
            this.btnSelecionaCarta.Name = "btnSelecionaCarta";
            this.btnSelecionaCarta.Size = new System.Drawing.Size(23, 23);
            this.btnSelecionaCarta.TabIndex = 1;
            this.btnSelecionaCarta.Text = "...";
            this.btnSelecionaCarta.Click += new System.EventHandler(this.btnSelecionaCarta_Click);
            // 
            // ofdComissao
            // 
            this.ofdComissao.FileName = "openFileDialog1";
            // 
            // FormComissaoCartaCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 468);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormComissaoCartaCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Carta";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCadastroObject_FormClosed);
            this.Load += new System.EventHandler(this.FormCadastro2_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
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
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label12;
        private CampoDecimal txtVCred;
        private System.Windows.Forms.Label label1;
        private CampoTexto txtObsDeb;
        private System.Windows.Forms.Label label2;
        private CampoDecimal txtVDeb;
        private System.Windows.Forms.Label label10;
        private CampoTexto txtObsCred;
        private GridData gridData2;
        private GridData gridData3;
        private GridData gridData1;
        private GridData gridData4;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label lblNCarta;
        private DevExpress.XtraEditors.SimpleButton btnVisualizaNFe;
        private DevExpress.XtraEditors.SimpleButton btnSelecionaNFe;
        private DevExpress.XtraEditors.SimpleButton btnVisualizaCarta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnSelecionaCarta;
        private System.Windows.Forms.OpenFileDialog ofdComissao;
        private DevExpress.XtraPdfViewer.PdfViewer pdfViewer1;
        private System.Windows.Forms.FolderBrowserDialog fbdComissao;
        private DevExpress.XtraEditors.SimpleButton btnSalvarNFe;
        private DevExpress.XtraEditors.SimpleButton btnSalvarCarta;
        private DevExpress.XtraEditors.SimpleButton btnDeletarNF;
        private DevExpress.XtraEditors.SimpleButton btnDeletarCarta;
        private System.Windows.Forms.Label label5;
        private CampoTexto txtCARTAANTERIOR;
        private System.Windows.Forms.Label label6;
        private CampoDecimal campoDecimalDEBITOANTERIOR;
    }
}