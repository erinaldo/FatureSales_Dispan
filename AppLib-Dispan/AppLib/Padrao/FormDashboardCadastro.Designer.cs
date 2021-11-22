namespace AppLib.Padrao
{
    partial class FormDashboardCadastro
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
            AppLib.Windows.CodigoNome codigoNome1 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome2 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.TipoArquivo tipoArquivo1 = new AppLib.Windows.TipoArquivo();
            AppLib.Windows.TipoArquivo tipoArquivo2 = new AppLib.Windows.TipoArquivo();
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoUSUARIOALTERACAO = new AppLib.Windows.CampoTexto();
            this.campoDATAALTERACAO = new AppLib.Windows.CampoDataHora();
            this.campoTextoUSUARIOCRIACAO = new AppLib.Windows.CampoTexto();
            this.campoDATACRIACAO = new AppLib.Windows.CampoDataHora();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.campoListaATIVO = new AppLib.Windows.CampoLista();
            this.campoTextoCONEXAO = new AppLib.Windows.CampoTexto();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.campoArquivoIDARQUIVO = new AppLib.Windows.CampoArquivo();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoNOME = new AppLib.Windows.CampoTexto();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroIDDASHBOARD = new AppLib.Windows.CampoInteiro();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridData1 = new AppLib.Windows.GridData();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(693, 359);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl9);
            this.tabPage1.Controls.Add(this.labelControl8);
            this.tabPage1.Controls.Add(this.labelControl7);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.campoTextoUSUARIOALTERACAO);
            this.tabPage1.Controls.Add(this.campoDATAALTERACAO);
            this.tabPage1.Controls.Add(this.campoTextoUSUARIOCRIACAO);
            this.tabPage1.Controls.Add(this.campoDATACRIACAO);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.campoListaATIVO);
            this.tabPage1.Controls.Add(this.campoTextoCONEXAO);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.campoArquivoIDARQUIVO);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.campoTextoNOME);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoInteiroIDDASHBOARD);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(685, 333);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(541, 257);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(85, 13);
            this.labelControl9.TabIndex = 36;
            this.labelControl9.Text = "Usuário Alteração";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(334, 257);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(72, 13);
            this.labelControl8.TabIndex = 35;
            this.labelControl8.Text = "Data Alteração";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(228, 257);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(75, 13);
            this.labelControl7.TabIndex = 34;
            this.labelControl7.Text = "Usuário Criação";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(21, 257);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(62, 13);
            this.labelControl6.TabIndex = 33;
            this.labelControl6.Text = "Data Criação";
            // 
            // campoTextoUSUARIOALTERACAO
            // 
            this.campoTextoUSUARIOALTERACAO.Campo = "USUARIOALTERACAO";
            this.campoTextoUSUARIOALTERACAO.Default = null;
            this.campoTextoUSUARIOALTERACAO.Edita = false;
            this.campoTextoUSUARIOALTERACAO.Location = new System.Drawing.Point(541, 276);
            this.campoTextoUSUARIOALTERACAO.MaximoCaracteres = null;
            this.campoTextoUSUARIOALTERACAO.Name = "campoTextoUSUARIOALTERACAO";
            this.campoTextoUSUARIOALTERACAO.Query = 0;
            this.campoTextoUSUARIOALTERACAO.Size = new System.Drawing.Size(100, 20);
            this.campoTextoUSUARIOALTERACAO.Tabela = "ZDASHBOARD";
            this.campoTextoUSUARIOALTERACAO.TabIndex = 32;
            // 
            // campoDATAALTERACAO
            // 
            this.campoDATAALTERACAO.Campo = "DATAALTERACAO";
            this.campoDATAALTERACAO.Default = null;
            this.campoDATAALTERACAO.Edita = false;
            this.campoDATAALTERACAO.Location = new System.Drawing.Point(334, 276);
            this.campoDATAALTERACAO.Name = "campoDATAALTERACAO";
            this.campoDATAALTERACAO.Query = 0;
            this.campoDATAALTERACAO.Size = new System.Drawing.Size(200, 20);
            this.campoDATAALTERACAO.Tabela = "ZDASHBOARD";
            this.campoDATAALTERACAO.TabIndex = 31;
            // 
            // campoTextoUSUARIOCRIACAO
            // 
            this.campoTextoUSUARIOCRIACAO.Campo = "USUARIOCRIACAO";
            this.campoTextoUSUARIOCRIACAO.Default = null;
            this.campoTextoUSUARIOCRIACAO.Edita = false;
            this.campoTextoUSUARIOCRIACAO.Location = new System.Drawing.Point(228, 276);
            this.campoTextoUSUARIOCRIACAO.MaximoCaracteres = null;
            this.campoTextoUSUARIOCRIACAO.Name = "campoTextoUSUARIOCRIACAO";
            this.campoTextoUSUARIOCRIACAO.Query = 0;
            this.campoTextoUSUARIOCRIACAO.Size = new System.Drawing.Size(100, 20);
            this.campoTextoUSUARIOCRIACAO.Tabela = "ZDASHBOARD";
            this.campoTextoUSUARIOCRIACAO.TabIndex = 30;
            // 
            // campoDATACRIACAO
            // 
            this.campoDATACRIACAO.Campo = "DATACRIACAO";
            this.campoDATACRIACAO.Default = null;
            this.campoDATACRIACAO.Edita = false;
            this.campoDATACRIACAO.Location = new System.Drawing.Point(21, 276);
            this.campoDATACRIACAO.Name = "campoDATACRIACAO";
            this.campoDATACRIACAO.Query = 0;
            this.campoDATACRIACAO.Size = new System.Drawing.Size(200, 20);
            this.campoDATACRIACAO.Tabela = "ZDASHBOARD";
            this.campoDATACRIACAO.TabIndex = 29;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(21, 209);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(25, 13);
            this.labelControl5.TabIndex = 28;
            this.labelControl5.Text = "Ativo";
            // 
            // campoListaATIVO
            // 
            this.campoListaATIVO.Campo = "ATIVO";
            this.campoListaATIVO.Default = null;
            this.campoListaATIVO.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.campoListaATIVO.comboBox1.FormattingEnabled = true;
            codigoNome1.Codigo = "1";
            codigoNome1.Nome = "Sim";
            codigoNome2.Codigo = "0";
            codigoNome2.Nome = "Não";
            this.campoListaATIVO.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2};
            this.campoListaATIVO.Location = new System.Drawing.Point(21, 228);
            this.campoListaATIVO.Name = "campoListaATIVO";
            this.campoListaATIVO.Query = 0;
            this.campoListaATIVO.Size = new System.Drawing.Size(100, 21);
            this.campoListaATIVO.Tabela = "ZDASHBOARD";
            this.campoListaATIVO.TabIndex = 27;
            // 
            // campoTextoCONEXAO
            // 
            this.campoTextoCONEXAO.Campo = "CONEXAO";
            this.campoTextoCONEXAO.Default = "Start";
            this.campoTextoCONEXAO.Edita = true;
            this.campoTextoCONEXAO.Location = new System.Drawing.Point(21, 181);
            this.campoTextoCONEXAO.MaximoCaracteres = null;
            this.campoTextoCONEXAO.Name = "campoTextoCONEXAO";
            this.campoTextoCONEXAO.Query = 0;
            this.campoTextoCONEXAO.Size = new System.Drawing.Size(100, 20);
            this.campoTextoCONEXAO.Tabela = "ZDASHBOARD";
            this.campoTextoCONEXAO.TabIndex = 26;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(21, 162);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(43, 13);
            this.labelControl4.TabIndex = 25;
            this.labelControl4.Text = "Conexão";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(21, 115);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(37, 13);
            this.labelControl3.TabIndex = 24;
            this.labelControl3.Text = "Arquivo";
            // 
            // campoArquivoIDARQUIVO
            // 
            this.campoArquivoIDARQUIVO.Campo = "IDARQUIVO";
            this.campoArquivoIDARQUIVO.Conexao = "Start";
            this.campoArquivoIDARQUIVO.Edita = true;
            tipoArquivo1.ExtensaoArquivo = "*.xml";
            tipoArquivo1.NomeArquivo = "Report";
            tipoArquivo2.ExtensaoArquivo = "*.*";
            tipoArquivo2.NomeArquivo = "Todos";
            this.campoArquivoIDARQUIVO.Filtro = new AppLib.Windows.TipoArquivo[] {
        tipoArquivo1,
        tipoArquivo2};
            this.campoArquivoIDARQUIVO.Location = new System.Drawing.Point(21, 134);
            this.campoArquivoIDARQUIVO.Name = "campoArquivoIDARQUIVO";
            this.campoArquivoIDARQUIVO.Query = 0;
            this.campoArquivoIDARQUIVO.Size = new System.Drawing.Size(620, 20);
            this.campoArquivoIDARQUIVO.Tabela = "ZDASHBOARD";
            this.campoArquivoIDARQUIVO.TabIndex = 23;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(27, 13);
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "Nome";
            // 
            // campoTextoNOME
            // 
            this.campoTextoNOME.Campo = "NOME";
            this.campoTextoNOME.Default = null;
            this.campoTextoNOME.Edita = true;
            this.campoTextoNOME.Location = new System.Drawing.Point(21, 87);
            this.campoTextoNOME.MaximoCaracteres = 100;
            this.campoTextoNOME.Name = "campoTextoNOME";
            this.campoTextoNOME.Query = 0;
            this.campoTextoNOME.Size = new System.Drawing.Size(620, 20);
            this.campoTextoNOME.Tabela = "ZDASHBOARD";
            this.campoTextoNOME.TabIndex = 21;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "Identificador";
            // 
            // campoInteiroIDDASHBOARD
            // 
            this.campoInteiroIDDASHBOARD.Campo = "IDDASHBOARD";
            this.campoInteiroIDDASHBOARD.Default = null;
            this.campoInteiroIDDASHBOARD.Edita = false;
            this.campoInteiroIDDASHBOARD.Location = new System.Drawing.Point(21, 40);
            this.campoInteiroIDDASHBOARD.Name = "campoInteiroIDDASHBOARD";
            this.campoInteiroIDDASHBOARD.Query = 0;
            this.campoInteiroIDDASHBOARD.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroIDDASHBOARD.Tabela = "ZDASHBOARD";
            this.campoInteiroIDDASHBOARD.TabIndex = 19;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridData1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(685, 333);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Permissões";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridData1
            // 
            this.gridData1.AutoAjuste = true;
            this.gridData1.BotaoEditar = true;
            this.gridData1.BotaoExcluir = true;
            this.gridData1.BotaoNovo = true;
            this.gridData1.Conexao = "Start";
            this.gridData1.Consulta = new string[] {
        "SELECT *",
        "FROM ZDASHBOARDPERFIL",
        "WHERE IDDASHBOARD = ?"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = "ZDASHBOARDPERFIL";
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(679, 327);
            this.gridData1.TabIndex = 0;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData1_SetParametros);
            this.gridData1.Novo += new AppLib.Windows.GridData.NovoHandler(this.gridData1_Novo);
            this.gridData1.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData1_Editar);
            this.gridData1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData1_Excluir);
            // 
            // FormDashboardCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 459);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormDashboardCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZDASHBOARD",
        "WHERE IDDASHBOARD = ?"};
            query1.Parametros = new string[] {
        "IDDASHBOARD"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZDASHBOARD";
            this.Text = "Cadastro de Dashboard";
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormDashboardCadastro_AntesSalvar);
            this.Load += new System.EventHandler(this.FormDashboardCadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private Windows.CampoTexto campoTextoUSUARIOALTERACAO;
        private Windows.CampoDataHora campoDATAALTERACAO;
        private Windows.CampoTexto campoTextoUSUARIOCRIACAO;
        private Windows.CampoDataHora campoDATACRIACAO;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Windows.CampoLista campoListaATIVO;
        private Windows.CampoTexto campoTextoCONEXAO;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Windows.CampoArquivo campoArquivoIDARQUIVO;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Windows.CampoTexto campoTextoNOME;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Windows.CampoInteiro campoInteiroIDDASHBOARD;
        private System.Windows.Forms.TabPage tabPage2;
        private Windows.GridData gridData1;
    }
}