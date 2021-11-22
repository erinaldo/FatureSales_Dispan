using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AppFatureClient.Classes;
using AppLib.Windows;

namespace AppFatureClient
{
    public partial class FormOrcamentoItem : AppLib.Windows.FormCadastroObject
    {
        public List<TITMMOV> Itens;
        public TITMMOV xTITMMOV = new TITMMOV();
        public AcaoForcada acao { get; set; }
        public bool banco = false;
        public string tipoVenda = string.Empty;
        public bool edita = false;
        public string CODCFO { get; set; }
        public string CODETD { get; set; }
        public string AplicaProd { get; set; }
        public string NSEQITEM { get; set; }
        public string IDMOV { get; set; }
        public bool consumidorFinal { get; set; }
        bool verificaDP = true;

        public FormOrcamentoItem()
        {
            InitializeComponent();

            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gridData2.gridControl1.DataSourceChanged += gridData2_DataSourceChanged;

            try
            {
                campoDecimalPRECOUNITARIO.textEdit1.TextChanged += ((object sender, EventArgs e) =>
                {

                    decimalPrecoTotal.Set(campoDecimalPRECOUNITARIO.Get() * campoDecimalQUANTIDADE.Get());

                });

                campoDecimalQUANTIDADE.textEdit1.TextChanged += ((object sender, EventArgs e) =>
                {

                    decimalPrecoTotal.Set(campoDecimalPRECOUNITARIO.Get() * campoDecimalQUANTIDADE.Get());

                });
            }
            catch (Exception)
            {

            }


            campoDP.textEdit1.TextChanged += LiberaCamposComplementares;
            medidaA.textEdit1.TextChanged += AtualizaGridItem;
            medidaB.textEdit1.TextChanged += AtualizaGridItem;
            medidaC.textEdit1.TextChanged += AtualizaGridItem;
            medidaD.textEdit1.TextChanged += AtualizaGridItem;
            raio.textEdit1.TextChanged += AtualizaGridItem;
            peso.textEdit1.TextChanged += AtualizaGridItem;

            listaComplemento.comboBox1.TextChanged += AtualizaGridItem;
            listaAcabamento.comboBox1.TextChanged += AtualizaGridItem;
            listaChapa.comboBox1.TextChanged += AtualizaGridItem;
            listaVirola.comboBox1.TextChanged += AtualizaGridItem;
            listaSepto.comboBox1.TextChanged += AtualizaGridItem;
            listaRaio.comboBox1.TextChanged += AtualizaGridItem;

            listaChapa.Set(null);
            listaAcabamento.Set(null);
            listaVirola.Set(null);
            listaRaio.Set(null);
            listaSepto.Set(null);
            listaComplemento.Set(null);
            listaTipoInox.Set(null);

        }

        private void CarregaSaldo()
        {
            SaldoItem saldo = ItensOrcamento.getSaldo(campoLookupPRODUTO.Get());

            decimalSaldoFisico.Set(saldo.SALDO_FISICO);
            decimalSaldoPedido.Set(saldo.SALDO_PEDIDO);
            decimalSaldoDisponivel.Set(saldo.SALDO_DISPONIVEL);
        }

