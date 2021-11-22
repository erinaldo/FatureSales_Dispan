using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class GridData : UserControl
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Habilitar botão novo")]
        public Boolean BotaoNovo { get; set; }

        [Category("_APP"), Description("Habilitar botão editar")]
        public Boolean BotaoEditar { get; set; }

        [Category("_APP"), Description("Habilitar botão excluir")]
        public Boolean BotaoExcluir { get; set; }

        [Category("_APP"), Description("Nome da grid")]
        public String NomeGrid { get; set; }

        [Category("_APP"), Description("Nome da conexão")]
        public String Conexao { get; set; }

        [Category("_APP"), Description("Consulta da grid")]
        public String[] Consulta { get; set; }

        [Category("_APP"), Description("Parâmetros da consulta")]
        public Object[] Parametros = new Object[] { };

        [Category("_APP"), Description("Tipo de filtro")]
        public Global.Types.TipoFiltro TipoFiltro { get; set; }

        [Category("_APP"), Description("Tipo de atualização")]
        public Global.Types.TipoAtualizacao TipoAtualizacao { get; set; }

        [Category("_APP"), Description("Propriedades das Colunas")]
        public GridProps[] Formatacao { get; set; }

        #endregion

        #region PROPRIEDADES PADRÕES

        [Category("_padrao"), Description("BindingSource de registros")]
        public System.Windows.Forms.BindingSource bs = new BindingSource();

        [Category("_padrao"), Description("Default auto ajuste")]
        public Boolean AutoAjuste { get; set; }

        [Category("_padrao"), Description("Formulário pai")]
        public Form FormPai { get; set; }

        [Category("_padrao"), Description("Grid em Modo Filtro")]
        public Boolean ModoFiltro { get; set; }

        [Category("_padrao"), Description("BindingSource de registros")]
        public Boolean Selecionou { get; set; }

        [Category("_padrao"), Description("Seleção em cascata")]
        public Boolean SelecaoCascata { get; set; }

        [Category("_padrao"), Description("Nome do Filtro Interno")]
        public String NomeFiltro = "";

        #endregion

        #region ATRIBUTOS

        private String Filtro = "";

        private String CondicaoTemp { get; set; }

        #endregion

        #region CONSTRUTOR

        public GridData()
        {
            InitializeComponent();

            BotaoNovo = false;
            BotaoEditar = false;
            BotaoExcluir = false;

            Conexao = "Start";

            AutoAjuste = true;
            SelecaoCascata = true;
            TipoFiltro = Global.Types.TipoFiltro.Todos;
            this.Dock = DockStyle.Fill;
        }

        #endregion

        #region EVENTOS

        private void Grid_Load(object sender, EventArgs e)
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
                    if (BotaoEditar)
                    {
                        toolStripButtonEDITAR.Enabled = true;
                    }
                    else
                    {
                        toolStripButtonEDITAR.Enabled = false;
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

                if (this.TipoFiltro != Global.Types.TipoFiltro.Nenhum)
                {
                    this.Atualizar(true, panelLookup.Visible, FormPai);
                }
            }

            try
            {
                this.CarregaFluxoVisao();
            }
            catch { }

        }

        public void CarregaFluxoVisao()
        {
            String consulta = "SELECT * FROM ZMENUFLUXOVISAO WHERE CODMENU = ?";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(consulta, new Object[] { this.NomeGrid });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.GetProcessos().Add(dt.Rows[i]["NOME"].ToString(), null, ExecutarFluxo);
            }

            if (dt.Rows.Count > 0)
            {
                this.GetProcessos().Add(new ToolStripSeparator());
            }
        }

        private void ExecutarFluxo(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem item = (System.Windows.Forms.ToolStripItem)sender;

            String consulta = "SELECT * FROM ZMENUFLUXOVISAO WHERE CODMENU = ? AND NOME = ?";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(consulta, new Object[] { this.NomeGrid, item.Text });

            if (dt.Rows.Count > 0)
            {
                String Fluxo = dt.Rows[0]["FLUXO"].ToString();

                if (new AppLib.Security.Access().Processo(this.Conexao, Fluxo, AppLib.Context.Perfil))
                {
                    AppLib.Fluxo.FormFluxoVisao f = new Fluxo.FormFluxoVisao();
                    f.Run(AppLib.Fluxo.Modo.Executar, Fluxo, this);
                }
            }
        }

        private void toolStripButtonNOVO_Click(object sender, EventArgs e)
        {
            if (BotaoNovo)
            {
                this.Novo(this, null);
                this.toolStripButtonATUALIZAR_Click(this, null);
            }
            else
            {
                if (this.Consulta[0].ToString().Contains("TTB2") || this.Consulta[0].ToString().Contains("TTB3") || this.Consulta[0].ToString().Contains("TTB4"))
                {
                    string tabela = this.NomeGrid.Substring(0, 4);

                    switch (tabela)
                    {
                        case "TTB2":

                            

                            break;
                        case "TTB3":

                            break;
                        case "TTB4":

                            break;
                        default:
                            break;
                    }
                }

                // MessageBox.Show("Botão desabilitado.");
            }
        }

        private void toolStripButtonEDITAR_Click(object sender, EventArgs e)
        {
            int[] handles = gridView1.GetSelectedRows();
            if (BotaoEditar)
            {
                if (gridView1.SelectedRowsCount <= 0)
                {
                    return;
                }

                bs.Position = gridView1.GetFocusedDataSourceRowIndex();
                this.Editar(this, null);
                this.toolStripButtonATUALIZAR_Click(this, null);
                gridView1.FocusedRowHandle = handles[0];
                this.gridView1.SelectRow(handles[0]);
            }
            else
            {
                // MessageBox.Show("Botão editar desabilitado.");
            }
        }

        private void toolStripButtonEXCLUIR_Click(object sender, EventArgs e)
        {
            if (BotaoExcluir)
            {
                this.Excluir(this, null);
                this.toolStripButtonATUALIZAR_Click(this, null);
            }
            else
            {
                // MessageBox.Show("Botão desabilitado.");
            }
        }

        private void toolStripButtonPESQUISAR_Click(object sender, EventArgs e)
        {
            if (gridView1.IsFindPanelVisible)
            {
                gridView1.HideFindPanel();
            }
            else
            {
                gridView1.ShowFindPanel();
            }
        }

        private void toolStripButtonAGRUPAR_Click(object sender, EventArgs e)
        {
            gridView1.OptionsView.ShowGroupPanel = !gridView1.OptionsView.ShowGroupPanel;
        }

        public void toolStripButtonATUALIZAR_Click(object sender, EventArgs e)
        {
            this.Atualizar(false, panelLookup.Visible, FormPai);
        }

        private void toolStripButtonFILTRO_Click(object sender, EventArgs e)
        {
            TipoFiltro = Global.Types.TipoFiltro.Selecionar;
            this.Atualizar(true, panelLookup.Visible, FormPai);
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            toolStripButtonEDITAR_Click(this, null);
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (panelLookup.Visible)
            {
                simpleButtonSELECIONAR_Click(this, null);
            }
            else
            {
                this.toolStripButtonEDITAR_Click(this, null);
            }
        }

        private void salvarConfiguraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSalvarDisposicao f = new FormSalvarDisposicao();
            f.Conexao = this.Conexao;
            f.NomeGrid = this.NomeGrid;
            f.Usuario = AppLib.Context.Usuario;
            f.NomeFiltro = this.Filtro;
            f.gridView1 = this.gridView1;
            f.ShowDialog();
        }

        private void formatarColunasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void configurarColunasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.ShowCustomization();
            gridView1.CustomizationForm.Text = "Colunas Ocultas";
        }

        private void selecionarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.SelectAll();
        }

        private void inveterSeleçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int registros = gridControl1.Views[0].DataRowCount;

            for (int i = 0; i < registros; i++)
            {
                gridView1.InvertRowSelection(i);
            }
        }

        private void limparSeleçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.ClearSelection();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (SelecaoCascata)
            {
                SelecionarCascata();
            }

            try
            {
                this.AposSelecao(this, null);
            }
            catch (Exception) { }
        }

        public void gridView1_EndSorting(object sender, EventArgs e)
        {
            try
            {
                bs.RemoveSort();

                String OrderBy = "";

                for (int i = 0; i < gridView1.SortedColumns.Count; i++)
                {
                    DevExpress.XtraGrid.Columns.GridColumn gc = gridView1.SortedColumns[i];
                    String coluna = gc.FieldName;
                    String ordem = "";

                    if (NomeGrid != "ZVWORCAMENTO")
                    {
                        if (gc.SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
                        {
                            ordem = "ASC";
                        }
                        else
                        {
                            ordem = "DESC";
                        }

                        if (ordem.Length > 0)
                        {
                            OrderBy += coluna + " " + ordem + ", ";
                        }
                    }                 
                }

                if (OrderBy.Length > 0)
                {
                    OrderBy = OrderBy.Substring(0, OrderBy.Length - 2);
                    bs.Sort = OrderBy;

                    gridView1.GridControl.DataSource = null;
                    gridView1.GridControl.DataSource = bs.DataSource;
                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError(ex.Message);
            }
        }

        private void toolStripButtonATUALIZAR_ButtonClick(object sender, EventArgs e)
        {
            this.toolStripButtonATUALIZAR_Click(this, null);

        }

        private void simpleButtonTODOS_Click(object sender, EventArgs e)
        {
            if (ModoFiltro)
            {
                NomeFiltro = " ";
                Selecionou = true;
                FormPai.Close();
            }
        }

        public void simpleButtonSELECIONAR_Click(object sender, EventArgs e)
        {
            if (ModoFiltro)
            {
                try
                {
                    Selecionou = true;
                    NomeFiltro = this.GetDataRow()["NOME"].ToString();
                    FormPai.Close();
                }
                catch (Exception) { }
            }
            else
            {
                Selecionou = true;
                FormPai.Close();
            }
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            if (ModoFiltro)
            {
                Selecionou = false;
                FormPai.Close();
            }
            else
            {
                Selecionou = false;
                FormPai.Close();
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (panelLookup.Visible)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    simpleButtonSELECIONAR_Click(this, null);
                }
            }
        }

        #region EXPORTAÇÃO

        private void exportarTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo texto|*.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Text, sfd.FileName);
            }
        }

        private void exportarRTFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo formatado|*.rtf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Rtf, sfd.FileName);
            }
        }

        private void exportarHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo navegador|*.html";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Html, sfd.FileName);
            }
        }

        private void exportarCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo separador por vírgula|*.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Csv, sfd.FileName);
            }
        }

        private void exportarXLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo Excel (formato antigo)|*.xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Xls, sfd.FileName);
            }
        }

        private void exportarXLSXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo Excel|*.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Xlsx, sfd.FileName);
            }
        }

        private void exportarPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo Adobe|*.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridView1.Export(DevExpress.XtraPrinting.ExportTarget.Pdf, sfd.FileName);
            }
        }

        #endregion

        #endregion

        #region MÉTODOS

        public String GetConsulta()
        {
            String result = "";

            if (Consulta != null)
            {
                for (int i = 0; i < Consulta.Length; i++)
                {
                    result += Consulta[i] + "\r\n";
                }
            }
            else
            {
                result = "SELECT * FROM " + NomeGrid;
            }

            return result;
        }

        public Object GetObjectRow()
        {
            if (gridView1.SelectedRowsCount == 1)
            {
                int[] handles = gridView1.GetSelectedRows();
                return gridView1.GetRow(handles[0]);
            }
            else
            {
                MessageBox.Show("Selecione um registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public List<Object> GetObjectRows()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                List<Object> lista = new List<object>();
                int[] handles = gridView1.GetSelectedRows();

                for (int i = 0; i < handles.Length; i++)
                {
                    lista.Add(gridView1.GetRow(handles[i]));
                }

                return lista;
            }
            else
            {
                MessageBox.Show("Selecione o(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public System.Data.DataRow GetDataRow()
        {
            return this.GetDataRow(true);
        }

        public System.Data.DataRow GetDataRow(Boolean ValidarSelecao)
        {
            if (gridView1.SelectedRowsCount == 1)
            {
                int[] handles = gridView1.GetSelectedRows();
                return gridView1.GetDataRow(handles[0]);
            }
            else
            {
                if (ValidarSelecao)
                {
                    MessageBox.Show("Selecione um registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
        }

        public System.Data.DataRowCollection GetDataRows()
        {
            return this.GetDataRows(true);
        }

        public System.Data.DataRowCollection GetDataRows(Boolean ValidarSelecao)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                System.Data.DataTable dt = new DataTable();
                int[] handles = gridView1.GetSelectedRows();

                for (int i = 0; i < handles.Length; i++)
                {
                    if (i == 0)
                    {
                        for (int col = 0; col < gridView1.GetDataRow(handles[i]).Table.Columns.Count; col++)
                        {
                            dt.Columns.Add(gridView1.GetDataRow(handles[i]).Table.Columns[col].ColumnName);
                        }
                    }

                    if (handles[i] >= 0)
                    {
                        dt.Rows.Add(gridView1.GetDataRow(handles[i]).ItemArray);
                    }
                }

                return dt.Rows;
            }
            else
            {
                if (ValidarSelecao)
                {
                    MessageBox.Show("Selecione o(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
        }

        public void Selecionar()
        {
            this.simpleButtonSELECIONAR_Click(this, null);
        }

        public void Atualizar()
        {
            this.Atualizar(false);
        }

        public void Atualizar(bool MostrarFiltro)
        {
            this.Atualizar(MostrarFiltro, false, null);
        }

        public void Atualizar(bool MostrarFiltro, bool _ModoLookup, Form _FormPai)
        {
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
                        //Consulta = AppLib.Context.poolConnection.Get(Conexao).ParseCommand( this.GetConsulta(), this.Parametros);
                        Consulta = AppLib.Context.poolConnection.Get(Conexao).ParseCommand("SELECT * FROM ( " + this.GetConsulta() + " ) X WHERE 0=1 ORDER BY 1,2 ", this.Parametros);
                        bs.DataSource = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(Consulta, new Object[] { });
                        gridView1.GridControl.DataSource = bs.DataSource;

                        this.gridView1_EndSorting(this, null);

                        this.filtro(false);
                    }
                    catch (Exception ex)
                    {
                        new FormExceptionSQL().Mostrar("Erro ao carregar visão", Consulta, ex);
                    }
                }
            }

            #endregion

            #region ANTES DE ATUALIZAR

            try
            {
                this.AntesAtualizar(this, null);
            }
            catch { }

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
                f.Conexao = this.Conexao;
                f.grid1.Conexao = f.Conexao;

                String ConsultaSchema = "";

                try
                {
                    //ConsultaSchema = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(this.GetConsulta(), Parametros);
                    ConsultaSchema = AppLib.Context.poolConnection.Get(Conexao).ParseCommand("SELECT * FROM ( " + this.GetConsulta() + " ) X WHERE 0=1 ", Parametros);
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
                        //ConsultaFiltro = this.GetConsulta();
                        ConsultaFiltro = "SELECT * FROM ( \r\n\n" + this.GetConsulta() + " \r\n) X \r\nWHERE 0=0 ";

                        if (!CondicaoQuery.Equals(""))
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
                        bs.DataSource = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(ConsultaFiltro, Parametros);
                        gridView1.GridControl.DataSource = bs.DataSource;

                        // Usar navegador embarcado
                        // gridView1.GridControl.UseEmbeddedNavigator = true;

                        // Realizar ajustes das propriedades de formatação das colunas
                        try
                        {
                            for (int i = 0; i < Formatacao.Length; i++)
                            {
                                GridProps gp = Formatacao[i];

                                for (int x = 0; x < gridView1.Columns.Count; x++)
                                {
                                    DevExpress.XtraGrid.Columns.GridColumn coll = gridView1.Columns[x];

                                    if (coll.FieldName.Equals(gp.Coluna))
                                    {
                                        coll.Visible = gp.Visivel;
                                        coll.Width = gp.Largura;

                                        if (gp.Agrupar)
                                        {
                                            coll.Group();
                                        }

                                        #region ALINHAMENTO

                                        if (gp.Alinhamento == Alinhamento.Esquerda)
                                        {
                                            coll.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                                            coll.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                                        }

                                        if (gp.Alinhamento == Alinhamento.Direita)
                                        {
                                            coll.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                                            coll.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                                        }

                                        if (gp.Alinhamento == Alinhamento.Centro)
                                        {
                                            coll.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                            coll.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                        }

                                        #endregion

                                        #region FORMATAÇÃO

                                        if (gp.Formato == Formato.Data)
                                        {
                                            coll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                            coll.DisplayFormat.FormatString = "dd/MM/yyyy";
                                        }

                                        if (gp.Formato == Formato.DataHora)
                                        {
                                            coll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                            coll.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
                                        }

                                        if (gp.Formato == Formato.DataHoraSegundos)
                                        {
                                            coll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                            coll.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                                        }

                                        if (gp.Formato == Formato.DataHoraMilisegundos)
                                        {
                                            coll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                            coll.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss.fff";
                                        }

                                        if (gp.Formato == Formato.Decimal2)
                                        {
                                            coll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                            coll.DisplayFormat.FormatString = "f2";
                                        }

                                        if (gp.Formato == Formato.Decimal4)
                                        {
                                            coll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                            coll.DisplayFormat.FormatString = "f4";
                                        }

                                        #endregion

                                    }
                                }
                            }
                        }
                        catch { }
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

                #region TRATAMENTOS PARA A GRID

                // Grid somente leitura
                gridView1.OptionsBehavior.Editable = false;

                // Grid com seleção de multiplos registros
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

                // Grid opção de seleção da linha toda
                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;

                // Grid com scroll horizontal
                gridView1.OptionsView.ColumnAutoWidth = false;

                // Grid sem linhas verticais
                // gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;

                // Formata altura dos grupos da grid para ter um espaçamento maior que o normal
                gridView1.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office2003;

                // Grid com auto ajuste do melhor tamanho das colunas
                gridView1.BestFitMaxRowCount = 800;

                // Ajustar automaticamente tamanho das coluas
                if (AutoAjuste)
                {
                    gridView1.BestFitColumns();
                }

                // Grid mostrar coluna de filtro
                gridView1.OptionsView.ShowAutoFilterRow = true;

                // Grid sem mostrar espaço para agrupamento de colunas
                // gridView1.OptionsView.ShowGroupPanel = false;

                // Grid sempre mostrar filtro no rodapé
                // gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;

                // Grid zebrada
                // gridView1.OptionsView.EnableAppearanceEvenRow = true;
                // gridView1.Appearance.EvenRow.BackColor = Color.Thistle;

                // Limpa a seleção dos registros da grid
                gridView1.ClearSelection();

                #endregion

            }

            #endregion

            #region TRATAMENTOS PARA VISÃO POR USUARIO

            try
            {
                String consultaConfigUsuario = @"
SELECT *
FROM ZVISAOUSUARIO
WHERE GRID = ?
  AND USUARIO = ?
  AND FILTRO = ?
ORDER BY SEQUENCIA";

                System.Data.DataTable dtConfigUsuario = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(consultaConfigUsuario, new Object[] { this.NomeGrid, AppLib.Context.Usuario, Filtro });

                // se tem configurações de usuário
                if (dtConfigUsuario.Rows.Count > 0)
                {
                    // esconde todas as colunas da grid
                    for (int i = 0; i < gridView1.Columns.Count; i++)
                    {
                        gridView1.Columns[i].Visible = false;
                    }

                    // inclui as colunas na grid
                    for (int i = 0; i < dtConfigUsuario.Rows.Count; i++)
                    {
                        int SEQUENCIA = int.Parse(dtConfigUsuario.Rows[i]["SEQUENCIA"].ToString());
                        String COLUNA = dtConfigUsuario.Rows[i]["COLUNA"].ToString();
                        int AGRUPAR = int.Parse(dtConfigUsuario.Rows[i]["AGRUPAR"].ToString());
                        int VISIVEL = int.Parse(dtConfigUsuario.Rows[i]["VISIVEL"].ToString());
                        int LARGURA = int.Parse(dtConfigUsuario.Rows[i]["LARGURA"].ToString());
                        String ALINHAMENTO = dtConfigUsuario.Rows[i]["ALINHAMENTO"].ToString();
                        String FORMATO = dtConfigUsuario.Rows[i]["FORMATO"].ToString();

                        try
                        {
                            gridView1.Columns[COLUNA].VisibleIndex = SEQUENCIA;

                            if (AGRUPAR == 1)
                            {
                                gridView1.Columns[COLUNA].Group();
                            }
                            else
                            {
                                gridView1.Columns[COLUNA].UnGroup();
                            }

                            if (VISIVEL == 1)
                            {
                                gridView1.Columns[COLUNA].Visible = true;
                            }
                            else
                            {
                                gridView1.Columns[COLUNA].Visible = false;
                            }

                            gridView1.Columns[COLUNA].Width = LARGURA;

                            #region ALINHAMENTO

                            gridView1.Columns[COLUNA].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                            gridView1.Columns[COLUNA].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

                            if (ALINHAMENTO.Equals("D"))
                            {
                                gridView1.Columns[COLUNA].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                                gridView1.Columns[COLUNA].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                            }

                            if (ALINHAMENTO.Equals("C"))
                            {
                                gridView1.Columns[COLUNA].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                gridView1.Columns[COLUNA].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            }

                            #endregion

                            #region FORMATAÇÃO

                            if (FORMATO.Equals("Data"))
                            {
                                gridView1.Columns[COLUNA].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                gridView1.Columns[COLUNA].DisplayFormat.FormatString.Equals("dd/MM/yyyy");
                            }

                            if (FORMATO.Equals("DataHora"))
                            {
                                gridView1.Columns[COLUNA].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                gridView1.Columns[COLUNA].DisplayFormat.FormatString.Equals("dd/MM/yyyy HH:mm");
                            }

                            if (FORMATO.Equals("DataHoraSegundos"))
                            {
                                gridView1.Columns[COLUNA].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                gridView1.Columns[COLUNA].DisplayFormat.FormatString.Equals("dd/MM/yyyy HH:mm:ss");
                            }

                            if (FORMATO.Equals("DataHoraMilisegundos"))
                            {
                                gridView1.Columns[COLUNA].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                gridView1.Columns[COLUNA].DisplayFormat.FormatString.Equals("dd/MM/yyyy HH:mm:ss.fff");
                            }

                            if (FORMATO.Equals("Decimal2"))
                            {
                                gridView1.Columns[COLUNA].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                gridView1.Columns[COLUNA].DisplayFormat.FormatString.Equals("f2");
                            }

                            if (FORMATO.Equals("Decimal4"))
                            {
                                gridView1.Columns[COLUNA].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                gridView1.Columns[COLUNA].DisplayFormat.FormatString.Equals("f4");
                            }

                            #endregion
                        }
                        catch { }
                    }

                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao atualizar a visão.\r\nDetalhes técnico: " + ex.Message);
            }
            #endregion

            #region APÓS ATUALIZAR

            // Expandir todos os grupos
            if (TipoAtualizacao == Global.Types.TipoAtualizacao.Expandir)
            {
                gridView1.ExpandAllGroups();
            }

            if (TipoAtualizacao == Global.Types.TipoAtualizacao.Encolher)
            {
                gridView1.CollapseAllGroups();
            }

            try
            {
                this.AposAtualizar(this, null);
            }
            catch { }

            #endregion

            #region TRATAMENTO PARA MODO LOOKUP

            if (_ModoLookup)
            {
                this.panelLookup.Visible = true;
                this.simpleButtonTODOS.Visible = false;
            }

            #endregion

            #region TRATAMENTO PARA JANELA DE SELEÇÃO DE FITLRO

            if (this.ModoFiltro)
            {
                this.panelLookup.Visible = true;
                this.simpleButtonTODOS.Visible = true;
            }

            #endregion

            #region TRATAMENTO PARA JANELA

            if (FormPai != null)
            {
                FormPai = _FormPai;
            }

            #endregion
        }

        public void filtro(Boolean atualizar)
        {
            FormFiltro f = new FormFiltro();
            f.NomeGrid = NomeGrid;
            f.Conexao = this.Conexao;
            f.grid1.Conexao = f.Conexao;

            f.Colunas = new List<String>();
            System.Data.DataView dv = ((System.Data.DataView)gridView1.GridControl.Views[0].DataSource);
            System.Data.DataTable dt = dv.Table;
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
                //f.dtSchema = AppLib.Context.poolConnection.Get(Conexao).GetSchemaTable(this.GetConsulta(), this.Parametros);
                f.dtSchema = AppLib.Context.poolConnection.Get(Conexao).GetSchemaTable("SELECT * FROM ( " + this.GetConsulta() + " ) X WHERE 0=1 ", this.Parametros);
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
                    this.Atualizar(false, false, null);
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

        public void SelecionarCascata()
        {
            try
            {
                int groupCount = gridView1.GroupCount;
                for (int groupIndex = 0; groupIndex < groupCount; groupIndex++)
                {
                    int[] handles = gridView1.GetSelectedRows();
                    for (int handleIndex = 0; handleIndex < handles.Length; handleIndex++)
                    {
                        if (handles[handleIndex] < 0)
                        {
                            int childCount = gridView1.GetChildRowCount(handles[handleIndex]);
                            for (int childIndex = 0; childIndex < childCount; childIndex++)
                            {
                                int childRowHandle = gridView1.GetChildRowHandle(handles[handleIndex], childIndex);
                                gridView1.SelectRow(childRowHandle);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public void Pesquisar(String texto)
        {
            gridView1.ShowFindPanel();
            gridView1.ApplyFindFilter(texto);
        }

        #endregion

        #region DELEGATE

        public delegate void SetNewParametrosHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método setar parametros da consulta"), DefaultValue(false)]
        public event SetNewParametrosHandler SetParametros;

        public delegate void NovoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método botão novo"), DefaultValue(false)]
        public event NovoHandler Novo;

        public delegate void EditarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método botão editar"), DefaultValue(false)]
        public event EditarHandler Editar;

        public delegate void ExcluirHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método botão excluir"), DefaultValue(false)]
        public event ExcluirHandler Excluir;

        public delegate void AposSelecaoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Após seleção de registro da grid"), DefaultValue(false)]
        public event AposSelecaoHandler AposSelecao;

        public delegate void AntesAtualizarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de atualizar a grid"), DefaultValue(false)]
        public event AntesAtualizarHandler AntesAtualizar;

        public delegate void AposAtualizarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado após atualizar a grid"), DefaultValue(false)]
        public event AposAtualizarHandler AposAtualizar;

        #endregion

    }
}
