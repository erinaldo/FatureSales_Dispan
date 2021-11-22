namespace AppFatureClient
{
    partial class FormRequisicaoPecasItemDevolucao
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
            this.txtObservacaoDevolucao = new AppLib.Windows.CampoMemo();
            this.label3 = new System.Windows.Forms.Label();
            this.txtObservacaoIda = new AppLib.Windows.CampoMemo();
            this.label30 = new System.Windows.Forms.Label();
            this.campoTextoCODUND = new AppLib.Windows.CampoTexto();
            this.label4 = new System.Windows.Forms.Label();
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
            this.tabPage1.Controls.Add(this.txtObservacaoDevolucao);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtObservacaoIda);
            this.tabPage1.Controls.Add(this.label30);
            this.tabPage1.Controls.Add(this.campoTextoCODUND);
            this.tabPage1.Controls.Add(this.label4);
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
            // txtObservacaoDevolucao
            // 
            this.txtObservacaoDevolucao.Campo = "HISTORICOLONGO";
            this.txtObservacaoDevolucao.Default = "";
            this.txtObservacaoDevolucao.Edita = true;
            this.txtObservacaoDevolucao.Location = new System.Drawing.Point(327, 134);
            this.txtObservacaoDevolucao.MaximoCaracteres = null;
            this.txtObservacaoDevolucao.Name = "txtObservacaoDevolucao";
            this.txtObservacaoDevolucao.Query = 0;
            this.txtObservacaoDevolucao.Size = new System.Drawing.Size(272, 157);
            this.txtObservacaoDevolucao.Tabela = null;
            this.txtObservacaoDevolucao.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Observação devolução";
            // 
            // txtObservacaoIda
            // 
            this.txtObservacaoIda.Campo = "HISTORICOLONGO";
            this.txtObservacaoIda.Default = "";
            this.txtObservacaoIda.Edita = true;
            this.txtObservacaoIda.Location = new System.Drawing.Point(22, 134);
            this.txtObservacaoIda.MaximoCaracteres = null;
            this.txtObservacaoIda.Name = "txtObservacaoIda";
            this.txtObservacaoIda.Query = 0;
            this.txtObservacaoIda.Size = new System.Drawing.Size(272, 157);
            this.txtObservacaoIda.Tabela = null;
            this.txtObservacaoIda.TabIndex = 8;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(19, 118);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(65, 13);
            this.label30.TabIndex = 40;
            this.label30.Text = "Observação";
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
            // campoDecimalQUANTIDADE
            // 
            this.campoDecimalQUANTIDADE.Campo = "QUANTIDADE";
            this.campoDecimalQUANTIDADE.Decimais = 2;
            this.campoDecimalQUANTIDADE.Default = null;
            this.campoDecimalQUANTIDADE.Edita = true;
            this.campoDecimalQUANTIDADE.Location = new System.Drawing.Point(139, 81);
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
            this.label2.Location = new System.Drawing.Point(136, 65);
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
            this.campoLookupPRODUTO.EditaLookup = false;
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
            // FormRequisicaoPecasItemDevolucao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 448);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormRequisicaoPecasItemDevolucao";
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
        private AppLib.Windows.CampoDecimal campoDecimalQUANTIDADE;
        private System.Windows.Forms.Label label2;
        private AppLib.Windows.CampoMemo txtObservacaoIda;
        private System.Windows.Forms.Label label30;
        private AppLib.Windows.CampoMemo txtObservacaoDevolucao;
        private System.Windows.Forms.Label label3;
    }
}