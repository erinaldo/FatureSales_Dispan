using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelTabPrecoRevendaComposto : DevExpress.XtraReports.UI.XtraReport
    {
        public string codRPR = string.Empty;

        public string codRevenda;


        public RelTabPrecoRevendaComposto()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE ID = (SELECT IDIMAGEM FROM GCOLIGADA WHERE CODCOLIGADA = ?)";
            sSql = AppLib.Context.poolConnection.Get().ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });

            byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get().ExecGetField(null, sSql, new Object[] { });
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            xrPictureBox1.Image = Image.FromStream(ms);
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void DetailReport_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT CODPRDREVENDA, CODPRDREVENDA + ' - ' + DESCRICAOPRDREVENDA REVENDA FROM TMOVCOMPL INNER JOIN TMOV ON TMOVCOMPL.IDMOV = TMOV.IDMOV AND TMOVCOMPL.CODCOLIGADA = TMOV.CODCOLIGADA WHERE TMOV.CODRPR = ? AND TMOVCOMPL.CODPRDREVENDA <> ''", new object[] { codRPR });
            DetailReport.DataSource = dt;
            xrLabel17.DataBindings.Add("Text", null, "REVENDA");
            xrLabel18.DataBindings.Add("Text", null, "CODPRDREVENDA");
        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT 
 (SELECT CODIGOPRD FROM TPRODUTO WHERE CODCOLPRD = TITMMOV.CODCOLIGADA AND IDPRD = TITMMOV.IDPRD) CODIGOPRD, 
 (SELECT DESCPADRAO FROM TPRDCOMPL WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND IDPRD = TPRODUTO.IDPRD) DESCPADRAO, 
 TITMMOV.CODUND, 
 TITMMOV.QUANTIDADEORIGINAL, 
 TMOVCOMPL.CODPRDREVENDA, 
 TITMMOV.PRECOUNITARIO, 
 TPRODUTO.NUMEROCCF,
 ((TITMMOV.QUANTIDADEORIGINAL * TITMMOV.PRECOUNITARIO) + ISNULL((SELECT SUM(VALOR) VALOR FROM TTRBMOV WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV AND CODTRB = 'IPI' AND NSEQITMMOV = TITMMOV.NSEQITMMOV),0)) TOTAL,
 
 ISNULL((((TITMMOV.QUANTIDADEORIGINAL * TITMMOV.PRECOUNITARIO) 
 + 
 ISNULL((SELECT SUM(VALOR) VALOR FROM TTRBMOV WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV AND CODTRB = 'IPI' AND NSEQITMMOV = TITMMOV.NSEQITMMOV),0))  - ISNULL((((TITMMOV.QUANTIDADEORIGINAL * TITMMOV.PRECOUNITARIO) + ISNULL((SELECT SUM(VALOR) VALOR FROM TTRBMOV WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV AND CODTRB = 'IPI' AND NSEQITMMOV = TITMMOV.NSEQITMMOV),0))
 *
 ISNULL((SELECT PERCDESCONTO FROM ZREPREDESC WHERE CODREPRESENTANTE = ? AND CODTB4FAT IN(
 SELECT DISTINCT(SUBSTRING(CODIGOPRD,1,10)) FROM TPRODUTO WHERE IDPRD IN 
 (SELECT IDPRD FROM TITMMOV WHERE NSEQITMMOV IN (
 SELECT NSEQITMMOV FROM TITMMOVCOMPL WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV AND NSEQITMMOV = TITMMOV.NSEQITMMOV AND TITMMOVCOMPL.PRDREVENDA = 'SIM')
 AND CODCOLIGADA = TMOV.CODCOLIGADA
 AND IDMOV = TMOV.IDMOV)) 
 AND CODCOLIGADA = TMOV.CODCOLIGADA),0)/100),0)),0) VALOR_COM_DESCONTO
 
FROM
 TMOV,
 TMOVCOMPL,
 TITMMOV,
 TITMMOVCOMPL,
 TPRODUTO
WHERE
    TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA
AND TMOV.IDMOV = TMOVCOMPL.IDMOV 

AND TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA
AND TMOV.IDMOV = TITMMOV.IDMOV
 
AND TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA
AND TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV
AND TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV


AND TITMMOV.CODCOLIGADA = TPRODUTO.CODCOLPRD
AND TITMMOV.IDPRD = TPRODUTO.IDPRD
AND TMOV.CODCOLIGADA = 1
AND TMOV.CODTMV = '2.1.03'
AND TMOVCOMPL.TIPOVENDA = 'T'
AND TMOV.STATUS = 'A'
AND TMOV.CODRPR = ?  -- COLOCAR PARAMETRO TMOV.CODRPR
AND TMOVCOMPL.CODPRDREVENDA = ?
ORDER BY
 TITMMOV.NSEQITMMOV

", new object[] { codRPR, codRPR, xrLabel18.Text });
            DetailReport1.DataSource = dt;
            xrLabel6.DataBindings.Add("Text", null, "CODIGOPRD");
            xrLabel7.DataBindings.Add("Text", null, "DESCPADRAO");
            xrLabel11.DataBindings.Add("Text", null, "CODUND");
            xrLabel14.DataBindings.Add("Text", null, "QUANTIDADEORIGINAL", "{0:n2}");
            xrLabel15.DataBindings.Add("Text", null, "CODPRDREVENDA");
            xrLabel16.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n2}");
            xrLabel12.DataBindings.Add("Text", null, "TOTAL", "{0:n2}");
            xrLabel13.DataBindings.Add("Text", null, "VALOR_COM_DESCONTO", "{0:n2}");
            xrLabel20.DataBindings.Add("Text", null, "NUMEROCCF");
        }
    }
}
