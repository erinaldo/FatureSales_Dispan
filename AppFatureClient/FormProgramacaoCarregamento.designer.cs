namespace AppFatureClient
{
    partial class FormProgramacaoCarregamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgramacaoCarregamento));
            AppLib.Windows.GridProps gridProps1 = new AppLib.Windows.GridProps();
            AppLib.Windows.GridProps gridProps2 = new AppLib.Windows.GridProps();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCANCELAR = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.cmbCaminhao = new System.Windows.Forms.ComboBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataIni = new AppLib.Windows.CampoData();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gridObject1 = new AppLib.Windows.GridObject();
            this.gridData2 = new AppLib.Windows.GridData();
            this.tabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK.Location = new System.Drawing.Point(1177, 632);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 18;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCANCELAR
            // 
            this.simpleButtonCANCELAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR.Location = new System.Drawing.Point(1267, 632);
            this.simpleButtonCANCELAR.Name = "simpleButtonCANCELAR";
            this.simpleButtonCANCELAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR.TabIndex = 19;
            this.simpleButtonCANCELAR.Text = "Cancelar";
            this.simpleButtonCANCELAR.Click += new System.EventHandler(this.simpleButtonCANCELAR_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(788, 125);
            this.tabControl2.TabIndex = 21;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cmbTipo);
            this.tabPage2.Controls.Add(this.cmbCaminhao);
            this.tabPage2.Controls.Add(this.simpleButton1);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtDataIni);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(780, 99);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Parâmetros";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "TOCO",
            "TRUCK",
            "CARRETA 2 EIXOS",
            "CARRETA BAÚ",
            "CARRETA 3 EIXOS",
            "CARRETA CAVALO TRUCKADO BAÚ",
            "BI-TREM (TREMINHÃO) -7 EIXOS"});
            this.cmbTipo.Location = new System.Drawing.Point(407, 42);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(175, 21);
            this.cmbTipo.TabIndex = 30;
            // 
            // cmbCaminhao
            // 
            this.cmbCaminhao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCaminhao.FormattingEnabled = true;
            this.cmbCaminhao.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.cmbCaminhao.Location = new System.Drawing.Point(300, 42);
            this.cmbCaminhao.Name = "cmbCaminhao";
            this.cmbCaminhao.Size = new System.Drawing.Size(101, 21);
            this.cmbCaminhao.TabIndex = 29;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.simpleButton1.Location = new System.Drawing.Point(147, 40);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(147, 23);
            this.simpleButton1.TabIndex = 26;
            this.simpleButton1.Text = "Verificar Caminhão";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(404, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Tipo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Caminhão";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Data Inicial";
            // 
            // txtDataIni
            // 
            this.txtDataIni.Campo = null;
            this.txtDataIni.Default = null;
            this.txtDataIni.Edita = true;
            this.txtDataIni.Location = new System.Drawing.Point(9, 42);
            this.txtDataIni.Name = "txtDataIni";
            this.txtDataIni.Query = 0;
            this.txtDataIni.Size = new System.Drawing.Size(132, 21);
            this.txtDataIni.Tabela = null;
            this.txtDataIni.TabIndex = 16;
            this.txtDataIni.Validating += new System.ComponentModel.CancelEventHandler(this.txtDataIni_Validating);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridData2);
            this.splitContainer1.Size = new System.Drawing.Size(1354, 667);
            this.splitContainer1.SplitterDistance = 788;
            this.splitContainer1.TabIndex = 24;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gridObject1);
            this.splitContainer2.Size = new System.Drawing.Size(788, 667);
            this.splitContainer2.SplitterDistance = 126;
            this.splitContainer2.TabIndex = 0;
            // 
            // gridObject1
            // 
            this.gridObject1.BotaoEditar = false;
            this.gridObject1.BotaoExcluir = false;
            this.gridObject1.BotaoNovo = false;
            this.gridObject1.Conexao = "Start";
            this.gridObject1.Consulta = new string[] {
        resources.GetString("gridObject1.Consulta"),
        "\tSELECT SUBLISTA.*,",
        "\t\tCAST((QTD_PEDIDO -  (ISNULL(QTD_PROG, 0)+ ISNULL(QTD_CARREGADA, 0))) AS NUMERIC" +
            "(15,2)) QTD_SALDO",
        "\tFROM",
        "\t\t(SELECT",
        "\t\tTITMMOV.IDMOV,",
        "\t\tTITMMOV.NSEQITMMOV,",
        "\t\tFCFO.NOMEFANTASIA,",
        "\t\tTMOV.NUMEROMOV,",
        "\t\tTMOV.DATAENTREGA,",
        "\t\tTPRODUTO.DESCRICAO,",
        "\t\tTITMMOV.QUANTIDADEORIGINAL QTD_PEDIDO,",
        "\t\tTMOV.CODTMV,",
        "\t\tTPRODUTO.CODIGOPRD,",
        "\t\tTITMMOV.IDPRDCOMPOSTO,",
        "\t\tTITMMOV.CODCOLIGADA,",
        "\t\t(SELECT SUM(ZPROGRAMACAOCARREGAMENTO.QTDcarregada)",
        "\t\tFROM ZPROGRAMACAOCARREGAMENTO",
        "\t\tWHERE",
        "\t\tZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA",
        "\t\tAND ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV",
        "\t\tAND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV",
        "\t\t) QTD_CARREGADA,",
        "\t\t(SELECT SUM(ZPROGRAMACAOCARREGAMENTO.QTDE)",
        "\t\tFROM  ZPROGRAMACAOCARREGAMENTO",
        "\t\tWHERE",
        "\t\t ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA",
        "\t\tAND ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV",
        "\t\tAND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV",
        "\t\t ) QTD_PROG",
        "\t\tFROM ",
        "\t\tTMOV, TITMMOV, TPRODUTO, FCFO",
        "\t\tWHERE ",
        "\t\tTMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA",
        "\t\tAND TMOV.IDMOV = TITMMOV.IDMOV",
        "\t\tAND TITMMOV.IDPRD = TPRODUTO.IDPRD",
        "\t\tAND TMOV.CODCOLCFO = FCFO.CODCOLIGADA",
        "\t\tAND TMOV.CODCFO = FCFO.CODCFO",
        "\t\t)SUBLISTA\t",
        ") LISTA",
        "WHERE",
        "LISTA.IDMOV IN (?)"};
            this.gridObject1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridObject1.Location = new System.Drawing.Point(0, 0);
            this.gridObject1.Name = "gridObject1";
            this.gridObject1.NomeGrid = "ZPROGRAMACAOCARREGAMENTO2";
            this.gridObject1.Size = new System.Drawing.Size(788, 537);
            this.gridObject1.TabIndex = 24;
            this.gridObject1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridObject1.SetParametros += new AppLib.Windows.GridObject.SetNewParametrosHandler(this.gridObject1_SetParametros);
            this.gridObject1.Atualizar += new AppLib.Windows.GridObject.AtualizarHandler(this.gridObject1_Atualizar);
            // 
            // gridData2
            // 
            this.gridData2.AutoAjuste = true;
            this.gridData2.BotaoEditar = false;
            this.gridData2.BotaoExcluir = true;
            this.gridData2.BotaoNovo = false;
            this.gridData2.Conexao = "Start";
            this.gridData2.Consulta = new string[] {
        "SELECT ",
        "ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO, ",
        "ZCARREGAMENTO.NUMERO,",
        "ZPROGRAMACAOCARREGAMENTO.CAMINHAO, ",
        "ZPROGRAMACAOCARREGAMENTO.TIPOCAMINHAO, ",
        "ZPROGRAMACAOCARREGAMENTO.IDMOV,",
        "ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA,",
        "TMOV.NUMEROMOV, ",
        "ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV, ",
        "CODIGOAUXILIAR, ",
        "TPRODUTO.NOMEFANTASIA PRODUTO, ",
        "ZPROGRAMACAOCARREGAMENTO.QTDE,",
        "TPRODUTO.CODIGOPRD,",
        "TMOV.STATUS",
        "FROM ZPROGRAMACAOCARREGAMENTO ",
        "INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA " +
            "AND ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAMACAOCARREGAMENTO." +
            "NSEQITMMOV = TITMMOV.NSEQITMMOV",
        "INNER JOIN TPRODUTO ON TITMMOV.CODCOLIGADA = TPRODUTO.CODCOLPRD AND TITMMOV.IDPRD" +
            " = TPRODUTO.IDPRD ",
        "INNER JOIN TMOV ON ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TMOV.CODCOLIGADA AND ZP" +
            "ROGRAMACAOCARREGAMENTO.IDMOV = TMOV.IDMOV",
        "LEFT JOIN ZCARREGAMENTO ON ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO = ZCARREGAMENT" +
            "O.IDCARREGAMENTO",
        "WHERE ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA = ? AND ZPROGRAMACAOCARREGAMENTO.CO" +
            "DCOLIGADA = ? AND REPROGRAMAR IS NULL"};
            this.gridData2.Dock = System.Windows.Forms.DockStyle.Fill;
            gridProps1.Agrupar = false;
            gridProps1.Alinhamento = AppLib.Windows.Alinhamento.Esquerda;
            gridProps1.Coluna = "CODIGOPRD";
            gridProps1.Formato = AppLib.Windows.Formato.Nenhum;
            gridProps1.Largura = 50;
            gridProps1.Sequencia = 0;
            gridProps1.Visivel = false;
            gridProps2.Agrupar = false;
            gridProps2.Alinhamento = AppLib.Windows.Alinhamento.Direita;
            gridProps2.Coluna = "QTDE";
            gridProps2.Formato = AppLib.Windows.Formato.Decimal2;
            gridProps2.Largura = 50;
            gridProps2.Sequencia = 0;
            gridProps2.Visivel = true;
            this.gridData2.Formatacao = new AppLib.Windows.GridProps[] {
        gridProps1,
        gridProps2};
            this.gridData2.FormPai = null;
            this.gridData2.Location = new System.Drawing.Point(0, 0);
            this.gridData2.ModoFiltro = false;
            this.gridData2.Name = "gridData2";
            this.gridData2.NomeGrid = "ZPROGRAMACAOCARREGAMENTO";
            this.gridData2.SelecaoCascata = true;
            this.gridData2.Selecionou = false;
            this.gridData2.Size = new System.Drawing.Size(562, 667);
            this.gridData2.TabIndex = 0;
            this.gridData2.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData2.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData2.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData2_SetParametros);
            this.gridData2.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData2_Excluir);
            // 
            // FormProgramacaoCarregamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 667);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.simpleButtonCANCELAR);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FormProgramacaoCarregamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Programação de Carregamento";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormProgramacaoCarregamento_Load);
            this.tabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCANCELAR;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoData txtDataIni;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.ComboBox cmbCaminhao;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private AppLib.Windows.GridData gridData2;
        private AppLib.Windows.GridObject gridObject1;
    }
}