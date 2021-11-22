namespace AppFatureClient
{
    partial class FormCarregamentoVisao
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
            this.grid1.BotaoEditar = true;
            this.grid1.BotaoExcluir = true;
            this.grid1.BotaoNovo = true;
            this.grid1.Consulta = new string[] {
        "SELECT DISTINCT",
        "ZCARREGAMENTO.IDCARREGAMENTO,",
        "ZCARREGAMENTO.DATAAGENDAMENTO DATAAUTORIZACAO,",
        "ZCARREGAMENTO.DATA DATAAGENDAMENTO,",
        "ZCARREGAMENTO.NUMERO ID, ",
        "ZCARREGAMENTO.CODTRA,",
        "TTRA.NOMEFANTASIA,",
        "ZCARREGAMENTO.PLACA,",
        "ZCARREGAMENTO.OBS OBSERVAÇÃO,",
        "ZCARREGAMENTO.DATABAIXA,",
        "ZCARREGAMENTO.STATUS STATUSOPERACAO,",
        "ZCARREGAMENTO.STATUSCARREGADO",
        "FROM ",
        "ZCARREGAMENTO",
        "INNER JOIN TTRA ON ZCARREGAMENTO.CODTRA = TTRA.CODTRA",
        "LEFT OUTER JOIN ZCARREGAMENTOITEMS ON ZCARREGAMENTO.IDCARREGAMENTO = ZCARREGAMENT" +
            "OITEMS.IDCARREGAMENTO",
        "WHERE",
        "TTRA.CODCOLIGADA = ?"};
            this.grid1.NomeGrid = "ZCARREGAMENTO";
            this.grid1.Size = new System.Drawing.Size(1122, 589);
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Selecionar;
            this.grid1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.grid1_SetParametros);
            this.grid1.Novo += new AppLib.Windows.GridData.NovoHandler(this.grid1_Novo);
            this.grid1.Editar += new AppLib.Windows.GridData.EditarHandler(this.grid1_Editar);
            this.grid1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.grid1_Excluir);
            this.grid1.AposSelecao += new AppLib.Windows.GridData.AposSelecaoHandler(this.grid1_AposSelecao);
            this.grid1.Load += new System.EventHandler(this.grid1_Load);
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridData1);
            this.splitContainer1.Size = new System.Drawing.Size(1122, 589);
            this.splitContainer1.SplitterDistance = 286;
            // 
            // gridData1
            // 
            this.gridData1.AutoAjuste = true;
            this.gridData1.BotaoEditar = false;
            this.gridData1.BotaoExcluir = false;
            this.gridData1.BotaoNovo = false;
            this.gridData1.Conexao = "Start";
            this.gridData1.Consulta = new string[] {
        "SELECT ",
        "ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO,",
        "FCFO.NOMEFANTASIA AS CLIENTE,",
        "ZPROGRAMACAOCARREGAMENTO.IDMOV,",
        "ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV,",
        "TPRODUTO.NOMEFANTASIA,",
        "TITMMOV.IDPRDCOMPOSTO,",
        "ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA,",
        "CONVERT(DECIMAL(12,4),ZPROGRAMACAOCARREGAMENTO.QTDE) AS QTD ",
        "FROM",
        "ZPROGRAMACAOCARREGAMENTO ",
        "INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAM" +
            "ACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV AND ZPROGRAMACAOCARREGAMENTO.CO" +
            "DCOLIGADA = TITMMOV.CODCOLIGADA",
        "INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD ",
        "INNER JOIN TMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TMOV.IDMOV",
        "INNER JOIN FCFO ON FCFO.CODCFO = TMOV.CODCFO",
        "WHERE ",
        "ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO = ?"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(0, 0);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = null;
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(150, 46);
            this.gridData1.TabIndex = 0;
            this.gridData1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData1_SetParametros);
            // 
            // FormCarregamentoVisao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 589);
            this.Name = "FormCarregamentoVisao";
            this.Text = "Visão Carregamento";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCarregamentoVisaoVenda_FormClosed);
            this.Load += new System.EventHandler(this.FormCarregamentoVisao_Load);
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