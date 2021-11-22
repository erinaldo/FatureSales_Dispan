namespace AppLib.Padrao
{
    partial class FormCuboCadastro
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
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.campoLookupIDSQL = new AppLib.Windows.CampoLookup();
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoNOME = new AppLib.Windows.CampoTexto();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroIDCUBO = new AppLib.Windows.CampoInteiro();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridData1 = new AppLib.Windows.GridData();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridData2 = new AppLib.Windows.GridData();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gridData3 = new AppLib.Windows.GridData();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.gridData4 = new AppLib.Windows.GridData();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(678, 306);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoLookupIDSQL);
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
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.campoTextoNOME);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoInteiroIDCUBO);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(670, 280);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.campoLookupIDSQL.Location = new System.Drawing.Point(22, 134);
            this.campoLookupIDSQL.MaximoCaracteres = null;
            this.campoLookupIDSQL.Name = "campoLookupIDSQL";
            this.campoLookupIDSQL.NomeGrid = "ZSQL";
            this.campoLookupIDSQL.Query = 0;
            this.campoLookupIDSQL.Size = new System.Drawing.Size(620, 21);
            this.campoLookupIDSQL.Tabela = "ZCUBO";
            this.campoLookupIDSQL.TabIndex = 71;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(542, 213);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(85, 13);
            this.labelControl9.TabIndex = 70;
            this.labelControl9.Text = "Usuário Alteração";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(335, 213);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(72, 13);
            this.labelControl8.TabIndex = 69;
            this.labelControl8.Text = "Data Alteração";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(229, 213);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(75, 13);
            this.labelControl7.TabIndex = 68;
            this.labelControl7.Text = "Usuário Criação";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(22, 213);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(62, 13);
            this.labelControl6.TabIndex = 67;
            this.labelControl6.Text = "Data Criação";
            // 
            // campoTextoUSUARIOALTERACAO
            // 
            this.campoTextoUSUARIOALTERACAO.Campo = "USUARIOALTERACAO";
            this.campoTextoUSUARIOALTERACAO.Default = null;
            this.campoTextoUSUARIOALTERACAO.Edita = false;
            this.campoTextoUSUARIOALTERACAO.Location = new System.Drawing.Point(542, 232);
            this.campoTextoUSUARIOALTERACAO.MaximoCaracteres = null;
            this.campoTextoUSUARIOALTERACAO.Name = "campoTextoUSUARIOALTERACAO";
            this.campoTextoUSUARIOALTERACAO.Query = 0;
            this.campoTextoUSUARIOALTERACAO.Size = new System.Drawing.Size(100, 20);
            this.campoTextoUSUARIOALTERACAO.Tabela = "ZCUBO";
            this.campoTextoUSUARIOALTERACAO.TabIndex = 62;
            // 
            // campoDATAALTERACAO
            // 
            this.campoDATAALTERACAO.Campo = "DATAALTERACAO";
            this.campoDATAALTERACAO.Default = null;
            this.campoDATAALTERACAO.Edita = false;
            this.campoDATAALTERACAO.Location = new System.Drawing.Point(335, 232);
            this.campoDATAALTERACAO.Name = "campoDATAALTERACAO";
            this.campoDATAALTERACAO.Query = 0;
            this.campoDATAALTERACAO.Size = new System.Drawing.Size(200, 20);
            this.campoDATAALTERACAO.Tabela = "ZCUBO";
            this.campoDATAALTERACAO.TabIndex = 61;
            // 
            // campoTextoUSUARIOCRIACAO
            // 
            this.campoTextoUSUARIOCRIACAO.Campo = "USUARIOCRIACAO";
            this.campoTextoUSUARIOCRIACAO.Default = null;
            this.campoTextoUSUARIOCRIACAO.Edita = false;
            this.campoTextoUSUARIOCRIACAO.Location = new System.Drawing.Point(229, 232);
            this.campoTextoUSUARIOCRIACAO.MaximoCaracteres = null;
            this.campoTextoUSUARIOCRIACAO.Name = "campoTextoUSUARIOCRIACAO";
            this.campoTextoUSUARIOCRIACAO.Query = 0;
            this.campoTextoUSUARIOCRIACAO.Size = new System.Drawing.Size(100, 20);
            this.campoTextoUSUARIOCRIACAO.Tabela = "ZCUBO";
            this.campoTextoUSUARIOCRIACAO.TabIndex = 60;
            // 
            // campoDATACRIACAO
            // 
            this.campoDATACRIACAO.Campo = "DATACRIACAO";
            this.campoDATACRIACAO.Default = null;
            this.campoDATACRIACAO.Edita = false;
            this.campoDATACRIACAO.Location = new System.Drawing.Point(22, 232);
            this.campoDATACRIACAO.Name = "campoDATACRIACAO";
            this.campoDATACRIACAO.Query = 0;
            this.campoDATACRIACAO.Size = new System.Drawing.Size(200, 20);
            this.campoDATACRIACAO.Tabela = "ZCUBO";
            this.campoDATACRIACAO.TabIndex = 59;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(22, 165);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(25, 13);
            this.labelControl5.TabIndex = 66;
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
            this.campoListaATIVO.Location = new System.Drawing.Point(22, 184);
            this.campoListaATIVO.Name = "campoListaATIVO";
            this.campoListaATIVO.Query = 0;
            this.campoListaATIVO.Size = new System.Drawing.Size(100, 21);
            this.campoListaATIVO.Tabela = "ZCUBO";
            this.campoListaATIVO.TabIndex = 58;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(22, 115);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(64, 13);
            this.labelControl3.TabIndex = 65;
            this.labelControl3.Text = "Consulta SQL";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(22, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(27, 13);
            this.labelControl2.TabIndex = 64;
            this.labelControl2.Text = "Nome";
            // 
            // campoTextoNOME
            // 
            this.campoTextoNOME.Campo = "NOME";
            this.campoTextoNOME.Default = null;
            this.campoTextoNOME.Edita = true;
            this.campoTextoNOME.Location = new System.Drawing.Point(22, 87);
            this.campoTextoNOME.MaximoCaracteres = 100;
            this.campoTextoNOME.Name = "campoTextoNOME";
            this.campoTextoNOME.Query = 0;
            this.campoTextoNOME.Size = new System.Drawing.Size(620, 20);
            this.campoTextoNOME.Tabela = "ZCUBO";
            this.campoTextoNOME.TabIndex = 56;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 63;
            this.labelControl1.Text = "Identificador";
            // 
            // campoInteiroIDCUBO
            // 
            this.campoInteiroIDCUBO.Campo = "IDCUBO";
            this.campoInteiroIDCUBO.Default = null;
            this.campoInteiroIDCUBO.Edita = false;
            this.campoInteiroIDCUBO.Location = new System.Drawing.Point(22, 40);
            this.campoInteiroIDCUBO.Name = "campoInteiroIDCUBO";
            this.campoInteiroIDCUBO.Query = 0;
            this.campoInteiroIDCUBO.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroIDCUBO.Tabela = "ZCUBO";
            this.campoInteiroIDCUBO.TabIndex = 55;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridData1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(670, 280);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parâmetros do Cubo";
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
        "FROM ZCUBOPARAM",
        "WHERE IDCUBO = ?"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = "ZCUBOPARAM";
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(664, 274);
            this.gridData1.TabIndex = 0;
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
            this.tabPage3.Size = new System.Drawing.Size(670, 280);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Parâmetros da SQL";
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
        "FROM ZCUBOSQLPAR",
        "WHERE IDCUBO = ?"};
            this.gridData2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData2.Formatacao = null;
            this.gridData2.FormPai = null;
            this.gridData2.Location = new System.Drawing.Point(3, 3);
            this.gridData2.ModoFiltro = false;
            this.gridData2.Name = "gridData2";
            this.gridData2.NomeGrid = "ZCUBOSQLPAR";
            this.gridData2.SelecaoCascata = true;
            this.gridData2.Selecionou = false;
            this.gridData2.Size = new System.Drawing.Size(664, 274);
            this.gridData2.TabIndex = 0;
            this.gridData2.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData2.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData2_SetParametros);
            this.gridData2.Novo += new AppLib.Windows.GridData.NovoHandler(this.gridData2_Novo);
            this.gridData2.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData2_Editar);
            this.gridData2.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData2_Excluir);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.gridData3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(670, 280);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Procedures";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // gridData3
            // 
            this.gridData3.AutoAjuste = true;
            this.gridData3.BotaoEditar = true;
            this.gridData3.BotaoExcluir = true;
            this.gridData3.BotaoNovo = true;
            this.gridData3.Conexao = "Start";
            this.gridData3.Consulta = new string[] {
        "SELECT *",
        "FROM ZCUBOSQLPROC",
        "WHERE IDCUBO = ?"};
            this.gridData3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData3.Formatacao = null;
            this.gridData3.FormPai = null;
            this.gridData3.Location = new System.Drawing.Point(3, 3);
            this.gridData3.ModoFiltro = false;
            this.gridData3.Name = "gridData3";
            this.gridData3.NomeGrid = "ZCUBOSQLPROC";
            this.gridData3.SelecaoCascata = true;
            this.gridData3.Selecionou = false;
            this.gridData3.Size = new System.Drawing.Size(664, 274);
            this.gridData3.TabIndex = 0;
            this.gridData3.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData3.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData3_SetParametros);
            this.gridData3.Novo += new AppLib.Windows.GridData.NovoHandler(this.gridData3_Novo);
            this.gridData3.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData3_Editar);
            this.gridData3.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData3_Excluir);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.gridData4);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(670, 280);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Permissões";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // gridData4
            // 
            this.gridData4.AutoAjuste = true;
            this.gridData4.BotaoEditar = true;
            this.gridData4.BotaoExcluir = true;
            this.gridData4.BotaoNovo = true;
            this.gridData4.Conexao = "Start";
            this.gridData4.Consulta = new string[] {
        "SELECT *",
        "FROM ZCUBOPERFIL",
        "WHERE IDCUBO = ?"};
            this.gridData4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData4.Formatacao = null;
            this.gridData4.FormPai = null;
            this.gridData4.Location = new System.Drawing.Point(3, 3);
            this.gridData4.ModoFiltro = false;
            this.gridData4.Name = "gridData4";
            this.gridData4.NomeGrid = "ZCUBOPERFIL";
            this.gridData4.SelecaoCascata = true;
            this.gridData4.Selecionou = false;
            this.gridData4.Size = new System.Drawing.Size(664, 274);
            this.gridData4.TabIndex = 0;
            this.gridData4.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData4.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData4_SetParametros);
            this.gridData4.Novo += new AppLib.Windows.GridData.NovoHandler(this.gridData4_Novo);
            this.gridData4.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData4_Editar);
            this.gridData4.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData4_Excluir);
            // 
            // FormCuboCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 406);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormCuboCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZCUBO",
        "WHERE IDCUBO = ?"};
            query1.Parametros = new string[] {
        "IDCUBO"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZCUBO";
            this.Text = "Cadastro de  Cubo";
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormCuboCadastro_AntesSalvar);
            this.Load += new System.EventHandler(this.FormCuboCadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
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
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Windows.CampoTexto campoTextoNOME;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Windows.CampoInteiro campoInteiroIDCUBO;
        private Windows.CampoLookup campoLookupIDSQL;
        private System.Windows.Forms.TabPage tabPage2;
        private Windows.GridData gridData1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private Windows.GridData gridData2;
        private Windows.GridData gridData3;
        private System.Windows.Forms.TabPage tabPage5;
        private Windows.GridData gridData4;
    }
}