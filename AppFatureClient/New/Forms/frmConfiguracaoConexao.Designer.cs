namespace AppFatureClient.New.Forms
{
    partial class frmConfiguracaoConexao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfiguracaoConexao));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnTestarConexaoERP = new DevExpress.XtraEditors.SimpleButton();
            this.tbUsuarioERP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.tbSenhaERP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.tbDatabaseERP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tbServerERP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnTestarConexaoRM = new DevExpress.XtraEditors.SimpleButton();
            this.tbUsuarioRM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.tbSenhaRM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.tbDatabaseRM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.tbServerRM = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbUsuarioERP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSenhaERP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDatabaseERP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbServerERP.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbUsuarioRM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSenhaRM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDatabaseRM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbServerRM.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.xtraTabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer1.Panel2.Controls.Add(this.btnConfirmar);
            this.splitContainer1.Size = new System.Drawing.Size(321, 259);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 6;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(321, 215);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.groupControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(315, 187);
            this.xtraTabPage1.Text = "ERP ";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.btnTestarConexaoERP);
            this.groupControl1.Controls.Add(this.tbUsuarioERP);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.tbSenhaERP);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.tbDatabaseERP);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.tbServerERP);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(315, 187);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Parâmetros de Conexão";
            // 
            // btnTestarConexaoERP
            // 
            this.btnTestarConexaoERP.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTestarConexaoERP.ImageOptions.Image")));
            this.btnTestarConexaoERP.Location = new System.Drawing.Point(160, 158);
            this.btnTestarConexaoERP.Name = "btnTestarConexaoERP";
            this.btnTestarConexaoERP.Size = new System.Drawing.Size(149, 23);
            this.btnTestarConexaoERP.TabIndex = 8;
            this.btnTestarConexaoERP.Text = "Testar conexão";
            // 
            // tbUsuarioERP
            // 
            this.tbUsuarioERP.Location = new System.Drawing.Point(12, 132);
            this.tbUsuarioERP.Name = "tbUsuarioERP";
            this.tbUsuarioERP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.tbUsuarioERP.Size = new System.Drawing.Size(142, 20);
            this.tbUsuarioERP.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 113);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Usuário";
            // 
            // tbSenhaERP
            // 
            this.tbSenhaERP.Location = new System.Drawing.Point(160, 132);
            this.tbSenhaERP.Name = "tbSenhaERP";
            this.tbSenhaERP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.tbSenhaERP.Properties.PasswordChar = '*';
            this.tbSenhaERP.Size = new System.Drawing.Size(149, 20);
            this.tbSenhaERP.TabIndex = 7;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(160, 113);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Senha";
            // 
            // tbDatabaseERP
            // 
            this.tbDatabaseERP.Location = new System.Drawing.Point(12, 87);
            this.tbDatabaseERP.Name = "tbDatabaseERP";
            this.tbDatabaseERP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.tbDatabaseERP.Size = new System.Drawing.Size(297, 20);
            this.tbDatabaseERP.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(77, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Banco de Dados";
            // 
            // tbServerERP
            // 
            this.tbServerERP.Location = new System.Drawing.Point(12, 42);
            this.tbServerERP.Name = "tbServerERP";
            this.tbServerERP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.tbServerERP.Size = new System.Drawing.Size(297, 20);
            this.tbServerERP.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Servidor";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.groupControl2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(315, 187);
            this.xtraTabPage2.Text = "RM";
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.Controls.Add(this.btnTestarConexaoRM);
            this.groupControl2.Controls.Add(this.tbUsuarioRM);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.tbSenhaRM);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.tbDatabaseRM);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.tbServerRM);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(315, 187);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Parâmetros de Conexão";
            // 
            // btnTestarConexaoRM
            // 
            this.btnTestarConexaoRM.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTestarConexaoRM.ImageOptions.Image")));
            this.btnTestarConexaoRM.Location = new System.Drawing.Point(160, 158);
            this.btnTestarConexaoRM.Name = "btnTestarConexaoRM";
            this.btnTestarConexaoRM.Size = new System.Drawing.Size(149, 23);
            this.btnTestarConexaoRM.TabIndex = 8;
            this.btnTestarConexaoRM.Text = "Testar conexão";
            // 
            // tbUsuarioRM
            // 
            this.tbUsuarioRM.Location = new System.Drawing.Point(12, 132);
            this.tbUsuarioRM.Name = "tbUsuarioRM";
            this.tbUsuarioRM.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.tbUsuarioRM.Size = new System.Drawing.Size(142, 20);
            this.tbUsuarioRM.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 113);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Usuário";
            // 
            // tbSenhaRM
            // 
            this.tbSenhaRM.Location = new System.Drawing.Point(160, 132);
            this.tbSenhaRM.Name = "tbSenhaRM";
            this.tbSenhaRM.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.tbSenhaRM.Properties.PasswordChar = '*';
            this.tbSenhaRM.Size = new System.Drawing.Size(149, 20);
            this.tbSenhaRM.TabIndex = 7;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(160, 113);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(30, 13);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "Senha";
            // 
            // tbDatabaseRM
            // 
            this.tbDatabaseRM.Location = new System.Drawing.Point(12, 87);
            this.tbDatabaseRM.Name = "tbDatabaseRM";
            this.tbDatabaseRM.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.tbDatabaseRM.Size = new System.Drawing.Size(297, 20);
            this.tbDatabaseRM.TabIndex = 3;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(12, 68);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(77, 13);
            this.labelControl7.TabIndex = 2;
            this.labelControl7.Text = "Banco de Dados";
            // 
            // tbServerRM
            // 
            this.tbServerRM.Location = new System.Drawing.Point(12, 42);
            this.tbServerRM.Name = "tbServerRM";
            this.tbServerRM.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.tbServerRM.Size = new System.Drawing.Size(297, 20);
            this.tbServerRM.TabIndex = 1;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(12, 23);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(40, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "Servidor";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(235, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(154, 4);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmar.TabIndex = 0;
            this.btnConfirmar.Text = "Confirmar";
            // 
            // frmConfiguracaoConexao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 259);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmConfiguracaoConexao";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração de Conexão ";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbUsuarioERP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSenhaERP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDatabaseERP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbServerERP.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbUsuarioRM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSenhaRM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDatabaseRM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbServerRM.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnTestarConexaoERP;
        private DevExpress.XtraEditors.TextEdit tbUsuarioERP;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit tbSenhaERP;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit tbDatabaseERP;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit tbServerERP;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnTestarConexaoRM;
        private DevExpress.XtraEditors.TextEdit tbUsuarioRM;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit tbSenhaRM;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit tbDatabaseRM;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit tbServerRM;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
    }
}