namespace AppLib.Windows
{
    partial class CampoLookup
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
            this.textBoxCODIGO = new DevExpress.XtraEditors.TextEdit();
            this.textBoxDESCRICAO = new System.Windows.Forms.TextBox();
            this.buttonSELECIONAR = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCODIGO.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxCODIGO
            // 
            this.textBoxCODIGO.Location = new System.Drawing.Point(0, 0);
            this.textBoxCODIGO.Name = "textBoxCODIGO";
            this.textBoxCODIGO.Size = new System.Drawing.Size(100, 20);
            this.textBoxCODIGO.TabIndex = 0;
            this.textBoxCODIGO.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // textBoxDESCRICAO
            // 
            this.textBoxDESCRICAO.Location = new System.Drawing.Point(140, 0);
            this.textBoxDESCRICAO.Name = "textBoxDESCRICAO";
            this.textBoxDESCRICAO.Size = new System.Drawing.Size(100, 20);
            this.textBoxDESCRICAO.TabIndex = 2;
            this.textBoxDESCRICAO.TabStop = false;
            this.textBoxDESCRICAO.Click += new System.EventHandler(this.textBoxDESCRICAO_Click);
            this.textBoxDESCRICAO.TextChanged += new System.EventHandler(this.textBoxDESCRICAO_TextChanged);
            // 
            // buttonSELECIONAR
            // 
            this.buttonSELECIONAR.Location = new System.Drawing.Point(106, 0);
            this.buttonSELECIONAR.Name = "buttonSELECIONAR";
            this.buttonSELECIONAR.Size = new System.Drawing.Size(28, 20);
            this.buttonSELECIONAR.TabIndex = 1;
            this.buttonSELECIONAR.Text = "...";
            this.buttonSELECIONAR.UseVisualStyleBackColor = true;
            this.buttonSELECIONAR.Click += new System.EventHandler(this.buttonSELECIONAR_Click);
            // 
            // CampoLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSELECIONAR);
            this.Controls.Add(this.textBoxDESCRICAO);
            this.Controls.Add(this.textBoxCODIGO);
            this.Name = "CampoLookup";
            this.Size = new System.Drawing.Size(243, 21);
            this.Load += new System.EventHandler(this.Lookup_Load);
            this.Resize += new System.EventHandler(this.Lookup_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCODIGO.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.TextEdit textBoxCODIGO;
        public System.Windows.Forms.TextBox textBoxDESCRICAO;
        public System.Windows.Forms.Button buttonSELECIONAR;
    }
}
