using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelOrcamentoBanco : DevExpress.XtraReports.UI.XtraReport
    {
        private decimal ValorUnitario;
        private decimal ValorTotal;
        private decimal SubTotal;
        private decimal TotalIPI;
        private decimal Dimensao;
        private decimal Desconto;
        private string OBSERVACAO;
        private string MSGFORNEC;

        public System.Data.DataRow SelectRow { get; set; }

        public RelOrcamentoBanco()
        {
            InitializeComponent();
            
            ValorUnitario = 0;
            ValorTotal = 0;
            SubTotal = 0;
            TotalIPI = 0;
            Dimensao = 0;
            Desconto = 0;


        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE ID = (SELECT IDIMAGEM FROM GCOLIGADA WHERE CODCOLIGADA = ?)";
            sSql = AppLib.Context.poolConnection.Get().ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });

            byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get().ExecGetField(null, sSql, new Object[] { });
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            xrPictureBox1.Image = Image.FromStream(ms);        
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT 
FCFO.CODCFO, FCFO.NOME, FCFO.CONTATO, ZORCAMENTOBANCO.DATAEMISSAO, TMOVCOMPL.MSGFORNEC, FCFO.SUFRAMA, 
CASE TMOVCOMPL.TIPOVENDA WHEN 'C' THEN 'VENDA DIRETA'
WHEN 'R' THEN 'REVENDA' ELSE NULL END TIPOVENDA,
ISNULL(TMOV.VALORFRETE,0) VALORFRETE, 
ISNULL(TMOV.VALORDESP,0) VALORDESP, 
ISNULL(TMOV.VALOREXTRA1,0) VALOREXTRA1, 
ISNULL(TMOV.VALORDESC,0) VALORDESC,
ISNULL(TMOV.VALORLIQUIDO,0) VALORLIQUIDO,
ISNULL(TMOV.VALORBRUTO,0) VALORBRUTO,
ISNULL(TMOV.VALOROUTROS,0) VALOROUTROS,

RUAPRINCIPAL.DESCRICAO DESTIPORUA, FCFO.RUA, FCFO.NUMERO, BAIRROPRINCIPAL.DESCRICAO DESTIPOBAIRRO, FCFO.BAIRRO, TMOV.NUMEROMOV, TMOVHISTORICO.HISTORICOLONGO, TMOV.NORDEM,
FCFO.CIDADE, FCFO.CODETD, FCFO.CEP,

RUAENTREGA.DESCRICAO DESTIPORUAENT, FCFO.RUAENTREGA, FCFO.NUMEROENTREGA, BAIRROENTREGA.DESCRICAO DESTIPOBAIRROENT, FCFO.BAIRROENTREGA, FCFO.CIDADEENTREGA, FCFO.CODETDENTREGA,
FCFO.TELEFONEENTREGA, FCFO.FAXENTREGA, FCFO.CEPENTREGA, FCFO.CGCCFO,

FCFO.EMAIL, TMOVCOMPL.TPCULTURA, FCFO.CIDENTIDADE, FCFO.INSCRESTADUAL, ZORCAMENTOBANCO.CONDPGTO, ZORCAMENTOBANCO.FINANCIADO, TTRA.NOME NOMETRA, TRPR.NOME NOMERPR, TMOV.CAMPOLIVRE1, TMOV.PRAZOENTREGA, FRETECIFOUFOB

FROM TMOV
LEFT JOIN ZORCAMENTOBANCO ON TMOV.CODCOLIGADA = ZORCAMENTOBANCO.CODCOLIGADA AND TMOV.IDMOV = ZORCAMENTOBANCO.IDMOV
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
            sSql = AppLib.Context.poolConnection.Get().ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(sSql, new Object[] { });

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

                Desconto = Convert.ToDecimal(row["VALORDESC"]);
                OBSERVACAO = row["HISTORICOLONGO"].ToString();
                MSGFORNEC = row["MSGFORNEC"].ToString();
                txtNORDEM.Text = row["NORDEM"].ToString();
            }        
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ValorUnitario = 0;
            ValorTotal = 0;

            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string IdMov = SelectRow["IDMOV"].ToString();

