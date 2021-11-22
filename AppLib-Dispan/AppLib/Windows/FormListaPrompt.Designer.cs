namespace AppLib.Windows
{
    partial class FormListaPrompt
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
            this.buttonCANCELAR = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(356, 98);
            this.buttonCANCELAR.Name = "buttonCANCELAR";
            this.buttonCANCELAR.Size = new System.Drawing.Size(75, 23);
            this.buttonCANCELAR.TabIndex = 7;
            this.buttonCANCELAR.Text = "Cancelar";
            this.buttonCANCELAR.UseVisualStyleBackColor = true;
            this.buttonCANCELAR.Click += new System.EventHandler(this.buttonCANCELAR_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(275, 98);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(12, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(419, 33);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "Valor do campo";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 62);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(419, 21);
            this.comboBox1.TabIndex = 9;
            // 
            // FormListaPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 164);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonCANCELAR);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormListaPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selecione";
            this.Load += new System.EventHandler(this.FormListaPrompt_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCANCELAR;
        private System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.RichTextBox richTextBox1;
        public System.Windows.Forms.ComboBox comboBox1;

    }
}