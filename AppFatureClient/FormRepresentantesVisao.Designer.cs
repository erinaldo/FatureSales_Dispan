namespace AppFatureClient
{
    partial class FormRepresentantesVisao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRepresentantesVisao));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.BotaoEditar = true;
            this.grid1.BotaoExcluir = true;
            this.grid1.BotaoNovo = true;
            this.grid1.Consulta = new string[] {
        "SELECT * FROM TRPR WHERE CODCOLIGADA = ?"};
            this.grid1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grid1.NomeGrid = "TRPR";
            this.grid1.Size = new System.Drawing.Size(553, 345);
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Selecionar;
            this.grid1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.grid1_SetParametros_1);
            this.grid1.Novo += new AppLib.Windows.GridData.NovoHandler(this.grid1_Novo_1);
            this.grid1.Editar += new AppLib.Windows.GridData.EditarHandler(this.grid1_Editar_1);
            this.grid1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.grid1_Excluir_1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Size = new System.Drawing.Size(553, 345);
            this.splitContainer1.SplitterWidth = 3;
            // 
            // FormRepresentantesVisao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 345);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormRepresentantesVisao";
            this.Text = "Consulta de Representantes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRepresentantesVisao_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}