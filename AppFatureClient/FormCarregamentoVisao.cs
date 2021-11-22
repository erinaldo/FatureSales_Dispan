using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AppLib.ORM;
using AppLib;

namespace AppFatureClient
{
    public partial class FormCarregamentoVisao : AppLib.Windows.FormVisao
    {
        private static FormCarregamentoVisao _instance = null;

        public static FormCarregamentoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormCarregamentoVisao();
            return _instance;
        }

        public FormCarregamentoVisao()
        {
            InitializeComponent();

        }
        private void ConcluirCarregamento(object sender, EventArgs e)
        {
            DataRowCollection drc = grid1.GetDataRows();
            AppLib.ORM.Jit ZCARREGAMENTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZCARREGAMENTO");
            if (MessageBox.Show("Deseja dar baixa nos itens selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                for (int i = 0; i < drc.Count; i++)
                {
                    string sql = @"SELECT 
                    ZCARREGAMENTOITEMS.IDCARREGAMENTOITEMS,
                    FCFO.NOMEFANTASIA AS CLIENTE,
                    ZCARREGAMENTOITEMS.IDMOV,
                    ZCARREGAMENTOITEMS.NSEQITMMOV,
                    TPRODUTO.NOMEFANTASIA,
                    TITMMOV.IDPRDCOMPOSTO,
                    ZCARREGAMENTOITEMS.CODCOLIGADA,
                    CONVERT(DECIMAL(12,4),ZCARREGAMENTOITEMS.QTDE) AS QTD 
                    FROM
                    ZCARREGAMENTOITEMS 
                    INNER JOIN TITMMOV ON ZCARREGAMENTOITEMS.IDMOV = TITMMOV.IDMOV AND ZCARREGAMENTOITEMS.NSEQITMMOV = TITMMOV.NSEQITMMOV AND ZCARREGAMENTOITEMS.CODCOLIGADA = TITMMOV.CODCOLIGADA
                    INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD 
                    INNER JOIN TMOV ON ZCARREGAMENTOITEMS.IDMOV = TMOV.IDMOV
                    INNER JOIN FCFO ON FCFO.CODCFO = TMOV.CODCFO
                    WHERE 
                    ZCARREGAMENTOITEMS.IDCARREGAMENTO = ?
                    AND CODCOLIGADA = ?";
                    DataTable retorno = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { Convert.ToInt32(drc[i]["IDCARREGAMENTO"].ToString()), AppLib.Context.Empresa });
                    sql = @"UPDATE TITMMOV SET QUANTIDADE = 0, QUANTIDADEARECEBER = 0
WHERE IDMOV = ? AND IDPRDCOMPOSTO = ? AND CODCOLIGADA = ? AND TITMMOV.NSEQITMMOV not in (SELECT TITMMOV.NSEQITMMOV FROM ZCARREGAMENTOITEMS, TITMMOV, ZCARREGAMENTO
WHERE
ZCARREGAMENTOITEMS.IDMOV = TITMMOV.IDMOV
AND ZCARREGAMENTOITEMS.CODCOLIGADA = TITMMOV.CODCOLIGADA
AND ZCARREGAMENTOITEMS.NSEQITMMOV = TITMMOV.NSEQITMMOV
AND ZCARREGAMENTOITEMS.IDCARREGAMENTO = ZCARREGAMENTO.IDCARREGAMENTO
AND ZCARREGAMENTO.IDCARREGAMENTO = ? AND ZCARREGAMENTOITEMS.CODCOLIGADA = ? AND ZCARREGAMENTOITEMS.IDMOV = ?)
";
                    for (int I = 0; I < retorno.Rows.Count; I++)
                    {
                        int resultado = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { retorno.Rows[I]["IDMOV"].ToString(), Convert.ToInt32(retorno.Rows[I]["IDPRDCOMPOSTO"].ToString()), AppLib.Context.Empresa, drc[i]["IDCARREGAMENTO"].ToString(), AppLib.Context.Empresa, retorno.Rows[I]["IDMOV"].ToString() });
                    }
                    ZCARREGAMENTO.Set("IDCARREGAMENTO", int.Parse(drc[i]["IDCARREGAMENTO"].ToString()));
                    ZCARREGAMENTO.Set("DATABAIXA", AppLib.Context.poolConnection.Get().GetDateTime());
                    ZCARREGAMENTO.Set("STATUS", "COMPLETO");
                    ZCARREGAMENTO.Save();
                }
            }
        }
        private void grid1_Novo(object sender, EventArgs e)
        {
            FormCarregamentoCadastro f = new FormCarregamentoCadastro();
            f.Novo();
            //FormCarregamentoCadastro f = FormCarregamentoCadastro();
            //////f.MdiParent = ;
            ////f.Novo();
            //f.Show()
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            DataRow dr = null;
            dr = grid1.GetDataRow();
            if (dr != null)
            {
                if (MessageBox.Show("Deseja realmente exluir esse carregamento?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    //Apagar os itens da ZCARREGAMENTOITENS E ZCARREGAMENTOITEMS
                    AppLib.Context.poolConnection.Get().ExecTransaction("DELETE ZCARREGAMENTO WHERE IDCARREGAMENTO = ?", dr["IDCARREGAMENTO"].ToString());
                    AppLib.Context.poolConnection.Get().ExecTransaction("UPDATE ZPROGRAMACAOCARREGAMENTO SET IDCARREGAMENTO = NULL, QTDCARREGADA = 0, QTDE = QTDCARREGADA, CARREGAMENTO = 'N', REPROGRAMAR = NULL, CARREGAR = NULL, DATABAIXA = NULL WHERE IDCARREGAMENTO = ?", new object[] { dr["IDCARREGAMENTO"].ToString() });
                    MessageBox.Show("Carregamento excluído com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormCarregamentoCadastro f = new FormCarregamentoCadastro();
            f.row = grid1.GetDataRow();
            f.Editar(grid1.bs);
        }

        private void FormCarregamentoVisao_Load(object sender, EventArgs e)
        {

            //grid1.GetProcessos().Add("Concluir Carregamento", null, baixaCarregamento);
            grid1.GetProcessos().Add("Reabrir Carregamento", null, reabrirCarregamento);
            grid1.GetAnexos().Add("Item de Carregamento", null, itemMovimento);
            grid1.GetAnexos().Add("Fechar Anexos", null, FecharAnexos);
            //grid1.GetProcessos().Add("teste Carregamento", null, ConcluirCarregamento);
            //grid1.GetProcessos().Add("Cancela Baixa no carregamento", null, cancelaBaixa);
            //grid1.GetProcessos().Add(new ToolStripSeparator());
            grid1.GetProcessos().Add("Imprimir Autorização de Carregamento", null, relatorio);
            //grid1.GetProcessos().Add("Relação de Carregamento Parcial", null, relacaoParcial);
        }
        private void reabrirCarregamento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8047", AppLib.Context.Perfil))
            {
                DataRowCollection drc = null;
                try
                {
                    drc = grid1.GetDataRows();
                }
                catch (Exception)
                {
                }
                if (drc.Equals(null))
                {
                    return;
                }
                if (MessageBox.Show("Deseja reabrir os itens selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    for (int i = 0; i < drc.Count; i++)
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE ZCARREGAMENTO SET STATUS = 'EM ABERTO', DATABAIXA = NULL, STATUSCARREGADO = NULL WHERE IDCARREGAMENTO = ?", new object[] { int.Parse(drc[i]["IDCARREGAMENTO"].ToString()) });
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE ZPROGRAMACAOCARREGAMENTO SET CARREGAMENTO = 'S', DATABAIXA = NULL, REPROGRAMAR = NULL, QTDCARREGADA = QTDE, QTDE = 0.00, STATUSBAIXA = NULL, IDCARREGAMENTO = IDCARREGAMENTOANTERIOR, IDCARREGAMENTOANTERIOR = NULL WHERE CODCOLIGADA = 1 AND IDCARREGAMENTOANTERIOR = ? AND REPROGRAMAR = 1", new object[] { int.Parse(drc[i]["IDCARREGAMENTO"].ToString()) });
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE ZPROGRAMACAOCARREGAMENTO SET DATABAIXA = NULL, STATUSBAIXA = NULL WHERE CODCOLIGADA = 1 AND IDCARREGAMENTO = ? AND REPROGRAMAR IS NULL", new object[] { int.Parse(drc[i]["IDCARREGAMENTO"].ToString()) });
                    }
                    MessageBox.Show("Carregamento(s) reaberto(s) com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    grid1.Atualizar(false);
                }
            }
            //if (new AppLib.Security.Access().Processo("Start", "APP8034", AppLib.Context.Perfil))
            //{
            //    DataRowCollection drc = null;
            //    try
            //    {
            //        drc = grid1.GetDataRows();
            //    }
            //    catch (Exception)
            //    {
            //    }
            //    if (drc.Equals(null))
            //    {
            //        return;
            //    }
            //    AppLib.ORM.Jit ZCARREGAMENTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZCARREGAMENTO");
            //    if (MessageBox.Show("Deseja reabrir os itens selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            //    {
            //        for (int i = 0; i < drc.Count; i++)
            //        {
            //            ZCARREGAMENTO.Set("IDCARREGAMENTO", int.Parse(drc[i]["IDCARREGAMENTO"].ToString()));
            //            ZCARREGAMENTO.Set("STATUS", "ABERTO");
            //            ZCARREGAMENTO.Set("DATABAIXA", DBNull.Value);
            //            ZCARREGAMENTO.Save();
            //        }
            //        grid1.Atualizar(false);
            //    }
            //}
        }
        private void itemMovimento(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                splitContainer1.Panel2Collapsed = false;
            }
        }
        #region Faturamento Sintetico
        private void faturamentoSintetico(object sender, EventArgs e)
        {
            //AppLib.Windows.FormMessagePrompt fmp = new AppLib.Windows.FormMessagePrompt();
            //try
            //{
            //    DateTime dataini = Convert.ToDateTime(fmp.Mostrar("Digite a data Inicial", null));
            //    fmp.textBox1.Clear();
            //    DateTime datafin = Convert.ToDateTime(fmp.Mostrar("Digite a data Final", null));

            //    RelFaturamentoSinteticoCliente rel = new RelFaturamentoSinteticoCliente(dataini, datafin);
            //    new DevExpress.XtraReports.UI.ReportPrintTool(rel).ShowRibbonPreviewDialog();
            //}
            //catch (FormatException)
            //{
            //    MessageBox.Show("Favor digitar uma data válida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            FormRelacaoParcial f = new FormRelacaoParcial("Data");
            f.Show();
        }

        #endregion
        private void cancelaBaixa(object sender, EventArgs e)
        {
            DataRowCollection drc = grid1.GetDataRows();
            if (MessageBox.Show("Deseja cancelar baixa nos itens selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                for (int i = 0; i < drc.Count; i++)
                {
                    string sql = @"UPDATE ZCARREGAMENTO SET DATABAIXA = NULL WHERE IDCARREGAMENTO = ?";
                    int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { Convert.ToInt32(drc[i]["IDCARREGAMENTO"].ToString()) });
                }
                grid1.Atualizar(false);
            }
        }
        #region Relação de Carregamento Parcial
        private void relacaoParcial(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8034", AppLib.Context.Perfil))
            {
                //AppLib.Windows.FormMessagePrompt fmp = new AppLib.Windows.FormMessagePrompt();
                //try
                //{
                //    DateTime dataini = Convert.ToDateTime(fmp.Mostrar("Digite a data Inicial", null));
                //    fmp.textBox1.Clear();
                //    DateTime datafin = Convert.ToDateTime(fmp.Mostrar("Digite a data Final", null));

                //    RelRelacaoParcial rel = new RelRelacaoParcial(dataini, datafin);
                //    new DevExpress.XtraReports.UI.ReportPrintTool(rel).ShowRibbonPreviewDialog();
                //}
                //catch (FormatException)
                //{
                //    MessageBox.Show("Favor digitar uma data válida.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                FormRelacaoParcial f = new FormRelacaoParcial("Data");
                f.ShowDialog();
            }
        }

        #endregion
        #region Autorização de Carregamento
        private void relatorio(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                DataRowCollection drc = grid1.GetDataRows();
                try
                {
                    for (int i = 0; i < drc.Count; i++)
                    {
                        RelAutorizacaoCarregamento rel = new RelAutorizacaoCarregamento(Convert.ToInt32(drc[i]["IDCARREGAMENTO"].ToString()));
                        new DevExpress.XtraReports.UI.ReportPrintTool(rel).ShowRibbonPreviewDialog();
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
        #endregion

        private void baixaCarregamento(object sender, EventArgs e)
        {
            //DataRow dr = grid1.GetDataRow();
            //FormConclusaoCarregamento f = new FormConclusaoCarregamento(Convert.ToInt32(dr["IDCARREGAMENTO"].ToString()));
            //f.Mostrar();

            //FormCarregamentoVisao f = FormCarregamentoVisao.GetInstance();
            //f.MdiParent = this;
            //f.Mostrar();
            if (new AppLib.Security.Access().Processo("Start", "APP8034", AppLib.Context.Perfil))
            {
                DataRow dr = null;
                try
                {
                    dr = grid1.GetDataRow();
                }
                catch (Exception)
                {
                }
                if (dr == null)
                    return;

                AppLib.ORM.Jit ZCARREGAMENTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZCARREGAMENTO");
                if (MessageBox.Show("Deseja dar baixa nos itens selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    //Verfica se o status não está como COMPLETO
                    if (!dr["STATUS"].Equals("CONCLUÍDO"))
                    {
                        ZCARREGAMENTO.Set("IDCARREGAMENTO", int.Parse(dr["IDCARREGAMENTO"].ToString()));
                        ZCARREGAMENTO.Set("DATABAIXA", AppLib.Context.poolConnection.Get().GetDateTime());
                        ZCARREGAMENTO.Set("STATUS", "CONCLUÍDO");
                        ZCARREGAMENTO.Save();
                        MessageBox.Show("Carregamento(s) concluido(s) com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        grid1.Atualizar(false);
                        //for (int i = 0; i < drc.Count; i++)
                        //{
                        //    //Verfica se o status não está como COMPLETO
                        //    if (!drc[i]["STATUS"].Equals("COMPLETO"))
                        //    {
                        //        ZCARREGAMENTO.Set("IDCARREGAMENTO", int.Parse(drc[i]["IDCARREGAMENTO"].ToString()));
                        //        ZCARREGAMENTO.Set("DATABAIXA", AppLib.Context.poolConnection.Get().GetDateTime());
                        //        ZCARREGAMENTO.Set("STATUS", "COMPLETO");
                        //        ZCARREGAMENTO.Save();    
                        //    }

                        //ALTERA O VALOR DA QUANTIDADE P/ 0 EM TUDO QUE FOR DIFERENTE DE 01.001
                        //                    string sql = @"UPDATE TITMMOV 
                        //SET 
                        //QUANTIDADE = 0,
                        //QUANTIDADEARECEBER = 0
                        //FROM
                        //TITMMOV 
                        //INNER JOIN ZCARREGAMENTOITEMS ON TITMMOV.IDMOV = ZCARREGAMENTOITEMS.IDMOV 
                        //INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD 
                        //WHERE 
                        //ZCARREGAMENTOITEMS.IDCARREGAMENTO = ? 
                        //AND ZCARREGAMENTOITEMS.CODCOLIGADA = ? 
                        //AND TPRODUTO.CODIGOPRD NOT LIKE '01.001%' 
                        //AND TPRODUTO.CODIGOPRD NOT LIKE '01.004%'";
                        //                    int retorno = AppLib.Context.poolConnection.Get().ExecTransaction(sql, new Object[] { Convert.ToInt32(drc[i]["IDCARREGAMENTO"].ToString()), AppLib.Context.Empresa });
                    }
                    else
                    {
                        MessageBox.Show("Carregamento já concluido, favor selecionar outro registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }
        private void grid1_SetParametros(object sender, EventArgs e)
        {
            //Alterar o valor do parametro por 
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
            //grid1.Parametros = new Object[]{"1"};
        }

        private void FormCarregamentoVisaoVenda_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void grid1_AposSelecao(object sender, EventArgs e)
        {

            if (splitContainer1.Panel2Collapsed.Equals(false))
            {
                this.gridData1.Atualizar(false);
            }

        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            try
            {
                gridData1.Parametros = new Object[] { grid1.GetDataRow()["IDCARREGAMENTO"] };
            }
            catch (Exception)
            {

            }

        }
        public void FecharAnexos(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
        }

        private void grid1_Load(object sender, EventArgs e)
        {

        }



    }
}
