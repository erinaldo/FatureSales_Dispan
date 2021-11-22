using AppFatureClient.Classes;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormProdutoVisao : AppLib.Windows.FormVisao
    {
        private static FormProdutoVisao _instance = null;
        public bool TemInativo = false;
        bool ItemOrcamento = false;


        public static FormProdutoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormProdutoVisao();
            return _instance;
        }

        public FormProdutoVisao()
        {
            InitializeComponent();

            // Processos
            grid1.GetProcessos().Add("Inserir Estados", null, InserirEstados);

            grid1.GetProcessos().Add("-", null, null);

            grid1.GetProcessos().Add("Mostrar Preço Vigente", null, MostrarPrecoVigente);
            grid1.GetProcessos().Add("Reajustar Preco (%)", null, ReajustarPreco);

            // Anexos
            grid1.GetAnexos().Add("Orcamentos do Item", null, ItensOrcamento);
            grid1.GetAnexos().Add("Pedidos do Item", null, ItensPedido);
            grid1.GetAnexos().Add("Fechar Anexos", null, FecharAnexos);

            grid1.gridView1.CellValueChanged += gridView1_CellValueChanged;
            grid1.gridView1.Click += gridView1_Click;
            grid1.gridView1.SelectionChanged += gridView1_SelectionChanged;
        }

        private void InserirEstados(object sender, EventArgs e)
        {
            try
            {
                DataRowCollection drc = grid1.GetDataRows();

                foreach (DataRow row in drc)
                {
                    ProdutoTributos.InsereEstados(int.Parse(row["IDPRD"].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarPrecoVigente(object sender, EventArgs e)
        {
            try
            {
                decimal precoCalculado = 0;
                int idPRD = 0;

                DataRowCollection drc = grid1.GetDataRows();

                New.Class.Controller.Products p = new New.Class.Controller.Products();

                try
                {
                    foreach (DataRow row in drc)
                    {
                        idPRD = Convert.ToInt32(row["IDPRD"]);
                        precoCalculado = p.calcularPrecoVigente(idPRD, "");

                        if (precoCalculado > 0)
                        {
                            p.atualizaPrecoAcabado(idPRD, precoCalculado);
                        }
                    }

                    grid1.Atualizar();

                    XtraMessageBox.Show("Preço(s) Vigente(s) atualizado(s) com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Não foi possível atualizar o Preço Vigente do produto de ID nº " + idPRD.ToString() + ". Detalhes: \r\n: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReajustarPreco(object sender, EventArgs e)
        {
            if (grid1.gridView1.SelectedRowsCount > 0)
            {
                DataRowCollection drc = grid1.GetDataRows();

                try
                {
                    New.Forms.Process.frmReajustarPreco f = new New.Forms.Process.frmReajustarPreco();
                    f.drc = drc;
                    f.ShowDialog();

                    grid1.Atualizar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void FecharAnexos(object sender, EventArgs e)
        {
            try
            {
                ItemOrcamento = false;
                splitContainer1.Panel2Collapsed = true;
            }
            catch { }
        }

        public void ItensOrcamento(object sender, EventArgs e)
        {
            DataRow dr = null;

            try
            {
                ItemOrcamento = true;
                dr = grid1.GetDataRow();
            }
            catch { }

            if (dr == null)
                return;

            string ConsultaFiltro = string.Empty;

            try
            {
                splitContainer1.Panel2Collapsed = false;

                ConsultaFiltro = @"select * from ZVWORCAMENTO(nolock)
                                    where CODCOLIGADA = 1
                                    and IDMOV in (select IDMOV from TITMMOV (nolock) where IDPRD = ?)";
                ConsultaFiltro = AppLib.Context.poolConnection.Get(grid1.Conexao).ParseCommand(ConsultaFiltro, new Object[] { dr["IDPRD"] });

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

        public void ItensPedido(object sender, EventArgs e)
        {
            DataRow dr = null;

            try
            {
                ItemOrcamento = true;
                dr = grid1.GetDataRow();
            }
            catch { }

            if (dr == null)
                return;

            string ConsultaFiltro = string.Empty;

            try
            {
                splitContainer1.Panel2Collapsed = false;

                ConsultaFiltro = @"select * from ZVWPEDIDO(nolock)
                                    where CODCOLIGADA = 1
                                    and IDMOV in (select IDMOV from TITMMOV (nolock) where IDPRD = ?)";
                ConsultaFiltro = AppLib.Context.poolConnection.Get(grid1.Conexao).ParseCommand(ConsultaFiltro, new Object[] { dr["IDPRD"] });

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

        private void grid1_Editar(object sender, EventArgs e)
        {
            DataRow dr = null;
            dr = grid1.GetDataRow();

            FormProdutoCadastro frm = new FormProdutoCadastro(true);
            frm.IDPRD = Convert.ToInt32(dr["IDPRD"]);

            frm.ShowDialog();

            //new FormProdutoCadastro(true).Editar(grid1.bs);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            new FormProdutoCadastro().Excluir(grid1.GetDataRows());
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            new FormProdutoCadastro().Novo();
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void FormProdutoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void grid1_AposSelecao(object sender, EventArgs e)
        {
            if (ItemOrcamento)
            {
                ItensOrcamento(this, null);
            }
        }

        private void grid1_Load(object sender, EventArgs e)
        {


        }

        private void Atualizar()
        {
            //DataRow row = grid1.GetDataRow();  
        }

        private void FormProdutoVisao_Load(object sender, EventArgs e)
        {
            New.Class.Controller.Produto produto = new New.Class.Controller.Produto();
            GridView view = grid1.gridView1 as GridView;

            produto.ConfiguraGridViewPadrao(grid1.gridControl1, view);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            New.Class.Controller.Produto produto = new New.Class.Controller.Produto();
            GridView view = sender as GridView;

            // Atualiza o Código do DP
            if (view.FocusedColumn.FieldName == "COD_DP")
            {
                produto.AtualizaCodigoDP(view, grid1.GetDataRow());
            }

            // Atualiza o(s) parâmetro(s) das regras do produto
            if (view.FocusedColumn.FieldName == "CHAPA" || view.FocusedColumn.FieldName == "ACABAMENTO")
            {
                produto.AtualizaRegraProduto(view, grid1.GetDataRow(), view.FocusedColumn.FieldName);
            }

            // Atualiza um dos preços do produto
            if (view.FocusedColumn.FieldName == "PRECO_FIXO" || view.FocusedColumn.FieldName == "PRECO_ACABADO" || view.FocusedColumn.FieldName == "PRECO_REVENDA")
            {
                produto.AtualizaPreco(view, grid1.GetDataRow());
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            if (grid1.gridView1.SelectedRowsCount > 0)
            {
                New.Class.Controller.Produto produto = new New.Class.Controller.Produto();

                grid1.gridView1 = produto.ConfiguraGridViewPreco(grid1.gridView1, grid1.GetDataRow());
                grid1.gridView1 = produto.ConfiguraGridViewRegraProduto(grid1.gridControl1, grid1.gridView1, grid1.GetDataRow());
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (grid1.gridView1.SelectedRowsCount > 0)
            {
                New.Class.Controller.Produto produto = new New.Class.Controller.Produto();

                grid1.gridView1 = produto.ConfiguraGridViewPreco(grid1.gridView1, grid1.GetDataRow());
                grid1.gridView1 = produto.ConfiguraGridViewRegraProduto(grid1.gridControl1, grid1.gridView1, grid1.GetDataRow());
            }
        }
    }
}
