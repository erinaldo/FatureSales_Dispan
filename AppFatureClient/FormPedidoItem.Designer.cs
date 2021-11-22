namespace AppFatureClient
{
    partial class FormPedidoItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPedidoItem));
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.campoInteiro3 = new AppLib.Windows.CampoInteiro();
            this.campoInteiro2 = new AppLib.Windows.CampoInteiro();
            this.campoInteiro1 = new AppLib.Windows.CampoInteiro();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(735, 185);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Controls.Add(this.campoInteiro3);
            this.tabPage1.Controls.Add(this.campoInteiro2);
            this.tabPage1.Controls.Add(this.campoInteiro1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(727, 159);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cadastro";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Desc. licitação:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "N. Seq. Item:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Id. Movimento:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cód. Coligada:";
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "DESCLICITACAO";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(115, 100);
            this.campoTexto1.MaximoCaracteres = null;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(581, 20);
            this.campoTexto1.Tabela = "TITMMOVCOMPL";
            this.campoTexto1.TabIndex = 3;
            // 
            // campoInteiro3
            // 
            this.campoInteiro3.Campo = "NSEQITMMOV";
            this.campoInteiro3.Default = null;
            this.campoInteiro3.Edita = true;
            this.campoInteiro3.Enabled = false;
            this.campoInteiro3.Location = new System.Drawing.Point(115, 73);
            this.campoInteiro3.Name = "campoInteiro3";
            this.campoInteiro3.Query = 0;
            this.campoInteiro3.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro3.Tabela = "TITMMOVCOMPL";
            this.campoInteiro3.TabIndex = 2;
            // 
            // campoInteiro2
            // 
            this.campoInteiro2.Campo = "IDMOV";
            this.campoInteiro2.Default = null;
            this.campoInteiro2.Edita = true;
            this.campoInteiro2.Enabled = false;
            this.campoInteiro2.Location = new System.Drawing.Point(115, 46);
            this.campoInteiro2.Name = "campoInteiro2";
            this.campoInteiro2.Query = 0;
            this.campoInteiro2.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro2.Tabela = "TITMMOVCOMPL";
            this.campoInteiro2.TabIndex = 1;
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "CODCOLIGADA";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Edita = true;
            this.campoInteiro1.Enabled = false;
            this.campoInteiro1.Location = new System.Drawing.Point(115, 19);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro1.Tabela = "TITMMOVCOMPL";
            this.campoInteiro1.TabIndex = 0;
            // 
            // FormPedidoItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 285);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPedidoItem";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM TITMMOVCOMPL",
        "WHERE CODCOLIGADA = ?",
        "  AND IDMOV = ?",
        "  AND NSEQITMMOV = ?"};
            query1.Parametros = new string[] {
        "CODCOLIGADA",
        "IDMOV",
        "NSEQITMMOV"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "TITMMOVCOMPL";
            this.Text = "Complementar do Item";
            this.Load += new System.EventHandler(this.FormPedidoItem_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoTexto campoTexto1;
        private AppLib.Windows.CampoInteiro campoInteiro3;
        private AppLib.Windows.CampoInteiro campoInteiro2;
        private AppLib.Windows.CampoInteiro campoInteiro1;
    }
}