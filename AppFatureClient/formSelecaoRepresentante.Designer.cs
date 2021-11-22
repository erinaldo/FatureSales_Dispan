namespace AppFatureClient
{
    partial class formSelecaoRepresentante
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formSelecaoRepresentante));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.label58 = new System.Windows.Forms.Label();
            this.campoLookupCODRPR = new AppLib.Windows.CampoLookup();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(365, 69);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 59;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(21, 23);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(77, 13);
            this.label58.TabIndex = 58;
            this.label58.Text = "Representante";
            // 
            // campoLookupCODRPR
            // 
            this.campoLookupCODRPR.AutoSize = true;
            this.campoLookupCODRPR.Campo = "CODRPR";
            this.campoLookupCODRPR.ColunaCodigo = "CODRPR";
            this.campoLookupCODRPR.ColunaDescricao = "NOMEFANTASIA";
            this.campoLookupCODRPR.ColunaIdentificador = null;
            this.campoLookupCODRPR.ColunaTabela = "TRPR";
            this.campoLookupCODRPR.Conexao = "Start";
            this.campoLookupCODRPR.Default = null;
            this.campoLookupCODRPR.Edita = true;
            this.campoLookupCODRPR.EditaLookup = false;
            this.campoLookupCODRPR.Location = new System.Drawing.Point(24, 39);
            this.campoLookupCODRPR.MaximoCaracteres = null;
            this.campoLookupCODRPR.Name = "campoLookupCODRPR";
            this.campoLookupCODRPR.NomeGrid = null;
            this.campoLookupCODRPR.Query = 0;
            this.campoLookupCODRPR.Size = new System.Drawing.Size(497, 24);
            this.campoLookupCODRPR.Tabela = "TRPR";
            this.campoLookupCODRPR.TabIndex = 57;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(446, 69);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 60;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // formSelecaoRepresentante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 112);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label58);
            this.Controls.Add(this.campoLookupCODRPR);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formSelecaoRepresentante";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleção de Representante";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.Label label58;
        private AppLib.Windows.CampoLookup campoLookupCODRPR;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
    }
}