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
    public partial class frmVisaoTabelaPreco : Form
    {
        #region Variáveis

        private New.Class.Utilities util = new Class.Utilities();

        #endregion

        public frmVisaoTabelaPreco()
        {
            InitializeComponent();
        }

        private void frmVisaoTabelaPreco_Load(object sender, EventArgs e)
        {
            CarregaGrid();
            DesabilitaBotoes();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            New.Forms.Register.frmCadastroTabelaPreco frmCadastroTabelaPreco = new Register.frmCadastroTabelaPreco();
            frmCadastroTabelaPreco.ShowDialog();

            CarregaGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                New.Forms.Register.frmCadastroTabelaPreco frmCadastroTabelaPreco = new Register.frmCadastroTabelaPreco();

                frmCadastroTabelaPreco.edita = true;
                frmCadastroTabelaPreco.ID = Convert.ToInt32(row["ID"]);
                frmCadastroTabelaPreco.ShowDialog();

                CarregaGrid();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (XtraMessageBox.Show("Deseja excluir o registro selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    DataTable dt = gridControl1.DataSource as DataTable;
                    int indexRow = dt.Rows.IndexOf(row);

                    try
                    {
                        if (!gridView1.IsMasterRowEmpty(indexRow))
                        {
                            // Exclui os itens da Tabela de Preço
                            if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM ZTPRODUTOTABPRECOCOMPL WHERE CODCOLIGADA = ? AND ID = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["ID"]) }) > 0)
                            {
                                // Exclui a Tabela de Preço
                                if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM ZTPRODUTOTABPRECO WHERE CODCOLIGADA = ? AND ID = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["ID"]) }) > 0)
                                {
                                    XtraMessageBox.Show("Tabela de Preço excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    CarregaGrid();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            // Exclui a Tabela de Preço
                            if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM ZTPRODUTOTABPRECO WHERE CODCOLIGADA = ? AND ID = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["ID"]) }) > 0)
                            {
                                XtraMessageBox.Show("Tabela de Preço excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                CarregaGrid();
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Erro ao excluir Tabela de Preço.\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsFind.AlwaysVisible == true)
            {
                gridView1.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView1.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        private void inativarTabelaDePreçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InativarTabelaPreco();
            CarregaGrid();
        }

        private void reativarTabelaDePreçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReativarTabelaPreco();
            CarregaGrid();
        }

        private void copiarTabelaDePreçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopiarTabelaPreco();
            CarregaGrid();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                New.Forms.Register.frmCadastroTabelaPreco frmCadastroTabelaPreco = new Register.frmCadastroTabelaPreco();

                frmCadastroTabelaPreco.edita = true;
                frmCadastroTabelaPreco.ID = Convert.ToInt32(row["ID"]);
                frmCadastroTabelaPreco.ShowDialog();

                CarregaGrid();
            }
        }

        private void gridView1_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            GridView view = gridView1.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;

            util.RenomearColunasDetalhesGridView(view, "ZTPRODUTOTABPRECOCOMPL");
            view.BestFitColumns();
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView View = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (e.RowHandle >= 0)
            {
                string categoria = View.GetRowCellDisplayText(e.RowHandle, View.Columns["VIGENTE"]);

                if (categoria == "SIM")
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
        }

        #region Métodos

        private void CarregaGrid()
        {
            DataSet ds = new DataSet();
            DataTable dtTabelaPreco = new DataTable();
            DataTable dtItens = new DataTable();

            try
            {
                // Tabela de Preço
                dtTabelaPreco = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT *, CASE WHEN GETDATE() BETWEEN INICIOVIGENCIA AND FIMVIGENCIA THEN 'SIM' ELSE 'NÃO' END AS 'VIGENTE' FROM ZTPRODUTOTABPRECO WHERE CODCOLIGADA = " + AppLib.Context.Empresa + "");
                dtTabelaPreco.TableName = "TabelaPreco";

                dtItens = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM  ZTPRODUTOTABPRECOCOMPL WHERE CODCOLIGADA = " + AppLib.Context.Empresa + "");
                dtItens.TableName = "Itens";

                if (dtItens != null && dtItens.Rows.Count > 0)
                {
                    ds.Tables.Add(dtTabelaPreco);
                    ds.Tables.Add(dtItens);

                    ds.Relations.Add("Itens", ds.Tables["TabelaPreco"].Columns["ID"], ds.Tables["Itens"].Columns["ID"]);

                    gridControl1.DataSource = ds.Tables["TabelaPreco"];
                    gridControl1.ForceInitialize();

                    util.RenomearColunasGridView(gridView1, "ZTPRODUTOTABPRECO");
                    gridView1.BestFitColumns();
                }
                else
                {
                    gridControl1.DataSource = dtTabelaPreco;

                    util.RenomearColunasGridView(gridView1, "ZTPRODUTOTABPRECO");
                    gridView1.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DesabilitaBotoes()
        {
            btnFiltros.Enabled = false;
            toolStripDropDownButton2.Enabled = false;
            toolStripDropDownButton4.Enabled = false;
        }

        private void InativarTabelaPreco()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (XtraMessageBox.Show("Deseja inativar o(s) registro(s) selecionado(s)?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row = null;

                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        try
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZTPRODUTOTABPRECO SET INATIVO = 1 WHERE CODCOLIGADA = ? AND ID = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["ID"]) });
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show("Não foi possível inativar a Tabela de Preço [" + row["ID"].ToString() + "].\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    XtraMessageBox.Show("Tabela(s) de Preço(s) inativada(s) com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void ReativarTabelaPreco()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (XtraMessageBox.Show("Deseja reativar o(s) registro(s) selecionado(s)?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row = null;

                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        try
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE ZTPRODUTOTABPRECO SET INATIVO = 0 WHERE CODCOLIGADA = ? AND ID = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["ID"]) });
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show("Não foi possível reativar a Tabela de Preço [" + row["ID"].ToString() + "].\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    XtraMessageBox.Show("Tabela(s) de Preço(s) reativada(s) com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void CopiarTabelaPreco()
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (XtraMessageBox.Show("Deseja copiar o registro selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row = null;

                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        try
                        {
                            New.Forms.Register.frmCadastroTabelaPreco frmCadastroTabelaPreco = new Register.frmCadastroTabelaPreco();

                            frmCadastroTabelaPreco.copia = true;
                            frmCadastroTabelaPreco.ID = Convert.ToInt32(row["ID"]);
                            frmCadastroTabelaPreco.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show("Não foi copiar a Tabela de Preço.\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    XtraMessageBox.Show("Tabela de Preço copiada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        #endregion
    }
}
