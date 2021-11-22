using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelRomaneio : DevExpress.XtraReports.UI.XtraReport
    {
        DateTime dataInicial;
        DateTime dataFinal;
        string codigoAuxiliar = string.Empty;
        string caminhao;
        int qtdeCaminhao = 0;
        System.Data.DataTable dt = new System.Data.DataTable();
        public RelRomaneio(DateTime dataini, DateTime datafin, string _codigoAuxiliar)
        {
            InitializeComponent();
            dataInicial = dataini;
            dataFinal = datafin;
            codigoAuxiliar = _codigoAuxiliar;
        }

        private void header()
        {
            txtDataIni.Text = string.Format("Período de {0: dd/MMM/yyyy} a {1: dd/MMM/yyyy}", dataInicial, dataFinal);
        }
        private void detalhe()
        {
            string sql = string.Empty;

            sql = @"SELECT 
ZPROGRAMACAOCARREGAMENTO.CAMINHAO, 
TRPR.NOMEFANTASIA REPRESENTANTE, 
TPRODUTO.CODIGOAUXILIAR, 
(case ISNUMERIC(REPLACE(TPRODUTO.CODIGOAUXILIAR,'.','')) 
		when 1 then TPRODUTO.DESCRICAO
		when 0 then TPRODUTO.CODIGOAUXILIAR
		end)
		as CODIGOAUXILIAR2,
ZPROGRAMACAOCARREGAMENTO.OBSROMANEIO, 
ISNULL(ZPROGRAMACAOCARREGAMENTO.QTDE,0) QTDE,
ISNULL(ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA,0) QTDCARREGADA,
FCFO.NOMEFANTASIA CLIENTE,
TMOV.NUMEROMOV,
TITMMOVCOMPL.SEQUENCIAL,
ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA,
ZPROGRAMACAOCARREGAMENTO.SEMANA,
ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO,
ZPROGRAMACAOCARREGAMENTO.DATAALTERACAO,
TMOVCOMPL.TPCULTURA,
TITMMOVCOMPL.OBSPRDPADRAO,
(
SELECT 
	CASE 
			WHEN (X.CONTADORMONO > 0) 
				THEN 
					CASE 
						WHEN Y.CONTADORTRI > 0 
							THEN 'MONO/TRI' 
							ELSE 'MONO' 
					END 
				ELSE 
					CASE 
						WHEN Y.CONTADORTRI > 0 
							THEN 'TRI' 
							ELSE 'S/ MOTOR' 
					END 
	END AS MOTORIZACAO
			
			FROM (SELECT COUNT(TPRODUTO.DESCRICAO) AS CONTADORMONO
					FROM TITMMOV (NOLOCK) , TPRODUTO (NOLOCK) , TMOV AS TMM (NOLOCK)
					WHERE 
						TMM.CODCOLIGADA = TMOV.CODCOLIGADA
					AND TMM.NUMEROMOV = TMOV.NUMEROMOV
					
					AND TITMMOV.IDMOV = TMM.IDMOV
					AND TITMMOV.CODCOLIGADA = TMM.CODCOLIGADA

					AND TITMMOV.CODCOLIGADA = TPRODUTO.CODCOLPRD
					AND TITMMOV.IDPRD = TPRODUTO.IDPRD
					
					AND (TPRODUTO.CODIGOPRD LIKE '02.001.148%' 
					OR TPRODUTO.CODIGOPRD LIKE '02.001.150%' 
					OR TPRODUTO.CODIGOPRD LIKE '02.001.146%')

					AND TPRODUTO.DESCRICAO LIKE '%MOTOR MONOF%'
				  ) AS X,

			     (SELECT COUNT(TPRODUTO.DESCRICAO) AS CONTADORTRI
					FROM TITMMOV (NOLOCK) , TPRODUTO (NOLOCK) , TMOV AS TMT (NOLOCK)
					WHERE 
					
						TMT.CODCOLIGADA = TMOV.CODCOLIGADA
					AND TMT.NUMEROMOV = TMOV.NUMEROMOV
					
					AND TITMMOV.IDMOV = TMT.IDMOV
					AND TITMMOV.CODCOLIGADA = TMT.CODCOLIGADA

					AND TITMMOV.CODCOLIGADA = TPRODUTO.CODCOLPRD
					AND TITMMOV.IDPRD = TPRODUTO.IDPRD
					
					AND (TPRODUTO.CODIGOPRD LIKE '02.001.148%' 
					OR TPRODUTO.CODIGOPRD LIKE '02.001.150%' 
					OR TPRODUTO.CODIGOPRD LIKE '02.001.146%')
					AND TPRODUTO.DESCRICAO LIKE '%MOTOR TRIF%'
				  ) AS Y

) MOTORIZACAO
FROM ZPROGRAMACAOCARREGAMENTO (NOLOCK)
INNER JOIN TITMMOV (NOLOCK) ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
INNER JOIN TMOV (NOLOCK) ON TITMMOV.IDMOV = TMOV.IDMOV
INNER JOIN TRPR (NOLOCK) ON TMOV.CODRPR = TRPR.CODRPR
INNER JOIN TPRODUTO (NOLOCK) ON TITMMOV.IDPRD = TPRODUTO.IDPRD AND TITMMOV.CODCOLIGADA = TPRODUTO.CODCOLPRD
INNER JOIN FCFO (NOLOCK) ON TMOV.CODCFO = FCFO.CODCFO 
INNER JOIN TITMMOVCOMPL (NOLOCK) ON TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV AND TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA AND TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV
INNER JOIN TMOVCOMPL (NOLOCK) ON TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA AND TMOV.IDMOV = TMOVCOMPL.IDMOV
LEFT JOIN ZCARREGAMENTO (NOLOCK) ON ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO = ZCARREGAMENTO.IDCARREGAMENTO
WHERE 
ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA >= ?
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA <= ?
AND TPRODUTO.CODIGOAUXILIAR LIKE ?
AND (ZCARREGAMENTO.STATUS <> 'CONCLUÍDO' OR ZCARREGAMENTO.STATUS IS NULL)
ORDER BY CAMINHAO, ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO
";
            //string a = AppLib.Context.poolConnection.Get().ParseCommand(sql, new Object[] { dataInicial, dataFinal, codigoAuxiliar });

            AppLib.Data.SqlClient ssql = new AppLib.Data.SqlClient();
            ssql.Timeout = 3600;

            dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dataInicial, dataFinal, codigoAuxiliar });
            if (dt.Rows.Count > 0)
            {
                this.DetailReport.DataSource = dt;
                txtCodPrd.DataBindings.Add("Text", null, "REPRESENTANTE");

                txtCodigoAuxiliar.DataBindings.Add("Text", null, "CODIGOAUXILIAR2");

                txtProduto.DataBindings.Add("Text", null, "OBSROMANEIO");
                txtQtd.DataBindings.Add("Text", null, "QTDE");
                xrLabel9.DataBindings.Add("Text", null, "QTDCARREGADA");
                txtData.DataBindings.Add("Text", null, "DATAAGENDAMENTO", "{0: dddd -- dd/MM/yyyy}");
                xrLabel17.DataBindings.Add("Text", null, "CLIENTE");
                xrLabel11.DataBindings.Add("Text", null, "SEQUENCIAL");
                xrLabel18.DataBindings.Add("Text", null, "NUMEROMOV");
                xrLabel27.DataBindings.Add("Text", null, "TPCULTURA");
                txtCaminhao.DataBindings.Add("Text", null, "CAMINHAO");
                txtData.DataBindings.Add("Text", null, "DATAPROGRAMADA", "{0:dd/MM/yyyy}");
                xrLabel26.DataBindings.Add("Text", null, "DATAALTERACAO", "{0:dd/MM/yyyy}");
                xrLabel30.DataBindings.Add("Text", null, "OBSPRDPADRAO");
                xrLabel33.DataBindings.Add("Text", null, "MOTORIZACAO");
                //xrLabel11.DataBindings.Add("Text", null, "SEMANA");
                xrLabel24.DataBindings.Add("Text", null, "IDCARREGAMENTO");
                //Criando o grupo
                GroupField grupo = new GroupField("DATAPROGRAMADA");
                GroupHeader1.GroupFields.Add(grupo);
                GroupField grupoCliente = new GroupField("CLIENTE");
                GroupHeader2.GroupFields.Add(grupoCliente);
                GroupField grupoPedido = new GroupField("NUMEROMOV");
                GroupHeader3.GroupFields.Add(grupoPedido);
                //




                
            }
        }

        private void TopMargin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            header();
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            detalhe();
            

        }

        private void DetailReport1_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                if (!txtCaminhao.Text.Equals(caminhao))
                {
                    if (!string.IsNullOrEmpty(txtCaminhao.Text))
                    {
                        qtdeCaminhao = qtdeCaminhao + 1;
                        caminhao = txtCaminhao.Text;
                    }
                    else
                    {
                        caminhao = string.Empty;
                    }
                }

            }
            catch (Exception)
            {
                qtdeCaminhao = 0;
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtData.Text))
            {
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery("SELECT CONVERT(INT, CAMINHAO) CAMINHAO FROM ZPROGRAMACAOCARREGAMENTO WHERE ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA = ? AND CODCOLIGADA = ? AND CAMINHAO IS NOT NULL GROUP BY CAMINHAO ORDER BY CAMINHAO", new object[] { Convert.ToDateTime(txtData.Text), AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count.Equals(1))
                    {
                        if (dt.Rows[0]["CAMINHAO"].Equals(0))
                        {
                            xrLabel16.Text = string.Format("QUANTIDADE DE CAMINHÕES PROGRAMADOS POR DIA: {0}", 0);
                        }
                        else
                        {
                            xrLabel16.Text = string.Format("QUANTIDADE DE CAMINHÕES PROGRAMADOS POR DIA: {0}", dt.Rows.Count);
                        }
                    }
                    else
                    {
                        if (dt.Rows[0]["CAMINHAO"].Equals(0))
                        {
                            xrLabel16.Text = string.Format("QUANTIDADE DE CAMINHÕES PROGRAMADOS POR DIA: {0}", dt.Rows.Count - 1);
                        }
                        else
                        {
                            xrLabel16.Text = string.Format("QUANTIDADE DE CAMINHÕES PROGRAMADOS POR DIA: {0}", dt.Rows.Count);
                        }
                    }
                }
            }
        }

        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                if (!txtCaminhao.Text.Equals(caminhao))
                {
                    if (!string.IsNullOrEmpty(txtCaminhao.Text))
                    {
                        qtdeCaminhao = qtdeCaminhao + 1;
                        caminhao = txtCaminhao.Text;
                    }
                    else
                    {
                        caminhao = string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                qtdeCaminhao = 0;
            }
 //           string sql = @"SELECT 
	//CASE 
	//		WHEN (X.CONTADORMONO > 0) 
	//			THEN 
	//				CASE 
	//					WHEN Y.CONTADORTRI > 0 
	//						THEN 'MONO/TRI' 
	//						ELSE 'MONO' 
	//				END 
	//			ELSE 
	//				CASE 
	//					WHEN Y.CONTADORTRI > 0 
	//						THEN 'TRI' 
	//						ELSE 'S/ MOTOR' 
	//				END 
	//END AS MOTORIZACAO
			
	//		FROM (SELECT COUNT(TPRODUTO.DESCRICAO) AS CONTADORMONO
	//				FROM TITMMOV, TPRODUTO, TMOV AS TMM
	//				WHERE 
	//					TMM.CODCOLIGADA = 1
	//				AND TMM.NUMEROMOV = ?
					
	//				AND TITMMOV.IDMOV = TMM.IDMOV
	//				AND TITMMOV.CODCOLIGADA = TMM.CODCOLIGADA

	//				AND TITMMOV.CODCOLIGADA = TPRODUTO.CODCOLPRD
	//				AND TITMMOV.IDPRD = TPRODUTO.IDPRD
					
	//				AND (TPRODUTO.CODIGOPRD LIKE '02.001.148%' 
	//				OR TPRODUTO.CODIGOPRD LIKE '02.001.150%' 
	//				OR TPRODUTO.CODIGOPRD LIKE '02.001.146%')

	//				AND TPRODUTO.DESCRICAO LIKE '%MOTOR MONOF%'
	//			  ) AS X,

	//		     (SELECT COUNT(TPRODUTO.DESCRICAO) AS CONTADORTRI
	//				FROM TITMMOV, TPRODUTO, TMOV AS TMT
	//				WHERE 
					
	//					TMT.CODCOLIGADA = 1
	//				AND TMT.NUMEROMOV = ?
					
	//				AND TITMMOV.IDMOV = TMT.IDMOV
	//				AND TITMMOV.CODCOLIGADA = TMT.CODCOLIGADA

	//				AND TITMMOV.CODCOLIGADA = TPRODUTO.CODCOLPRD
	//				AND TITMMOV.IDPRD = TPRODUTO.IDPRD
					
	//				AND (TPRODUTO.CODIGOPRD LIKE '02.001.148%' 
	//				OR TPRODUTO.CODIGOPRD LIKE '02.001.150%' 
	//				OR TPRODUTO.CODIGOPRD LIKE '02.001.146%')
	//				AND TPRODUTO.DESCRICAO LIKE '%MOTOR TRIF%'
	//			  ) AS Y";
 //           if (!string.IsNullOrEmpty(xrLabel18.Text))
 //           {
 //               xrLabel33.Text = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, sql, new object[] { xrLabel18.Text, xrLabel18.Text }).ToString();
 //           }
        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql;

            //            sql = @"SELECT 
            //CODIGOAUXILIAR, TPRODUTO.DESCRICAO, SUM(ISNULL(ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA,0)) + sum(ISNULL(ZPROGRAMACAOCARREGAMENTO.QTDE,0)) QUANTIDADE 
            //FROM ZPROGRAMACAOCARREGAMENTO
            //INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
            //INNER JOIN TMOV ON TITMMOV.IDMOV = TMOV.IDMOV
            //INNER JOIN TRPR ON TMOV.CODRPR = TRPR.CODRPR
            //INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD AND TITMMOV.CODCOLIGADA = TPRODUTO.CODCOLPRD
            //INNER JOIN FCFO ON TMOV.CODCFO = FCFO.CODCFO 
            //INNER JOIN TITMMOVCOMPL ON TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV AND TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA AND TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV
            //INNER JOIN TMOVCOMPL ON TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA AND TMOV.IDMOV = TMOVCOMPL.IDMOV
            //LEFT JOIN ZCARREGAMENTO ON ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO = ZCARREGAMENTO.IDCARREGAMENTO
            //WHERE 
            //ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA >= ?
            //AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA <= ?
            //AND TPRODUTO.CODIGOAUXILIAR LIKE ?
            //AND (ZCARREGAMENTO.STATUS <> 'CONCLUÍDO' OR ZCARREGAMENTO.STATUS IS NULL)
            //GROUP BY CODIGOAUXILIAR, DESCRICAO
            //ORDER BY CODIGOAUXILIAR

            sql = @"SELECT 
CODIGOAUXILIAR, SUBSTRING(CONVERT(VARCHAR(MAX), TPRDCOMPL.DESCPADRAO),1,50) DESCRICAO, SUM(ISNULL(ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA,0)) + sum(ISNULL(ZPROGRAMACAOCARREGAMENTO.QTDE,0)) QUANTIDADE 
FROM ZPROGRAMACAOCARREGAMENTO
INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
INNER JOIN TMOV ON TITMMOV.IDMOV = TMOV.IDMOV
INNER JOIN TRPR ON TMOV.CODRPR = TRPR.CODRPR
INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD AND TITMMOV.CODCOLIGADA = TPRODUTO.CODCOLPRD
INNER JOIN TPRDCOMPL ON TPRODUTO.CODCOLPRD = TPRDCOMPL.CODCOLIGADA AND TPRODUTO.IDPRD = TPRDCOMPL.IDPRD
INNER JOIN FCFO ON TMOV.CODCFO = FCFO.CODCFO 
INNER JOIN TITMMOVCOMPL ON TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV AND TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA AND TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV
INNER JOIN TMOVCOMPL ON TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA AND TMOV.IDMOV = TMOVCOMPL.IDMOV
LEFT JOIN ZCARREGAMENTO ON ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO = ZCARREGAMENTO.IDCARREGAMENTO
WHERE 
ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA >= ?
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA <= ?
AND TPRODUTO.CODIGOAUXILIAR LIKE ?
AND (ZCARREGAMENTO.STATUS <> 'CONCLUÍDO' OR ZCARREGAMENTO.STATUS IS NULL)
GROUP BY CODIGOAUXILIAR, CONVERT(VARCHAR(MAX), DESCPADRAO)
ORDER BY CODIGOAUXILIAR";
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dataInicial, dataFinal, codigoAuxiliar });
                if (dt.Rows.Count > 0)
                {
                    this.DetailReport1.DataSource = dt;
                    xrLabel20.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
                    xrLabel21.DataBindings.Add("Text", null, "DESCRICAO");
                    xrLabel22.DataBindings.Add("Text", null, "QUANTIDADE");
                }

            }
            catch (Exception)
            {
            }
        }

        private void GroupHeader3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          


        }

        private void GroupHeader3_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void GroupHeader3_BandLevelChanged(object sender, EventArgs e)
        {
            
        }

        private void GroupHeader2_AfterPrint(object sender, EventArgs e)
        {
           
        }
    }
}
