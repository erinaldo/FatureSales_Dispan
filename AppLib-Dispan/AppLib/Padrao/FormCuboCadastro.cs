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
    public partial class FormCuboCadastro : AppLib.Windows.FormCadastroData
    {
        public FormCuboCadastro()
        {
            InitializeComponent();
        }

        private void FormCuboCadastro_Load(object sender, EventArgs e)
        {

        }

        private void FormCuboCadastro_AntesSalvar(object sender, EventArgs e)
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
            gridData1.Parametros = new Object[] { campoInteiroIDCUBO.Get() };
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            FormCuboParamCadastro f = new FormCuboParamCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Novo();
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            FormCuboParamCadastro f = new FormCuboParamCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Editar(gridData1);
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            FormCuboParamCadastro f = new FormCuboParamCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Excluir(gridData1);
        }

        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            gridData2.Parametros = new Object[] { campoInteiroIDCUBO.Get() };
        }

        private void gridData2_Novo(object sender, EventArgs e)
        {
            FormCuboSQLParCadastro f = new FormCuboSQLParCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Novo();
        }

        private void gridData2_Editar(object sender, EventArgs e)
        {
            FormCuboSQLParCadastro f = new FormCuboSQLParCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Editar(gridData2);
        }

        private void gridData2_Excluir(object sender, EventArgs e)
        {
            FormCuboSQLParCadastro f = new FormCuboSQLParCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Excluir(gridData2);
        }

        private void gridData3_SetParametros(object sender, EventArgs e)
        {
            gridData3.Parametros = new Object[] { campoInteiroIDCUBO.Get() };
        }

        private void gridData3_Novo(object sender, EventArgs e)
        {
            FormCuboSQLProcCadastro f = new FormCuboSQLProcCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Novo();
        }

        private void gridData3_Editar(object sender, EventArgs e)
        {
            FormCuboSQLProcCadastro f = new FormCuboSQLProcCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Editar(gridData3);
        }

        private void gridData3_Excluir(object sender, EventArgs e)
        {
            FormCuboSQLProcCadastro f = new FormCuboSQLProcCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Excluir(gridData3);
        }

        private void gridData4_SetParametros(object sender, EventArgs e)
        {
            gridData4.Parametros = new Object[] { campoInteiroIDCUBO.Get() };
        }

        private void gridData4_Novo(object sender, EventArgs e)
        {
            FormCuboPerfilCadastro f = new FormCuboPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Novo();
        }

        private void gridData4_Editar(object sender, EventArgs e)
        {
            FormCuboPerfilCadastro f = new FormCuboPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Editar(gridData4);
        }

        private void gridData4_Excluir(object sender, EventArgs e)
        {
            FormCuboPerfilCadastro f = new FormCuboPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDCUBO", campoInteiroIDCUBO.Get()));
            f.Excluir(gridData4);
        }
    }
}
