using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelOrcamentoDescontoOff : DevExpress.XtraReports.UI.XtraReport
    {
        public System.Data.DataRow SelectRow { get; set; }
        public string Conexao { get; set; }

        private decimal ValorUnitario;
        private decimal ValorTotal;
        private decimal SubTotal;
        private decimal TotalIPI;
        private decimal Dimensao;
        private decimal Desconto;
        private string OBSERVACAO;
        private string MSGFORNEC;

        public RelOrcamentoDescontoOff()
        {
            InitializeComponent();

            ValorUnitario = 0;
            ValorTotal = 0;
            SubTotal = 0;
            TotalIPI = 0;
            Dimensao = 0;
            Desconto = 0;

            txtVALORFRETE.Text = string.Format("{0:n}", "0");
            txtVALORDESP.Text = string.Format("{0:n}", "0");
            txtVALOREXTRA1.Text = string.Format("{0:n}", "0");
            txtVALORDESC.Text = string.Format("{0:n}", "0");
        }

        private void CarregaLogo()
        {
            /*
            string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE ID = (SELECT IDIMAGEM FROM GCOLIGADA WHERE CODCOLIGADA = ?)";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });

            byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(sSql, new Object[] { });
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            xrPictureBox1.Image = Image.FromStream(ms);
            */
        }

        private void Detalhe()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT 
FCFO.CODCFO, FCFO.NOME, FCFO.CONTATO, FCFO.CGCCFO, TMOV.DATAEMISSAO, TMOV.IDMOV NUMEROMOV, TMOV.HISTORICOLONGO, TMOV.NORDEM, FCFO.SUFRAMA,
CASE TMOVCOMPL.TIPOVENDA WHEN 'C' THEN 'VENDA DIRETA'
WHEN 'R' THEN 'REVENDA' ELSE NULL END TIPOVENDA,
ISNULL(TMOV.VALORFRETE,0) VALORFRETE, 
ISNULL(TMOV.VALORDESP,0) VALORDESP, 
ISNULL(TMOV.VALOREXTRA1,0) VALOREXTRA1, 
ISNULL(TMOV.VALORDESC,0) VALORDESC,


RUAPRINCIPAL.DESCRICAO DESTIPORUA, FCFO.RUA, FCFO.NUMERO, BAIRROPRINCIPAL.DESCRICAO DESTIPOBAIRRO, FCFO.BAIRRO, FCFO.CIDADE, FCFO.CODETD, FCFO.CEP,
RUAENTREGA.DESCRICAO DESTIPORUAENT, FCFO.RUAENTREGA, FCFO.NUMEROENTREGA, BAIRROENTREGA.DESCRICAO DESTIPOBAIRROENT, FCFO.BAIRROENTREGA, FCFO.CIDADEENTREGA, FCFO.CODETDENTREGA, FCFO.TELEFONEENTREGA, FCFO.FAXENTREGA, FCFO.CEPENTREGA,

FCFO.EMAIL, FCFO.CIDENTIDADE, FCFO.INSCRESTADUAL, 
TMOV.MSGFORNEC, TMOV.TPCULTURA, TMOV.CONDPGTO, TMOV.FINANCIADO, 

TTRA.NOME NOMETRA, TRPR.NOME NOMERPR, TMOV.CAMPOLIVRE1, TMOV.PRAZOENTREGA, FRETECIFOUFOB

FROM ZORCAMENTO TMOV
LEFT JOIN ZTTRA TTRA ON TTRA.CODCOLIGADA = TMOV.CODCOLIGADA AND TTRA.CODTRA = TMOV.CODTRA
LEFT JOIN ZTRPR TRPR ON TRPR.CODCOLIGADA = TMOV.CODCOLIGADA AND TRPR.CODRPR = TMOV.CODRPR,

ZVW_FCFOREL FCFO
LEFT JOIN ZDTIPORUA RUAPRINCIPAL ON FCFO.TIPORUA = RUAPRINCIPAL.CODIGO
LEFT JOIN ZDTIPOBAIRRO BAIRROPRINCIPAL ON FCFO.TIPOBAIRRO = BAIRROPRINCIPAL.CODIGO
LEFT JOIN ZDTIPORUA RUAENTREGA ON FCFO.TIPORUAENTREGA = RUAENTREGA.CODIGO
LEFT JOIN ZDTIPOBAIRRO BAIRROENTREGA ON FCFO.TIPOBAIRROENTREGA = BAIRROENTREGA.CODIGO

WHERE TMOV.CODCOLIGADA = ?
AND TMOV.IDMOV = ?
AND 
((TMOV.CODCFO IS NULL AND NOT(TMOV.IDMOV IS NULL)) OR
 (NOT(TMOV.CODCFO) IS NULL AND FCFO.CODCOLIGADA = TMOV.CODCOLCFO))
 
AND
((TMOV.CODCFO IS NULL AND FCFO.CODCFO = TMOV.IDOFF) OR
 (NOT(TMOV.CODCFO) IS NULL AND FCFO.CODCFO = TMOV.CODCFO))";
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
                txtCIDENTIDADE.Text = row["SUFRAMA"].ToString();
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
                txtNORDEM.Text = row["NORDEM"].ToString();
            }        
        }

        private void Detalhe1()
        {
            ValorUnitario = 0;
            ValorTotal = 0;

            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            //            string sSql = @"SELECT
            //TITMMOV.PROJETO SEQUENCIAL, TITMMOV.QUANTIDADE, TITMMOV.CODUND, TITMMOV.IDPRDCOMPOSTO, 
            //(TITMMOV.PRECOUNITARIO - (TITMMOV.PRECOUNITARIO * TMOV.PERCENTUALDESC)/100) PRECOUNITARIO, 
            //(TITMMOV.QUANTIDADE * TITMMOV.PRECOUNITARIO) - ((((TITMMOV.QUANTIDADE * TITMMOV.PRECOUNITARIO) * TMOV.PERCENTUALDESC))/100) AS VALORTOTAL,

            //CAST(CASE WHEN TPRDCOMPL.CODCOLIGADA = 1 THEN   
            //SUBSTRING(ISNULL(TPRDCOMPL.DESCPADRAO,'') ,1,1000)+ '   '+SUBSTRING(ISNULL(TPRDCOMPL.DESCPADRAO,'') ,1001,2000)+ '   '+
            //ISNULL(CAST(TITMMOV.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+ISNULL(TPRD.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRD.NUMEROCCF,'')) FROM ZTPRODUTO WHERE IDPRD = TITMMOV.IDPRD AND CODCOLPRD = TITMMOV.CODCOLIGADA) + ' - ' + (SELECT 
            //		CASE 
            //			WHEN (SELECT FATORSUBST FROM TPRDCODFISCAL WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND IDPRD = TITMMOV.IDPRD AND CODETD = (SELECT CODETD FROM FCFO WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND CODCFO = TMOV.CODCFO)) > 0 
            //			THEN ' COM ICMS-ST' 
            //			ELSE ''
            //			END ST)  

            //ELSE 
            //CAST(ISNULL(TPRDCOMPL.DESCAUXILIAR,'') AS VARCHAR(5000))+ '   '+
            //ISNULL(CAST(TITMMOV.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+ISNULL(TPRD.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRD.NUMEROCCF,'')) FROM ZTPRODUTO WHERE IDPRD = TITMMOV.IDPRD AND CODCOLPRD = TITMMOV.CODCOLIGADA) + ' - ' + (SELECT 
            //		CASE 
            //			WHEN (SELECT FATORSUBST FROM TPRDCODFISCAL WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND IDPRD = TITMMOV.IDPRD AND CODETD = (SELECT CODETD FROM FCFO WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND CODCFO = TMOV.CODCFO)) > 0 
            //			THEN ' COM ICMS-ST' 
            //			ELSE ''
            //			END ST)  

            //END AS TEXT) DESCRICAO

            //FROM ZTPRDCOMPL TPRDCOMPL, ZORCAMENTOITEM TITMMOV, ZTPRODUTO TPRD, ZORCAMENTO TMOV
            //WHERE TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA
            //AND TMOV.IDMOV = TITMMOV.IDMOV
            //AND TPRD.CODCOLPRD = TITMMOV.CODCOLIGADA 
            //AND TPRD.IDPRD = TITMMOV.IDPRD
            //AND TPRD.CODCOLPRD = TPRDCOMPL.CODCOLIGADA 
            //AND TPRD.IDPRD = TPRDCOMPL.IDPRD
            //AND TITMMOV.PRODUTOCOMPOSTO = 1
            //AND NOT(TITMMOV.PROJETO IS NULL)
            //AND TITMMOV.CODCOLIGADA = ?
            //AND TITMMOV.IDMOV = ?";
            string sSql = @"SELECT
TITMMOV.PROJETO SEQUENCIAL, TITMMOV.QUANTIDADE, TITMMOV.CODUND, TITMMOV.IDPRDCOMPOSTO, 
(TITMMOV.PRECOUNITARIO - (TITMMOV.PRECOUNITARIO * TMOV.PERCENTUALDESC)/100) PRECOUNITARIO, 
(TITMMOV.QUANTIDADE * TITMMOV.PRECOUNITARIO) - ((((TITMMOV.QUANTIDADE * TITMMOV.PRECOUNITARIO) * TMOV.PERCENTUALDESC))/100) AS VALORTOTAL,

CAST(CASE WHEN TPRDCOMPL.CODCOLIGADA = 1 THEN   
SUBSTRING(ISNULL(TPRDCOMPL.DESCPADRAO,'') ,1,1000)+ '   '+SUBSTRING(ISNULL(TPRDCOMPL.DESCPADRAO,'') ,1001,2000)+ '   '+
ISNULL(CAST(TITMMOV.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+ISNULL(TPRD.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRD.NUMEROCCF,'')) FROM ZTPRODUTO WHERE IDPRD = TITMMOV.IDPRD AND CODCOLPRD = TITMMOV.CODCOLIGADA)

ELSE 
CAST(ISNULL(TPRDCOMPL.DESCAUXILIAR,'') AS VARCHAR(5000))+ '   '+
ISNULL(CAST(TITMMOV.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+ISNULL(TPRD.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRD.NUMEROCCF,'')) FROM ZTPRODUTO WHERE IDPRD = TITMMOV.IDPRD AND CODCOLPRD = TITMMOV.CODCOLIGADA) 
END AS TEXT) DESCRICAO

FROM ZTPRDCOMPL TPRDCOMPL, ZORCAMENTOITEM TITMMOV, ZTPRODUTO TPRD, ZORCAMENTO TMOV
WHERE TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA
AND TMOV.IDMOV = TITMMOV.IDMOV
AND TPRD.CODCOLPRD = TITMMOV.CODCOLIGADA 
AND TPRD.IDPRD = TITMMOV.IDPRD
AND TPRD.CODCOLPRD = TPRDCOMPL.CODCOLIGADA 
AND TPRD.IDPRD = TPRDCOMPL.IDPRD
AND TITMMOV.PRODUTOCOMPOSTO = 1
AND NOT(TITMMOV.PROJETO IS NULL)
AND TITMMOV.CODCOLIGADA = ?
AND TITMMOV.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport.DataSource = dt;

            txtNUMEROSEQUENCIAL.DataBindings.Add("Text", null, "SEQUENCIAL");
            txtDESCRICAO.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE.DataBindings.Add("Text", null, "QUANTIDADE", "{0:n}");
            txtCODUND.DataBindings.Add("Text", null, "CODUND");
            txtIDPRDCOMPOSTO.DataBindings.Add("Text", null, "IDPRDCOMPOSTO");
            txtPRECOUNITARIO.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtVALORTOTAL.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
        }

        private void Detalhe2()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT TM.IDMOV, TT.NSEQITMMOV NUMEROSEQUENCIAL, CAST(ISNULL(TCPL.DESCPADRAO,'') AS VARCHAR (8000))+ '   '+  ISNULL(CAST(TT.HISTORICOLONGO AS VARCHAR (8000)),'') AS DESCRICAO,
