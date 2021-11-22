using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormFaturamentoSinteticoCliente : Form
    {
        public FormFaturamentoSinteticoCliente()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            RelFaturamentoSinteticoCliente rel = new RelFaturamentoSinteticoCliente(Convert.ToDateTime(txtDataIni.Get()), Convert.ToDateTime(txtDataFin.Get()));
            new DevExpress.XtraReports.UI.ReportPrintTool(rel).ShowRibbonPreviewDialog();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
