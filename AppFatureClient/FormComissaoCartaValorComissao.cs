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
    public partial class FormComissaoCartaValorComissao : Form
    {
        public string porcentagem { get; private set; }

        public FormComissaoCartaValorComissao()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cpPorcentagem.textEdit1.Text))
            {
                porcentagem = cpPorcentagem.textEdit1.Text;
                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