TT.CODUND CODUNDVENDA, TT.QUANTIDADE, TT.PRECOUNITARIO, TT.NSEQITMMOV, TT.IDPRDCOMPOSTO, 
(TT.PRECOUNITARIO - (TT.PRECOUNITARIO * TM.PERCENTUALDESC)/100) PRECOUNITARIO, 
(TT.QUANTIDADE * TT.PRECOUNITARIO) - ((((TT.QUANTIDADE * TT.PRECOUNITARIO) * TM.PERCENTUALDESC))/100) AS VALORTOTAL,

ISNULL((
SELECT
--  SUM(
  CASE WHEN TT.CODUND = 'M' THEN ISNULL(TT.QUANTIDADE,0) 
  ELSE ISNULL((ISNULL(TCPL.MEDIDAFIXA,0) * ISNULL(TT.QUANTIDADE,0)),0) 
  END
--  ) DIMENSAO
FROM ZTPRODUTO T (NOLOCK), ZTPRDCOMPL TCPL (NOLOCK)
WHERE T.CODCOLPRD = TCPL.CODCOLIGADA
AND T.IDPRD = TCPL.IDPRD
AND TT.CODCOLIGADA = T.CODCOLPRD
AND TT.IDPRD = T.IDPRD
AND TCPL.CONTROLEDIM = 'SIM'

),0) DIMENSAO

