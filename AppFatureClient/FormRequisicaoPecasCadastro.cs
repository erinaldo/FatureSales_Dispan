using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFatureClient.Classes;

namespace AppFatureClient
{
    public partial class FormRequisicaoPecasCadastro : AppLib.Windows.FormCadastroObject
    {
        // João Pedro Luchiari - 03/01/2018

        public DataRow row;

        public Boolean CopiaDeMovimento = false;
        private List<TITMMOV> Itens = new List<TITMMOV>();
        private Boolean ExcluiuTudo = false;
        bool EditarReg;

        List<ItemReq> it = new List<ItemReq>();
        DataTable dt;

        public FormRequisicaoPecasCadastro()
        {
            InitializeComponent();

            //MetodosSQL.CS = AppLib.Context.poolConnection.Get().ConnectionString;

            this.grid21.GetProcessos().Add("Expandir todos itens", null, ExpandirTodosItens);
            this.grid21.GetProcessos().Add("Aplicar tabela de preço", null, AplicarTabelaPreco);

            this.gridItemDevolucao.GetProcessos().Add("Baixar produto", null, BaixarProduto);
        }

        private void FormOrcamentoCadastro_Load(object sender, EventArgs e)
        {
            grid21_Atualizar(this, null);

            if (campoDataEMISSAO.Get() == null)
                campoDataEMISSAO.Set(DateTime.Now);

            if (campoListaFRETECIFOUFOB.Get() == null)
                campoListaFRETECIFOUFOB.Set("2");

            //if (campoLookupCODCPG.Get() == null)
            //campoLookupCODCPG.Set("F001");

            if (CopiaDeMovimento)
            {
                campoInteiroIDMOV.Set(null);
            }
        }

        private void campoInteiroPRAZOENTREGA_Leave(object sender, EventArgs e)
        {
            int prazodias = Convert.ToInt32(campoInteiroPRAZOENTREGA.Get());
            campoDataENTREGA.Set(Convert.ToDateTime(campoDataEMISSAO.Get()).AddDays(prazodias));
        }

        private void campoDataENTREGA_Leave(object sender, EventArgs e)
        {
            DateTime Emissao, Entrega;

            if (campoDataEMISSAO.Get() == null)
                return;
            else
                Emissao = (DateTime)campoDataEMISSAO.Get();


            if (campoDataENTREGA.Get() == null)
                return;
            else
                Entrega = (DateTime)campoDataENTREGA.Get();

            campoInteiroPRAZOENTREGA.Set((Entrega - Emissao).Days);
        }

        private void campoDataEMISSAO_Leave(object sender, EventArgs e)
        {
            DateTime Emissao, Entrega;

            if (campoDataEMISSAO.Get() == null)
                return;
            else
                Emissao = (DateTime)campoDataEMISSAO.Get();


            if (campoDataENTREGA.Get() == null)
                return;
            else
                Entrega = (DateTime)campoDataENTREGA.Get();

            campoInteiroPRAZOENTREGA.Set((Entrega - Emissao).Days);
        }

        #region CAMPOS LOOKUP

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            //return new FormClienteVisao().MostrarLookup(campoLookupCODCFO);

            String consulta1 = @"
SELECT CODCFO, ISNULL(NOMEFANTASIA, NOME) NOMEFANTASIA, CGCCFO, CODETD, CIDADE
FROM FCFO
WHERE CODCOLIGADA IN (0, ?)
  AND PAGREC IN (1, 3)
  AND ATIVO = 1";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCFO, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup3_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"
SELECT CODRPR, ISNULL(NOMEFANTASIA, NOME) NOMEFANTASIA, SIGLA, CGC, CODETD, CIDADE, BAIRRO
FROM TRPR
WHERE CODCOLIGADA = ?
  AND INATIVO = 0";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODRPR, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup4_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"
SELECT CODTRA, NOME, CGC, CODETD, CIDADE, BAIRRO
FROM TTRA
WHERE CODCOLIGADA = ?
  AND INATIVO = 0";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODTRA, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookupTPCULTURA_SetFormConsulta(object sender, EventArgs e)
        {
            String Consulta = @"
SELECT CODCLIENTE, DESCRICAO
FROM GCONSIST
WHERE CODCOLIGADA = ?
  AND APLICACAO = 'T'
  AND CODTABELA = 'CLASSREQ' and CODCLIENTE like '__'";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCLASSREQ, Consulta, new Object[] { AppLib.Context.Empresa });
        }





        private void campoLookupTPCULTURA_SetDescricao(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT DESCRICAO FROM GCONSIST WHERE CODCOLIGADA = ? AND APLICACAO = 'T' AND CODTABELA = 'TPCULTURA' AND CODCLIENTE = ?";
            campoLookupCLASSREQ.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta1, new Object[] { AppLib.Context.Empresa, campoLookupCLASSREQ.textBoxCODIGO.Text }).ToString();
        }

        #endregion

        #region MÉTODOS DA GRID

        private void grid21_Novo(object sender, EventArgs e)
        {
            FormRequisicaoPecasItem f = new FormRequisicaoPecasItem();
            //f.Itens = this.Itens;
            f.ShowDialog();

            if (f.acao == AcaoForcada.Salvar)
            {
                Itens.Add(f.xTITMMOV);
                this.grid21_Atualizar(this, null);
            }

            if (f.acao == AcaoForcada.Excluir)
            {
                // ignorar o registro novo
            }
        }

        private void grid21_Editar(object sender, EventArgs e)
        {
            int indice = 0;

            FormRequisicaoPecasItem f = new FormRequisicaoPecasItem();
            System.Data.DataRow dr = grid21.GetDataRow();
            TITMMOV reg = (TITMMOV)grid21.GetObjectRow();
            indice = Itens.IndexOf(reg);

            f.xTITMMOV = reg;
            f.AtualizarForm();
            f.ShowDialog();

            if (f.acao == AcaoForcada.Salvar)
            {
                Itens.RemoveAt(indice);
                Itens.Insert(indice, f.xTITMMOV);

                //Itens.Remove(reg);
                //Itens.Add(f.xTITMMOV);
                grid21_Atualizar(this, null);
            }

            if (f.acao == AcaoForcada.Excluir)
            {
                Itens.Remove(reg);

                if (Itens.Count == 0)
                {
                    ExcluiuTudo = true;
                }

                grid21_Atualizar(this, null);
            }
        }

