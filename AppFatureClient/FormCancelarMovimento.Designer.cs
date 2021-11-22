namespace AppFatureClient
{
    partial class FormCancelarMovimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCancelarMovimento));
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCANCELAR = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.campoTextoMOTIVO = new AppLib.Windows.CampoTexto();
            this.campoDataDATA = new AppLib.Windows.CampoData();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::AppFatureClient.WaitForm1), true, false);
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButtonOK);
            this.panel1.Controls.Add(this.simpleButtonCANCELAR);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 222);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 61);
            this.panel1.TabIndex = 11;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOK.Location = new System.Drawing.Point(281, 18);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 3;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCANCELAR
            // 
            this.simpleButtonCANCELAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCANCELAR.Location = new System.Drawing.Point(371, 18);
            this.simpleButtonCANCELAR.Name = "simpleButtonCANCELAR";
            this.simpleButtonCANCELAR.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCANCELAR.TabIndex = 4;
            this.simpleButtonCANCELAR.Text = "Cancelar";
            this.simpleButtonCANCELAR.Click += new System.EventHandler(this.simpleButtonCANCELAR_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(468, 222);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.campoTextoMOTIVO);
            this.tabPage1.Controls.Add(this.campoDataDATA);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(460, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cancelar Movimento";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // campoTextoMOTIVO
            // 
            this.campoTextoMOTIVO.Campo = null;
            this.campoTextoMOTIVO.Default = null;
            this.campoTextoMOTIVO.Edita = true;
            this.campoTextoMOTIVO.Location = new System.Drawing.Point(11, 83);
            this.campoTextoMOTIVO.MaximoCaracteres = null;
            this.campoTextoMOTIVO.Name = "campoTextoMOTIVO";
            this.campoTextoMOTIVO.Query = 0;
            this.campoTextoMOTIVO.Size = new System.Drawing.Size(341, 20);
            this.campoTextoMOTIVO.Tabela = null;
            this.campoTextoMOTIVO.TabIndex = 2;
            // 
            // campoDataDATA
            // 
            this.campoDataDATA.Campo = null;
            this.campoDataDATA.Default = null;
            this.campoDataDATA.Edita = true;
            this.campoDataDATA.Location = new System.Drawing.Point(11, 34);
            this.campoDataDATA.Name = "campoDataDATA";
            this.campoDataDATA.Query = 0;
            this.campoDataDATA.Size = new System.Drawing.Size(100, 20);
            this.campoDataDATA.Tabela = null;
            this.campoDataDATA.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Motivo do Cancelamento";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Data do Cancelamento";
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // FormCancelarMovimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 283);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormCancelarMovimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cancelar Movimento";
            this.Load += new System.EventHandler(this.FormCancelarMovimento_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCANCELAR;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoTexto campoTextoMOTIVO;
        private AppLib.Windows.CampoData campoDataDATA;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}