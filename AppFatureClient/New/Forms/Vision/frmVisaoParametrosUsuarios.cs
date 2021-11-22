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
    public partial class frmVisaoParametrosUsuarios : Form
    {
        public frmVisaoParametrosUsuarios()
        {
            InitializeComponent();
        }

        private void frmVisaoParametrosUsuarios_Load(object sender, EventArgs e)
        {
            DesabilitaBotoes();
            CarregaGrid();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            New.Forms.Register.frmCadastroUsuariosParametros frmCadastroUsuariosParametros = new Register.frmCadastroUsuariosParametros();
            frmCadastroUsuariosParametros.ShowDialog();

            CarregaGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                New.Forms.Register.frmCadastroUsuariosParametros frmCadastroUsuariosParametros = new Register.frmCadastroUsuariosParametros();

                frmCadastroUsuariosParametros.ID = Convert.ToInt32(row["ID"]);
                frmCadastroUsuariosParametros.edita = true;
                frmCadastroUsuariosParametros.ShowDialog();

                CarregaGrid();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (XtraMessageBox.Show("Deseja excluir o registro selecionado?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                    try
                    {
                        int result = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM ZUSUARIOPARAM WHERE CODCOLIGADA = ? AND ID = ? AND CODUSUARIO = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["ID"]), AppLib.Context.Usuario });

                        if (result > 0)
                        {
                            XtraMessageBox.Show("Registro excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CarregaGrid();
                            return;
                        }
                        else
                        {
                            XtraMessageBox.Show("Não foi possível excluir o registro selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Não foi possível excluir o registro selecionado.\r\nDetalhes: " + ex.Message + "", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                New.Forms.Register.frmCadastroUsuariosParametros frmCadastroUsuariosParametros = new Register.frmCadastroUsuariosParametros();

                frmCadastroUsuariosParametros.ID = Convert.ToInt32(row["ID"]);
                frmCadastroUsuariosParametros.edita = true;
                frmCadastroUsuariosParametros.ShowDialog();

                CarregaGrid();
            }
        }

        #region Métodos

        private void CarregaGrid()
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZUSUARIOPARAM WHERE CODCOLIGADA = " + AppLib.Context.Empresa + "");

                gridControl1.DataSource = dt;
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
