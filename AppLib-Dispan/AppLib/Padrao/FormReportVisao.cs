using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.DataAccess.Native;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;

namespace AppLib.Padrao
{
    public partial class FormReportVisao : AppLib.Windows.FormVisao
    {
        private static FormReportVisao _instance = null;

        public static FormReportVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormReportVisao();

            return _instance;
        }

        private void FormReportVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormReportVisao()
        {
            InitializeComponent();
            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
        }

        private void FormReportVisao_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Visualizar", null, Visualizar);
            grid1.GetProcessos().Add("Formatar", null, Formatar);
            grid1.GetProcessos().Add(new ToolStripSeparator());
            grid1.GetProcessos().Add("Novo Report Designer", null, ReportDesigner);
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormReportCadastro f = new FormReportCadastro();
            f.Conexao = grid1.Conexao;
            f.Querys[0].Conexao = grid1.Conexao;
            f.campoLookupCODREPORTTIPO.Conexao = grid1.Conexao;
            f.campoArquivoIDARQUIVO.Conexao = grid1.Conexao;
            f.gridData1.Conexao = grid1.Conexao;

            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormReportCadastro f = new FormReportCadastro();
            f.Conexao = grid1.Conexao;
            f.Querys[0].Conexao = grid1.Conexao;
            f.campoLookupCODREPORTTIPO.Conexao = grid1.Conexao;
            f.campoArquivoIDARQUIVO.Conexao = grid1.Conexao;
            f.gridData1.Conexao = grid1.Conexao;
            f.Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormReportCadastro f = new FormReportCadastro();
            f.Conexao = grid1.Conexao;
            f.Querys[0].Conexao = grid1.Conexao;
            f.campoLookupCODREPORTTIPO.Conexao = grid1.Conexao;
            f.campoArquivoIDARQUIVO.Conexao = grid1.Conexao;
            f.gridData1.Conexao = grid1.Conexao;
            f.Excluir(grid1.GetDataRows());
        }

        public void Mostrar(Boolean formatar, int IDREPORT, params Object[] valores)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();

                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(grid1.Conexao).ExecQuery("SELECT * FROM ZREPORT WHERE IDREPORT = ?", new Object[] { IDREPORT });
                System.Data.DataRow dr = dt.Rows[0];

                if (dr["IDARQUIVO"] == DBNull.Value)
                {
                    throw new Exception("O Report selecionado não possui um arquivo associado.");
                }

                String CODREPORTTIPO = "GER";
                if (dr["CODREPORTTIPO"] != DBNull.Value)
                {
                    CODREPORTTIPO = dr["CODREPORTTIPO"].ToString();
                }

                int IDARQUIVO = int.Parse(dr["IDARQUIVO"].ToString());
                String CONEXAO = dr["CONEXAO"].ToString();

                String consulta = "SELECT ARQUIVO FROM ZARQUIVO WHERE IDARQUIVO = ?";
                String ARQUIVOBASE64 = AppLib.Context.poolConnection.Get(grid1.Conexao).ExecGetField(String.Empty, consulta, new Object[] { IDARQUIVO }).ToString();
                String ARQUIVO = AppLib.Util.Conversor.Base64Decode(ARQUIVOBASE64, Encoding.UTF8);

                String nomeRerpotTemp = "temp.repx";

                if (System.IO.File.Exists(nomeRerpotTemp))
                {
                    System.IO.File.Delete(nomeRerpotTemp);
                }

                System.IO.File.WriteAllText(nomeRerpotTemp, ARQUIVO, Encoding.UTF8);
                String[] linhas = System.IO.File.ReadAllLines(nomeRerpotTemp, Encoding.UTF8);
                System.Data.SqlClient.SqlConnectionStringBuilder builder = AppLib.Data.AssistantDecode.SqlClient(AppLib.Context.poolConnection.Get(CONEXAO).ConnectionString);

                String conteudo = "";

                for (int i = 0; i < linhas.Length; i++)
                {
                    String temp = linhas[i];

                    #region TRATA A CONEXÃO

                    if (temp.Contains("msSqlConnectionParameters1.ServerName"))
                    {
                        linhas[i] = "msSqlConnectionParameters1.ServerName = \"" + builder.DataSource + "\";";
                    }

                    if (temp.Contains("msSqlConnectionParameters1.DatabaseName"))
                    {
                        linhas[i] = "msSqlConnectionParameters1.DatabaseName = \"" + builder.InitialCatalog + "\";";
                    }

                    if (temp.Contains("msSqlConnectionParameters1.UserName"))
                    {
                        linhas[i] = "msSqlConnectionParameters1.UserName = \"" + builder.UserID + "\";";
                    }

                    if (temp.Contains("msSqlConnectionParameters1.Password"))
                    {
                        linhas[i] = "msSqlConnectionParameters1.Password = \"" + builder.Password + "\";";
                    }

                    #endregion

                    #region TRATA PARÂMETROS ( MÉTODO SUBSTITUÍDO )

                    //if ( ! formatar)
                    //{
                    //    if (temp.Contains("customSqlQuery"))
                    //    {
                    //        if (CODREPORTTIPO.ToUpper().Equals("OPERACAO"))
                    //        {
                    //            linhas[i] = linhas[i].Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                    //            linhas[i] = linhas[i].Replace("@CODOPER", valores[0].ToString());
                    //        }

                    //        if (CODREPORTTIPO.ToUpper().Equals("LANCA"))
                    //        {
                    //            linhas[i] = linhas[i].Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                    //            linhas[i] = linhas[i].Replace("@CODLANCA", valores[0].ToString());
                    //        }

                    //        if (CODREPORTTIPO.ToUpper().Equals("EXTRATO"))
                    //        {
                    //            linhas[i] = linhas[i].Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                    //            linhas[i] = linhas[i].Replace("@IDEXTRATO", valores[0].ToString());
                    //        }

                    //        if (CODREPORTTIPO.ToUpper().Equals("CHEQUE"))
                    //        {
                    //            linhas[i] = linhas[i].Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                    //            linhas[i] = linhas[i].Replace("@CODCHEQUE", valores[0].ToString());
                    //        }

                    //        if (CODREPORTTIPO.ToUpper().Equals("BOLETO"))
                    //        {
                    //            linhas[i] = linhas[i].Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                    //            linhas[i] = linhas[i].Replace("@CODLANCA", valores[0].ToString());
                    //        }
                    //    }
                    //}

                    #endregion

                    conteudo += linhas[i] + "\r\n";
                }

                System.IO.File.Delete(nomeRerpotTemp);
                System.IO.File.WriteAllLines(nomeRerpotTemp, linhas, Encoding.UTF8);

                XtraReport1 report = new XtraReport1();
                report.LoadLayout(nomeRerpotTemp);

                //var template = conteudo;
                //byte[] bytes = Encoding.UTF8.GetBytes(template);
                //// byte[] bytes = System.IO.File.ReadAllBytes(nomeRerpotTemp);
                //using (var ms = new System.IO.MemoryStream(bytes))
                //{
                //    report.LoadLayout(ms);
                //}

                if (formatar)
                {
                    //DevExpress.XtraReports.UI.ReportDesignTool design = new DevExpress.XtraReports.UI.ReportDesignTool(report);
                    splashScreenManager1.CloseWaitForm();
                    //design.ShowRibbonDesigner();

                    ReportDesignTool designer = new ReportDesignTool(report);
                    XRDesignMdiController controller = designer.DesignRibbonForm.DesignMdiController;
                    controller.SqlWizardSettings.EnableCustomSql = true;
                    controller.DataSourceWizardSettings.SqlWizardSettings.EnableCustomSql = true;

                    DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
                    //controller.DataSourceWizardSettings.SqlWizardSettings.ToSqlWizardOptions.da

                    designer.ShowRibbonDesigner();
                }
                else
                {
                    var ds = (report.DataSource as SqlDataSource);
                    
                    for (int i = 0; i < ds.Queries.Count; i++)
                    {
                        if (CODREPORTTIPO.ToUpper().Equals("OPERACAO"))
                        {
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODOPER", valores[0].ToString());
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODUSUARIO", "'" + AppLib.Context.Usuario + "'");
                        }

                        if (CODREPORTTIPO.ToUpper().Equals("LANCA"))
                        {
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODLANCA", valores[0].ToString());
                        }

                        if (CODREPORTTIPO.ToUpper().Equals("EXTRATO"))
                        {
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@IDEXTRATO", valores[0].ToString());
                        }

                        if (CODREPORTTIPO.ToUpper().Equals("CHEQUE"))
                        {
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODCHEQUE", valores[0].ToString());
                        }

                        if (CODREPORTTIPO.ToUpper().Equals("BOLETO"))
                        {
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODLANCA", valores[0].ToString());
                        }
                        if (CODREPORTTIPO.ToUpper().Equals("ORDEM"))
                        {
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString());
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODFILIAL", valores[1].ToString());
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@CODIGOOP", "'" + valores[2].ToString() + "'");
                            (ds.Queries[i] as CustomSqlQuery).Sql = (ds.Queries[i] as CustomSqlQuery).Sql.Replace("@SEQOP", valores[3].ToString());
                        }
                    }

                    if (!CODREPORTTIPO.ToUpper().Equals("GER"))
                    {
                        report.RequestParameters = false;
                    }

                    DevExpress.XtraReports.UI.ReportPrintTool view = new DevExpress.XtraReports.UI.ReportPrintTool(report);
                    splashScreenManager1.CloseWaitForm();
                    view.AutoShowParametersPanel = report.RequestParameters;

                    view.ShowRibbonPreviewDialog();
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
            System.Data.DataRow dr = grid1.GetDataRow();

            if (dr != null)
            {
                int IDREPORT = int.Parse(dr["IDREPORT"].ToString());

                String CODREPORTTIPO = "GER";

                if (dr["CODREPORTTIPO"] != DBNull.Value)
                {
                    CODREPORTTIPO = dr["CODREPORTTIPO"].ToString();
                }


                if (CODREPORTTIPO.ToUpper().Equals("GER"))
                {
                    if (new AppLib.Security.Access().ReportVisualizar(grid1.Conexao, IDREPORT, AppLib.Context.Perfil))
                    {
                        this.Mostrar(false, IDREPORT);
                    }
                }
                else
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Apenas relatórios do tipo gerencial podem ser visualizados á partir do menu de relatórios.");
                }
            }
        }

        public void Visualizar(int IDREPORT, params Object[] valores)
        {
            if (new AppLib.Security.Access().ReportVisualizar(grid1.Conexao, IDREPORT, AppLib.Context.Perfil))
            {
                this.Mostrar(false, IDREPORT, valores);
            }
        }

        private void Formatar(object sender, EventArgs e)
        {
            System.Data.DataRow dr = grid1.GetDataRow();

            if (dr != null)
            {
                int IDREPORT = int.Parse(dr["IDREPORT"].ToString());

                if (new AppLib.Security.Access().ReportFormatar(grid1.Conexao, IDREPORT, AppLib.Context.Perfil))
                {
                    this.Mostrar(true, IDREPORT);
                }
            }
        }

        private void ReportDesigner(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(grid1.Conexao, "REPORTDESIGNER", AppLib.Context.Perfil))
            {
                AppLib.Padrao.FormReportDesigner f = new AppLib.Padrao.FormReportDesigner();
                f.WindowState = FormWindowState.Maximized;
                f.ShowDialog();
            }
        }



    }
}
