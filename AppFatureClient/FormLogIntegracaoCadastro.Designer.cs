namespace AppFatureClient
{
    partial class FormLogIntegracaoCadastro
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
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.campoMemo1 = new AppLib.Windows.CampoMemo();
            this.campoDataHora2 = new AppLib.Windows.CampoDataHora();
            this.label6 = new System.Windows.Forms.Label();
            this.campoTexto5 = new AppLib.Windows.CampoTexto();
            this.label5 = new System.Windows.Forms.Label();
            this.campoTexto4 = new AppLib.Windows.CampoTexto();
            this.label4 = new System.Windows.Forms.Label();
            this.campoTexto3 = new AppLib.Windows.CampoTexto();
            this.label3 = new System.Windows.Forms.Label();
            this.campoTexto2 = new AppLib.Windows.CampoTexto();
            this.label2 = new System.Windows.Forms.Label();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.label11 = new System.Windows.Forms.Label();
            this.campoTextoNORDEM = new AppLib.Windows.CampoTexto();
            this.campoInteiro1 = new AppLib.Windows.CampoInteiro();
            this.campoInteiroIDMOV = new AppLib.Windows.CampoInteiro();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(645, 306);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.campoMemo1);
            this.tabPage1.Controls.Add(this.campoDataHora2);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.campoTexto5);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.campoTexto4);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.campoTexto3);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.campoTexto2);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.campoTextoNORDEM);
            this.tabPage1.Controls.Add(this.campoInteiro1);
            this.tabPage1.Controls.Add(this.campoInteiroIDMOV);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(637, 280);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Mensagem";
            // 
            // campoMemo1
            // 
            this.campoMemo1.Campo = "MSG_ERRO";
            this.campoMemo1.Default = null;
            this.campoMemo1.Enabled = false;
            this.campoMemo1.Location = new System.Drawing.Point(26, 150);
            this.campoMemo1.Name = "campoMemo1";
            this.campoMemo1.Query = 0;
            this.campoMemo1.Size = new System.Drawing.Size(593, 124);
            this.campoMemo1.Tabela = "ZLOGINTEGRACAO";
            this.campoMemo1.TabIndex = 26;
            // 
            // campoDataHora2
            // 
            this.campoDataHora2.Campo = "DATA_ERRO";
            this.campoDataHora2.Default = null;
            this.campoDataHora2.Enabled = false;
            this.campoDataHora2.Location = new System.Drawing.Point(450, 111);
            this.campoDataHora2.Name = "campoDataHora2";
            this.campoDataHora2.Query = 0;
            this.campoDataHora2.Size = new System.Drawing.Size(169, 20);
            this.campoDataHora2.Tabela = "ZLOGINTEGRACAO";
            this.campoDataHora2.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(447, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Data";
            // 
            // campoTexto5
            // 
            this.campoTexto5.Campo = "ROTINA";
            this.campoTexto5.Default = null;
            this.campoTexto5.Enabled = false;
            this.campoTexto5.Location = new System.Drawing.Point(26, 111);
            this.campoTexto5.Name = "campoTexto5";
            this.campoTexto5.Query = 0;
            this.campoTexto5.Size = new System.Drawing.Size(418, 20);
            this.campoTexto5.Tabela = "ZLOGINTEGRACAO";
            this.campoTexto5.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Rotina";
            // 
            // campoTexto4
            // 
            this.campoTexto4.Campo = "VALORCAMPO";
            this.campoTexto4.Default = null;
            this.campoTexto4.Enabled = false;
            this.campoTexto4.Location = new System.Drawing.Point(450, 72);
            this.campoTexto4.Name = "campoTexto4";
            this.campoTexto4.Query = 0;
            this.campoTexto4.Size = new System.Drawing.Size(169, 20);
            this.campoTexto4.Tabela = "ZLOGINTEGRACAO";
            this.campoTexto4.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(447, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Valor";
            // 
            // campoTexto3
            // 
            this.campoTexto3.Campo = "CAMPOTABELA";
            this.campoTexto3.Default = null;
            this.campoTexto3.Enabled = false;
            this.campoTexto3.Location = new System.Drawing.Point(344, 72);
            this.campoTexto3.Name = "campoTexto3";
            this.campoTexto3.Query = 0;
            this.campoTexto3.Size = new System.Drawing.Size(100, 20);
            this.campoTexto3.Tabela = "ZLOGINTEGRACAO";
            this.campoTexto3.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(341, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Campo";
            // 
            // campoTexto2
            // 
            this.campoTexto2.Campo = "APLICATIVO";
            this.campoTexto2.Default = null;
            this.campoTexto2.Enabled = false;
            this.campoTexto2.Location = new System.Drawing.Point(238, 72);
            this.campoTexto2.Name = "campoTexto2";
            this.campoTexto2.Query = 0;
            this.campoTexto2.Size = new System.Drawing.Size(100, 20);
            this.campoTexto2.Tabela = "ZLOGINTEGRACAO";
            this.campoTexto2.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Aplicativo";
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "TABELA";
            this.campoTexto1.Default = null;
            this.campoTexto1.Enabled = false;
            this.campoTexto1.Location = new System.Drawing.Point(132, 72);
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(100, 20);
            this.campoTexto1.Tabela = "ZLOGINTEGRACAO";
            this.campoTexto1.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(129, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Tabela";
            // 
            // campoTextoNORDEM
            // 
            this.campoTextoNORDEM.Campo = "USUARIO";
            this.campoTextoNORDEM.Default = null;
            this.campoTextoNORDEM.Enabled = false;
            this.campoTextoNORDEM.Location = new System.Drawing.Point(26, 72);
            this.campoTextoNORDEM.Name = "campoTextoNORDEM";
            this.campoTextoNORDEM.Query = 0;
            this.campoTextoNORDEM.Size = new System.Drawing.Size(100, 20);
            this.campoTextoNORDEM.Tabela = "ZLOGINTEGRACAO";
            this.campoTextoNORDEM.TabIndex = 12;
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "IDLOG";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Enabled = false;
            this.campoInteiro1.Location = new System.Drawing.Point(26, 33);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro1.Tabela = "ZLOGINTEGRACAO";
            this.campoInteiro1.TabIndex = 11;
            // 
            // campoInteiroIDMOV
            // 
            this.campoInteiroIDMOV.Campo = "CODCOLIGADA";
            this.campoInteiroIDMOV.Default = null;
            this.campoInteiroIDMOV.Enabled = false;
            this.campoInteiroIDMOV.Location = new System.Drawing.Point(519, 17);
            this.campoInteiroIDMOV.Name = "campoInteiroIDMOV";
            this.campoInteiroIDMOV.Query = 0;
            this.campoInteiroIDMOV.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroIDMOV.Tabela = "ZLOGINTEGRACAO";
            this.campoInteiroIDMOV.TabIndex = 10;
            this.campoInteiroIDMOV.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Usuário";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Id. Log";
            // 
            // FormLogIntegracaoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BotaoExcluir = true;
            this.ClientSize = new System.Drawing.Size(645, 398);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormLogIntegracaoCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT * FROM ZLOGINTEGRACAO WHERE CODCOLIGADA = ? AND IDLOG = ?"};
            query1.Parametros = new string[] {
        "CODCOLIGADA",
        "IDLOG"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZLOGINTEGRACAO";
            this.Text = "Log de Integração";
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoInteiro campoInteiro1;
        private AppLib.Windows.CampoInteiro campoInteiroIDMOV;
        private System.Windows.Forms.Label label6;
        private AppLib.Windows.CampoTexto campoTexto5;
        private System.Windows.Forms.Label label5;
        private AppLib.Windows.CampoTexto campoTexto4;
        private System.Windows.Forms.Label label4;
        private AppLib.Windows.CampoTexto campoTexto3;
        private System.Windows.Forms.Label label3;
        private AppLib.Windows.CampoTexto campoTexto2;
        private System.Windows.Forms.Label label2;
        private AppLib.Windows.CampoTexto campoTexto1;
        private System.Windows.Forms.Label label11;
        private AppLib.Windows.CampoTexto campoTextoNORDEM;
        private AppLib.Windows.CampoDataHora campoDataHora2;
        private System.Windows.Forms.Label label8;
        private AppLib.Windows.CampoMemo campoMemo1;
    }
}