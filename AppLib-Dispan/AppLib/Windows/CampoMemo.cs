using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class CampoMemo : UserControl
    {
        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo")]
        public String Campo { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Valor padrão")]
        public String Default { get; set; }

        [Category("_APP"), Description("Número Máximo de Caracteres Permitido")]
        public int? MaximoCaracteres { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }

        public CampoMemo()
        {
            InitializeComponent();

            Query = 0;

            if (MaximoCaracteres != null)
            {
                richTextBox1.MaxLength = (int)MaximoCaracteres;
            }

            Edita = true;
        }

        private void CampoMemo_Load(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = !Edita;
        }

        public String Get()
        {
            if (richTextBox1.Text.Equals(""))
            {
                return String.Empty;
            }
            else
            {
                return richTextBox1.Text;
            }
        }

        public void Set(String valor)
        {
            richTextBox1.Text = valor;
        }

        private void CampoMemo_Resize(object sender, EventArgs e)
        {
            richTextBox1.Height = this.Height;
            richTextBox1.Width = this.Width;
        }

    }
}
