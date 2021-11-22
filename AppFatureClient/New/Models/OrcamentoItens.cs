using AppFatureClient.Classes;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;

namespace AppFatureClient.New.Models
{
    public class OrcamentoItens
    {
        #region Variáveis

        public DataTable TabelaItens = new DataTable();
        public TITMMOV item;
        public List<TITMMOV> itens;
        public List<ItemComposicao> itensComposicao;
        private New.Class.Utilities util = new Class.Utilities();

        #endregion

        #region Construtor

        public OrcamentoItens()
        {
            ConfiguraColunas(TabelaItens);
        }

        #endregion

        #region Métodos

        #region Manipulação da GridView

        public DataTable GetTabela()
        {
            return TabelaItens;
        }

        public DataTable EspelhaGridView(DataTable dt)
        {
            TabelaItens = dt;

            return TabelaItens;
        }

        /// <summary>
        /// Carrega os itens do Orçamento
        /// </summary>
        /// <param name="IDMOV">ID do Movimento</param>
        public void CarregaGridViewItens(int? IDMOV = null)
        {
            if (IDMOV != null)
            {
                string Comando = "";

                Comando = @"SELECT
TITMMOV.NSEQITMMOV,
TITMMOV.NUMEROSEQUENCIAL,
TITMMOV.IDPRD,
TPRD.CODIGOAUXILIAR,
TPRD.CODIGOPRD,
ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
TITMMOV.CODUND,
CAST(TITMMOV.QUANTIDADETOTAL AS INT) QUANTIDADE,
CAST(TITMMOV.PRECOUNITARIO AS NUMERIC(15,2)) PRECOUNITARIO,
TPRD.NUMEROCCF,

CAST(ISNULL(TTRBMOV.ALIQUOTA,0) AS NUMERIC (15,2)) ALIQUOTA,
TITMMOVHISTORICO.HISTORICOLONGO,
TITMMOV.PRECOUNITARIOSELEC,

TITMMOVCOMPL.APLICPROD,
TITMMOV.IDNAT,
TITMMOV.DATAENTREGA,
TITMMOVFISCAL.NUMPEDIDO,
TITMMOVFISCAL.NUMITEMPEDIDO,
TITMMOVCOMPL.TIPO,
TITMMOVCOMPL.VALORPINTURA,
TITMMOVCOMPL.TIPOPINTURA,
TITMMOVCOMPL.CORPINTURA,
TITMMOVCOMPL.AJUSTEVALOR,
TITMMOVCOMPL.TIPOAJUSTEVALOR,
TITMMOVCOMPL.PRECOTABELA,
TITMMOVCOMPL.DISTANCIAMENTO,
TITMMOV.VALORTOTALITEM,
ISNULL(TITMMOVCOMPL.VALORDESCORIGINAL,0) AS 'VALORDESCORIGINAL', 
ISNULL(TITMMOVCOMPL.VALORDESPORIGINAL,0) AS 'VALORDESPORIGINAL', 

(SELECT CODNAT FROM DCFOP WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND ATIVO = 1 AND FISCAL = 1 AND IDNAT = TITMMOV.IDNAT) AS 'NUMEROCFOP'

FROM TITMMOV

LEFT JOIN TITMMOVFISCAL
ON TITMMOVFISCAL.CODCOLIGADA = TITMMOV.CODCOLIGADA
AND TITMMOVFISCAL.IDMOV = TITMMOV.IDMOV
AND TITMMOVFISCAL.NSEQITMMOV = TITMMOV.NSEQITMMOV

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

                Comando = AppLib.Context.poolConnection.Get("Start").ParseCommand(Comando, new Object[] { AppLib.Context.Empresa, IDMOV });
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(Comando, new Object[] { });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TabelaItens.Rows.Add();

                    TabelaItens.Rows[i]["NSEQITMMOV"] = dt.Rows[i]["NSEQITMMOV"];
                    TabelaItens.Rows[i]["NUMEROSEQUENCIAL"] = dt.Rows[i]["NUMEROSEQUENCIAL"];
                    TabelaItens.Rows[i]["CODAUXILIAR"] = dt.Rows[i]["CODIGOAUXILIAR"];
                    TabelaItens.Rows[i]["PRODUTO"] = dt.Rows[i]["PRODUTO"];
                    TabelaItens.Rows[i]["UNIDADE"] = dt.Rows[i]["CODUND"];
                    TabelaItens.Rows[i]["QUANTIDADE"] = dt.Rows[i]["QUANTIDADE"];
                    TabelaItens.Rows[i]["PRECOTABELA"] = dt.Rows[i]["PRECOTABELA"];
                    TabelaItens.Rows[i]["VALORPINTURA"] = dt.Rows[i]["VALORPINTURA"];
                    TabelaItens.Rows[i]["AJUSTEVALOR"] = dt.Rows[i]["AJUSTEVALOR"];
                    TabelaItens.Rows[i]["PRECOUNITARIO"] = dt.Rows[i]["PRECOUNITARIO"];
                    TabelaItens.Rows[i]["VALORTOTAL"] = dt.Rows[i]["VALORTOTALITEM"];
                    TabelaItens.Rows[i]["NUMEROCCF"] = dt.Rows[i]["NUMEROCCF"];
                    TabelaItens.Rows[i]["DATAENTREGA"] = dt.Rows[i]["DATAENTREGA"];
                    TabelaItens.Rows[i]["NUMEROCFOP"] = dt.Rows[i]["NUMEROCFOP"];
                    TabelaItens.Rows[i]["VALORDESCORIGINAL"] = dt.Rows[i]["VALORDESCORIGINAL"];
                    TabelaItens.Rows[i]["VALORDESPORIGINAL"] = dt.Rows[i]["VALORDESPORIGINAL"];
                    TabelaItens.Rows[i]["IDPRD"] = dt.Rows[i]["IDPRD"];
                    TabelaItens.Rows[i]["CODIGOPRD"] = dt.Rows[i]["CODIGOPRD"];
                    TabelaItens.Rows[i]["ALIQUOTAIPI"] = dt.Rows[i]["ALIQUOTA"];
                    TabelaItens.Rows[i]["HISTORICOLONGO"] = dt.Rows[i]["HISTORICOLONGO"];
                    TabelaItens.Rows[i]["PRECOUNITARIOSELEC"] = dt.Rows[i]["PRECOUNITARIOSELEC"];
                    TabelaItens.Rows[i]["IDNAT"] = dt.Rows[i]["IDNAT"];
                    TabelaItens.Rows[i]["APLICACAO"] = dt.Rows[i]["APLICPROD"];
                    TabelaItens.Rows[i]["TIPOINOX"] = dt.Rows[i]["TIPO"];
                    TabelaItens.Rows[i]["TIPOPINTURA"] = dt.Rows[i]["TIPOPINTURA"];
                    TabelaItens.Rows[i]["CORPINTURA"] = dt.Rows[i]["CORPINTURA"];
                    TabelaItens.Rows[i]["TIPOAJUSTEVALOR"] = dt.Rows[i]["TIPOAJUSTEVALOR"];
                    TabelaItens.Rows[i]["NUMPEDIDO"] = dt.Rows[i]["NUMPEDIDO"];
                    TabelaItens.Rows[i]["NUMITEMPEDIDO"] = dt.Rows[i]["NUMITEMPEDIDO"];
                    TabelaItens.Rows[i]["DISTANCIAMENTO"] = dt.Rows[i]["DISTANCIAMENTO"];
                }

                for (int i = TabelaItens.Rows.Count - 1; i >= 0; i--)
                {
                    if (TabelaItens.Rows[i][1] == DBNull.Value)
                    {
                        TabelaItens.Rows[i].Delete();
                    }
                }

                TabelaItens.AcceptChanges();

                for (int i = 0; i < TabelaItens.Rows.Count; i++)
                {
                    String sql = String.Format(@"SELECT ISNULL(X.SALDO_FISICO, 0) AS 'SALDO_FISICO', 
ISNULL(Y.SALDO_PEDIDO, 0) AS 'SALDO_PEDIDO', 
ISNULL(X.SALDO_FISICO, 0)-ISNULL(Y.SALDO_PEDIDO, 0) AS 'SALDO_DISPONIVEL' 

FROM TPRODUTO

LEFT JOIN (SELECT IDPRD, SUM(TITMMOV.QUANTIDADE) SALDO_PEDIDO
FROM TMOV
INNER JOIN TITMMOV
ON TITMMOV.CODCOLIGADA = TMOV.CODCOLIGADA
AND TITMMOV.IDMOV = TMOV.IDMOV 
WHERE TMOV.CODCOLIGADA = 1
AND TMOV.STATUS IN ('A','G')
AND TMOV.CODTMV IN ('2.1.10','2.1.15')
AND TITMMOV.QUANTIDADE > 0
GROUP BY IDPRD) Y
ON Y.IDPRD = TPRODUTO.IDPRD

LEFT JOIN (SELECT IDPRD, SALDOFISICO2 SALDO_FISICO
FROM TPRDLOC 
WHERE CODCOLIGADA = 1) X
ON X.IDPRD = TPRODUTO.IDPRD

WHERE TPRODUTO.IDPRD = {0}", Convert.ToInt32(TabelaItens.Rows[i]["IDPRD"]));

                    System.Data.DataTable dtSaldo = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new Object[] { });

                    for (int j = 0; j < dtSaldo.Rows.Count; j++)
                    {
                        TabelaItens.Rows[i]["SALDO_PEDIDO"] = dtSaldo.Rows[j]["SALDO_PEDIDO"];
                        TabelaItens.Rows[i]["SALDO_FISICO"] = dtSaldo.Rows[j]["SALDO_FISICO"];
                        TabelaItens.Rows[i]["SALDO_DISPONIVEL"] = dtSaldo.Rows[j]["SALDO_DISPONIVEL"];
                    }
                }
            }
            else
            {
                TabelaItens.Rows.Add();
            }

