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
    public partial class FormOffOrcamentoCadastro : AppLib.Windows.FormCadastroData
    {
        public int? CODCOLIGADA { get; set; }
        public int? IDMOV { get; set; }

        public FormOffOrcamentoCadastro()
        {
            InitializeComponent();

            this.gridData1.GetProcessos().Add("Aplicar tabela de preço", null, AplicarTabelaPreco);
        }

        public void ExpandirTodosItens()
        {
            this.Cursor = Cursors.WaitCursor;

            String ConsultaItens = @"SELECT ZORCAMENTOITEM.*,
	                                 ( SELECT CODIGOPRD FROM ZTPRODUTO WHERE CODCOLPRD = ZORCAMENTOITEM.CODCOLIGADA AND IDPRD = ZORCAMENTOITEM.IDPRD ) CODIGOPRD
	                                 FROM ZORCAMENTOITEM
                                     WHERE CODCOLIGADA = ?
                                       AND IDMOV = ?";

            System.Data.DataTable dtItens = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(ConsultaItens, new Object[] { campoInteiroCODCOLIGADA.Get(), campoInteiroIDMOV.Get() });

            for (int i = 0; i < dtItens.Rows.Count; i++)
            {
                int CODCOLIGADA = int.Parse(dtItens.Rows[i]["CODCOLIGADA"].ToString());
                string IDMOV = dtItens.Rows[i]["IDMOV"].ToString();
                int NSEQITMMOV = int.Parse(dtItens.Rows[i]["NSEQITMMOV"].ToString());

                int IDPRD = int.Parse(dtItens.Rows[i]["IDPRD"].ToString());
                String CODIGOPRD = dtItens.Rows[i]["CODIGOPRD"].ToString();
                decimal QUANTIDADE = decimal.Parse(dtItens.Rows[i]["QUANTIDADE"].ToString());
                decimal PRECOUNITARIO = decimal.Parse(dtItens.Rows[i]["PRECOUNITARIO"].ToString());

                decimal? ALIQUOTAIPI = null;

                if ( ! dtItens.Rows[i]["ALIQUOTAIPI"].ToString().Equals(""))
                {
                    ALIQUOTAIPI = decimal.Parse(dtItens.Rows[i]["ALIQUOTAIPI"].ToString());
                }

                int PRODUTOCOMPOSTO = int.Parse(dtItens.Rows[i]["PRODUTOCOMPOSTO"].ToString());

                String PROJETO = String.Empty;

                if (!dtItens.Rows[i]["PROJETO"].ToString().Equals(""))
                {
                    PROJETO = dtItens.Rows[i]["PROJETO"].ToString();
                }

                int MATERIALINTERLIGACAO = int.Parse(dtItens.Rows[i]["MATERIALINTERLIGACAO"].ToString());

                String HISTORICOLONGO = String.Empty;

                if (!dtItens.Rows[i]["HISTORICOLONGO"].ToString().Equals(""))
                {
                    HISTORICOLONGO = dtItens.Rows[i]["HISTORICOLONGO"].ToString();
                }

                // SE NÃO FOI EXPANDIDO
                if (PRODUTOCOMPOSTO == 9)
                {
                    // SE TEM COMPOSIÇÃO
                    System.Data.DataTable dt = this.BuscarComposicao(CODCOLIGADA, IDPRD);

                    if (dt.Rows.Count > 0)
                    {
                        for ( int n = 0; n < dt.Rows.Count; n++ )
                        {
                            String Consulta = "SELECT ISNULL(MAX(NSEQITMMOV),0) + 1 NSEQITMMOV FROM ZORCAMENTOITEM WHERE CODCOLIGADA = ? AND IDMOV = ?";

                            // INCLUI OS ITENS EXPANDIDOS
                            AppLib.ORM.Jit row = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), "ZORCAMENTOITEM");
                            row.Set("CODCOLIGADA", CODCOLIGADA);
                            row.Set("IDMOV", IDMOV);
                            row.Set("NSEQITMMOV", AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, Consulta, new Object[] { CODCOLIGADA, IDMOV }));
                            row.Set("IDPRD", int.Parse(dt.Rows[n]["IDPRDCOMPONENTE"].ToString()));
                            row.Set("IDPRDCOMPOSTO", IDPRD);
                            row.Set("CODUND", dt.Rows[n]["CODUND"].ToString());

                            decimal QTDTEMP = decimal.Parse(dt.Rows[n]["QUANTIDADE"].ToString());
                            row.Set("QUANTIDADE", ( QUANTIDADE * QTDTEMP ));
                            row.Set("PRECOUNITARIO", 0);
                            row.Set("ALIQUOTAIPI", (dt.Rows[n]["ALIQUOTAIPI"] == DBNull.Value)? 0 : decimal.Parse(dt.Rows[n]["ALIQUOTAIPI"].ToString()));

                            // SE É O PRODUTO ACABADO
                            if (this.produtoAcabado(CODIGOPRD, dt.Rows[n]["CODIGOPRD"].ToString()))
                            {
                                row.Set("PRODUTOCOMPOSTO", 1);
                                row.Set("HISTORICOLONGO", HISTORICOLONGO);
                            }
                            else
                            {
                                row.Set("PRODUTOCOMPOSTO", 0);
                            }

                            row.Set("PROJETO", PROJETO);
                            row.Set("MATERIALINTERLIGACAO", MATERIALINTERLIGACAO);

                            int tempInsert = row.Insert();

                            if (tempInsert != 1)
                            {
                                MessageBox.Show("Erro ao salvar IDPRDCOMPONENTE = " + int.Parse(dt.Rows[n]["IDPRDCOMPONENTE"].ToString()));
                            }

                        }

                        // EXCLUI O ITEM CADASTRADO
                        String Comando = "DELETE ZORCAMENTOITEM WHERE CODCOLIGADA = ? AND IDMOV = ? AND NSEQITMMOV = ?";
                        int tempDelete = AppLib.Context.poolConnection.Get(Conexao).ExecTransaction(Comando, new Object[] { CODCOLIGADA, IDMOV, NSEQITMMOV });

                        if (tempDelete != 1)
                        {
                            MessageBox.Show("Erro ao excluir item, quando codcoligada " + CODCOLIGADA + " idmov " + IDMOV + " e nseqitmmov " + NSEQITMMOV + ".");
                        }
                    }
                    else
                    {
                        String Comando = "UPDATE ZORCAMENTOITEM SET PRODUTOCOMPOSTO = 0 WHERE CODCOLIGADA = ? AND IDMOV = ? AND NSEQITMMOV = ?";
                        int tempUpdate = AppLib.Context.poolConnection.Get(Conexao).ExecTransaction(Comando, new Object[] { CODCOLIGADA, IDMOV, NSEQITMMOV });

                        if (tempUpdate != 1)
                        {
                            MessageBox.Show("Erro ao atualizar campo produto composto = 0 quando codcoligada " + CODCOLIGADA + " idmov " + IDMOV + " e nseqitmmov " + NSEQITMMOV + ".");
                        }
                    }
                }
            }

            this.Cursor = Cursors.Default;
            gridData1.toolStripButtonATUALIZAR_Click(this, null);

        }

        public void RecalcularTotais()
        {
            int codcoligada = (int)campoInteiroCODCOLIGADA.Get();
            string idmov = campoInteiroIDMOV.Get();

            #region VALOR BRUTO

            String queryValorBruto = @"
SELECT 
SUM(TOTAL)
FROM
(

SELECT 
CASE WHEN IDPRDCOMPOSTO IS NULL THEN 
((ISNULL(QUANTIDADE, 0) * ISNULL(PRECOUNITARIO, 0)) * (1 + (ISNULL(ALIQUOTAIPI, 0)/100)))
ELSE
(ISNULL(QUANTIDADE, 0) * ISNULL(PRECOUNITARIO, 0))
END TOTAL

FROM ZORCAMENTOITEM
WHERE CODCOLIGADA = ?
  AND IDMOV = ?
)X";

            decimal? valorBruto = 0;

            try
            {
                valorBruto = decimal.Parse(AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, queryValorBruto, new Object[] { codcoligada, idmov }).ToString());
            }
            catch { }
            
            #endregion

            #region VALOR OUTROS

            decimal? valorDespesa = 0;

            try
            {
                valorDespesa = campoDecimalVALORDESP.Get();
            }
            catch { }

            decimal? valorSeguro = 0;

            try
            {
                valorSeguro = campoDecimalVALORSEGURO.Get();
            }
            catch { }

            decimal? valorOutros = valorBruto + valorDespesa + valorSeguro;

            #endregion

            #region VALOR LIQUIDO

            decimal? valorFrete = 0;

            try
            {
                valorFrete = campoDecimalVALORFRETE.Get();
            }
            catch { }

            decimal? valorDesconto = 0;

            try
            {
                valorDesconto = campoDecimalVALORDESC.Get();
            }
            catch { }

            decimal? valorExtra1 = 0;

            try
            {
                valorExtra1 = campoDecimalVALOREXTRA1.Get();
            }
            catch { }

            decimal? valorLiquido = valorOutros + valorFrete - valorDesconto - valorExtra1;

            #endregion

            #region ATUALIZA CAMPOS NA TELA

            campoDecimalVALORBRUTO.Set(valorBruto);
            campoDecimalVALOROUTROS.Set(valorOutros);
            campoDecimalVALORLIQUIDO.Set(valorLiquido);

            #endregion

            #region ATUALIZA REGISTRO NO BANCO DE DADOS

            String updateValores = @"
