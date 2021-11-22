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
    public partial class frmMensagemDesbloqueio : Form
    {
        public string Motivo = string.Empty;

        public frmMensagemDesbloqueio()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbMotivo.Text))
            {
                MessageBox.Show("Favor informar o motivo do desbloqueio.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Motivo = tbMotivo.Text;
            this.Dispose();
        }
    }
}
