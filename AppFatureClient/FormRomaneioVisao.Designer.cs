namespace AppFatureClient
{
    partial class FormRomaneioVisao
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.BotaoEditar = true;
            this.grid1.Consulta = new string[] {
        "SELECT ",
        "ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO,",
        "ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO,",
        "ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTOANTERIOR,",
        "ZPROGRAMACAOCARREGAMENTO.CARREGAMENTO,",
        "ZCARREGAMENTO.NUMERO AS \'NUMERO CARREGAMENTO\',",
        "ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA,",
        "ZCARREGAMENTO.DATAAGENDAMENTO,",
        "ZCARREGAMENTO.DATA DATACARREGAMENTO,",
        "ZPROGRAMACAOCARREGAMENTO.DATABAIXA,",
        "ZPROGRAMACAOCARREGAMENTO.IDMOV,",
        "ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV,",
        "TMOV.NUMEROMOV,",
        "(CASE TMOV.STATUS",
        "WHEN \'A\' THEN \'A FATURAR\'",
        "WHEN \'G\' THEN \'PARCIALMENTE FATURADO\'  ",
        "WHEN \'F\' THEN \'FATURADO\' ",
        "WHEN \'C\' THEN \'CANCELADO\' ",
        "WHEN \'U\' THEN \'EM FATURAMENTO\'",
        "END) AS STATUS,",
        "FCFO.NOMEFANTASIA AS CLIENTE,",
        "TPRODUTO.CODIGOPRD,",
        "TPRODUTO.NOMEFANTASIA AS PRODUTO,",
        "ZPROGRAMACAOCARREGAMENTO.QTDE QTDPROGRAMADA,",
        "ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA, ",
        "ZPROGRAMACAOCARREGAMENTO.CAMINHAO,",
        "ZPROGRAMACAOCARREGAMENTO.REPROGRAMAR,",
        "ZPROGRAMACAOCARREGAMENTO.STATUSBAIXA,",
        "ZPROGRAMACAOCARREGAMENTO.ROMANEIO,",
        "ZPROGRAMACAOCARREGAMENTO.OBSROMANEIO,",
        "TITMMOVCOMPL.OBSPRDPADRAO",
        "FROM",
        "TMOV INNER JOIN ZPROGRAMACAOCARREGAMENTO ON TMOV.IDMOV = ZPROGRAMACAOCARREGAMENTO" +
            ".IDMOV",
        "",
        "INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAM" +
            "ACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV AND ZPROGRAMACAOCARREGAMENTO.CO" +
            "DCOLIGADA = TITMMOV.CODCOLIGADA",
        "INNER JOIN TITMMOVCOMPL ON TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV AND TITMMOV.CODCOLI" +
            "GADA = TITMMOVCOMPL.CODCOLIGADA and TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV" +
            "",
        "INNER JOIN FCFO ON TMOV.CODCFO = FCFO.CODCFO",
        "INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD AND TITMMOV.CODCOLIGADA = T" +
            "PRODUTO.CODCOLPRD",
        "LEFT JOIN ZCARREGAMENTO ON ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO = ZCARREGAMENT" +
            "O.IDCARREGAMENTO",
        "WHERE ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = ?"};
            this.grid1.NomeGrid = "ZPROGRAMACAOCARREGAMENTO";
            this.grid1.Size = new System.Drawing.Size(624, 441);
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Selecionar;
            this.grid1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.grid1_SetParametros);
            this.grid1.Novo += new AppLib.Windows.GridData.NovoHandler(this.grid1_Novo);
            this.grid1.Editar += new AppLib.Windows.GridData.EditarHandler(this.grid1_Editar);
            this.grid1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.grid1_Excluir);
            this.grid1.AposAtualizar += new AppLib.Windows.GridData.AposAtualizarHandler(this.grid1_AposAtualizar);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Size = new System.Drawing.Size(624, 441);
            // 
            // FormRomaneioVisao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Name = "FormRomaneioVisao";
            this.Text = "Visão Romaneio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRomaneioVisao_FormClosed);
            this.Load += new System.EventHandler(this.FormRomaneioVisao_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}