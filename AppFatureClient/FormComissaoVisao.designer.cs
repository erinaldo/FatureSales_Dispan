using AppLib.Windows;

namespace AppFatureClient
{
    partial class FormComissaoVisao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComissaoVisao));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grid1 = new AppLib.Windows.GridData();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridData1 = new AppLib.Windows.GridData();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridData2 = new AppLib.Windows.GridData();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grid1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(710, 391);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 5;
            // 
            // grid1
            // 
            this.grid1.AutoAjuste = true;
            this.grid1.BotaoEditar = false;
            this.grid1.BotaoExcluir = false;
            this.grid1.BotaoNovo = false;
            this.grid1.Conexao = "Start";
            this.grid1.Consulta = new string[] {
        "SELECT \t   TM.CODCOLIGADA AS \'COLIGADA\',",
        "       \t   TM.IDMOV AS \'ID MOVIMENTO\',",
        "                      TM.STATUS,",
        "\t   TM.NUMEROMOV AS \'PEDIDO\',",
        "                      FCFO.CODCFO AS \'CÓDIGO CLIENTE\',",
        "\t   FCFO.NOMEFANTASIA AS \'CLIENTE\',",
        "\t   TRPR.NOMEFANTASIA AS \'REPRESENTANTE\',",
        "\t   TM.VALORLIQUIDOORIG AS \'VALOR PEDIDO\',",
        "\t   ZTC.VALORPAGO AS \'VALOR PAGO\',",
        "\t   (ZTC.VALORLIQ - ZTC.VALORPAGO) AS \'VALOR PENDENTE\',",
        "\t   ZTC.NCARTA AS \'NUMERO DA CARTA\',",
        "\t   ZTC.CONCLUIDO AS \'CONCLUIDO\',",
        "\t   ZTC.USERCRIACAO AS \'USUÁRIO DE CRIAÇÃO\',",
        "\t   ZTC.DATACARTA AS \'DATA DA CARTA\'",
        "\t   ",
        "FROM TMOV TM",
        "",
        "INNER JOIN FCFO",
        "ON FCFO.CODCFO = TM.CODCFO",
        "",
        "INNER JOIN TRPR ",
        "ON TRPR.CODRPR = TM.CODRPR",
        "",
        "LEFT JOIN ZTMOVCOMISSAO ZTC",
        "ON ZTC.IDMOV = TM.IDMOV",
        "AND ZTC.CODCOLIGADA = TM.CODCOLIGADA",
        "",
        "WHERE TM.CODTMV = \'2.1.10\'"};
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.Formatacao = null;
            this.grid1.FormPai = null;
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.ModoFiltro = false;
            this.grid1.Name = "grid1";
            this.grid1.NomeGrid = "ZVWCOMISSAO";
            this.grid1.SelecaoCascata = true;
            this.grid1.Selecionou = false;
            this.grid1.Size = new System.Drawing.Size(710, 391);
            this.grid1.TabIndex = 5;
            this.grid1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Selecionar;
            this.grid1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.grid1_Excluir);
            this.grid1.AposSelecao += new AppLib.Windows.GridData.AposSelecaoHandler(this.grid1_AposSelecao);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(150, 46);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridData1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(142, 20);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridData1
            // 
            this.gridData1.AutoAjuste = true;
            this.gridData1.BotaoEditar = false;
            this.gridData1.BotaoExcluir = true;
            this.gridData1.BotaoNovo = false;
            this.gridData1.Conexao = "Start";
            this.gridData1.Consulta = new string[] {
        "SELECT     top 0",
        "\tFLAN.CODCOLIGADA,",
        "\tFLAN.IDMOV,",
        "\tFLAN.IDLAN,",
        "\tFLAN.CODTDO,",
        "\tFLAN.NUMERODOCUMENTO,",
        "\tFLAN.VALORORIGINAL,",
        "\tFLANBAIXA.VALORNOTACREDITOADIANTAMENTO,",
        "\tFLAN.VALORBAIXADO,",
        "\tFLAN.STATUSLAN",
        "",
        "FROM FLAN",
        "",
        "left join FLANBAIXA ",
        "on FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA",
        "and FLANBAIXA.IDLAN = FLAN.IDLAN",
        "",
        "WHERE FLAN.CODCOLIGADA = 1",
        "AND FLAN.CODTDO = \'ADC\'"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = "";
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(136, 14);
            this.gridData1.TabIndex = 7;
            this.gridData1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData1_Excluir);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridData2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(142, 20);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridData2
            // 
            this.gridData2.AutoAjuste = true;
            this.gridData2.BotaoEditar = false;
            this.gridData2.BotaoExcluir = false;
            this.gridData2.BotaoNovo = false;
            this.gridData2.Conexao = "Start";
            this.gridData2.Consulta = new string[] {
        "select top 0",
        "           F.CODCOLIGADA,",
        "           F.IDLAN, ",
        "           F.STATUSLAN,",
        "           F.NUMERODOCUMENTO,",
        "           FCFO.NOME,",
        "           F.VALORORIGINAL,",
        "           F.VALORBAIXADO,",
        "           F.CODTDO,",
        "           F.DATAVENCIMENTO,",
        "           F.DATABAIXA,",
        "           F.CAMPOALFAOP3 as \'PEDIDO\',",
        "\t\t   FCFC1.CODCONTA as \'Conta 1\',",
        "\t       FCFC1.CLASSCONTA as \'Class Conta 1\',",
        "\t\t   FCFC2.CODCONTA as \'Conta 2\',",
        "\t\t   FCFC2.CLASSCONTA as \'Calss Conta 2\'",
        "",
        "from FLAN F",
        "",
        "left join FCFOCONT FCFC1",
        "on FCFC1.CODCFO = F.CODCFO",
        "and FCFC1.CODCONTA like \'1.01.03.%\'",
        "",
        "left join FCFOCONT FCFC2",
        "on FCFC2.CODCFO = F.CODCFO",
        "and FCFC2.CODCONTA like \'2.01.04.%\'",
        "",
        "inner join FCFO",
        "on FCFO.CODCFO = F.CODCFO",
        "",
        "where F.CODTDO = \'ADC\'",
        "and FCFC1.PAGREC = 1",
        "and FCFC2.PAGREC = 1"};
            this.gridData2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData2.Formatacao = null;
            this.gridData2.FormPai = null;
            this.gridData2.Location = new System.Drawing.Point(3, 3);
            this.gridData2.ModoFiltro = false;
            this.gridData2.Name = "gridData2";
            this.gridData2.NomeGrid = "";
            this.gridData2.SelecaoCascata = true;
            this.gridData2.Selecionou = false;
            this.gridData2.Size = new System.Drawing.Size(136, 14);
            this.gridData2.TabIndex = 8;
            this.gridData2.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData2.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            // 
            // FormComissaoVisao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 391);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormComissaoVisao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de ADC";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormVisao_FormClosed);
            this.Load += new System.EventHandler(this.FormConsulta_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.SplitContainer splitContainer1;
        public GridData grid1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public GridData gridData1;
        private System.Windows.Forms.TabPage tabPage2;
        public GridData gridData2;
    }
}