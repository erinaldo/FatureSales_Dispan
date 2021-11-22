using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class formSelecaoRepresentante : Form
    {
        public bool composto = false;
        public bool IPI = false;

        public formSelecaoRepresentante()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(campoLookupCODRPR.Get()))
            {
                if (composto == false && IPI == false)
                {
                    RelTabPrecoRevenda relatorio = new RelTabPrecoRevenda();
                    relatorio.codRPR = campoLookupCODRPR.Get();
                    DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                    tool.ShowRibbonPreviewDialog();
                }
                else if (composto == false && IPI == true)
                {
                    RelTabPrecoRevendaSipi relatorio = new RelTabPrecoRevendaSipi();
                    relatorio.codRPR = campoLookupCODRPR.Get();
                    DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                    tool.ShowRibbonPreviewDialog();
                }
                else
                {
                    if (IPI == false)
                    {
                        RelTabPrecoRevendaComposto relatorio = new RelTabPrecoRevendaComposto();
                        relatorio.codRPR = campoLookupCODRPR.Get();
                        DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                        tool.ShowRibbonPreviewDialog();
                    }
                    else
                    {
                        RelTabPrecoRevendaCompostoSipi relatorio = new RelTabPrecoRevendaCompostoSipi();
                        relatorio.codRPR = campoLookupCODRPR.Get();
                        DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(relatorio);
                        tool.ShowRibbonPreviewDialog();
                    }
                   
                }

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
