using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelOrcamentoDispan : DevExpress.XtraReports.UI.XtraReport
    {
        public System.Data.DataRow SelectRow { get; set; }
        public string Conexao { get; set; }

        private string contribuinteICMS = "";

        public RelOrcamentoDispan()
        {
            InitializeComponent();
        }

        private void ReportHeaderCabecalho()
        {
            DetailTotal();
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string Idmov = SelectRow["IDMOV"].ToString();

            string sSql = @"
       SET LANGUAGE 'Brazilian'

    SELECT
	TMOV.NUMEROMOV,
    datename(weekday, TMOV.DATAEMISSAO) + ' ' + datename(day, TMOV.DATAEMISSAO) + ' de ' + datename(month, TMOV.DATAEMISSAO) + ' de ' + datename(year, TMOV.DATAEMISSAO) as 'DATAEMISSAO',
	CASE
		TMOV.CODCFO
	WHEN
		'00002'
	THEN
		(SELECT NOMEFCFOORC FROM TMOVCOMPL WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV)
    WHEN 
        '00632'
    THEN
		(SELECT NOMEFCFOORC FROM TMOVCOMPL WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND IDMOV = TMOV.IDMOV)
	ELSE
		(SELECT NOME + ' - ' + CGCCFO FROM FCFO WHERE CODCOLIGADA = TMOV.CODCOLCFO AND CODCFO = TMOV.CODCFO)
	END as 'CLIENTE',
	TMOV.VALORBRUTO,
    CASE WHEN trim(TMOVCOMPL.CONTATOCOM) IS NULL AND FCFO.CODCFO NOT IN ('00002','00632') then FCFO.CONTATO else TMOVCOMPL.CONTATOCOM end as 'CONTATOCOM',
    CASE WHEN trim(TMOVCOMPL.TELEFONECOM) IS NULL AND FCFO.CODCFO NOT IN ('00002','00632') then FCFO.TELEFONE else TMOVCOMPL.TELEFONECOM end as 'TELEFONE',
    CASE WHEN trim(TMOVCOMPL.EMAILCOM) IS NULL AND FCFO.CODCFO NOT IN ('00002','00632') then FCFO.EMAIL else TMOVCOMPL.EMAILCOM end as 'EMAILCOM',
	TMOVHISTORICO.HISTORICOLONGO,
    CASE TMOVCOMPL.NCONTRIB WHEN 0 THEN 'NãO CONTRIBUINTE' WHEN 1 THEN 'CONTRIBUINTE' WHEN 2 THEN 'ISENTO' ELSE '' END as 'CONTRIB_ICMS',
    case APLICPROD 
           when '1' then 'Uso/Consumo'
		   when '2' then 'Uso/Consumo Sem ST'
           when '3' then 'Revender'
		   when '4' then 'Revender Sem ST'
           when '5' then 'Industrializar'
           when '6' then 'Ativo Imobilizado'
           end as 'APLICPROD',
	CASE
		TMOV.FRETECIFOUFOB
	WHEN '1' THEN CASE WHEN TMOV.CODTRA IS NULL THEN 'CIF' ELSE 'CIF - ' + (SELECT TTRA.NOME FROM TTRA WHERE TTRA.CODCOLIGADA = TMOV.CODCOLIGADA AND TTRA.CODTRA = TMOV.CODTRA) END
	WHEN '2' THEN CASE WHEN TMOV.CODTRA IS NULL THEN 'FOB' ELSE 'FOB - ' + (SELECT TTRA.NOME FROM TTRA WHERE TTRA.CODCOLIGADA = TMOV.CODCOLIGADA AND TTRA.CODTRA = TMOV.CODTRA) END
	ELSE CASE WHEN TMOV.CODTRA IS NULL THEN '' ELSE (SELECT TTRA.NOME FROM TTRA WHERE TTRA.CODCOLIGADA = TMOV.CODCOLIGADA AND TTRA.CODTRA = TMOV.CODTRA) END
	END	FRETE,
	(SELECT NOME FROM TCPG WHERE CODCOLIGADA = TMOV.CODCOLIGADA AND CODCPG = TMOV.CODCPG) CODPGTO,
	(SELECT DESCRICAO FROM GCONSIST WHERE CODCOLIGADA = 1 AND APLICACAO = 'T' AND CODTABELA = 'PRAZOFAB' AND CODCLIENTE = TMOVCOMPL.PRAZO) PRAZO, 
	TMOV.CAMPOLIVRE1 VALIDADE_PROPOSTA
FROM 
	TMOV 

inner join TMOVCOMPL
on TMOVCOMPL.IDMOV = TMOV.IDMOV
and TMOVCOMPL.CODCOLIGADA = TMOV.CODCOLIGADA

inner join FCFO
on FCFO.CODCOLIGADA = TMOV.CODCOLCFO
and FCFO.CODCFO = TMOV.CODCFO

inner join TMOVHISTORICO
on TMOVHISTORICO.CODCOLIGADA = TMOV.CODCOLIGADA
and TMOVHISTORICO.IDMOV = TMOV.IDMOV

WHERE 
	TMOV.CODCOLIGADA = ? 
AND TMOV.IDMOV = ?";

            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, Idmov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                txtDataCabecalho.Text = row["DATAEMISSAO"].ToString();
                txtAplicProd.Text = row["APLICPROD"].ToString();
                txtPara.Text = row["CLIENTE"].ToString();
                txtFoneFax.Text = row["TELEFONE"].ToString();
                txtEmail.Text = row["EMAILCOM"].ToString();
                txtAC.Text = row["CONTATOCOM"].ToString();
                txtNumeroProposta.Text = row["NUMEROMOV"].ToString();
                txtValDaProposta.Text = row["VALIDADE_PROPOSTA"].ToString();
                txtFrete.Text = row["FRETE"].ToString();
                txtFaturamento.Text = row["CODPGTO"].ToString() + " (após aprovação de cadastro)";
                txtPrazoFabricacao.Text = row["PRAZO"].ToString() + " (conforme disponibilidade do estoque + tempo de transporte)";
                //txtTotalProdutos.Text = string.Format("{0:n}", row["VALORBRUTO"]);
                txtHISTORICO.Text = row["HISTORICOLONGO"].ToString();
                
                contribuinteICMS = row["CONTRIB_ICMS"].ToString();
            }

            sSql = @"select                                         (SELECT	isnull(SUM(VALOR),0) as 'IPI'
                                                FROM	TTRBMOV
                                                WHERE	TTRBMOV.CODCOLIGADA = 1
                                                AND	TTRBMOV.IDMOV = TM.IDMOV
                                                AND	TTRBMOV.CODTRB = 'IPI') as 'IPI',

                                                (SELECT	TOP 1 ALIQUOTA as 'PIS'
                                                FROM	TTRBMOV
                                                WHERE	TTRBMOV.CODCOLIGADA = 1
                                                AND	TTRBMOV.IDMOV = TM.IDMOV
                                                AND	TTRBMOV.CODTRB = 'PIS') as 'PIS',

                                                (SELECT	TOP 1 ALIQUOTA as 'COFINS'
                                                FROM	TTRBMOV
                                                WHERE	TTRBMOV.CODCOLIGADA = 1
                                                AND	TTRBMOV.IDMOV = TM.IDMOV
                                                AND	TTRBMOV.CODTRB = 'COFINS') as 'COFINS',

                                                (SELECT	TOP 1 ALIQUOTA as 'ICMS'
                                                FROM	TTRBMOV
                                                WHERE	TTRBMOV.CODCOLIGADA = 1
                                                AND	TTRBMOV.IDMOV = TM.IDMOV
                                                AND	TTRBMOV.CODTRB = 'ICMS') as 'ICMS',

                                        
                                            (SELECT 
	                                            CASE 
		                                            WHEN (ISNULL(ST.VALOR, 0) - ISNULL(ICMS.VALOR, 0)) < 0 
		                                            THEN 0 
		                                            ELSE (ISNULL(ST.VALOR, 0) - ISNULL(ICMS.VALOR, 0)) 
	                                            END as 'ICMSST'
                                            FROM TMOV,

                                            (SELECT	SUM (TTRBMOV.VALOR) AS VALOR
                                                 FROM	TTRBMOV
                                                 WHERE	TTRBMOV.CODCOLIGADA = 1
                                                 AND	TTRBMOV.IDMOV = TM.IDMOV
                                                 AND	TTRBMOV.CODTRB = 'ICMSST') ST,

                                            (SELECT	SUM (TTRBMOV.VALOR) AS VALOR
                                                 FROM	TTRBMOV
                                                 WHERE	TTRBMOV.CODCOLIGADA = 1
                                                 AND	TTRBMOV.IDMOV = TM.IDMOV
                                                 AND	TTRBMOV.CODTRB = 'ICMS') ICMS

                                            WHERE CODCOLIGADA = 1
                                            AND   IDMOV = TM.IDMOV) as 'ICMSST' from TMOV TM

