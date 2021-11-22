using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient.New.Forms.Process
{
    public partial class frmCopiaMovimento : Form
    {
        #region Variáveis

        public DataRowCollection drcMovimento;
        public DataTable dtMovimento = new DataTable();
        public DataRow rowMovimento;

        #endregion

        public frmCopiaMovimento()
        {
            InitializeComponent();
        }

        private void frmCopiaMovimento_Load(object sender, EventArgs e)
        {
            CarregaLookupClienteFornecedor();
        }

        private void lpClienteFornecedor_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lpClienteFornecedor.EditValue.ToString()))
            {
                CarregaGrid(lpClienteFornecedor.EditValue.ToString());
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (gvMovimento.OptionsFind.AlwaysVisible == true)
            {
                gvMovimento.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gvMovimento.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            if (gvMovimento.OptionsView.ShowGroupPanel == true)
            {
                gvMovimento.OptionsView.ShowGroupPanel = false;
                gvMovimento.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gvMovimento.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void gvMovimento_DoubleClick(object sender, EventArgs e)
        {
            if (gvMovimento.RowCount > 0)
            {
                DataRow row = gvMovimento.GetDataRow(Convert.ToInt32(gvMovimento.GetSelectedRows().GetValue(0).ToString()));
                SelecionarMovimentoParaCopia(row);

                this.Dispose();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (gvMovimento.RowCount > 0)
            {
                DataRow row = gvMovimento.GetDataRow(Convert.ToInt32(gvMovimento.GetSelectedRows().GetValue(0).ToString()));
                SelecionarMovimentoParaCopia(row);

                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #region Métodos

        private void CarregaLookupClienteFornecedor()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
	                                                                                DISTINCT 
		                                                                                FCFO.CODCFO AS 'Código', 
		                                                                                FCFO.NOMEFANTASIA AS 'Nome'
                                                                                FROM 
	                                                                                TMOV
	                                                                                INNER JOIN FCFO ON TMOV.CODCOLCFO = FCFO.CODCOLIGADA AND TMOV.CODCFO = FCFO.CODCFO
                                                                                WHERE
	                                                                                TMOV.CODCOLIGADA = 1
                                                                                AND TMOV.CODTMV = '2.1.10'
                                                                                AND FCFO.ATIVO = 1
                                                                                ORDER BY 
	                                                                                FCFO.NOMEFANTASIA ASC", new object[] { });

            lpClienteFornecedor.Properties.DataSource = dt;
            lpClienteFornecedor.Properties.DisplayMember = dt.Columns["Nome"].ToString();
            lpClienteFornecedor.Properties.ValueMember = dt.Columns["Código"].ToString();
            lpClienteFornecedor.Properties.NullText = "Selecione...";

            lpClienteFornecedor.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        }

        private void CarregaGrid(string codCFO)
        {
            dtMovimento = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM ZVWPEDIDO WHERE CODCOLIGADA = ? AND CODCFO = ?", new object[] { AppLib.Context.Empresa, codCFO });

            gcMovimento.DataSource = dtMovimento;
            gvMovimento.BestFitColumns();
        }

        private DataRow SelecionarMovimentoParaCopia(DataRow row)
        {
            rowMovimento = row;

            return rowMovimento;
        }

        #endregion
    }
}
