namespace AppFatureClient
{
    partial class FormFamiliaProdutoCadastro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFamiliaProdutoCadastro));
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ceAnaliseItemCompra = new DevExpress.XtraEditors.CheckEdit();
            this.campoInteiroCODCOLIGADA = new AppLib.Windows.CampoInteiro();
            this.label23 = new System.Windows.Forms.Label();
            this.campoTextoNOME = new AppLib.Windows.CampoTexto();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.campoTextoCODTRA = new AppLib.Windows.CampoTexto();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.campoDecimalPercDescontoRepr = new AppLib.Windows.CampoDecimal();
            this.label3 = new System.Windows.Forms.Label();
            this.campoTextoDescricaoRepr = new AppLib.Windows.CampoTexto();
            this.label58 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnNovo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPesquisar = new System.Windows.Forms.ToolStripButton();
            this.btnAgrupar = new System.Windows.Forms.ToolStripButton();
            this.btnFiltros = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnAtualizar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelecionarColunas = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSalvarLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridData1 = new AppLib.Windows.GridData();
            this.campoLookupCODRPR = new AppLib.Windows.CampoLookup();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceAnaliseItemCompra.Properties)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(742, 427);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ceAnaliseItemCompra);
            this.tabPage1.Controls.Add(this.campoInteiroCODCOLIGADA);
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.campoTextoNOME);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.campoTextoCODTRA);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(734, 401);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ceAnaliseItemCompra
            // 
            this.ceAnaliseItemCompra.EditValue = true;
            this.ceAnaliseItemCompra.Location = new System.Drawing.Point(411, 30);
            this.ceAnaliseItemCompra.Name = "ceAnaliseItemCompra";
            this.ceAnaliseItemCompra.Properties.Caption = "Aparece na analise de compra";
            this.ceAnaliseItemCompra.Size = new System.Drawing.Size(165, 19);
            this.ceAnaliseItemCompra.TabIndex = 28;
            this.ceAnaliseItemCompra.CheckedChanged += new System.EventHandler(this.ceAnaliseItemCompra_CheckedChanged);
            // 
            // campoInteiroCODCOLIGADA
            // 
            this.campoInteiroCODCOLIGADA.Campo = "CODCOLIGADA";
            this.campoInteiroCODCOLIGADA.Default = null;
            this.campoInteiroCODCOLIGADA.Edita = true;
            this.campoInteiroCODCOLIGADA.Location = new System.Drawing.Point(225, 29);
            this.campoInteiroCODCOLIGADA.Name = "campoInteiroCODCOLIGADA";
            this.campoInteiroCODCOLIGADA.Query = 0;
            this.campoInteiroCODCOLIGADA.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroCODCOLIGADA.Tabela = "TTB4";
            this.campoInteiroCODCOLIGADA.TabIndex = 23;
            this.campoInteiroCODCOLIGADA.Visible = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(222, 12);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(96, 13);
            this.label23.TabIndex = 27;
            this.label23.Text = "Coligada (invisivel)";
            this.label23.Visible = false;
            // 
            // campoTextoNOME
            // 
            this.campoTextoNOME.Campo = "DESCRICAO";
            this.campoTextoNOME.Default = null;
            this.campoTextoNOME.Edita = true;
            this.campoTextoNOME.Location = new System.Drawing.Point(20, 68);
            this.campoTextoNOME.MaximoCaracteres = 100;
            this.campoTextoNOME.Name = "campoTextoNOME";
            this.campoTextoNOME.Query = 0;
            this.campoTextoNOME.Size = new System.Drawing.Size(683, 20);
            this.campoTextoNOME.Tabela = "TTB4";
            this.campoTextoNOME.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Descrição";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Código";
            // 
            // campoTextoCODTRA
            // 
            this.campoTextoCODTRA.Campo = "CODTB4FAT";
            this.campoTextoCODTRA.Default = null;
            this.campoTextoCODTRA.Edita = true;
            this.campoTextoCODTRA.Location = new System.Drawing.Point(20, 29);
            this.campoTextoCODTRA.MaximoCaracteres = 10;
            this.campoTextoCODTRA.Name = "campoTextoCODTRA";
            this.campoTextoCODTRA.Query = 0;
            this.campoTextoCODTRA.Size = new System.Drawing.Size(100, 20);
            this.campoTextoCODTRA.Tabela = "TTB4";
            this.campoTextoCODTRA.TabIndex = 22;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(734, 401);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Representante";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.campoLookupCODRPR);
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.campoDecimalPercDescontoRepr);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.campoTextoDescricaoRepr);
            this.splitContainer1.Panel1.Controls.Add(this.label58);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(728, 395);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(639, 74);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 56;
            this.btnAdd.Text = "Adicionar";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(532, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "Perc. Desconto";
            // 
            // campoDecimalPercDescontoRepr
            // 
            this.campoDecimalPercDescontoRepr.Campo = null;
            this.campoDecimalPercDescontoRepr.Decimais = 2;
            this.campoDecimalPercDescontoRepr.Default = null;
            this.campoDecimalPercDescontoRepr.Edita = true;
            this.campoDecimalPercDescontoRepr.Location = new System.Drawing.Point(533, 75);
            this.campoDecimalPercDescontoRepr.Name = "campoDecimalPercDescontoRepr";
            this.campoDecimalPercDescontoRepr.Query = 0;
            this.campoDecimalPercDescontoRepr.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalPercDescontoRepr.Tabela = null;
            this.campoDecimalPercDescontoRepr.TabIndex = 54;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Descrição";
            // 
            // campoTextoDescricaoRepr
            // 
            this.campoTextoDescricaoRepr.Campo = null;
            this.campoTextoDescricaoRepr.Default = null;
            this.campoTextoDescricaoRepr.Edita = true;
            this.campoTextoDescricaoRepr.Location = new System.Drawing.Point(14, 75);
            this.campoTextoDescricaoRepr.MaximoCaracteres = null;
            this.campoTextoDescricaoRepr.Name = "campoTextoDescricaoRepr";
            this.campoTextoDescricaoRepr.Query = 0;
            this.campoTextoDescricaoRepr.Size = new System.Drawing.Size(513, 20);
            this.campoTextoDescricaoRepr.Tabela = null;
            this.campoTextoDescricaoRepr.TabIndex = 52;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(11, 16);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(77, 13);
            this.label58.TabIndex = 51;
            this.label58.Text = "Representante";
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
            this.splitContainer2.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gridControl1);
            this.splitContainer2.Size = new System.Drawing.Size(728, 281);
            this.splitContainer2.SplitterDistance = 30;
            this.splitContainer2.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNovo,
            this.btnEditar,
            this.btnExcluir,
            this.toolStripSeparator1,
            this.btnPesquisar,
            this.btnAgrupar,
            this.btnFiltros,
            this.toolStripDropDownButton1,
            this.toolStripSeparator2,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton3,
            this.toolStripDropDownButton4});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(728, 30);
            this.toolStrip2.TabIndex = 7;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnNovo
            // 
            this.btnNovo.Enabled = false;
            this.btnNovo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo.Image")));
            this.btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(58, 27);
            this.btnNovo.Text = "Novo";
            // 
            // btnEditar
            // 
            this.btnEditar.Enabled = false;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(61, 27);
            this.btnEditar.Text = "Editar";
            // 
            // btnExcluir
            // 
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(64, 27);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Enabled = false;
            this.btnPesquisar.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisar.Image")));
            this.btnPesquisar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(79, 27);
            this.btnPesquisar.Text = "Pesquisar";
            // 
            // btnAgrupar
            // 
            this.btnAgrupar.Enabled = false;
            this.btnAgrupar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgrupar.Image")));
            this.btnAgrupar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgrupar.Name = "btnAgrupar";
            this.btnAgrupar.Size = new System.Drawing.Size(72, 27);
            this.btnAgrupar.Text = "Agrupar";
            // 
            // btnFiltros
            // 
            this.btnFiltros.Enabled = false;
            this.btnFiltros.Image = ((System.Drawing.Image)(resources.GetObject("btnFiltros.Image")));
            this.btnFiltros.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFiltros.Name = "btnFiltros";
            this.btnFiltros.Size = new System.Drawing.Size(62, 27);
            this.btnFiltros.Text = "Filtros";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAtualizar,
            this.btnSelecionarColunas,
            this.btnSalvarLayout});
            this.toolStripDropDownButton1.Enabled = false;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(67, 27);
            this.toolStripDropDownButton1.Text = "Visão";
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(164, 22);
            this.btnAtualizar.Text = "Atualizar";
            // 
            // btnSelecionarColunas
            // 
            this.btnSelecionarColunas.Name = "btnSelecionarColunas";
            this.btnSelecionarColunas.Size = new System.Drawing.Size(164, 22);
            this.btnSelecionarColunas.Text = "Selecionar Colunas";
            // 
            // btnSalvarLayout
            // 
            this.btnSalvarLayout.Name = "btnSalvarLayout";
            this.btnSalvarLayout.Size = new System.Drawing.Size(164, 22);
            this.btnSalvarLayout.Text = "Salvar Layout";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.Enabled = false;
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(78, 27);
            this.toolStripDropDownButton2.Text = "Anexos";
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.Enabled = false;
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(90, 27);
            this.toolStripDropDownButton3.Text = "Processos";
            // 
            // toolStripDropDownButton4
            // 
            this.toolStripDropDownButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportExcel});
            this.toolStripDropDownButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton4.Image")));
            this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
            this.toolStripDropDownButton4.Size = new System.Drawing.Size(84, 26);
            this.toolStripDropDownButton4.Text = "Exportar";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(105, 28);
            this.btnExportExcel.Text = "Excel";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(728, 247);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.FindDelay = 100;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridData1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(734, 401);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Exclusões";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gridData1
            // 
            this.gridData1.AutoAjuste = true;
            this.gridData1.BotaoEditar = false;
            this.gridData1.BotaoExcluir = true;
            this.gridData1.BotaoNovo = true;
            this.gridData1.Conexao = "Start";
            this.gridData1.Consulta = new string[] {
        "select ZPA.CODIGOPRD, TP.NOMEFANTASIA from ZPRODUTOANALISE ZPA",
        "",
        "left join TPRODUTO TP",
        "on TP.CODIGOPRD = ZPA.CODIGOPRD ",
        "",
        "where ZPA.APLICACAO = \'E\'"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = null;
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(728, 395);
            this.gridData1.TabIndex = 3;
            this.gridData1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.Novo += new AppLib.Windows.GridData.NovoHandler(this.gridData1_Novo);
            this.gridData1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData1_Excluir);
            // 
            // campoLookupCODRPR
            // 
            this.campoLookupCODRPR.Campo = null;
            this.campoLookupCODRPR.ColunaCodigo = "CODRPR";
            this.campoLookupCODRPR.ColunaDescricao = "NOMEFANTASIA";
            this.campoLookupCODRPR.ColunaIdentificador = null;
            this.campoLookupCODRPR.ColunaTabela = "(select * from TRPR) U";
            this.campoLookupCODRPR.Conexao = "Start";
            this.campoLookupCODRPR.Default = null;
            this.campoLookupCODRPR.Edita = true;
            this.campoLookupCODRPR.EditaLookup = false;
            this.campoLookupCODRPR.Location = new System.Drawing.Point(14, 32);
            this.campoLookupCODRPR.MaximoCaracteres = null;
            this.campoLookupCODRPR.Name = "campoLookupCODRPR";
            this.campoLookupCODRPR.NomeGrid = null;
            this.campoLookupCODRPR.Query = 0;
            this.campoLookupCODRPR.Size = new System.Drawing.Size(700, 21);
            this.campoLookupCODRPR.Tabela = null;
            this.campoLookupCODRPR.TabIndex = 57;
            // 
            // FormFamiliaProdutoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 527);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormFamiliaProdutoCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM TTB4",
        "WHERE CODCOLIGADA =?",
        "  AND CODTB4FAT = ?"};
            query1.Parametros = new string[] {
        "CODCOLIGADA",
        "CODTB4FAT"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "TTB4";
            this.Text = "Cadastro Familia de Produto";
            this.ValidarSalvar += new AppLib.Windows.FormCadastroData.ValidarSalvarHandler(this.FormFamiliaProdutoCadastro_Validar);
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormFamiliaProdutoCadastro_AntesSalvar);
            this.Load += new System.EventHandler(this.FormFamiliaProdutoCadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceAnaliseItemCompra.Properties)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private AppLib.Windows.CampoInteiro campoInteiroCODCOLIGADA;
        private System.Windows.Forms.Label label23;
        private AppLib.Windows.CampoTexto campoTextoNOME;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoTexto campoTextoCODTRA;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label4;
        private AppLib.Windows.CampoDecimal campoDecimalPercDescontoRepr;
        private System.Windows.Forms.Label label3;
        private AppLib.Windows.CampoTexto campoTextoDescricaoRepr;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnNovo;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnExcluir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnPesquisar;
        private System.Windows.Forms.ToolStripButton btnAgrupar;
        private System.Windows.Forms.ToolStripButton btnFiltros;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem btnAtualizar;
        private System.Windows.Forms.ToolStripMenuItem btnSelecionarColunas;
        private System.Windows.Forms.ToolStripMenuItem btnSalvarLayout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;
        private System.Windows.Forms.ToolStripMenuItem btnExportExcel;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.CheckEdit ceAnaliseItemCompra;
        private System.Windows.Forms.TabPage tabPage3;
        private AppLib.Windows.GridData gridData1;
        private AppLib.Windows.CampoLookup campoLookupCODRPR;
    }
}