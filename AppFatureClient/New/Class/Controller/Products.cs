using AppFatureClient.Classes;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace AppFatureClient.New.Class.Controller
{
    public class Products
    {
        #region Construtor

        public Products()
        {

        }

        #endregion

        #region Métodos

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal calcularPrecoVigente(int _idPrd, string _tipo)
        {
            #region Variáveis para as especificações do produto, resposáveis pelo cálculo do preço acabado

            decimal peso = 0;
            decimal preco = 0;
            string formula = "";

            decimal largura = 0;
            decimal altura = 0;
            decimal comprimento = 0;
            decimal distanciamento = 0;

            #endregion

            decimal precoCalculado = 0;

            DataTable dtSituacaoMercadoria = null;
            DataTable dtPrecoFixo = null;
            DataTable dtEspecificacoes = null;

            try
            {
                dtSituacaoMercadoria = getDataTableSituacaoMercadoria(_idPrd);

                // Verifica se existem registros referentes à situação da mercadoria. Caso existam, então a preenche o DataTable referente ao preço fixo.
                if (dtSituacaoMercadoria != null)
                {
                    dtPrecoFixo = getDataTablePrecoFixo(_idPrd);
                }

                if (dtPrecoFixo != null)
                {
                    if (Convert.ToInt32(dtPrecoFixo.Rows[0]["USAPRECOFIXO"]) == 1)
                    {
                        precoCalculado = Convert.ToDecimal(dtPrecoFixo.Rows[0]["PRECOFIXO"]);
                    }
                    else
                    {
                        dtEspecificacoes = getDataTableEspecificacoes(_idPrd, _tipo);

                        if (dtEspecificacoes != null)
                        {
                            foreach (DataRow row in dtEspecificacoes.Rows)
                            {
                                peso = Convert.ToDecimal(row["PESO"]);
                                preco = Convert.ToDecimal(row["PRECOKG"]);
                                formula = row["FORMULAPRECO"].ToString();
                                largura = Convert.ToDecimal(row["LARGURA"]);
                                altura = Convert.ToDecimal(row["ALTURA"]);
                                comprimento = Convert.ToDecimal(row["COMPRIMENTO"]);
                                distanciamento = Convert.ToDecimal(row["DISTANCIAMENTO"]);

                                // Formata a fórmula de modo que os parâmetros são substituídos pelos valores.
                                formula = getFormulaFormatada(formula, peso, preco, largura, altura, comprimento, distanciamento);

                                // Executa o cálculo
                                precoCalculado = Convert.ToDecimal(Tools.Calc(formula));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Não foi possível efetuar o cálculo do preço vigente. Detalhes: \r\n: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return precoCalculado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_idPrd"></param>
        /// <returns></returns>
        private DataTable getDataTableSituacaoMercadoria(int _idPrd)
        {
            string sql = "";

            DataTable dt = null;

            sql = String.Format(@"SELECT SITUACAOMERCADORIA, PRECO1, TPRDFISCAL.IDPRD FROM TPRDFISCAL
                                    INNER JOIN TPRODUTO
                                    ON TPRODUTO.CODCOLPRD = TPRDFISCAL.CODCOLIGADA
                                    AND TPRODUTO.IDPRD = TPRDFISCAL.IDPRD

                                    INNER JOIN TPRODUTODEF
                                    ON TPRODUTODEF.CODCOLIGADA = TPRODUTO.CODCOLPRD
                                    AND TPRODUTODEF.IDPRD = TPRODUTO.IDPRD

                                    WHERE TPRDFISCAL.CODCOLIGADA = {0} AND TPRODUTO.IDPRD = {1}", AppLib.Context.Empresa, _idPrd);

            dt = MetodosSQL.GetDT(sql);

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_idPrd"></param>
        /// <returns></returns>
        private DataTable getDataTablePrecoFixo(int _idPrd)
        {
            string sql = "";

            DataTable dt = null;

            sql = String.Format(@"SELECT TOP 1 ISNULL(USAPRECOFIXO, 0) AS 'USAPRECOFIXO', PRECOFIXO 
                                    FROM ZTPRODUTOCOMPL 

                                    WHERE CODCOLIGADA = {0} AND IDPRD = {1}", AppLib.Context.Empresa, _idPrd);

            dt = MetodosSQL.GetDT(sql);

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_idPrd"></param>
        /// <returns></returns>
        private DataTable getDataTableEspecificacoes(int _idPrd, string _tipo)
        {
            string sql = "";

            DataTable dt = null;

            sql = String.Format(@"SELECT ZTTPC.PESO, PRECOKG, ZTP.FORMULAPRECO, ZTC.LARGURA, ZTC.ALTURA, ZTC.COMPRIMENTO, ZTC.DISTANCIAMENTO 
                                    FROM TPRODUTO TP

                                    INNER JOIN ZTPRODUTOCOMPL ZTC
                                    ON ZTC.CODCOLIGADA = TP.CODCOLPRD
                                    AND ZTC.IDPRD = TP.IDPRD

                                    INNER JOIN ZTPRODUTOTABPRECO ZTTP
                                    ON ZTTP.CODCOLIGADA = TP.CODCOLPRD
                                    AND ZTTP.CODIGO = ZTC.TABELAPRECO
                                    AND ZTTP.ACABAMENTO = ZTC.ACABAMENTO

                                    INNER JOIN ZTPRODUTOTABPRECOCOMPL ZTTPC
                                    ON ZTTPC.ID = ZTTP.ID
                                    AND ZTTPC.CODCOLIGADA = ZTTP.CODCOLIGADA
                                    AND ZTTPC.CHAPA = ZTC.CHAPA

                                    INNER JOIN ZTPRODUTOREGRA ZTP
                                    ON ZTP.CODCOLIGADA = TP.CODCOLPRD
                                    AND ZTP.CODDP = ZTC.CODDP

                                    WHERE TP.CODCOLPRD = 1
                                    AND GETDATE() BETWEEN ZTTP.INICIOVIGENCIA AND ZTTP.FIMVIGENCIA
                                    AND TP.IDPRD  = {0}
									AND ZTTP.TIPO = (CASE WHEN ZTC.ACABAMENTO = 'INOX' THEN '{1}' ELSE ZTTP.TIPO END)", _idPrd, _tipo);

            dt = MetodosSQL.GetDT(sql);

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_formula"></param>
        /// <param name="_peso"></param>
        /// <param name="_preco"></param>
        /// <param name="_largura"></param>
        /// <param name="_altura"></param>
        /// <param name="_comprimento"></param>
        /// <param name="_distanciamento"></param>
        /// <returns></returns>
        private string getFormulaFormatada(string _formula, decimal _peso, decimal _preco, decimal _largura, decimal _altura, decimal _comprimento, decimal _distanciamento)
        {
            return _formula.Replace("PS", _peso.ToString()).Replace("PR", _preco.ToString()).
                            Replace("A", _largura.ToString()).
                            Replace("B", _altura.ToString()).
                            Replace("C", _comprimento.ToString()).
                            Replace("D", _distanciamento.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_idPRD"></param>
        /// <param name="_precoCalculado"></param>
        public void atualizaPrecoAcabado(int _idPRD, decimal _precoCalculado)
        {
            try
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE ZTPRODUTOCOMPL SET PRECOCALCULADO = ? WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { _precoCalculado, AppLib.Context.Empresa, _idPRD });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Não foi possível atualizar o preço acabado. Detalhes: \r\n: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        public decimal consultaPrecoCalculado(int _idPRD)
        {
            decimal precoCalculado = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT PRECOCALCULADO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, _idPRD }));

            return precoCalculado;
        }

        #endregion
    }
}
