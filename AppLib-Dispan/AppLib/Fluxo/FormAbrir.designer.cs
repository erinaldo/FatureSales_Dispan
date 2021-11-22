namespace AppLib.Fluxo
{
    partial class FormAbrir
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCANCELAR = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDESCRICAO = new System.Windows.Forms.TextBox();
            this.comboBoxFLUXO = new System.Windows.Forms.ComboBox();
            this.comboBoxCLASSE = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxBIBLIOTECA = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Controls.Add(this.buttonCANCELAR);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 222);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 46);
            this.panel1.TabIndex = 6;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(85, 11);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(166, 11);
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
            this.tabControl1.Size = new System.Drawing.Size(436, 222);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBoxDESCRICAO);
            this.tabPage1.Controls.Add(this.comboBoxFLUXO);
            this.tabPage1.Controls.Add(this.comboBoxCLASSE);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.comboBoxBIBLIOTECA);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(428, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Filtro";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Descrição:";
            // 
            // textBoxDESCRICAO
            // 
            this.textBoxDESCRICAO.Location = new System.Drawing.Point(81, 98);
            this.textBoxDESCRICAO.Multiline = true;
            this.textBoxDESCRICAO.Name = "textBoxDESCRICAO";
            this.textBoxDESCRICAO.ReadOnly = true;
            this.textBoxDESCRICAO.Size = new System.Drawing.Size(312, 72);
            this.textBoxDESCRICAO.TabIndex = 14;
            // 
            // comboBoxFLUXO
            // 
            this.comboBoxFLUXO.FormattingEnabled = true;
            this.comboBoxFLUXO.Location = new System.Drawing.Point(81, 71);
            this.comboBoxFLUXO.Name = "comboBoxFLUXO";
            this.comboBoxFLUXO.Size = new System.Drawing.Size(168, 21);
            this.comboBoxFLUXO.TabIndex = 13;
            this.comboBoxFLUXO.SelectedIndexChanged += new System.EventHandler(this.comboBoxFLUXO_SelectedIndexChanged);
            // 
            // comboBoxCLASSE
            // 
            this.comboBoxCLASSE.FormattingEnabled = true;
            this.comboBoxCLASSE.Location = new System.Drawing.Point(81, 44);
            this.comboBoxCLASSE.Name = "comboBoxCLASSE";
            this.comboBoxCLASSE.Size = new System.Drawing.Size(168, 21);
            this.comboBoxCLASSE.TabIndex = 12;
            this.comboBoxCLASSE.SelectedIndexChanged += new System.EventHandler(this.comboBoxCLASSE_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Fluxo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Classe:";
            // 
            // comboBoxBIBLIOTECA
            // 
            this.comboBoxBIBLIOTECA.FormattingEnabled = true;
            this.comboBoxBIBLIOTECA.Location = new System.Drawing.Point(81, 17);
            this.comboBoxBIBLIOTECA.Name = "comboBoxBIBLIOTECA";
            this.comboBoxBIBLIOTECA.Size = new System.Drawing.Size(168, 21);
            this.comboBoxBIBLIOTECA.TabIndex = 9;
            this.comboBoxBIBLIOTECA.SelectedIndexChanged += new System.EventHandler(this.comboBoxBIBLIOTECA_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Biblioteca:";
            // 
            // FormAbrir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 268);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormAbrir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abrir Fluxo";
            this.Load += new System.EventHandler(this.FormAbrir_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCANCELAR;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxBIBLIOTECA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFLUXO;
        private System.Windows.Forms.ComboBox comboBoxCLASSE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDESCRICAO;
    }
}