using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormFluxoCadastro : AppLib.Windows.FormCadastroData
    {
        public FormFluxoCadastro()
        {
            InitializeComponent();
        }

        private void FormFluxoCadastro_Load(object sender, EventArgs e)
        {
            
        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            gridData1.Parametros = new Object[] { campoTextoFLUXO.Get() };
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            FormFluxoCadastroVariavel f = new FormFluxoCadastroVariavel();
            f.Chave.Add(new ORM.CampoValor("FLUXO", campoTextoFLUXO.Get()));
            f.Chave.Add(new ORM.CampoValor("TIPOVARIAVEL", "Parâmetro"));
            f.Novo();
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            FormFluxoCadastroVariavel f = new FormFluxoCadastroVariavel();
            f.Chave.Add(new ORM.CampoValor("FLUXO", campoTextoFLUXO.Get()));
            f.Chave.Add(new ORM.CampoValor("TIPOVARIAVEL", "Parâmetro"));
            f.Editar(gridData1);
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            FormFluxoCadastroVariavel f = new FormFluxoCadastroVariavel();
            f.Chave.Add(new ORM.CampoValor("FLUXO", campoTextoFLUXO.Get()));
            f.Chave.Add(new ORM.CampoValor("TIPOVARIAVEL", "Parâmetro"));
            f.Excluir(gridData1);
        }

        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            gridData2.Parametros = new Object[] { campoTextoFLUXO.Get() };
        }

        private void gridData2_Novo(object sender, EventArgs e)
        {
            FormFluxoCadastroVariavel f = new FormFluxoCadastroVariavel();
            f.Chave.Add(new ORM.CampoValor("FLUXO", campoTextoFLUXO.Get()));
            f.Chave.Add(new ORM.CampoValor("TIPOVARIAVEL", "Variável"));
            f.Novo();
        }

        private void gridData2_Editar(object sender, EventArgs e)
        {
            FormFluxoCadastroVariavel f = new FormFluxoCadastroVariavel();
            f.Chave.Add(new ORM.CampoValor("FLUXO", campoTextoFLUXO.Get()));
            f.Chave.Add(new ORM.CampoValor("TIPOVARIAVEL", "Variável"));
            f.Editar(gridData2);
        }

        private void gridData2_Excluir(object sender, EventArgs e)
        {
            FormFluxoCadastroVariavel f = new FormFluxoCadastroVariavel();
            f.Chave.Add(new ORM.CampoValor("FLUXO", campoTextoFLUXO.Get()));
            f.Chave.Add(new ORM.CampoValor("TIPOVARIAVEL", "Variável"));
            f.Excluir(gridData2);
        }

        private void FormFluxoCadastro_AntesSalvar(object sender, EventArgs e)
        {
            campoTextoZFLUXOEXT_FLUXO.Set(campoTextoFLUXO.Get());
        }

        private void FormFluxoCadastro_AntesExcluir(object sender, EventArgs e)
        {
            AppLib.Context.poolConnection.Get().ExecTransaction("DELETE ZFLUXOSUBFLUXO WHERE FLUXO = ?", new Object[] { campoTextoFLUXO.Get() });
            AppLib.Context.poolConnection.Get().ExecTransaction("DELETE ZFLUXOFIGURA WHERE FLUXO = ?", new Object[] { campoTextoFLUXO.Get() });
            AppLib.Context.poolConnection.Get().ExecTransaction("DELETE ZFLUXOVARIAVEL WHERE FLUXO = ?", new Object[] { campoTextoFLUXO.Get() });
        }
    }
}
