using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormRequisicaoDevolucaoFiltro : Form
    {
        public DateTime? DtaInicio { get; private set; } = null;
        public DateTime? DtaFim { get; private set; } = null;
        public String Representante { get; private set; } = null;
        public String Cliente { get; private set; } = null;
        public String Status { get; private set; } = null;

        public FormRequisicaoDevolucaoFiltro()
        {
            InitializeComponent();
            campoLookupCODCFO.modeloNovo();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("en-US");

            if (ceData.Checked)
            {
                

                DtaInicio = Convert.ToDateTime(txtDataIni.Get(), culture);
                DtaFim = Convert.ToDateTime(txtDataFin.Get(), culture);
            }else
            {
                DtaInicio = Convert.ToDateTime("01/01/1900", culture);
                DtaFim = Convert.ToDateTime("01/01/3000", culture);
            }
            
            if(ceRepresentante.Checked)
            {
                Representante = campoLookupCODRPR.textBoxCODIGO.Text;
            }else
            {
                Representante = "%";
            }

            if (ceCliente.Checked)
            {
                Cliente = campoLookupCODCFO.textBoxCODIGO.Text;
            }else if(ceMultiselecao.Checked)
            {
                using (AppLib.Windows.FormVisao visao = new AppLib.Windows.FormVisao())
                {
                    visao.grid1.Consulta = "select * from FCFO where ATIVO = 1".Split(' ');
                    visao.ShowDialog();
                    DataRowCollection drc = visao.grid1.GetDataRows();

                    List<string> cod = new List<string>();

                    foreach (DataRow row in drc)
                    {
                        cod.Add(String.Format("'{0}'", (String)row["CODCFO"]));
                    }

                    Cliente = String.Join(",", cod);
                }
            }
            else
            {
                Cliente = "%";
            }
            
            if(ceFaturado.Checked && ceAFaturar.Checked)
            {
                Status = "'F','A'";
            }else if(ceFaturado.Checked && !ceAFaturar.Checked)
            {
                Status = "'F'";
            }
            else
            {
                Status = "'A'";
            }

            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DtaInicio = null;
            DtaFim = null;
            Representante = null;
            Cliente = null;
            this.Dispose();
        }

        private void ceData_CheckedChanged(object sender, EventArgs e)
        {
            txtDataIni.Enabled = ceData.Checked;
            txtDataFin.Enabled = ceData.Checked;
        }

        private void ceRepresentante_CheckedChanged(object sender, EventArgs e)
        {
            campoLookupCODRPR.Enabled = ceRepresentante.Checked;
        }

        private void ceCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (ceMultiselecao.Checked && ceCliente.Checked)
            {
                ceMultiselecao.Checked = false;
            }
            campoLookupCODCFO.Enabled = ceCliente.Checked;
        }

        private void ceAFaturar_CheckedChanged(object sender, EventArgs e)
        {
            if(!ceFaturado.Checked && !ceAFaturar.Checked)
            {
                ceFaturado.Checked = true;
            }
        }

        private void ceFaturado_CheckedChanged(object sender, EventArgs e)
        {
            if (!ceFaturado.Checked && !ceAFaturar.Checked)
            {
                ceAFaturar.Checked = true;
            }
        }

        private void ceMultiselecao_CheckedChanged(object sender, EventArgs e)
        {
            if(ceMultiselecao.Checked && ceCliente.Checked)
            {
                ceCliente.Checked = false;
            }
            
        }
    }
}