            DataView dv = TabelaItens.DefaultView;

            if (!string.IsNullOrEmpty(util.GetOrdenacaoVisaoUsuario()))
            {
                dv.Sort = util.GetOrdenacaoVisaoUsuario();
            }
            else
            {
                dv.Sort = "NUMEROSEQUENCIAL ASC";
            }

            TabelaItens = dv.ToTable();
        }

        /// <summary>
        /// Atualiza a Grid conforme atualização dos itens
        /// </summary>
        /// <param name="itens"></param>
        /// <returns></returns>
        public DataTable AtualizaGridView(List<TITMMOV> itens)
        {
            DataTable dt = GetTabela();

            for (int i = 0; i < itens.Count; i++)
            {
                if (dt.Rows.Count < itens.Count)
                {
                    dt.Rows.Add();
                }

                dt.Rows[i]["NSEQITMMOV"] = itens[i].NSEQITEMMOV;
                dt.Rows[i]["NUMEROSEQUENCIAL"] = itens[i].NUMEROSEQUENCIAL;
                dt.Rows[i]["CODAUXILIAR"] = itens[i].CODAUXILIAR;
                dt.Rows[i]["PRODUTO"] = itens[i].PRODUTO;
                dt.Rows[i]["PRODUTO"] = itens[i].PRODUTO;
                dt.Rows[i]["QUANTIDADE"] = itens[i].QUANTIDADE;
                dt.Rows[i]["PRECOTABELA"] = itens[i].PRECOTABELA;
                dt.Rows[i]["VALORPINTURA"] = itens[i].VALORPINTURA;
                dt.Rows[i]["AJUSTEVALOR"] = itens[i].AJUSTEVALOR;
                dt.Rows[i]["PRECOUNITARIO"] = itens[i].PRECOUNITARIO;
                dt.Rows[i]["VALORTOTAL"] = itens[i].VALORTOTAL;
                dt.Rows[i]["NUMEROCCF"] = itens[i].NUMEROCCF;

                if (itens[i].DATAENTREGA != null)
                {
                    dt.Rows[i]["DATAENTREGA"] = itens[i].DATAENTREGA;
                }

                dt.Rows[i]["NUMEROCFOP"] = itens[i].NUMEROCFOP;
                dt.Rows[i]["VALORDESCORIGINAL"] = itens[i].VALORDESCORIGINAL;
                dt.Rows[i]["VALORDESPORIGINAL"] = itens[i].VALORDESPORIGINAL;
                dt.Rows[i]["IDPRD"] = itens[i].IDPRD;
                dt.Rows[i]["CODIGOPRD"] = itens[i].CODIGOPRD;
                dt.Rows[i]["ALIQUOTAIPI"] = itens[i].ALIQUOTAIPI;
                dt.Rows[i]["HISTORICOLONGO"] = itens[i].HISTORICOLONGO;

                if (!string.IsNullOrEmpty(itens[i].PRECOUNITARIOSELEC))
                {
                    dt.Rows[i]["PRECOUNITARIOSELEC"] = itens[i].PRECOUNITARIOSELEC.ToString();
                }

                dt.Rows[i]["UNIDADE"] = itens[i].UNIDADE;
                dt.Rows[i]["IDNAT"] = itens[i].IDNAT;
                dt.Rows[i]["APLICACAO"] = itens[i].APLICACAO;
                dt.Rows[i]["TIPOINOX"] = itens[i].TIPOINOX;
                dt.Rows[i]["TIPOPINTURA"] = itens[i].TIPOPINTURA;
                dt.Rows[i]["CORPINTURA"] = itens[i].CORPINTURA;
                dt.Rows[i]["TIPOAJUSTEVALOR"] = itens[i].TIPOAJUSTEVALOR;
                dt.Rows[i]["NUMPEDIDO"] = itens[i].NUMPEDIDO;
                dt.Rows[i]["NUMITEMPEDIDO"] = itens[i].NUMITEMPEDIDO;
                dt.Rows[i]["DISTANCIAMENTO"] = itens[i].DISTANCIAMENTO;
            }

            DataView dv = TabelaItens.DefaultView;

            if (!string.IsNullOrEmpty(util.GetOrdenacaoVisaoUsuario()))
            {
                dv.Sort = util.GetOrdenacaoVisaoUsuario();
            }
            else
            {
                dv.Sort = "NUMEROSEQUENCIAL ASC";
            }

            DataTable dtOrdenado = dv.ToTable();

            return dtOrdenado;
        }

