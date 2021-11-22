using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using System.IO;
using System.Configuration;
using AppFatureClient;
using AppFatureClient.Classes;
using DevExpress.XtraReports.UI;
using AppLib.Windows;

namespace AppFatureClient
{
    public partial class FormPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Boolean ConexaoDefault { get; set; }
        public Boolean ConexaoConn2 { get; set; }
        public String Versao { get; set; }

        public FormPrincipal()
        {
            InitializeComponent();
            InitSkinGallery();

            if (!FatureContexto.Remoto)
            {
                MetodosSQL.CS = AppLib.Context.poolConnection.Get().ConnectionString;
            }

            //<Define valor do timeout baseado no parametro em AppFatureClient.exe.config na pasta raiz>
            try
            {
                AppLib.Context.poolConnection.Get("Start").Timeout = Convert.ToInt32(Properties.Settings.Default.TimeOut);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK);
            }
            //</Define valor do timeout baseado no parametro em AppFatureClient.exe.config na pasta raiz>
        }

        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(ribbonGalleryBarItem1, true);
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true;
            this.barStaticItem3.Caption = "Usuário: " + AppLib.Context.Usuario;
            this.barStaticItem4.Caption = "| Versão: " + this.Versao;
            this.barStaticItem5.Caption = "| Ambiente: " + ((FatureContexto.Remoto) ? " Acesso remoto" : " Acesso Local");

            New.Class.EnviromentHelper.Versao = this.Versao;

            if (New.Class.EnviromentHelper.IndexAlias > 0)
            {
                this.barStaticItem5.Caption = "| Ambiente: Teste";
            }
            else
            {
                this.barStaticItem5.Caption = "| Ambiente: Produção";
            }

            //if (AppLib.Context.poolConnection.Get().ConnectionString.Contains("Fature"))
            //{
            //    this.barStaticItem5.Caption = "| Ambiente: Teste";
            //}
            //else
            //{
            //    this.barStaticItem5.Caption = "| Ambiente: Produção";
            //}

            // CADASTROS
            //barButtonItemCLIENTES.Enabled = ConexaoDefault;

            barButtonItem21.Enabled = ConexaoDefault;
            barSubItem3.Enabled = ConexaoDefault;
            barSubItem1.Enabled = ConexaoDefault;

            barButtonItemPRODUTOS.Enabled = ConexaoDefault;
            barButtonItemTRANSPORTADORAS.Enabled = ConexaoDefault;
            barButtonItemCONDICOESPAGTO.Enabled = ConexaoDefault;
            barButtonItemTIPOPRODUTO.Enabled = ConexaoDefault;
            barButtonItemGRUPOPRODUTO.Enabled = ConexaoDefault;
            barButtonItemFAMILIAPRODUTO.Enabled = ConexaoDefault;
            barButtonItem1.Enabled = ConexaoDefault;
            barButtonItem8.Enabled = ConexaoDefault;

            // ROTINAS
            barButtonItemOFFORCAMENTO.Enabled = ConexaoConn2;
            barButtonItemORCAMENTO.Enabled = ConexaoDefault;
            barButtonItemPEDIDO.Enabled = ConexaoDefault;
            barButtonItemORDEMPRODUCAO.Enabled = ConexaoDefault;
            barButtonItemDEMONSTRACAO.Enabled = ConexaoDefault;
            barButtonItemREQUISICAO.Enabled = ConexaoDefault;
            barButtonItemLOGINTEGRACAO.Enabled = ConexaoDefault;
            barButtonItem10.Enabled = ConexaoDefault;

            // RELATORIOS
            barButtonItem5.Enabled = ConexaoDefault;
            barButtonItem6.Enabled = ConexaoDefault;
            barButtonItem7.Enabled = ConexaoDefault;

            // SEGURANÇA
            barButtonItemACESSO.Enabled = ConexaoDefault;

            ParametrizaUsuarioSupervisor();
        }

        #region CADASTROS

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormClienteVisao f = new FormClienteVisao();
            FormClienteVisao f = FormClienteVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem5_ItemClick_3(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            FormOffClienteVisao f = new FormOffClienteVisao();
            f.MdiParent = this;
            f.Mostrar();
            */

            //FormOffNovoClienteVisao f = new FormOffNovoClienteVisao();
            //FormOffNovoClienteVisao f = FormOffNovoClienteVisao.GetInstance();
            //f.MdiParent = this;
            //f.Mostrar();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            FormOffNovoClienteVisao f = new FormOffNovoClienteVisao();
            f.MdiParent = this;
            f.Mostrar();
            */
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormTransportadoraVisao f = new FormTransportadoraVisao();
            FormTransportadoraVisao f = FormTransportadoraVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormCondPagamentoVisao f = new FormCondPagamentoVisao();
            FormCondPagamentoVisao f = FormCondPagamentoVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Primeiro exemplo 

            //FormProdutoVisao f = new FormProdutoVisao();
            //FormProdutoVisao f = FormProdutoVisao.GetInstance();
            //f.MdiParent = this;
            //f.Mostrar();

            if (new AppLib.Security.Access().Consultar("Start", "ZVWTPRD", AppLib.Context.Perfil))
            {
                New.Forms.Filters.frmFiltroProduto frmFiltroProduto = new New.Forms.Filters.frmFiltroProduto();
                frmFiltroProduto.ShowDialog();

                if (!string.IsNullOrEmpty(frmFiltroProduto.condicao))
                {
                    New.Forms.Vision.frmVisaoProduto frm = new New.Forms.Vision.frmVisaoProduto();
                    frm.produto.condicao = frmFiltroProduto.condicao;
                    //frm.BringToFront();

                    frm.MdiParent = this;
                    frm.Show();
                }

                // Tratar seleção filtro

                //AppLib.Windows.FormFiltro frmFiltro = new FormFiltro();
                //frmFiltro.NomeGrid = "ZVWTPRD";
                //frmFiltro.ShowDialog();

                //DataTable dt = frmFiltro.grid1.gridControl1.DataSource as DataTable;

                //// Novo formulário de Visão do Produto
                //New.Forms.Vision.frmVisaoProduto frm = new New.Forms.Vision.frmVisaoProduto();
                //frm.produto.dtProduto = dt;
                //frm.produto.nomeFiltro = frmFiltro.grid1.NomeFiltro;             

                //frm.MdiParent = this;

                //frm.Show();

                ///////////////////////

                //FormProdutoVisao f = new FormProdutoVisao();
                //f.Show();

                //f.Hide();

                //DataTable dt = f.grid1.gridControl1.DataSource as DataTable;

                //New.Forms.Vision.frmVisaoProduto frm = new New.Forms.Vision.frmVisaoProduto();
                //frm.produto.dtProduto = dt;
                //frm.produto.nomeFiltro = f.grid1.NomeFiltro;

                //f.Dispose();

                //frm.MdiParent = this;

                //frm.Show();
            }
            else
            {
                MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso a este menu (ZVWTPRD).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void barButtonItem5_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormMenuPerfilVisao f = new FormMenuPerfilVisao();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem5_ItemClick_2(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormTipoProdutoVisao f = new FormTipoProdutoVisao();
            FormTipoProdutoVisao f = FormTipoProdutoVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItemGRUPOPRODUTO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormGrupoProdutoVisao f = new FormGrupoProdutoVisao();
            FormGrupoProdutoVisao f = FormGrupoProdutoVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItemFAMILIAPRODUTO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormFamiliaProdutoVisao f = new FormFamiliaProdutoVisao();
            FormFamiliaProdutoVisao f = FormFamiliaProdutoVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        #endregion

        #region ROTINAS

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormOffOrcamentoVisao f = new FormOffOrcamentoVisao();
            //FormOffOrcamentoVisao f = FormOffOrcamentoVisao.GetInstance();
            //f.MdiParent = this;
            //f.Mostrar();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormOrcamentoVisao f = new FormOrcamentoVisao();
            FormOrcamentoVisao f = FormOrcamentoVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormPedidoVisao f = new FormPedidoVisao();
            FormPedidoVisao f = FormPedidoVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormOrdemProducaoVisao f = new FormOrdemProducaoVisao();
            FormCotacoesDiversasVisao f = FormCotacoesDiversasVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormDemonstracaoBrindeVisao f = new FormDemonstracaoBrindeVisao();
            FormSeparacaoMateriaisVisao f = FormSeparacaoMateriaisVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItemREQUISICAO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormRequisicaoPecasVisao f = new FormRequisicaoPecasVisao();7
            FormRequisicaoPecasVisao f = FormRequisicaoPecasVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormLogIntegracaoVisao f = new FormLogIntegracaoVisao();
            //FormLogIntegracaoVisao f = FormLogIntegracaoVisao.GetInstance();
            //f.MdiParent = this;
            //f.Mostrar();
        }

        private void barButtonItem9_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormCarregamentoVisao f = FormCarregamentoVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem10_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormCarregamentoVisao f = FormCarregamentoVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        #endregion

        #region RELATÓRIOS

        //private void barButtonItem5_ItemClick_4(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    if (new AppLib.Security.Access().Processo("Start", "APP8029", AppLib.Context.Perfil))
        //    {
        //        RelTabMaquina relatorio = new RelTabMaquina();
        //        relatorio.Conexao = "Start";
        //        DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
        //        tool.ShowRibbonPreviewDialog();
        //    }
        //}

        //private void barButtonItem6_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    if (new AppLib.Security.Access().Processo("Start", "APP8030", AppLib.Context.Perfil))
        //    {
        //        RelTabImplemento relatorio = new RelTabImplemento();
        //        relatorio.Conexao = "Start";
        //        DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
        //        tool.ShowRibbonPreviewDialog();
        //    }
        //}

        //private void barButtonItem7_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    if (new AppLib.Security.Access().Processo("Start", "APP8031", AppLib.Context.Perfil))
        //    {
        //        RelTabPecas relatorio = new RelTabPecas();
        //        relatorio.Conexao = "Start";
        //        DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
        //        tool.ShowRibbonPreviewDialog();
        //    }
        //}

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8032", AppLib.Context.Perfil))
            {
                FormFaturamentoSinteticoCliente f = new FormFaturamentoSinteticoCliente();
                f.ShowDialog();
            }
        }

        #endregion

        #region SINCRONISMO

        //public DateTime BuscaUltimoRegistro(String Conexao, String TABELA)
        //{
        //    String campo = string.Empty;
        //    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get().Clone();
        //    AppLib.Data.Connection conn2 = AppLib.Context.poolConnection.Get("Conn2").Clone();
        //    String name = AppLib.Context.poolConnection.Get(Conexao).Name;
        //    AppLib.Global.Types.Database database = AppLib.Context.poolConnection.Get(Conexao).Database;
        //    String connectionString = AppLib.Context.poolConnection.Get(Conexao).ConnectionString;

        //    AppLib.Data.Connection conexaoDinamica = new AppLib.Data.Connection(name, database, connectionString);

        //    if (TABELA == "ZPERFIL" || TABELA == "ZUSUARIO" || TABELA == "ZMENU" || TABELA == "ZMENUPERFIL" || TABELA == "ZMENUPROCESSO" || TABELA == "ZPROCESSOPERFIL" || TABELA == "ZPROCESSO")
        //        campo = "DATAALTERACAO";
        //    else
        //        campo = "RECMODIFIEDON";

        //    if (TABELA == "ZTPRDCOMPOSTO")
        //    {
        //        //
        //        string sql = @"SELECT IDPRD, IDPRDCOMPONENTE FROM ZTPRDCOMPOSTO WHERE CODCOLIGADA = ?";
        //        DataTable ZTPRDCOMPOSTOOFF = conn2.ExecQuery(sql, new object[] { AppLib.Context.Empresa });
        //        if (!ZTPRDCOMPOSTOOFF.Rows.Count.Equals(0))
        //        {
        //            int valor = ZTPRDCOMPOSTOOFF.Rows.Count;
        //            sql = @"SELECT IDPRD, IDPRDCOMPONENTE FROM TPRDCOMPOSTO WHERE CODCOLIGADA = ?";

        //            DataTable ZTPRDCOMPOSTOON = conn.ExecQuery(sql, new object[] { AppLib.Context.Empresa });

        //            if (!ZTPRDCOMPOSTOON.Rows.Count.Equals(0))
        //            {
        //                if (ZTPRDCOMPOSTOON.Rows.Count < ZTPRDCOMPOSTOOFF.Rows.Count)
        //                {
        //                    for (int i = 0; i < ZTPRDCOMPOSTOOFF.Rows.Count; i++)
        //                    {
        //                        DataRow[] linha = ZTPRDCOMPOSTOON.Select("IDPRD = " + ZTPRDCOMPOSTOOFF.Rows[i]["IDPRD"].ToString() + " AND IDPRDCOMPONENTE = " + ZTPRDCOMPOSTOOFF.Rows[i]["IDPRDCOMPONENTE"].ToString());
        //                        if (linha.Length == 0)
        //                        {
        //                            // Excluir do banco OFF
        //                            sql = @"DELETE FROM ZTPRDCOMPOSTO WHERE IDPRD = ? AND CODCOLIGADA = ? AND IDPRDCOMPONENTE = ?";
        //                            int retorno = AppLib.Context.poolConnection.Get("Conn2").ExecTransaction(sql, new object[] { ZTPRDCOMPOSTOOFF.Rows[i]["IDPRD"].ToString(), AppLib.Context.Empresa, ZTPRDCOMPOSTOOFF.Rows[i]["IDPRDCOMPONENTE"].ToString() });
        //                            valor = valor - 1;
        //                            if (ZTPRDCOMPOSTOON.Rows.Count == valor)
        //                            {
        //                                break;

        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    String Consulta = @"SELECT ISNULL(MAX(" + campo + "), '1800-01-01') FROM " + TABELA;
        //    System.DateTime dt = (DateTime)conexaoDinamica.ExecGetField(null, Consulta, new Object[] { });
        //    return dt;
        //    //return DateTime.Now.Date;
        //}

        //private void syncTool1_SetParametros(object sender, EventArgs e)
        //{
        //    // até a 20 referem-se ao recebimento
        //    for (int i = 0; i <= 20; i++)
        //    {
        //        syncTool1.Item[i].Parametros = new Object[] { this.BuscaUltimoRegistro(syncTool1.Item[i].ConexaoLocal, syncTool1.Item[i].Tabela) };

        //        // tratamento especial para tabela 2 de ZUSUARIO
        //        if (i == 2)
        //        {
        //            syncTool1.Item[i].Parametros = new Object[] { this.BuscaUltimoRegistro(syncTool1.Item[i].ConexaoLocal, syncTool1.Item[i].Tabela), AppLib.Context.Usuario };
        //        }
        //    }

        //    // da tabela 21 em diante referem-se ao envio
        //    for (int i = 21; i <= 23; i++)
        //    {
        //        syncTool1.Item[i].Parametros = new Object[] { AppLib.Context.Usuario, this.BuscaUltimoRegistro(syncTool1.Item[i].ConexaoRemota, syncTool1.Item[i].Tabela) };
        //    }
        //}

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (syncTool1.Status.Equals(""))
        //    {
        //        barStaticItem1.Caption = "";
        //    }
        //    else
        //    {
        //        barStaticItem1.Caption = "| Status da sincronização: " + syncTool1.Status;
        //    }
        //}

        #endregion
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8029", AppLib.Context.Perfil))
            {
                RelTabMaquina relatorio = new RelTabMaquina();
                relatorio.Conexao = "Start";
                DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                tool.ShowRibbonPreviewDialog();
            }
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8030", AppLib.Context.Perfil))
            {
                RelTabImplemento relatorio = new RelTabImplemento();
                relatorio.Conexao = "Start";
                DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                tool.ShowRibbonPreviewDialog();
            }
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8031", AppLib.Context.Perfil))
            {
                RelTabPecas relatorio = new RelTabPecas();
                relatorio.Conexao = "Start";
                DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                tool.ShowRibbonPreviewDialog();
            }
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8032", AppLib.Context.Perfil))
            {
                FormFaturamentoSinteticoCliente f = new FormFaturamentoSinteticoCliente();
                f.ShowDialog();
            }
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                FormRelacaoParcial f = new FormRelacaoParcial("Data");
                f.label1.Text = "Data Programada Inicial";
                f.label2.Text = "Data Programada Final";
                f.ShowDialog();
            }
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                FormRelacaoParcial f = new FormRelacaoParcial("Representante");
                f.Text = "Representante";
                f.ShowDialog();
            }
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FormAgendaVisao f = FormAgendaVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                FormCarregamentoVisao f = FormCarregamentoVisao.GetInstance();
                f.MdiParent = this;
                f.Mostrar();
            }
        }

        private void barButtonItem9_ItemClick_2(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            //{
            //    FormAgendaCarregamento f = FormAgendaCarregamento.GetInstance();
            //    f.ShowDialog();
            //}
        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                FormRelacaoParcial f = new FormRelacaoParcial("Concluído");
                f.Text = "Carregamento Concluído";
                f.label1.Text = "Data Autorização Inicial";
                f.label2.Text = "Data Autorização Final";
                f.ShowDialog();
            }
        }

        private void barButtonItem9_ItemClick_3(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8034", AppLib.Context.Perfil))
            {
                FormRomaneioVisao f = FormRomaneioVisao.GetInstance();
                f.MdiParent = this;
                f.Text = "Visão Romaneio";
                f.Mostrar();
            }
        }

        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8033", AppLib.Context.Perfil))
            {
                FormRelacaoParcial f = new FormRelacaoParcial("Romaneio");
                f.Text = "Romaneio";
                f.ShowDialog();
            }
        }

        #region CONFIGURAÇÕES DE SEGURANÇA

        private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormMenuVisao f = AppLib.Padrao.FormMenuVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem1_ItemClick_2(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormProcessoVisao f = AppLib.Padrao.FormProcessoVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }


        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormMenuPerfilVisao f = AppLib.Padrao.FormMenuPerfilVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem8_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormProcessoPerfilVisao f = AppLib.Padrao.FormProcessoPerfilVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        #endregion

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8029", AppLib.Context.Perfil))
            {
                formSelecaoRepresentante frm = new formSelecaoRepresentante();
                frm.Show();
            }
        }

        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8029", AppLib.Context.Perfil))
            {
                formSelecaoRepresentante frm = new formSelecaoRepresentante();
                frm.composto = true;
                frm.Show();
            }
        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8029", AppLib.Context.Perfil))
            {
                formSelecaoRepresentante frm = new formSelecaoRepresentante();
                frm.IPI = true;
                frm.Show();
            }
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8029", AppLib.Context.Perfil))
            {
                formSelecaoRepresentante frm = new formSelecaoRepresentante();
                frm.composto = true;
                frm.IPI = true;
                frm.Show();
            }
        }

        private void barButtonRepresentantes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FormRepresentantesVisao f = FormRepresentantesVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();

        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Sair e fechar o sistema?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnComissoes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8045", AppLib.Context.Perfil))
            {
                AtualizaValorBaixa();
                FormComissaoVisao frm = new FormComissaoVisao();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void AtualizaValorBaixa()
        {
            try
            {
                if (new AppLib.Security.Access().Processo("Start", "APP8045", AppLib.Context.Perfil))
                {
                    DialogResult r = MessageBox.Show("Deseja atualizar os valores antes de prosseguir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        splashScreenManager1.ShowWaitForm();
                        Classes.Comissao.MetodosComissao.AtualizaValores();
                        splashScreenManager1.CloseWaitForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AtualizaValorBaixa();
        }

        private void btnAdicionarCarta_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8045", AppLib.Context.Perfil))
            {
                AtualizaValorBaixa();
                FormComissaoCartaVisao f = new FormComissaoCartaVisao();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnGerenciarPorentagem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8045", AppLib.Context.Perfil))
            {
                FormComissaoPorcentagemVisao f = new FormComissaoPorcentagemVisao();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnAnaliseItensCompra_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8048", AppLib.Context.Perfil))
            {
                FormRequisicaoDevolucaoFiltro frm = new FormRequisicaoDevolucaoFiltro();
                frm.campoLookupCODCFO.Visible = false;
                frm.campoLookupCODRPR.Visible = false;
                frm.lblCliente.Visible = false;
                frm.lblRepresentante.Visible = false;
                frm.ceRepresentante.Visible = false;
                frm.ceCliente.Visible = false;
                frm.ceData.Visible = false;
                frm.ceData.Checked = true;
                frm.txtDataIni.Enabled = true;
                frm.txtDataFin.Enabled = true;

                frm.ShowDialog();

                RelAnaliseItensPedidoCompras report = new RelAnaliseItensPedidoCompras(frm.DtaInicio, frm.DtaFim);

                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    printTool.ShowPreviewDialog();
                }
            }
        }

        private void btnRegraProduto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormRegraProdutoVisao frm = new FormRegraProdutoVisao();
            frm.MdiParent = this;
            frm.Mostrar();
        }

        private void btnRegraCFOP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormRegraCFOPVisao frm = new FormRegraCFOPVisao();
            frm.MdiParent = this;
            frm.Mostrar();
        }

        private void btnTabPreco_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormTabPrecoVisao frm = new FormTabPrecoVisao();
            //frm.MdiParent = this;
            //frm.Mostrar();

            New.Forms.Vision.frmVisaoTabelaPreco frmVisaoTabelaPreco = new New.Forms.Vision.frmVisaoTabelaPreco();
            frmVisaoTabelaPreco.MdiParent = this;
            frmVisaoTabelaPreco.Show();
        }

        private void btnRateio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormOPRateadas frm = new FormOPRateadas();
            frm.MdiParent = this;
            frm.Mostrar();
        }

        private void FormPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        #region Relatórios

        private void btnRelatoriosVendasVendedor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8073", AppLib.Context.Perfil))
            {

                New.Forms.Filters.frmFiltroRelatorioConferenciaVendas frmFiltro = new New.Forms.Filters.frmFiltroRelatorioConferenciaVendas();
                frmFiltro.ShowDialog();

                New.Reports.xrConferenciaVendas xrConferenciaVendas = new New.Reports.xrConferenciaVendas();
                xrConferenciaVendas.DataInicial = frmFiltro.DataInicial;
                xrConferenciaVendas.DataFinal = frmFiltro.DataFinal;
                xrConferenciaVendas.CodVendedor = frmFiltro.CodVendendor;

                if (!string.IsNullOrEmpty(frmFiltro.DataInicial) || !string.IsNullOrEmpty(frmFiltro.DataFinal) || !string.IsNullOrEmpty(frmFiltro.DataFinal))
                {
                    ReportPrintTool printTool = new ReportPrintTool(xrConferenciaVendas);
                    printTool.ShowPreview();
                }
            }
        }

        #endregion

        #region Parâmetros

        private void btnParametros_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "APP8074", AppLib.Context.Perfil))
            {
                New.Forms.Vision.frmVisaoParametros frmVisaoParametros = new New.Forms.Vision.frmVisaoParametros();
                frmVisaoParametros.MdiParent = this;
                frmVisaoParametros.Show();
            }
        }

        #endregion

        #region Parâmetros - Usuários

        private void btnParametrosUsuarios_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            New.Forms.Vision.frmVisaoParametrosUsuarios frmVisaoParametrosUsuarios = new New.Forms.Vision.frmVisaoParametrosUsuarios();
            frmVisaoParametrosUsuarios.MdiParent = this;
            frmVisaoParametrosUsuarios.Show();
        }

        #endregion

        #region Tabelas de Classificação 

        #region Tipo de Produto

        private void btnTipoProduto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            New.Forms.Vision.frmVisaoTipoProduto frmVisaoTipoProduto = new New.Forms.Vision.frmVisaoTipoProduto();
            frmVisaoTipoProduto.MdiParent = this;
            frmVisaoTipoProduto.Show();
        }

        #endregion

        #region Grupo de Produto

        private void btnGrupoProduto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            New.Forms.Vision.frmVisaoGrupoProduto frmVisaoGrupoProduto = new New.Forms.Vision.frmVisaoGrupoProduto();
            frmVisaoGrupoProduto.MdiParent = this;
            frmVisaoGrupoProduto.Show();
        }

        #endregion

        #region Família de Produto

        private void btnFamiliaProduto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            New.Forms.Vision.frmVisaoFamiliaProduto frmVisaoFamiliaProduto = new New.Forms.Vision.frmVisaoFamiliaProduto();
            frmVisaoFamiliaProduto.MdiParent = this;
            frmVisaoFamiliaProduto.Show();
        }

        #endregion

        #endregion

        #region Métodos

        private void ParametrizaUsuarioSupervisor()
        {
            int supervisor = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT SUPERVISOR FROM GPERMIS WHERE CODCOLIGADA = ? AND CODUSUARIO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Usuario }));

            if (supervisor != 1)
            {
                btnRateio.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            Properties.Settings.Default.Supervisor = supervisor;
            Properties.Settings.Default.Save();
        }

        #endregion
    }
}
