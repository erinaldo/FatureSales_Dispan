namespace AppLib.Windows
{
    partial class FormVisao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVisao));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grid1 = new AppLib.Windows.GridData();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
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
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1224, 504);
            this.splitContainer1.SplitterDistance = 323;
            this.splitContainer1.TabIndex = 5;
            // 
            // grid1
            // 
            this.grid1.AutoAjuste = true;
            this.grid1.BotaoEditar = false;
            this.grid1.BotaoExcluir = false;
            this.grid1.BotaoNovo = false;
            this.grid1.Conexao = "Start";
            this.grid1.Consulta = null;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.Formatacao = null;
            this.grid1.FormPai = null;
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.ModoFiltro = false;
            this.grid1.Name = "grid1";
            this.grid1.NomeGrid = null;
            this.grid1.SelecaoCascata = true;
            this.grid1.Selecionou = false;
            this.grid1.Size = new System.Drawing.Size(1224, 504);
            this.grid1.TabIndex = 5;
            this.grid1.TipoAtualizacao = AppLib.Global.Types.TipoAtualizacao.Expandir;
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            // 
            // FormVisao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 504);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormVisao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormVisao_FormClosed);
            this.Load += new System.EventHandler(this.FormConsulta_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public GridData grid1;
        public System.Windows.Forms.SplitContainer splitContainer1;


    }
}