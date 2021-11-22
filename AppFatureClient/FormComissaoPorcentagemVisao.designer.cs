using AppLib.Windows;

namespace AppFatureClient
{
    partial class FormComissaoPorcentagemVisao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComissaoPorcentagemVisao));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grid1 = new AppLib.Windows.GridData();
            this.gridData1 = new AppLib.Windows.GridData();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grid1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridData1);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(710, 391);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 5;
            // 
            // grid1
            // 
            this.grid1.AutoAjuste = true;
            this.grid1.BotaoEditar = true;
            this.grid1.BotaoExcluir = true;
            this.grid1.BotaoNovo = true;
            this.grid1.Conexao = "Start";
            this.grid1.Consulta = new string[] {
        "select * from ZPORCENTAGEMCARTA"};
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.Formatacao = null;
            this.grid1.FormPai = null;
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.ModoFiltro = false;
            this.grid1.Name = "grid1";
            this.grid1.NomeGrid = "ZVWPORCENTAGEMCARTA";
            this.grid1.SelecaoCascata = true;
            this.grid1.Selecionou = false;
            this.grid1.Size = new System.Drawing.Size(710, 391);
            this.grid1.TabIndex = 5;
            this.grid1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Selecionar;
            this.grid1.Novo += new AppLib.Windows.GridData.NovoHandler(this.grid1_Novo);
            this.grid1.Editar += new AppLib.Windows.GridData.EditarHandler(this.grid1_Editar);
            this.grid1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.grid1_Excluir);
            this.grid1.AposSelecao += new AppLib.Windows.GridData.AposSelecaoHandler(this.grid1_AposSelecao);
            this.grid1.Load += new System.EventHandler(this.grid1_Load);
            this.grid1.Enter += new System.EventHandler(this.grid1_Enter);
            // 
            // gridData1
            // 
            this.gridData1.AutoAjuste = true;
            this.gridData1.BotaoEditar = false;
            this.gridData1.BotaoExcluir = true;
            this.gridData1.BotaoNovo = false;
            this.gridData1.Conexao = "Start";
            this.gridData1.Consulta = new string[] {
        "select ",
        "           F.CODCOLIGADA,",
        "           F.IDLAN, ",
        "           F.STATUSLAN,",
        "           F.NUMERODOCUMENTO,",
        "           F.VALORORIGINAL,",
        "           F.VALORBAIXADO,",
        "           F.CODTDO,",
        "           F.DATAVENCIMENTO,",
        "           F.DATABAIXA,",
        "           F.CAMPOALFAOP3 as \'PEDIDO\',",
        "\t\t   FCFC1.CODCONTA as \'Conta 1\',",
        "\t       FCFC1.CLASSCONTA as \'Class Conta 1\',",
        "\t\t   FCFC2.CODCONTA as \'Conta 2\',",
        "\t\t   FCFC2.CLASSCONTA as \'Calss Conta 2\'",
        "",
        "from FLAN F",
        "",
        "left join FCFOCONT FCFC1",
        "on FCFC1.CODCFO = F.CODCFO",
        "and FCFC1.CODCONTA like \'1.01.03.%\'",
        "",
        "left join FCFOCONT FCFC2",
        "on FCFC2.CODCFO = F.CODCFO",
        "and FCFC2.CODCONTA like \'2.01.04.%\'",
        "",
        "",
        "where F.CAMPOALFAOP3 = ?",
        "and F.CODTDO = \'ADC\'",
        "and FCFC1.PAGREC = 1",
        "and FCFC2.PAGREC = 1"};
            this.gridData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData1.Formatacao = null;
            this.gridData1.FormPai = null;
            this.gridData1.Location = new System.Drawing.Point(0, 0);
            this.gridData1.ModoFiltro = false;
            this.gridData1.Name = "gridData1";
            this.gridData1.NomeGrid = "";
            this.gridData1.SelecaoCascata = true;
            this.gridData1.Selecionou = false;
            this.gridData1.Size = new System.Drawing.Size(150, 46);
            this.gridData1.TabIndex = 6;
            this.gridData1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.gridData1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.gridData1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.gridData1_SetParametros);
            this.gridData1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.gridData1_Excluir);
            // 
            // FormComissaoPorcentagemVisao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 391);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormComissaoPorcentagemVisao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de Porcentagem";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormVisao_FormClosed);
            this.Load += new System.EventHandler(this.FormConsulta_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.SplitContainer splitContainer1;
        public GridData grid1;
        public GridData gridData1;
    }
}