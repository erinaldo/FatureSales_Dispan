namespace AppFatureClient
{
    partial class FormRequisicaoPecasItem
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
            this.comboBoxMATERIALINTERLIGACAO = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.campoTextoPROJETO = new AppLib.Windows.CampoTexto();
            this.label6 = new System.Windows.Forms.Label();
            this.campoMemoHISTORICOLONGO = new AppLib.Windows.CampoMemo();
            this.label30 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.campoDecimalALIQUOTA = new AppLib.Windows.CampoDecimal();
            this.comboBoxPRODUTOCOMPOSTO = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.campoTextoCODUND = new AppLib.Windows.CampoTexto();
            this.label4 = new System.Windows.Forms.Label();
            this.campoDecimalPRECOUNITARIO = new AppLib.Windows.CampoDecimal();
            this.label3 = new System.Windows.Forms.Label();
            this.campoDecimalQUANTIDADE = new AppLib.Windows.CampoDecimal();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.campoLookupPRODUTO = new AppLib.Windows.CampoLookup();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(639, 348);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBoxMATERIALINTERLIGACAO);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.campoTextoPROJETO);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.campoMemoHISTORICOLONGO);
            this.tabPage1.Controls.Add(this.label30);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.campoDecimalALIQUOTA);
            this.tabPage1.Controls.Add(this.comboBoxPRODUTOCOMPOSTO);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.campoTextoCODUND);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.campoDecimalPRECOUNITARIO);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.campoDecimalQUANTIDADE);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.campoLookupPRODUTO);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(631, 322);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Principal";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBoxMATERIALINTERLIGACAO
            // 
            this.comboBoxMATERIALINTERLIGACAO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMATERIALINTERLIGACAO.FormattingEnabled = true;
            this.comboBoxMATERIALINTERLIGACAO.Items.AddRange(new object[] {
            "0 - Sim",
            "1 - Não"});
            this.comboBoxMATERIALINTERLIGACAO.Location = new System.Drawing.Point(234, 122);
            this.comboBoxMATERIALINTERLIGACAO.Name = "comboBoxMATERIALINTERLIGACAO";
            this.comboBoxMATERIALINTERLIGACAO.Size = new System.Drawing.Size(99, 21);
            this.comboBoxMATERIALINTERLIGACAO.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(231, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Material Interligação";
            // 
            // campoTextoPROJETO
            // 
            this.campoTextoPROJETO.Campo = null;
            this.campoTextoPROJETO.Default = null;
            this.campoTextoPROJETO.Edita = true;
            this.campoTextoPROJETO.Location = new System.Drawing.Point(128, 123);
            this.campoTextoPROJETO.MaximoCaracteres = null;
            this.campoTextoPROJETO.Name = "campoTextoPROJETO";
            this.campoTextoPROJETO.Query = 0;
            this.campoTextoPROJETO.Size = new System.Drawing.Size(100, 20);
            this.campoTextoPROJETO.Tabela = null;
            this.campoTextoPROJETO.TabIndex = 41;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(125, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Ref. Item Projeto";
            // 
            // campoMemoHISTORICOLONGO
            // 
            this.campoMemoHISTORICOLONGO.Campo = "HISTORICOLONGO";
            this.campoMemoHISTORICOLONGO.Default = "";
            this.campoMemoHISTORICOLONGO.Edita = true;
            this.campoMemoHISTORICOLONGO.Location = new System.Drawing.Point(23, 167);
            this.campoMemoHISTORICOLONGO.MaximoCaracteres = null;
            this.campoMemoHISTORICOLONGO.Name = "campoMemoHISTORICOLONGO";
            this.campoMemoHISTORICOLONGO.Query = 0;
            this.campoMemoHISTORICOLONGO.Size = new System.Drawing.Size(576, 157);
            this.campoMemoHISTORICOLONGO.Tabela = null;
            this.campoMemoHISTORICOLONGO.TabIndex = 8;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(20, 151);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(48, 13);
            this.label30.TabIndex = 40;
            this.label30.Text = "Histórico";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(125, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Aliquota IPI";
            // 
            // campoDecimalALIQUOTA
            // 
            this.campoDecimalALIQUOTA.Campo = "ALIQUOTAIPI";
            this.campoDecimalALIQUOTA.Decimais = 2;
            this.campoDecimalALIQUOTA.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimalALIQUOTA.Edita = true;
            this.campoDecimalALIQUOTA.Enabled = false;
            this.campoDecimalALIQUOTA.Location = new System.Drawing.Point(128, 81);
            this.campoDecimalALIQUOTA.Name = "campoDecimalALIQUOTA";
            this.campoDecimalALIQUOTA.Query = 0;
            this.campoDecimalALIQUOTA.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalALIQUOTA.Tabela = "TITMMOV";
            this.campoDecimalALIQUOTA.TabIndex = 2;
            // 
            // comboBoxPRODUTOCOMPOSTO
            // 
            this.comboBoxPRODUTOCOMPOSTO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPRODUTOCOMPOSTO.Enabled = false;
            this.comboBoxPRODUTOCOMPOSTO.FormattingEnabled = true;
            this.comboBoxPRODUTOCOMPOSTO.Items.AddRange(new object[] {
            "0 - Não",
            "1 - Sim"});
            this.comboBoxPRODUTOCOMPOSTO.Location = new System.Drawing.Point(23, 122);
            this.comboBoxPRODUTOCOMPOSTO.Name = "comboBoxPRODUTOCOMPOSTO";
            this.comboBoxPRODUTOCOMPOSTO.Size = new System.Drawing.Size(99, 21);
            this.comboBoxPRODUTOCOMPOSTO.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Produto composto";
            // 
            // campoTextoCODUND
            // 
            this.campoTextoCODUND.Campo = "CODUND";
            this.campoTextoCODUND.Default = null;
            this.campoTextoCODUND.Edita = true;
            this.campoTextoCODUND.Enabled = false;
            this.campoTextoCODUND.Location = new System.Drawing.Point(22, 81);
            this.campoTextoCODUND.MaximoCaracteres = 5;
            this.campoTextoCODUND.Name = "campoTextoCODUND";
            this.campoTextoCODUND.Query = 0;
            this.campoTextoCODUND.Size = new System.Drawing.Size(100, 20);
            this.campoTextoCODUND.Tabela = "TITMMOV";
            this.campoTextoCODUND.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Unidade Medida";
            // 
            // campoDecimalPRECOUNITARIO
            // 
            this.campoDecimalPRECOUNITARIO.Campo = "PRECOUNITARIO";
            this.campoDecimalPRECOUNITARIO.Decimais = 2;
            this.campoDecimalPRECOUNITARIO.Default = null;
            this.campoDecimalPRECOUNITARIO.Edita = true;
            this.campoDecimalPRECOUNITARIO.Location = new System.Drawing.Point(340, 81);
            this.campoDecimalPRECOUNITARIO.Name = "campoDecimalPRECOUNITARIO";
            this.campoDecimalPRECOUNITARIO.Query = 0;
            this.campoDecimalPRECOUNITARIO.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalPRECOUNITARIO.Tabela = "TITMMOV";
            this.campoDecimalPRECOUNITARIO.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(337, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Preço Unitário";
            // 
            // campoDecimalQUANTIDADE
            // 
            this.campoDecimalQUANTIDADE.Campo = "QUANTIDADE";
            this.campoDecimalQUANTIDADE.Decimais = 2;
            this.campoDecimalQUANTIDADE.Default = null;
            this.campoDecimalQUANTIDADE.Edita = true;
            this.campoDecimalQUANTIDADE.Location = new System.Drawing.Point(234, 81);
            this.campoDecimalQUANTIDADE.Name = "campoDecimalQUANTIDADE";
            this.campoDecimalQUANTIDADE.Query = 0;
            this.campoDecimalQUANTIDADE.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalQUANTIDADE.Tabela = "TITMMOV";
            this.campoDecimalQUANTIDADE.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(231, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Quantidade";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Produto";
            // 
            // campoLookupPRODUTO
            // 
            this.campoLookupPRODUTO.AutoSize = true;
            this.campoLookupPRODUTO.Campo = "IDPRD";
            this.campoLookupPRODUTO.ColunaCodigo = "CODIGOPRD";
            this.campoLookupPRODUTO.ColunaDescricao = "NOMEFANTASIA";
            this.campoLookupPRODUTO.ColunaIdentificador = null;
            this.campoLookupPRODUTO.ColunaTabela = "ZVWTPRD";
            this.campoLookupPRODUTO.Conexao = "Start";
            this.campoLookupPRODUTO.Default = null;
            this.campoLookupPRODUTO.Edita = true;
            this.campoLookupPRODUTO.Location = new System.Drawing.Point(22, 39);
            this.campoLookupPRODUTO.MaximoCaracteres = null;
            this.campoLookupPRODUTO.Name = "campoLookupPRODUTO";
            this.campoLookupPRODUTO.NomeGrid = null;
            this.campoLookupPRODUTO.Query = 0;
            this.campoLookupPRODUTO.Size = new System.Drawing.Size(577, 24);
            this.campoLookupPRODUTO.Tabela = "TITMMOV";
            this.campoLookupPRODUTO.TabIndex = 0;
            this.campoLookupPRODUTO.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupPRODUTO_SetFormConsulta);
            this.campoLookupPRODUTO.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupPRODUTO_SetDescricao);
            this.campoLookupPRODUTO.AposSelecao += new AppLib.Windows.CampoLookup.AposSelecaoHandler(this.campoLookupPRODUTO_AposSelecao);
            // 
            // FormRequisicaoPecasItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 448);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormRequisicaoPecasItem";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM TITMMOV",
        "LEFT JOIN TTRBMOV ON TITMMOV.CODCOLIGADA = TTRBMOV.CODCOLIGADA ",
        "  AND TITMMOV.IDMOV = TTRBMOV.IDMOV",
        "  AND TITMMOV.NSEQITMMOV = TTRBMOV.NSEQITMMOV",
        "  AND TTRBMOV.CODTRB = \'IPI\',",
        "  TITMMOVCOMPL",
        "",
        "WHERE TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA",
        "  AND TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV",
        "  AND TITMMOV.CODCOLIGADA = 0",
        "  AND TITMMOV.IDMOV = 0"};
            query1.Parametros = new string[0];
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "TITMMOV";
            this.Text = "Cadastro de Item da Requisição de Peças";
            this.SalvarObject += new AppLib.Windows.FormCadastroObject.Salvar2Handler(this.FormOrcamentoItem_Salvar2);
            this.ExcluirObject += new AppLib.Windows.FormCadastroObject.Excluir2Handler(this.FormOrcamentoItem_Excluir2);
            this.ValidarSalvar += new AppLib.Windows.FormCadastroObject.ValidarSalvarHandler(this.FormOrcamentoItem_Validar);
            this.AntesSalvar += new AppLib.Windows.FormCadastroObject.PrepararHandler(this.FormOrcamentoItem_Preparar);
            this.Load += new System.EventHandler(this.FormOrcamentoItem_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoLookup campoLookupPRODUTO;
        private AppLib.Windows.CampoTexto campoTextoCODUND;
        private System.Windows.Forms.Label label4;
        private AppLib.Windows.CampoDecimal campoDecimalPRECOUNITARIO;
        private System.Windows.Forms.Label label3;
        private AppLib.Windows.CampoDecimal campoDecimalQUANTIDADE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxPRODUTOCOMPOSTO;
        private System.Windows.Forms.Label label8;
        private AppLib.Windows.CampoDecimal campoDecimalALIQUOTA;
        private AppLib.Windows.CampoMemo campoMemoHISTORICOLONGO;
        private System.Windows.Forms.Label label30;
        private AppLib.Windows.CampoTexto campoTextoPROJETO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxMATERIALINTERLIGACAO;
        private System.Windows.Forms.Label label7;
    }
}