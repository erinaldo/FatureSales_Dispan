
namespace AppFatureClient.New.Forms.Process
{
    partial class frmExportarColunas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportarColunas));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gcColunasItens = new DevExpress.XtraGrid.GridControl();
            this.gvColunasItens = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCopiar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcColunasItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvColunasItens)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.gcColunasItens);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCopiar);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer1.Size = new System.Drawing.Size(720, 386);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 0;
            // 
            // gcColunasItens
            // 
            this.gcColunasItens.Location = new System.Drawing.Point(0, 0);
            this.gcColunasItens.MainView = this.gvColunasItens;
            this.gcColunasItens.Name = "gcColunasItens";
            this.gcColunasItens.Size = new System.Drawing.Size(720, 350);
            this.gcColunasItens.TabIndex = 17;
            this.gcColunasItens.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvColunasItens});
            // 
            // gvColunasItens
            // 
            this.gvColunasItens.GridControl = this.gcColunasItens;
            this.gvColunasItens.Name = "gvColunasItens";
            this.gvColunasItens.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvColunasItens.OptionsCustomization.AllowColumnMoving = false;
            this.gvColunasItens.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.gvColunasItens.OptionsFind.FindNullPrompt = "Pesquisar...";
            this.gvColunasItens.OptionsFind.ShowFindButton = false;
            this.gvColunasItens.OptionsSelection.MultiSelect = true;
            this.gvColunasItens.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gvColunasItens.OptionsView.ColumnAutoWidth = false;
            this.gvColunasItens.OptionsView.RowAutoHeight = true;
            this.gvColunasItens.OptionsView.ShowAutoFilterRow = true;
            this.gvColunasItens.OptionsView.ShowGroupPanel = false;
            // 
            // btnCopiar
            // 
            this.btnCopiar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCopiar.ImageOptions.Image")));
            this.btnCopiar.Location = new System.Drawing.Point(552, 3);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(75, 23);
            this.btnCopiar.TabIndex = 4;
            this.btnCopiar.Text = "Copiar";
            this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.ImageOptions.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(633, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmExportarColunas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 386);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmExportarColunas";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportação de Colunas";
            this.Load += new System.EventHandler(this.frmExportarColunas_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcColunasItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvColunasItens)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnCopiar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraGrid.GridControl gcColunasItens;
        private DevExpress.XtraGrid.Views.Grid.GridView gvColunasItens;
    }
}