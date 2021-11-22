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
    public partial class FormRequisicaoPecasItemDevolucao : AppLib.Windows.FormCadastroObject
    {
        public Boolean EDIT { get; set; }
        public String COD { get; set; }
        public String DESC { get; set; }
        public String UNIDADE { get; set; }
        public String QUANTIDADE { get; set; }
        public String OBS { get; set; }
        public String OBSDEV { get; set; }

        public FormRequisicaoPecasItemDevolucao()
        {
            InitializeComponent();
        }

        private void FormOrcamentoItem_Load(object sender, EventArgs e)
        {
            if(EDIT)
            {
                campoLookupPRODUTO.textBoxCODIGO.Text = COD;
                campoLookupPRODUTO.textBoxDESCRICAO.Text = DESC;
                campoTextoCODUND.Set(UNIDADE);
                campoDecimalQUANTIDADE.textEdit1.Text = QUANTIDADE;
                txtObservacaoIda.Set(OBS);
                txtObservacaoDevolucao.Set(OBSDEV);

                campoLookupPRODUTO.Enabled = false;
                campoTextoCODUND.Enabled = false;
                campoDecimalQUANTIDADE.Enabled = false;
                txtObservacaoIda.Enabled = false;

                txtObservacaoDevolucao.Enabled = true;
            }
            else
            {
                txtObservacaoDevolucao.Enabled = false;
            }
        }        

        #region EVENTOS DO FORM

        private bool FormOrcamentoItem_Validar(object sender, EventArgs e)
        {
            if (campoLookupPRODUTO.Get() == string.Empty)
            {
                MessageBox.Show("Campo Produto obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoTextoCODUND.Get() == string.Empty)
            {
                MessageBox.Show("Campo Unidade obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoDecimalQUANTIDADE.Get() <= 0)
            {
                MessageBox.Show("Campo Quantidade deve ser maior que zero.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public Boolean ProdutoTemComposicao()
        {
            // criar a query na TPRDCOMPOSTO
            return true;
        }

        private void FormOrcamentoItem_Preparar(object sender, EventArgs e)
        {
            if (ProdutoTemComposicao())
            {
                // inserir a composição
            }
            else
            {
                // inserir o próprio produto
            }
        }

        private Boolean FormOrcamentoItem_Salvar2(object sender, EventArgs e)
        {
            if(EDIT)
            {
                OBSDEV = txtObservacaoDevolucao.Get();
                return true;
            }
            else
            {


                COD = campoLookupPRODUTO.textBoxCODIGO.Text;
                DESC = campoLookupPRODUTO.textBoxDESCRICAO.Text;
                UNIDADE = campoTextoCODUND.Get();
                QUANTIDADE = campoDecimalQUANTIDADE.textEdit1.Text;
                OBS = txtObservacaoIda.Get();
                OBSDEV = txtObservacaoDevolucao.Get();
                return true;
            }
            
        }

        private void FormOrcamentoItem_Excluir2(object sender, EventArgs e)
        {
            
        }
        
        #endregion

        #region EVENTOS DOS COMPONENTES

        private bool campoLookupPRODUTO_SetFormConsulta(object sender, EventArgs e)
        {
//            String consulta = @"
//SELECT TPRD.IDPRD, TPRD.CODIGOPRD, ISNULL(TPRD.NOMEFANTASIA, TPRD.DESCRICAO) NOME, TPRD.CODUNDVENDA, 
//ISNULL(TTRBPRD.ALIQUOTA, 0) ALIQUOTA
//FROM TPRD
//LEFT JOIN TTRBPRD ON TPRD.CODCOLIGADA = TTRBPRD.CODCOLIGADA 
//  AND TPRD.IDPRD = TTRBPRD.IDPRD
//  AND TTRBPRD.CODTRB = 'IPI' 
//  
//WHERE TPRD.CODCOLIGADA = ?
//  AND TPRD.ULTIMONIVEL = 1
//  AND TPRD.INATIVO = 0";

//            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupPRODUTO, consulta, new Object[] { AppLib.Context.Empresa });

            FormProdutoVisao f = new FormProdutoVisao();
            f.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            return f.MostrarLookup(campoLookupPRODUTO, " AND ULTIMONIVEL = 1 AND INATIVO = 0");
        }

        private void campoLookupPRODUTO_AposSelecao(object sender, EventArgs e)
        {
            string consulta1 = @"SELECT TPRD.CODUNDVENDA, ISNULL(TTRBPRD.ALIQUOTA, 0) ALIQUOTA
                                FROM TPRD
                                LEFT JOIN TTRBPRD ON TPRD.CODCOLIGADA = TTRBPRD.CODCOLIGADA 
                                  AND TPRD.IDPRD = TTRBPRD.IDPRD
                                  AND TTRBPRD.CODTRB = 'IPI' WHERE TPRD.CODCOLIGADA = ? AND TPRD.CODIGOPRD = ?";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(consulta1, new Object[] { AppLib.Context.Empresa, campoLookupPRODUTO.Get() });

            if (dt.Rows.Count > 0)
            {
                System.Data.DataRow dr = dt.Rows[0];

                campoTextoCODUND.Set(dr["CODUNDVENDA"].ToString());
            }
            else
            {
                MessageBox.Show("Erro ao obter dados do produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion        

        #region EVENTOS DE MANIPULAÇÃO DOS DADOS

        /// <summary>
        /// CARREGA OS DADOS DO FORM PARA O OBJETO
        /// </summary>
        public void AtualizarObjeto()
        {
            
        }

        /// <summary>
        /// CARREGA OS DADOS DO OBJETO PARA O FORM
        /// </summary>
        public void AtualizarForm()
        {
           
        }
        
        #endregion

        private void campoLookupPRODUTO_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM ZVWTPRD WHERE CODCOLIGADA = ? AND CODIGOPRD = ?";
            campoLookupPRODUTO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupPRODUTO.Conexao).ExecGetField("", sql, new object[] {AppLib.Context.Empresa, campoLookupPRODUTO.Get() }).ToString();
        }
    }
}
