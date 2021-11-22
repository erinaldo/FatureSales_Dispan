using AppFatureClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFatureClient.Classes;

namespace AppLib.Windows
{
    public partial class FormComissaoCadastro : DevExpress.XtraEditors.XtraForm
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Registro da grid")]
        private DataRow dr;

        [Category("_APP"), Description("Registro em edição")]
        public AppLib.ORM.Jit x { get; set; }

        [Category("_APP"), Description("Tabela principal")]
        public String TabelaPrincipal { get; set; }

        [Category("_APP"), Description("Demais tabelas")]
        private List<String> Tabelas { get; set; }

        [Category("_APP"), Description("Nome da conexão")]
        public String Conexao { get; set; }

        [Category("_APP"), Description("Querys de edição")]
        public Query[] Querys { get; set; }

        [Category("_APP"), Description("Habilitar botão novo")]
        public Boolean BotaoNovo { get; set; }

        [Category("_APP"), Description("Habilitar botão excluir")]
        public Boolean BotaoExcluir { get; set; }

        [Category("_APP"), Description("Ação que acionou o form")]
        public Global.Types.Acao Acao { get; set; }

        #endregion

        #region CONSTRUTORES

        string cod = string.Empty;
        Boolean editar = false;

        Comissao.ParametrosComissao pc = new Comissao.ParametrosComissao();

        public FormComissaoCadastro(string _cod, Boolean _Editar)
        {
            InitializeComponent();

            BotaoNovo = false;
            BotaoExcluir = false;

            Conexao = "Start";

            cod = _cod;
            editar = _Editar;

            gridData1.GetProcessos().Add("Associar conta", null, AssociarConta);
            gridData1.GetProcessos().Add("Incluir conta", null, IncluirConta);

            gridData1.BotaoExcluir = true;

            clNatOrcamentaria.textBoxCODIGO.Text = "01.01.006";
            clNatOrcamentaria.textBoxDESCRICAO.Text = "ADIANTAMENTO DE CLIENTE";

            clCentroCusto.textBoxCODIGO.Text = "02.06";
            clCentroCusto.textBoxDESCRICAO.Text = "FINANCEIRO";
            clCentroCusto_AposSelecao(null, null);

            dtaVencimento.dateTimePicker1.TextChanged += SetDataPrevBaixa;
        }

