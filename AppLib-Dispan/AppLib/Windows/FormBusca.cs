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
    public partial class FormBusca : Form
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Default auto ajuste")]
        public Boolean AutoAjuste { get; set; }

        [Category("_APP"), Description("Nome da grid")]
        public String NomeGrid { get; set; }

        [Category("_APP"), Description("Nome da conexão")]
        public String Conexao { get; set; }

        [Category("_APP"), Description("Consulta da grid")]
        public String[] Consulta { get; set; }

        public Object[] Parametros = new Object[] { };

        [Category("_APP"), Description("Propriedades das Colunas")]
        public GridProps[] Formatacao { get; set; }

        [Category("_APP"), Description("Colunas para Agrupar a grid")]
        public String[] ColunaAgrupar { get; set; }

        public System.Windows.Forms.BindingSource bs = new BindingSource();
        public Boolean Selecionou = false;
        public System.Data.DataRow dr;

        #endregion

        #region CONSTRUTOR E LOAD

        public FormBusca()
        {
            InitializeComponent();

            Conexao = "Start";
            AutoAjuste = true;
        }

        private void FormLookup2_Load(object sender, EventArgs e)
        {
            gridView1.ClearSelection();
        }

        #endregion

        #region EVENTOS

        private void simpleButtonFILTRAR_Click(object sender, EventArgs e)
        {
            this.Filtrar();
        }

        private void simpleButtonLIMPAR_Click(object sender, EventArgs e)
        {
            this.Limpar();
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textEdit1.Text.Equals(""))
                {
                    this.Limpar();
                }
                else
                {
                    this.Filtrar();
                }
            }

            if (e.KeyCode == Keys.Down)
            {
                gridView1.Focus();
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{UP}");
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                int indice = gridView1.GetFocusedDataSourceRowIndex();

                if (indice == 0)
                {
                    textEdit1.Focus();
                    SendKeys.Send("{END}");
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                this.Selecionar();
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void simpleButtonSELECIONAR_Click(object sender, EventArgs e)
        {
            this.Selecionar();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            this.Selecionar();
        }
        
        #endregion

        #region DELEGATE

        public delegate void SetNewParametrosHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método setar parametros da consulta"), DefaultValue(false)]
        public event SetNewParametrosHandler SetParametros;

        public delegate void AposAtualizarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado após atualizar a grid"), DefaultValue(false)]
        public event AposAtualizarHandler AposAtualizar;

        #endregion

        #region MÉTODOS

        public String GetConsulta()
        {
            String result = "";

            for (int i = 0; i < Consulta.Length; i++)
            {
                result += Consulta[i] + "\r\n";
            }

            return result;
        }

        public void Atualizar()
        {
            try
            {
                this.SetParametros(this, null);
            }
            catch (Exception) { }

            #region PREENCHE A GRID

            String ConsultaFiltro = "";

            try
            {
                ConsultaFiltro = "SELECT * FROM ( \r\n\n" + this.GetConsulta() + " \r\n) X \r\nWHERE 0=0 ";

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
                                coll.Width = gp.Largura;

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

            #region TRATAMENTOS PARA A GRID

            // Grid somente leitura
            gridView1.OptionsBehavior.Editable = false;

            // Grid com seleção de apenas 1 registro
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
            // gridView1.OptionsView.ShowAutoFilterRow = true;

            // Grid sem mostrar espaço para agrupamento de colunas
            gridView1.OptionsView.ShowGroupPanel = false;

            // Grid sempre mostrar filtro no rodapé
            // gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;

            // Grid zebrada
            // gridView1.OptionsView.EnableAppearanceEvenRow = true;
            // gridView1.Appearance.EvenRow.BackColor = Color.Thistle;

            // TRATA AGRUPAMENTO (TREEVIEW)
            try
            {
                for (int i = 0; i < ColunaAgrupar.Length; i++)
                {
                    String tempColuna = ColunaAgrupar[i];
                    gridView1.Columns[tempColuna].Group();
                }

                if (ColunaAgrupar.Length > 0)
                {
                    gridView1.ExpandAllGroups();
                }
            }
            catch { }

            #endregion
            
            try
            {
                this.AposAtualizar(this, null);
            }
            catch { }
        }

        public void Filtrar()
        {
            this.Atualizar();

            System.Data.DataTable dt = (System.Data.DataTable)bs.DataSource;
            System.Data.DataTable dtTemp = dt.Copy();

            Boolean busca;

            // varrer linhas
            for (int linha = 0; linha < dtTemp.Rows.Count; linha++)
            {
                busca = false;

                // varrer colunas
                for (int coluna = 0; coluna < dtTemp.Columns.Count; coluna++)
                {
                    String celula = dtTemp.Rows[linha][coluna].ToString().ToUpper();
                    String texto = textEdit1.Text.ToUpper();

                    // se contér na busca
                    if (celula.Contains(texto))
                    {
                        busca = true;
                        coluna = dtTemp.Columns.Count;
                    }
                }

                if (busca == false)
                {
                    dtTemp.Rows.RemoveAt(linha);
                    linha--;
                }
            }

            bs.DataSource = null;
            bs.DataSource = dtTemp;
            gridControl1.DataSource = bs;
        }

        public void Limpar()
        {
            this.Atualizar();
        }

        public void Selecionar()
        {
            System.Data.DataRowCollection drc = this.GetDataRows();

            if (drc.Count == 0)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Selecione o registro.");
            }

            if (drc.Count == 1)
            {
                Selecionou = true;
                dr = drc[0];
                this.Close();
            }

            if (drc.Count > 1)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Selecione apenas um registro.");
            }
        }

        public System.Data.DataRow GetDataRow()
        {
            if (gridView1.SelectedRowsCount == 1)
            {
                int[] handles = gridView1.GetSelectedRows();
                return gridView1.GetDataRow(handles[0]);
            }
            else
            {
                // MessageBox.Show("Selecione um registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public System.Data.DataRowCollection GetDataRows()
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
                // MessageBox.Show("Selecione o(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public void SelecionarTudo()
        {
            gridView1.SelectAll();
        }

        private void simpleButtonSELECIONAR_Click_1(object sender, EventArgs e)
        {
            this.Selecionar();
        }

        #endregion

    }
}
