using AppFatureClient.Classes;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormOrcamentoVisao : AppLib.Windows.FormVisao
    {
        private static FormOrcamentoVisao _instance = null;

        private bool novoOrcamento = false;
        private int IDMOV;

        string EnderecoEmail;
        int PortaEmail;
        string UsuarioEmail;
        string SenhaEmail;
        bool SSL;
        string EnviarComo;
        string EnviarComoDisplay;
        string EnviarPara;
        string Enderecos;
        string Assunto;
        object rowHandle;

        public static FormOrcamentoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormOrcamentoVisao();
            return _instance;
        }

        private bool AnexoItemMovimento;
        private bool AnexoMail;

        public FormOrcamentoVisao()
        {
            InitializeComponent();
        }

        private void AbreRelatorio(DevExpress.XtraReports.IReport report)
        {
            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.ShowPreviewDialog();
            }
        }

        private void FormOrcamentoVisao_Load(object sender, EventArgs e)
        {
            AnexoItemMovimento = false;

            grid1.GetAnexos().Add("Item do Movimento", null, ItemMovimento);
            grid1.GetAnexos().Add("E-mails Enviados", null, MailMovimento);
            grid1.GetAnexos().Add("Fechar Anexos", null, FecharAnexos);

            //grid1.GetProcessos().Add("Imprimir Orçamento Líquido", null, ImprimirOrcamentoLiquido);
            //grid1.GetProcessos().Add("Imprimir Orçamento Desconto", null, ImprimirOrcamentoDesconto);
            //grid1.GetProcessos().Add("Imprimir Orçamento Troca", null, ImprimirOrcamentoTroca);
            //grid1.GetProcessos().Add("Imprimir Orçamento Banco", null, ImprimirOrcamentoBanco);
            grid1.GetProcessos().Add("Imprimir Orçamento", null, ImprimirOrcamento);
            grid1.GetProcessos().Add("Enviar Email", null, EnviaEmail);
            //grid1.GetProcessos().Add("Imprimir Orçamento Acessórios", null, ImprimirOrcamentoAcessorios);

            grid1.GetProcessos().Add("-", null, null);

            //grid1.GetProcessos().Add("Aprovar Desconto", null, AprovarMovimento);
            //grid1.GetProcessos().Add("Aprovar Financeiro", null, AprovarFinanceiro);

            //grid1.GetProcessos().Add("-", null, null);

            //grid1.GetProcessos().Add("Desaprovar Desconto", null, DesaprovarDesconto);
            //grid1.GetProcessos().Add("Desaprovar Financeiro", null, DesaprovarFinanceiro);

            //grid1.GetProcessos().Add("-", null, null);

            grid1.GetProcessos().Add("Faturar Movimento", null, FaturarMovimento);
            grid1.GetProcessos().Add("Cancelar Movimento", null, CancelarMovimento);
            grid1.GetProcessos().Add("Cópia de Movimento", null, CopiaDeMovimento);
            grid1.GetProcessos().Add("Cópia de Movimento (Pedido)", null, CopiaMovimentoByPedido);
            grid1.GetProcessos().Add("Rastrear Movimento", null, Rastreabilidade);

            //grid1.GetProcessos().Add("Aplicar Tabela de Preço", null, AplicarTabelaPreco);

            FormataValoresGridView();
            OrdenaGridView();
        }

        public void EnviaEmail(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8048", AppLib.Context.Perfil))
            {
                DataRow dr = grid1.GetDataRow();
                if ((string)dr["APROVACAO"] == "APROVAR")
                {
                    MessageBox.Show("Não é permitido enviar email de orçamentos pendentes de aprovação.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MetodosEmail.EnviarEmailOrcamentoPedido(dr, grid1.Conexao);
            }
        }

        private void DesaprovarDesconto(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8049", AppLib.Context.Perfil))
            {
                if (MessageBox.Show("Deseja realmente Desaprovar o Movimento?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    DataRow dr = null;
                    try
                    {
                        dr = grid1.GetDataRow();
                    }
                    catch
                    {
                    }
                    if (dr == null)
                    {
                        return;
                    }
                    AppLib.ORM.Jit ZTMOVAPROVA = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZTMOVAPROVA");
                    ZTMOVAPROVA.Set("CODCOLIGADA", dr["CODCOLIGADA"]);
                    ZTMOVAPROVA.Set("IDMOV", dr["IDMOV"]);
                    int retorno = ZTMOVAPROVA.Delete();
                    if (retorno.Equals(1))
                    {
                        MessageBox.Show("Movimento desaprovado com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Esse movimento " + dr["IDMOV"].ToString() + " não foi aprovado financeiramente ainda.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    grid1.Atualizar();
                }
            }
        }

        private void DesaprovarFinanceiro(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente Desaprovar o Financeiro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                DataRow dr = null;
                try
                {
                    dr = grid1.GetDataRow();
                }
                catch
                {
                }
                if (dr == null)
                {
                    return;
                }
                AppLib.ORM.Jit ZTMOVAPROVAFIN = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZTMOVAPROVAFIN");
                ZTMOVAPROVAFIN.Set("CODCOLIGADA", dr["CODCOLIGADA"]);
                ZTMOVAPROVAFIN.Set("IDMOV", dr["IDMOV"]);
                int retorno = ZTMOVAPROVAFIN.Delete();
                if (retorno.Equals(1))
                {
                    MessageBox.Show("Movimento financeiro desaprovado com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Esse movimento " + dr["IDMOV"].ToString() + " não foi aprovado financeiramente ainda.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void FecharAnexos(object sender, EventArgs e)
        {
            try
            {
                AnexoItemMovimento = false;
                AnexoMail = false;

                xtpItensMovimento.PageVisible = false;
                xtpEmail.PageVisible = false;

                splitContainer1.Panel2Collapsed = true;
            }
            catch { }
        }

        public void MailMovimento(object sender, EventArgs e)
        {
            if (grid1.gridView1.SelectedRowsCount > 0)
            {
                DataRow dr = null;

                try
                {
                    dr = grid1.GetDataRow();
                }
                catch { }

                if (dr == null)
                    return;

                AnexoMail = true;
                string ConsultaFiltro = string.Empty;

                try
                {
                    splitContainer1.Panel2Collapsed = false;

                    ConsultaFiltro = String.Format(@"select CODUSUARIO, SENDERADDR, TOADDR, SUBJECT, SENDTIME, BODY from ZMAILSEND 
                                                 where CODCOLIGADA = 1 and CODFILIAL = 1 and IDMOV = '{0}'", dr["IDMOV"].ToString());

                    ConsultaFiltro = AppLib.Context.poolConnection.Get(grid1.Conexao).ParseCommand(ConsultaFiltro, new Object[] { dr["CODCOLIGADA"], dr["IDMOV"] });

                    gridControl2.DataSource = null;
                    gridView2.Columns.Clear();
                    gridView2.GridControl.DataSource = AppLib.Context.poolConnection.Get(grid1.Conexao).ExecQuery(ConsultaFiltro, new Object[] { });

                    // formata as colunas
                    for (int i = 0; i < gridView2.Columns.Count; i++)
                    {
                        String TempColuna = gridView2.Columns[i].FieldName;
                        String TempTipo = gridView2.Columns[i].ColumnType.ToString();
                        String TempFormat = gridView2.Columns[i].DisplayFormat.ToString();

                        if (gridView2.Columns[i].ColumnType == typeof(DateTime))
                        {
                            gridView2.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            gridView2.Columns[i].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss.fff";
                        }
                    }

                    // Tratamentos para a grid
                    // Grid somente leitura
                    gridView2.OptionsBehavior.Editable = false;

                    // Grid com seleção de multiplos registros
                    gridView2.OptionsSelection.MultiSelect = true;
                    gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

                    // Grid com scroll horizontal
                    gridView2.OptionsView.ColumnAutoWidth = false;

                    // Grid com auto ajuste do melhor tamanho das colunas
                    gridView2.BestFitMaxRowCount = 800;
                    gridView2.BestFitColumns();

                    // Grid mostrar coluna de filtro
                    gridView2.OptionsView.ShowAutoFilterRow = true;

                    xtpEmail.PageVisible = true;
                }
                catch (Exception ex)
                {
                    new AppLib.Windows.FormExceptionSQL().Mostrar("Erro ao executar consulta.", ConsultaFiltro, ex);
                }
            }
        }

        public void ItemMovimento(object sender, EventArgs e)
        {
            if (grid1.gridView1.SelectedRowsCount > 0)
            {
                DataRow dr = null;

                try
                {
                    dr = grid1.GetDataRow();
                }
                catch { }

                if (dr == null)
                    return;

                AnexoItemMovimento = true;
                string ConsultaFiltro = string.Empty;

                try
                {
                    splitContainer1.Panel2Collapsed = false;

                    ConsultaFiltro = @"SELECT
TITMMOV.NSEQITMMOV,
TITMMOV.IDPRD,
TPRD.CODIGOPRD,
ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
TITMMOV.CODUND,
TITMMOV.QUANTIDADEORIGINAL QUANTIDADE,
TITMMOV.PRECOUNITARIO,
TPRD.NUMEROCCF

FROM TITMMOV
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
                    ConsultaFiltro = AppLib.Context.poolConnection.Get(grid1.Conexao).ParseCommand(ConsultaFiltro, new Object[] { dr["CODCOLIGADA"], dr["IDMOV"] });

                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                    gridView1.GridControl.DataSource = AppLib.Context.poolConnection.Get(grid1.Conexao).ExecQuery(ConsultaFiltro, new Object[] { });

                    // formata as colunas
                    for (int i = 0; i < gridView1.Columns.Count; i++)
                    {
                        String TempColuna = gridView1.Columns[i].FieldName;
                        String TempTipo = gridView1.Columns[i].ColumnType.ToString();
                        String TempFormat = gridView1.Columns[i].DisplayFormat.ToString();

                        if (gridView1.Columns[i].ColumnType == typeof(DateTime))
                        {
                            gridView1.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            gridView1.Columns[i].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss.fff";
                        }
                    }

                    // Tratamentos para a grid
                    // Grid somente leitura
                    gridView1.OptionsBehavior.Editable = false;

                    // Grid com seleção de multiplos registros
                    gridView1.OptionsSelection.MultiSelect = true;
                    gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

                    // Grid com scroll horizontal
                    gridView1.OptionsView.ColumnAutoWidth = false;

                    // Grid com auto ajuste do melhor tamanho das colunas
                    gridView1.BestFitMaxRowCount = 800;
                    gridView1.BestFitColumns();

                    // Grid mostrar coluna de filtro
                    gridView1.OptionsView.ShowAutoFilterRow = true;

                    xtpItensMovimento.PageVisible = true;
                }
                catch (Exception ex)
                {
                    new AppLib.Windows.FormExceptionSQL().Mostrar("Erro ao executar consulta.", ConsultaFiltro, ex);
                }
            }
        }

        public void Rastreabilidade(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    FormRastreabilidade f = new FormRastreabilidade();
                    f.Movimento = grid1.GetDataRow();
                    f.ShowDialog();
                }
            }
            catch
            { }
        }

        public void AprovarMovimento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8013", AppLib.Context.Perfil))
            {
                int codcoligada = AppLib.Context.Empresa;
                String usuarioAprovacao = AppLib.Context.Usuario;

                if (new Valida().AcessoSupervisor(grid1.Conexao, codcoligada, "T", usuarioAprovacao))
                {
                    System.Data.DataRowCollection drc = grid1.GetDataRows();

                    if (drc != null)
                    {
                        FormAprovarMovimento f = new FormAprovarMovimento();
                        f.Movimentos = grid1.GetDataRows();
                        f.ShowDialog();

                        grid1.toolStripButtonATUALIZAR_Click(this, null);
                    }
                }
            }
        }

        public void AprovarFinanceiro(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8028", AppLib.Context.Perfil))
            {
                int codcoligada = AppLib.Context.Empresa;
                String usuarioAprovacao = AppLib.Context.Usuario;
                if (new Valida().AcessoSupervisor(grid1.Conexao, codcoligada, "T", usuarioAprovacao))
                {
                    //System.Data.DataRow row = grid1.GetDataRow();
                    DataRowCollection rows = grid1.GetDataRows();
                    if (rows != null)
                    {
                        FormAprovarFinanceiro f = new FormAprovarFinanceiro();
                        //f.Movimento = row;
                        f.Movimentos = grid1.GetDataRows();
                        f.ShowDialog();
                        grid1.toolStripButtonATUALIZAR_Click(this, null);
                    }
                }
            }
        }

        public void ImprimirOrcamentoAcessorios(object sender, EventArgs e)
        {
            if (this.MovimentosNecessitamAprovacao())
            {
                // NÃO FAZER NADA
            }
            else
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    RelOrcamentoAcessorios relatorio = new RelOrcamentoAcessorios();
                    relatorio.SelectRow = grid1.GetDataRow();
                    relatorio.Conexao = grid1.Conexao;
                    DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                    tool.ShowRibbonPreviewDialog();
                }
            }
        }

        public void ImprimirOrcamento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8047", AppLib.Context.Perfil))
            {
                if (novoOrcamento == true)
                {
                    if (grid1.gridView1.RowCount == 0)
                    {
                        grid1.Atualizar();
                        grid1.gridView1.RefreshData();
                    }

                    DataTable dtOrcamento = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT * FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, IDMOV });

                    System.Data.DataRow dr = dtOrcamento.Rows[0];

                    RelOrcamentoDispan rel = new RelOrcamentoDispan();
                    rel.SelectRow = dr;
                    rel.Conexao = grid1.Conexao;

                    FormPrintPreview fpp = new FormPrintPreview();
                    fpp.SelectRow = dr;
                    fpp.Conexao = grid1.Conexao;
                    fpp.relatorio = rel;
                    fpp.ShowDialog();

                    novoOrcamento = false;
                }
                else
                {
                    if (grid1.gridView1.SelectedRowsCount > 0)
                    {
                        try
                        {
                            if (grid1.gridView1.RowCount == 0)
                            {
                                grid1.Atualizar();
                                grid1.gridView1.RefreshData();
                            }

                            System.Data.DataRowCollection drc = grid1.GetDataRows();

                            if (drc != null)
                            {
                                if (drc.Count > 1)
                                {
                                    MessageBox.Show("Selecione apenas um movimento.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                //if (novoOrcamento == true)
                                //{
                                //    DataTable dtOrcamento = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT * FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, IDMOV });

                                //    System.Data.DataRow dr = dtOrcamento.Rows[0];

                                //    RelOrcamentoDispan rel = new RelOrcamentoDispan();
                                //    rel.SelectRow = dr;
                                //    rel.Conexao = grid1.Conexao;

                                //    FormPrintPreview fpp = new FormPrintPreview();
                                //    fpp.SelectRow = dr;
                                //    fpp.Conexao = grid1.Conexao;
                                //    fpp.relatorio = rel;
                                //    fpp.ShowDialog();

                                //    novoOrcamento = false;
                                //}
                                //else
                                //{
                                RelOrcamentoDispan relatorio = new RelOrcamentoDispan();
                                relatorio.SelectRow = grid1.GetDataRow();
                                relatorio.Conexao = grid1.Conexao;

                                FormPrintPreview f = new FormPrintPreview();
                                f.SelectRow = grid1.GetDataRow();
                                f.Conexao = grid1.Conexao;
                                f.relatorio = relatorio;
                                f.ShowDialog();
                                //}
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("System.DBNull"))
                            {
                                MessageBox.Show("O filtro atual não contempla o registro incluído.\r\nConsidere refazer o filtro e imprimir novamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
        }

        public void ImprimirOrcamento()
        {
            try
            {
                if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8047", AppLib.Context.Perfil))
                {
                    //MovimentosNecessitamAprovacao();

                    System.Data.DataTable dt = (DataTable)grid1.gridControl1.DataSource;

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            System.Data.DataRow dr = grid1.gridView1.GetDataRow(dt.Rows.Count - 1);

                            RelOrcamentoDispan relatorio = new RelOrcamentoDispan();
                            relatorio.SelectRow = dr;
                            relatorio.Conexao = grid1.Conexao;

                            FormPrintPreview f = new FormPrintPreview();
                            f.SelectRow = dr;
                            f.Conexao = grid1.Conexao;
                            f.relatorio = relatorio;
                            f.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ImprimirOrcamentoBanco(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8039", AppLib.Context.Perfil))
            {
                if (this.MovimentosNecessitamAprovacao())
                {
                    // NÃO FAZER NADA
                }
                else
                {
                    System.Data.DataRowCollection drc = grid1.GetDataRows();

                    if (drc != null)
                    {
                        if (grid1.GetDataRows().Count > 1)
                        {
                            MessageBox.Show("Selecione apenas um movimento.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        System.Data.DataRow dr = grid1.GetDataRow();

                        string Consulta = @"UPDATE TITMMOV SET TITMMOV.QUANTIDADEORIGINAL = TITMMOV.QUANTIDADETOTAL 
                                    FROM  TITMMOV, TMOV 
                                    WHERE TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA
                                    AND TMOV.IDMOV = TITMMOV.IDMOV
                                    AND TMOV.CODTMV IN ('2.1.03')
                                    AND TITMMOV.CODCOLIGADA = ?
                                    AND TITMMOV.IDMOV = ?";

                        Consulta = AppLib.Context.poolConnection.Get().ParseCommand(Consulta, new Object[] { dr["CODCOLIGADA"], dr["IDMOV"] });
                        AppLib.Context.poolConnection.Get().ExecTransaction(Consulta, new Object[] { });

                        RelOrcamentoBanco relatorio = new RelOrcamentoBanco();
                        relatorio.SelectRow = grid1.GetDataRow();


                        DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                        tool.ShowRibbonPreviewDialog();

                        //AbreRelatorio(relatorio);
                    }
                }
            }
        }
        public void ImprimirOrcamentoTroca(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8038", AppLib.Context.Perfil))
            {
                if (this.MovimentosNecessitamAprovacao())
                {
                    // NÃO FAZER NADA
                }
                else
                {
                    System.Data.DataRowCollection drc = grid1.GetDataRows();

                    if (drc != null)
                    {
                        if (grid1.GetDataRows().Count > 1)
                        {
                            MessageBox.Show("Selecione apenas um movimento.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        System.Data.DataRow dr = grid1.GetDataRow();

                        string Consulta = @"UPDATE TITMMOV SET TITMMOV.QUANTIDADEORIGINAL = TITMMOV.QUANTIDADETOTAL 
                                    FROM  TITMMOV, TMOV 
                                    WHERE TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA
                                    AND TMOV.IDMOV = TITMMOV.IDMOV
                                    AND TMOV.CODTMV IN ('2.1.03')
                                    AND TITMMOV.CODCOLIGADA = ?
                                    AND TITMMOV.IDMOV = ?";

                        Consulta = AppLib.Context.poolConnection.Get(grid1.Conexao).ParseCommand(Consulta, new Object[] { dr["CODCOLIGADA"], dr["IDMOV"] });
                        AppLib.Context.poolConnection.Get(grid1.Conexao).ExecTransaction(Consulta, new Object[] { });

                        RelOrcamentoTroca relatorio = new RelOrcamentoTroca();
                        relatorio.SelectRow = grid1.GetDataRow();
                        relatorio.Conexao = grid1.Conexao;

                        FormPrintPreview f = new FormPrintPreview();
                        f.SelectRow = grid1.GetDataRow();
                        f.Conexao = grid1.Conexao;
                        f.relatorio = relatorio;
                        f.ShowDialog();

                        //AbreRelatorio(relatorio);
                    }
                }
            }
        }
        public void ImprimirOrcamentoLiquido(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8036", AppLib.Context.Perfil))
            {
                if (this.MovimentosNecessitamAprovacao())
                {
                    // NÃO FAZER NADA
                }
                else
                {
                    System.Data.DataRowCollection drc = grid1.GetDataRows();

                    if (drc != null)
                    {
                        if (grid1.GetDataRows().Count > 1)
                        {
                            MessageBox.Show("Selecione apenas um movimento.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        System.Data.DataRow dr = grid1.GetDataRow();

                        string Consulta = @"UPDATE TITMMOV SET TITMMOV.QUANTIDADEORIGINAL = TITMMOV.QUANTIDADETOTAL 
                                    FROM  TITMMOV, TMOV 
                                    WHERE TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA
                                    AND TMOV.IDMOV = TITMMOV.IDMOV
                                    AND TMOV.CODTMV IN ('2.1.03')
                                    AND TITMMOV.CODCOLIGADA = ?
                                    AND TITMMOV.IDMOV = ?";

                        Consulta = AppLib.Context.poolConnection.Get(grid1.Conexao).ParseCommand(Consulta, new Object[] { dr["CODCOLIGADA"], dr["IDMOV"] });
                        AppLib.Context.poolConnection.Get(grid1.Conexao).ExecTransaction(Consulta, new Object[] { });

                        RelOrcamentoLiquido relatorio = new RelOrcamentoLiquido();
                        relatorio.SelectRow = grid1.GetDataRow();
                        relatorio.Conexao = grid1.Conexao;

                        FormPrintPreview f = new FormPrintPreview();
                        f.SelectRow = grid1.GetDataRow();
                        f.Conexao = grid1.Conexao;
                        f.relatorio = relatorio;
                        f.ShowDialog();

                        //AbreRelatorio(relatorio);
                    }
                }
            }
        }

        public void ImprimirOrcamentoDesconto(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8037", AppLib.Context.Perfil))
            {
                if (this.MovimentosNecessitamAprovacao())
                {
                    // NÃO FAZER NADA
                }
                else
                {
                    System.Data.DataRowCollection drc = grid1.GetDataRows();

                    if (drc != null)
                    {
                        if (grid1.GetDataRows().Count > 1)
                        {
                            MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        System.Data.DataRow dr = grid1.GetDataRow();

                        string Consulta = @"UPDATE TITMMOV SET TITMMOV.QUANTIDADEORIGINAL = TITMMOV.QUANTIDADETOTAL 
                                    FROM  TITMMOV, TMOV 
                                    WHERE TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA
                                    AND TMOV.IDMOV = TITMMOV.IDMOV
                                    AND TMOV.CODTMV IN ('2.1.03')
                                    AND TITMMOV.CODCOLIGADA = ?
                                    AND TITMMOV.IDMOV = ?";

                        Consulta = AppLib.Context.poolConnection.Get(grid1.Conexao).ParseCommand(Consulta, new Object[] { dr["CODCOLIGADA"], dr["IDMOV"] });
                        AppLib.Context.poolConnection.Get(grid1.Conexao).ExecTransaction(Consulta, new Object[] { });

                        RelOrcamentoDesconto relatorio = new RelOrcamentoDesconto();
                        relatorio.SelectRow = grid1.GetDataRow();
                        relatorio.Conexao = grid1.Conexao;

                        FormPrintPreview f = new FormPrintPreview();
                        f.SelectRow = grid1.GetDataRow();
                        f.Conexao = grid1.Conexao;
                        f.relatorio = relatorio;
                        f.ShowDialog();

                        //AbreRelatorio(relatorio);
                    }
                }
            }
        }

        public Boolean MovimentosNecessitamAprovacao()
        {
            System.Data.DataRowCollection drc = grid1.GetDataRows();

            if (drc != null)
            {

                for (int i = 0; i < drc.Count; i++)
                {
                    if (drc[i]["APROVACAO"].Equals("APROVAR"))
                    {
                        throw new Exception("Não é permitido faturar, imprimir ou enviar emails de movimentos que requerem aprovação.");
                    }
                }
            }

            return false;
        }

        public void FaturarMovimento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8014", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                string Cliente = "";

                if (drc != null)
                {
                    for (int i = 0; i < drc.Count; i++)
                    {
                        string tipoVenda = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT TMOVCOMPL.TIPOVENDA FROM TMOV INNER JOIN TMOVCOMPL ON TMOV.IDMOV = TMOVCOMPL.IDMOV AND TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA WHERE TMOV.IDMOV = ? AND TMOV.CODCOLIGADA = ?", new object[] { drc[i]["IDMOV"], AppLib.Context.Empresa }).ToString();
                        Cliente = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT TMOV.CODCFO FROM TMOV INNER JOIN TMOVCOMPL ON TMOV.IDMOV = TMOVCOMPL.IDMOV AND TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA WHERE TMOV.IDMOV = ? AND TMOV.CODCOLIGADA = ?", new object[] { drc[i]["IDMOV"], AppLib.Context.Empresa }).ToString();

                        if ((Cliente == "00002" || Cliente == "00632") && tipoVenda == "1")
                        {
                            MessageBox.Show("Orçamento(s) de clientes sem cadastro não podem ser faturados.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if ((string)drc[i]["APROVACAO"] == "APROVAR")
                        {
                            MessageBox.Show("Não é permitido faturar orçamento pendentes de aprovação.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (Cliente != "00002" && Cliente != "00062")
                    {
                        if (ValidaLimiteCredito(Cliente))
                        {
                            FormFaturar f = new FormFaturar();
                            f.CodTmvOrigem = "2.1.05";
                            f.Movimentos = grid1.GetDataRows();
                            f.ShowDialog();

                            grid1.toolStripButtonATUALIZAR_Click(this, null);
                        }
                    }
                    else
                    {
                        FormFaturar f = new FormFaturar();
                        f.CodTmvOrigem = "2.1.05";
                        f.Movimentos = grid1.GetDataRows();
                        f.ShowDialog();

                        grid1.toolStripButtonATUALIZAR_Click(this, null);
                    }
                }
            }
        }

        private void AplicarTabelaPreco(object sender, EventArgs e)
        {
            DataRowCollection drc = grid1.GetDataRows();
            if (drc == null)
            {
                MessageBox.Show("Favor selecionar pelo menos um registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FormAplicarTabelaPreco f = new FormAplicarTabelaPreco();
            f.ShowDialog();

            if (f.TabelaPreco == null)
            {
                return;
            }

            for (int i = 0; i < drc.Count; i++)
            {
                DataTable dtItens = AppLib.Context.poolConnection.Get().ExecQuery("SELECT VALORBRUTOITEM, VALORBRUTOITEMORIG, VALORLIQUIDO, IDPRD, NSEQITMMOV, QUANTIDADE FROM TITMMOV WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { drc[i]["IDMOV"], AppLib.Context.Empresa });
                for (int ii = 0; ii < dtItens.Rows.Count; ii++)
                {
                    DataTable dtPreco = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT ISNULL(PRECO1, 0) PRECO1, ISNULL(PRECO2, 0) PRECO2, ISNULL(PRECO3, 0) PRECO3, ISNULL(PRECO4, 0) PRECO4, ISNULL(PRECO5, 0) PRECO5 FROM TPRD WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[ii]["IDPRD"] });
                    if (dtPreco.Rows.Count > 0)
                    {
                        switch (f.TabelaPreco)
                        {
                            case "1":
                                AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE TITMMOV SET PRECOUNITARIO = ?, VALORBRUTOITEM = ?, VALORBRUTOITEMORIG = ?, VALORLIQUIDO = ? WHERE IDMOV = ? AND NSEQITMMOV = ?", new object[] { Convert.ToDecimal(dtPreco.Rows[0]["PRECO1"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO1"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO1"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO1"]), drc[i]["IDMOV"], dtItens.Rows[ii]["NSEQITMMOV"] });
                                break;
                            case "2":
                                AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE TITMMOV SET PRECOUNITARIO = ?, VALORBRUTOITEM = ?, VALORBRUTOITEMORIG = ?, VALORLIQUIDO = ? WHERE IDMOV = ? AND NSEQITMMOV = ?", new object[] { Convert.ToDecimal(dtPreco.Rows[0]["PRECO2"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO2"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO2"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO2"]), drc[i]["IDMOV"], dtItens.Rows[ii]["NSEQITMMOV"] });
                                break;
                            case "3":
                                AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE TITMMOV SET PRECOUNITARIO = ?, VALORBRUTOITEM = ?, VALORBRUTOITEMORIG = ?, VALORLIQUIDO = ? WHERE IDMOV = ? AND NSEQITMMOV = ?", new object[] { Convert.ToDecimal(dtPreco.Rows[0]["PRECO3"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO3"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO3"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO3"]), drc[i]["IDMOV"], dtItens.Rows[ii]["NSEQITMMOV"] });
                                break;
                            case "4":
                                AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE TITMMOV SET PRECOUNITARIO = ?, VALORBRUTOITEM = ?, VALORBRUTOITEMORIG = ?, VALORLIQUIDO = ? WHERE IDMOV = ? AND NSEQITMMOV = ?", new object[] { Convert.ToDecimal(dtPreco.Rows[0]["PRECO4"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO4"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO4"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO4"]), drc[i]["IDMOV"], dtItens.Rows[ii]["NSEQITMMOV"] });
                                break;
                            case "5":
                                AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE TITMMOV SET PRECOUNITARIO = ?, VALORBRUTOITEM = ?, VALORBRUTOITEMORIG = ?, VALORLIQUIDO = ? WHERE IDMOV = ? AND NSEQITMMOV = ?", new object[] { Convert.ToDecimal(dtPreco.Rows[0]["PRECO5"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO5"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO5"]), Convert.ToDecimal(dtItens.Rows[ii]["QUANTIDADE"]) * Convert.ToDecimal(dtPreco.Rows[0]["PRECO5"]), drc[i]["IDMOV"], dtItens.Rows[ii]["NSEQITMMOV"] });
                                break;
                            default:
                                break;
                        }
                    }
                }
                #region Atualiza Valores
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get().Clone();
                conn.BeginTransaction();
                try
                {
                    DataTable dtValoresMovimento = conn.ExecQuery(@"SELECT ISNULL(VALORDESP, 0) VALORDESP, ISNULL(VALORBRUTO, 0) VALORBRUTO, ISNULL(PERCENTUALDESP, 0) PERCENTUALDESP, ISNULL(VALORSEGURO, 0) VALORSEGURO, ISNULL(VALORFRETE, 0) VALORFRETE, ISNULL(VALORDESC, 0) VALORDESC, ISNULL(VALOREXTRA1, 0) VALOREXTRA1, ISNULL(PERCENTUALDESC, 0) PERCENTUALDESC, ISNULL(VALOROUTROS, 0) VALOROUTROS, ISNULL(VALORLIQUIDO, 0) VALORLIQUIDO, ISNULL(PERCENTUALFRETE, 0) PERCENTUALFRETE, ISNULL(PERCENTUALEXTRA1, 0) PERCENTUALEXTRA1, ISNULL(PERCENTUALSEGURO, 0) PERCENTUALSEGURO FROM TMOV WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { drc[i]["IDMOV"], AppLib.Context.Empresa });

                    decimal PercentualDesp = Convert.ToDecimal(dtValoresMovimento.Rows[0]["PERCENTUALDESP"]);
                    decimal ValorDesp = Convert.ToDecimal(dtValoresMovimento.Rows[0]["VALORDESP"]);
                    decimal ValorSeguro = Convert.ToDecimal(dtValoresMovimento.Rows[0]["VALORSEGURO"]);
                    decimal ValorFrete = Convert.ToDecimal(dtValoresMovimento.Rows[0]["VALORFRETE"]);
                    decimal ValorDesc = Convert.ToDecimal(dtValoresMovimento.Rows[0]["VALORDESC"]);
                    decimal ValorExtra1 = Convert.ToDecimal(dtValoresMovimento.Rows[0]["VALOREXTRA1"]);
                    decimal PercentualDesc = Convert.ToDecimal(dtValoresMovimento.Rows[0]["PERCENTUALDESC"]);
                    decimal ValorOutros = Convert.ToDecimal(dtValoresMovimento.Rows[0]["VALOROUTROS"]);
                    decimal ValorLiquido = Convert.ToDecimal(dtValoresMovimento.Rows[0]["VALORLIQUIDO"]);
                    decimal PercentualFrete = Convert.ToDecimal(dtValoresMovimento.Rows[0]["PERCENTUALFRETE"]);
                    decimal PercentualExtra1 = Convert.ToDecimal(dtValoresMovimento.Rows[0]["PERCENTUALEXTRA1"]);
                    decimal PercentualSeguro = Convert.ToDecimal(dtValoresMovimento.Rows[0]["PERCENTUALSEGURO"]);

                    #region Valor Bruto

                    decimal ValorBruto = Convert.ToDecimal(conn.ExecGetField(0, @"
    SELECT 
    (
    ISNULL((
    SELECT SUM(QUANTIDADE * PRECOUNITARIO)
    FROM TITMMOV
    WHERE CODCOLIGADA = ?
    AND IDMOV = ?
    ),0)

    +

    ISNULL((
    SELECT SUM(VALOR)
    FROM TTRBMOV
    WHERE CODTRB = 'IPI'
    AND CODCOLIGADA = ?
    AND IDMOV = ?
    ),0)
    ) VALOR

    FROM GCOLIGADA 
    WHERE CODCOLIGADA = ?", AppLib.Context.Empresa, drc[i]["IDMOV"], AppLib.Context.Empresa, drc[i]["IDMOV"], AppLib.Context.Empresa));

                    #endregion

                    #region Despesa

                    if (ValorDesp != null)
                    {
                        if (ValorDesp != 0)
                        {
                            PercentualDesp = Arredonda(((ValorDesp / ValorBruto) * 100));
                        }
                        else
                        {
                            if (PercentualDesp != null)
                            {
                                if (PercentualDesp != 0)
                                {
                                    ValorDesp = Arredonda(((ValorBruto * PercentualDesp) / 100));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PercentualDesp != null)
                        {
                            if (PercentualDesp != 0)
                            {
                                ValorDesp = Arredonda(((ValorBruto * PercentualDesp) / 100));
                            }
                        }
                    }
                    #endregion

                    #region Seguro
                    if (ValorSeguro != null)
                    {
                        if (ValorSeguro != 0)
                        {
                            PercentualSeguro = Arredonda(((ValorSeguro / ValorBruto) * 100));
                        }
                        else
                        {
                            if (PercentualSeguro != null)
                            {
                                if (PercentualSeguro != 0)
                                {
                                    ValorSeguro = Arredonda(((ValorBruto * PercentualSeguro) / 100));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PercentualSeguro != null)
                        {
                            if (PercentualSeguro != 0)
                            {
                                ValorSeguro = Arredonda(((ValorBruto * PercentualSeguro) / 100));
                            }
                        }
                    }
                    #endregion

                    #region Frete
                    if (ValorFrete != null)
                    {
                        if (ValorFrete != 0)
                        {
                            PercentualFrete = Arredonda(((ValorFrete / ValorBruto) * 100));
                        }
                        else
                        {
                            if (PercentualFrete != null)
                            {
                                if (PercentualFrete != 0)
                                {
                                    ValorFrete = Arredonda(((ValorBruto * PercentualFrete) / 100));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PercentualFrete != null)
                        {
                            if (PercentualFrete != 0)
                            {
                                ValorFrete = Arredonda(((ValorBruto * PercentualFrete) / 100));
                            }
                        }
                    }
                    #endregion

                    #region Desconto
                    if (ValorDesc != null)
                    {
                        if (ValorDesc != 0)
                        {
                            PercentualDesc = Arredonda(((ValorDesc / ValorBruto) * 100));
                        }
                        else
                        {
                            if (PercentualDesc != null)
                            {
                                if (PercentualDesc != 0)
                                {
                                    ValorDesc = Arredonda(((ValorBruto * PercentualDesc) / 100));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PercentualDesc != null)
                        {
                            if (PercentualDesc != 0)
                            {
                                ValorDesc = Arredonda(((ValorBruto * PercentualFrete) / 100));
                            }
                        }
                    }
                    #endregion

                    #region Extra1
                    if (ValorExtra1 != null)
                    {
                        if (ValorExtra1 != 0)
                        {
                            PercentualExtra1 = Arredonda(((ValorExtra1 / ValorBruto) * 100));
                        }
                        else
                        {
                            if (PercentualExtra1 != null)
                            {
                                if (PercentualExtra1 != 0)
                                {
                                    ValorExtra1 = Arredonda(((ValorBruto * PercentualExtra1) / 100));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PercentualExtra1 != null)
                        {
                            if (PercentualExtra1 != 0)
                            {
                                ValorExtra1 = Arredonda(((ValorBruto * PercentualExtra1) / 100));
                            }
                        }
                    }
                    #endregion

                    #region Outros Valores

                    ValorOutros = ValorBruto + ValorDesp + ValorSeguro;

                    #endregion

                    #region Valor Liquido

                    ValorLiquido = ValorBruto + ValorDesp + ValorSeguro - ValorDesc + ValorFrete - ValorExtra1;

                    #endregion

                    #region Update Valores
                    conn.ExecTransaction(@"UPDATE TMOV SET 
                                VALORBRUTO = ?, VALORLIQUIDO = ?, VALOROUTROS = ?, 
                                PERCENTUALFRETE = ?, VALORFRETE = ?,
                                PERCENTUALSEGURO = ?, VALORSEGURO = ?,
                                PERCENTUALDESC = ?, VALORDESC = ?,
                                PERCENTUALDESP = ?, VALORDESP = ?,
                                PERCENTUALEXTRA1 = ?, VALOREXTRA1 = ?,
                                VALORBRUTOORIG = ?, VALORLIQUIDOORIG = ?, VALOROUTROSORIG = ?
                            WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[]{ ValorBruto, ValorLiquido, ValorOutros,
                            PercentualFrete, ValorFrete,
                            PercentualSeguro, ValorSeguro,
                            PercentualDesc, ValorDesc,
                            PercentualDesp, ValorDesp,
                            PercentualExtra1, ValorExtra1,
                            ValorBruto, ValorLiquido, ValorOutros,
                            AppLib.Context.Empresa,  drc[i]["IDMOV"]});
                    #endregion

                    conn.Commit();
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Rollback();
                }
            }
        }

        public decimal Arredonda(decimal? valor)
        {
            if (valor == null)
                return 0;
            else
                return Decimal.Round((decimal)valor, (int)2, MidpointRounding.AwayFromZero);
        }

        public void CancelarMovimento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8015", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    FormCancelarMovimento f = new FormCancelarMovimento();
                    f.Movimentos = grid1.GetDataRows();
                    f.ShowDialog();

                    grid1.toolStripButtonATUALIZAR_Click(this, null);
                }
            }
        }

        public void CopiaDeMovimento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8050", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string sql = String.Format(@"select case when COUNT(1) > 0 then 'NAO' else 'SIM' end as 'COPIAVEL' from TITMMOV TTM

                                             inner join TPRODUTO TP
                                             on TP.IDPRD = TTM.IDPRD

                                             where TP.INATIVO = 1
                                             and TTM.IDMOV = {0}", drc[0]["IDMOV"]);

                    Boolean Copiavel = MetodosSQL.GetField(sql, "COPIAVEL") == "SIM";


                    if (Copiavel)
                    {
                        FormOrcamentoCadastro f = new FormOrcamentoCadastro();
                        f.CopiaDeMovimento = true;
                        f.CODSERIE = "ORC";
                        f.Editar(grid1.GetDataRow(), true);

                        grid1.toolStripButtonATUALIZAR_Click(this, null);

                        if (f.isCancelado == false)
                        {
                            if (MessageBox.Show("Deseja visualizar o Orçamento?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                ImprimirOrcamento();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este movimento possui itens inativos e por isso não pode ser copiado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void CopiaMovimentoByPedido(object sender, EventArgs e)
        {
            // Valida acesso ao Processo
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8050", AppLib.Context.Perfil))
            {
                New.Forms.Process.frmCopiaMovimento frm = new New.Forms.Process.frmCopiaMovimento();
                //frm.dtMovimento = grid1.gridControl1.DataSource as DataTable;
                frm.ShowDialog();

                if (frm.rowMovimento != null)
                {
                    FormOrcamentoCadastro f = new FormOrcamentoCadastro();
                    f.CopiaDeMovimento = true;
                    f.CODSERIE = "ORC";
                    f.Editar(frm.rowMovimento, true);

                    grid1.toolStripButtonATUALIZAR_Click(this, null);

                    if (f.isCancelado == false)
                    {
                        if (MessageBox.Show("Deseja visualizar o Orçamento?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ImprimirOrcamento();
                        }
                    }
                }
            }
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormOrcamentoCadastro frm = new FormOrcamentoCadastro();
            frm.CODSERIE = "ORC";
            frm.isNovoRegistro = true;
            frm.Novo();
            //FrmCad.Novo();

            if (frm.isCancelado == false)
            {
                if (MessageBox.Show("Deseja visualizar o Orçamento?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    novoOrcamento = true;
                    IDMOV = frm.IdMov;
                    ImprimirOrcamento(sender, e);
                }
            }
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            if (grid1.gridView1.SelectedRowsCount <= 0)
            {
                return;
            }

            FormOrcamentoCadastro frm = new FormOrcamentoCadastro();
            frm.CODSERIE = "ORC";
            frm.row = grid1.GetDataRow();
            frm.Editar(grid1.bs);
            //new FormOrcamentoCadastro().Editar(grid1.bs);
            //FrmCad.Editar(grid1.bs);

            if (frm.isCancelado == false)
            {
                if (MessageBox.Show("Deseja visualizar o Orçamento?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ImprimirOrcamento(sender, e);
                }
            }
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            MessageBox.Show("Não é possível realizar exclusão de orçamentos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void grid1_EventoClick(object sender, EventArgs e)
        {
            if (AnexoItemMovimento)
            {
                ItemMovimento(this, null);
            }

            if (AnexoMail)
            {
                MailMovimento(this, null);
            }

        }

        private void FormOrcamentoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void grid1_Load(object sender, EventArgs e)
        {

        }

        private void grid1_AposAtualizar(object sender, EventArgs e)
        {
            OrdenaGridView();
            FormataValoresGridView();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView View = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //if (e.RowHandle >= 0)
            //{
            //    string CodSerie = View.GetRowCellDisplayText(e.RowHandle, View.Columns["SERIE"]);

            //    if (CodSerie == "PVP" || CodSerie == "COT")
            //    {
            //        string Aprovacao = View.GetRowCellDisplayText(e.RowHandle, View.Columns["APROVACAO"]);

            //        if (Aprovacao == "VERIFICAR")
            //        {
            //            e.Appearance.BackColor = Color.Red;
            //        }
            //    }
            //}
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.ColumnType == typeof(decimal))
            {
                gridView1.Columns[e.Column.FieldName].AppearanceCell.TextOptions.RightToLeft = true;
            }
        }

        private void FormOrcamentoVisao_Enter(object sender, EventArgs e)
        {

        }

        private void FormOrcamentoVisao_Shown(object sender, EventArgs e)
        {

        }

        private void gridView1_EndSorting(object sender, EventArgs e)
        {

        }

        #region Métodos

        private void FormataValoresGridView()
        {
            // Formata as colunas
            for (int i = 0; i < grid1.gridView1.Columns.Count; i++)
            {
                if (grid1.gridView1.Columns[i].ColumnType == typeof(decimal))
                {
                    grid1.gridView1.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    grid1.gridView1.Columns[i].AppearanceCell.TextOptions.RightToLeft = true;
                }
            }
        }

        private void OrdenaGridView()
        {
            grid1.gridView1.ClearSorting();
            grid1.gridView1.Columns["IDMOV"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

            //DataTable dt = grid1.gridControl1.DataSource as DataTable;

            //DataView dv = dt.DefaultView;

            //dv.Sort = "IDMOV DESC";

            //dt = dv.ToTable();

            //grid1.bs.DataSource = dt;
            //grid1.gridControl1.DataSource = dt;
        }

        private bool ValidaLimiteCredito(string codCliente)
        {
            decimal limiteCredito = 0;
            decimal valorAberto = 0;
            decimal pedidosFaturar = 0;
            decimal limiteDisponivel = 0;

            limiteCredito = Convert.ToDecimal(AppLib.Context.poolConnection.Get().ExecGetField(0.00, @"SELECT 
	                                                                                                        FCFO.LIMITECREDITO LIMITE 
                                                                                                       FROM 
	                                                                                                       FCFO 
                                                                                                       WHERE 
	                                                                                                       (CODCOLIGADA = ? OR CODCOLIGADA = 0) 
                                                                                                       AND CODCFO = ?", new object[] { AppLib.Context.Empresa, codCliente }));

            valorAberto = Convert.ToDecimal(AppLib.Context.poolConnection.Get().ExecGetField(0.00, @"SELECT 
	                                                                                                     SUM(VALORORIGINAL - VALORBAIXADO) FINANCEIRO 
                                                                                                     FROM 
	                                                                                                     FLAN 
                                                                                                     WHERE 
	                                                                                                     CODCOLIGADA = ?
                                                                                                     AND CODCFO = ?
                                                                                                     AND  PAGREC = 1 
                                                                                                     AND STATUSLAN IN (0,4) 
                                                                                                     AND CODTDO = 'NF-e'", new object[] { AppLib.Context.Empresa, codCliente }));

            pedidosFaturar = Convert.ToDecimal(AppLib.Context.poolConnection.Get().ExecGetField(0.00, @"SELECT 
	                                                                                                       SUM(VALORORIGINAL - VALORBAIXADO) PEDIDOS 
                                                                                                       FROM 
	                                                                                                       FLAN 
                                                                                                       WHERE 
	                                                                                                       CODCOLIGADA = ?
                                                                                                       AND CODCFO = ? 
                                                                                                       AND PAGREC = 1 
                                                                                                       AND STATUSLAN IN (0,4) 
                                                                                                       AND CODTDO = 'PRVV'", new object[] { AppLib.Context.Empresa, codCliente }));

            limiteDisponivel = limiteCredito - valorAberto - pedidosFaturar;

            if (limiteCredito != 0)
            {
                if (limiteDisponivel <= 0)
                {
                    XtraMessageBox.Show("Limite de Crédito insuficiente para realizar processo de faturamento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
