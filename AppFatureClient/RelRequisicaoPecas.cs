using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelRequisicaoPecas : DevExpress.XtraReports.UI.XtraReport
    {
        public System.Data.DataRow SelectRow { get; set; }
        public string Conexao { get; set; }

        private decimal Desconto;
        private decimal PercentualDesc;
        private string OBSERVACAO;
        private string MSGFORNEC;

        public RelRequisicaoPecas()
        {
            InitializeComponent();

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
FCFO.CIDADE, FCFO.CODETD, FCFO.CEP, TMOV.NORDEM,

RUAENTREGA.DESCRICAO DESTIPORUAENT, FCFO.RUAENTREGA, FCFO.NUMEROENTREGA, BAIRROENTREGA.DESCRICAO DESTIPOBAIRROENT, FCFO.BAIRROENTREGA, FCFO.CIDADEENTREGA, FCFO.CODETDENTREGA,
FCFO.TELEFONEENTREGA, FCFO.FAXENTREGA, FCFO.CEPENTREGA, FCFO.CGCCFO,

FCFO.EMAILENTREGA, TMOVCOMPL.CLASSREQ, FCFO.CIDENTIDADE, FCFO.INSCRESTADUAL, TMOVCOMPL.CONDPGTO, TMOVCOMPL.FINANCIADO, TTRA.NOME NOMETRA, TRPR.NOME NOMERPR, TMOV.CAMPOLIVRE1, TMOV.PRAZOENTREGA, FRETECIFOUFOB,

CASE WHEN TMOVCOMPL.FINANCIADO = 'NÃO' THEN 'Descrição das Mercadorias Alienadas com Reserva de Domínio' ELSE '' END MSGFINANCIADO,

(SELECT DESCRICAO
FROM GCONSIST
WHERE CODCOLIGADA = TMOVCOMPL.CODCOLIGADA
  AND APLICACAO = 'T'
  AND CODCLIENTE = TMOVCOMPL.CLASSREQ
  AND CODTABELA = 'CLASSREQ') DESCLASSREQ


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
                xrLabel23.Text = string.Concat(xrLabel23.Text, " ", row["NUMEROMOV"].ToString());

                txtCODCFO.Text = row["CODCFO"].ToString();
                txtNOME.Text = row["NOME"].ToString();
                txtNOME1.Text = row["NOME"].ToString();
                txtCONTATO.Text = row["CONTATO"].ToString();
                txtDATAEMISSAO.Text = string.Format("{0:dd/MM/yyyy}", row["DATAEMISSAO"]);
                txtNUMEROMOV.Text = row["NORDEM"].ToString();

                txtDESTIPORUAENT.Text = row["DESTIPORUA"].ToString();
                txtRUAENTREGA.Text = row["RUA"].ToString();
                txtNUMEROENTREGA.Text = row["NUMERO"].ToString();
                txtDESTIPOBAIRROENT.Text = row["DESTIPOBAIRRO"].ToString();
                txtBAIRROENT.Text = row["BAIRRO"].ToString();
                txtCIDADEENTREGA.Text = row["CIDADE"].ToString();
                txtUFENTREGA.Text = row["CODETD"].ToString();
                txtCEP.Text = row["CEP"].ToString();

                txtNOMETRA.Text = row["NOMETRA"].ToString();
                txtNOMERPR.Text = row["NOMERPR"].ToString();
                txtDESCLASSREQ.Text = row["DESCLASSREQ"].ToString();

                txtVALORFRETE.Text = string.Format("{0:n}", row["VALORFRETE"]);
                txtVALORDESP.Text = string.Format("{0:n}", row["VALORDESP"]);
                txtVALOREXTRA1.Text = string.Format("{0:n}", row["VALOREXTRA1"]);
                txtVALORDESC.Text = string.Format("{0:n}", row["VALORDESC"]);
                Desconto = Convert.ToDecimal(row["VALORDESC"]);
                PercentualDesc = Convert.ToDecimal(row["PERCENTUALDESC"]);
                OBSERVACAO = row["HISTORICOLONGO"].ToString();
                txtUSUARIOCRIACAO.Text = row["USUARIOCRIACAO"].ToString();
                MSGFORNEC = row["MSGFORNEC"].ToString();
            }        
        }

        private void Detalhe1()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT
