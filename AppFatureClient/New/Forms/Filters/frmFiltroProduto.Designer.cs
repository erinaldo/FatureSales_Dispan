
namespace AppFatureClient.New.Forms.Filters
{
    partial class frmFiltroProduto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFiltroProduto));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAtivo = new DevExpress.XtraEditors.CheckEdit();
            this.tbValorFiltro = new DevExpress.XtraEditors.TextEdit();
            this.lpTipoProduto = new DevExpress.XtraEditors.LookUpEdit();
            this.lbValorFiltro = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbStatus = new System.Windows.Forms.RadioButton();
            this.rbCodigoProduto = new System.Windows.Forms.RadioButton();
            this.rbTipoProduto = new System.Windows.Forms.RadioButton();
            this.rbNomeFantasia = new System.Windows.Forms.RadioButton();
            this.rbIdentificador = new System.Windows.Forms.RadioButton();
            this.rbCodigoAuxiliar = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirmar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAtivo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbValorFiltro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpTipoProduto.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(304, 366);
            this.splitContainer1.SplitterDistance = 317;
            this.splitContainer1.TabIndex = 5;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(304, 317);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.groupBox2);
            this.xtraTabPage1.Controls.Add(this.groupBox1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(298, 289);
            this.xtraTabPage1.Text = "Opções";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkAtivo);
            this.groupBox2.Controls.Add(this.tbValorFiltro);
            this.groupBox2.Controls.Add(this.lpTipoProduto);
            this.groupBox2.Controls.Add(this.lbValorFiltro);
            this.groupBox2.Location = new System.Drawing.Point(3, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(292, 142);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parâmetros";
            // 
            // chkAtivo
            // 
            this.chkAtivo.Location = new System.Drawing.Point(40, 106);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Properties.Caption = "Ativo?";
            this.chkAtivo.Size = new System.Drawing.Size(64, 19);
            this.chkAtivo.TabIndex = 23;
            this.chkAtivo.Visible = false;
            // 
            // tbValorFiltro
            // 
            this.tbValorFiltro.Location = new System.Drawing.Point(40, 80);
            this.tbValorFiltro.Name = "tbValorFiltro";
            this.tbValorFiltro.Size = new System.Drawing.Size(212, 20);
            this.tbValorFiltro.TabIndex = 22;
            this.tbValorFiltro.Visible = false;
            this.tbValorFiltro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbValorFiltro_KeyPress);
            // 
            // lpTipoProduto
            // 
            this.lpTipoProduto.Location = new System.Drawing.Point(40, 54);
            this.lpTipoProduto.Name = "lpTipoProduto";
            this.lpTipoProduto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lpTipoProduto.Size = new System.Drawing.Size(212, 20);
            this.lpTipoProduto.TabIndex = 21;
            this.lpTipoProduto.Visible = false;
            // 
            // lbValorFiltro
            // 
            this.lbValorFiltro.Location = new System.Drawing.Point(40, 35);
            this.lbValorFiltro.Name = "lbValorFiltro";
            this.lbValorFiltro.Size = new System.Drawing.Size(24, 13);
            this.lbValorFiltro.TabIndex = 0;
            this.lbValorFiltro.Text = "Valor";
            this.lbValorFiltro.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbStatus);
            this.groupBox1.Controls.Add(this.rbCodigoProduto);
            this.groupBox1.Controls.Add(this.rbTipoProduto);
            this.groupBox1.Controls.Add(this.rbNomeFantasia);
            this.groupBox1.Controls.Add(this.rbIdentificador);
            this.groupBox1.Controls.Add(this.rbCodigoAuxiliar);
            this.groupBox1.Controls.Add(this.rbTodos);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // rbStatus
            // 
            this.rbStatus.AutoSize = true;
            this.rbStatus.Location = new System.Drawing.Point(190, 78);
            this.rbStatus.Name = "rbStatus";
            this.rbStatus.Size = new System.Drawing.Size(56, 17);
            this.rbStatus.TabIndex = 13;
            this.rbStatus.Text = "Status";
            this.rbStatus.UseVisualStyleBackColor = true;
            this.rbStatus.CheckedChanged += new System.EventHandler(this.rbInativo_CheckedChanged);
            // 
            // rbCodigoProduto
            // 
            this.rbCodigoProduto.AutoSize = true;
            this.rbCodigoProduto.Location = new System.Drawing.Point(8, 101);
            this.rbCodigoProduto.Name = "rbCodigoProduto";
            this.rbCodigoProduto.Size = new System.Drawing.Size(114, 17);
            this.rbCodigoProduto.TabIndex = 8;
            this.rbCodigoProduto.Text = "Código do Produto";
            this.rbCodigoProduto.UseVisualStyleBackColor = true;
            this.rbCodigoProduto.CheckedChanged += new System.EventHandler(this.rbCodigoProduto_CheckedChanged);
            // 
            // rbTipoProduto
            // 
            this.rbTipoProduto.AutoSize = true;
            this.rbTipoProduto.Location = new System.Drawing.Point(190, 55);
            this.rbTipoProduto.Name = "rbTipoProduto";
            this.rbTipoProduto.Size = new System.Drawing.Size(86, 17);
            this.rbTipoProduto.TabIndex = 11;
            this.rbTipoProduto.Text = "Tipo Produto";
            this.rbTipoProduto.UseVisualStyleBackColor = true;
            this.rbTipoProduto.CheckedChanged += new System.EventHandler(this.rbTipoProduto_CheckedChanged);
            // 
            // rbNomeFantasia
            // 
            this.rbNomeFantasia.AutoSize = true;
            this.rbNomeFantasia.Location = new System.Drawing.Point(190, 32);
            this.rbNomeFantasia.Name = "rbNomeFantasia";
            this.rbNomeFantasia.Size = new System.Drawing.Size(96, 17);
            this.rbNomeFantasia.TabIndex = 10;
            this.rbNomeFantasia.Text = "Nome Fantasia";
            this.rbNomeFantasia.UseVisualStyleBackColor = true;
            this.rbNomeFantasia.CheckedChanged += new System.EventHandler(this.rbNomeFantasia_CheckedChanged);
            // 
            // rbIdentificador
            // 
            this.rbIdentificador.AutoSize = true;
            this.rbIdentificador.Location = new System.Drawing.Point(8, 55);
            this.rbIdentificador.Name = "rbIdentificador";
            this.rbIdentificador.Size = new System.Drawing.Size(86, 17);
            this.rbIdentificador.TabIndex = 9;
            this.rbIdentificador.Text = "Identificador";
            this.rbIdentificador.UseVisualStyleBackColor = true;
            this.rbIdentificador.CheckedChanged += new System.EventHandler(this.rbIdentificador_CheckedChanged);
            // 
            // rbCodigoAuxiliar
            // 
            this.rbCodigoAuxiliar.AutoSize = true;
            this.rbCodigoAuxiliar.Location = new System.Drawing.Point(8, 78);
            this.rbCodigoAuxiliar.Name = "rbCodigoAuxiliar";
            this.rbCodigoAuxiliar.Size = new System.Drawing.Size(96, 17);
            this.rbCodigoAuxiliar.TabIndex = 8;
            this.rbCodigoAuxiliar.Text = "Código Auxiliar";
            this.rbCodigoAuxiliar.UseVisualStyleBackColor = true;
            this.rbCodigoAuxiliar.CheckedChanged += new System.EventHandler(this.rbCodigoAuxiliar_CheckedChanged);
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Location = new System.Drawing.Point(8, 32);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(54, 17);
            this.rbTodos.TabIndex = 7;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbTodos_CheckedChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(217, 10);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(136, 10);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmar.TabIndex = 0;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // frmFiltroProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 366);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFiltroProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro";
            this.Load += new System.EventHandler(this.frmFiltroProduto_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAtivo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbValorFiltro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpTipoProduto.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.LookUpEdit lpTipoProduto;
        private DevExpress.XtraEditors.LabelControl lbValorFiltro;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnConfirmar;
        private System.Windows.Forms.RadioButton rbCodigoProduto;
        private System.Windows.Forms.RadioButton rbTipoProduto;
        private System.Windows.Forms.RadioButton rbNomeFantasia;
        private System.Windows.Forms.RadioButton rbIdentificador;
        private System.Windows.Forms.RadioButton rbCodigoAuxiliar;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.RadioButton rbStatus;
        private DevExpress.XtraEditors.TextEdit tbValorFiltro;
        private DevExpress.XtraEditors.CheckEdit chkAtivo;
    }
}