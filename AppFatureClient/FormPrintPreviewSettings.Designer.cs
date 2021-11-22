namespace AppFatureClient
{
    partial class FormPrintPreviewSettings
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.rbTudo = new System.Windows.Forms.RadioButton();
            this.rbAtual = new System.Windows.Forms.RadioButton();
            this.rbSelecao = new System.Windows.Forms.RadioButton();
            this.rbPaginas = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(194, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "&Imprimir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(275, 202);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "&Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Impressora";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(128, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(141, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(275, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "&Preferencias";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cópias";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(128, 59);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(141, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Intervalo de Páginas";
            this.label3.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(275, 61);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(63, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Agrupar";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // rbTudo
            // 
            this.rbTudo.AutoSize = true;
            this.rbTudo.Location = new System.Drawing.Point(128, 100);
            this.rbTudo.Name = "rbTudo";
            this.rbTudo.Size = new System.Drawing.Size(50, 17);
            this.rbTudo.TabIndex = 9;
            this.rbTudo.TabStop = true;
            this.rbTudo.Text = "Tudo";
            this.rbTudo.UseVisualStyleBackColor = true;
            this.rbTudo.Visible = false;
            // 
            // rbAtual
            // 
            this.rbAtual.AutoSize = true;
            this.rbAtual.Location = new System.Drawing.Point(194, 100);
            this.rbAtual.Name = "rbAtual";
            this.rbAtual.Size = new System.Drawing.Size(49, 17);
            this.rbAtual.TabIndex = 10;
            this.rbAtual.TabStop = true;
            this.rbAtual.Text = "Atual";
            this.rbAtual.UseVisualStyleBackColor = true;
            this.rbAtual.Visible = false;
            // 
            // rbSelecao
            // 
            this.rbSelecao.AutoSize = true;
            this.rbSelecao.Location = new System.Drawing.Point(261, 100);
            this.rbSelecao.Name = "rbSelecao";
            this.rbSelecao.Size = new System.Drawing.Size(64, 17);
            this.rbSelecao.TabIndex = 11;
            this.rbSelecao.TabStop = true;
            this.rbSelecao.Text = "Seleção";
            this.rbSelecao.UseVisualStyleBackColor = true;
            this.rbSelecao.Visible = false;
            // 
            // rbPaginas
            // 
            this.rbPaginas.AutoSize = true;
            this.rbPaginas.Location = new System.Drawing.Point(128, 135);
            this.rbPaginas.Name = "rbPaginas";
            this.rbPaginas.Size = new System.Drawing.Size(63, 17);
            this.rbPaginas.TabIndex = 12;
            this.rbPaginas.TabStop = true;
            this.rbPaginas.Text = "Páginas";
            this.rbPaginas.UseVisualStyleBackColor = true;
            this.rbPaginas.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(194, 135);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 20);
            this.textBox1.TabIndex = 13;
            this.textBox1.Visible = false;
            // 
            // FormPrintPreviewSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 251);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.rbPaginas);
            this.Controls.Add(this.rbSelecao);
            this.Controls.Add(this.rbAtual);
            this.Controls.Add(this.rbTudo);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPrintPreviewSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impressão";
            this.Load += new System.EventHandler(this.FormPrintPreviewSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RadioButton rbTudo;
        private System.Windows.Forms.RadioButton rbAtual;
        private System.Windows.Forms.RadioButton rbSelecao;
        private System.Windows.Forms.RadioButton rbPaginas;
        private System.Windows.Forms.TextBox textBox1;
    }
}