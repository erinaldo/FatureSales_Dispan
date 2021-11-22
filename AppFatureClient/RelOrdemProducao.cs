﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelOrdemProducao : DevExpress.XtraReports.UI.XtraReport
    {
        public System.Data.DataRow SelectRow { get; set; }
        public string Conexao { get; set; }
        public DateTime Impressao { get; set; }

        private decimal ValorUnitario;
        private decimal ValorTotal;
        private decimal SubTotal;
        private decimal TotalIPI;
        private decimal Dimensao;
        private decimal Desconto;
        private decimal PercentualDesc;
        private string OBSERVACAO;
        private string MSGFORNEC;

        public RelOrdemProducao()
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
FCFO.CODCFO, FCFO.NOME, FCFO.CONTATO, TMOV.DATAEMISSAO, TMOVCOMPL.MSGFORNEC, TMOV.DATAENTREGA,
ISNULL(TMOV.VALORFRETE,0) VALORFRETE, ISNULL(TMOV.VALORDESP,0) VALORDESP, ISNULL(TMOV.VALOREXTRA1,0) VALOREXTRA1, ISNULL(TMOV.VALORDESC,0) VALORDESC, TMOV.USUARIOCRIACAO, ISNULL(PERCENTUALDESC,0) PERCENTUALDESC,
RUAPRINCIPAL.DESCRICAO DESTIPORUA, FCFO.RUA, FCFO.NUMERO, BAIRROPRINCIPAL.DESCRICAO DESTIPOBAIRRO, FCFO.BAIRRO, TMOV.NUMEROMOV, TMOVHISTORICO.HISTORICOLONGO,
FCFO.CIDADE, FCFO.CODETD, FCFO.CEP,
CASE TMOVCOMPL.TIPOVENDA WHEN 'C' THEN 'VENDA DIRETA'
WHEN 'R' THEN 'REVENDA' ELSE NULL END TIPOVENDA,

RUAENTREGA.DESCRICAO DESTIPORUAENT, FCFO.RUAENTREGA, FCFO.NUMEROENTREGA, BAIRROENTREGA.DESCRICAO DESTIPOBAIRROENT, FCFO.BAIRROENTREGA, FCFO.CIDADEENTREGA, FCFO.CODETDENTREGA,
FCFO.TELEFONEENTREGA, FCFO.FAXENTREGA, FCFO.CEPENTREGA, FCFO.CGCCFO,

FCFO.EMAILENTREGA, TMOVCOMPL.TPCULTURA, FCFO.CIDENTIDADE, FCFO.INSCRESTADUAL, TMOVCOMPL.CONDPGTO, TMOVCOMPL.FINANCIADO, TTRA.NOME NOMETRA, TRPR.NOME NOMERPR, TMOV.CAMPOLIVRE1, TMOV.PRAZOENTREGA, FRETECIFOUFOB,

CASE WHEN TMOVCOMPL.FINANCIADO = 'NÃO' THEN 'Descrição das Mercadorias Alienadas com Reserva de Domínio' ELSE '' END MSGFINANCIADO,
TMOV.DATAEXTRA2

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
                txtNOME1.Text = row["NOME"].ToString();
                txtCONTATO.Text = row["CONTATO"].ToString();
                txtDATAEMISSAO.Text = string.Format("{0:dd/MM/yyyy}", row["DATAEMISSAO"]);
                txtNUMEROMOV.Text = row["NUMEROMOV"].ToString();

                txtDESTIPORUAENT.Text = row["DESTIPORUA"].ToString();
                txtRUAENTREGA.Text = row["RUA"].ToString();
                txtNUMEROENTREGA.Text = row["NUMERO"].ToString();
                txtDESTIPOBAIRROENT.Text = row["DESTIPOBAIRRO"].ToString();
                txtBAIRROENT.Text = row["BAIRRO"].ToString();
                txtCIDADEENTREGA.Text = row["CIDADE"].ToString();
                txtUFENTREGA.Text = row["CODETD"].ToString();
                txtTPCULTURA.Text = row["TPCULTURA"].ToString();
                //lblVenda.Text = row["TIPOVENDA"].ToString();
                txtCIF.Text = "[  ]";
                txtFOB.Text = "[  ]";
                if (Convert.ToInt32(row["FRETECIFOUFOB"]) == 1)
                    txtCIF.Text = "[X]";
                else
                    txtFOB.Text = "[X]";

                txtNOMETRA.Text = row["NOMETRA"].ToString();
                txtNOMERPR.Text = row["NOMERPR"].ToString();
                txtPRAZOENTREGA.Text = string.Format("{0:dd/MM/yyyy}", row["DATAENTREGA"]);

                //txtVALORFRETE.Text = string.Format("{0:n}", row["VALORFRETE"]);
                //txtVALORDESP.Text = string.Format("{0:n}", row["VALORDESP"]);
                //txtVALOREXTRA1.Text = string.Format("{0:n}", row["VALOREXTRA1"]);
                //txtVALORDESC.Text = string.Format("{0:n}", row["VALORDESC"]);
                Desconto = Convert.ToDecimal(row["VALORDESC"]);
                PercentualDesc = Convert.ToDecimal(row["PERCENTUALDESC"]);
                OBSERVACAO = row["HISTORICOLONGO"].ToString();
                txtUSUARIOCRIACAO.Text = row["USUARIOCRIACAO"].ToString();
                MSGFORNEC = row["MSGFORNEC"].ToString();
                txtDATAHORAIMPRESSAO.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss}", (row["DATAEXTRA2"] == DBNull.Value) ? Impressao : row["DATAEXTRA2"]);
                txtFINANCIADO.Text = row["FINANCIADO"].ToString();
            }        
        }

        private void Detalhe1()
        {
            ValorUnitario = 0;
            ValorTotal = 0;

            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT
TITMMOVCOMPL.SEQUENCIAL, TITMMOV.QUANTIDADETOTAL, TITMMOV.CODUND, TITMMOV.IDPRDCOMPOSTO, TITMMOV.PRECOUNITARIO, TITMMOV.QUANTIDADETOTAL * TITMMOV.PRECOUNITARIO AS VALORTOTAL,

CAST(CASE WHEN TPRDCOMPL.CODCOLIGADA = 1 THEN   
SUBSTRING(ISNULL(TPRDCOMPL.DESCPADRAO,'') ,1,1000)+ '   '+SUBSTRING(ISNULL(TPRDCOMPL.DESCPADRAO,'') ,1001,2000)+ '   '+
ISNULL(CAST(TITMMOVHISTORICO.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+ISNULL(TPRD.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRD.NUMEROCCF,'')) FROM TPRD WHERE IDPRD = TITMMOV.IDPRD AND CODCOLIGADA = TITMMOV.CODCOLIGADA) + ' - ' + (SELECT 
		CASE 
			WHEN (SELECT FATORSUBST FROM TPRDCODFISCAL WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND IDPRD = TITMMOV.IDPRD AND CODETD = (SELECT CODETD FROM FCFO WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND CODCFO = TMOV.CODCFO)) > 0 
			THEN ' COM ICMS-ST' 
			ELSE ''
			END ST)  

ELSE 
CAST(ISNULL(TPRDCOMPL.DESCAUXILIAR,'') AS VARCHAR(5000))+ '   '+
ISNULL(CAST(TITMMOVHISTORICO.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+ISNULL(TPRD.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRD.NUMEROCCF,'')) FROM TPRD WHERE IDPRD = TITMMOV.IDPRD AND CODCOLIGADA = TITMMOV.CODCOLIGADA) + ' - ' + (SELECT 
		CASE 
			WHEN (SELECT FATORSUBST FROM TPRDCODFISCAL WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND IDPRD = TITMMOV.IDPRD AND CODETD = (SELECT CODETD FROM FCFO WHERE CODCOLIGADA = TITMMOV.CODCOLIGADA AND CODCFO = TMOV.CODCFO)) > 0 
			THEN ' COM ICMS-ST' 
			ELSE ''
			END ST)  

END AS TEXT) DESCRICAO,
TITMMOVCOMPL.OBSPRDPADRAO

FROM 
TITMMOV (NOLOCK)
LEFT OUTER JOIN TITMMOVHISTORICO ON TITMMOVHISTORICO.CODCOLIGADA = TITMMOV.CODCOLIGADA AND TITMMOVHISTORICO.IDMOV = TITMMOV.IDMOV AND TITMMOVHISTORICO.NSEQITMMOV = TITMMOV.NSEQITMMOV
INNER JOIN TMOV ON TMOV.CODCOLIGADA = TITMMOV.CODCOLIGADA AND TMOV.IDMOV = TITMMOV.IDMOV
INNER JOIN TITMMOVCOMPL ON TITMMOVCOMPL.CODCOLIGADA = TITMMOV.CODCOLIGADA AND TITMMOVCOMPL.IDMOV = TITMMOV.IDMOV AND TITMMOVCOMPL.NSEQITMMOV = TITMMOV.NSEQITMMOV
INNER JOIN TPRD ON TPRD.IDPRD = TITMMOV.IDPRD
INNER JOIN TPRDCOMPL ON TPRDCOMPL.IDPRD = TPRD.IDPRD

WHERE 
    TITMMOVCOMPL.PRODPRICIPAL = 'SIM'
AND NOT(TITMMOVCOMPL.SEQUENCIAL IS NULL)
AND TITMMOV.CODCOLIGADA = ?
AND TITMMOV.IDMOV = ?
";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport.DataSource = dt;

            txtNUMEROSEQUENCIAL.DataBindings.Add("Text", null, "SEQUENCIAL");
            txtDESCRICAO.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE.DataBindings.Add("Text", null, "QUANTIDADETOTAL", "{0:n}");
            txtCODUND.DataBindings.Add("Text", null, "CODUND");
            txtIDPRDCOMPOSTO.DataBindings.Add("Text", null, "IDPRDCOMPOSTO");
            //txtPRECOUNITARIO.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            //txtVALORTOTAL.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
            xrLabel45.DataBindings.Add("Text", null, "OBSPRDPADRAO");


        }

        private void Detalhe2()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT TM.IDMOV, TT.NUMEROSEQUENCIAL, CAST(ISNULL(TCPL.DESCPADRAO,'') AS VARCHAR (8000))+ '   '+  ISNULL(CAST(TH.HISTORICOLONGO AS VARCHAR (8000)),'') AS DESCRICAO,
