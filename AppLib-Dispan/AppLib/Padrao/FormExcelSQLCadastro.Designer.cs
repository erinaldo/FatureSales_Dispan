namespace AppLib.Padrao
{
    partial class FormExcelSQLCadastro
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.campoTexto5 = new AppLib.Windows.CampoTexto();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto6 = new AppLib.Windows.CampoTexto();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto7 = new AppLib.Windows.CampoTexto();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroIDPLANILHASQL = new AppLib.Windows.CampoInteiro();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroIDPLANILHA = new AppLib.Windows.CampoInteiro();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookupIDSQL = new AppLib.Windows.CampoLookup();
            this.campoInteiroORDEM = new AppLib.Windows.CampoInteiro();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridData1 = new AppLib.Windows.GridData();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridData2 = new AppLib.Windows.GridData();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(678, 374);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoTexto5);
            this.tabPage1.Controls.Add(this.labelControl9);
            this.tabPage1.Controls.Add(this.campoTexto6);
            this.tabPage1.Controls.Add(this.labelControl10);
            this.tabPage1.Controls.Add(this.campoTexto7);
            this.tabPage1.Controls.Add(this.labelControl11);
            this.tabPage1.Controls.Add(this.campoInteiroIDPLANILHASQL);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.campoInteiroIDPLANILHA);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.campoLookupIDSQL);
            this.tabPage1.Controls.Add(this.campoInteiroORDEM);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(670, 348);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // campoTexto5
            // 
            this.campoTexto5.Campo = "CELULALINHA";
            this.campoTexto5.Default = null;
            this.campoTexto5.Edita = true;
            this.campoTexto5.Location = new System.Drawing.Point(20, 307);
            this.campoTexto5.MaximoCaracteres = null;
            this.campoTexto5.Name = "campoTexto5";
            this.campoTexto5.Query = 0;
            this.campoTexto5.Size = new System.Drawing.Size(182, 20);
            this.campoTexto5.Tabela = "ZPLANILHASQL";
            this.campoTexto5.TabIndex = 28;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(20, 288);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(25, 13);
            this.labelControl9.TabIndex = 31;
            this.labelControl9.Text = "Linha";
            // 
            // campoTexto6
            // 
            this.campoTexto6.Campo = "CELULACOLUNA";
            this.campoTexto6.Default = null;
            this.campoTexto6.Edita = true;
            this.campoTexto6.Location = new System.Drawing.Point(20, 252);
            this.campoTexto6.MaximoCaracteres = null;
            this.campoTexto6.Name = "campoTexto6";
            this.campoTexto6.Query = 0;
            this.campoTexto6.Size = new System.Drawing.Size(182, 20);
            this.campoTexto6.Tabela = "ZPLANILHASQL";
            this.campoTexto6.TabIndex = 27;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(20, 233);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(33, 13);
            this.labelControl10.TabIndex = 30;
            this.labelControl10.Text = "Coluna";
            // 
            // campoTexto7
            // 
            this.campoTexto7.Campo = "CELULAABA";
            this.campoTexto7.Default = null;
            this.campoTexto7.Edita = true;
            this.campoTexto7.Location = new System.Drawing.Point(20, 198);
            this.campoTexto7.MaximoCaracteres = null;
            this.campoTexto7.Name = "campoTexto7";
            this.campoTexto7.Query = 0;
            this.campoTexto7.Size = new System.Drawing.Size(491, 20);
            this.campoTexto7.Tabela = "ZPLANILHASQL";
            this.campoTexto7.TabIndex = 26;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(20, 179);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(19, 13);
            this.labelControl11.TabIndex = 29;
            this.labelControl11.Text = "Aba";
            // 
            // campoInteiroIDPLANILHASQL
            // 
            this.campoInteiroIDPLANILHASQL.Campo = "IDPLANILHASQL";
            this.campoInteiroIDPLANILHASQL.Default = null;
            this.campoInteiroIDPLANILHASQL.Edita = false;
            this.campoInteiroIDPLANILHASQL.Location = new System.Drawing.Point(20, 39);
            this.campoInteiroIDPLANILHASQL.Name = "campoInteiroIDPLANILHASQL";
            this.campoInteiroIDPLANILHASQL.Query = 0;
            this.campoInteiroIDPLANILHASQL.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroIDPLANILHASQL.Tabela = "ZPLANILHASQL";
            this.campoInteiroIDPLANILHASQL.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(20, 125);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(178, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Ordem de Execução (menor primeiro)";
            // 
            // campoInteiroIDPLANILHA
            // 
            this.campoInteiroIDPLANILHA.Campo = "IDPLANILHA";
            this.campoInteiroIDPLANILHA.Default = null;
            this.campoInteiroIDPLANILHA.Edita = false;
            this.campoInteiroIDPLANILHA.Location = new System.Drawing.Point(126, 39);
            this.campoInteiroIDPLANILHA.Name = "campoInteiroIDPLANILHA";
            this.campoInteiroIDPLANILHA.Query = 0;
            this.campoInteiroIDPLANILHA.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroIDPLANILHA.Tabela = "ZPLANILHASQL";
            this.campoInteiroIDPLANILHA.TabIndex = 1;
            this.campoInteiroIDPLANILHA.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Identificador";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 71);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(64, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Consulta SQL";
            // 
            // campoLookupIDSQL
            // 
            this.campoLookupIDSQL.Campo = "IDSQL";
            this.campoLookupIDSQL.ColunaCodigo = "IDSQL";
            this.campoLookupIDSQL.ColunaDescricao = "NOME";
            this.campoLookupIDSQL.ColunaIdentificador = null;
            this.campoLookupIDSQL.ColunaTabela = "ZSQL";
            this.campoLookupIDSQL.Conexao = "Start";
            this.campoLookupIDSQL.Default = null;
            this.campoLookupIDSQL.Edita = true;
            this.campoLookupIDSQL.Location = new System.Drawing.Point(20, 90);
            this.campoLookupIDSQL.MaximoCaracteres = null;
            this.campoLookupIDSQL.Name = "campoLookupIDSQL";
            this.campoLookupIDSQL.NomeGrid = "ZSQL";
            this.campoLookupIDSQL.Query = 0;
            this.campoLookupIDSQL.Size = new System.Drawing.Size(625, 21);
            this.campoLookupIDSQL.Tabela = "ZPLANILHASQL";
            this.campoLookupIDSQL.TabIndex = 2;
            // 
            // campoInteiroORDEM
            // 
            this.campoInteiroORDEM.Campo = "ORDEM";
            this.campoInteiroORDEM.Default = null;
            this.campoInteiroORDEM.Edita = true;
            this.campoInteiroORDEM.Location = new System.Drawing.Point(20, 144);
            this.campoInteiroORDEM.Name = "campoInteiroORDEM";
            this.campoInteiroORDEM.Query = 0;
            this.campoInteiroORDEM.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroORDEM.Tabela = "ZPLANILHASQL";
            this.campoInteiroORDEM.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(126, 20);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "IDPLANILHA (FK)";
            this.labelControl2.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridData1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(670, 348);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Parâmetros";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gridData1
            // 
            this.gridData1.AutoAjuste = true;
            this.gridData1.BotaoEditar = true;
            this.gridData1.BotaoExcluir = true;
            this.gridData1.BotaoNovo = true;
            this.gridData1.Conexao = "Start";
            this.gridData1.Consulta = new string[] {
        "SELECT",
        "IDPLANILHASQL,",
        "PARAMETRO,",
        "\'ABA: \' + CELULAABA + \' | Campo: \' + CELULACOLUNA + CONVERT(VARCHAR, CELULALINHA)" +
            " ENDERECO",
        "",
        "FROM ZPLANILHASQLPAR",
        "",
        "WHERE IDPLANILHASQL = ?"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = "ZPLANILHASQLPAR";
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(664, 342);
            this.gridData1.TabIndex = 10;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData1_SetParametros);
            this.gridData1.Novo += new AppLib.Windows.GridData.NovoHandler(this.gridData1_Novo);
            this.gridData1.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData1_Editar);
            this.gridData1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData1_Excluir);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridData2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(670, 348);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Procedures";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridData2
            // 
            this.gridData2.AutoAjuste = true;
            this.gridData2.BotaoEditar = true;
            this.gridData2.BotaoExcluir = true;
            this.gridData2.BotaoNovo = true;
            this.gridData2.Conexao = "Start";
            this.gridData2.Consulta = new string[] {
        "SELECT",
        "IDPLANILHASQLPROC,",
        "NOMEPROCEDURE,",
        "PARAMETRO,",
        "\'ABA: \' + CELULAABA + \' | Campo: \' + CELULACOLUNA + CONVERT(VARCHAR, CELULALINHA)" +
            " ENDERECO",
        "",
        "FROM ZPLANILHASQLPROC",
        "",
        "WHERE IDPLANILHASQL = ?"};
            this.gridData2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData2.Formatacao = null;
            this.gridData2.FormPai = null;
            this.gridData2.Location = new System.Drawing.Point(3, 3);
            this.gridData2.ModoFiltro = false;
            this.gridData2.Name = "gridData2";
            this.gridData2.NomeGrid = "ZPLANILHASQLPROC";
            this.gridData2.SelecaoCascata = true;
            this.gridData2.Selecionou = false;
            this.gridData2.Size = new System.Drawing.Size(664, 342);
            this.gridData2.TabIndex = 0;
            this.gridData2.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData2.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData2_SetParametros);
            this.gridData2.Novo += new AppLib.Windows.GridData.NovoHandler(this.gridData2_Novo);
            this.gridData2.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData2_Editar);
            this.gridData2.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData2_Excluir);
            // 
            // FormExcelSQLCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 474);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormExcelSQLCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZPLANILHASQL",
        "WHERE IDPLANILHASQL = ?"};
            query1.Parametros = new string[] {
        "IDPLANILHASQL"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZPLANILHASQL";
            this.Text = "Cadastro de SQL na Planilha";
            this.Load += new System.EventHandler(this.FormExcelSQLCadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Windows.CampoLookup campoLookupIDSQL;
        private Windows.CampoInteiro campoInteiroORDEM;
        private Windows.CampoInteiro campoInteiroIDPLANILHA;
        private Windows.CampoInteiro campoInteiroIDPLANILHASQL;
        private System.Windows.Forms.TabPage tabPage2;
        private Windows.GridData gridData2;
        private System.Windows.Forms.TabPage tabPage3;
        private Windows.GridData gridData1;
        private Windows.CampoTexto campoTexto5;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private Windows.CampoTexto campoTexto6;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private Windows.CampoTexto campoTexto7;
        private DevExpress.XtraEditors.LabelControl labelControl11;
    }
}