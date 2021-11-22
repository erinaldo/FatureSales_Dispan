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
    public partial class frmVisaoParametros : Form
    {
        public frmVisaoParametros()
        {
            InitializeComponent();
        }

        private void frmVisaoParametros_Load(object sender, EventArgs e)
        {
            DesabilitaBotoes();
            CarregaGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                New.Forms.Register.frmCadastroParametro frmCadastroParametro = new Register.frmCadastroParametro();
                frmCadastroParametro.ShowDialog();
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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                New.Forms.Register.frmCadastroParametro frmCadastroParametro = new Register.frmCadastroParametro();
                frmCadastroParametro.ShowDialog();
            }
        }

        #region Métodos

        private void CarregaGrid()
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZPARAMFATURE WHERE CODCOLIGADA = "+ AppLib.Context.Empresa +"");

                gridControl1.DataSource = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DesabilitaBotoes()
        {
            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnFiltros.Enabled = false;
            toolStripDropDownButton1.Enabled = false;
            toolStripDropDownButton2.Enabled = false;
            toolStripDropDownButton3.Enabled = false;
            toolStripDropDownButton4.Enabled = false;
        }

        #endregion
    }
}