TT.CODUND CODUNDVENDA, TT.QUANTIDADETOTAL, TT.PRECOUNITARIO, TT.NSEQITMMOV, TT.IDPRDCOMPOSTO, TT.QUANTIDADETOTAL * TT.PRECOUNITARIO AS VALORTOTAL, TTC.OBSPRDPADRAO,

ISNULL((
SELECT
--  SUM(
  CASE WHEN TT.CODUND = 'M' THEN ISNULL(TT.QUANTIDADETOTAL,0) 
  ELSE ISNULL((ISNULL(TCPL.MEDIDAFIXA,0) * ISNULL(TT.QUANTIDADETOTAL,0)),0) 
  END
--  ) DIMENSAO
FROM TPRD T (NOLOCK), TPRDCOMPL TCPL (NOLOCK)
WHERE T.CODCOLIGADA = TCPL.CODCOLIGADA
AND T.IDPRD = TCPL.IDPRD
AND TT.CODCOLIGADA = T.CODCOLIGADA
AND TT.IDPRD = T.IDPRD
AND TCPL.CONTROLEDIM = 'SIM'

),0) DIMENSAO

FROM  
TITMMOV TT (NOLOCK)
INNER JOIN TMOV TM (NOLOCK) ON TM.CODCOLIGADA = TT.CODCOLIGADA AND TM.IDMOV = TT.IDMOV
INNER JOIN TPRD T (NOLOCK) ON T.CODCOLIGADA = TT.CODCOLIGADA AND T.IDPRD = TT.IDPRD
INNER JOIN TITMMOVCOMPL TTC (NOLOCK) ON TTC.CODCOLIGADA = TT.CODCOLIGADA AND TTC.IDMOV = TT.IDMOV AND TTC.NSEQITMMOV = TT.NSEQITMMOV
INNER JOIN TPRDCOMPL TCPL (NOLOCK) ON TCPL.CODCOLIGADA = T.CODCOLIGADA AND TCPL.IDPRD = T.IDPRD
LEFT OUTER JOIN TITMMOVHISTORICO TH (NOLOCK) ON TH.CODCOLIGADA = TT.CODCOLIGADA AND TH.IDMOV = TT.IDMOV AND TH.NSEQITMMOV = TT.NSEQITMMOV

