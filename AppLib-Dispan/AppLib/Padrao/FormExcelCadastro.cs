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
    public partial class FormExcelCadastro : AppLib.Windows.FormCadastroData
    {
        public FormExcelCadastro()
        {
            InitializeComponent();
        }

        private void FormExcelCadastro_Load(object sender, EventArgs e)
        {

        }

        private void FormExcelCadastro_AntesSalvar(object sender, EventArgs e)
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
            gridData1.Parametros = new Object[] { campoInteiroIDPLANILHA.Get() };
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            this.dropDownButtonSalvar_Click(this, null);
            FormExcelParamCadastro f = new FormExcelParamCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHA", campoInteiroIDPLANILHA.Get()));
            f.Novo();
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            FormExcelParamCadastro f = new FormExcelParamCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHA", campoInteiroIDPLANILHA.Get()));
            f.Editar(gridData1);
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            FormExcelParamCadastro f = new FormExcelParamCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHA", campoInteiroIDPLANILHA.Get()));
            f.Excluir(gridData1);
        }

        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            gridData2.Parametros = new Object[] { campoInteiroIDPLANILHA.Get() };
        }

        private void gridData2_Novo(object sender, EventArgs e)
        {
            this.dropDownButtonSalvar_Click(this, null);
            FormExcelSQLCadastro f = new FormExcelSQLCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHA", campoInteiroIDPLANILHA.Get()));
            f.Novo();
        }

        private void gridData2_Editar(object sender, EventArgs e)
        {
            FormExcelSQLCadastro f = new FormExcelSQLCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHA", campoInteiroIDPLANILHA.Get()));
            f.Editar(gridData2);
        }

        private void gridData2_Excluir(object sender, EventArgs e)
        {
            FormExcelSQLCadastro f = new FormExcelSQLCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHA", campoInteiroIDPLANILHA.Get()));
            f.Excluir(gridData2);
        }

        private void gridData3_SetParametros(object sender, EventArgs e)
        {
            gridData3.Parametros = new Object[] { campoInteiroIDPLANILHA.Get() };
        }

        private void gridData3_Novo(object sender, EventArgs e)
        {
            this.dropDownButtonSalvar_Click(this, null);
            FormExcelPerfilCadastro f = new FormExcelPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHA", campoInteiroIDPLANILHA.Get()));
            f.Novo();
        }

        private void gridData3_Editar(object sender, EventArgs e)
        {
            FormExcelPerfilCadastro f = new FormExcelPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHA", campoInteiroIDPLANILHA.Get()));
            f.Editar(gridData3);
        }

        private void gridData3_Excluir(object sender, EventArgs e)
        {
            FormExcelPerfilCadastro f = new FormExcelPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDPLANILHA", campoInteiroIDPLANILHA.Get()));
            f.Excluir(gridData3);
        }
    }
}
