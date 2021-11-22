using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Expressao
{
    public partial class FormExpressaoSelecao : Form
    {
        /// <summary>
        /// Usado para listar variáveis do fluxo em questão
        /// </summary>
        public String Fluxo { get; set; }

        public AppLib.Global.Types.Confirmacao confirmacao { get; set; }
        public AppLib.Global.Types.TipoExpressao tipoExpressao { get; set; }
        public String Result { get; set; }

        public FormExpressaoSelecao()
        {
            InitializeComponent();
        }

        private void FormExpressaoSelecao_Load(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.Cancelar;
        }

        private void simpleButtonOK_VALORFIXO_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.OK;
            tipoExpressao = Global.Types.TipoExpressao.ValorFixo;
            Result = campoTextoVALORFIXO.Get();
            this.Close();
        }

        private void simpleButtonOK_VARIAVEL_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.OK;
            tipoExpressao = Global.Types.TipoExpressao.Variavel;
            Result = campoLookupVARIAVEL.Get();
            this.Close();
        }

        private void simpleButtonOK_FLUXO_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.OK;
            tipoExpressao = Global.Types.TipoExpressao.Fluxo;
            Result = campoLookupFLUXO.Get();
            this.Close();
        }

        private void simpleButtonCANCELAR_VALORFIXO_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.Cancelar;
            this.Close();
        }

        private void simpleButtonCANCELAR_VARIAVEL_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.Cancelar;
            this.Close();
        }

        private void simpleButtonCANCELAR_FLUXO_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.Cancelar;
            this.Close();
        }

        private bool campoLookupVARIAVEL_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta = @"SELECT VARIAVEL, TIPOVARIAVEL, TIPODADO FROM ZFLUXOVARIAVEL WHERE FLUXO = ?";
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupVARIAVEL, consulta, new Object[] { Fluxo });
        }

        private void campoLookupVARIAVEL_SetDescricao(object sender, EventArgs e)
        {
            String consulta = @"SELECT TIPODADO FROM ZFLUXOVARIAVEL WHERE FLUXO = ? AND VARIAVEL = ?";
            AppLib.Windows.CampoLookup.Descricao(campoLookupVARIAVEL, consulta, new Object[] { campoLookupVARIAVEL.Get() });
        }

        private bool campoLookupFLUXO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta = @"
SELECT *
FROM ZFLUXO
WHERE NOME NOT IN ( SELECT FLUXO FROM ZFLUXOEXT WHERE ATIVO = 0 )";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupFLUXO, consulta, new Object[] { });
        }

        private void campoLookupVARIAVEL_AposSelecao(object sender, EventArgs e)
        {
            this.simpleButtonOK_VARIAVEL_Click(this, null);
        }

        private void campoLookupFLUXO_AposSelecao(object sender, EventArgs e)
        {
            this.simpleButtonOK_FLUXO_Click(this, null);
        }
    }
}