where TM.CODCOLIGADA = ?
and TM.IDMOV = ?";

            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, Idmov });
            dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            foreach (System.Data.DataRow row in dt.Rows)
            {
                txtImpAliquotaIcms.Text = string.Format("{0:n}", (row["ICMS"]));

                txtImpostoPIS.Text = string.Format("{0:n}", row["PIS"]);
                //txtImpostoIPI.Text = string.Format("{0:n}", row["IPI"]); 

                Font defaultFont = SystemFonts.DefaultFont;

                txtImpostoIPIPrimeiraComposicao.Text = "Vergalhões: 10%";
                txtImpostoIPIPrimeiraComposicao.Font = new Font(defaultFont.FontFamily, 5, FontStyle.Bold);
                txtImpostoIPISegundaComposicao.Text = "/ Demais itens: ALIQUOTA REDUZIDA 0%.";
                txtImpostoCOFINS.Text = string.Format("{0:n}", row["COFINS"]); 
                txtImpostoIcmsST.Text = "R$ " + string.Format("{0:n}", row["ICMSST"]);
            }

            // Tratamento dos impostos (INCLUSO X EXCLUSO)

            int SemImpostos = Convert.ToInt32(AppLib.Context.poolConnection.Get(this.Conexao).ExecGetField(0, "SELECT SEMIMPOSTOS FROM TMOVCOMPL WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, Idmov }));

            if (SemImpostos == 0)
            {
                txtImpAliquotaIcms.Text = txtImpAliquotaIcms.Text + " % - INCLUSO - Cliente: " + contribuinteICMS;
                txtImpostoPIS.Text = txtImpostoPIS.Text + " % - INCLUSO";
                txtImpostoCOFINS.Text = txtImpostoCOFINS.Text + " % - INCLUSO";
            }
            else
            {
                txtImpAliquotaIcms.Text = txtImpAliquotaIcms.Text + " % - EXCLUSO - Cliente: " + contribuinteICMS;
                txtImpostoPIS.Text = txtImpostoPIS.Text + " % - EXCLUSO";
                txtImpostoCOFINS.Text = txtImpostoCOFINS.Text + " % - EXCLUSO";
            }
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ReportHeaderCabecalho();
        }

        private void DetailItens()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string Idmov = SelectRow["IDMOV"].ToString();

            string sSql = @"SELECT 
	TTM.NUMEROSEQUENCIAL, 
	TP.CODIGOAUXILIAR, 
	CONCAT
	(
	TP.DESCRICAO, 

	CASE 
		WHEN CAST(TTMH.HISTORICOLONGO AS VARCHAR(MAX)) <>  '' 
		THEN ' - ' + UPPER(CAST(TTMH.HISTORICOLONGO AS VARCHAR(MAX))) + ''
		ELSE '' 
	END,
/*
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
*/
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
	) AS 'DESCRICAO',
	
	CASE 
		ZTPC.CHAPA 
		WHEN 'A' THEN '#14' 
		WHEN 'B' THEN '#16'
		WHEN 'C' THEN '#18' 
		WHEN 'D' THEN '#18'
		WHEN 'E' THEN '#20' 
		WHEN 'F' THEN '#22' 
		WHEN 'G' THEN '#24' 
		WHEN 'H' THEN '#26' 
		WHEN 'X' THEN '#12' 
	END AS 'CHAPA', 
	
	/*ZTPC.ACABAMENTO, */
	TTM.QUANTIDADETOTAL AS QUANTIDADE, 
	TTM.PRECOUNITARIO, 
	TTM.CODUND, 
	(TTM.QUANTIDADETOTAL * TTM.PRECOUNITARIO) AS 'VALORTOTAL', 
	
	CASE WHEN 
	(SELECT 
		SUM (TTRBMOV.VALOR) AS VALOR 
	FROM 
		TTRBMOV 
	WHERE 
		TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA AND TTRBMOV.IDMOV = TTM.IDMOV 
	AND TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV AND TTRBMOV.CODTRB = 'ICMSST') 
	> 0 THEN 
	((SELECT 
		SUM (TTRBMOV.VALOR) AS VALOR 
	FROM 
		TTRBMOV 
	WHERE 
		TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA AND TTRBMOV.IDMOV = TTM.IDMOV 
	AND TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV AND TTRBMOV.CODTRB = 'ICMSST') 
	-
	(SELECT 
		SUM (TTRBMOV.VALOR) AS VALOR 
	FROM 
		TTRBMOV 
	WHERE 
		TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA 
	AND TTRBMOV.IDMOV = TTM.IDMOV AND TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV AND TTRBMOV.CODTRB = 'ICMS'))
	ELSE 0 END 
	AS 'R$_ST_DIFAL', 
	
	(
	SELECT 
		ISNULL(SUM(CAST(ALIQUOTA AS NUMERIC (2,0))),0) AS 'IPI' 
	FROM 
		TTRBMOV 
	WHERE 
		TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA 
	AND TTRBMOV.IDMOV = TTM.IDMOV 
	AND TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV 
	AND TTRBMOV.CODTRB = 'IPI'
	) AS '% IPI', 
	
	(
	SELECT 
		ISNULL(SUM(VALOR),0) AS 'IPI' 
	FROM 
		TTRBMOV 
	WHERE 
		TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA 
	AND TTRBMOV.IDMOV = TTM.IDMOV AND TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV AND TTRBMOV.CODTRB = 'IPI'
	) 
	 AS 'R$_IPI'

	FROM 
		TITMMOV TTM 
		
		LEFT JOIN TITMMOVCOMPL TTMC 
		ON TTMC.CODCOLIGADA = TTM.CODCOLIGADA AND TTMC.IDMOV = TTM.IDMOV AND TTMC.NSEQITMMOV = TTM.NSEQITMMOV 
		
		LEFT JOIN TITMMOVHISTORICO TTMH 
		ON TTMH.CODCOLIGADA = TTM.CODCOLIGADA AND TTMH.IDMOV = TTM.IDMOV AND TTMH.NSEQITMMOV = TTM.NSEQITMMOV 
		
		LEFT JOIN TPRODUTO TP 
		ON TP.CODCOLPRD = TTM.CODCOLIGADA AND TP.IDPRD = TTM.IDPRD 
		
		LEFT JOIN ZTPRODUTOCOMPL ZTPC 
		ON ZTPC.CODCOLIGADA = TTM.CODCOLIGADA AND ZTPC.CODFILIAL = TTM.CODFILIAL AND ZTPC.IDPRD = TTM.IDPRD 
		
		LEFT JOIN TITMMOVFISCAL TTMF 
		ON TTMF.CODCOLIGADA = TTM.CODCOLIGADA AND TTMF.IDMOV = TTM.IDMOV AND TTMF.NSEQITMMOV = TTM.NSEQITMMOV 
WHERE
	TTM.CODCOLIGADA = ? 
	AND TTM.IDMOV = ?
ORDER BY TTM.NUMEROSEQUENCIAL ASC";

            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, Idmov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

            this.DetailReport.DataSource = dt;

            txtDetailItem.DataBindings.Add("Text", null, "NUMEROSEQUENCIAL");
            txtDetailRefDispan.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
            txtDetailProdutotxtDetailProduto.DataBindings.Add("Text", null, "DESCRICAO");
            txtDetailIPI.DataBindings.Add("Text", null, "% IPI");
            txtDetailQTD.DataBindings.Add("Text", null, "QUANTIDADE", "{0:n0}");
            txtDetailUN.DataBindings.Add("Text", null, "CODUND");
            txtChapa.DataBindings.Add("Text", null, "CHAPA");
            txtIcmsST.DataBindings.Add("Text", null, "R$_ST_DIFAL", "{0:n2}");
            txtDetailRUnit.DataBindings.Add("Text", null, "PRECOUNITARIO", "{0:n}");
            txtDetailTotal.DataBindings.Add("Text", null, "VALORTOTAL", "{0:n}");
            txtDetailTotalSt.DataBindings.Add("Text", null, "R$_IPI", "{0:n}");          
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailItens();
        }

        private void DetailTotal()
        {
            int CodColigada = Convert.ToInt32(SelectRow["CODCOLIGADA"]);
            string Idmov = SelectRow["IDMOV"].ToString();

            string sSql = @"select VALORLIQUIDOORIG AS VALORLIQUIDO,
                                   VALOROUTROSORIG AS VALOROUTROS,
                                   VALORBRUTOORIG AS VALORBRUTO,
                                   VALORDESC, 
                                   VALORFRETE,
                                   (SELECT	isnull(SUM(VALOR),0) as 'IPI'
                                                                                FROM	TTRBMOV
                                                                                WHERE	TTRBMOV.CODCOLIGADA = TM.CODCOLIGADA
                                                                                AND	TTRBMOV.IDMOV = TM.IDMOV
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
                                                                                 WHERE	TTRBMOV.CODCOLIGADA = TM.CODCOLIGADA
                                                                                 AND	TTRBMOV.IDMOV = TM.IDMOV
                                                                                 AND	TTRBMOV.CODTRB = 'ICMSST') ST,

                                                                            (SELECT	SUM (TTRBMOV.VALOR) AS VALOR
                                                                                 FROM	TTRBMOV
                                                                                 WHERE	TTRBMOV.CODCOLIGADA = TM.CODCOLIGADA
                                                                                 AND	TTRBMOV.IDMOV = TM.IDMOV
                                                                                 AND	TTRBMOV.CODTRB = 'ICMS') ICMS

                                                                            WHERE CODCOLIGADA = TM.CODCOLIGADA
                                                                            AND   IDMOV = TM.IDMOV) AS 'TOTALSTIPI'                                                
                        from TMOV TM
                        where TM.CODCOLIGADA = ?
                        AND TM.IDMOV = ?";
            sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, Idmov });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });
        
            foreach (System.Data.DataRow row in dt.Rows)
            {
                txtTotalProdutos.Text = "R$ " + string.Format("{0:n}", row["VALORBRUTO"]);
                txtValTotProdSemST.Text = "R$ " + string.Format("{0:n}", row["VALORBRUTO"]);
                txtTotalSTIPI.Text = "R$ " + string.Format("{0:n}", row["TOTALSTIPI"]);
                txtTotalGeral.Text = "R$ " + string.Format("{0:n}", row["VALORLIQUIDO"]);

                AjustaValores(Convert.ToDecimal(row["VALORFRETE"]), Convert.ToDecimal(row["VALORDESC"]));
            }

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
            txtVendedorNome.Text = dtInformacoesUsuario.Rows[0]["VENDEDOR_NOME"].ToString();

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

        private void Detail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailTotal();
        }

        private void AjustaValores(decimal _valorFrete, decimal _valorDesconto)
        {
            if (_valorFrete > 0)
            {
                lbValorFreteTexto.Text = "Valor Frete:";
                lbValorFrete.Text = "R$ " + _valorFrete.ToString("F2");
            }
            else
            {
                lbValorFreteTexto.Text = "";
                lbValorFrete.Text = "";
            }

            if (_valorDesconto > 0)
            {
                lbValorDescontoTexto.Text = "Valor Desconto:";
                lbValorDesconto.Text = "R$ " + _valorDesconto.ToString("F2");
            }
            else
            {
                lbValorDescontoTexto.Text = "";
                lbValorDesconto.Text = "";
            }
        }
    }
}
