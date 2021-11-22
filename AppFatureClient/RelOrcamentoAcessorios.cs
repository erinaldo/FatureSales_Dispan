using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelOrcamentoAcessorios : DevExpress.XtraReports.UI.XtraReport
    {
        public System.Data.DataRow SelectRow { get; set; }
        public string Conexao { get; set; }

        private decimal ValorUnitario;
        private decimal ValorTotal;
        private decimal SubTotal;
        private decimal TotalIPI;
        private decimal Desconto;
        private string OBSERVACAO;
        private string MSGFORNEC;

        public RelOrcamentoAcessorios()
        {
            InitializeComponent();

            ValorUnitario = 0;
            ValorTotal = 0;
            SubTotal = 0;
            TotalIPI = 0;
            Desconto = 0;
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
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT 
FCFO.CODCFO, FCFO.NOME, FCFO.CONTATO, TMOV.DATAEMISSAO, TMOVCOMPL.MSGFORNEC, TMOV.VALORFRETE, TMOV.VALORDESP, TMOV.VALOREXTRA1, TMOV.VALORDESC,
RUAPRINCIPAL.DESCRICAO DESTIPORUA, FCFO.RUA, FCFO.NUMERO, BAIRROPRINCIPAL.DESCRICAO DESTIPOBAIRRO, FCFO.BAIRRO, TMOV.NUMEROMOV, TMOVHISTORICO.HISTORICOLONGO,
FCFO.CIDADE, FCFO.CODETD, FCFO.CEP,
CASE TMOVCOMPL.TIPOVENDA WHEN 'C' THEN 'VENDA DIRETA'
WHEN 'R' THEN 'REVENDA' ELSE NULL END TIPOVENDA,

RUAENTREGA.DESCRICAO DESTIPORUAENT, FCFO.RUAENTREGA, FCFO.NUMEROENTREGA, BAIRROENTREGA.DESCRICAO DESTIPOBAIRROENT, FCFO.BAIRROENTREGA, FCFO.CIDADEENTREGA, FCFO.CODETDENTREGA,
FCFO.TELEFONEENTREGA, FCFO.FAXENTREGA, FCFO.CEPENTREGA, FCFO.CGCCFO,

FCFO.EMAIL, TMOVCOMPL.TPCULTURA, FCFO.CIDENTIDADE, FCFO.INSCRESTADUAL, TMOVCOMPL.CONDPGTO, TMOVCOMPL.FINANCIADO, TTRA.NOME NOMETRA, TRPR.NOME NOMERPR, TMOV.CAMPOLIVRE1, TMOV.PRAZOENTREGA, FRETECIFOUFOB

FROM TMOV
LEFT JOIN TTRA ON TTRA.CODCOLIGADA = TMOV.CODCOLIGADA AND TTRA.CODTRA = TMOV.CODTRA
LEFT JOIN TRPR ON TRPR.CODCOLIGADA = TMOV.CODCOLIGADA AND TRPR.CODRPR = TMOV.CODRPR,

FCFO
LEFT JOIN DTIPORUA RUAPRINCIPAL ON FCFO.TIPORUA = RUAPRINCIPAL.CODIGO
LEFT JOIN DTIPOBAIRRO BAIRROPRINCIPAL ON FCFO.TIPOBAIRRO = BAIRROPRINCIPAL.CODIGO
LEFT JOIN DTIPORUA RUAENTREGA ON FCFO.TIPORUAENTREGA = RUAENTREGA.CODIGO
LEFT JOIN DTIPOBAIRRO BAIRROENTREGA ON FCFO.TIPOBAIRROENTREGA = BAIRROENTREGA.CODIGO,
TMOVCOMPL,
TMOVHISTORICO

WHERE TMOV.CODCOLIGADA = ?
AND TMOV.IDMOV = ?
AND FCFO.CODCOLIGADA = TMOV.CODCOLCFO
AND FCFO.CODCFO = TMOV.CODCFO
AND TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA
AND TMOV.IDMOV = TMOVCOMPL.IDMOV
AND TMOV.CODCOLIGADA = TMOVHISTORICO.CODCOLIGADA
AND TMOV.IDMOV = TMOVHISTORICO.IDMOV";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                txtCODCFO.Text = row["CODCFO"].ToString();
                txtNOME.Text = row["NOME"].ToString();
                txtCONTATO.Text = row["CONTATO"].ToString();
                txtDATAEMISSAO.Text = string.Format("{0:dd/MM/yyyy}", row["DATAEMISSAO"]);
                txtDESTIPORUA.Text = row["DESTIPORUA"].ToString();
                txtRUA.Text = row["RUA"].ToString();
                txtNUMERO.Text = row["NUMERO"].ToString();
                txtDESTIPOBAIRRO.Text = row["DESTIPOBAIRRO"].ToString();
                txtBAIRRO.Text = row["BAIRRO"].ToString();
                txtNUMEROMOV.Text = row["NUMEROMOV"].ToString();
                txtCIDADE.Text = row["CIDADE"].ToString();
                txtUF.Text = row["CODETD"].ToString();
                txtCEP.Text = row["CEP"].ToString();
                lblVenda.Text = row["TIPOVENDA"].ToString();
                txtDESTIPORUAENT.Text = row["DESTIPORUAENT"].ToString();
                txtRUAENTREGA.Text = row["RUAENTREGA"].ToString();
                txtNUMEROENTREGA.Text = row["NUMEROENTREGA"].ToString();
                txtDESTIPOBAIRROENT.Text = row["DESTIPOBAIRROENT"].ToString();
                txtBAIRROENT.Text = row["BAIRROENTREGA"].ToString();
                txtCIDADEENTREGA.Text = row["CIDADEENTREGA"].ToString();
                txtUFENTREGA.Text = row["CODETDENTREGA"].ToString();
                txtTELEFONEENTREGA.Text = row["TELEFONEENTREGA"].ToString();
                txtFAXENTREGA.Text = row["FAXENTREGA"].ToString();
                txtCEPENTREGA.Text = row["CEPENTREGA"].ToString();
                txtCGCCFO.Text = row["CGCCFO"].ToString();
                txtEMAILENTREGA.Text = row["EMAIL"].ToString();
                txtTPCULTURA.Text = row["TPCULTURA"].ToString();
                txtCIDENTIDADE.Text = row["CIDENTIDADE"].ToString();
                txtINSCRESTADUAL.Text = row["INSCRESTADUAL"].ToString();

                txtCIF.Text = "[  ]";
                txtFOB.Text = "[  ]";
                if (Convert.ToInt32(row["FRETECIFOUFOB"]) == 1)
                    txtCIF.Text = "[X]";
                else
                    txtFOB.Text = "[X]";

                txtCONDPGTO.Text = row["CONDPGTO"].ToString();
                txtFINANCIADO.Text = row["FINANCIADO"].ToString();
                txtNOMETRA.Text = row["NOMETRA"].ToString();
                txtNOMERPR.Text = row["NOMERPR"].ToString();
                txtCAMPOLIVRE1.Text = row["CAMPOLIVRE1"].ToString();
                txtPRAZOENTREGA.Text = row["PRAZOENTREGA"].ToString();

                txtVALORFRETE.Text = string.Format("{0:n}", row["VALORFRETE"]);
                txtVALORDESP.Text = string.Format("{0:n}", row["VALORDESP"]);
                txtVALOREXTRA1.Text = string.Format("{0:n}", row["VALOREXTRA1"]);
                txtVALORDESC.Text = string.Format("{0:n}", row["VALORDESC"]);
                Desconto = Convert.ToDecimal(row["VALORDESC"]);
                OBSERVACAO = row["HISTORICOLONGO"].ToString();
                MSGFORNEC = row["MSGFORNEC"].ToString();
            }        
        }

        private void Detalhe1()
        {
            ValorUnitario = 0;
            ValorTotal = 0;

            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            //            string sSql = @"SELECT
            //TITMMOV.NSEQITMMOV, TITMMOV.QUANTIDADETOTAL, TITMMOV.CODUND, TITMMOV.IDPRDCOMPOSTO, TITMMOV.PRECOUNITARIO, TITMMOV.QUANTIDADETOTAL * TITMMOV.PRECOUNITARIO AS VALORTOTAL,

            //CAST(CASE WHEN TPRDCOMPL.CODCOLIGADA = 1 THEN   
            //SUBSTRING(TPRDCOMPL.DESCPADRAO ,1,1000)+ '   '+SUBSTRING(TPRDCOMPL.DESCPADRAO ,1001,2000)+ '   '+
            //ISNULL(CAST(TITMMOVHISTORICO.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+TPRD.CODIGOPRD +'     NCM:  '+ TPRD.NUMEROCCF) FROM TPRD WHERE IDPRD = TITMMOV.IDPRD AND CODCOLIGADA = TITMMOV.CODCOLIGADA) + ' - ' + (SELECT 
            //		CASE 
            //			WHEN (SELECT FATORSUBST FROM TPRDCODFISCAL WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND IDPRD = TITMMOV.IDPRD AND CODETD = (SELECT CODETD FROM FCFO WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND CODCFO = TMOV.CODCFO)) > 0 
            //			THEN ' COM ICMS-ST' 
            //			ELSE ''
            //			END ST)  

            //ELSE 
            //CAST(TPRDCOMPL.DESCAUXILIAR AS VARCHAR(5000))+ '   '+
            //ISNULL(CAST(TITMMOVHISTORICO.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+TPRD.CODIGOPRD +'     NCM:  '+ TPRD.NUMEROCCF) FROM TPRD WHERE IDPRD = TITMMOV.IDPRD AND CODCOLIGADA = TITMMOV.CODCOLIGADA) + ' - ' + (SELECT 
            //		CASE 
            //			WHEN (SELECT FATORSUBST FROM TPRDCODFISCAL WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND IDPRD = TITMMOV.IDPRD AND CODETD = (SELECT CODETD FROM FCFO WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND CODCFO = TMOV.CODCFO)) > 0 
            //			THEN ' COM ICMS-ST' 
            //			ELSE ''
            //			END ST)  

            //END AS TEXT) DESCRICAO,

            //ISNULL((
            //SELECT
            //  ALIQUOTA
            //FROM
            //  TTRBMOV (NOLOCK)
            //WHERE    TITMMOV.CODCOLIGADA = TTRBMOV.CODCOLIGADA
            //     AND TITMMOV.IDMOV       = TTRBMOV.IDMOV
            //     AND TITMMOV.NSEQITMMOV  = TTRBMOV.NSEQITMMOV
            //     AND TTRBMOV.CODTRB      = 'IPI'
            //),0) ALIQUOTAIPI,

            //ISNULL((
            //SELECT
            //  VALOR
            //FROM
            //  TTRBMOV (NOLOCK)
            //WHERE    TITMMOV.CODCOLIGADA = TTRBMOV.CODCOLIGADA
            //     AND TITMMOV.IDMOV       = TTRBMOV.IDMOV
            //     AND TITMMOV.NSEQITMMOV  = TTRBMOV.NSEQITMMOV
            //     AND TTRBMOV.CODTRB      = 'IPI'
            //),0) VALORIPI

            //FROM 
            //TITMMOV
            //LEFT OUTER JOIN TITMMOVHISTORICO ON TITMMOVHISTORICO.CODCOLIGADA = TITMMOV.CODCOLIGADA AND TITMMOVHISTORICO.IDMOV = TITMMOV.IDMOV AND TITMMOVHISTORICO.NSEQITMMOV = TITMMOV.NSEQITMMOV
            //INNER JOIN TMOV ON TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA AND TMOV.IDMOV = TITMMOV.IDMOV
            //INNER JOIN TITMMOVCOMPL ON TITMMOVCOMPL.CODCOLIGADA = TITMMOV.CODCOLIGADA AND TITMMOVCOMPL.IDMOV = TITMMOV.IDMOV AND TITMMOVCOMPL.NSEQITMMOV = TITMMOV.NSEQITMMOV
            //INNER JOIN TPRD ON TPRD.IDPRD = TITMMOV.IDPRD
            //INNER JOIN TPRDCOMPL ON TPRDCOMPL.IDPRD = TPRD.IDPRD

            //WHERE 
            //    TITMMOV.CODCOLIGADA = ?
            //AND TITMMOV.IDMOV = ?";
            string sSql = @"SELECT
TITMMOV.NSEQITMMOV, TITMMOV.QUANTIDADETOTAL, TITMMOV.CODUND, TITMMOV.IDPRDCOMPOSTO, TITMMOV.PRECOUNITARIO, TITMMOV.QUANTIDADETOTAL * TITMMOV.PRECOUNITARIO AS VALORTOTAL,

CAST(CASE WHEN TPRDCOMPL.CODCOLIGADA = 1 THEN   
SUBSTRING(TPRDCOMPL.DESCPADRAO ,1,1000)+ '   '+SUBSTRING(TPRDCOMPL.DESCPADRAO ,1001,2000)+ '   '+
ISNULL(CAST(TITMMOVHISTORICO.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+TPRD.CODIGOPRD +'     NCM:  '+ TPRD.NUMEROCCF) FROM TPRD WHERE IDPRD = TITMMOV.IDPRD AND CODCOLIGADA = TITMMOV.CODCOLIGADA)

ELSE 
CAST(TPRDCOMPL.DESCAUXILIAR AS VARCHAR(5000))+ '   '+
ISNULL(CAST(TITMMOVHISTORICO.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+TPRD.CODIGOPRD +'     NCM:  '+ TPRD.NUMEROCCF) FROM TPRD WHERE IDPRD = TITMMOV.IDPRD AND CODCOLIGADA = TITMMOV.CODCOLIGADA) 

END AS TEXT) DESCRICAO,

ISNULL((
SELECT
  ALIQUOTA
FROM
  TTRBMOV (NOLOCK)
WHERE    TITMMOV.CODCOLIGADA = TTRBMOV.CODCOLIGADA
     AND TITMMOV.IDMOV       = TTRBMOV.IDMOV
     AND TITMMOV.NSEQITMMOV  = TTRBMOV.NSEQITMMOV
     AND TTRBMOV.CODTRB      = 'IPI'
),0) ALIQUOTAIPI,

ISNULL((
SELECT
  VALOR
FROM
  TTRBMOV (NOLOCK)
WHERE    TITMMOV.CODCOLIGADA = TTRBMOV.CODCOLIGADA
     AND TITMMOV.IDMOV       = TTRBMOV.IDMOV
     AND TITMMOV.NSEQITMMOV  = TTRBMOV.NSEQITMMOV
     AND TTRBMOV.CODTRB      = 'IPI'
),0) VALORIPI

FROM 
TITMMOV
LEFT OUTER JOIN TITMMOVHISTORICO ON TITMMOVHISTORICO.CODCOLIGADA = TITMMOV.CODCOLIGADA AND TITMMOVHISTORICO.IDMOV = TITMMOV.IDMOV AND TITMMOVHISTORICO.NSEQITMMOV = TITMMOV.NSEQITMMOV
INNER JOIN TMOV ON TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA AND TMOV.IDMOV = TITMMOV.IDMOV
INNER JOIN TITMMOVCOMPL ON TITMMOVCOMPL.CODCOLIGADA = TITMMOV.CODCOLIGADA AND TITMMOVCOMPL.IDMOV = TITMMOV.IDMOV AND TITMMOVCOMPL.NSEQITMMOV = TITMMOV.NSEQITMMOV
INNER JOIN TPRD ON TPRD.IDPRD = TITMMOV.IDPRD
INNER JOIN TPRDCOMPL ON TPRDCOMPL.IDPRD = TPRD.IDPRD

WHERE 
    TITMMOV.CODCOLIGADA = ?
AND TITMMOV.IDMOV = ?";

            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport.DataSource = dt;

            txtNUMEROSEQUENCIAL.DataBindings.Add("Text", null, "NSEQITMMOV");
            txtDESCRICAO.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE.DataBindings.Add("Text", null, "QUANTIDADETOTAL", "{0:n}");
            txtCODUND.DataBindings.Add("Text", null, "CODUND");
            txtIIPI.DataBindings.Add("Text", null, "VALORIPI", "{0:n}");
            txtPRECOUNITARIO.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtVALORTOTAL.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
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
            this.GroupHeader3.GroupFields.Add(new GroupField("SEQUENCIAL"));

            Detalhe1();
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            txtTOTALIZADORPRECOUNITARIO1.Text = string.Format("{0:n}", (ValorTotal / Convert.ToDecimal(txtQUANTIDADE.Text)));
            txtTOTALIZADORVALORTORAL1.Text = string.Format("{0:n}", ValorTotal);

            SubTotal = SubTotal + ValorTotal;
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            ValorUnitario = 0;
            ValorTotal = 0;
        }

        private void DetailReport6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            txtMSGFORNEC.Text = MSGFORNEC;
        }

        private void DetailReport5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            txtOBSERVACAO.Text = OBSERVACAO;
            txtSUBTOTAL.Text = string.Format("{0:n}", SubTotal);
            txtTOTALIPI.Text = string.Format("{0:n}", TotalIPI);
            txtDESPESAS.Text = string.Format("{0:n}", txtVALORDESP.Text);
            txtFRETE.Text = string.Format("{0:n}", txtVALORFRETE.Text);
            txtDEVOLUCAO.Text = string.Format("{0:n}", txtVALOREXTRA1.Text);
            txtSUBTOTALGERAL.Text = string.Format("{0:n}", (SubTotal + TotalIPI + Convert.ToDecimal(txtVALORDESP.Text) + Convert.ToDecimal(txtVALORFRETE.Text)));

            if (Desconto > 0)
            {
                txtDESCDESCONTO.Text = "Desconto Especial:";
                txtDESCONTOESPECIAL.Text = string.Format("{0:n}", Desconto);
            }
            else
            {
                txtDESCDESCONTO.Text = "";
                txtDESCONTOESPECIAL.Text = string.Format("{0:n}", 0);
            }

            txtTOTALGERAL.Text = string.Format("{0:n}", ((SubTotal + TotalIPI + Convert.ToDecimal(txtVALORDESP.Text) + Convert.ToDecimal(txtVALORFRETE.Text)) - Convert.ToDecimal(txtDESCONTOESPECIAL.Text)));
        }

        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPRECOUNITARIO.Text))
                ValorUnitario = ValorUnitario + Convert.ToDecimal(txtPRECOUNITARIO.Text);

            if (!string.IsNullOrEmpty(txtVALORTOTAL.Text))
                ValorTotal = ValorTotal + Convert.ToDecimal(txtVALORTOTAL.Text);
        }
    }
}
