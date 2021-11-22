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
    public partial class FormTipoCobranca : Form
    {
        public string codTipoCobrana = string.Empty;
        
        public FormTipoCobranca()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(campoLookup1.textBoxDESCRICAO.Text))
            {
                codTipoCobrana = campoLookup1.textBoxCODIGO.Text;
            }
            else
            {
                MessageBox.Show("Favor selecionar um tipo de cobrança.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Dispose();
        }
    }
}