        /// <summary>
        /// Configura o tamanho das colunas e desabilita sua edição
        /// </summary>
        /// <param name="gridView"></param>
        public void ConfiguraColunasGridView(GridView gridView)
        {
            gridView.Columns["NSEQITMMOV"].OptionsColumn.AllowEdit = false;
            //gridView.Columns["NUMEROSEQUENCIAL"].OptionsColumn.AllowEdit = false;
            gridView.Columns["CODAUXILIAR"].OptionsColumn.AllowEdit = false;
            //gridView.Columns["QUANTIDADE"].OptionsColumn.AllowEdit = false;
            gridView.Columns["PRODUTO"].OptionsColumn.AllowEdit = false;
            gridView.Columns["UNIDADE"].OptionsColumn.AllowEdit = false;
            gridView.Columns["VALORPINTURA"].OptionsColumn.AllowEdit = false;
            gridView.Columns["AJUSTEVALOR"].OptionsColumn.AllowEdit = false;
            gridView.Columns["PRECOUNITARIO"].OptionsColumn.AllowEdit = false;
            gridView.Columns["VALORTOTAL"].OptionsColumn.AllowEdit = false;
            gridView.Columns["NUMEROCCF"].OptionsColumn.AllowEdit = false;
            gridView.Columns["DATAENTREGA"].OptionsColumn.AllowEdit = false;
            gridView.Columns["SALDO_FISICO"].OptionsColumn.AllowEdit = false;
            gridView.Columns["SALDO_PEDIDO"].OptionsColumn.AllowEdit = false;
            gridView.Columns["SALDO_DISPONIVEL"].OptionsColumn.AllowEdit = false;
            gridView.Columns["NUMEROCFOP"].OptionsColumn.AllowEdit = false;
            gridView.Columns["IDPRD"].OptionsColumn.AllowEdit = false;
            gridView.Columns["CODIGOPRD"].OptionsColumn.AllowEdit = false;
            gridView.Columns["ALIQUOTAIPI"].OptionsColumn.AllowEdit = false;
            //gridView.Columns["HISTORICOLONGO"].OptionsColumn.AllowEdit = false;
            gridView.Columns["PRECOUNITARIOSELEC"].OptionsColumn.AllowEdit = false;
            gridView.Columns["IDNAT"].OptionsColumn.AllowEdit = false;
            gridView.Columns["APLICACAO"].OptionsColumn.AllowEdit = false;
            gridView.Columns["TIPOINOX"].OptionsColumn.AllowEdit = false;
            gridView.Columns["TIPOPINTURA"].OptionsColumn.AllowEdit = false;
            gridView.Columns["CORPINTURA"].OptionsColumn.AllowEdit = false;
            gridView.Columns["VALORDESCORIGINAL"].OptionsColumn.AllowEdit = false;
            gridView.Columns["VALORDESPORIGINAL"].OptionsColumn.AllowEdit = false;
            gridView.Columns["TIPOAJUSTEVALOR"].OptionsColumn.AllowEdit = false;
            //gridView.Columns["NUMPEDIDO"].OptionsColumn.AllowEdit = false;
            //gridView.Columns["NUMITEMPEDIDO"].OptionsColumn.AllowEdit = false;
            gridView.Columns["DISTANCIAMENTO"].OptionsColumn.AllowEdit = false;

            gridView.Columns["PRECOTABELA"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns["PRECOTABELA"].DisplayFormat.FormatString = "n2";
            gridView.Columns["PRECOTABELA"].AppearanceCell.TextOptions.RightToLeft = true;

            gridView.Columns["SALDO_FISICO"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns["SALDO_FISICO"].DisplayFormat.FormatString = "n2";
            gridView.Columns["SALDO_FISICO"].AppearanceCell.TextOptions.RightToLeft = true;

            gridView.Columns["SALDO_PEDIDO"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns["SALDO_PEDIDO"].DisplayFormat.FormatString = "n2";
            gridView.Columns["SALDO_PEDIDO"].AppearanceCell.TextOptions.RightToLeft = true;

            gridView.Columns["SALDO_DISPONIVEL"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns["SALDO_DISPONIVEL"].DisplayFormat.FormatString = "n2";
            gridView.Columns["SALDO_DISPONIVEL"].AppearanceCell.TextOptions.RightToLeft = true;

            gridView.Columns["VALORTOTAL"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns["VALORTOTAL"].DisplayFormat.FormatString = "n2";
            gridView.Columns["VALORTOTAL"].AppearanceCell.TextOptions.RightToLeft = true;

            gridView.Columns["QUANTIDADE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView.Columns["QUANTIDADE"].DisplayFormat.FormatString = "n0";
            gridView.Columns["QUANTIDADE"].AppearanceCell.TextOptions.RightToLeft = true;

            gridView.BestFitColumns();
        }

        public void RenomeiaColunasGridView(GridView gridView)
        {
            gridView.Columns["NSEQITMMOV"].Caption = "NSEQ";
            gridView.Columns["NUMEROSEQUENCIAL"].Caption = "NSEQCLI";

            gridView.Columns["VALORDESCORIGINAL"].Caption = "VALORDESCONTO";
            gridView.Columns["VALORDESPORIGINAL"].Caption = "VALORDESPESA";

            gridView.Columns["UNIDADE"].Caption = "UN";
        }

        /// <summary>
        /// Adiciona as colunas dos itens
        /// </summary>
        /// <param name="dt"></param>
        private void ConfiguraColunas(DataTable dt)
        {
            dt.Columns.Add("NSEQITMMOV", typeof(int));
            dt.Columns.Add("NUMEROSEQUENCIAL", typeof(int));
            dt.Columns.Add("CODAUXILIAR", typeof(string));
            dt.Columns.Add("PRODUTO", typeof(string));
            dt.Columns.Add("QUANTIDADE", typeof(decimal));
            dt.Columns.Add("UNIDADE", typeof(string));
            dt.Columns.Add("PRECOTABELA", typeof(decimal));
            dt.Columns.Add("VALORPINTURA", typeof(decimal));
            dt.Columns.Add("AJUSTEVALOR", typeof(decimal));
            dt.Columns.Add("PRECOUNITARIO", typeof(decimal));
            dt.Columns.Add("VALORTOTAL", typeof(decimal));
            dt.Columns.Add("NUMEROCCF", typeof(string));
            dt.Columns.Add("DATAENTREGA", typeof(DateTime));
            dt.Columns.Add("SALDO_FISICO", typeof(decimal));
            dt.Columns.Add("SALDO_PEDIDO", typeof(decimal));
            dt.Columns.Add("SALDO_DISPONIVEL", typeof(decimal));
            dt.Columns.Add("NUMEROCFOP", typeof(string));
            dt.Columns.Add("IDPRD", typeof(int));
            dt.Columns.Add("CODIGOPRD", typeof(string));
            dt.Columns.Add("ALIQUOTAIPI", typeof(decimal));
            dt.Columns.Add("HISTORICOLONGO", typeof(string));
            dt.Columns.Add("PRECOUNITARIOSELEC", typeof(int));
            dt.Columns.Add("IDNAT", typeof(int));
            dt.Columns.Add("APLICACAO", typeof(string));
            dt.Columns.Add("TIPOINOX", typeof(string));
            dt.Columns.Add("TIPOPINTURA", typeof(string));
            dt.Columns.Add("CORPINTURA", typeof(string));
            dt.Columns.Add("VALORDESCORIGINAL", typeof(decimal));
            dt.Columns.Add("VALORDESPORIGINAL", typeof(decimal));
            dt.Columns.Add("TIPOAJUSTEVALOR", typeof(int));
            dt.Columns.Add("NUMPEDIDO", typeof(string));
            dt.Columns.Add("NUMITEMPEDIDO", typeof(string));
            dt.Columns.Add("DISTANCIAMENTO", typeof(string));
        }

        #endregion

        #region Manipulação dos objetos 

        /// <summary>
        /// Atribuia valor aos itens de acordo com a GridView atual
        /// </summary>
        /// <param name="listComp"></param>
        /// <returns></returns>
        public List<TITMMOV> CarregaItens(List<ItemComposicao> listComp = null, List<TITMMOV> itens = null)
        {
            if (itens != null)
            {
                itens = GetItens(itens, listComp);

                return itens;
            }

            itens = new List<TITMMOV>();

            if (TabelaItens.Rows.Count > 0)
            {
                for (int i = 0; i < TabelaItens.Rows.Count; i++)
                {
                    item = new TITMMOV();

                    item.AJUSTEVALOR = TabelaItens.Rows[i]["AJUSTEVALOR"] == DBNull.Value ? 0 : Convert.ToDecimal(TabelaItens.Rows[i]["AJUSTEVALOR"]);
                    item.ALIQUOTAIPI = TabelaItens.Rows[i]["ALIQUOTAIPI"] == DBNull.Value ? 0 : Convert.ToDecimal(TabelaItens.Rows[i]["ALIQUOTAIPI"]);
                    item.APLICACAO = TabelaItens.Rows[i]["APLICACAO"].ToString();
                    item.CODAUXILIAR = TabelaItens.Rows[i]["CODAUXILIAR"].ToString();
                    item.CODIGOPRD = TabelaItens.Rows[i]["CODIGOPRD"].ToString();
                    item.CORPINTURA = TabelaItens.Rows[i]["CORPINTURA"].ToString();

                    if (TabelaItens.Rows[i]["DATAENTREGA"] != null && !string.IsNullOrEmpty(TabelaItens.Rows[i]["DATAENTREGA"].ToString()))
                    {
                        item.DATAENTREGA = Convert.ToDateTime(TabelaItens.Rows[i]["DATAENTREGA"]);
                    }

                    item.HISTORICOLONGO = TabelaItens.Rows[i]["HISTORICOLONGO"].ToString();
                    item.IDNAT = Convert.ToInt32(TabelaItens.Rows[i]["IDNAT"]);
                    item.IDPRD = Convert.ToInt32(TabelaItens.Rows[i]["IDPRD"]);
                    item.NSEQITEMMOV = TabelaItens.Rows[i]["NSEQITMMOV"].ToString();
                    item.NUMEROSEQUENCIAL = Convert.ToInt32(TabelaItens.Rows[i]["NUMEROSEQUENCIAL"]);
                    item.NUMEROCCF = TabelaItens.Rows[i]["NUMEROCCF"].ToString();
                    item.NUMEROCFOP = TabelaItens.Rows[i]["NUMEROCFOP"].ToString();
                    item.NUMITEMPEDIDO = TabelaItens.Rows[i]["NUMITEMPEDIDO"].ToString();
                    item.NUMPEDIDO = TabelaItens.Rows[i]["NUMPEDIDO"].ToString();
                    item.DISTANCIAMENTO = TabelaItens.Rows[i]["DISTANCIAMENTO"].ToString();
                    item.PRECOTABELA = TabelaItens.Rows[i]["PRECOTABELA"] == DBNull.Value ? 0 : Convert.ToDecimal(TabelaItens.Rows[i]["PRECOTABELA"]);
                    item.PRECOUNITARIO = TabelaItens.Rows[i]["PRECOUNITARIO"] == DBNull.Value ? 0 : Convert.ToDecimal(TabelaItens.Rows[i]["PRECOUNITARIO"]);
                    item.PRECOUNITARIOSELEC = TabelaItens.Rows[i]["PRECOUNITARIOSELEC"].ToString();
                    item.PRODUTO = TabelaItens.Rows[i]["PRODUTO"].ToString();
                    item.QUANTIDADE = Convert.ToInt32(TabelaItens.Rows[i]["QUANTIDADE"]);
                    item.SALDO_DISPONIVEL = TabelaItens.Rows[i]["SALDO_DISPONIVEL"] == DBNull.Value ? 0 : Convert.ToDecimal(TabelaItens.Rows[i]["SALDO_DISPONIVEL"]);
                    item.SALDO_FISICO = TabelaItens.Rows[i]["SALDO_FISICO"] == DBNull.Value ? 0 : Convert.ToDecimal(TabelaItens.Rows[i]["SALDO_FISICO"]);
                    item.SALDO_PEDIDO = TabelaItens.Rows[i]["SALDO_PEDIDO"] == DBNull.Value ? 0 : Convert.ToDecimal(TabelaItens.Rows[i]["SALDO_PEDIDO"]);
                    item.TIPOAJUSTEVALOR = TabelaItens.Rows[i]["TIPOAJUSTEVALOR"] == DBNull.Value ? 0 : Convert.ToInt32(TabelaItens.Rows[i]["TIPOAJUSTEVALOR"]);
                    item.TIPOINOX = TabelaItens.Rows[i]["TIPOINOX"].ToString();
                    item.TIPOPINTURA = TabelaItens.Rows[i]["TIPOPINTURA"].ToString();
                    item.UNIDADE = TabelaItens.Rows[i]["UNIDADE"].ToString();
                    item.VALORDESCORIGINAL = TabelaItens.Rows[i]["VALORDESCORIGINAL"] == DBNull.Value ? 0 : Convert.ToDecimal(TabelaItens.Rows[i]["VALORDESCORIGINAL"]);
                    item.VALORDESPORIGINAL = TabelaItens.Rows[i]["VALORDESPORIGINAL"] == DBNull.Value ? 0 : Convert.ToDecimal(TabelaItens.Rows[i]["VALORDESPORIGINAL"]);
                    item.VALORPINTURA = TabelaItens.Rows[i]["VALORPINTURA"] == DBNull.Value ? 0 : Convert.ToDecimal(TabelaItens.Rows[i]["VALORPINTURA"]);
                    item.VALORTOTAL = TabelaItens.Rows[i]["VALORTOTAL"] == DBNull.Value ? 0 : Convert.ToDecimal(TabelaItens.Rows[i]["VALORTOTAL"]);

                    if (listComp != null)
                    {
                        if (listComp.Count > 0)
                        {
                            // Inserir os outros códigos referentes à composição
                            if (item.CODAUXILIAR.Contains("DP800") || ((item.CODAUXILIAR.Contains("DP801") || ((item.CODAUXILIAR.Contains("DP802") || (item.CODAUXILIAR.Contains("DP803") || ((item.CODAUXILIAR.Contains("DP804") || ((item.CODAUXILIAR.Contains("DP805")))))))))))
                            {
                                if (listComp[0].NSEQ.ToString() == item.NSEQITEMMOV)
                                {
                                    item.COMPOSICAO = listComp;
                                }
                            }
                        }
                    }

                    itens.Add(item);
                }
            }

            return itens;
        }

        private List<TITMMOV> GetItens(List<TITMMOV> listTITMMOV, List<ItemComposicao> listComposicao)
        {
            itens = new List<TITMMOV>();

            for (int i = 0; i < listTITMMOV.Count; i++)
            {
                item = new TITMMOV();

                item.AJUSTEVALOR = listTITMMOV[i].AJUSTEVALOR;
                item.ALIQUOTAIPI = listTITMMOV[i].ALIQUOTAIPI;
                item.APLICACAO = listTITMMOV[i].APLICACAO;
                item.CODAUXILIAR = listTITMMOV[i].CODAUXILIAR;
                item.CODIGOPRD = listTITMMOV[i].CODIGOPRD;
                item.CORPINTURA = listTITMMOV[i].CORPINTURA;

                if (listTITMMOV[i].DATAENTREGA != null && !string.IsNullOrEmpty(listTITMMOV[i].DATAENTREGA.ToString()))
                {
                    item.DATAENTREGA = listTITMMOV[i].DATAENTREGA;
                }

                item.HISTORICOLONGO = listTITMMOV[i].HISTORICOLONGO;
                item.IDNAT = listTITMMOV[i].IDNAT;
                item.IDPRD = listTITMMOV[i].IDPRD;
                item.NSEQITEMMOV = listTITMMOV[i].NSEQITEMMOV;
                item.NUMEROSEQUENCIAL = listTITMMOV[i].NUMEROSEQUENCIAL;
                item.NUMEROCCF = listTITMMOV[i].NUMEROCCF;
                item.NUMEROCFOP = listTITMMOV[i].NUMEROCFOP;
                item.NUMITEMPEDIDO = listTITMMOV[i].NUMITEMPEDIDO;
                item.NUMPEDIDO = listTITMMOV[i].NUMPEDIDO;
                item.DISTANCIAMENTO = listTITMMOV[i].DISTANCIAMENTO;
                item.PRECOTABELA = listTITMMOV[i].PRECOTABELA == null ? 0 : listTITMMOV[i].PRECOTABELA;
                item.PRECOUNITARIO = listTITMMOV[i].PRECOUNITARIO;
                item.PRECOUNITARIOSELEC = listTITMMOV[i].PRECOUNITARIOSELEC;
                item.PRODUTO = listTITMMOV[i].PRODUTO;
                item.QUANTIDADE = listTITMMOV[i].QUANTIDADE;
                item.SALDO_DISPONIVEL = listTITMMOV[i].SALDO_DISPONIVEL;
                item.SALDO_FISICO = listTITMMOV[i].SALDO_FISICO;
                item.SALDO_PEDIDO = listTITMMOV[i].SALDO_PEDIDO;
                item.TIPOAJUSTEVALOR = listTITMMOV[i].TIPOAJUSTEVALOR;
                item.TIPOINOX = listTITMMOV[i].TIPOINOX;
                item.TIPOPINTURA = listTITMMOV[i].TIPOPINTURA;
                item.UNIDADE = listTITMMOV[i].UNIDADE;
                item.VALORDESCORIGINAL = listTITMMOV[i].VALORDESCORIGINAL;
                item.VALORDESPORIGINAL = listTITMMOV[i].VALORDESPORIGINAL;
                item.VALORPINTURA = listTITMMOV[i].VALORPINTURA;
                item.VALORTOTAL = listTITMMOV[i].VALORTOTAL;

                if (listComposicao != null)
                {
                    if (listComposicao.Count > 0)
                    {
                        // Inserir os outros códigos referentes à composição
                        if (item.CODAUXILIAR.Contains("DP800") || ((item.CODAUXILIAR.Contains("DP801") || ((item.CODAUXILIAR.Contains("DP802") || (item.CODAUXILIAR.Contains("DP803") || ((item.CODAUXILIAR.Contains("DP804") || ((item.CODAUXILIAR.Contains("DP805")))))))))))
                        {
                            item.COMPOSICAO = listComposicao;
                        }
                    }
                }

                itens.Add(item);
            }

            return itens;
        }

        public List<ItemComposicao> GetComposicaoInserida(int NseqItem, int? IDMOV = null)
        {
            List<ItemComposicao> composicao = new List<ItemComposicao>();
            ItemComposicao itemComposto;

            if (IDMOV != null)
            {
                string Comando = String.Format(@"select * from ZORCAMENTOITEMCOMPOSTO 

                                                        inner join TPRODUTO TP
                                                        on TP.IDPRD = ZORCAMENTOITEMCOMPOSTO.IDPRD

                                                        where CODCOLIGADA = 1 and CODFILIAL = 1 and CODCOLPRD = 1 and IDMOV = '{0}' and NSEQ = '{1}'",
                            IDMOV, NseqItem);

                DataTable dtComposicao = MetodosSQL.GetDT(Comando);

                if (dtComposicao.Rows.Count > 0)
                {
                    foreach (DataRow row in dtComposicao.Rows)
                    {
                        itemComposto = new ItemComposicao();

                        itemComposto.CODCOLIGADA = AppLib.Context.Empresa;
                        itemComposto.CODFILIAL = AppLib.Context.Filial;
                        itemComposto.IDMOV = (int)IDMOV;
                        itemComposto.CODIGOPRD = (string)row["CODIGOPRD"];
                        itemComposto.CODIGOAUXILIAR = (string)row["CODIGOAUXILIAR"];
                        itemComposto.IDPRD = (int)row["IDPRD"];
                        itemComposto.NOMEFANTASIA = (string)row["NOMEFANTASIA"];
                        itemComposto.NSEQ = NseqItem;
                        itemComposto.QUANTIDADE = (decimal)row["QUANTIDADE"];
                        itemComposto.PRECOUNITARIO = (decimal)row["PRECOUNITARIO"];

                        composicao.Add(itemComposto);
                    }
                }
            }

            return composicao;
        }

        #endregion

        #endregion
    }
}
