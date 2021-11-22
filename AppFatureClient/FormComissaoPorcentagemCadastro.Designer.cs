namespace AppFatureClient
{
    partial class FormComissaoPorcentagemCadastro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComissaoPorcentagemCadastro));
            this.txtDe = new AppLib.Windows.CampoDecimal();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAte = new AppLib.Windows.CampoDecimal();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPorcentagem = new AppLib.Windows.CampoDecimal();
            this.btnSalvar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // txtDe
            // 
            this.txtDe.Campo = null;
            this.txtDe.Decimais = 2;
            this.txtDe.Default = null;
            this.txtDe.Edita = true;
            this.txtDe.Location = new System.Drawing.Point(33, 39);
            this.txtDe.Name = "txtDe";
            this.txtDe.Query = 0;
            this.txtDe.Size = new System.Drawing.Size(100, 20);
            this.txtDe.Tabela = null;
            this.txtDe.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "De";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(147, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Até";
            // 
            // txtAte
            // 
            this.txtAte.Campo = null;
            this.txtAte.Decimais = 2;
            this.txtAte.Default = null;
            this.txtAte.Edita = true;
            this.txtAte.Location = new System.Drawing.Point(150, 39);
            this.txtAte.Name = "txtAte";
            this.txtAte.Query = 0;
            this.txtAte.Size = new System.Drawing.Size(100, 20);
            this.txtAte.Tabela = null;
            this.txtAte.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(265, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Porcentagem";
            // 
            // txtPorcentagem
            // 
            this.txtPorcentagem.Campo = null;
            this.txtPorcentagem.Decimais = 2;
            this.txtPorcentagem.Default = null;
            this.txtPorcentagem.Edita = true;
            this.txtPorcentagem.Location = new System.Drawing.Point(268, 39);
            this.txtPorcentagem.Name = "txtPorcentagem";
            this.txtPorcentagem.Query = 0;
            this.txtPorcentagem.Size = new System.Drawing.Size(100, 20);
            this.txtPorcentagem.Tabela = null;
            this.txtPorcentagem.TabIndex = 4;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(212, 90);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(293, 90);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormComissaoPorcentagemCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 140);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPorcentagem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAte);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDe);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormComissaoPorcentagemCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alterar Porcentagem";
            this.Load += new System.EventHandler(this.FormComissaoPorcentagemCadastro_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AppLib.Windows.CampoDecimal txtDe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private AppLib.Windows.CampoDecimal txtAte;
        private System.Windows.Forms.Label label3;
        private AppLib.Windows.CampoDecimal txtPorcentagem;
        private DevExpress.XtraEditors.SimpleButton btnSalvar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
    }
}