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
    public partial class FormUsuarioCadastro : AppLib.Windows.FormCadastroData
    {
        public FormUsuarioCadastro()
        {
            InitializeComponent();
        }

        private void FormUsuarioCadastro_Load(object sender, EventArgs e)
        {
            
        }

        private void FormUsuarioCadastro_AntesSalvar(object sender, EventArgs e)
        {
            if (campoDATACRIACAO.Get() == null)
            {
                campoDATACRIACAO.Set(DateTime.Now);
                campoUSUARIOCRIACAO.Set(AppLib.Context.Usuario);
            }

            campoDATAALTERACAO.Set(DateTime.Now);
            campoUSUARIOALTERACAO.Set(AppLib.Context.Usuario);
        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            FormPerfilVisao f = new FormPerfilVisao();
            f.grid1.Conexao = this.Conexao;
            return f.MostrarLookup(campoLookup1);
        }

    }
}