FROM ZORCAMENTOITEM TT(NOLOCK), ZTPRODUTO T (NOLOCK), ZORCAMENTO TM (NOLOCK), ZTPRDCOMPL TCPL (NOLOCK)
WHERE T.CODCOLPRD = TT.CODCOLIGADA
AND T.IDPRD = TT.IDPRD
AND TM.CODCOLIGADA = TT.CODCOLIGADA
AND TM.IDMOV = TT.IDMOV
AND TCPL.CODCOLIGADA = T.CODCOLPRD
AND TCPL.IDPRD = T.IDPRD
AND TT.PRODUTOCOMPOSTO = 0
AND TT.MATERIALINTERLIGACAO = '1'
AND TM.CODCOLIGADA = ?
AND TM.IDMOV = ?
AND TT.IDPRDCOMPOSTO = ?
ORDER BY TT.IDMOV, TT.IDPRDCOMPOSTO, TT.NSEQITMMOV";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov, txtIDPRDCOMPOSTO.Text });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport1.DataSource = dt;

            //txtNUMEROSEQUENCIAL1.DataBindings.Add("Text", null, "NUMEROSEQUENCIAL");
            txtDESCRICAO1.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE1.DataBindings.Add("Text", null, "QUANTIDADE", "{0:n}");
            txtCODUND1.DataBindings.Add("Text", null, "CODUNDVENDA");
            txtDIMENSAO.DataBindings.Add("Text", null, "DIMENSAO", "{0:n}");
            txtPRECOUNITARIO1.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtVALORTOTAL1.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
        }

        private void Detalhe3()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT TT.PROJETO SEQUENCIAL, TT.QUANTIDADE, TT.CODUND CODUNDVENDA,
