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
    public partial class FormExcelVisao : AppLib.Windows.FormVisao
    {
        private static FormExcelVisao _instance = null;

        public static FormExcelVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormExcelVisao();

            return _instance;
        }

        private void FormExcelVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormExcelVisao()
        {
            InitializeComponent();
        }

        private void FormExcelVisao_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Recalcular Tudo", null, RecalcularTudo);
            grid1.GetProcessos().Add(new ToolStripSeparator());
            grid1.GetProcessos().Add("Recalcular Com Seleção", null, RecalcularAbrir);
            // grid1.GetProcessos().Add("Recalcular", null, Recalcular);
            // grid1.GetProcessos().Add("Abrir", null, Abrir);
            grid1.GetProcessos().Add("Exportar", null, Exportar);
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormExcelCadastro f = new FormExcelCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormExcelCadastro f = new FormExcelCadastro();
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormExcelCadastro f = new FormExcelCadastro();
            f.Excluir(grid1);
        }

        private void RecalcularTudo(object sender, EventArgs e)
        {
            Recalcular(true);
            Abrir(this, null);
        }

        private void RecalcularAbrir(object sender, EventArgs e)
        {
            Recalcular(this, null);
            Abrir(this, null);
        }

        private void SalvaParametro(int IDPLANILHAPARAM, String VALOR)
        {
            String comando = "UPDATE ZPLANILHAPARAM SET ORIGDIGVALOR = ? WHERE IDPLANILHAPARAM = ?";
            int temp = AppLib.Context.poolConnection.Get().ExecTransaction(comando, new Object[] { VALOR, IDPLANILHAPARAM });

            if (temp == 1)
            {
                // ok
            }
            else
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao salvar o parâmetro " + IDPLANILHAPARAM);
            }
        }

        public Boolean Recalcular(String Conexao, int IDPLANILHA, Boolean SelecionarTudo)
        {
            if (new AppLib.Security.Access().PlanilhaProcessar(Conexao, IDPLANILHA, AppLib.Context.Perfil))
            {
                String consulta = "SELECT * FROM ZARQUIVO WHERE IDARQUIVO IN (SELECT IDARQUIVO FROM ZPLANILHA WHERE IDPLANILHA = ?)";
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(consulta, new Object[] { IDPLANILHA });

                if (dt.Rows.Count > 0)
                {
                    int IDARQUIVO = int.Parse(dt.Rows[0]["IDARQUIVO"].ToString());
                    String NOMEARQUIVO = dt.Rows[0]["NOMEARQUIVO"].ToString();

                    System.IO.FileInfo fi = new System.IO.FileInfo("Temp\\" + NOMEARQUIVO);

                    AppLib.Util.ExcelManager excel = new Util.ExcelManager(fi.FullName);

                    try
                    {
                        excel.Abrir();

                        #region RECALCULO

                        #region TRATA PARÂMETROS DA PLANILHA

                        String consultaParam = @"SELECT * FROM ZPLANILHAPARAM WHERE IDPLANILHA = ?";
                        System.Data.DataTable dtParam = AppLib.Context.poolConnection.Get().ExecQuery(consultaParam, new Object[] { IDPLANILHA });

                        for (int iParam = 0; iParam < dtParam.Rows.Count; iParam++)
                        {
                            int IDPLANILHAPARAM = int.Parse(dtParam.Rows[iParam]["IDPLANILHAPARAM"].ToString());
                            String ORIGDIGTEXTO = dtParam.Rows[iParam]["ORIGDIGTEXTO"].ToString();
                            String ORIGDIGTIPO = dtParam.Rows[iParam]["ORIGDIGTIPO"].ToString();
                            String ORIGDIGVALOR = dtParam.Rows[iParam]["ORIGDIGVALOR"].ToString();
                            String DESTCELABA = dtParam.Rows[iParam]["DESTCELABA"].ToString();
                            String DESTCELCOLUNA = dtParam.Rows[iParam]["DESTCELCOLUNA"].ToString();
                            int DESTCELLINHA = int.Parse(dtParam.Rows[iParam]["DESTCELLINHA"].ToString());

                            AppLib.Windows.FormMessagePrompt formPrompt1 = new Windows.FormMessagePrompt();
                            String result = formPrompt1.Mostrar(ORIGDIGTEXTO, ORIGDIGVALOR);

                            if (formPrompt1.confirmacao == Global.Types.Confirmacao.OK)
                            {
                                this.SalvaParametro(IDPLANILHAPARAM, result);

                                #region TRATA OS TIPOS

                                try
                                {
                                    if (ORIGDIGTIPO.Equals("S")) // String
                                    {
                                        excel.SetValor(DESTCELABA, DESTCELCOLUNA, DESTCELLINHA, result);
                                    }

                                    if (ORIGDIGTIPO.Equals("D")) // DateTime
                                    {
                                        excel.SetValor(DESTCELABA, DESTCELCOLUNA, DESTCELLINHA, DateTime.Parse(result));
                                    }

                                    if (ORIGDIGTIPO.Equals("I")) // int
                                    {
                                        excel.SetValor(DESTCELABA, DESTCELCOLUNA, DESTCELLINHA, int.Parse(result));
                                    }

                                    if (ORIGDIGTIPO.Equals("V")) // decimal
                                    {
                                        excel.SetValor(DESTCELABA, DESTCELCOLUNA, DESTCELLINHA, decimal.Parse(result));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Erro ao tratar o tipo de parâmetro da planilha - " + ex.Message);
                                }

                                #endregion
                            }

                            if (formPrompt1.confirmacao == Global.Types.Confirmacao.Cancelar)
                            {
                                // iGrid = drc.Count;
                                throw new Exception("Cancelar");
                            }
                        }

                        #endregion

                        #region TRATA A SELEÇÃO PARA EXECUÇÃO

                        FormExcelAbaSelecao formExcelAbaSelecao1 = new FormExcelAbaSelecao();
                        formExcelAbaSelecao1.listaAbas = excel.GetAbas();

                        String consultaSQLSelecao = @"
SELECT ZSQL.IDSQL, ZSQL.NOME
FROM ZPLANILHASQL, ZSQL
WHERE ZPLANILHASQL.IDSQL = ZSQL.IDSQL
  AND ZPLANILHASQL.IDPLANILHA = ?
ORDER BY ZSQL.NOME";

                        System.Data.DataTable dtSQLSelecao = AppLib.Context.poolConnection.Get().ExecQuery(consultaSQLSelecao, new Object[] { IDPLANILHA });

                        List<String> listaSQL = new List<String>();

                        for (int i = 0; i < dtSQLSelecao.Rows.Count; i++)
                        {
                            int IDSQL = int.Parse(dtSQLSelecao.Rows[i]["IDSQL"].ToString());
                            String NOME = dtSQLSelecao.Rows[i]["NOME"].ToString();
                            listaSQL.Add(IDSQL + " - " + NOME);
                        }

                        formExcelAbaSelecao1.listaSQL = listaSQL;

                        if (SelecionarTudo)
                        {
                            formExcelAbaSelecao1.checkedListBoxAba.Items.AddRange(formExcelAbaSelecao1.listaAbas.ToArray());

                            for (int iListAba = 0; iListAba < formExcelAbaSelecao1.checkedListBoxAba.Items.Count; iListAba++)
                            {
                                formExcelAbaSelecao1.checkedListBoxAba.SetItemChecked(iListAba, true);
                            }

                            formExcelAbaSelecao1.confirmacao = Global.Types.Confirmacao.OK;
                            formExcelAbaSelecao1.montaResultAbas();
                            formExcelAbaSelecao1.resultSQL = "IS NOT NULL";
                        }
                        else
                        {
                            formExcelAbaSelecao1.ShowDialog();
                        }

                        #endregion

                        #region TRATA AS SQL

                        String consultaSQL = @"
SELECT *
FROM ZPLANILHASQL, ZSQL
WHERE ZPLANILHASQL.IDSQL = ZSQL.IDSQL
  AND ZPLANILHASQL.IDPLANILHA = ?
  AND ZPLANILHASQL.CELULAABA " + formExcelAbaSelecao1.resultAbas + @"
  AND ZPLANILHASQL.IDSQL " + formExcelAbaSelecao1.resultSQL + @"
ORDER BY ZPLANILHASQL.ORDEM";

                        System.Data.DataTable dtSQL = AppLib.Context.poolConnection.Get().ExecQuery(consultaSQL, new Object[] { IDPLANILHA });

                        for (int iSQL = 0; iSQL < dtSQL.Rows.Count; iSQL++)
                        {
                            int IDPLANILHASQL = int.Parse(dtSQL.Rows[iSQL]["IDPLANILHASQL"].ToString());
                            int IDSQL = int.Parse(dtSQL.Rows[iSQL]["IDSQL"].ToString());
                            String CONEXAO = dtSQL.Rows[iSQL]["CONEXAO"].ToString();
                            String COMANDO = dtSQL.Rows[iSQL]["COMANDO"].ToString();
                            String CELULAABA = dtSQL.Rows[iSQL]["CELULAABA"].ToString();
                            String CELULACOLUNA = dtSQL.Rows[iSQL]["CELULACOLUNA"].ToString();
                            int CELULALINHA = int.Parse(dtSQL.Rows[iSQL]["CELULALINHA"].ToString());

                            #region TRATA AS PROCEDURES DA CONSULTA SQL

                            // Obtém as procedures
                            String consultaListaProc = @"
SELECT DISTINCT NOMEPROCEDURE
FROM ZPLANILHASQLPROC
WHERE IDPLANILHASQL = ?";

                            System.Data.DataTable dtListaProc = AppLib.Context.poolConnection.Get().ExecQuery(consultaListaProc, new Object[] { IDPLANILHASQL });

                            for (int iListaProc = 0; iListaProc < dtListaProc.Rows.Count; iListaProc++)
                            {
                                String NOMEPROCEDURE = dtListaProc.Rows[iListaProc]["NOMEPROCEDURE"].ToString();

                                String consultaProc = @"
SELECT *
FROM ZPLANILHASQLPROC
WHERE IDPLANILHASQL = ?
  AND NOMEPROCEDURE = ?";

                                System.Data.DataTable dtProc = AppLib.Context.poolConnection.Get().ExecQuery(consultaProc, new Object[] { IDPLANILHASQL, NOMEPROCEDURE });

                                List<String> Parametros = new List<String>();
                                List<Object> Valores = new List<Object>();

                                // Monta os parâmetros
                                for (int iProc = 0; iProc < dtProc.Rows.Count; iProc++)
                                {
                                    String Proc_PARAMETRO = dtProc.Rows[iProc]["PARAMETRO"].ToString();
                                    String Proc_CELULAABA = dtProc.Rows[iProc]["CELULAABA"].ToString();
                                    String Proc_CELULACOLUNA = dtProc.Rows[iProc]["CELULACOLUNA"].ToString();
                                    int Proc_CELULALINHA = int.Parse(dtProc.Rows[iProc]["CELULALINHA"].ToString());

                                    Parametros.Add(Proc_PARAMETRO);

                                    Object result = excel.GetValor(null, Proc_CELULAABA, Proc_CELULACOLUNA, Proc_CELULALINHA);
                                    Valores.Add(result);
                                }

                                int temp = AppLib.Context.poolConnection.Get().ExecProcedure(NOMEPROCEDURE, Parametros.ToArray(), Valores.ToArray());

                            }

                            #endregion

                            #region TRATA OS PARÂMETROS DA SQL

                            String consultaSQLPar = @"
SELECT *
FROM ZPLANILHASQLPAR
WHERE IDPLANILHASQL = ?
ORDER BY PARAMETRO";

                            System.Data.DataTable dtSQLPar = AppLib.Context.poolConnection.Get().ExecQuery(consultaSQLPar, new Object[] { IDPLANILHASQL });

                            List<Object> listaSQLPar = new List<Object>();

                            for (int iSQLPar = 0; iSQLPar < dtSQLPar.Rows.Count; iSQLPar++)
                            {
                                try
                                {
                                    String SQLPar_CELULAABA = dtSQLPar.Rows[iSQLPar]["CELULAABA"].ToString();
                                    String SQLPar_CELULACOLUNA = dtSQLPar.Rows[iSQLPar]["CELULACOLUNA"].ToString();
                                    int SQLPar_CELULALINHA = int.Parse(dtSQLPar.Rows[iSQLPar]["CELULALINHA"].ToString());

                                    Object result = excel.GetValor(null, SQLPar_CELULAABA, SQLPar_CELULACOLUNA, SQLPar_CELULALINHA);
                                    listaSQLPar.Add(result);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Erro no tratamento do parâmetro da consulta SQL " + IDSQL + " - " + ex.Message);
                                }
                            }

                            #endregion

                            #region PREENCHE O RESULTADO DA SQL NA PLANILHA

                            try
                            {
                                System.Data.DataTable dados = AppLib.Context.poolConnection.Get(CONEXAO).ExecQuery(COMANDO, listaSQLPar.ToArray());
                                excel.SetDados2(CELULAABA, CELULACOLUNA, CELULALINHA, dados);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Erro ao preencher o resultado da consulta SQL " + IDSQL + " na planilha - " + ex.Message);
                            }

                            #endregion
                        }

                        #endregion

                        #endregion

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("Cancelar"))
                        {
                            AppLib.Windows.FormMessageDefault.ShowInfo("Operação cancelada pelo usuário.");
                        }
                        else
                        {
                            AppLib.Windows.FormMessageDefault.ShowError("Erro ao recalcular planilha " + IDPLANILHA + " - " + NOMEARQUIVO + "\r\n\nDetalhe técnico: " + ex.Message);
                        }

                        return false;
                    }
                    finally
                    {
                        excel.Fechar(true);
                    }
                }
            }

            return true;
        }

        public void Recalcular(Boolean SelecionarTudo)
        {
            try
            {
                this.Exportar(false);

                System.Data.DataRowCollection drc = grid1.GetDataRows(false);

                if (drc != null)
                {
                    splashScreenManager1.ShowWaitForm();

                    for (int iGrid = 0; iGrid < drc.Count; iGrid++)
                    {
                        int IDPLANILHA = int.Parse(drc[iGrid]["IDPLANILHA"].ToString());
                        if (Recalcular(grid1.Conexao, IDPLANILHA, SelecionarTudo))
                        {
                            // ok
                        }
                        else
                        {
                            iGrid = drc.Count;
                        }
                    }

                    splashScreenManager1.CloseWaitForm();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Recalcular: " + ex.Message);
            }
            
        }

        private void Recalcular(object sender, EventArgs e)
        {
            Recalcular(false);
        }

        private void Exportar(object sender, EventArgs e)
        {
            this.Exportar(false);
            AppLib.Windows.FormMessageDefault.ShowInfo("Exportação realizada com sucesso!");
        }

        public String Exportar(String Conexao, int IDPLANILHA, Boolean abrir)
        {
            String NOMEARQUIVO = String.Empty;
            String ARQUIVO = String.Empty;

            if (new AppLib.Security.Access().PlanilhaProcessar(grid1.Conexao, IDPLANILHA, AppLib.Context.Perfil))
            {
                String consulta = "SELECT * FROM ZARQUIVO WHERE IDARQUIVO IN (SELECT IDARQUIVO FROM ZPLANILHA WHERE IDPLANILHA = ?)";
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(consulta, new Object[] { IDPLANILHA });

                if (dt.Rows.Count > 0)
                {
                    NOMEARQUIVO = dt.Rows[0]["NOMEARQUIVO"].ToString();
                    ARQUIVO = dt.Rows[0]["ARQUIVO"].ToString();

                    if (!System.IO.Directory.Exists("Temp"))
                    {
                        System.IO.Directory.CreateDirectory("Temp");
                    }

                    if (System.IO.File.Exists("Temp\\" + NOMEARQUIVO))
                    {
                        System.IO.File.Delete("Temp\\" + NOMEARQUIVO);
                    }

                    AppLib.Util.Conversor.Base64ToFile("Temp\\" + NOMEARQUIVO, ARQUIVO);

                    if (abrir)
                    {
                        System.Diagnostics.Process.Start("Temp\\" + NOMEARQUIVO);
                    }
                }
            }

            return "Temp\\" + NOMEARQUIVO;
        }

        public void Exportar(Boolean abrir)
        {
            System.Data.DataRowCollection drc = grid1.GetDataRows(false);

            if (drc != null)
            {
                for (int i = 0; i < drc.Count; i++)
                {
                    int IDPLANILHA = int.Parse(drc[i]["IDPLANILHA"].ToString());
                    if (this.Exportar(grid1.Conexao, IDPLANILHA, abrir) != String.Empty)
                    {
                        // ok
                    }
                    else
                    {
                        i = drc.Count;
                    }
                }
            }
        }

        public void Importar(int IDARQUIVO, String nomeArquivo)
        {
            String ARQUIVOBASE64 = AppLib.Util.Conversor.FileToBase64(nomeArquivo);

            String comando = @"UPDATE ZARQUIVO SET ARQUIVO = ? WHERE IDARQUIVO = ?";
            int temp = AppLib.Context.poolConnection.Get().ExecTransaction(comando, new Object[] { ARQUIVOBASE64, IDARQUIVO });

            if (temp == 1)
            {
                // ok
            }
            else
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao importar o arquivo: " + IDARQUIVO + " - " + nomeArquivo);
            }
        }

        private void Abrir(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows(false);

                if (drc != null)
                {
                    for (int i = 0; i < drc.Count; i++)
                    {
                        int IDPLANILHA = int.Parse(drc[i]["IDPLANILHA"].ToString());

                        if (new AppLib.Security.Access().PlanilhaProcessar(grid1.Conexao, IDPLANILHA, AppLib.Context.Perfil))
                        {
                            String consulta = "SELECT NOMEARQUIVO FROM ZARQUIVO WHERE IDARQUIVO IN (SELECT IDARQUIVO FROM ZPLANILHA WHERE IDPLANILHA = ?)";

                            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(consulta, new Object[] { IDPLANILHA });

                            if (dt.Rows.Count > 0)
                            {
                                String NOMEARQUIVO = dt.Rows[0]["NOMEARQUIVO"].ToString();

                                try
                                {
                                    System.Diagnostics.Process.Start("Temp\\" + NOMEARQUIVO);
                                }
                                catch
                                {
                                    this.RecalcularAbrir(this, null);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Abrir: " + ex.Message);
            }
            
        }

        

    }
}
