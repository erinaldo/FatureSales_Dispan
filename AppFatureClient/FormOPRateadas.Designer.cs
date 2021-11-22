namespace AppFatureClient
{
    partial class FormOPRateadas
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
            AppLib.Windows.GridProps gridProps1 = new AppLib.Windows.GridProps();
            AppLib.Windows.GridProps gridProps2 = new AppLib.Windows.GridProps();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::AppFatureClient.WaitForm2), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.BotaoExcluir = true;
            this.grid1.BotaoNovo = true;
            this.grid1.Consulta = new string[] {
        "select ZOR.*,",
        "           TM.DATAEMISSAO,",
        "           KAO.IDPRODUTO,",
        "           TP.NOMEFANTASIA,",
        "           KME.IDMOV as \'IDMOVBAIXA\',",
        "           KME.DATAHORA as \'DATABAIXA\'    ",
        "    from ZOPRATEADAS ZOR",
        "",
        "    left join (select CODCOLIGADA, CODORDEM, IDMOV, DATAHORA from KMOVESTOQUE whe" +
            "re IDPRODUTO IN (81725, 82181, 83805,83950,83916)) KME",
        "    on KME.CODCOLIGADA = ZOR.CODCOLIGADA",
        "    and KME.CODORDEM = ZOR.CODORDEM",
        "",
        "    inner join KATVORDEMMP KAO",
        "    on KAO.CODCOLIGADA = ZOR.CODCOLIGADA",
        "    and KAO.CODFILIAL = ZOR.CODFILIAL",
        "    and KAO.IDATVORDEMMATERIAPRIMA = ZOR.IDKAO",
        "",
        "    inner join TPRODUTO TP",
        "    on TP.CODCOLPRD = ZOR.CODCOLIGADA",
        "    and TP.IDPRD = KAO.IDPRODUTO",
        "",
        "    inner join TMOV TM",
        "    on TM.CODCOLIGADA = ZOR.CODCOLIGADA",
        "    and TM.CODFILIAL = ZOR.CODFILIAL",
        "    and TM.IDMOV = ZOR.IDMOV"};
            gridProps1.Agrupar = false;
            gridProps1.Alinhamento = AppLib.Windows.Alinhamento.Esquerda;
            gridProps1.Coluna = "PRECO1";
            gridProps1.Formato = AppLib.Windows.Formato.Decimal2;
            gridProps1.Largura = 50;
            gridProps1.Sequencia = 0;
            gridProps1.Visivel = true;
            gridProps2.Agrupar = false;
            gridProps2.Alinhamento = AppLib.Windows.Alinhamento.Esquerda;
            gridProps2.Coluna = "ALIQ_IPI";
            gridProps2.Formato = AppLib.Windows.Formato.Decimal2;
            gridProps2.Largura = 50;
            gridProps2.Sequencia = 0;
            gridProps2.Visivel = true;
            this.grid1.Formatacao = new AppLib.Windows.GridProps[] {
        gridProps1,
        gridProps2};
            this.grid1.NomeGrid = "ZOPRATEADAS";
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
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // FormOPRateadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Name = "FormOPRateadas";
            this.Text = "Ordens de Produção Rateadas";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormOPRateadas_FormClosed);
            this.Load += new System.EventHandler(this.FormOPRateadas_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}