TITMMOVCOMPL.SEQUENCIAL, TITMMOV.QUANTIDADETOTAL, TITMMOV.CODUND, TITMMOV.IDPRDCOMPOSTO, TITMMOV.PRECOUNITARIO, TITMMOV.QUANTIDADETOTAL * TITMMOV.PRECOUNITARIO AS VALORTOTAL,

CAST(CASE WHEN TPRDCOMPL.CODCOLIGADA = 1 THEN   
SUBSTRING(ISNULL(TPRDCOMPL.DESCPADRAO,'') ,1,1000)+ '   '+SUBSTRING(ISNULL(TPRDCOMPL.DESCPADRAO,'') ,1001,2000)+ '   '+
ISNULL(CAST(TITMMOVHISTORICO.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+ISNULL(TPRD.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRD.NUMEROCCF,'')) FROM TPRD WHERE IDPRD = TITMMOV.IDPRD AND CODCOLIGADA = TITMMOV.CODCOLIGADA) 

ELSE 
CAST(ISNULL(TPRDCOMPL.DESCAUXILIAR,'') AS VARCHAR(5000))+ '   '+
ISNULL(CAST(TITMMOVHISTORICO.HISTORICOLONGO AS VARCHAR (5000)),'') +' '+ (SELECT('CODIGO:  '+ISNULL(TPRD.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRD.NUMEROCCF,'')) FROM TPRD WHERE IDPRD = TITMMOV.IDPRD AND CODCOLIGADA = TITMMOV.CODCOLIGADA) 

END AS TEXT) DESCRICAO

FROM 
TITMMOV (NOLOCK)
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

            txtNUMEROSEQUENCIAL.DataBindings.Add("Text", null, "SEQUENCIAL");
            txtDESCRICAO.DataBindings.Add("Text", null, "DESCRICAO");
            txtQUANTIDADE.DataBindings.Add("Text", null, "QUANTIDADETOTAL", "{0:n}");
            txtCODUND.DataBindings.Add("Text", null, "CODUND");
            txtIDPRDCOMPOSTO.DataBindings.Add("Text", null, "IDPRDCOMPOSTO");
            txtPRECOUNITARIO.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtVALORTOTAL.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
        }

        private void Detalhe2()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"select ZRD.QUANTIDADE - ZRD.QUANTIDADEBAIXADA as 'QUANTIDADE', TP.CODUNDCONTROLE, 


                            SUBSTRING(ISNULL(TPC.DESCPADRAO,'') ,1,1000)+ '   '+SUBSTRING(ISNULL(TPC.DESCPADRAO,'') ,1001,2000)+ '   '+ ISNULL(CAST(ZRD.OBSERVACAO AS VARCHAR (5000)),'') as 'NOMEFANTASIA'

                            from ZREQDEVOLUCAO ZRD

                            inner join TPRD TP
                            on TP.IDPRD = ZRD.IDPRD

							inner join TPRDCOMPL TPC
							on TPC.IDPRD = TP.IDPRD

                            where ZRD.IDREQ = ?
                            and ZRD.QUANTIDADEBAIXADA < ZRD.QUANTIDADE";

            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport1.DataSource = dt;
            //foreach (System.Data.DataRow row in dt.Rows)
            //{
                txtDESCRICAODEVOLUCAO.DataBindings.Add("Text", null, "NOMEFANTASIA");
                txtQUANTIDADEDEVOLUCAO.DataBindings.Add("Text", null, "QUANTIDADE");
                txtCODUNDDEVOLUCAO.DataBindings.Add("Text", null, "CODUNDCONTROLE");
            //}
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

        private void DetailReport6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            txtMSGFORNEC.Text = MSGFORNEC;
        }

        private void DetailReport5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            txtOBSERVACAO.Text = OBSERVACAO;
        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detalhe2();
        }
    }
}
