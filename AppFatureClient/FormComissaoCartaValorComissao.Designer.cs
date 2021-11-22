namespace AppFatureClient
{
    partial class FormComissaoCartaValorComissao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComissaoCartaValorComissao));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.label58 = new System.Windows.Forms.Label();
            this.cpPorcentagem = new AppLib.Windows.CampoDecimal();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(23, 77);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 59;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(104, 77);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 60;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(21, 23);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(132, 13);
            this.label58.TabIndex = 58;
            this.label58.Text = "Porcentagem da comissão";
            // 
            // cpPorcentagem
            // 
            this.cpPorcentagem.Campo = null;
            this.cpPorcentagem.Decimais = 2;
            this.cpPorcentagem.Default = null;
            this.cpPorcentagem.Edita = true;
            this.cpPorcentagem.Location = new System.Drawing.Point(24, 39);
            this.cpPorcentagem.Name = "cpPorcentagem";
            this.cpPorcentagem.Query = 0;
            this.cpPorcentagem.Size = new System.Drawing.Size(155, 20);
            this.cpPorcentagem.Tabela = null;
            this.cpPorcentagem.TabIndex = 62;
            // 
            // FormComissaoCartaValorComissao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 112);
            this.Controls.Add(this.cpPorcentagem);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label58);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormComissaoCartaValorComissao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Porcentagem";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private System.Windows.Forms.Label label58;
        private AppLib.Windows.CampoDecimal cpPorcentagem;
    }
}