WHERE 
    TTC.PRODPRICIPAL = 'NÃO'
AND TTC.TIPOMAT = '1'
AND TM.CODCOLIGADA = ?
AND TM.IDMOV = ?
AND TT.IDPRDCOMPOSTO = ?

ORDER BY TT.IDMOV,TT.IDPRDCOMPOSTO,TT.NUMEROSEQUENCIAL";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov, txtIDPRDCOMPOSTO.Text });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport1.DataSource = dt;

            //txtNUMEROSEQUENCIAL1.DataBindings.Add("Text", null, "NUMEROSEQUENCIAL");
            txtDESCRICAO1.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE1.DataBindings.Add("Text", null, "QUANTIDADETOTAL", "{0:n}");
            txtCODUND1.DataBindings.Add("Text", null, "CODUNDVENDA");
            txtDIMENSAO.DataBindings.Add("Text", null, "DIMENSAO", "{0:n}");
            txtPRECOUNITARIO1.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtVALORTOTAL1.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
            xrLabel26.DataBindings.Add("Text", null, "OBSPRDPADRAO");
        }

        private void Detalhe3()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT TTC.SEQUENCIAL, TT.QUANTIDADETOTAL, TT.CODUND CODUNDVENDA,
CAST(ISNULL(TCPL.DESCPADRAO,'') AS VARCHAR(1000))+ '   '+  ISNULL(CAST(TH.HISTORICOLONGO AS VARCHAR (500)),'')
+' '+ (SELECT('CODIGO:  '+ISNULL(TPRD.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRD.NUMEROCCF,'')) 
FROM TPRD, TITMMOV 
WHERE TPRD.CODCOLIGADA = TITMMOV.CODCOLIGADA 
AND TPRD.IDPRD = TITMMOV.IDPRD 
AND TITMMOV.CODCOLIGADA = TM.CODCOLIGADA 
AND TITMMOV.IDMOV       = TM.IDMOV
AND TITMMOV.NSEQITMMOV  = TT.NSEQITMMOV) DESCRICAO,
TT.PRECOUNITARIO,
(TT.QUANTIDADETOTAL *  TT.PRECOUNITARIO) VALORTOTAL,
TM.IDMOV,
TT.NSEQITMMOV,
TT.NUMEROSEQUENCIAL,

