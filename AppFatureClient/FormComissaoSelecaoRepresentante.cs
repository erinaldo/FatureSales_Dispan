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
    public partial class FormComissaoSelecaoRepresentante : Form
    {
        public bool composto = false;
        public bool IPI = false;
        public string codrpr { get; private set; }

        public FormComissaoSelecaoRepresentante()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(campoLookupCODRPR.Get()))
            {
                codrpr = campoLookupCODRPR.Get();
                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            codrpr = null;
        }
    }
}
