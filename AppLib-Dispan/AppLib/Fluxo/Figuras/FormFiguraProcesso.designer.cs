namespace AppLib.Fluxo
{
    partial class FormFiguraProcesso
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
            this.buttonEDITOR = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxEXPRESSAO = new System.Windows.Forms.TextBox();
            this.buttonLIMPARDESTINO = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDESTINO = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTEXTO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNOME = new System.Windows.Forms.TextBox();
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
            this.panel1.Location = new System.Drawing.Point(0, 316);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 46);
            this.panel1.TabIndex = 7;
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
            this.tabControl1.Size = new System.Drawing.Size(484, 316);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonEDITOR);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBoxEXPRESSAO);
            this.tabPage1.Controls.Add(this.buttonLIMPARDESTINO);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.comboBoxDESTINO);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textBoxTEXTO);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBoxNOME);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(476, 290);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Geral";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonEDITOR
            // 
            this.buttonEDITOR.Location = new System.Drawing.Point(89, 224);
            this.buttonEDITOR.Name = "buttonEDITOR";
            this.buttonEDITOR.Size = new System.Drawing.Size(75, 23);
            this.buttonEDITOR.TabIndex = 11;
            this.buttonEDITOR.Text = "Editor";
            this.buttonEDITOR.UseVisualStyleBackColor = true;
            this.buttonEDITOR.Click += new System.EventHandler(this.buttonEDITOR_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Expressão:";
            // 
            // textBoxEXPRESSAO
            // 
            this.textBoxEXPRESSAO.Location = new System.Drawing.Point(89, 146);
            this.textBoxEXPRESSAO.Multiline = true;
            this.textBoxEXPRESSAO.Name = "textBoxEXPRESSAO";
            this.textBoxEXPRESSAO.ReadOnly = true;
            this.textBoxEXPRESSAO.Size = new System.Drawing.Size(312, 72);
            this.textBoxEXPRESSAO.TabIndex = 9;
            // 
            // buttonLIMPARDESTINO
            // 
            this.buttonLIMPARDESTINO.Location = new System.Drawing.Point(216, 117);
            this.buttonLIMPARDESTINO.Name = "buttonLIMPARDESTINO";
            this.buttonLIMPARDESTINO.Size = new System.Drawing.Size(23, 23);
            this.buttonLIMPARDESTINO.TabIndex = 8;
            this.buttonLIMPARDESTINO.Text = "X";
            this.buttonLIMPARDESTINO.UseVisualStyleBackColor = true;
            this.buttonLIMPARDESTINO.Click += new System.EventHandler(this.buttonLIMPARDESTINO_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Destino:";
            // 
            // comboBoxDESTINO
            // 
            this.comboBoxDESTINO.FormattingEnabled = true;
            this.comboBoxDESTINO.Location = new System.Drawing.Point(89, 119);
            this.comboBoxDESTINO.Name = "comboBoxDESTINO";
            this.comboBoxDESTINO.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDESTINO.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Texto:";
            // 
            // textBoxTEXTO
            // 
            this.textBoxTEXTO.Location = new System.Drawing.Point(89, 41);
            this.textBoxTEXTO.Multiline = true;
            this.textBoxTEXTO.Name = "textBoxTEXTO";
            this.textBoxTEXTO.Size = new System.Drawing.Size(312, 72);
            this.textBoxTEXTO.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome:";
            // 
            // textBoxNOME
            // 
            this.textBoxNOME.Location = new System.Drawing.Point(89, 15);
            this.textBoxNOME.Name = "textBoxNOME";
            this.textBoxNOME.ReadOnly = true;
            this.textBoxNOME.Size = new System.Drawing.Size(100, 20);
            this.textBoxNOME.TabIndex = 0;
            // 
            // FormFiguraProcesso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 362);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormFiguraProcesso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Propriedades Figua Processo";
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDESTINO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTEXTO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNOME;
        private System.Windows.Forms.Button buttonLIMPARDESTINO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxEXPRESSAO;
        private System.Windows.Forms.Button buttonEDITOR;
    }
}