ISNULL((
SELECT
  ALIQUOTA
FROM
  TTRBMOV (NOLOCK)
WHERE    TT.CODCOLIGADA = TTRBMOV.CODCOLIGADA
     AND TT.IDMOV       = TTRBMOV.IDMOV
     AND TT.NSEQITMMOV  = TTRBMOV.NSEQITMMOV
     AND TTRBMOV.CODTRB      = 'IPI'
),0) ALIQUOTAIPI,

ISNULL((
SELECT
  VALOR
FROM
  TTRBMOV (NOLOCK)
WHERE    TT.CODCOLIGADA = TTRBMOV.CODCOLIGADA
     AND TT.IDMOV       = TTRBMOV.IDMOV
     AND TT.NSEQITMMOV  = TTRBMOV.NSEQITMMOV
     AND TTRBMOV.CODTRB      = 'IPI'
),0) VALORIPI,
TTC.OBSPRDPADRAO

FROM  
TITMMOV TT (NOLOCK)
INNER JOIN TMOV TM (NOLOCK) ON TM.CODCOLIGADA = TT.CODCOLIGADA AND TM.IDMOV = TT.IDMOV
INNER JOIN TPRD T (NOLOCK) ON T.CODCOLIGADA = TT.CODCOLIGADA AND T.IDPRD = TT.IDPRD
INNER JOIN TITMMOVCOMPL TTC (NOLOCK) ON TTC.CODCOLIGADA = TT.CODCOLIGADA AND TTC.IDMOV = TT.IDMOV AND TTC.NSEQITMMOV = TT.NSEQITMMOV
INNER JOIN TPRDCOMPL TCPL (NOLOCK) ON TCPL.CODCOLIGADA = T.CODCOLIGADA AND TCPL.IDPRD = T.IDPRD
LEFT OUTER JOIN TITMMOVHISTORICO TH (NOLOCK) ON TH.CODCOLIGADA = TT.CODCOLIGADA AND TH.IDMOV = TT.IDMOV AND TH.NSEQITMMOV = TT.NSEQITMMOV

