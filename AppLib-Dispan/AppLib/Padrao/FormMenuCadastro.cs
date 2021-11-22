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
    public partial class FormMenuCadastro : AppLib.Windows.FormCadastroData
    {
        public FormMenuCadastro()
        {
            InitializeComponent();
        }

        private void FormMenuCadastro_Load(object sender, EventArgs e)
        {

        }

        private void FormMenuCadastro_AntesSalvar(object sender, EventArgs e)
        {
            if (campoDATACRIACAO.Get() == null)
            {
                campoDATACRIACAO.Set(DateTime.Now);
                campoUSUARIOCRIACAO.Set(AppLib.Context.Usuario);
            }

            campoDATAALTERACAO.Set(DateTime.Now);
            campoUSUARIOALTERACAO.Set(AppLib.Context.Usuario);
        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            gridData1.Parametros = new Object[] { campoTexto1.Get() };
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            FormMenuFluxoVisaoCadastro f = new FormMenuFluxoVisaoCadastro();
            f.Chave.Add(new ORM.CampoValor("CODMENU", campoTexto1.Get()));
            f.Novo();
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            FormMenuFluxoVisaoCadastro f = new FormMenuFluxoVisaoCadastro();
            f.Chave.Add(new ORM.CampoValor("CODMENU", campoTexto1.Get()));
            f.Editar(gridData1);
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            FormMenuFluxoVisaoCadastro f = new FormMenuFluxoVisaoCadastro();
            f.Chave.Add(new ORM.CampoValor("CODMENU", campoTexto1.Get()));
            f.Excluir(gridData1);
        }


    }
}
