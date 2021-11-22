using DevExpress.XtraEditors;
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
    public partial class frmVisaoGrupoProduto : Form
    {
        public frmVisaoGrupoProduto()
        {
            InitializeComponent();
        }

        private void frmVisaoGrupoProduto_Load(object sender, EventArgs e)
        {
            CarregaGrid();
            DesabilitaBotoes();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            New.Forms.Register.frmCadastroGrupoProduto frmCadastroGrupoProduto = new Register.frmCadastroGrupoProduto();
            frmCadastroGrupoProduto.ShowDialog();

            CarregaGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                New.Forms.Register.frmCadastroGrupoProduto frmCadastroGrupoProduto = new Register.frmCadastroGrupoProduto();

                frmCadastroGrupoProduto.codigoGrupoProduto = row["CODTB3FAT"].ToString();
                frmCadastroGrupoProduto.edita = true;
                frmCadastroGrupoProduto.ShowDialog();

                CarregaGrid();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (XtraMessageBox.Show("ATENÇÃO\r\nAo clicar no botão de excluir você estará inativando o Grupo de Produto, deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                    try
                    {
                        int result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE TTB3 SET INATIVO = 1 WHERE CODCOLIGADA = ? AND CODTB3FAT = ?", new object[] { AppLib.Context.Empresa, row["CODTB3FAT"].ToString() });

                        if (result > 0)
                        {
                            XtraMessageBox.Show("Grupo de Produto inativado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CarregaGrid();
                            return;
                        }
                        else
                        {
                            XtraMessageBox.Show("Não foi possível inativar o registro selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Não foi possível inativar o registro selecionado.\r\nDetalhes: " + ex.Message + "", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                New.Forms.Register.frmCadastroGrupoProduto frmCadastroGrupoProduto = new Register.frmCadastroGrupoProduto();

                frmCadastroGrupoProduto.codigoGrupoProduto = row["CODTB3FAT"].ToString();
                frmCadastroGrupoProduto.edita = true;
                frmCadastroGrupoProduto.ShowDialog();

                CarregaGrid();
            }
        }

        #region Métodos

        private void CarregaGrid()
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODCOLIGADA, CODTB3FAT, DESCRICAO, CONVERT(BIT,INATIVO) AS 'INATIVO' FROM TTB3 WHERE CODCOLIGADA = " + AppLib.Context.Empresa + "");

                gridControl1.DataSource = dt;
                gridView1.BestFitColumns();
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
            toolStripDropDownButton3.Enabled = false;
            toolStripDropDownButton4.Enabled = false;
        }

        #endregion
    }
}
