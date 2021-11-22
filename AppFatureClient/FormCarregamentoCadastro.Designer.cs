namespace AppFatureClient
{
    partial class FormCarregamentoCadastro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCarregamentoCadastro));
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.campoData2 = new AppLib.Windows.CampoData();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto8 = new AppLib.Windows.CampoTexto();
            this.campoTexto6 = new AppLib.Windows.CampoTexto();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto5 = new AppLib.Windows.CampoTexto();
            this.txtDataBaixa = new AppLib.Windows.CampoTexto();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.campoHora1 = new AppLib.Windows.CampoHora();
            this.campoData1 = new AppLib.Windows.CampoData();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto4 = new AppLib.Windows.CampoTexto();
            this.campoTexto3 = new AppLib.Windows.CampoTexto();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.campoLookup1 = new AppLib.Windows.CampoLookup();
            this.campoIdCarregamento = new AppLib.Windows.CampoInteiro();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto2 = new AppLib.Windows.CampoTexto();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gridData2 = new AppLib.Windows.GridData();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.gridData3 = new AppLib.Windows.GridData();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridData1 = new AppLib.Windows.GridData();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridData4 = new AppLib.Windows.GridData();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1354, 155);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoData2);
            this.tabPage1.Controls.Add(this.labelControl9);
            this.tabPage1.Controls.Add(this.campoTexto8);
            this.tabPage1.Controls.Add(this.campoTexto6);
            this.tabPage1.Controls.Add(this.labelControl7);
            this.tabPage1.Controls.Add(this.campoTexto5);
            this.tabPage1.Controls.Add(this.txtDataBaixa);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.campoHora1);
            this.tabPage1.Controls.Add(this.campoData1);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.campoTexto4);
            this.tabPage1.Controls.Add(this.campoTexto3);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Controls.Add(this.campoLookup1);
            this.tabPage1.Controls.Add(this.campoIdCarregamento);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.campoTexto2);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1346, 129);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // campoData2
            // 
            this.campoData2.Campo = "DATAAGENDAMENTO";
            this.campoData2.Default = null;
            this.campoData2.Edita = false;
            this.campoData2.Enabled = false;
            this.campoData2.Location = new System.Drawing.Point(699, 28);
            this.campoData2.Name = "campoData2";
            this.campoData2.Query = 0;
            this.campoData2.Size = new System.Drawing.Size(136, 20);
            this.campoData2.Tabela = "ZCARREGAMENTO";
            this.campoData2.TabIndex = 42;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(699, 10);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(136, 13);
            this.labelControl9.TabIndex = 43;
            this.labelControl9.Text = "Data Autorização / Chegada";
            // 
            // campoTexto8
            // 
            this.campoTexto8.Campo = "STATUS";
            this.campoTexto8.Default = "";
            this.campoTexto8.Edita = false;
            this.campoTexto8.Location = new System.Drawing.Point(1197, 2);
            this.campoTexto8.MaximoCaracteres = null;
            this.campoTexto8.Name = "campoTexto8";
            this.campoTexto8.Query = 0;
            this.campoTexto8.Size = new System.Drawing.Size(18, 20);
            this.campoTexto8.Tabela = "ZCARREGAMENTO";
            this.campoTexto8.TabIndex = 41;
            this.campoTexto8.Visible = false;
            // 
            // campoTexto6
            // 
            this.campoTexto6.Campo = "NUMERO";
            this.campoTexto6.Default = null;
            this.campoTexto6.Edita = false;
            this.campoTexto6.Location = new System.Drawing.Point(145, 28);
            this.campoTexto6.MaximoCaracteres = 7;
            this.campoTexto6.Name = "campoTexto6";
            this.campoTexto6.Query = 0;
            this.campoTexto6.Size = new System.Drawing.Size(109, 20);
            this.campoTexto6.Tabela = "ZCARREGAMENTO";
            this.campoTexto6.TabIndex = 1;
            this.campoTexto6.Validating += new System.ComponentModel.CancelEventHandler(this.campoTexto6_Validating);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(145, 9);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(37, 13);
            this.labelControl7.TabIndex = 36;
            this.labelControl7.Text = "Numero";
            // 
            // campoTexto5
            // 
            this.campoTexto5.Campo = "TIPOCARREGAMENTO";
            this.campoTexto5.Default = "V";
            this.campoTexto5.Edita = true;
            this.campoTexto5.Location = new System.Drawing.Point(1197, 54);
            this.campoTexto5.MaximoCaracteres = null;
            this.campoTexto5.Name = "campoTexto5";
            this.campoTexto5.Query = 0;
            this.campoTexto5.Size = new System.Drawing.Size(18, 20);
            this.campoTexto5.Tabela = "ZCARREGAMENTO";
            this.campoTexto5.TabIndex = 29;
            this.campoTexto5.Visible = false;
            // 
            // txtDataBaixa
            // 
            this.txtDataBaixa.Campo = "DATABAIXA";
            this.txtDataBaixa.Default = null;
            this.txtDataBaixa.Edita = true;
            this.txtDataBaixa.Location = new System.Drawing.Point(1197, 28);
            this.txtDataBaixa.MaximoCaracteres = null;
            this.txtDataBaixa.Name = "txtDataBaixa";
            this.txtDataBaixa.Query = 0;
            this.txtDataBaixa.Size = new System.Drawing.Size(18, 20);
            this.txtDataBaixa.Tabela = "ZCARREGAMENTO";
            this.txtDataBaixa.TabIndex = 28;
            this.txtDataBaixa.Visible = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(699, 61);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(23, 13);
            this.labelControl6.TabIndex = 27;
            this.labelControl6.Text = "Hora";
            // 
            // campoHora1
            // 
            this.campoHora1.Campo = "HORA";
            this.campoHora1.Default = null;
            this.campoHora1.Edita = true;
            this.campoHora1.Location = new System.Drawing.Point(699, 80);
            this.campoHora1.Name = "campoHora1";
            this.campoHora1.Query = 0;
            this.campoHora1.Size = new System.Drawing.Size(170, 20);
            this.campoHora1.Tabela = "ZCARREGAMENTO";
            this.campoHora1.TabIndex = 4;
            // 
            // campoData1
            // 
            this.campoData1.Campo = "DATA";
            this.campoData1.Default = null;
            this.campoData1.Edita = true;
            this.campoData1.Location = new System.Drawing.Point(575, 79);
            this.campoData1.Name = "campoData1";
            this.campoData1.Query = 0;
            this.campoData1.Size = new System.Drawing.Size(115, 20);
            this.campoData1.Tabela = "ZCARREGAMENTO";
            this.campoData1.TabIndex = 3;
            this.campoData1.Validating += new System.ComponentModel.CancelEventHandler(this.campoData1_Validating);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(575, 61);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(93, 13);
            this.labelControl5.TabIndex = 25;
            this.labelControl5.Text = "Data Agendamento";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(145, 60);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(58, 13);
            this.labelControl4.TabIndex = 24;
            this.labelControl4.Text = "Observação";
            // 
            // campoTexto4
            // 
            this.campoTexto4.Campo = "CODCOLIGADA";
            this.campoTexto4.Default = "";
            this.campoTexto4.Edita = true;
            this.campoTexto4.Location = new System.Drawing.Point(1197, 91);
            this.campoTexto4.MaximoCaracteres = null;
            this.campoTexto4.Name = "campoTexto4";
            this.campoTexto4.Query = 0;
            this.campoTexto4.Size = new System.Drawing.Size(18, 19);
            this.campoTexto4.Tabela = "ZCARREGAMENTO";
            this.campoTexto4.TabIndex = 22;
            this.campoTexto4.Visible = false;
            // 
            // campoTexto3
            // 
            this.campoTexto3.Campo = "CODTRA";
            this.campoTexto3.Default = null;
            this.campoTexto3.Edita = true;
            this.campoTexto3.Location = new System.Drawing.Point(1197, 80);
            this.campoTexto3.MaximoCaracteres = null;
            this.campoTexto3.Name = "campoTexto3";
            this.campoTexto3.Query = 0;
            this.campoTexto3.Size = new System.Drawing.Size(18, 19);
            this.campoTexto3.Tabela = "ZCARREGAMENTO";
            this.campoTexto3.TabIndex = 20;
            this.campoTexto3.Visible = false;
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "OBS";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(142, 79);
            this.campoTexto1.MaximoCaracteres = 255;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(427, 20);
            this.campoTexto1.Tabela = "ZCARREGAMENTO";
            this.campoTexto1.TabIndex = 7;
            // 
            // campoLookup1
            // 
            this.campoLookup1.Campo = "CODTRA";
            this.campoLookup1.ColunaCodigo = "CODTRA";
            this.campoLookup1.ColunaDescricao = "NOME";
            this.campoLookup1.ColunaIdentificador = null;
            this.campoLookup1.ColunaTabela = "TTRA";
            this.campoLookup1.Conexao = "Start";
            this.campoLookup1.Default = null;
            this.campoLookup1.Edita = true;
            this.campoLookup1.EditaLookup = false;
            this.campoLookup1.Location = new System.Drawing.Point(260, 28);
            this.campoLookup1.MaximoCaracteres = null;
            this.campoLookup1.Name = "campoLookup1";
            this.campoLookup1.NomeGrid = null;
            this.campoLookup1.Query = 0;
            this.campoLookup1.Size = new System.Drawing.Size(430, 20);
            this.campoLookup1.Tabela = "TTRA";
            this.campoLookup1.TabIndex = 5;
            this.campoLookup1.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookup1_SetFormConsulta);
            this.campoLookup1.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookup1_SetDescricao);
            // 
            // campoIdCarregamento
            // 
            this.campoIdCarregamento.Campo = "IDCARREGAMENTO";
            this.campoIdCarregamento.Default = null;
            this.campoIdCarregamento.Edita = false;
            this.campoIdCarregamento.Location = new System.Drawing.Point(24, 28);
            this.campoIdCarregamento.Name = "campoIdCarregamento";
            this.campoIdCarregamento.Query = 0;
            this.campoIdCarregamento.Size = new System.Drawing.Size(109, 20);
            this.campoIdCarregamento.Tabela = "ZCARREGAMENTO";
            this.campoIdCarregamento.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 13);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "Id Carregamento";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(24, 61);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(25, 13);
            this.labelControl3.TabIndex = 21;
            this.labelControl3.Text = "Placa";
            // 
            // campoTexto2
            // 
            this.campoTexto2.Campo = "PLACA";
            this.campoTexto2.Default = null;
            this.campoTexto2.Edita = true;
            this.campoTexto2.Location = new System.Drawing.Point(24, 79);
            this.campoTexto2.MaximoCaracteres = 9;
            this.campoTexto2.Name = "campoTexto2";
            this.campoTexto2.Query = 0;
            this.campoTexto2.Size = new System.Drawing.Size(109, 20);
            this.campoTexto2.Tabela = "ZCARREGAMENTO";
            this.campoTexto2.TabIndex = 6;
            this.campoTexto2.Validating += new System.ComponentModel.CancelEventHandler(this.campoTexto2_Validating_1);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(260, 10);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(75, 13);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "Transportadora";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 194);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl3);
            this.splitContainer1.Size = new System.Drawing.Size(1354, 478);
            this.splitContainer1.SplitterDistance = 696;
            this.splitContainer1.TabIndex = 20;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(696, 478);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.gridData2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(688, 452);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Itens Carregamento";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // gridData2
            // 
            this.gridData2.AutoAjuste = true;
            this.gridData2.BotaoEditar = false;
            this.gridData2.BotaoExcluir = false;
            this.gridData2.BotaoNovo = false;
            this.gridData2.Conexao = "Start";
            this.gridData2.Consulta = new string[] {
        "SELECT ",
        "ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO,",
        "ZPROGRAMACAOCARREGAMENTO.IDMOV,",
        "ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV,",
        "TMOV.NUMEROMOV,",
        "FCFO.NOMEFANTASIA AS CLIENTE,",
        "TPRODUTO.CODIGOPRD,",
        "TPRODUTO.NOMEFANTASIA AS PRODUTO,",
        "ZPROGRAMACAOCARREGAMENTO.QTDE,",
        "ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA,",
        "ZPROGRAMACAOCARREGAMENTO.CAMINHAO,",
        "ZPROGRAMACAOCARREGAMENTO.TIPOCAMINHAO,",
        "ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA,",
        "TMOV.CODTMV",
        "FROM",
        "ZPROGRAMACAOCARREGAMENTO ",
        "INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAM" +
            "ACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV AND ZPROGRAMACAOCARREGAMENTO.CO" +
            "DCOLIGADA = TITMMOV.CODCOLIGADA",
        "INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD ",
        "INNER JOIN TMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TMOV.IDMOV",
        "INNER JOIN FCFO ON FCFO.CODCFO = TMOV.CODCFO",
        "WHERE ",
        "ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO = ?",
        "AND ZPROGRAMACAOCARREGAMENTO.CARREGAMENTO = \'S\'"};
            this.gridData2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData2.Formatacao = null;
            this.gridData2.FormPai = null;
            this.gridData2.Location = new System.Drawing.Point(3, 3);
            this.gridData2.ModoFiltro = false;
            this.gridData2.Name = "gridData2";
            this.gridData2.NomeGrid = null;
            this.gridData2.SelecaoCascata = true;
            this.gridData2.Selecionou = false;
            this.gridData2.Size = new System.Drawing.Size(682, 446);
            this.gridData2.TabIndex = 0;
            this.gridData2.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData2.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData2.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData2_SetParametros);
            this.gridData2.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData2_Editar);
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage5);
            this.tabControl3.Controls.Add(this.tabPage2);
            this.tabControl3.Controls.Add(this.tabPage3);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(654, 478);
            this.tabControl3.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.gridData3);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(646, 452);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Itens Programados";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // gridData3
            // 
            this.gridData3.AutoAjuste = true;
            this.gridData3.BotaoEditar = false;
            this.gridData3.BotaoExcluir = false;
            this.gridData3.BotaoNovo = false;
            this.gridData3.Conexao = "Start";
            this.gridData3.Consulta = new string[] {
        "SELECT ",
        "ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO,",
        "ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA,",
        "ZPROGRAMACAOCARREGAMENTO.IDMOV,",
        "ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV,",
        "TMOV.NUMEROMOV,",
        "FCFO.NOMEFANTASIA AS CLIENTE,",
        "TPRODUTO.CODIGOPRD,",
        "TPRODUTO.NOMEFANTASIA AS PRODUTO,",
        "ZPROGRAMACAOCARREGAMENTO.QTDE,",
        "ZPROGRAMACAOCARREGAMENTO.CAMINHAO,",
        "ZPROGRAMACAOCARREGAMENTO.TIPOCAMINHAO,",
        "ZPROGRAMACAOCARREGAMENTO.CARREGAMENTO,",
        "TMOV.CODTMV,",
        "TITMMOVCOMPL.OBSPRDPADRAO",
        "FROM",
        "TMOV ",
        "INNER JOIN ZPROGRAMACAOCARREGAMENTO ON TMOV.IDMOV = ZPROGRAMACAOCARREGAMENTO.IDMO" +
            "V",
        "",
        "INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAM" +
            "ACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA AND ZPROGRAMACAOCARREGAMENTO." +
            "NSEQITMMOV = TITMMOV.NSEQITMMOV",
        "INNER JOIN TITMMOVCOMPL ON TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV AND TITMMOV.CODCOLI" +
            "GADA = TITMMOVCOMPL.CODCOLIGADA and TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV" +
            "",
        "INNER JOIN FCFO ON TMOV.CODCFO = FCFO.CODCFO",
        "INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD AND TITMMOV.CODCOLIGADA = T" +
            "PRODUTO.CODCOLPRD",
        "WHERE ",
        "ZPROGRAMACAOCARREGAMENTO.CARREGAMENTO IS NULL",
        "OR ZPROGRAMACAOCARREGAMENTO.CARREGAMENTO = \'N\'"};
            this.gridData3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData3.Formatacao = null;
            this.gridData3.FormPai = null;
            this.gridData3.Location = new System.Drawing.Point(3, 3);
            this.gridData3.ModoFiltro = false;
            this.gridData3.Name = "gridData3";
            this.gridData3.NomeGrid = null;
            this.gridData3.SelecaoCascata = true;
            this.gridData3.Selecionou = false;
            this.gridData3.Size = new System.Drawing.Size(640, 446);
            this.gridData3.TabIndex = 0;
            this.gridData3.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData3.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData3.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData3_SetParametros);
            this.gridData3.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData3_Editar);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridData1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(646, 452);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Programação";
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
        "SELECT DISTINCT",
        "LISTA.CODCOLIGADA,",
        "LISTA.IDMOV,",
        "LISTA.NSEQITMMOV,",
        "LISTA.NUMEROMOV,",
        "LISTA.CODIGOPRD,",
        "LISTA.DATAENTREGA,",
        "LISTA.DESCRICAO DESCRICAO_DO_PRODUTO,",
        "LISTA.IDPRDCOMPOSTO,",
        "CAST(QTD_PEDIDO AS NUMERIC(15,2)) QTD_PEDIDO,",
        "CAST(QTD_SALDO AS NUMERIC(15,2)) QTD_SALDO,",
        "LISTA.STATUS,",
        "CONVERT(DECIMAL(15,2),QTD_PROG) QTD_PROG,",
        "CONVERT(DECIMAL(15,2), QTD_CARREGADA) QTD_CARREGADA ",
        "\tFROM ",
        "\t(",
        "\t\tSELECT SUBLISTA.*,",
        "\t\t(",
        "\t\t\tQTD_PEDIDO -  (ISNULL(QTD_PROG, 0) + ISNULL(QTD_CARREGADA, 0))) QTD_SALDO",
        "\t\t\t\tFROM",
        "\t\t\t\t(",
        "\t\t\t\t\tSELECT",
        "\t\t\t\t\t\tTITMMOV.IDMOV,",
        "\t\t\t\t\t\tTITMMOV.NSEQITMMOV,",
        "\t\t\t\t\t\tFCFO.NOMEFANTASIA,",
        "\t\t\t\t\t\tTMOV.NUMEROMOV,",
        "\t\t\t\t\t\tTMOV.DATAENTREGA,",
        "\t\t\t\t\t\tTPRODUTO.DESCRICAO,",
        "\t\t\t\t\t\tTITMMOV.QUANTIDADEORIGINAL QTD_PEDIDO,",
        "\t\t\t\t\t\tTMOV.CODTMV,",
        "\t\t\t\t\t\tTPRODUTO.CODIGOPRD,",
        "\t\t\t\t\t\tTITMMOV.IDPRDCOMPOSTO,",
        "\t\t\t\t\t\tTITMMOV.CODCOLIGADA,",
        "\t\t\t\t\t\tTMOV.STATUS,",
        "\t\t\t\t\t\t(",
        "\t\t\t\t\t\t\tSELECT SUM(ZCARREGAMENTOITEMS.QTDE)",
        "\t\t\t\t\t\t\tFROM ZCARREGAMENTOITEMS",
        "\t\t\t\t\t\t\tINNER JOIN TITMMOV ON ZCARREGAMENTOITEMS.CODCOLIGADA = TITMMOV.CODCOLIGADA" +
            " AND ZCARREGAMENTOITEMS.IDMOV = TITMMOV.IDMOV AND ZCARREGAMENTOITEMS.NSEQITMMOV " +
            "= TITMMOV.NSEQITMMOV",
        "\t\t\t\t\t\t) QTD_CARREGADA,",
        "\t\t\t\t\t\t(",
        "\t\t\t\t\t\t\tSELECT SUM(ZPROGRAMACAOCARREGAMENTO.QTDE)",
        "\t\t\t\t\t\t\tFROM  ZPROGRAMACAOCARREGAMENTO",
        "\t\t\t\t\t\t\tINNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCO" +
            "LIGADA AND ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAMACAOCARREG" +
            "AMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV",
        "\t\t\t\t\t\t ) QTD_PROG",
        "\t\t\t\tFROM ",
        "\t\t\t\t\tTMOV",
        "\t\t\t\t\tINNER JOIN TITMMOV ON TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA AND TMOV.IDMOV " +
            "= TITMMOV.IDMOV",
        "\t\t\t\t\tINNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD",
        "\t\t\t\t\tINNER JOIN FCFO ON TMOV.CODCOLCFO = FCFO.CODCOLIGADA AND TMOV.CODCFO = FCFO." +
            "CODCFO",
        resources.GetString("gridData1.Consulta"),
        "\t\t\t\tWHERE ",
        "\t\t\t\tTITMMOV.IDMOV <> ZPROGRAMACAOCARREGAMENTO.IDMOV",
        "\t\t\t\tAND TITMMOV.NSEQITMMOV <> ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV",
        "\t\t\t\t)SUBLISTA\t",
        "\t) LISTA",
        "WHERE",
        "LISTA.STATUS IN (\'A\', \'G\')",
        "AND LISTA.CODIGOPRD NOT LIKE \'01.001%\'",
        "AND LISTA.CODIGOPRD NOT LIKE \'01.004%\'",
        "AND LISTA.QTD_PEDIDO > 0",
        "AND LISTA.QTD_SALDO > 0",
        "AND CODTMV IN (\'2.1.30\', \'2.1.20\', \'2.1.05\')",
        "AND LISTA.CODCOLIGADA = ?"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = null;
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(640, 446);
            this.gridData1.TabIndex = 0;
            this.gridData1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Nenhum;
            this.gridData1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData1_SetParametros);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridData4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(646, 452);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Reprogramar";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gridData4
            // 
            this.gridData4.AutoAjuste = true;
            this.gridData4.BotaoEditar = false;
            this.gridData4.BotaoExcluir = false;
            this.gridData4.BotaoNovo = false;
            this.gridData4.Conexao = "Start";
            this.gridData4.Consulta = new string[] {
        "SELECT ",
        "ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO,",
        "ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTOANTERIOR,",
        "ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA,",
        "ZPROGRAMACAOCARREGAMENTO.IDMOV,",
        "ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV,",
        "TMOV.NUMEROMOV,",
        "FCFO.NOMEFANTASIA AS CLIENTE,",
        "TPRODUTO.CODIGOPRD,",
        "TPRODUTO.NOMEFANTASIA AS PRODUTO,",
        "ZPROGRAMACAOCARREGAMENTO.QTDE,",
        "ZPROGRAMACAOCARREGAMENTO.CAMINHAO,",
        "ZPROGRAMACAOCARREGAMENTO.TIPOCAMINHAO,",
        "ZPROGRAMACAOCARREGAMENTO.CARREGAMENTO,",
        "TITMMOVCOMPL.OBSPRDPADRAO",
        "FROM",
        "TMOV ",
        "INNER JOIN ZPROGRAMACAOCARREGAMENTO ON TMOV.IDMOV = ZPROGRAMACAOCARREGAMENTO.IDMO" +
            "V",
        "INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAM" +
            "ACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA AND ZPROGRAMACAOCARREGAMENTO." +
            "NSEQITMMOV = TITMMOV.NSEQITMMOV",
        "INNER JOIN TITMMOVCOMPL ON TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV AND TITMMOV.CODCOLI" +
            "GADA = TITMMOVCOMPL.CODCOLIGADA and TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV" +
            "",
        "INNER JOIN FCFO ON TMOV.CODCFO = FCFO.CODCFO",
        "INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD AND TITMMOV.CODCOLIGADA = T" +
            "PRODUTO.CODCOLPRD",
        "WHERE ",
        "ZPROGRAMACAOCARREGAMENTO.REPROGRAMAR = 1"};
            this.gridData4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData4.Formatacao = null;
            this.gridData4.FormPai = null;
            this.gridData4.Location = new System.Drawing.Point(3, 3);
            this.gridData4.ModoFiltro = false;
            this.gridData4.Name = "gridData4";
            this.gridData4.NomeGrid = "REPROGRAMAR";
            this.gridData4.SelecaoCascata = true;
            this.gridData4.Selecionou = false;
            this.gridData4.Size = new System.Drawing.Size(640, 446);
            this.gridData4.TabIndex = 0;
            this.gridData4.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData4.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            // 
            // FormCarregamentoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 733);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tabControl1);
            this.IsMdiContainer = true;
            this.Name = "FormCarregamentoCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT * FROM ZCARREGAMENTO WHERE IDCARREGAMENTO = ?"};
            query1.Parametros = new string[] {
        "IDCARREGAMENTO"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZCARREGAMENTO";
            this.Text = "Cadastro Carregamento Venda";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ValidarSalvar += new AppLib.Windows.FormCadastroData.ValidarSalvarHandler(this.FormCarregamentoCadastroVenda_ValidarSalvar);
            this.ValidarExcluir += new AppLib.Windows.FormCadastroData.ValidarExcluirHandler(this.FormCarregamentoCadastro_ValidarExcluir);
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormCarregamentoCadastro_AntesSalvar);
            this.Load += new System.EventHandler(this.FormCarregamentoCadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private AppLib.Windows.CampoTexto txtDataBaixa;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private AppLib.Windows.CampoHora campoHora1;
        private AppLib.Windows.CampoTexto campoTexto4;
        private AppLib.Windows.CampoTexto campoTexto3;
        private AppLib.Windows.CampoData campoData1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private AppLib.Windows.CampoTexto campoTexto1;
        private AppLib.Windows.CampoLookup campoLookup1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private AppLib.Windows.CampoTexto campoTexto2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private AppLib.Windows.CampoInteiro campoIdCarregamento;
        private AppLib.Windows.CampoTexto campoTexto5;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private AppLib.Windows.CampoTexto campoTexto6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage5;
        private AppLib.Windows.GridData gridData2;
        private AppLib.Windows.GridData gridData3;
        private AppLib.Windows.CampoTexto campoTexto8;
        private System.Windows.Forms.TabPage tabPage2;
        private AppLib.Windows.GridData gridData1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private System.Windows.Forms.TabPage tabPage3;
        private AppLib.Windows.GridData gridData4;
        public AppLib.Windows.CampoData campoData2;
    }
}