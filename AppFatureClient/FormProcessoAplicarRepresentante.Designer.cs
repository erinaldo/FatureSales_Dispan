namespace AppFatureClient
{
    partial class FormProcessoAplicarRepresentante
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcessoAplicarRepresentante));
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.campoDecimalPercDescontoRepr = new AppLib.Windows.CampoDecimal();
            this.campoLookupCODRPR = new AppLib.Windows.CampoLookup();
            this.label58 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(554, 72);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 59;
            this.btnAdd.Text = "Aplicar";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // campoDecimalPercDescontoRepr
            // 
            this.campoDecimalPercDescontoRepr.Campo = null;
            this.campoDecimalPercDescontoRepr.Decimais = 2;
            this.campoDecimalPercDescontoRepr.Default = null;
            this.campoDecimalPercDescontoRepr.Edita = true;
            this.campoDecimalPercDescontoRepr.Location = new System.Drawing.Point(12, 75);
            this.campoDecimalPercDescontoRepr.Name = "campoDecimalPercDescontoRepr";
            this.campoDecimalPercDescontoRepr.Query = 0;
            this.campoDecimalPercDescontoRepr.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalPercDescontoRepr.Tabela = null;
            this.campoDecimalPercDescontoRepr.TabIndex = 58;
            // 
            // campoLookupCODRPR
            // 
            this.campoLookupCODRPR.AutoSize = true;
            this.campoLookupCODRPR.Campo = "CODREPRESENTANTE";
            this.campoLookupCODRPR.ColunaCodigo = "CODRPR";
            this.campoLookupCODRPR.ColunaDescricao = "NOMEFANTASIA";
            this.campoLookupCODRPR.ColunaIdentificador = null;
            this.campoLookupCODRPR.ColunaTabela = "TRPR";
            this.campoLookupCODRPR.Conexao = "Start";
            this.campoLookupCODRPR.Default = null;
            this.campoLookupCODRPR.Edita = true;
            this.campoLookupCODRPR.EditaLookup = false;
            this.campoLookupCODRPR.Location = new System.Drawing.Point(12, 32);
            this.campoLookupCODRPR.MaximoCaracteres = null;
            this.campoLookupCODRPR.Name = "campoLookupCODRPR";
            this.campoLookupCODRPR.NomeGrid = null;
            this.campoLookupCODRPR.Query = 0;
            this.campoLookupCODRPR.Size = new System.Drawing.Size(619, 24);
            this.campoLookupCODRPR.Tabela = "ZREPREDESC";
            this.campoLookupCODRPR.TabIndex = 57;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(12, 16);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(77, 13);
            this.label58.TabIndex = 60;
            this.label58.Text = "Representante";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "Perc. Desconto";
            // 
            // FormProcessoAplicarRepresentante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 109);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label58);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.campoDecimalPercDescontoRepr);
            this.Controls.Add(this.campoLookupCODRPR);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormProcessoAplicarRepresentante";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aplicar Representante";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private AppLib.Windows.CampoDecimal campoDecimalPercDescontoRepr;
        private AppLib.Windows.CampoLookup campoLookupCODRPR;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label4;
    }
}