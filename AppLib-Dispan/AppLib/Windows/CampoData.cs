using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace AppLib.Windows
{
    public partial class CampoData : UserControl
    {
        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo")]
        public String Campo { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Valor padrão")]
        public DateTime? Default { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }

        public CampoData()
        {
            InitializeComponent();
            Query = 0;
            Edita = true;
        }

        private void CampoData_Load(object sender, EventArgs e)
        {
            maskedTextBox1.Enabled = Edita;
            dateTimePicker1.Enabled = Edita;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            maskedTextBox1.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private Boolean Validar()
        {
            try
            {
                DateTime temp = DateTime.Parse(maskedTextBox1.Text);
                return true;
            }
            catch { }

            return false;
        }

        public DateTime? Get()
        {
            if (maskedTextBox1.Text.Equals("  /  /"))
            {
                return null;
            }
            else
            {
                if (this.Validar())
                {
                    return DateTime.Parse(maskedTextBox1.Text);
                }
                else
                {
                    return null;
                }                
            }
        }

        public void Set(DateTime? valor)
        {
            if (valor == null)
            {
                maskedTextBox1.Text = "  /  /";
            }
            else
            {
                maskedTextBox1.Text = valor.ToString();
            }
        }

        private void CampoData_Resize(object sender, EventArgs e)
        {
            maskedTextBox1.Width = (this.Width -32 ) - 3;
            dateTimePicker1.Width = this.Width - 3;
        }

    }
}
