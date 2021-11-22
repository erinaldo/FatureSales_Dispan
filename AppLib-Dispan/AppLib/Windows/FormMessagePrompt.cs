using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class FormMessagePrompt : DevExpress.XtraEditors.XtraForm
    {
        public AppLib.Global.Types.Confirmacao confirmacao { get; set; }

        public FormMessagePrompt()
        {
            InitializeComponent();
        }

        private void FormMessagePrompt_Load(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.Cancelar;
        }

        public String Mostrar(String Pergunta)
        {
            return this.Mostrar(Pergunta, null);
        }

        public String Mostrar(String Pergunta, String Valor)
        {
            richTextBox1.Text = Pergunta;

            if (Valor != null)
            {
                if (Valor != String.Empty)
                {
                    textBox1.Text = Valor;
                }
            }

            this.ShowDialog();

            if (textBox1.Text.Length > 0)
            {
                return textBox1.Text;
            }
            else
            {
                return null;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.OK;
            this.Close();
        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.Cancelar;
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(this, null);
            }
        }

    }
}
