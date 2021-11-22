using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelTabImplemento : DevExpress.XtraReports.UI.XtraReport
    {
        public string Conexao { get; set; }

        public RelTabImplemento()
        {
            InitializeComponent();
        }

        private void CarregaLogo()
        {
            string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE ID = (SELECT IDIMAGEM FROM GCOLIGADA WHERE CODCOLIGADA = ?)";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });

            byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(null, sSql, new Object[] { });
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            xrPictureBox1.Image = Image.FromStream(ms);        
        }

        private void Detalhe()
        {
                  
        }

        private void Detalhe1()
        {
            string sSql = @"SELECT CAST(TPRDCOMPL.DESCPADRAO AS VARCHAR (2000)) DESCPADRAO, TPRD.PRECO1, TPRD.CODIGOAUXILIAR, TPRD.DESCRICAO, TPRD.IDPRD, TPRD.NUMEROCCF, TPRD.CODIGOPRD
FROM TPRD, TPRDCOMPL
WHERE TPRD.CODCOLIGADA = TPRDCOMPL.CODCOLIGADA AND
TPRD.IDPRD = TPRDCOMPL.IDPRD AND
TPRDCOMPL.TABELAPRECO = 'SIM'  AND
TPRD.CODIGOPRD LIKE '01.004.%' AND
TPRD.ULTIMONIVEL = 1 AND
TPRD.CODCOLIGADA = ?
ORDER BY TPRD.DESCRICAO";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport.DataSource = dt;

            txtIDPRD.DataBindings.Add("Text", null, "IDPRD");
            txtCODIGOAUXILIAR.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
            txtCODIGOPRD.DataBindings.Add("Text", null, "CODIGOPRD");
            txtDESCRICAO.DataBindings.Add("Text", null, "DESCPADRAO");
            txtNUMEROCCF.DataBindings.Add("Text", null, "NUMEROCCF");
            txtPRECOUNITARIO.DataBindings.Add("Text", null, "PRECO1", "{0:n}");
        }

        private void Detalhe2()
        {
            string sSql = @"SELECT TPRD.CODIGOAUXILIAR, TPRDCOMPL.DESCPADRAO, ISNULL(TPRD.PRECO1,0), TPRD.IDPRD, TPRD.CODIGOPRD
FROM TPRD, TPRDCOMPL
WHERE TPRD.CODCOLIGADA = TPRDCOMPL.CODCOLIGADA AND
TPRD.IDPRD = TPRDCOMPL.IDPRD AND
TPRD.CODIGOPRD LIKE '99.'+ SUBSTRING( ? , 4, 12)AND
TPRD.CODCOLIGADA = ? ";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { txtCODIGOPRD.Text, AppLib.Context.Empresa });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport1.DataSource = dt;

            txtIDPRD2.DataBindings.Add("Text", null, "IDPRD");          
        }

        private void Detalhe3()
        {
            string sSql = @"SELECT TPRDCOMPOSTO.QUANTIDADE, 
(CAST(TPRDCOMPL.DESCPADRAO AS VARCHAR (2000))) DESCPADRAO, 
ISNULL(TPRD.PRECO1,0) PRECO1, 
TPRD.CODIGOAUXILIAR, 
TPRD.CODUNDVENDA, 
RTRIM(CONVERT(CHAR,CONVERT(INT,ISNULL(TTRBPRD.ALIQUOTA,'0'))))+ '%' ALIQUOTA, 
TPRD.CODIGOPRD, 
TPRDCOMPOSTO.QUANTIDADE * ISNULL(TPRD.PRECO1,0) PRECO

FROM 
TPRD
LEFT OUTER JOIN TTRBPRD ON TTRBPRD.CODCOLIGADA = TPRD.CODCOLIGADA AND TTRBPRD.IDPRD = TPRD.IDPRD
INNER JOIN TPRDCOMPL ON TPRDCOMPL.CODCOLIGADA = TPRD.CODCOLIGADA AND TPRDCOMPL.IDPRD = TPRD.IDPRD
INNER JOIN TPRDCOMPOSTO ON  TPRDCOMPOSTO.CODCOLIGADA = TPRD.CODCOLIGADA AND TPRDCOMPOSTO.IDPRDCOMPONENTE = TPRD.IDPRD

WHERE 
    TPRDCOMPOSTO.IDPRD = ? 
AND (TPRD.CODIGOAUXILIAR IS NULL OR TPRD.CODIGOAUXILIAR NOT IN ( ? )) 
AND TPRD.ULTIMONIVEL = 1 
AND TPRDCOMPOSTO.IDPRDCOMPONENTE <> ? 
AND TPRD.CODCOLIGADA = ? 
AND TTRBPRD.CODTRB = 'IPI'
ORDER BY TPRD.DESCRICAO  ";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { txtIDPRD2.Text, txtCODIGOPRD.Text, txtIDPRD.Text, AppLib.Context.Empresa });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport2.DataSource = dt;

            txtCODIGOPRD2.DataBindings.Add("Text", null, "CODIGOPRD");
            txtQUANTIDADE2.DataBindings.Add("Text", null, "QUANTIDADE", "{0:n}");
            txtCODUND2.DataBindings.Add("Text", null, "CODUNDVENDA");
            txtDESCRICAO2.DataBindings.Add("Text", null, "DESCPADRAO");
            txtIPI2.DataBindings.Add("Text", null, "ALIQUOTA");
            txtPRECOUNITARIO2.DataBindings.Add("Text", null, "PRECO", "{0:n}");
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CarregaLogo();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe();
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe1();
        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe2();
        }

        private void DetailReport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe3();
        }
    }
}
