using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormSelecaoDataBase : Form
    {
        public string selecao = string.Empty;

        public FormSelecaoDataBase()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbEmissao.Checked == true)
            {
                selecao = "Emissão";
            }
            else if (rbEntrega.Checked == true)
            {
                selecao = "Entrega";
            }
            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