WHERE 
    TTC.TIPOMAT = '1'
AND T.TIPO <> 'S'
AND TM.CODCOLIGADA = ?
AND TM.IDMOV = ?
AND TT.IDPRDCOMPOSTO IS NULL
ORDER BY TTC.SEQUENCIAL, TT.NUMEROSEQUENCIAL
";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov});
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport2.DataSource = dt;

            txtNUMEROSEQUENCIAL2.DataBindings.Add("Text", null, "SEQUENCIAL");
            txtDESCRICAO2.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE2.DataBindings.Add("Text", null, "QUANTIDADETOTAL", "{0:n}");
            txtCODUND2.DataBindings.Add("Text", null, "CODUNDVENDA");
            txtIPI2.DataBindings.Add("Text", null, "ALIQUOTAIPI", "{0:n}");
            txtPRECOUNITARIO2.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtVALORTOTAL2.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
            txtVALORIPI1.DataBindings.Add("Text", null, "VALORIPI", "{0:n}");
            xrLabel41.DataBindings.Add("Text", null, "OBSPRDPADRAO");

        }

        private void Detalhe4()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT TT.NUMEROSEQUENCIAL,
TT.QUANTIDADETOTAL,
TT.CODUND CODUNDVENDA,
CAST(ISNULL(TCPL.DESCPADRAO,'') AS VARCHAR(1000))+ '   '+  ISNULL(CAST(TH.HISTORICOLONGO AS VARCHAR (500)),'')
+' '+ (SELECT('CODIGO:  '+ISNULL(T.CODIGOPRD,'') +'     NCM:  '+ ISNULL(T.NUMEROCCF,''))) DESCRICAO,
TT.PRECOUNITARIO,
(TT.QUANTIDADETOTAL *   TT.PRECOUNITARIO) VALORTOTAL,
TM.IDMOV,
TT.NSEQITMMOV,

ISNULL((
SELECT
  ALIQUOTA
FROM
  TTRBMOV (NOLOCK)
WHERE    TT.CODCOLIGADA = TTRBMOV.CODCOLIGADA
     AND TT.IDMOV       = TTRBMOV.IDMOV
     AND TT.NSEQITMMOV  = TTRBMOV.NSEQITMMOV
     AND TTRBMOV.CODTRB      = 'IPI'
),0) ALIQUOTAIPI,

ISNULL((
SELECT
  VALOR
FROM
  TTRBMOV (NOLOCK)
WHERE    TT.CODCOLIGADA = TTRBMOV.CODCOLIGADA
     AND TT.IDMOV       = TTRBMOV.IDMOV
     AND TT.NSEQITMMOV  = TTRBMOV.NSEQITMMOV
     AND TTRBMOV.CODTRB      = 'IPI'
),0) VALORIPI,
TTC.OBSPRDPADRAO