UPDATE ZORCAMENTO
SET VALORBRUTO = ?, VALOROUTROS = ?, VALORLIQUIDO = ?
WHERE CODCOLIGADA = ?
  AND IDMOV = ?";

            int contUpdate = AppLib.Context.poolConnection.Get("Conn2").ExecTransaction(updateValores, new Object[] { valorBruto, valorOutros, valorLiquido, codcoligada, idmov });

            if (contUpdate != 1)
            {
                MessageBox.Show("Erro ao atualizar valores do registro CODCOLIGADA: " + codcoligada + " IDMOV: " + idmov + ".", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
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

        public System.Data.DataTable BuscarComposicao(int CODCOLIGADA, int IDPRD)
        {
            String Consulta = @"
SELECT IDPRDCOMPONENTE, NUMEROSEQUENCIAL, QUANTIDADE, CODUND,
( SELECT CODIGOPRD FROM ZTPRODUTO WHERE CODCOLPRD = ZTPRDCOMPOSTO.CODCOLIGADA AND IDPRD = ZTPRDCOMPOSTO.IDPRDCOMPONENTE ) CODIGOPRD,
( SELECT ALIQUOTAIPI FROM ZTPRODUTO WHERE CODCOLPRD = ZTPRDCOMPOSTO.CODCOLIGADA AND IDPRD = ZTPRDCOMPOSTO.IDPRDCOMPONENTE ) ALIQUOTAIPI
FROM ZTPRDCOMPOSTO
WHERE CODCOLIGADA = ?
  AND IDPRD = ?";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(Consulta, new Object[] { CODCOLIGADA, IDPRD });
            return dt;
        }

        public void AplicarTabelaPreco(object sender, EventArgs e)
        {
            FormAplicarTabelaPreco f = new FormAplicarTabelaPreco();
            f.ConexaoDefault = false;
            f.ShowDialog();

            if (f.TabelaPreco == null)
            {
                // ignorar
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;

                gridData1.gridView1.SelectAll();
                System.Data.DataRowCollection drc = gridData1.GetDataRows();

                AppLib.ORM.Jit row = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Conn2"), "ZORCAMENTOITEM");

                for (int i = 0; i < drc.Count; i++)
                {
                    int CODCOLIGADA = int.Parse(drc[i]["CODCOLIGADA"].ToString());
                    string IDMOV = drc[i]["IDMOV"].ToString();
                    int NSEQITMMOV = int.Parse(drc[i]["NSEQITMMOV"].ToString());

                    int IDPRD = int.Parse(drc[i]["IDPRD"].ToString());

                    String Consulta = @"
SELECT
ISNULL(PRECO1, 0) PRECO1,
ISNULL(PRECO2, 0) PRECO2,
ISNULL(PRECO3, 0) PRECO3,
ISNULL(PRECO4, 0) PRECO4,
ISNULL(PRECO5, 0) PRECO5
FROM ZTPRODUTO
WHERE IDPRD = ?";

                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Consulta, new Object[] { IDPRD });

                    if (dt.Rows.Count > 0)
                    {
                        decimal PRECOUNITARIO = 0;

                        if (f.TabelaPreco.Equals("1"))
                        {
                            PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO1"].ToString());
                        }

                        if (f.TabelaPreco.Equals("2"))
                        {
                            PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO2"].ToString());
                        }

                        if (f.TabelaPreco.Equals("3"))
                        {
                            PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO3"].ToString());
                        }

                        if (f.TabelaPreco.Equals("4"))
                        {
                            PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO4"].ToString());
                        }

                        if (f.TabelaPreco.Equals("5"))
                        {
                            PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO5"].ToString());
                        }

                        row.Set("CODCOLIGADA", CODCOLIGADA);
                        row.Set("IDMOV", IDMOV);
                        row.Set("NSEQITMMOV", NSEQITMMOV);
                        row.Set("PRECOUNITARIO", PRECOUNITARIO);

                        if (row.Save() != 1)
                        {
                            MessageBox.Show("Erro ao aplicar preço do item sequencial " + NSEQITMMOV + ".", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                gridData1.toolStripButtonATUALIZAR_Click(this, null);
                this.dropDownButtonSalvar_Click(this, null);

                this.Cursor = Cursors.Default;
            }
        }

        private void FormOffOrcamentoCadastro_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(campoInteiroIDMOV.Get()))
            {
                campoDataDATAEMISSAO.Set(DateTime.Now);
                campoDataDATAENTREGA.Set(DateTime.Now);
                campoInteiroPRAZOENTREGA.Set(0);
            }
            
            
            
            
            
            
            campoListaFINANCIADO.Set("Não");
            campoListaDEMONSBRINDE.Set("Não");
            campoListaPARAFINANC.Set("Não");
        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            gridData1.Parametros = new Object[] { campoInteiroCODCOLIGADA.Get(), campoInteiroIDMOV.Get() };
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(campoInteiroIDMOV.Get()))
                {
                    this.dropDownButtonSalvar_Click(this, null);
                }

                if (!string.IsNullOrEmpty(campoInteiroIDMOV.Get()))
                {
                    FormOffOrcamentoItem f = new FormOffOrcamentoItem();
                    f.CODCOLIGADA = campoInteiroCODCOLIGADA.Get();
                    f.IDMOV = campoInteiroIDMOV.Get();
                    f.Novo();

                    this.ExpandirTodosItens();
                    this.RecalcularTotais();
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            try
            {
               FormOffOrcamentoItem f = new FormOffOrcamentoItem();
                System.Data.DataRow dr = gridData1.GetDataRow();
                f.CODCOLIGADA = campoInteiroCODCOLIGADA.Get();
                f.IDMOV = campoInteiroIDMOV.Get();
                f.NSEQITMMOV = int.Parse(dr["NSEQITMMOV"].ToString());
                f.Editar(gridData1.bs);

                this.ExpandirTodosItens();
                this.RecalcularTotais();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            try
            {
                new FormOffOrcamentoItem().Excluir(gridData1.GetDataRows());
                this.RecalcularTotais();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCFO, "SELECT CODCOLIGADA, CODCFO, NOME, NOMEFANTASIA, PESSOAFISOUJUR, CGCCFO, INSCRESTADUAL, EMAIL, RUA, NUMERO, COMPLEMENTO, BAIRRO, PAIS, CODETD, CIDADE, CONTATO FROM ZFCFO WHERE CODCOLIGADA IN (0, ?) AND PAGREC IN (1, 3) AND ATIVO = 1", new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup2_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODRPR, "SELECT * FROM ZTRPR WHERE CODCOLIGADA = ? AND INATIVO = 0", new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookupTTRA_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupTRANSPORTADORA, "SELECT * FROM ZTTRA WHERE CODCOLIGADA = ? AND INATIVO = 0", new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookupTPCULTURA_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupTPCULTURA, "SELECT * FROM ZTPCULTURA WHERE CODCOLIGADA = ?", new Object[] { AppLib.Context.Empresa });
        }

        public void ContarPrazo()
        {
            try
            {
                DateTime DATAEMISSAO = (DateTime)campoDataDATAEMISSAO.Get();
                DateTime DATAENTREGA = (DateTime)campoDataDATAENTREGA.Get();
                int PRAZOENTREGA = AppLib.Util.Calculo.DateDiff(DATAEMISSAO, DATAENTREGA);
                campoInteiroPRAZOENTREGA.Set(PRAZOENTREGA);
            }
            catch { }
        }

        private void campoDataDATAEMISSAO_Leave(object sender, EventArgs e)
        {
            //this.ContarPrazo();

            DateTime Emissao, Entrega;

            if (campoDataDATAEMISSAO.Get() == null)
                return;
            else
                Emissao = (DateTime)campoDataDATAEMISSAO.Get();


            if (campoDataDATAENTREGA.Get() == null)
                return;
            else
                Entrega = (DateTime)campoDataDATAENTREGA.Get();

            campoInteiroPRAZOENTREGA.Set((Entrega - Emissao).Days);
        }

        private void campoDataDATAENTREGA_Leave(object sender, EventArgs e)
        {
            //this.ContarPrazo();

            DateTime Emissao, Entrega;

            if (campoDataDATAEMISSAO.Get() == null)
                return;
            else
                Emissao = (DateTime)campoDataDATAEMISSAO.Get();


            if (campoDataDATAENTREGA.Get() == null)
                return;
            else
                Entrega = (DateTime)campoDataDATAENTREGA.Get();

            campoInteiroPRAZOENTREGA.Set((Entrega - Emissao).Days);
        }

        private void campoInteiroPRAZOENTREGA_Leave(object sender, EventArgs e)
        {
            /*
            try
            {
                campoDataDATAENTREGA.Set( campoDataDATAEMISSAO.Get().Value.AddDays(double.Parse(campoInteiroPRAZOENTREGA.Get().ToString()) ));
            }
            catch { }
            */

            int prazodias = Convert.ToInt32(campoInteiroPRAZOENTREGA.Get());
            campoDataDATAENTREGA.Set(Convert.ToDateTime(campoDataDATAEMISSAO.Get()).AddDays(prazodias));
        }

        public Boolean Validar()
        {
            if (string.IsNullOrEmpty(campoLookupCODCFO.Get()) && string.IsNullOrEmpty(campoLookup1.Get()))
            {
                MessageBox.Show("Campo Cliente ou Cliente Novo obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoDataDATAEMISSAO.Get() == null)
            {
                MessageBox.Show("Data de Emissão obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoDataDATAENTREGA.Get() == null)
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

            if (String.IsNullOrEmpty(campoListaFINANCIADO.Get()))
            {
                MessageBox.Show("Selecione uma opção para o campo Financiado.");
                return false;
            }

            if (String.IsNullOrEmpty(campoListaDEMONSBRINDE.Get()))
            {
                MessageBox.Show("Selecione uma opção para o campo Demonstração ou Brinde.");
                return false;
            }

            if (String.IsNullOrEmpty(campoListaPARAFINANC.Get()))
            {
                MessageBox.Show("Selecione uma opção para o campo Orçamento para Financiamento.");
                return false;
            }

            /*
            String ConsultaITENS = "SELECT COUNT(IDPRD) CONTADOR FROM ZORCAMENTOITEM WHERE CODCOLIGADA = ? AND IDMOV = ?";
            int CONTADOR = int.Parse(AppLib.Context.poolConnection.Get(Conexao).ExecGetField(ConsultaITENS, new Object[] { campoInteiroCODCOLIGADA.Get(), campoInteiroIDMOV.Get() }).ToString());
            if (CONTADOR <= 0)
            {
                MessageBox.Show("Atenção. Movimento não possui itens.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            */

            String ConsultaITENS = "SELECT COUNT(IDPRD) CONTADOR FROM ZORCAMENTOITEM WHERE CODCOLIGADA = ? AND IDMOV = ? AND PRODUTOCOMPOSTO = 1 AND (PROJETO IS NULL OR PROJETO = '')";
            int CONTADOR = int.Parse(AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, ConsultaITENS, new Object[] { campoInteiroCODCOLIGADA.Get(), campoInteiroIDMOV.Get() }).ToString());
            if (CONTADOR > 0)
            {
                MessageBox.Show("Atenção. Existe produto composto ao qual não foi informada a Ref. Item Projeto.");
                return false;
            }

            return true;
        }

        private bool FormOffOrcamentoCadastro_Validar(object sender, EventArgs e)
        {
            return this.Validar();
        }

        private void FormOffOrcamentoCadastro_AntesSalvar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(campoInteiroIDMOV.Get()))
            {
                String Consulta = "SELECT RTRIM(REPLACE(CONVERT(CHAR,GETDATE(),11),'/','')) + RTRIM(REPLACE(CONVERT(CHAR,GETDATE(),14),':','')) CHAVE FROM ZGCOLIGADA WHERE CODCOLIGADA = ?";
                campoInteiroIDMOV.Set(AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, Consulta, new Object[] { AppLib.Context.Empresa }).ToString());
            }

            campoInteiroCODCOLIGADA.Set(AppLib.Context.Empresa);

            #region OBTÉM O CODCOLCFO

            String ConsultaCODCOLCFO = "SELECT MIN(CODCOLIGADA) FROM ZFCFO WHERE CODCOLIGADA IN (0, ?) AND CODCFO = ?";
            int CODCOLCFO = int.Parse(AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, ConsultaCODCOLCFO, new Object[] { AppLib.Context.Empresa, campoLookupCODCFO.Get() }).ToString());
            campoInteiroCODCOLCFO.Set(CODCOLCFO);

            #endregion
        }

        private void FormOffOrcamentoCadastro_AposSalvar(object sender, EventArgs e)
        {
            this.RecalcularTotais();
        }

        #region TRATAMENTO DOS CAMPOS VALORES

        public decimal? CalculaValor(decimal? valorBruto, decimal? percentual)
        {
            return (valorBruto * (percentual / 100));
        }

        public decimal? CalculaPercentual(decimal? valorBruto, decimal? valor)
        {
            return ((valor / valorBruto) * 100);
        }

        private void campoDecimalPERCENTUALFRETE_Leave(object sender, EventArgs e)
        {
            if ((campoDecimalPERCENTUALFRETE.Get() != null) && (campoDecimalPERCENTUALFRETE.Get() != 0))
            {
                campoDecimalVALORFRETE.Set(CalculaValor(campoDecimalVALORBRUTO.Get(), campoDecimalPERCENTUALFRETE.Get()));
            }
        }

        private void campoDecimalVALORFRETE_Leave(object sender, EventArgs e)
        {
            if ((campoDecimalVALORFRETE.Get() != null) && (campoDecimalVALORFRETE.Get() != 0))
            {
                campoDecimalPERCENTUALFRETE.Set(CalculaPercentual(campoDecimalVALORBRUTO.Get(), campoDecimalVALORFRETE.Get()));
            }
        }

        private void campoDecimalPERCENTUALDESC_Leave(object sender, EventArgs e)
        {
            if ((campoDecimalPERCENTUALDESC.Get() != null) && (campoDecimalPERCENTUALDESC.Get() != 0))
            {
                campoDecimalVALORDESC.Set(CalculaValor(campoDecimalVALORBRUTO.Get(), campoDecimalPERCENTUALDESC.Get()));
            }
        }

        private void campoDecimalVALORDESC_Leave(object sender, EventArgs e)
        {
            if ((campoDecimalVALORDESC.Get() != null) && (campoDecimalVALORDESC.Get() != 0))
            {
                campoDecimalPERCENTUALDESC.Set(CalculaPercentual(campoDecimalVALORBRUTO.Get(), campoDecimalVALORDESC.Get()));
            }
        }

        private void campoDecimalPERCENTUALDESP_Leave(object sender, EventArgs e)
        {
            if ((campoDecimalPERCENTUALDESP.Get() != null) && (campoDecimalPERCENTUALDESP.Get() != 0))
            {
                campoDecimalVALORDESP.Set(CalculaValor( campoDecimalVALORBRUTO.Get(), campoDecimalPERCENTUALDESP.Get() ));
            }
        }

        private void campoDecimalVALORDESP_Leave(object sender, EventArgs e)
        {
            if ((campoDecimalVALORDESP.Get() != null) && (campoDecimalVALORDESP.Get() != 0))
            {
                campoDecimalPERCENTUALDESP.Set( CalculaPercentual( campoDecimalVALORBRUTO.Get(), campoDecimalVALORDESP.Get() ) );
            }
        }

        private void campoDecimalPERCENTUALSEGURO_Leave(object sender, EventArgs e)
        {
            if ((campoDecimalPERCENTUALSEGURO.Get() != null) && (campoDecimalPERCENTUALSEGURO.Get() != 0))
            {
                campoDecimalVALORSEGURO.Set(CalculaValor( campoDecimalVALORBRUTO.Get(), campoDecimalPERCENTUALSEGURO.Get() ));
            }
        }

        private void campoDecimalVALORSEGURO_Leave(object sender, EventArgs e)
        {
            if ((campoDecimalVALORSEGURO.Get() != null) && (campoDecimalVALORSEGURO.Get() != 0))
            {
                campoDecimalPERCENTUALSEGURO.Set(CalculaPercentual(campoDecimalVALORBRUTO.Get(), campoDecimalVALORSEGURO.Get()));
            }
        }

        private void campoDecimalPERCENTUALEXTRA1_Leave(object sender, EventArgs e)
        {
            if ((campoDecimalPERCENTUALEXTRA1.Get() != null) && (campoDecimalPERCENTUALEXTRA1.Get() != 0))
            {
                campoDecimalVALOREXTRA1.Set( CalculaValor( campoDecimalVALORBRUTO.Get(), campoDecimalPERCENTUALEXTRA1.Get() ) );
            }
        }

        private void campoDecimalVALOREXTRA1_Leave(object sender, EventArgs e)
        {
            if ((campoDecimalVALOREXTRA1.Get() != null) && (campoDecimalVALOREXTRA1.Get() != 0))
            {
                campoDecimalPERCENTUALEXTRA1.Set(CalculaPercentual(campoDecimalVALORBRUTO.Get(), campoDecimalVALOREXTRA1.Get()));
            }
        }

        #endregion    

        private bool campoLookup1_SetFormConsulta_1(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1, "SELECT * FROM ZFCFOX WHERE CODCOLIGADA IN (0, ?) AND ENVIAR = 0", new Object[] { AppLib.Context.Empresa });
        }

        private void FormOffOrcamentoCadastro_AposNovo(object sender, EventArgs e)
        {
            campoListaFINANCIADO.Set("NÃO");
            campoListaDEMONSBRINDE.Set("NÃO");
            campoListaPARAFINANC.Set("NÃO");
        }

        private void campoLookupCODCFO_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM ZFCFO WHERE CODCOLIGADA in (0,?) AND CODCFO = ?";
            campoLookupCODCFO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODCFO.Conexao).ExecGetField("", sql, new object[] {AppLib.Context.Empresa, campoLookupCODCFO.Get() }).ToString();
        }

        private void campoLookup1_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM ZFCFOX WHERE CODCOLIGADA = ? AND CODCFO = ? AND IDOFF = ?";
            campoLookup1.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup1.Conexao).ExecGetField("", sql, new object[] {AppLib.Context.Empresa, campoLookupCODCFO.Get(), campoLookup1.Get() }).ToString();
        }

        private void campoLookupCODRPR_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM ZTRPR WHERE CODCOLIGADA = ? AND CODRPR = ?";
            campoLookupCODRPR.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODRPR.Conexao).ExecGetField("", sql, new object[] {AppLib.Context.Empresa, campoLookupCODRPR.Get() }).ToString();
        }

        private void campoLookupTRANSPORTADORA_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM ZTTRA WHERE CODCOLIGADA = ? AND CODTRA = ?";
            campoLookupTRANSPORTADORA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupTRANSPORTADORA.Conexao).ExecGetField("", sql, new object[] {AppLib.Context.Empresa, campoLookupTRANSPORTADORA.Get() }).ToString();
        }

        private void campoLookupTPCULTURA_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT DESCRICAO FROM ZTPCULTURA WHERE CODCOLIGADA = ? AND CODCLIENTE = ?";
            campoLookupTPCULTURA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupTPCULTURA.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupTPCULTURA.Get() }).ToString();
        }
    }
}
