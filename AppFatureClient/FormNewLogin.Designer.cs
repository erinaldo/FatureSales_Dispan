namespace AppFatureClient
{
    partial class FormNewLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewLogin));
            this.panel_Login = new System.Windows.Forms.Panel();
            this.panel_Alias = new System.Windows.Forms.Panel();
            this.tb_Senha = new DevExpress.XtraEditors.TextEdit();
            this.tb_Usuario = new DevExpress.XtraEditors.TextEdit();
            this.cb_Alias = new System.Windows.Forms.ComboBox();
            this.panel_Sair = new System.Windows.Forms.Panel();
            this.panel_Suporte = new System.Windows.Forms.Panel();
            this.lblVersao = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Senha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Usuario.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Login
            // 
            this.panel_Login.BackColor = System.Drawing.Color.Transparent;
            this.panel_Login.Location = new System.Drawing.Point(281, 229);
            this.panel_Login.Name = "panel_Login";
            this.panel_Login.Size = new System.Drawing.Size(213, 42);
            this.panel_Login.TabIndex = 0;
            this.panel_Login.Click += new System.EventHandler(this.panel_Login_Click);
            // 
            // panel_Alias
            // 
            this.panel_Alias.BackColor = System.Drawing.Color.Transparent;
            this.panel_Alias.Location = new System.Drawing.Point(456, 189);
            this.panel_Alias.Name = "panel_Alias";
            this.panel_Alias.Size = new System.Drawing.Size(38, 24);
            this.panel_Alias.TabIndex = 1;
            // 
            // tb_Senha
            // 
            this.tb_Senha.Location = new System.Drawing.Point(281, 159);
            this.tb_Senha.Name = "tb_Senha";
            this.tb_Senha.Properties.Appearance.BackColor = System.Drawing.SystemColors.Menu;
            this.tb_Senha.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Senha.Properties.Appearance.Options.UseBackColor = true;
            this.tb_Senha.Properties.Appearance.Options.UseFont = true;
            this.tb_Senha.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tb_Senha.Properties.NullValuePrompt = "Senha";
            this.tb_Senha.Properties.NullValuePromptShowForEmptyValue = true;
            this.tb_Senha.Properties.PasswordChar = '*';
            this.tb_Senha.Size = new System.Drawing.Size(213, 24);
            this.tb_Senha.TabIndex = 3;
            this.tb_Senha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Senha_KeyDown);
            // 
            // tb_Usuario
            // 
            this.tb_Usuario.EditValue = "";
            this.tb_Usuario.Location = new System.Drawing.Point(281, 118);
            this.tb_Usuario.Name = "tb_Usuario";
            this.tb_Usuario.Properties.Appearance.BackColor = System.Drawing.SystemColors.Menu;
            this.tb_Usuario.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Usuario.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.tb_Usuario.Properties.Appearance.Options.UseBackColor = true;
            this.tb_Usuario.Properties.Appearance.Options.UseFont = true;
            this.tb_Usuario.Properties.Appearance.Options.UseForeColor = true;
            this.tb_Usuario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tb_Usuario.Properties.NullValuePrompt = "Usuário";
            this.tb_Usuario.Properties.NullValuePromptShowForEmptyValue = true;
            this.tb_Usuario.Size = new System.Drawing.Size(213, 24);
            this.tb_Usuario.TabIndex = 2;
            this.tb_Usuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Usuario_KeyDown);
            this.tb_Usuario.Leave += new System.EventHandler(this.tb_Usuario_Leave);
            // 
            // cb_Alias
            // 
            this.cb_Alias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Alias.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Alias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(53)))));
            this.cb_Alias.FormattingEnabled = true;
            this.cb_Alias.Items.AddRange(new object[] {
            "Produção",
            "Teste"});
            this.cb_Alias.Location = new System.Drawing.Point(281, 191);
            this.cb_Alias.Name = "cb_Alias";
            this.cb_Alias.Size = new System.Drawing.Size(169, 22);
            this.cb_Alias.TabIndex = 4;
            this.cb_Alias.SelectedIndexChanged += new System.EventHandler(this.cb_Alias_SelectedIndexChanged);
            // 
            // panel_Sair
            // 
            this.panel_Sair.BackColor = System.Drawing.Color.Transparent;
            this.panel_Sair.Location = new System.Drawing.Point(543, 12);
            this.panel_Sair.Name = "panel_Sair";
            this.panel_Sair.Size = new System.Drawing.Size(20, 24);
            this.panel_Sair.TabIndex = 2;
            this.panel_Sair.Click += new System.EventHandler(this.panel_Sair_Click);
            // 
            // panel_Suporte
            // 
            this.panel_Suporte.BackColor = System.Drawing.Color.Transparent;
            this.panel_Suporte.Location = new System.Drawing.Point(543, 326);
            this.panel_Suporte.Name = "panel_Suporte";
            this.panel_Suporte.Size = new System.Drawing.Size(20, 24);
            this.panel_Suporte.TabIndex = 5;
            this.panel_Suporte.Click += new System.EventHandler(this.panel_Suporte_Click);
            // 
            // lblVersao
            // 
            this.lblVersao.AutoSize = true;
            this.lblVersao.BackColor = System.Drawing.Color.Transparent;
            this.lblVersao.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersao.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblVersao.Location = new System.Drawing.Point(111, 288);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(123, 16);
            this.lblVersao.TabIndex = 34;
            this.lblVersao.Text = "dd/mm/yyyy.nnn";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label5.Location = new System.Drawing.Point(45, 288);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 16);
            this.label5.TabIndex = 33;
            this.label5.Text = "Versão:";
            // 
            // FormNewLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AppFatureClient.Properties.Resources.Tela_de_login_fature_sales_Horizontal__oficial_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(575, 362);
            this.Controls.Add(this.lblVersao);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel_Suporte);
            this.Controls.Add(this.panel_Sair);
            this.Controls.Add(this.cb_Alias);
            this.Controls.Add(this.tb_Senha);
            this.Controls.Add(this.tb_Usuario);
            this.Controls.Add(this.panel_Alias);
            this.Controls.Add(this.panel_Login);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormNewLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FatureSales Login";
            this.Load += new System.EventHandler(this.FormNewLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tb_Senha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Usuario.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_Login;
        private System.Windows.Forms.Panel panel_Alias;
        private DevExpress.XtraEditors.TextEdit tb_Senha;
        private DevExpress.XtraEditors.TextEdit tb_Usuario;
        private System.Windows.Forms.ComboBox cb_Alias;
        private System.Windows.Forms.Panel panel_Sair;
        private System.Windows.Forms.Panel panel_Suporte;
        private System.Windows.Forms.Label lblVersao;
        private System.Windows.Forms.Label label5;
    }
}