        private void CarregaGridTributo()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(IDMOV))
                {
                    string sql = String.Format(@"select CODCOLIGADA, 
	                                                   IDMOV, 
	                                                   CODTRB, 
	                                                   BASEDECALCULO,  
	                                                   ALIQUOTA,
	                                                   VALOR,
                                                       SITTRIBUTARIA,
	                                                   FATORREDUCAO,
	                                                   FATORSUBSTTRIB
	                                                   from TTRBMOV 
	                                                   where CODCOLIGADA = '1'
	                                                   and IDMOV = '{0}'
	                                                   and NSEQITMMOV = '{1}'", IDMOV, NSEQITEM);

                    gridData1.Consulta = sql.Split(' ');
                    gridData1.Atualizar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static class ComplementoProduto
        {
            public static string DP { get; set; }
            public static string LARGURA { get; set; }
            public static string ALTURA { get; set; }
            public static string COMPRIMENTO { get; set; }
            public static string ESPESSURA { get; set; }
            public static string CHAPA { get; set; }
            public static string ACABAMENTO { get; set; }
            public static string VIROLA { get; set; }
            public static string RAIO { get; set; }
            public static string TIPORAIO { get; set; }
            public static string SEPTO { get; set; }
            public static string COMPLEMENTO { get; set; }
            public static string PESO { get; set; }
        }



        private void LiberaCamposComplementares(object sender, EventArgs e)
        {
            try
            {

                gridControl1.DataSource = new DataTable();

                string sql = String.Format(@"select * from ZTPRODUTOREGRA where CODDP = '{0}'", campoDP.Get());

                DataTable dt = MetodosSQL.GetDT(sql);

                int i = 0;

                if (dt.Rows.Count == 1)
                {
                    medidaA.Enabled = (int)dt.Rows[0]["FLAGLARGURA"] == 1;
                    medidaB.Enabled = (int)dt.Rows[0]["FLAGALTURA"] == 1;
                    medidaC.Enabled = (int)dt.Rows[0]["FLAGCOMPRIMENTO"] == 1;
                    medidaD.Enabled = (int)dt.Rows[0]["FLAGESPESSURA"] == 1;
                    listaChapa.Enabled = (int)dt.Rows[0]["FLAGCHAPA"] == 1;
                    listaAcabamento.Enabled = (int)dt.Rows[0]["FLAGACABAMENTO"] == 1;
                    listaVirola.Enabled = (int)dt.Rows[0]["FLAGVIROLA"] == 1;
                    raio.Enabled = (int)dt.Rows[0]["FLAGRAIO"] == 1;
                    listaRaio.Enabled = (int)dt.Rows[0]["FLAGTIPORAIO"] == 1;
                    listaSepto.Enabled = (int)dt.Rows[0]["FLAGSEPTO"] == 1;
                    listaComplemento.Enabled = (int)dt.Rows[0]["FLAGCOMPLEMENTO"] == 1;
                    peso.Enabled = (int)dt.Rows[0]["FLAGPESO"] == 1;


                    if ((int)dt.Rows[0]["FLAGLARGURA"] == 0) { medidaA.Set(null); }
                    if ((int)dt.Rows[0]["FLAGALTURA"] == 0) { medidaB.Set(null); }
                    if ((int)dt.Rows[0]["FLAGCOMPRIMENTO"] == 0) { medidaC.Set(null); }
                    if ((int)dt.Rows[0]["FLAGESPESSURA"] == 0) { medidaD.Set(null); }
                    if ((int)dt.Rows[0]["FLAGCHAPA"] == 0) { listaChapa.Set(null); }
                    if ((int)dt.Rows[0]["FLAGACABAMENTO"] == 0) { listaAcabamento.Set(null); }
                    if ((int)dt.Rows[0]["FLAGVIROLA"] == 0) { listaVirola.Set(null); }
                    if ((int)dt.Rows[0]["FLAGRAIO"] == 0) { raio.Set(null); }
                    if ((int)dt.Rows[0]["FLAGTIPORAIO"] == 0) { listaRaio.Set(null); }
                    if ((int)dt.Rows[0]["FLAGSEPTO"] == 0) { listaSepto.Set(null); }
                    if ((int)dt.Rows[0]["FLAGCOMPLEMENTO"] == 0) { listaComplemento.Set(null); }
                    if ((int)dt.Rows[0]["FLAGPESO"] == 0) { peso.Set(null); }

                    AtualizaGridItem(null, null);

                    verificaDP = true;


                }
                else
                {
                    sql = String.Format(@"select TPRODUTO.CODIGOPRD,
	                                               TPRODUTO.CODIGOAUXILIAR,
	                                               TPRODUTO.NOMEFANTASIA,
												   TPRODUTODEF.CODUNDVENDA,
												   PRECO1 as 'PRECO'

												   from TPRODUTO

												   inner join ZTPRODUTOCOMPL
												   on ZTPRODUTOCOMPL.CODCOLIGADA = TPRODUTO.CODCOLPRD
												   and ZTPRODUTOCOMPL.IDPRD = TPRODUTO.IDPRD

												   inner join TPRODUTODEF
												   on TPRODUTODEF.CODCOLIGADA = TPRODUTO.CODCOLPRD
												   and TPRODUTODEF.IDPRD = TPRODUTO.IDPRD

												   where CODDP = '{0}'", campoDP.Get());

                    dt = MetodosSQL.GetDT(sql);
                    gridControl1.DataSource = dt;
                    gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
                    gridView1.BestFitColumns();
                    verificaDP = false;
                }





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setComplementares()
        {
            if (!String.IsNullOrWhiteSpace(campoDP.Get()))
            {
                ComplementoProduto.DP = campoDP.Get();
                ComplementoProduto.LARGURA = medidaA.Enabled ? (String.IsNullOrWhiteSpace(medidaA.Get().ToString()) ? "%" : medidaA.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.ALTURA = medidaB.Enabled ? (String.IsNullOrWhiteSpace(medidaB.Get().ToString()) ? "%" : medidaB.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.COMPRIMENTO = medidaC.Enabled ? (String.IsNullOrWhiteSpace(medidaC.Get().ToString()) ? "%" : medidaC.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.ESPESSURA = medidaD.Enabled ? (String.IsNullOrWhiteSpace(medidaD.Get().ToString()) ? "%" : medidaD.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.CHAPA = listaChapa.Enabled ? (String.IsNullOrWhiteSpace(listaChapa.Get().ToString()) ? "%" : listaChapa.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.ACABAMENTO = listaAcabamento.Enabled ? (String.IsNullOrWhiteSpace(listaAcabamento.Get().ToString()) ? "%" : listaAcabamento.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.VIROLA = listaVirola.Enabled ? (String.IsNullOrWhiteSpace(listaVirola.Get().ToString()) ? "%" : listaVirola.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.RAIO = raio.Enabled ? (String.IsNullOrWhiteSpace(raio.Get().ToString()) ? "%" : raio.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.TIPORAIO = listaRaio.Enabled ? (String.IsNullOrWhiteSpace(listaRaio.Get().ToString()) ? "%" : listaRaio.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.SEPTO = listaSepto.Enabled ? (String.IsNullOrWhiteSpace(listaSepto.Get().ToString()) ? "%" : listaSepto.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.COMPLEMENTO = listaComplemento.Enabled ? (String.IsNullOrWhiteSpace(listaComplemento.Get().ToString()) ? "%" : listaComplemento.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.PESO = peso.Enabled ? (String.IsNullOrWhiteSpace(peso.Get().ToString()) ? "%" : peso.Get().ToString().Replace(",", ".")) : "%";
            }
            else
            {
                MessageBox.Show("Insira um campo para o DP");
            }
        }


        private void AtualizaGridItem(object sender, EventArgs e)
        {
            try
            {
                string sql = String.Empty;
                gridControl1.DataSource = new DataTable();
                if (!String.IsNullOrWhiteSpace(campoDP.Get()))
                {

                    setComplementares();

                    sql = String.Format(@"select TPRODUTO.CODIGOPRD,
	                                               TPRODUTO.CODIGOAUXILIAR,
	                                               TPRODUTO.NOMEFANTASIA,
												   TPRODUTODEF.CODUNDVENDA,
												   0.00 as 'PRECO',
	                                               ZTPRODUTOCOMPL.LARGURA,
	                                               ZTPRODUTOCOMPL.ALTURA,
	                                               ZTPRODUTOCOMPL.COMPRIMENTO,
	                                               ZTPRODUTOCOMPL.ESPESSURA,
	                                               ZTPRODUTOCOMPL.CHAPA,
	                                               ZTPRODUTOCOMPL.ACABAMENTO,
	                                               ZTPRODUTOCOMPL.VIROLA,
	                                               ZTPRODUTOCOMPL.RAIO,
	                                               ZTPRODUTOCOMPL.TIPORAIO,
	                                               ZTPRODUTOCOMPL.SEPTO,
	                                               ZTPRODUTOCOMPL.COMPLEMENTO,
	                                               ZTPRODUTOCOMPL.PESO
                                            
                                            from ZTPRODUTOCOMPL

                                            inner join TPRODUTO
                                            on TPRODUTO.IDPRD = ZTPRODUTOCOMPL.IDPRD
                                            and TPRODUTO.CODCOLPRD = ZTPRODUTOCOMPL.CODCOLIGADA

											inner join TPRODUTODEF
											on TPRODUTODEF.CODCOLIGADA = TPRODUTO.CODCOLPRD
											and TPRODUTODEF.IDPRD = TPRODUTO.IDPRD

                                            where ZTPRODUTOCOMPL.CODCOLIGADA = 1
                                            and ZTPRODUTOCOMPL.CODFILIAL = 1
                                            and ZTPRODUTOCOMPL.CODDP = '{0}'
                                            and ZTPRODUTOCOMPL.LARGURA like '{1}'
                                            and ZTPRODUTOCOMPL.ALTURA like '{2}'
                                            and ZTPRODUTOCOMPL.COMPRIMENTO like '{3}'
                                            and ZTPRODUTOCOMPL.ESPESSURA like '{4}'
                                            and ZTPRODUTOCOMPL.CHAPA like '{5}'
                                            and ZTPRODUTOCOMPL.ACABAMENTO like '{6}'
                                            and ZTPRODUTOCOMPL.VIROLA like '{7}'
                                            and ZTPRODUTOCOMPL.RAIO like '{8}'
                                            and ZTPRODUTOCOMPL.TIPORAIO like '{9}'
                                            and ZTPRODUTOCOMPL.SEPTO like '{10}'
                                            and ZTPRODUTOCOMPL.COMPLEMENTO like '{11}'
                                            and ZTPRODUTOCOMPL.PESO like '%'",
                                            ComplementoProduto.DP,
                                            ComplementoProduto.LARGURA,
                                            ComplementoProduto.ALTURA,
                                            ComplementoProduto.COMPRIMENTO,
                                            ComplementoProduto.ESPESSURA,
                                            ComplementoProduto.CHAPA,
                                            ComplementoProduto.ACABAMENTO,
                                            ComplementoProduto.VIROLA,
                                            ComplementoProduto.RAIO,
                                            ComplementoProduto.TIPORAIO,
                                            ComplementoProduto.SEPTO,
                                            ComplementoProduto.COMPLEMENTO,
                                            ComplementoProduto.PESO);

                    DataTable dt = MetodosSQL.GetDT(sql);
                    dt.Columns["PRECO"].ReadOnly = false;

                    foreach (DataRow comp in dt.Rows)
                    {
                        comp["PRECO"] = CalculoPreco.CacularPreco((String)comp["CODIGOPRD"], listaTipoInox.Get()).PRECO;
                    }

                    gridControl1.DataSource = dt;
                    gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
                    gridView1.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void CarregaComplemento()
        {
            try
            {
                string sql = String.Format(@"select TPRODUTO.CODIGOAUXILIAR,
	                                               ZTPRODUTOCOMPL.LARGURA,
	                                               ZTPRODUTOCOMPL.ALTURA,
	                                               ZTPRODUTOCOMPL.COMPRIMENTO,
	                                               ZTPRODUTOCOMPL.ESPESSURA,
	                                               ZTPRODUTOCOMPL.CHAPA,
	                                               ZTPRODUTOCOMPL.ACABAMENTO,
	                                               ZTPRODUTOCOMPL.VIROLA,
	                                               ZTPRODUTOCOMPL.RAIO,
	                                               ZTPRODUTOCOMPL.TIPORAIO,
	                                               ZTPRODUTOCOMPL.SEPTO,
	                                               ZTPRODUTOCOMPL.COMPLEMENTO,
	                                               ZTPRODUTOCOMPL.PESO,
												   ZTPRODUTOCOMPL.CODDP,
												   ZTPRODUTOREGRA.* from ZTPRODUTOCOMPL

                                            inner join TPRODUTO
                                            on TPRODUTO.IDPRD = ZTPRODUTOCOMPL.IDPRD
                                            and TPRODUTO.CODCOLPRD = ZTPRODUTOCOMPL.CODCOLIGADA

											inner join ZTPRODUTOREGRA
											on ZTPRODUTOREGRA.CODDP = ZTPRODUTOCOMPL.CODDP
											and ZTPRODUTOREGRA.CODCOLIGADA = ZTPRODUTOCOMPL.CODCOLIGADA

											where ZTPRODUTOCOMPL.CODCOLIGADA = 1
                                            and ZTPRODUTOCOMPL.CODFILIAL = 1
											and TPRODUTO.CODIGOPRD = '{0}'", campoLookupPRODUTO.Get());

                DataTable dt = MetodosSQL.GetDT(sql);

                if (dt.Rows.Count == 1)
                {
                    campoDP.Set(dt.Rows[0]["CODDP"].ToString());

                    if ((int)dt.Rows[0]["FLAGLARGURA"] == 1) { medidaA.Set((decimal)dt.Rows[0]["LARGURA"]); }
                    if ((int)dt.Rows[0]["FLAGALTURA"] == 1) { medidaB.Set((decimal)dt.Rows[0]["ALTURA"]); }
                    if ((int)dt.Rows[0]["FLAGCOMPRIMENTO"] == 1) { medidaC.Set((decimal)dt.Rows[0]["COMPRIMENTO"]); }
                    if ((int)dt.Rows[0]["FLAGESPESSURA"] == 1) { medidaD.Set((decimal)dt.Rows[0]["ESPESSURA"]); }
                    if ((int)dt.Rows[0]["FLAGCHAPA"] == 1) { listaChapa.Set((string)dt.Rows[0]["CHAPA"]); }
                    if ((int)dt.Rows[0]["FLAGACABAMENTO"] == 1) { listaAcabamento.Set((string)dt.Rows[0]["ACABAMENTO"]); }
                    if ((int)dt.Rows[0]["FLAGVIROLA"] == 1) { listaVirola.Set(dt.Rows[0]["VIROLA"].ToString()); }
                    if ((int)dt.Rows[0]["FLAGRAIO"] == 1) { raio.Set((decimal)dt.Rows[0]["RAIO"]); }
                    if ((int)dt.Rows[0]["FLAGTIPORAIO"] == 1) { listaRaio.Set(dt.Rows[0]["TIPORAIO"].ToString()); }
                    if ((int)dt.Rows[0]["FLAGSEPTO"] == 1) { listaSepto.Set(dt.Rows[0]["SEPTO"].ToString()); }
                    if ((int)dt.Rows[0]["FLAGCOMPLEMENTO"] == 1) { listaComplemento.Set(dt.Rows[0]["COMPLEMENTO"].ToString()); }
                    if ((int)dt.Rows[0]["FLAGPESO"] == 1) { peso.Set((decimal)dt.Rows[0]["PESO"]); }
                }
                else
                {
                    if(verificaDP)
                    {
                        campoDP.Set(null);
                    }
                    

                    medidaA.Set(null); medidaA.Enabled = false;
                    medidaB.Set(null); medidaB.Enabled = false;
                    medidaC.Set(null); medidaC.Enabled = false;
                    medidaD.Set(null); medidaD.Enabled = false;
                    peso.Set(null); peso.Enabled = false;
                    raio.Set(null); raio.Enabled = false;

                    listaChapa.Set(null); listaChapa.Enabled = false;
                    listaAcabamento.Set(null); listaAcabamento.Enabled = false;
                    listaVirola.Set(null); listaVirola.Enabled = false;
                    listaRaio.Set(null); listaRaio.Enabled = false;
                    listaSepto.Set(null); listaSepto.Enabled = false;
                    listaComplemento.Set(null); listaComplemento.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        List<ItemComposicao> listaItemComposicao = new List<ItemComposicao>();

        private void CarregaCompisicao()
        {
            try
            {
                if(!edita)
                {
                    if (verificaDP)
                    {
                        string sql = String.Format(@"SELECT
	                                            TPRODUTO.CODIGOPRD,
                                                TPRODUTO.IDPRD,
	                                            TPRODUTO.CODIGOAUXILIAR,
	                                            TPRODUTO.NOMEFANTASIA,
	                                            (KCOMPONENTE.QTDUSADA/10000) QTDUSADA,
	                                            (SELECT CODUNDVENDA FROM TPRODUTODEF WHERE CODCOLIGADA = TPRODUTO.CODCOLPRD AND IDPRD = TPRODUTO.IDPRD) as 'UN',
												ZTPRODUTOCOMPL.CODDP,
												ZTPRODUTOCOMPL.TABELAPRECO,
												ZTPRODUTOCOMPL.LARGURA,
												ZTPRODUTOCOMPL.ALTURA,
												ZTPRODUTOCOMPL.COMPRIMENTO,
												ZTPRODUTOCOMPL.ESPESSURA,
                                                ZTPRODUTOCOMPL.ACABAMENTO,
												ZTPRODUTOCOMPL.CHAPA,
                                                TPRODUTO.INATIVO
                                            FROM 
	                                            TPRODUTO

												inner join KCOMPONENTE 
	                                            on TPRODUTO.CODCOLPRD = KCOMPONENTE.CODCOLIGADA
												AND TPRODUTO.IDPRD = KCOMPONENTE.IDPRODUTO

												left join ZTPRODUTOCOMPL
												on ZTPRODUTOCOMPL.CODCOLIGADA = TPRODUTO.CODCOLPRD
												and ZTPRODUTOCOMPL.IDPRD = TPRODUTO.IDPRD


                                            WHERE KCOMPONENTE.CODESTRUTURA = '{0}'
                                            AND TPRODUTO.CODIGOPRD NOT LIKE '01.%'
                                            ORDER BY 1", campoLookupPRODUTO.Get());

                        DataTable dt = MetodosSQL.GetDT(sql);

                        listaItemComposicao.Clear();

                        foreach (DataRow item in dt.Rows)
                        {
                            ParametrosPreco parametros = new ParametrosPreco();
                            ItemComposicao it = new ItemComposicao();
                            it.CODCOLIGADA = 1;
                            it.CODFILIAL = 1;
                            it.IDMOV = IDMOV == null ? 0 : int.Parse(IDMOV);
                            it.IDPRD = (int)item["IDPRD"];
                            it.CODIGOPRD = (String)item["CODIGOPRD"];
                            it.CODIGOAUXILIAR = (String)item["CODIGOAUXILIAR"];
                            it.NOMEFANTASIA = (String)item["NOMEFANTASIA"];
                            it.QUANTIDADE = (decimal)item["QTDUSADA"];
                            it.UNIDADEMEDIDA = (String)item["UN"];

                            RetornoPreco retorno = CalculoPreco.CacularPreco(it.CODIGOPRD, listaTipoInox.Get());

                            it.PRECOUNITARIO = retorno.PRECO;

                            listaItemComposicao.Add(it);
                        }


                        xTITMMOV.COMPOSICAO = listaItemComposicao;

                    }
                    else
                    {
                        xTITMMOV.COMPOSICAO = new List<ItemComposicao>();
                    }
                }
                else
                {
                   
                }
                

                gridData2_DataSourceChanged(null, null);
            }
            catch (Exception)
            {

            }
        }

        private void FormOrcamentoItem_Load(object sender, EventArgs e)
        {

            if (consumidorFinal)
            {
                AppLib.Windows.CodigoNome[] a = new AppLib.Windows.CodigoNome[2];

                a[0] = new AppLib.Windows.CodigoNome("1", "Uso/Consumo");
                a[1] = new AppLib.Windows.CodigoNome("4", "Ativo Imobilizado");

                campoListaAplicacao.Lista = a;
                campoListaAplicacao.Set(null);

            }

            if (!edita)
            {
                campoListaAplicacao.Set(AplicaProd);
                campoDP.Focus();
            }
            else
            {
                setCFOP(null, null);

                CarregaComplemento();

                CarregaCompisicao();

                string consulta1 = String.Format(@"select NUMEROCCF from TPRODUTO where CODCOLPRD = 1 and CODIGOPRD = '{0}'", campoLookupPRODUTO.Get());
                txtNCM.Set(MetodosSQL.GetField(consulta1, "NUMEROCCF"));

                string sql = String.Format(@"select SITUACAOMERCADORIA from TPRDFISCAL where CODCOLIGADA = 1 and IDPRD = {0}", xTITMMOV.IDPRD);
                if(MetodosSQL.GetField(sql, "SITUACAOMERCADORIA") == "01")
                {
                    sql = String.Format(@"select TPRODUTO.CODIGOPRD,
	                                               TPRODUTO.CODIGOAUXILIAR,
	                                               TPRODUTO.NOMEFANTASIA,
												   TPRODUTODEF.CODUNDVENDA,
												   PRECO1 as 'PRECO',
                                                   CODDP

												   from TPRODUTO

												   inner join ZTPRODUTOCOMPL
												   on ZTPRODUTOCOMPL.CODCOLIGADA = TPRODUTO.CODCOLPRD
												   and ZTPRODUTOCOMPL.IDPRD = TPRODUTO.IDPRD

												   inner join TPRODUTODEF
												   on TPRODUTODEF.CODCOLIGADA = TPRODUTO.CODCOLPRD
												   and TPRODUTODEF.IDPRD = TPRODUTO.IDPRD

												   where CODIGOPRD = '{0}'", xTITMMOV.CODIGOPRD);

                    campoDP.Set(MetodosSQL.GetField(sql, "CODDP"));

                    DataTable dt = MetodosSQL.GetDT(sql);

                    gridControl1.DataSource = dt;

                    CarregaSaldo();
                }
                else
                {
                    CarregaComplemento();
                    CarregaGridTributo();
                    gridData2.gridControl1.DataSource = xTITMMOV.COMPOSICAO;
                }
            }


            

            campoListaAplicacao.comboBox1.SelectedValueChanged += setCFOP;

            campoDP.Focus();


            //if (comboBoxMATERIALINTERLIGACAO.Text.Equals(""))
            //    comboBoxMATERIALINTERLIGACAO.SelectedIndex = 1;
            //if (tipoVenda == "T" && comboBoxPRODUTOCOMPOSTO.Text == "1 - Sim" )
            //{
            //    comboBox2.Enabled = true;
            //}
            //else
            //{
            //    comboBox2.Enabled = false;
            //}

            //if (edita == false)
            //{
            //    comboBox2.SelectedIndex = 1;
            //}
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

            if (txtCFOP.Get() == "Não encontrado.")
            {
                MessageBox.Show("Campo CFOP não encontrado", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((campoDecimalPRECOUNITARIO.Get() <= 0) || (decimalPrecoTotal.Get() <= 0))
            {
                MessageBox.Show("Preço não pode ser 0", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //if (campoLookupPRODUTO.textBoxCODIGO.Text.Substring(0,2) == "01")
            //{
            //    if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == -1)
            //    {
            //        MessageBox.Show("Favor selecionar o produto padrão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return false;
            //    }
            //    else if (comboBox1.SelectedIndex == 2)
            //    {
            //        if (string.IsNullOrEmpty(cmObs.richTextBox1.Text))
            //        {
            //            MessageBox.Show("Favor preencher o campo obs padrão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return false;
            //        }
            //    }
            //}




            return true;
        }

        public Boolean ProdutoTemComposicao()
        {
            // criar a query na TPRDCOMPOSTO
            return true;
        }

        private void FormOrcamentoItem_Preparar(object sender, EventArgs e)
        {

            //if (ProdutoTemComposicao())
            //{
            //    // inserir a composição
            //}
            //else
            //{
            //    // inserir o próprio produto
            //}
        }

        private Boolean FormOrcamentoItem_Salvar2(object sender, EventArgs e)
        {
            this.AtualizarObjeto();
            acao = AcaoForcada.Salvar;
            //Itens.Add(xTITMMOV);
            //this.Close();
            return true;
        }

        private void FormOrcamentoItem_Excluir2(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir registro selecionado ?", "Mensagem", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            acao = AcaoForcada.Excluir;
            this.Close();
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
            //f.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            //if (banco == false)
            //{
            //    return f.MostrarLookup(campoLookupPRODUTO, " AND ULTIMONIVEL = 1 AND INATIVO = 0");
            //}
            //else
            //{
            //    return f.MostrarLookup(campoLookupPRODUTO, " AND ULTIMONIVEL = 1 AND INATIVO = 0 AND CODIGOPRD LIKE '92%'");
            //}

            return f.MostrarLookup(campoLookupPRODUTO);

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
                campoDecimalALIQUOTA.Set(Convert.ToDecimal(dr["ALIQUOTA"].ToString()));
                campoDecimalQUANTIDADE.Set(1);
                campoDecimalPRECOUNITARIO.Set(0);
                consulta1 = String.Format(@"select NUMEROCCF from TPRODUTO where CODCOLPRD = 1 and CODIGOPRD = '{0}'", campoLookupPRODUTO.Get());
                txtNCM.Set(MetodosSQL.GetField(consulta1, "NUMEROCCF"));

                CarregaCompisicao();

                CarregaSaldo();
            }
            else
            {
                MessageBox.Show("Erro ao obter dados do produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CarregaComplemento();
        }

        #endregion

        #region EVENTOS DE MANIPULAÇÃO DOS DADOS

        public void AtualizarObjeto()
        {
            //xTITMMOV = new TITMMOV();
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery("SELECT IDPRD, NUMEROCCF FROM TPRD WHERE CODCOLIGADA = ? AND CODIGOPRD = ?", new Object[] { AppLib.Context.Empresa, campoLookupPRODUTO.Get() });

            if (dt.Rows.Count > 0)
            {
                System.Data.DataRow dr = dt.Rows[0];
                xTITMMOV.IDPRD = int.Parse(dr["IDPRD"].ToString());
                xTITMMOV.NUMEROCCF = dr["NUMEROCCF"].ToString();
            }



            xTITMMOV.CODIGOPRD = campoLookupPRODUTO.textBoxCODIGO.Text;
            xTITMMOV.PRODUTO = campoLookupPRODUTO.textBoxDESCRICAO.Text;
            xTITMMOV.UNIDADE = campoTextoCODUND.Get();
            xTITMMOV.QUANTIDADE = (decimal)campoDecimalQUANTIDADE.Get();
            xTITMMOV.PRECOUNITARIO = (decimal)campoDecimalPRECOUNITARIO.Get();
            xTITMMOV.VALORTOTAL = xTITMMOV.QUANTIDADE * xTITMMOV.PRECOUNITARIO;
            xTITMMOV.ALIQUOTAIPI = (decimal)campoDecimalALIQUOTA.Get();
            xTITMMOV.HISTORICOLONGO = campoMemoHISTORICOLONGO.Get();
            xTITMMOV.DATAENTREGA = dtEntrega.Get();
            xTITMMOV.IDNAT = ItensOrcamento.getIDNAT(txtCFOP.Get());
            xTITMMOV.APLICACAO = campoListaAplicacao.Get();
            xTITMMOV.NUMEROCCF = txtNCM.Get();
            xTITMMOV.NUMEROCFOP = txtCFOP.Get();
        }

        public void AtualizarForm()
        {
            campoLookupPRODUTO.textBoxCODIGO.Text = xTITMMOV.CODIGOPRD;
            campoLookupPRODUTO.textBoxDESCRICAO.Text = xTITMMOV.PRODUTO;
            campoTextoCODUND.Set(xTITMMOV.UNIDADE);
            campoDecimalQUANTIDADE.Set(xTITMMOV.QUANTIDADE);
            campoDecimalPRECOUNITARIO.Set(xTITMMOV.PRECOUNITARIO);
            campoDecimalALIQUOTA.Set(xTITMMOV.ALIQUOTAIPI);
            campoMemoHISTORICOLONGO.Set(xTITMMOV.HISTORICOLONGO);
            campoListaAplicacao.Set(xTITMMOV.APLICACAO);
            dtEntrega.Set(xTITMMOV.DATAENTREGA);
            setCFOP(null, null);
        }

        #endregion

        private void campoLookupPRODUTO_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM ZVWTPRD WHERE CODCOLIGADA = ? AND CODIGOPRD = ?";
            campoLookupPRODUTO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupPRODUTO.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupPRODUTO.Get() }).ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmObs_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxPRODUTOCOMPOSTO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void campoDP_Leave(object sender, EventArgs e)
        {


        }

        private void medidaA_Leave(object sender, EventArgs e)
        {

        }

        private void listaChapa_Leave(object sender, EventArgs e)
        {

        }

        private void setCFOP(object sender, EventArgs e)
        {
            string CFOP = ItensOrcamento.getCFOP(CODETD, campoListaAplicacao.Get(), campoLookupPRODUTO.Get(), consumidorFinal? "1" : "0");
            if (!String.IsNullOrWhiteSpace(CFOP))
            {
                txtCFOP.Set(CFOP);
            }
            else
            {
                txtCFOP.Set("Não encontrado.");
            }

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                xTITMMOV.COMPOSICAO = null;

                if(xTITMMOV.COMPOSICAO == null)
                {
                    xTITMMOV.COMPOSICAO = new List<ItemComposicao>();
                }

                if (xTITMMOV.COMPOSICAO.Count == 0)
                {
                    var obj = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CODIGOPRD");

                    campoLookupPRODUTO.textBoxCODIGO.Text = obj.ToString();

                    //campoLookupPRODUTO.textBoxDESCRICAO.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DESCRICAO").ToString();

                    campoLookupPRODUTO.textBox1_Leave(null, null);

                    setCFOP(null, null);

                    CarregaComplemento();

                    CarregaCompisicao();

                    LiberaCamposComplementares(null, null);

                    campoDecimalQUANTIDADE.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            campoLookupPRODUTO.textBoxCODIGO.Text = "";
            campoLookupPRODUTO.textBoxDESCRICAO.Text = "";
            campoTextoCODUND.Set(null);
            campoDecimalALIQUOTA.Set(null);
            campoDecimalQUANTIDADE.Set(null);
            campoDecimalPRECOUNITARIO.Set(null);
            dtEntrega.Set(null);

            txtCFOP.Set(null);

            campoDP.Set(null);
            medidaA.Set(null);
            medidaB.Set(null);
            medidaC.Set(null);
            medidaD.Set(null);
            raio.Set(null);
            peso.Set(null);

            listaComplemento.Set(null);
            listaAcabamento.Set(null);
            listaChapa.Set(null);
            listaVirola.Set(null);
            listaSepto.Set(null);
            listaRaio.Set(null);

            medidaA.Enabled = false;
            medidaB.Enabled = false;
            medidaC.Enabled = false;
            medidaD.Enabled = false;
            raio.Enabled = false;
            peso.Enabled = false;

            listaComplemento.Enabled = false;
            listaAcabamento.Enabled = false;
            listaChapa.Enabled = false;
            listaVirola.Enabled = false;
            listaSepto.Enabled = false;
            listaRaio.Enabled = false;

            gridControl1.DataSource = new DataTable();
            xTITMMOV.COMPOSICAO = new List<ItemComposicao>();
            gridData2_DataSourceChanged(null, null);
            txtNCM.Set(null);

            decimalSaldoFisico.Set(null);
            decimalSaldoPedido.Set(null);
            decimalSaldoDisponivel.Set(null);
        }

        private void gridData2_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                gridData2.gridControl1.DataSource = xTITMMOV.COMPOSICAO;

                decimal total = CalculoPreco.CacularPreco(campoLookupPRODUTO.Get(), listaTipoInox.Get()).PRECO;

                if (xTITMMOV.COMPOSICAO != null)
                {
                    foreach (ItemComposicao comp in xTITMMOV.COMPOSICAO)
                    {
                        total += (comp.QUANTIDADE * comp.PRECOUNITARIO);
                    }
                }

                campoDecimalPRECOUNITARIO.Set(total);
                //gridData2.Atualizar();
                gridData2.gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void gridData2_Novo(object sender, EventArgs e)
        {


            gridData2.Atualizar();
        }

        private void gridData2_Excluir(object sender, EventArgs e)
        {

        }

        private void gridData2_Editar(object sender, EventArgs e)
        {
            try
            {
                List<ItemComposicao> listaComp = new List<ItemComposicao>();

                List<Object> lista = gridData2.GetObjectRows();
                String Familia = String.Empty;

                foreach (Object comp in lista)
                {
                    listaComp.Add((ItemComposicao)comp);
                }

                if (lista.Count == 1)
                {
                    ItemComposicao remov = (ItemComposicao)lista[0];


                    FormAlteraItemComposicao frm = new FormAlteraItemComposicao();
                    frm.CODPRODUTO = remov.CODIGOPRD;
                    frm.QUANTIDADE = remov.QUANTIDADE;
                    frm.ShowDialog();

                    if (frm.RETORNO != null && !String.IsNullOrEmpty(frm.RETORNO.CODIGOPRD))
                    {
                        listaComp.Add(frm.RETORNO);
                        listaComp.Remove(remov);
                        xTITMMOV.COMPOSICAO = listaComp;
                        gridData2.Atualizar();
                    }

                }
                else
                {
                    MessageBox.Show("Selecione apenas um item para editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                //
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void listaAcabamento_AposSelecao(object sender, EventArgs e)
        {
            if(listaAcabamento.Get() == "INOX")
            {
                listaTipoInox.Enabled = true;
            }
            else
            {
                listaTipoInox.Enabled = false;
            }
        }
    }
}
