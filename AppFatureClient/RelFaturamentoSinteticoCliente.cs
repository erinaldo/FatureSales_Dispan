using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelFaturamentoSinteticoCliente : DevExpress.XtraReports.UI.XtraReport
    {
        DateTime dataInicial;
        DateTime dataFinal;
        decimal valor, valorTotal;
        public RelFaturamentoSinteticoCliente(DateTime dataini, DateTime datafin)
        {
            InitializeComponent();
            dataInicial = dataini;
            dataFinal = datafin;
        }

        private void CarregaLogo()
        {
            string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE ID = (SELECT IDIMAGEM FROM GCOLIGADA WHERE CODCOLIGADA = ?)";
            sSql = AppLib.Context.poolConnection.Get().ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });

            byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get().ExecGetField(null, sSql, new Object[] { });
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            xrPictureBox1.Image = Image.FromStream(ms);
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CarregaLogo();
        }
        private void grupo()
        {

            string sql = @"SELECT 
TMOV.CODCOLCFO,
TMOV.CODCFO,
FCFO.NOMEFANTASIA,
TMOV.CODTMV,
TMOV.NUMEROMOV,
FCFO.CIDADE,
FCFO.CODETD,
TMOV.VALORLIQUIDOORIG,
TMOV.DATAEMISSAO
FROM TMOV (NOLOCK)
LEFT OUTER JOIN FCFO (NOLOCK) ON ((TMOV.CODCOLCFO=FCFO.CODCOLIGADA) AND (TMOV.CODCFO=FCFO.CODCFO)) 
LEFT OUTER JOIN TMOVCOMPL (NOLOCK) ON ((TMOV.CODCOLIGADA=TMOVCOMPL.CODCOLIGADA) AND (TMOV.IDMOV=TMOVCOMPL.IDMOV)) 
WHERE TMOV.CODCOLIGADA = 1
AND TMOV.STATUS = 'F'
AND TMOV.CODTMV LIKE '2.1.10'
AND TMOV.DATAEMISSAO >= ?
AND TMOV.DATAEMISSAO <= ?
AND TMOVCOMPL.DEMONSBRINDE = 'NÃO'
ORDER BY
FCFO.NOMEFANTASIA ASC,
TMOV.NUMEROMOV ASC
";
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dataInicial, dataFinal });
                this.DetailReport.DataSource = dt;
                txtCLIENTE.DataBindings.Add("Text", null, "NOMEFANTASIA");
                txtCODCFO.DataBindings.Add("Text", null, "CODCFO");
                txtCODTMV.DataBindings.Add("Text", null, "CODTMV");
                txtDATAEMISSAO.DataBindings.Add("Text", null, "DATAEMISSAO", "{0:dd/MM/yyyy}");
                txtNUMEROMOV.DataBindings.Add("Text", null, "NUMEROMOV");
                txtCIDADE.DataBindings.Add("Text", null, "CIDADE");
                txtESTADO.DataBindings.Add("Text", null, "CODETD");
                txtVALOR.DataBindings.Add("Text", null, "VALORLIQUIDOORIG", "{0:n2}");
                //Criando o grupo
                GroupHeader1.GroupFields.Add(new GroupField("NOMEFANTASIA"));
                GroupHeader1.GroupFields.Add(new GroupField("CODCFO"));
            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("Favor fornecer uma data válida.", "Informação do Sistema.", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            grupo();
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            txttotal.Text = string.Format("{0:n}", (valor ));
            valor = 0;
        }

        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                valor = valor + Convert.ToDecimal(txtVALOR.Text);
            }
            catch (Exception)
            {
                valor = 0;
            }
            
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                valorTotal = valorTotal + Convert.ToDecimal(txttotal.Text);
            }
            catch (Exception)
            {
                valorTotal = 0;
            }
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            txtSubTotal.Text = string.Format("{0:n}", valorTotal);
            valor = 0;
        }
    }
}
