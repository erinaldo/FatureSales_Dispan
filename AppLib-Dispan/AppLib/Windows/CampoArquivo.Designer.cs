namespace AppLib.Windows
{
    partial class CampoArquivo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textEditIDENTIFICADOR = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.hyperLinkEditDESCRICAO = new DevExpress.XtraEditors.HyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditIDENTIFICADOR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditDESCRICAO.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEditIDENTIFICADOR
            // 
            this.textEditIDENTIFICADOR.Location = new System.Drawing.Point(0, 0);
            this.textEditIDENTIFICADOR.Name = "textEditIDENTIFICADOR";
            this.textEditIDENTIFICADOR.Size = new System.Drawing.Size(100, 20);
            this.textEditIDENTIFICADOR.TabIndex = 0;
            this.textEditIDENTIFICADOR.Leave += new System.EventHandler(this.textEditIDENTIFICADOR_Leave);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(106, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(28, 20);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "...";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // hyperLinkEditDESCRICAO
            // 
            this.hyperLinkEditDESCRICAO.EditValue = "";
            this.hyperLinkEditDESCRICAO.Location = new System.Drawing.Point(140, 0);
            this.hyperLinkEditDESCRICAO.Name = "hyperLinkEditDESCRICAO";
            this.hyperLinkEditDESCRICAO.Size = new System.Drawing.Size(100, 20);
            this.hyperLinkEditDESCRICAO.TabIndex = 2;
            this.hyperLinkEditDESCRICAO.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyperLinkEditDESCRICAO_OpenLink);
            // 
            // CampoArquivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hyperLinkEditDESCRICAO);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.textEditIDENTIFICADOR);
            this.Name = "CampoArquivo";
            this.Size = new System.Drawing.Size(243, 20);
            this.Load += new System.EventHandler(this.CampoArquivo_Load);
            this.Resize += new System.EventHandler(this.CampoArquivo_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.textEditIDENTIFICADOR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditDESCRICAO.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.TextEdit textEditIDENTIFICADOR;
        public DevExpress.XtraEditors.HyperLinkEdit hyperLinkEditDESCRICAO;
    }
}
