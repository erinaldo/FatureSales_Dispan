
namespace AppFatureClient.New.Forms.Register
{
    partial class frmCadastroTributoProduto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroTributoProduto));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.tbCodigoContribuicaoSocial = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.tbSituacaoTributariaSaida = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tbSituacaoTributariaEntrada = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.clTributo = new AppLib.Windows.CampoLookup();
            this.tbAliquota = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.chkInativo = new DevExpress.XtraEditors.CheckEdit();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCodigoContribuicaoSocial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSituacaoTributariaSaida.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSituacaoTributariaEntrada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAliquota.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkInativo.Properties)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Panel2.Controls.Add(this.btnSalvar);
            this.splitContainer1.Size = new System.Drawing.Size(428, 301);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 4;
            this.splitContainer1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(428, 258);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.tbCodigoContribuicaoSocial);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.tbSituacaoTributariaSaida);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.tbSituacaoTributariaEntrada);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.clTributo);
            this.tabPage1.Controls.Add(this.tbAliquota);
            this.tabPage1.Controls.Add(this.labelControl13);
            this.tabPage1.Controls.Add(this.chkInativo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(420, 232);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Principal";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(8, 187);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(126, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Código Contribuição Social";
            // 
            // tbCodigoContribuicaoSocial
            // 
            this.tbCodigoContribuicaoSocial.Location = new System.Drawing.Point(8, 206);
            this.tbCodigoContribuicaoSocial.Name = "tbCodigoContribuicaoSocial";
            this.tbCodigoContribuicaoSocial.Size = new System.Drawing.Size(404, 20);
            this.tbCodigoContribuicaoSocial.TabIndex = 9;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 142);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(134, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Situação Tributária de Saída";
            // 
            // tbSituacaoTributariaSaida
            // 
            this.tbSituacaoTributariaSaida.Location = new System.Drawing.Point(8, 161);
            this.tbSituacaoTributariaSaida.Name = "tbSituacaoTributariaSaida";
            this.tbSituacaoTributariaSaida.Size = new System.Drawing.Size(404, 20);
            this.tbSituacaoTributariaSaida.TabIndex = 7;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 97);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(146, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Situação Tributária de Entrada";
            // 
            // tbSituacaoTributariaEntrada
            // 
            this.tbSituacaoTributariaEntrada.Location = new System.Drawing.Point(8, 116);
            this.tbSituacaoTributariaEntrada.Name = "tbSituacaoTributariaEntrada";
            this.tbSituacaoTributariaEntrada.Size = new System.Drawing.Size(404, 20);
            this.tbSituacaoTributariaEntrada.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(39, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Alíquota";
            // 
            // clTributo
            // 
            this.clTributo.Campo = null;
            this.clTributo.ColunaCodigo = "CODTRB";
            this.clTributo.ColunaDescricao = "DESCRICAO";
            this.clTributo.ColunaIdentificador = null;
            this.clTributo.ColunaTabela = "DTRIBUTO";
            this.clTributo.Conexao = "Start";
            this.clTributo.Default = null;
            this.clTributo.Edita = true;
            this.clTributo.EditaLookup = false;
            this.clTributo.Location = new System.Drawing.Point(8, 25);
            this.clTributo.MaximoCaracteres = null;
            this.clTributo.Name = "clTributo";
            this.clTributo.NomeGrid = null;
            this.clTributo.Query = 0;
            this.clTributo.Size = new System.Drawing.Size(406, 21);
            this.clTributo.Tabela = null;
            this.clTributo.TabIndex = 1;
            // 
            // tbAliquota
            // 
            this.tbAliquota.Location = new System.Drawing.Point(8, 71);
            this.tbAliquota.Name = "tbAliquota";
            this.tbAliquota.Size = new System.Drawing.Size(99, 20);
            this.tbAliquota.TabIndex = 3;
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(8, 6);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(85, 13);
            this.labelControl13.TabIndex = 0;
            this.labelControl13.Text = "Código do Tributo";
            // 
            // chkInativo
            // 
            this.chkInativo.Location = new System.Drawing.Point(550, 26);
            this.chkInativo.Name = "chkInativo";
            this.chkInativo.Properties.Caption = "Inativo?";
            this.chkInativo.Size = new System.Drawing.Size(70, 19);
            this.chkInativo.TabIndex = 4;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(341, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(260, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(179, 5);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 0;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // frmCadastroTributoProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 301);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCadastroTributoProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCadastroTributoProduto";
            this.Load += new System.EventHandler(this.frmCadastroTributoProduto_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCodigoContribuicaoSocial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSituacaoTributariaSaida.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSituacaoTributariaEntrada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAliquota.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkInativo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.TextEdit tbAliquota;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.CheckEdit chkInativo;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnSalvar;
        private AppLib.Windows.CampoLookup clTributo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit tbCodigoContribuicaoSocial;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit tbSituacaoTributariaSaida;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit tbSituacaoTributariaEntrada;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}