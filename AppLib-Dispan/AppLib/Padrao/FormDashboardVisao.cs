using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Padrao
{
    public partial class FormDashboardVisao : AppLib.Windows.FormVisao
    {

        private static FormDashboardVisao _instance = null;

        public static FormDashboardVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormDashboardVisao();

            return _instance;
        }

        private void FormDashboardVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormDashboardVisao()
        {
            InitializeComponent();
        }

        private void FormDashboardVisao_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Visualizar", null, Visualizar);
            grid1.GetProcessos().Add("Formatar", null, Formatar);
            grid1.GetProcessos().Add(new ToolStripSeparator());
            grid1.GetProcessos().Add("Novo Dashboard Designer", null, Novo);
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormDashboardCadastro f = new FormDashboardCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormDashboardCadastro f = new FormDashboardCadastro();
            f.Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormDashboardCadastro f = new FormDashboardCadastro();
            f.Excluir(grid1.GetDataRows());
        }

        private void Mostar(Boolean formatar)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();

                System.Data.DataRow dr = grid1.GetDataRow();

                if (dr != null)
                {
                    int IDDASHBOARD = int.Parse(dr["IDDASHBOARD"].ToString());

                    if (dr["IDARQUIVO"] == DBNull.Value)
                    {
                        throw new Exception("O Dashboard selecionado não possui um arquivo associado.");
                    }

                    int IDARQUIVO = int.Parse(dr["IDARQUIVO"].ToString());
                    String CONEXAO = dr["CONEXAO"].ToString();

                    String consulta = "SELECT ARQUIVO FROM ZARQUIVO WHERE IDARQUIVO = ?";
                    String ARQUIVOBASE64 = AppLib.Context.poolConnection.Get().ExecGetField(String.Empty, consulta, new Object[] { IDARQUIVO }).ToString();
                    String ARQUIVO = AppLib.Util.Conversor.Base64Decode(ARQUIVOBASE64);

                    // WORK-AROUND
                    ARQUIVO = ARQUIVO.Replace("???<?xml", "<?xml");

                    String nomeDashboardTemp = "dashboard.xml";

                    if (System.IO.File.Exists(nomeDashboardTemp))
                    {
                        System.IO.File.Delete(nomeDashboardTemp);
                    }

                    System.IO.File.WriteAllText(nomeDashboardTemp, ARQUIVO);

                    if (formatar)
                    {
                        if (new AppLib.Security.Access().DashboardFormatar(grid1.Conexao, IDDASHBOARD, AppLib.Context.Perfil))
                        {
                            AppLib.Padrao.FormDashboardDesigner f = new FormDashboardDesigner();
                            f.dashboardBarController1.Control.LoadDashboard(nomeDashboardTemp);
                            f.WindowState = FormWindowState.Maximized;
                            f.Conexao = CONEXAO;
                            f.ArquivoXML = nomeDashboardTemp;
                            splashScreenManager1.CloseWaitForm();

                            try
                            {
                                f.ShowDialog();
                            }
                            catch { }
                        }
                        else
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                    }
                    else
                    {
                        if (new AppLib.Security.Access().DashboardVisualizar(grid1.Conexao, IDDASHBOARD, AppLib.Context.Perfil))
                        {
                            AppLib.Padrao.FormDashboardViewer f = new AppLib.Padrao.FormDashboardViewer();
                            f.WindowState = FormWindowState.Maximized;
                            f.Conexao = CONEXAO;
                            f.ArquivoXML = nomeDashboardTemp;
                            splashScreenManager1.CloseWaitForm();

                            try
                            {
                                f.ShowDialog();
                            }
                            catch { }
                        }
                        else
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao visualizar o report.\r\n\nException: " + ex.Message);
            }
        }

        private void Visualizar(object sender, EventArgs e)
        {
            this.Mostar(false);
        }

        private void Formatar(object sender, EventArgs e)
        {
            this.Mostar(true);
        }

        private void Novo(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "NOVODASHBOARD", AppLib.Context.Perfil))
            {
                AppLib.Padrao.FormDashboardDesigner f = new AppLib.Padrao.FormDashboardDesigner();
                f.WindowState = FormWindowState.Maximized;
                f.ShowDialog();
            }
        }

        

    }
}
