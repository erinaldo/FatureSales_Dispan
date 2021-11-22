using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class RelPedidoDispan : DevExpress.XtraReports.UI.XtraReport
    {
        public System.Data.DataRow SelectRow { get; set; }
        public string Conexao { get; set; }

        public RelPedidoDispan()
        {
            InitializeComponent();
        }

        public void CarregaLogo()
        {
            /*string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE ID = 5";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });

            byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(null, sSql, new Object[] { });
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            xrPictureBox1.Image = Image.FromStream(ms);*/

        }

        private void ReportHeaderCabecalho()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string Idmov = SelectRow["IDMOV"].ToString();

            string sSql = $@"SELECT
	                                TMOV.CODCOLIGADA,
	                                TMOV.CODFILIAL,
	                                TMOV.IDMOV,
	                                TMOV.NUMEROMOV,
                                    TMOV.CODTMV, 
	                                TMOV.SERIE,
	                                TMOV.CODTMV,
	                                TMOV.NORDEM as 'PEDIDO_CLIENTE',
	                                GFILIAL.RUA as 'FILIAL_RUA',
	                                GFILIAL.NUMERO as 'FILIAL_NUMERO',
	                                UPPER(GFILIAL.CIDADE) as 'FILIAL_CIDADE',
	                                GFILIAL.ESTADO as 'FILIAL_ESTADO',
	                                GFILIAL.TELEFONE as 'FILIAL_TELEFONE',
	                                GFILIAL.FAX as 'FILIAL_FAX',
	                                GFILIAL.CAIXAPOSTAL as 'FILIAL_CXPOSTAL',
	                                GFILIAL.CEP as 'FILIAL_CEP',
	                                GFILIAL.BAIRRO as 'FILIAL_BAIRRO',
	                                GFILIAL.NOME as 'FILIAL_NOME',
	                                GFILIAL.INSCRICAOESTADUAL as 'FILIAL_IE',
	                                GFILIAL.CGC as 'FILIAL_CNPJ',
	                                'WWW.DISPAN.COM.BR' as 'WEBSITE',
	                                FCFO.CODCFO as 'CLIENTE_CODIGO',
	                                CASE WHEN TMOVCOMPL.NOMEFCFOORC IS NULL THEN FCFO.NOME ELSE TMOVCOMPL.NOMEFCFOORC END as 'CLIENTE_NOME',
	                                UPPER(CASE WHEN trim(TMOVCOMPL.CONTATOCOM) IS NULL then FCFO.CONTATO else TMOVCOMPL.CONTATOCOM end) as 'CLIENTE_CONTATO',
	                                FCFO.CGCCFO as 'CLIENTE_CNPJ',
	                                FCFO.COMPLEMENTO as 'CLIENTE_COMPLEMENTO',
	                                FCFO.BAIRRO as 'CLIENTE_BAIRRO',
                                    (FCFO.RUA + ' , ' + FCFO.NUMERO) as 'CLIENTE_RUA',
	                                FCFO.CEP as 'CLIENTE_CEP',
	                                UPPER(FCFO.CIDADE) as 'CLIENTE_CIDADE',
	                                CASE WHEN trim(TMOVCOMPL.TELEFONECOM) IS NULL then FCFO.TELEFONE else TMOVCOMPL.TELEFONECOM end as 'CLIENTE_TELEFONE',
	                                FCFO.FAX as 'CLIENTE_FAX',
	                                FCFO.CODETD as 'CLIENTE_ESTADO',
	                                FCFO.INSCRESTADUAL as 'CLIENTE_IE',
	                                TTRA.NOME as 'TRANSP_NOME',
	                                TTRA.TELEFONE as 'TRANSP_TEL',
	                                CONVERT (VARCHAR, TMOV.DATAEMISSAO, 103) as 'DATA_EMISSAO',
                                    CONVERT (VARCHAR, TMOV.DATAENTREGA, 103) as 'DATA_ENTREGA',
	                                --CONVERT (VARCHAR, TMOV.DATAENTREGA, 103) as 'PRAZO_FABRICACAO',
	                                (SELECT DESCRICAO FROM GCONSIST WHERE CODCOLIGADA = 1 AND APLICACAO = 'T' AND CODTABELA = 'PRAZOFAB' AND CODCLIENTE = TMOVCOMPL.PRAZO) PRAZO_FABRICACAO,
	                                TMOV.VALORFRETE as 'VALOR_FRETE',
	                                TMOV.VALORBRUTOORIG as 'VALOR_BRUTO',
	                                TMOV.VALORLIQUIDOORIG as 'VALOR_LIQUIDO',
	                                TMOVHISTORICO.HISTORICOLONGO,
	                                CASE TMOVCOMPL.NCONTRIB WHEN 0 THEN 'NãO CONTRIBUINTE' WHEN 1 THEN 'CONTRIBUINTE' WHEN 2 THEN 'ISENTO' ELSE '' END as 'CONTRIB_ICMS',
	                                CASE TMOV.FRETECIFOUFOB WHEN 1 THEN 'CIF' WHEN 2 THEN 'FOB' ELSE '' END as 'TIPO_FRETE',
	                                (SELECT NOME FROM TCPG WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND CODCPG = TMOV.CODCPG) + isnull(INFCPLPGTO, '') as 'CONDPGTO',
	                                (SELECT INFCPLPGTO FROM FCFOCOMPL WHERE CODCOLIGADA = FCFO.CODCOLIGADA AND CODCFO = FCFO.CODCFO) as 'COMPL_CONDPGTO',
	                                (SELECT NOME FROM TVEN WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND CODVEN = TMOV.CODVEN1) as 'VENDEDOR_NOME',
	                                (SELECT EMAIL FROM GUSUARIO WHERE CODUSUARIO IN ( 
                                        SELECT CODUSUARIO FROM PPESSOA WHERE CODIGO IN (
                                            SELECT CODPESSOA FROM TVEN   WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND CODVEN = TMOV.CODVEN1))) as 'VENDEDOR_EMAIL',
	                                (SELECT SUM(BASEDECALCULO) FROM TTRBMOV WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV AND CODTRB = 'ICMS') as 'BASE_ICMS',
	                                (SELECT SUM(BASEDECALCULO) FROM TTRBMOV WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV AND CODTRB = 'ICMSST') as 'BASE_ICMS_ST',
	                                (SELECT SUM(VALOR) FROM TTRBMOV WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV AND CODTRB = 'ICMS') as 'VALOR_ICMS',
	                                CASE 
	                                    WHEN (SELECT SUM(VALOR) FROM TTRBMOV WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV AND CODTRB = 'ICMSST') > 0 
                                    THEN 
	                                    ((SELECT SUM(VALOR) FROM TTRBMOV WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV AND CODTRB = 'ICMSST') 
	                                    -
	                                    (SELECT SUM(VALOR) FROM TTRBMOV WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV AND CODTRB = 'ICMS')) 
                                    ELSE 0 
                                    END as 'VALOR_ICMS_ST',
	                                (SELECT SUM(VALOR) FROM TTRBMOV WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV AND CODTRB = 'IPI') as 'VALOR_IPI',
	                                TMOV.VALORDESC as 'VALOR_DESCONTO',
                                    case APLICPROD 
                                    when '1' then 'Uso/Consumo'
		                            when '2' then 'Uso/Consumo Sem ST'
                                    when '3' then 'Revender'
		                            when '4' then 'Revender Sem ST'
                                    when '5' then 'Industrializar'
                                    when '6' then 'Ativo Imobilizado'
                                       end as 'APLICPROD'
                                FROM 
	                                TMOV (NOLOCK) 
                                LEFT OUTER JOIN GCOLIGADA (NOLOCK) ON ((TMOV.CODCOLIGADA=GCOLIGADA.CODCOLIGADA)) 
                                LEFT OUTER JOIN GFILIAL (NOLOCK) ON ((TMOV.CODCOLIGADA=GFILIAL.CODCOLIGADA) AND (TMOV.CODFILIAL=GFILIAL.CODFILIAL)) 
                                LEFT OUTER JOIN FCFO (NOLOCK) ON ((TMOV.CODCOLCFO=FCFO.CODCOLIGADA) AND (TMOV.CODCFO=FCFO.CODCFO)) 
                                LEFT OUTER JOIN FCFOCOMPL (NOLOCK) ON FCFOCOMPL.CODCOLIGADA = FCFO.CODCOLIGADA and FCFOCOMPL.CODCFO = FCFO.CODCFO
                                LEFT OUTER JOIN TTRA  (NOLOCK) ON ((TMOV.CODCOLIGADA=TTRA.CODCOLIGADA) AND (TMOV.CODTRA=TTRA.CODTRA)) 
                                LEFT OUTER JOIN TMOVHISTORICO (NOLOCK) ON ((TMOV.CODCOLIGADA=TMOVHISTORICO.CODCOLIGADA) AND (TMOV.IDMOV=TMOVHISTORICO.IDMOV)) 
                                LEFT OUTER JOIN TMOVCOMPL (NOLOCK) ON ((TMOV.CODCOLIGADA=TMOVCOMPL.CODCOLIGADA) AND (TMOV.IDMOV=TMOVCOMPL.IDMOV)) 
                                WHERE
	                                TMOV.CODCOLIGADA = {AppLib.Context.Empresa}
                                 AND TMOV.IDMOV = {Idmov}";

            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                string codTmv = row["CODTMV"].ToString();

                txtDataCabecalho.Text = (codTmv == "2.1.15") ? "COTAÇÃO N:" : "PEDIDO DE VENDA N:";

                txtDataEmissao.Text = row["DATA_EMISSAO"].ToString();   
                txtAplicProd.Text = row["APLICPROD"].ToString();
                txtDataEntrega.Text = row["DATA_ENTREGA"].ToString();
                txtNumeroProposta.Text = row["NUMEROMOV"].ToString();
                xrNumeroMovimento.Text = row["CODTMV"].ToString();
                txtNome.Text = row["CLIENTE_NOME"].ToString();
                txtEndereco.Text = row["CLIENTE_RUA"].ToString();
                txtCidade.Text = row["CLIENTE_CIDADE"].ToString();
                txtCnpjCpf.Text = row["CLIENTE_CNPJ"].ToString();
                txtContato.Text = row["CLIENTE_CONTATO"].ToString();
                txtTransportador.Text = row["TRANSP_NOME"].ToString();
                txtCondPgto.Text = row["CONDPGTO"].ToString();
                txtContribIcms.Text = row["CONTRIB_ICMS"].ToString();
                txtBairro.Text = row["CLIENTE_BAIRRO"].ToString();
                txtUf.Text = row["CLIENTE_ESTADO"].ToString();
                txtCep.Text = row["CLIENTE_CEP"].ToString();
                txtInscrEst.Text = row["CLIENTE_IE"].ToString();
                txtFone.Text = row["CLIENTE_TELEFONE"].ToString();
                txtFax.Text = row["CLIENTE_FAX"].ToString();
                txtFrete.Text = row["TIPO_FRETE"].ToString();
                txtPrazoFab.Text = row["PRAZO_FABRICACAO"].ToString();
                txtBcIcms.Text = string.Format("{0:n}", row["BASE_ICMS"]);
                txtBcIcmsSt.Text = string.Format("{0:n}", row["BASE_ICMS_ST"]);
                txtVlrIcms.Text = string.Format("{0:n}", row["VALOR_ICMS"]);
                txtVlricmsSt.Text = string.Format("{0:n}", row["VALOR_ICMS_ST"]);
                txtIpi.Text = string.Format("{0:n}", row["VALOR_IPI"]);
                txtFreteDetail2.Text = string.Format("{0:n}", row["VALOR_FRETE"]);
                txtVlrBruto.Text = string.Format("{0:n}", row["VALOR_BRUTO"]);
                txtDesconto.Text = string.Format("{0:n}", row["VALOR_DESCONTO"]);
                txtVlrLiq.Text = string.Format("{0:n}", row["VALOR_LIQUIDO"]);
                xrHistoricoLongo.Text = row["HISTORICOLONGO"].ToString();
            }                                     
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ReportHeaderCabecalho();
            CarregaLogo();
        }

        private void DetailItens()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string Idmov = SelectRow["IDMOV"].ToString();

            string sSql = $@"select TTM.NUMEROSEQUENCIAL,
                                       TP.CODIGOAUXILIAR,
                                       CONCAT
                                    	(
                                    	TP.DESCRICAO, 
                                    
                                    	CASE 
                                    		WHEN CAST(TTMH.HISTORICOLONGO AS VARCHAR(MAX)) <>  '' 
                                    		THEN ' - ' + UPPER(CAST(TTMH.HISTORICOLONGO AS VARCHAR(MAX))) + ''
                                    		ELSE '' 
                                    	END,
                                    	
                                    	CASE 
                                    		WHEN TTMC.DISTANCIAMENTO <> '' 
                                    		THEN ' - DISTANCIAMENTO: '  + SUBSTRING(TTMC.DISTANCIAMENTO,1,3) + ' mm'
                                    		ELSE '' 
                                    	END,  
                                    	CASE 
                                    		TTMC.TIPOPINTURA 
                                    		WHEN 1 THEN ' - PINTURA: EXTERNA - COR: '  + UPPER(TTMC.CORPINTURA)
                                    		WHEN 2 THEN ' - PINTURA: INTERNA/EXTERNA - COR: ' + UPPER(TTMC.CORPINTURA)
                                    		ELSE '' 
                                    	END, 
                                    /*
                                    	CASE 
                                    		WHEN TRIM(TTMF.NUMPEDIDO) <> '' 
                                    		THEN ' PEDIDO NUM.: ' + TTMF.NUMPEDIDO 
                                    	END,
                                    */	
                                    	CASE 
                                    		WHEN	TTMF.NUMITEMPEDIDO > 0 
                                    		THEN ' - NUM. ITEM PED.: ' + CONVERT(VARCHAR(6),TTMF.NUMITEMPEDIDO) 
                                    	END, 
                                    
                                    	' - NCM: ', TP.NUMEROCCF
                                    	) as 'DESCRICAO', 
                                       ZTPC.DISTANCIAMENTO,
                                       ZTPC.CHAPA,
                                       ZTPC.ACABAMENTO,
                                       TTM.QUANTIDADEORIGINAL AS QUANTIDADE,
                                       TTM.PRECOUNITARIO,
                                       TTM.CODUND,
                                       (TTM.QUANTIDADEORIGINAL * TTM.PRECOUNITARIO) as 'VALORTOTAL',
                                       (SELECT	isnull(SUM(VALOR),0) as 'IPI'
                                                                                FROM	TTRBMOV
                                                                                WHERE	TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA
                                                                                AND	TTRBMOV.IDMOV = TTM.IDMOV
										AND     TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV
                                                                                AND	TTRBMOV.CODTRB = 'IPI') as 'IPI',
                                        (SELECT 
	                                                                            CASE 
		                                                                            WHEN (ISNULL(ST.VALOR, 0) - ISNULL(ICMS.VALOR, 0)) < 0 
		                                                                            THEN 0 
		                                                                            ELSE (ISNULL(ST.VALOR, 0) - ISNULL(ICMS.VALOR, 0)) 
	                                                                            END as 'ICMSST'
                                                                            FROM TMOV,

                                                                            (SELECT	SUM (TTRBMOV.VALOR) AS VALOR
                                                                                 FROM	TTRBMOV
                                                                                 WHERE	TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA
                                                                                 AND	TTRBMOV.IDMOV = TTM.IDMOV
										 AND    TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV
                                                                                 AND	TTRBMOV.CODTRB = 'ICMSST') ST,

                                                                            (SELECT	SUM (TTRBMOV.VALOR) AS VALOR
                                                                                 FROM	TTRBMOV
                                                                                 WHERE	TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA
                                                                                 AND	TTRBMOV.IDMOV = TTM.IDMOV
										 AND    TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV
                                                                                 AND	TTRBMOV.CODTRB = 'ICMS') ICMS

                                                                            WHERE CODCOLIGADA = TTM.CODCOLIGADA
                                                                            AND   IDMOV = TTM.IDMOV) AS 'ST_DIFAL',
                                       (SELECT	isnull(SUM(VALOR),0) as 'IPI'
                                                                                FROM	TTRBMOV
                                                                                WHERE	TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA
                                                                                AND	TTRBMOV.IDMOV = TTM.IDMOV
										AND     TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV
                                                                                AND	TTRBMOV.CODTRB = 'IPI') +
                                        (SELECT 
	                                                                            CASE 
		                                                                            WHEN (ISNULL(ST.VALOR, 0) - ISNULL(ICMS.VALOR, 0)) < 0 
		                                                                            THEN 0 
		                                                                            ELSE (ISNULL(ST.VALOR, 0) - ISNULL(ICMS.VALOR, 0)) 
	                                                                            END as 'ICMSST'
                                                                            FROM TMOV,

                                                                            (SELECT	SUM (TTRBMOV.VALOR) AS VALOR
                                                                                 FROM	TTRBMOV
                                                                                 WHERE	TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA
                                                                                 AND	TTRBMOV.IDMOV = TTM.IDMOV
										 AND    TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV
                                                                                 AND	TTRBMOV.CODTRB = 'ICMSST') ST,

                                                                            (SELECT	SUM (TTRBMOV.VALOR) AS VALOR
                                                                                 FROM	TTRBMOV
                                                                                 WHERE	TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA
                                                                                 AND	TTRBMOV.IDMOV = TTM.IDMOV
										 AND    TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV
                                                                                 AND	TTRBMOV.CODTRB = 'ICMS') ICMS

                                                                            WHERE CODCOLIGADA = TTM.CODCOLIGADA
                                                                            AND   IDMOV = TTM.IDMOV) AS 'R$_TOTAL_ST_IPI'


                                from TITMMOV TTM 
                                
                                left join TITMMOVCOMPL TTMC
                                on TTMC.CODCOLIGADA = TTM.CODCOLIGADA
                                and TTMC.IDMOV = TTM.IDMOV
                                and TTMC.NSEQITMMOV = TTM.NSEQITMMOV

                                left join TITMMOVHISTORICO TTMH
                                on TTMH.CODCOLIGADA = TTM.CODCOLIGADA
                                and TTMH.IDMOV = TTM.IDMOV
                                and TTMH.NSEQITMMOV = TTM.NSEQITMMOV

                                left join TPRODUTO TP
                                on TP.CODCOLPRD = TTM.CODCOLIGADA
                                and TP.IDPRD = TTM.IDPRD

                                left join ZTPRODUTOCOMPL ZTPC
                                on ZTPC.CODCOLIGADA = TTM.CODCOLIGADA
                                and ZTPC.CODFILIAL = TTM.CODFILIAL
                                and ZTPC.IDPRD = TTM.IDPRD
                    
                                left join TITMMOVFISCAL TTMF
                                on TTMF.CODCOLIGADA = TTM.CODCOLIGADA
                                and TTMF.IDMOV = TTM.IDMOV
                                and TTMF.NSEQITMMOV = TTM.NSEQITMMOV

                                where TTM.CODCOLIGADA = {AppLib.Context.Empresa}
                                and TTM.IDMOV = {Idmov} ORDER BY TTM.NUMEROSEQUENCIAL ASC";

            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] {});
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport.DataSource = dt;

            txtDetailItem.DataBindings.Add("Text", null, "NUMEROSEQUENCIAL");
            txtCodAuxiliar.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
            txtDetailProduto.DataBindings.Add("Text", null, "DESCRICAO");
            txtDetailQTD.DataBindings.Add("Text", null, "QUANTIDADE", "{0:n0}");
            txtDetailUN.DataBindings.Add("Text", null, "CODUND");
            txtDetailRUnit.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtDetailTotal.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
            txtVlIpi.DataBindings.Add("Text", null, "R$_TOTAL_ST_IPI", "{0:n}");       
        }

        private void InformacoesRodape()
        {
            string Idmov = SelectRow["IDMOV"].ToString();

            // Informações Rodapé
            System.Data.DataTable dtInformacoesUsuario = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(@"SELECT 
                                                                                                                    	(SELECT 
                                                                                                                    		NOME 
                                                                                                                    	FROM 
                                                                                                                    		TVEN 
                                                                                                                    	WHERE 
                                                                                                                    		CODCOLIGADA = TMOV.CODCOLIGADA 
                                                                                                                    	AND CODVEN = TMOV.CODVEN1) AS 'VENDEDOR_NOME',
                                                                                                                    	
                                                                                                                    	(SELECT 
                                                                                                                    		EMAIL 
                                                                                                                    	FROM 
                                                                                                                    		GUSUARIO 
                                                                                                                    	WHERE 
                                                                                                                    		CODUSUARIO IN ( 
                                                                                                                    						SELECT 
                                                                                                                    							CODUSUARIO 
                                                                                                                    						FROM 
                                                                                                                    							PPESSOA 
                                                                                                                    								WHERE CODIGO IN (
                                                                                                                    												SELECT 
                                                                                                                    													CODPESSOA 
                                                                                                                    												FROM 
                                                                                                                    													TVEN   
                                                                                                                    												WHERE 
                                                                                                                    													CODCOLIGADA = TMOV.CODCOLIGADA 
                                                                                                                    												AND CODVEN = TMOV.CODVEN1))) AS 'VENDEDOR_EMAIL',
                                                                                                                    	(SELECT 
                                                                                                                    		CARGO 
                                                                                                                    	FROM 
                                                                                                                    		TVEN 
                                                                                                                    	WHERE 
                                                                                                                    		CODCOLIGADA = TMOV.CODCOLIGADA 
                                                                                                                    	AND CODVEN = TMOV.CODVEN1) AS 'VENDEDOR_CARGO',
                                                                                                                    	
                                                                                                                    	
                                                                                                                    	(SELECT 
                                                                                                                    		TELEFONECOM 
                                                                                                                    	FROM 
                                                                                                                    		TVENCOMPL 
                                                                                                                    	WHERE 
                                                                                                                    		CODCOLIGADA = TMOV.CODCOLIGADA 
                                                                                                                    	AND CODVEN = TMOV.CODVEN1) AS 'VENDEDOR_TELCOMERCIAL',
                                                                                                                    	
                                                                                                                    	(SELECT 
                                                                                                                    		TELEFONECOMDIR 
                                                                                                                    	FROM 
                                                                                                                    		TVENCOMPL 
                                                                                                                    	WHERE 
                                                                                                                    		CODCOLIGADA = TMOV.CODCOLIGADA 
                                                                                                                    	AND CODVEN = TMOV.CODVEN1) AS 'VENDEDOR_TELCOMERCIALDIRETO'
                                                                                                                    
                                                                                                                    FROM TMOV 
                                                                                                                                WHERE CODCOLIGADA = ? AND IDMOV = ?", new Object[] { AppLib.Context.Empresa, Idmov });

            // Nome Vendedor
            txtUsuario.Text = dtInformacoesUsuario.Rows[0]["VENDEDOR_NOME"].ToString();

            // Cargo Vendedor
            txtVendedorCargo.Text = dtInformacoesUsuario.Rows[0]["VENDEDOR_CARGO"].ToString();

            // Telefone Comercial Vendedor
            if (dtInformacoesUsuario.Rows[0]["VENDEDOR_TELCOMERCIAL"] != DBNull.Value)
            {
                string codigoAreaPais = dtInformacoesUsuario.Rows[0]["VENDEDOR_TELCOMERCIAL"].ToString().Substring(0, 2);
                string codigoDDD = dtInformacoesUsuario.Rows[0]["VENDEDOR_TELCOMERCIAL"].ToString().Substring(2, 2);
                string telefone = dtInformacoesUsuario.Rows[0]["VENDEDOR_TELCOMERCIAL"].ToString().Substring(4, 8);
                telefone = string.Format("{0:####-####}", Convert.ToInt32(telefone));

                txtTelefoneComercialVendedor.Text = codigoAreaPais + " " + "(" + codigoDDD + ")" + " " + telefone;
            }

            // Telefone Comercial Direto Vendedor
            if (dtInformacoesUsuario.Rows[0]["VENDEDOR_TELCOMERCIALDIRETO"] != DBNull.Value)
            {
                string telefoneDireto = dtInformacoesUsuario.Rows[0]["VENDEDOR_TELCOMERCIALDIRETO"].ToString().Substring(4, 8);
                telefoneDireto = string.Format("{0:####-####}", Convert.ToInt32(telefoneDireto));

                txtTelefoneDiretoVendedor.Text = "/ Direto: " + telefoneDireto;
            }

            txtVendedorEmail.Text = dtInformacoesUsuario.Rows[0]["VENDEDOR_EMAIL"].ToString();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailItens();

            InformacoesRodape();
        }

        private void Detail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }
    }
}
