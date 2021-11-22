using AppFatureClient.Classes;
using AppLib.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
        public bool descItem { get; set; }
        public bool POSSUIBENEFICIO { get; set; }

        public bool inserirNovoItem { get; set; } = false;

        bool verificaDP = true;

        string CODPRD = string.Empty;

        public DateTime dataEntrega { get; set; }
        public string pedidoCliente { get; set; }

        public int Nseq;
        public string CodSerie;
        public int numeroSequencial;

        private string codigoDP = "";
        private bool isPrecoCarregado = false;

        public FormOrcamentoItem()
        {
            InitializeComponent();

            campoDP.textEdit1.PreviewKeyDown += campoDP_PreviewKeyDown;

            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gridData2.gridControl1.DataSourceChanged += gridData2_DataSourceChanged;

            //this.toolStrip1.Enabled = false;

            this.simpleButtonCANCELAR.Click += (s, ev) =>
            {
                inserirNovoItem = false;
                this.Dispose();
            };

            txtNUMPEDIDO.textEdit1.Properties.MaxLength = 15;
            txtNuMITEMPEDIDO.textEdit1.Properties.MaxLength = 6;

            campoDP.textEdit1.TextChanged += LiberaCamposComplementares;
            campoDP.textEdit1.KeyPress += campoDP_KeyPress;

            listaChapa.Set(null);
            listaAcabamento.Set(null);
            listaVirola.Set(null);
            listaRaio.Set(null);
            listaSepto.Set(null);
            listaComplemento.Set(null);
            listaTipoInox.Set(null);
            raio.Set(null);
            medidaD.Set(null);
            listaPintura.Set(null);
        }

        private void AtualizaValoresCampos()
        {
            try
            {
                campoDecimalPRECOUNITARIO.Set(RecalculaPrecoUnitario());

                if (rbSoma.Checked)
                {
                    campoDecimalPRECOUNITARIO.Set(campoDecimalPRECOUNITARIO.Get() + Convert.ToDecimal(tbAjusteValor.Text));
                }
                else if (rbSubtracao.Checked)
                {
                    campoDecimalPRECOUNITARIO.Set(campoDecimalPRECOUNITARIO.Get() - Convert.ToDecimal(tbAjusteValor.Text));
                }

                decimalPrecoTotal.Set(Convert.ToDecimal(tbQuantidade.Text) * campoDecimalPRECOUNITARIO.Get());

                //decimalPrecoTotal.Set(campoDecimalQUANTIDADE.Get() * campoDecimalPRECOUNITARIO.Get());
            }
            catch (Exception)
            {

            }
        }

        private void HabilitarControleGroupEspecs(bool Habilitar)
        {
            foreach (Control c in gbEspecs.Controls)
            {
                if (c.GetType() != typeof(Label))
                {
                    c.Enabled = Habilitar;
                }

            }
        }

        private void CarregaPreco()
        {
            try
            {
                decimal total = 0;

                if (edita)
                {
                    if (xTITMMOV.TIPOAJUSTEVALOR >= 0)
                    {
                        total = (xTITMMOV.PRECOUNITARIO - xTITMMOV.VALORPINTURA - xTITMMOV.AJUSTEVALOR);
                    }
                    else
                    {
                        total = (xTITMMOV.PRECOUNITARIO - xTITMMOV.VALORPINTURA);
                    }

                    tbQuantidade.Text = xTITMMOV.QUANTIDADE.ToString();
                    //btnClean.Visible = false;
                    tbQuantidade.Enabled = true;

                    tbPrecoTabela.EditValue = total;
                    //decimalPrecoTabela.Set(total);

                    AtualizaValoresCampos();
                }
                else
                {
                    if (!isPrecoCarregado)
                    {
                        string IDPRD = AppLib.Context.poolConnection.Get("Start").ExecGetField("", "SELECT IDPRD FROM TPRODUTO WHERE CODCOLPRD = ? AND CODIGOPRD = ?", new object[] { AppLib.Context.Empresa, CODPRD }).ToString();

                        total = CalculoPreco.CacularPreco(IDPRD, listaTipoInox.Get(), "", "").PRECO;

                        if (xTITMMOV.COMPOSICAO != null)
                        {
                            foreach (ItemComposicao comp in xTITMMOV.COMPOSICAO)
                            {
                                total += (comp.QUANTIDADE * comp.PRECOUNITARIO);
                            }
                        }

                        tbPrecoTabela.EditValue = total;
                    }
                }

                if (edita == true)
                {
                    if (xTITMMOV.TIPOINOX != null)
                    {
                        listaTipoInox.comboBox1.SelectedValue = xTITMMOV.TIPOINOX;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpaCampos()
        {
            EventosFiltragem(false);

            campoLookupPRODUTO.textBoxCODIGO.Text = "";
            campoLookupPRODUTO.textBoxDESCRICAO.Text = "";
            CODPRD = "";
            campoTextoCODUND.Set(null);
            campoDecimalALIQUOTA.Set(null);
            tbQuantidade.EditValue = null;
            campoDecimalPRECOUNITARIO.Set(null);
            //dtEntrega.Set(dataEntrega);

            clCFOP.Set(null);
            clCFOP.textBoxCODIGO.ResetText();
            txtNUMPEDIDO.Set(null);
            txtNuMITEMPEDIDO.Set(null);


            campoDP.Set(null);
            campoDP.Enabled = true;
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
            listaPintura.Set(null);
            listaTipoInox.Set(null);

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
            listaPintura.Enabled = false;
            txtCorPintura.Enabled = false;


            gridControl1.DataSource = new DataTable();

            IDMOV = "";
            xTITMMOV = new TITMMOV();
            xTITMMOV.COMPOSICAO = new List<ItemComposicao>();

            gridData2_DataSourceChanged(null, null);
            txtNCM.Set(null);

            decimalSaldoFisico.Set(null);
            decimalSaldoPedido.Set(null);
            decimalSaldoDisponivel.Set(null);
            tbPrecoPintura.EditValue = 0.00;
            tbPrecoTabela.EditValue = 0.00;
            //decimalPrecoTabela.Set(null);
            txtCorPintura.Set("");

            campoDP.Focus();
            //gbEspecs.Enabled = true;
            //HabilitarControleGroupEspecs(false);
            EventosFiltragem(true);
        }

        private void EventosFiltragem(Boolean Ativa)
        {
            if (Ativa)
            {
                medidaA.textEdit1.TextChanged += AtualizaGridItem;
                medidaB.textEdit1.TextChanged += AtualizaGridItem;
                medidaC.textEdit1.TextChanged += AtualizaGridItem;
                //medidaD.comboBox1.TextChanged += AtualizaGridItem;
                raio.comboBox1.TextChanged += AtualizaGridItem;
                peso.textEdit1.TextChanged += AtualizaGridItem;

                listaComplemento.comboBox1.TextChanged += AtualizaGridItem;
                listaAcabamento.comboBox1.TextChanged += AtualizaGridItem;
                listaChapa.comboBox1.TextChanged += AtualizaGridItem;
                listaVirola.comboBox1.TextChanged += AtualizaGridItem;
                listaSepto.comboBox1.TextChanged += AtualizaGridItem;
                listaRaio.comboBox1.TextChanged += AtualizaGridItem;
            }
            else
            {
                medidaA.textEdit1.TextChanged -= AtualizaGridItem;
                medidaB.textEdit1.TextChanged -= AtualizaGridItem;
                medidaC.textEdit1.TextChanged -= AtualizaGridItem;
                //medidaD.comboBox1.TextChanged -= AtualizaGridItem;
                raio.comboBox1.TextChanged -= AtualizaGridItem;
                peso.textEdit1.TextChanged -= AtualizaGridItem;

                listaComplemento.comboBox1.TextChanged -= AtualizaGridItem;
                listaAcabamento.comboBox1.TextChanged -= AtualizaGridItem;
                listaChapa.comboBox1.TextChanged -= AtualizaGridItem;
                listaVirola.comboBox1.TextChanged -= AtualizaGridItem;
                listaSepto.comboBox1.TextChanged -= AtualizaGridItem;
                listaRaio.comboBox1.TextChanged -= AtualizaGridItem;
            }
        }

        private void CarregaSaldo()
        {
            SaldoItem saldo = ItensOrcamento.getSaldo(CODPRD);

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
            public static string DISTANCIAMENTO { get; set; }
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
            if (campoDP.Get() != null)
            {
                if (Regex.IsMatch(campoDP.Get(), @"\d+"))
                {
                    codigoDP = Regex.Match(campoDP.Get(), @"\d+").Value;

                    if (codigoDP.Length == 3)
                    {
                        try
                        {
                            EventosFiltragem(false);
                            string sql = String.Format(@"select (select count(1) from ZTPRODUTOCOMPL where CODDP LIKE '{0}%') as 'PRODUTO',
	                                                (select count(1) from ZTPRODUTOREGRA where CODDP LIKE '{0}%') as 'REGRA'", codigoDP);

                            int CPROD = int.Parse(MetodosSQL.GetField(sql, "PRODUTO"));
                            int CREG = int.Parse(MetodosSQL.GetField(sql, "REGRA"));

                            if (CREG == 1)
                            {
                                HabilitarControleGroupEspecs(true);
                                sql = String.Format(@"select * from ZTPRODUTOREGRA where CODDP = '{0}'", codigoDP);

                                DataTable dt = MetodosSQL.GetDT(sql);

                                medidaD.Enabled = (int)dt.Rows[0]["FLAGDISTANCIAMENTO"] == 1;
                                listaVirola.Enabled = (int)dt.Rows[0]["FLAGVIROLA"] == 1;
                                raio.Enabled = (int)dt.Rows[0]["FLAGRAIO"] == 1;
                                listaRaio.Enabled = (int)dt.Rows[0]["FLAGTIPORAIO"] == 1;
                                listaSepto.Enabled = (int)dt.Rows[0]["FLAGSEPTO"] == 1;
                                listaComplemento.Enabled = (int)dt.Rows[0]["FLAGCOMPLEMENTO"] == 1;
                                peso.Enabled = (int)dt.Rows[0]["FLAGPESO"] == 1;

                                if ((int)dt.Rows[0]["FLAGLARGURA"] == 0) { medidaA.Set(null); }
                                if ((int)dt.Rows[0]["FLAGALTURA"] == 0) { medidaB.Set(null); }
                                if ((int)dt.Rows[0]["FLAGCOMPRIMENTO"] == 0) { medidaC.Set(null); }
                                if ((int)dt.Rows[0]["FLAGDISTANCIAMENTO"] == 0) { medidaD.Set(null); }
                                if ((int)dt.Rows[0]["FLAGCHAPA"] == 0) { listaChapa.Set(null); }
                                if ((int)dt.Rows[0]["FLAGACABAMENTO"] == 0) { listaAcabamento.Set(null); }
                                if ((int)dt.Rows[0]["FLAGVIROLA"] == 0) { listaVirola.Set(null); }
                                if ((int)dt.Rows[0]["FLAGRAIO"] == 0) { raio.Set(null); }
                                if ((int)dt.Rows[0]["FLAGTIPORAIO"] == 0) { listaRaio.Set(null); }
                                if ((int)dt.Rows[0]["FLAGSEPTO"] == 0) { listaSepto.Set(null); }
                                if ((int)dt.Rows[0]["FLAGCOMPLEMENTO"] == 0) { listaComplemento.Set(null); }
                                if ((int)dt.Rows[0]["FLAGPESO"] == 0) { peso.Set(null); }

                                verificaDP = false;
                                AtualizaGridItem(null, null);
                                EventosFiltragem(true);

                                CarregaComplemento();
                            }
                            else if (CPROD > 0 && CREG == 0)
                            {
                                HabilitarControleGroupEspecs(false);
                                verificaDP = false;
                                AtualizaGridItem(null, null);
                            }
                            else
                            {
                                HabilitarControleGroupEspecs(false);
                                gridControl1.DataSource = new DataTable();

                                verificaDP = false;
                                AtualizaGridItem(null, null);
                            }

                            listaPintura.Enabled = false;
                            txtCorPintura.Enabled = false;

                            if (listaAcabamento.Get() == "INOX")
                            {
                                if (!string.IsNullOrEmpty(xTITMMOV.TIPOINOX))
                                {
                                    listaTipoInox.Enabled = true;
                                }
                            }
                            else
                            {
                                listaTipoInox.Enabled = false;
                            }

                            // Carrega Distanciamento 

                            string distanciamento = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT DISTANCIAMENTO FROM TITMMOVCOMPL WHERE IDMOV = ? AND NSEQITMMOV = ?", new object[] { IDMOV, NSEQITEM }).ToString();

                            switch (distanciamento)
                            {
                                case " - Selecione":
                                    medidaD.comboBox1.SelectedIndex = 0;
                                    break;
                                case "200 - 15":
                                    medidaD.comboBox1.SelectedIndex = 1;
                                    break;
                                case "250 - 12":
                                    medidaD.comboBox1.SelectedIndex = 2;
                                    break;
                                case "300 - 10":
                                    medidaD.comboBox1.SelectedIndex = 3;
                                    break;
                                case "500 - 6":
                                    medidaD.comboBox1.SelectedIndex = 4;
                                    break;
                                default:
                                    medidaD.comboBox1.SelectedIndex = 0;
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
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
                ComplementoProduto.DISTANCIAMENTO = medidaD.Enabled ? (String.IsNullOrWhiteSpace(medidaD.Get().ToString()) ? "%" : medidaD.Get().ToString().Replace(",", ".")) : "%";
                ComplementoProduto.CHAPA = listaChapa.Enabled ? (String.IsNullOrEmpty(listaChapa.Get().ToString()) ? "%" : listaChapa.Get().ToString().Replace(",", ".")) : "%";
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
                gridView1.ActiveFilterCriteria = null;
                gridView1.ActiveFilterString = string.Empty;

                string sql = String.Empty;

                if (edita == true)
                {
                    sql = @"SELECT 
	                        TPRODUTO.CODIGOAUXILIAR,
	                        TPRODUTO.NOMEFANTASIA,
	                        TPRODUTODEF.CODUNDVENDA,
	                        0.00 AS 'PRECO',
	                        TPRODUTO.CODIGOPRD, 
	                        TPRODUTO.IDPRD                         
                        FROM 
	                        TPRODUTO

                        LEFT JOIN ZTPRODUTOCOMPL
                        ON ZTPRODUTOCOMPL.IDPRD = TPRODUTO.IDPRD
                        AND ZTPRODUTOCOMPL.CODCOLIGADA = TPRODUTO.CODCOLPRD

                        INNER JOIN TPRODUTODEF
                        ON TPRODUTODEF.CODCOLIGADA = TPRODUTO.CODCOLPRD
                        AND TPRODUTODEF.IDPRD = TPRODUTO.IDPRD

                        WHERE TPRODUTO.CODCOLPRD = 1
                        AND TPRODUTO.INATIVO = 0
                        AND TPRODUTODEF.CODTB2FAT IN ('02','03')";

                    sql = String.Format(@"{0} and CODIGOAUXILIAR = '{1}'", sql, campoLookupPRODUTO.textBoxCODIGO.Text);

                    gridControl1.DataSource = new DataTable();

                    if (!String.IsNullOrWhiteSpace(codigoDP) && verificaDP)
                    {
                        setComplementares();

                        sql = String.Format(@"  {0}                                           
                                            and ZTPRODUTOCOMPL.LARGURA like '{1}'
                                            and ZTPRODUTOCOMPL.ALTURA like '{2}'
                                            and ZTPRODUTOCOMPL.COMPRIMENTO like '{3}'
                                            and ZTPRODUTOCOMPL.DISTANCIAMENTO like '{4}'
                                            and ZTPRODUTOCOMPL.CHAPA like '{5}'
                                            and ZTPRODUTOCOMPL.ACABAMENTO like '{6}'
                                            and ZTPRODUTOCOMPL.VIROLA like '{7}'
                                            and ZTPRODUTOCOMPL.RAIO like '{8}'
                                            and ZTPRODUTOCOMPL.TIPORAIO like '{9}'
                                            and ZTPRODUTOCOMPL.SEPTO like '{10}'
                                            and ZTPRODUTOCOMPL.COMPLEMENTO like '{11}'",
                                                sql,
                                                ComplementoProduto.LARGURA,
                                                ComplementoProduto.ALTURA,
                                                ComplementoProduto.COMPRIMENTO,
                                                ComplementoProduto.DISTANCIAMENTO,
                                                ComplementoProduto.CHAPA,
                                                ComplementoProduto.ACABAMENTO,
                                                ComplementoProduto.VIROLA,
                                                ComplementoProduto.RAIO,
                                                ComplementoProduto.TIPORAIO,
                                                ComplementoProduto.SEPTO,
                                                ComplementoProduto.COMPLEMENTO);
                    }
                    else if (!String.IsNullOrWhiteSpace(CODPRD) && !verificaDP)
                    {
                        sql = String.Format(@"{0} and CODIGOPRD = '{1}'", sql, CODPRD);
                    }
                    else if (!String.IsNullOrWhiteSpace(campoDP.Get()) && !verificaDP)
                    {
                        sql = String.Format(@"{0} and CODDP = '{1}'", sql, codigoDP);
                    }

                    DataTable dtNew = MetodosSQL.GetDT(sql);

                    if (!String.IsNullOrWhiteSpace(CODPRD))
                    {
                        dtNew.Columns["PRECO"].ReadOnly = false;

                        decimal PrecoUnitario = 0;

                        foreach (DataRow comp in dtNew.Rows)
                        {
                            comp["PRECO"] = CalculoPreco.CacularPreco(comp["IDPRD"].ToString(), listaTipoInox.Get(), "", "").PRECO;

                            PrecoUnitario = Convert.ToDecimal(comp["PRECO"]);
                        }

                        if (PrecoUnitario > 0)
                        {
                            tbPrecoTabela.Enabled = true;
                            //decimalPrecoTabela.Enabled = true;
                        }
                        else
                        {
                            tbPrecoTabela.Enabled = true;
                            //decimalPrecoTabela.Enabled = true;
                        }
                    }

                    gridControl1.DataSource = dtNew;
                    gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
                    gridView1.BestFitColumns();

                    if (string.IsNullOrWhiteSpace(campoLookupPRODUTO.Get()))
                    {
                        gridView1.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;

                        if (string.IsNullOrWhiteSpace(codigoDP))
                        {
                            gridView1.FocusedColumn = gridView1.Columns["NOMEFANTASIA"];
                        }
                        else
                        {
                            gridView1.FocusedColumn = gridView1.Columns["CODIGOAUXILIAR"];
                        }

                        gridView1.Focus();
                    }
                }
                else
                {
                    if (verificaDP)
                    {
                        sql = @"SELECT 
	                    TPRODUTO.CODIGOPRD,
	                    TPRODUTO.CODIGOAUXILIAR,
	                    TPRODUTO.NOMEFANTASIA,
	                    TPRODUTODEF.CODUNDVENDA,
	                    0.00 AS 'PRECO',
	                    TPRODUTO.IDPRD
                    FROM 
	                    TPRODUTO
                    INNER JOIN TPRODUTODEF
                    ON TPRODUTODEF.CODCOLIGADA = TPRODUTO.CODCOLPRD
                    AND TPRODUTODEF.IDPRD = TPRODUTO.IDPRD
                    WHERE
	                    TPRODUTO.CODCOLPRD = 1
                    AND TPRODUTO.INATIVO = 0
                    AND TPRODUTODEF.CODTB2FAT IN ('02','03')";
                    }
                    else
                    {
                        sql = @"SELECT 
	                        TPRODUTO.CODIGOAUXILIAR,
	                        TPRODUTO.NOMEFANTASIA,
	                        TPRODUTODEF.CODUNDVENDA,
	                        0.00 AS 'PRECO',
	                        TPRODUTO.CODIGOPRD, 
	                        TPRODUTO.IDPRD                         
                        FROM 
	                        TPRODUTO

                        LEFT JOIN ZTPRODUTOCOMPL
                        ON ZTPRODUTOCOMPL.IDPRD = TPRODUTO.IDPRD
                        AND ZTPRODUTOCOMPL.CODCOLIGADA = TPRODUTO.CODCOLPRD

                        INNER JOIN TPRODUTODEF
                        ON TPRODUTODEF.CODCOLIGADA = TPRODUTO.CODCOLPRD
                        AND TPRODUTODEF.IDPRD = TPRODUTO.IDPRD

                        WHERE TPRODUTO.CODCOLPRD = 1
                        AND TPRODUTO.INATIVO = 0
                        AND TPRODUTODEF.CODTB2FAT IN ('02','03')";
                    }

                    gridControl1.DataSource = new DataTable();
                    if (!String.IsNullOrWhiteSpace(CODPRD) && verificaDP)
                    {
                        sql = String.Format(@"{0} and TPRODUTO.CODIGOPRD = '{1}'", sql, CODPRD);
                    }
                    else if (!String.IsNullOrWhiteSpace(codigoDP) && verificaDP)
                    {
                        setComplementares();

                        sql = String.Format(@"  {0}
                                            and ZTPRODUTOCOMPL.CODDP = '{1}'
                                            and ZTPRODUTOCOMPL.LARGURA like '{2}'
                                            and ZTPRODUTOCOMPL.ALTURA like '{3}'
                                            and ZTPRODUTOCOMPL.COMPRIMENTO like '{4}'
                                            and ZTPRODUTOCOMPL.DISTANCIAMENTO like '{5}'
                                            and ZTPRODUTOCOMPL.CHAPA like '{6}'
                                            and ZTPRODUTOCOMPL.ACABAMENTO like '{7}'
                                            and ZTPRODUTOCOMPL.VIROLA like '{8}'
                                            and ZTPRODUTOCOMPL.RAIO like '{9}'
                                            and ZTPRODUTOCOMPL.TIPORAIO like '{10}'
                                            and ZTPRODUTOCOMPL.SEPTO like '{11}'
                                            and ZTPRODUTOCOMPL.COMPLEMENTO like '{12}'",
                                                sql,
                                                ComplementoProduto.DP,
                                                ComplementoProduto.LARGURA,
                                                ComplementoProduto.ALTURA,
                                                ComplementoProduto.COMPRIMENTO,
                                                ComplementoProduto.DISTANCIAMENTO,
                                                ComplementoProduto.CHAPA,
                                                ComplementoProduto.ACABAMENTO,
                                                ComplementoProduto.VIROLA,
                                                ComplementoProduto.RAIO,
                                                ComplementoProduto.TIPORAIO,
                                                ComplementoProduto.SEPTO,
                                                ComplementoProduto.COMPLEMENTO);
                    }
                    else if (!String.IsNullOrWhiteSpace(CODPRD) && !verificaDP)
                    {
                        sql = String.Format(@"{0} and CODIGOPRD = '{1}'", sql, CODPRD);
                    }
                    else if (!String.IsNullOrWhiteSpace(codigoDP) && !verificaDP)
                    {
                        sql = String.Format(@"{0} and CODDP LIKE '{1}%'", sql, codigoDP);
                    }

                    DataTable dt = MetodosSQL.GetDT(sql);

                    if (!String.IsNullOrWhiteSpace(CODPRD))
                    {
                        dt.Columns["PRECO"].ReadOnly = false;

                        decimal PrecoUnitario = 0;

                        string tipoProduto = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT CODTB2FAT FROM TPRODUTODEF WHERE CODCOLIGADA = ? AND IDPRD IN (SELECT IDPRD FROM TPRODUTO WHERE CODCOLPRD = ? AND CODIGOPRD = ?)", new object[] { AppLib.Context.Empresa, AppLib.Context.Empresa, CODPRD }).ToString();
                        int calcComoAcabado = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT CALCCOMOACABADO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD IN (SELECT IDPRD FROM TPRODUTO WHERE CODCOLPRD = ? AND CODIGOPRD = ?)", new object[] { AppLib.Context.Empresa, AppLib.Context.Empresa, CODPRD }));
                        int usaPrecoCalculadoRevenda = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT USAPRECOCALCULADOREVENDA FROM ZPARAMFATURE WHERE CODCOLIGADA = ?", new object[] { AppLib.Context.Empresa }));
                        int usaPrecoFixo = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT USAPRECOFIXO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD IN (SELECT IDPRD FROM TPRODUTO WHERE CODCOLPRD = ? AND CODIGOPRD = ?)", new object[] { AppLib.Context.Empresa, AppLib.Context.Empresa, CODPRD }));
                        int usaPrecoCalculadoFixo = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT USAPRECOCALCULADOFIXO FROM ZPARAMFATURE WHERE CODCOLIGADA = ?", new object[] { AppLib.Context.Empresa }));

                        // Revenda

                        if (tipoProduto == "03" && calcComoAcabado == 0 && usaPrecoCalculadoRevenda == 1)
                        {
                            PrecoUnitario = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT PRECO1 FROM TPRODUTODEF WHERE CODCOLIGADA = ? AND IDPRD IN (SELECT IDPRD FROM TPRODUTO WHERE CODCOLPRD = ? AND CODIGOPRD = ?)", new object[] { AppLib.Context.Empresa, AppLib.Context.Empresa, CODPRD }));

                            tbPrecoTabela.Enabled = true;
                            tbPrecoTabela.EditValue = PrecoUnitario;

                            gridControl1.DataSource = dt;
                            gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
                            gridView1.BestFitColumns();

                            if (string.IsNullOrWhiteSpace(campoDP.Get()))
                            {
                                gridView1.FocusedColumn = gridView1.Columns["NOMEFANTASIA"];
                            }
                            else
                            {
                                gridView1.FocusedColumn = gridView1.Columns["CODIGOAUXILIAR"];
                            }

                            isPrecoCarregado = true;

                            return;
                        }
                        else
                        {
                            PrecoUnitario = 0;
                            tbPrecoTabela.EditValue = PrecoUnitario;

                            gridControl1.DataSource = dt;
                            gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
                            gridView1.BestFitColumns();

                            if (string.IsNullOrWhiteSpace(campoDP.Get()))
                            {
                                gridView1.FocusedColumn = gridView1.Columns["NOMEFANTASIA"];
                            }
                            else
                            {
                                gridView1.FocusedColumn = gridView1.Columns["CODIGOAUXILIAR"];
                            }

                            isPrecoCarregado = true;
                        }

                        // Acabado

                        if (tipoProduto == "02" && usaPrecoFixo == 1 && usaPrecoCalculadoFixo == 0)
                        {
                            PrecoUnitario = 0;
                            tbPrecoTabela.EditValue = PrecoUnitario;

                            gridControl1.DataSource = dt;
                            gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
                            gridView1.BestFitColumns();

                            if (string.IsNullOrWhiteSpace(campoDP.Get()))
                            {
                                gridView1.FocusedColumn = gridView1.Columns["NOMEFANTASIA"];
                            }
                            else
                            {
                                gridView1.FocusedColumn = gridView1.Columns["CODIGOAUXILIAR"];
                            }

                            isPrecoCarregado = true;

                            return;
                        }
                        else
                        {
                            PrecoUnitario = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT PRECOFIXO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD IN (SELECT IDPRD FROM TPRODUTO WHERE CODCOLPRD = ? AND CODIGOPRD = ?)", new object[] { AppLib.Context.Empresa, AppLib.Context.Empresa, CODPRD }));

                            if (PrecoUnitario == 0)
                            {
                                PrecoUnitario = CalculoPreco.CacularPreco(dt.Rows[0]["IDPRD"].ToString(), listaTipoInox.Get(), "", "").PRECO;
                            }

                            tbPrecoTabela.Enabled = true;
                            tbPrecoTabela.EditValue = PrecoUnitario;

                            gridControl1.DataSource = dt;
                            gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
                            gridView1.BestFitColumns();

                            if (string.IsNullOrWhiteSpace(campoDP.Get()))
                            {
                                gridView1.FocusedColumn = gridView1.Columns["NOMEFANTASIA"];
                            }
                            else
                            {
                                gridView1.FocusedColumn = gridView1.Columns["CODIGOAUXILIAR"];
                            }

                            isPrecoCarregado = true;

                            return;
                        }
                    }
                    else
                    {
                        gridControl1.DataSource = dt;
                        gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
                        gridView1.BestFitColumns();

                        if (string.IsNullOrWhiteSpace(campoLookupPRODUTO.Get()))
                        {
                            gridView1.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;

                            if (string.IsNullOrWhiteSpace(campoDP.Get()))
                            {
                                gridView1.FocusedColumn = gridView1.Columns["NOMEFANTASIA"];
                            }
                            else
                            {
                                gridView1.FocusedColumn = gridView1.Columns["CODIGOAUXILIAR"];
                            }

                            gridView1.Focus();

                            isPrecoCarregado = true;

                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CarregaComplemento(string codPRD = "")
        {
            try
            {
                EventosFiltragem(false);
                string sql = String.Format(@"select (select count(1) from ZTPRODUTOCOMPL where CODDP = '{0}') as 'PRODUTO',
	                                                (select count(1) from ZTPRODUTOREGRA where CODDP = '{0}') as 'REGRA'", codigoDP);

                int CPROD = int.Parse(MetodosSQL.GetField(sql, "PRODUTO"));
                int CREG = int.Parse(MetodosSQL.GetField(sql, "REGRA"));

                if (CREG == 1)
                {
                    if (string.IsNullOrEmpty(CODPRD))
                    {
                        sql = String.Format(@"select TPRODUTO.CODIGOAUXILIAR,
	                                               ZTPRODUTOCOMPL.LARGURA,
	                                               ZTPRODUTOCOMPL.ALTURA,
	                                               ZTPRODUTOCOMPL.COMPRIMENTO,
	                                               ZTPRODUTOCOMPL.DISTANCIAMENTO,
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
											and ZTPRODUTOCOMPL.IDPRD = {0}", xTITMMOV.IDPRD);
                    }
                    else
                    {
                        sql = String.Format(@"select TPRODUTO.CODIGOAUXILIAR,
	                                               ZTPRODUTOCOMPL.LARGURA,
	                                               ZTPRODUTOCOMPL.ALTURA,
	                                               ZTPRODUTOCOMPL.COMPRIMENTO,
	                                               ZTPRODUTOCOMPL.DISTANCIAMENTO,
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
											and TPRODUTO.CODIGOPRD = '{0}'", xTITMMOV.CODIGOPRD);
                    }

                    DataTable dt = MetodosSQL.GetDT(sql);

                    if (dt.Rows.Count > 0)
                    {
                        campoDP.Set(dt.Rows[0]["CODDP"].ToString());

                        if ((int)dt.Rows[0]["FLAGLARGURA"] == 1) { medidaA.Set((decimal)dt.Rows[0]["LARGURA"]); }
                        if ((int)dt.Rows[0]["FLAGALTURA"] == 1) { medidaB.Set((decimal)dt.Rows[0]["ALTURA"]); }
                        if ((int)dt.Rows[0]["FLAGCOMPRIMENTO"] == 1) { medidaC.Set((decimal)dt.Rows[0]["COMPRIMENTO"]); }

                        if (edita == false)
                        {
                            if ((int)dt.Rows[0]["FLAGDISTANCIAMENTO"] == 1) { medidaD.Set(null); }
                        }
                        else
                        {
                            if ((int)dt.Rows[0]["FLAGDISTANCIAMENTO"] == 1)
                            {
                                if (xTITMMOV.COMPOSICAO != null)
                                {
                                    for (int i = 0; i < xTITMMOV.COMPOSICAO.Count; i++)
                                    {
                                        if (xTITMMOV.COMPOSICAO[i].CODIGOPRD.ToString().StartsWith("14"))
                                        {
                                            switch (xTITMMOV.COMPOSICAO[i].QUANTIDADE)
                                            {
                                                case 15:
                                                    medidaD.comboBox1.SelectedIndex = 1;
                                                    break;
                                                case 12:
                                                    medidaD.comboBox1.SelectedIndex = 2;
                                                    break;
                                                case 10:
                                                    medidaD.comboBox1.SelectedIndex = 3;
                                                    break;
                                                case 6:
                                                    medidaD.comboBox1.SelectedIndex = 4;
                                                    break;
                                                default:
                                                    medidaD.Set(null);
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if ((int)dt.Rows[0]["FLAGCHAPA"] == 1) { listaChapa.Set((string)dt.Rows[0]["CHAPA"]); }
                        if ((int)dt.Rows[0]["FLAGACABAMENTO"] == 1) { listaAcabamento.Set((string)dt.Rows[0]["ACABAMENTO"]); }
                        if ((int)dt.Rows[0]["FLAGVIROLA"] == 1) { listaVirola.Set(dt.Rows[0]["VIROLA"].ToString()); }
                        if ((int)dt.Rows[0]["FLAGRAIO"] == 1) { raio.Set(dt.Rows[0]["RAIO"].ToString()); }
                        if ((int)dt.Rows[0]["FLAGTIPORAIO"] == 1) { listaRaio.Set(dt.Rows[0]["TIPORAIO"].ToString()); }
                        if ((int)dt.Rows[0]["FLAGSEPTO"] == 1) { listaSepto.Set(dt.Rows[0]["SEPTO"].ToString()); }
                        if ((int)dt.Rows[0]["FLAGCOMPLEMENTO"] == 1) { listaComplemento.Set(dt.Rows[0]["COMPLEMENTO"].ToString()); }
                        if ((int)dt.Rows[0]["FLAGPESO"] == 1) { peso.Set((decimal)dt.Rows[0]["PESO"]); }
                    }
                }
                else
                {
                    if (verificaDP)
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

                EventosFiltragem(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CarregaComposicao()
        {
            try
            {
                if (campoDP.textEdit1.Text == "800" || (campoDP.textEdit1.Text == "801" || (campoDP.textEdit1.Text == "802" || (campoDP.textEdit1.Text == "803" || (campoDP.textEdit1.Text == "804" || (campoDP.textEdit1.Text == "805"))))))
                {
                    if (edita == false)
                    {
                        List<ItemComposicao> listaItemComposicao = new List<ItemComposicao>();
                        string sql = String.Format(@"SELECT
	                                            TPRODUTO.CODIGOPRD,
                                                TPRODUTO.IDPRD,
	                                            TPRODUTO.CODIGOAUXILIAR,
	                                            TPRODUTO.NOMEFANTASIA,
	                                            ZTPRODUTOCOMPLEMENTO.QUANTIDADE as 'QTDUSADA',
                                                ZTPRODUTOCOMPLEMENTO.PRECO,
												ZTPRODUTOCOMPL.CODDP,
												ZTPRODUTOCOMPL.TABELAPRECO,
												ZTPRODUTOCOMPL.LARGURA,
												ZTPRODUTOCOMPL.ALTURA,
												ZTPRODUTOCOMPL.COMPRIMENTO,
												ZTPRODUTOCOMPL.DISTANCIAMENTO,
                                                ZTPRODUTOCOMPL.ACABAMENTO,
												ZTPRODUTOCOMPL.CHAPA,
                                                TPRODUTO.INATIVO,
                                                ZTPRODUTOCOMPLEMENTO.QUANTIDADE * ZTPRODUTOCOMPLEMENTO.PRECO AS 'TOTAL'
                                            FROM 
	                                            TPRODUTO

												left join ZTPRODUTOCOMPL
												on ZTPRODUTOCOMPL.CODCOLIGADA = TPRODUTO.CODCOLPRD
												and ZTPRODUTOCOMPL.IDPRD = TPRODUTO.IDPRD

                                                inner join ZTPRODUTOCOMPLEMENTO
                                                on ZTPRODUTOCOMPLEMENTO.IDPRD = TPRODUTO.IDPRD


                                            WHERE ZTPRODUTOCOMPLEMENTO.CODCOLIGADA = 1
                                            and ZTPRODUTOCOMPLEMENTO.CODFILIAL = 1
                                            and ZTPRODUTOCOMPLEMENTO.IDPRDPRINCIPAL = (select IDPRD from TPRODUTO WHERE CODIGOPRD = '{0}') 
                                            ORDER BY 1", CODPRD);

                        DataTable dt = MetodosSQL.GetDT(sql);

                        listaItemComposicao.Clear();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ParametrosPreco parametros = new ParametrosPreco();
                            ItemComposicao it = new ItemComposicao();
                            it.CODCOLIGADA = 1;
                            it.CODFILIAL = 1;
                            it.IDMOV = IDMOV == null ? 0 : int.Parse(IDMOV);
                            it.IDPRD = (int)dt.Rows[i]["IDPRD"];
                            it.CODIGOPRD = (String)dt.Rows[i]["CODIGOPRD"];
                            it.CODIGOAUXILIAR = (String)dt.Rows[i]["CODIGOAUXILIAR"];
                            it.NOMEFANTASIA = (String)dt.Rows[i]["NOMEFANTASIA"];
                            it.NSEQ = Nseq;

                            if (medidaD.comboBox1.Text == " - Selecione")
                            {
                                if (it.CODIGOPRD.StartsWith("14"))
                                {
                                    it.QUANTIDADE = 0;
                                    it.TOTAL = 0;
                                }
                                else
                                {
                                    it.QUANTIDADE = (decimal)dt.Rows[i]["QTDUSADA"];

                                    // Verificar o parâmetro da ZPARAMFATURE
                                    it.TOTAL = 0;
                                }
                            }

                            int usaPrecoCalculado = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT USAPRECOCALCULADO FROM ZPARAMFATURE WHERE CODCOLIGADA = ?", new object[] { AppLib.Context.Empresa }));

                            if (usaPrecoCalculado == 0)
                            {
                                it.PRECOUNITARIO = 0;
                                it.TOTAL = 0;
                            }
                            else
                            {
                                if ((decimal)dt.Rows[i]["PRECO"] > 0)
                                {
                                    it.PRECOUNITARIO = (decimal)dt.Rows[i]["PRECO"];
                                }
                                else
                                {
                                    RetornoPreco retorno = CalculoPreco.CacularPreco(it.CODIGOPRD, listaTipoInox.Get(), "", "");

                                    it.PRECOUNITARIO = retorno.PRECO;
                                }
                            }

                            listaItemComposicao.Add(it);
                        }

                        xTITMMOV.COMPOSICAO = listaItemComposicao;
                    }
                    else
                    {
                        List<ItemComposicao> listaItemComposicao = new List<ItemComposicao>();
                        string sql = String.Format(@"SELECT 
	                                                ZORCAMENTOITEMCOMPOSTO.CODCOLIGADA,
	                                                ZORCAMENTOITEMCOMPOSTO.CODFILIAL,
	                                                ZORCAMENTOITEMCOMPOSTO.IDMOV,
	                                                ZORCAMENTOITEMCOMPOSTO.IDPRD,
	                                                TPRODUTO.CODIGOPRD,
	                                                TPRODUTO.CODIGOAUXILIAR,
	                                                TPRODUTO.NOMEFANTASIA,
	                                                ZORCAMENTOITEMCOMPOSTO.QUANTIDADE,
	                                                ZORCAMENTOITEMCOMPOSTO.PRECOUNITARIO,
	                                                (ZORCAMENTOITEMCOMPOSTO.QUANTIDADE * ZORCAMENTOITEMCOMPOSTO.PRECOUNITARIO) AS TOTAL
                                                FROM 
	                                                TPRODUTO, 
	                                                ZORCAMENTOITEMCOMPOSTO
                                                WHERE
	                                                TPRODUTO.CODCOLPRD = ZORCAMENTOITEMCOMPOSTO.CODCOLIGADA
                                                AND TPRODUTO.IDPRD = ZORCAMENTOITEMCOMPOSTO.IDPRD
                                                AND ZORCAMENTOITEMCOMPOSTO.CODCOLIGADA = {0}
                                                AND ZORCAMENTOITEMCOMPOSTO.IDMOV = '{1}'
                                                AND ZORCAMENTOITEMCOMPOSTO.NSEQ = {2}", AppLib.Context.Empresa, IDMOV, NSEQITEM);

                        DataTable dt = MetodosSQL.GetDT(sql);

                        if (dt.Rows.Count < 0)
                        {
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
                                it.QUANTIDADE = (decimal)item["QUANTIDADE"];
                                it.TOTAL = (decimal)item["TOTAL"];

                                //it.UNIDADEMEDIDA = (String)item["UN"];

                                if ((decimal)item["PRECOUNITARIO"] > 0)
                                {
                                    it.PRECOUNITARIO = (decimal)item["PRECOUNITARIO"];
                                }
                                else
                                {
                                    RetornoPreco retorno = CalculoPreco.CacularPreco(it.CODIGOPRD, listaTipoInox.Get(), "", "");

                                    it.PRECOUNITARIO = retorno.PRECO;
                                }

                                listaItemComposicao.Add(it);
                            }

                            xTITMMOV.COMPOSICAO = listaItemComposicao;
                        }
                        else
                        {
                            gridData2_DataSourceChanged(null, null);
                        }
                    }
                }

                else
                {
                    xTITMMOV.COMPOSICAO = new List<ItemComposicao>();

                    try
                    {
                        AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction("DELETE FROM ZORCAMENTOITEMCOMPOSTO WHERE CODCOLIGADA = ? AND IDMOV = ? AND NSEQ = ?", new object[] { AppLib.Context.Empresa, IDMOV, NSEQITEM });
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                gridData2_DataSourceChanged(null, null);
            }
            catch (Exception)
            {

            }
        }

        private void FormOrcamentoItem_Load(object sender, EventArgs e)
        {
            toolStrip1.Enabled = false;

            if (descItem)
            {
                campoDecimalPRECOUNITARIO.Enabled = false;
                decimalPrecoTotal.Enabled = false;
            }

            if (consumidorFinal)
            {
                AppLib.Windows.CodigoNome[] a = new AppLib.Windows.CodigoNome[3];

                a[0] = new AppLib.Windows.CodigoNome("1", "Uso/Consumo");
                a[1] = new AppLib.Windows.CodigoNome("2", "Uso/Consumo Sem ST");
                a[2] = new AppLib.Windows.CodigoNome("6", "Ativo Imobilizado");

                campoListaAplicacao.Lista = a;
                campoListaAplicacao.Set(AplicaProd);
            }

            if (!edita)
            {
                listaPintura.Set(null);
                // Seta o tipo de formato
                tbPrecoPintura.EditValue = 0.00;
                campoListaAplicacao.Set(AplicaProd);
                campoDP.Focus();
                tbQuantidade.Enabled = true;
                //dtEntrega.Set(dataEntrega);
                txtNUMPEDIDO.Set(pedidoCliente);
                checkInsercaoRapida.Checked = inserirNovoItem;
                listaTipoInox.Set(null);
                clCFOP.Enabled = false;

                tbAjusteValor.EditValue = 0.00;
            }
            else
            {
                string sql;

                if (xTITMMOV.IDPRD > 0)
                {
                    sql = String.Format(@"select CODDP from ZTPRODUTOCOMPL where CODCOLIGADA = 1 and CODFILIAL = 1 and IDPRD = '{0}'", xTITMMOV.IDPRD);

                    string DP = AppLib.Context.poolConnection.Get("Start").ExecGetField("", sql).ToString();

                    campoDP.Set(DP);

                    //campoDP.Set(MetodosSQL.GetField(sql, "CODDP"));
                }

                campoLookupPRODUTO_AposSelecao(null, null);

                sql = String.Format(@"select SITUACAOMERCADORIA from TPRDFISCAL where CODCOLIGADA = 1 and IDPRD = {0}", xTITMMOV.IDPRD);
                if (MetodosSQL.GetField(sql, "SITUACAOMERCADORIA") == "01")
                {

                }
                else
                {
                    int usaPrecoCalculado = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT USAPRECOCALCULADO FROM ZPARAMFATURE WHERE CODCOLIGADA = ?", new object[] { AppLib.Context.Empresa }));

                    if (usaPrecoCalculado == 0)
                    {
                        if (xTITMMOV.COMPOSICAO != null)
                        {
                            for (int i = 0; i < xTITMMOV.COMPOSICAO.Count; i++)
                            {
                                xTITMMOV.COMPOSICAO[i].PRECOUNITARIO = 0;
                            }
                        }
                    }

                    gridData2.gridControl1.DataSource = xTITMMOV.COMPOSICAO;
                    gridData2.Atualizar();
                }

                CarregaGridTributo();

                txtCorPintura.Enabled = listaPintura.Enabled = Convert.ToDecimal(tbPrecoPintura.Text) > 0;

                checkInsercaoRapida.Enabled = false;
                label14.Enabled = false;

                if (xTITMMOV.AJUSTEVALOR == 0)
                {
                    string valorAjustado = "";

                    if (this.Acao == AppLib.Global.Types.Acao.Editar && IDMOV != null)
                    {
                        string sqlAjusteValor = String.Format(@"SELECT AJUSTEVALOR FROM TITMMOVCOMPL WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " AND NSEQITMMOV = {0} AND IDMOV = {1}", xTITMMOV.NSEQITEMMOV, IDMOV);
                        valorAjustado = MetodosSQL.GetField(sqlAjusteValor, "AJUSTEVALOR");
                    }

                    if (string.IsNullOrEmpty(valorAjustado))
                    {
                        tbAjusteValor.Text = xTITMMOV.AJUSTEVALOR.ToString();
                    }
                    else
                    {
                        tbAjusteValor.EditValue = valorAjustado == "" ? 0 : Convert.ToDecimal(valorAjustado);
                    }
                }
                else
                {
                    tbAjusteValor.Text = xTITMMOV.AJUSTEVALOR.ToString();
                }

                if (xTITMMOV.TIPOAJUSTEVALOR != null)
                {
                    if (xTITMMOV.TIPOAJUSTEVALOR == 1)
                    {
                        rbSoma.Checked = true;
                    }
                    else
                    {
                        rbSubtracao.Checked = true;
                    }
                }
                else
                {
                    string tipoAjusteValor = "";

                    if (this.Acao == AppLib.Global.Types.Acao.Editar && IDMOV != null)
                    {
                        string sqlTipoAjusteValor = String.Format(@"SELECT TIPOAJUSTEVALOR FROM TITMMOVCOMPL WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " AND NSEQITMMOV = {0} AND IDMOV = {1}", xTITMMOV.NSEQITEMMOV, IDMOV);
                        tipoAjusteValor = MetodosSQL.GetField(sqlTipoAjusteValor, "TIPOAJUSTEVALOR");
                    }

                    if (string.IsNullOrEmpty(tipoAjusteValor))
                    {
                        if (xTITMMOV.TIPOAJUSTEVALOR == 1)
                        {
                            rbSoma.Checked = true;
                        }
                        else
                        {
                            rbSubtracao.Checked = true;
                        }
                    }
                    else
                    {
                        if (tipoAjusteValor == "1")
                        {
                            rbSoma.Checked = true;
                        }
                        else
                        {
                            rbSubtracao.Checked = true;
                        }
                    }
                }

                if (xTITMMOV.PRECOTABELA != null)
                {
                    tbPrecoTabela.EditValue = xTITMMOV.PRECOTABELA;
                    //decimalPrecoTabela.Set(xTITMMOV.PRECOTABELA);
                }
                else
                {
                    string PrecoTabela = "";

                    if (this.Acao == AppLib.Global.Types.Acao.Editar && IDMOV != null)
                    {
                        string sqlPrecoTabela = String.Format(@"SELECT PRECOTABELA FROM TITMMOVCOMPL WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " AND NSEQITMMOV = {0} AND IDMOV = {1}", xTITMMOV.NSEQITEMMOV, IDMOV);
                        PrecoTabela = MetodosSQL.GetField(sqlPrecoTabela, "PRECOTABELA");
                    }

                    if (string.IsNullOrEmpty(PrecoTabela))
                    {
                        tbPrecoTabela.EditValue = xTITMMOV.AJUSTEVALOR;
                        //decimalPrecoTabela.Set(xTITMMOV.AJUSTEVALOR);
                    }
                    else
                    {
                        tbPrecoTabela.EditValue = (PrecoTabela == "" ? 0 : Convert.ToDecimal(PrecoTabela));
                        //decimalPrecoTabela.Set(PrecoTabela == "" ? 0 : Convert.ToDecimal(PrecoTabela));
                    }
                }

                if (xTITMMOV.PRECOUNITARIO != 0)
                {
                    campoDecimalPRECOUNITARIO.Set(xTITMMOV.PRECOUNITARIO);
                }
                else
                {
                    string sqlPrecoUnitario = String.Format(@"SELECT PRECOUNITARIO FROM TITMMOVCOMPL WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " AND NSEQITMMOV = {0} AND IDMOV = {1}", xTITMMOV.NSEQITEMMOV, IDMOV);
                    string PrecoUnitario = MetodosSQL.GetField(sqlPrecoUnitario, "PRECOTABELA");

                    if (string.IsNullOrEmpty(PrecoUnitario))
                    {
                        campoDecimalPRECOUNITARIO.Set(xTITMMOV.PRECOUNITARIO);
                    }
                    else
                    {
                        tbPrecoTabela.EditValue = (PrecoUnitario == "" ? 0 : Convert.ToDecimal(PrecoUnitario));
                        //decimalPrecoTabela.Set(PrecoUnitario == "" ? 0 : Convert.ToDecimal(PrecoUnitario));
                    }
                }

                AtualizaValoresCampos();
            }

            string INICIAL = string.Empty;

            if (CODETD == "SP")
            {
                INICIAL = "5";
            }
            else if (CODETD == "EX")
            {
                INICIAL = "7";
            }
            else
            {
                INICIAL = "6";
            }

            clCFOP.ColunaTabela = $@"(select CODNAT, NOME 
                                        from DCFOP 
                                        where CODCOLIGADA = 1 
                                        and ATIVO = 1 
                                        and FISCAL = 1 
                                        and SUBSTRING(CODNAT, 1, 1) in ({INICIAL}) 
                                        and LEN(CODNAT) > 5) W";

            campoListaAplicacao.comboBox1.SelectedValueChanged += setCFOP;

            this.ActiveControl = campoDP;

            if (edita == true)
            {
                // Os itens precisam estar disponíveis para o pedido
                //btnClean.Enabled = false;
                //campoDP.Enabled = false;
                //campoLookupPRODUTO.Enabled = false;
                //campoLookupPRODUTO.textBoxCODIGO.Enabled = false;
            }

            if (POSSUIBENEFICIO == false)
            {
                clCFOP.Enabled = false;
            }
            else
            {
                clCFOP.Enabled = true;
                clCFOP.EditaLookup = true;

                clCFOP.textBoxCODIGO.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
                clCFOP.textBoxCODIGO.Properties.Mask.EditMask = "#.###.###";
            }

            // Seta a localização fixa dos botões
            simpleButtonOK.Location = new System.Drawing.Point(990, 16);
            simpleButtonCANCELAR.Location = new System.Drawing.Point(1071, 16);
            simpleButtonSALVAR.Location = new System.Drawing.Point(909, 16);

            if (edita == false)
            {
                tbNumeroSequencial.Text = Nseq.ToString();
            }
            else
            {
                tbNumeroSequencial.Text = NSEQITEM.ToString();
            }
        }

        #region EVENTOS DO FORM

        private bool FormOrcamentoItem_Validar(object sender, EventArgs e)
        {
            if (CODPRD == string.Empty)
            {
                MessageBox.Show("Campo Produto obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (campoTextoCODUND.Get() == string.Empty)
            {
                MessageBox.Show("Campo Unidade obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToDecimal(tbQuantidade.Text) <= 0)
            {
                MessageBox.Show("Campo Quantidade deve ser maior que zero.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (clCFOP.textBoxCODIGO.Text.Trim() == "")
            {
                MessageBox.Show("Campo CFOP não encontrado", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (campoDecimalPRECOUNITARIO.Get() <= 0)
            {
                MessageBox.Show("O preço unitário não pode ser menor ou igual à zero", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (xTITMMOV.COMPOSICAO != null)
            {
                foreach (ItemComposicao item in xTITMMOV.COMPOSICAO)
                {
                    int usaPrecoCalculado = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT USAPRECOCALCULADO FROM ZPARAMFATURE WHERE CODCOLIGADA = ?", new object[] { AppLib.Context.Empresa }));

                    if (usaPrecoCalculado != 0)
                    {
                        if (item.PRECOUNITARIO == 0)
                        {
                            MessageBox.Show("Existem itens na composição com o preço 0", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
            }

            try
            {
                if (Convert.ToDecimal(tbPrecoPintura.Text) > 0 && listaPintura.comboBox1.Text == " - Selecione")
                {
                    MessageBox.Show("Necessário informar o tipo de pintura.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (Convert.ToDecimal(tbPrecoPintura.Text) > 0 && string.IsNullOrWhiteSpace(txtCorPintura.Get()))
                {
                    MessageBox.Show("Necessário informar a cor da pintura.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            try
            {
                if (!String.IsNullOrWhiteSpace(txtNuMITEMPEDIDO.Get()))
                {
                    int.Parse(txtNuMITEMPEDIDO.Get());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Numero Item Pedido deve conter apenas numeros", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                if (txtNUMPEDIDO.Get() != null)
                {
                    if (txtNUMPEDIDO.Get().Length > 15)
                    {
                        MessageBox.Show("Numero Pedido não pode ser maior que 15", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                //
            }


            try
            {
                if (txtNuMITEMPEDIDO.Get() != null)
                {
                    if (txtNuMITEMPEDIDO.Get().Length > 6)
                    {
                        MessageBox.Show("Numero Item Pedido não pode ser maior que 6", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                //
            }

            try
            {
                if (medidaD.Enabled == true && medidaD.comboBox1.Text == " - Selecione")
                {
                    MessageBox.Show("É necessário informar o campo de Distanciamento.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception)
            {
                //
            }

            dtEntrega.Set(null);

            return true;
        }

        public Boolean ProdutoTemComposicao()
        {
            return true;
        }

        private void FormOrcamentoItem_Preparar(object sender, EventArgs e)
        {

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
            FormProdutoVisao f = new FormProdutoVisao();

            return f.MostrarLookup(campoLookupPRODUTO);
        }

        public bool IsIDPRD(string texto)
        {
            if (this.contemLetras(texto) && this.contemNumeros(texto))
            {
                //Contem Letras e Números
                return false;
            }
            else if (this.contemLetras(texto))
            {
                //Contem somente letras
                return false;
            }
            else
            {
                //Contem somente numeros
                return true;
            }
        }

        public bool contemLetras(string texto)
        {
            if (texto.Where(c => char.IsLetter(c)).Count() > 0)
                return true;
            else
                return false;
        }

        public bool contemNumeros(string texto)
        {
            if (texto.Where(c => char.IsNumber(c)).Count() > 0)
                return true;
            else
                return false;
        }

        private void campoLookupPRODUTO_AposSelecao(object sender, EventArgs e)
        {
            string consulta1 = "";

            if (!IsIDPRD(campoLookupPRODUTO.Get()))
            {
                consulta1 = string.Format(@"SELECT CODIGOPRD, NOMEFANTASIA from TPRODUTO where CODIGOAUXILIAR = '{0}' AND CODCOLPRD = " + AppLib.Context.Empresa + "", campoLookupPRODUTO.Get());
            }
            else
            {
                consulta1 = string.Format(@"SELECT CODIGOPRD, NOMEFANTASIA from TPRODUTO where IDPRD = '{0}' AND CODCOLPRD = " + AppLib.Context.Empresa + "", campoLookupPRODUTO.Get());
            }

            String NomeFantasia = MetodosSQL.GetField(consulta1, "NOMEFANTASIA");
            CODPRD = MetodosSQL.GetField(consulta1, "CODIGOPRD");

            consulta1 = @"SELECT TPRD.CODUNDVENDA, ISNULL(TTRBPRD.ALIQUOTA, 0) ALIQUOTA
                                FROM TPRD
                                LEFT JOIN TTRBPRD ON TPRD.CODCOLIGADA = TTRBPRD.CODCOLIGADA 
                                  AND TPRD.IDPRD = TTRBPRD.IDPRD
                                  AND TTRBPRD.CODTRB = 'IPI' WHERE TPRD.CODCOLIGADA = ? AND TPRD.CODIGOPRD = ?";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(consulta1, new Object[] { AppLib.Context.Empresa, CODPRD });

            consulta1 = string.Format(@"select ZTPC.CODDP from TPRODUTO TP

                                            inner join ZTPRODUTOCOMPL ZTPC
                                            on ZTPC.CODCOLIGADA = TP.CODCOLPRD
                                            and ZTPC.IDPRD = TP.IDPRD

                                            where TP.CODCOLPRD = {0} 
                                            and TP.CODIGOPRD = '{1}'", AppLib.Context.Empresa, CODPRD);

            if (campoDP.Get() == null)
            {
                campoDP.Set(MetodosSQL.GetField(consulta1, "CODDP"));
            }

            if (dt.Rows.Count > 0)
            {
                System.Data.DataRow dr = dt.Rows[0];

                campoTextoCODUND.Set(dr["CODUNDVENDA"].ToString());
                campoDecimalALIQUOTA.Set(Convert.ToDecimal(dr["ALIQUOTA"].ToString()));
                tbQuantidade.Text = "1";
                //campoDecimalPRECOUNITARIO.Set(0);
                consulta1 = String.Format(@"select NUMEROCCF from TPRODUTO where CODCOLPRD = " + AppLib.Context.Empresa + " and CODIGOPRD = '{0}'", CODPRD);
                txtNCM.Set(MetodosSQL.GetField(consulta1, "NUMEROCCF"));
                setCFOP(null, null);
                CarregaSaldo();
                CarregaComplemento();
                AtualizaGridItem(null, null);

                //xTITMMOV.COMPOSICAO = new List<ItemComposicao>();
                //gridData2_DataSourceChanged(null, null);

                //AtualizarObjeto();

                //2020-06-21 19:02
                //HabilitarControleGroupEspecs(false);

                if (listaAcabamento.Get() == "INOX")
                {
                    if (!string.IsNullOrEmpty(xTITMMOV.TIPOINOX))
                    {
                        listaTipoInox.Enabled = true;
                    }
                }

                campoDP.Enabled = false;

                CarregaComposicao();
                CarregaPreco();

                listaPintura.Enabled = false;
                txtCorPintura.Enabled = false;

                campoLookupPRODUTO.textBoxDESCRICAO.Text = NomeFantasia;

                if (edita)
                {
                    btnClean.Enabled = true;
                    btnClean.Visible = true;
                }

                tbQuantidade.Focus();

                if (campoDP.textEdit1.Text == "800" || (campoDP.textEdit1.Text == "801" || (campoDP.textEdit1.Text == "802" || (campoDP.textEdit1.Text == "803" || (campoDP.textEdit1.Text == "804" || (campoDP.textEdit1.Text == "805"))))))
                {
                    medidaD.Enabled = true;
                }

                if (edita == true)
                {
                    // Tipo Pintura
                    if (listaPintura.Get() == null)
                    {
                        listaPintura.comboBox1.SelectedIndex = 0;
                    }

                    // Tipo Inox
                    if (listaTipoInox.Get() == null)
                    {
                        listaTipoInox.comboBox1.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                MessageBox.Show("Erro ao obter dados do produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            tbQuantidade.Focus();
            tbQuantidade.Select();
        }

        #endregion

        #region EVENTOS DE MANIPULAÇÃO DOS DADOS

        public void AtualizarObjeto()
        {
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery("SELECT IDPRD, NUMEROCCF FROM TPRD WHERE CODCOLIGADA = ? AND CODIGOPRD = ?", new Object[] { AppLib.Context.Empresa, CODPRD });

            if (dt.Rows.Count > 0)
            {
                System.Data.DataRow dr = dt.Rows[0];
                xTITMMOV.IDPRD = int.Parse(dr["IDPRD"].ToString());
                xTITMMOV.NUMEROCCF = dr["NUMEROCCF"].ToString();
            }

            xTITMMOV.NSEQITEMMOV = Nseq.ToString();
            xTITMMOV.NUMEROSEQUENCIAL = numeroSequencial;
            xTITMMOV.CODIGOPRD = CODPRD;
            xTITMMOV.CODAUXILIAR = campoLookupPRODUTO.textBoxCODIGO.Text;
            xTITMMOV.PRODUTO = campoLookupPRODUTO.textBoxDESCRICAO.Text;
            xTITMMOV.UNIDADE = campoTextoCODUND.Get();
            xTITMMOV.QUANTIDADE = Convert.ToDecimal(tbQuantidade.Text);
            xTITMMOV.PRECOUNITARIO = (decimal)campoDecimalPRECOUNITARIO.Get();
            xTITMMOV.VALORTOTAL = (decimal)decimalPrecoTotal.Get();
            xTITMMOV.ALIQUOTAIPI = (decimal)campoDecimalALIQUOTA.Get();
            xTITMMOV.HISTORICOLONGO = campoMemoHISTORICOLONGO.Get();
            xTITMMOV.DATAENTREGA = null;
            xTITMMOV.IDNAT = ItensOrcamento.getIDNAT(clCFOP.Get());
            xTITMMOV.APLICACAO = campoListaAplicacao.Get();

            if (txtNCM.Get() == null)
            {
                string consulta1 = String.Format(@"select NUMEROCCF from TPRODUTO where CODCOLPRD = 1 and CODIGOPRD = '{0}'", CODPRD);
                txtNCM.Set(MetodosSQL.GetField(consulta1, "NUMEROCCF"));
            }

            xTITMMOV.NUMEROCCF = txtNCM.Get();
            xTITMMOV.NUMEROCFOP = clCFOP.Get();
            xTITMMOV.NUMPEDIDO = txtNUMPEDIDO.Get();
            xTITMMOV.NUMITEMPEDIDO = txtNuMITEMPEDIDO.Get();
            xTITMMOV.TIPOINOX = listaTipoInox.Get();
            xTITMMOV.VALORPINTURA = Convert.ToDecimal(tbPrecoPintura.Text);
            xTITMMOV.TIPOPINTURA = listaPintura.Get();
            xTITMMOV.CORPINTURA = txtCorPintura.Get();
            xTITMMOV.AJUSTEVALOR = Convert.ToDecimal(tbAjusteValor.Text);
            xTITMMOV.DISTANCIAMENTO = medidaD.comboBox1.Text == " - Selecione" ? "" : medidaD.comboBox1.Text;

            if (rbSoma.Checked)
            {
                xTITMMOV.TIPOAJUSTEVALOR = 1;
            }
            else
            {
                xTITMMOV.TIPOAJUSTEVALOR = 0;
            }

            if (!edita)
            {
                xTITMMOV.VALORDESCORIGINAL = 0;
                xTITMMOV.VALORDESPORIGINAL = 0;
                inserirNovoItem = checkInsercaoRapida.Checked;
            }
            else
            {
                inserirNovoItem = false;
            }

            xTITMMOV.PRECOTABELA = Convert.ToDecimal(tbPrecoTabela.EditValue);
        }

        public void AtualizarForm()
        {
            campoLookupPRODUTO.textBoxCODIGO.Text = xTITMMOV.CODAUXILIAR;
            campoLookupPRODUTO.textBoxDESCRICAO.Text = xTITMMOV.PRODUTO;
            campoTextoCODUND.Set(xTITMMOV.UNIDADE);
            tbQuantidade.Text = xTITMMOV.QUANTIDADE.ToString();
            campoDecimalPRECOUNITARIO.Set(xTITMMOV.PRECOUNITARIO);
            campoDecimalALIQUOTA.Set(xTITMMOV.ALIQUOTAIPI);
            campoMemoHISTORICOLONGO.Set(xTITMMOV.HISTORICOLONGO);
            campoListaAplicacao.Set(xTITMMOV.APLICACAO);
            txtNUMPEDIDO.Set(xTITMMOV.NUMPEDIDO);
            txtNuMITEMPEDIDO.Set(xTITMMOV.NUMITEMPEDIDO);
            clCFOP.Set(xTITMMOV.NUMEROCFOP);
            listaTipoInox.Set(xTITMMOV.TIPOINOX);
            tbPrecoPintura.Text = xTITMMOV.VALORPINTURA.ToString();
            listaPintura.Set(xTITMMOV.TIPOPINTURA);
            medidaD.Set(xTITMMOV.DISTANCIAMENTO);
            txtCorPintura.Set(xTITMMOV.CORPINTURA);
            tbAjusteValor.Text = xTITMMOV.AJUSTEVALOR.ToString();

            if (xTITMMOV.TIPOAJUSTEVALOR == 1)
            {
                rbSoma.Checked = true;
            }
            else
            {
                if (this.Acao == AppLib.Global.Types.Acao.Editar && IDMOV != null)
                {
                    string sqlTipoAjusteValor = String.Format(@"SELECT TIPOAJUSTEVALOR FROM TITMMOVCOMPL WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " AND NSEQITMMOV = {0} AND IDMOV = {1}", xTITMMOV.NSEQITEMMOV, IDMOV);
                    string tipoAjusteValor = MetodosSQL.GetField(sqlTipoAjusteValor, "TIPOAJUSTEVALOR");

                    if (tipoAjusteValor == "0")
                    {
                        rbSubtracao.Checked = true;
                    }
                }
            }

            numeroSequencial = xTITMMOV.NUMEROSEQUENCIAL;
        }

        #endregion

        private void campoLookupPRODUTO_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM ZVWTPRD WHERE CODCOLIGADA = ? AND CODIGOPRD = ?";
            campoLookupPRODUTO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupPRODUTO.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, CODPRD }).ToString();
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
            string CFOP = ItensOrcamento.getCFOP(CODETD, AplicaProd, CODPRD, consumidorFinal ? "1" : "0");
            if (!String.IsNullOrWhiteSpace(CFOP))
            {
                if (POSSUIBENEFICIO)
                {
                    clCFOP.Enabled = true;
                    clCFOP.EditaLookup = true;

                    clCFOP.textBoxCODIGO.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
                    clCFOP.textBoxCODIGO.Properties.Mask.EditMask = "#.###.###";
                }
                else
                {
                    clCFOP.Set(CFOP);
                    clCFOP.Enabled = false;
                }

            }
            else
            {
                clCFOP.Set("Não encontrado.");
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                return;
            }

            var obj = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CODIGOAUXILIAR");

            campoLookupPRODUTO.textBoxCODIGO.Text = obj.ToString();

            campoLookupPRODUTO.textBox1_Leave(null, null);

            tbQuantidade.Focus();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            edita = false;
            LimpaCampos();
        }

        private void gridData2_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                gridData2.gridControl1.DataSource = xTITMMOV.COMPOSICAO;
                gridData2.gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void gridData2_Novo(object sender, EventArgs e)
        {


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
                    frm.TIPOINOX = listaTipoInox.Get();
                    frm.ShowDialog();

                    if (frm.RETORNO != null && !String.IsNullOrEmpty(frm.RETORNO.CODIGOPRD))
                    {
                        gridData2.Atualizar();
                        CarregaPreco();
                    }
                }
                else
                {
                    MessageBox.Show("Selecione apenas um item para editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {

            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void listaAcabamento_AposSelecao(object sender, EventArgs e)
        {
            if (listaAcabamento.Get() == null)
            {
                if (listaAcabamento.Get() == "INOX")
                {
                    listaTipoInox.Enabled = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(xTITMMOV.TIPOINOX))
                    {
                        listaTipoInox.Enabled = false;
                    }
                    else
                    {
                        listaTipoInox.Enabled = true;
                    }
                }
            }
            else
            {
                xTITMMOV.TIPOINOX = listaAcabamento.Get();
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void listaTipoInox_AposSelecao(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(listaTipoInox.Get()) && listaAcabamento.Get() == "INOX")
            {
                CarregaPreco();

            }
            else if ((listaTipoInox.Get() == "304" || listaTipoInox.Get() == "316") && listaAcabamento.Get() == "INOX")
            {
                CarregaPreco();
            }
        }

        private void campoLista1_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtNuMITEMPEDIDO_Load(object sender, EventArgs e)
        {

        }

        private void gridControl1_EditorKeyDown(object sender, KeyEventArgs e)
        {


        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var obj = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CODIGOAUXILIAR");

                if (obj != null)
                {
                    campoLookupPRODUTO.textBoxCODIGO.Text = obj.ToString();
                    campoLookupPRODUTO.textBox1_Leave(null, null);

                    tbQuantidade.Focus();
                }
            }
        }

        private void campoDP_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void campoDP_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void campoDP_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!string.IsNullOrEmpty(campoDP.Get()))
            {
                if (Regex.IsMatch(campoDP.textEdit1.Text, @"\d+"))
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        if (string.IsNullOrWhiteSpace(campoDP.Get()))
                        {
                            AtualizaGridItem(null, null);
                        }
                    }
                }
            }
        }

        private void FormOrcamentoItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(campoLookupPRODUTO.Get()))
                {
                    inserirNovoItem = false;
                }
            }
            catch (Exception)
            {
                //
            }
        }

        private void label64_Click(object sender, EventArgs e)
        {

        }

        private void listaAcabamento_Load(object sender, EventArgs e)
        {

        }

        private void campoLookupPRODUTO_Click(object sender, EventArgs e)
        {

        }

        private void campoLookupPRODUTO_Validated(object sender, EventArgs e)
        {

        }

        private void medidaD_Validated(object sender, EventArgs e)
        {

        }

        private void medidaD_AutoValidateChanged(object sender, EventArgs e)
        {

        }

        private void medidaD_AposSelecao(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(campoDP.Get()))
            {
                if (campoDP.textEdit1.Text == "800" || (campoDP.textEdit1.Text == "801" || (campoDP.textEdit1.Text == "802" || (campoDP.textEdit1.Text == "803" || (campoDP.textEdit1.Text == "804" || (campoDP.textEdit1.Text == "805"))))))
                {
                    if (!string.IsNullOrEmpty(medidaD.comboBox1.Text) && medidaD.comboBox1.Text != " - Selecione")
                    {
                        var itemComposicao = xTITMMOV.COMPOSICAO;

                        if (medidaD.comboBox1.SelectedIndex > 0)
                        {
                            var quantidadeByDistanciamento = medidaD.comboBox1.Text.Substring(6);

                            if (itemComposicao != null)
                            {
                                for (int i = 0; i < itemComposicao.Count; i++)
                                {
                                    if (itemComposicao[i].CODIGOPRD.StartsWith("14"))
                                    {
                                        //xTITMMOV.COMPOSICAO[i].QUANTIDADE = Convert.ToDecimal(quantidadeByDistanciamento);

                                        //gridData2.Atualizar();

                                        //CarregaPreco();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void campoMemoHISTORICOLONGO_Leave(object sender, EventArgs e)
        {
            simpleButtonOK.Select();
        }

        private void campoDecimalAjusteValor_Validated(object sender, EventArgs e)
        {
            campoDecimalPRECOUNITARIO.Set(RecalculaPrecoUnitario());

            if (rbSoma.Checked)
            {
                campoDecimalPRECOUNITARIO.Set(campoDecimalPRECOUNITARIO.Get() + Convert.ToDecimal(tbAjusteValor.Text));
            }
            else if (rbSubtracao.Checked)
            {
                campoDecimalPRECOUNITARIO.Set(campoDecimalPRECOUNITARIO.Get() - Convert.ToDecimal(tbAjusteValor.Text));
            }

            decimalPrecoTotal.Set(Convert.ToDecimal(tbQuantidade.Text) * campoDecimalPRECOUNITARIO.Get());
        }

        private decimal? RecalculaPrecoUnitario()
        {
            decimal? valorPintura = Convert.ToDecimal(tbPrecoPintura.Text);
            decimal? PrecoTabela = Convert.ToDecimal(tbPrecoTabela.EditValue);
            decimal Desconto = xTITMMOV.VALORDESCORIGINAL;
            decimal Despesa = xTITMMOV.VALORDESPORIGINAL;

            decimal? PrecoUnitario = valorPintura + PrecoTabela - Desconto + Despesa;

            return PrecoUnitario;
        }

        private decimal CalculaValorTotal(decimal _valorTotal, decimal _ajusteValor)
        {
            decimal ValorTotal = _valorTotal;

            if (rbSoma.Checked)
            {
                ValorTotal = ValorTotal + _ajusteValor;
            }
            else
            {
                ValorTotal = ValorTotal - _ajusteValor;
            }

            return ValorTotal;
        }

        private void rbSoma_CheckedChanged(object sender, EventArgs e)
        {
            campoDecimalPRECOUNITARIO.Set(RecalculaPrecoUnitario());

            if (rbSoma.Checked)
            {
                campoDecimalPRECOUNITARIO.Set(campoDecimalPRECOUNITARIO.Get() + Convert.ToDecimal(tbAjusteValor.Text));
            }
            else if (rbSubtracao.Checked)
            {
                campoDecimalPRECOUNITARIO.Set(campoDecimalPRECOUNITARIO.Get() - Convert.ToDecimal(tbAjusteValor.Text));
            }

            decimalPrecoTotal.Set(Convert.ToDecimal(tbQuantidade.Text) * campoDecimalPRECOUNITARIO.Get());
        }

        private void rbSubtracao_CheckedChanged(object sender, EventArgs e)
        {
            campoDecimalPRECOUNITARIO.Set(RecalculaPrecoUnitario());

            if (rbSoma.Checked)
            {
                campoDecimalPRECOUNITARIO.Set(campoDecimalPRECOUNITARIO.Get() + Convert.ToDecimal(tbAjusteValor.Text));
            }
            else if (rbSubtracao.Checked)
            {
                campoDecimalPRECOUNITARIO.Set(campoDecimalPRECOUNITARIO.Get() - Convert.ToDecimal(tbAjusteValor.Text));
            }

            decimalPrecoTotal.Set(Convert.ToDecimal(tbQuantidade.Text) * campoDecimalPRECOUNITARIO.Get());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool ValidaComposicao(int IDPRD)
        {
            bool valida = false;

            if (Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT COUNT (*) FROM ZTPRODUTOCOMPLEMENTO WHERE CODCOLIGADA = 1 AND IDPRDPRINCIPAL = ?", new object[] { IDPRD })) > 0)
            {
                valida = true;
            }

            return valida;
        }

        private void tbPrecoTabela_TextChanged(object sender, EventArgs e)
        {
            AtualizaValoresCampos();
        }

        private void tbQuantidade_TextChanged(object sender, EventArgs e)
        {
            AtualizaValoresCampos();
        }

        private void tbPrecoPintura_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(tbPrecoPintura.Text))
                {
                    if (Convert.ToDecimal(tbPrecoPintura.Text) > 0)
                    {
                        listaPintura.Enabled = true;

                        if (listaPintura.comboBox1.SelectedIndex > 0)
                        {
                            AtualizaValoresCampos();
                            return;
                        }
                        else
                        {
                            listaPintura.comboBox1.SelectedIndex = 0;
                        }

                        //listaTipoInox.comboBox1.SelectedIndex = 0;

                        txtCorPintura.Enabled = true;
                    }
                    else
                    {
                        listaPintura.Enabled = false;
                        txtCorPintura.Enabled = false;

                        //listaTipoInox.comboBox1.SelectedIndex = 0;
                        listaPintura.comboBox1.SelectedIndex = 0;
                    }

                    AtualizaValoresCampos();
                }
            }
            catch (Exception)
            {

            }
        }

        private void txtCorPintura_Leave(object sender, EventArgs e)
        {
            //if (listaPintura.Get() != null)
            //{
            //    if (medidaD.Enabled && !string.IsNullOrEmpty(medidaD.Get()))
            //    {
            //        if (string.IsNullOrEmpty(campoMemoHISTORICOLONGO.richTextBox1.Text))
            //        {
            //            campoMemoHISTORICOLONGO.Set("DISTANCIAMENTO: " + medidaD.comboBox1.Text);
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(listaPintura.Get()) && !string.IsNullOrEmpty(txtCorPintura.textEdit1.Text))
            //    {
            //        if (string.IsNullOrEmpty(campoMemoHISTORICOLONGO.richTextBox1.Text) || campoMemoHISTORICOLONGO.richTextBox1.Text.Contains("DISTANCIAMENTO"))
            //        {
            //            if (!campoMemoHISTORICOLONGO.richTextBox1.Text.Contains("PINTURA") || !campoMemoHISTORICOLONGO.richTextBox1.Text.Contains("COR"))
            //            {
            //                campoMemoHISTORICOLONGO.Set(campoMemoHISTORICOLONGO.Get() + (" PINTURA: " + listaPintura.comboBox1.Text.Remove(0, 4) + " COR: " + txtCorPintura.Get()).ToUpper());
            //            }
            //        }
            //    }
            //}
        }

        private void FormOrcamentoItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Insert)
            {
                this.FormOrcamentoItem_Validar(this, null);
                this.FormOrcamentoItem_Salvar2(this, null);

                this.Dispose();
            }
        }

        private void btnCopiarInformacoesComplemento_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(medidaD.Get()))
            {
                campoMemoHISTORICOLONGO.Set("DISTANCIAMENTO: " + medidaD.comboBox1.Text.Substring(0, 3));
            }

            if (listaPintura.Get() != null && !string.IsNullOrEmpty(txtCorPintura.textEdit1.Text))
            {
                if (!string.IsNullOrEmpty(campoMemoHISTORICOLONGO.Get()))
                {
                    campoMemoHISTORICOLONGO.Set(campoMemoHISTORICOLONGO.Get() + " - PINTURA: " + listaPintura.comboBox1.Text.Remove(0, 4));
                }
                else
                {
                    campoMemoHISTORICOLONGO.Set("PINTURA: " + listaPintura.comboBox1.Text.Remove(0, 4));
                }
            }

            if (txtCorPintura.Get() != null)
            {
                if (!string.IsNullOrEmpty(campoMemoHISTORICOLONGO.Get()))
                {
                    campoMemoHISTORICOLONGO.Set(campoMemoHISTORICOLONGO.Get() + " - COR: " + txtCorPintura.Get().ToString().ToUpper());
                }
                else
                {
                    campoMemoHISTORICOLONGO.Set(" - COR: " + txtCorPintura.Get().ToString().ToUpper());
                }
            }
        }

        private void campoDP_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
