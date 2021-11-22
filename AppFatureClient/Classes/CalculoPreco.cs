using System;
using System.Data;

namespace AppFatureClient.Classes
{
    public class ParametrosPreco
    {
        public String CODDP { get; set; }
        public String PRODUTO { get; set; }
        public String ACABAMENTO { get; set; }
        public String CHAPA { get; set; }
        public Decimal LARGURA { get; set; }
        public Decimal ALTURA { get; set; }
        public Decimal COMPRIMENTO { get; set; }
        public Decimal DISTANCIAMENTO { get; set; }
    }

    public class RetornoPreco
    {
        public String FORMULAORIGINAL { get; set; }
        public String FORMULAFINAL { get; set; }
        public Decimal PRECO { get; set; }

        public string MSG { get; set; }
    }

    public static class CalculoPreco
    {
        public static RetornoPreco CacularPreco(String COD, String TIPOINOX, string _tabelaPreco, string _acabamento, string largura = "", string altura = "", string comprimento = "", string distanciamento = "")
        {
            try
            {
                RetornoPreco retorno = new RetornoPreco();

                #region OLD
                //string sql = String.Format(@"select ACABAMENTO, CHAPA from TPRODUTO TP

                //                                inner join ZTPRODUTOCOMPL ZTC
                //                                on ZTC.CODCOLIGADA = TP.CODCOLPRD
                //                                and ZTC.IDPRD = TP.IDPRD

                //                                where TP.CODCOLPRD = 1 
                //                                and TP.CODIGOPRD = '{0}'", parametros.PRODUTO);

                //sql = String.Format(@"select * from ZTPRODUTOTABPRECO ZTTP

                //                    inner join ZTPRODUTOTABPRECOCOMPL ZTTPC
                //                    on ZTTPC.ID = ZTTP.ID
                //                    and ZTTPC.CODCOLIGADA = ZTTP.CODCOLIGADA

                //                    where ZTTP.CODCOLIGADA = 1 
                //                    and ZTTP.CODIGO = '{0}'
                //                    and ZTTP.ACABAMENTO = '{1}'
                //                    and GETDATE() between ZTTP.INICIOVIGENCIA and ZTTP.FIMVIGENCIA
                //                    and ZTTPC.CHAPA = '{2}'", parametros.PRODUTO, parametros.ACABAMENTO, parametros.CHAPA);
                #endregion

                string sql = String.Format(@"select SITUACAOMERCADORIA, PRECO1, TPRDFISCAL.IDPRD from TPRDFISCAL

                                                inner join TPRODUTO
                                                on TPRODUTO.CODCOLPRD = TPRDFISCAL.CODCOLIGADA
                                                and TPRODUTO.IDPRD = TPRDFISCAL.IDPRD

                                                inner join TPRODUTODEF
                                                on TPRODUTODEF.CODCOLIGADA = TPRODUTO.CODCOLPRD
                                                and TPRODUTODEF.IDPRD = TPRODUTO.IDPRD

                                                where TPRDFISCAL.CODCOLIGADA = 1
                                                and (TPRODUTO.CODIGOPRD = '{0}' or cast(TPRODUTO.IDPRD as varchar) = '{0}')", COD);

                DataTable dt2 = MetodosSQL.GetDT(sql);
                DataTable dt3 = null;

                if (dt2.Rows.Count > 0)
                {
                    sql = $@"select top 1 isnull(USAPRECOFIXO, 0) as 'USAPRECOFIXO', PRECOFIXO 
                            from ZTPRODUTOCOMPL 
                            where CODCOLIGADA = {AppLib.Context.Empresa} 
                            and IDPRD = {(int)dt2.Rows[0]["IDPRD"]}";

                    dt3 = MetodosSQL.GetDT(sql);
                }

                if (dt2.Rows.Count > 0 && dt3.Rows.Count > 0)
                {
                    if ((int)dt3.Rows[0]["USAPRECOFIXO"] == 1)
                    {
                        string codDP = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT C.CODDP FROM ZTPRODUTOCOMPL C WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, COD }).ToString();

                        int regraPrecoFixo = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT USAPRECOFIXO FROM ZTPRODUTOREGRA WHERE CODCOLIGADA = ? AND CODDP = ?", new object[] { AppLib.Context.Empresa, codDP }));

                        if (regraPrecoFixo > 0)
                        {
                            DateTime dataValidade = Convert.ToDateTime(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, @"SELECT DATAVALPRECOFIXO FROM ZTPRODUTOREGRA WHERE CODCOLIGADA = ? AND CODDP = ?", new object[] { AppLib.Context.Empresa, codDP }));

                            if (dataValidade >= DateTime.Now)
                            {
                                retorno.PRECO = (decimal)dt3.Rows[0]["PRECOFIXO"];
                                retorno.FORMULAORIGINAL = "";
                                retorno.FORMULAFINAL = "";
                            }
                            else
                            {
                                retorno.PRECO = 0;
                                retorno.FORMULAORIGINAL = "";
                                retorno.FORMULAFINAL = "";
                            }
                        }
                        else
                        {
                            retorno.PRECO = (decimal)dt3.Rows[0]["PRECOFIXO"];
                            retorno.FORMULAORIGINAL = "";
                            retorno.FORMULAFINAL = "";
                        }
                    }
                    else
                    {
                        AtualizaTabelaPreco(_tabelaPreco, COD);

                        AtualizaAcabamento(_acabamento, COD);

                        sql = String.Format(@"select ZTTPC.PESO, PRECOKG, ZTP.FORMULAPRECO, ZTC.LARGURA, ZTC.ALTURA, ZTC.COMPRIMENTO, ZTC.DISTANCIAMENTO from TPRODUTO TP

                                                    inner join ZTPRODUTOCOMPL ZTC
                                                    on ZTC.CODCOLIGADA = TP.CODCOLPRD
                                                    and ZTC.IDPRD = TP.IDPRD

                                                    inner join ZTPRODUTOTABPRECO ZTTP
                                                    on ZTTP.CODCOLIGADA = TP.CODCOLPRD
                                                    and ZTTP.CODIGO = ZTC.TABELAPRECO
                                                    and ZTTP.ACABAMENTO = ZTC.ACABAMENTO

                                                    inner join ZTPRODUTOTABPRECOCOMPL ZTTPC
                                                    on ZTTPC.ID = ZTTP.ID
                                                    and ZTTPC.CODCOLIGADA = ZTTP.CODCOLIGADA
                                                    and ZTTPC.CHAPA = ZTC.CHAPA

                                                    inner join ZTPRODUTOREGRA ZTP
                                                    on ZTP.CODCOLIGADA = TP.CODCOLPRD
                                                    and ZTP.CODDP = ZTC.CODDP

                                                    where TP.CODCOLPRD = 1
                                                    and GETDATE() between ZTTP.INICIOVIGENCIA and ZTTP.FIMVIGENCIA
                                                    and (TP.CODIGOPRD = '{0}' or cast(TP.IDPRD as varchar) = '{0}')
													and ZTTP.TIPO = (case when ZTC.ACABAMENTO = 'INOX' then '{1}' else ZTTP.TIPO end)", COD, TIPOINOX);

                        dt2 = MetodosSQL.GetDT(sql);

                        decimal peso = 0;
                        decimal preco = 0;
                        string formula = String.Empty;

                        decimal LARGURA = 0;
                        decimal ALTURA = 0;
                        decimal COMPRIMENTO = 0;
                        decimal DISTANCIAMENTO = 0;

                        foreach (DataRow tabPreco in dt2.Rows)
                        {
                            peso = (Decimal)tabPreco["PESO"];
                            preco = (Decimal)tabPreco["PRECOKG"];
                            formula = (String)tabPreco["FORMULAPRECO"];

                            if (string.IsNullOrEmpty(largura) || string.IsNullOrEmpty(altura) || string.IsNullOrEmpty(comprimento) || string.IsNullOrEmpty(distanciamento))
                            {
                                LARGURA = (Decimal)tabPreco["LARGURA"];
                                ALTURA = (Decimal)tabPreco["ALTURA"];
                                COMPRIMENTO = (Decimal)tabPreco["COMPRIMENTO"];
                                DISTANCIAMENTO = (int)tabPreco["DISTANCIAMENTO"];
                            }
                            else
                            {
                                LARGURA = Convert.ToDecimal(largura);
                                ALTURA = Convert.ToDecimal(altura);
                                COMPRIMENTO = Convert.ToDecimal(comprimento);

                                if (!distanciamento.Equals("ecione"))
                                {
                                    DISTANCIAMENTO = Convert.ToDecimal(distanciamento);
                                }                       
                            }
                        }

                        retorno.FORMULAORIGINAL = formula;

                        formula = formula.Replace("PS", peso.ToString());

                        formula = formula.Replace("PR", preco.ToString());

                        formula = formula.Replace("A", LARGURA.ToString());

                        formula = formula.Replace("B", ALTURA.ToString());


                        formula = formula.Replace("C", COMPRIMENTO.ToString());

                        formula = formula.Replace("D", DISTANCIAMENTO.ToString());

                        // Atribui os resultados para as respectivas variáveis.
                        retorno.FORMULAFINAL = formula;

                        if (String.IsNullOrWhiteSpace(formula))
                        {
                            retorno.PRECO = 0;
                        }
                        else
                        {
                            retorno.PRECO = Convert.ToDecimal(Tools.Calc(formula));
                        }
                    }
                }

                // Verifica a tabela ZPARAMFATURE para validar se o preço será setado com ou sem valor
                int usaPrecoCalculado = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT USAPRECOCALCULADO FROM ZPARAMFATURE WHERE CODCOLIGADA = ?", new object[] { AppLib.Context.Empresa }));

                if (usaPrecoCalculado == 0)
                {
                    retorno.PRECO = 0;
                }
                else
                {
                    if (dt2.Rows.Count == 0)
                    {
                        if (!IsDataVigenciaValida(COD, TIPOINOX))
                        {
                            retorno.MSG = "Preço não calculado.\r\nVerificar especificações do produto e/ou tabela de preço.";
                        }
                    }

                    else
                    {
                        retorno.MSG = "";
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static RetornoPreco CarregaValoresByParametros(string _idPRD, string _tipoInox)
        {
            string sql = String.Format(@"SELECT ZTTPC.PESO, PRECOKG, ZTP.FORMULAPRECO, ZTC.LARGURA, ZTC.ALTURA, ZTC.COMPRIMENTO, ZTC.DISTANCIAMENTO
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
                                        AND CAST(TP.IDPRD AS VARCHAR) = '{0}' 
                                        AND ZTTP.TIPO = (CASE WHEN ZTC.ACABAMENTO = 'INOX' THEN '{1}' ELSE ZTTP.TIPO END) ", _idPRD, _tipoInox);

            DataTable dtValores = MetodosSQL.GetDT(sql);

            RetornoPreco retorno = new RetornoPreco();

            decimal peso = 0;
            decimal preco = 0;
            string formula = String.Empty;

            decimal LARGURA = 0;
            decimal ALTURA = 0;
            decimal COMPRIMENTO = 0;
            decimal DISTANCIAMENTO = 0;

            foreach (DataRow tabPreco in dtValores.Rows)
            {
                peso = (Decimal)tabPreco["PESO"];
                preco = (Decimal)tabPreco["PRECOKG"];
                formula = (String)tabPreco["FORMULAPRECO"];

                LARGURA = (Decimal)tabPreco["LARGURA"];
                ALTURA = (Decimal)tabPreco["ALTURA"];
                COMPRIMENTO = (Decimal)tabPreco["COMPRIMENTO"];
                DISTANCIAMENTO = (int)tabPreco["DISTANCIAMENTO"];
            }

            retorno.FORMULAORIGINAL = formula;

            formula = formula.Replace("PS", peso.ToString());

            formula = formula.Replace("PR", preco.ToString());

            formula = formula.Replace("A", LARGURA.ToString());

            formula = formula.Replace("B", ALTURA.ToString());

            formula = formula.Replace("C", COMPRIMENTO.ToString());

            formula = formula.Replace("D", DISTANCIAMENTO.ToString());

            if (String.IsNullOrWhiteSpace(formula))
            {
                retorno.PRECO = 0;
            }
            else
            {
                retorno.PRECO = Convert.ToDecimal(Tools.Calc(formula));
            }

            // Atribui os resultados para as respectivas variáveis.
            retorno.FORMULAFINAL = formula;

            return retorno;
        }

        #region Métodos

        public static bool IsDataVigenciaValida(string _idPRD, string _tipoInox)
        {
            #region Código comentado

            //string sql = String.Format(@"SELECT ZTTP.INICIOVIGENCIA, ZTTP.FIMVIGENCIA
            //                            FROM TPRODUTO TP

            //                            INNER JOIN ZTPRODUTOCOMPL ZTC
            //                            ON ZTC.CODCOLIGADA = TP.CODCOLPRD
            //                            AND ZTC.IDPRD = TP.IDPRD

            //                            INNER JOIN ZTPRODUTOTABPRECO ZTTP
            //                            ON ZTTP.CODCOLIGADA = TP.CODCOLPRD
            //                            AND ZTTP.CODIGO = ZTC.TABELAPRECO
            //                            AND ZTTP.ACABAMENTO = ZTC.ACABAMENTO

            //                            INNER JOIN ZTPRODUTOTABPRECOCOMPL ZTTPC
            //                            ON ZTTPC.ID = ZTTP.ID
            //                            AND ZTTPC.CODCOLIGADA = ZTTP.CODCOLIGADA
            //                            AND ZTTPC.CHAPA = ZTC.CHAPA

            //                            INNER JOIN ZTPRODUTOREGRA ZTP
            //                            ON ZTP.CODCOLIGADA = TP.CODCOLPRD
            //                            AND ZTP.CODDP = ZTC.CODDP

            //                            WHERE TP.CODCOLPRD = 1 
            //                            AND CAST(TP.IDPRD AS VARCHAR) = '{0}' 
            //                            AND ZTTP.TIPO = (CASE WHEN ZTC.ACABAMENTO = 'INOX' THEN '{1}' ELSE ZTTP.TIPO END)", _idPRD, _tipoInox);

            #endregion

            string sql = String.Format(@"SELECT ZTTP.INICIOVIGENCIA, ZTTP.FIMVIGENCIA
                                        FROM TPRODUTO TP

                                        INNER JOIN ZTPRODUTOCOMPL ZTC
                                        ON ZTC.CODCOLIGADA = TP.CODCOLPRD
                                        AND ZTC.IDPRD = TP.IDPRD

                                        INNER JOIN ZTPRODUTOTABPRECO ZTTP
                                        ON ZTTP.CODCOLIGADA = TP.CODCOLPRD
                                        AND ZTTP.CODIGO = ZTC.TABELAPRECO
                                        AND ZTTP.ACABAMENTO = ZTC.ACABAMENTO

                                        WHERE TP.CODCOLPRD = 1 
                                        AND CAST(TP.IDPRD AS VARCHAR) = '{0}' 
                                        AND ZTTP.TIPO = (CASE WHEN ZTC.ACABAMENTO = 'INOX' THEN '{1}' ELSE ZTTP.TIPO END)", _idPRD, _tipoInox);

            DataTable dtRetornoValidao = MetodosSQL.GetDT(sql);

            if (dtRetornoValidao.Rows.Count > 0)
            {
                if (Convert.ToDateTime(dtRetornoValidao.Rows[0]["FIMVIGENCIA"]) < DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public static void AtualizaTabelaPreco(string _tabelaPreco, string _idPRD)
        {
            if (string.IsNullOrEmpty(_tabelaPreco))
            {
                _tabelaPreco = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"select ZTC.TABELAPRECO FROM TPRODUTO TP
                                                                                                            inner join ZTPRODUTOCOMPL ZTC
                                                                                                            on ZTC.CODCOLIGADA = TP.CODCOLPRD
                                                                                                            and ZTC.IDPRD = TP.IDPRD

                                                                                                            inner join ZTPRODUTOTABPRECO ZTTP
                                                                                                            on ZTTP.CODCOLIGADA = TP.CODCOLPRD
                                                                                                            and ZTTP.CODIGO = ZTC.TABELAPRECO
                                                                                                            and ZTTP.ACABAMENTO = ZTC.ACABAMENTO
                                                                                                            
                                                                                                            WHERE CAST(TP.IDPRD AS VARCHAR) = ? AND ZTC.CODCOLIGADA = ?", new object[] { _idPRD, AppLib.Context.Empresa }).ToString();
            }

            AppLib.Context.poolConnection.Get("Start").ExecQuery("UPDATE ZTPRODUTOCOMPL SET TABELAPRECO = ? WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { _tabelaPreco, AppLib.Context.Empresa, _idPRD });
        }

        public static void AtualizaAcabamento(string _acabamento, string _idPRD)
        {
            if (string.IsNullOrEmpty(_acabamento))
            {
                _acabamento = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"select ZTC.ACABAMENTO FROM TPRODUTO TP
                                                                                                            inner join ZTPRODUTOCOMPL ZTC
                                                                                                            on ZTC.CODCOLIGADA = TP.CODCOLPRD
                                                                                                            and ZTC.IDPRD = TP.IDPRD

                                                                                                            inner join ZTPRODUTOTABPRECO ZTTP
                                                                                                            on ZTTP.CODCOLIGADA = TP.CODCOLPRD
                                                                                                            and ZTTP.CODIGO = ZTC.TABELAPRECO
                                                                                                            and ZTTP.ACABAMENTO = ZTC.ACABAMENTO
                                                                                                            
                                                                                                            WHERE TP.IDPRD = ? AND ZTC.CODCOLIGADA = ?", new object[] { _idPRD, AppLib.Context.Empresa }).ToString();
            }

            AppLib.Context.poolConnection.Get("Start").ExecQuery("UPDATE ZTPRODUTOCOMPL SET ACABAMENTO = ? WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { _acabamento, AppLib.Context.Empresa, _idPRD });
        }

        #endregion

    }
}
