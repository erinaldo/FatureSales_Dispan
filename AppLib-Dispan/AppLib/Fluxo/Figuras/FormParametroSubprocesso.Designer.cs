namespace AppLib.Fluxo
{
    partial class FormParametroSubprocesso
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSALVAR = new System.Windows.Forms.Button();
            this.buttonCANCELAR = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonLIMPARRETORNO = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxVALOR = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPARAMETRO = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSALVAR);
            this.panel1.Controls.Add(this.buttonCANCELAR);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 216);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 46);
            this.panel1.TabIndex = 6;
            // 
            // buttonSALVAR
            // 
            this.buttonSALVAR.Location = new System.Drawing.Point(12, 11);
            this.buttonSALVAR.Name = "buttonSALVAR";
            this.buttonSALVAR.Size = new System.Drawing.Size(75, 23);
            this.buttonSALVAR.TabIndex = 2;
            this.buttonSALVAR.Text = "Salvar";
            this.buttonSALVAR.UseVisualStyleBackColor = true;
            this.buttonSALVAR.Click += new System.EventHandler(this.buttonSALVAR_Click);
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(93, 11);
            this.buttonCANCELAR.Name = "buttonCANCELAR";
            this.buttonCANCELAR.Size = new System.Drawing.Size(75, 23);
            this.buttonCANCELAR.TabIndex = 3;
            this.buttonCANCELAR.Text = "Cancelar";
            this.buttonCANCELAR.UseVisualStyleBackColor = true;
            this.buttonCANCELAR.Click += new System.EventHandler(this.buttonCANCELAR_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(284, 216);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonLIMPARRETORNO);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.comboBoxVALOR);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBoxPARAMETRO);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(276, 190);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Geral";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonLIMPARRETORNO
            // 
            this.buttonLIMPARRETORNO.Location = new System.Drawing.Point(216, 41);
            this.buttonLIMPARRETORNO.Name = "buttonLIMPARRETORNO";
            this.buttonLIMPARRETORNO.Size = new System.Drawing.Size(20, 21);
            this.buttonLIMPARRETORNO.TabIndex = 11;
            this.buttonLIMPARRETORNO.Text = "X";
            this.buttonLIMPARRETORNO.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Valor:";
            // 
            // comboBoxVALOR
            // 
            this.comboBoxVALOR.FormattingEnabled = true;
            this.comboBoxVALOR.Location = new System.Drawing.Point(89, 41);
            this.comboBoxVALOR.Name = "comboBoxVALOR";
            this.comboBoxVALOR.Size = new System.Drawing.Size(121, 21);
            this.comboBoxVALOR.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Parâmetro:";
            // 
            // textBoxPARAMETRO
            // 
            this.textBoxPARAMETRO.Location = new System.Drawing.Point(89, 15);
            this.textBoxPARAMETRO.Name = "textBoxPARAMETRO";
            this.textBoxPARAMETRO.ReadOnly = true;
            this.textBoxPARAMETRO.Size = new System.Drawing.Size(100, 20);
            this.textBoxPARAMETRO.TabIndex = 0;
            // 
            // FormParametroSubprocesso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormParametroSubprocesso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parâmetro de subprocesso";
            this.Load += new System.EventHandler(this.FormParametroSubprocesso_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSALVAR;
        private System.Windows.Forms.Button buttonCANCELAR;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonLIMPARRETORNO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxVALOR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPARAMETRO;
    }
}