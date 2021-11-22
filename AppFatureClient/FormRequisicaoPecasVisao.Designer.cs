namespace AppFatureClient
{
    partial class FormRequisicaoPecasVisao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRequisicaoPecasVisao));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridData1 = new AppLib.Windows.GridData();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridData2 = new AppLib.Windows.GridData();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.BotaoEditar = true;
            this.grid1.BotaoExcluir = true;
            this.grid1.BotaoNovo = true;
            this.grid1.Consulta = new string[] {
        "SELECT *",
        "FROM ZVWREQUISICAOPECAS",
        "WHERE CODCOLIGADA = ?"};
            this.grid1.NomeGrid = "ZVWREQUISICAOPECAS";
            this.grid1.Size = new System.Drawing.Size(1220, 604);
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Selecionar;
            this.grid1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.grid1_SetParametros);
            this.grid1.Novo += new AppLib.Windows.GridData.NovoHandler(this.grid1_Novo);
            this.grid1.Editar += new AppLib.Windows.GridData.EditarHandler(this.grid1_Editar);
            this.grid1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.grid1_Excluir);
            this.grid1.AposSelecao += new AppLib.Windows.GridData.AposSelecaoHandler(this.grid1_EventoClick);
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1220, 604);
            this.splitContainer1.SplitterDistance = 327;
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 3);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(136, 14);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
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
            this.tabControl1.Size = new System.Drawing.Size(150, 46);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(142, 20);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridData1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(142, 20);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridData1
            // 
            this.gridData1.AutoAjuste = true;
            this.gridData1.BotaoEditar = false;
            this.gridData1.BotaoExcluir = false;
            this.gridData1.BotaoNovo = false;
            this.gridData1.Conexao = "Start";
            this.gridData1.Consulta = new string[] {
        resources.GetString("gridData1.Consulta"),
        "\tSELECT SUBLISTA.*,",
        "\t\t(QTD_PEDIDO -  (ISNULL(QTD_PROG, 0)+ ISNULL(QTD_CARREGADA, 0))) QTD_SALDO",
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
        "\t\t(SELECT SUM(ZCARREGAMENTOITEMS.QTDE)",
        "\t\tFROM ZCARREGAMENTOITEMS",
        "\t\tWHERE",
        "\t\tZCARREGAMENTOITEMS.CODCOLIGADA = TITMMOV.CODCOLIGADA",
        "\t\tAND ZCARREGAMENTOITEMS.IDMOV = TITMMOV.IDMOV",
        "\t\tAND ZCARREGAMENTOITEMS.NSEQITMMOV = TITMMOV.NSEQITMMOV",
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
        "LISTA.IDMOV = ?"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = "ZPROGRAMACAO";
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(136, 14);
            this.gridData1.TabIndex = 0;
            this.gridData1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData1_SetParametros);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridData2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(142, 20);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gridData2
            // 
            this.gridData2.AutoAjuste = true;
            this.gridData2.BotaoEditar = false;
            this.gridData2.BotaoExcluir = true;
            this.gridData2.BotaoNovo = false;
            this.gridData2.Conexao = "Start";
            this.gridData2.Consulta = new string[] {
        "SELECT",
        "ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO,",
        "ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO,",
        "ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA,",
        "ZPROGRAMACAOCARREGAMENTO.IDMOV,",
        "ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV,",
        "TPRODUTO.CODIGOPRD,",
        "TPRODUTO.NOMEFANTASIA AS PRODUTO,",
        "TITMMOV.IDPRDCOMPOSTO,",
        "ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA,",
        "ZPROGRAMACAOCARREGAMENTO.QTDE,",
        "ZPROGRAMACAOCARREGAMENTO.CAMINHAO,",
        "ZPROGRAMACAOCARREGAMENTO.DATAALTERACAO",
        "FROM",
        "TMOV,",
        "ZPROGRAMACAOCARREGAMENTO,",
        "TITMMOV,",
        "FCFO,",
        "TPRODUTO",
        "WHERE ",
        "TITMMOV.IDMOV = ZPROGRAMACAOCARREGAMENTO.IDMOV",
        "AND TITMMOV.CODCOLIGADA = ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA",
        "AND TITMMOV.NSEQITMMOV = ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV",
        "AND TITMMOV.IDMOV = TMOV.IDMOV",
        "AND TMOV.CODCFO = FCFO.CODCFO",
        "AND TPRODUTO.IDPRD = TITMMOV.IDPRD",
        "AND TITMMOV.IDMOV = ?"};
            this.gridData2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData2.Formatacao = null;
            this.gridData2.FormPai = null;
            this.gridData2.Location = new System.Drawing.Point(3, 3);
            this.gridData2.ModoFiltro = false;
            this.gridData2.Name = "gridData2";
            this.gridData2.NomeGrid = null;
            this.gridData2.SelecaoCascata = true;
            this.gridData2.Selecionou = false;
            this.gridData2.Size = new System.Drawing.Size(136, 14);
            this.gridData2.TabIndex = 0;
            this.gridData2.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData2.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData2.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData2_SetParametros);
            this.gridData2.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData2_Excluir);
            // 
            // FormRequisicaoPecasVisao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 604);
            this.Name = "FormRequisicaoPecasVisao";
            this.Text = "Consulta Requisição de Peças";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRequisicaoPecasVisao_FormClosed);
            this.Load += new System.EventHandler(this.FormRequisicaoPecasVisao_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private AppLib.Windows.GridData gridData1;
        private System.Windows.Forms.TabPage tabPage3;
        private AppLib.Windows.GridData gridData2;
    }
}