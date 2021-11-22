namespace AppLib.Windows
{
    partial class CampoHora
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
            this.timeEdit1 = new DevExpress.XtraEditors.TimeEdit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // timeEdit1
            // 
            this.timeEdit1.EditValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.timeEdit1.Location = new System.Drawing.Point(0, 0);
            this.timeEdit1.Name = "timeEdit1";
            this.timeEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit1.Properties.Mask.EditMask = "t";
            this.timeEdit1.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            this.timeEdit1.Size = new System.Drawing.Size(100, 20);
            this.timeEdit1.TabIndex = 0;
            // 
            // CampoHora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.timeEdit1);
            this.Name = "CampoHora";
            this.Size = new System.Drawing.Size(100, 20);
            this.Load += new System.EventHandler(this.CampoHora_Load);
            this.Resize += new System.EventHandler(this.CampoHora_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.TimeEdit timeEdit1;


    }
}
