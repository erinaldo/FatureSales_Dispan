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
    public partial class FormDashboardPerfilCadastro : AppLib.Windows.FormCadastroData
    {
        public FormDashboardPerfilCadastro()
        {
            InitializeComponent();
        }

        private void FormDashboardPerfilCadastro_Load(object sender, EventArgs e)
        {

        }

        private void FormDashboardPerfilCadastro_AntesSalvar(object sender, EventArgs e)
        {
            DateTime AGORA = AppLib.Context.poolConnection.Get().GetDateTime();

            if (Acao == Global.Types.Acao.Novo)
            {
                campoDATACRIACAO.Set(AGORA);
                campoUSUARIOCRIACAO.Set(AppLib.Context.Usuario);
            }

            campoDATAALTERACAO.Set(AGORA);
            campoUSUARIOALTERACAO.Set(AppLib.Context.Usuario);
        }
    }
}
