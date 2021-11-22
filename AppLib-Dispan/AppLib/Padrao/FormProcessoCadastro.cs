using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Padrao
{
    public partial class FormProcessoCadastro : AppLib.Windows.FormCadastroData
    {
        public FormProcessoCadastro()
        {
            InitializeComponent();
        }

        private void FormProcessoCadastro_Load(object sender, EventArgs e)
        {

        }

        private void FormProcessoCadastro_AntesSalvar(object sender, EventArgs e)
        {
            if (campoDATACRIACAO.Get() == null)
            {
                campoDATACRIACAO.Set(DateTime.Now);
                campoUSUARIOCRIACAO.Set(AppLib.Context.Usuario);
            }

            campoDATAALTERACAO.Set(DateTime.Now);
            campoUSUARIOALTERACAO.Set(AppLib.Context.Usuario);
        }
    }
}
