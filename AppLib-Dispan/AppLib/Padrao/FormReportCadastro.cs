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
    public partial class FormReportCadastro : AppLib.Windows.FormCadastroData
    {
        public FormReportCadastro()
        {
            InitializeComponent();
        }

        private void FormReportCadastro_Load(object sender, EventArgs e)
        {

        }

        private void FormReportCadastro_AntesSalvar(object sender, EventArgs e)
        {
            DateTime AGORA = AppLib.Context.poolConnection.Get(this.Conexao).GetDateTime();

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
            gridData1.Parametros = new Object[] { campoInteiroIDREPORT.Get() };
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            FormReportPerfilCadastro f = new FormReportPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDREPORT", campoInteiroIDREPORT.Get()));
            f.Conexao = this.Conexao;
            f.Querys[0].Conexao = f.Conexao;
            f.campoLookupIDREPORT.Conexao = f.Conexao;
            f.campoLookupCODPERFIL.Conexao = f.Conexao;
            f.Novo();
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            FormReportPerfilCadastro f = new FormReportPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDREPORT", campoInteiroIDREPORT.Get()));
            f.Conexao = this.Conexao;
            f.Querys[0].Conexao = f.Conexao;
            f.campoLookupIDREPORT.Conexao = f.Conexao;
            f.campoLookupCODPERFIL.Conexao = f.Conexao;
            f.Editar(gridData1.bs);
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            FormReportPerfilCadastro f = new FormReportPerfilCadastro();
            f.Chave.Add(new ORM.CampoValor("IDREPORT", campoInteiroIDREPORT.Get()));
            f.Conexao = this.Conexao;
            f.Querys[0].Conexao = f.Conexao;
            f.campoLookupIDREPORT.Conexao = f.Conexao;
            f.campoLookupCODPERFIL.Conexao = f.Conexao;
            f.Excluir(gridData1.GetDataRows());
        }
    }
}
