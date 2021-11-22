namespace AppLib.Windows
{
    partial class FormFiltro
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
            AppLib.Windows.GridProps gridProps1 = new AppLib.Windows.GridProps();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFiltro));
            this.grid1 = new AppLib.Windows.GridData();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.AutoAjuste = true;
            this.grid1.BotaoEditar = true;
            this.grid1.BotaoExcluir = true;
            this.grid1.BotaoNovo = true;
            this.grid1.Conexao = "Start";
            this.grid1.Consulta = new string[] {
        "SELECT NOME /*,",
        "",
        "CASE WHEN ( USUARIO IS NULL ) THEN \'Sim\' ELSE \'Não\' END COMPARTILHADO,",
        "CASE WHEN ( NATIVO = 1 ) THEN \'Sim\' ELSE \'Não\' END NATIVO*/",
        "",
        "FROM ZFILTRO",
        "",
        "WHERE GRID = ?"};
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridProps1.Agrupar = true;
            gridProps1.Alinhamento = AppLib.Windows.Alinhamento.Esquerda;
            gridProps1.Coluna = "COMPARTILHADO";
            gridProps1.Formato = AppLib.Windows.Formato.Nenhum;
            gridProps1.Largura = 50;
            gridProps1.Visivel = true;
            this.grid1.Formatacao = new AppLib.Windows.GridProps[] {
        gridProps1};
            this.grid1.FormPai = null;
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.ModoFiltro = false;
            this.grid1.Name = "grid1";
            this.grid1.NomeGrid = "FormFiltro";
            this.grid1.SelecaoCascata = true;
            this.grid1.Selecionou = false;
            this.grid1.Size = new System.Drawing.Size(457, 412);
            this.grid1.TabIndex = 2;
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            this.grid1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.grid1_SetParametros);
            this.grid1.Novo += new AppLib.Windows.GridData.NovoHandler(this.grid1_Novo);
            this.grid1.Editar += new AppLib.Windows.GridData.EditarHandler(this.grid1_Editar);
            this.grid1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.grid1_Excluir);
            // 
            // FormFiltro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 412);
            this.Controls.Add(this.grid1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFiltro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormFiltro_FormClosed);
            this.Load += new System.EventHandler(this.FormFiltro_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public GridData grid1;


    }
}