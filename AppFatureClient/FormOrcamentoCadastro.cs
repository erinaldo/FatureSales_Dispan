using AppFatureClient.Classes;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormOrcamentoCadastro : AppLib.Windows.FormCadastroObject
    {
        #region Variáveis

        public DataRow row;

        public Boolean CopiaDeMovimento = false;
        private List<TITMMOV> Itens = new List<TITMMOV>();
        private List<TITMMOV> ItensBanco = new List<TITMMOV>();
        private Boolean ExcluiuTudo = false;
        private string status;
        public int IDMOVCOPIADO = 0;
        private bool InserirNovo = true;
        public int IdMov;

        public bool isNovoRegistro = false;
        public bool isCancelado = false;

        private bool CopiandoNumeroPedido = false;

        bool IsPrecoUnitarioCalculado = false;
        bool IsValorTotalCalculado = false;

        public string CODSERIE;

        private int indexRegistroSelecionado = 0;

        private New.Class.Utilities util = new New.Class.Utilities();

        #endregion
        public FormOrcamentoCadastro()
        {
            InitializeComponent();

            SetConsulta();

            campoTextoNORDEM.textEdit1.Properties.MaxLength = 15;

            txtTelefoneCom.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            txtTelefoneCom.textEdit1.Properties.Mask.EditMask = "(99) 9999-9999";
            cbTel.Checked = true;
        }

        private void FormOrcamentoCadastro_Load(object sender, EventArgs e)
        {
            try
            {
                tbValorFrete.EditValue = 0.0000;
                tbPercentualFrete.EditValue = 0.0000;

                tbAcrescimoPercentual.EditValue = 0.0000;
                tbAcrescimoDesp.EditValue = 0.0000;

                tbValorDesconto.EditValue = 0.0000;
                tbDescontoPercentual.EditValue = 0.0000;

                #region Eventos Keydown

                dteDataEmissao.KeyDown += dteDataEmissao_KeyDown;
                campoTextoNORDEM.textEdit1.KeyDown += campoTextoNORDEM_KeyDown;
                campoLookupCODCFO.textBoxCODIGO.KeyDown += campoLookupCODCFO_KeyDown;
                campoLista2.KeyDown += campoLista2_KeyDown;
                listaContribuinteICMS.KeyDown += listaContribuinteICMS_KeyDown;

                txtNomeCliente.textEdit1.KeyDown += txtNomeCliente_KeyDown;
                cePossuiBeneficio.KeyDown += cePossuiBeneficio_KeyDown;
                ceSemImposto.KeyDown += ceSemImposto_KeyDown;
                cbConsumidorFinal.KeyDown += cbConsumidorFinal_KeyDown;
                txtCONTATOCOM.textEdit1.KeyDown += txtCONTATOCOM_KeyDown;
                cbTel.KeyDown += cbTel_KeyDown;
                cbCel.KeyDown += cbCel_KeyDown;
                txtTelefoneCom.textEdit1.KeyDown += txtTelefoneCom_KeyDown;
                txtEMAILCOM.textEdit1.KeyDown += txtEMAILCOM_KeyDown;
                campoDataENTREGA.dateTimePicker1.KeyDown += campoDataENTREGA_KeyDown;
                campoLista1.comboBox1.KeyDown += campoLista1_KeyDown;
                campoListaAplicacao.comboBox1.KeyDown += campoListaAplicacao_KeyDown;
                campoLookupCODRPR.textBoxCODIGO.KeyDown += campoLookupCODRPR_KeyDown;
                campoLookupCODTRA.textBoxCODIGO.KeyDown += campoLookupCODTRA_KeyDown;
                campoListaFRETECIFOUFOB.comboBox1.KeyDown += campoListaFRETECIFOUFOB_KeyDown;
                clLocalEstoque.textBoxCODIGO.KeyDown += clLocalEstoque_KeyDown;
                clSerie.textBoxCODIGO.KeyDown += clSerie_KeyDown;
                clVendedorInterno.textBoxCODIGO.KeyDown += clVendedorInterno_KeyDown;
                clCondigaoPagamento.textBoxCODIGO.KeyDown += clCondigaoPagamento_KeyDown;
                clTabelaFaturamento.textBoxCODIGO.KeyDown += clTabelaFaturamento_KeyDown;

                #endregion

                #region Eventos PreviewKeyDown

                dteDataEmissao.PreviewKeyDown += dteDataEmissao_PreviewKeyDown;
                //campoTextoNORDEM.textEdit1.PreviewKeyDown += campoTextoNORDEM_PreviewKeyDown;
                campoLookupCODCFO.textBoxCODIGO.PreviewKeyDown += campoLookupCODCFO_PreviewKeyDown;
                campoLista2.comboBox1.PreviewKeyDown += campoLista2_PreviewKeyDown;
                listaContribuinteICMS.comboBox1.PreviewKeyDown += listaContribuinteICMS_PreviewKeyDown;
                txtNomeCliente.textEdit1.PreviewKeyDown += txtNomeCliente_PreviewKeyDown;
                cePossuiBeneficio.PreviewKeyDown += cePossuiBeneficio_PreviewKeyDown;
                ceSemImposto.PreviewKeyDown += ceSemImposto_PreviewKeyDown;
                cbConsumidorFinal.PreviewKeyDown += cbConsumidorFinal_PreviewKeyDown;
                txtCONTATOCOM.textEdit1.PreviewKeyDown += txtCONTATOCOM_PreviewKeyDown;
                cbTel.PreviewKeyDown += cbTel_PreviewKeyDown;
                cbCel.PreviewKeyDown += cbCel_PreviewKeyDown;
                txtTelefoneCom.textEdit1.PreviewKeyDown += txtTelefoneCom_PreviewKeyDown;
                txtEMAILCOM.textEdit1.PreviewKeyDown += txtEMAILCOM_PreviewKeyDown;
                campoDataENTREGA.dateTimePicker1.PreviewKeyDown += campoDataENTREGA_PreviewKeyDown;
                campoLista1.comboBox1.PreviewKeyDown += campoLista1_PreviewKeyDown;
                campoListaAplicacao.comboBox1.PreviewKeyDown += campoListaAplicacao_PreviewKeyDown;
                campoLookupCODRPR.textBoxCODIGO.PreviewKeyDown += campoLookupCODRPR_PreviewKeyDown;
                campoLookupCODTRA.textBoxCODIGO.PreviewKeyDown += campoLookupCODTRA_PreviewKeyDown;
                campoListaFRETECIFOUFOB.comboBox1.PreviewKeyDown += campoListaFRETECIFOUFOB_PreviewKeyDown;
                clLocalEstoque.textBoxCODIGO.PreviewKeyDown += clLocalEstoque_PreviewKeyDown;
                clSerie.textBoxCODIGO.PreviewKeyDown += clSerie_PreviewKeyDown;
                clVendedorInterno.textBoxCODIGO.PreviewKeyDown += clVendedorInterno_PreviewKeyDown;
                clCondigaoPagamento.textBoxCODIGO.PreviewKeyDown += clCondigaoPagamento_PreviewKeyDown;
                clTabelaFaturamento.textBoxCODIGO.PreviewKeyDown += clTabelaFaturamento_PreviewKeyDown;

                #endregion

                campoLookupCODCFO.textBoxDESCRICAO.Click += TextBoxDESCRICAO_Click;

                listaContribuinteICMS.comboBox1.TextChanged += ((object sender2, EventArgs e2) =>
                {
                    if (listaContribuinteICMS.Get() == "0")
                    {
                        cbConsumidorFinal.Checked = true;
                    }
                    else
                    {
                        cbConsumidorFinal.Checked = false;
                    }
                });

                toolStrip1.Enabled = false;

                // Transformar em método 
                DataTable dt = MetodosSQL.GetDT(@"SELECT CODINTERNO, DESCRICAO 
                                                  FROM GCONSIST 
                                                  WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " AND CODTABELA = 'PRAZOFAB'");

                cbPrazoFabricacao.DataSource = dt;

                cbPrazoFabricacao.DisplayMember = "DESCRICAO";
                cbPrazoFabricacao.ValueMember = "CODINTERNO";

                if (CopiaDeMovimento)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormCaption("Processando...");
                }

                if (this.Acao == AppLib.Global.Types.Acao.Editar)
                {
                    #region Código antigo

                    status = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT STATUS FROM TMOV WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Empresa }).ToString();


                    string sql = String.Format(@"select TIPOVENDA, APLICPROD, NCONTRIB, UFORCAMENTO, TIPODESC, SEMIMPOSTOS, USABENEFICIO from TMOVCOMPL
                                                  where CODCOLIGADA = {0}
                                                  and IDMOV = {1}", AppLib.Context.Empresa, campoInteiroIDMOV.Get());

                    if (MetodosSQL.GetField(sql, "USABENEFICIO") == "1")
                    {
                        cePossuiBeneficio.Checked = true;
                    }
                    else
                    {
                        cePossuiBeneficio.Checked = false;
                    }

                    campoLista1.Set(MetodosSQL.GetField(sql, "TIPOVENDA"));
                    campoListaAplicacao.Set(MetodosSQL.GetField(sql, "APLICPROD"));

                    if (MetodosSQL.GetField(sql, "TIPODESC") == "1")
                    {
                        rbItem.Checked = true;
                    }
                    else
                    {
                        rbGeral.Checked = true;
                    }

                    CalcularSaldo();

                    #endregion

                    #region Código novo

                    // Instância do objeto "item", responsável pelos métodos de manipulação dos itens
                    New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();

                    // Cria a tabela dos itens e configura as colunas
                    gcItensMovimento.DataSource = Item.GetTabela();
                    //gvItensMovimento.BestFitColumns();

                    // Carrega os itens de acordo com o IDMOV
                    Item.CarregaGridViewItens(Convert.ToInt32(campoInteiroIDMOV.Get()));

                    // Configura as colunas da grid, configurando assim os campos que podem ser modificados e o tamanho das colunas
                    Item.ConfiguraColunasGridView(gvItensMovimento);
                    Item.RenomeiaColunasGridView(gvItensMovimento);

                    // Atualiza a propriedade Datasource, atualizando a tabela com as novas configurações
                    gcItensMovimento.DataSource = Item.GetTabela();
                    //gvItensMovimento.BestFitColumns();

                    List<ItemComposicao> itemCompTemp = new List<ItemComposicao>();
                    for (int i = 0; i < Item.TabelaItens.Rows.Count; i++)
                    {
                        if (ValidaComposicao(Convert.ToInt32(Item.TabelaItens.Rows[i]["IDPRD"])))
                        {
                            foreach (ItemComposicao iCompTemp in GetItemComposicao(Convert.ToInt32(Item.TabelaItens.Rows[i]["NSEQITMMOV"])))
                            {
                                itemCompTemp.Add(iCompTemp);
                            }
                        }
                        else
                        {
                            Itens = Item.CarregaItens();
                        }

                        // Carrega os itens no objeto
                        /*
                        if (ValidaComposicao(Convert.ToInt32(Item.TabelaItens.Rows[i]["IDPRD"])))
                        {
                            Itens = Item.CarregaItens(GetItemComposicao(Convert.ToInt32(Item.TabelaItens.Rows[i]["NSEQITMMOV"])));
                        }
                        else
                        {
                            Itens = Item.CarregaItens();
                        }
                        */
                    }

                    Itens = Item.CarregaItens(itemCompTemp);

                    util.SetVisaoUsuario(gvItensMovimento, "ITENS");

                    New.Class.EnviromentHelper.IDMOV = (int)campoInteiroIDMOV.Get();

                    // Validação para carregar o valor do Prazo de Fabricação
                    string PrazoFabricacao = AppLib.Context.poolConnection.Get("Start").ExecGetField("", "SELECT PRAZO FROM TMOVCOMPL WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, (int)campoInteiroIDMOV.Get() }).ToString();

                    if (PrazoFabricacao == "99")
                    {
                        cbPrazoFabricacao.SelectedValue = PrazoFabricacao;
                        DateTime? DataEntrega = Convert.ToDateTime(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, "SELECT DATAENTREGA FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, (int)campoInteiroIDMOV.Get() }));
                        campoDataENTREGA.Set(DataEntrega);

                        //campoDataENTREGA.dateTimePicker1.Value = Convert.ToDateTime(DataEntrega);
                        //campoDataENTREGA.maskedTextBox1.Text = Convert.ToDateTime(DataEntrega).ToShortDateString();
                    }
                    else
                    {
                        cbPrazoFabricacao.SelectedValue = PrazoFabricacao;
                    }

                    CarregaCamposDesconto();

                    #endregion
                }
                else
                {
                    // Novo registro

                    #region Código antigo

                    campoTextoCAMPOLIVRE1.Set(MetodosSQL.GetField(@"select top 1 VALPROPOSTA from ZPARAMFATURE", "VALPROPOSTA"));

                    rbItem.Checked = true;

                    #endregion

                    #region Código novo

                    // Instância do objeto "item", responsável pelos métodos de manipulação dos itens
                    New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();

                    if (CopiaDeMovimento == true)
                    {
                        IDMOVCOPIADO = (int)campoInteiroIDMOV.Get();
                        campoLookupCODCFO_AposSelecao(null, null);
                        campoInteiroIDMOV.Set(null);
                        dteDataEmissao.Value = DateTime.Now;
                        campoTextoNUMEROMOV.Set(null);

                        // Cria a tabela dos itens e configura as colunas
                        gcItensMovimento.DataSource = Item.GetTabela();
                        //gvItensMovimento.BestFitColumns();

                        // Carrega os itens de acordo com o IDMOV
                        Item.CarregaGridViewItens(Convert.ToInt32(IDMOVCOPIADO));

                        // Configura as colunas da grid, configurando assim os campos que podem ser modificados e o tamanho das colunas
                        Item.ConfiguraColunasGridView(gvItensMovimento);
                        Item.RenomeiaColunasGridView(gvItensMovimento);

                        // Atualiza a propriedade Datasource, atualizando a tabela com as novas configurações
                        gcItensMovimento.DataSource = Item.GetTabela();
                        //gvItensMovimento.BestFitColumns();

                        // Verifica se o item possui composição
                        DataTable dtItens = gcItensMovimento.DataSource as DataTable;

                        List<ItemComposicao> composicaoItem = new List<ItemComposicao>();

                        for (int i = 0; i < dtItens.Rows.Count; i++)
                        {
                            composicaoItem = GetItemComposicao(Convert.ToInt32(dtItens.Rows[i]["NSEQITMMOV"]));
                        }

                        // Carrega os itens no objeto 
                        Itens = Item.CarregaItens(composicaoItem);

                        util.SetVisaoUsuario(gvItensMovimento, "ITENS");
                    }
                    else
                    {
                        // Cria a tabela dos itens e configura as colunas
                        gcItensMovimento.DataSource = Item.GetTabela();
                        //gvItensMovimento.BestFitColumns();

                        // Carrega a tabela sem itens
                        Item.CarregaGridViewItens();

                        // Configura as colunas da grid, configurando assim os campos que podem ser modificados e o tamanho das colunas
                        Item.ConfiguraColunasGridView(gvItensMovimento);
                        Item.RenomeiaColunasGridView(gvItensMovimento);

                        util.SetVisaoUsuario(gvItensMovimento, "ITENS");
                    }

                    #endregion
                }

                AtualizaValorTributo();

                SetConsulta();

                if (CopiaDeMovimento)
                {
                    if (IDMOVCOPIADO > 0)
                    {
                        IDMOVCOPIADO = (int)campoInteiroIDMOV.Get();
                        campoLookupCODCFO_AposSelecao(null, null);
                        campoInteiroIDMOV.Set(null);
                        dteDataEmissao.Value = DateTime.Now;
                        campoDataENTREGA.Set(DateTime.Now);
                        campoTextoNUMEROMOV.Set(null);

                        // Prazo de Fabricação
                        string PrazoFabricacao = AppLib.Context.poolConnection.Get("Start").ExecGetField("", "SELECT PRAZO FROM TMOVCOMPL WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, IDMOVCOPIADO }).ToString();

                        cbPrazoFabricacao.SelectedValue = PrazoFabricacao;

                        cbPrazoFabricacao_SelectedIndexChanged(sender, e);

                        if (CopiaDeMovimento && splashScreenManager1.IsSplashFormVisible)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }

                        if (MessageBox.Show("Deseja recalcular os preços dos itens copiados?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            for (int i = 0; i < Itens.Count; i++)
                            {
                                decimal total = 0;

                                Itens[i].PRECOTABELA = CalculoPreco.CacularPreco(Itens[i].IDPRD.ToString(), Itens[i].TIPOINOX, "", "").PRECO;

                                if (Itens[i].COMPOSICAO != null)
                                {
                                    foreach (ItemComposicao comp in Itens[i].COMPOSICAO)
                                    {
                                        total += (comp.QUANTIDADE * comp.PRECOUNITARIO);
                                    }

                                    Itens[i].PRECOTABELA = total;
                                }
                            }
                        }

                        New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();

                        gcItensMovimento.DataSource = Item.AtualizaGridView(Itens);

                        util.SetVisaoUsuario(gvItensMovimento, "ITENS");
                    }
                }

                // Data de Entrega
                DateTime dataEntrega = Convert.ToDateTime(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, @"SELECT DATAENTREGA FROM TMOV WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Empresa }));

                if (dataEntrega == new DateTime(0001, 01, 01, 0, 0, 0))
                {
                    campoDataENTREGA.Set(DateTime.Now);
                }
                else
                {
                    campoDataENTREGA.Set(dataEntrega);
                }

                // Data de Emissão
                DateTime dataEmissao = Convert.ToDateTime(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, @"SELECT DATAEMISSAO FROM TMOV WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Empresa }));

                if (dataEmissao == new DateTime(0001, 01, 01, 0, 0, 0))
                {
                    dteDataEmissao.Value = DateTime.Now;
                }
                else
                {
                    dteDataEmissao.Value = dataEmissao;
                }

                GetCODTMV();

                string sqlSemImpostos = String.Format(@"select SEMIMPOSTOS from TMOVCOMPL
                                                  where CODCOLIGADA = {0}
                                                  and IDMOV = {1}", AppLib.Context.Empresa, campoInteiroIDMOV.Get() == null ? IDMOVCOPIADO : campoInteiroIDMOV.Get());


                if (MetodosSQL.GetField(sqlSemImpostos, "SEMIMPOSTOS") == "1")
                {
                    ceSemImposto.Checked = true;
                }
                else
                {
                    ceSemImposto.Checked = false;
                }

                // Valores
                tbValorOutros.EditValue = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT VALOROUTROSORIG FROM TMOV WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Empresa }).ToString();
                tbValorBruto.EditValue = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT VALOROUTROSORIG FROM TMOV WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Empresa }).ToString();
                tbValorLiquido.EditValue = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT VALORLIQUIDOORIG FROM TMOV WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Empresa }).ToString();

                CarregaCreditoCliente(campoLookupCODCFO.Get());

                if (CODSERIE != "ORC")
                {
                    // Data Entrega
                    campoDataENTREGA.Enabled = true;

                    // Tipo de Venda
                    label2.Visible = true;
                    campoLista1.Visible = true;

                    campoLista1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            simpleButtonCANCELAR.Location = new System.Drawing.Point(1034, 15);
            simpleButtonOK.Location = new System.Drawing.Point(953, 15);
            simpleButtonSALVAR.Location = new System.Drawing.Point(872, 15);

            dteDataEmissao.Select();
            dteDataEmissao.Focus();
        }

        private void btnCopiarNumeroPedido_Click(object sender, EventArgs e)
        {
            CopiandoNumeroPedido = true;

            for (int i = 0; i < gvItensMovimento.RowCount; i++)
            {
                gvItensMovimento.SetRowCellValue(i, "NUMPEDIDO", campoTextoNORDEM.Get());
            }

            CopiandoNumeroPedido = false;
        }

        private void FecharAnexo(object sender, EventArgs e)
        {

        }

        private void TextBoxDESCRICAO_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(campoLookupCODCFO.Get()))
            {
                if (campoLookupCODCFO.dr == null)
                {
                    DataTable dtCliente = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZVWFCFO WHERE CODCFO = '" + campoLookupCODCFO.Get() + "'");

                    DataRow row = dtCliente.Rows[0];

                    new FormClienteCadastro().Editar(row, true);
                }
                else
                {
                    new FormClienteCadastro().Editar(campoLookupCODCFO.dr, true);
                }
            }
        }

        private void FormOrcamentoCadastro_AposSalvar(object sender, EventArgs e)
        {
            try
            {
                AppLib.ORM.Jit TMOVCOMPL = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "TMOVCOMPL");

                TMOVCOMPL.Set("CODCOLIGADA", AppLib.Context.Empresa);
                TMOVCOMPL.Set("IDMOV", (int)campoInteiroIDMOV.Get());
                TMOVCOMPL.Set("PRAZO", cbPrazoFabricacao.SelectedValue);
                TMOVCOMPL.Set("APLICPROD", campoListaAplicacao.Get());
                TMOVCOMPL.Save();
            }
            catch (Exception)
            {

            }

            //Instância do objeto "item", responsável pelos métodos de manipulação dos itens
            New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();

            // Carrega os itens no objeto 
            if (CopiaDeMovimento == true)
            {
                for (int j = 0; j < Itens.Count; j++)
                {
                    if (ValidaComposicao(Itens[j].IDPRD))
                    {
                        Itens = Item.CarregaItens(GetItemComposicao(Itens[j].NSEQITEMMOV), Itens);
                    }
                }
            }

            //Atualiza a tabela TITMMOVCOMPL        
            for (int i = 0; i < Itens.Count; i++)
            {
                decimal PrecoTabela = 0;

                decimal ValorDescOriginal = 0;
                decimal ValorDespOriginal = 0;

                // Adição
                if (Itens[i].TIPOAJUSTEVALOR == 1)
                {
                    PrecoTabela = (decimal)Itens[i].PRECOTABELA + Itens[i].VALORPINTURA + Itens[i].AJUSTEVALOR;
                }

                // Substração
                if (Itens[i].TIPOAJUSTEVALOR == 0 && Itens[i].AJUSTEVALOR > 0)
                {
                    PrecoTabela = (decimal)Itens[i].PRECOTABELA + Itens[i].VALORPINTURA - Itens[i].AJUSTEVALOR;
                }

                // Não teve alteração nenhuma
                if (Itens[i].TIPOAJUSTEVALOR == 0 && Itens[i].AJUSTEVALOR == 0)
                {
                    PrecoTabela = (decimal)Itens[i].PRECOTABELA + Itens[i].VALORPINTURA;
                }

                if (rbItem.Checked)
                {
                    ValorDescOriginal = PrecoTabela * ((decimal)tbDescontoPercentual.EditValue / 100);
                }
                else if (rbGeral.Checked)
                {
                    if (Convert.ToDecimal(tbDescontoPercentual.EditValue) > 0)
                    {
                        ValorDescOriginal = PrecoTabela * ((decimal)tbDescontoPercentual.EditValue / 100);
                    }
                    else
                    {
                        ValorDescOriginal = (decimal)tbValorDesconto.EditValue;
                    }
                }
                else
                {
                    ValorDescOriginal = 0;
                }

                ValorDespOriginal = PrecoTabela * (Convert.ToDecimal(tbAcrescimoPercentual.Text) / 100);

                try
                {
                    AppLib.ORM.Jit TITMMOVCOMPL = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "TITMMOVCOMPL");

                    TITMMOVCOMPL.Set("CODCOLIGADA", AppLib.Context.Empresa);
                    TITMMOVCOMPL.Set("IDMOV", (int)campoInteiroIDMOV.Get());
                    TITMMOVCOMPL.Set("NSEQITMMOV", Itens[i].NSEQITEMMOV);
                    TITMMOVCOMPL.Set("AJUSTEVALOR", Itens[i].AJUSTEVALOR);
                    TITMMOVCOMPL.Set("TIPOAJUSTEVALOR", Itens[i].TIPOAJUSTEVALOR);
                    TITMMOVCOMPL.Set("PRECOTABELA", Itens[i].PRECOTABELA);
                    TITMMOVCOMPL.Set("TIPOPINTURA", Itens[i].TIPOPINTURA);
                    TITMMOVCOMPL.Set("CORPINTURA", Itens[i].CORPINTURA);
                    TITMMOVCOMPL.Set("VALORPINTURA", Itens[i].VALORPINTURA);
                    TITMMOVCOMPL.Set("TIPO", Itens[i].TIPOINOX);
                    TITMMOVCOMPL.Set("APLICPROD", campoListaAplicacao.Get() != Itens[i].APLICACAO ? campoListaAplicacao.Get() : Itens[i].APLICACAO);
                    TITMMOVCOMPL.Set("XPED", Itens[i].NUMPEDIDO);
                    TITMMOVCOMPL.Set("NITEMPED", Itens[i].NUMITEMPEDIDO);

                    TITMMOVCOMPL.Set("DISTANCIAMENTO", Itens[i].DISTANCIAMENTO);

                    if (rbItem.Checked)
                    {
                        TITMMOVCOMPL.Set("VALORDESCORIGINAL", ValorDescOriginal);
                    }
                    else
                    {
                        TITMMOVCOMPL.Set("VALORDESCORIGINAL", 0);
                    }

                    TITMMOVCOMPL.Set("VALORDESPORIGINAL", ValorDespOriginal);
                    TITMMOVCOMPL.Save();
                }
                catch (Exception)
                {

                }
            }

            // Atualiza o objeto Itens após a atualização de alguns campos da tabela TITMMOVCOMPL
            Item.CarregaGridViewItens((int)campoInteiroIDMOV.Get());

            // TITMMOV
            for (int i = 0; i < Itens.Count; i++)
            {
                string Precounitario = "";
                decimal ValorTotal = 0;

                // Adição
                if (Itens[i].TIPOAJUSTEVALOR == 1)
                {
                    if (rbItem.Checked)
                    {
                        Precounitario = (Itens[i].PRECOTABELA + Itens[i].VALORPINTURA - Itens[i].VALORDESCORIGINAL + Itens[i].VALORDESPORIGINAL + Itens[i].AJUSTEVALOR).ToString().Replace(',', '.');
                    }
                    else
                    {
                        Precounitario = (Itens[i].PRECOTABELA + Itens[i].VALORPINTURA + Itens[i].AJUSTEVALOR).ToString().Replace(',', '.');
                    }
                }

                // Substração
                if (Itens[i].TIPOAJUSTEVALOR == 0 && Itens[i].AJUSTEVALOR > 0)
                {
                    if (rbItem.Checked)
                    {
                        Precounitario = (Itens[i].PRECOTABELA + Itens[i].VALORPINTURA - Itens[i].VALORDESCORIGINAL + Itens[i].VALORDESPORIGINAL - Itens[i].AJUSTEVALOR).ToString().Replace(',', '.');
                    }
                    else
                    {
                        Precounitario = (Itens[i].PRECOTABELA + Itens[i].VALORPINTURA - Itens[i].AJUSTEVALOR).ToString().Replace(',', '.');
                    }
                }

                // Não teve alteração nenhuma
                if (Itens[i].TIPOAJUSTEVALOR == 0)
                {
                    if (rbItem.Checked)
                    {
                        Precounitario = (Itens[i].PRECOTABELA + Itens[i].VALORPINTURA - Itens[i].VALORDESCORIGINAL + Itens[i].VALORDESPORIGINAL).ToString().Replace(',', '.');
                    }
                    else
                    {
                        Precounitario = (Itens[i].PRECOTABELA + Itens[i].VALORPINTURA).ToString().Replace(',', '.');
                    }
                }

                var ReversaoPrecoUnitario = Precounitario.ToString().Replace('.', ',');

                ValorTotal = (Convert.ToDecimal(ReversaoPrecoUnitario) * Itens[i].QUANTIDADE);
                var valorTotalConvertido = ValorTotal.ToString().Replace(',', '.');

                Itens[i].UNIDADE = ValidaUnidade(Itens[i]);
            }

            for (int i = 0; i < Itens.Count; i++)
            {
                if (Itens[i].COMPOSICAO != null)
                {
                    if (Itens[i].COMPOSICAO.Count > 0)
                    {
                        try
                        {
                            string IdMov = "";

                            IdMov = campoInteiroIDMOV.Get().ToString();

                            foreach (var composicao in Itens[i].COMPOSICAO)
                            {
                                // Verificar e há necessidade de validar o item aqui
                                if (ItemCompostoExists(IdMov, composicao.NSEQ, composicao.IDPRD))
                                {
                                    continue;
                                }
                                else
                                {
                                    AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction(@"insert into ZORCAMENTOITEMCOMPOSTO values (?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, 1, IdMov, composicao.NSEQ, composicao.IDPRD, composicao.QUANTIDADE.ToString().Replace(",", "."), composicao.PRECOUNITARIO.ToString().Replace(",", ".") });
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }

            if (!SetInformacaoComercial(tbInformacoesComerciais.Text, campoLookupCODCFO.textBoxCODIGO.Text))
            {
                MessageBox.Show("Não foi possível salvar as informações comerciais para o cliente selecionado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            AtualizaValorTributo();

            CalcularSaldo();

            // Atualiza o objeto Itens após a atualização de alguns campos da tabela TITMMOVCOMPL
            Item = new New.Models.OrcamentoItens();
            Item.CarregaGridViewItens((int)campoInteiroIDMOV.Get());

            Item.ConfiguraColunasGridView(gvItensMovimento);

            Itens = Item.CarregaItens();

            /*
            for (int i = 0; i < Itens.Count; i++)
            {
                if (ValidaComposicao(Itens[i].IDPRD))
                {
                    Itens = Item.CarregaItens(GetItemComposicao(Itens[i].NSEQITEMMOV));
                }
            }
            */

            List<ItemComposicao> itemCompTemp = new List<ItemComposicao>();
            for (int i = 0; i < Itens.Count; i++)
            {
                if (ValidaComposicao(Convert.ToInt32(Itens[i].IDPRD)))
                {
                    foreach (ItemComposicao iCompTemp in GetItemComposicao(Convert.ToInt32(Itens[i].NSEQITEMMOV)))
                    {
                        itemCompTemp.Add(iCompTemp);
                    }
                }
                else
                {
                    Itens = Item.CarregaItens();
                }
            }

            Itens = Item.CarregaItens(itemCompTemp);

            gcItensMovimento.DataSource = Item.AtualizaGridView(Itens);

            util.SetVisaoUsuario(gvItensMovimento, "ITENS");

            //CarregaOrdernacaoItens();

            ConsultaValoresOrcamento();

            IdMov = (int)campoInteiroIDMOV.Get();
            New.Class.EnviromentHelper.IDMOV = IdMov;

            CarregaCamposDesconto();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            isCancelado = true;
        }

        private void FormOrcamentoCadastro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close"))
            {
                // Usuário optou por fechar o formulário
            }
            else
            {
                // Clicou no botão "X" ou apertou Alt + F4 para fechar o formulário          
                isCancelado = true;
            }
        }

        #region Grid Itens do orçamento

        private void btnNovoItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(campoLookupCODCFO.Get()))
            {
                MessageBox.Show("Selecione um Cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (String.IsNullOrWhiteSpace(campoListaAplicacao.Get()))
            {
                MessageBox.Show("Selecione uma Aplicação.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (String.IsNullOrWhiteSpace(campoLista2.Get()))
            {
                MessageBox.Show("Selecione uma UF.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (String.IsNullOrWhiteSpace(listaContribuinteICMS.Get()))
            {
                MessageBox.Show("Necessário informar o campo Contribuinte ICMS.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                FormOrcamentoItem f = new FormOrcamentoItem();
                f.CODCOLIGADA = AppLib.Context.Empresa;
                f.IDMOV = Convert.ToInt32(campoInteiroIDMOV.Get());
                f.NSEQITEMMOV = 0;
                f.CODCFO = campoLookupCODCFO.Get();
                f.AplicaProd = campoListaAplicacao.Get();
                f.consumidorFinal = cbConsumidorFinal.Checked;
                f.CODETD = campoLista2.Get();
                //f.dataEntrega = (DateTime)campoDataENTREGA.Get();
                f.pedidoCliente = campoTextoNORDEM.Get();
                f.Itens = this.Itens;
                f.descItem = rbItem.Checked;
                f.inserirNovoItem = true;
                f.POSSUIBENEFICIO = cePossuiBeneficio.Checked;

                int NseqPrevio = 0;
                int NumeroSequencial = 0;

                if (gvItensMovimento.RowCount == 0 && Itens.Count == 0)
                {
                    NseqPrevio++;
                    NumeroSequencial++;
                }
                else
                {
                    List<TITMMOV> lista = Itens;

                    NseqPrevio = ProcuraMaiorNumeroSequencial(Itens, "NSEQITMMOV") + 1;
                    NumeroSequencial = ProcuraMaiorNumeroSequencial(Itens, "NUMEROSEQUENCIAL") + 1;

                    for (int i = 0; i <= lista.Count - 1; i++)
                    {
                        if (i == lista.Count - 1)
                        {
                            NumeroSequencial = Convert.ToInt32(lista[i].NUMEROSEQUENCIAL) + 1;
                        }
                    }
                }

                f.Nseq = NseqPrevio;

                f.ShowDialog();

                if (f.acao == AcaoForcada.Salvar)
                {
                    f.xTITMMOV.NSEQITEMMOV = NseqPrevio;

                    f.xTITMMOV.NUMEROSEQUENCIAL = NumeroSequencial;

                    Itens.Add(f.xTITMMOV);
                    CalcularSaldo();

                    New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
                    gcItensMovimento.DataSource = orc.AtualizaGridView(Itens);
                    //gvItensMovimento.BestFitColumns();

                    util.SetVisaoUsuario(gvItensMovimento, "ITENS");
                }

                if (f.acao == AcaoForcada.Excluir)
                {
                    // ignorar o registro novo
                }

                InserirNovo = f.inserirNovoItem;

                if (f.inserirNovoItem)
                {
                    // Chama outro item
                    btnNovoItem_Click(sender, e);
                }
            }
        }

        private void btnEditarItem_Click(object sender, EventArgs e)
        {
            EditarItem(sender, e);
            AtualizaRegistroSelecionado();
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (gvItensMovimento.RowCount > 0)
            {
                if (MessageBox.Show("Deseja excluir o(s) registro(s) selecionado(s)?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                DataTable dtItens = gcItensMovimento.DataSource as DataTable;
                DataRow row = null;
                TITMMOV item = null;

                for (int i = 0; i < gvItensMovimento.SelectedRowsCount; i++)
                {
                    row = gvItensMovimento.GetDataRow(gvItensMovimento.GetSelectedRows()[i]);

                    item = new TITMMOV();
                    item = Itens.Where(x => x.NSEQITEMMOV == Convert.ToInt32(row["NSEQITMMOV"])).First();

                    Itens.Remove(item);
                }
            }

            if (Itens.Count == 0)
            {
                ExcluiuTudo = true;
            }

            // Atualizar grid
            New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
            gcItensMovimento.DataSource = orc.AtualizaGridView(Itens);

            util.SetVisaoUsuario(gvItensMovimento, "ITENS");
            //gvItensMovimento.BestFitColumns();
        }

        private void AtualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
            gcItensMovimento.DataSource = orc.AtualizaGridView(Itens);

            util.SetVisaoUsuario(gvItensMovimento, "ITENS");
        }

        private void configurarColunasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New.Forms.frmSelecaoColunas frm = new New.Forms.frmSelecaoColunas("ITENS");
            frm.ShowDialog();

            // Atualizar grid
            New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
            gcItensMovimento.DataSource = orc.AtualizaGridView(Itens);

            util.SetVisaoUsuario(gvItensMovimento, "ITENS");
        }

        private void salvarConfiguraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE ZVISAOUSUARIO SET VISIVEL = 0 WHERE USUARIO = ? AND GRID = ?", new object[] { AppLib.Context.Usuario, "ITENS" });

            DataTable dtItens = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZVISAOUSUARIO WHERE USUARIO = ? AND GRID = ?", new object[] { AppLib.Context.Usuario, "ITENS" });

            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("Processando");

            for (int i = 0; i < gvItensMovimento.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit ZVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "ZVISAOUSUARIO");

                ZVISAOUSUARIO.Set("GRID", "ITENS");
                ZVISAOUSUARIO.Set("USUARIO", AppLib.Context.Usuario);
                ZVISAOUSUARIO.Set("COLUNA", gvItensMovimento.Columns[i].FieldName);
                ZVISAOUSUARIO.Set("FILTRO", "");
                ZVISAOUSUARIO.Set("LARGURA", gvItensMovimento.Columns[i].Width);
                ZVISAOUSUARIO.Set("VISIVEL", 1);
                ZVISAOUSUARIO.Set("VISIBILIDADE", 1);
                ZVISAOUSUARIO.Set("SEQUENCIA", gvItensMovimento.Columns[i].VisibleIndex);

                if (gvItensMovimento.Columns[i].SortOrder == DevExpress.Data.ColumnSortOrder.Descending)
                {
                    ZVISAOUSUARIO.Set("ORDENACAO", "DESC");
                }
                else if (gvItensMovimento.Columns[i].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
                {
                    ZVISAOUSUARIO.Set("ORDENACAO", "ASC");
                }
                else
                {
                    ZVISAOUSUARIO.Set("ORDENACAO", "");
                }

                ZVISAOUSUARIO.Set("ALINHAMENTO", "E");
                ZVISAOUSUARIO.Set("AGRUPAR", 0);
                ZVISAOUSUARIO.Set("FORMATO", "");

                ZVISAOUSUARIO.Save();
            }

            //for (int i = 0; i < gvItensMovimento.Columns.Count; i++)
            //{
            //    AppLib.ORM.Jit ZVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "ZVISAOUSUARIO");

            //    ZVISAOUSUARIO.Set("GRID", "ITENS");
            //    ZVISAOUSUARIO.Set("USUARIO", AppLib.Context.Usuario);
            //    ZVISAOUSUARIO.Set("COLUNA", gvItensMovimento.Columns[i].FieldName);
            //    ZVISAOUSUARIO.Set("FILTRO", "");         
            //    ZVISAOUSUARIO.Set("LARGURA", gvItensMovimento.Columns[i].Width);

            //    if (gvItensMovimento.Columns[i].Visible == false)
            //    {
            //        int index = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT SEQUENCIA FROM ZVISAOUSUARIO WHERE USUARIO = ? AND GRID = ? AND COLUNA = ?", new object[] { AppLib.Context.Usuario, "ITENS", gvItensMovimento.Columns[i].FieldName }));

            //        ZVISAOUSUARIO.Set("SEQUENCIA", index);
            //        ZVISAOUSUARIO.Set("VISIVEL", 0);
            //        ZVISAOUSUARIO.Set("VISIBILIDADE", 0);
            //    }
            //    else
            //    {
            //        ZVISAOUSUARIO.Set("VISIVEL", 1);
            //        ZVISAOUSUARIO.Set("VISIBILIDADE", 1);
            //        ZVISAOUSUARIO.Set("SEQUENCIA", gvItensMovimento.Columns[i].VisibleIndex);
            //    }

            //    if (gvItensMovimento.Columns[i].SortOrder == DevExpress.Data.ColumnSortOrder.Descending)
            //    {
            //        ZVISAOUSUARIO.Set("ORDENACAO", "DESC");
            //    }
            //    else if (gvItensMovimento.Columns[i].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //    {
            //        ZVISAOUSUARIO.Set("ORDENACAO", "ASC");
            //    }
            //    else
            //    {
            //        ZVISAOUSUARIO.Set("ORDENACAO", "");
            //    }

            //    ZVISAOUSUARIO.Set("ALINHAMENTO", "E");
            //    ZVISAOUSUARIO.Set("AGRUPAR", 0);
            //    ZVISAOUSUARIO.Set("FORMATO", "");

            //    ZVISAOUSUARIO.Save();
            //}

            splashScreenManager1.CloseWaitForm();

            New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
            gcItensMovimento.DataSource = orc.AtualizaGridView(Itens);

            util.SetVisaoUsuario(gvItensMovimento, "ITENS");
        }

        private void renumerarItensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New.Forms.Process.frmRenumerarItens frmRenumerarItens = new New.Forms.Process.frmRenumerarItens();

            DataTable dtItensGrid = gcItensMovimento.DataSource as DataTable;

            frmRenumerarItens.dtItens = dtItensGrid.Copy();
            frmRenumerarItens.ShowDialog();

            if (frmRenumerarItens.dtRenumeracaoItens.Rows.Count > 0)
            {
                // Instância do objeto "item", responsável pelos métodos de manipulação dos itens
                New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();
                Item.EspelhaGridView(frmRenumerarItens.dtRenumeracaoItens);
                Itens = Item.CarregaItens();

                gcItensMovimento.DataSource = Item.AtualizaGridView(Itens);
            }
        }

        private void exportarColunasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New.Forms.Process.frmExportarColunas frmExportarColunas = new New.Forms.Process.frmExportarColunas();

            DataTable dtItensGrid = gcItensMovimento.DataSource as DataTable;

            frmExportarColunas.dtColunasItens = dtItensGrid.Copy();
            frmExportarColunas.ShowDialog();
        }

        private void gvItensMovimento_DoubleClick(object sender, EventArgs e)
        {
            EditarItem(sender, e);
            AtualizaRegistroSelecionado();
        }

        private void gvItensMovimento_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;

            decimal PrecoTabela = 0;
            decimal ValorPintura = 0;
            decimal AjusteValor = 0;
            int? TipoAjuste;
            decimal PrecoUnitario = 0;
            decimal ValorTotal = 0;
            int Quantidade = 0;
            int numeroSequencial = 0;
            int nseqitmmov = 0;
            decimal ValorDesconto = 0;
            decimal ValorDespesa = 0;

            string numPedido = "";
            string historicolongo = "";

            if (CopiandoNumeroPedido)
            {
                view.FocusedColumn.FieldName = "NUMPEDIDO";
            }

            if (IsPrecoUnitarioCalculado == true && IsValorTotalCalculado == true)
            {
                IsPrecoUnitarioCalculado = false;
                IsValorTotalCalculado = false;

                return;
            }

            if (view.GetFocusedValue() != DBNull.Value)
            {
                if (IsPrecoUnitarioCalculado == true)
                {
                    PrecoUnitario = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "PRECOUNITARIO"));
                    Quantidade = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "QUANTIDADE"));

                    ValorTotal = PrecoUnitario * Quantidade;

                    IsValorTotalCalculado = true;
                    // Atribui o valor do Valor Total
                    view.SetRowCellValue(e.RowHandle, "VALORTOTAL", ValorTotal);

                    return;
                }

                if (view.FocusedColumn.ColumnType == view.GetFocusedValue().GetType())
                {
                    // Preço de Tabela
                    if (view.FocusedColumn.FieldName == "PRECOTABELA")
                    {
                        // Atribui os valores para as respectivas variáveis
                        PrecoTabela = Convert.ToDecimal(e.Value);
                        AjusteValor = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "AJUSTEVALOR"));
                        ValorPintura = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "VALORPINTURA"));
                        TipoAjuste = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "TIPOAJUSTEVALOR"));
                        PrecoUnitario = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "PRECOUNITARIO"));
                        ValorTotal = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "VALORTOTAL"));
                        Quantidade = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "QUANTIDADE"));
                        ValorDesconto = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "VALORDESCONTO"));
                        ValorDespesa = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "VALORDESPESA"));

                        if (TipoAjuste != null)
                        {
                            if (TipoAjuste == 1)
                            {
                                // Adição
                                PrecoUnitario = ValorPintura + PrecoTabela - ValorDesconto + ValorDespesa + AjusteValor;
                            }
                            else if (TipoAjuste == 0 && AjusteValor > 0)
                            {
                                // Subtração
                                PrecoUnitario = ValorPintura + PrecoTabela - ValorDesconto + ValorDespesa - AjusteValor;
                            }
                            else
                            {
                                // Apenas realiza o cálculo
                                PrecoUnitario = ValorPintura + PrecoTabela - ValorDesconto + ValorDespesa;

                                ValorTotal = PrecoUnitario * Quantidade;
                            }
                        }
                        else
                        {
                            // Apenas realiza o cálculo
                            PrecoUnitario = ValorPintura + PrecoTabela - ValorDesconto + ValorDespesa;

                            ValorTotal = PrecoUnitario * Quantidade;
                        }

                        IsPrecoUnitarioCalculado = true;

                        // Atribui o valor do Preço Unitário
                        view.SetRowCellValue(e.RowHandle, "PRECOUNITARIO", PrecoUnitario);

                        // Atualizar o valor do objeto PRECOUNITARIO do obejto itens
                        New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
                        DataTable dtItens = gcItensMovimento.DataSource as DataTable;

                        orc.EspelhaGridView(dtItens);

                        List<ItemComposicao> itemCompTemp = new List<ItemComposicao>();
                        for (int i = 0; i < Itens.Count; i++)
                        {
                            if (ValidaComposicao(Itens[i].IDPRD))
                            {
                                foreach (ItemComposicao iCompTemp in GetItemComposicao(Itens[i].NSEQITEMMOV))
                                {
                                    itemCompTemp.Add(iCompTemp);
                                }
                            }
                            else
                            {
                                Itens = orc.CarregaItens();
                            }
                        }

                        Itens = orc.CarregaItens(itemCompTemp);

                        return;
                    }
                    else if (view.FocusedColumn.FieldName == "QUANTIDADE")
                    {
                        // Quantidade
                        PrecoUnitario = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, "PRECOUNITARIO"));
                        Quantidade = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "QUANTIDADE"));
                        nseqitmmov = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "NSEQITMMOV"));

                        ValorTotal = PrecoUnitario * Quantidade;

                        IsPrecoUnitarioCalculado = true;
                        IsValorTotalCalculado = true;
                        // Atribui o valor do Valor Total
                        view.SetRowCellValue(e.RowHandle, "VALORTOTAL", ValorTotal);

                        foreach (TITMMOV item in Itens)
                        {
                            if (item.NSEQITEMMOV == nseqitmmov)
                            {
                                item.PRECOUNITARIO = PrecoUnitario;
                                item.QUANTIDADE = Quantidade;
                                item.VALORTOTAL = ValorTotal;
                            }
                        }

                        New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();
                        gcItensMovimento.DataSource = Item.AtualizaGridView(Itens);
                    }
                    else if (view.FocusedColumn.FieldName == "NUMPEDIDO")
                    {
                        // Número do Pedido
                        numPedido = view.GetRowCellValue(e.RowHandle, "NUMPEDIDO").ToString();
                        nseqitmmov = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "NSEQITMMOV"));

                        IsPrecoUnitarioCalculado = true;
                        IsValorTotalCalculado = true;

                        view.SetRowCellValue(e.RowHandle, "NUMPEDIDO", numPedido);

                        foreach (TITMMOV item in Itens)
                        {
                            if (item.NSEQITEMMOV == nseqitmmov)
                            {
                                item.NUMPEDIDO = numPedido;
                            }
                        }

                        New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();
                        gcItensMovimento.DataSource = Item.AtualizaGridView(Itens);
                    }
                    else if (view.FocusedColumn.FieldName == "NUMITEMPEDIDO")
                    {
                        // Número do Pedido
                        numPedido = view.GetRowCellValue(e.RowHandle, "NUMITEMPEDIDO").ToString();
                        nseqitmmov = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "NSEQITMMOV"));

                        IsPrecoUnitarioCalculado = true;
                        IsValorTotalCalculado = true;

                        view.SetRowCellValue(e.RowHandle, "NUMITEMPEDIDO", numPedido);

                        foreach (TITMMOV item in Itens)
                        {
                            if (item.NSEQITEMMOV == nseqitmmov)
                            {
                                item.NUMITEMPEDIDO = numPedido;
                            }
                        }

                        New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();
                        gcItensMovimento.DataSource = Item.AtualizaGridView(Itens);

                    }
                    else if (view.FocusedColumn.FieldName == "NUMEROSEQUENCIAL")
                    {
                        // Número do Pedido
                        numeroSequencial = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "NUMEROSEQUENCIAL"));
                        nseqitmmov = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "NSEQITMMOV"));

                        //view.SetRowCellValue(e.RowHandle, "NUMEROSEQUENCIAL", numeroSequencial);

                        // Atualizar o valor do objeto PRECOUNITARIO do obejto itens
                        New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
                        DataTable dtItens = gcItensMovimento.DataSource as DataTable;

                        orc.EspelhaGridView(dtItens);

                        TITMMOV itemSelecionado = orc.GetItemBySeq(Itens, nseqitmmov);
                        TITMMOV itemOrigem = orc.GetItemByNumSeq(Itens, numeroSequencial);

                        if (itemOrigem.NSEQITEMMOV > 0)
                        {
                            bool flag = false;
                            List<TITMMOV> itensTemp = new List<TITMMOV>();
                            foreach (TITMMOV item in Itens)
                            {
                                if (item.NSEQITEMMOV == itemSelecionado.NSEQITEMMOV || item.NSEQITEMMOV == itemOrigem.NSEQITEMMOV)
                                {
                                    flag = true;
                                }

                                if (!flag)
                                {
                                    itensTemp.Add(item);
                                }

                                flag = false;
                            }

                            //int numeroSequencialantigo = itemSelecionado.NUMEROSEQUENCIAL;
                            itemSelecionado.NUMEROSEQUENCIAL = numeroSequencial;
                            //itemOrigem.NUMEROSEQUENCIAL = numeroSequencialantigo;
                            //itemOrigem.NUMEROSEQUENCIAL = numeroSequencial;

                            itensTemp.Add(itemSelecionado);
                            itensTemp.Add(itemOrigem);

                            Itens = itensTemp;
                        }
                        else
                        {
                            bool flag = false;
                            List<TITMMOV> itensTemp = new List<TITMMOV>();
                            foreach (TITMMOV item in Itens)
                            {
                                if (item.NSEQITEMMOV == itemSelecionado.NSEQITEMMOV)
                                {
                                    flag = true;
                                }

                                if (!flag)
                                {
                                    itensTemp.Add(item);
                                }

                                flag = false;
                            }

                            itemSelecionado.NUMEROSEQUENCIAL = numeroSequencial;

                            itensTemp.Add(itemSelecionado);

                            Itens = itensTemp;
                        }

                        //New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();
                        //gcItensMovimento.DataSource = Item.AtualizaGridView(Itens);

                        //ValidaSequencialRepetido();
                    }
                    else if (view.FocusedColumn.FieldName == "HISTORICOLONGO")
                    {
                        historicolongo = view.GetRowCellValue(e.RowHandle, "HISTORICOLONGO").ToString();
                        nseqitmmov = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "NSEQITMMOV"));

                        IsPrecoUnitarioCalculado = true;
                        IsValorTotalCalculado = true;

                        view.SetRowCellValue(e.RowHandle, "HISTORICOLONGO", historicolongo);

                        foreach (TITMMOV item in Itens)
                        {
                            if (item.NSEQITEMMOV == nseqitmmov)
                            {
                                item.HISTORICOLONGO = historicolongo;
                            }
                        }

                        New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();
                        gcItensMovimento.DataSource = Item.AtualizaGridView(Itens);
                    }
                }
                else
                {
                    MessageBox.Show("Apenas valores numéricos são aceitos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        #endregion

        #region Métodos
        private bool SetInformacaoComercial(string _infCompl, string _codCfo)
        {
            bool works = true;

            if (campoLookupCODCFO.textBoxCODIGO.Text != "")
            {
                int result = 0;

                result = AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction("UPDATE FCFOCOMPL SET INFCOMERCIAL = ? WHERE CODCFO = ?", new object[] { _infCompl, _codCfo });

                if (result < 0)
                {
                    works = false;
                }

                return works;
            }
            else
            {
                return false;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                MessageBox.Show("What the Ctrl+F?");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AtualizaItem()
        {
            if (Itens == null)
            {
                DataTable dtItens = gcItensMovimento.DataSource as DataTable;

                // Instância do objeto "item", responsável pelos métodos de manipulação dos itens
                New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();

                Item.TabelaItens = Item.EspelhaGridView(dtItens);

                // Carrega os itens no objeto 

                Itens = Item.CarregaItens();

                for (int i = 0; i < Itens.Count; i++)
                {
                    if (ValidaComposicao(Itens[i].IDPRD))
                    {
                        Itens[i].COMPOSICAO = GetItemComposicao(Itens[i].NSEQITEMMOV);
                    }
                }
            }

            //Atualiza a tabela TITMMOV 
            for (int i = 0; i < Itens.Count; i++)
            {
                int result = 0;

                var PrecoUnitario = Itens[i].PRECOUNITARIO.ToString().Replace(',', '.');
                var valorTotal = Itens[i].VALORTOTAL.ToString().Replace(',', '.');

                Itens[i].UNIDADE = ValidaUnidade(Itens[i]);

                result = AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction(@"UPDATE TITMMOV 
                                                                                            SET QUANTIDADE = ?, CODUND = ?, PRECOUNITARIO = ?, VALORTOTALITEM = ?, VALORBRUTOITEM = ? WHERE CODCOLIGADA = ? AND IDMOV = ? AND NSEQITMMOV = ?", new object[] { Itens[i].QUANTIDADE, Itens[i].UNIDADE, PrecoUnitario, valorTotal, valorTotal, AppLib.Context.Empresa, campoInteiroIDMOV.Get(), Itens[i].NSEQITEMMOV });

                if (result > 0)
                {
                    continue;
                }
            }

            //Atualiza a tabela TITMMOVCOMPL
            for (int i = 0; i < Itens.Count; i++)
            {
                int result = 0;

                var AjusteValor = Itens[i].AJUSTEVALOR.ToString().Replace(',', '.');
                var PrecoTabela = Itens[i].PRECOTABELA.ToString().Replace(',', '.');
                var ValorPintura = Itens[i].VALORPINTURA.ToString().Replace(',', '.');
                var Aplicacao = campoListaAplicacao.Get() != Itens[i].APLICACAO.ToString() ? campoListaAplicacao.Get() : Itens[i].APLICACAO.ToString();
                var Distanciamento = Itens[i].DISTANCIAMENTO;

                result = AppLib.Context.poolConnection.Get(this.Conexao).ExecTransaction(@"UPDATE TITMMOVCOMPL 
                                                                                            SET AJUSTEVALOR = ?, TIPOAJUSTEVALOR = ?, PRECOTABELA = ?, VALORPINTURA = ?, APLICPROD = ?, DISTANCIAMENTO = ? WHERE CODCOLIGADA = ? AND IDMOV = ? AND NSEQITMMOV = ?", new object[] { AjusteValor, Itens[i].TIPOAJUSTEVALOR, PrecoTabela, ValorPintura, Aplicacao, Distanciamento, AppLib.Context.Empresa, campoInteiroIDMOV.Get(), Itens[i].NSEQITEMMOV });

                if (result > 0)
                {
                    continue;
                }
            }
        }

        private void CalcularSaldo()
        {
            try
            {
                List<TITMMOV> NewItens = new List<TITMMOV>();

                foreach (TITMMOV item in Itens)
                {
                    SaldoItem saldo = ItensOrcamento.getSaldo(item.IDPRD);

                    item.SALDO_FISICO = saldo.SALDO_FISICO;
                    item.SALDO_PEDIDO = saldo.SALDO_PEDIDO;
                    item.SALDO_DISPONIVEL = saldo.SALDO_DISPONIVEL;

                    NewItens.Add(item);
                }

                Itens = NewItens;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AtualizaValorTributo()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(campoInteiroIDMOV.Get().ToString()))
                {
                    string sql = String.Format(@"SELECT	isnull(SUM(VALOR),0) as 'IPI'
                                                FROM	TTRBMOV
                                                WHERE	TTRBMOV.CODCOLIGADA = 1
                                                AND	TTRBMOV.IDMOV = {0}
                                                AND	TTRBMOV.CODTRB = 'IPI'", campoInteiroIDMOV.Get());
                    decimalIPI.Set(Convert.ToDecimal(MetodosSQL.GetField(sql, "IPI")));

                    sql = String.Format(@"SELECT	isnull(SUM(VALOR),0) as 'PIS'
                                                FROM	TTRBMOV
                                                WHERE	TTRBMOV.CODCOLIGADA = 1
                                                AND	TTRBMOV.IDMOV = {0}
                                                AND	TTRBMOV.CODTRB = 'PIS'", campoInteiroIDMOV.Get());
                    decimalPIS.Set(Convert.ToDecimal(MetodosSQL.GetField(sql, "PIS")));

                    sql = String.Format(@"SELECT	isnull(SUM(VALOR),0) as 'COFINS'
                                                FROM	TTRBMOV
                                                WHERE	TTRBMOV.CODCOLIGADA = 1
                                                AND	TTRBMOV.IDMOV = {0}
                                                AND	TTRBMOV.CODTRB = 'COFINS'", campoInteiroIDMOV.Get());
                    decimalCOFINS.Set(Convert.ToDecimal(MetodosSQL.GetField(sql, "COFINS")));

                    sql = String.Format(@"SELECT	isnull(SUM(VALOR),0) as 'ICMS'
                                                FROM	TTRBMOV
                                                WHERE	TTRBMOV.CODCOLIGADA = 1
                                                AND	TTRBMOV.IDMOV = {0}
                                                AND	TTRBMOV.CODTRB = 'ICMS'", campoInteiroIDMOV.Get());
                    decimalICMS.Set(Convert.ToDecimal(MetodosSQL.GetField(sql, "ICMS")));

                    sql = String.Format(@"SELECT 
	                                            CASE 
		                                            WHEN (ISNULL(ST.VALOR, 0) - ISNULL(ICMS.VALOR, 0)) < 0 
		                                            THEN 0 
		                                            ELSE (ISNULL(ST.VALOR, 0) - ISNULL(ICMS.VALOR, 0)) 
	                                            END as 'ICMSST'
                                            FROM TMOV,

                                            (SELECT	SUM (TTRBMOV.VALOR) AS VALOR
                                                 FROM	TTRBMOV
                                                 WHERE	TTRBMOV.CODCOLIGADA = 1
                                                 AND	TTRBMOV.IDMOV = {0}
                                                 AND	TTRBMOV.CODTRB = 'ICMSST') ST,

                                            (SELECT	SUM (TTRBMOV.VALOR) AS VALOR
                                                 FROM	TTRBMOV
                                                 WHERE	TTRBMOV.CODCOLIGADA = 1
                                                 AND	TTRBMOV.IDMOV = {0}
                                                 AND	TTRBMOV.CODTRB = 'ICMS') ICMS

                                            WHERE CODCOLIGADA = 1
                                            AND   IDMOV = {0}", campoInteiroIDMOV.Get());
                    decimalICMSST.Set(Convert.ToDecimal(MetodosSQL.GetField(sql, "ICMSST")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetConsulta()
        {
            clLocalEstoque.ColunaCodigo = "CODLOC";
            clLocalEstoque.ColunaDescricao = "NOME";
            string sql = String.Format(@"(select CODLOC, NOME from TLOC 
                                                            where CODFILIAL = '1'
                                                            and CODCOLIGADA = '{1}') W", AppLib.Context.Filial, AppLib.Context.Empresa);
            clLocalEstoque.ColunaTabela = sql;

            clLocalEstoque.Set("001");
            clLocalEstoque.textBox1_Leave(null, null);

            clSerie.ColunaCodigo = "SERIE";
            clSerie.ColunaDescricao = "DESCRICAO";
            clSerie.ColunaTabela = String.Format(@"(SELECT TTMV.CODTMV,
                                                           'Orçamento de Venda' as 'DESCRICAO', 
                                                           TTMVSERIE.SERIE 
                                                    FROM TTMV, TTMVSERIE 
                                                    WHERE TTMV.CODCOLIGADA = TTMVSERIE.CODCOLIGADA 
                                                    AND TTMV.CODCOLIGADA = 1 
                                                    AND TTMV.CODTMV = TTMVSERIE.CODTMV 
                                                    AND TTMV.CODTMV IN ('2.1.05') 
                                                    AND TTMVSERIE.PRINCIPAL = 1
                                                    UNION
                                                    SELECT TTMV.CODTMV,
                                                           'Pedido de Venda' as 'DESCRICAO', 
                                                           TTMVSERIE.SERIE 
                                                    FROM TTMV, TTMVSERIE 
                                                    WHERE TTMV.CODCOLIGADA = TTMVSERIE.CODCOLIGADA 
                                                    AND TTMV.CODCOLIGADA = 1 
                                                    AND TTMV.CODTMV = TTMVSERIE.CODTMV 
                                                    AND TTMV.CODTMV IN ('2.1.10') 
                                                    AND TTMVSERIE.PRINCIPAL = 1
                                                    UNION
                                                    SELECT TTMV.CODTMV,
                                                           'Cotações Diversas' as 'DESCRICAO', 
                                                           TTMVSERIE.SERIE 
                                                    FROM TTMV, TTMVSERIE 
                                                    WHERE TTMV.CODCOLIGADA = TTMVSERIE.CODCOLIGADA 
                                                    AND TTMV.CODCOLIGADA = 1 
                                                    AND TTMV.CODTMV = TTMVSERIE.CODTMV 
                                                    AND TTMV.CODTMV IN ('2.1.15') 
                                                    AND TTMVSERIE.PRINCIPAL = 1) W", AppLib.Context.Empresa);

            clSerie.Set(CODSERIE);

            clVendedorInterno.ColunaCodigo = "CODVEN";
            clVendedorInterno.ColunaDescricao = "NOME";
            clVendedorInterno.ColunaTabela = String.Format(@"(SELECT 
	                                                                CODVEN,
	                                                                NOME
                                                                FROM 
	                                                                TVEN 
                                                                WHERE 
	                                                                CODCOLIGADA = {0}
                                                                and INATIVO = 0) W", AppLib.Context.Empresa);
            if (!(this.Acao == AppLib.Global.Types.Acao.Editar))
            {
                sql = String.Format(@"SELECT 
	                                        CODVEN 
                                        FROM 
	                                        TVEN 
                                        WHERE 
	                                        CODCOLIGADA = 1 
                                        AND CODPESSOA IN (
				                                        SELECT 
					                                        CODIGO 
				                                        FROM 
					                                        PPESSOA 
				                                        WHERE 
					                                        CODCOLIGADA = {0} 
					                                        AND CODUSUARIO = '{1}')", AppLib.Context.Empresa, AppLib.Context.Usuario);

                clVendedorInterno.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODVEN");

            }

            clVendedorInterno.textBox1_Leave(null, null);

            clCondigaoPagamento.ColunaCodigo = "CODCPG";
            clCondigaoPagamento.ColunaDescricao = "NOME";
            clCondigaoPagamento.ColunaTabela = String.Format(@"(SELECT 
	                                                                CODCPG, 
	                                                                NOME 
                                                                FROM 
	                                                                TCPG 
                                                                WHERE 
	                                                                CODCOLIGADA = {0} 
                                                                AND PLANOVENDA = 1 
                                                                AND INATIVO = 0 OR INATIVO IS NULL) W", AppLib.Context.Empresa);
        }

        private void RestaurarValorOriginal(int idmov)
        {
            AtualizaItem();
        }

        private void AtualizaDescontoItem(int idmov)
        {
            try
            {
                string sql = "";

                sql = String.Format(@"select TTM.NSEQITMMOV, IDPRD, PRECOUNITARIO, QUANTIDADE, (QUANTIDADE * PRECOUNITARIO) as 'VALORTOTALITEM', TTMC.VALORDESCORIGINAL, TTMC.PRECOTABELA from TITMMOV TTM

                                                inner join TITMMOVCOMPL TTMC
                                                on TTMC.IDMOV = TTM.IDMOV
                                                and TTMC.CODCOLIGADA = TTM.CODCOLIGADA
                                                and TTMC.NSEQITMMOV = TTM.NSEQITMMOV

                                                where TTM.CODCOLIGADA = 1 
                                                and TTM.IDMOV = {0}", idmov);



                using (DataTable dt = MetodosSQL.GetDT(sql))
                {
                    List<string> update = new List<string>();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (rbItem.Checked && Convert.ToDecimal(tbDescontoPercentual.EditValue) > 0)
                        {
                            decimal precoNovo = 0;

                            precoNovo = ((decimal)row["VALORDESCORIGINAL"] * ((100 - (decimal)tbDescontoPercentual.EditValue) / 100));

                            update.Add(string.Format(@"UPDATE TITMMOVCOMPL
                                                    set PRECOTABELA = {0}
                                                    where CODCOLIGADA = 1 
                                                    and IDMOV= {1}
                                                    and NSEQITMMOV = {2}", precoNovo.ToString().Replace(',', '.'), idmov, (int)row["NSEQITMMOV"]));
                        }
                    }

                    MetodosSQL.ExecMultiple(update);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ProcuraMaiorNumeroSequencial(List<TITMMOV> itens, string tipo)
        {
            if (itens.Count == 0)
            {
                throw new InvalidOperationException("Lista vazia");
            }

            int sequencialMaximo = int.MinValue;

            foreach (TITMMOV item in itens)
            {
                if (tipo == "NSEQITMMOV")
                {
                    if (Convert.ToInt32(item.NSEQITEMMOV) > sequencialMaximo)
                    {
                        sequencialMaximo = Convert.ToInt32(item.NSEQITEMMOV);
                    }
                }

                if (tipo == "NUMEROSEQUENCIAL")
                {
                    if (Convert.ToInt32(item.NUMEROSEQUENCIAL) > sequencialMaximo)
                    {
                        sequencialMaximo = Convert.ToInt32(item.NUMEROSEQUENCIAL);
                    }
                }
            }

            return sequencialMaximo;
        }

        private string ValidaUnidade(TITMMOV item)
        {
            string unidade = AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField("", @"SELECT CODUNDVENDA FROM TPRODUTODEF WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, item.IDPRD }).ToString();

            return unidade;
        }

        private bool IsIDMovValido()
        {
            bool idmov = Convert.ToBoolean(AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField("", @"SELECT COUNT(CODCOLIGADA) FROM TITMMOV WHERE IDMOV = ?", new object[] { campoInteiroIDMOV.Get() }));

            return idmov;
        }

        private void ConsultaValoresOrcamento()
        {
            DataTable dtvalores = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(@"SELECT 
	                                                                                            VALOROUTROSORIG,
	                                                                                            VALOROUTROSORIG,
	                                                                                            VALORLIQUIDOORIG
                                                                                            FROM 
	                                                                                            TMOV 
                                                                                            WHERE 
	                                                                                            CODCOLIGADA = ? 
                                                                                            AND IDMOV = ?", new object[] { AppLib.Context.Empresa, (int)campoInteiroIDMOV.Get() });

            tbValorBruto.EditValue = dtvalores.Rows[0]["VALOROUTROSORIG"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dtvalores.Rows[0]["VALOROUTROSORIG"]);

            tbValorOutros.EditValue = dtvalores.Rows[0]["VALOROUTROSORIG"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dtvalores.Rows[0]["VALOROUTROSORIG"]);

            tbValorLiquido.EditValue = dtvalores.Rows[0]["VALORLIQUIDOORIG"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dtvalores.Rows[0]["VALORLIQUIDOORIG"]);
        }

        private void EditarItem(object sender, EventArgs e)
        {
            int NumeroSequencial = 0;
            int indice = 0;
            TITMMOV reg = new TITMMOV();
            System.Data.DataRow dr = gvItensMovimento.GetDataRow(gvItensMovimento.GetSelectedRows()[0]);

            reg.CODCOLIGADA = AppLib.Context.Empresa;
            reg.IDMOV = Convert.ToInt32(campoInteiroIDMOV.Get());
            reg.NSEQITEMMOV = Convert.ToInt32(dr["NSEQITMMOV"]);
            reg.NUMEROSEQUENCIAL = Convert.ToInt32(dr["NUMEROSEQUENCIAL"]);
            reg.AJUSTEVALOR = dr["AJUSTEVALOR"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["AJUSTEVALOR"]);
            reg.ALIQUOTAIPI = dr["ALIQUOTAIPI"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ALIQUOTAIPI"]);
            reg.APLICACAO = dr["APLICACAO"].ToString();
            reg.CODAUXILIAR = dr["CODAUXILIAR"].ToString();
            reg.CODIGOPRD = dr["CODIGOPRD"].ToString();
            reg.IDPRD = Convert.ToInt32(dr["IDPRD"]);

            if (this.Acao == AppLib.Global.Types.Acao.Editar)
            {
                if (ValidaComposicao(reg.IDPRD))
                {
                    for (int i = 0; i < Itens.Count; i++)
                    {
                        if (ValidaComposicao(Itens[i].IDPRD))
                        {
                            reg.COMPOSICAO = GetItemComposicao(reg.NSEQITEMMOV);
                        }
                    }
                }
            }
            else
            {
                if (CopiaDeMovimento == true)
                {
                    for (int i = 0; i < Itens.Count; i++)
                    {
                        if (ValidaComposicao(Itens[i].IDPRD))
                        {
                            reg.COMPOSICAO = GetItemComposicao(reg.NSEQITEMMOV);
                        }
                    }
                }
                else
                {
                    if (ValidaComposicao(reg.IDPRD))
                    {
                        for (int i = 0; i < Itens.Count; i++)
                        {
                            if (ValidaComposicao(Itens[i].IDPRD))
                            {
                                if (Itens[i].COMPOSICAO != null)
                                {
                                    if (Itens[i].COMPOSICAO.Count > 0)
                                    {
                                        reg.COMPOSICAO = Itens[i].COMPOSICAO;
                                    }
                                    else
                                    {
                                        reg.COMPOSICAO = GetItemComposicao(reg.NSEQITEMMOV);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            reg.CORPINTURA = dr["CORPINTURA"].ToString();

            if (dr["DATAENTREGA"] != null && !string.IsNullOrEmpty(dr["DATAENTREGA"].ToString()))
            {
                reg.DATAENTREGA = Convert.ToDateTime(dr["DATAENTREGA"]);
            }

            reg.HISTORICOLONGO = dr["HISTORICOLONGO"].ToString();
            reg.IDNAT = Convert.ToInt32(dr["IDNAT"]);
            reg.IDPRD = Convert.ToInt32(dr["IDPRD"]);
            reg.NUMEROCCF = dr["NUMEROCCF"].ToString();
            reg.NUMEROCFOP = dr["NUMEROCFOP"].ToString();
            reg.NUMITEMPEDIDO = dr["NUMITEMPEDIDO"].ToString();
            reg.NUMPEDIDO = dr["NUMPEDIDO"].ToString();
            reg.PRECOTABELA = Convert.ToDecimal(dr["PRECOTABELA"]);
            reg.PRECOUNITARIO = Convert.ToDecimal(dr["PRECOUNITARIO"]);
            reg.PRECOUNITARIOSELEC = dr["PRECOUNITARIOSELEC"].ToString();
            reg.PRODUTO = dr["PRODUTO"].ToString();
            reg.QUANTIDADE = Convert.ToInt32(dr["QUANTIDADE"]);
            reg.SALDO_DISPONIVEL = dr["SALDO_DISPONIVEL"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SALDO_DISPONIVEL"]);
            reg.SALDO_FISICO = dr["SALDO_FISICO"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SALDO_FISICO"]);
            reg.SALDO_PEDIDO = dr["SALDO_PEDIDO"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SALDO_PEDIDO"]);
            reg.TIPOAJUSTEVALOR = Convert.ToInt32(dr["TIPOAJUSTEVALOR"]);
            reg.TIPOINOX = dr["TIPOINOX"].ToString();
            reg.TIPOPINTURA = dr["TIPOPINTURA"].ToString();
            reg.UNIDADE = dr["UNIDADE"].ToString();
            reg.VALORDESCORIGINAL = dr["VALORDESCORIGINAL"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["VALORDESCORIGINAL"]);
            reg.VALORDESPORIGINAL = dr["VALORDESPORIGINAL"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["VALORDESPORIGINAL"]);
            reg.VALORPINTURA = dr["VALORPINTURA"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["VALORPINTURA"]);
            reg.VALORTOTAL = dr["VALORTOTAL"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["VALORTOTAL"]);

            FormOrcamentoItem f = new FormOrcamentoItem();

            indice = gvItensMovimento.GetDataSourceRowIndex(gvItensMovimento.GetSelectedRows()[0]);
            f.CODCOLIGADA = AppLib.Context.Empresa;
            f.IDMOV = CopiaDeMovimento == true ? IDMOVCOPIADO : Convert.ToInt32(campoInteiroIDMOV.Get());
            f.tipoVenda = campoLista1.Get();
            f.edita = true;
            f.CODCFO = campoLookupCODCFO.Get();
            f.CODETD = campoLista2.Get();
            f.AplicaProd = campoListaAplicacao.Get();
            f.xTITMMOV = reg;
            f.AtualizarForm();
            f.NSEQITEMMOV = reg.NSEQITEMMOV;
            f.consumidorFinal = cbConsumidorFinal.Checked;
            f.descItem = rbItem.Checked;
            f.POSSUIBENEFICIO = cePossuiBeneficio.Checked;

            if (CODSERIE == "PVP")
            {
                f.Text = "Cadastro de Itens do Pedido";
            }
            else if (CODSERIE == "COT")
            {
                f.Text = "Cadastro de Itens de Cotações Diversas";
            }

            New.Class.EnviromentHelper.IDMOV = campoInteiroIDMOV.Get();
            New.Class.EnviromentHelper.NSEQITEMMOV = Convert.ToInt32(reg.NSEQITEMMOV);
            indexRegistroSelecionado = gvItensMovimento.FocusedRowHandle;

            NumeroSequencial = reg.NUMEROSEQUENCIAL;

            this.Acao = AppLib.Global.Types.Acao.Editar;
            f.ShowDialog();

            if (f.acao == AcaoForcada.Salvar)
            {
                f.xTITMMOV.NUMEROSEQUENCIAL = NumeroSequencial;

                if (f.xTITMMOV.NSEQITEMMOV == 0)
                {
                    f.xTITMMOV.NSEQITEMMOV = New.Class.EnviromentHelper.NSEQITEMMOV;
                }

                New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
                DataTable dt = gcItensMovimento.DataSource as DataTable;

                gcItensMovimento.DataSource = orc.EspelhaGridView(dt);

                //Itens = orc.CarregaItens();
                List<TITMMOV> itemTemp = new List<TITMMOV>();

                itemTemp.Add(f.xTITMMOV);
                foreach (TITMMOV titmmov in Itens)
                {
                    if (titmmov.NSEQITEMMOV != f.xTITMMOV.NSEQITEMMOV)
                        itemTemp.Add(titmmov);
                }

                Itens = itemTemp;

                var item = f.xTITMMOV;

                //Itens[indexRegistroSelecionado] = item;

                //Itens.RemoveAll(x => x.NSEQITEMMOV == New.Class.EnviromentHelper.NSEQITEMMOV.ToString());

                //Itens.Add(f.xTITMMOV);

                CalcularSaldo();

                // Atualizar grid
                gcItensMovimento.DataSource = orc.AtualizaGridView(Itens);

                //util.GetVisaoUsuario(gvItensMovimento, "ITENS");
                //gvItensMovimento.BestFitColumns();
            }

            if (f.acao == AcaoForcada.Excluir)
            {
                Itens.Remove(reg);

                if (Itens.Count == 0)
                {
                    ExcluiuTudo = true;
                }

                // Atualizar grid
                New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
                gcItensMovimento.DataSource = orc.AtualizaGridView(Itens);

                util.SetVisaoUsuario(gvItensMovimento, "ITENS");
                //gvItensMovimento.BestFitColumns();
            }

            if (f.inserirNovoItem)
            {
                btnEditarItem_Click(sender, e);
            }
        }

        private void CarregaCamposDesconto()
        {
            DataTable dtCamposValoresOrcamento = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(@"SELECT 
                                                                                                                PERCENTUALFRETE, 
                                                                                                                VALORFRETE, 
                                                                                                                PERCENTUALDESP, 
                                                                                                                VALORDESP, 
                                                                                                                PERCENTUALDESC, 
                                                                                                                VALORDESC 
                                                                                                                FROM TMOV  
                                                                                                                WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Empresa });

            DataTable dtCamposValoresItensOrcamento = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(@"SELECT TIPODESC, VALORDESCRAT, VALORDESPRAT
                                                                                                                    FROM TMOVCOMPL
                                                                                                                    WHERE IDMOV = ?  AND CODCOLIGADA = ?", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Empresa });
            if (dtCamposValoresOrcamento.Rows.Count > 0)
            {
                // Frete
                tbPercentualFrete.EditValue = Convert.ToDecimal(dtCamposValoresOrcamento.Rows[0]["PERCENTUALFRETE"]);
                tbValorFrete.EditValue = Convert.ToDecimal(dtCamposValoresOrcamento.Rows[0]["VALORFRETE"]);

                // Acréscimo
                tbAcrescimoPercentual.EditValue = Convert.ToDecimal(dtCamposValoresOrcamento.Rows[0]["PERCENTUALDESP"]);
                tbAcrescimoDesp.EditValue = Convert.ToDecimal(dtCamposValoresOrcamento.Rows[0]["VALORDESP"]);

                // Desconto
                tbDescontoPercentual.EditValue = Convert.ToDecimal(dtCamposValoresOrcamento.Rows[0]["PERCENTUALDESC"]);
                tbValorDesconto.EditValue = Convert.ToDecimal(dtCamposValoresOrcamento.Rows[0]["VALORDESC"]);
            }

            if (dtCamposValoresItensOrcamento.Rows.Count > 0)
            {
                if (dtCamposValoresItensOrcamento.Rows[0]["TIPODESC"] != DBNull.Value)
                {
                    // Tipo de Desconto 
                    if (Convert.ToInt32(dtCamposValoresItensOrcamento.Rows[0]["TIPODESC"]) == 0)
                    {
                        // Geral
                        rbGeral.Checked = true;

                        if (Convert.ToDecimal(dtCamposValoresItensOrcamento.Rows[0]["VALORDESCRAT"]) > 0)
                        {
                            tbDescontoPercentual.EditValue = Convert.ToDecimal(dtCamposValoresItensOrcamento.Rows[0]["VALORDESCRAT"]);
                        }
                    }
                    else
                    {
                        // Item 
                        rbItem.Checked = true;

                        tbDescontoPercentual.EditValue = Convert.ToDecimal(dtCamposValoresItensOrcamento.Rows[0]["VALORDESCRAT"]);
                    }
                }

                // Acréscimo
                if (Convert.ToDecimal(dtCamposValoresItensOrcamento.Rows[0]["VALORDESPRAT"]) > 0)
                {
                    tbAcrescimoPercentual.EditValue = Convert.ToDecimal(dtCamposValoresItensOrcamento.Rows[0]["VALORDESPRAT"]);
                }
            }
        }

        public void UpdateUltimoPreco()
        {
            try
            {
                if (campoInteiroIDMOV.Get() > 0)
                {
                    if (MetodosSQL.ExisteMovimento(1, (int)campoInteiroIDMOV.Get()))
                    {
                        string sql = String.Format(@"update TMOVCOMPL
                                            set RECMODIFIEDON = getdate(),
											    RECMODIFIEDBY = '{0}',
                                                ULTIMORECAL = getdate()
                                            where CODCOLIGADA = 1
                                            and IDMOV = {1}", AppLib.Context.Usuario, campoInteiroIDMOV.Get());

                        MetodosSQL.ExecQuery(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCODTMV()
        {
            try
            {
                if (CODSERIE != null)
                {
                    if (CODSERIE == "ORC")
                    {
                        this.Text = "Cadastro de Orçamento";
                        ceSemImposto.Visible = true;
                        ceSemImposto.Enabled = true;
                        cePossuiBeneficio.Visible = true;
                        cePossuiBeneficio.Enabled = true;
                        return "2.1.05";
                    }
                    else if (CODSERIE == "PVP")
                    {
                        campoLista1.Visible = false;
                        label2.Visible = false;

                        ceSemImposto.Checked = false;
                        cePossuiBeneficio.Checked = false;
                        this.Text = "Cadastro de Pedido";
                        return "2.1.10";
                    }
                    else if (CODSERIE == "COT")
                    {
                        campoLista1.Visible = false;
                        label2.Visible = false;

                        ceSemImposto.Visible = true;
                        ceSemImposto.Enabled = true;

                        cePossuiBeneficio.Checked = false;
                        this.Text = "Cadastro de Cotações Diversas";
                        return "2.1.15";
                    }
                    else
                    {
                        throw new Exception("Serie incompativel");
                    }
                }
                else
                {
                    // Formulário veio através do processo de Rastrear Movimento
                    string codMovimento = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT CODTMV
                                                                                                        FROM TMOV 
                                                                                                        WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { (int)campoInteiroIDMOV.Get(), AppLib.Context.Empresa }).ToString();

                    switch (codMovimento)
                    {
                        case "2.1.05":
                            this.Text = "Cadastro de Orçamento";
                            ceSemImposto.Visible = true;
                            ceSemImposto.Enabled = true;
                            cePossuiBeneficio.Visible = true;
                            cePossuiBeneficio.Enabled = true;
                            CODSERIE = "ORC";
                            return "2.1.05";
                        case "2.1.10":
                            campoLista1.Visible = false;
                            label2.Visible = false;
                            ceSemImposto.Checked = false;
                            cePossuiBeneficio.Checked = false;
                            this.Text = "Cadastro de Pedido";
                            CODSERIE = "PVP";
                            return "2.1.10";
                        case "2.1.15":
                            campoLista1.Visible = false;
                            label2.Visible = false;
                            ceSemImposto.Visible = true;
                            ceSemImposto.Enabled = true;
                            cePossuiBeneficio.Checked = false;
                            CODSERIE = "COT";
                            this.Text = "Cadastro de Cotações Diversas";
                            return "2.1.15";
                        default:
                            return "";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("[Erro ao definir CODTMV] " + ex.Message);
            }
        }

        public void RecalcularPreco(object sender, EventArgs e)
        {
            try
            {
                foreach (TITMMOV item in Itens)
                {
                    decimal vl = CalculoPreco.CacularPreco(item.IDPRD.ToString(), item.TIPOINOX, "", "").PRECO;

                    if (vl != 0)
                    {
                        item.PRECOUNITARIO = vl;
                    }

                    List<ItemComposicao> IComp = item.COMPOSICAO;

                    foreach (ItemComposicao comp in IComp)
                    {
                        vl = CalculoPreco.CacularPreco(comp.IDPRD.ToString(), item.TIPOINOX, "", "").PRECO;
                        if (vl != 0)
                        {
                            comp.PRECOUNITARIO = vl;
                        }
                    }

                    item.COMPOSICAO = IComp;
                }

                UpdateUltimoPreco();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ItemComposicao> GetItemComposicao(int nSeqItem)
        {
            string Comando = String.Format(@"select * from ZORCAMENTOITEMCOMPOSTO 

                                                        inner join TPRODUTO TP
                                                        on TP.IDPRD = ZORCAMENTOITEMCOMPOSTO.IDPRD

                                                        where CODCOLIGADA = 1 and CODCOLPRD = 1 and IDMOV = '{0}' and NSEQ = '{1}'",
                            CopiaDeMovimento == true ? IDMOVCOPIADO : campoInteiroIDMOV.Get(), nSeqItem);

            DataTable itemComposicao = MetodosSQL.GetDT(Comando);
            List<ItemComposicao> it = new List<ItemComposicao>();

            // Verifica itens 
            if (Itens != null)
            {
                for (int i = 0; i < Itens.Count; i++)
                {
                    if (Itens[i].COMPOSICAO != null)
                    {
                        if (Itens[i].COMPOSICAO.Count > 0)
                        {
                            if (Itens[i].COMPOSICAO[0].NSEQ == nSeqItem)
                            {
                                return Itens[i].COMPOSICAO;
                            }
                        }
                    }
                }
            }

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
                    itemComposto.TOTAL = (decimal)itc["TOTAL"];

                    it.Add(itemComposto);
                }
            }
            else
            {
                for (int i = 0; i < Itens.Count; i++)
                {
                    if (Itens[i].COMPOSICAO != null)
                    {
                        if (Itens[i].COMPOSICAO.Count > 0 && Itens[i].NSEQITEMMOV == nSeqItem || Convert.ToInt32(Itens[i].NSEQITEMMOV) == Convert.ToInt32(nSeqItem))
                        {
                            foreach (var item in Itens[i].COMPOSICAO)
                            {
                                ItemComposicao itemComposto = new ItemComposicao();
                                itemComposto.CODCOLIGADA = AppLib.Context.Empresa;
                                itemComposto.CODFILIAL = item.CODFILIAL;

                                if (this.Acao == AppLib.Global.Types.Acao.Editar)
                                {
                                    itemComposto.IDMOV = Convert.ToInt32(campoInteiroIDMOV.Get());
                                }

                                itemComposto.CODIGOPRD = item.CODIGOPRD;
                                itemComposto.CODIGOAUXILIAR = item.CODIGOAUXILIAR;
                                itemComposto.IDPRD = item.IDPRD;
                                itemComposto.NOMEFANTASIA = item.NOMEFANTASIA;
                                itemComposto.NSEQ = item.NSEQ;
                                itemComposto.QUANTIDADE = item.QUANTIDADE;
                                itemComposto.PRECOUNITARIO = item.PRECOUNITARIO;
                                itemComposto.TOTAL = item.TOTAL;

                                it.Add(itemComposto);
                            }
                        }
                    }
                }
            }

            return it;
        }

        private bool ItemCompostoExists(string idMov, int Nseq, int IDPRD)
        {
            bool exists = false;

            if (Convert.ToInt32(AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(-1, @"SELECT NSEQ FROM ZORCAMENTOITEMCOMPOSTO WHERE IDMOV = ? AND NSEQ = ? AND IDPRD = ?", new object[] { idMov, Nseq, IDPRD })) > 0)
            {
                exists = true;
            }

            return exists;
        }

        private void VerificaDesconto()
        {
            decimal PrecoTabela = 0;
            decimal ValorPintura = 0;
            decimal AjusteValor = 0;
            int? TipoAjuste;
            decimal PrecoUnitario = 0;
            decimal ValorTotal = 0;
            decimal Quantidade = 0;
            decimal ValorDesconto = 0;
            decimal ValorDespesa = 0;

            New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();

            Item.CarregaGridViewItens((int)campoInteiroIDMOV.Get());

            // Seleciona a grid atual e atribui ao objeto datatable
            DataTable dtItens = gcItensMovimento.DataSource as DataTable;

            for (int i = 0; i < dtItens.Rows.Count; i++)
            {
                // Atribui os valores para as respectivas variáveis
                PrecoTabela = Convert.ToDecimal(dtItens.Rows[i]["PRECOTABELA"]);
                AjusteValor = Convert.ToDecimal(dtItens.Rows[i]["AJUSTEVALOR"]);
                ValorPintura = Convert.ToDecimal(dtItens.Rows[i]["VALORPINTURA"]);
                TipoAjuste = Convert.ToInt32(dtItens.Rows[i]["TIPOAJUSTEVALOR"]);
                PrecoUnitario = Convert.ToDecimal(dtItens.Rows[i]["PRECOUNITARIO"]);
                ValorTotal = Convert.ToDecimal(dtItens.Rows[i]["VALORTOTAL"]);
                Quantidade = Convert.ToDecimal(dtItens.Rows[i]["QUANTIDADE"]);
                ValorDesconto = Convert.ToDecimal(dtItens.Rows[i]["VALORDESCONTO"]);
                ValorDespesa = Convert.ToDecimal(dtItens.Rows[i]["VALORDESPESA"]);

                // Recalcula os valores
                //PrecoTabela = PrecoTabela - (PrecoTabela * (valorPercentual / 100));

                if (TipoAjuste != null)
                {
                    if (TipoAjuste == 1)
                    {
                        // Adição
                        PrecoUnitario = ValorPintura + PrecoTabela + AjusteValor;
                    }
                    else if (TipoAjuste == 0 && AjusteValor > 0)
                    {
                        // Subtração
                        PrecoUnitario = ValorPintura + PrecoTabela - AjusteValor;
                    }
                    else
                    {
                        // Apenas realiza o cálculo
                        PrecoUnitario = ValorPintura + PrecoTabela;

                        ValorTotal = PrecoUnitario * Quantidade;
                    }
                }
                else
                {
                    // Apenas realiza o cálculo
                    PrecoUnitario = ValorPintura + PrecoTabela;

                    ValorTotal = PrecoUnitario * Quantidade;
                }

                dtItens.Rows[i]["PRECOTABELA"] = PrecoTabela;
                dtItens.Rows[i]["PRECOUNITARIO"] = PrecoUnitario;
                dtItens.Rows[i]["VALORTOTAL"] = ValorTotal;
            }

            gcItensMovimento.DataSource = Item.EspelhaGridView(dtItens);

            util.SetVisaoUsuario(gvItensMovimento, "ITENS");
            //gvItensMovimento.BestFitColumns();
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

        private int GetPrazo(string prazoFabricacao)
        {
            int prazo = 0;

            switch (prazoFabricacao)
            {
                case "00":
                    prazo = 0;
                    break;
                case "01":
                    prazo = 0;
                    break;
                case "02":
                    prazo = 1;
                    break;
                case "03":
                    prazo = 3;
                    break;
                case "04":
                    prazo = 5;
                    break;
                case "05":
                    prazo = 7;
                    break;
                case "06":
                    prazo = 10;
                    break;
                case "07":
                    prazo = 12;
                    break;
                case "08":
                    prazo = 15;
                    break;
                case "09":
                    prazo = 20;
                    break;
                case "99":
                    prazo = 0;
                    break;
                default:
                    prazo = 0;
                    break;
            }

            return prazo;
        }

        private void CarregaCreditoCliente(string codigoCliente)
        {
            if (codigoCliente == "00002" || codigoCliente == "00632")
            {
                // Limite de Crédito
                tbLimiteCredito.Text = "--";

                // Valor em Aberto
                tbValorAberto.Text = "--";

                // Valor em Aberto
                tbPedidosAFaturar.Text = "--";

                // Limite Disponível
                tbLimiteDisponivel.Text = "--";
            }
            else
            {
                // Limite de Crédito
                tbLimiteCredito.Text = Convert.ToDecimal(AppLib.Context.poolConnection.Get().ExecGetField(0.00, @"SELECT 
	                                                                                                                FCFO.LIMITECREDITO LIMITE 
                                                                                                               FROM 
	                                                                                                               FCFO 
                                                                                                               WHERE 
	                                                                                                               (CODCOLIGADA = ? OR CODCOLIGADA = 0) 
                                                                                                               AND CODCFO = ?", new object[] { AppLib.Context.Empresa, codigoCliente })).ToString("n2");

                // Valor em Aberto
                tbValorAberto.Text = Convert.ToDecimal(AppLib.Context.poolConnection.Get().ExecGetField(0.00, @"SELECT 
	                                                                                                             SUM(VALORORIGINAL - VALORBAIXADO) FINANCEIRO 
                                                                                                             FROM 
	                                                                                                             FLAN 
                                                                                                             WHERE 
	                                                                                                             CODCOLIGADA = ?
                                                                                                             AND CODCFO = ?
                                                                                                             AND  PAGREC = 1 
                                                                                                             AND STATUSLAN IN (0,4) 
                                                                                                             AND CODTDO = 'NF-e'", new object[] { AppLib.Context.Empresa, codigoCliente })).ToString("n2");

                // Pedidos a Faturar
                tbPedidosAFaturar.Text = Convert.ToDecimal(AppLib.Context.poolConnection.Get().ExecGetField(0.00, @"SELECT 
	                                                                                                            SUM(VALORORIGINAL - VALORBAIXADO) PEDIDOS 
                                                                                                            FROM 
	                                                                                                            FLAN 
                                                                                                            WHERE 
	                                                                                                            CODCOLIGADA = ?
                                                                                                            AND CODCFO = ? 
                                                                                                            AND PAGREC = 1 
                                                                                                            AND STATUSLAN IN (0,4) 
                                                                                                            AND CODTDO = 'PRVV'", new object[] { AppLib.Context.Empresa, codigoCliente })).ToString("n2");

                // Limite Disponível
                if (Convert.ToDecimal(tbLimiteCredito.Text) == 0)
                {
                    tbLimiteDisponivel.Text = "--";
                }
                else
                {
                    tbLimiteDisponivel.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    tbLimiteDisponivel.Properties.Mask.EditMask = "n2";
                    tbLimiteDisponivel.Properties.Mask.UseMaskAsDisplayFormat = true;

                    tbLimiteDisponivel.Text = (Convert.ToDecimal(tbLimiteCredito.Text) - Convert.ToDecimal(tbValorAberto.Text) - Convert.ToDecimal(tbPedidosAFaturar.Text)).ToString("n2");
                }
            }

            CustomizaCoresLimitesCredito();
            ValidaLimiteDisponivel(codigoCliente);
        }

        private void ValidaLimiteDisponivel(string codigoCliente)
        {
            if (!string.IsNullOrEmpty(codigoCliente))
            {
                if (codigoCliente != "00002" || codigoCliente != "00632")
                {
                    if (tbLimiteDisponivel.Text != "--")
                    {
                        if (Convert.ToDecimal(tbLimiteCredito.Text) <= 0)
                        {
                            XtraMessageBox.Show("Atenção\r\nO cliente selecionado não possui limite disponível.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private bool ValidaSequencialRepetido()
        {
            bool valida = true;

            DataTable dtRenumeracaoItens = gcItensMovimento.DataSource as DataTable;

            int[] numerosSequenciais = new int[dtRenumeracaoItens.Rows.Count];

            for (int i = 0; i < dtRenumeracaoItens.Rows.Count; i++)
            {
                numerosSequenciais[i] = Convert.ToInt32(dtRenumeracaoItens.Rows[i]["NUMEROSEQUENCIAL"]);
            }

            var groups = numerosSequenciais.GroupBy(v => v);

            foreach (var g in groups)
            {
                if (g.Count() > 1)
                {
                    XtraMessageBox.Show("Item com sequencial repetido, favor verificar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valida = false;
                    return valida;
                }
            }

            return valida;
        }

        private void CustomizaCoresLimitesCredito()
        {
            // Limite de Crédito
            tbLimiteCredito.ForeColor = Color.Blue;

            // Valor em Aberto
            tbValorAberto.ForeColor = Color.Red;

            // Pedidos a Faturar
            tbPedidosAFaturar.ForeColor = Color.Red;

            // Limite Disponível
            if (tbLimiteDisponivel.Text != "--")
            {
                if (Convert.ToDecimal(tbLimiteDisponivel.Text) >= 0)
                {
                    tbLimiteDisponivel.ForeColor = Color.Green;
                }
                else
                {
                    tbLimiteDisponivel.ForeColor = Color.Red;
                }
            }
            else
            {
                tbLimiteDisponivel.ForeColor = Color.Red;
            }
        }

        private bool ValidaParametrosUsuario()
        {
            bool valida = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT TABPORENTER FROM ZUSUARIOPARAM WHERE CODCOLIGADA = ? AND CODUSUARIO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Usuario }));

            return valida;
        }

        private void TabulacaoPorEnter(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (ValidaParametrosUsuario())
                {
                    //switch (ActiveControl.Name)
                    //{
                    //    case "campoLookupCODCFO":
                    //        campoLista2.Focus();
                    //        break;
                    //    case "campoLista2":
                    //        listaContribuinteICMS.Focus();
                    //        break;
                    //    case "campoLista1":
                    //        ActiveControl = campoListaAplicacao;
                    //        campoListaAplicacao.Focus();
                    //        campoListaAplicacao.Select();

                    //        e.IsInputKey = true;
                    //        break;
                    //    default:
                    //        break;
                    //}

                    //SelectNextControl(GetNextControl((Control)sender, true), true, true, true, true);

                    //if ((GetNextControl((Control)sender, true).GetType() == typeof(System.Windows.Forms.ComboBox)) || (GetNextControl((Control)sender, true).GetType() == typeof(AppLib.Windows.CampoLista)))
                    //{
                    //    MessageBox.Show(ActiveControl.Name);
                    //    MessageBox.Show(GetNextControl((Control)sender, true).Name);
                    //    SelectNextControl(ActiveControl, true, true, true, true);
                    //    MessageBox.Show(ActiveControl.Name);

                    //    campoListaAplicacao.comboBox1.Focus();
                    //}
                    //else
                    //{
                    //    SelectNextControl(GetNextControl((Control)sender, true), true, true, true, true);
                    //}
                }
                else
                {
                    GetNextControl((Control)sender, false).Select();
                }
            }
            else if (e.KeyData == Keys.Tab)
            {
                if (!ValidaParametrosUsuario())
                {
                    //SelectNextControl((Control)sender, true, true, true, false);
                }
                else
                {
                    SelectNextControl((Control)sender, false, true, false, false);
                }
            }
        }

        private void CarregaOrdernacaoItens()
        {
            if (New.Class.EnviromentHelper.ItensOrdenados != null)
            {
                // Instância do objeto "item", responsável pelos métodos de manipulação dos itens
                New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();

                DataTable dt = gcItensMovimento.DataSource as DataTable;

                for (int i = 0; i < New.Class.EnviromentHelper.ItensOrdenados.Rows.Count; i++)
                {
                    dt.Rows[i]["NUMEROSEQUENCIAL"] = New.Class.EnviromentHelper.ItensOrdenados.Rows[i]["NUMEROSEQUENCIAL"];
                }

                Item.EspelhaGridView(dt);
                Itens = Item.CarregaItens();

                gcItensMovimento.DataSource = Item.AtualizaGridView(Itens);

                util.SetVisaoUsuario(gvItensMovimento, "ITENS");
            }
        }

        private void AtualizaRegistroSelecionado()
        {
            gvItensMovimento.FocusedRowHandle = indexRegistroSelecionado;
            gvItensMovimento.SelectRow(indexRegistroSelecionado);
        }

        #endregion

        #region Campos Lookup

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            FormClienteVisao f = new FormClienteVisao();
            f.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            return f.MostrarLookup(campoLookupCODCFO, "  AND PAGREC IN (1, 3) AND ATIVO = 1 ");
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
SELECT CODTRA, NOME, NOMEFANTASIA, CGC, CODETD, CIDADE, BAIRRO
FROM TTRA
WHERE CODCOLIGADA = ?
  AND INATIVO = 0";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODTRA, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private void campoLookupCODCFO_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM ZVWFCFO WHERE CODCOLIGADA in (0,?) AND CODCFO = ?";
            campoLookupCODCFO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODCFO.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODCFO.Get() }).ToString();
        }

        private void campoLookupCODRPR_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM TRPR WHERE CODCOLIGADA = ? AND CODRPR = ?";
            campoLookupCODRPR.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODRPR.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODRPR.Get() }).ToString();
        }

        private void campoLookupCODTRA_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM TTRA WHERE CODCOLIGADA = ? AND CODTRA = ?";
            campoLookupCODTRA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODTRA.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODTRA.Get() }).ToString();
        }

        #endregion

        #region Validação do Registro

        public Boolean Validar()
        {
            string sql = $@"SELECT count(1) as 'TOTAL' FROM KITMMOVITEMORDEM WHERE CODCOLIGADA = {AppLib.Context.Empresa} AND IDMOV = {campoInteiroIDMOV.Get()}";
            if (CODSERIE == "PVP" && int.Parse(MetodosSQL.GetField(sql, "TOTAL")) > 0)
            {
                MessageBox.Show("Não é possivel alterar pedido com ordem associada.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (CopiaDeMovimento == false)
            {
                if (status == "F")
                {
                    MessageBox.Show("Não é possivel salvar o orçamento com status Faturado.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (String.IsNullOrWhiteSpace(campoLookupCODRPR.Get()))
            {
                MessageBox.Show("Campo Representante é obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoInteiroIDMOV.Get() > 0)
            {
                if (MetodosSQL.ExisteMovimento(1, (int)campoInteiroIDMOV.Get()))
                {
                    sql = String.Format(@"select case when dateadd(DAY, (select distinct PRAZORECALPRECO from ZPARAMFATURE), ULTIMORECAL) < GETDATE() then 'SIM' else 'NAO' end as 'FORAVIGENCIA'
                                                        from TMOVCOMPL
                                                        where CODCOLIGADA = 1
                                                        and IDMOV = {0}", campoInteiroIDMOV.Get());

                    if (MetodosSQL.GetField(sql, "FORAVIGENCIA") == "SIM" && CODSERIE == "ORC")
                    {
                        DialogResult r = MessageBox.Show("Preços estão fora da vigencia e o orçamento não poderá ser salvo. Deseja recalcular?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r == DialogResult.Yes)
                        {
                            RecalcularPreco(null, null);
                        }
                        else
                        {

                        }
                    }
                }
            }

            if (campoLookupCODCFO.Get() == null)
            {
                MessageBox.Show("Campo Cliente obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dteDataEmissao.Value == null)
            {
                MessageBox.Show("Data de Emissão obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cbPrazoFabricacao.SelectedValue == null)
            {
                MessageBox.Show("Prazo de Fabricação obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoInteiroPRAZOENTREGA.Get() < 0)
            {
                MessageBox.Show("Data de Entrega deve ser maior ou igual a Data de Emissão.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoLista1.Get() == null && CODSERIE == "ORC")
            {
                MessageBox.Show("Tipo de Venda obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

            if (campoLookupCODTRA.Get() == null)
            {
                MessageBox.Show("Campo Transportadora obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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
                if (Itens[i].PRECOTABELA == 0)
                {
                    MessageBox.Show("ATENÇÃO\r\nExistem itens com o Preço de Tabela igual à zero. Favor revisar e tentar novamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (gvItensMovimento.RowCount > 0)
            {
                // Instância do objeto "item", responsável pelos métodos de manipulação dos itens
                New.Models.OrcamentoItens Item = new New.Models.OrcamentoItens();

                if (this.Acao == AppLib.Global.Types.Acao.Editar)
                {
                    DataTable dtItens = gcItensMovimento.DataSource as DataTable;
                    Item.EspelhaGridView(dtItens);

                    // Carrega os itens no objeto 
                    if (CopiaDeMovimento == true)
                    {
                        /*
                        for (int i = 0; i < Itens.Count; i++)
                        {
                            if (ValidaComposicao(Itens[i].IDPRD))
                            {
                                Itens = Item.CarregaItens(GetItemComposicao(Itens[i].NSEQITEMMOV));
                            }
                            else
                            {
                                Itens = Item.CarregaItens();
                            }
                        }
                        */
                    }
                }
                else
                {
                    DataTable dtItens = gcItensMovimento.DataSource as DataTable;
                    Item.EspelhaGridView(dtItens);

                    for (int i = 0; i < Itens.Count; i++)
                    {
                        if (Itens[i].CODAUXILIAR.Contains("DP800") || (Itens[i].CODAUXILIAR.Contains("DP801") || (Itens[i].CODAUXILIAR.Contains("DP802") || (Itens[i].CODAUXILIAR.Contains("DP803") || (Itens[i].CODAUXILIAR.Contains("DP804") || (Itens[i].CODAUXILIAR.Contains("DP805")))))))
                        {
                            Itens = Item.CarregaItens(GetItemComposicao(Itens[i].NSEQITEMMOV), Itens);
                        }
                    }
                }
            }

            if (Itens.Count <= 0)
            {
                MessageBox.Show("Não é permitido gravar movimentos sem itens!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int cont = 0;

            if (cont > 1)
            {
                MessageBox.Show("Existe mais de um item com o padrão de revenda.");
                return false;
            }

            if (campoLista1.Get().ToString() == "" && CODSERIE == "ORC")
            {
                MessageBox.Show("Selecione uma opção para o campo Tipo de Venda.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (campoListaAplicacao.Get().ToString() == "")
            {
                MessageBox.Show("Selecione uma opção para o campo Aplicação.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbPrazoFabricacao.Text == " - Selecione")
            {
                MessageBox.Show("Selecione uma opção para o campo Prazo de Fabricação.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (clCondigaoPagamento.Get() == null)
            {
                MessageBox.Show("Selecione uma opção para o campo Condição de Pagamento.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string ItemInativo = String.Empty;
            foreach (TITMMOV x in Itens)
            {
                sql = String.Format(@"select INATIVO from TPRODUTO where CODCOLPRD = '1' and IDPRD = '{0}'", x.IDPRD);
                bool Inativo = MetodosSQL.GetField(sql, "INATIVO") == "1";

                if (Inativo)
                {
                    ItemInativo += String.Format(@"{0} - {1} {2}", x.CODIGOPRD, x.PRODUTO, Environment.NewLine);
                }
            }

            if (!String.IsNullOrWhiteSpace(ItemInativo))
            {
                string msg = String.Format(@"Existen itens inativos inseridos no orçamento. {0}{1}{2}", Environment.NewLine, Environment.NewLine, ItemInativo);
                MessageBox.Show(msg, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (row != null)
            {
                if (row["STATUS"].ToString() == "FATURADO")
                {
                    MessageBox.Show("Não é permitido editar orçamento 'faturado'.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            string EditaOrc = MetodosSQL.GetField("select EDITAORCOUTVEN from ZPARAMFATURE where CODCOLIGADA = 1", "EDITAORCOUTVEN");

            if (this.Acao == AppLib.Global.Types.Acao.Editar && AppLib.Context.Usuario != clVendedorInterno.Get() && EditaOrc == "0")
            {
                MessageBox.Show("Não é permitido alterações em orçamentos de outros vendedores!");
                return false;
            }

            if (campoLookupCODCFO.textBoxCODIGO.Text == "00002" | campoLookupCODCFO.textBoxCODIGO.Text == "00632" && string.IsNullOrEmpty(txtNomeCliente.textEdit1.Text))
            {
                MessageBox.Show("Favor informar o nome complementar.");
                return false;
            }

            if (!ValidaSequencialRepetido())
            {
                return false;
            }

            sql = String.Format(@"select case when COUNT(1) > 0 then 'NAO' else 'SIM' end as 'COPIAVEL' from TITMMOV TTM

                                             inner join TPRODUTO TP
                                             on TP.IDPRD = TTM.IDPRD

                                             where TP.INATIVO = 1
                                             and TTM.IDMOV = {0}", IDMOVCOPIADO);

            bool Copiavel = MetodosSQL.GetField(sql, "COPIAVEL") == "SIM";

            if (!Copiavel)
            {
                MessageBox.Show("Este movimento possui itens inativos e por isso não pode ser copiado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        private bool FormOrcamentoCadastro_Validar(object sender, EventArgs e)
        {
            return this.Validar();
        }

        private void FormOrcamentoCadastro_Preparar(object sender, EventArgs e)
        {
            //  Bloco de código para realizar alguma validação antes de salvar caso necessário
        }

        private void FormOrcamentoCadastro_Excluir2(object sender, EventArgs e)
        {
            MessageBox.Show("Operação não permitida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private Boolean FormOrcamentoCadastro_Salvar2(object sender, EventArgs e)
        {
            string sPoint = string.Empty;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                Guid oGuid = Guid.NewGuid();
                string sSql = string.Empty;

                AppInterop.MovMovimentoPar movimento = new AppInterop.MovMovimentoPar();

                sPoint = "Camada: [Client], Campo: [Código da Coligada]";
                movimento.CodColigada = AppLib.Context.Empresa;

                sPoint = "Camada: [Client], Campo: [Identificador do Movimento]";
                if (campoInteiroIDMOV.Get() != null)
                {
                    movimento.IdMov = int.Parse(campoInteiroIDMOV.Get().ToString());
                }

                sPoint = "Camada: [Client], Campo: [GuidId]";
                movimento.GuidId = oGuid.ToString();
                sPoint = "Camada: [Client], Campo: [Tipo de Movimento]";
                movimento.CodTMv = GetCODTMV();
                sPoint = "Camada: [Client], Campo: [Filial]";
                movimento.CodFilial = 1;
                sPoint = "Camada: [Client], Campo: [Local de Estoque]";
                movimento.CodLoc = "001";
                sPoint = "Camada: [Client], Campo: [Coligada do Cliente]";
                movimento.CodColCfo = 0;
                sPoint = "Camada: [Client], Campo: [Código do Cliente]";
                movimento.CodCfo = campoLookupCODCFO.Get();
                sPoint = "Camada: [Client], Campo: [Nome do Cliente]";
                movimento.NOMECLIENTEORC = txtNomeCliente.Get();
                sPoint = "Camada: [Client], Campo: [Contato Com]";
                movimento.CONTATOCOM = txtCONTATOCOM.Get();
                sPoint = "Camada: [Client], Campo: [Telefone Com]";
                movimento.TELEFONECOM = txtTelefoneCom.Get();
                sPoint = "Camada: [Client], Campo: [Email Com]";
                movimento.EMAILCOM = txtEMAILCOM.Get();
                sPoint = "Camada: [Client], Campo: [Local de estoque]";
                movimento.CodLoc = clLocalEstoque.Get();
                sPoint = "Camada: [Client], Campo: [Aplicação]";
                movimento.APLICPROD = campoListaAplicacao.Get();
                sPoint = "Camada: [Client], Campo: [Série]";
                movimento.Serie = clSerie.Get();
                sPoint = "Camada: [Client], Campo: [Vendedor interno]";
                movimento.CodVen1 = clVendedorInterno.Get();
                sPoint = "Camada: [Client], Campo: [Condição de pagamento]";
                movimento.CodCondicaoPagamento = clCondigaoPagamento.Get();

                // Função para calcular as datas de entrega e emissão 

                sPoint = "Camada: [Client], Campo: [Data de Emissão]";
                movimento.DataEmissao = dteDataEmissao.Value;
                sPoint = "Camada: [Client], Campo: [Data de Entrega]";
                movimento.DataEntrega = campoDataENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [Prazo de Entrega]";
                movimento.PrazoEntrega = campoInteiroPRAZOENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [Representante]";
                movimento.CodRepresentante = campoLookupCODRPR.Get();
                sPoint = "Camada: [Client], Campo: [Frete]";
                movimento.FreteCIFouFOB = int.Parse(campoListaFRETECIFOUFOB.Get());
                sPoint = "Camada: [Client], Campo: [Transportadora]";
                movimento.CodTra = campoLookupCODTRA.Get();
                sPoint = "Camada: [Client], Campo: [Tabelas de Faturamento]";
                movimento.CodTb1Opcional = clTabelaFaturamento.Get();
                sPoint = "Camada: [Client], Campo: [UF Cliente]";
                movimento.UFORCAMENTO = campoLista2.Get();
                sPoint = "Camada: [Client], Campo: [Consumidor Final]";
                movimento.NCONTRIB = int.Parse(listaContribuinteICMS.Get());

                sPoint = "Camada: [Client], Campo: [Sem Impostos]";
                movimento.SEMIMPOSTOS = ceSemImposto.Checked ? 1 : 0;
                sPoint = "Camada: [Client], Campo: [Possui Beneficio]";
                movimento.POSSUIBENEFICIO = cePossuiBeneficio.Checked ? 1 : 0;

                decimal valorOrcamento = 0;

                if (rbGeral.Checked)
                {
                    sPoint = "Camada: [Client], Campo: [Percentual de Desconto]";
                    movimento.PercentualDesc = decimal.Parse(tbDescontoPercentual.Text);
                    sPoint = "Camada: [Client], Campo: [Valor de Desconto]";

                    decimal valorTotalOriginal = 0;
                    decimal precoOriginal = 0;

                    for (int i = 0; i < Itens.Count; i++)
                    {
                        // Adição
                        if (Itens[i].TIPOAJUSTEVALOR == 1)
                        {
                            precoOriginal = (decimal)Itens[i].PRECOTABELA + Itens[i].VALORPINTURA + Itens[i].AJUSTEVALOR;
                        }

                        // Substração
                        if (Itens[i].TIPOAJUSTEVALOR == 0 && Itens[i].AJUSTEVALOR > 0)
                        {
                            precoOriginal = (decimal)Itens[i].PRECOTABELA + Itens[i].VALORPINTURA - Itens[i].AJUSTEVALOR;
                        }

                        // Não teve alteração nenhuma
                        if (Itens[i].TIPOAJUSTEVALOR == 0 && Itens[i].AJUSTEVALOR == 0)
                        {
                            precoOriginal = (decimal)Itens[i].PRECOTABELA + Itens[i].VALORPINTURA;
                        }

                        // Multiplica o preço Original com a quantidade requerida de itens
                        precoOriginal = precoOriginal * Itens[i].QUANTIDADE;

                        // Valida o Preço do item
                        if (precoOriginal == 0)
                        {
                            MessageBox.Show("ATENÇÃO\r\nExistem itens com o preço igual à zero. Favor revisar e tentar novamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Cursor = Cursors.Default;

                            return false;
                        }

                        valorTotalOriginal += (decimal)precoOriginal;

                        valorOrcamento = valorTotalOriginal;
                    }

                    if (Convert.ToDecimal(tbDescontoPercentual.EditValue) > 0)
                    {
                        movimento.ValorDesc = ((valorTotalOriginal * Convert.ToDecimal(tbDescontoPercentual.EditValue)) / 100);
                    }
                    else
                    {
                        movimento.ValorDesc = Convert.ToDecimal(tbValorDesconto.EditValue);
                    }

                    sPoint = "Camada: [Client], Campo: [Valor de Desconto Rateado]";
                    movimento.VALORDESCRAT = 0;
                    sPoint = "Camada: [Client], Campo: [Tipo de Desconto]";
                    movimento.TIPODESC = 0;
                }
                else
                {
                    sPoint = "Camada: [Client], Campo: [Percentual de Desconto]";
                    //movimento.PercentualDesc = (decimal)campoDecimalPERCENTUALDESC.Get();
                    movimento.PercentualDesc = 0;
                    sPoint = "Camada: [Client], Campo: [Valor de Desconto]";
                    movimento.ValorDesc = 0;

                    sPoint = "Camada: [Client], Campo: [Valor de Desconto Rateado]";
                    movimento.VALORDESCRAT = decimal.Parse(tbDescontoPercentual.Text);
                    sPoint = "Camada: [Client], Campo: [Tipo de Desconto]";
                    movimento.TIPODESC = 1;
                }

                sPoint = "Camada: [Client], Campo: [Percentual de Frete]";

                if (Convert.ToDecimal(tbValorFrete.Text) > 0)
                {
                    movimento.PercentualFrete = 0;
                }
                else
                {
                    if (Convert.ToDecimal(tbPercentualFrete.Text) > 0)
                    {
                        movimento.PercentualFrete = decimal.Parse(tbPercentualFrete.Text);
                    }
                    else
                    {
                        movimento.PercentualFrete = 0;
                    }
                }

                sPoint = "Camada: [Client], Campo: [Valor de Frete]";

                decimal valorTotalOriginalFrete = 0;
                decimal precoOriginalFrete = 0;

                for (int i = 0; i < Itens.Count; i++)
                {
                    // Adição
                    if (Itens[i].TIPOAJUSTEVALOR == 1)
                    {
                        precoOriginalFrete = (decimal)Itens[i].PRECOTABELA + Itens[i].VALORPINTURA + Itens[i].AJUSTEVALOR;
                    }

                    // Substração
                    if (Itens[i].TIPOAJUSTEVALOR == 0 && Itens[i].AJUSTEVALOR > 0)
                    {
                        precoOriginalFrete = (decimal)Itens[i].PRECOTABELA + Itens[i].VALORPINTURA - Itens[i].AJUSTEVALOR;
                    }

                    // Não teve alteração nenhuma
                    if (Itens[i].TIPOAJUSTEVALOR == 0 && Itens[i].AJUSTEVALOR == 0)
                    {
                        precoOriginalFrete = (decimal)Itens[i].PRECOTABELA + Itens[i].VALORPINTURA;
                    }

                    // Multiplica o preço Original com a quantidade requerida de itens
                    precoOriginalFrete = precoOriginalFrete * Itens[i].QUANTIDADE;

                    // Valida o Preço do item
                    if (precoOriginalFrete == 0)
                    {
                        MessageBox.Show("ATENÇÃO\r\nExistem itens com o preço igual à zero. Favor revisar e tentar novamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;

                        return false;
                    }

                    valorTotalOriginalFrete += (decimal)precoOriginalFrete;

                    valorOrcamento = valorTotalOriginalFrete;
                }

                if (movimento.PercentualFrete > 0)
                {
                    movimento.ValorFrete = ((valorOrcamento * Convert.ToDecimal(tbPercentualFrete.EditValue)) / 100);
                }
                else
                {
                    movimento.ValorFrete = decimal.Parse(tbValorFrete.Text);
                }

                sPoint = "Camada: [Client], Campo: [Percentual de Despesa]";

                movimento.VALORDESPRAT = decimal.Parse(tbAcrescimoPercentual.Text);
                movimento.PercentualDesp = 0;
                sPoint = "Camada: [Client], Campo: [Valor de Despesa]";
                movimento.ValorDesp = 0;

                sPoint = "Camada: [Client], Campo: [Observação]";
                movimento.Observacao = campoTextoOBSERVACAO.Get();
                sPoint = "Camada: [Client], Campo: [Campo Livre 1]";
                movimento.CampoLivre1 = campoTextoCAMPOLIVRE1.Get();
                sPoint = "Camada: [Client], Campo: [Histórico Longo]";
                movimento.HistoricoLongo = campoMemoHISTORICOLONGO.Get();
                sPoint = "Camada: [Client], Campo: [Numero da Ordem]";
                movimento.NumeroOrdem = campoTextoNORDEM.Get();
                sPoint = "Camada: [Client], Campo: [Segundo Numero]";
                movimento.SegundoNumero = campoTexto1.Get();
                sPoint = "Camada: [Client], Campo: [Usuário]";
                movimento.CodUsuario = AppLib.Context.Usuario;
                sPoint = "Camada: [Client], Campo: [Usuário Logado]";
                movimento.CodUsuarioLogado = AppLib.Context.Usuario;
                sPoint = "Camada: [Client], Campo: [Data de Criação]";
                movimento.DataCriacao = DateTime.Now;
                sPoint = "Camada: [Client], Campo: [Data do Movimento]";
                movimento.DataMovimento = DateTime.Now;

                sPoint = string.Concat("Identificador do Movimento do Cliente]");
                sSql = @"SELECT MAX(IDHISTORICO) IDMOVCFO FROM FCFOHISTORICO WHERE CODCOLIGADA = ? AND CODCFO = ?";
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { movimento.CodColCfo, movimento.CodCfo });

                if (dt.Rows.Count > 0)
                    movimento.IdMovCfo = Convert.ToInt32(dt.Rows[0]["IDMOVCFO"].ToString());
                else
                    movimento.IdMovCfo = null;

                //Campos Complementares
                sPoint = "Camada: [Client], Campo: [Compl Tipo Venda]";
                movimento.TIPOVENDA = campoLista1.Get();

                List<String> queryItemComposto = new List<string>();

                queryItemComposto.Add(String.Format(@"delete from ZORCAMENTOITEMCOMPOSTO where CODCOLIGADA = 1 and IDMOV = '{0}'", String.IsNullOrWhiteSpace(campoInteiroIDMOV.Get().ToString()) ? oGuid.ToString() : campoInteiroIDMOV.Get().ToString()));

                List<AppInterop.MovMovimentoItemPar> lMovMovimentoItemPar = new List<AppInterop.MovMovimentoItemPar>();

                List<TITMMOV> ItensTemp = Itens;

                for (int i = 0; i < Itens.Count; i++)
                {
                    TITMMOV item = new TITMMOV();

                    New.Models.OrcamentoItens OrcItem = new New.Models.OrcamentoItens();
                    DataTable dtItens = gcItensMovimento.DataSource as DataTable;
                    OrcItem.TabelaItens = OrcItem.EspelhaGridView(dtItens);

                    //Itens = OrcItem.CarregaItens();

                    item = Itens[i];

                    item.UNIDADE = ValidaUnidade(item);

                    /*
                    if (this.Acao == AppLib.Global.Types.Acao.Editar)
                    {
                        item.COMPOSICAO = OrcItem.GetComposicaoInserida(Convert.ToInt32(item.NSEQITEMMOV), Convert.ToInt32(campoInteiroIDMOV.Get()));
                    }
                    */

                    AppInterop.MovMovimentoItemPar itmmov = new AppInterop.MovMovimentoItemPar();

                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Código da Coligada");
                    itmmov.CodColigada = movimento.CodColigada;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Seq.");
                    itmmov.NSeqItmMov = Convert.ToInt32(item.NSEQITEMMOV);
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Número Seq.");
                    itmmov.NumeroSequencial = Convert.ToInt32(item.NUMEROSEQUENCIAL);
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Identificador do Produto");
                    itmmov.IdPrd = item.IDPRD;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Unidade de Medida");
                    itmmov.CodUnd = item.UNIDADE;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Quantidade");
                    itmmov.Quantidade = item.QUANTIDADE;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Preço Unitário");
                    itmmov.PrecoUnitario = item.PRECOUNITARIO;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Tabela de preço");
                    itmmov.PRECOUNITARIOSELEC = item.PRECOUNITARIOSELEC;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Aliquota do IPI");
                    itmmov.AliquotaIPI = item.ALIQUOTAIPI;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Histórico Longo");
                    itmmov.HistoricoLongo = item.HISTORICOLONGO;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " IDNAT");

                    if (cePossuiBeneficio.Checked)
                    {
                        itmmov.IDNAT = item.IDNAT;
                    }
                    else
                    {
                        itmmov.IDNAT = item.IDNAT == 0 ? ItensOrcamento.getIDNAT(ItensOrcamento.getCFOP(campoLista2.Get(), item.APLICACAO, item.CODIGOPRD, cbConsumidorFinal.Checked ? "1" : "0")) : item.IDNAT;
                    }

                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Aplicação do produto");

                    if (campoListaAplicacao.Get() != item.APLICACAO)
                    {
                        itmmov.APLICAPROD = campoListaAplicacao.Get();
                    }
                    else
                    {
                        itmmov.APLICAPROD = item.APLICACAO;
                    }

                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Numero Pedido");
                    itmmov.NUMPEDIDO = item.NUMPEDIDO;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Numero Item Pedido");
                    itmmov.NUMITEMPEDIDO = item.NUMITEMPEDIDO;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Tipo Inox");
                    itmmov.TIPOINOX = item.TIPOINOX;

                    //Campos Complementares

                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Data Entrega");
                    //itmmov.DATAENTREGA = item.DATAENTREGA;
                    itmmov.DATAENTREGA = campoDataENTREGA.Get();
                    sPoint = string.Concat("Item ", (i + 1), " Registra ZTITMMOVORCTEMP");
                    sSql = @"INSERT INTO ZTITMMOVORCTEMP(ROWID, CODCOLIGADA, NSEQITMMOV, NUMEROSEQUENCIAL, IDPRD, IDPRDCOMPOSTO, CODUND, QUANTIDADE, PRECOUNITARIO, ALIQUOTAIPI, 
                                HISTORICOLONGO, PRODPRICIPAL, SEQUENCIAL, TIPOMAT, PRDPADRAO, OBSPRDPADRAO, PRDREVENDA, PRECOUNITARIOSELEC, DATAENTREGA, IDNAT, APLICPROD, NUMPEDIDO, NUMITEMPEDIDO, TIPO, VALORPINTURA, TIPOPINTURA, VALORORIGINAL, CORPINTURA, PRECOTABELA) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    Object NIP;

                    if (String.IsNullOrWhiteSpace(itmmov.NUMITEMPEDIDO))
                    {
                        NIP = DBNull.Value;
                    }
                    else
                    {
                        NIP = itmmov.NUMITEMPEDIDO;
                    }

                    if (rbItem.Checked)
                    {
                        decimal PrecoTabela = 0;

                        decimal ValorDescOriginal = 0;
                        decimal ValorDespOriginal = 0;

                        // Adição
                        if (item.TIPOAJUSTEVALOR == 1)
                        {
                            PrecoTabela = (decimal)item.PRECOTABELA + (decimal)item.VALORPINTURA + (decimal)item.AJUSTEVALOR;
                        }

                        // Substração
                        if (item.TIPOAJUSTEVALOR == 0 && item.AJUSTEVALOR > 0)
                        {
                            PrecoTabela = (decimal)item.PRECOTABELA + (decimal)item.VALORPINTURA - (decimal)item.AJUSTEVALOR;
                        }

                        // Não teve alteração nenhuma
                        if (item.TIPOAJUSTEVALOR == 0 && item.AJUSTEVALOR == 0)
                        {
                            PrecoTabela = (decimal)item.PRECOTABELA + (decimal)item.VALORPINTURA;
                        }

                        // Valida o Preço do item
                        if (PrecoTabela == 0)
                        {
                            MessageBox.Show("ATENÇÃO\r\nExistem itens com o preço igual à zero. Favor revisar e tentar novamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Cursor = Cursors.Default;

                            return false;
                        }

                        itmmov.PRECOTABELA = PrecoTabela;

                        ValorDescOriginal = PrecoTabela * (Convert.ToDecimal(tbDescontoPercentual.EditValue) / 100);
                        ValorDespOriginal = PrecoTabela * (Convert.ToDecimal(tbAcrescimoPercentual.EditValue) / 100);

                        itmmov.PrecoUnitario = PrecoTabela - ValorDescOriginal + ValorDespOriginal;
                    }
                    else
                    {
                        decimal PrecoTabela = 0;

                        decimal ValorDescOriginal = 0;
                        decimal ValorDespOriginal = 0;

                        // Adição
                        if (item.TIPOAJUSTEVALOR == 1)
                        {
                            PrecoTabela = (decimal)item.PRECOTABELA + (decimal)item.VALORPINTURA + (decimal)item.AJUSTEVALOR;
                        }

                        // Substração
                        if (item.TIPOAJUSTEVALOR == 0 && item.AJUSTEVALOR > 0)
                        {
                            PrecoTabela = (decimal)item.PRECOTABELA + (decimal)item.VALORPINTURA - (decimal)item.AJUSTEVALOR;
                        }

                        // Não teve alteração nenhuma
                        if (item.TIPOAJUSTEVALOR == 0 && item.AJUSTEVALOR == 0)
                        {
                            PrecoTabela = (decimal)item.PRECOTABELA + (decimal)item.VALORPINTURA;
                        }

                        // Valida o Preço do item
                        if (PrecoTabela == 0)
                        {
                            MessageBox.Show("ATENÇÃO\r\nExistem itens com o preço igual à zero. Favor revisar e tentar novamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Cursor = Cursors.Default;

                            return false;
                        }

                        itmmov.PrecoUnitario = PrecoTabela - ValorDescOriginal + ValorDespOriginal;
                    }

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(sSql, new object[] { oGuid, itmmov.CodColigada, itmmov.NSeqItmMov, itmmov.NumeroSequencial, itmmov.IdPrd,
                        itmmov.IdPrdComposto, itmmov.CodUnd, itmmov.Quantidade, itmmov.PrecoUnitario, itmmov.AliquotaIPI, itmmov.HistoricoLongo,
                        itmmov.PRODPRICIPAL, itmmov.SEQUENCIAL, itmmov.TIPOMAT, itmmov.PRDPADRAO, itmmov.OBSPRDPADRAO, itmmov.PRDREVENDA, itmmov.PRECOUNITARIOSELEC, item.DATAENTREGA, itmmov.IDNAT, itmmov.APLICAPROD,
                        itmmov.NUMPEDIDO, NIP, itmmov.TIPOINOX, item.VALORPINTURA, item.TIPOPINTURA, item.VALORDESCORIGINAL, item.CORPINTURA, itmmov.PRECOTABELA});


                    if (item.COMPOSICAO != null && item.COMPOSICAO.Count > 0)
                    {
                        foreach (ItemComposicao itmComposicao in item.COMPOSICAO)
                        {
                            sSql = String.Format(@"insert into ZORCAMENTOITEMCOMPOSTO values (/*CODCOLIGADA*/ {0},
	                                                                                           /*CODFILIAL*/ {1}, 
	                                                                                           /*IDMOV*/ '{2}',
	                                                                                           /*NSEQ*/ {3},
	                                                                                           /*IDPRD*/ {4},
	                                                                                           /*QUANTIDADE*/ {5},
	                                                                                           /*PRECOUNITARIO*/ {6},
                                                                                               /*TOTAL*/ {7})",

                                                                                                   /*CODCOLIGADA*/ itmComposicao.CODCOLIGADA,
                                                                                                   /*CODFILIAL*/ itmComposicao.CODFILIAL,
                                                                                                   /*IDMOV*/ oGuid,
                                                                                                   /*NSEQ*/ itmmov.NSeqItmMov,
                                                                                                   /*IDPRD*/ itmComposicao.IDPRD,
                                                                                                   /*QUANTIDADE*/ itmComposicao.CODIGOPRD.StartsWith("14") ? item.DISTANCIAMENTO != "" ? item.DISTANCIAMENTO.Substring(6) : itmComposicao.QUANTIDADE.ToString().Replace(",", ".") : itmComposicao.QUANTIDADE.ToString().Replace(",", "."),
                                                                                                   /*UNIDADEMEDIDA itmComposicao.UNIDADEMEDIDA,*/
                                                                                                   /*PRECOUNITARIO*/ itmComposicao.PRECOUNITARIO.ToString().Replace(",", "."),
                                                                                                   /*TOTAL*/ itmComposicao.TOTAL.ToString().Replace(",", ".")
                                                                                                   );
                            queryItemComposto.Add(sSql);
                        }
                    }

                    movimento.ItemMovimento.Add(itmmov);
                }

                if (Itens.Sum(x => x.VALORTOTAL) <= 0)
                {
                    sPoint = string.Concat("Camada: [Client]: Verifica se preco nao vai dar 0.");
                    sSql = String.Format(@"SELECT isnull(sum((QUANTIDADE*PRECOUNITARIO)),0) as 'TOTAL' FROM ZTITMMOVORCTEMP where ROWID = '{0}'", movimento.GuidId);
                    Decimal total = Convert.ToDecimal(MetodosSQL.GetField(sSql, "TOTAL"));
                    if (total <= 0)
                    {
                        throw new Exception("Oçamento não pode ser salvo com soma total de itens resultando em 0");
                    }
                }

                sPoint = string.Concat("Camada: [Client]: Gera IDNAT do movimento.");
                string sql = String.Empty;
                sql = String.Format(@"select CODNAT from DCFOP where CODCOLIGADA = 1 and IDNAT = '{0}'", Itens[0].IDNAT);
                string[] IDSPLIT = MetodosSQL.GetField(sql, "CODNAT").Split('.');

                sPoint = "Camada: [Client], Campo: [IDNAT]";
                sql = String.Format(@"select IDNAT from DCFOP where CODCOLIGADA = 1 and CODNAT = '{0}'", IDSPLIT[0]);
                movimento.IdNat = int.Parse(MetodosSQL.GetField(sql, "IDNAT"));

                sPoint = string.Concat("Camada: [Client], Rotina: [Executa Método OrcamentoSave");

                AppInterop.Message mensagem;
                if (FatureContexto.Remoto)
                {
                    mensagem = new Util().ConvertToMessage(FatureContexto.ServiceSoapClient.OrcamentoSave(AppLib.Context.Usuario, AppLib.Context.Senha, new Util().ConvertToWSMovMovimentoPar(movimento)));
                }
                else
                {
                    mensagem = FatureContexto.ServiceClient.OrcamentoSave(AppLib.Context.Usuario, AppLib.Context.Senha, movimento);
                }

                sPoint = string.Concat("Camada: [Client], Rotina: [Verifica Retorno");
                if (Itens.Sum(x => (x.PRECOUNITARIO * x.QUANTIDADE)) > 0)
                {
                    sPoint = string.Concat("Camada: [Client], Rotina: [Grava Consumidor Final]");
                    int ConsuFinal = cbConsumidorFinal.Checked ? 1 : 0;

                    MetodosSQL.ExecMultiple(queryItemComposto);

                    sql = String.Format(@"update TMOVFISCAL
                                            set OPERACAOCONSUMIDORFINAL = '{0}'
                                            where CODCOLIGADA = 1 
                                            AND IDMOV = '{1}'", ConsuFinal, mensagem.Retorno);
                    MetodosSQL.ExecQuery(sql);

                    sPoint = string.Concat("Camada: [Client], Rotina: [Retorno do Identificador do Movimento");

                    if (Convert.ToInt32(campoInteiroIDMOV.Get()) <= 0 || campoInteiroIDMOV.Get() == null)
                    {
                        // Verifica se o movimento está sendo copiado
                        if (CopiaDeMovimento == true)
                        {
                            campoInteiroIDMOV.Set(int.Parse(mensagem.Retorno.ToString()));
                        }

                        // Verifica se um novo registro está sendo inserido
                        if (this.Acao == AppLib.Global.Types.Acao.Novo)
                        {
                            campoInteiroIDMOV.Set(int.Parse(mensagem.Retorno.ToString()));
                        }
                        else // Registro pode ser editado ou estar sendo alterado um item durante a inserção 
                        {
                            // Se ambos os valores estiverem nulos, o registro está sendo inserido e seus itens editados
                            if (campoInteiroIDMOV.Get() == null && New.Class.EnviromentHelper.IDMOV == null)
                            {
                                campoInteiroIDMOV.Set(int.Parse(mensagem.Retorno.ToString()));
                            }
                        }
                    }

                    sSql = String.Format(@"update ZORCAMENTOITEMCOMPOSTO
                                                    set IDMOV = {0}
                                                    where CODCOLIGADA = 1
                                                    and IDMOV = '{1}'", campoInteiroIDMOV.Get(), oGuid);
                    MetodosSQL.ExecQuery(sSql);

                    #region Atualiza o IPI dos produtos ao salvar
                    try
                    {
                        string Update = String.Format(@"update TTRBMOV set ALIQUOTA = TTRBPRD.ALIQUOTA from TTRBMOV

                                      inner join TITMMOV TTM
                                      on TTRBMOV.IDMOV = TTM.IDMOV
                                      and TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV
                                      and TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA

                                      inner join TMOV TM
                                      on TM.CODCOLIGADA = TTM.CODCOLIGADA
                                      and TM.IDMOV = TTM.IDMOV

                                      inner join TTRBPRD 
                                      on TTRBPRD.IDPRD = TTM.IDPRD
                                      and TTRBPRD.CODCOLIGADA = TTM.CODCOLIGADA

                                      where TM.CODTMV = '2.1.05'
                                      and TTRBPRD.CODTRB = 'IPI'
                                      and TTM.CODCOLIGADA = 1
                                      and TTRBMOV.CODTRB = 'IPI'
                                      and TM.IDMOV = {0}", (int)campoInteiroIDMOV.Get());

                        AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Update);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao atualizar IPI: " + ex.Message);
                    }
                    #endregion

                    try
                    {
                        sSql = @"SELECT NUMEROMOV FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?";
                        sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, (int)campoInteiroIDMOV.Get() });
                        System.Data.DataTable dt1 = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                        if (dt.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(dt1.Rows[0]["NUMEROMOV"].ToString()))
                            {
                                campoTextoNUMEROMOV.Set(dt1.Rows[0]["NUMEROMOV"].ToString());
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //sSql = String.Format(@"delete from ZORCAMENTOITEMCOMPOSTO where CODCOLIGADA = 1 and CODFILIAL = 1 and IDMOV = '{0}'", String.IsNullOrWhiteSpace(campoInteiroIDMOV.Get().ToString()) ? oGuid.ToString() : campoInteiroIDMOV.Get().ToString());
                        this.Cursor = Cursors.Default;
                        sPoint = "Camada: [Client], Rotina: [Busca Numero do Movimento";
                        throw new Exception(mensagem.Mensagem);
                    }

                    sPoint = string.Concat("Camada: [Client], Rotina: [Busca dados do movimento");
                    sSql = @"SELECT * FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?";
                    sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, (int)campoInteiroIDMOV.Get() });
                    System.Data.DataTable dt2 = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                    ExcluiuTudo = false;

                    if (dt2.Rows.Count > 0)
                    {
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Bruto");
                        tbValorBruto.EditValue = dt2.Rows[0]["VALORBRUTO"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALORBRUTO"]);
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Outros");
                        tbValorOutros.EditValue = dt2.Rows[0]["VALOROUTROS"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALOROUTROS"]);
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Liquido");
                        tbValorLiquido.EditValue = dt2.Rows[0]["VALORLIQUIDO"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALORLIQUIDO"]);

                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Frete");
                        tbValorFrete.EditValue = dt2.Rows[0]["VALORFRETE"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALORFRETE"]);
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Percentual Frete");
                        tbPercentualFrete.EditValue = dt2.Rows[0]["PERCENTUALFRETE"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["PERCENTUALFRETE"]);
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Desconto");
                        tbValorDesconto.EditValue = dt2.Rows[0]["VALORDESC"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALORDESC"]);
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Percentual Desconto");

                        if (rbGeral.Checked)
                        {
                            tbDescontoPercentual.EditValue = dt2.Rows[0]["PERCENTUALDESC"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["PERCENTUALDESC"]);
                        }
                        else
                        {
                            tbDescontoPercentual.EditValue = 0;
                        }

                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Despesa");
                        //tbAcrescimoDesp.EditValue = dt2.Rows[0]["VALORDESP"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALORDESP"]);
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Percentual Despesa");
                        //tbAcrescimoPercentual.EditValue = dt2.Rows[0]["PERCENTUALDESP"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["PERCENTUALDESP"]);
                    }

                    if (rbItem.Checked)
                    {
                        tbDescontoPercentual.EditValue = movimento.VALORDESCRAT;
                    }
                    else
                    {
                        tbDescontoPercentual.EditValue = movimento.PercentualDesc;
                    }

                    this.Cursor = Cursors.Default;
                    return true;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    sPoint = "Camada: [Server], ";
                    throw new Exception(mensagem.Mensagem);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(string.Concat(sPoint, " - ", ex.Message), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void FormOrcamentoCadastro_AposNovo(object sender, EventArgs e)
        {
            Itens.Clear();

            if (dteDataEmissao.Value == null)
                dteDataEmissao.Value = DateTime.Now;

            if (campoListaFRETECIFOUFOB.Get() == null)
                campoListaFRETECIFOUFOB.Set("2");

            if (tbValorDesconto.EditValue == null)
            {
                tbValorDesconto.EditValue = 0;
            }

            if (tbDescontoPercentual.EditValue == null)
            {
                tbValorDesconto.EditValue = 0;
            }

            if (tbAcrescimoDesp.EditValue == null)
            {
                tbAcrescimoDesp.EditValue = 0;
            }

            if (tbAcrescimoPercentual.EditValue == null)
            {
                tbAcrescimoPercentual.EditValue = 0;
            }
        }

        private void FormOrcamentoCadastro_AntesEditar(object sender, EventArgs e)
        {
            Itens.Clear();
            ExcluiuTudo = false;
        }

        private void FormOrcamentoCadastro_AposEditar(object sender, EventArgs e)
        {
            Valida v = new Valida();
        }

        #endregion

        #region Validação da saída dos componentes Lookup

        private void campoLookupCODCFO_Leave(object sender, EventArgs e)
        {
            if (campoLookupCODCFO.textBoxCODIGO.EditValue != null)
            {
                if (!string.IsNullOrEmpty(campoLookupCODCFO.textBoxCODIGO.EditValue.ToString()))
                {
                    New.Class.Utilities util = new New.Class.Utilities();

                    if (!util.ExisteCampo(campoLookupCODCFO))
                    {
                        XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        campoLookupCODCFO.textBoxCODIGO.Select();
                        return;
                    }
                }
            }
        }

        private void campoLookupCODRPR_Leave(object sender, EventArgs e)
        {
            if (campoLookupCODRPR.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookupCODRPR))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookupCODRPR.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void campoLookupCODTRA_Leave(object sender, EventArgs e)
        {
            if (campoLookupCODTRA.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(campoLookupCODTRA))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    campoLookupCODTRA.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void clLocalEstoque_Leave(object sender, EventArgs e)
        {
            if (clLocalEstoque.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(clLocalEstoque))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clLocalEstoque.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void clSerie_Leave(object sender, EventArgs e)
        {
            if (clSerie.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(clSerie))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clSerie.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void clVendedorInterno_Leave(object sender, EventArgs e)
        {
            if (clVendedorInterno.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(clVendedorInterno))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clVendedorInterno.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void clCondigaoPagamento_Leave(object sender, EventArgs e)
        {
            if (clCondigaoPagamento.textBoxCODIGO.EditValue != null)
            {
                New.Class.Utilities util = new New.Class.Utilities();

                if (!util.ExisteCampo(clCondigaoPagamento))
                {
                    XtraMessageBox.Show("Informe um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clCondigaoPagamento.textBoxCODIGO.Select();
                    return;
                }
            }
        }

        private void clTabelaFaturamento_Leave(object sender, EventArgs e)
        {

        }

        private void campoLookupTPCULTURA_Leave(object sender, EventArgs e)
        {

        }

        #endregion

        #region Evento - Após Seleção

        private void campoListaAplicacao_AposSelecao(object sender, EventArgs e)
        {
            try
            {
                List<TITMMOV> NewItens = Itens;

                foreach (TITMMOV item in NewItens)
                {
                    item.APLICACAO = campoListaAplicacao.Get();

                    if (!cePossuiBeneficio.Checked)
                    {
                        item.NUMEROCFOP = ItensOrcamento.getCFOP(campoLista2.Get(), item.APLICACAO, item.CODIGOPRD, cbConsumidorFinal.Checked ? "1" : "0");
                    }

                    item.IDNAT = ItensOrcamento.getIDNAT(item.NUMEROCFOP);
                }

                Itens = NewItens;

                New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
                gcItensMovimento.DataSource = orc.AtualizaGridView(Itens);
                //gvItensMovimento.BestFitColumns();

                util.SetVisaoUsuario(gvItensMovimento, "ITENS");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void campoLista1_AposSelecao(object sender, EventArgs e)
        {
            if (campoLista1.comboBox1.SelectedValue != null)
            {
                if (campoLista1.comboBox1.SelectedValue.ToString() == "2")
                {
                    ceSemImposto.Checked = true;
                    cePossuiBeneficio.Enabled = false;
                }
                else
                {
                    cePossuiBeneficio.Enabled = true;
                    ceSemImposto.Checked = false;
                }
            }
        }

        private void campoLookupCODCFO_AposSelecao(object sender, EventArgs e)
        {
            int CFOIMOB = Convert.ToInt32(AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(0, "SELECT CFOIMOB FROM FCFO WHERE CODCFO = ?", new object[] { campoLookupCODCFO.Get() }));

            if (CFOIMOB > 0)
            {
                MessageBox.Show("O cliente possui bloqueio cadastral.", "Aviso.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            DataTable dt = MetodosSQL.GetDT("select distinct CODETD from ZREGRACFOP order by CODETD");

            AppLib.Windows.CodigoNome[] a = new AppLib.Windows.CodigoNome[dt.Rows.Count];


            int i = 0;
            foreach (DataRow prod in dt.Rows)
            {
                a[i] = new AppLib.Windows.CodigoNome(prod["CODETD"].ToString(), prod["CODETD"].ToString());
                i++;
            }

            campoLista2.Lista = a;

            listaContribuinteICMS.Set(null);

            if (campoLookupCODCFO.Get() == "00002" || campoLookupCODCFO.Get() == "00632")
            {
                campoLista2.Enabled = true;

                campoLista2.Set("SP");
                listaContribuinteICMS.Set(null);
                listaContribuinteICMS.Enabled = true;

                if (this.Acao == AppLib.Global.Types.Acao.Editar)
                {
                    string sql = String.Format(@"select NCONTRIB, UFORCAMENTO from TMOVCOMPL
                                                  where CODCOLIGADA = {0}
                                                  and IDMOV = {1}", AppLib.Context.Empresa, campoInteiroIDMOV.Get());
                    campoLista2.Set(MetodosSQL.GetField(sql, "UFORCAMENTO"));
                    listaContribuinteICMS.Set(MetodosSQL.GetField(sql, "NCONTRIB"));
                }
                else if (IDMOVCOPIADO > 0)
                {
                    string sql = String.Format(@"select NCONTRIB, UFORCAMENTO, NOMEFCFOORC from TMOVCOMPL
                                                  where CODCOLIGADA = {0}
                                                  and IDMOV = {1}", AppLib.Context.Empresa, IDMOVCOPIADO);
                    campoLista2.Set(MetodosSQL.GetField(sql, "UFORCAMENTO"));
                    listaContribuinteICMS.Set(MetodosSQL.GetField(sql, "NCONTRIB"));
                    txtNomeCliente.Set(MetodosSQL.GetField(sql, "NOMEFCFOORC"));
                }
                else
                {

                }

                txtNomeCliente.Enabled = true;

                if (listaContribuinteICMS.Get() == "0")
                {
                    cbConsumidorFinal.Checked = true;
                }
                else
                {
                    cbConsumidorFinal.Checked = false;
                }
            }
            else
            {
                string sql = String.Format(@"SELECT CONTRIBUINTE, FCFO.CODETD FROM FCFO 

                                                left join ZREGRACFOP
                                                on ZREGRACFOP.CODCOLIGADA = FCFO.CODCOLIGADA
                                                and ZREGRACFOP.CODETD = FCFO.CODETD

                                                WHERE CODCFO = '{0}'", campoLookupCODCFO.textBoxCODIGO.Text);

                listaContribuinteICMS.Set(MetodosSQL.GetField(sql, "CONTRIBUINTE"));
                listaContribuinteICMS.Enabled = false;

                String ufCliente = MetodosSQL.GetField(sql, "CODETD");
                cbConsumidorFinal.Enabled = false;

                if (!String.IsNullOrWhiteSpace(ufCliente))
                {
                    campoLista2.Set(ufCliente);
                    campoLista2.Enabled = false;
                }
                else
                {
                    if (this.Acao == AppLib.Global.Types.Acao.Editar)
                    {
                        MessageBox.Show("UF do Cliente sem Regra Cadastrada.", "Aviso.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (listaContribuinteICMS.Get() == "0")
                {
                    cbConsumidorFinal.Checked = true;
                }
                else
                {
                    cbConsumidorFinal.Checked = false;
                }

                txtNomeCliente.Enabled = false;
                txtNomeCliente.Set(null);
            }

            if (this.Acao != AppLib.Global.Types.Acao.Editar)
            {

                string sql = string.Format(@"select isnull(PERCENTUALDESC , 0) as 'PERCDESCONTO' from FCFODEF 
                                                where CODCFO = '{0}'", campoLookupCODCFO.Get());

                tbDescontoPercentual.EditValue = (decimal)MetodosSQL.GetObject(sql, "PERCDESCONTO");
            }

            tbInformacoesComerciais.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField("", "SELECT INFCOMERCIAL FROM FCFOCOMPL WHERE CODCFO = ?", new object[] { campoLookupCODCFO.textBoxCODIGO.Text }).ToString();

            CarregaCreditoCliente(campoLookupCODCFO.Get());

            SelectNextControl(GetNextControl((Control)sender, true), true, true, true, true);
        }

        private void campoLista2_AposSelecao(object sender, EventArgs e)
        {
            try
            {
                List<TITMMOV> NewItens = Itens;

                foreach (TITMMOV item in NewItens)
                {
                    item.APLICACAO = campoListaAplicacao.Get();

                    if (!cePossuiBeneficio.Checked)
                    {
                        item.NUMEROCFOP = ItensOrcamento.getCFOP(campoLista2.Get(), item.APLICACAO, item.CODIGOPRD, cbConsumidorFinal.Checked ? "1" : "0");
                    }

                    item.IDNAT = ItensOrcamento.getIDNAT(item.NUMEROCFOP);
                }

                Itens = NewItens;

                New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
                gcItensMovimento.DataSource = orc.AtualizaGridView(Itens);
                //gvItensMovimento.BestFitColumns();

                util.SetVisaoUsuario(gvItensMovimento, "ITENS");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listaContribuinteICMS_AposSelecao(object sender, EventArgs e)
        {
            try
            {
                List<TITMMOV> NewItens = Itens;

                foreach (TITMMOV item in NewItens)
                {
                    item.APLICACAO = campoListaAplicacao.Get();

                    if (!cePossuiBeneficio.Checked)
                    {
                        item.NUMEROCFOP = ItensOrcamento.getCFOP(campoLista2.Get(), item.APLICACAO, item.CODIGOPRD, cbConsumidorFinal.Checked ? "1" : "0");
                    }

                    item.IDNAT = ItensOrcamento.getIDNAT(item.NUMEROCFOP);
                }

                Itens = NewItens;

                New.Models.OrcamentoItens orc = new New.Models.OrcamentoItens();
                gcItensMovimento.DataSource = orc.AtualizaGridView(Itens);
                //gvItensMovimento.BestFitColumns();

                util.SetVisaoUsuario(gvItensMovimento, "ITENS");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void campoLookupCODRPR_AposSelecao(object sender, EventArgs e)
        {
            if (campoLookupCODRPR.Get() != null)
            {
                //SelectNextControl(GetNextControl((Control)sender, true), true, true, true, true);
            }
        }

        private void campoLookupCODTRA_AposSelecao(object sender, EventArgs e)
        {
            if (campoLookupCODTRA.Get() != null)
            {
                //SelectNextControl(GetNextControl((Control)sender, true), true, true, true, true);
            }
        }

        private void clVendedorInterno_AposSelecao(object sender, EventArgs e)
        {
            if (clVendedorInterno.Get() != null)
            {
                //SelectNextControl(GetNextControl((Control)sender, true), true, true, true, true);
            }
        }

        private void clCondigaoPagamento_AposSelecao(object sender, EventArgs e)
        {
            if (clCondigaoPagamento.Get() != null)
            {
                //SelectNextControl(GetNextControl((Control)sender, true), true, true, true, true);
            }
        }

        #endregion

        #region Evento - ValueChanged

        private void dteDataEmissao_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbPrazoFabricacao.SelectedItem == null)
                {
                    cbPrazoFabricacao.SelectedIndex = 0;
                    return;
                }

                if (cbPrazoFabricacao.SelectedValue.GetType() == typeof(DataRowView))
                {
                    return;
                }

                if (cbPrazoFabricacao.SelectedValue.ToString() == "99")
                {
                    campoDataENTREGA.Edita = true;
                    campoDataENTREGA.maskedTextBox1.Enabled = true;
                }
                else
                {
                    int prazodias = GetPrazo(cbPrazoFabricacao.SelectedValue.ToString());

                    DateTime tmpDate = Convert.ToDateTime(dteDataEmissao.Value);
                    while (prazodias > 0)
                    {
                        tmpDate = tmpDate.AddDays(1);
                        if (tmpDate.DayOfWeek < DayOfWeek.Saturday &&
                            tmpDate.DayOfWeek > DayOfWeek.Sunday)
                            prazodias--;
                    }

                    campoDataENTREGA.Edita = false;
                    campoDataENTREGA.maskedTextBox1.Enabled = false;
                    campoDataENTREGA.Set(tmpDate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Evento - SelectedIndexChanged

        private void cbPrazoFabricacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbPrazoFabricacao.SelectedItem == null)
                {
                    return;
                }

                if (cbPrazoFabricacao.SelectedValue.GetType() == typeof(DataRowView))
                {
                    return;
                }

                if (cbPrazoFabricacao.SelectedValue.ToString() == "99")
                {
                    campoDataENTREGA.Edita = true;
                    campoDataENTREGA.maskedTextBox1.Enabled = true;
                }
                else
                {
                    int prazodias = GetPrazo(cbPrazoFabricacao.SelectedValue.ToString());

                    DateTime tmpDate = Convert.ToDateTime(dteDataEmissao.Value);
                    while (prazodias > 0)
                    {
                        tmpDate = tmpDate.AddDays(1);
                        if (tmpDate.DayOfWeek < DayOfWeek.Saturday &&
                            tmpDate.DayOfWeek > DayOfWeek.Sunday)
                            prazodias--;
                    }

                    campoDataENTREGA.Edita = false;
                    campoDataENTREGA.maskedTextBox1.Enabled = false;
                    campoDataENTREGA.Set(tmpDate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Evento - CheckedChanged

        private void cbCel_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCel.Checked == true)
            {
                txtTelefoneCom.textEdit1.Properties.Mask.EditMask = "(99) 99999-9999";
                txtTelefoneCom.Focus();
                cbTel.Checked = false;
            }
        }

        private void cbTel_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTel.Checked == true)
            {
                txtTelefoneCom.textEdit1.Properties.Mask.EditMask = "(99) 9999-9999";
                txtTelefoneCom.Focus();
                cbCel.Checked = false;
            }
        }

        private void rbItem_CheckedChanged(object sender, EventArgs e)
        {
            if (rbItem.Checked)
            {
                tbValorDesconto.EditValue = 0;

                tbValorDesconto.Visible = false;
                label40.Visible = false;
            }
            else
            {
                tbValorDesconto.Visible = true;
                label40.Visible = true;
            }
        }

        private void rbGeral_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGeral.Checked)
            {
                if (Itens.Count > 0)
                {
                    tbValorDesconto.EditValue = 0;
                    tbDescontoPercentual.EditValue = 0;

                    tbAcrescimoPercentual.EditValue = 0;
                    tbAcrescimoDesp.EditValue = 0;

                    simpleButtonSALVAR_Click(this, null);
                }
            }
        }

        private void cbConsumidorFinal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbConsumidorFinal.Checked)
                {
                    AppLib.Windows.CodigoNome[] a = new AppLib.Windows.CodigoNome[3];

                    a[0] = new AppLib.Windows.CodigoNome("1", "Uso/Consumo");
                    a[1] = new AppLib.Windows.CodigoNome("2", "Uso/Consumo Sem ST");
                    a[2] = new AppLib.Windows.CodigoNome("6", "Ativo Imobilizado");

                    campoListaAplicacao.Lista = a;
                    campoListaAplicacao.Set(null);
                }
                else
                {
                    AppLib.Windows.CodigoNome[] a = new AppLib.Windows.CodigoNome[6];

                    a[0] = new AppLib.Windows.CodigoNome("1", "Uso/Consumo");
                    a[1] = new AppLib.Windows.CodigoNome("2", "Uso/Consumo Sem ST");
                    a[2] = new AppLib.Windows.CodigoNome("3", "Revender");
                    a[3] = new AppLib.Windows.CodigoNome("4", "Revender Sem ST");
                    a[4] = new AppLib.Windows.CodigoNome("5", "Industrializar");
                    a[5] = new AppLib.Windows.CodigoNome("6", "Ativo Imobilizado");

                    campoListaAplicacao.Enabled = true;
                    campoListaAplicacao.Lista = a;
                    campoListaAplicacao.Set(null);
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Evento - Leave

        private void campoInteiroPRAZOENTREGA_Leave(object sender, EventArgs e)
        {
            try
            {
                int prazodias = Convert.ToInt32(campoInteiroPRAZOENTREGA.Get());

                DateTime tmpDate = Convert.ToDateTime(dteDataEmissao.Value);
                while (prazodias > 0)
                {
                    tmpDate = tmpDate.AddDays(1);
                    if (tmpDate.DayOfWeek < DayOfWeek.Saturday &&
                        tmpDate.DayOfWeek > DayOfWeek.Sunday)
                        prazodias--;
                }

                campoDataENTREGA.Set(tmpDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void campoDataENTREGA_Leave(object sender, EventArgs e)
        {
            if (CODSERIE == "ORC")
            {
                DateTime Emissao, Entrega;

                if (dteDataEmissao.Value == null)
                    return;
                else
                    Emissao = (DateTime)dteDataEmissao.Value;


                if (campoDataENTREGA.Get() == null)
                    return;
                else
                    Entrega = (DateTime)campoDataENTREGA.Get();

                campoInteiroPRAZOENTREGA.Set((Entrega - Emissao).Days);
            }
        }

        private void campoDataEMISSAO_Leave(object sender, EventArgs e)
        {
            try
            {
                int prazodias = GetPrazo(cbPrazoFabricacao.SelectedValue.ToString());

                DateTime tmpDate = Convert.ToDateTime(dteDataEmissao.Value);
                while (prazodias > 0)
                {
                    tmpDate = tmpDate.AddDays(1);
                    if (tmpDate.DayOfWeek < DayOfWeek.Saturday &&
                        tmpDate.DayOfWeek > DayOfWeek.Sunday)
                        prazodias--;
                }

                campoDataENTREGA.Set(tmpDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbValorFrete_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(tbValorFrete.EditValue) == 0)
            {
                tbPercentualFrete.EditValue = 0.0000;

                tbPercentualFrete.Enabled = true;
            }
            else
            {
                tbPercentualFrete.EditValue = 0.0000;

                tbPercentualFrete.Enabled = false;

                simpleButtonOK.Select();
            }
        }

        private void tbPercentualFrete_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(tbPercentualFrete.EditValue) == 0)
            {
                tbValorFrete.EditValue = 0.0000;

                tbValorFrete.Enabled = true;
            }
            else
            {
                tbValorFrete.EditValue = 0.0000;

                tbValorFrete.Enabled = false;

                simpleButtonOK.Select();
            }
        }

        private void tbValorDesconto_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(tbValorDesconto.EditValue) == 0)
            {
                tbDescontoPercentual.EditValue = 0.0000;

                tbAcrescimoDesp.Enabled = true;
                tbAcrescimoPercentual.Enabled = true;

                tbDescontoPercentual.Enabled = true;
            }
            else
            {
                tbDescontoPercentual.EditValue = 0.0000;

                tbDescontoPercentual.Enabled = false;

                tbAcrescimoDesp.Enabled = false;
                tbAcrescimoPercentual.Enabled = false;

                simpleButtonOK.Select();
            }
        }

        private void tbDescontoPercentual_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(tbDescontoPercentual.EditValue) == 0)
            {
                tbValorDesconto.EditValue = 0.0000;

                tbAcrescimoDesp.Enabled = true;
                tbAcrescimoPercentual.Enabled = true;

                tbValorDesconto.Enabled = true;
            }
            else
            {
                tbValorDesconto.EditValue = 0.0000;

                tbValorDesconto.Enabled = false;

                tbAcrescimoDesp.Enabled = false;
                tbAcrescimoPercentual.Enabled = false;

                simpleButtonOK.Select();
            }
        }

        private void tbAcrescimoPercentual_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(tbAcrescimoPercentual.EditValue) > 0)
            {
                tbDescontoPercentual.Enabled = false;
                tbValorDesconto.Enabled = false;

                rbItem.Checked = true;
                rbGeral.Enabled = false;

                simpleButtonOK.Select();
            }
            else
            {
                tbDescontoPercentual.Enabled = true;
                tbValorDesconto.Enabled = true;

                rbGeral.Enabled = true;
            }
        }

        #endregion

        #region Evento - PreviewKeyDown

        private void dteDataEmissao_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void campoTextoNORDEM_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void campoLookupCODCFO_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void campoLista2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void listaContribuinteICMS_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void txtNomeCliente_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void cePossuiBeneficio_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void ceSemImposto_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void cbConsumidorFinal_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void txtCONTATOCOM_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void cbTel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void cbCel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void txtTelefoneCom_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void txtEMAILCOM_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void cbPrazoFabricacao_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void campoDataENTREGA_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void campoLista1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void campoListaAplicacao_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void campoLookupCODRPR_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void campoLookupCODTRA_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void campoListaFRETECIFOUFOB_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void tbInformacoesComerciais_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void clLocalEstoque_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void clSerie_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void clVendedorInterno_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void clCondigaoPagamento_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void clTabelaFaturamento_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void tbValorFrete_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void tbPercentualFrete_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void tbAcrescimoPercentual_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void tbAcrescimoDesp_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void tbValorDesconto_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void tbDescontoPercentual_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void rbItem_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        private void rbGeral_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //TabulacaoPorEnter(sender, e);
        }

        #endregion

        #region Evento - Keydown

        private void FormOrcamentoCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (Validar())
                {
                    FormOrcamentoCadastro_Salvar2(null, null);
                    Itens = new List<TITMMOV>();
                    //rid21_Atualizar(null, null);
                    MessageBox.Show("Registro gravado com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dteDataEmissao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void campoTextoNORDEM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }
        private void campoLookupCODCFO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);

                MessageBox.Show(ActiveControl.Name);
            }
        }

        private void campoLista2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void listaContribuinteICMS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void txtNomeCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void cePossuiBeneficio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void ceSemImposto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cbConsumidorFinal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void txtCONTATOCOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void cbTel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void cbCel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void txtTelefoneCom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void txtEMAILCOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void cbPrazoFabricacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void campoDataENTREGA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void campoLista1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void campoListaAplicacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void campoLookupCODRPR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void campoLookupCODTRA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void campoListaFRETECIFOUFOB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void tbInformacoesComerciais_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void clLocalEstoque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void clSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void clVendedorInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void clCondigaoPagamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void clTabelaFaturamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        #endregion
    }
}
