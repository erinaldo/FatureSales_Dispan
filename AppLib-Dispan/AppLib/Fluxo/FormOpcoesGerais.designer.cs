namespace AppLib.Fluxo
{
    partial class FormOpcoesGerais
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
            this.buttonFECHAR = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonEXCLUIR = new System.Windows.Forms.Button();
            this.textBoxBIBLIOTECAEXT = new System.Windows.Forms.TextBox();
            this.buttonINSERIR = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonFECHAR);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 371);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 46);
            this.panel1.TabIndex = 7;
            // 
            // buttonFECHAR
            // 
            this.buttonFECHAR.Location = new System.Drawing.Point(332, 11);
            this.buttonFECHAR.Name = "buttonFECHAR";
            this.buttonFECHAR.Size = new System.Drawing.Size(75, 23);
            this.buttonFECHAR.TabIndex = 2;
            this.buttonFECHAR.Text = "Fechar";
            this.buttonFECHAR.UseVisualStyleBackColor = true;
            this.buttonFECHAR.Click += new System.EventHandler(this.buttonFECHAR_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(440, 371);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(432, 345);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Biblioteca Externa";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 48);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(426, 294);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.buttonEXCLUIR);
            this.panel2.Controls.Add(this.textBoxBIBLIOTECAEXT);
            this.panel2.Controls.Add(this.buttonINSERIR);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(426, 45);
            this.panel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Biblioteca:";
            // 
            // buttonEXCLUIR
            // 
            this.buttonEXCLUIR.Location = new System.Drawing.Point(373, 10);
            this.buttonEXCLUIR.Name = "buttonEXCLUIR";
            this.buttonEXCLUIR.Size = new System.Drawing.Size(27, 23);
            this.buttonEXCLUIR.TabIndex = 4;
            this.buttonEXCLUIR.Text = "-";
            this.buttonEXCLUIR.UseVisualStyleBackColor = true;
            this.buttonEXCLUIR.Click += new System.EventHandler(this.buttonEXCLUIR_Click);
            // 
            // textBoxBIBLIOTECAEXT
            // 
            this.textBoxBIBLIOTECAEXT.Location = new System.Drawing.Point(78, 12);
            this.textBoxBIBLIOTECAEXT.Name = "textBoxBIBLIOTECAEXT";
            this.textBoxBIBLIOTECAEXT.Size = new System.Drawing.Size(256, 20);
            this.textBoxBIBLIOTECAEXT.TabIndex = 2;
            // 
            // buttonINSERIR
            // 
            this.buttonINSERIR.Location = new System.Drawing.Point(340, 10);
            this.buttonINSERIR.Name = "buttonINSERIR";
            this.buttonINSERIR.Size = new System.Drawing.Size(27, 23);
            this.buttonINSERIR.TabIndex = 3;
            this.buttonINSERIR.Text = "+";
            this.buttonINSERIR.UseVisualStyleBackColor = true;
            this.buttonINSERIR.Click += new System.EventHandler(this.buttonINSERIR_Click);
            // 
            // FormOpcoesGerais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 417);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormOpcoesGerais";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opções Gerais";
            this.Load += new System.EventHandler(this.FormOpcoesGerais_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonFECHAR;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonEXCLUIR;
        private System.Windows.Forms.Button buttonINSERIR;
        private System.Windows.Forms.TextBox textBoxBIBLIOTECAEXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}