FROM  
TITMMOV TT (NOLOCK)
INNER JOIN TMOV TM (NOLOCK) ON TM.CODCOLIGADA = TT.CODCOLIGADA AND TM.IDMOV = TT.IDMOV
INNER JOIN TPRD T (NOLOCK) ON T.CODCOLIGADA = TT.CODCOLIGADA AND T.IDPRD = TT.IDPRD
INNER JOIN TITMMOVCOMPL TTC (NOLOCK) ON TTC.CODCOLIGADA = TT.CODCOLIGADA AND TTC.IDMOV = TT.IDMOV AND TTC.NSEQITMMOV = TT.NSEQITMMOV
INNER JOIN TPRDCOMPL TCPL (NOLOCK) ON TCPL.CODCOLIGADA = T.CODCOLIGADA AND TCPL.IDPRD = T.IDPRD
LEFT OUTER JOIN TITMMOVHISTORICO TH (NOLOCK) ON TH.CODCOLIGADA = TT.CODCOLIGADA AND TH.IDMOV = TT.IDMOV AND TH.NSEQITMMOV = TT.NSEQITMMOV

WHERE 
    TTC.TIPOMAT = '0'
AND TM.CODCOLIGADA = ?
AND TM.IDMOV = ?     
ORDER BY TT.NUMEROSEQUENCIAL
";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport3.DataSource = dt;

            txtNUMEROSEQUENCIAL3.DataBindings.Add("Text", null, "SEQUENCIAL");
            txtDESCRICAO3.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE3.DataBindings.Add("Text", null, "QUANTIDADETOTAL", "{0:n}");
            txtCODUND3.DataBindings.Add("Text", null, "CODUNDVENDA");
            txtIPI3.DataBindings.Add("Text", null, "ALIQUOTAIPI", "{0:n}");
            txtPRECOUNITARIO3.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtVALORTOTAL3.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
            txtVALORIPI2.DataBindings.Add("Text", null, "VALORIPI", "{0:n}");
            xrLabel43.DataBindings.Add("Text", null, "OBSPRDPADRAO");
        }

        private void Detalhe5()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT TT.NUMEROSEQUENCIAL,
TT.QUANTIDADETOTAL,
TT.CODUND CODUNDVENDA,
CAST(ISNULL(TCPL.DESCPADRAO,'') AS VARCHAR(1000))+ '   '+
ISNULL(CAST(TH.HISTORICOLONGO AS VARCHAR (500)),'') DESCRICAO,
TT.PRECOUNITARIO,
(TT.QUANTIDADETOTAL * TT.PRECOUNITARIO ) VALORTOTAL,
TM.IDMOV,
TT.NSEQITMMOV,
TTC.OBSPRDPADRAO

FROM  
TITMMOV TT (NOLOCK)
INNER JOIN TMOV TM (NOLOCK) ON TM.CODCOLIGADA = TT.CODCOLIGADA AND TM.IDMOV = TT.IDMOV
INNER JOIN TPRD T (NOLOCK) ON T.CODCOLIGADA = TT.CODCOLIGADA AND T.IDPRD = TT.IDPRD
INNER JOIN TITMMOVCOMPL TTC (NOLOCK) ON TTC.CODCOLIGADA = TT.CODCOLIGADA AND TTC.IDMOV = TT.IDMOV AND TTC.NSEQITMMOV = TT.NSEQITMMOV
INNER JOIN TPRDCOMPL TCPL (NOLOCK) ON TCPL.CODCOLIGADA = T.CODCOLIGADA AND TCPL.IDPRD = T.IDPRD
LEFT OUTER JOIN TITMMOVHISTORICO TH (NOLOCK) ON TH.CODCOLIGADA = TT.CODCOLIGADA AND TH.IDMOV = TT.IDMOV AND TH.NSEQITMMOV = TT.NSEQITMMOV

WHERE 
    T.TIPO = 'S'
AND TM.CODCOLIGADA = ?
AND TM.IDMOV = ?