        private void grid21_Excluir(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir registro selecionado ?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            List<Object> lista = grid21.GetObjectRows();

            for (int i = 0; i < lista.Count; i++)
            {
                TITMMOV temp = (TITMMOV)lista[i];
                Itens.Remove(temp);
            }

            if (Itens.Count == 0)
            {
                ExcluiuTudo = true;
            }

            grid21_Atualizar(this, null);
        }

        private void grid21_Atualizar(object sender, EventArgs e)
        {
            if (!ExcluiuTudo)
            {
                // CARREGAR OS ITENS
                if (grid21.gridControl1.Views[0].DataRowCount == 0)
                {
                    String Comando = "";

                    try
                    {
                        /*
                        Comando = @"
SELECT
TITMMOV.NSEQITMMOV,
TITMMOV.IDPRD,
TITMMOV.IDPRDCOMPOSTO,
TPRD.CODIGOPRD,
ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
TITMMOV.CODUND,
TITMMOV.QUANTIDADEORIGINAL QUANTIDADE,
TITMMOV.PRECOUNITARIO,
TPRD.NUMEROCCF,

CASE 
WHEN TITMMOVCOMPL.PRODPRICIPAL = 'NÃO' THEN 0
WHEN TITMMOVCOMPL.PRODPRICIPAL = 'SIM' THEN 1
ELSE NULL
END PRODPRICIPAL,

TITMMOVCOMPL.PRODPRICIPAL PRODUTO_COMPOSTO,

CASE 
WHEN TITMMOVCOMPL.TIPOMAT = 0 THEN 'SIM'
WHEN TITMMOVCOMPL.TIPOMAT = 1 THEN 'NÃO'
ELSE NULL
END MATERIAL_INTERLIGACAO,

TITMMOVCOMPL.SEQUENCIAL,
TITMMOVCOMPL.TIPOMAT,
TITMMOVCOMPL.COMPRIMENTO,
TITMMOVCOMPL.QTDPAREDECOMP,
TITMMOVCOMPL.LARGURA,
TITMMOVCOMPL.QTDPAREDELARG,
TITMMOVCOMPL.ALTURA,
TTRBMOV.ALIQUOTA,
TITMMOVHISTORICO.HISTORICOLONGO

FROM TITMMOV
LEFT JOIN TTRBMOV ON TITMMOV.CODCOLIGADA = TTRBMOV.CODCOLIGADA 
  AND TITMMOV.IDMOV = TTRBMOV.IDMOV
  AND TITMMOV.NSEQITMMOV = TTRBMOV.NSEQITMMOV
  AND TTRBMOV.CODTRB = 'IPI'
LEFT JOIN TITMMOVCOMPL ON TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA
  AND TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV
  AND TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV
LEFT JOIN TITMMOVHISTORICO ON TITMMOV.CODCOLIGADA = TITMMOVHISTORICO.CODCOLIGADA
  AND TITMMOV.IDMOV = TITMMOVHISTORICO.IDMOV
  AND TITMMOV.NSEQITMMOV = TITMMOVHISTORICO.NSEQITMMOV,
  TPRD

WHERE TITMMOV.CODCOLIGADA = TPRD.CODCOLIGADA
  AND TITMMOV.IDPRD = TPRD.IDPRD
  AND TITMMOV.CODCOLIGADA = ?
  AND TITMMOV.IDMOV = ?";
*/
                        Comando = @"
SELECT
TITMMOV.NSEQITMMOV,
TITMMOV.IDPRD,
TITMMOV.IDPRDCOMPOSTO,
TPRD.CODIGOPRD,
ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
TITMMOV.CODUND,
CAST(TITMMOV.QUANTIDADETOTAL AS NUMERIC(15,2)) QUANTIDADE,
CAST(TITMMOV.PRECOUNITARIO AS NUMERIC(15,2)) PRECOUNITARIO,
TPRD.NUMEROCCF,

CASE 
WHEN TITMMOVCOMPL.PRODPRICIPAL = 'NÃO' THEN 0
WHEN TITMMOVCOMPL.PRODPRICIPAL = 'SIM' THEN 1
ELSE NULL
END PRODPRICIPAL,

TITMMOVCOMPL.PRODPRICIPAL PRODUTO_COMPOSTO,

CASE 
WHEN TITMMOVCOMPL.TIPOMAT = 0 THEN 'SIM'
WHEN TITMMOVCOMPL.TIPOMAT = 1 THEN 'NÃO'
ELSE NULL
END MATERIAL_INTERLIGACAO,

TITMMOVCOMPL.SEQUENCIAL,
TITMMOVCOMPL.TIPOMAT,
TITMMOVCOMPL.COMPRIMENTO,
TITMMOVCOMPL.QTDPAREDECOMP,
TITMMOVCOMPL.LARGURA,
TITMMOVCOMPL.QTDPAREDELARG,
TITMMOVCOMPL.ALTURA,
CAST(ISNULL(TTRBMOV.ALIQUOTA,0) AS NUMERIC(15,2)) ALIQUOTA,
TITMMOVHISTORICO.HISTORICOLONGO

FROM TITMMOV
LEFT JOIN TTRBMOV ON TITMMOV.CODCOLIGADA = TTRBMOV.CODCOLIGADA 
  AND TITMMOV.IDMOV = TTRBMOV.IDMOV
  AND TITMMOV.NSEQITMMOV = TTRBMOV.NSEQITMMOV
  AND TTRBMOV.CODTRB = 'IPI'
LEFT JOIN TITMMOVCOMPL ON TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA
  AND TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV
  AND TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV
LEFT JOIN TITMMOVHISTORICO ON TITMMOV.CODCOLIGADA = TITMMOVHISTORICO.CODCOLIGADA
  AND TITMMOV.IDMOV = TITMMOVHISTORICO.IDMOV
  AND TITMMOV.NSEQITMMOV = TITMMOVHISTORICO.NSEQITMMOV,
  TPRD

WHERE TITMMOV.CODCOLIGADA = TPRD.CODCOLIGADA
  AND TITMMOV.IDPRD = TPRD.IDPRD
  AND TITMMOV.CODCOLIGADA = ?
  AND TITMMOV.IDMOV = ?";

                        Comando = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(Comando, new Object[] { AppLib.Context.Empresa, campoInteiroIDMOV.Get() });
                        System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Comando, new Object[] { });

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TITMMOV titmmov = new TITMMOV();

                            titmmov.IDPRD = int.Parse(dt.Rows[i]["IDPRD"].ToString());
                            

                            titmmov.CODIGOPRD = dt.Rows[i]["CODIGOPRD"].ToString();
                            titmmov.PRODUTO = dt.Rows[i]["PRODUTO"].ToString();
                            titmmov.UNIDADE = dt.Rows[i]["CODUND"].ToString();
                            titmmov.QUANTIDADE = decimal.Parse(dt.Rows[i]["QUANTIDADE"].ToString());
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[i]["PRECOUNITARIO"].ToString());
                            titmmov.VALORTOTAL = Convert.ToDecimal(string.Format("{0:n2}", titmmov.QUANTIDADE * titmmov.PRECOUNITARIO));
                            titmmov.ALIQUOTAIPI = decimal.Parse(dt.Rows[i]["ALIQUOTA"].ToString());
                            titmmov.HISTORICOLONGO = dt.Rows[i]["HISTORICOLONGO"].ToString();

                            titmmov.NUMEROCCF = dt.Rows[i]["NUMEROCCF"].ToString();
                            

                            

                            
                            

                            Itens.Add(titmmov);
                        }
                    }
                    catch (Exception ex)
                    {
                        new AppLib.Windows.FormExceptionSQL().Mostrar("Erro ao atualizar a grid. Processo de carregar itens do movimento para a grid de edição.", Comando, ex);
                    }
                }
            }

            grid21.gridControl1.DataSource = null;
            grid21.gridControl1.DataSource = Itens;

            // TRATAMENTO ESPECIAL PARA ESTE OBTER COMPONENTE HERDADO DE UM USER CONTROL
            DevExpress.XtraGrid.Views.Grid.GridView gridView1 = (DevExpress.XtraGrid.Views.Grid.GridView)grid21.gridControl1.Views[0];
            // FIM DO TRATAMENTO ESPECIAL

            // Tratamentos para a grid
            // Grid somente leitura
            gridView1.OptionsBehavior.Editable = false;

            // Grid com seleção de multiplos registros
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

            // Grid com scroll horizontal
            gridView1.OptionsView.ColumnAutoWidth = false;

            // Grid com auto ajuste do melhor tamanho das colunas
            gridView1.BestFitMaxRowCount = 800;
            gridView1.BestFitColumns();

            // Grid mostrar coluna de filtro
            gridView1.OptionsView.ShowAutoFilterRow = true;

            //Desabilita Colunas PRODUTOCOMPOSTO e MATERIALINTERLIGACAO
            DevExpress.XtraGrid.Views.Grid.GridView oGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grid21.gridControl1.Views[0];
            oGridView.Columns["PRODUTOCOMPOSTO"].Visible = false;
            oGridView.Columns["MATERIALINTERLIGACAO"].Visible = false;
            //oGridView.Columns["PRODUTO_COMPOSTO"].Visible = false;
            //oGridView.Columns["MATERIAL_INTERLIGACAO"].Visible = false;
            //oGridView.Columns["PROJETO"].Visible = false;
            //oGridView.Columns["MATERIAL_INTERLIGACAO"].Visible = false;

            // Grid sempre mostrar filtro no rodapé
            // gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;

            // Grid zebrada
            // gridView1.OptionsView.EnableAppearanceEvenRow = true;
            // gridView1.Appearance.EvenRow.BackColor = Color.Thistle;
        }

        #endregion

        #region PROCESSOS

        public void AplicarTabelaPreco(object sender, EventArgs e)
        {
            FormAplicarTabelaPreco f = new FormAplicarTabelaPreco();
            f.ShowDialog();

            if (f.TabelaPreco == null)
            {
                // ignorar
            }
            else
            {
                for (int i = 0; i < grid21.GetObjectRows().Count; i++)
                {
                    TITMMOV titmmov = (TITMMOV)grid21.GetObjectRows()[i];

                    String Consulta = @"
SELECT
ISNULL(PRECO1, 0) PRECO1,
ISNULL(PRECO2, 0) PRECO2,
ISNULL(PRECO3, 0) PRECO3,
ISNULL(PRECO4, 0) PRECO4,
ISNULL(PRECO5, 0) PRECO5
FROM TPRD
WHERE CODCOLIGADA = ?
  AND IDPRD = ?";

                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Consulta, new Object[] { AppLib.Context.Empresa, titmmov.IDPRD });

                    if (dt.Rows.Count > 0)
                    {
                        if (f.TabelaPreco.Equals("1"))
                        {
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO1"].ToString());
                            titmmov.VALORTOTAL = Convert.ToDecimal(string.Format("{0:n2}", titmmov.QUANTIDADE * titmmov.PRECOUNITARIO));
                        }

                        if (f.TabelaPreco.Equals("2"))
                        {
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO2"].ToString());
                            titmmov.VALORTOTAL = Convert.ToDecimal(string.Format("{0:n2}", titmmov.QUANTIDADE * titmmov.PRECOUNITARIO));
                        }

                        if (f.TabelaPreco.Equals("3"))
                        {
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO3"].ToString());
                            titmmov.VALORTOTAL = Convert.ToDecimal(string.Format("{0:n2}", titmmov.QUANTIDADE * titmmov.PRECOUNITARIO));
                        }

                        if (f.TabelaPreco.Equals("4"))
                        {
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO4"].ToString());
                            titmmov.VALORTOTAL = Convert.ToDecimal(string.Format("{0:n2}", titmmov.QUANTIDADE * titmmov.PRECOUNITARIO));
                        }

                        if (f.TabelaPreco.Equals("5"))
                        {
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO5"].ToString());
                            titmmov.VALORTOTAL = Convert.ToDecimal(string.Format("{0:n2}", titmmov.QUANTIDADE * titmmov.PRECOUNITARIO));
                        }
                    }
                }

                grid21_Atualizar(this, null);
            }
        }

        public void ExpandirTodosItens(object sender, EventArgs e)
        {
            // varre os itens
            for (int i = 0; i < Itens.Count; i++)
            {
                TITMMOV titmmov = Itens[i];

               
            }

            this.grid21_Atualizar(this, null);
        }

        private System.Data.DataTable composicaoProduto(int idprd)
        {
            String Consulta = @"
SELECT
TPRDCOMPOSTO.IDPRDCOMPONENTE IDPRD,
TPRDCOMPOSTO.IDPRD IDPRDCOMPOSTO,
TPRD.CODIGOPRD,
ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
TPRDCOMPOSTO.CODUND,
TPRDCOMPOSTO.QUANTIDADE,
ISNULL(TTRBPRD.ALIQUOTA, 0) ALIQUOTA,
TPRD.NUMEROCCF

FROM TPRDCOMPOSTO
LEFT JOIN TTRBPRD ON TPRDCOMPOSTO.CODCOLIGADA = TTRBPRD.CODCOLIGADA 
  AND TPRDCOMPOSTO.IDPRDCOMPONENTE = TTRBPRD.IDPRD
  AND TTRBPRD.CODTRB = 'IPI', 
  TPRD

WHERE TPRDCOMPOSTO.CODCOLIGADA = TPRD.CODCOLIGADA
  AND TPRDCOMPOSTO.IDPRDCOMPONENTE = TPRD.IDPRD

  AND TPRDCOMPOSTO.CODCOLIGADA = ?
  AND TPRDCOMPOSTO.IDPRD = ?

ORDER BY TPRDCOMPOSTO.NUMEROSEQUENCIAL";

            return AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Consulta, new Object[] { AppLib.Context.Empresa, idprd });
        }

        private Boolean produtoAcabado(String ProdutoDigitado, String ProdutoComposicao)
        {
            if (ProdutoDigitado.Substring(4).Equals(ProdutoComposicao.Substring(4)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region MANIPULAÇÃO DO REGISTRO

        private bool FormOrcamentoCadastro_Validar(object sender, EventArgs e)
        {
            if (campoLookupCODCFO.Get() == null)
            {
                MessageBox.Show("Campo Cliente obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoDataEMISSAO.Get() == null)
            {
                MessageBox.Show("Data de Emissão obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoDataENTREGA.Get() == null)
            {
                MessageBox.Show("Data de Entrega obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoInteiroPRAZOENTREGA.Get() == null)
            {
                MessageBox.Show("Prazo de Entrega obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoInteiroPRAZOENTREGA.Get() < 0)
            {
                MessageBox.Show("Data de Entrega deve ser maior ou igual a Data de Emissão.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoLookupCODRPR.Get() == null)
            {
                if (string.IsNullOrEmpty(campoLookupCODRPR.Get()))
                {
                    MessageBox.Show("Representante obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (campoTextoNUMEROMOV.Get() != null)
            {
                if (campoTextoNUMEROMOV.MaximoCaracteres != null)
                {
                    if (campoTextoNUMEROMOV.Get().Length > campoTextoNUMEROMOV.MaximoCaracteres)
                    {
                        MessageBox.Show("Campo Númedo do Movimento excedeu o limite de " + campoTextoNUMEROMOV.MaximoCaracteres + " caracteres.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            if (campoTextoNORDEM.Get() != null)
            {
                if (campoTextoNORDEM.MaximoCaracteres != null)
                {
                    if (campoTextoNORDEM.Get().Length > campoTextoNORDEM.MaximoCaracteres)
                    {
                        MessageBox.Show("Campo Pedido do Cliente excedeu o limite de " + campoTextoNORDEM.MaximoCaracteres + " caracteres.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            if (campoTexto1.Get() != null)
            {
                if (campoTexto1.MaximoCaracteres != null)
                {
                    if (campoTexto1.Get().Length > campoTexto1.MaximoCaracteres)
                    {
                        MessageBox.Show("Campo Segundo Número excedeu o limite de " + campoTexto1.MaximoCaracteres + " caracteres.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            if (campoTextoOBSERVACAO.Get() != null)
            {
                if (campoTextoOBSERVACAO.MaximoCaracteres != null)
                {
                    if (campoTextoOBSERVACAO.Get().Length > campoTextoOBSERVACAO.MaximoCaracteres)
                    {
                        MessageBox.Show("Campo Observação excedeu o limite de " + campoTextoOBSERVACAO.MaximoCaracteres + " caracteres.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            for (int i = 0; i < Itens.Count; i++)
            {
                TITMMOV item = Itens[i];

                
            }

            if (Itens.Count <= 0)
            {
                MessageBox.Show("Movimento não possui itens.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (row["STATUS"].ToString() == "FATURADO")
            {
                MessageBox.Show("Não é permitido editar requisição de peça 'faturada'.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //this.BotaoSalvar = false;
                //this.BotaoOK = false;
                return false;
            }
            return true;
        }

        private void FormOrcamentoCadastro_Preparar(object sender, EventArgs e)
        {
            this.ExpandirTodosItens(this, null);
        }

        private void FormOrcamentoCadastro_Excluir2(object sender, EventArgs e)
        {
            MessageBox.Show("Operação não permitida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private Boolean FormOrcamentoCadastro_Salvar2(object sender, EventArgs e)
        {
            try
            {
                //Verificação de preenchimento obrigatório
                if (String.IsNullOrWhiteSpace(campoLookupCLASSREQ.textBoxCODIGO.Text)) { throw new Exception("Campo Classificação da requisição não preenchida"); }
                if (String.IsNullOrWhiteSpace(campoLookupCLASSREQCPL.textBoxCODIGO.Text)) { throw new Exception("Campo Classificação da requisição complementar não preenchida"); }

                this.Cursor = Cursors.WaitCursor;

                Guid oGuid = Guid.NewGuid();
                string sSql = string.Empty;

                AppInterop.MovMovimentoPar movimento = new AppInterop.MovMovimentoPar();
                movimento.CodColigada = AppLib.Context.Empresa;

                if (campoInteiroIDMOV.Get() != null)
                {
                    movimento.IdMov = int.Parse(campoInteiroIDMOV.Get().ToString());
                    SalvaClassificacao(campoInteiroIDMOV.Get().ToString());
                }

                movimento.GuidId = oGuid.ToString();
                movimento.CodTMv = "2.1.05";
                movimento.CodFilial = 1;
                movimento.CodLoc = "001";
                movimento.CodColCfo = 0;
                movimento.CodCfo = campoLookupCODCFO.Get();
                movimento.Serie = "RQP";
                movimento.DataEmissao = campoDataEMISSAO.Get();
                movimento.DataEntrega = campoDataENTREGA.Get();
                movimento.PrazoEntrega = campoInteiroPRAZOENTREGA.Get();
                movimento.CodRepresentante = campoLookupCODRPR.Get();
                movimento.FreteCIFouFOB = int.Parse(campoListaFRETECIFOUFOB.Get());
                movimento.CodTra = campoLookupCODTRA.Get();

                movimento.Observacao = campoTextoOBSERVACAO.Get();
                movimento.HistoricoLongo = campoMemoHISTORICOLONGO.Get();
                movimento.NumeroOrdem = campoTextoNORDEM.Get();
                movimento.SegundoNumero = campoTexto1.Get();

                //Campos Complementares
                movimento.CLASSREQ = campoLookupCLASSREQ.Get();
                movimento.CLASSREQCPL = campoLookupCLASSREQCPL.Get();


                List<AppInterop.MovMovimentoItemPar> lMovMovimentoItemPar = new List<AppInterop.MovMovimentoItemPar>();
                for (int i = 0; i < Itens.Count; i++)
                {
                    TITMMOV item = Itens[i];

                    /*
                    if (item.PRODUTOCOMPOSTO != null && item.PRODUTOCOMPOSTO == 1)
                    {
                        if (string.IsNullOrEmpty(item.PROJETO))
                            throw new Exception("Atenção. Existe produto composto ao qual não foi informada a Ref. Item Projeto.");
                    }
                    */

                    AppInterop.MovMovimentoItemPar itmmov = new AppInterop.MovMovimentoItemPar();
                    itmmov.CodColigada = movimento.CodColigada;
                    itmmov.NSeqItmMov = (i + 1);
                    itmmov.IdPrd = item.IDPRD;
                    itmmov.CodUnd = item.UNIDADE;
                    itmmov.Quantidade = item.QUANTIDADE;
                    itmmov.PrecoUnitario = item.PRECOUNITARIO;
                    itmmov.AliquotaIPI = item.ALIQUOTAIPI;
                    itmmov.HistoricoLongo = item.HISTORICOLONGO;

                    

                    sSql = @"INSERT INTO ZTITMMOVORCTEMP(ROWID, CODCOLIGADA, NSEQITMMOV, IDPRD, IDPRDCOMPOSTO, CODUND, QUANTIDADE, PRECOUNITARIO, ALIQUOTAIPI, 
                                HISTORICOLONGO, PRODPRICIPAL, SEQUENCIAL, TIPOMAT) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)";

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(sSql, new object[] { oGuid, itmmov.CodColigada, itmmov.NSeqItmMov, itmmov.IdPrd,
                        itmmov.IdPrdComposto, itmmov.CodUnd, itmmov.Quantidade, itmmov.PrecoUnitario, itmmov.AliquotaIPI, itmmov.HistoricoLongo,
                        itmmov.PRODPRICIPAL, itmmov.SEQUENCIAL, itmmov.TIPOMAT});

                    movimento.ItemMovimento.Add(itmmov);
                }

                movimento.CodUsuario = AppLib.Context.Usuario;
                movimento.CodUsuarioLogado = AppLib.Context.Usuario;
                movimento.DataCriacao = DateTime.Now;
                movimento.DataMovimento = DateTime.Now;

                sSql = @"SELECT MAX(IDHISTORICO) IDMOVCFO FROM FCFOHISTORICO WHERE CODCOLIGADA = ? AND CODCFO = ?";
                sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { movimento.CodColCfo, movimento.CodCfo });
                System.Data.DataTable dt2 = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                if (dt2.Rows.Count > 0)
                    movimento.IdMovCfo = Convert.ToInt32(dt2.Rows[0]["IDMOVCFO"].ToString());
                else
                    movimento.IdMovCfo = null;


                AppInterop.Message mensagem;
                if (FatureContexto.Remoto)
                {
                    mensagem = new Util().ConvertToMessage(FatureContexto.ServiceSoapClient.RequisicaoPecasSave(AppLib.Context.Usuario, AppLib.Context.Senha, new Util().ConvertToWSMovMovimentoPar(movimento)));
                }
                else
                {
                    mensagem = FatureContexto.ServiceClient.RequisicaoPecasSave(AppLib.Context.Usuario, AppLib.Context.Senha, movimento);
                }

                if (int.Parse(mensagem.Retorno.ToString()) > 0)
                {
                    campoInteiroIDMOV.Set(int.Parse(mensagem.Retorno.ToString()));
                    SalvaClassificacao(campoInteiroIDMOV.Get().ToString());

                    try
                    {
                        sSql = @"SELECT NUMEROMOV FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?";
                        sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, (int)campoInteiroIDMOV.Get() });
                        System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                        if (dt.Rows.Count > 0)
                        {
                            campoTextoNUMEROMOV.Set(dt.Rows[0]["NUMEROMOV"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Default;
                        new AppLib.Windows.FormExceptionSQL().Mostrar(ex.Message, sSql, ex);
                    }

                    //Força reflesh nos campos de totais
                    sSql = @"SELECT * FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?";
                    sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, (int)campoInteiroIDMOV.Get() });
                    System.Data.DataTable dt1 = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                    if (dt1.Rows.Count > 0)
                    {
                        campoDecimalVALORBRUTO.Set((decimal?)Convert.ToDecimal(dt1.Rows[0]["VALORBRUTO"]));
                        campoDecimalVALOROUTROS.Set((decimal?)Convert.ToDecimal(dt1.Rows[0]["VALOROUTROS"]));
                        campoDecimalVALORLIQUIDO.Set((decimal?)Convert.ToDecimal(dt1.Rows[0]["VALORLIQUIDO"]));
                    }
                    this.Cursor = Cursors.Default;
                    //MessageBox.Show(mensagem.Mensagem);
                    return true;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    throw new Exception(mensagem.Mensagem);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void FormRequisicaoPecasCadastro_AposNovo(object sender, EventArgs e)
        {
            Itens.Clear();
            grid21_Atualizar(this, null);

            if (campoDataEMISSAO.Get() == null)
                campoDataEMISSAO.Set(DateTime.Now);

            if (campoListaFRETECIFOUFOB.Get() == null)
                campoListaFRETECIFOUFOB.Set("2");

            //if (campoLookupCODCPG.Get() == null)
            //campoLookupCODCPG.Set("F001");

            Valida v = new Valida();
            if (v.IsRepresentante())
            {
                string sSql = @"SELECT CODRPR FROM TRPR WHERE CODCOLIGADA = ? AND CODUSUARIO = ?";
                sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, AppLib.Context.Usuario });
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                if (dt.Rows.Count > 0)
                {
                    if (AppLib.Context.Perfil.Substring(0, 6) == "APPRPR")
                    {
                        campoLookupCODRPR.textBoxCODIGO.Text = dt.Rows[0]["CODRPR"].ToString();
                    }
                    else
                    {
                        if (AppLib.Context.Perfil.Substring(0, 6) == "APPRVD")
                        {
                            campoLookupCODRPR.textBoxCODIGO.Text = "00033";
                        }
                    }
                    //campoLookupCODRPR_SetDescricao(this, null);
                    campoLookupCODRPR.setDescricao();
                    campoLookupCODRPR.Enabled = false;
                }
                else
                {
                    campoLookupCODRPR.Enabled = true;
                }
            }
            else
            {
                campoLookupCODRPR.Enabled = true;
            }
        }

        private void FormRequisicaoPecasCadastro_AntesEditar(object sender, EventArgs e)
        {
            Itens.Clear();
            ExcluiuTudo = false;
        }

        private void FormRequisicaoPecasCadastro_AposEditar(object sender, EventArgs e)
        {
            Valida v = new Valida();
            if (v.IsRepresentante())
            {
                /*
                string sSql = @"SELECT CODRPR FROM TRPR WHERE CODCOLIGADA = ? AND CODUSUARIO = ?";
                sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, AppLib.Context.Usuario });
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                if (dt.Rows.Count > 0)
                {
                    if (AppLib.Context.Perfil.Substring(0, 6) == "APPRPR")
                    {
                        campoLookupCODRPR.textBoxCODIGO.Text = dt.Rows[0]["CODRPR"].ToString();
                    }
                    else
                    {
                        if (AppLib.Context.Perfil.Substring(0, 6) == "APPRVD")
                        {
                            campoLookupCODRPR.textBoxCODIGO.Text = "00033";
                        }                    
                    }
                    campoLookupCODRPR_SetDescricao(this, null);
                    campoLookupCODRPR.Enabled = false;
                }
                else
                {
                    campoLookupCODRPR.Enabled = true;
                }
                */

                campoLookupCODRPR.Enabled = false;
            }
            else
            {
                campoLookupCODRPR.Enabled = true;
            }
        }

        #endregion

        private void campoLookupCODCFO_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM FCFO WHERE CODCOLIGADA = ? AND CODCFO = ?";
            campoLookupCODCFO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODCFO.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODCFO.Get() }).ToString();
        }

        private void campoLookupCODRPR_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM TRPR WHERE CODCOLIGADA = ? AND CODRPR = ?";
            campoLookupCODRPR.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODRPR.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODRPR.Get() }).ToString();
        }

        private void campoLookupCODTRA_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM TTRA WHERE CODCOLIGADA = ? AND CODTRA = ?";
            campoLookupCODTRA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODTRA.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODTRA.Get() }).ToString();
        }

        private void campoLookupCLASSREQ_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT DESCRICAO FROM GCONSIST WHERE CODCOLIGADA = ?  AND APLICACAO = 'T' AND CODTABELA = 'CLASSREQ' AND CODCLIENTE = ?";
            campoLookupCLASSREQ.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCLASSREQ.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCLASSREQ.Get() }).ToString();
        }

        private void campoLookupCLASSREQCPL_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT DESCRICAO FROM GCONSIST WHERE CODCOLIGADA = ?  AND APLICACAO = 'T' AND CODTABELA = 'CLASSREQ' AND CODCLIENTE = ?";
            campoLookupCLASSREQCPL.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCLASSREQCPL.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCLASSREQCPL.Get() }).ToString();
        }

        private void campoLookupCLASSREQ_AposSelecao(object sender, EventArgs e)
        {
            campoLookupCLASSREQCPL.Enabled = true;

            campoLookupCLASSREQCPL.ColunaTabela = String.Format("(select CODCLIENTE, DESCRICAO  from GCONSIST where APLICACAO = 'T' and CODTABELA = 'CLASSREQ' and CODCLIENTE like '{0}.__') w", campoLookupCLASSREQ.textBoxCODIGO.Text);



            campoLookupCLASSREQ.ColunaTabela = String.Format("(select CODCLIENTE, DESCRICAO from GCONSIST where APLICACAO = 'T' and CODTABELA = 'CLASSREQ' and CODCOLIGADA = 1 and CODCLIENTE like '__') W", campoLookupCLASSREQ.textBoxCODIGO.Text);

            if (campoLookupCLASSREQ.textBoxCODIGO.Text == "01" || campoLookupCLASSREQ.textBoxCODIGO.Text == "05")
            {
                tabPage4.UseVisualStyleBackColor = true;
            }
            else
            {
                tabPage4.UseVisualStyleBackColor = false;
            }


        }



        private void gridItemDevolucao_Novo(object sender, EventArgs e)
        {
            try
            {
                FormRequisicaoPecasItemDevolucao frm = new FormRequisicaoPecasItemDevolucao();
                frm.EDIT = false;
                frm.ShowDialog();

                if (!String.IsNullOrWhiteSpace(frm.COD))
                {
                    string IDPRD = MetodosSQL.GetField(String.Format(@"select IDPRD from ZVWTPRD where CODIGOPRD = '{0}'", frm.COD), "IDPRD");

                    int i = dt.AsEnumerable().Where(n => n["IDPRD"].ToString() == IDPRD).Count();

                    if (i == 0)
                    {
                        dt.Rows.Add(IDPRD,
                                frm.COD,
                                frm.DESC,
                                frm.UNIDADE,
                                frm.QUANTIDADE,
                                "0.00",
                                frm.OBS,
                                frm.OBSDEV);

                        gridItemDevolucao.gridControl1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Produto já está na lista", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }

        private void campoLookupCLASSREQCPL_AposSelecao(object sender, EventArgs e)
        {
            campoLookupCLASSREQCPL1.Visible = true;
            campoLookupCLASSREQCPL1.Enabled = true;
            label8.Visible = true;
            campoLookupCLASSREQCPL1.ColunaTabela = String.Format("(select CODCLIENTE, DESCRICAO  from GCONSIST where APLICACAO = 'T' and CODTABELA = 'CLASSREQ' and CODCLIENTE like '{0}.__') w", campoLookupCLASSREQ.textBoxCODIGO.Text);
        }

        private void campoLookupCLASSREQCPL1_AposSelecao(object sender, EventArgs e)
        {
            campoLookupCLASSREQCPL2.Visible = true;
            campoLookupCLASSREQCPL2.Enabled = true;
            label9.Visible = true;
            campoLookupCLASSREQCPL2.ColunaTabela = String.Format("(select CODCLIENTE, DESCRICAO  from GCONSIST where APLICACAO = 'T' and CODTABELA = 'CLASSREQ' and CODCLIENTE like '{0}.__') w", campoLookupCLASSREQ.textBoxCODIGO.Text);
        }

        public void BaixarProduto(object sender, EventArgs e)
        {
            try
            {
                dt.Columns["QUANTIDADEBAIXADA"].ReadOnly = false;

                DataRow SelectedRow = gridItemDevolucao.GetDataRow();

                AppLib.Windows.FormMessagePrompt frm = new AppLib.Windows.FormMessagePrompt();
                frm.richTextBox1.Text = "Digite a quantidade baixada";

                var isNumeric = false;

                frm.ShowDialog();

                while (isNumeric)
                {
                    frm.ShowDialog();
                    isNumeric = int.TryParse(frm.textBox1.Text, out int n);
                }

                foreach (DataRow dr in dt.Rows) // search whole table
                {
                    if (dr["IDPRD"].ToString() == SelectedRow["IDPRD"].ToString()) // if id==2
                    {
                        double total = double.Parse(frm.textBox1.Text) + Double.Parse(dr["QUANTIDADEBAIXADA"].ToString().Replace(".", ","));

                        double quantidade = Convert.ToDouble(dr["QUANTIDADE"].ToString().Replace(".", ","));

                        if (total <= quantidade)
                        {
                            dr["QUANTIDADEBAIXADA"] = total.ToString("F");

                            if (campoInteiroIDMOV.Get() > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {

                                    string qtdbaixa = "";

                                    if (String.IsNullOrWhiteSpace(row["QUANTIDADEBAIXADA"].ToString()))
                                    {
                                        qtdbaixa = "0.00";
                                    }
                                    else
                                    {
                                        qtdbaixa = row["QUANTIDADEBAIXADA"].ToString();
                                    }

                                    string ID = campoInteiroIDMOV.Get().ToString();

                                    int c = int.Parse(MetodosSQL.GetField(String.Format(@"select count(1) as 'Contador' from ZREQDEVOLUCAO where IDREQ = {0} and IDPRD = {1}", ID, row["IDPRD"].ToString()), "Contador"));

                                    string sql = String.Empty;


                                    if (c == 1)
                                    {
                                        sql = String.Format(@"update ZREQDEVOLUCAO
	                                        set IDPRD = {0},
			                                    QUANTIDADE = {1},
		                                    	QUANTIDADEBAIXADA = {2},
		                                    	OBSERVACAO = '{3}',
		                                    	OBSERVACAODEVOLUCAO = '{4}',
		                                    	USERALTERACAO = '{5}',
			                                    DATABAIXA = getdate()
		                                    	where IDREQ = {6} and IDPRD = {0}", row["IDPRD"], row["QUANTIDADE"].ToString().Replace(",", "."), qtdbaixa.Replace(",", "."), row["OBS"], row["OBSDEV"], AppLib.Context.Usuario, ID);

                                        MetodosSQL.ExecQuery(sql);
                                    }
                                    else
                                    {

                                        sql = String.Format(@"insert into ZREQDEVOLUCAO values ({0},{1},{2},{3},'{4}','{5}','{6}',GETDATE())", ID, row["IDPRD"], row["QUANTIDADE"].ToString().Replace(",", "."), qtdbaixa.Replace(",", "."), row["OBS"], row["OBSDEV"], AppLib.Context.Usuario);
                                        MetodosSQL.ExecQuery(sql);
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não é possivel dar baixa em quantidade maior que a quantidade pré estipulada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void SalvaClassificacao(string ID)
        {
            string sql = String.Format(@"update TMOVCOMPL
                                            set CLASSREQCPL1 = '{0}',
	                                            CLASSREQCPL2 = '{1}'
	                                            where IDMOV = '{2}'", campoLookupCLASSREQCPL1.textBoxCODIGO.Text, campoLookupCLASSREQCPL2.textBoxCODIGO.Text, ID);
            MetodosSQL.ExecQuery(sql);

            foreach (DataRow row in dt.Rows)
            {

                string qtdbaixa = "";

                if (String.IsNullOrWhiteSpace(row["QUANTIDADEBAIXADA"].ToString()))
                {
                    qtdbaixa = "0.00";
                }
                else
                {
                    qtdbaixa = row["QUANTIDADEBAIXADA"].ToString();
                }


                int c = int.Parse(MetodosSQL.GetField(String.Format(@"select count(1) as 'Contador' from ZREQDEVOLUCAO where IDREQ = {0} and IDPRD = {1}", ID, row["IDPRD"].ToString()), "Contador"));


                if (c == 1)
                {
                    sql = String.Format(@"update ZREQDEVOLUCAO
	                                        set IDPRD = {0},
			                                    QUANTIDADE = {1},
		                                    	QUANTIDADEBAIXADA = {2},
		                                    	OBSERVACAO = '{3}',
		                                    	OBSERVACAODEVOLUCAO = '{4}',
		                                    	USERALTERACAO = '{5}',
			                                    DATABAIXA = getdate()
		                                    	where IDREQ = {6} and IDPRD = {0}", row["IDPRD"], row["QUANTIDADE"].ToString().Replace(",", "."), qtdbaixa.Replace(",", "."), row["OBS"], row["OBSDEV"], AppLib.Context.Usuario, ID);

                    MetodosSQL.ExecQuery(sql);
                }
                else
                {

                    sql = String.Format(@"insert into ZREQDEVOLUCAO values ({0},{1},{2},{3},'{4}','{5}','{6}',GETDATE())", ID, row["IDPRD"], row["QUANTIDADE"].ToString().Replace(",", "."), qtdbaixa.Replace(",", "."), row["OBS"], row["OBSDEV"], AppLib.Context.Usuario);
                    MetodosSQL.ExecQuery(sql);
                }
            }
        }

        private void gridItemDevolucao_AntesAtualizar(object sender, EventArgs e)
        {
            gridItemDevolucao.gridControl1.DataSource = dt;
        }

        private void gridItemDevolucao_AposAtualizar(object sender, EventArgs e)
        {
            gridItemDevolucao.gridControl1.DataSource = dt;
        }

        private void AtualizaItensReq()
        {
            string sql = String.Format(@"select ZRD.IDPRD, 
			   cast((select CODIGOPRD from ZVWTPRD ZT where ZT.IDPRD = ZRD.IDPRD) as varchar(max)) as 'COD', 
			   cast((select NOMEFANTASIA from ZVWTPRD ZT where ZT.IDPRD = ZRD.IDPRD) as varchar(max)) as 'DESC', 
			   cast((select CODUNDCONTROLE from TPRD where IDPRD = ZRD.IDPRD) as varchar(max)) as 'UNIDADE', 
			   cast(QUANTIDADE as varchar(max)) as 'QUANTIDADE', 
			   cast(QUANTIDADEBAIXADA as varchar(max)) as 'QUANTIDADEBAIXADA', 
			   cast(OBSERVACAO as varchar(max)) as 'OBS', 
			   cast(OBSERVACAODEVOLUCAO as varchar(max)) as 'OBSDEV' from ZREQDEVOLUCAO ZRD where IDREQ = '{0}'", campoInteiroIDMOV.Get().ToString());

            dt = MetodosSQL.GetDT(sql);

            gridItemDevolucao.gridControl1.DataSource = dt;
        }

        private void gridItemDevolucao_Load(object sender, EventArgs e)
        {
            AtualizaItensReq();
        }

        private void gridItemDevolucao_Editar(object sender, EventArgs e)
        {
            try
            {
                if (campoInteiroIDMOV.Get() > 0)
                {
                    DataRow SelectedRow = gridItemDevolucao.GetDataRow();

                    if (SelectedRow["QUANTIDADE"].ToString().Replace(".", ",") != SelectedRow["QUANTIDADEBAIXADA"].ToString().Replace(".", ","))
                    {
                        FormRequisicaoPecasItemDevolucao frm = new FormRequisicaoPecasItemDevolucao();
                        frm.EDIT = true;
                        frm.COD = SelectedRow["COD"].ToString();
                        frm.DESC = SelectedRow["DESC"].ToString();
                        frm.UNIDADE = SelectedRow["UNIDADE"].ToString();
                        frm.QUANTIDADE = SelectedRow["QUANTIDADE"].ToString();
                        frm.OBS = SelectedRow["OBS"].ToString();
                        frm.OBSDEV = SelectedRow["OBSDEV"].ToString();
                        frm.ShowDialog();

                        string sql = String.Format(@"update ZREQDEVOLUCAO set OBSERVACAODEVOLUCAO = '{0}' where IDREQ = {1} and IDPRD = {2}", frm.OBSDEV, campoInteiroIDMOV.Get(), SelectedRow["IDPRD"].ToString());

                        MetodosSQL.ExecQuery(sql);

                        AtualizaItensReq();
                    }
                    else
                    {
                        MessageBox.Show("Não é possivel editar itens com baixa finalizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void gridItemDevolucao_Excluir(object sender, EventArgs e)
        {
            DataRow SelectedRow = gridItemDevolucao.GetDataRow();

            try
            {
                if (campoInteiroIDMOV.Get() > 0)
                {

                    if (Double.Parse(SelectedRow["QUANTIDADEBAIXADA"].ToString().Replace(".", ",")) == 0)
                    {
                        string sql = String.Format(@"delete from ZREQDEVOLUCAO where IDREQ = {0} and IDPRD = {1}", campoInteiroIDMOV.Get().ToString(), SelectedRow["IDPRD"].ToString());

                        MetodosSQL.ExecQuery(sql);

                        gridItemDevolucao.Atualizar();
                    }
                    else
                    {
                        MessageBox.Show("Não é possivel excluir item com baixa registrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["IDPRD"].ToString() == SelectedRow["IDPRD"].ToString())
                    {
                        dr.Delete();
                    }
                }
                dt.AcceptChanges();

                gridItemDevolucao.gridControl1.DataSource = dt;
            }
            catch
            {

            }
        }
    }
}
