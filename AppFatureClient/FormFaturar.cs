using AppFatureClient.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormFaturar : Form
    {
        public DataRowCollection Movimentos { get; set; }
        public string CodTmvOrigem { get; set; }

        public FormFaturar()
        {
            InitializeComponent();
        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT TTMV.CODTMV, TTMV.NOME 
FROM TTMVFATURAMENTO, TTMV 
WHERE TTMVFATURAMENTO.CODTMV = ?
AND TTMVFATURAMENTO.CODCOLIGADA = ?
AND TTMVFATURAMENTO.CODTMVFAT = TTMV.CODTMV
AND TTMVFATURAMENTO.CODCOLIGADA = TTMV.CODCOLIGADA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1, consulta1, new Object[] { CodTmvOrigem, AppLib.Context.Empresa });
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (campoLookup1.Get() == null)
                {
                    MessageBox.Show("Selecione o movimento de destino.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                splashScreenManager1.ShowWaitForm();

                this.Cursor = Cursors.WaitCursor;

                for (int i = 0; i < Movimentos.Count; i++)
                {
                    AppInterop.MovMovimentoFatPar faturamento = new AppInterop.MovMovimentoFatPar();
                    string sSql = string.Empty;

                    if (CodTmvOrigem == "2.1.05" && Movimentos[i]["TIPO_VENDA"].ToString() == "VENDA INTERNA")
                    {
                        ///
                    }

                    sSql = @"SELECT ID_EXERCICIO
                            FROM DEXERCICIONATUREZA
                            WHERE GETDATE() BETWEEN DATAINICIAL AND DATAFINAL
                            AND CODCOLIGADA = ?";

                    int IdExercicioFiscal = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, sSql, new Object[] { AppLib.Context.Empresa }));

                    faturamento.CodColigada = AppLib.Context.Empresa;
                    faturamento.CodTmvOrigem = CodTmvOrigem;
                    faturamento.CodUsuario = AppLib.Context.Usuario;
                    faturamento.realizaBaixaPedido = true;
                    faturamento.TipoFaturamento = 1;
                    faturamento.CodSistema = "T";

                    faturamento.IdExercicioFiscal = IdExercicioFiscal;

                    if (CodTmvOrigem == "2.1.10")
                    {
                        faturamento.CodTmvDestino = "2.1.30";
                    }
                    else if (Movimentos[i]["TIPO_VENDA"].ToString() == "VENDA INTERNA")
                    {
                        faturamento.CodTmvDestino = "2.1.10";
                    }
                    else
                    {
                        faturamento.CodTmvDestino = "2.1.15";
                    }


                    faturamento.numeroMov = Movimentos[i]["NUMEROMOV"].ToString();

                    sSql = @"SELECT DATAEMISSAO FROM TMOV WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                    sSql = sSql.Replace(":CODCOLIGADA", AppLib.Context.Empresa.ToString());
                    sSql = sSql.Replace(":IDMOV", Movimentos[i]["IDMOV"].ToString());
                    faturamento.dataEmissao = Convert.ToDateTime(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { }));

                    faturamento.IdMov = new List<int>();
                    faturamento.IdMov.Add(Convert.ToInt32(Movimentos[i]["IDMOV"]));

                    sSql = @"SELECT CODCOLIGADA, IDMOV, NSEQITMMOV, QUANTIDADE FROM TITMMOV WHERE CODCOLIGADA = :CODCOLIGADA AND IDMOV = :IDMOV";
                    sSql = sSql.Replace(":CODCOLIGADA", AppLib.Context.Empresa.ToString());
                    sSql = sSql.Replace(":IDMOV", Movimentos[i]["IDMOV"].ToString());
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { });
                    if (dt.Rows.Count > 0)
                    {
                        faturamento.listaMovItemFatAutomatico = new List<AppInterop.MovMovimentoItemFatPar>();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            AppInterop.MovMovimentoItemFatPar item = new AppInterop.MovMovimentoItemFatPar();
                            item.Checked = 1;
                            item.CodColigada = Convert.ToInt32(dt.Rows[j]["CODCOLIGADA"]);
                            item.IdMov = Convert.ToInt32(dt.Rows[j]["IDMOV"]);
                            item.NSeqItmMov = Convert.ToInt32(dt.Rows[j]["NSEQITMMOV"]);
                            item.Quantidade = Convert.ToDecimal(dt.Rows[j]["QUANTIDADE"]);

                            faturamento.listaMovItemFatAutomatico.Add(item);
                        }
                    }

                    AppInterop.Message msgfaturar;
                    if (FatureContexto.Remoto)
                    {
                        msgfaturar = new Util().ConvertToMessage(FatureContexto.ServiceSoapClient.MovimentoFaturar(AppLib.Context.Usuario, AppLib.Context.Senha, new Util().ConvertToWSMovMovimentoFatPar(faturamento)));
                    }
                    else
                    {
                        msgfaturar = FatureContexto.ServiceClient.MovimentoFaturar(AppLib.Context.Usuario, AppLib.Context.Senha, faturamento);
                    }

                    if (int.Parse(msgfaturar.Retorno.ToString()) > 0)
                    {
                        bool bFaturado = false;
                        while (!bFaturado)
                        {
                            sSql = @"SELECT STATUS FROM ZTMOVFAT WHERE CODCOLIGADA = ? AND IDMOV = ?";
                            string sStatus = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sSql, new Object[] { Movimentos[i]["CODCOLIGADA"], Movimentos[i]["IDMOV"] }).ToString();
                            if (sStatus != "A")
                            {
                                if (sStatus == "R")
                                {
                                    //System.Threading.Thread.Sleep(5000); //5s
                                }

                                if (sStatus == "F")
                                {
                                    bFaturado = true;

                                    sSql = "SELECT IDMOVDES FROM ZTMOVFAT WHERE CODCOLIGADA = " + Movimentos[i]["CODCOLIGADA"] + " AND IDMOV = " + Movimentos[i]["IDMOV"];
                                    msgfaturar.Retorno = AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { }).ToString();

                                    //// INSERT nas tabelas remanescentes 
                                    //DataTable dtFaturamento = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT
                                    //                                                                        TITMMOV.NSEQITMMOV,
                                    //                                                                        TITMMOV.IDPRD,
                                    //                                                                        TPRD.CODIGOAUXILIAR,
                                    //                                                                        TPRD.CODIGOPRD,
                                    //                                                                        ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
                                    //                                                                        TITMMOV.CODUND,
                                    //                                                                        CAST(TITMMOV.QUANTIDADETOTAL AS NUMERIC (15,2)) QUANTIDADE,
                                    //                                                                        CAST(TITMMOV.PRECOUNITARIO AS NUMERIC(15,2)) PRECOUNITARIO,
                                    //                                                                        TPRD.NUMEROCCF,
                                                                                                            
                                    //                                                                        CAST(ISNULL(TTRBMOV.ALIQUOTA,0) AS NUMERIC (15,2)) ALIQUOTA,
                                    //                                                                        TITMMOVHISTORICO.HISTORICOLONGO,
                                    //                                                                        TITMMOV.PRECOUNITARIOSELEC,
                                                                                                            
                                    //                                                                        TITMMOVCOMPL.APLICPROD,
                                    //                                                                        TITMMOV.IDNAT,
                                    //                                                                        TITMMOV.DATAENTREGA,
                                    //                                                                        TITMMOVCOMPL.XPED AS 'NUMPEDIDO',
                                    //                                                                        TITMMOVCOMPL.NITEMPED AS 'NUMITEMPEDIDO',
                                    //                                                                        TITMMOVCOMPL.TIPO,
                                    //                                                                        TITMMOVCOMPL.VALORPINTURA,
                                    //                                                                        TITMMOVCOMPL.TIPOPINTURA,
                                    //                                                                        TITMMOVCOMPL.CORPINTURA,
                                    //                                                                        TITMMOVCOMPL.AJUSTEVALOR,
                                    //                                                                        TITMMOVCOMPL.TIPOAJUSTEVALOR,
                                    //                                                                        TITMMOVCOMPL.PRECOTABELA,
                                    //                                                                        TITMMOVCOMPL.DISTANCIAMENTO,
                                    //                                                                        TITMMOV.VALORTOTALITEM,
                                    //                                                                        ISNULL(TITMMOVCOMPL.VALORDESCORIGINAL,0) AS 'VALORDESCORIGINAL', 
                                    //                                                                        ISNULL(TITMMOVCOMPL.VALORDESPORIGINAL,0) AS 'VALORDESPORIGINAL', 
                                                                                                            
                                    //                                                                        (SELECT CODNAT FROM DCFOP WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND ATIVO = 1 AND FISCAL = 1 AND IDNAT = TITMMOV.IDNAT) AS 'NUMEROCFOP'
                                                                                                            
                                    //                                                                        FROM TITMMOV
                                                                                                            
                                    //                                                                        LEFT JOIN TITMMOVFISCAL
                                    //                                                                        ON TITMMOVFISCAL.CODCOLIGADA = TITMMOV.CODCOLIGADA
                                    //                                                                        AND TITMMOVFISCAL.IDMOV = TITMMOV.IDMOV
                                    //                                                                        AND TITMMOVFISCAL.NSEQITMMOV = TITMMOV.NSEQITMMOV
                                                                                                            
                                    //                                                                        LEFT JOIN TTRBMOV ON TITMMOV.CODCOLIGADA = TTRBMOV.CODCOLIGADA 
                                    //                                                                          AND TITMMOV.IDMOV = TTRBMOV.IDMOV
                                    //                                                                          AND TITMMOV.NSEQITMMOV = TTRBMOV.NSEQITMMOV
                                    //                                                                          AND TTRBMOV.CODTRB = 'IPI'
                                    //                                                                        LEFT JOIN TITMMOVCOMPL ON TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA
                                    //                                                                          AND TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV
                                    //                                                                          AND TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV
                                    //                                                                        LEFT JOIN TITMMOVHISTORICO ON TITMMOV.CODCOLIGADA = TITMMOVHISTORICO.CODCOLIGADA
                                    //                                                                          AND TITMMOV.IDMOV = TITMMOVHISTORICO.IDMOV
                                    //                                                                          AND TITMMOV.NSEQITMMOV = TITMMOVHISTORICO.NSEQITMMOV,
                                    //                                                                          TPRD
                                                                                                            
                                    //                                                                        WHERE TITMMOV.CODCOLIGADA = TPRD.CODCOLIGADA
                                    //                                                                          AND TITMMOV.IDPRD = TPRD.IDPRD
                                    //                                                                          AND TITMMOV.CODCOLIGADA = ?
                                    //                                                                          AND TITMMOV.IDMOV = ?", new object[] { AppLib.Context.Empresa, Movimentos[i]["IDMOV"] });

                                    //New.Models.OrcamentoItens itens = new New.Models.OrcamentoItens();

                                    //for (int j = 0; j < dtFaturamento.Rows.Count; j++)
                                    //{
                                    //    if (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP800") || (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP801") || (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP802") || (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP803") || (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP804") || (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP805")))))))
                                    //    {
                                    //        if (GetItemComposicao(dtFaturamento.Rows[j]["NSEQITMMOV"].ToString(), msgfaturar.Retorno.ToString()).Count <= 0)
                                    //        {
                                    //            // INSERT nas tabelas 

                                    //            // TITMMOVCOMPL
                                    //            try
                                    //            {
                                    //                AppLib.ORM.Jit TITMMOVCOMPL = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "TITMMOVCOMPL");

                                    //                TITMMOVCOMPL.Set("CODCOLIGADA", AppLib.Context.Empresa);
                                    //                TITMMOVCOMPL.Set("IDMOV", msgfaturar.Retorno.ToString());
                                    //                TITMMOVCOMPL.Set("NSEQITMMOV", dtFaturamento.Rows[j]["NSEQITMMOV"].ToString());
                                    //                TITMMOVCOMPL.Set("AJUSTEVALOR", dtFaturamento.Rows[j]["AJUSTEVALOR"]);
                                    //                TITMMOVCOMPL.Set("TIPOAJUSTEVALOR", dtFaturamento.Rows[j]["TIPOAJUSTEVALOR"]);
                                    //                TITMMOVCOMPL.Set("PRECOTABELA", dtFaturamento.Rows[j]["PRECOTABELA"]);
                                    //                TITMMOVCOMPL.Set("TIPOPINTURA", dtFaturamento.Rows[j]["TIPOPINTURA"]);
                                    //                TITMMOVCOMPL.Set("CORPINTURA", dtFaturamento.Rows[j]["CORPINTURA"]);
                                    //                TITMMOVCOMPL.Set("VALORPINTURA", dtFaturamento.Rows[j]["VALORPINTURA"]);
                                    //                TITMMOVCOMPL.Set("TIPO", dtFaturamento.Rows[j]["TIPO"]);
                                    //                TITMMOVCOMPL.Set("APLICPROD", dtFaturamento.Rows[j]["APLICPROD"]);
                                    //                TITMMOVCOMPL.Set("XPED", dtFaturamento.Rows[j]["NUMPEDIDO"]);
                                    //                TITMMOVCOMPL.Set("NITEMPED", dtFaturamento.Rows[j]["NUMITEMPEDIDO"]);

                                    //                TITMMOVCOMPL.Set("DISTANCIAMENTO", dtFaturamento.Rows[j]["DISTANCIAMENTO"].ToString());

                                    //                TITMMOVCOMPL.Set("VALORDESCORIGINAL", dtFaturamento.Rows[j]["VALORDESCORIGINAL"]);

                                    //                TITMMOVCOMPL.Set("VALORDESPORIGINAL", dtFaturamento.Rows[j]["VALORDESPORIGINAL"]);
                                    //                TITMMOVCOMPL.Save();
                                    //            }
                                    //            catch (Exception)
                                    //            {

                                    //            }

                                    //            if (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP800") || (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP801") || (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP802") || (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP803") || (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP804") || (dtFaturamento.Rows[j]["CODIGOAUXILIAR"].ToString().Contains("DP805")))))))
                                    //            {
                                    //                itens.CarregaGridViewItens((int)faturamento.IdMov[0]);
                                    //                itens.itens = itens.CarregaItens(GetItemComposicao(dtFaturamento.Rows[j]["NSEQITMMOV"].ToString(), faturamento.IdMov[0].ToString()));

                                    //                // ZORCAMENTOITEMCOMPOSTO
                                    //                for (int k = 0; k < itens.itens.Count; k++)
                                    //                {
                                    //                    if (itens.itens[k].COMPOSICAO != null)
                                    //                    {
                                    //                        if (itens.itens[k].COMPOSICAO.Count > 0)
                                    //                        {
                                    //                            for (int kk = 0; kk < itens.itens[k].COMPOSICAO.Count; kk++)
                                    //                            {
                                    //                                AppLib.Context.poolConnection.Get().ExecTransaction(@"insert into ZORCAMENTOITEMCOMPOSTO values (?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, 1, msgfaturar.Retorno.ToString(), itens.itens[k].COMPOSICAO[kk].NSEQ, itens.itens[k].COMPOSICAO[kk].IDPRD, itens.itens[k].COMPOSICAO[kk].QUANTIDADE.ToString().Replace(",", "."), itens.itens[k].COMPOSICAO[kk].PRECOUNITARIO.ToString().Replace(",", ".") });
                                    //                            }
                                    //                        }
                                    //                    }
                                    //                }

                                    //            }
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        // TITMMOVCOMPL
                                    //        try
                                    //        {
                                    //            AppLib.ORM.Jit TITMMOVCOMPL = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "TITMMOVCOMPL");

                                    //            TITMMOVCOMPL.Set("CODCOLIGADA", AppLib.Context.Empresa);
                                    //            TITMMOVCOMPL.Set("IDMOV", msgfaturar.Retorno.ToString());
                                    //            TITMMOVCOMPL.Set("NSEQITMMOV", dtFaturamento.Rows[j]["NSEQITMMOV"].ToString());
                                    //            TITMMOVCOMPL.Set("AJUSTEVALOR", dtFaturamento.Rows[j]["AJUSTEVALOR"]);
                                    //            TITMMOVCOMPL.Set("TIPOAJUSTEVALOR", dtFaturamento.Rows[j]["TIPOAJUSTEVALOR"]);
                                    //            TITMMOVCOMPL.Set("PRECOTABELA", dtFaturamento.Rows[j]["PRECOTABELA"]);
                                    //            TITMMOVCOMPL.Set("TIPOPINTURA", dtFaturamento.Rows[j]["TIPOPINTURA"]);
                                    //            TITMMOVCOMPL.Set("CORPINTURA", dtFaturamento.Rows[j]["CORPINTURA"]);
                                    //            TITMMOVCOMPL.Set("VALORPINTURA", dtFaturamento.Rows[j]["VALORPINTURA"]);
                                    //            TITMMOVCOMPL.Set("TIPO", dtFaturamento.Rows[j]["TIPO"]);
                                    //            TITMMOVCOMPL.Set("APLICPROD", dtFaturamento.Rows[j]["APLICPROD"]);
                                    //            TITMMOVCOMPL.Set("XPED", dtFaturamento.Rows[j]["NUMPEDIDO"]);
                                    //            TITMMOVCOMPL.Set("NITEMPED", dtFaturamento.Rows[j]["NUMITEMPEDIDO"]);

                                    //            TITMMOVCOMPL.Set("DISTANCIAMENTO", dtFaturamento.Rows[j]["DISTANCIAMENTO"].ToString());

                                    //            TITMMOVCOMPL.Set("VALORDESCORIGINAL", dtFaturamento.Rows[j]["VALORDESCORIGINAL"]);

                                    //            TITMMOVCOMPL.Set("VALORDESPORIGINAL", dtFaturamento.Rows[j]["VALORDESPORIGINAL"]);
                                    //            TITMMOVCOMPL.Save();
                                    //        }
                                    //        catch (Exception)
                                    //        {

                                    //        }
                                    //    }
                                    //}

                                    msgfaturar.Mensagem = "Faturamento realizado com sucesso";
                                }

                                if (sStatus == "E")
                                {
                                    bFaturado = true;

                                    sSql = "SELECT MENSAGEM FROM ZTMOVFAT WHERE CODCOLIGADA = " + Movimentos[i]["CODCOLIGADA"] + " AND IDMOV = " + Movimentos[i]["IDMOV"];
                                    msgfaturar.Mensagem = AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { }).ToString();


                                    splashScreenManager1.CloseWaitForm();

                                    throw new Exception(msgfaturar.Mensagem);
                                }
                            }
                            else
                            {
                                //System.Threading.Thread.Sleep(5000); //5s
                            }
                        }

                        if (bFaturado == true)
                        {
                            ///
                        }

                        splashScreenManager1.CloseWaitForm();

                        MessageBox.Show(msgfaturar.Mensagem);
                        this.Close();
                    }
                    else
                    {
                        splashScreenManager1.CloseWaitForm();

                        throw new Exception(msgfaturar.Mensagem);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ItemComposicao> GetItemComposicao(string nSeqItem, string idmov)
        {
            string Comando = String.Format(@"select * from ZORCAMENTOITEMCOMPOSTO 

                                                        inner join TPRODUTO TP
                                                        on TP.IDPRD = ZORCAMENTOITEMCOMPOSTO.IDPRD

                                                        where CODCOLIGADA = 1 and CODFILIAL = 1 and CODCOLPRD = 1 and IDMOV = '{0}' and NSEQ = '{1}'",
                            idmov, nSeqItem);

            DataTable itemComposicao = MetodosSQL.GetDT(Comando);
            List<ItemComposicao> it = new List<ItemComposicao>();

            if (itemComposicao.Rows.Count > 0)
            {
                foreach (DataRow itc in itemComposicao.Rows)
                {
                    int o;
                    ItemComposicao itemComposto = new ItemComposicao();
                    itemComposto.CODCOLIGADA = AppLib.Context.Empresa;
                    itemComposto.CODFILIAL = AppLib.Context.Filial;
                    itemComposto.IDMOV = int.TryParse(itc["IDMOV"].ToString(), out o) ? int.Parse(itc["IDMOV"].ToString()) : 0;
                    itemComposto.CODIGOPRD = (string)itc["CODIGOPRD"];
                    itemComposto.CODIGOAUXILIAR = (string)itc["CODIGOAUXILIAR"];
                    itemComposto.IDPRD = (int)itc["IDPRD"];
                    itemComposto.NOMEFANTASIA = (string)itc["NOMEFANTASIA"];
                    itemComposto.NSEQ = (int)itc["NSEQ"];
                    itemComposto.QUANTIDADE = (decimal)itc["QUANTIDADE"];
                    itemComposto.PRECOUNITARIO = (decimal)itc["PRECOUNITARIO"];

                    it.Add(itemComposto);
                }
            }
            else
            {

            }

            return it;
        }

        private void FormFaturar_Load(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(CodTmvOrigem))
            {
                if (CodTmvOrigem == "2.1.05")
                {
                    campoLookup1.textBoxCODIGO.Text = "2.1.10";
                    campoLookup1.textBox1_Leave(this, null);

                    campoLookup2.textBoxCODIGO.Text = "2.1.15";
                    campoLookup2.textBox1_Leave(this, null);
                }

                if (CodTmvOrigem == "2.1.10" || CodTmvOrigem == "2.1.15")
                {
                    campoLookup1.textBoxCODIGO.Text = "2.1.30";
                    campoLookup1.textBox1_Leave(this, null);
                    label1.Text = "Movimento de Destino";
                    // campoLookup1.Set("2.1.20");
                    campoLookup2.Visible = false;
                    label2.Visible = false;

                }
            }
        }

        private void campoLookup1_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM TTMV WHERE CODCOLIGADA = ? AND CODTMV = ?";
            campoLookup1.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup1.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookup1.Get() }).ToString();
        }
    }
}
