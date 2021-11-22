using AppFatureClient.Classes;
using AppLib.Windows;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormProdutoCadastro : AppLib.Windows.FormCadastroData
    {
        private int CODCOLPRD_COPY;
        private int IDPRD_COPY;

        private string EnderecoEmail;
        private int PortaEmail;
        private string UsuarioEmail;
        private string SenhaEmail;
        private bool SSL;
        private string EnviarComo;
        private string EnviarComoDisplay;
        private string EnviarPara;
        private string Procedencia;
        private string Enderecos;
        bool edita = false;
        bool verificaDP = true;
        int i;

        public int IDPRD;

        #region Variáveis

        private bool isPrecoCalculadoValidado = false;
        private bool isCaregandoComponentes = true;

        #endregion

        public FormProdutoCadastro(bool edita = false)
        {
            InitializeComponent();

            gridData2.GetProcessos().Add("Inserir Estados", null, InserirEstados);

            campoTexto1.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            //campoTexto1.textEdit1.Properties.Mask.EditMask = "00.000.000.0000";

            campoTexto11.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            campoTexto11.textEdit1.Properties.Mask.EditMask = "0000.00.00";

            campoTextoIDPRD.textEdit1.TextChanged += new System.EventHandler(campoTextoIDPRD_TextChanged);

            List<ProdutoNivel> PrdNivel = new List<ProdutoNivel>();
            PrdNivel.Add(new ProdutoNivel { ValueMember = "0", DisplayMember = "00" });
            PrdNivel.Add(new ProdutoNivel { ValueMember = "1", DisplayMember = "00.00" });
            PrdNivel.Add(new ProdutoNivel { ValueMember = "2", DisplayMember = "00.00.000" });
            PrdNivel.Add(new ProdutoNivel { ValueMember = "3", DisplayMember = "00.00.000.00000" });
            campoLista5.DataSource = PrdNivel;
            campoLista5.DisplayMember = "DisplayMember";
            campoLista5.ValueMember = "ValueMember";

            this.edita = edita;

            this.Text = "Cadastro de Produtos";

            tabControl1.TabPages.Remove(tabPage11);
            tabControl1.TabPages.Remove(tabPage12);

            //cbCodigoDP.TextChanged += LiberaCamposComplementares;

            listaChapa.Set(null);
            listaAcabamento.Set(null);
            listaVirola.Set(null);
            listaRaio.Set(null);
            listaSepto.Set(null);
            listaComplemento.Set(null);
            listaTipoInox.Set(null);

            DataTable dt = MetodosSQL.GetDT(@"SELECT '' as 'COD'
                                                union
                                                SELECT DISTINCT(CODIGO) as 'COD' FROM ZTPRODUTOTABPRECO WHERE CODCOLIGADA = 1");

            AppLib.Windows.CodigoNome[] a = new AppLib.Windows.CodigoNome[dt.Rows.Count];


            int i = 0;
            foreach (DataRow prod in dt.Rows)
            {
                a[i] = new AppLib.Windows.CodigoNome(prod["COD"].ToString(), "");
                i++;
            }

            listaProduto.Lista = a;
            listaProduto.Set(null);

            tabControl1.TabPages.Remove(tabPage10);
            tabControl1.TabPages.Remove(tabPage8);
        }

        private void InserirEstados(object sender, EventArgs e)
        {
            try
            {
                ProdutoTributos.InsereEstados(int.Parse(campoTextoIDPRD.Get()));
                gridData2.Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                if (verificaDP)
                {
                    string sql = String.Format(@"select * from ZTPRODUTOREGRA where CODDP = '{0}'", cbCodigoDP.SelectedItem.ToString());

                    DataTable dt = MetodosSQL.GetDT(sql);

                    if (dt.Rows.Count == 1)
                    {
                        tbLargura.Enabled = (int)dt.Rows[0]["FLAGLARGURA"] == 1;
                        tbAltura.Enabled = (int)dt.Rows[0]["FLAGALTURA"] == 1;
                        tbComprimento.Enabled = (int)dt.Rows[0]["FLAGCOMPRIMENTO"] == 1;
                        medidaD.Enabled = (int)dt.Rows[0]["FLAGDISTANCIAMENTO"] == 1;
                        listaChapa.Enabled = (int)dt.Rows[0]["FLAGCHAPA"] == 1;
                        listaAcabamento.Enabled = (int)dt.Rows[0]["FLAGACABAMENTO"] == 1;
                        listaVirola.Enabled = (int)dt.Rows[0]["FLAGVIROLA"] == 1;
                        raio.Enabled = (int)dt.Rows[0]["FLAGRAIO"] == 1;
                        listaRaio.Enabled = (int)dt.Rows[0]["FLAGTIPORAIO"] == 1;
                        listaSepto.Enabled = (int)dt.Rows[0]["FLAGSEPTO"] == 1;
                        listaComplemento.Enabled = (int)dt.Rows[0]["FLAGCOMPLEMENTO"] == 1;
                        //peso.Enabled = (int)dt.Rows[0]["FLAGPESO"] == 1;

                        if (((int)dt.Rows[0]["FLAGVIROLA"] == 1) && ((int)dt.Rows[0]["FLAGSEPTO"] == 1))
                        {
                            listaVirola.Set("1");
                            listaSepto.Set("1");
                        }

                        if ((int)dt.Rows[0]["FLAGLARGURA"] == 0) { tbLargura.EditValue = null; }
                        if ((int)dt.Rows[0]["FLAGALTURA"] == 0) { tbAltura.EditValue = null; }
                        if ((int)dt.Rows[0]["FLAGCOMPRIMENTO"] == 0) { tbComprimento.EditValue = null; }
                        if ((int)dt.Rows[0]["FLAGDISTANCIAMENTO"] == 0) { medidaD.Set(null); }
                        if ((int)dt.Rows[0]["FLAGCHAPA"] == 0) { listaChapa.Set(null); }
                        if ((int)dt.Rows[0]["FLAGACABAMENTO"] == 0) { listaAcabamento.Set(null); }
                        if ((int)dt.Rows[0]["FLAGVIROLA"] == 0) { listaVirola.Set(null); }
                        if ((int)dt.Rows[0]["FLAGRAIO"] == 0) { raio.Set(null); }
                        if ((int)dt.Rows[0]["FLAGTIPORAIO"] == 0) { listaRaio.Set(null); }
                        if ((int)dt.Rows[0]["FLAGSEPTO"] == 0) { listaSepto.Set(null); }
                        if ((int)dt.Rows[0]["FLAGCOMPLEMENTO"] == 0) { listaComplemento.Set(null); }
                        //if ((int)dt.Rows[0]["FLAGPESO"] == 0) { peso.Set(null); }

                        listaProduto.Enabled = true;

                        string formula = dt.Rows[0]["FORMULAPRECO"].ToString();

                        txtFormula.Set(formula);

                        if (dt.Rows[0]["USAPRECOFIXO"] != DBNull.Value)
                        {
                            if (Convert.ToInt32(dt.Rows[0]["USAPRECOFIXO"]) == 1)
                            {
                                cePrecoFixo.Checked = true;
                                cePrecoFixo.Enabled = false;
                                listaProduto.Enabled = false;
                            }
                            else
                            {
                                cePrecoFixo.Checked = false;
                                cePrecoFixo.Enabled = true;
                                listaProduto.Enabled = true;
                            }
                        }
                        else
                        {
                            cePrecoFixo.Checked = false;
                            cePrecoFixo.Enabled = true;
                            listaProduto.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CarregaValores()
        {
            RetornoPreco retorno = CalculoPreco.CacularPreco(campoTextoIDPRD.Get(), listaTipoInox.Get(), listaProduto.Get(), listaAcabamento.Get(), tbLargura.Text, tbAltura.Text, tbComprimento.Text, medidaD.comboBox1.Text.Substring(6).ToString());

            if (retorno.MSG == "Não foi possivel definir um valor para o produto pois a data de vigência está fora do período de vigência.")
            {
                retorno = CalculoPreco.CarregaValoresByParametros(campoTextoIDPRD.Get(), listaTipoInox.Get());

                tbPrecoAcabado.EditValue = retorno.PRECO;
                txtFormula.Set(retorno.FORMULAFINAL);
            }
            else
            {
                tbPrecoAcabado.EditValue = retorno.PRECO;
                txtFormula.Set(retorno.FORMULAFINAL);
            }
        }

        private void CalculaPreco()
        {
            RetornoPreco retorno = CalculoPreco.CacularPreco(campoTextoIDPRD.Get(), listaTipoInox.Get(), listaProduto.Get(), listaAcabamento.Get(), tbLargura.Text, tbAltura.Text, tbComprimento.Text, (medidaD.Enabled == true ? medidaD.comboBox1.Text.Substring(6).ToString() : "0"));

            // Erro: Exibir a mensagem de erro de validação da DP
            if (!String.IsNullOrWhiteSpace(retorno.MSG))
            {
                if (!isCaregandoComponentes)
                {
                    MessageBox.Show(retorno.MSG);
                }

                isPrecoCalculadoValidado = false;
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(campoTextoIDPRD.Get()) && !cePrecoFixo.Checked)
                {
                    tbPrecoAcabado.EditValue = retorno.PRECO;
                    txtFormula.Set(retorno.FORMULAFINAL);
                    //txtFormula.Set(FormataFormula(txtFormula.textEdit1.Text, campoTextoIDPRD.Get(), listaTipoInox.Get()));
                    isPrecoCalculadoValidado = true;
                }
                else
                {
                    txtFormula.textEdit1.ResetText();
                    isPrecoCalculadoValidado = true;
                }
            }
        }

        private string FormataFormula(string formula, string IDPRD, string tipoInox)
        {
            string sql = String.Format(@"select ZTTPC.PESO, PRECOKG, ZTP.FORMULAPRECO, ZTC.LARGURA, ZTC.ALTURA, ZTC.COMPRIMENTO, ZTC.DISTANCIAMENTO from TPRODUTO TP

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
													and ZTTP.TIPO = (case when ZTC.ACABAMENTO = 'INOX' then '{1}' else ZTTP.TIPO end)", IDPRD, tipoInox);

            DataTable dtFormula = MetodosSQL.GetDT(sql);

            decimal peso = 0;
            decimal preco = 0;
            decimal DISTANCIAMENTO = 0;

            foreach (DataRow tabPreco in dtFormula.Rows)
            {
                peso = (Decimal)tabPreco["PESO"];
                preco = (Decimal)tabPreco["PRECOKG"];
                DISTANCIAMENTO = (int)tabPreco["DISTANCIAMENTO"];
                formula = tabPreco["FORMULAPRECO"].ToString();
            }

            formula = formula.Replace("PS", peso.ToString());

            formula = formula.Replace("PR", preco.ToString());

            formula = formula.Replace("A", tbLargura.Text);

            formula = formula.Replace("B", tbAltura.Text);

            formula = formula.Replace("C", tbComprimento.Text);

            if (medidaD.Enabled == true)
            {
                formula = formula.Replace("D", medidaD.comboBox1.Text.Substring(6).ToString());
            }
            else
            {
                formula = formula.Replace("D", "0");
            }


            return formula;
        }

        private void CarregaComplemento()
        {
            try
            {
                string sql = String.Format(@"select TPRODUTO.CODIGOAUXILIAR,
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
                                                   ZTPRODUTOCOMPL.TABELAPRECO,
                                                   ZTPRODUTOCOMPL.PRECOCALCULADO,
												   ZTPRODUTOREGRA.* from ZTPRODUTOCOMPL

                                            inner join TPRODUTO
                                            on TPRODUTO.IDPRD = ZTPRODUTOCOMPL.IDPRD
                                            and TPRODUTO.CODCOLPRD = ZTPRODUTOCOMPL.CODCOLIGADA

											inner join ZTPRODUTOREGRA
											on ZTPRODUTOREGRA.CODDP = ZTPRODUTOCOMPL.CODDP
											and ZTPRODUTOREGRA.CODCOLIGADA = ZTPRODUTOCOMPL.CODCOLIGADA

											where ZTPRODUTOCOMPL.CODCOLIGADA = 1
                                            and ZTPRODUTOCOMPL.CODFILIAL = 1
											and TPRODUTO.IDPRD = '{0}'", campoTextoIDPRD.Get());

                DataTable dt = MetodosSQL.GetDT(sql);

                if (dt.Rows.Count == 1)
                {
                    ParametrosPreco parametros = new ParametrosPreco();

                    cbCodigoDP.SelectedItem = dt.Rows[0]["CODDP"].ToString();

                    if (!cePrecoFixo.Checked)
                    {
                        if (cbCodigoDP.SelectedItem.ToString().Length <= 3)
                        {
                            if (campoLookup2.textBoxCODIGO.Text == "02" && (Convert.ToInt32(cbCodigoDP.SelectedItem.ToString().Substring(0, 3)) < 800 || Convert.ToInt32(cbCodigoDP.SelectedItem.ToString().Substring(0, 3)) > 805))
                            {
                                //if (!isCaregandoComponentes)
                                //{
                                //    CarregaValores();
                                //}

                                //isCaregandoComponentes = false;
                            }
                            else if (campoLookup2.textBoxCODIGO.Text == "03" && chkCalculadoFormulado.Checked == false)
                            {
                                txtFormula.textEdit1.ResetText();
                            }
                        }
                    }

                    if (dt.Rows[0]["CHAPA"] != DBNull.Value)
                        if ((int)dt.Rows[0]["FLAGCHAPA"] == 1) { listaChapa.Set((string)dt.Rows[0]["CHAPA"]); }

                    if (dt.Rows[0]["ACABAMENTO"] != DBNull.Value)
                        if ((int)dt.Rows[0]["FLAGACABAMENTO"] == 1) { listaAcabamento.Set((string)dt.Rows[0]["ACABAMENTO"]); }

                    if (dt.Rows[0]["TABELAPRECO"] == DBNull.Value) { listaProduto.Set(null); } else { listaProduto.Set(dt.Rows[0]["TABELAPRECO"].ToString()); }

                    if (dt.Rows[0]["LARGURA"] != DBNull.Value)
                        if ((int)dt.Rows[0]["FLAGLARGURA"] == 1) { tbLargura.EditValue = (decimal)dt.Rows[0]["LARGURA"]; /*txtFormula.Set(txtFormula.Get().Replace("A", medidaA.Get().ToString()));*/ }
                    if (dt.Rows[0]["ALTURA"] != DBNull.Value)
                        if ((int)dt.Rows[0]["FLAGALTURA"] == 1) { tbAltura.EditValue = (decimal)dt.Rows[0]["ALTURA"]; /*txtFormula.Set(txtFormula.Get().Replace("B", medidaB.Get().ToString()));*/ }
                    if (dt.Rows[0]["COMPRIMENTO"] != DBNull.Value)
                        if ((int)dt.Rows[0]["FLAGCOMPRIMENTO"] == 1) { tbComprimento.EditValue = (decimal)dt.Rows[0]["COMPRIMENTO"]; /*txtFormula.Set(txtFormula.Get().Replace("C", medidaC.Get().ToString()));*/ }
                    if (dt.Rows[0]["DISTANCIAMENTO"] != DBNull.Value)
                        if ((int)dt.Rows[0]["FLAGDISTANCIAMENTO"] == 1) { medidaD.Set(dt.Rows[0]["DISTANCIAMENTO"].ToString()); /*txtFormula.Set(txtFormula.Get().Replace("D", medidaD.Get().ToString()));*/ }
                    if (dt.Rows[0]["COMPLEMENTO"] != DBNull.Value)
                        if ((int)dt.Rows[0]["FLAGCOMPLEMENTO"] == 1) { listaComplemento.Set(dt.Rows[0]["COMPLEMENTO"].ToString()); }
                    if (dt.Rows[0]["VIROLA"] != DBNull.Value)
                        if ((int)dt.Rows[0]["FLAGVIROLA"] == 1) { listaVirola.Set(dt.Rows[0]["VIROLA"].ToString()); }
                    if (dt.Rows[0]["RAIO"] != DBNull.Value)
                        if ((int)dt.Rows[0]["FLAGRAIO"] == 1) { raio.Set(dt.Rows[0]["RAIO"].ToString()); }
                    if (dt.Rows[0]["TIPORAIO"] != DBNull.Value)
                        if ((int)dt.Rows[0]["FLAGTIPORAIO"] == 1) { listaRaio.Set(dt.Rows[0]["TIPORAIO"].ToString()); }
                    if (dt.Rows[0]["SEPTO"] != DBNull.Value)
                        if ((int)dt.Rows[0]["FLAGSEPTO"] == 1) { listaSepto.Set(dt.Rows[0]["SEPTO"].ToString()); }

                    if (medidaD.Get() == null)
                    {
                        medidaD.comboBox1.SelectedIndex = 0;
                    }

                    if (dt.Rows[0]["PRECOCALCULADO"] != DBNull.Value)
                    {
                        tbPrecoAcabado.EditValue = Convert.ToDecimal(dt.Rows[0]["PRECOCALCULADO"]);
                    }
                    else
                    {
                        tbPrecoAcabado.EditValue = null;
                    }
                }
                else
                {
                    if (verificaDP)
                    {
                        sql = String.Format(@"select CODDP from ZTPRODUTOCOMPL 
                                                where CODCOLIGADA = 1
                                                and CODFILIAL = 1
                                                and IDPRD = {0}",
                                                campoTextoIDPRD.Get());

                        cbCodigoDP.SelectedItem = MetodosSQL.GetField(sql, "CODDP");
                    }
                    else
                    {
                        cbCodigoDP.SelectedItem = null;
                    }


                    tbLargura.EditValue = null; tbLargura.Enabled = false;
                    tbAltura.EditValue = null; tbAltura.Enabled = false;
                    tbComprimento.EditValue = null; tbComprimento.Enabled = false;
                    medidaD.Set(null); medidaD.Enabled = false;
                    //peso.Set(null); peso.Enabled = false;
                    raio.Set(null); raio.Enabled = false;

                    listaChapa.Set(null); listaChapa.Enabled = false;
                    listaAcabamento.Set(null); listaAcabamento.Enabled = false;
                    listaVirola.Set(null); listaVirola.Enabled = false;
                    listaRaio.Set(null); listaRaio.Enabled = false;
                    listaSepto.Set(null); listaSepto.Enabled = false;
                    listaComplemento.Set(null); listaComplemento.Enabled = false;

                    txtFormula.Set(null);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["PRECOCALCULADO"] != DBNull.Value)
                        {
                            tbPrecoAcabado.EditValue = Convert.ToDecimal(dt.Rows[0]["PRECOCALCULADO"]);
                        }
                        else
                        {
                            tbPrecoAcabado.EditValue = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void campoTextoIDPRD_TextChanged(object sender, EventArgs e)
        {
            string sSQL = @"SELECT CODIGOPRD FROM TPRD WHERE CODIGOPRD = ? AND CODCOLIGADA = ?";
            object CodigoPrd = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(null, sSQL, new Object[] { campoTexto1.Get(), AppLib.Context.Empresa });
            if (CodigoPrd != null)
            {
                if (CodigoPrd.ToString().Length == 14)
                {
                    campoLista5.SelectedIndex = 3;
                }

                if (CodigoPrd.ToString().Length == 10)
                {
                    campoLista5.SelectedIndex = 2;
                }

                if (CodigoPrd.ToString().Length == 6)
                {
                    campoLista5.SelectedIndex = 1;
                }

                if (CodigoPrd.ToString().Length == 2)
                {
                    campoLista5.SelectedIndex = 0;
                }

                if (campoTexto2.Get() != null && campoTexto3.Get() != null)
                {
                    this.Text = "Cadastro de Produtos - " + campoTexto2.Get().ToString() + " - " + campoTexto3.Get().ToString();
                }
            }
        }

        #region LOOKUP

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODFAB, NOME FROM TFAB WHERE CODCOLIGADA = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup2_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODTB2FAT, DESCRICAO FROM TTB2 WHERE CODCOLIGADA = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup2, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup3_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODTB3FAT, DESCRICAO FROM TTB3 WHERE CODCOLIGADA = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup3, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup4_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODTB4FAT, DESCRICAO FROM TTB4 WHERE CODCOLIGADA = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup4, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup5_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODTB5FAT, DESCRICAO FROM TTB5 WHERE CODCOLIGADA = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup5, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup6_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODTBORCAMENTO, DESCRICAO FROM TTBORCAMENTO WHERE CODCOLIGADA = ?";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup6, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup7_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT SIMBOLO, DESCRICAO FROM GMOEDA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup7, consulta1, new Object[] { });
        }

        private bool campoLookup8_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT SIMBOLO, DESCRICAO FROM GMOEDA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup8, consulta1, new Object[] { });
        }

        private bool campoLookup9_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT SIMBOLO, DESCRICAO FROM GMOEDA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup9, consulta1, new Object[] { });
        }

        private bool campoLookup10_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT SIMBOLO, DESCRICAO FROM GMOEDA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup10, consulta1, new Object[] { });
        }

        private bool campoLookup11_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT SIMBOLO, DESCRICAO FROM GMOEDA";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup11, consulta1, new Object[] { });
        }

        private bool campoLookup12_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODUND, DESCRICAO FROM TUND";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup12, consulta1, new Object[] { });
        }

        private bool campoLookup13_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODUND, DESCRICAO FROM TUND";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup13, consulta1, new Object[] { });
        }

        private bool campoLookup14_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODUND, DESCRICAO FROM TUND";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup14, consulta1, new Object[] { });
        }

        private bool campoLookup15_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODCLIENTE, DESCRICAO FROM GCONSIST  WHERE CODCOLIGADA = ? AND APLICACAO = 'T' AND CODTABELA = 'SIMNAO'";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup15, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup16_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT CODCLIENTE, DESCRICAO FROM GCONSIST  WHERE CODCOLIGADA = ? AND APLICACAO = 'T' AND CODTABELA = 'SIMNAO'";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup16, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private void campoLookup15_SetDescricao(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT DESCRICAO FROM GCONSIST WHERE CODCOLIGADA = ? AND APLICACAO = 'T' AND CODTABELA = 'SIMNAO' AND CODCLIENTE = ?";
            campoLookup15.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta1, new Object[] { AppLib.Context.Empresa, campoLookup15.textBoxCODIGO.Text }).ToString();
        }

        private void campoLookup16_SetDescricao(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT DESCRICAO FROM GCONSIST WHERE CODCOLIGADA = ? AND APLICACAO = 'T' AND CODTABELA = 'SIMNAO' AND CODCLIENTE = ?";
            campoLookup16.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta1, new Object[] { AppLib.Context.Empresa, campoLookup16.textBoxCODIGO.Text }).ToString();
        }

        #endregion

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            gridData1.Parametros = new object[] { campoTexto14.Get(), campoTextoIDPRD.Get() };
        }


        #region FormProdutoCompostoCadastro
        private void gridData1_Editar(object sender, EventArgs e)
        {
            FormProdutoCompostoCadastro f = new FormProdutoCompostoCadastro();
            f.CodColigada = Convert.ToInt32(campoTexto14.Get());
            f.IdPrd = Convert.ToInt32(campoTextoIDPRD.Get());
            f.Editar(gridData1.bs);
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            FormProdutoCompostoCadastro f = new FormProdutoCompostoCadastro();
            f.CodColigada = Convert.ToInt32(campoTexto14.Get());
            f.IdPrd = Convert.ToInt32(campoTextoIDPRD.Get());
            f.Excluir(gridData1.GetDataRows());
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            FormProdutoCompostoCadastro f = new FormProdutoCompostoCadastro();
            f.CodColigada = Convert.ToInt32(campoTexto14.Get());
            f.IdPrd = Convert.ToInt32(campoTextoIDPRD.Get());
            f.Novo();
        }
        #endregion

        private void mnCopiarProduto_Click(object sender, EventArgs e)
        {
            if (!campoTextoIDPRD.Get().Equals("") || !campoTextoIDPRD.Get().Equals("0"))
            {
                IDPRD_COPY = Convert.ToInt32(campoTextoIDPRD.Get());
                CODCOLPRD_COPY = Convert.ToInt32(campoTexto14.Get());

                mnColarProduto.Enabled = true;
            }
        }

        private void mnColarProduto_Click(object sender, EventArgs e)
        {
            if (IDPRD_COPY > 0)
            {
                //IDPRD
                campoTextoIDPRD.Set("0");
                campoTexto5.Set("");
                campoTexto15.Set("");
                campoTexto19.Set("");
                campoTexto17.Set("");

                string sSQL = string.Empty;
                sSQL = @"SELECT * FROM TPRODUTO WHERE CODCOLPRD = ? AND IDPRD = ?";
                sSQL = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSQL, new Object[] { CODCOLPRD_COPY, IDPRD_COPY });
                DataTable dtTPRODUTO = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSQL, new Object[] { });
                foreach (DataRow row in dtTPRODUTO.Rows)
                {
                    campoLista1.Set(row["TIPO"].ToString());
                    campoTexto1.Set(row["CODIGOPRD"].ToString());
                    campoTexto2.Set(row["CODIGOAUXILIAR"].ToString());
                    campoLista2.Set(row["ULTIMONIVEL"].ToString());
                    campoLista3.Set(row["INATIVO"].ToString());
                    campoTexto20.Set(row["CODIGOREDUZIDO"].ToString());
                    campoTexto3.Set(row["NOMEFANTASIA"].ToString());
                    campoMemo1.Set(row["DESCRICAO"].ToString());
                    campoMemo2.Set(row["DESCRICAOAUX"].ToString());
                    campoDecimal1.Set((row["PESOBRUTO"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["PESOBRUTO"]));
                    campoDecimal2.Set((row["COMPRIMENTO"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["COMPRIMENTO"]));
                    campoDecimal3.Set((row["LARGURA"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["LARGURA"]));
                    campoTexto12.Set(row["QTDEVOLUME"].ToString());
                    campoTexto9.Set(row["OBSERVACAO"].ToString());
                    campoTexto10.Set(row["CAMPOLIVRE2"].ToString());
                    campoTexto11.Set(row["NUMEROCCF"].ToString());
                    campoLista4.Set(row["REFERENCIACP"].ToString());

                }

                sSQL = @"SELECT * FROM TPRODUTODEF WHERE CODCOLIGADA = ? AND IDPRD = ?";
                sSQL = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSQL, new Object[] { CODCOLPRD_COPY, IDPRD_COPY });
                DataTable dtTPRODUTODEF = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSQL, new Object[] { });
                foreach (DataRow row in dtTPRODUTODEF.Rows)
                {
                    campoLookup1.textBoxCODIGO.Text = row["CODFAB"].ToString();
                    campoLookup1.textBox1_Leave(this, null);
                    campoTexto4.Set(row["NUMNOFABRIC"].ToString());
                    campoLookup2.textBoxCODIGO.Text = row["CODTB2FAT"].ToString();
                    campoLookup2.textBox1_Leave(this, null);
                    campoLookup3.textBoxCODIGO.Text = row["CODTB3FAT"].ToString();
                    campoLookup3.textBox1_Leave(this, null);
                    campoLookup4.textBoxCODIGO.Text = row["CODTB4FAT"].ToString();
                    campoLookup4.textBox1_Leave(this, null);
                    campoLookup5.textBoxCODIGO.Text = row["CODTB5FAT"].ToString();
                    campoLookup5.textBox1_Leave(this, null);
                    campoLookup6.textBoxCODIGO.Text = row["CODTBORCAMENTO"].ToString();
                    campoLookup6.textBox1_Leave(this, null);
                    campoLookup7.textBoxCODIGO.Text = row["CODMOEPRECO1"].ToString();
                    campoLookup7.textBox1_Leave(this, null);
                    campoLookup8.textBoxCODIGO.Text = row["CODMOEPRECO2"].ToString();
                    campoLookup8.textBox1_Leave(this, null);
                    campoLookup9.textBoxCODIGO.Text = row["CODMOEPRECO3"].ToString();
                    campoLookup9.textBox1_Leave(this, null);
                    campoLookup10.textBoxCODIGO.Text = row["CODMOEPRECO4"].ToString();
                    campoLookup10.textBox1_Leave(this, null);
                    campoLookup11.textBoxCODIGO.Text = row["CODMOEPRECO5"].ToString();
                    campoLookup11.textBox1_Leave(this, null);
                    campoData1.Set((row["DATABASEPRECO1"] == DBNull.Value) ? null : (DateTime?)Convert.ToDateTime(row["DATABASEPRECO1"]));
                    campoData2.Set((row["DATABASEPRECO2"] == DBNull.Value) ? null : (DateTime?)Convert.ToDateTime(row["DATABASEPRECO2"]));
                    campoData3.Set((row["DATABASEPRECO3"] == DBNull.Value) ? null : (DateTime?)Convert.ToDateTime(row["DATABASEPRECO3"]));
                    campoData4.Set((row["DATABASEPRECO4"] == DBNull.Value) ? null : (DateTime?)Convert.ToDateTime(row["DATABASEPRECO4"]));
                    campoData5.Set((row["DATABASEPRECO5"] == DBNull.Value) ? null : (DateTime?)Convert.ToDateTime(row["DATABASEPRECO5"]));
                    tbPrecoRevenda.EditValue = (row["PRECO1"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["PRECO1"]);
                    campoDecimal5.Set((row["PRECO2"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["PRECO2"]));
                    campoDecimal6.Set((row["PRECO3"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["PRECO3"]));
                    campoDecimal7.Set((row["PRECO4"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["PRECO4"]));
                    campoDecimal8.Set((row["PRECO5"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["PRECO5"]));
                    campoDecimal9.Set((row["TOLERANCIASUP"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["TOLERANCIASUP"]));
                    campoDecimal10.Set((row["TOLERANCIAINF"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["TOLERANCIAINF"]));
                    campoDecimal11.Set((row["TOLINFPRECO"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["TOLINFPRECO"]));
                    campoDecimal12.Set((row["TOLSUPPRECO"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["TOLSUPPRECO"]));
                    campoLookup12.textBoxCODIGO.Text = row["CODUNDCONTROLE"].ToString();
                    campoLookup12.textBox1_Leave(this, null);
                    campoLookup13.textBoxCODIGO.Text = row["CODUNDCOMPRA"].ToString();
                    campoLookup13.textBox1_Leave(this, null);
                    campoLookup14.textBoxCODIGO.Text = row["CODUNDVENDA"].ToString();
                    campoLookup14.textBox1_Leave(this, null);
                    campoData6.Set((row["DATACUSTOMEDIO"] == DBNull.Value) ? null : (DateTime?)Convert.ToDateTime(row["DATACUSTOMEDIO"]));
                    campoDecimal13.Set((row["CUSTOMEDIO"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["CUSTOMEDIO"]));
                    campoData7.Set((row["DTCUSTOUNITARIO"] == DBNull.Value) ? null : (DateTime?)Convert.ToDateTime(row["DTCUSTOUNITARIO"]));
                    campoDecimal14.Set((row["CUSTOUNITARIO"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(row["CUSTOUNITARIO"]));
                }

                sSQL = @"SELECT * FROM TPRDHISTORICO WHERE CODCOLIGADA = ? AND IDPRD = ?";
                sSQL = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSQL, new Object[] { CODCOLPRD_COPY, IDPRD_COPY });
                DataTable dtTPRDHISTORICO = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSQL, new Object[] { });
                foreach (DataRow row in dtTPRDHISTORICO.Rows)
                {
                    campoMemo3.Set(row["HISTORICOLONGO"].ToString());
                }

                sSQL = $@"select top 1 (SUBSTRING(CODIGOPRD,0,11)) + RIGHT('00000' + CAST((SUBSTRING(CODIGOPRD,12,5)+1) as varchar) , 5) as 'NEWCODE' from TPRODUTO
                            where (SUBSTRING(CODIGOPRD,0,11)) = (SUBSTRING('{campoTexto1.Get()}',0,11))
                            order by (SUBSTRING(CODIGOPRD,12,5)) DESC";

                campoTexto1.Set(MetodosSQL.GetField(sSQL, "NEWCODE"));
            }
        }

        private void FormProdutoCadastro_Load(object sender, EventArgs e)
        {
            mnColarProduto.Enabled = false;
            IDPRD_COPY = 0;
            CODCOLPRD_COPY = 0;
            listaSituacaoMercadoria.Query = 4;

            cbCodigoDP.TextChanged += LiberaCamposComplementares;

            DataTable dtCodigoDP = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DISTINCT(CODDP) CODDP FROM ZTPRODUTOREGRA WHERE CODCOLIGADA = ?", new object[] { AppLib.Context.Empresa });

            for (int i = 0; i < dtCodigoDP.Rows.Count; i++)
            {
                cbCodigoDP.Properties.Items.Add(dtCodigoDP.Rows[i]["CODDP"].ToString());
            }

            cbCodigoDP.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            CarregaProduto();

            if (!String.IsNullOrWhiteSpace(campoTextoIDPRD.Get()))
            {
                CarregaComplemento();

                SaldoItem saldo = ItensOrcamento.getSaldo(int.Parse(campoTextoIDPRD.Get()));

                decimalSaldoFisico.Set(saldo.SALDO_FISICO);
                decimalSaldoPedido.Set(saldo.SALDO_PEDIDO);
                decimalSaldoDisponivel.Set(saldo.SALDO_DISPONIVEL);

                gridData2.Consulta = String.Format(@"SELECT CODCOLIGADA,
                                                               IDPRD,
                                                               CODETD, 
                                                               CODFISCAL, 
                                                               ALIQINTICMS, 
                                                               FATORREDUCAOICMS, 
                                                               TRIBUTADOICMSST, 
                                                               FATORSUBST, 
                                                               MODALIDADEBCICMS, 
                                                               MODALIDADEBCICMSST,
                                                               PERFECP,
                                                               RECCREATEDBY,
                                                               RECCREATEDON
                                                        from TPRDCODFISCAL
                                                        where IDPRD = {0}", campoTextoIDPRD.Get()).Split(' ');
                gridData2.Atualizar();
                atualizaComposicao();
            }
            else
            {
                listaSituacaoMercadoria.Set(null);
                listaProduto.Set(null);
            }

            if (listaSituacaoMercadoria.Get() == "01")
            {
                verificaDP = true;
            }
            else
            {
                verificaDP = true;
            }

            if (listaSituacaoMercadoria.Get() == "04")
            {
                listaProduto.Enabled = true;
            }
            else
            {
                listaProduto.Enabled = false;
            }

            if (gridData3.gridControl1.DataSource != null)
            {
                if (gridData3.gridView1.RowCount > 0)
                {
                    listaProduto.Enabled = false;
                }
            }

            chkCalculadoFormulado.Checked = IsCalculoAcabado();

            // CODCOLIGADA
            campoCodColPrd.Set(AppLib.Context.Empresa);
            campoTexto14.Set(AppLib.Context.Empresa.ToString());
            campoTexto6.Set(AppLib.Context.Empresa.ToString());
            campoTexto18.Set(AppLib.Context.Empresa.ToString());
            campoTexto16.Set(AppLib.Context.Empresa.ToString());
            campoInteiro1.Set(AppLib.Context.Empresa);

            isCaregandoComponentes = false;

            if (edita == false)
            {
                // Carrega informações para a Inserção do Produto

                // Nível
                campoLista5.SelectedIndex = 3;

                // Último Nível
                campoLista2.comboBox1.SelectedIndex = 0;
            }
            else
            {
                if (campoLista5.SelectedIndex == 3)
                {
                    campoLista2.comboBox1.SelectedIndex = 0;
                }

                if (cePrecoFixo.Checked)
                {
                    listaProduto.Enabled = false;
                }
            }

            // Tributos do Produto
            CarregaGridTributos();
            DesabilitaBotoes();

            ValidaTipoProduto();
        }

        private void ValidaTipoProduto()
        {
            // Revenda
            if (campoLookup2.Get() == "03")
            {
                tbPrecoAcabado.Enabled = false;
                cePrecoFixo.Enabled = false;
            }
            // Acabado
            else if (campoLookup2.Get() == "03")
            {
                tbPrecoRevenda.Enabled = false;
            }
            else
            {
                return;
            }
        }

        private bool FormProdutoCadastro_Validar(object sender, EventArgs e)
        {
            string sSQL = string.Empty;
            object CodigoPrd = string.Empty;

            if (string.IsNullOrEmpty(campoTexto1.Get()))
            {
                MessageBox.Show("Campo Código do Produto obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                #region valida tamanho

                // Último Nível - Sim
                if (campoLista2.comboBox1.SelectedIndex == 0)
                {
                    if (campoTexto1.Get().Length != 14)
                    {
                        MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    /*
                    if (campoLista5.SelectedValue.ToString() == "0")
                    {
                        if (campoTexto1.Get().Length == 0)
                        {
                            MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (campoTexto1.Get().Length == 1)
                        {
                            MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (campoTexto1.Get().Length == 3)
                        {
                            MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    else if (campoLista5.SelectedValue.ToString() == "1")
                    {
                        if (campoTexto1.Get().Length == 4)
                        {
                            MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    else if (campoLista5.SelectedValue.ToString() == "2")
                    {
                        if (campoTexto1.Get().Length == 5)
                        {
                            MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (campoTexto1.Get().Length == 7)
                        {
                            MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    else if (campoLista5.SelectedValue.ToString() == "3")
                    {
                        if (campoTexto1.Get().Length == 8)
                        {
                            MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (campoTexto1.Get().Length == 9)
                        {
                            MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (campoTexto1.Get().Length == 11)
                        {
                            MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (campoTexto1.Get().Length == 12)
                        {
                            MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (campoTexto1.Get().Length == 13)
                        {
                            MessageBox.Show("Código do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    */
                }


                #endregion

                if (campoLookup2.Get() == "02" || campoLookup2.Get() == "03")
                {
                    if (string.IsNullOrEmpty(campoTexto2.Get()))
                    {
                        MessageBox.Show("O Código Auxiliar do Produto deve ser informado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(campoTexto4.Get()))
                {
                    campoTexto4.Set(campoTexto1.textEdit1.Text);
                }

                /*
                if (campoTexto1.Get().Length == 15)
                {
                    sSQL = @"SELECT CODTB4FAT, DESCRICAO FROM TTB4 WHERE CODTB4FAT = ? AND CODCOLIGADA = ?";
                    CodigoPrd = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(null, sSQL, new Object[] { campoTexto1.Get().Substring(0, 9), AppLib.Context.Empresa });
                    if (CodigoPrd == null)
                    {
                        MessageBox.Show("Nível [XX.XXX.XXX] do produto não encontrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                if (campoTexto1.Get().Length == 10)
                {
                    sSQL = @"SELECT CODTB4FAT, DESCRICAO FROM TTB4 WHERE CODTB4FAT = ? AND CODCOLIGADA = ?";
                    CodigoPrd = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(null, sSQL, new Object[] { campoTexto1.Get().Substring(0, 6), AppLib.Context.Empresa });
                    if (CodigoPrd == null)
                    {
                        MessageBox.Show("Nível [XX.XXX] do produto não encontrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                if (campoTexto1.Get().Length == 6)
                {
                    sSQL = @"SELECT CODTB4FAT, DESCRICAO FROM TTB4 WHERE CODTB4FAT = ? AND CODCOLIGADA = ?";
                    CodigoPrd = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(null, sSQL, new Object[] { campoTexto1.Get().Substring(0, 2), AppLib.Context.Empresa });
                    if (CodigoPrd == null)
                    {
                        MessageBox.Show("Nível [XX] do produto não encontrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                */

                if (campoTextoIDPRD.Get().Equals("0"))
                {
                    sSQL = @"SELECT CODIGOPRD FROM TPRD WHERE CODIGOPRD = ? AND CODCOLIGADA = ?";
                    CodigoPrd = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(null, sSQL, new Object[] { campoTexto1.Get(), AppLib.Context.Empresa });
                    if (CodigoPrd != null)
                    {
                        MessageBox.Show("Código do Produto já existente na base de dados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    sSQL = @"SELECT CODIGOPRD FROM TPRD WHERE CODIGOPRD = ? AND IDPRD <> ? AND CODCOLIGADA = ?";
                    CodigoPrd = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(null, sSQL, new Object[] { campoTexto1.Get(), campoTextoIDPRD.Get(), AppLib.Context.Empresa });
                    if (CodigoPrd != null)
                    {
                        MessageBox.Show("Código do Produto já existente na base de dados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                if (campoLista4.comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Favor informar a procedência do produto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrEmpty(campoLookup12.Get()) || string.IsNullOrEmpty(campoLookup13.Get()) || string.IsNullOrEmpty(campoLookup14.Get()))
                {
                    MessageBox.Show("Favor informar as unidades de medida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(campoTexto3.Get()))
            {
                MessageBox.Show("Favor informar o Nome Fantasia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(campoMemo1.Get()) && edita == false)
                {
                    campoMemo1.Set(campoTexto3.Get());
                }
            }

            string sSql = @"SELECT * FROM ZPARAMFATURE WHERE CODCOLIGADA = ?";

            DataTable param = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, campoTexto14.Get());

            if (Convert.ToInt32(param.Rows[0]["USAPRECOCALCULADO"]) == 1)
            {
                // Validação do preço acabado
                if (campoLookup2.textBoxCODIGO.Text == "02" && (Convert.ToInt32(cbCodigoDP.SelectedItem.ToString().Substring(0, 3)) < 800 || Convert.ToInt32(cbCodigoDP.SelectedItem.ToString().Substring(0, 3)) > 805))
                {
                    // Validação da Tabela de Preço
                    if (!cePrecoFixo.Checked)
                    {
                        //if (string.IsNullOrEmpty(listaProduto.Get()))
                        //{
                        //    MessageBox.Show("Favor informar a Tabela de Preço.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return false;
                        //}

                        //if (!ValidarComplementares())
                        //{
                        //    return false;
                        //}

                        //CalculaPreco();
                    }
                    else
                    {
                        //if (Convert.ToDecimal(tbPrecoAcabado.EditValue) == 0)
                        //{
                        //    MessageBox.Show("Favor informar o preço fixo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return false;
                        //}
                    }

                    //if (isPrecoCalculadoValidado == false)
                    //{
                    //    return false;
                    //}
                }
            }

            if (campoLookup2.textBoxCODIGO.Text == "03" && chkCalculadoFormulado.Checked == false)
            {
                // Comentado em 19/08/2020 conforme demanda.

                // Validação do preço de Revenda
                //if (campoDecimal4.Get() == 0)
                //{
                //    MessageBox.Show("Favor informar o preço de revenda.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}
            }

            if (chkCalculadoFormulado.Checked == true)
            {
                // Validação do preço de Revenda
                if (string.IsNullOrEmpty(listaProduto.comboBox1.Text) || listaProduto.comboBox1.Text == " - ")
                {
                    MessageBox.Show("Favor informar a Tabela de Preço.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void FormProdutoCadastro_AntesSalvar(object sender, EventArgs e)
        {
            // Procedência
            if (campoLista4.Get() == null)
            {
                Procedencia = "0";
                campoLista4.comboBox1.SelectedIndex = 0;
            }
            else
            {
                Procedencia = campoLista4.comboBox1.SelectedValue.ToString();
            }

            if (campoTextoIDPRD.Get().Equals("0"))
            {
                string sSQL = string.Empty;
                sSQL = @"SELECT VALAUTOINC + 1 VALOR FROM GAUTOINC WHERE CODSISTEMA = 'T' AND CODAUTOINC = 'IDPRD'";

                sSQL = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSQL, new Object[] { });
                string IdPrd = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(null, sSQL, new Object[] { }).ToString();

                sSQL = @"UPDATE GAUTOINC SET VALAUTOINC = ?  WHERE CODSISTEMA = 'T' AND CODAUTOINC = 'IDPRD'";
                AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction(sSQL, new Object[] { IdPrd }).ToString();

                //IDPRD
                campoTextoIDPRD.Set(IdPrd);
                campoTexto5.Set(IdPrd);
                campoTexto15.Set(IdPrd);
                campoTexto19.Set(IdPrd);
                campoTexto17.Set(IdPrd);

                //CODCOLIGADA
                campoTexto14.Set(AppLib.Context.Empresa.ToString());
                campoTexto6.Set(AppLib.Context.Empresa.ToString());
                campoTexto18.Set(AppLib.Context.Empresa.ToString());
                campoTexto16.Set(AppLib.Context.Empresa.ToString());
                campoInteiro1.Set(AppLib.Context.Empresa);

                //OUTROS
                DateTime Atual = DateTime.Now;

                campoTextoUSUARIOCRIACAO.Set(AppLib.Context.Usuario);
                campoDataHora1.Set(Atual);
                campoTextoCODUSUARIO.Set(AppLib.Context.Usuario);
                campoDataHora2.Set(Atual);

                campoDataHora3.Set(Atual);
                campoDataHora4.Set(Atual);
                campoDataHora5.Set(Atual);
                campoDataHora6.Set(Atual);

                campoDataHora7.Set(Atual);
                campoDataHora8.Set(Atual);
                campoDataHora9.Set(Atual);
                campoDataHora10.Set(Atual);

                sSQL = @"SELECT VALAUTOINC + 1 VALOR FROM GAUTOINC WHERE CODSISTEMA = 'T' AND CODAUTOINC = 'CODIGOREDUZIDO' AND CODCOLIGADA = ?";
                sSQL = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSQL, new Object[] { AppLib.Context.Empresa });
                string CodigoReduzido = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(null, sSQL, new Object[] { }).ToString();

                sSQL = @"UPDATE GAUTOINC SET VALAUTOINC = ?  WHERE CODSISTEMA = 'T' AND CODAUTOINC = 'CODIGOREDUZIDO' AND CODCOLIGADA = ?";
                AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction(sSQL, new Object[] { CodigoReduzido, AppLib.Context.Empresa }).ToString();

                campoTexto20.Set(CodigoReduzido);
            }
            else
            {
                campoTextoCODUSUARIO.Set(AppLib.Context.Usuario);
                campoDataHora1.Set(DateTime.Now);
                campoDataHora2.Set(DateTime.Now);

                campoDataHora3.Set(DateTime.Now);
                campoDataHora4.Set(DateTime.Now);
                campoDataHora5.Set(DateTime.Now);
                campoDataHora6.Set(DateTime.Now);
            }

            //Aplica % na nos preços
            if (tbPrecoRevenda.EditValue != null)
            {
                if (!string.IsNullOrEmpty(tbPrecoRevenda.EditValue.ToString()))
                {
                    if (Convert.ToDecimal(tbPrecoRevenda.EditValue) > 0)
                    {
                        decimal valorbase = Convert.ToDecimal(tbPrecoRevenda.EditValue);

                        campoDecimal5.Set(valorbase + (valorbase * 10 / 100));
                        campoDecimal6.Set(valorbase + (valorbase * 15 / 100));
                        campoDecimal7.Set(valorbase + (valorbase * 20 / 100));
                        campoDecimal8.Set(valorbase + (valorbase * 25 / 100));
                    }
                    else
                    {
                        campoDecimal5.Set(0);
                        campoDecimal6.Set(0);
                        campoDecimal7.Set(0);
                        campoDecimal8.Set(0);
                    }
                }
            }

            if (cbCodigoDP.EditValue != null)
            {
                if (!string.IsNullOrEmpty(cbCodigoDP.SelectedItem.ToString()))
                {
                    if (campoLookup2.textBoxCODIGO.EditValue.ToString() == "02" && (Convert.ToInt32(cbCodigoDP.SelectedItem.ToString().Substring(0, 3)) < 800 || Convert.ToInt32(cbCodigoDP.SelectedItem.ToString().Substring(0, 3)) > 805))
                    {
                        if (!cePrecoFixo.Checked)
                        {
                            CalculaPreco();
                        }
                    }
                }
            }

            if (campoLookup2.textBoxCODIGO.EditValue.ToString() == "03" && chkCalculadoFormulado.Checked == true)
            {
                CalculaPreco();
            }

            //FormataFormula(txtFormula.textEdit1.Text, campoTextoIDPRD.Get(), listaTipoInox.Get());
        }

        private void campoLista5_SelectedValueChanged(object sender, EventArgs e)
        {
            if (campoLista5.SelectedValue.ToString() == "0")
                campoTexto1.textEdit1.Properties.Mask.EditMask = "00";

            if (campoLista5.SelectedValue.ToString() == "1")
                campoTexto1.textEdit1.Properties.Mask.EditMask = "00.00";

            if (campoLista5.SelectedValue.ToString() == "2")
                campoTexto1.textEdit1.Properties.Mask.EditMask = "00.00.000";

            if (campoLista5.SelectedValue.ToString() == "3")
                campoTexto1.textEdit1.Properties.Mask.EditMask = "00.00.000.00000";

            if (campoLista5.SelectedIndex != 3)
            {
                if (campoLista2.comboBox1.Items.Count > 0)
                {
                    campoLista2.comboBox1.SelectedIndex = 1;
                }
            }
        }

        private void pictureBoxWIZARDCODIGO_Click(object sender, EventArgs e)
        {
            try
            {
                AppLib.Windows.CampoLookup cl = new AppLib.Windows.CampoLookup();
                cl.ColunaCodigo = "CODTB4FAT";
                cl.ColunaDescricao = "DESCRICAO";

                AppLib.Windows.FormVisao f = new AppLib.Windows.FormVisao();
                f.grid1.NomeGrid = "WIZARDCODIGO";
                f.grid1.Consulta = new String[] { "SELECT CODTB4FAT, DESCRICAO FROM TTB4 WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " and LEN(CODTB4FAT) = 9" };
                f.MostrarLookup(cl);

                String consulta1 = @"
SELECT TOP 1 CODIGOPRD FROM (

SELECT SUBSTRING(CODIGOPRD, 11, 5) CODIGOPRD
FROM TPRD
WHERE CODCOLIGADA = ?
  AND CODIGOPRD LIKE ? + '%'

) X
ORDER BY CODIGOPRD DESC";

                System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta1, new Object[] { AppLib.Context.Empresa, cl.textBoxCODIGO.Text });

                if (dt.Rows.Count > 0)
                {
                    int ultimo = int.Parse(dt.Rows[0][0].ToString());
                    int proximo = ultimo + 1;
                    int zeros = (5 - proximo.ToString().Length);

                    String result = cl.textBoxCODIGO.Text + ".";

                    for (int i = 0; i < zeros; i++)
                    {
                        result += "0";
                    }

                    result += proximo;

                    campoTexto1.Set(result);
                }
                else
                {
                    campoTexto1.Set(cl.textBoxCODIGO.Text + ".0001");
                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao buscar próximo código de produto: " + ex.Message);
            }
        }

        private void FormProdutoCadastro_AposSalvar(object sender, EventArgs e)
        {
            SalvaComplementares();
            CarregaComplemento();

            CarregaValores();
            string sSql = string.Empty;
            sSql = "SELECT CODIGOPRD FROM TPRODUTO WHERE CODCOLPRD = ? AND IDPRD = ? AND RECCREATEDON = RECMODIFIEDON";

            if (AppLib.Context.poolConnection.Get(this.Conexao).ExecHasRows(sSql, campoTexto14.Get(), campoTextoIDPRD.Get()))
            {
                sSql = @"SELECT * FROM ZPARAMFATURE WHERE CODCOLIGADA = ?";

                DataTable param = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, campoTexto14.Get());

                EnderecoEmail = (param.Rows[0]["ENDERECOEMAIL"] == DBNull.Value) ? null : param.Rows[0]["ENDERECOEMAIL"].ToString();
                PortaEmail = (param.Rows[0]["PORTAEMAIL"] == DBNull.Value) ? 80 : Convert.ToInt32(param.Rows[0]["PORTAEMAIL"]);
                UsuarioEmail = (param.Rows[0]["USUARIOEMAIL"] == DBNull.Value) ? null : param.Rows[0]["USUARIOEMAIL"].ToString();
                SenhaEmail = (param.Rows[0]["SENHAEMAIL"] == DBNull.Value) ? null : param.Rows[0]["SENHAEMAIL"].ToString();
                EnviarComo = (param.Rows[0]["ENVIARCOMO"] == DBNull.Value) ? null : param.Rows[0]["ENVIARCOMO"].ToString();
                EnviarComoDisplay = (param.Rows[0]["ENVIARCOMODISPLAY"] == DBNull.Value) ? null : param.Rows[0]["ENVIARCOMODISPLAY"].ToString();
                Enderecos = (param.Rows[0]["ENVIARPARA"] == DBNull.Value) ? null : param.Rows[0]["ENVIARPARA"].ToString();
                SSL = (Convert.ToInt32(param.Rows[0]["SSLEMAIL"]) == 0) ? false : true;

                string sMsg = string.Empty;

                string CodigoPrd = campoTexto1.Get();
                string NomeFantasia = campoTexto3.Get();
                DateTime DataAlteracao = (DateTime)campoDataHora1.Get();
                string UsuarioAlteracao = campoTextoUSUARIOCRIACAO.Get();

                sMsg = "ATENÇÃO!!! \r\n\n";
                sMsg = sMsg + "O produto: " + CodigoPrd + " - " + NomeFantasia + " foi cadastrado no sistema em " + DataAlteracao + " pelo usuário " + UsuarioAlteracao + ".";

                sMsg = sMsg + "\r\n\n";
                sMsg = sMsg + "Mensagem enviada automaticamente.\r\n";
                sMsg = sMsg + "Não responda esta mensagem.";

                Regex rSplit = new Regex(";");
                String[] sResult = rSplit.Split(Enderecos);

                sSql = String.Format(@"select * from ZENVIAEMAIL where EVENTO = 'CADASTRO DE PRODUTO' and CODCOLIGADA = '{0}'", AppLib.Context.Empresa);

                DataTable email = MetodosSQL.GetDT(sSql);


                foreach (DataRow mail in email.Rows)
                {
                    EnviarPara = mail["EMAIL"].ToString();
                }
            }

            if (cbCodigoDP.EditValue != null)
            {
                if (cbCodigoDP.SelectedItem.ToString().Length >= 3)
                {
                    if (cbCodigoDP.SelectedItem.ToString().Substring(0, 3) == "800" || (cbCodigoDP.SelectedItem.ToString().Substring(0, 3) == "801" || (cbCodigoDP.SelectedItem.ToString().Substring(0, 3) == "802" || (cbCodigoDP.SelectedItem.ToString().Substring(0, 3) == "803" || (cbCodigoDP.SelectedItem.ToString().Substring(0, 3) == "804" || (cbCodigoDP.SelectedItem.ToString().Substring(0, 3) == "805"))))))
                    {
                        decimal total = 0;

                        for (int i = 0; i < gridData3.gridView1.RowCount; i++)
                        {
                            DataRow row = gridData3.gridView1.GetDataRow(i);

                            decimal Quantidade = Convert.ToDecimal(row["QTDUSADA"]);
                            decimal Preco = Convert.ToDecimal(row["PRECO"]);

                            total += Quantidade * Preco;
                        }

                        tbPrecoAcabado.EditValue = total;
                    }
                }
            }

            edita = true;

            if (listaSituacaoMercadoria.comboBox1.SelectedValue != null)
            {
                string situacao = listaSituacaoMercadoria.comboBox1.SelectedValue.ToString();

                try
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE TPRDFISCAL SET SITUACAOMERCADORIA = ? WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { situacao, AppLib.Context.Empresa, campoTextoIDPRD.Get() });
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }

            if (campoLookup2.Get() == "03")
            {
                try
                {
                    MetodosSQL.ExecQuery(@"UPDATE TPRODUTODEF SET PRECO1 = '" + Convert.ToDecimal(tbPrecoRevenda.EditValue).ToString().Replace(',', '.') + "' WHERE IDPRD = " + Convert.ToInt32(campoTextoIDPRD.textEdit1.EditValue) + " AND CODCOLIGADA = " + AppLib.Context.Empresa + "");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }
        }

        private void SendMessage(string Mensagem)
        {
            try
            {
                AppLib.Util.Email email = new AppLib.Util.Email();
                email.Host = EnderecoEmail;
                email.Porta = PortaEmail;
                email.Usuario = UsuarioEmail;
                email.Senha = SenhaEmail;
                email.UsaSSL = SSL;
                email.De = EnviarComo;
                email.DeDisplay = EnviarComoDisplay;
                email.Para = EnviarPara;
                email.Assunto = "Aviso de Cadastro de Produto no Sistema";
                email.Mensagem = Mensagem;
                email.Html = false;

                bool enviou = email.Enviar();

                if (enviou)
                {

                }
                else
                {
                    throw new Exception("Erro ao Enviar E-mail de Alerta.");
                }
            }
            catch (Exception)
            {

            }
        }

        private void campoLookup1_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM TFAB WHERE CODCOLIGADA = ? AND CODFAB = ?";
            campoLookup1.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup1.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookup1.Get() }).ToString();
        }

        private void campoLookup2_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT DESCRICAO FROM TTB2 WHERE CODCOLIGADA = ? AND CODTB2FAT = ?";
            campoLookup2.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup2.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookup2.Get() }).ToString();

            if ((campoLookup2.Get() != "02") && (campoLookup2.Get() != "03"))
            {
                tabControl1.TabPages.Remove(tabPage13);
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage13);
                tabControl1.TabPages.Add(tabPage13);
            }

            if (campoLookup2.Get() == "03")
            {
                tbPrecoRevenda.Enabled = true;
                listaSituacaoMercadoria.Set("01");
            }
            else
            {
                tbPrecoRevenda.Enabled = false;
            }

            if (campoLookup2.Get() == "02")
            {
                listaSituacaoMercadoria.Set("04");

                listaProduto.Enabled = true;
                cePrecoFixo.Enabled = true;
            }
            else
            {
                listaProduto.Enabled = false;
                cePrecoFixo.Enabled = false;
            }

        }

        private void regraEnableDisable()
        {
            string sql = @"SELECT DESCRICAO FROM TTB2 WHERE CODCOLIGADA = ? AND CODTB2FAT = ?";
            campoLookup2.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup2.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookup2.Get() }).ToString();

            if ((campoLookup2.Get() != "02") && (campoLookup2.Get() != "03"))
            {
                tabControl1.TabPages.Remove(tabPage13);

                AtualizaTabelasByTipoProduto();

                DeleteEspecificacoes();
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage13);
                tabControl1.TabPages.Add(tabPage13);
            }

            //chkCalculadoFormulado.Checked = IsCalculoAcabado();

            if (campoLookup2.Get() == "03" && chkCalculadoFormulado.Checked == true)
            {
                // Tabela de preço - habilita
                listaProduto.Enabled = true;

                // Desabilita campo preço Revenda
                tbPrecoRevenda.Enabled = false;

                tbPrecoRevenda.EditValue = 0;

                // Traz a formula
                string sqlFormula = String.Format(@"select FORMULAPRECO from ZTPRODUTOREGRA where CODDP = '{0}'", cbCodigoDP.SelectedItem.ToString());

                txtFormula.textEdit1.Text = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField("", sqlFormula, new Object[] { }).ToString();

                txtFormula.Enabled = true;

                HabilitaCamposComplementares();

                // Zera o campo de Preço acabado
                tbPrecoAcabado.Enabled = true;

                //CalculaPreco();
            }
            else if (campoLookup2.Get() == "03")
            {
                // Preço revenda
                tbPrecoRevenda.Enabled = true;

                // Zera o campo de Preço acabado
                tbPrecoAcabado.EditValue = 0;

                // Zera o campo de Preço acabado
                tbPrecoAcabado.Enabled = false;

                // Tabela de Preço - desabilita
                listaProduto.Enabled = false;

                // Situação de Mercadoria
                listaSituacaoMercadoria.Set("01");

                // Fórmula
                txtFormula.textEdit1.ResetText();

                // Tabela de preço
                listaProduto.Enabled = false;
                listaProduto.comboBox1.SelectedValue = 0;
                listaProduto.Lista.GetValue(0);

                // Desabilita o Preço Fixo
                cePrecoFixo.Enabled = false;
                cePrecoFixo.Checked = false;
            }
            else if (campoLookup2.Get() == "02")
            {
                // Tabela de preço - habilita
                listaProduto.Enabled = true;

                // Checkbox Preço fixo - habilita
                cePrecoFixo.Enabled = true;

                // Situação de Mercadoria
                listaSituacaoMercadoria.Set("04");

                // Desabilita campo preço Revenda
                tbPrecoRevenda.Enabled = false;

                tbPrecoRevenda.EditValue = 0;

                if (cbCodigoDP.SelectedItem != null)
                {
                    // Traz a formula
                    string sqlFormula = String.Format(@"select FORMULAPRECO from ZTPRODUTOREGRA where CODDP = '{0}'", cbCodigoDP.SelectedItem.ToString());

                    txtFormula.textEdit1.Text = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField("", sqlFormula, new Object[] { }).ToString();

                    txtFormula.Enabled = true;
                }  
            }
            else
            {
                listaProduto.Enabled = false;
                cePrecoFixo.Enabled = false;
            }
        }

        private void campoLookup3_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT DESCRICAO FROM TTB3 WHERE CODCOLIGADA = ? AND CODTB3FAT = ?";
            campoLookup3.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup3.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookup3.Get() }).ToString();
        }

        private void campoLookup4_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT DESCRICAO FROM TTB4 WHERE CODCOLIGADA = ? AND CODTB4FAT = ?";
            campoLookup4.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup4.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookup4.Get() }).ToString();
        }

        private void campoLookup5_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT DESCRICAO FROM TTB5 WHERE CODCOLIGADA = ? AND CODTB5FAT = ?";
            campoLookup5.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup5.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookup5.Get() }).ToString();
        }

        private void campoLookup6_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT DESCRICAO FROM TTBORCAMENTO WHERE CODCOLIGADA = ? AND CODTBORCAMENTO = ?";
            campoLookup6.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup6.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookup6.Get() }).ToString();
        }

        private void campoLookup15_SetDescricao_1(object sender, EventArgs e)
        {
            string sql = @"SELECT DESCRICAO FROM GCONSIST WHERE CODCOLIGADA = ? AND CODCLIENTE = ?";
            campoLookup15.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup15.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookup15.Get() }).ToString();

        }


        private void FormProdutoCadastro_AntesEditar(object sender, EventArgs e)
        {
            //campoLista4.comboBox1.SelectedIndex = -1;
        }


        #region EventoSalvarComplementos

        private Boolean ValidarComplementares()
        {
            try
            {
                if (tbLargura.Enabled && String.IsNullOrWhiteSpace(tbLargura.EditValue.ToString())) { throw new Exception("Insira a Largura"); }
                if (tbAltura.Enabled && String.IsNullOrWhiteSpace(tbAltura.EditValue.ToString())) { throw new Exception("Insira a Altura"); }
                if (tbComprimento.Enabled && String.IsNullOrWhiteSpace(tbComprimento.EditValue.ToString())) { throw new Exception("Insira o Comprimento"); }
                //if (medidaD.Enabled && String.IsNullOrWhiteSpace(medidaD.Get())) { throw new Exception("Inisira a Distanciamento"); }
                if (listaChapa.Enabled && String.IsNullOrWhiteSpace(listaChapa.Get())) { throw new Exception("Defina uma Chapa"); }
                if (listaAcabamento.Enabled && String.IsNullOrWhiteSpace(listaAcabamento.Get())) { throw new Exception("Defina um Acabamento"); }
                if (listaVirola.Enabled && String.IsNullOrWhiteSpace(listaVirola.Get())) { throw new Exception("Defina uma Virola"); }
                if (raio.Enabled && String.IsNullOrWhiteSpace(raio.Get())) { throw new Exception("Inisira o Raio"); }
                if (listaRaio.Enabled && String.IsNullOrWhiteSpace(listaRaio.Get())) { throw new Exception("Defina o Tipo do Raio"); }
                if (listaSepto.Enabled && String.IsNullOrWhiteSpace(listaSepto.Get())) { throw new Exception("Defina o Septo"); }
                if (listaComplemento.Enabled && String.IsNullOrWhiteSpace(listaComplemento.Get())) { throw new Exception("Defina o Complemento"); }
                //if (peso.Enabled && String.IsNullOrWhiteSpace(peso.Get().ToString())) { throw new Exception("Insira o peso"); }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void SalvaComplementares()
        {
            string precoAcabado = "";

            if (!cePrecoFixo.Checked)
            {
                if (tbPrecoAcabado.EditValue != null)
                {
                    precoAcabado = tbPrecoAcabado.Text.ToString().Replace(",", ".");
                }
                else
                {
                    precoAcabado = "0";
                }
            }
            else
            {
                precoAcabado = "0";
            }

            try
            {
                string SQL = String.Empty;
                SQL = String.Format(@"select COUNT(1) as 'TOTAL' from ZTPRODUTOCOMPL where CODCOLIGADA = 1 and CODFILIAL = 1 and IDPRD = '{0}'", campoTextoIDPRD.Get());
                int totalRegras = int.Parse(MetodosSQL.GetField(SQL, "TOTAL"));

                if (verificaDP)
                {
                    if (edita && totalRegras > 0)
                    {
                        SQL = String.Format(@"update ZTPRODUTOCOMPL 
                                                set CODDP = '{1}', 
		                                                LARGURA = {2},
		                                                ALTURA = {3},
		                                                COMPRIMENTO = {4},
		                                                DISTANCIAMENTO = {5},
		                                                CHAPA = '{6}',
														ACABAMENTO = '{7}',
		                                                VIROLA = '{8}',
		                                                RAIO = {9},
														TIPORAIO = '{10}',
														SEPTO = '{11}',
		                                                COMPLEMENTO = '{12}',
		                                                PESO = {13},
                                                        TABELAPRECO = '{14}',
                                                        USAPRECOFIXO = {15},
                                                        PRECOFIXO = {16}, 
                                                        CALCCOMOACABADO = {17},
                                                        PRECOCALCULADO = {18}
														where IDPRD = {0}",
                                                             /*IDPRD*/ campoTextoIDPRD.Get(),
                                                                /*CODDP*/ cbCodigoDP.EditValue != null ? cbCodigoDP.SelectedItem.ToString() : "",
                                                                /*LARGURA*/ tbLargura.Enabled && !string.IsNullOrEmpty(tbLargura.Text) ? tbLargura.Text.Replace(",", ".") : "0",
                                                                /*ALTURA*/ tbAltura.Enabled && !string.IsNullOrEmpty(tbAltura.Text) ? tbAltura.Text.Replace(",", ".") : "0",
                                                                /*COMPRIMENTO*/ tbComprimento.Enabled && !string.IsNullOrEmpty(tbComprimento.Text) ? tbComprimento.Text.Replace(",", ".") : "0",
                                                                /*DISTANCIAMENTO*/ !string.IsNullOrEmpty(medidaD.Get()) ? medidaD.Get().ToString() : "0",
                                                                ///*DISTANCIAMENTO*/ medidaD.comboBox1.Text == " - Selecione" ? "0" : medidaD.Get(),
                                                                /*CHAPA*/ listaChapa.Enabled ? listaChapa.Get() : "",
                                                                /*ACABAMENTO*/ listaAcabamento.Enabled ? listaAcabamento.Get() : "",
                                                                /*VIROLA*/ listaVirola.Enabled ? listaVirola.Get() : "",
                                                                /*RAIO*/ raio.Enabled ? raio.Get() : "0",
                                                                /*TIPORAIOP*/ listaRaio.Enabled ? listaRaio.Get() : "",
                                                                /*SEPTO*/ listaSepto.Enabled ? listaSepto.Get() : "",
                                                                /*COMPLEMENTO*/ listaComplemento.Enabled ? listaComplemento.Get() : "",
                                                                /*PESO*/ 0 /*peso.Enabled ? peso.Get().ToString().Replace(",", ".") : "0"*/,
                                                                /*TABELAPRECO*/ listaProduto.Get(),
                                                                /*USAPRECOFIXO*/ cePrecoFixo.Checked ? 1 : 0,
                                                                /*PRECOFIXO*/ cePrecoFixo.Checked ? tbPrecoAcabado.Text.Replace(",", ".") : "0",
                                                                /* CALCULADO COMO ACABADO*/ chkCalculadoFormulado.Checked ? 1 : 0,
                                                                /* PREÇO CALCULADO*/ precoAcabado);
                    }
                    else
                    {
                        SQL = String.Format(@"insert into ZTPRODUTOCOMPL 
                                                values (/*CODCOLIGADA*/1, 
		                                                /*CODFILIAL*/1, 
		                                                /*IDPRD*/ {0}, 
		                                                /*CODDP*/ '{1}', 
		                                                /*LARGURA*/ {2},
		                                                /*ALTURA*/ {3},
		                                                /*COMPRIMENTO*/ {4},
		                                                /*DISTANCIAMENTO*/ {5},
		                                                /*CHAPA*/ '{6}',
														/*ACABAMENTO*/ '{7}',
		                                                /*VIROLA*/ '{8}',
		                                                /*RAIO*/ {9},
														/*TIPORAIO*/ '{10}',
														/*SEPTO*/ '{11}',
		                                                /*COMPLEMENTO*/ '{12}',
		                                                /*PESO*/ {13},
                                                        /*TABELAPRECO*/ '{14}',
                                                        /*USAPRECOFIXO*/ {15},
                                                        /*PRECOFIXO*/ {16},
                                                        /*CALCCOMOACABADO*/ {17},
                                                        /*PRECOCALCULADO*/ {18})",
                                                                /*IDPRD*/ campoTextoIDPRD.Get(),
                                                                /*CODDP*/ cbCodigoDP.EditValue != null ? cbCodigoDP.SelectedItem.ToString() : "",
                                                                /*LARGURA*/ tbLargura.Enabled && !string.IsNullOrEmpty(tbLargura.Text) ? tbLargura.Text.Replace(",", ".") : "0",
                                                                /*ALTURA*/ tbAltura.Enabled && !string.IsNullOrEmpty(tbAltura.Text) ? tbAltura.Text.Replace(",", ".") : "0",
                                                                /*COMPRIMENTO*/ tbComprimento.Enabled && !string.IsNullOrEmpty(tbComprimento.Text) ? tbComprimento.Text.Replace(",", ".") : "0",
                                                                 /*DISTANCIAMENTO*/ !string.IsNullOrEmpty(medidaD.Get()) ? medidaD.Get().ToString() : "0",
                                                                /*CHAPA*/ listaChapa.Enabled ? listaChapa.Get() : "",
                                                                /*ACABAMENTO*/ listaAcabamento.Enabled ? listaAcabamento.Get() : "",
                                                                /*VIROLA*/ listaVirola.Enabled ? listaVirola.Get() : "",
                                                                /*RAIO*/ raio.Enabled ? raio.Get() : "0",
                                                                /*TIPORAIOP*/ listaRaio.Enabled ? listaRaio.Get() : "",
                                                                /*SEPTO*/ listaSepto.Enabled ? listaSepto.Get() : "",
                                                                /*COMPLEMENTO*/ listaComplemento.Enabled ? listaComplemento.Get() : "",
                                                                /*PESO*/ 0 /*peso.Enabled ? peso.Get().ToString().Replace(",", ".") : "0"*/,
                                                                /*TABELAPRECO*/ listaProduto.Get(),
                                                                /*USAPRECOFIXO*/ cePrecoFixo.Checked ? 1 : 0,
                                                                /*PRECOFIXO*/  cePrecoFixo.Checked ? tbPrecoAcabado.Text.Replace(",", ".") : "0",
                                                                /* CALCULADO COMO ACABADO*/ chkCalculadoFormulado.Checked ? 1 : 0,
                                                                /* PREÇO CALCULADO*/ precoAcabado);

                    }
                }
                else
                {
                    if (edita && totalRegras > 0)
                    {
                        SQL = String.Format(@"update ZTPRODUTOCOMPL
                                                set CODDP = '{0}', 
		                                                LARGURA = null,
		                                                ALTURA = null,
		                                                COMPRIMENTO = null,
		                                                DISTANCIAMENTO = null,
		                                                CHAPA = null,
														ACABAMENTO = null,
		                                                VIROLA = null,
		                                                RAIO = null,
														TIPORAIO = null,
														SEPTO = null,
		                                                COMPLEMENTO = null,
		                                                PESO = null,
                                                        TABELAPRECO = null,
                                                        PRECOFIXO = null
                                                where CODCOLIGADA = 1
                                                and CODFILIAL = 1
                                                and IDPRD = {1}",
                                                /*CODDP*/ cbCodigoDP.SelectedItem.ToString(),
                                                /*IDPRD*/ campoTextoIDPRD.Get());
                    }
                    else
                    {
                        SQL = String.Format(@"insert into ZTPRODUTOCOMPL (CODCOLIGADA, CODFILIAL, IDPRD, CODDP) values (1, 1, /*IDPRD*/ {0}, /*CODDP*/ '{1}')",
                                                                /*IDPRD*/ campoTextoIDPRD.Get(),
                                                                /*CODDP*/ cbCodigoDP.SelectedItem.ToString());
                    }
                }

                if (string.IsNullOrEmpty(medidaD.Get()))
                {
                    SQL = SQL.Replace("/*DISTANCIAMENTO*/ ,", "'',");
                }

                MetodosSQL.ExecQuery(SQL);

                SQL = String.Format(@"update tproduto set referenciacp = {0} where idprd = {1}", Procedencia, campoTextoIDPRD.Get());

                MetodosSQL.ExecQuery(SQL);

                //txtFormula.Set(MetodosSQL.GetField(String.Format(@"select * from ZTPRODUTOREGRA where CODCOLIGADA = 1 and CODFILIAL = 1 and CODDP = {0}", campoDP.Get()), "FORMULAPRECO"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            SalvaComplementares();
        }

        private void listaSituacaoMercadoria_AposSelecao(object sender, EventArgs e)
        {
            if (listaSituacaoMercadoria.Get() == "01")
            {
                verificaDP = true;
            }
            else
            {
                verificaDP = true;
            }

            if (listaSituacaoMercadoria.Get() == "04")
            {
                listaProduto.Enabled = true;
            }
            else
            {
                listaProduto.Enabled = false;
            }
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void listaAcabamento_AposSelecao(object sender, EventArgs e)
        {
            if (listaAcabamento.Get() == "INOX")
            {
                listaTipoInox.Enabled = true;
            }
            else
            {
                listaTipoInox.Enabled = false;
            }
        }

        private void listaTipoInox_AposSelecao(object sender, EventArgs e)
        {
            if (listaTipoInox.Get() != null && listaTipoInox.comboBox1.Text != "Selecione")
            {
                CalculaPreco();
            }
        }

        private void gridData2_Novo(object sender, EventArgs e)
        {
            FormProdutoTributoCadastro frm = new FormProdutoTributoCadastro(campoTextoIDPRD.Get(), null);
            frm.ShowDialog();

            gridData2.Atualizar();
        }

        private void gridData2_Editar(object sender, EventArgs e)
        {
            try
            {
                DataRow row = gridData2.GetDataRow();

                FormProdutoTributoCadastro frm = new FormProdutoTributoCadastro(campoTextoIDPRD.Get(), (string)row["CODETD"]);
                frm.ShowDialog();

                gridData2.Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void gridData2_Excluir(object sender, EventArgs e)
        {
            try
            {
                DataRow row = gridData2.GetDataRow();

                string sql = String.Format(@"delete from TPRDCODFISCAL where IDPRD = {0} and CODETD = '{1}'", campoTextoIDPRD.Get(), (string)row["CODETD"]);

                MetodosSQL.ExecQuery(sql);

                gridData2.Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void atualizaComposicao()
        {
            try
            {
                string sql = String.Format(@"SELECT 
	                                            TPRODUTO.CODIGOPRD,
                                                TPRODUTO.IDPRD,
	                                            TPRODUTO.CODIGOAUXILIAR,
	                                            TPRODUTO.NOMEFANTASIA,
	                                            ZTPRODUTOCOMPLEMENTO.QUANTIDADE as 'QTDUSADA',        
                                                ZTPRODUTOCOMPLEMENTO.PRECO,       
                                                TPRODUTO.INATIVO
                                            FROM 
	                                            TPRODUTO
										
                                                inner join ZTPRODUTOCOMPLEMENTO
                                                on ZTPRODUTOCOMPLEMENTO.IDPRD = TPRODUTO.IDPRD

                                            where ZTPRODUTOCOMPLEMENTO.IDPRDPRINCIPAL = {0}", campoTextoIDPRD.Get());

                //    string sql = String.Format(@"SELECT 
                //                                 TPRODUTO.CODIGOPRD,
                //                                    TPRODUTO.IDPRD,
                //                                 TPRODUTO.CODIGOAUXILIAR,
                //                                 TPRODUTO.NOMEFANTASIA,
                //                                 ZTPRODUTOCOMPLEMENTO.QUANTIDADE as 'QTDUSADA',
                //ZTPRODUTOCOMPL.CODDP,
                //ZTPRODUTOCOMPL.TABELAPRECO,
                //ZTPRODUTOCOMPL.LARGURA,
                //ZTPRODUTOCOMPL.ALTURA,
                //ZTPRODUTOCOMPL.COMPRIMENTO,
                //ZTPRODUTOCOMPL.DISTANCIAMENTO,
                //                                    ZTPRODUTOCOMPL.ACABAMENTO,
                //ZTPRODUTOCOMPL.CHAPA,
                //                                    TPRODUTO.INATIVO
                //                                FROM 
                //                                 TPRODUTO

                //left join ZTPRODUTOCOMPL
                //on ZTPRODUTOCOMPL.CODCOLIGADA = TPRODUTO.CODCOLPRD
                //and ZTPRODUTOCOMPL.IDPRD = TPRODUTO.IDPRD

                //                                    inner join ZTPRODUTOCOMPLEMENTO
                //                                    on ZTPRODUTOCOMPLEMENTO.IDPRD = TPRODUTO.IDPRD

                //                                where ZTPRODUTOCOMPLEMENTO.IDPRDPRINCIPAL = {0}", campoTextoIDPRD.Get());


                DataTable dt = MetodosSQL.GetDT(sql);

                if (medidaD.comboBox1.SelectedIndex > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["CODIGOPRD"].ToString().StartsWith("14"))
                        {
                            string Quantidade = medidaD.comboBox1.Text.Substring(6);

                            dt.Rows[i]["QTDUSADA"] = Convert.ToDecimal(Quantidade);
                        }
                    }
                }

                gridData3.Consulta = sql.Split(' ');
                gridData3.Atualizar();

                gridData3.gridControl1.DataSource = dt;

                sql = $@"select isnull(USAPRECOFIXO, 0) as 'USAPRECOFIXO' 
                            from ZTPRODUTOCOMPL
                            where IDPRD = {campoTextoIDPRD.Get()}";

                cePrecoFixo.Checked = MetodosSQL.GetField(sql, "USAPRECOFIXO") == "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridData3_Novo(object sender, EventArgs e)
        {
            try
            {
                using (FormSelecionaItemComposicao frm = new FormSelecionaItemComposicao())
                {
                    frm.ShowDialog();

                    if (frm.IDPRD > 0)
                    {
                        string sql = String.Format(@"insert into ZTPRODUTOCOMPLEMENTO values (1,1, {0}, {1}, {2}, {3})", campoTextoIDPRD.Get(), frm.IDPRD, frm.QUANTIDADE.ToString().Replace(",", "."), frm.PRECO.ToString().Replace(",", "."));

                        MetodosSQL.ExecQuery(sql);
                    }

                    atualizaComposicao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridData3_Excluir(object sender, EventArgs e)
        {
            try
            {
                DataRowCollection drc = gridData3.GetDataRows();

                string sql = @"delete from ZTPRODUTOCOMPLEMENTO where CODCOLIGADA = 1 and CODFILIAL = 1 and IDPRD = {0} AND IDPRDPRINCIPAL = {1}";

                List<string> delete = new List<string>();

                foreach (DataRow row in drc)
                {
                    delete.Add(String.Format(sql, row["IDPRD"].ToString(), campoTextoIDPRD.textEdit1.EditValue.ToString()));
                }

                MetodosSQL.ExecMultiple(delete);

                atualizaComposicao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cePrecoFixo_CheckedChanged(object sender, EventArgs e)
        {
            if ((cePrecoFixo.Checked) && (campoLookup2.Get() == "02"))
            {

                tbPrecoAcabado.Enabled = true;

                DataTable dtPrecoFixo = new DataTable();

                dtPrecoFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT PRECOFIXO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(campoTextoIDPRD.textEdit1.Text) });

                if (dtPrecoFixo.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dtPrecoFixo.Rows[0]["PRECOFIXO"].ToString()) | dtPrecoFixo.Rows[0]["PRECOFIXO"].ToString() == "0,00")
                    {
                        tbPrecoAcabado.Enabled = cePrecoFixo.Checked;
                        tbPrecoAcabado.EditValue = 0;
                        txtFormula.textEdit1.ResetText();

                        listaProduto.Enabled = false;
                        listaProduto.comboBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        tbPrecoAcabado.Enabled = true;

                        txtFormula.textEdit1.ResetText();

                        tbPrecoAcabado.EditValue = Convert.ToDecimal(dtPrecoFixo.Rows[0]["PRECOFIXO"]);
                    }
                }
            }
            else if (campoLookup2.Get() == "03")
            {
                listaProduto.Enabled = false;

                tbPrecoAcabado.ResetText();
                tbPrecoAcabado.Enabled = cePrecoFixo.Checked;

                txtFormula.textEdit1.ResetText();
            }
            else
            {
                listaProduto.Enabled = true;

                tbPrecoAcabado.ResetText();
                tbPrecoAcabado.Enabled = cePrecoFixo.Checked;

                string sql = String.Format(@"select FORMULAPRECO from ZTPRODUTOREGRA where CODDP = '{0}'", cbCodigoDP.SelectedItem.ToString());

                txtFormula.textEdit1.Text = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField("", sql, new Object[] { }).ToString();

                // Atualiza os valores na tabela
                AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction(@"UPDATE ZTPRODUTOCOMPL
                                                                                    SET USAPRECOFIXO = 0, PRECOFIXO = 0 
                                                                                    WHERE IDPRD = ? AND CODCOLIGADA = ?", new Object[] { Convert.ToInt32(campoTextoIDPRD.textEdit1.EditValue), AppLib.Context.Empresa });
            }

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void campoLookup2_AposSelecao(object sender, EventArgs e)
        {
            regraEnableDisable();
        }

        private void chkCalculadoFormulado_CheckedChanged(object sender, EventArgs e)
        {
            regraEnableDisable();
        }

        private void medidaD_AposSelecao(object sender, EventArgs e)
        {
            try
            {
                string sql = String.Format(@"SELECT 
	                                            TPRODUTO.CODIGOPRD,
                                                TPRODUTO.IDPRD,
	                                            TPRODUTO.CODIGOAUXILIAR,
	                                            TPRODUTO.NOMEFANTASIA,
	                                            ZTPRODUTOCOMPLEMENTO.QUANTIDADE as 'QTDUSADA',        
                                                ZTPRODUTOCOMPLEMENTO.PRECO,       
                                                TPRODUTO.INATIVO
                                            FROM 
	                                            TPRODUTO
										
                                                inner join ZTPRODUTOCOMPLEMENTO
                                                on ZTPRODUTOCOMPLEMENTO.IDPRD = TPRODUTO.IDPRD

                                            where ZTPRODUTOCOMPLEMENTO.IDPRDPRINCIPAL = {0}", campoTextoIDPRD.Get());

                DataTable dt = MetodosSQL.GetDT(sql);

                if (medidaD.comboBox1.SelectedIndex > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["CODIGOPRD"].ToString().StartsWith("14"))
                        {
                            decimal Quantidade = Convert.ToDecimal(medidaD.comboBox1.Text.Substring(6));

                            AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction("UPDATE ZTPRODUTOCOMPLEMENTO SET QUANTIDADE = ? WHERE IDPRDPRINCIPAL = ? AND CODCOLIGADA = ? AND IDPRD = ?", new object[] { Quantidade, campoTextoIDPRD.Get(), AppLib.Context.Empresa, dt.Rows[i]["IDPRD"] });
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["CODIGOPRD"].ToString().StartsWith("14"))
                        {
                            AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction("UPDATE ZTPRODUTOCOMPLEMENTO SET QUANTIDADE = 0 WHERE IDPRDPRINCIPAL = ? AND CODCOLIGADA = ? AND IDPRD = ?", new object[] { campoTextoIDPRD.Get(), AppLib.Context.Empresa, dt.Rows[i]["IDPRD"] });
                        }
                    }
                }

                gridData3.Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar Distanciamento. Detalhes: \r\n" + ex.Message);
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Especificações")
            {
                FormProdutoCadastro_AntesSalvar(this, null);
                this.Salvar();
            }
        }

        private void campoTexto2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(campoTexto2.Get()))
            {
                string _sql = @"SELECT CODIGOAUXILIAR FROM TPRD WHERE CODIGOAUXILIAR = ? AND CODCOLIGADA = ? " + (edita == true ? " AND IDPRD <> '" + campoTextoIDPRD.Get() + "'" : "");
                if (AppLib.Context.poolConnection.Get(this.Conexao).ExecHasRows(_sql, new Object[] { campoTexto2.Get(), AppLib.Context.Empresa }))
                {
                    MessageBox.Show("Código auxiliar já existente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        #region Métodos

        /// <summary>
        /// Método responsável por limpar os registos dos campos da aba Especificações quando o Tipo de Produto for diferente de "02" ou "03"
        /// </summary>
        /// 
        private void AtualizaTabelasByTipoProduto()
        {
            DataTable ZtprodutocomplExists;
            DataTable TprofutodefExists;

            ZtprodutocomplExists = MetodosSQL.GetDT(@"SELECT P.CODDP, P.USAPRECOFIXO, P.PRECOFIXO, P.LARGURA, P.ALTURA, P.COMPRIMENTO, P.DISTANCIAMENTO, P.RAIO, P.TIPORAIO, P.COMPLEMENTO, P.CHAPA, P.ACABAMENTO, P.VIROLA, P.SEPTO, P.TABELAPRECO 
                                                        FROM ZTPRODUTOCOMPL P
                                                        WHERE IDPRD = " + Convert.ToInt32(campoTextoIDPRD.textEdit1.EditValue) + " AND CODCOLIGADA = " + AppLib.Context.Empresa + "");

            TprofutodefExists = MetodosSQL.GetDT(@"SELECT D.PRECO1 FROM TPRODUTODEF D WHERE IDPRD = " + Convert.ToInt32(campoTextoIDPRD.textEdit1.EditValue) + " AND CODCOLIGADA = " + AppLib.Context.Empresa + "");

            if (ZtprodutocomplExists.Rows.Count > 0)
            {
                try
                {
                    MetodosSQL.ExecQuery(@"UPDATE ZTPRODUTOCOMPL 
                                            SET CODDP = '', USAPRECOFIXO = 0, PRECOFIXO = 0, LARGURA = 0, ALTURA = 0, COMPRIMENTO = 0, DISTANCIAMENTO = 0, RAIO = 0, TIPORAIO = 0, COMPLEMENTO = 0, CHAPA = '', ACABAMENTO = '', VIROLA = 0, SEPTO = 0, TABELAPRECO = ''
                                            WHERE IDPRD = " + Convert.ToInt32(campoTextoIDPRD.textEdit1.EditValue) + " AND CODCOLIGADA = " + AppLib.Context.Empresa + "");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            if (TprofutodefExists.Rows.Count > 0)
            {
                try
                {
                    MetodosSQL.ExecQuery(@"UPDATE TPRODUTODEF 
                                            SET PRECO1 = 0
                                            WHERE IDPRD = " + Convert.ToInt32(campoTextoIDPRD.textEdit1.EditValue) + " AND CODCOLIGADA = " + AppLib.Context.Empresa + "");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            LimpaCamposByTipoProduto();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LimpaCamposByTipoProduto()
        {
            cbCodigoDP.ResetText();
            tbPrecoRevenda.ResetText();
            tbPrecoAcabado.ResetText();

            tbLargura.ResetText();
            tbAltura.ResetText();
            tbComprimento.ResetText();

            listaAcabamento.comboBox1.SelectedIndex = 0;
            listaChapa.comboBox1.SelectedIndex = 0;
            listaProduto.comboBox1.SelectedIndex = 0;

            txtFormula.textEdit1.ResetText();
        }

        /// <summary>
        /// 
        /// </summary>
        private void HabilitaCamposComplementares()
        {
            try
            {
                if (verificaDP)
                {
                    string sql = String.Format(@"select * from ZTPRODUTOREGRA where CODDP = '{0}'", cbCodigoDP.SelectedItem.ToString());

                    DataTable dt = MetodosSQL.GetDT(sql);

                    if (dt.Rows.Count == 1)
                    {
                        tbLargura.Enabled = (int)dt.Rows[0]["FLAGLARGURA"] == 1;
                        tbAltura.Enabled = (int)dt.Rows[0]["FLAGALTURA"] == 1;
                        tbComprimento.Enabled = (int)dt.Rows[0]["FLAGCOMPRIMENTO"] == 1;
                        medidaD.Enabled = (int)dt.Rows[0]["FLAGDISTANCIAMENTO"] == 1;
                        listaChapa.Enabled = (int)dt.Rows[0]["FLAGCHAPA"] == 1;
                        listaAcabamento.Enabled = (int)dt.Rows[0]["FLAGACABAMENTO"] == 1;
                        listaVirola.Enabled = (int)dt.Rows[0]["FLAGVIROLA"] == 1;
                        raio.Enabled = (int)dt.Rows[0]["FLAGRAIO"] == 1;
                        listaRaio.Enabled = (int)dt.Rows[0]["FLAGTIPORAIO"] == 1;
                        listaSepto.Enabled = (int)dt.Rows[0]["FLAGSEPTO"] == 1;
                        listaComplemento.Enabled = (int)dt.Rows[0]["FLAGCOMPLEMENTO"] == 1;

                        if (((int)dt.Rows[0]["FLAGVIROLA"] == 1) && ((int)dt.Rows[0]["FLAGSEPTO"] == 1))
                        {
                            listaVirola.Set("1");
                            listaSepto.Set("1");
                        }

                        if ((int)dt.Rows[0]["FLAGLARGURA"] == 0) { tbLargura.EditValue = null; }
                        if ((int)dt.Rows[0]["FLAGALTURA"] == 0) { tbAltura.EditValue = null; }
                        if ((int)dt.Rows[0]["FLAGCOMPRIMENTO"] == 0) { tbComprimento.EditValue = null; }
                        if ((int)dt.Rows[0]["FLAGDISTANCIAMENTO"] == 0) { medidaD.Set(null); }
                        if ((int)dt.Rows[0]["FLAGCHAPA"] == 0) { listaChapa.Set(null); }
                        if ((int)dt.Rows[0]["FLAGACABAMENTO"] == 0) { listaAcabamento.Set(null); }
                        if ((int)dt.Rows[0]["FLAGVIROLA"] == 0) { listaVirola.Set(null); }
                        if ((int)dt.Rows[0]["FLAGRAIO"] == 0) { raio.Set(null); }
                        if ((int)dt.Rows[0]["FLAGTIPORAIO"] == 0) { listaRaio.Set(null); }
                        if ((int)dt.Rows[0]["FLAGSEPTO"] == 0) { listaSepto.Set(null); }
                        if ((int)dt.Rows[0]["FLAGCOMPLEMENTO"] == 0) { listaComplemento.Set(null); }

                        listaProduto.Enabled = true;

                        string formula = dt.Rows[0]["FORMULAPRECO"].ToString();

                        txtFormula.Set(formula);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsCalculoAcabado()
        {
            int CalculadoAcabado = (int)AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CALCCOMOACABADO FROM ZTPRODUTOCOMPL WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, campoTextoIDPRD.textEdit1.Text });

            if (CalculadoAcabado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DeleteEspecificacoes()
        {
            try
            {
                AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction("DELETE FROM ZTPRODUTOCOMPL WHERE ZTPRODUTOCOMPL.CODCOLIGADA = ? AND ZTPRODUTOCOMPL.IDPRD = ?", new object[] { AppLib.Context.Empresa, campoTextoIDPRD.textEdit1.EditValue });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CarregaProduto()
        {
            DataTable dtTPRODUTO = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * 
                                                                                               FROM TPRODUTO
                                                                                               WHERE CODCOLPRD = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, IDPRD });

            DataTable dtTPRODUTODEF = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * 
                                                                                               FROM TPRODUTODEF
                                                                                               WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, IDPRD });

            DataTable dtTPRDHISTORICO = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * 
                                                                                               FROM TPRDHISTORICO
                                                                                               WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, IDPRD });

            DataTable dtTPRDCOMPL = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * 
                                                                                               FROM TPRDCOMPL
                                                                                               WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, IDPRD });

            DataTable dtTPRDFISCAL = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * 
                                                                                               FROM TPRDFISCAL
                                                                                               WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, IDPRD });

            if (dtTPRODUTO != null)
            {
                for (int i = 0; i < dtTPRODUTO.Rows.Count; i++)
                {
                    // Nome Fantasia
                    campoTexto3.Set(dtTPRODUTO.Rows[i]["NOMEFANTASIA"].ToString());

                    // Código Auxiliar
                    campoTexto2.Set(dtTPRODUTO.Rows[i]["CODIGOAUXILIAR"].ToString());

                    // Código do Produto
                    campoTexto1.Set(dtTPRODUTO.Rows[i]["CODIGOPRD"].ToString());

                    // IDPRD
                    campoTextoIDPRD.Set(dtTPRODUTO.Rows[i]["IDPRD"].ToString());

                    // Tipo
                    campoLista1.Set(dtTPRODUTO.Rows[i]["TIPO"].ToString());

                    // Ultimo Nível
                    campoLista2.Set(dtTPRODUTO.Rows[i]["ULTIMONIVEL"].ToString());

                    // Inativo
                    campoLista3.Set(dtTPRODUTO.Rows[i]["INATIVO"].ToString());

                    // Código Reduzido
                    campoTexto20.Set(dtTPRODUTO.Rows[i]["CODIGOREDUZIDO"].ToString());

                    // Data Cadastramento
                    campoDataHora1.Set(Convert.ToDateTime(dtTPRODUTO.Rows[i]["DTCADASTRAMENTO"]));

                    // Usuário Criação
                    campoTextoUSUARIOCRIACAO.Set(dtTPRODUTO.Rows[i]["USUARIOCRIACAO"].ToString());

                    // Data da Última Alteração
                    campoDataHora2.Set(Convert.ToDateTime(dtTPRODUTO.Rows[i]["DATAULTALTERACAO"]));

                    // Usuário da Última Alteração
                    campoTextoCODUSUARIO.Set(dtTPRODUTO.Rows[i]["CODUSUARIO"].ToString());

                    // Descrição 
                    campoMemo1.Set(dtTPRODUTO.Rows[i]["DESCRICAO"].ToString());

                    // Descrição Auxiliar
                    campoMemo2.Set(dtTPRODUTO.Rows[i]["DESCRICAOAUX"].ToString());

                    // Peso Bruto
                    if (dtTPRODUTO.Rows[i]["PESOBRUTO"] != DBNull.Value)
                    {
                        campoDecimal1.Set(Convert.ToDecimal(dtTPRODUTO.Rows[i]["PESOBRUTO"]));
                    }

                    // Comprimento
                    if (dtTPRODUTO.Rows[i]["COMPRIMENTO"] != DBNull.Value)
                    {
                        campoDecimal2.Set(Convert.ToDecimal(dtTPRODUTO.Rows[i]["COMPRIMENTO"]));
                    }

                    // Largura
                    if (dtTPRODUTO.Rows[i]["LARGURA"] != DBNull.Value)
                    {
                        campoDecimal3.Set(Convert.ToDecimal(dtTPRODUTO.Rows[i]["LARGURA"]));
                    }

                    // Quantidade Volume
                    campoTexto12.Set(dtTPRODUTO.Rows[i]["QTDEVOLUME"].ToString());

                    // Observação
                    campoTexto9.Set(dtTPRODUTO.Rows[i]["OBSERVACAO"].ToString());

                    // Campo Livre 2
                    campoTexto10.Set(dtTPRODUTO.Rows[i]["CAMPOLIVRE2"].ToString());

                    // Número 
                    campoTexto11.Set(dtTPRODUTO.Rows[i]["NUMEROCCF"].ToString());

                    // Procedência
                    campoLista4.Set(dtTPRODUTO.Rows[i]["REFERENCIACP"].ToString());
                }
            }

            if (dtTPRODUTODEF != null)
            {
                for (int i = 0; i < dtTPRODUTODEF.Rows.Count; i++)
                {
                    // Código do Fabricante
                    campoLookup1.Set(dtTPRODUTODEF.Rows[i]["CODFAB"].ToString());

                    // Número Fabricante
                    campoTexto4.Set(dtTPRODUTODEF.Rows[i]["NUMNOFABRIC"].ToString());

                    // Tipo de Produto
                    campoLookup2.Set(dtTPRODUTODEF.Rows[i]["CODTB2FAT"].ToString());

                    // Grupo de Produto
                    campoLookup3.Set(dtTPRODUTODEF.Rows[i]["CODTB3FAT"].ToString());

                    // Família de Produto
                    campoLookup4.Set(dtTPRODUTODEF.Rows[i]["CODTB4FAT"].ToString());

                    // Local de Estoque
                    campoLookup5.Set(dtTPRODUTODEF.Rows[i]["CODTB5FAT"].ToString());

                    // Natureza Orçamentária/Financeira
                    campoLookup6.Set(dtTPRODUTODEF.Rows[i]["CODTBORCAMENTO"].ToString());

                    // Moeda Padrão
                    campoLookup7.Set(dtTPRODUTODEF.Rows[i]["CODMOEPRECO1"].ToString());

                    // Tabela 10
                    campoLookup8.Set(dtTPRODUTODEF.Rows[i]["CODMOEPRECO2"].ToString());

                    // Tabela 15
                    campoLookup9.Set(dtTPRODUTODEF.Rows[i]["CODMOEPRECO3"].ToString());

                    // Tabela 20
                    campoLookup10.Set(dtTPRODUTODEF.Rows[i]["CODMOEPRECO4"].ToString());

                    // Tabela 25
                    campoLookup11.Set(dtTPRODUTODEF.Rows[i]["CODMOEPRECO5"].ToString());

                    // Data Base - Preço 1
                    if (dtTPRODUTODEF.Rows[i]["DATABASEPRECO1"] != DBNull.Value)
                    {
                        campoData1.Set(Convert.ToDateTime(dtTPRODUTODEF.Rows[i]["DATABASEPRECO1"]));
                    }

                    // Data Base - Preço 2
                    if (dtTPRODUTODEF.Rows[i]["DATABASEPRECO2"] != DBNull.Value)
                    {
                        campoData1.Set(Convert.ToDateTime(dtTPRODUTODEF.Rows[i]["DATABASEPRECO2"]));
                    }

                    // Data Base - Preço 3
                    if (dtTPRODUTODEF.Rows[i]["DATABASEPRECO3"] != DBNull.Value)
                    {
                        campoData1.Set(Convert.ToDateTime(dtTPRODUTODEF.Rows[i]["DATABASEPRECO3"]));
                    }

                    // Data Base - Preço 4
                    if (dtTPRODUTODEF.Rows[i]["DATABASEPRECO4"] != DBNull.Value)
                    {
                        campoData1.Set(Convert.ToDateTime(dtTPRODUTODEF.Rows[i]["DATABASEPRECO4"]));
                    }

                    // Data Base - Preço 5
                    if (dtTPRODUTODEF.Rows[i]["DATABASEPRECO5"] != DBNull.Value)
                    {
                        campoData1.Set(Convert.ToDateTime(dtTPRODUTODEF.Rows[i]["DATABASEPRECO5"]));
                    }

                    // Preço 1
                    if (dtTPRODUTODEF.Rows[i]["PRECO1"] != DBNull.Value)
                    {
                        tbPrecoRevenda.EditValue = Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["PRECO1"]);
                    }

                    // Preço 2
                    if (dtTPRODUTODEF.Rows[i]["PRECO2"] != DBNull.Value)
                    {
                        campoDecimal1.Set(Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["PRECO2"]));
                    }

                    // Preço 3
                    if (dtTPRODUTODEF.Rows[i]["PRECO3"] != DBNull.Value)
                    {
                        campoDecimal1.Set(Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["PRECO3"]));
                    }

                    // Preço 4
                    if (dtTPRODUTODEF.Rows[i]["PRECO4"] != DBNull.Value)
                    {
                        campoDecimal1.Set(Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["PRECO4"]));
                    }

                    // Preço 5
                    if (dtTPRODUTODEF.Rows[i]["PRECO5"] != DBNull.Value)
                    {
                        campoDecimal1.Set(Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["PRECO5"]));
                    }

                    // Tolerância Preço - Inferior 
                    if (dtTPRODUTODEF.Rows[i]["TOLINFPRECO"] != DBNull.Value)
                    {
                        campoDecimal11.Set(Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["TOLINFPRECO"]));
                    }

                    // Tolerância Preço - Superior
                    if (dtTPRODUTODEF.Rows[i]["TOLSUPPRECO"] != DBNull.Value)
                    {
                        campoDecimal12.Set(Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["TOLSUPPRECO"]));
                    }

                    // Tolerância Quantidade - Inferior 
                    if (dtTPRODUTODEF.Rows[i]["TOLERANCIAINF"] != DBNull.Value)
                    {
                        campoDecimal10.Set(Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["TOLERANCIAINF"]));
                    }

                    // Tolerância Quantidade - Superior
                    if (dtTPRODUTODEF.Rows[i]["TOLERANCIASUP"] != DBNull.Value)
                    {
                        campoDecimal9.Set(Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["TOLERANCIASUP"]));
                    }

                    // Unidade de Controle
                    campoLookup12.Set(dtTPRODUTODEF.Rows[i]["CODUNDCONTROLE"].ToString());

                    // Unidade de Compra
                    campoLookup13.Set(dtTPRODUTODEF.Rows[i]["CODUNDCOMPRA"].ToString());

                    // Unidade de Venda
                    campoLookup14.Set(dtTPRODUTODEF.Rows[i]["CODUNDVENDA"].ToString());

                    // Custo Médio - Data
                    if (dtTPRODUTODEF.Rows[i]["DATACUSTOMEDIO"] != DBNull.Value)
                    {
                        campoData6.Set(Convert.ToDateTime(dtTPRODUTODEF.Rows[i]["DATACUSTOMEDIO"]));
                    }

                    // Custo Médio - Valor
                    if (dtTPRODUTODEF.Rows[i]["CUSTOMEDIO"] != DBNull.Value)
                    {
                        campoDecimal13.Set(Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["CUSTOMEDIO"]));
                    }

                    // Custo Unitário - Data
                    if (dtTPRODUTODEF.Rows[i]["DTCUSTOUNITARIO"] != DBNull.Value)
                    {
                        campoData7.Set(Convert.ToDateTime(dtTPRODUTODEF.Rows[i]["DTCUSTOUNITARIO"]));
                    }

                    // Custo Unitário - Valor
                    if (dtTPRODUTODEF.Rows[i]["CUSTOUNITARIO"] != DBNull.Value)
                    {
                        campoDecimal14.Set(Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["CUSTOUNITARIO"]));
                    }
                }
            }

            if (dtTPRDHISTORICO != null)
            {
                for (int i = 0; i < dtTPRDHISTORICO.Rows.Count; i++)
                {
                    // Histórico Longo
                    campoMemo3.Set(dtTPRDHISTORICO.Rows[i]["HISTORICOLONGO"].ToString());
                }
            }

            if (dtTPRDFISCAL != null)
            {
                for (int i = 0; i < dtTPRDFISCAL.Rows.Count; i++)
                {
                    // Situação da Mercadoria
                    listaSituacaoMercadoria.Set(dtTPRDFISCAL.Rows[i]["SITUACAOMERCADORIA"].ToString());
                }
            }

            if (dtTPRDCOMPL != null)
            {
                for (int i = 0; i < dtTPRDCOMPL.Rows.Count; i++)
                {
                    // Código da Atividades Economica - P100
                    //if (dtTPRDCOMPL.Rows[i]["CODATIVECONOMIC"] != DBNull.Value)
                    //{
                    //    campoTexto13.Set(dtTPRDCOMPL.Rows[i]["CODATIVECONOMIC"].ToString());
                    //}                  

                    // Controla Dimensão
                    //campoLookup15.Set(dtTPRDCOMPL.Rows[i]["CONTROLEDIM"].ToString());

                    // Descrição para Anexo BNDES
                    //campoMemo4.Set(dtTPRDCOMPL.Rows[i]["DESCANEXOBNDES"].ToString());

                    // Descrição para Financiamentos
                    //campoMemo5.Set(dtTPRDCOMPL.Rows[i]["DESCAUXILIAR"].ToString());

                    // Descrição Padrão
                    //campoMemo7.Set(dtTPRDCOMPL.Rows[i]["DESCPADRAO"].ToString());

                    // Medida Fixa
                    //if (dtTPRDCOMPL.Rows[i]["MEDIDAFIXA"] != DBNull.Value)
                    //{
                    //    campoDecimal15.Set(Convert.ToDecimal(dtTPRODUTODEF.Rows[i]["MEDIDAFIXA"]));
                    //}

                    // Imprime Tabela de Preço (Necessário preencher o valor do IPI)
                    //campoLookup16.Set(dtTPRDCOMPL.Rows[i]["TABELAPRECO"].ToString());
                }
            }
        }

        private void CarregaGridTributos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT IDPRD, CODTRB, ALIQUOTA, CODCONTRIBSOCIAL, SITTRIBUTARIAENT, SITTRIBUTARIASAI, CODIGORECEITA
                                                                                    FROM TTRBPRD
                                                                                    WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, IDPRD });

            gcTributos.DataSource = dt;
            gvTributos.BestFitColumns();
        }

        private void DesabilitaBotoes()
        {
            btnFiltros.Enabled = false;
            toolStripDropDownButton2.Enabled = false;
            toolStripDropDownButton3.Enabled = false;
            toolStripDropDownButton4.Enabled = false;
        }

        #endregion

        #region Tributos do Produto

        private void btnNovo_Click(object sender, EventArgs e)
        {
            New.Forms.Register.frmCadastroTributoProduto frmCadastroTributoProduto = new New.Forms.Register.frmCadastroTributoProduto();
            frmCadastroTributoProduto.IDPRD = IDPRD;
            frmCadastroTributoProduto.ShowDialog();

            CarregaGridTributos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (gvTributos.SelectedRowsCount > 0)
            {
                DataRow row = gvTributos.GetDataRow(Convert.ToInt32(gvTributos.GetSelectedRows().GetValue(0).ToString()));
                New.Forms.Register.frmCadastroTributoProduto frmCadastroTributoProduto = new New.Forms.Register.frmCadastroTributoProduto();

                frmCadastroTributoProduto.IDPRD = Convert.ToInt32(row["IDPRD"]);
                frmCadastroTributoProduto.edita = true;
                frmCadastroTributoProduto.ShowDialog();

                CarregaGridTributos();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (gvTributos.SelectedRowsCount > 0)
            {
                if (XtraMessageBox.Show("Deseja excluir o registro selecionado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DataRow row = gvTributos.GetDataRow(Convert.ToInt32(gvTributos.GetSelectedRows().GetValue(0).ToString()));

                    try
                    {
                        int result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM TTRBPRD WHERE CODCOLIGADA = ? AND IDPRD = ? AND CODTRB = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["IDPRD"]), row["CODTRB"].ToString() });

                        if (result > 0)
                        {
                            XtraMessageBox.Show("Tributo do Produto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CarregaGridTributos();
                            return;
                        }
                        else
                        {
                            XtraMessageBox.Show("Não foi possível excluir o registro selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Não foi possível excluir o registro selecionado.\r\nDetalhes: " + ex.Message + "", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (gvTributos.OptionsFind.AlwaysVisible == true)
            {
                gvTributos.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gvTributos.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            if (gvTributos.OptionsView.ShowGroupPanel == true)
            {
                gvTributos.OptionsView.ShowGroupPanel = false;
                gvTributos.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gvTributos.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void gvTributos_DoubleClick(object sender, EventArgs e)
        {
            if (gvTributos.SelectedRowsCount > 0)
            {
                DataRow row = gvTributos.GetDataRow(Convert.ToInt32(gvTributos.GetSelectedRows().GetValue(0).ToString()));
                New.Forms.Register.frmCadastroTributoProduto frmCadastroTributoProduto = new New.Forms.Register.frmCadastroTributoProduto();

                frmCadastroTributoProduto.IDPRD = Convert.ToInt32(row["IDPRD"]);
                frmCadastroTributoProduto.codigoTributo = row["CODTRB"].ToString();
                frmCadastroTributoProduto.edita = true;
                frmCadastroTributoProduto.ShowDialog();

                CarregaGridTributos();
            }
        }

        #endregion

        #region Validação da saída dos componentes LookUp

        private void campoLookup1_Leave(object sender, EventArgs e)
        {
            if (campoLookup1.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup1))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup1.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup2_Leave(object sender, EventArgs e)
        {
            if (campoLookup2.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup2))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup2.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup3_Leave(object sender, EventArgs e)
        {
            if (campoLookup3.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup3))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup3.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup4_Leave(object sender, EventArgs e)
        {
            if (campoLookup4.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup4))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup4.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup5_Leave(object sender, EventArgs e)
        {
            if (campoLookup5.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup5))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup5.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup6_Leave(object sender, EventArgs e)
        {
            if (campoLookup6.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup6))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup6.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup7_Leave(object sender, EventArgs e)
        {
            if (campoLookup7.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup7))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup7.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup8_Leave(object sender, EventArgs e)
        {
            if (campoLookup8.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup8))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup8.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup9_Leave(object sender, EventArgs e)
        {
            if (campoLookup9.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup9))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup9.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup10_Leave(object sender, EventArgs e)
        {
            if (campoLookup10.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup10))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup10.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup11_Leave(object sender, EventArgs e)
        {
            if (campoLookup11.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup11))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup11.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup12_Leave(object sender, EventArgs e)
        {
            if (campoLookup12.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup12))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup12.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup13_Leave(object sender, EventArgs e)
        {
            if (campoLookup13.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup13))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup13.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup14_Leave(object sender, EventArgs e)
        {
            if (campoLookup14.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup14))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup14.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookup15_Leave(object sender, EventArgs e)
        {
            if (campoLookup15.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookup15))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookup15.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        #endregion

        private void campoLista5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