CAST(ISNULL(TCPL.DESCPADRAO,'') AS VARCHAR(1000))+ '   '+  ISNULL(CAST(TT.HISTORICOLONGO AS VARCHAR (500)),'')
+' '+ (SELECT('CODIGO:  '+ISNULL(TPRD.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRD.NUMEROCCF,'')) 
FROM ZTPRODUTO TPRD, ZORCAMENTOITEM TITMMOV 
WHERE TPRD.CODCOLPRD = TITMMOV.CODCOLIGADA 
AND TPRD.IDPRD = TITMMOV.IDPRD 
AND TITMMOV.CODCOLIGADA = TM.CODCOLIGADA 
AND TITMMOV.IDMOV       = TM.IDMOV
AND TITMMOV.NSEQITMMOV  = TT.NSEQITMMOV) DESCRICAO,
(TT.PRECOUNITARIO - (TT.PRECOUNITARIO * TM.PERCENTUALDESC)/100) + (((TT.QUANTIDADE *  TT.PRECOUNITARIO) * TT.ALIQUOTAIPI)/100) PRECOUNITARIO, 
(TT.QUANTIDADE * TT.PRECOUNITARIO) - ((((TT.QUANTIDADE * TT.PRECOUNITARIO) * TM.PERCENTUALDESC))/100) + (((TT.QUANTIDADE *  TT.PRECOUNITARIO) * TT.ALIQUOTAIPI)/100) AS VALORTOTAL,
TM.IDMOV,
TT.NSEQITMMOV,
TT.ALIQUOTAIPI,
(((TT.QUANTIDADE *  TT.PRECOUNITARIO) * TT.ALIQUOTAIPI)/100) VALORIPI