        private void SetDataPrevBaixa(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(dtaPrevBaixa.Get().ToString()))
                dtaPrevBaixa.Set(dtaVencimento.Get());

        }

        private void FormCadastro2_Load(object sender, EventArgs e)
        {

            if (!this.DesignMode)
            {
                try
                {
                    if (BotaoNovo)
                    {
                        toolStripButtonNOVO.Enabled = true;
                    }
                    else
                    {
                        toolStripButtonNOVO.Enabled = false;
                    }
                }
                catch (Exception) { }

                try
                {
                    if (BotaoExcluir)
                    {
                        toolStripButtonEXCLUIR.Enabled = true;
                    }
                    else
                    {
                        toolStripButtonEXCLUIR.Enabled = false;
                    }
                }
                catch (Exception) { }

            }

            if (editar)
            {
                CarregaVisaoEditar();
            }
            else
            {
                CarregaVisao(cod);
            }


        }

        private void AssociarConta(object sender, EventArgs e)
        {
            string sql = String.Format("select count(1) from FCFOCONT where CLASSCONTA in ('C','ADC') and CODCFO = {0}", clCliente.textBoxCODIGO.Text);

            try
            {
                string ncontas = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, sql, new Object[] { }).ToString();

                FormComissaoAssociar frm = new FormComissaoAssociar();
                frm.codCliente = clCliente.textBoxCODIGO.Text;
                frm.nomeCliente = clCliente.textBoxDESCRICAO.Text;
                frm.ShowDialog();

                //if (int.Parse(ncontas) < 2)
                //{

                //}
                //else
                //{
                //    MessageBox.Show("Cliente já contem as contas contabeis necessarias", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IncluirConta(object sender, EventArgs e)
        {
            string sql = String.Format("select count(1) from FCFOCONT where CLASSCONTA in ('C','ADC') and CODCFO = {0}", clCliente.textBoxCODIGO.Text);

            try
            {
                string ncontas = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, sql, new Object[] { }).ToString();

                if (int.Parse(ncontas) < 2)
                {
                    sql = String.Format("select count(1) from FCFOCONT where CLASSCONTA in ('C','ADC') and CODCFO = {0} and CODCONTA like '1.01.03.00001.%'", clCliente.textBoxCODIGO.Text);
                    ncontas = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, sql, new Object[] { }).ToString();

                    if (int.Parse(ncontas) == 0)
                    {
                        AppFatureClient.Classes.Comissao.MetodosComissao.InsereConta1(clCliente.textBoxCODIGO.Text);
                    }

                    sql = String.Format("select count(1) from FCFOCONT where CLASSCONTA in ('C','ADC') and CODCFO = {0} and CODCONTA like '2.01.04.00001.%'", clCliente.textBoxCODIGO.Text);
                    ncontas = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, sql, new Object[] { }).ToString();

                    if (int.Parse(ncontas) == 0)
                    {
                        AppFatureClient.Classes.Comissao.MetodosComissao.InsereConta2(clCliente.textBoxCODIGO.Text);
                    }

                    MessageBox.Show("Conta(s) incluida(s) com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cliente já contem as contas contabeis necessarias", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                gridData1.Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaVisaoEditar()
        {
            try
            {
                string consulta = String.Format(@"select FL.CODCOLIGADA, 
                                       FL.PAGREC, 
	                                   FL.CODFILIAL, 
	                                   FL.CODCOLCFO, 
	                                   FL.CODCFO, 
	                                   FL.CODTDO, 
	                                   FL.NUMERODOCUMENTO,
	                                   FL.SEGUNDONUMERO,
	                                   convert(char,FL.DATAEMISSAO,103) as 'DATAEMISSAO',
	                                   convert(char,FL.DATAVENCIMENTO,103) as 'DATAVENCIMENTO',
	                                   convert(char,FL.DATAPREVBAIXA,103) as 'DATAPREVBAIXA',
	                                   FL.HISTORICO,
	                                   FL.SERIEDOCUMENTO,
	                                   FL.VALORORIGINAL,
	                                   FL.CODMOEVALORORIGINAL,
	                                   isnull(FL.CODCCUSTO,FLR.CODCCUSTO) as 'CODCCUSTO',
	                                   FL.CODRPR,
	                                   FL.CODTB2FLX,
	                                   FL.IDLAN,
                                       FL.CAMPOALFAOP3,
	                                   FLR.CODCCUSTO as 'CUSTORAT', 
	                                   FLR.CODNATFINANCEIRA,
	                                   FLR.VALOR,
	                                   FLR.PERCENTUAL 
	   
                                from FLAN FL

                                inner join FLANRATCCU FLR
                                on FLR.IDLAN = FL.IDLAN
                                and FLR.CODCOLIGADA = FL.CODCOLIGADA

                                where FL.CODCOLIGADA = 1
                                and FL.IDLAN = {0}", cod);


                DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(consulta, new Object[] { cod });

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    txtNumeroDocumento.Set(row["NUMERODOCUMENTO"].ToString());
                    txtSegundoNumero.Set(row["SEGUNDONUMERO"].ToString());
                    txtValorOriginal.Set((decimal?)row["VALORORIGINAL"]);
                    txtHistorico.Set(row["HISTORICO"].ToString());
                    txtSerieDocumento.Set(row["SERIEDOCUMENTO"].ToString());
                    txtPedido.Set(row["CAMPOALFAOP3"].ToString());
                    txtValorRateio.Set((decimal?)row["VALOR"]);
                    txtPorcentagem.Set(row["PERCENTUAL"].ToString());
                    txtIDLAN.Set(row["IDLAN"].ToString());
                    dtaEmissao.Set(Convert.ToDateTime(row["DATAEMISSAO"].ToString()));
                    dtaVencimento.Set(Convert.ToDateTime(row["DATAVENCIMENTO"].ToString()));
                    dtaPrevBaixa.Set(Convert.ToDateTime(row["DATAPREVBAIXA"].ToString()));
                    clCliente.textBoxCODIGO.Text = row["CODCFO"].ToString();

                    clCentroCusto.textBoxCODIGO.Text = row["CODCCUSTO"].ToString();
                    clTipoCobranca.textBoxCODIGO.Text = row["CODTB2FLX"].ToString();
                    clReprensentante.textBoxCODIGO.Text = row["CODRPR"].ToString();

                    clCentroCustoRateio.textBoxCODIGO.Text = row["CUSTORAT"].ToString();
                    clNatOrcamentaria.textBoxCODIGO.Text = row["CODNATFINANCEIRA"].ToString();
                }

                consulta = @"select CGCCFO from FCFO where CODCFO = ?";
                txtCNPJCPF.Set(AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { clCliente.textBoxCODIGO.Text }).ToString());

                consulta = @"select NOME from FCFO where CODCFO = ?";
                clCliente.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { clCliente.textBoxCODIGO.Text }).ToString();

                consulta = @"select NOME from GCCUSTO where CODCOLIGADA = 1 and CODCCUSTO = ?";
                clCentroCusto.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { clCentroCusto.textBoxCODIGO.Text }).ToString();

                consulta = @"select DESCRICAO from FTB2 where CODCOLIGADA = 1 and CODTB2FLX = ?";
                clTipoCobranca.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { clTipoCobranca.textBoxCODIGO.Text }).ToString();

                consulta = @"select NOME from TRPR where CODCOLIGADA = 1 and CODRPR = ?";
                clReprensentante.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { clReprensentante.textBoxCODIGO.Text }).ToString();

                consulta = @"select DESCRICAO from TTBORCAMENTO where CODCOLIGADA = 1 and CODTBORCAMENTO = ?";
                clNatOrcamentaria.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { clNatOrcamentaria.textBoxCODIGO.Text }).ToString();

                consulta = @"select NOME from GCCUSTO where CODCOLIGADA = 1 and CODCCUSTO = ?";
                clCentroCustoRateio.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { clCentroCustoRateio.textBoxCODIGO.Text }).ToString();


                panelRateio.Enabled = true;


                txtTipo.Set("Receber");
                txtSerieDocumento.Set("@@@");

                consulta = @"SELECT CODCOLIGADA FROM GFILIAL where CODCOLIGADA = ?";
                clFilial.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { 1 }).ToString();
                consulta = @"SELECT NOMEFANTASIA FROM GFILIAL where CODCOLIGADA = ?";
                clFilial.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { 1 }).ToString();

                consulta = @"select CODTDO from FTDO where CODTDO = 'ADC' and CODCOLIGADA = ?";
                clTipoDocumento.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { 1 }).ToString();
                consulta = @"select DESCRICAO from FTDO where CODTDO = 'ADC' and CODCOLIGADA = ?";
                clTipoDocumento.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { 1 }).ToString();

                consulta = @"select SIMBOLO from GMOEDA where SIMBOLO = 'R$'";
                clMoeda.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { }).ToString();
                consulta = @"select DESCRICAO from GMOEDA where SIMBOLO = 'R$'";
                clMoeda.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { }).ToString();

                string sql = String.Format(@"select CCONTA.CODCONTA, CCONTA.REDUZIDO, FCFO.NOME, FCFOCONT.PERCENTUAL, FCFOCONT.CLASSCONTA from FCFOCONT inner join FCFO on FCFO.CODCFO = FCFOCONT.CODCFO inner join CCONTA on CCONTA.CODCONTA = FCFOCONT.CODCONTA and CCONTA.CODCOLIGADA = 1 where FCFOCONT.CLASSCONTA in ('C','ADC') and FCFOCONT.CODCFO = {0}", clCliente.textBoxCODIGO.Text);

                gridData1.Consulta = sql.Split(' ');
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void CarregaVisao(string pedido)
        {
            string consulta = @"select TM.CODCOLIGADA as 'Coligada',
                                       TM.IDMOV as 'IDMov',
	                                   TM.NUMEROMOV as 'Pedido',
	                                   FCFO.NOMEFANTASIA as 'Cliente',
                                       FCFO.CODCFO as 'CodCliente',
                                       FCFO.CGCCFO as 'CJPNCPF',
	                                   TRPR.NOMEFANTASIA as 'Representante',
	                                   TM.VALORLIQUIDO as 'Valor pedido',
	                                   ZTC.VALORPAGO as 'Valor pago',
	                                   (ZTC.VALORLIQ - ZTC.VALORPAGO) as 'Valor pendente',
	                                   ZTC.NCARTA as 'Numero da carta',
	                                   ZTC.CONCLUIDO as 'Concluido',
	                                   ZTC.USERCRIACAO as 'Usuário de criação',
	                                   ZTC.DATACARTA as 'Data da carta'
	   
                                from TMOV TM

                                inner join FCFO
                                on FCFO.CODCFO = TM.CODCFO

                                inner join TRPR 
                                on TRPR.CODRPR = TM.CODRPR

                                left join ZTMOVCOMISSAO ZTC
                                on ZTC.IDMOV = TM.IDMOV
                                and ZTC.CODCOLIGADA = TM.CODCOLIGADA

                                where TM.CODTMV = '2.1.10'
                                and TM.NUMEROMOV = ?";
            DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(consulta, new Object[] { pedido });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                clCliente.textBoxCODIGO.Text = row["CodCliente"].ToString();
                clCliente.textBoxDESCRICAO.Text = row["Cliente"].ToString();
                txtCNPJCPF.Set(row["CJPNCPF"].ToString());
            }


            consulta = String.Format(@"select count(1)+1 as 'CONT' from FLAN where NUMERODOCUMENTO like  '{0}%' and CODCOLIGADA = 1 and CODTDO = 'ADC'", pedido);
            txtNumeroDocumento.Set(pedido + "/" + MetodosSQL.GetField(consulta, "CONT"));

            txtTipo.Set("Receber");
            txtSerieDocumento.Set("@@@");
            txtPedido.Set(cod);

            consulta = @"SELECT CODCOLIGADA FROM GFILIAL where CODCOLIGADA = ?";
            clFilial.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { 1 }).ToString();
            consulta = @"SELECT NOMEFANTASIA FROM GFILIAL where CODCOLIGADA = ?";
            clFilial.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { 1 }).ToString();

            consulta = @"select CODTDO from FTDO where CODTDO = 'ADC' and CODCOLIGADA = ?";
            clTipoDocumento.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { 1 }).ToString();
            consulta = @"select DESCRICAO from FTDO where CODTDO = 'ADC' and CODCOLIGADA = ?";
            clTipoDocumento.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { 1 }).ToString();

            consulta = @"select SIMBOLO from GMOEDA where SIMBOLO = 'R$'";
            clMoeda.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { }).ToString();
            consulta = @"select DESCRICAO from GMOEDA where SIMBOLO = 'R$'";
            clMoeda.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta, new Object[] { }).ToString();



            string sql = String.Format(@"select CCONTA.CODCONTA, CCONTA.REDUZIDO, FCFO.NOME, FCFOCONT.PERCENTUAL from FCFOCONT inner join FCFO on FCFO.CODCFO = FCFOCONT.CODCFO inner join CCONTA on CCONTA.CODCONTA = FCFOCONT.CODCONTA and CCONTA.CODCOLIGADA = 1 where FCFOCONT.CLASSCONTA in ('C','ADC') and FCFOCONT.CODCFO = {0}", clCliente.textBoxCODIGO.Text);

            gridData1.Consulta = sql.Split(' ');
        }

        #endregion

        #region EVENTOS

        private void toolStripButtonNOVO_Click(object sender, EventArgs e)
        {
            Acao = Global.Types.Acao.Novo;
            if (new AppLib.Security.Access().Cadastrar(this.Conexao, this.TabelaPrincipal, AppLib.Context.Perfil))
            {
                if (BotaoNovo)
                {
                    try
                    {
                        this.AntesNovo(this, null);
                    }
                    catch { }

                    this.LimparTela(this.Controls);

                    try
                    {
                        this.AposNovo(this, null);
                    }
                    catch { }
                }
                else
                {
                    // MessageBox.Show("Botão desabilitado.");
                }
            }
            else
            {
                MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso para salvar este cadastro (" + this.TabelaPrincipal + ").", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripButtonEXCLUIR_Click(object sender, EventArgs e)
        {
            if (BotaoExcluir)
            {
                Boolean excluiu;

                try
                {
                    excluiu = this.ValidarExcluir(this, null);
                }
                catch
                {
                    excluiu = true;
                }

                if (excluiu)
                {
                    try
                    {
                        try
                        {
                            this.AntesExcluir(this, null);
                        }
                        catch { }

                        this.ExcluirObject(this, null);

                        try
                        {
                            this.AposExcluir(this, null);
                        }
                        catch { }
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao excluir registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // MessageBox.Show("Botão desabilitado.");
            }
        }

        private void toolStripButtonPRIMEIRO_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveFirst();
            System.Data.DataRowView drv = (System.Data.DataRowView)bindingSource1.Current;
            this.Editar(drv.Row, false);
        }

        private void toolStripButtonANTERIOR_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
            System.Data.DataRowView drv = (System.Data.DataRowView)bindingSource1.Current;
            this.Editar(drv.Row, false);
        }

        private void toolStripButtonPROXIMO_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
            System.Data.DataRowView drv = (System.Data.DataRowView)bindingSource1.Current;
            this.Editar(drv.Row, false);
        }

        private void toolStripButtonULTIMO_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveLast();
            System.Data.DataRowView drv = (System.Data.DataRowView)bindingSource1.Current;
            this.Editar(drv.Row, false);
        }

        private Boolean CarregaParametros()
        {
            pc.tipo = txtTipo.Get();
            pc.codColigada = clFilial.textBoxCODIGO.Text;
            pc.codCliente = clCliente.textBoxCODIGO.Text;
            pc.CpfCnpj = txtCNPJCPF.Get();
            pc.tipoDocumento = clTipoDocumento.textBoxCODIGO.Text;
            pc.numDocumento = txtNumeroDocumento.Get();
            pc.segundoNumero = txtSegundoNumero.Get();
            pc.Moeda = clMoeda.textBoxCODIGO.Text;

            if (!String.IsNullOrWhiteSpace(txtValorOriginal.Get().ToString())) { pc.valorOriginal = txtValorOriginal.Get(); } else { MessageBox.Show("Insira valor original"); return false; }

            if (!String.IsNullOrWhiteSpace(clCentroCusto.textBoxCODIGO.Text)) { pc.centroCusto = clCentroCusto.textBoxCODIGO.Text; } else { MessageBox.Show("Favor Informar o centro de custo"); return false; }

            if (!String.IsNullOrWhiteSpace(clReprensentante.textBoxCODIGO.Text)) { pc.Representante = clReprensentante.textBoxCODIGO.Text; } else { MessageBox.Show("Favor Informar o representante"); return false; }

            if (!String.IsNullOrWhiteSpace(clTipoCobranca.textBoxCODIGO.Text)) { pc.tipoCobranca = clTipoCobranca.textBoxCODIGO.Text; } else { MessageBox.Show("Favor Informar o tipo de cobrança"); return false; }

            if (!String.IsNullOrWhiteSpace(dtaEmissao.Get().ToString())) { pc.dataEmissao = dtaEmissao.Get(); } else { MessageBox.Show("Seleciona a data de emissão"); return false; }

            if (!String.IsNullOrWhiteSpace(dtaVencimento.Get().ToString())) { pc.dataVencimento = dtaVencimento.Get(); } else { MessageBox.Show("Selecione a data de vencimento"); return false; }

            if (!String.IsNullOrWhiteSpace(dtaPrevBaixa.Get().ToString())) { pc.dataPrevBaixa = dtaPrevBaixa.Get(); } else { MessageBox.Show("Seleciona a data prevista de baixa"); return false; }

            if (!String.IsNullOrWhiteSpace(clNatOrcamentaria.textBoxCODIGO.Text)) { pc.codNatOrcamentaria = clNatOrcamentaria.textBoxCODIGO.Text; } else { MessageBox.Show("Favor Informar a natureza orçamentaria"); return false; }

            if (!String.IsNullOrWhiteSpace(clCentroCustoRateio.textBoxCODIGO.Text)) { pc.codCentroCusto = clCentroCustoRateio.textBoxCODIGO.Text; } else { MessageBox.Show("Favor Informar o centro de custo"); return false; }

            if (!String.IsNullOrWhiteSpace(txtValorRateio.Get().ToString())) { pc.Valor = txtValorRateio.Get(); } else { MessageBox.Show("Insira valor do rateio"); return false; }

            //if (!String.IsNullOrWhiteSpace(txtHistorico.Get())) { pc.Historico = txtHistorico.Get(); } else { MessageBox.Show("Campo Histórico não pode ficar vazio"); return false; }


            pc.serieDocumento = txtSerieDocumento.Get();

            pc.nPedido = txtPedido.Get();
            pc.Porcentagem = txtPorcentagem.Get();

            return true;
        }

        public void simpleButtonSALVAR_Click(object sender, EventArgs e)
        {
            //Boolean validou;

            //try
            //{
            //    validou = this.ValidarSalvar(this, null);
            //}
            //catch
            //{
            //    validou = true;
            //}

            //if (validou)
            //{
            //    try
            //    {
            //        this.AntesSalvar(this, null);
            //    }
            //    catch { }

            //    try
            //    {
            //        this.SalvarObject(this, null);

            //        try
            //        {
            //            this.AposSalvar(this, null);
            //        }
            //        catch { }
            //    }
            //    catch 
            //    {
            //        MessageBox.Show("Erro ao salvar o registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

            string step = String.Empty;

            try
            {
                step = "CarregaParametros";
                bool ret = CarregaParametros();

                if (ret)
                {
                    if (editar)
                    {
                        step = "Atualiza o ADC";
                        Comissao.MetodosComissao.AtualizaADC(pc, txtIDLAN.Get());
                        MessageBox.Show("ADC atualizado com sucesso");
                    }
                    else
                    {
                        string sql = String.Format("select count(1) from FCFOCONT where CLASSCONTA in ('C','ADC') and CODCFO = {0}", clCliente.textBoxCODIGO.Text);
                        string ncontas = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, sql, new Object[] { }).ToString();

                        if (int.Parse(ncontas) == 2)
                        {
                            step = "Inclui ADC";
                            Comissao.MetodosComissao.IncluiADC(pc);
                            MessageBox.Show("ADC incluido com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Não é possivel incluir ADC sem vinculo a contas contabeis.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }


                    }

                    step = "Retorna ID";
                    txtIDLAN.Set(AppLib.Context.poolConnection.Get().ExecGetField("", "select IDLAN from FLAN where CODCOLIGADA = 1 and NUMERODOCUMENTO = ?", new Object[] { txtNumeroDocumento.Get() }).ToString());

                    step = "Modo Editar";
                    editar = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na etapa [" + step + "] - " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            //Boolean validou;

            //try
            //{
            //    validou = this.ValidarSalvar(this, null);
            //}
            //catch
            //{
            //    validou = true;
            //}

            //if (validou)
            //{
            //    try
            //    {
            //        this.AntesSalvar(this, null);
            //    }
            //    catch { }

            //    try
            //    {
            //        if (this.SalvarObject(this, null))
            //        {

            //            Acao = Global.Types.Acao.Editar;

            //            try
            //            {
            //                this.AposSalvar(this, null);
            //            }
            //            catch { }

            //            this.Close();
            //        }
            //    }
            //    catch { }
            //}


            string step = String.Empty;

            try
            {
                step = "CarregaParametros";
                bool ret = CarregaParametros();

                if (ret)
                {

                    if (editar)
                    {
                        step = "Atualiza o ADC";
                        Comissao.MetodosComissao.AtualizaADC(pc, txtIDLAN.Get());
                        MessageBox.Show("ADC atualizado com sucesso");
                    }
                    else
                    {
                        string sql = String.Format("select count(1) from FCFOCONT where CLASSCONTA in ('C','ADC') and CODCFO = {0}", clCliente.textBoxCODIGO.Text);
                        string ncontas = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, sql, new Object[] { }).ToString();

                        if (int.Parse(ncontas) == 2)
                        {
                            step = "Inclui ADC";
                            Comissao.MetodosComissao.IncluiADC(pc);
                        }
                        else
                        {
                            MessageBox.Show("Não é possivel incluir ADC sem vinculo a contas contabeis.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na etapa [" + step + "] - " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCadastroObject_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManager.ReleaseUnusedMemory(false);
        }

        #endregion

        #region MÉTODOS EXTERNOS

        public void Novo()
        {
            Acao = Global.Types.Acao.Novo;
            toolStripButtonNOVO_Click(this, null);
            this.ShowDialog();
        }

        public void Editar(System.Windows.Forms.BindingSource bs)
        {
            Acao = Global.Types.Acao.Editar;
            if (bs.Count > 0)
            {
                try
                {
                    this.AntesEditar(this, null);
                }

                catch { }
                bindingSource1 = bs;
                System.Data.DataRowView registro = (System.Data.DataRowView)bindingSource1.Current;
                this.Editar(registro.Row, true);

                try
                {
                    this.AposEditar(this, null);
                }
                catch { }
            }
        }

        public void Excluir(System.Data.DataRowCollection registros)
        {
            Acao = Global.Types.Acao.Excluir;
            if (registros != null)
            {
                for (int i = 0; i < registros.Count; i++)
                {
                    this.Editar(registros[i], false);
                    toolStripButtonEXCLUIR_Click(this, null);
                }
            }
        }

        public System.Windows.Forms.ToolStripItemCollection GetAnexos()
        {
            return this.toolStripButtonANEXOS.DropDownItems;
        }

        public System.Windows.Forms.ToolStripItemCollection GetProcessos()
        {
            return this.toolStripButtonPROCESSOS.DropDownItems;
        }

        #endregion

        #region MÉTODOS INTERNOS

        private void LimparTela(System.Windows.Forms.Control.ControlCollection componentes)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                LimparTela(componentes[i]);
            }
        }

        private void LimparTela(System.Windows.Forms.Control componente)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                LimparTela(campo.Controls);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                LimparTela(campo.Controls);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                LimparTela(campo.Controls);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                LimparTela(campo.Controls);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                LimparTela(campo.Controls);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                LimparTela(campo.Controls);
            }

            #endregion

            #region COMPONENTES NOVOS

            if (componente.GetType() == typeof(CampoInteiro))
            {
                CampoInteiro campo = (CampoInteiro)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoDecimal))
            {
                CampoDecimal campo = (CampoDecimal)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoData))
            {
                CampoData campo = (CampoData)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoHora))
            {
                CampoHora campo = (CampoHora)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoDataHora))
            {
                CampoDataHora campo = (CampoDataHora)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoTexto))
            {
                CampoTexto campo = (CampoTexto)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoMemo))
            {
                CampoMemo campo = (CampoMemo)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoLista))
            {
                CampoLista campo = (CampoLista)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoLookup))
            {
                CampoLookup campo = (CampoLookup)componente;
                campo.textBoxCODIGO.Text = campo.Default;
                campo.textBox1_Leave(this, null);
            }

            if (componente.GetType() == typeof(CampoImagem))
            {
                CampoImagem campo = (CampoImagem)componente;
                campo.Clear();
            }

            #endregion
        }

        public void Editar(System.Data.DataRow registro, Boolean mostrarForm)
        {
            if (registro != null)
            {
                dr = registro;

                try
                {
                    this.AntesEditar(this, null);
                }
                catch { }

                // Carrega as querys
                for (int i = 0; i < this.Querys.Length; i++)
                {
                    Query q = this.Querys[i];

                    String consulta = "";

                    try
                    {
                        consulta = AppLib.Context.poolConnection.Get(q.Conexao).ParseCommand(q.GetConsulta(), this.MontarParametros(q.Parametros));
                        q.dt = AppLib.Context.poolConnection.Get(q.Conexao).ExecQuery(consulta, new Object[] { });

                        // Carrega os campos
                        this.CarregarForm(i, q.dt, this.Controls);
                    }
                    catch (Exception ex)
                    {
                        new FormExceptionSQL().Mostrar("Erro na montagem da query", consulta, ex);
                    }
                }

                try
                {
                    this.AposEditar(this, null);
                }
                catch { }

                if (mostrarForm)
                {
                    this.ShowDialog();
                }
            }
        }

        #endregion

        #region MÉTODOS DE CARGA

        private void CarregarForm(int posicaoQuery, System.Data.DataTable dt, System.Windows.Forms.Control.ControlCollection componentes)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                CarregarForm(posicaoQuery, dt, componentes[i]);
            }
        }

        private void CarregarForm(int posicaoQuery, System.Data.DataTable dt, System.Windows.Forms.Control componente)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            #endregion

            #region COMPONENTES LIB

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                String coluna = dt.Columns[i].ColumnName;
                Object valor = dt.Rows[0][i];

                if (componente.GetType() == typeof(CampoInteiro))
                {
                    CampoInteiro campo = (CampoInteiro)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(null);
                            }
                            else
                            {
                                campo.Set(int.Parse(valor.ToString()));
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoDecimal))
                {
                    CampoDecimal campo = (CampoDecimal)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(null);
                            }
                            else
                            {
                                campo.Set(decimal.Parse(valor.ToString()));
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoData))
                {
                    CampoData campo = (CampoData)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(null);
                            }
                            else
                            {
                                campo.Set(DateTime.Parse(valor.ToString()));
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoHora))
                {
                    CampoHora campo = (CampoHora)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(null);
                            }
                            else
                            {
                                campo.Set(DateTime.Parse(valor.ToString()));
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoDataHora))
                {
                    CampoDataHora campo = (CampoDataHora)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(null);
                            }
                            else
                            {
                                campo.Set(DateTime.Parse(valor.ToString()));
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoTexto))
                {
                    CampoTexto campo = (CampoTexto)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(String.Empty);
                            }
                            else
                            {
                                campo.Set(valor.ToString());
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoMemo))
                {
                    CampoMemo campo = (CampoMemo)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(String.Empty);
                            }
                            else
                            {
                                campo.Set(valor.ToString());
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoLista))
                {
                    CampoLista campo = (CampoLista)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(String.Empty);
                            }
                            else
                            {
                                campo.Set(valor.ToString());
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoLookup))
                {
                    CampoLookup campo = (CampoLookup)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(String.Empty);
                            }
                            else
                            {
                                campo.textBoxCODIGO.Text = valor.ToString();
                                campo.textBox1_Leave(this, null);
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoImagem))
                {
                    CampoImagem campo = (CampoImagem)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        // nome imagem
                        if (coluna.ToUpper().Equals(campo.ColunaNomeImagem.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.SetNomeImagem(null);
                            }
                            else
                            {
                                campo.SetNomeImagem(valor.ToString()); ;
                            }
                        }

                        // imagem
                        if (coluna.ToUpper().Equals(campo.ColunaImagem.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.SetImagem(null);
                            }
                            else
                            {
                                campo.SetImagem(valor.ToString()); ;
                            }
                        }
                    }
                }

            } // fim do for

            #endregion

            #region GRID LIB

            if (componente.GetType() == typeof(GridData))
            {
                GridData campo = (GridData)componente;
                campo.Atualizar(false, false, null);
            }

            if (componente.GetType() == typeof(GridObject))
            {
                GridObject campo = (GridObject)componente;
                campo.toolStripButtonATUALIZAR_Click(this, null);
            }

            #endregion
        }

        /// <summary>
        /// Chamado somnete na ocasião de auto incremento
        /// </summary>
        /// <param name="tabela">Nome da tabela</param>
        /// <param name="componentes">Collections de compoenntes do form</param>
        /// <param name="incremento">Valor do incremento</param>
        private void CarregarForm(String tabela, System.Windows.Forms.Control.ControlCollection componentes, int incremento)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                CarregarForm(tabela, componentes[i], incremento);
            }
        }

        private void CarregarForm(String tabela, System.Windows.Forms.Control componente, int incremento)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            #endregion

            #region COMPONENTES LIB

            // O AUTO INCREMENTO PODE OCORRER SOMENTE EM CAMPOS INTEIRO
            // POR ESTA RAZÃO OS DEMAIS TIPOS FORAM IGNORADOS
            if (componente.GetType() == typeof(CampoInteiro))
            {
                CampoInteiro campo = (CampoInteiro)componente;
                if (this.EhAutoIncremento(campo.Tabela, campo.Campo))
                {
                    campo.Set(incremento);
                }
            }

            #endregion
        }

        private void CarregarObjeto(String tabela, System.Windows.Forms.Control.ControlCollection componentes)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                CarregarObjeto(tabela, componentes[i]);
            }
        }

        private void CarregarObjeto(String tabela, System.Windows.Forms.Control componente)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            #endregion

            #region COMPONENTES NOVOS

            if (componente.GetType() == typeof(CampoInteiro))
            {
                CampoInteiro campo = (CampoInteiro)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoDecimal))
            {
                CampoDecimal campo = (CampoDecimal)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoData))
            {
                CampoData campo = (CampoData)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoHora))
            {
                CampoHora campo = (CampoHora)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoDataHora))
            {
                CampoDataHora campo = (CampoDataHora)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoTexto))
            {
                CampoTexto campo = (CampoTexto)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoMemo))
            {
                CampoMemo campo = (CampoMemo)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoLista))
            {
                CampoLista campo = (CampoLista)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoLookup))
            {
                CampoLookup campo = (CampoLookup)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoImagem))
            {
                CampoImagem campo = (CampoImagem)componente;
                x.Set(campo.ColunaNomeImagem, campo.GetNomeImagem());
                x.Set(campo.ColunaImagem, campo.GetImagem());
            }

            #endregion
        }

        #endregion

        #region MÉTODOS DE APOIO

        public Object[] MontarParametros(String[] parametros)
        {
            Object[] result = new Object[parametros.Length];

            for (int p = 0; p < parametros.Length; p++)
            {
                String coluna = parametros[p];

                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    if (dr.Table.Columns[i].ColumnName.ToUpper().Equals(coluna.ToUpper()))
                    {
                        result[p] = dr[i].ToString();
                        i = dr.Table.Columns.Count;
                    }
                }
            }

            return result;
        }

        public List<String> getTabelas()
        {
            Tabelas = new List<String>();
            getTabelas(this.Controls);
            return Tabelas;
        }

        private void getTabelas(System.Windows.Forms.Control.ControlCollection componentes)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                getTabelas(componentes[i]);
            }
        }

        private void getTabelas(System.Windows.Forms.Control componente)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                getTabelas(campo.Controls);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                getTabelas(campo.Controls);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                getTabelas(campo.Controls);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                getTabelas(campo.Controls);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                getTabelas(campo.Controls);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                getTabelas(campo.Controls);
            }

            #endregion

            #region COMPONENTES NOVOS

            if (componente.GetType() == typeof(CampoInteiro))
            {
                CampoInteiro campo = (CampoInteiro)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoDecimal))
            {
                CampoDecimal campo = (CampoDecimal)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoData))
            {
                CampoData campo = (CampoData)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoHora))
            {
                CampoHora campo = (CampoHora)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoDataHora))
            {
                CampoDataHora campo = (CampoDataHora)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoTexto))
            {
                CampoTexto campo = (CampoTexto)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoMemo))
            {
                CampoMemo campo = (CampoMemo)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoLista))
            {
                CampoLista campo = (CampoLista)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoLookup))
            {
                CampoLookup campo = (CampoLookup)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoImagem))
            {
                CampoImagem campo = (CampoImagem)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            #endregion
        }

        private Boolean TabelaExiste(String tabela)
        {
            for (int i = 0; i < Tabelas.Count; i++)
            {
                if (Tabelas[i].ToString().ToUpper().Equals(tabela.ToUpper()))
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean EhAutoIncremento(String tabela, String campo)
        {
            // Se estou na tabela correta
            if (x.Metadados[0].NomeTabela.ToUpper().Equals(tabela.ToUpper()))
            {
                // Varre os campos
                for (int i = 0; i < x.Metadados.Count; i++)
                {
                    // Se encontrei o campo
                    if (x.Metadados[i].NomeCampo.ToUpper().Equals(campo.ToUpper()))
                    {
                        // Se é auto incremento
                        if (x.Metadados[i].AutoIncremento)
                        {
                            // Sim, interrompe o look e retorna verdadeiro
                            i = x.Metadados.Count;
                            return true;
                        }
                        else
                        {
                            // Não é auto incremento
                            return false;
                        }
                    }
                }
            }

            // Não sendo da tabela retorna falso
            return false;
        }

        #endregion

        #region MÉTODOS CUSTOMIZADOS

        public delegate Boolean Salvar2Handler(object sender, EventArgs e);
        [Category("_APP"), Description("Método botão salvar"), DefaultValue(false)]
        public event Salvar2Handler SalvarObject;

        public delegate void Excluir2Handler(object sender, EventArgs e);
        [Category("_APP"), Description("Método botão excluir"), DefaultValue(false)]
        public event Excluir2Handler ExcluirObject;

        public delegate Boolean ValidarSalvarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método de validação executado antes de salvar"), DefaultValue(false)]
        public event ValidarSalvarHandler ValidarSalvar;

        public delegate Boolean ValidarExcluirHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método de validação executado antes de excluir"), DefaultValue(false)]
        public event ValidarExcluirHandler ValidarExcluir;

        public delegate void AntesNovoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de limpar o form"), DefaultValue(false)]
        public event AntesNovoHandler AntesNovo;

        public delegate void AposNovoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado depois de limpar o form"), DefaultValue(false)]
        public event AposNovoHandler AposNovo;

        public delegate void AntesEditarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de editar um registro"), DefaultValue(false)]
        public event AntesEditarHandler AntesEditar;

        public delegate void AposEditarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado depois de editar um registro"), DefaultValue(false)]
        public event AposEditarHandler AposEditar;

        public delegate void PrepararHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de salvar"), DefaultValue(false)]
        public event PrepararHandler AntesSalvar;

        public delegate void AposSalvarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado após salvar"), DefaultValue(false)]
        public event AposSalvarHandler AposSalvar;

        public delegate void AntesExcluirHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de excluir"), DefaultValue(false)]
        public event AntesExcluirHandler AntesExcluir;

        public delegate void AposExcluirHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado depois de excluir"), DefaultValue(false)]
        public event AposExcluirHandler AposExcluir;



        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void txtValorOriginal_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtValorOriginal.Get().ToString()))
            {
                txtValorRateio.Set(txtValorOriginal.Get());
                panelRateio.Enabled = true;
                CalculaPorcentagemRateio();
            }
            else
            {
                txtValorRateio.Set(0);
                panelRateio.Enabled = false;
                txtPorcentagem.Set("");
            }
        }

        private void txtValorRateio_Leave(object sender, EventArgs e)
        {
            if (txtValorRateio.Get() > txtValorOriginal.Get())
            {
                MessageBox.Show("Valor não pode ser maior que o valor do documento");
                txtValorRateio.Set(txtValorOriginal.Get());
            }
            else
            {
                CalculaPorcentagemRateio();
            }
        }

        private void txtValorRateio_Load(object sender, EventArgs e)
        {

        }

        private void CalculaPorcentagemRateio()
        {
            txtPorcentagem.Set(((100 * txtValorRateio.Get()) / txtValorOriginal.Get()).ToString() + "%");
        }



        private void gridData1_Excluir(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Deseja realmente exluir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                if (gridData1.GetDataRows().Count > 1)
                {
                    MessageBox.Show("Selecione apenas uma conta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (gridData1.GetDataRows().Count < 1)
                {
                    MessageBox.Show("Selecione uma conta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataRow dr = gridData1.GetDataRow();

                    Comissao.MetodosComissao.ExcluiAssociacaoConta(clCliente.textBoxCODIGO.Text, dr["CODCONTA"].ToString());
                }
            }
        }

        private void clCentroCusto_AposSelecao(object sender, EventArgs e)
        {
            clCentroCustoRateio.textBoxCODIGO.Text = clCentroCusto.textBoxCODIGO.Text;
            clCentroCustoRateio.textBoxDESCRICAO.Text = clCentroCusto.textBoxDESCRICAO.Text;
        }

        private void dtaVencimento_Validated(object sender, EventArgs e)
        {

        }

        private void dtaVencimento_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtaVencimento_MouseLeave(object sender, EventArgs e)
        {

        }

        private void dtaPrevBaixa_Validated(object sender, EventArgs e)
        {

        }
    }
}