//            string sSql = @"SELECT 
//ZORCAMENTOITEMBANCO.PROJETO,  
//ZORCAMENTOITEMBANCO.QUANTIDADE, 
//ZORCAMENTOITEMBANCO.CODUND, 
//TPRODUTO.CODIGOPRD,
//ZORCAMENTOITEMBANCO.PRECOUNITARIO, 
//(ZORCAMENTOITEMBANCO.PRECOUNITARIO * ZORCAMENTOITEMBANCO.QUANTIDADE) VALORTOTAL, 
//((SELECT CAST(TPRDCOMPL.DESCPADRAO AS VARCHAR (5000)) FROM TPRDCOMPL WHERE TPRDCOMPL.CODCOLIGADA = TPRODUTO.CODCOLPRD AND TPRDCOMPL.IDPRD = TPRODUTO.IDPRD)
//+ ' - ' + ZORCAMENTOITEMBANCO.HISTORICOLONGO) DESCRICAO
//FROM 
//ZORCAMENTOITEMBANCO
//INNER JOIN TPRODUTO ON ZORCAMENTOITEMBANCO.IDPRD = TPRODUTO.IDPRD AND ZORCAMENTOITEMBANCO.CODCOLIGADA = TPRODUTO.CODCOLPRD
//LEFT OUTER JOIN TITMMOVCOMPL ON ZORCAMENTOITEMBANCO.IDMOV = TITMMOVCOMPL.IDMOV AND ZORCAMENTOITEMBANCO.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV AND ZORCAMENTOITEMBANCO.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA
//
//where 
//ZORCAMENTOITEMBANCO.CODCOLIGADA = ?
//AND ZORCAMENTOITEMBANCO.IDMOV = ?";
            string sSql = @"SELECT 
ZORCAMENTOITEMBANCO.PROJETO,  
ZORCAMENTOITEMBANCO.QUANTIDADE, 
ZORCAMENTOITEMBANCO.CODUND, 
TPRODUTO.CODIGOPRD,
ZORCAMENTOITEMBANCO.PRECOUNITARIO, 
(ZORCAMENTOITEMBANCO.PRECOUNITARIO * ZORCAMENTOITEMBANCO.QUANTIDADE) VALORTOTAL, 
((SELECT CAST(TPRDCOMPL.DESCPADRAO AS VARCHAR (5000)) FROM TPRDCOMPL WHERE TPRDCOMPL.CODCOLIGADA = TPRODUTO.CODCOLPRD AND TPRDCOMPL.IDPRD = TPRODUTO.IDPRD)
+ '  ' + ISNULL(ZORCAMENTOITEMBANCO.HISTORICOLONGO,'')  +'  '+  (SELECT('  CODIGO:  '+ ISNULL(TPRODUTO.CODIGOPRD,'') +'     NCM:  '+ ISNULL(TPRODUTO.NUMEROCCF,'')))) DESCRICAO
FROM 
ZORCAMENTOITEMBANCO
INNER JOIN TPRODUTO ON ZORCAMENTOITEMBANCO.IDPRD = TPRODUTO.IDPRD AND ZORCAMENTOITEMBANCO.CODCOLIGADA = TPRODUTO.CODCOLPRD
LEFT OUTER JOIN TITMMOVCOMPL ON ZORCAMENTOITEMBANCO.IDMOV = TITMMOVCOMPL.IDMOV AND ZORCAMENTOITEMBANCO.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV AND ZORCAMENTOITEMBANCO.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA

where 
ZORCAMENTOITEMBANCO.CODCOLIGADA = ?
AND ZORCAMENTOITEMBANCO.IDMOV = ?
order by ZORCAMENTOITEMBANCO.PROJETO ";
            sSql = AppLib.Context.poolConnection.Get().ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, IdMov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(sSql, new Object[] { });
            //
            this.DetailReport.DataSource = dt;
            //txtDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString().Replace("\r\n", " ");
            xrRichText1.DataBindings.Add("Text", null, "DESCRICAO");
            txtNumeroItem.DataBindings.Add("Text", null, "PROJETO");
            //txtDescricao.DataBindings.Add("Text", null, "DESCRICAO");
            txtQtd.DataBindings.Add("Text", null, "QUANTIDADE", "{0:n}");
            txtUnid.DataBindings.Add("Text", null, "CODUND");
            //txtCodPrd.DataBindings.Add("Text", null, "CODIGOPRD");
            txtPrecoUnitario.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtValorTotal.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
        }

        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            if (txtValorTotal.Text == "")
            {
                txtValorTotal.Text = "0,00";
            }
            ValorTotal = ValorTotal + Convert.ToDecimal(txtValorTotal.Text);
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //txtOBSERVACAO.Text = OBSERVACAO;
            txtSUBTOTAL.Text = string.Format("{0:n2}", ValorTotal);
            txtTOTALIPI.Text = "0,00";
            txtDESPESAS.Text = "0,00";
            txtFRETE.Text = "0,00";
            txtSUBTOTALGERAL.Text = txtSUBTOTAL.Text;
            txtDEVOLUCAO.Text = "0,00";
            txtDESCONTOESPECIAL.Text = "0,00";
            txtTOTALGERAL.Text = txtSUBTOTAL.Text;
            txtMSGFORNEC.Text = MSGFORNEC;

        }

        private void DetailReport_BandHeightChanged(object sender, BandEventArgs e)
        {

        }

    }
}
