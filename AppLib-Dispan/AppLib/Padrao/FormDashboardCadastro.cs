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
    public partial class FormDashboardCadastro : AppLib.Windows.FormCadastroData
    {
        public FormDashboardCadastro()
        {
            InitializeComponent();
        }

        private void FormDashboardCadastro_Load(object sender, EventArgs e)
        {

        }

        private void FormDashboardCadastro_AntesSalvar(object sender, EventArgs e)
        {
            DateTime AGORA = AppLib.Context.poolConnection.Get().GetDateTime();

            if (Acao == Global.Types.Acao.Novo)
            {
                campoDATACRIACAO.Set(AGORA);
                campoTextoUSUARIOCRIACAO.Set(AppLib.Context.Usuario);
            }

            campoDATAALTERACAO.Set(AGORA);
            campoTextoUSUARIOALTERACAO.Set(AppLib.Context.Usuario);
        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            gridData1.Parametros = new Object[] { campoInteiroIDDASHBOARD.Get() };
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            FormDashboardPerfilCadastro f = new FormDashboardPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDDASHBOARD", campoInteiroIDDASHBOARD.Get()));
            f.Novo();
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            FormDashboardPerfilCadastro f = new FormDashboardPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDDASHBOARD", campoInteiroIDDASHBOARD.Get()));
            f.Editar(gridData1);
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            FormDashboardPerfilCadastro f = new FormDashboardPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDDASHBOARD", campoInteiroIDDASHBOARD.Get()));
            f.Excluir(gridData1);
        }
    }
}