FROM ZORCAMENTOITEM TT(NOLOCK), ZTPRODUTO T (NOLOCK), ZORCAMENTO TM (NOLOCK), ZTPRDCOMPL TCPL (NOLOCK)
WHERE T.CODCOLPRD = TT.CODCOLIGADA
AND T.IDPRD = TT.IDPRD
AND TM.CODCOLIGADA = TT.CODCOLIGADA
AND TM.IDMOV = TT.IDMOV
AND TCPL.CODCOLIGADA = T.CODCOLPRD
AND TCPL.IDPRD = T.IDPRD
AND tt.MATERIALINTERLIGACAO = '1'
AND T.TIPO <> 'S'
AND TM.CODCOLIGADA = ?
AND TM.IDMOV = ?
AND TT.IDPRDCOMPOSTO IS NULL
ORDER BY TT.PROJETO, TT.NSEQITMMOV";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov});
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport2.DataSource = dt;

            txtNUMEROSEQUENCIAL2.DataBindings.Add("Text", null, "SEQUENCIAL");
            txtDESCRICAO2.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE2.DataBindings.Add("Text", null, "QUANTIDADE", "{0:n}");
            txtCODUND2.DataBindings.Add("Text", null, "CODUNDVENDA");
            txtIPI2.DataBindings.Add("Text", null, "ALIQUOTAIPI", "{0:n}");
            txtPRECOUNITARIO2.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtVALORTOTAL2.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
            txtVALORIPI1.DataBindings.Add("Text", null, "VALORIPI", "{0:n}");
        }

        private void Detalhe4()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT TT.NSEQITMMOV SEQUENCIAL,
TT.QUANTIDADE,
TT.CODUND CODUNDVENDA,
CAST(ISNULL(TCPL.DESCPADRAO,'') AS VARCHAR(1000))+ '   '+  ISNULL(CAST(TT.HISTORICOLONGO AS VARCHAR (500)),'')
+' '+ (SELECT('CODIGO:  '+ISNULL(TPRD.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRD.NUMEROCCF,'')) 
FROM ZTPRODUTO TPRD, ZORCAMENTOITEM TITMMOV 
WHERE TPRD.CODCOLPRD = TITMMOV.CODCOLIGADA 
AND TPRD.IDPRD = TITMMOV.IDPRD 
AND TITMMOV.CODCOLIGADA = TM.CODCOLIGADA 
AND TITMMOV.IDMOV       = TM.IDMOV
AND TITMMOV.NSEQITMMOV  = TT.NSEQITMMOV) DESCRICAO,
(TT.PRECOUNITARIO - (TT.PRECOUNITARIO * TM.PERCENTUALDESC)/100) + (TT.PRECOUNITARIO - (TT.PRECOUNITARIO * TM.PERCENTUALDESC)/100) PRECOUNITARIO, 
(TT.QUANTIDADE * TT.PRECOUNITARIO) - ((((TT.QUANTIDADE * TT.PRECOUNITARIO) * TM.PERCENTUALDESC))/100) + (TT.PRECOUNITARIO - (TT.PRECOUNITARIO * TM.PERCENTUALDESC)/100) AS VALORTOTAL,
TM.IDMOV,
TT.NSEQITMMOV,
TT.ALIQUOTAIPI,
(((TT.QUANTIDADE *  TT.PRECOUNITARIO) * TT.ALIQUOTAIPI)/100) VALORIPI

