using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelAutorizacaoCarregamento : DevExpress.XtraReports.UI.XtraReport
    {
        private int IDCARREGAMENTO;
        public RelAutorizacaoCarregamento(int _IDCARREGAMENTO)
        {
            InitializeComponent();
            IDCARREGAMENTO = _IDCARREGAMENTO;
        }
        private void CarregaLogo()
        {
            string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE ID = (SELECT IDIMAGEM FROM GCOLIGADA WHERE CODCOLIGADA = ?)";
            sSql = AppLib.Context.poolConnection.Get().ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });

            byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get().ExecGetField(null, sSql, new Object[] { });
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            logo.Image = Image.FromStream(ms);
        }

        private void cabecalho()
        {
            string sql = @"SELECT 
TMOV.NUMEROMOV, 
TTRA.NOMEFANTASIA AS TRANSPORTADORA, 
ZCARREGAMENTO.PLACA, 
ZCARREGAMENTO.OBS,
 ZCARREGAMENTO.DATA, 
 ZCARREGAMENTO.HORA,  
 ZCARREGAMENTO.NUMERO, 
 ZCARREGAMENTO.OBS, 
 ZCARREGAMENTO.DATAAGENDAMENTO
FROM 
ZCARREGAMENTO,
TTRA,
TMOV,
ZPROGRAMACAOCARREGAMENTO
WHERE
ZCARREGAMENTO.IDCARREGAMENTO = ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO
AND ZPROGRAMACAOCARREGAMENTO.IDMOV = TMOV.IDMOV
AND ZCARREGAMENTO.CODTRA = TTRA.CODTRA
AND TTRA.CODCOLIGADA = ?
AND ZCARREGAMENTO.IDCARREGAMENTO = ?";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { AppLib.Context.Empresa, IDCARREGAMENTO });
            if (dt.Rows.Count.Equals(0))
            {
                System.Windows.Forms.MessageBox.Show("Não foi possível gerar o relatório. Não existe item pra ser carregado.", "Informação do Sistema.", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            txtIDCARREGAMENTO.Text = dt.Rows[0]["NUMERO"].ToString();
            txtTRANSPORTADORA.Text = dt.Rows[0]["TRANSPORTADORA"].ToString();
            txtPLACA.Text = dt.Rows[0]["PLACA"].ToString();
            txtHora.Text = dt.Rows[0]["HORA"].ToString();
            txtData.Text = Convert.ToDateTime(dt.Rows[0]["DATA"].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["DATAAGENDAMENTO"].ToString()))
            {
                xrLabel12.Text = Convert.ToDateTime(dt.Rows[0]["DATAAGENDAMENTO"].ToString()).ToShortDateString();
            }
            xrLabel7.Text = dt.Rows[0]["OBS"].ToString();
            xrLabel14.Text = AppLib.Context.Usuario;
        }

        private void detalhe()
        {
            string sql = @"SELECT DISTINCT TMOV.NUMEROMOV, FCFO.NOMEFANTASIA CLIENTE, TRPR.NOMEFANTASIA REPRESENTANTE, ZPROGRAMACAOCARREGAMENTO.CARREGAR
FROM TMOV,
 FCFO,
 TRPR,
 ZPROGRAMACAOCARREGAMENTO,
 TITMMOV,
 ZCARREGAMENTO,
 TTRA
WHERE 
 ZPROGRAMACAOCARREGAMENTO.IDMOV = TMOV.IDMOV AND
 TMOV.CODCOLCFO = FCFO.CODCOLIGADA AND
 TMOV.CODCFO = FCFO.CODCFO AND
 TMOV.CODRPR = TRPR.CODRPR AND
 ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND
 ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO = ZCARREGAMENTO.IDCARREGAMENTO AND
 ZCARREGAMENTO.CODTRA = TTRA.CODTRA AND
 ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA AND
 ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV AND
 ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TTRA.CODCOLIGADA AND
 ZCARREGAMENTO.IDCARREGAMENTO = ? 
AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = ?
AND TRPR.CODCOLIGADA = ?
AND ZPROGRAMACAOCARREGAMENTO.CARREGAMENTO = 'S'
";
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { IDCARREGAMENTO, AppLib.Context.Empresa, AppLib.Context.Empresa });
            this.DetailReport.DataSource = dt;
            txtNUMEROMOVIMENTO.DataBindings.Add("Text", null, "NUMEROMOV");
            txtCLIENTE.DataBindings.Add("Text", null, "CLIENTE");
            txtREPRESENTANTE.DataBindings.Add("Text", null, "REPRESENTANTE");
            txtNseqConca.DataBindings.Add("Text", null, "CARREGAR");
        }

        private void TopMargin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CarregaLogo();
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            cabecalho();
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            detalhe();
        }


    }
}
