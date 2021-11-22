namespace AppFatureClient
{
    partial class FormRepresentantesCadastro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRepresentantesCadastro));
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCODCOLIGADA = new AppLib.Windows.CampoInteiro();
            this.label11 = new System.Windows.Forms.Label();
            this.txtcontato = new AppLib.Windows.CampoTexto();
            this.txtfax = new AppLib.Windows.CampoTexto();
            this.txtcelular = new AppLib.Windows.CampoTexto();
            this.txttelefone = new AppLib.Windows.CampoTexto();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtpais = new AppLib.Windows.CampoTexto();
            this.label25 = new System.Windows.Forms.Label();
            this.lookupestado = new AppLib.Windows.CampoLookup();
            this.lookupmunicipio = new AppLib.Windows.CampoLookup();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtbairro = new AppLib.Windows.CampoTexto();
            this.label28 = new System.Windows.Forms.Label();
            this.txtcomplemento = new AppLib.Windows.CampoTexto();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txtnumero = new AppLib.Windows.CampoTexto();
            this.txtrua = new AppLib.Windows.CampoTexto();
            this.label31 = new System.Windows.Forms.Label();
            this.txtcep = new AppLib.Windows.CampoTexto();
            this.label32 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.campoDecimal3 = new AppLib.Windows.CampoDecimal();
            this.campoDecimal2 = new AppLib.Windows.CampoDecimal();
            this.campoDecimal1 = new AppLib.Windows.CampoDecimal();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtie = new AppLib.Windows.CampoTexto();
            this.label9 = new System.Windows.Forms.Label();
            this.txtcnpj = new AppLib.Windows.CampoTexto();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbinativo = new AppLib.Windows.CampoLista();
            this.txtnomefantasia = new AppLib.Windows.CampoTexto();
            this.label3 = new System.Windows.Forms.Label();
            this.txtnome = new AppLib.Windows.CampoTexto();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtcodigo = new AppLib.Windows.CampoTexto();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnIncluir = new DevExpress.XtraEditors.SimpleButton();
            this.label12 = new System.Windows.Forms.Label();
            this.lookupusuario = new AppLib.Windows.CampoLookup();
            this.grid1 = new AppLib.Windows.GridData();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cbComissaoPadrao = new DevExpress.XtraEditors.CheckEdit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbComissaoPadrao.Properties)).BeginInit();
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
            this.tabControl1.Size = new System.Drawing.Size(869, 424);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.txtie);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.txtcnpj);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.cmbinativo);
            this.tabPage1.Controls.Add(this.txtnomefantasia);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtnome);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtcodigo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(861, 398);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCODCOLIGADA);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtcontato);
            this.groupBox2.Controls.Add(this.txtfax);
            this.groupBox2.Controls.Add(this.txtcelular);
            this.groupBox2.Controls.Add(this.txttelefone);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.txtpais);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.lookupestado);
            this.groupBox2.Controls.Add(this.lookupmunicipio);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.txtbairro);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.txtcomplemento);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.txtnumero);
            this.groupBox2.Controls.Add(this.txtrua);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.txtcep);
            this.groupBox2.Controls.Add(this.label32);
            this.groupBox2.Location = new System.Drawing.Point(9, 130);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(818, 359);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Endereço";
            // 
            // txtCODCOLIGADA
            // 
            this.txtCODCOLIGADA.Campo = "CODCOLIGADA";
            this.txtCODCOLIGADA.Default = null;
            this.txtCODCOLIGADA.Edita = true;
            this.txtCODCOLIGADA.Location = new System.Drawing.Point(703, 33);
            this.txtCODCOLIGADA.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCODCOLIGADA.Name = "txtCODCOLIGADA";
            this.txtCODCOLIGADA.Query = 0;
            this.txtCODCOLIGADA.Size = new System.Drawing.Size(100, 20);
            this.txtCODCOLIGADA.Tabela = "TRPR";
            this.txtCODCOLIGADA.TabIndex = 65;
            this.txtCODCOLIGADA.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(700, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 13);
            this.label11.TabIndex = 66;
            this.label11.Text = "Coligada (invisivel)";
            this.label11.Visible = false;
            // 
            // txtcontato
            // 
            this.txtcontato.Campo = "CONTATO";
            this.txtcontato.Default = null;
            this.txtcontato.Edita = true;
            this.txtcontato.Location = new System.Drawing.Point(422, 242);
            this.txtcontato.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtcontato.MaximoCaracteres = 20;
            this.txtcontato.Name = "txtcontato";
            this.txtcontato.Query = 0;
            this.txtcontato.Size = new System.Drawing.Size(130, 20);
            this.txtcontato.Tabela = "TRPR";
            this.txtcontato.TabIndex = 11;
            // 
            // txtfax
            // 
            this.txtfax.Campo = "FAX";
            this.txtfax.Default = null;
            this.txtfax.Edita = true;
            this.txtfax.Location = new System.Drawing.Point(286, 242);
            this.txtfax.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtfax.MaximoCaracteres = 15;
            this.txtfax.Name = "txtfax";
            this.txtfax.Query = 0;
            this.txtfax.Size = new System.Drawing.Size(130, 20);
            this.txtfax.Tabela = "TRPR";
            this.txtfax.TabIndex = 10;
            // 
            // txtcelular
            // 
            this.txtcelular.Campo = "CELULAR";
            this.txtcelular.Default = null;
            this.txtcelular.Edita = true;
            this.txtcelular.Location = new System.Drawing.Point(150, 242);
            this.txtcelular.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtcelular.MaximoCaracteres = 15;
            this.txtcelular.Name = "txtcelular";
            this.txtcelular.Query = 0;
            this.txtcelular.Size = new System.Drawing.Size(130, 20);
            this.txtcelular.Tabela = "TRPR";
            this.txtcelular.TabIndex = 9;
            // 
            // txttelefone
            // 
            this.txttelefone.Campo = "TELEFONE";
            this.txttelefone.Default = null;
            this.txttelefone.Edita = true;
            this.txttelefone.Location = new System.Drawing.Point(13, 242);
            this.txttelefone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txttelefone.MaximoCaracteres = 15;
            this.txttelefone.Name = "txttelefone";
            this.txttelefone.Query = 0;
            this.txttelefone.Size = new System.Drawing.Size(130, 20);
            this.txttelefone.Tabela = "TRPR";
            this.txttelefone.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(419, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 64;
            this.label4.Text = "Contato";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(283, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 63;
            this.label5.Text = "Fax";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(147, 226);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(22, 13);
            this.label23.TabIndex = 62;
            this.label23.Text = "Cel";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(10, 226);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(49, 13);
            this.label24.TabIndex = 61;
            this.label24.Text = "Telefone";
            // 
            // txtpais
            // 
            this.txtpais.Campo = "PAIS";
            this.txtpais.Default = null;
            this.txtpais.Edita = true;
            this.txtpais.Location = new System.Drawing.Point(645, 198);
            this.txtpais.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtpais.MaximoCaracteres = 20;
            this.txtpais.Name = "txtpais";
            this.txtpais.Query = 0;
            this.txtpais.Size = new System.Drawing.Size(100, 20);
            this.txtpais.Tabela = "TRPR";
            this.txtpais.TabIndex = 7;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(273, 183);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(50, 13);
            this.label25.TabIndex = 60;
            this.label25.Text = "Municipio";
            // 
            // lookupestado
            // 
            this.lookupestado.Campo = "CODETD";
            this.lookupestado.ColunaCodigo = "CODETD";
            this.lookupestado.ColunaDescricao = "NOME";
            this.lookupestado.ColunaIdentificador = null;
            this.lookupestado.ColunaTabela = "GETD";
            this.lookupestado.Conexao = "Start";
            this.lookupestado.Default = null;
            this.lookupestado.Edita = true;
            this.lookupestado.EditaLookup = false;
            this.lookupestado.Location = new System.Drawing.Point(13, 198);
            this.lookupestado.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lookupestado.MaximoCaracteres = null;
            this.lookupestado.Name = "lookupestado";
            this.lookupestado.NomeGrid = null;
            this.lookupestado.Query = 0;
            this.lookupestado.Size = new System.Drawing.Size(241, 36);
            this.lookupestado.Tabela = "TRPR";
            this.lookupestado.TabIndex = 5;
            this.lookupestado.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.lookupestado_SetFormConsulta);
            // 
            // lookupmunicipio
            // 
            this.lookupmunicipio.Campo = "CIDADE";
            this.lookupmunicipio.ColunaCodigo = "CODMUNICIPIO";
            this.lookupmunicipio.ColunaDescricao = "NOMEMUNICIPIO";
            this.lookupmunicipio.ColunaIdentificador = null;
            this.lookupmunicipio.ColunaTabela = "GMUNICIPIO";
            this.lookupmunicipio.Conexao = "Start";
            this.lookupmunicipio.Default = null;
            this.lookupmunicipio.Edita = true;
            this.lookupmunicipio.EditaLookup = false;
            this.lookupmunicipio.Location = new System.Drawing.Point(275, 198);
            this.lookupmunicipio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lookupmunicipio.MaximoCaracteres = null;
            this.lookupmunicipio.Name = "lookupmunicipio";
            this.lookupmunicipio.NomeGrid = null;
            this.lookupmunicipio.Query = 0;
            this.lookupmunicipio.Size = new System.Drawing.Size(351, 25);
            this.lookupmunicipio.Tabela = "TRPR";
            this.lookupmunicipio.TabIndex = 6;
            this.lookupmunicipio.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.lookupmunicipio_SetFormConsulta);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(10, 182);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(40, 13);
            this.label26.TabIndex = 59;
            this.label26.Text = "Estado";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(642, 182);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(26, 13);
            this.label27.TabIndex = 58;
            this.label27.Text = "Pais";
            // 
            // txtbairro
            // 
            this.txtbairro.Campo = "BAIRRO";
            this.txtbairro.Default = null;
            this.txtbairro.Edita = true;
            this.txtbairro.Location = new System.Drawing.Point(13, 159);
            this.txtbairro.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtbairro.MaximoCaracteres = 80;
            this.txtbairro.Name = "txtbairro";
            this.txtbairro.Query = 0;
            this.txtbairro.Size = new System.Drawing.Size(305, 20);
            this.txtbairro.Tabela = "TRPR";
            this.txtbairro.TabIndex = 4;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(10, 143);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(35, 13);
            this.label28.TabIndex = 57;
            this.label28.Text = "Bairro";
            // 
            // txtcomplemento
            // 
            this.txtcomplemento.Campo = "COMPLEMENTO";
            this.txtcomplemento.Default = null;
            this.txtcomplemento.Edita = true;
            this.txtcomplemento.Location = new System.Drawing.Point(13, 120);
            this.txtcomplemento.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtcomplemento.MaximoCaracteres = 60;
            this.txtcomplemento.Name = "txtcomplemento";
            this.txtcomplemento.Query = 0;
            this.txtcomplemento.Size = new System.Drawing.Size(305, 20);
            this.txtcomplemento.Tabela = "TRPR";
            this.txtcomplemento.TabIndex = 3;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(10, 104);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(72, 13);
            this.label29.TabIndex = 56;
            this.label29.Text = "Complemento";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(321, 65);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(44, 13);
            this.label30.TabIndex = 55;
            this.label30.Text = "Número";
            // 
            // txtnumero
            // 
            this.txtnumero.Campo = "NUMERO";
            this.txtnumero.Default = null;
            this.txtnumero.Edita = true;
            this.txtnumero.Location = new System.Drawing.Point(323, 81);
            this.txtnumero.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtnumero.MaximoCaracteres = 8;
            this.txtnumero.Name = "txtnumero";
            this.txtnumero.Query = 0;
            this.txtnumero.Size = new System.Drawing.Size(100, 20);
            this.txtnumero.Tabela = "TRPR";
            this.txtnumero.TabIndex = 2;
            // 
            // txtrua
            // 
            this.txtrua.Campo = "RUA";
            this.txtrua.Default = null;
            this.txtrua.Edita = true;
            this.txtrua.Location = new System.Drawing.Point(13, 81);
            this.txtrua.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtrua.MaximoCaracteres = 100;
            this.txtrua.Name = "txtrua";
            this.txtrua.Query = 0;
            this.txtrua.Size = new System.Drawing.Size(305, 20);
            this.txtrua.Tabela = "TRPR";
            this.txtrua.TabIndex = 1;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(10, 65);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(26, 13);
            this.label31.TabIndex = 54;
            this.label31.Text = "Rua";
            // 
            // txtcep
            // 
            this.txtcep.Campo = "CEP";
            this.txtcep.Default = null;
            this.txtcep.Edita = true;
            this.txtcep.Location = new System.Drawing.Point(13, 42);
            this.txtcep.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtcep.MaximoCaracteres = 9;
            this.txtcep.Name = "txtcep";
            this.txtcep.Query = 0;
            this.txtcep.Size = new System.Drawing.Size(100, 20);
            this.txtcep.Tabela = "TRPR";
            this.txtcep.TabIndex = 0;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(10, 26);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(26, 13);
            this.label32.TabIndex = 53;
            this.label32.Text = "CEP";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbComissaoPadrao);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.campoDecimal3);
            this.groupBox1.Controls.Add(this.campoDecimal2);
            this.groupBox1.Controls.Add(this.campoDecimal1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(572, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(255, 112);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Percentuais";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(119, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 13);
            this.label13.TabIndex = 68;
            this.label13.Text = "Perc. Desconto";
            // 
            // campoDecimal3
            // 
            this.campoDecimal3.Campo = "PERCDESC";
            this.campoDecimal3.Decimais = 2;
            this.campoDecimal3.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimal3.Edita = true;
            this.campoDecimal3.Location = new System.Drawing.Point(122, 78);
            this.campoDecimal3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.campoDecimal3.Name = "campoDecimal3";
            this.campoDecimal3.Query = 0;
            this.campoDecimal3.Size = new System.Drawing.Size(103, 20);
            this.campoDecimal3.Tabela = "TRPRCOMPL";
            this.campoDecimal3.TabIndex = 1;
            // 
            // campoDecimal2
            // 
            this.campoDecimal2.Campo = "PERCENTREPASSE";
            this.campoDecimal2.Decimais = 2;
            this.campoDecimal2.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimal2.Edita = true;
            this.campoDecimal2.Location = new System.Drawing.Point(15, 78);
            this.campoDecimal2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.campoDecimal2.Name = "campoDecimal2";
            this.campoDecimal2.Query = 0;
            this.campoDecimal2.Size = new System.Drawing.Size(99, 20);
            this.campoDecimal2.Tabela = "TRPR";
            this.campoDecimal2.TabIndex = 2;
            // 
            // campoDecimal1
            // 
            this.campoDecimal1.Campo = "PERCENTCOMISSAO";
            this.campoDecimal1.Decimais = 2;
            this.campoDecimal1.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimal1.Edita = true;
            this.campoDecimal1.Enabled = false;
            this.campoDecimal1.Location = new System.Drawing.Point(15, 36);
            this.campoDecimal1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.campoDecimal1.Name = "campoDecimal1";
            this.campoDecimal1.Query = 0;
            this.campoDecimal1.Size = new System.Drawing.Size(99, 20);
            this.campoDecimal1.Tabela = "TRPR";
            this.campoDecimal1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Comissão";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Repasse";
            // 
            // txtie
            // 
            this.txtie.Campo = "INSCRESTADUAL";
            this.txtie.Default = null;
            this.txtie.Edita = true;
            this.txtie.Location = new System.Drawing.Point(232, 103);
            this.txtie.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtie.MaximoCaracteres = 20;
            this.txtie.Name = "txtie";
            this.txtie.Query = 0;
            this.txtie.Size = new System.Drawing.Size(204, 20);
            this.txtie.Tabela = "TRPR";
            this.txtie.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(229, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Inscrição Estadual";
            // 
            // txtcnpj
            // 
            this.txtcnpj.Campo = "CGC";
            this.txtcnpj.Default = null;
            this.txtcnpj.Edita = true;
            this.txtcnpj.Location = new System.Drawing.Point(9, 103);
            this.txtcnpj.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtcnpj.MaximoCaracteres = 20;
            this.txtcnpj.Name = "txtcnpj";
            this.txtcnpj.Query = 0;
            this.txtcnpj.Size = new System.Drawing.Size(204, 20);
            this.txtcnpj.Tabela = "TRPR";
            this.txtcnpj.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "CNPJ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(455, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Inativo";
            // 
            // cmbinativo
            // 
            this.cmbinativo.Campo = "INATIVO";
            this.cmbinativo.Default = null;
            this.cmbinativo.Edita = true;
            codigoNome1.Codigo = "0";
            codigoNome1.Nome = "Ativo";
            codigoNome2.Codigo = "1";
            codigoNome2.Nome = "Inativo";
            this.cmbinativo.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2};
            this.cmbinativo.Location = new System.Drawing.Point(457, 64);
            this.cmbinativo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbinativo.Name = "cmbinativo";
            this.cmbinativo.Query = 0;
            this.cmbinativo.Size = new System.Drawing.Size(100, 21);
            this.cmbinativo.Tabela = "TRPR";
            this.cmbinativo.TabIndex = 3;
            // 
            // txtnomefantasia
            // 
            this.txtnomefantasia.Campo = "NOMEFANTASIA";
            this.txtnomefantasia.Default = null;
            this.txtnomefantasia.Edita = true;
            this.txtnomefantasia.Location = new System.Drawing.Point(131, 27);
            this.txtnomefantasia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtnomefantasia.MaximoCaracteres = 20;
            this.txtnomefantasia.Name = "txtnomefantasia";
            this.txtnomefantasia.Query = 0;
            this.txtnomefantasia.Size = new System.Drawing.Size(427, 20);
            this.txtnomefantasia.Tabela = "TRPR";
            this.txtnomefantasia.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nome Fantasia";
            // 
            // txtnome
            // 
            this.txtnome.Campo = "NOME";
            this.txtnome.Default = null;
            this.txtnome.Edita = true;
            this.txtnome.Location = new System.Drawing.Point(8, 66);
            this.txtnome.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtnome.MaximoCaracteres = 40;
            this.txtnome.Name = "txtnome";
            this.txtnome.Query = 0;
            this.txtnome.Size = new System.Drawing.Size(428, 20);
            this.txtnome.Tabela = "TRPR";
            this.txtnome.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Código";
            // 
            // txtcodigo
            // 
            this.txtcodigo.Campo = "CODRPR";
            this.txtcodigo.Default = null;
            this.txtcodigo.Edita = true;
            this.txtcodigo.Location = new System.Drawing.Point(8, 27);
            this.txtcodigo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtcodigo.MaximoCaracteres = 15;
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Query = 0;
            this.txtcodigo.Size = new System.Drawing.Size(100, 20);
            this.txtcodigo.Tabela = "TRPR";
            this.txtcodigo.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(861, 398);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Usuários";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(2, 2);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnIncluir);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.lookupusuario);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grid1);
            this.splitContainer1.Size = new System.Drawing.Size(857, 394);
            this.splitContainer1.SplitterDistance = 47;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnIncluir
            // 
            this.btnIncluir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnIncluir.ImageOptions.Image")));
            this.btnIncluir.Location = new System.Drawing.Point(473, 26);
            this.btnIncluir.Margin = new System.Windows.Forms.Padding(2);
            this.btnIncluir.Name = "btnIncluir";
            this.btnIncluir.Size = new System.Drawing.Size(25, 25);
            this.btnIncluir.TabIndex = 61;
            this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 60;
            this.label12.Text = "Usuario";
            // 
            // lookupusuario
            // 
            this.lookupusuario.Campo = "USUARIO";
            this.lookupusuario.ColunaCodigo = "USUARIO";
            this.lookupusuario.ColunaDescricao = "USUARIO";
            this.lookupusuario.ColunaIdentificador = null;
            this.lookupusuario.ColunaTabela = "USUARIO";
            this.lookupusuario.Conexao = "Start";
            this.lookupusuario.Default = null;
            this.lookupusuario.Edita = true;
            this.lookupusuario.EditaLookup = false;
            this.lookupusuario.Location = new System.Drawing.Point(16, 27);
            this.lookupusuario.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lookupusuario.MaximoCaracteres = null;
            this.lookupusuario.Name = "lookupusuario";
            this.lookupusuario.NomeGrid = null;
            this.lookupusuario.Query = 0;
            this.lookupusuario.Size = new System.Drawing.Size(439, 25);
            this.lookupusuario.Tabela = "TRPR";
            this.lookupusuario.TabIndex = 13;
            this.lookupusuario.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.lookupusuario_SetFormConsulta);
            // 
            // grid1
            // 
            this.grid1.AutoAjuste = true;
            this.grid1.BotaoEditar = false;
            this.grid1.BotaoExcluir = true;
            this.grid1.BotaoNovo = true;
            this.grid1.Conexao = "Start";
            this.grid1.Consulta = new string[] {
        "SELECT CODUSUARIO as \'Usuários\' ",
        "FROM ZTRPR",
        "WHERE CODCOLIGADA = ? ",
        "AND CODRPR = ?"};
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.Formatacao = null;
            this.grid1.FormPai = null;
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grid1.ModoFiltro = false;
            this.grid1.Name = "grid1";
            this.grid1.NomeGrid = null;
            this.grid1.SelecaoCascata = true;
            this.grid1.Selecionou = false;
            this.grid1.Size = new System.Drawing.Size(857, 344);
            this.grid1.TabIndex = 6;
            this.grid1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.grid1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.grid1_SetParametros);
            this.grid1.Novo += new AppLib.Windows.GridData.NovoHandler(this.grid1_Novo);
            this.grid1.Editar += new AppLib.Windows.GridData.EditarHandler(this.grid1_Editar);
            this.grid1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.grid1_Excluir);
            // 
            // cbComissaoPadrao
            // 
            this.cbComissaoPadrao.Location = new System.Drawing.Point(121, 35);
            this.cbComissaoPadrao.Name = "cbComissaoPadrao";
            this.cbComissaoPadrao.Properties.Caption = "Usar comissão padrão";
            this.cbComissaoPadrao.Size = new System.Drawing.Size(129, 19);
            this.cbComissaoPadrao.TabIndex = 69;
            this.cbComissaoPadrao.CheckedChanged += new System.EventHandler(this.cbComissaoPadrao_CheckedChanged);
            // 
            // FormRepresentantesCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 524);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormRepresentantesCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT * FROM TRPR INNER JOIN TRPRCOMPL ON TRPR.CODCOLIGADA = TRPRCOMPL.CODCOLIGA" +
            "DA AND TRPR.CODRPR = TRPRCOMPL.CODRPR WHERE TRPR.CODCOLIGADA = ? AND TRPR.CODRPR" +
            " = ?"};
            query1.Parametros = new string[] {
        "CODCOLIGADA",
        "CODRPR"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "TRPR";
            this.Text = "Cadastro de Representantes";
            this.ValidarSalvar += new AppLib.Windows.FormCadastroData.ValidarSalvarHandler(this.FormRepresentantesCadastro_ValidarSalvar);
            this.AntesEditar += new AppLib.Windows.FormCadastroData.AntesEditarHandler(this.FormRepresentantesCadastro_AntesEditar);
            this.AposEditar += new AppLib.Windows.FormCadastroData.AposEditarHandler(this.FormRepresentantesCadastro_AposEditar);
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormRepresentantesCadastro_AntesSalvar);
            this.AposSalvar += new AppLib.Windows.FormCadastroData.AposSalvarHandler(this.FormRepresentantesCadastro_AposSalvar);
            this.Load += new System.EventHandler(this.FormRepresentantesCadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbComissaoPadrao.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label10;
        private AppLib.Windows.CampoTexto txtie;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private AppLib.Windows.CampoTexto txtcnpj;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private AppLib.Windows.CampoLista cmbinativo;
        private AppLib.Windows.CampoTexto txtnomefantasia;
        private System.Windows.Forms.Label label3;
        private AppLib.Windows.CampoTexto txtnome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoTexto txtcodigo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private AppLib.Windows.CampoTexto txtcontato;
        private AppLib.Windows.CampoTexto txtfax;
        private AppLib.Windows.CampoTexto txtcelular;
        private AppLib.Windows.CampoTexto txttelefone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private AppLib.Windows.CampoTexto txtpais;
        private System.Windows.Forms.Label label25;
        private AppLib.Windows.CampoLookup lookupestado;
        private AppLib.Windows.CampoLookup lookupmunicipio;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private AppLib.Windows.CampoTexto txtbairro;
        private System.Windows.Forms.Label label28;
        private AppLib.Windows.CampoTexto txtcomplemento;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private AppLib.Windows.CampoTexto txtnumero;
        private AppLib.Windows.CampoTexto txtrua;
        private System.Windows.Forms.Label label31;
        private AppLib.Windows.CampoTexto txtcep;
        private System.Windows.Forms.Label label32;
        private AppLib.Windows.CampoInteiro txtCODCOLIGADA;
        private System.Windows.Forms.Label label11;
        private AppLib.Windows.CampoDecimal campoDecimal2;
        private AppLib.Windows.CampoDecimal campoDecimal1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public AppLib.Windows.GridData grid1;
        private AppLib.Windows.CampoLookup lookupusuario;
        private DevExpress.XtraEditors.SimpleButton btnIncluir;
        private System.Windows.Forms.Label label12;
        private AppLib.Windows.CampoDecimal campoDecimal3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label13;
        private DevExpress.XtraEditors.CheckEdit cbComissaoPadrao;
    }
}