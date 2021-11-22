namespace AppLib.Windows
{
    partial class FormTabPrecoCadastroItem
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
            this.components = new System.ComponentModel.Container();
            AppLib.Windows.CodigoNome codigoNome1 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome2 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome3 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome4 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome5 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome6 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome7 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome8 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome9 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome10 = new AppLib.Windows.CodigoNome();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTabPrecoCadastroItem));
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCANCELAR = new DevExpress.XtraEditors.SimpleButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.listaChapa = new AppLib.Windows.CampoLista();
            this.label63 = new System.Windows.Forms.Label();
            this.decimalPeso = new AppLib.Windows.CampoDecimal();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.decimalPreco = new AppLib.Windows.CampoDecimal();
            this.txtDescricao = new AppLib.Windows.CampoTexto();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButtonOK);
            this.panel1.Controls.Add(this.simpleButtonCANCELAR);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 61);
            this.panel1.TabIndex = 11;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK.Location = new System.Drawing.Point(238, 18);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCANCELAR
            // 
            this.simpleButtonCANCELAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR.Location = new System.Drawing.Point(328, 18);
            this.simpleButtonCANCELAR.Name = "simpleButtonCANCELAR";
            this.simpleButtonCANCELAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR.TabIndex = 3;
            this.simpleButtonCANCELAR.Text = "Cancelar";
            this.simpleButtonCANCELAR.Click += new System.EventHandler(this.simpleButtonCANCELAR_Click);
            // 
            // listaChapa
            // 
            this.listaChapa.Campo = null;
            this.listaChapa.Default = null;
            this.listaChapa.Edita = true;
            codigoNome1.Codigo = "";
            codigoNome1.Nome = "Selecione";
            codigoNome2.Codigo = "A";
            codigoNome2.Nome = "14";
            codigoNome3.Codigo = "B";
            codigoNome3.Nome = "16";
            codigoNome4.Codigo = "C";
            codigoNome4.Nome = "18";
            codigoNome5.Codigo = "D";
            codigoNome5.Nome = "19";
            codigoNome6.Codigo = "E";
            codigoNome6.Nome = "20";
            codigoNome7.Codigo = "F";
            codigoNome7.Nome = "22";
            codigoNome8.Codigo = "G";
            codigoNome8.Nome = "24";
            codigoNome9.Codigo = "H";
            codigoNome9.Nome = "26";
            codigoNome10.Codigo = "X";
            codigoNome10.Nome = "13";
            this.listaChapa.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2,
        codigoNome3,
        codigoNome4,
        codigoNome5,
        codigoNome6,
        codigoNome7,
        codigoNome8,
        codigoNome9,
        codigoNome10};
            this.listaChapa.Location = new System.Drawing.Point(12, 25);
            this.listaChapa.Name = "listaChapa";
            this.listaChapa.Query = 0;
            this.listaChapa.Size = new System.Drawing.Size(184, 21);
            this.listaChapa.Tabela = null;
            this.listaChapa.TabIndex = 117;
            this.listaChapa.AposSelecao += new AppLib.Windows.CampoLista.AposSelecaoHandler(this.listaChapa_AposSelecao);
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(9, 9);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(38, 13);
            this.label63.TabIndex = 116;
            this.label63.Text = "Chapa";
            // 
            // decimalPeso
            // 
            this.decimalPeso.Campo = null;
            this.decimalPeso.Decimais = 2;
            this.decimalPeso.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.decimalPeso.Edita = true;
            this.decimalPeso.Location = new System.Drawing.Point(202, 26);
            this.decimalPeso.Name = "decimalPeso";
            this.decimalPeso.Query = 0;
            this.decimalPeso.Size = new System.Drawing.Size(100, 20);
            this.decimalPeso.Tabela = null;
            this.decimalPeso.TabIndex = 118;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(199, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 119;
            this.label1.Text = "Peso";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(305, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 121;
            this.label2.Text = "Preço";
            // 
            // decimalPreco
            // 
            this.decimalPreco.Campo = null;
            this.decimalPreco.Decimais = 2;
            this.decimalPreco.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.decimalPreco.Edita = true;
            this.decimalPreco.Location = new System.Drawing.Point(308, 26);
            this.decimalPreco.Name = "decimalPreco";
            this.decimalPreco.Query = 0;
            this.decimalPreco.Size = new System.Drawing.Size(100, 20);
            this.decimalPreco.Tabela = null;
            this.decimalPreco.TabIndex = 120;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Campo = null;
            this.txtDescricao.Default = null;
            this.txtDescricao.Edita = true;
            this.txtDescricao.Location = new System.Drawing.Point(12, 65);
            this.txtDescricao.MaximoCaracteres = null;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Query = 0;
            this.txtDescricao.Size = new System.Drawing.Size(396, 20);
            this.txtDescricao.Tabela = null;
            this.txtDescricao.TabIndex = 122;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 123;
            this.label3.Text = "Descrição Chapa";
            // 
            // FormTabPrecoCadastroItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 171);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.decimalPreco);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.decimalPeso);
            this.Controls.Add(this.listaChapa);
            this.Controls.Add(this.label63);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTabPrecoCadastroItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Item da Tabela de Preço";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCadastroObject_FormClosed);
            this.Load += new System.EventHandler(this.FormCadastro2_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCANCELAR;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private CampoLista listaChapa;
        private System.Windows.Forms.Label label63;
        private CampoDecimal decimalPeso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CampoDecimal decimalPreco;
        private CampoTexto txtDescricao;
        private System.Windows.Forms.Label label3;
    }
}