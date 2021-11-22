using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelCarregamento : DevExpress.XtraReports.UI.XtraReport
    {
        private int IDCARREGAMENTO;
        public RelCarregamento(int _IDCARREGAMENTO)
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
            string sql = @"SELECT ZCARREGAMENTO.IDCARREGAMENTO, TTRA.NOMEFANTASIA AS TRANSPORTADORA, ZCARREGAMENTO.PLACA, ZCARREGAMENTO.OBS, ZCARREGAMENTO.DATA FROM ZCARREGAMENTO, TTRA WHERE ZCARREGAMENTO.CODTRA = TTRA.CODTRA AND IDCARREGAMENTO = ?";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, IDCARREGAMENTO);
            foreach (System.Data.DataRow row in dt.Rows)
            {
                txtIDCARREGAMENTO.Text = row["IDCARREGAMENTO"].ToString();
                txtTRANSPORTADORA.Text = row["TRANSPORTADORA"].ToString();
                txtPLACA.Text = row["PLACA"].ToString();
                txtObservacao.Text = row["OBS"].ToString();
                txtData.Text = Convert.ToDateTime(row["DATA"].ToString()).ToShortDateString();
            }


        }
        private void detalhe()
        {
            string sql = @"SELECT TMOV.NUMEROMOV, FCFO.NOMEFANTASIA CLIENTE, TRPR.NOMEFANTASIA REPRESENTANTE, FCFO.CIDADE, FCFO.CODETD UF, ZCARREGAMENTOITEMS.QTDE, TPRODUTO.NOMEFANTASIA PRODUTO
FROM
 TMOV,
 FCFO,
 TRPR,
 ZCARREGAMENTOITEMS,
 TPRODUTO, 
 TITMMOV,
 ZCARREGAMENTO,
 TTRA
WHERE
 ZCARREGAMENTOITEMS.IDMOV = TMOV.IDMOV AND
 TMOV.CODCFO = FCFO.CODCFO AND
 TMOV.CODRPR = TRPR.CODRPR AND
 ZCARREGAMENTOITEMS.IDMOV = TITMMOV.IDMOV AND
 TITMMOV.IDPRD = TPRODUTO.IDPRD AND
 ZCARREGAMENTOITEMS.IDCARREGAMENTO = ZCARREGAMENTO.IDCARREGAMENTO AND
 ZCARREGAMENTO.CODTRA = TTRA.CODTRA AND
 ZCARREGAMENTOITEMS.CODCOLIGADA = TITMMOV.CODCOLIGADA AND
 ZCARREGAMENTOITEMS.NSEQITMMOV = TITMMOV.NSEQITMMOV AND
 ZCARREGAMENTOITEMS.CODCOLIGADA = TTRA.CODCOLIGADA AND
 ZCARREGAMENTO.IDCARREGAMENTO = ? AND ZCARREGAMENTOITEMS.CODCOLIGADA = ?
";
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { IDCARREGAMENTO, AppLib.Context.Empresa});
            this.DetailReport.DataSource = dt;
            txtNUMEROMOVIMENTO.DataBindings.Add("Text", null, "NUMEROMOV");
            txtCLIENTE.DataBindings.Add("Text", null, "CLIENTE");
            txtREPRESENTANTE.DataBindings.Add("Text", null, "REPRESENTANTE");
            txtCIDADE.DataBindings.Add("Text", null, "CIDADE");
            txtUF.DataBindings.Add("Text", null, "UF");
            txtQTDE.DataBindings.Add("Text", null, "QTDE");
            txtPRODUTO.DataBindings.Add("Text", null, "PRODUTO");
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
