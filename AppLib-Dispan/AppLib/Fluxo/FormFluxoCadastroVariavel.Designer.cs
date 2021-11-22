namespace AppLib.Fluxo
{
    partial class FormFluxoCadastroVariavel
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
            AppLib.Windows.CodigoNome codigoNome1 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome2 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome3 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome4 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome5 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome6 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome7 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.campoListaTIPODADO = new AppLib.Windows.CampoLista();
            this.campoTextoTIPOVARIAVEL = new AppLib.Windows.CampoTexto();
            this.campoTextoVARIAVEL = new AppLib.Windows.CampoTexto();
            this.campoTextoFLUXO = new AppLib.Windows.CampoTexto();
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
            this.tabControl1.Size = new System.Drawing.Size(530, 246);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.campoListaTIPODADO);
            this.tabPage1.Controls.Add(this.campoTextoTIPOVARIAVEL);
            this.tabPage1.Controls.Add(this.campoTextoVARIAVEL);
            this.tabPage1.Controls.Add(this.campoTextoFLUXO);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(522, 220);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(69, 46);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(63, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Tipo de Dado";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(69, 115);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(38, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Variável";
            // 
            // campoListaTIPODADO
            // 
            this.campoListaTIPODADO.Campo = "TIPODADO";
            this.campoListaTIPODADO.Default = null;
            codigoNome1.Codigo = "Inteiro";
            codigoNome1.Nome = "int";
            codigoNome2.Codigo = "Decimal";
            codigoNome2.Nome = "decimal";
            codigoNome3.Codigo = "Texto";
            codigoNome3.Nome = "String";
            codigoNome4.Codigo = "Data e Hora";
            codigoNome4.Nome = "DateTime";
            codigoNome5.Codigo = "Lista";
            codigoNome5.Nome = "List<Object>";
            codigoNome6.Codigo = "Tabela";
            codigoNome6.Nome = "DataTable";
            codigoNome7.Codigo = "Verdadeiro ou Falso";
            codigoNome7.Nome = "Boolean";
            this.campoListaTIPODADO.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2,
        codigoNome3,
        codigoNome4,
        codigoNome5,
        codigoNome6,
        codigoNome7};
            this.campoListaTIPODADO.Location = new System.Drawing.Point(69, 65);
            this.campoListaTIPODADO.Name = "campoListaTIPODADO";
            this.campoListaTIPODADO.Query = 0;
            this.campoListaTIPODADO.Size = new System.Drawing.Size(206, 21);
            this.campoListaTIPODADO.Tabela = "ZFLUXOVARIAVEL";
            this.campoListaTIPODADO.TabIndex = 5;
            // 
            // campoTextoTIPOVARIAVEL
            // 
            this.campoTextoTIPOVARIAVEL.Campo = "TIPOVARIAVEL";
            this.campoTextoTIPOVARIAVEL.Default = null;
            this.campoTextoTIPOVARIAVEL.Edita = true;
            this.campoTextoTIPOVARIAVEL.Location = new System.Drawing.Point(377, 65);
            this.campoTextoTIPOVARIAVEL.MaximoCaracteres = null;
            this.campoTextoTIPOVARIAVEL.Name = "campoTextoTIPOVARIAVEL";
            this.campoTextoTIPOVARIAVEL.Query = 0;
            this.campoTextoTIPOVARIAVEL.Size = new System.Drawing.Size(32, 20);
            this.campoTextoTIPOVARIAVEL.Tabela = "ZFLUXOVARIAVEL";
            this.campoTextoTIPOVARIAVEL.TabIndex = 2;
            this.campoTextoTIPOVARIAVEL.Visible = false;
            // 
            // campoTextoVARIAVEL
            // 
            this.campoTextoVARIAVEL.Campo = "VARIAVEL";
            this.campoTextoVARIAVEL.Default = null;
            this.campoTextoVARIAVEL.Edita = true;
            this.campoTextoVARIAVEL.Location = new System.Drawing.Point(69, 134);
            this.campoTextoVARIAVEL.MaximoCaracteres = null;
            this.campoTextoVARIAVEL.Name = "campoTextoVARIAVEL";
            this.campoTextoVARIAVEL.Query = 0;
            this.campoTextoVARIAVEL.Size = new System.Drawing.Size(206, 20);
            this.campoTextoVARIAVEL.Tabela = "ZFLUXOVARIAVEL";
            this.campoTextoVARIAVEL.TabIndex = 1;
            // 
            // campoTextoFLUXO
            // 
            this.campoTextoFLUXO.Campo = "FLUXO";
            this.campoTextoFLUXO.Default = null;
            this.campoTextoFLUXO.Edita = true;
            this.campoTextoFLUXO.Location = new System.Drawing.Point(339, 65);
            this.campoTextoFLUXO.MaximoCaracteres = null;
            this.campoTextoFLUXO.Name = "campoTextoFLUXO";
            this.campoTextoFLUXO.Query = 0;
            this.campoTextoFLUXO.Size = new System.Drawing.Size(32, 20);
            this.campoTextoFLUXO.Tabela = "ZFLUXOVARIAVEL";
            this.campoTextoFLUXO.TabIndex = 0;
            this.campoTextoFLUXO.Visible = false;
            // 
            // FormFluxoCadastroVariavel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 346);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormFluxoCadastroVariavel";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM ZFLUXOVARIAVEL",
        "WHERE FLUXO = ?",
        "  AND VARIAVEL = ?"};
            query1.Parametros = new string[] {
        "FLUXO",
        "VARIAVEL"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "ZFLUXOVARIAVEL";
            this.Text = "Cadastro de Variável";
            this.Load += new System.EventHandler(this.FormFluxoCadastroVariavel_Load);
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
        private Windows.CampoTexto campoTextoTIPOVARIAVEL;
        private Windows.CampoTexto campoTextoVARIAVEL;
        private Windows.CampoTexto campoTextoFLUXO;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Windows.CampoLista campoListaTIPODADO;
    }
}