namespace AppFatureClient
{
    partial class FormOrdemProducaoCadastro
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
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            AppLib.Windows.Query query2 = new AppLib.Windows.Query();
            AppLib.Windows.Query query3 = new AppLib.Windows.Query();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label28 = new System.Windows.Forms.Label();
            this.campoDecimalVALORLIQUIDO = new AppLib.Windows.CampoDecimal();
            this.label29 = new System.Windows.Forms.Label();
            this.campoDecimalVALOROUTROS = new AppLib.Windows.CampoDecimal();
            this.label30 = new System.Windows.Forms.Label();
            this.campoDecimalVALORBRUTO = new AppLib.Windows.CampoDecimal();
            this.label31 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.campoLista15 = new AppLib.Windows.CampoLista();
            this.label48 = new System.Windows.Forms.Label();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.label1 = new System.Windows.Forms.Label();
            this.campoInteiro5 = new AppLib.Windows.CampoInteiro();
            this.campoInteiro6 = new AppLib.Windows.CampoInteiro();
            this.label46 = new System.Windows.Forms.Label();
            this.campoLookupCODCPG = new AppLib.Windows.CampoLookup();
            this.campoTextoNORDEM = new AppLib.Windows.CampoTexto();
            this.label45 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.campoInteiroPRAZOENTREGA = new AppLib.Windows.CampoInteiro();
            this.label33 = new System.Windows.Forms.Label();
            this.campoTextoNUMEROMOV = new AppLib.Windows.CampoTexto();
            this.label35 = new System.Windows.Forms.Label();
            this.campoDataENTREGA = new AppLib.Windows.CampoData();
            this.label36 = new System.Windows.Forms.Label();
            this.campoDataEMISSAO = new AppLib.Windows.CampoData();
            this.label37 = new System.Windows.Forms.Label();
            this.campoLookupCODCFO = new AppLib.Windows.CampoLookup();
            this.label44 = new System.Windows.Forms.Label();
            this.campoInteiro2 = new AppLib.Windows.CampoInteiro();
            this.label34 = new System.Windows.Forms.Label();
            this.campoInteiro1 = new AppLib.Windows.CampoInteiro();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.campoMemoHISTORICOLONGO = new AppLib.Windows.CampoMemo();
            this.label26 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridData1 = new AppLib.Windows.GridData();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gridParcelas = new AppLib.Windows.GridData();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.campoDecimalVALORLIQUIDO);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.campoDecimalVALOROUTROS);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.campoDecimalVALORBRUTO);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 502);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(787, 61);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(371, 15);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(67, 13);
            this.label28.TabIndex = 40;
            this.label28.Text = "Valor Liquido";
            // 
            // campoDecimalVALORLIQUIDO
            // 
            this.campoDecimalVALORLIQUIDO.Campo = "VALORLIQUIDO";
            this.campoDecimalVALORLIQUIDO.Decimais = 2;
            this.campoDecimalVALORLIQUIDO.Default = null;
            this.campoDecimalVALORLIQUIDO.Edita = true;
            this.campoDecimalVALORLIQUIDO.Enabled = false;
            this.campoDecimalVALORLIQUIDO.Location = new System.Drawing.Point(374, 31);
            this.campoDecimalVALORLIQUIDO.Name = "campoDecimalVALORLIQUIDO";
            this.campoDecimalVALORLIQUIDO.Query = 0;
            this.campoDecimalVALORLIQUIDO.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalVALORLIQUIDO.Tabela = "TMOV";
            this.campoDecimalVALORLIQUIDO.TabIndex = 39;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(265, 15);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(47, 13);
            this.label29.TabIndex = 38;
            this.label29.Text = "Subtotal";
            // 
            // campoDecimalVALOROUTROS
            // 
            this.campoDecimalVALOROUTROS.Campo = "VALOROUTROS";
            this.campoDecimalVALOROUTROS.Decimais = 2;
            this.campoDecimalVALOROUTROS.Default = null;
            this.campoDecimalVALOROUTROS.Edita = true;
            this.campoDecimalVALOROUTROS.Enabled = false;
            this.campoDecimalVALOROUTROS.Location = new System.Drawing.Point(268, 31);
            this.campoDecimalVALOROUTROS.Name = "campoDecimalVALOROUTROS";
            this.campoDecimalVALOROUTROS.Query = 0;
            this.campoDecimalVALOROUTROS.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalVALOROUTROS.Tabela = "TMOV";
            this.campoDecimalVALOROUTROS.TabIndex = 37;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(159, 15);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(60, 13);
            this.label30.TabIndex = 36;
            this.label30.Text = "Valor Bruto";
            // 
            // campoDecimalVALORBRUTO
            // 
            this.campoDecimalVALORBRUTO.Campo = "VALORBRUTO";
            this.campoDecimalVALORBRUTO.Decimais = 2;
            this.campoDecimalVALORBRUTO.Default = null;
            this.campoDecimalVALORBRUTO.Edita = true;
            this.campoDecimalVALORBRUTO.Enabled = false;
            this.campoDecimalVALORBRUTO.Location = new System.Drawing.Point(162, 31);
            this.campoDecimalVALORBRUTO.Name = "campoDecimalVALORBRUTO";
            this.campoDecimalVALORBRUTO.Query = 0;
            this.campoDecimalVALORBRUTO.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalVALORBRUTO.Tabela = "TMOV";
            this.campoDecimalVALORBRUTO.TabIndex = 35;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(55, 32);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(56, 16);
            this.label31.TabIndex = 34;
            this.label31.Text = "Totais:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(787, 463);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.campoLista15);
            this.tabPage2.Controls.Add(this.label48);
            this.tabPage2.Controls.Add(this.campoTexto1);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.campoInteiro5);
            this.tabPage2.Controls.Add(this.campoInteiro6);
            this.tabPage2.Controls.Add(this.label46);
            this.tabPage2.Controls.Add(this.campoLookupCODCPG);
            this.tabPage2.Controls.Add(this.campoTextoNORDEM);
            this.tabPage2.Controls.Add(this.label45);
            this.tabPage2.Controls.Add(this.label32);
            this.tabPage2.Controls.Add(this.campoInteiroPRAZOENTREGA);
            this.tabPage2.Controls.Add(this.label33);
            this.tabPage2.Controls.Add(this.campoTextoNUMEROMOV);
            this.tabPage2.Controls.Add(this.label35);
            this.tabPage2.Controls.Add(this.campoDataENTREGA);
            this.tabPage2.Controls.Add(this.label36);
            this.tabPage2.Controls.Add(this.campoDataEMISSAO);
            this.tabPage2.Controls.Add(this.label37);
            this.tabPage2.Controls.Add(this.campoLookupCODCFO);
            this.tabPage2.Controls.Add(this.label44);
            this.tabPage2.Controls.Add(this.campoInteiro2);
            this.tabPage2.Controls.Add(this.label34);
            this.tabPage2.Controls.Add(this.campoInteiro1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(779, 437);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Identificação";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // campoLista15
            // 
            this.campoLista15.Campo = "TIPOVENDA";
            this.campoLista15.Default = null;
            this.campoLista15.Edita = true;
            this.campoLista15.Enabled = false;
            codigoNome1.Codigo = "";
            codigoNome1.Nome = "Selecione";
            codigoNome2.Codigo = "C";
            codigoNome2.Nome = "Consumo";
            codigoNome3.Codigo = "R";
            codigoNome3.Nome = "Revenda";
            this.campoLista15.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2,
        codigoNome3};
            this.campoLista15.Location = new System.Drawing.Point(341, 148);
            this.campoLista15.Name = "campoLista15";
            this.campoLista15.Query = 0;
            this.campoLista15.Size = new System.Drawing.Size(100, 21);
            this.campoLista15.Tabela = "TMOV";
            this.campoLista15.TabIndex = 85;
            this.campoLista15.Visible = false;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(337, 131);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(75, 13);
            this.label48.TabIndex = 84;
            this.label48.Text = "Tipo de Venda";
            this.label48.Visible = false;
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "SEGUNDONUMERO";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(235, 68);
            this.campoTexto1.MaximoCaracteres = 20;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(100, 20);
            this.campoTexto1.Tabela = "TMOV";
            this.campoTexto1.TabIndex = 83;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(232, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 82;
            this.label1.Text = "Segundo Número";
            // 
            // campoInteiro5
            // 
            this.campoInteiro5.Campo = "CODCOLIGADA";
            this.campoInteiro5.Default = null;
            this.campoInteiro5.Edita = true;
            this.campoInteiro5.Enabled = false;
            this.campoInteiro5.Location = new System.Drawing.Point(341, 29);
            this.campoInteiro5.Name = "campoInteiro5";
            this.campoInteiro5.Query = 2;
            this.campoInteiro5.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro5.Tabela = "TMOVHISTORICO";
            this.campoInteiro5.TabIndex = 81;
            this.campoInteiro5.Visible = false;
            // 
            // campoInteiro6
            // 
            this.campoInteiro6.Campo = "IDMOV";
            this.campoInteiro6.Default = null;
            this.campoInteiro6.Edita = true;
            this.campoInteiro6.Enabled = false;
            this.campoInteiro6.Location = new System.Drawing.Point(235, 29);
            this.campoInteiro6.Name = "campoInteiro6";
            this.campoInteiro6.Query = 2;
            this.campoInteiro6.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro6.Tabela = "TMOVHISTORICO";
            this.campoInteiro6.TabIndex = 80;
            this.campoInteiro6.Visible = false;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(20, 178);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(93, 13);
            this.label46.TabIndex = 77;
            this.label46.Text = "Cond. Pagamento";
            // 
            // campoLookupCODCPG
            // 
            this.campoLookupCODCPG.AutoSize = true;
            this.campoLookupCODCPG.Campo = "CODCPG";
            this.campoLookupCODCPG.ColunaCodigo = "CODCPG";
            this.campoLookupCODCPG.ColunaDescricao = "NOME";
            this.campoLookupCODCPG.ColunaIdentificador = null;
            this.campoLookupCODCPG.ColunaTabela = "TCPG";
            this.campoLookupCODCPG.Conexao = "Start";
            this.campoLookupCODCPG.Default = null;
            this.campoLookupCODCPG.Edita = true;
            this.campoLookupCODCPG.EditaLookup = false;
            this.campoLookupCODCPG.Enabled = false;
            this.campoLookupCODCPG.Location = new System.Drawing.Point(23, 194);
            this.campoLookupCODCPG.MaximoCaracteres = null;
            this.campoLookupCODCPG.Name = "campoLookupCODCPG";
            this.campoLookupCODCPG.NomeGrid = null;
            this.campoLookupCODCPG.Query = 0;
            this.campoLookupCODCPG.Size = new System.Drawing.Size(567, 24);
            this.campoLookupCODCPG.Tabela = "TMOV";
            this.campoLookupCODCPG.TabIndex = 8;
            this.campoLookupCODCPG.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupCODCPG_SetFormConsulta);
            this.campoLookupCODCPG.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupCODCPG_SetDescricao);
            // 
            // campoTextoNORDEM
            // 
            this.campoTextoNORDEM.Campo = "NORDEM";
            this.campoTextoNORDEM.Default = null;
            this.campoTextoNORDEM.Edita = true;
            this.campoTextoNORDEM.Enabled = false;
            this.campoTextoNORDEM.Location = new System.Drawing.Point(129, 68);
            this.campoTextoNORDEM.MaximoCaracteres = null;
            this.campoTextoNORDEM.Name = "campoTextoNORDEM";
            this.campoTextoNORDEM.Query = 0;
            this.campoTextoNORDEM.Size = new System.Drawing.Size(100, 20);
            this.campoTextoNORDEM.Tabela = "TMOV";
            this.campoTextoNORDEM.TabIndex = 3;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(126, 52);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(75, 13);
            this.label45.TabIndex = 74;
            this.label45.Text = "Pedido Cliente";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(232, 133);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(75, 13);
            this.label32.TabIndex = 73;
            this.label32.Text = "Prazo Entrega";
            // 
            // campoInteiroPRAZOENTREGA
            // 
            this.campoInteiroPRAZOENTREGA.Campo = "PRAZOENTREGA";
            this.campoInteiroPRAZOENTREGA.Default = null;
            this.campoInteiroPRAZOENTREGA.Edita = true;
            this.campoInteiroPRAZOENTREGA.Location = new System.Drawing.Point(235, 149);
            this.campoInteiroPRAZOENTREGA.Name = "campoInteiroPRAZOENTREGA";
            this.campoInteiroPRAZOENTREGA.Query = 0;
            this.campoInteiroPRAZOENTREGA.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroPRAZOENTREGA.Tabela = "TMOV";
            this.campoInteiroPRAZOENTREGA.TabIndex = 7;
            this.campoInteiroPRAZOENTREGA.Leave += new System.EventHandler(this.campoInteiroPRAZOENTREGA_Leave);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(20, 52);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(87, 13);
            this.label33.TabIndex = 71;
            this.label33.Text = "Núm. Movimento";
            // 
            // campoTextoNUMEROMOV
            // 
            this.campoTextoNUMEROMOV.Campo = "NUMEROMOV";
            this.campoTextoNUMEROMOV.Default = null;
            this.campoTextoNUMEROMOV.Edita = true;
            this.campoTextoNUMEROMOV.Enabled = false;
            this.campoTextoNUMEROMOV.Location = new System.Drawing.Point(23, 68);
            this.campoTextoNUMEROMOV.MaximoCaracteres = null;
            this.campoTextoNUMEROMOV.Name = "campoTextoNUMEROMOV";
            this.campoTextoNUMEROMOV.Query = 0;
            this.campoTextoNUMEROMOV.Size = new System.Drawing.Size(100, 20);
            this.campoTextoNUMEROMOV.Tabela = "TMOV";
            this.campoTextoNUMEROMOV.TabIndex = 2;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(126, 133);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(71, 13);
            this.label35.TabIndex = 69;
            this.label35.Text = "Data Entrega";
            // 
            // campoDataENTREGA
            // 
            this.campoDataENTREGA.Campo = "DATAENTREGA";
            this.campoDataENTREGA.Default = null;
            this.campoDataENTREGA.Edita = true;
            this.campoDataENTREGA.Location = new System.Drawing.Point(129, 149);
            this.campoDataENTREGA.Name = "campoDataENTREGA";
            this.campoDataENTREGA.Query = 0;
            this.campoDataENTREGA.Size = new System.Drawing.Size(100, 20);
            this.campoDataENTREGA.Tabela = "TMOV";
            this.campoDataENTREGA.TabIndex = 6;
            this.campoDataENTREGA.Leave += new System.EventHandler(this.campoDataENTREGA_Leave);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(20, 133);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(71, 13);
            this.label36.TabIndex = 67;
            this.label36.Text = "Data Emissão";
            // 
            // campoDataEMISSAO
            // 
            this.campoDataEMISSAO.Campo = "DATAEMISSAO";
            this.campoDataEMISSAO.Default = null;
            this.campoDataEMISSAO.Edita = true;
            this.campoDataEMISSAO.Location = new System.Drawing.Point(23, 149);
            this.campoDataEMISSAO.Name = "campoDataEMISSAO";
            this.campoDataEMISSAO.Query = 0;
            this.campoDataEMISSAO.Size = new System.Drawing.Size(100, 20);
            this.campoDataEMISSAO.Tabela = "TMOV";
            this.campoDataEMISSAO.TabIndex = 5;
            this.campoDataEMISSAO.Leave += new System.EventHandler(this.campoDataEMISSAO_Leave);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(20, 91);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(40, 13);
            this.label37.TabIndex = 65;
            this.label37.Text = "Cliente";
            // 
            // campoLookupCODCFO
            // 
            this.campoLookupCODCFO.AutoSize = true;
            this.campoLookupCODCFO.Campo = "CODCFO";
            this.campoLookupCODCFO.ColunaCodigo = "CODCFO";
            this.campoLookupCODCFO.ColunaDescricao = "NOMEFANTASIA";
            this.campoLookupCODCFO.ColunaIdentificador = null;
            this.campoLookupCODCFO.ColunaTabela = "ZVWFCFO";
            this.campoLookupCODCFO.Conexao = "Start";
            this.campoLookupCODCFO.Default = null;
            this.campoLookupCODCFO.Edita = true;
            this.campoLookupCODCFO.EditaLookup = false;
            this.campoLookupCODCFO.Enabled = false;
            this.campoLookupCODCFO.Location = new System.Drawing.Point(23, 107);
            this.campoLookupCODCFO.MaximoCaracteres = null;
            this.campoLookupCODCFO.Name = "campoLookupCODCFO";
            this.campoLookupCODCFO.NomeGrid = null;
            this.campoLookupCODCFO.Query = 0;
            this.campoLookupCODCFO.Size = new System.Drawing.Size(568, 24);
            this.campoLookupCODCFO.Tabela = "TMOV";
            this.campoLookupCODCFO.TabIndex = 4;
            this.campoLookupCODCFO.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupCODCFO_SetFormConsulta);
            this.campoLookupCODCFO.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupCODCFO_SetDescricao);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(127, 14);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(103, 13);
            this.label44.TabIndex = 63;
            this.label44.Text = "Código da Coligada:";
            this.label44.Visible = false;
            // 
            // campoInteiro2
            // 
            this.campoInteiro2.Campo = "CODCOLIGADA";
            this.campoInteiro2.Default = null;
            this.campoInteiro2.Edita = true;
            this.campoInteiro2.Enabled = false;
            this.campoInteiro2.Location = new System.Drawing.Point(129, 29);
            this.campoInteiro2.Name = "campoInteiro2";
            this.campoInteiro2.Query = 0;
            this.campoInteiro2.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro2.Tabela = "TMOV";
            this.campoInteiro2.TabIndex = 1;
            this.campoInteiro2.Visible = false;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(20, 14);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(73, 13);
            this.label34.TabIndex = 28;
            this.label34.Text = "ID Movimento";
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "IDMOV";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Edita = true;
            this.campoInteiro1.Enabled = false;
            this.campoInteiro1.Location = new System.Drawing.Point(23, 29);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro1.Tabela = "TMOV";
            this.campoInteiro1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.campoMemoHISTORICOLONGO);
            this.tabPage3.Controls.Add(this.label26);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(779, 437);
            this.tabPage3.TabIndex = 5;
            this.tabPage3.Text = "Observações";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // campoMemoHISTORICOLONGO
            // 
            this.campoMemoHISTORICOLONGO.Campo = "HISTORICOLONGO";
            this.campoMemoHISTORICOLONGO.Default = "";
            this.campoMemoHISTORICOLONGO.Edita = true;
            this.campoMemoHISTORICOLONGO.Location = new System.Drawing.Point(6, 282);
            this.campoMemoHISTORICOLONGO.MaximoCaracteres = null;
            this.campoMemoHISTORICOLONGO.Name = "campoMemoHISTORICOLONGO";
            this.campoMemoHISTORICOLONGO.Query = 2;
            this.campoMemoHISTORICOLONGO.Size = new System.Drawing.Size(763, 157);
            this.campoMemoHISTORICOLONGO.Tabela = "TMOVHISTORICO";
            this.campoMemoHISTORICOLONGO.TabIndex = 33;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(3, 266);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 13);
            this.label26.TabIndex = 34;
            this.label26.Text = "Histórico";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridData1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(779, 437);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Financeiro";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridData1
            // 
            this.gridData1.AutoAjuste = true;
            this.gridData1.BotaoEditar = false;
            this.gridData1.BotaoExcluir = false;
            this.gridData1.BotaoNovo = false;
            this.gridData1.Conexao = "Start";
            this.gridData1.Consulta = new string[] {
        "SELECT FLAN.CODCOLIGADA, FLAN.IDMOV, FLAN.IDLAN, FLAN.CODTDO, FLAN.NUMERODOCUMENT" +
            "O, FCFO.NOMEFANTASIA, FLAN.DATAEMISSAO, FLAN.DATAVENCIMENTO, CAST(FLAN.VALORORIG" +
            "INAL AS NUMERIC(15,2)) VALORORIGINAL",
        "FROM FLAN, FCFO",
        "WHERE ",
        " FLAN.CODCOLCFO = FCFO.CODCOLIGADA",
        "AND FLAN.CODCFO = FCFO.CODCFO",
        "AND FLAN.CODCOLIGADA = ? AND FLAN.IDMOV = ?"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(3, 3);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = "FLAN";
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(773, 431);
            this.gridData1.TabIndex = 0;
            this.gridData1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData1_SetParametros);
            this.gridData1.Editar += new AppLib.Windows.GridData.EditarHandler(this.gridData1_Editar);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.gridParcelas);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(779, 437);
            this.tabPage4.TabIndex = 6;
            this.tabPage4.Text = "Parcelas";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // gridParcelas
            // 
            this.gridParcelas.AutoAjuste = true;
            this.gridParcelas.BotaoEditar = false;
            this.gridParcelas.BotaoExcluir = false;
            this.gridParcelas.BotaoNovo = false;
            this.gridParcelas.Conexao = "Start";
            this.gridParcelas.Consulta = new string[] {
        "SELECT ",
        "\tCODCOLIGADA,",
        "\tIDMOV, ",
        "\tNUMEROPARCELA,",
        "\tCODTMV,",
        "\tNUMEROMOV,",
        "\tVENCIMENTO,",
        "\tVALOR",
        "FROM ",
        "\tZPARCELAS ",
        "WHERE ",
        "\tCODCOLIGADA = ?",
        "\t AND IDMOV = ?"};
            this.gridParcelas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridParcelas.Formatacao = null;
            this.gridParcelas.FormPai = null;
            this.gridParcelas.Location = new System.Drawing.Point(3, 3);
            this.gridParcelas.ModoFiltro = false;
            this.gridParcelas.Name = "gridParcelas";
            this.gridParcelas.NomeGrid = "ZPARCELAS";
            this.gridParcelas.SelecaoCascata = true;
            this.gridParcelas.Selecionou = false;
            this.gridParcelas.Size = new System.Drawing.Size(773, 431);
            this.gridParcelas.TabIndex = 1;
            this.gridParcelas.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridParcelas.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridParcelas.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridParcelas_SetParametros);
            // 
            // FormOrdemProducaoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 624);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormOrdemProducaoCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM TMOV",
        "WHERE CODCOLIGADA = ?",
        "  AND IDMOV = ?"};
            query1.Parametros = new string[] {
        "CODCOLIGADA",
        "IDMOV"};
            query2.Conexao = "Start";
            query2.Consulta = new string[] {
        "SELECT *",
        "FROM TMOVCOMPL",
        "WHERE CODCOLIGADA = ?",
        "  AND IDMOV = ?"};
            query2.Parametros = new string[] {
        "CODCOLIGADA",
        "IDMOV"};
            query3.Conexao = "Start";
            query3.Consulta = new string[] {
        "SELECT * FROM TMOVHISTORICO WHERE CODCOLIGADA = ? AND IDMOV = ?"};
            query3.Parametros = new string[] {
        "CODCOLIGADA",
        "IDMOV"};
            this.Querys = new AppLib.Windows.Query[] {
        query1,
        query2,
        query3};
            this.TabelaPrincipal = "TMOV";
            this.Text = "Cotações Diversas";
            this.ValidarSalvar += new AppLib.Windows.FormCadastroData.ValidarSalvarHandler(this.FormOrdemProducaoCadastro_ValidarSalvar);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label28;
        private AppLib.Windows.CampoDecimal campoDecimalVALORLIQUIDO;
        private System.Windows.Forms.Label label29;
        private AppLib.Windows.CampoDecimal campoDecimalVALOROUTROS;
        private System.Windows.Forms.Label label30;
        private AppLib.Windows.CampoDecimal campoDecimalVALORBRUTO;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label46;
        private AppLib.Windows.CampoLookup campoLookupCODCPG;
        private AppLib.Windows.CampoTexto campoTextoNORDEM;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label32;
        private AppLib.Windows.CampoInteiro campoInteiroPRAZOENTREGA;
        private System.Windows.Forms.Label label33;
        private AppLib.Windows.CampoTexto campoTextoNUMEROMOV;
        private System.Windows.Forms.Label label35;
        private AppLib.Windows.CampoData campoDataENTREGA;
        private System.Windows.Forms.Label label36;
        private AppLib.Windows.CampoData campoDataEMISSAO;
        private System.Windows.Forms.Label label37;
        private AppLib.Windows.CampoLookup campoLookupCODCFO;
        private System.Windows.Forms.Label label44;
        private AppLib.Windows.CampoInteiro campoInteiro2;
        private System.Windows.Forms.Label label34;
        private AppLib.Windows.CampoInteiro campoInteiro1;
        private System.Windows.Forms.TabPage tabPage1;
        private AppLib.Windows.GridData gridData1;
        private System.Windows.Forms.TabPage tabPage3;
        private AppLib.Windows.CampoMemo campoMemoHISTORICOLONGO;
        private System.Windows.Forms.Label label26;
        private AppLib.Windows.CampoInteiro campoInteiro5;
        private AppLib.Windows.CampoInteiro campoInteiro6;
        private AppLib.Windows.CampoTexto campoTexto1;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoLista campoLista15;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TabPage tabPage4;
        private AppLib.Windows.GridData gridParcelas;
    }
}