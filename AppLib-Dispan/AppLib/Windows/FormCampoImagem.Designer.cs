namespace AppLib.Windows
{
    partial class FormCampoImagem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCampoImagem));
            this.simpleButtonIMPORTAR = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonEXPORTAR = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonAPAGAR = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCANCELAR = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // simpleButtonIMPORTAR
            // 
            this.simpleButtonIMPORTAR.Location = new System.Drawing.Point(12, 12);
            this.simpleButtonIMPORTAR.Name = "simpleButtonIMPORTAR";
            this.simpleButtonIMPORTAR.Size = new System.Drawing.Size(139, 48);
            this.simpleButtonIMPORTAR.TabIndex = 0;
            this.simpleButtonIMPORTAR.Text = "Importar";
            this.simpleButtonIMPORTAR.Click += new System.EventHandler(this.simpleButtonIMPORTAR_Click);
            // 
            // simpleButtonEXPORTAR
            // 
            this.simpleButtonEXPORTAR.Location = new System.Drawing.Point(12, 66);
            this.simpleButtonEXPORTAR.Name = "simpleButtonEXPORTAR";
            this.simpleButtonEXPORTAR.Size = new System.Drawing.Size(139, 48);
            this.simpleButtonEXPORTAR.TabIndex = 1;
            this.simpleButtonEXPORTAR.Text = "Exportar";
            this.simpleButtonEXPORTAR.Click += new System.EventHandler(this.simpleButtonEXPORTAR_Click);
            // 
            // simpleButtonAPAGAR
            // 
            this.simpleButtonAPAGAR.Location = new System.Drawing.Point(12, 120);
            this.simpleButtonAPAGAR.Name = "simpleButtonAPAGAR";
            this.simpleButtonAPAGAR.Size = new System.Drawing.Size(139, 48);
            this.simpleButtonAPAGAR.TabIndex = 2;
            this.simpleButtonAPAGAR.Text = "Apagar";
            this.simpleButtonAPAGAR.Click += new System.EventHandler(this.simpleButtonAPAGAR_Click);
            // 
            // simpleButtonCANCELAR
            // 
            this.simpleButtonCANCELAR.Location = new System.Drawing.Point(12, 174);
            this.simpleButtonCANCELAR.Name = "simpleButtonCANCELAR";
            this.simpleButtonCANCELAR.Size = new System.Drawing.Size(139, 48);
            this.simpleButtonCANCELAR.TabIndex = 3;
            this.simpleButtonCANCELAR.Text = "Cancelar";
            this.simpleButtonCANCELAR.Click += new System.EventHandler(this.simpleButtonCANCELAR_Click);
            // 
            // FormCampoImagem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(163, 233);
            this.Controls.Add(this.simpleButtonCANCELAR);
            this.Controls.Add(this.simpleButtonAPAGAR);
            this.Controls.Add(this.simpleButtonEXPORTAR);
            this.Controls.Add(this.simpleButtonIMPORTAR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCampoImagem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opções da imagem";
            this.Load += new System.EventHandler(this.FormCampoImagem_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButtonIMPORTAR;
        private DevExpress.XtraEditors.SimpleButton simpleButtonEXPORTAR;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAPAGAR;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCANCELAR;
    }
}