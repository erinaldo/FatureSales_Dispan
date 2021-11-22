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
    public partial class FormRelacaoParcial : Form
    {
        string tipo;

        public FormRelacaoParcial(string _tipo)
        {
            this.tipo = _tipo;
            InitializeComponent();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (tipo.Equals("Representante"))
            {
                if (string.IsNullOrEmpty(txtCodigoAuxiliar.textEdit1.Text))
                {
                    txtCodigoAuxiliar.textEdit1.Text = "%";
                }
                else if (txtCodigoAuxiliar.textEdit1.Text != "%")
                {
                    txtCodigoAuxiliar.textEdit1.Text = "%" + txtCodigoAuxiliar.textEdit1.Text + "%";
                }
                RelProgCarregamentoRepresentante rel = new RelProgCarregamentoRepresentante(Convert.ToDateTime(txtDataIni.Get()), Convert.ToDateTime(txtDataFin.Get()), campoLookup1.textBoxCODIGO.Text, txtCliente.textEdit1.Text);
                new DevExpress.XtraReports.UI.ReportPrintTool(rel).ShowRibbonPreviewDialog();
                this.Dispose();
            }
            else if (tipo.Equals("Data"))
            {
                if (string.IsNullOrEmpty(txtCodigoAuxiliar.textEdit1.Text))
                {
                    txtCodigoAuxiliar.textEdit1.Text = "%";
                }
                else if (txtCodigoAuxiliar.textEdit1.Text != "%")
                {
                    txtCodigoAuxiliar.textEdit1.Text = "%" + txtCodigoAuxiliar.textEdit1.Text + "%";
                }
                RelRelacaoParcial rel = new RelRelacaoParcial(Convert.ToDateTime(txtDataIni.Get()), Convert.ToDateTime(txtDataFin.Get()), txtCodigoAuxiliar.textEdit1.Text);
                new DevExpress.XtraReports.UI.ReportPrintTool(rel).ShowRibbonPreviewDialog();
                this.Dispose();
            }
            else if (tipo.Equals("Concluído"))
            {
                if (string.IsNullOrEmpty(txtCodigoAuxiliar.textEdit1.Text))
                {
                    txtCodigoAuxiliar.textEdit1.Text = "%";
                }
                else if (txtCodigoAuxiliar.textEdit1.Text != "%")
                {
                    txtCodigoAuxiliar.textEdit1.Text = "%" + txtCodigoAuxiliar.textEdit1.Text + "%";
                }
                RelRelacaoCarregamentoCompleto rel = new RelRelacaoCarregamentoCompleto(Convert.ToDateTime(txtDataIni.Get()), Convert.ToDateTime(txtDataFin.Get()), txtCodigoAuxiliar.textEdit1.Text);
                new DevExpress.XtraReports.UI.ReportPrintTool(rel).ShowRibbonPreviewDialog();
                this.Dispose();
            }
            else if (tipo.Equals("Romaneio"))
            {
                if (string.IsNullOrEmpty(txtCodigoAuxiliar.textEdit1.Text))
                {
                    txtCodigoAuxiliar.textEdit1.Text = "%";
                }
                else if (txtCodigoAuxiliar.textEdit1.Text != "%")
                {
                    txtCodigoAuxiliar.textEdit1.Text = "%" + txtCodigoAuxiliar.textEdit1.Text + "%"; 
                }

                // João Pedro Luchiari - 04/01/2018
                AppLib.Data.SqlClient timeout = new AppLib.Data.SqlClient();
                timeout.Timeout = 4000;

                //RelRomaneio rel = new RelRomaneio(Convert.ToDateTime(txtDataIni.Get()), Convert.ToDateTime(txtDataFin.Get()), txtCodigoAuxiliar.textEdit1.Text);
                RelRomaneio2 rel = new RelRomaneio2(Convert.ToDateTime(txtDataIni.Get()), Convert.ToDateTime(txtDataFin.Get()), txtCodigoAuxiliar.textEdit1.Text);
                new DevExpress.XtraReports.UI.ReportPrintTool(rel).ShowRibbonPreviewDialog();
                this.Dispose();
            }
        }

        private void FormRelacaoParcial_Load(object sender, EventArgs e)
        {
            if (!tipo.Equals("Representante"))
            {
                campoLookup1.Visible = false;
                btnLimparLista.Visible = false;
                txtCodigoAuxiliar.Visible = false;
                label3.Text = "Código Auxiliar";
                txtCodigoAuxiliar.Visible = true;
                txtCodigoAuxiliar.textEdit1.Text = "%";
                txtCliente.Visible = false;
                lblCliente.Visible = false;
            }
            else
            {
                label3.Text = "Representante";
                txtCodigoAuxiliar.Visible = false;
                txtCliente.Visible = true;
                lblCliente.Visible = true;
                txtCliente.textEdit1.Text = "%";
            }
        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1, "SELECT TRPR.CODRPR, TRPR.NOMEFANTASIA FROM TRPR, TMOV, ZPROGRAMACAOCARREGAMENTO WHERE TRPR.CODRPR = TMOV.CODRPR AND  TRPR.CODCOLIGADA = TMOV.CODCOLIGADA AND TMOV.IDMOV = ZPROGRAMACAOCARREGAMENTO.IDMOV AND TMOV.CODCOLIGADA = ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA GROUP BY TRPR.NOMEFANTASIA, TRPR.CODRPR", new Object[] { AppLib.Context.Empresa });
        }

        private void btnLimparLista_Click(object sender, EventArgs e)
        {
            campoLookup1.Clear();
        }

        private void campoLookup1_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM TRPR WHERE CODCOLIGADA = ? AND CODRPR = ?";
            campoLookup1.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup1.Conexao).ExecGetField("", sql, new object[] {AppLib.Context.Empresa, campoLookup1.Get() }).ToString();
        }
    }
}