FROM ZORCAMENTOITEM TT(NOLOCK), ZTPRODUTO T (NOLOCK), ZORCAMENTO TM (NOLOCK), ZTPRDCOMPL TCPL (NOLOCK)
WHERE T.CODCOLPRD = TT.CODCOLIGADA
AND T.IDPRD = TT.IDPRD
AND TM.CODCOLIGADA = TT.CODCOLIGADA
AND TM.IDMOV = TT.IDMOV
AND TCPL.CODCOLIGADA = T.CODCOLPRD
AND TCPL.IDPRD = T.IDPRD
AND TT.MATERIALINTERLIGACAO = '0'
AND TM.CODCOLIGADA = ?
AND TM.IDMOV = ?     
ORDER BY TT.NSEQITMMOV";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport3.DataSource = dt;

            txtNUMEROSEQUENCIAL3.DataBindings.Add("Text", null, "SEQUENCIAL");
            txtDESCRICAO3.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE3.DataBindings.Add("Text", null, "QUANTIDADE", "{0:n}");
            txtCODUND3.DataBindings.Add("Text", null, "CODUNDVENDA");
            txtIPI3.DataBindings.Add("Text", null, "ALIQUOTAIPI", "{0:n}");
            txtPRECOUNITARIO3.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtVALORTOTAL3.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
            txtVALORIPI2.DataBindings.Add("Text", null, "VALORIPI", "{0:n}");
        }

        private void Detalhe5()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT TT.NSEQITMMOV SEQUENCIAL,
