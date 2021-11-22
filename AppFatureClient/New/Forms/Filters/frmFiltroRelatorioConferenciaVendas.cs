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

namespace AppFatureClient.New.Forms.Filters
{
    public partial class frmFiltroRelatorioConferenciaVendas : Form
    {
        public string DataInicial, DataFinal, CodVendendor;

        public frmFiltroRelatorioConferenciaVendas()
        {
            InitializeComponent();
        }

        private void frmFiltroRelatorioConferenciaVendas_Load(object sender, EventArgs e)
        {
            CarregaLookup();
        }

        private void dteDataInicial_EditValueChanged(object sender, EventArgs e)
        {
            DataInicial = Convert.ToDateTime(dteDataInicial.Text).ToString("yyy-MM-dd HH:mm:ss");
        }

        private void dteDataFinal_EditValueChanged(object sender, EventArgs e)
        {
            DataFinal = Convert.ToDateTime(dteDataFinal.Text).ToString("yyy-MM-dd HH:mm:ss");
        }

        private void lpVendedor_EditValueChanged(object sender, EventArgs e)
        {
            if (lpVendedor.EditValue != null)
            {
                CodVendendor = lpVendedor.EditValue.ToString();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidaFiltros())
            {
                dxErrorProvider1.ClearErrors();

                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos

        private void CarregaLookup()
        {
            DataTable dtFiltro = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
	                                                                                        CODVEN AS 'Código',
	                                                                                        NOME AS 'Nome'
                                                                                        FROM 
	                                                                                        TVEN 
                                                                                        WHERE 
	                                                                                        CODCOLIGADA = ? 
                                                                                        AND INATIVO = 0 
                                                                                        AND VENDECOMPRA IN (0) 
                                                                                        AND CODVEN NOT IN ('0001','0002')
                                                                                        ORDER BY NOME", new object[] { AppLib.Context.Empresa });

            lpVendedor.Properties.DataSource = dtFiltro;
            lpVendedor.Properties.DisplayMember = dtFiltro.Columns["Nome"].ToString();
            lpVendedor.Properties.ValueMember = dtFiltro.Columns["Código"].ToString();
            lpVendedor.Properties.NullText = "Selecione...";
        }

        private bool ValidaFiltros()
        {
            bool valida = true;

            if (string.IsNullOrEmpty(DataInicial))
            {
                dxErrorProvider1.SetIconAlignment(dteDataInicial, ErrorIconAlignment.TopRight);
                dxErrorProvider1.SetError(dteDataInicial, "Informe a data inicial.");
                valida = false;
            }
            else
            {
                dxErrorProvider1.ClearErrors();
            }

            if (string.IsNullOrEmpty(DataFinal))
            {
                dxErrorProvider1.SetIconAlignment(dteDataFinal, ErrorIconAlignment.TopRight);
                dxErrorProvider1.SetError(dteDataFinal, "Informe a data final.");
                valida = false;
            }
            else
            {
                dxErrorProvider1.ClearErrors();
            }

            return valida;
        }

        #endregion
    }
}