ORDER BY TT.NUMEROSEQUENCIAL";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport4.DataSource = dt;

            txtNUMEROSEQUENCIAL4.DataBindings.Add("Text", null, "SEQUENCIAL");
            txtDESCRICAO4.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE4.DataBindings.Add("Text", null, "QUANTIDADETOTAL", "{0:n}");
            txtCODUND4.DataBindings.Add("Text", null, "CODUNDVENDA");
            txtPRECOUNITARIO4.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtVALORTOTAL4.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
            xrLabel44.DataBindings.Add("Text", null, "OBSPRDPADRAO");

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
            if(!string.IsNullOrEmpty(txtDIMENSAO.Text))
                Dimensao = Dimensao + Convert.ToDecimal(txtDIMENSAO.Text);

            if (!string.IsNullOrEmpty(txtPRECOUNITARIO1.Text))
                ValorUnitario = ValorUnitario + Convert.ToDecimal(txtPRECOUNITARIO1.Text);

            if (!string.IsNullOrEmpty(txtVALORTOTAL1.Text))
                ValorTotal = ValorTotal + Convert.ToDecimal(txtVALORTOTAL1.Text);
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (Convert.ToDecimal(txtQUANTIDADE.Text) == 0)
            //    txtTOTALIZADORPRECOUNITARIO1.Text = string.Format("{0:n}", (ValorTotal + Convert.ToDecimal(txtPRECOUNITARIO.Text)));
            //else
            //    txtTOTALIZADORPRECOUNITARIO1.Text = string.Format("{0:n}", ((ValorTotal / Convert.ToDecimal(txtQUANTIDADE.Text)) + Convert.ToDecimal(txtPRECOUNITARIO.Text)));

            //if (Convert.ToDecimal(txtQUANTIDADE.Text) == 0)
            //    txtTOTALIZADORVALORTORAL1.Text = string.Format("{0:n}", (ValorTotal + Convert.ToDecimal(txtPRECOUNITARIO.Text)));
            //else
            //    txtTOTALIZADORVALORTORAL1.Text = string.Format("{0:n}", (ValorTotal + (Convert.ToDecimal(txtPRECOUNITARIO.Text) * Convert.ToDecimal(txtQUANTIDADE.Text))));

            if (Dimensao > 0)
            {
                if (Convert.ToDecimal(txtQUANTIDADE.Text) == 0)
                    txtTOTALIZADORDIMENSAO.Text = string.Concat("Dimensão Total: ", string.Format("{0:n}", Dimensao), " m");
                else
                    txtTOTALIZADORDIMENSAO.Text = string.Concat("Dimensão Total: ", string.Format("{0:n}", Dimensao / Convert.ToDecimal(txtQUANTIDADE.Text)), " m");
            }
            else
                txtTOTALIZADORDIMENSAO.Text = string.Empty;

            //if (Convert.ToDecimal(txtQUANTIDADE.Text) == 0)
            //    SubTotal = SubTotal + (ValorTotal + Convert.ToDecimal(txtPRECOUNITARIO.Text));
            //else
            //    SubTotal = SubTotal + (ValorTotal + (Convert.ToDecimal(txtPRECOUNITARIO.Text) * Convert.ToDecimal(txtQUANTIDADE.Text)));
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
                txtDESCPERCENTUALDESC.Text = "Percentual Desconto:";
                txtPERCENTUALDESC.Text = string.Format("{0:n}", PercentualDesc);
            }
            else
            {
                txtDESCDESCONTO.Text = "";
                txtDESCONTOESPECIAL.Text = string.Format("{0:n}", 0);
                txtDESCPERCENTUALDESC.Text = "";
                txtPERCENTUALDESC.Text = string.Format("{0:n}", 0);
            }

            txtTOTALGERAL.Text = string.Format("{0:n}", ((SubTotal + TotalIPI + Convert.ToDecimal(txtVALORDESP.Text) + Convert.ToDecimal(txtVALORFRETE.Text)) - Convert.ToDecimal(txtDESCONTOESPECIAL.Text)));
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
