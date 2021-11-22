namespace AppLib.Windows
{
    partial class FormBusca
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBusca));
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButtonLIMPAR = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonBUSCAR = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelLookup = new System.Windows.Forms.Panel();
            this.simpleButtonCANCELAR = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonSELECIONAR = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.panelLookup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButtonLIMPAR);
            this.panel1.Controls.Add(this.simpleButtonBUSCAR);
            this.panel1.Controls.Add(this.textEdit1);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 54);
            this.panel1.TabIndex = 0;
            // 
            // simpleButtonLIMPAR
            // 
            this.simpleButtonLIMPAR.Location = new System.Drawing.Point(478, 15);
            this.simpleButtonLIMPAR.Name = "simpleButtonLIMPAR";
            this.simpleButtonLIMPAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonLIMPAR.TabIndex = 2;
            this.simpleButtonLIMPAR.Text = "Limpar";
            this.simpleButtonLIMPAR.Click += new System.EventHandler(this.simpleButtonLIMPAR_Click);
            // 
            // simpleButtonBUSCAR
            // 
            this.simpleButtonBUSCAR.Location = new System.Drawing.Point(397, 15);
            this.simpleButtonBUSCAR.Name = "simpleButtonBUSCAR";
            this.simpleButtonBUSCAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonBUSCAR.TabIndex = 1;
            this.simpleButtonBUSCAR.Text = "Filtrar";
            this.simpleButtonBUSCAR.Click += new System.EventHandler(this.simpleButtonFILTRAR_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(77, 17);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(314, 20);
            this.textEdit1.TabIndex = 0;
            this.textEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEdit1_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(55, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Buscar por:";
            // 
            // panelLookup
            // 
            this.panelLookup.Controls.Add(this.simpleButtonCANCELAR);
            this.panelLookup.Controls.Add(this.simpleButtonSELECIONAR);
            this.panelLookup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLookup.Location = new System.Drawing.Point(0, 357);
            this.panelLookup.Name = "panelLookup";
            this.panelLookup.Size = new System.Drawing.Size(584, 54);
            this.panelLookup.TabIndex = 9;
            // 
            // simpleButtonCANCELAR
            // 
            this.simpleButtonCANCELAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR.Location = new System.Drawing.Point(487, 16);
            this.simpleButtonCANCELAR.Name = "simpleButtonCANCELAR";
            this.simpleButtonCANCELAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR.TabIndex = 1;
            this.simpleButtonCANCELAR.Text = "Cancelar";
            this.simpleButtonCANCELAR.Click += new System.EventHandler(this.simpleButtonCANCELAR_Click);
            // 
            // simpleButtonSELECIONAR
            // 
            this.simpleButtonSELECIONAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonSELECIONAR.Location = new System.Drawing.Point(397, 16);
            this.simpleButtonSELECIONAR.Name = "simpleButtonSELECIONAR";
            this.simpleButtonSELECIONAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonSELECIONAR.TabIndex = 0;
            this.simpleButtonSELECIONAR.Text = "Selecionar";
            this.simpleButtonSELECIONAR.Click += new System.EventHandler(this.simpleButtonSELECIONAR_Click_1);
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 54);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(584, 303);
            this.gridControl1.TabIndex = 10;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.DoubleClick += new System.EventHandler(this.gridControl1_DoubleClick);
            this.gridControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridControl1_KeyDown);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // FormBusca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelLookup);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FormBusca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleção de Registro";
            this.Load += new System.EventHandler(this.FormLookup2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.panelLookup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonBUSCAR;
        public System.Windows.Forms.Panel panelLookup;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCANCELAR;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSELECIONAR;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonLIMPAR;
        public DevExpress.XtraEditors.TextEdit textEdit1;
    }
}