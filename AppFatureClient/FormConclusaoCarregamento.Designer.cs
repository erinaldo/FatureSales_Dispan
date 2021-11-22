namespace AppFatureClient
{
    partial class FormConclusaoCarregamento
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
            this.gridData1 = new AppLib.Windows.GridData();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.Consulta = new string[] {
        "SELECT ",
        "ZCARREGAMENTOITEMS.IDCARREGAMENTOITEMS,",
        "FCFO.NOMEFANTASIA AS CLIENTE,",
        "ZCARREGAMENTOITEMS.IDMOV,",
        "ZCARREGAMENTOITEMS.NSEQITMMOV,",
        "TPRODUTO.NOMEFANTASIA,",
        "TITMMOV.IDPRDCOMPOSTO,",
        "ZCARREGAMENTOITEMS.CODCOLIGADA,",
        "CONVERT(DECIMAL(12,4),ZCARREGAMENTOITEMS.QTDE) AS QTD ",
        "FROM",
        "ZCARREGAMENTOITEMS ",
        "INNER JOIN TITMMOV ON ZCARREGAMENTOITEMS.IDMOV = TITMMOV.IDMOV AND ZCARREGAMENTOI" +
            "TEMS.NSEQITMMOV = TITMMOV.NSEQITMMOV AND ZCARREGAMENTOITEMS.CODCOLIGADA = TITMMO" +
            "V.CODCOLIGADA",
        "INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD ",
        "INNER JOIN TMOV ON ZCARREGAMENTOITEMS.IDMOV = TMOV.IDMOV",
        "INNER JOIN FCFO ON FCFO.CODCFO = TMOV.CODCFO",
        "WHERE ",
        "ZCARREGAMENTOITEMS.IDCARREGAMENTO = ?"};
            this.grid1.NomeGrid = "ZCARREGAMENTOITEMS";
            this.grid1.Size = new System.Drawing.Size(1233, 299);
            this.grid1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.grid1_SetParametros);
            this.grid1.AposSelecao += new AppLib.Windows.GridData.AposSelecaoHandler(this.grid1_AposSelecao);
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridData1);
            this.splitContainer1.Panel2Collapsed = false;
            this.splitContainer1.Size = new System.Drawing.Size(1233, 620);
            this.splitContainer1.SplitterDistance = 299;
            // 
            // gridData1
            // 
            this.gridData1.AutoAjuste = true;
            this.gridData1.BotaoEditar = false;
            this.gridData1.BotaoExcluir = false;
            this.gridData1.BotaoNovo = false;
            this.gridData1.Conexao = "Start";
            this.gridData1.Consulta = new string[] {
        "SELECT TITMMOV.IDMOV, NSEQITMMOV, IDPRDCOMPOSTO,  TPRODUTO.CODIGOPRD, NOMEFANTASI" +
            "A, TITMMOV.QUANTIDADE, QUANTIDADEARECEBER",
        "FROM ",
        "TITMMOV",
        "INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD",
        "WHERE",
        "IDPRDCOMPOSTO = ? ",
        "AND TITMMOV.IDMOV = ?",
        "AND NSEQITMMOV <> ?"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(0, 0);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = null;
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(1233, 317);
            this.gridData1.TabIndex = 0;
            this.gridData1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData1_SetParametros);
            // 
            // FormConclusaoCarregamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 620);
            this.Name = "FormConclusaoCarregamento";
            this.Text = "Concluir Carregamento";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConclusaoCarregamento_FormClosing);
            this.Load += new System.EventHandler(this.FormConclusaoCarregamento_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AppLib.Windows.GridData gridData1;
    }
}