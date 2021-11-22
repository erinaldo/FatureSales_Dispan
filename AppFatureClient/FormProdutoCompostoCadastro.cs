using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormProdutoCompostoCadastro : AppLib.Windows.FormCadastroData
    {
        public int CodColigada;
        public int IdPrd;
        int produto;
        public FormProdutoCompostoCadastro()
        {
            InitializeComponent();
        }

        private bool campoLookupPRODUTO_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta = @"
SELECT TPRD.IDPRD, TPRD.CODIGOPRD, ISNULL(TPRD.NOMEFANTASIA, TPRD.DESCRICAO) NOME, TPRD.CODUNDVENDA
FROM TPRD  
WHERE TPRD.CODCOLIGADA = ?
  AND TPRD.ULTIMONIVEL = 1
  AND TPRD.INATIVO = 0";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupPRODUTO, consulta, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta = @"SELECT CODUND, DESCRICAO FROM TUND";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1, consulta, new Object[] { });
        }

        private void FormProdutoCompostoCadastro_AntesSalvar(object sender, EventArgs e)
        {

            if (!Convert.ToInt32(campoLookupPRODUTO.textBoxCODIGO.Text).Equals(produto))
            {
                //Excluir o produto composto
                AppLib.ORM.Jit TPRDCOMPOSTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "TPRDCOMPOSTO");
                TPRDCOMPOSTO.Set("IDPRD", campoTextoIDPRD.Get());
                TPRDCOMPOSTO.Set("IDPRDCOMPONENTE", produto);
                TPRDCOMPOSTO.Set("CODCOLIGADA", campoTexto14.Get());
                TPRDCOMPOSTO.Delete();
            }
        }

        private void FormProdutoCompostoCadastro_Load(object sender, EventArgs e)
        {
            if (this.Acao == AppLib.Global.Types.Acao.Editar)
            {
                produto = Convert.ToInt32(campoLookupPRODUTO.textBoxCODIGO.Text);
            }
            else
            {
                campoTextoIDPRD.Set(IdPrd.ToString());
                int retorno = Convert.ToInt32(AppLib.Context.poolConnection.Get().ExecGetField(null, "SELECT MAX(NUMEROSEQUENCIAL) FROM TPRDCOMPOSTO WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, IdPrd }));
                if (retorno.Equals(-1))
                {
                    campoInteiro1.Set(1);                
                }
                else
                {
                    campoInteiro1.Set(retorno + 1);                
                }
    
                campoDataHora3.Set(AppLib.Context.poolConnection.Get().GetDateTime());
                campoTexto14.Set(AppLib.Context.Empresa.ToString());
            }
        }

        private void campoLookupPRODUTO_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT ISNULL(TPRD.NOMEFANTASIA, TPRD.DESCRICAO) NOME FROM TPRD WHERE CODCOLIGADA = ? AND IDPRD = ?";
            campoLookupPRODUTO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupPRODUTO.Conexao).ExecGetField("", sql, new object[] {AppLib.Context.Empresa, campoLookupPRODUTO.Get() }).ToString();
        }

        
    }
}
