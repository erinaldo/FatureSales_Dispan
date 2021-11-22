using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppLib.Windows
{
    public partial class FormExceptionSQL : DevExpress.XtraEditors.XtraForm
    {
        public FormExceptionSQL()
        {
            InitializeComponent();
        }

        private void FormException_Load(object sender, EventArgs e)
        {

        }

        public void Mostrar(String Mensagem, String Comando, Exception ex)
        {
            richTextBox1.Text = Mensagem;
            richTextBox2.Text = Comando;
            richTextBox3.Text = ex.Message;

            this.ShowDialog();
        }

        private void buttonFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}