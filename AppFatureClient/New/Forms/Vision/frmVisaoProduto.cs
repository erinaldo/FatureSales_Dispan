using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient.New.Forms.Vision
{
    public partial class frmVisaoProduto : Form
    {
        public New.Class.Controller.Produto produto = new New.Class.Controller.Produto();
        private New.Class.Utilities util = new Class.Utilities();

        string tabela = "ZVWTPRD";
        bool isGridOrcamentoExibida = false;
        bool isGridPedidoExibida = false;

        public frmVisaoProduto()
        {
            InitializeComponent();
        }

        private void frmVisaoProduto_Load(object sender, EventArgs e)
        {
            try
            {
                produto.CarregaGrid(gcProdutos, gvProdutos);
                produto.ConfiguraGridViewPadrao(gcProdutos, gvProdutos);
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Erro ao carregar visão", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovoProduto_Click(object sender, EventArgs e)
        {
            new FormProdutoCadastro().Novo();

            produto.CarregaGrid(gcProdutos, gvProdutos);
            produto.ConfiguraGridViewPadrao(gcProdutos, gvProdutos);
        }

        private void btnEditarProduto_Click(object sender, EventArgs e)
        {
            if (gvProdutos.SelectedRowsCount > 0)
            {
                DataRow dr = gvProdutos.GetDataRow(Convert.ToInt32(gvProdutos.GetSelectedRows().GetValue(0).ToString()));
                int indexRow = gvProdutos.GetSelectedRows()[0];

                FormProdutoCadastro frm = new FormProdutoCadastro(true);
                frm.IDPRD = Convert.ToInt32(dr["IDPRD"]);

                frm.ShowDialog();

                AtualizarColuna(indexRow);
            }
        }

        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            if (gvProdutos.SelectedRowsCount > 0)
            {
                new FormProdutoCadastro().Excluir(produto.GetDataRowCollection(gvProdutos));
            }
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            if (gvProdutos.OptionsFind.AlwaysVisible == true)
            {
                gvProdutos.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gvProdutos.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgruparProduto_Click(object sender, EventArgs e)
        {
            if (gvProdutos.OptionsView.ShowGroupPanel == true)
            {
                gvProdutos.OptionsView.ShowGroupPanel = false;
                gvProdutos.ClearGrouping();
                btnAgruparProduto.Text = "Agrupar";
            }
            else
            {
                gvProdutos.OptionsView.ShowGroupPanel = true;
                btnAgruparProduto.Text = "Desagrupar";
            }
        }

        private void AtualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            produto.CarregaGrid(gcProdutos, gvProdutos);
            produto.ConfiguraGridViewPadrao(gcProdutos, gvProdutos);
        }

        private void configurarColunasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();

            produto.CarregaGrid(gcProdutos, gvProdutos);
            produto.ConfiguraGridViewPadrao(gcProdutos, gvProdutos);
        }

        private void salvarConfiguraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            util.SalvarDisposicao(tabela, gvProdutos);

            produto.CarregaGrid(gcProdutos, gvProdutos);
            produto.ConfiguraGridViewPadrao(gcProdutos, gvProdutos);
        }

        private void btnFiltrosProduto_Click(object sender, EventArgs e)
        {
            New.Forms.Filters.frmFiltroProduto frmFiltroProduto = new New.Forms.Filters.frmFiltroProduto();
            frmFiltroProduto.ShowDialog();

            produto.condicao = frmFiltroProduto.condicao;

            produto.CarregaGrid(gcProdutos, gvProdutos);
            produto.ConfiguraGridViewPadrao(gcProdutos, gvProdutos);
        }

        #region Métodos

        private void AtualizarColuna(int index)
        {
            // Atualizar de acordo com o index
            produto.CarregaGrid(gcProdutos, gvProdutos);
            produto.ConfiguraGridViewPadrao(gcProdutos, gvProdutos);

            gvProdutos.SelectRow(index);
            gvProdutos.FocusedRowHandle = index;

            //DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM " + tabela + " WHERE IDPRD = ?", new object[] { dr["IDPRD"] });

            //var index = gvProdutos.GetSelectedRows();

            //for (int i = 0; i < gvProdutos.VisibleColumns.Count; i++)
            //{
            //    try
            //    {

            //        gvProdutos.SetRowCellValue(index[0], "PRECO_FIXO", 100);


            //        //dr[gvProdutos.Columns[i].FieldName] = dt.Rows[0][gvProdutos.Columns[i].FieldName];
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}

            //gvProdutos.RefreshRow(index[0]);

            //for (int i = 0; i < gvProdutos.VisibleColumns.Count; i++)
            //{
            //    try
            //    {
            //        dr[gvProdutos.Columns[i].FieldName] = dt.Rows[0][gvProdutos.Columns[i].FieldName];
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}

            //DataTable dtProduto = gcProdutos.DataSource as DataTable;

            //dtProduto.LoadDataRow(dr.ItemArray, true);
        }

        #endregion

        #region Anexos

        private void orçamentosDoItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvProdutos.SelectedRowsCount > 0)
            {
                isGridOrcamentoExibida = true;
                string consulta = "";

                DataRow dr = gvProdutos.GetDataRow(Convert.ToInt32(gvProdutos.GetSelectedRows().GetValue(0).ToString()));

                try
                {
                    splitContainer1.Panel2Collapsed = false;

                    consulta = string.Format(@"SELECT * FROM ZVWORCAMENTO(NOLOCK) WHERE CODCOLIGADA = {0} AND IDMOV IN (SELECT IDMOV FROM TITMMOV (NOLOCK) WHERE IDPRD = {1})", AppLib.Context.Empresa, dr["IDPRD"]);

                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                    gridView1.GridControl.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta, new object[] { });

                    // formata as colunas
                    for (int i = 0; i < gridView1.Columns.Count; i++)
                    {
                        string TempColuna = gridView1.Columns[i].FieldName;
                        string TempTipo = gridView1.Columns[i].ColumnType.ToString();
                        string TempFormat = gridView1.Columns[i].DisplayFormat.ToString();

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
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Não foi possível consultar os itens do orçamento.\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void pedidosDoItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvProdutos.SelectedRowsCount > 0)
            {
                isGridPedidoExibida = true;
                string consulta = "";

                DataRow dr = gvProdutos.GetDataRow(Convert.ToInt32(gvProdutos.GetSelectedRows().GetValue(0).ToString()));

                try
                {
                    splitContainer1.Panel2Collapsed = false;

                    consulta = string.Format(@"SELECT * FROM ZVWPEDIDO(NOLOCK) WHERE CODCOLIGADA = {0} AND IDMOV IN (SELECT IDMOV FROM TITMMOV (NOLOCK) WHERE IDPRD = {1})", AppLib.Context.Empresa, dr["IDPRD"]);

                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                    gridView1.GridControl.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta, new object[] { });

                    // formata as colunas
                    for (int i = 0; i < gridView1.Columns.Count; i++)
                    {
                        string TempColuna = gridView1.Columns[i].FieldName;
                        string TempTipo = gridView1.Columns[i].ColumnType.ToString();
                        string TempFormat = gridView1.Columns[i].DisplayFormat.ToString();

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
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Não foi possível consultar os itens do Pedido.\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void fecharAnexosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isGridOrcamentoExibida = false;
            isGridPedidoExibida = false;

            splitContainer1.Panel2Collapsed = true;
        }

        #endregion

        #region Processos

        private void inserirEstadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowCollection drc = produto.GetDataRowCollection(gvProdutos);

                foreach (DataRow row in drc)
                {
                    Classes.ProdutoTributos.InsereEstados(int.Parse(row["IDPRD"].ToString()));
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mostrarPreçoVigenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                decimal precoCalculado = 0;
                int idPRD = 0;

                DataRowCollection drc = produto.GetDataRowCollection(gvProdutos);

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

                    // Atualizar grid
                    produto.CarregaGrid(gcProdutos, gvProdutos);

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

        private void reajustarPrecoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvProdutos.SelectedRowsCount > 0)
            {
                DataRowCollection drc = produto.GetDataRowCollection(gvProdutos);

                try
                {
                    New.Forms.Process.frmReajustarPreco f = new New.Forms.Process.frmReajustarPreco();
                    f.drc = drc;
                    f.ShowDialog();

                    // Atualizar grid
                    produto.CarregaGrid(gcProdutos, gvProdutos);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Eventos

        private void gvProdutos_DoubleClick(object sender, EventArgs e)
        {
            if (gvProdutos.SelectedRowsCount > 0)
            {
                DataRow dr = gvProdutos.GetDataRow(Convert.ToInt32(gvProdutos.GetSelectedRows().GetValue(0).ToString()));
                int indexRow = gvProdutos.GetSelectedRows()[0];


                FormProdutoCadastro frm = new FormProdutoCadastro(true);
                frm.IDPRD = Convert.ToInt32(dr["IDPRD"]);

                frm.ShowDialog();

                AtualizarColuna(indexRow);
            }
        }

        private void gvProdutos_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (gvProdutos.SelectedRowsCount > 0)
            {
                // Verifica se existem anexos abertos

                // Orçamentos do Item
                if (isGridOrcamentoExibida)
                {
                    orçamentosDoItemToolStripMenuItem_Click(sender, e);
                }

                // Pedidos do Item
                if (isGridPedidoExibida)
                {
                    pedidosDoItemToolStripMenuItem_Click(sender, e);
                }

                New.Class.Controller.Produto produto = new New.Class.Controller.Produto();
                DataRow row = gvProdutos.GetDataRow(Convert.ToInt32(gvProdutos.GetSelectedRows().GetValue(0).ToString()));

                gvProdutos = produto.ConfiguraGridViewPreco(gvProdutos, row);
                gvProdutos = produto.ConfiguraGridViewRegraProduto(gcProdutos, gvProdutos, row);
            }
        }

        private void gvProdutos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            New.Class.Controller.Produto produto = new New.Class.Controller.Produto();
            DataRow row = gvProdutos.GetDataRow(Convert.ToInt32(gvProdutos.GetSelectedRows().GetValue(0).ToString()));

            // Atualiza o Código do DP
            if (gvProdutos.FocusedColumn.FieldName == "COD_DP")
            {
                produto.AtualizaCodigoDP(gvProdutos, row);
            }

            // Atualiza o(s) parâmetro(s) das regras do produto
            if (gvProdutos.FocusedColumn.FieldName == "CHAPA" || gvProdutos.FocusedColumn.FieldName == "ACABAMENTO")
            {
                produto.AtualizaRegraProduto(gvProdutos, row, gvProdutos.FocusedColumn.FieldName);
            }

            // Atualiza um dos preços do produto
            if (gvProdutos.FocusedColumn.FieldName == "PRECO_FIXO" || gvProdutos.FocusedColumn.FieldName == "PRECO_ACABADO" || gvProdutos.FocusedColumn.FieldName == "PRECO_REVENDA")
            {
                produto.AtualizaPreco(gvProdutos, row);
            }
        }

        private void gvProdutos_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            util.SalvarLarguraVisaoUsuario(e.Column.Width, tabela, e.Column.FieldName);
        }

        private void gvProdutos_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            if (e.DropInfo.Index > 0)
            {
                util.SalvarSequenciaVisaoUsuario(tabela, gvProdutos);
            }
            else
            {
                // Coluna ficará invísível
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZVISAOUSUARIO SET VISIVEL = 0, VISIBILIDADE = 0 WHERE USUARIO = ? AND GRID = ? AND COLUNA = ? AND FILTRO = '' ", new object[] { AppLib.Context.Usuario, tabela, e.DragObject.ToString() });
            }
        }

        #endregion
    }
}