TT.QUANTIDADE,
TT.CODUND CODUNDVENDA,
CAST(ISNULL(TCPL.DESCPADRAO,'') AS VARCHAR(1000))+ '   '+
ISNULL(CAST(TT.HISTORICOLONGO AS VARCHAR (500)),'') DESCRICAO,
(TT.PRECOUNITARIO - (TT.PRECOUNITARIO * TM.PERCENTUALDESC)/100) PRECOUNITARIO, 
(TT.QUANTIDADE * TT.PRECOUNITARIO) - ((((TT.QUANTIDADE * TT.PRECOUNITARIO) * TM.PERCENTUALDESC))/100) AS VALORTOTAL,
TM.IDMOV,
TT.NSEQITMMOV
FROM ZORCAMENTOITEM TT(NOLOCK), ZTPRODUTO T (NOLOCK), ZORCAMENTO TM (NOLOCK), ZTPRDCOMPL TCPL (NOLOCK)
WHERE T.CODCOLPRD = TT.CODCOLIGADA
AND T.IDPRD = TT.IDPRD
AND TM.CODCOLIGADA = TT.CODCOLIGADA
AND TM.IDMOV = TT.IDMOV
AND TCPL.CODCOLIGADA = T.CODCOLPRD
AND TCPL.IDPRD = T.IDPRD
AND T.TIPO = 'S'
AND TM.CODCOLIGADA = ?
AND TM.IDMOV = ?
ORDER BY TT.NSEQITMMOV";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport4.DataSource = dt;

            txtNUMEROSEQUENCIAL4.DataBindings.Add("Text", null, "SEQUENCIAL");
            txtDESCRICAO4.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE4.DataBindings.Add("Text", null, "QUANTIDADE", "{0:n}");
            txtCODUND4.DataBindings.Add("Text", null, "CODUNDVENDA");
            txtPRECOUNITARIO4.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtVALORTOTAL4.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
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
            this.GroupHeader3.GroupFields.Add(new GroupField("IDPRDCOMPOSTO"));

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

        private void DetailReport3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe4();
        }

        private void DetailReport4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe5();
        }

        private void Detail2_AfterPrint(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDIMENSAO.Text))
                Dimensao = Dimensao + Convert.ToDecimal(txtDIMENSAO.Text);

            if (!string.IsNullOrEmpty(txtPRECOUNITARIO1.Text))
                ValorUnitario = ValorUnitario + Convert.ToDecimal(txtPRECOUNITARIO1.Text);

            if (!string.IsNullOrEmpty(txtVALORTOTAL1.Text))
                ValorTotal = ValorTotal + Convert.ToDecimal(txtVALORTOTAL1.Text);
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDecimal(txtQUANTIDADE.Text) == 0)
                txtTOTALIZADORPRECOUNITARIO1.Text = string.Format("{0:n}", (ValorTotal + Convert.ToDecimal(txtPRECOUNITARIO.Text)));
            else
                txtTOTALIZADORPRECOUNITARIO1.Text = string.Format("{0:n}", ((ValorTotal / Convert.ToDecimal(txtQUANTIDADE.Text)) + Convert.ToDecimal(txtPRECOUNITARIO.Text)));

            if (Convert.ToDecimal(txtQUANTIDADE.Text) == 0)
                txtTOTALIZADORVALORTORAL1.Text = string.Format("{0:n}", (ValorTotal + Convert.ToDecimal(txtPRECOUNITARIO.Text)));
            else
                txtTOTALIZADORVALORTORAL1.Text = string.Format("{0:n}", (ValorTotal + (Convert.ToDecimal(txtPRECOUNITARIO.Text) * Convert.ToDecimal(txtQUANTIDADE.Text))));

            if (Dimensao > 0)
            {
                if (Convert.ToDecimal(txtQUANTIDADE.Text) == 0)
                    txtTOTALIZADORDIMENSAO.Text = string.Concat("Dimensão Total: ", string.Format("{0:n}", Dimensao), " m");
                else
                    txtTOTALIZADORDIMENSAO.Text = string.Concat("Dimensão Total: ", string.Format("{0:n}", Dimensao / Convert.ToDecimal(txtQUANTIDADE.Text)), " m");
            }
            else
                txtTOTALIZADORDIMENSAO.Text = string.Empty;

            if (Convert.ToDecimal(txtQUANTIDADE.Text) == 0)
                SubTotal = SubTotal + (ValorTotal + Convert.ToDecimal(txtPRECOUNITARIO.Text));
            else
                SubTotal = SubTotal + (ValorTotal + (Convert.ToDecimal(txtPRECOUNITARIO.Text) * Convert.ToDecimal(txtQUANTIDADE.Text)));
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            Dimensao = 0;
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

            txtTOTALGERAL.Text = string.Format("{0:n}", ((SubTotal + TotalIPI + Convert.ToDecimal(txtVALORDESP.Text) + Convert.ToDecimal(txtVALORFRETE.Text)) /*- Convert.ToDecimal(txtDESCONTOESPECIAL.Text)*/ - Convert.ToDecimal(txtDEVOLUCAO.Text)));
        }

        private void Detail4_AfterPrint(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVALORTOTAL3.Text))
                SubTotal = SubTotal + Convert.ToDecimal(txtVALORTOTAL3.Text);

            if (!string.IsNullOrEmpty(txtVALORIPI2.Text))
                TotalIPI = TotalIPI + Convert.ToDecimal(txtVALORIPI2.Text);
        }

        private void Detail3_AfterPrint(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVALORTOTAL2.Text))
                SubTotal = SubTotal + Convert.ToDecimal(txtVALORTOTAL2.Text);

            if (!string.IsNullOrEmpty(txtVALORIPI1.Text))
                TotalIPI = TotalIPI + Convert.ToDecimal(txtVALORIPI1.Text);
        }

        private void Detail5_AfterPrint(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVALORTOTAL4.Text))
                SubTotal = SubTotal + Convert.ToDecimal(txtVALORTOTAL4.Text);
        }
    }
}
