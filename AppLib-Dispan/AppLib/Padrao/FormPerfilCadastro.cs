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
    public partial class FormPerfilCadastro : AppLib.Windows.FormCadastroData
    {
        public FormPerfilCadastro()
        {
            InitializeComponent();
        }

        private void FormPerfilCadastro_Load(object sender, EventArgs e)
        {
            
        }

        private void FormPerfilCadastro_AntesSalvar(object sender, EventArgs e)
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
            gridData1.Parametros = new Object[] { campoTextoCODPERFIL.Get() };
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            FormProcessoPerfilCadastro f = new FormProcessoPerfilCadastro();
            f.Conexao = this.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.campoLookup1.Conexao = f.Conexao;
            f.campoLookup2.Conexao = f.Conexao;
            f.Chave.Add(new ORM.CampoValor("CODPERFIL", campoTextoCODPERFIL.Get()));
            f.Novo();
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            FormProcessoPerfilCadastro f = new FormProcessoPerfilCadastro();
            f.Conexao = this.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.campoLookup1.Conexao = f.Conexao;
            f.campoLookup2.Conexao = f.Conexao;
            f.Chave.Add(new ORM.CampoValor("CODPERFIL", campoTextoCODPERFIL.Get()));
            f.Editar(gridData1.bs);
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            FormProcessoPerfilCadastro f = new FormProcessoPerfilCadastro();
            f.Conexao = this.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.campoLookup1.Conexao = f.Conexao;
            f.campoLookup2.Conexao = f.Conexao;
            f.Chave.Add(new ORM.CampoValor("CODPERFIL", campoTextoCODPERFIL.Get()));
            f.Excluir(gridData1.GetDataRows());
        }

        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            gridData2.Parametros = new Object[] { campoTextoCODPERFIL.Get() };
        }

        private void gridData2_Novo(object sender, EventArgs e)
        {
            FormMenuPerfilCadastro f = new FormMenuPerfilCadastro();
            f.Conexao = this.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.campoLookup1.Conexao = f.Conexao;
            f.campoLookup2.Conexao = f.Conexao;
            f.Chave.Add(new ORM.CampoValor("CODPERFIL", campoTextoCODPERFIL.Get()));
            f.Novo();
        }

        private void gridData2_Editar(object sender, EventArgs e)
        {
            FormMenuPerfilCadastro f = new FormMenuPerfilCadastro();
            f.Conexao = this.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.campoLookup1.Conexao = f.Conexao;
            f.campoLookup2.Conexao = f.Conexao;
            f.Chave.Add(new ORM.CampoValor("CODPERFIL", campoTextoCODPERFIL.Get()));
            f.Editar(gridData2.bs);
        }

        private void gridData2_Excluir(object sender, EventArgs e)
        {
            FormMenuPerfilCadastro f = new FormMenuPerfilCadastro();
            f.Conexao = this.Conexao;

            for (int i = 0; i < f.Querys.Length; i++)
            {
                f.Querys[i].Conexao = f.Conexao;
            }

            f.campoLookup1.Conexao = f.Conexao;
            f.campoLookup2.Conexao = f.Conexao;
            f.Chave.Add(new ORM.CampoValor("CODPERFIL", campoTextoCODPERFIL.Get()));
            f.Excluir(gridData2.GetDataRows());         
        }
    }
}
