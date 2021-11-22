using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using DevExpress.XtraReports.UI;
using AppFatureClient.Classes;

namespace AppFatureClient
{
    public partial class FormOrdemProducaoVisao : AppLib.Windows.FormVisao
    {
        private static FormOrdemProducaoVisao _instance = null;
        DateTime data;
        int qtde = 0;
        public static FormOrdemProducaoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormOrdemProducaoVisao();
            return _instance;
        }

        //private bool AnexoItemMovimento;

        public FormOrdemProducaoVisao()
        {
            InitializeComponent();
        }

        private void FormOrdemProducaoVisao_Load(object sender, EventArgs e)
        {
            // AnexoItemMovimento = false;

            grid1.GetAnexos().Add("Item do Movimento", null, ItemMovimento);
            grid1.GetProcessos().Add("Imprimir Ordem de Produção", null, ImprimirOrdemProducao);
            grid1.GetProcessos().Add("Imprimir Pedido Fiscal", null, ImprimirMovimentoFiscal);
            grid1.GetProcessos().Add("Programar Carregamento", null, agendarProgramacao);
            grid1.GetAnexos().Add("Consultar Programação Carregamento", null, consultaProgramacao);
            grid1.GetAnexos().Add("Fechar Anexo", null, FecharAnexo);
            grid1.GetAnexos().Add("Fechar todos Anexos", null, FecharAnexos);


            grid1.GetProcessos().Add("Cancelar Movimento", null, CancelarMovimento);
            //grid1.GetProcessos().Add("Alterar Valor Financeiro", null, AlterarFinanceiro);
            grid1.GetProcessos().Add("Rastreamento de Movimentos", null, Rastreabilidade);
            //Criação dos processos na grid de programação do carregamento.
            gridData1.GetProcessos().Add("Programar Carregamento", null, programacaoCarregamento);
            //Criação dos processos na grid de consulta da programação do carregamento.
            gridData2.GetProcessos().Add("Alterar data da programação.", null, alterarDataProgramacao);
            gridData2.GetProcessos().Add("Alterar quantidade da programação.", null, alterarQuantidadeProgramacao);
            hideTabPage hide = new hideTabPage();
            hide.HideTabPage(tabPage1, tabControl1);
            hide.HideTabPage(tabPage2, tabControl1);
            hide.HideTabPage(tabPage3, tabControl1);

        }
        private void alterarDataProgramacao(object sender, EventArgs e)
        {
            AppLib.ORM.Jit ZPROGRAMACAOCARREGAMENTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZPROGRAMACAOCARREGAMENTO");
            if (MessageBox.Show("Deseja alterar a data de programação nos itens selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                AppLib.Windows.FormMessagePrompt promt = new AppLib.Windows.FormMessagePrompt();
                promt.ShowDialog();
                if (promt.confirmacao.Equals(AppLib.Global.Types.Confirmacao.Cancelar))
                {
                    return;
                }

                try
                {
                    data = Convert.ToDateTime(promt.textBox1.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Favor digitar uma data válida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataRowCollection drc = gridData2.GetDataRows();
                for (int i = 0; i < drc.Count; i++)
                {
                    ZPROGRAMACAOCARREGAMENTO.Set("IDPROGRAMACAOCARREGAMENTO", int.Parse(drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString()));
                    ZPROGRAMACAOCARREGAMENTO.Set("DATAPROGRAMADA", data);
                    ZPROGRAMACAOCARREGAMENTO.Set("DATAALTERACAO", AppLib.Context.poolConnection.Get().GetDateTime());
                    ZPROGRAMACAOCARREGAMENTO.Save();
                }
                new ValidaEnvioEmail().validaEnvio(data, gridData2.GetDataRows(), "data", 0);


                MessageBox.Show("Alteração realizada com sucesso.", "Informação do sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridData2.Atualizar(false);
            }
        }
        private void alterarQuantidadeProgramacao(object sender, EventArgs e)
        {
            DataRow dr = null;
            try
            {
                dr = gridData2.GetDataRow();
            }
            catch { }

            if (dr == null)
                return;
            AppLib.ORM.Jit ZPROGRAMACAOCARREGAMENTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZPROGRAMACAOCARREGAMENTO");
            if (MessageBox.Show("Deseja alterar a quantidade do item selecionado?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                AppLib.Windows.FormMessagePrompt promt = new AppLib.Windows.FormMessagePrompt();
                promt.ShowDialog();
                if (promt.confirmacao.Equals(AppLib.Global.Types.Confirmacao.Cancelar))
                {
                    return;
                }
                try
                {
                    qtde = int.Parse(promt.textBox1.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Favor digitar uma quantidade válida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Select pra saber se a quantidade é maior do que tem cadastrado na TITMMOV
                string sql = @"-- SELECT PAI
SELECT IDMOV, NSEQITMMOV, QTD_PEDIDO, QTD_SALDO, CONVERT(DECIMAL(15,4),QTD_PROG) QTD_PROG FROM (
	--SELECT FILHO
	SELECT SUBLISTA.*,
		(QTD_PEDIDO -  ISNULL(QTD_PROG, 0)) QTD_SALDO
	FROM
	--CAMPOS SELECT FILHO
		(SELECT
		TITMMOV.IDMOV,
		TITMMOV.NSEQITMMOV,
		TITMMOV.QUANTIDADE QTD_PEDIDO,
        --SUB SELECT PRA TRAZER UM CAMPO  
		(SELECT SUM(ZPROGRAMACAOCARREGAMENTO.QTDE)
		FROM  ZPROGRAMACAOCARREGAMENTO
		WHERE
		 ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
		AND ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV
		AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
		 ) QTD_PROG
		--INFORMANDO AS TABELAS DA PESQUISA
		FROM  TITMMOV
		) SUBLISTA
--FECHA SELECT PAI		
) LISTA
--WHERE SELECT PAI
WHERE LISTA.IDMOV = ? and LISTA.NSEQITMMOV = ?";

                DataTable retorno = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dr["IDMOV"].ToString(), dr["NSEQITMMOV"].ToString() });
                decimal saldo = Convert.ToDecimal(retorno.Rows[0]["QTD_SALDO"].ToString());

                if (saldo.Equals(0))
                {
                    if (qtde > Convert.ToDecimal(retorno.Rows[0]["QTD_PROG"].ToString()))
                    {
                        MessageBox.Show("Não a saldo suficiente pra realizar essa transação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (saldo > 0)
                {

                    if (qtde > (decimal.Parse(retorno.Rows[0]["QTD_PROG"].ToString()) + saldo))
                    {
                        MessageBox.Show("Não a saldo suficiente pra realizar essa transação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                ZPROGRAMACAOCARREGAMENTO.Set("IDPROGRAMACAOCARREGAMENTO", int.Parse(dr[0].ToString()));
                ZPROGRAMACAOCARREGAMENTO.Set("QTDE", qtde);
                ZPROGRAMACAOCARREGAMENTO.Set("DATAALTERACAO", AppLib.Context.poolConnection.Get().GetDateTime());
                ZPROGRAMACAOCARREGAMENTO.Save();

                new ValidaEnvioEmail().validaEnvio(Convert.ToDateTime(dr["DATAPROGRAMADA"]), gridData2.GetDataRows(), "quantidade", qtde);

                MessageBox.Show("Alteração realizada com sucesso.", "Informação do sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridData2.Atualizar(false);
            }
        }

        public void consultaProgramacao(object sender, EventArgs e)
        {
            DataRow dr = null;
            try
            {
                dr = grid1.GetDataRow();
            }
            catch { }

            if (dr == null)
                return;
            //selecao = 3;
            //AnexoItemMovimento = false;
            hideTabPage hide = new hideTabPage();
            tabPage3.Text = "Consultar Programação Carregamento";
            hide.ShowTabPage(tabPage3, tabControl1);


            if (splitContainer1.Panel2Collapsed.Equals(true))
            {
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                this.gridData2.Atualizar(false);
            }
        }
        public void agendarProgramacao(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8033", AppLib.Context.Perfil))
            {
                DateTime data = System.DateTime.Now;
                DataRowCollection drc = grid1.GetDataRows();
                FormProgramacaoCarregamento f = new FormProgramacaoCarregamento();
                f.drc = drc;
                f.ShowDialog();
                //gridData1.Atualizar(false);
                //DataRow dr = null;
                //try
                //{
                //    dr = grid1.GetDataRow();
                //}
                //catch { }

                //if (dr == null)
                //    return;
                ////Verifica o status 
                //string sql = @"SELECT STATUS FROM ZVWORDEMPRODUCAO WHERE CODCOLIGADA = ? and IDMOV = ?";
                //if (!AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, sql, new object[] { AppLib.Context.Empresa, dr["IDMOV"] }).Equals("FATURADO"))
                //{
                //    hideTabPage hide = new hideTabPage();
                //    tabPage2.Text = "Programar Carregamento";
                //    //hide.HideTabPage(tabPage1, tabControl1);
                //    //hide.HideTabPage(tabPage3, tabControl1);
                //    hide.ShowTabPage(tabPage2, tabControl1);

                //    //this.gridData1.Atualizar(false);

                //    if (splitContainer1.Panel2Collapsed.Equals(true))
                //    {
                //        splitContainer1.Panel2Collapsed = false;
                //    }
                //    else
                //    {
                //        this.gridData1.Atualizar(false);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Item já Faturado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}


            }
        }
        //Programação do Carregamento
        private void programacaoCarregamento(object sender, EventArgs e)
        {
            DateTime data = System.DateTime.Now;
            DataRowCollection drc = gridData1.GetDataRows();
            FormProgramacaoCarregamento f = new FormProgramacaoCarregamento();
            f.drc = drc;
            f.ShowDialog();
            gridData1.Atualizar(false);
        }

        public void FecharAnexo(object sender, EventArgs e)
        {
            hideTabPage hide = new hideTabPage();
            try
            {
                hide.HideTabPage(tabControl1.SelectedTab, tabControl1);
            }
            catch (Exception)
            {
            }

            if (tabControl1.TabPages.Count.Equals(0))
            {
                //AnexoItemMovimento = false;
                //selecao = 0;
                splitContainer1.Panel2Collapsed = true;
            }
        }
        public void FecharAnexos(object sender, EventArgs e)
        {
            try
            {
                //  AnexoItemMovimento = false;
                hideTabPage hide = new hideTabPage();
                hide.HideTabPage(tabPage1, tabControl1);
                hide.HideTabPage(tabPage2, tabControl1);
                hide.HideTabPage(tabPage3, tabControl1);
                splitContainer1.Panel2Collapsed = true;
                //selecao = 0;
            }
            catch { }
        }
        public void ItemMovimento(object sender, EventArgs e)
        {
            hideTabPage hide = new hideTabPage();
            //hide.HideTabPage(tabPage2, tabControl1);
            //hide.HideTabPage(tabPage3, tabControl1);
            tabPage1.Text = "Item de Movimento";
            hide.ShowTabPage(tabPage1, tabControl1);

            DataRow dr = null;
            // selecao = 0;
            try
            {
                dr = grid1.GetDataRow();
            }
            catch { }

            if (dr == null)
                return;

            //   AnexoItemMovimento = true;

            string ConsultaFiltro = string.Empty;

            try
            {
                splitContainer1.Panel2Collapsed = false;

                ConsultaFiltro = @"
SELECT
TITMMOV.NSEQITMMOV,
TITMMOV.IDPRD,
TPRD.CODIGOPRD,
ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
TITMMOV.CODUND,
CAST(TITMMOV.QUANTIDADEORIGINAL AS NUMERIC(15,2)) QUANTIDADE,
CAST(TITMMOV.PRECOUNITARIO AS NUMERIC(15,2)) PRECOUNITARIO,
TPRD.NUMEROCCF,

CASE 
WHEN TITMMOVCOMPL.PRODPRICIPAL = 'NÃO' THEN 0
WHEN TITMMOVCOMPL.PRODPRICIPAL = 'SIM' THEN 1
ELSE NULL
END PRODPRICIPAL,

TITMMOVCOMPL.PRODPRICIPAL PRODUTO_COMPOSTO,

CASE 
WHEN TITMMOVCOMPL.TIPOMAT = 0 THEN 'SIM'
WHEN TITMMOVCOMPL.TIPOMAT = 1 THEN 'NÃO'
ELSE NULL
END MATERIAL_INTERLIGACAO,

TITMMOVCOMPL.SEQUENCIAL

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


                // Grid sempre mostrar filtro no rodapé
                // gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;

                // Grid zebrada
                // gridView1.OptionsView.EnableAppearanceEvenRow = true;
                // gridView1.Appearance.EvenRow.BackColor = Color.Thistle;   
            }
            catch (Exception ex)
            {
                new AppLib.Windows.FormExceptionSQL().Mostrar("Erro ao executar consulta.", ConsultaFiltro, ex);
            }
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormOrdemProducaoCadastro frm = new FormOrdemProducaoCadastro();
            frm.row = grid1.GetDataRow();
            frm.Editar(grid1.bs);
            //new FormOrdemProducaoCadastro().Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {

            if (MessageBox.Show("Deseja excluir o movimento ?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }




            //Verifciar se existe carregamento para o IDMOV
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataRowCollection Movimentos = grid1.GetDataRows();
                if (Movimentos != null)
                {
                    for (int i = 0; i < Movimentos.Count; i++)
                    {
                        string sql = String.Format(@"select COUNT(1) as 'CONT' from ZTMOVCOMISSAO where IDMOVOP = {0} and NCARTA is not null", Movimentos[i]["IDMOV"]);

                        if (int.Parse(MetodosSQL.GetField(sql, "CONT")) == 0)
                        {
                            sql = @"SELECT Count(*) FROM ZPROGRAMACAOCARREGAMENTO WHERE CODCOLIGADA = ? AND IDMOV = ?";
                            string retorno = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sql, new Object[] { Movimentos[i]["CODCOLIGADA"], Movimentos[i]["IDMOV"] }).ToString();
                            if (!retorno.Equals("0"))
                            {
                                MessageBox.Show("Não é permitido a exclusão de movimentos que já que possuem programação efetuada.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue;
                            }
                            excluirItem(Convert.ToInt32(Movimentos[i]["CODCOLIGADA"]), Convert.ToInt32(Movimentos[i]["IDMOV"]));

                            sql = String.Format(@"update ZTMOVCOMISSAO
                                              set IDMOVOP = null
                                              where IDMOVOP = {0}", Movimentos[i]["IDMOV"]);

                            MetodosSQL.ExecQuery(sql);
                        }
                        else
                        {
                            MessageBox.Show("Não é permitido exluir o pedido " + Movimentos[i]["NUMEROMOV"] + " pois ele está associado ao processo de comissão", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void excluirItem(int codcoligada, int idmov)
        {
            AppInterop.MovExclusaoPar exclusao = new AppInterop.MovExclusaoPar();
            exclusao.CodColigada = AppLib.Context.Empresa;
            exclusao.CodSistemaLogado = "T";
            exclusao.CodUsuarioLogado = AppLib.Context.Usuario;
            exclusao.IdMov = Convert.ToInt32(idmov);

            AppInterop.Message msgexclusao;
            if (FatureContexto.Remoto)
            {
                msgexclusao = new Util().ConvertToMessage(FatureContexto.ServiceSoapClient.ExcluiMovimento(AppLib.Context.Usuario, AppLib.Context.Senha, new Util().ConvertToWSMovExclusaoPar(exclusao)));
            }
            else
            {
                msgexclusao = FatureContexto.ServiceClient.ExcluiMovimento(AppLib.Context.Usuario, AppLib.Context.Senha, exclusao);
            }

            this.Cursor = Cursors.Default;

            if (int.Parse(msgexclusao.Retorno.ToString()) > 0)
            {
                string sMensagem = string.Empty;
                bool bExclusao = false;
                while (!bExclusao)
                {
                    string sSql = @"SELECT STATUS FROM ZTMOVCANEXC WHERE CODCOLIGADA = ? AND IDMOV = ?";
                    string sStatus = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sSql, new Object[] { codcoligada, idmov }).ToString();

                    if (sStatus != "A")
                    {
                        if (sStatus == "R")
                        {
                            //System.Threading.Thread.Sleep(5000); //5s                                    
                        }

                        if (sStatus == "S")
                        {
                            bExclusao = true;
                            msgexclusao.Mensagem = "Exclusão realizada com sucesso";
                        }

                        if (sStatus == "E")
                        {
                            bExclusao = true;

                            sSql = "SELECT MENSAGEM FROM ZTMOVCANEXC WHERE OPERACAO = 'E' AND CODCOLIGADA = " + codcoligada + " AND IDMOV = " + idmov;
                            msgexclusao.Mensagem = AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { }).ToString();
                            throw new Exception(msgexclusao.Mensagem);
                        }
                    }
                    else
                    {
                        //System.Threading.Thread.Sleep(5000); //5s
                    }
                }

                MessageBox.Show(msgexclusao.Mensagem);
            }
            else
            {
                throw new Exception(msgexclusao.Mensagem);
            }
        }
        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormOrdemProducaoCadastro().Novo();
        }

        public void ImprimirOrdemProducao(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8016", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DateTime Impressao = DateTime.Now;

                    RelOrdemProducao relatorio = new RelOrdemProducao();
                    relatorio.SelectRow = grid1.GetDataRow();
                    relatorio.Conexao = grid1.Conexao;
                    relatorio.Impressao = Impressao;


                    FormPrintPreview f = new FormPrintPreview();
                    f.SelectRow = grid1.GetDataRow();
                    f.Conexao = grid1.Conexao;
                    f.Impressao = Impressao;
                    f.relatorio = relatorio;
                    f.ShowDialog();
                }
            }
        }

        public void CancelarMovimento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8017", AppLib.Context.Perfil))
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

        public void AlterarFinanceiro(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8018", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    if (grid1.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    FormLancamentoFinanceiro f = new FormLancamentoFinanceiro();
                    f.Movimento = grid1.GetDataRow();
                    f.ShowDialog();
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

        private void grid1_EventoClick(object sender, EventArgs e)
        {
            if (tabControl1.TabCount.Equals(0))
            {
                return;
            }
            if (tabControl1.SelectedTab.Text.Equals("Item de Movimento"))
            {
                ItemMovimento(this, null);
                return;
            }

            if (tabControl1.SelectedTab.Text.Equals("Programar Carregamento"))
            {
                agendarProgramacao(this, null);
                return;
            }
            if (tabControl1.SelectedTab.Text.Equals("Consultar Programação Carregamento"))
            {
                consultaProgramacao(this, null);
                return;
            }

        }

        private void FormOrdemProducaoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {

            try
            {
                gridData1.Parametros = new Object[] { grid1.GetDataRow()["IDMOV"] };

            }
            catch (Exception)
            {
                gridData1.Parametros = new object[] { 0 };
            }


        }

        private void gridData2_Excluir(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8043", AppLib.Context.Perfil))
            {
                if (MessageBox.Show("Para efetuar essa exclusão, todos os itens do romaneio serão excluído.\nDeseja realmente exluir esse item?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    DataRowCollection drc = gridData2.GetDataRows();
                    for (int i = 0; i < drc.Count; i++)
                    {
                        //AppLib.Context.poolConnection.Get().ExecQuery("delete ZROMANEIO where IDPROGRAMACAOCARREGAMENTO = ?", drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString());
                        AppLib.Context.poolConnection.Get().ExecQuery("delete ZPROGRAMACAOCARREGAMENTO where IDPROGRAMACAOCARREGAMENTO = ?", drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString());
                    }
                }
                gridData2.Atualizar(false);
            }

        }

        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            try
            {
                gridData2.Parametros = new Object[] { grid1.GetDataRow()["IDMOV"] };

            }
            catch (Exception)
            {
                gridData2.Parametros = new object[] { 0 };
            }
        }

        private void grid1_AposAtualizar(object sender, EventArgs e)
        {
            try
            {
                if (MetodosSQL.VerificaPermissao("APP8049"))
                {
                    grid1.gridView1.Columns.Remove(grid1.gridView1.Columns["R$_BRUTO"]);
                    grid1.gridView1.Columns.Remove(grid1.gridView1.Columns["R$_LIQUIDO"]);
                    grid1.gridView1.Columns.Remove(grid1.gridView1.Columns["R$_LIQUIDO_ORG"]);
                    grid1.gridView1.Columns.Remove(grid1.gridView1.Columns["R$_SUBTOTAL"]);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public void ImprimirMovimentoFiscal(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "APP8044", AppLib.Context.Perfil))
            {
                try
                {
                    DataRowCollection DR = grid1.GetDataRows();

                    if (DR.Count > 1)
                    {
                        foreach (DataRow x in DR)
                        {
                            RelPedido relatorio = new RelPedido();
                            relatorio.SelectRow = x;
                            relatorio.Conexao = grid1.Conexao;

                            using (ReportPrintTool printTool = new ReportPrintTool(relatorio))
                            {
                                printTool.Print();
                            }
                        }
                    }
                    else
                    {
                        RelPedido relatorio = new RelPedido();
                        relatorio.SelectRow = grid1.GetDataRow();
                        relatorio.Conexao = grid1.Conexao;

                        using (ReportPrintTool printTool = new ReportPrintTool(relatorio))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
