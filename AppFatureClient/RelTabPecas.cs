using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelTabPecas : DevExpress.XtraReports.UI.XtraReport
    {
        public string Conexao { get; set; }

        public RelTabPecas()
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
            string sSql = @"SELECT

CONVERT(VARCHAR(4000), ( SELECT TOP 1 DESCPADRAO FROM TPRDCOMPL T WHERE T.CODCOLIGADA = 1
  AND T.IDPRD = X.IDPRD )) DESCPADRA,
  
--RTRIM(CONVERT(CHAR,CONVERT(INT,ISNULL(X.ALIQUOTA,'0'))))+ '%' IPI,  
isnull(CONVERT(varchar(10),CAST(X.ALIQUOTA as int)) + '%','') as 'IPI',

X.*

FROM (

SELECT TPRD.PRECO1, MIN(TPRD.CODIGOAUXILIAR) CODIGOAUXILIAR, 
MIN(TPRD.DESCRICAO) DESCRICAO, TPRD.IDPRD IDPRD, AVG(TTRBPRD.ALIQUOTA) ALIQUOTA, MIN(NUMEROCCF)NCM,
UPPER(MIN(TPRD.CODUNDVENDA)) CODUNDVENDA, MIN(TPRD.CODIGOPRD) CODIGOPRD

FROM 
TPRD
LEFT OUTER JOIN TTRBPRD ON TTRBPRD.CODCOLIGADA = TPRD.CODCOLIGADA AND TTRBPRD.IDPRD = TPRD.IDPRD
INNER JOIN TPRDCOMPL ON TPRDCOMPL.CODCOLIGADA = TPRD.CODCOLIGADA AND TPRDCOMPL.IDPRD = TPRD.IDPRD

WHERE 

   (TPRD.CODIGOPRD LIKE '01.002.%' OR TPRD.CODIGOPRD LIKE '02.%') 
AND TPRD.IDPRD NOT IN (SELECT TPRDCOMPOSTO.IDPRDCOMPONENTE 
					FROM TPRDCOMPOSTO, TPRD
					WHERE TPRDCOMPOSTO.IDPRDCOMPONENTE = TPRD.IDPRD 
					AND TPRDCOMPOSTO.CODCOLIGADA = TPRD.CODCOLIGADA 
					AND TPRDCOMPOSTO.CODCOLIGADA = 1 
					AND TPRD.CODIGOPRD NOT LIKE ('02.001.148.%') 
					AND TPRD.CODIGOPRD NOT LIKE ('02.001.150.%') 
					AND TPRD.CODIGOPRD NOT LIKE ('02.001.196.%') 
					AND TPRD.CODIGOPRD NOT LIKE ('02.001.146.%')) 
AND TPRD.ULTIMONIVEL = 1 
AND TPRD.INATIVO = 0 
AND TPRDCOMPL.TABELAPRECO = 'SIM'
AND TPRD.CODCOLIGADA = ?

GROUP BY TPRD.PRECO1, TPRD.IDPRD 

) X

ORDER BY 1";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport.DataSource = dt;

            txtCODIGOPRD.DataBindings.Add("Text", null, "CODIGOPRD");
            txtDESCRICAO.DataBindings.Add("Text", null, "DESCPADRA");
            txtNUMEROCCF.DataBindings.Add("Text", null, "NCM");
            txtICODUND.DataBindings.Add("Text", null, "CODUNDVENDA");
            txtPRECOUNITARIO.DataBindings.Add("Text", null, "PRECO1", "{0:n}");
            txtIPI.DataBindings.Add("Text", null, "IPI");                  
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CarregaLogo();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Detalhe();
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe();
        }
    }
}
