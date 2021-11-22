using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class GridCubo : UserControl
    {
        #region PROPRIEDADES E ATRIBUTOS

        [Category("_APP"), Description("ID do cubo")]
        public int IDCUBO { get; set; }

        [Category("_APP"), Description("Nome da grid")]
        public String NomeGrid { get; set; }

        [Category("_APP"), Description("Tipo de filtro")]
        public Global.Types.TipoFiltro TipoFiltro { get; set; }

        // PADRÕES
        [Category("_padrao"), Description("BindingSource de registros")]
        public System.Windows.Forms.BindingSource bs = new BindingSource();
        
        // ATRIBUTOS
        private String NomeFiltro = "";
        private String Filtro = "";
        private String CondicaoTemp { get; set; }
        private String Conexao { get; set; }
        private String Consulta { get; set; }
        private Object[] Parametros { get; set; }
        private System.Data.DataTable dt;
        
        #endregion

        #region CONSTRUTOR E LOAD

        public GridCubo()
        {
            InitializeComponent();
            TipoFiltro = Global.Types.TipoFiltro.Todos;
            this.Dock = DockStyle.Fill;
        }

        private void GridCubo_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.Atualizar();
            }
        }
        
        #endregion

        #region EVENTOS

        private void toolStripButtonATUALIZAR_ButtonClick(object sender, EventArgs e)
        {
            this.Atualizar();
        }

        private void atualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Atualizar();
        }

        private void selecionarColunasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pivotGridControl1.ShowCustomization();
        }

        private void salvarDisposiçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SalvarDisposicao();
        }

        private void salvarDisposiçãoComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SalvarDisposicaoComo();
        }

        private void formatarColunasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FormatarColunas();
        }

        #region SELEÇÃO

        private void selecionarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void inveterSeleçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void limparSeleçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pivotGridControl1.ShowRibbonPrintPreview();
        }

        #region EXPORTAÇÃO

        private void imagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo Excel|*.bmp";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pivotGridControl1.ExportToImage(sfd.FileName);
            }
        }

        private void arquivoTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo texto|*.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pivotGridControl1.ExportToText(sfd.FileName);
            }
        }

        private void arquivoRTFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo formatado|*.rtf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pivotGridControl1.ExportToRtf(sfd.FileName);
            }
        }

        private void arquivoHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo navegador|*.html";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pivotGridControl1.ExportToHtml(sfd.FileName);
            }
        }

        private void arquivoMHTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo único de navegador|*.mht";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pivotGridControl1.ExportToMht(sfd.FileName);
            }
        }

        private void arquivoCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo separador por vírgula|*.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pivotGridControl1.ExportToCsv(sfd.FileName);
            }
        }

        private void arquivoXLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo Excel (formato antigo)|*.xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pivotGridControl1.ExportToXls(sfd.FileName);
            }
        }

        private void arquivoXLSXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo Excel|*.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pivotGridControl1.ExportToXlsx(sfd.FileName);
            }
        }

        private void arquivoPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo Adobe|*.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pivotGridControl1.ExportToPdf(sfd.FileName);
            }
        }

        #endregion

        private void toolStripButtonFILTROS_Click(object sender, EventArgs e)
        {
            this.SelecionarFiltro();
        }
        
        #endregion

        #region MÉTODOS

        public void filtro(Boolean atualizar)
        {
            FormFiltro f = new FormFiltro();
            f.NomeGrid = NomeGrid;
            f.Conexao = this.Conexao;
            f.grid1.Conexao = f.Conexao;

            f.Colunas = new List<String>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                f.Colunas.Add(dt.Columns[i].ColumnName);
            }

            // Tratamento para a query
            try
            {
                this.SetParametros(this, null);
            }
            catch (Exception) { }

            try
            {
                f.dtSchema = AppLib.Context.poolConnection.Get(Conexao).GetSchemaTable("SELECT * FROM ( " + this.Consulta + " ) X WHERE 0=1 ", this.Parametros);
                f.ShowDialog();
            }
            catch (Exception) { }

            if (f.grid1.Selecionou)
            {
                Filtro = f.grid1.NomeFiltro;
                TipoFiltro = Global.Types.TipoFiltro.Selecionar;

                CondicaoTemp = null;

                if (atualizar)
                {
                    this.Atualizar();
                }
            }
        }

        public void Atualizar()
        {
            this.Atualizar(false);
        }

        public void Atualizar(bool MostrarFiltro)
        {
            #region VARIÁVEIS

            Boolean cancelou = false;

            #endregion

            #region CONSULTAS

            String consultaZCUBOPARAM = @"SELECT * FROM ZCUBOPARAM WHERE IDCUBO = ? ORDER BY IDCUBOPARAM";
            System.Data.DataTable dtZCUBOPARAM = AppLib.Context.poolConnection.Get().ExecQuery(consultaZCUBOPARAM, new Object[] { IDCUBO });

            String consultaZCUBOSQLPROC = @"
SELECT MIN(IDCUBOSQLPROC) IDCUBOSQLPROC, NOMEPROCEDURE
FROM ZCUBOSQLPROC
WHERE IDCUBO = ?
GROUP BY NOMEPROCEDURE
ORDER BY IDCUBOSQLPROC";
            System.Data.DataTable dtZCUBOSQLPROC = AppLib.Context.poolConnection.Get().ExecQuery(consultaZCUBOSQLPROC, new Object[] { IDCUBO });

            String consultaZCUBO = @"SELECT * FROM ZCUBO WHERE IDCUBO = ?";
            System.Data.DataTable dtZCUBO = AppLib.Context.poolConnection.Get().ExecQuery(consultaZCUBO, new Object[] { IDCUBO });

            String consultaZCUBOSQLPAR = @"SELECT IDCUBOPARAM FROM ZCUBOSQLPAR WHERE IDCUBO = ? ORDER BY PARAMETRO, IDCUBOSQLPAR";
            System.Data.DataTable dtZCUBOSQLPAR = AppLib.Context.poolConnection.Get().ExecQuery(consultaZCUBOSQLPAR, new Object[] { IDCUBO });

            String consultaZSQL = @"SELECT  * FROM ZSQL WHERE IDSQL IN ( SELECT IDSQL FROM ZCUBO WHERE IDCUBO = ? )";
            System.Data.DataTable dtZSQL = AppLib.Context.poolConnection.Get().ExecQuery(consultaZSQL, new Object[] { IDCUBO });

            #endregion

            #region OBTÉM E GUARDA OS VALORES DOS PARÂMETROS

            for (int iZCUBOPARAM = 0; iZCUBOPARAM < dtZCUBOPARAM.Rows.Count; iZCUBOPARAM++)
            {
                int IDCUBOPARAM = int.Parse(dtZCUBOPARAM.Rows[iZCUBOPARAM]["IDCUBOPARAM"].ToString());
                String ORIGDIGTEXTO = dtZCUBOPARAM.Rows[iZCUBOPARAM]["ORIGDIGTEXTO"].ToString();

                String ORIGDIGVALOR = "";

                if (dtZCUBOPARAM.Rows[iZCUBOPARAM]["ORIGDIGVALOR"] != DBNull.Value)
                {
                    ORIGDIGVALOR = dtZCUBOPARAM.Rows[iZCUBOPARAM]["ORIGDIGVALOR"].ToString();
                }

                AppLib.Windows.FormMessagePrompt formMessagePrompt1 = new Windows.FormMessagePrompt();
                String result = formMessagePrompt1.Mostrar(ORIGDIGTEXTO, ORIGDIGVALOR);

                if (formMessagePrompt1.confirmacao == Global.Types.Confirmacao.OK)
                {
                    #region SALVA O VALOR DO PARÂMETRO

                    String comandoZCUBOPARAM = "UPDATE ZCUBOPARAM SET ORIGDIGVALOR = ? WHERE IDCUBOPARAM = ?";
                    int tempZCUBOPARAM = AppLib.Context.poolConnection.Get().ExecTransaction(comandoZCUBOPARAM, new Object[] { result, IDCUBOPARAM });

                    #endregion
                }

                if (formMessagePrompt1.confirmacao == Global.Types.Confirmacao.Cancelar)
                {
                    cancelou = true;
                    iZCUBOPARAM = dtZCUBOPARAM.Rows.Count;
                }
            }

            #endregion

            if (!cancelou)
            {
                #region OBTÉM A CONEXÃO E O COMANDO

                this.Conexao = dtZSQL.Rows[0]["CONEXAO"].ToString();
                this.Consulta = dtZSQL.Rows[0]["COMANDO"].ToString();

                #endregion

                #region VARRE E EXECUTA AS PROCEDURES

                for (int iZCUBOSQLPROC = 0; iZCUBOSQLPROC < dtZCUBOSQLPROC.Rows.Count; iZCUBOSQLPROC++)
                {
                    String NOMEPROCEDURE = dtZCUBOSQLPROC.Rows[iZCUBOSQLPROC]["NOMEPROCEDURE"].ToString();

                    String consultaZCUBOSQLPROC2 = @"SELECT * FROM ZCUBOSQLPROC WHERE IDCUBO = ? AND NOMEPROCEDURE = ? ORDER BY IDCUBOSQLPROC";
                    System.Data.DataTable dtZCUBOSQLPROC2 = AppLib.Context.poolConnection.Get().ExecQuery(consultaZCUBOSQLPROC2, new Object[] { IDCUBO, NOMEPROCEDURE });

                    List<String> parametros = new List<String>();
                    List<Object> valores = new List<Object>();

                    for (int iZCUBOSQLPROC2 = 0; iZCUBOSQLPROC2 < dtZCUBOSQLPROC2.Rows.Count; iZCUBOSQLPROC2++)
                    {
                        String PARAMETRO = dtZCUBOSQLPROC2.Rows[iZCUBOSQLPROC2]["PARAMETRO"].ToString();
                        int IDCUBOPARAM = int.Parse(dtZCUBOSQLPROC2.Rows[iZCUBOSQLPROC2]["IDCUBOPARAM"].ToString());

                        Object VALOR = AppLib.Context.poolConnection.Get().ExecGetField(null, "SELECT ORIGDIGVALOR FROM ZCUBOPARAM WHERE IDCUBOPARAM = ?", new Object[] { IDCUBOPARAM });

                        parametros.Add(PARAMETRO);
                        valores.Add(VALOR);
                    }

                    int tempZCUBOSQLPROC = AppLib.Context.poolConnection.Get(Conexao).ExecProcedure(NOMEPROCEDURE, parametros.ToArray(), valores.ToArray());
                }

                #endregion

                #region MONTA OS PARAMETROS DA SQL

                List<Object> paramSQL = new List<Object>();

                for (int iZCUBOSQLPAR = 0; iZCUBOSQLPAR < dtZCUBOSQLPAR.Rows.Count; iZCUBOSQLPAR++)
                {
                    int IDCUBOPARAM = int.Parse(dtZCUBOSQLPAR.Rows[iZCUBOSQLPAR]["IDCUBOPARAM"].ToString());

                    Object VALOR = AppLib.Context.poolConnection.Get().ExecGetField(null, "SELECT ORIGDIGVALOR FROM ZCUBOPARAM WHERE IDCUBOPARAM = ?", new Object[] { IDCUBOPARAM });
                    paramSQL.Add(VALOR);
                }

                this.Parametros = paramSQL.ToArray();

                #endregion

                #region TRATAMENTO PARA O FILTRO

                if (TipoFiltro == Global.Types.TipoFiltro.Todos)
                {
                    Filtro = " ";
                }
                else
                {
                    if (MostrarFiltro)
                    {
                        // Tratamento para a query
                        try
                        {
                            this.SetParametros(this, null);
                        }
                        catch (Exception) { }

                        String Consulta = "";

                        try
                        {
                            Consulta = AppLib.Context.poolConnection.Get(Conexao).ParseCommand("SELECT * FROM ( " + this.Consulta + " ) X WHERE 0=1 ", this.Parametros);
                            this.dt = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(Consulta, new Object[] { });
                            bs.DataSource = this.dt;
                            pivotGridControl1.DataSource = bs.DataSource;

                            this.filtro(false);
                        }
                        catch (Exception ex)
                        {
                            new FormExceptionSQL().Mostrar("Erro ao carregar visão", Consulta, ex);
                        }
                    }
                }

                #endregion

                #region ATUALIZAR


                if (Filtro.Equals(""))
                {
                    // Caso seja necessário implementar algum tratamento
                }
                else
                {
                    // Tratamento para a query
                    try
                    {
                        this.SetParametros(this, null);
                    }
                    catch (Exception) { }

                    FormFiltro f = new FormFiltro();
                    f.NomeGrid = NomeGrid;
                    f.Conexao = Conexao;
                    f.grid1.Conexao = f.Conexao;

                    String ConsultaSchema = "";

                    try
                    {
                        ConsultaSchema = AppLib.Context.poolConnection.Get(Conexao).ParseCommand("SELECT * FROM ( " + this.Consulta + " ) X WHERE 0=1 ", this.Parametros);
                        f.dtSchema = AppLib.Context.poolConnection.Get(Conexao).GetSchemaTable(ConsultaSchema, new Object[] { });

                        String CondicaoQuery = "";

                        if (CondicaoTemp == null)
                        {
                            CondicaoQuery = f.getFiltro(Conexao, NomeGrid, Filtro);
                            CondicaoTemp = CondicaoQuery;
                        }
                        else
                        {
                            CondicaoQuery = CondicaoTemp;
                        }

                        #region PREENCHE A GRID

                        String ConsultaFiltro = "";

                        try
                        {
                            ConsultaFiltro = "SELECT * FROM ( \r\n\n" + this.Consulta + " \r\n) X \r\nWHERE 0=0 ";

                            if ( ! CondicaoQuery.Equals(""))
                            {
                                ConsultaFiltro += " " + CondicaoQuery;
                            }

                            //Filtro extendido
                            if (AppLib.Context.ControlarAcessos)
                            {
                                DataTable dtFiltro = AppLib.Context.poolConnection.Get(Conexao).ExecQuery("SELECT FILTRO FROM ZMENUPERFIL WHERE CODPERFIL = ? AND CODMENU IN (SELECT CODMENU FROM ZMENU WHERE TELACONSULTA = ?) ", new Object[] { AppLib.Context.Perfil, this.NomeGrid });
                                if (dtFiltro.Rows.Count > 0)
                                {
                                    string FiltroExt = dtFiltro.Rows[0][0].ToString();
                                    if (!FiltroExt.Equals(""))
                                    {
                                        ConsultaFiltro += FiltroExt;
                                    }
                                }
                            }

                            ConsultaFiltro = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(ConsultaFiltro, Parametros);
                            ConsultaFiltro = AppLib.Context.poolConnection.Get(Conexao).ParseVariaveis(ConsultaFiltro);
                            this.dt = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(ConsultaFiltro, Parametros);
                            bs.DataSource = this.dt; 
                            pivotGridControl1.DataSource = bs.DataSource;
                        }
                        catch (Exception ex)
                        {
                            new FormExceptionSQL().Mostrar("Erro de consulta SQL", ConsultaFiltro, ex);
                        }

                        #endregion

                    }
                    catch (Exception ex)
                    {
                        new FormExceptionSQL().Mostrar("Erro de consulta SQL", ConsultaSchema, ex);
                    }
                }

                #endregion

                #region TRATA VISÃO POR USUÁRIO / FILTRO

                #region REMOVE TODOS CAMPOS DA VISÃO

                pivotGridControl1.Fields.Clear();

                #endregion

                #region CARREGA E CONFIGURAÇÕES DE VISÃO

                String consultaCuboVisao = @"
SELECT COLUNA, AREA, LARGURA, ALINHAMENTO, FORMATO
FROM ZCUBOVISAO
WHERE GRID = 'Cubo' + CONVERT(VARCHAR, ?)
  AND USUARIO = ?
  AND FILTRO = ?
ORDER BY SEQUENCIA";

                System.Data.DataTable dtCuboVisao = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(consultaCuboVisao, new Object[]{ IDCUBO, AppLib.Context.Usuario, this.Filtro });

                for (int i = 0; i < dtCuboVisao.Rows.Count; i++)
                {
                    String coluna = dtCuboVisao.Rows[i]["COLUNA"].ToString();
                    String area = dtCuboVisao.Rows[i]["AREA"].ToString();
                    int largura = int.Parse(dtCuboVisao.Rows[i]["LARGURA"].ToString());
                    String alinhamento = dtCuboVisao.Rows[i]["ALINHAMENTO"].ToString();
                    String formato = dtCuboVisao.Rows[i]["FORMATO"].ToString();

                    DevExpress.XtraPivotGrid.PivotGridField campo = new DevExpress.XtraPivotGrid.PivotGridField();

                    #region Áreas

                    if (area.Equals("L"))
                    {
                        campo = new DevExpress.XtraPivotGrid.PivotGridField(coluna, DevExpress.XtraPivotGrid.PivotArea.RowArea);
                    }

                    if (area.Equals("C"))
                    {
                        campo = new DevExpress.XtraPivotGrid.PivotGridField(coluna, DevExpress.XtraPivotGrid.PivotArea.ColumnArea);
                    }

                    if (area.Equals("D"))
                    {
                        campo = new DevExpress.XtraPivotGrid.PivotGridField(coluna, DevExpress.XtraPivotGrid.PivotArea.DataArea);
                    }

                    if (area.Equals("F"))
                    {
                        campo = new DevExpress.XtraPivotGrid.PivotGridField(coluna, DevExpress.XtraPivotGrid.PivotArea.FilterArea);
                    }

                    if (area.Equals("O"))
                    {
                        campo = new DevExpress.XtraPivotGrid.PivotGridField(coluna, DevExpress.XtraPivotGrid.PivotArea.FilterArea);
                        campo.Visible = false;
                    }
                    
                    #endregion

                    campo.Width = largura;

                    #region Alinhamento

                    if (alinhamento.Equals("E"))
                    {
                        campo.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    }

                    if (alinhamento.Equals("D"))
                    {
                        campo.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    }

                    if (alinhamento.Equals("C"))
                    {
                        campo.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    }

                    #endregion

                    #region Formatação

                    campo.ValueFormat.FormatType = DevExpress.Utils.FormatType.None;

                    if (formato.Equals(Formato.Data.ToString()))
                    {
                        campo.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        campo.ValueFormat.FormatString = "dd/MM/yyyy";
                    }

                    if (formato.Equals(Formato.DataHora.ToString()))
                    {
                        campo.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        campo.ValueFormat.FormatString = "dd/MM/yyyy HH:mm";
                    }

                    if (formato.Equals(Formato.DataHoraSegundos))
                    {
                        campo.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        campo.ValueFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    }

                    if (formato.Equals(Formato.DataHoraMilisegundos))
                    {
                        campo.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        campo.ValueFormat.FormatString = "dd/MM/yyyy HH:mm:ss.fff";
                    }

                    if (formato.Equals(Formato.Decimal2.ToString()))
                    {
                        campo.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        campo.ValueFormat.FormatString = "f2";
                    }

                    if (formato.Equals(Formato.Decimal4.ToString()))
                    {
                        campo.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        campo.ValueFormat.FormatString = "f4";
                    }

                    #endregion

                    pivotGridControl1.Fields.Add(campo);

                }

                #endregion

                #region ADICIONA NOVOS CAMPOS A VISÃO NA ÁREA DE FILTRO

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    String coluna = dt.Columns[i].ColumnName;

                    if ( ! this.ColunaExiste(coluna))
                    {
                        pivotGridControl1.Fields.Add(coluna, DevExpress.XtraPivotGrid.PivotArea.FilterArea);
                    }
                }

                #endregion

                #endregion

            }
        }

        public Boolean ColunaExiste(String NomeColuna)
        {
            for (int i = 0; i < pivotGridControl1.Fields.Count; i++)
            {
                String NomeCampo = pivotGridControl1.Fields[i].FieldName;

                if (NomeCampo.Equals(NomeColuna))
                {
                    return true;
                }
            }

            return false;
        }

        public void SalvarDisposicao(String NomeDisposicao)
        {
            FormSalvarDisposicaoCubo f = new FormSalvarDisposicaoCubo();
            f.NomeGrid = this.NomeGrid;
            f.Usuario = AppLib.Context.Usuario;
            f.NomeFiltro = this.Filtro;
            f.pivotGridControl1 = this.pivotGridControl1;
            f.ShowDialog();
        }

        public void SalvarDisposicao()
        {
            this.SalvarDisposicao(this.NomeFiltro);
        }

        public void SalvarDisposicaoComo()
        {
            AppLib.Windows.FormMessagePrompt formMessagePrompt1 = new Windows.FormMessagePrompt();
            String NOMEVISAO = formMessagePrompt1.Mostrar("Informe o nome para esta configuração de visão", null);

            if (formMessagePrompt1.confirmacao == Global.Types.Confirmacao.OK)
            {
                String consultaDisposicao = @"SELECT * FROM ZFILTRO WHERE GRID = ? AND NOME = ?";
                if (AppLib.Context.poolConnection.Get(this.Conexao).ExecHasRows(consultaDisposicao, new Object[] { this.NomeGrid, this.NomeFiltro }))
                {
                    if (AppLib.Windows.FormMessageDefault.ShowQuestion("O nome informado já existe. Gostaria de sobrepor a visão informada?") == DialogResult.Yes)
                    {
                        this.SalvarDisposicao(this.NomeFiltro);
                    }
                    else
                    {
                        AppLib.Windows.FormMessageDefault.ShowInfo("O disposição não foi salva.");
                    }
                }
                else
                {
                    this.SalvarDisposicao(this.NomeFiltro);
                }
            }
        }

        public void FormatarColunas()
        {

        }

        public void SelecionarFiltro()
        {
            TipoFiltro = Global.Types.TipoFiltro.Selecionar;
            this.Atualizar(true);
        }
        
        #endregion

        #region DELEGATE

        public delegate void SetNewParametrosHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método setar parametros da consulta"), DefaultValue(false)]
        public event SetNewParametrosHandler SetParametros;

        #endregion

    }
}
