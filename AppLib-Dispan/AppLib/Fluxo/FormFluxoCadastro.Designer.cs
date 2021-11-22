namespace AppLib.Fluxo
{
    partial class FormFluxoCadastro
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
            AppLib.Windows.CodigoNome codigoNome3 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome4 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome5 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome6 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome7 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome8 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome9 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome10 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            AppLib.Windows.Query query2 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.campoListaATIVO = new AppLib.Windows.CampoLista();
            this.campoTextoZFLUXOEXT_FLUXO = new AppLib.Windows.CampoTexto();
            this.campoInteiroCONTADOR = new AppLib.Windows.CampoInteiro();
            this.campoMemoDESCRICAO = new AppLib.Windows.CampoMemo();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoListaTIPORETORNO = new AppLib.Windows.CampoLista();
            this.campoTextoFLUXO = new AppLib.Windows.CampoTexto();
            this.campoTextoCLASSE = new AppLib.Windows.CampoTexto();
            this.campoTextoBIBLIOTECA = new AppLib.Windows.CampoTexto();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridData1 = new AppLib.Windows.GridData();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridData2 = new AppLib.Windows.GridData();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(670, 442);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.campoListaATIVO);
            this.tabPage1.Controls.Add(this.campoTextoZFLUXOEXT_FLUXO);
            this.tabPage1.Controls.Add(this.campoInteiroCONTADOR);
            this.tabPage1.Controls.Add(this.campoMemoDESCRICAO);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoListaTIPORETORNO);
            this.tabPage1.Controls.Add(this.campoTextoFLUXO);
            this.tabPage1.Controls.Add(this.campoTextoCLASSE);
            this.tabPage1.Controls.Add(this.campoTextoBIBLIOTECA);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(662, 416);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(29, 314);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(25, 13);
            this.labelControl6.TabIndex = 13;
            this.labelControl6.Text = "Ativo";
            // 
            // campoListaATIVO
            // 
            this.campoListaATIVO.Campo = "ATIVO";
            this.campoListaATIVO.Default = "1";
            codigoNome1.Codigo = "1";
            codigoNome1.Nome = "Sim";
            codigoNome2.Codigo = "0";
            codigoNome2.Nome = "Não";
            this.campoListaATIVO.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2};
            this.campoListaATIVO.Location = new System.Drawing.Point(29, 333);
            this.campoListaATIVO.Name = "campoListaATIVO";
            this.campoListaATIVO.Query = 1;
            this.campoListaATIVO.Size = new System.Drawing.Size(206, 21);
            this.campoListaATIVO.Tabela = "ZFLUXOEXT";
            this.campoListaATIVO.TabIndex = 12;
            // 
            // campoTextoZFLUXOEXT_FLUXO
            // 
            this.campoTextoZFLUXOEXT_FLUXO.Campo = "FLUXO";
            this.campoTextoZFLUXOEXT_FLUXO.Default = null;
            this.campoTextoZFLUXOEXT_FLUXO.Edita = true;
            this.campoTextoZFLUXOEXT_FLUXO.Location = new System.Drawing.Point(489, 38);
            this.campoTextoZFLUXOEXT_FLUXO.MaximoCaracteres = null;
            this.campoTextoZFLUXOEXT_FLUXO.Name = "campoTextoZFLUXOEXT_FLUXO";
            this.campoTextoZFLUXOEXT_FLUXO.Query = 1;
            this.campoTextoZFLUXOEXT_FLUXO.Size = new System.Drawing.Size(31, 20);
            this.campoTextoZFLUXOEXT_FLUXO.Tabela = "ZFLUXOEXT";
            this.campoTextoZFLUXOEXT_FLUXO.TabIndex = 11;
            this.campoTextoZFLUXOEXT_FLUXO.Visible = false;
            // 
            // campoInteiroCONTADOR
            // 
            this.campoInteiroCONTADOR.Campo = "CONTADOR";
            this.campoInteiroCONTADOR.Default = 1;
            this.campoInteiroCONTADOR.Edita = false;
            this.campoInteiroCONTADOR.Location = new System.Drawing.Point(452, 38);
            this.campoInteiroCONTADOR.Name = "campoInteiroCONTADOR";
            this.campoInteiroCONTADOR.Query = 0;
            this.campoInteiroCONTADOR.Size = new System.Drawing.Size(31, 20);
            this.campoInteiroCONTADOR.Tabela = "ZFLUXO";
            this.campoInteiroCONTADOR.TabIndex = 10;
            this.campoInteiroCONTADOR.Visible = false;
            // 
            // campoMemoDESCRICAO
            // 
            this.campoMemoDESCRICAO.Campo = "DESCRICAO";
            this.campoMemoDESCRICAO.Default = null;
            this.campoMemoDESCRICAO.Edita = true;
            this.campoMemoDESCRICAO.Location = new System.Drawing.Point(29, 186);
            this.campoMemoDESCRICAO.MaximoCaracteres = null;
            this.campoMemoDESCRICAO.Name = "campoMemoDESCRICAO";
            this.campoMemoDESCRICAO.Query = 0;
            this.campoMemoDESCRICAO.Size = new System.Drawing.Size(454, 64);
            this.campoMemoDESCRICAO.Tabela = "ZFLUXO";
            this.campoMemoDESCRICAO.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(29, 259);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(77, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Tipo do Retorno";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(29, 167);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(90, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Descrição do Fluxo";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(29, 118);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(71, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Nome do Fluxo";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(29, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Classe";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(29, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Biblioteca";
            // 
            // campoListaTIPORETORNO
            // 
            this.campoListaTIPORETORNO.Campo = "TIPORETORNO";
            this.campoListaTIPORETORNO.Default = null;
            codigoNome3.Codigo = "";
            codigoNome3.Nome = "Sem retorno";
            codigoNome4.Codigo = "Inteiro";
            codigoNome4.Nome = "int";
            codigoNome5.Codigo = "Decimal";
            codigoNome5.Nome = "decimal";
            codigoNome6.Codigo = "Texto";
            codigoNome6.Nome = "String";
            codigoNome7.Codigo = "Data e Hora";
            codigoNome7.Nome = "DateTime";
            codigoNome8.Codigo = "Lista";
            codigoNome8.Nome = "List<Object>";
            codigoNome9.Codigo = "Tabela";
            codigoNome9.Nome = "DataTable";
            codigoNome10.Codigo = "Verdadeiro ou Falso";
            codigoNome10.Nome = "Boolean";
            this.campoListaTIPORETORNO.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome3,
        codigoNome4,
        codigoNome5,
        codigoNome6,
        codigoNome7,
        codigoNome8,
        codigoNome9,
        codigoNome10};
            this.campoListaTIPORETORNO.Location = new System.Drawing.Point(29, 279);
            this.campoListaTIPORETORNO.Name = "campoListaTIPORETORNO";
            this.campoListaTIPORETORNO.Query = 0;
            this.campoListaTIPORETORNO.Size = new System.Drawing.Size(206, 21);
            this.campoListaTIPORETORNO.Tabela = "ZFLUXO";
            this.campoListaTIPORETORNO.TabIndex = 4;
            // 
            // campoTextoFLUXO
            // 
            this.campoTextoFLUXO.Campo = "NOME";
            this.campoTextoFLUXO.Default = null;
            this.campoTextoFLUXO.Edita = true;
            this.campoTextoFLUXO.Location = new System.Drawing.Point(29, 138);
            this.campoTextoFLUXO.MaximoCaracteres = null;
            this.campoTextoFLUXO.Name = "campoTextoFLUXO";
            this.campoTextoFLUXO.Query = 0;
            this.campoTextoFLUXO.Size = new System.Drawing.Size(206, 20);
            this.campoTextoFLUXO.Tabela = "ZFLUXO";
            this.campoTextoFLUXO.TabIndex = 2;
            // 
            // campoTextoCLASSE
            // 
            this.campoTextoCLASSE.Campo = "CLASSE";
            this.campoTextoCLASSE.Default = null;
            this.campoTextoCLASSE.Edita = true;
            this.campoTextoCLASSE.Location = new System.Drawing.Point(29, 87);
            this.campoTextoCLASSE.MaximoCaracteres = null;
            this.campoTextoCLASSE.Name = "campoTextoCLASSE";
            this.campoTextoCLASSE.Query = 0;
            this.campoTextoCLASSE.Size = new System.Drawing.Size(206, 20);
            this.campoTextoCLASSE.Tabela = "ZFLUXO";
            this.campoTextoCLASSE.TabIndex = 1;
            // 
            // campoTextoBIBLIOTECA
            // 
            this.campoTextoBIBLIOTECA.Campo = "BIBLIOTECA";
            this.campoTextoBIBLIOTECA.Default = null;
            this.campoTextoBIBLIOTECA.Edita = true;
            this.campoTextoBIBLIOTECA.Location = new System.Drawing.Point(29, 38);
            this.campoTextoBIBLIOTECA.MaximoCaracteres = null;
            this.campoTextoBIBLIOTECA.Name = "campoTextoBIBLIOTECA";
            this.campoTextoBIBLIOTECA.Query = 0;
            this.campoTextoBIBLIOTECA.Size = new System.Drawing.Size(206, 20);
            this.campoTextoBIBLIOTECA.Tabela = "ZFLUXO";
            this.campoTextoBIBLIOTECA.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridData1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(662, 416);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parâmetros";
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
        "FROM ZFLUXOVARIAVEL",
        "WHERE FLUXO = ?",
        "  AND TIPOVARIAVEL = \'Parâmetro\'"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = "ZFLUXOVARIAVEL1";
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(656, 410);
            this.gridData1.TabIndex = 0;
            this.gridData1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData1_SetParametros);
            this.gridData1.Novo += new AppLib.Windows.GridData.NovoHandler(this.gridData1_Novo);
            this.gridData1.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData1_Editar);
            this.gridData1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData1_Excluir);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridData2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(662, 416);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Variáveis";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gridData2
            // 
            this.gridData2.AutoAjuste = true;
            this.gridData2.BotaoEditar = true;
            this.gridData2.BotaoExcluir = true;
            this.gridData2.BotaoNovo = true;
            this.gridData2.Conexao = "Start";
            this.gridData2.Consulta = new string[] {
        "SELECT *",
        "FROM ZFLUXOVARIAVEL",
        "WHERE FLUXO = ?",
        "  AND TIPOVARIAVEL = \'Variável\'"};
            this.gridData2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData2.Formatacao = null;
            this.gridData2.FormPai = null;
            this.gridData2.Location = new System.Drawing.Point(3, 3);
            this.gridData2.ModoFiltro = false;
            this.gridData2.Name = "gridData2";
            this.gridData2.NomeGrid = "ZFLUXOVARIAVEL2";
            this.gridData2.SelecaoCascata = true;
            this.gridData2.Selecionou = false;
            this.gridData2.Size = new System.Drawing.Size(656, 410);
            this.gridData2.TabIndex = 1;
            this.gridData2.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData2.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData2.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData2_SetParametros);
            this.gridData2.Novo += new AppLib.Windows.GridData.NovoHandler(this.gridData2_Novo);
            this.gridData2.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData2_Editar);
            this.gridData2.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData2_Excluir);
            // 
            // FormFluxoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 542);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormFluxoCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZFLUXO",
        "WHERE NOME = ?"};
            query1.Parametros = new string[] {
        "NOME"};
            query2.Conexao = "Start";
            query2.Consulta = new string[] {
        "SELECT *",
        "FROM ZFLUXOEXT",
        "WHERE FLUXO = ?"};
            query2.Parametros = new string[] {
        "NOME"};
            this.Querys = new AppLib.Windows.Query[] {
        query1,
        query2};
            this.TabelaPrincipal = "ZFLUXO";
            this.Text = "Cadastro de Fluxo";
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormFluxoCadastro_AntesSalvar);
            this.AntesExcluir += new AppLib.Windows.FormCadastroData.AntesExcluirHandler(this.FormFluxoCadastro_AntesExcluir);
            this.Load += new System.EventHandler(this.FormFluxoCadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Windows.CampoLista campoListaTIPORETORNO;
        private Windows.CampoTexto campoTextoFLUXO;
        private Windows.CampoTexto campoTextoCLASSE;
        private Windows.CampoTexto campoTextoBIBLIOTECA;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Windows.CampoMemo campoMemoDESCRICAO;
        private Windows.CampoInteiro campoInteiroCONTADOR;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private Windows.GridData gridData1;
        private Windows.GridData gridData2;
        private Windows.CampoTexto campoTextoZFLUXOEXT_FLUXO;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private Windows.CampoLista campoListaATIVO;
    }
}