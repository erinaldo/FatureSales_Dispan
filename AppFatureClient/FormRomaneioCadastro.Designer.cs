namespace AppFatureClient
{
    partial class FormRomaneioCadastro
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
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridData2 = new AppLib.Windows.GridData();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridData1 = new AppLib.Windows.GridData();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.campoData1 = new AppLib.Windows.CampoData();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.txtIdRomaneio = new AppLib.Windows.CampoInteiro();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(1226, 361);
            this.splitContainer1.SplitterDistance = 632;
            this.splitContainer1.TabIndex = 22;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(632, 361);
            this.tabControl2.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridData2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(624, 335);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Romaneio";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gridData2
            // 
            this.gridData2.AutoAjuste = true;
            this.gridData2.BotaoEditar = true;
            this.gridData2.BotaoExcluir = false;
            this.gridData2.BotaoNovo = false;
            this.gridData2.Conexao = "Start";
            this.gridData2.Consulta = new string[] {
        "SELECT ",
        "ZROMANEIOITEMS.IDROMANEIOITEMS,",
        "ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO,",
        "ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA,",
        "ZPROGRAMACAOCARREGAMENTO.IDMOV,",
        "ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV,",
        "ZROMANEIOITEMS.OBS,",
        "TMOV.NUMEROMOV,",
        "FCFO.NOMEFANTASIA AS CLIENTE,",
        "TPRODUTO.CODIGOPRD,",
        "TPRODUTO.NOMEFANTASIA AS PRODUTO,",
        "ZPROGRAMACAOCARREGAMENTO.QTDE,",
        "ZPROGRAMACAOCARREGAMENTO.CAMINHAO,",
        "ZPROGRAMACAOCARREGAMENTO.TIPOCAMINHAO",
        "FROM ZROMANEIOITEMS ",
        "INNER JOIN ZPROGRAMACAOCARREGAMENTO ON ZROMANEIOITEMS.IDPROGRAMACAOCARREGAMENTO =" +
            " ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO ",
        "INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAM" +
            "ACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA AND ZPROGRAMACAOCARREGAMENTO." +
            "NSEQITMMOV = TITMMOV.NSEQITMMOV",
        "INNER JOIN TMOV ON TITMMOV.IDMOV = tmov.IDMOV",
        "INNER JOIN FCFO ON TMOV.CODCFO = FCFO.CODCFO",
        "INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD",
        "WHERE ZROMANEIOITEMS.IDROMANEIO = ?"};
            this.gridData2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData2.Formatacao = null;
            this.gridData2.FormPai = null;
            this.gridData2.Location = new System.Drawing.Point(3, 3);
            this.gridData2.ModoFiltro = false;
            this.gridData2.Name = "gridData2";
            this.gridData2.NomeGrid = null;
            this.gridData2.SelecaoCascata = true;
            this.gridData2.Selecionou = false;
            this.gridData2.Size = new System.Drawing.Size(618, 329);
            this.gridData2.TabIndex = 0;
            this.gridData2.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData2.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData2_SetParametros);
            this.gridData2.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData2_Editar);
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage2);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(590, 361);
            this.tabControl3.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridData1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(582, 335);
            this.tabPage2.TabIndex = 0;
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
        "ZPROGRAMACAOCARREGAMENTO.TIPOCAMINHAO",
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
        "AND TPRODUTO.IDPRD = TITMMOV.IDPRD"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = null;
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(576, 329);
            this.gridData1.TabIndex = 0;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1226, 155);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoData1);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Controls.Add(this.txtIdRomaneio);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1218, 129);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // campoData1
            // 
            this.campoData1.Campo = "DATAAGENDAMENTO";
            this.campoData1.Default = null;
            this.campoData1.Edita = true;
            this.campoData1.Location = new System.Drawing.Point(145, 28);
            this.campoData1.Name = "campoData1";
            this.campoData1.Query = 0;
            this.campoData1.Size = new System.Drawing.Size(109, 20);
            this.campoData1.Tabela = "ZROMANEIO";
            this.campoData1.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(149, 10);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(23, 13);
            this.labelControl5.TabIndex = 25;
            this.labelControl5.Text = "Data";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(27, 60);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(58, 13);
            this.labelControl4.TabIndex = 24;
            this.labelControl4.Text = "Observação";
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "OBS";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(24, 79);
            this.campoTexto1.MaximoCaracteres = 255;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(604, 20);
            this.campoTexto1.Tabela = "ZROMANEIO";
            this.campoTexto1.TabIndex = 7;
            // 
            // txtIdRomaneio
            // 
            this.txtIdRomaneio.Campo = "IDROMANEIO";
            this.txtIdRomaneio.Default = null;
            this.txtIdRomaneio.Edita = false;
            this.txtIdRomaneio.Location = new System.Drawing.Point(24, 28);
            this.txtIdRomaneio.Name = "txtIdRomaneio";
            this.txtIdRomaneio.Query = 0;
            this.txtIdRomaneio.Size = new System.Drawing.Size(109, 20);
            this.txtIdRomaneio.Tabela = "ZROMANEIO";
            this.txtIdRomaneio.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 13);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "Id Romaneio";
            // 
            // FormRomaneioCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 616);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormRomaneioCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT * FROM ZROMANEIO WHERE IDROMANEIO = ?"};
            query1.Parametros = new string[] {
        "IDROMANEIO"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZROMANEIO";
            this.Text = "FormRomaneioCadastro";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.AntesNovo += new AppLib.Windows.FormCadastroData.AntesNovoHandler(this.FormRomaneioCadastro_AntesNovo);
            this.Load += new System.EventHandler(this.FormRomaneioCadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private AppLib.Windows.CampoData campoData1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private AppLib.Windows.CampoTexto campoTexto1;
        private AppLib.Windows.CampoInteiro txtIdRomaneio;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage2;
        private AppLib.Windows.GridData gridData1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private AppLib.Windows.GridData gridData2;

    }
}