namespace AppLib.Padrao
{
    partial class FormMudarSenha
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
            this.textBoxSenhaAtual = new System.Windows.Forms.TextBox();
            this.textBoxNovaSenha = new System.Windows.Forms.TextBox();
            this.textBoxConfirmeNovaSenha = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxSenhaAtual
            // 
            this.textBoxSenhaAtual.Location = new System.Drawing.Point(22, 36);
            this.textBoxSenhaAtual.Name = "textBoxSenhaAtual";
            this.textBoxSenhaAtual.PasswordChar = '*';
            this.textBoxSenhaAtual.Size = new System.Drawing.Size(156, 20);
            this.textBoxSenhaAtual.TabIndex = 0;
            this.textBoxSenhaAtual.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSenhaAtual_KeyDown);
            // 
            // textBoxNovaSenha
            // 
            this.textBoxNovaSenha.Location = new System.Drawing.Point(22, 86);
            this.textBoxNovaSenha.Name = "textBoxNovaSenha";
            this.textBoxNovaSenha.PasswordChar = '*';
            this.textBoxNovaSenha.Size = new System.Drawing.Size(156, 20);
            this.textBoxNovaSenha.TabIndex = 1;
            this.textBoxNovaSenha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxNovaSenha_KeyDown);
            // 
            // textBoxConfirmeNovaSenha
            // 
            this.textBoxConfirmeNovaSenha.Location = new System.Drawing.Point(22, 137);
            this.textBoxConfirmeNovaSenha.Name = "textBoxConfirmeNovaSenha";
            this.textBoxConfirmeNovaSenha.PasswordChar = '*';
            this.textBoxConfirmeNovaSenha.Size = new System.Drawing.Size(156, 20);
            this.textBoxConfirmeNovaSenha.TabIndex = 2;
            this.textBoxConfirmeNovaSenha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxConfirmeNovaSenha_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Senha Atual";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nova Senha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Confirme a Nova Senha";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(22, 176);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Location = new System.Drawing.Point(103, 176);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelar.TabIndex = 7;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // FormMudarSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 224);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxConfirmeNovaSenha);
            this.Controls.Add(this.textBoxNovaSenha);
            this.Controls.Add(this.textBoxSenhaAtual);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormMudarSenha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mudar a Senha";
            this.Load += new System.EventHandler(this.FormMudarSenha_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSenhaAtual;
        private System.Windows.Forms.TextBox textBoxNovaSenha;
        private System.Windows.Forms.TextBox textBoxConfirmeNovaSenha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancelar;
    }
}