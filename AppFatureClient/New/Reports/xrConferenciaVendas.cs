using System;
using System.Data;

namespace AppFatureClient.New.Reports
{
    public partial class xrConferenciaVendas : DevExpress.XtraReports.UI.XtraReport
    {
        public string DataInicial, DataFinal, CodVendedor;

		private decimal TotalPedidos = 0;
		private decimal TotalCotacoes = 0; 

        public xrConferenciaVendas()
        {
            InitializeComponent();          
        }

        private void xrConferenciaVendas_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CarregaCabecalho();
            CarregaDetalhes();
            CarregaTotalGrupos();
			CarregaTotalGeral();
        }

        #region Métodos

        private void CarregaCabecalho()
        {
            xrEmissao.Text = DateTime.Now.ToString("dd-MM-yyyy");
            xrPeriodo.Text = "De " + Convert.ToDateTime(DataInicial).ToString("dd-MM-yyyy") + " à " + Convert.ToDateTime(DataFinal).ToString("dd-MM-yyyy");
        }

        private void CarregaDetalhes()
        {
            DataTable dtHistoricoVendas = null;
            DataTable dtHistoricoVendas_2 = null;

            if (string.IsNullOrEmpty(CodVendedor))
            {
                // Filtra todos os vendedores de acordo com o período informado.
				// 2.1.10
                dtHistoricoVendas = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
	                                                                                        TMOV.CODVEN1 AS COD_VENDEDOR,

	                                                                                        (SELECT 
		                                                                                        NOME 
	                                                                                        FROM 
		                                                                                        TVEN 
	                                                                                        WHERE 
		                                                                                        CODCOLIGADA = TMOV.CODCOLIGADA 
	                                                                                        AND CODVEN = TMOV.CODVEN1) AS VENDEDOR,

	                                                                                        (SELECT 
		                                                                                        COUNT(T.NUMEROMOV) 
	                                                                                        FROM 
		                                                                                        TMOV T 
	                                                                                        WHERE 
		                                                                                        T.CODCOLIGADA = TMOV.CODCOLIGADA 
	                                                                                        AND T.DATAEMISSAO >= ?
	                                                                                        AND T.DATAEMISSAO <= ?
	                                                                                        AND T.CODVEN1 = TMOV.CODVEN1 
	                                                                                        AND T.CODTMV = TMOV.CODTMV) QTD_PEDIDO,


	                                                                                        (SELECT 
		                                                                                        COUNT(DISTINCT(F.CODCFO)) 
	                                                                                        FROM 
		                                                                                        TMOV T
		                                                                                        INNER JOIN FCFO F 
		                                                                                        ON F.CODCOLIGADA = T.CODCOLCFO AND F.CODCFO = T.CODCFO 
		                                                                                        AND T.DATAEMISSAO >= ?
		                                                                                        AND T.DATAEMISSAO <= ?
		                                                                                        AND T.CODVEN1 = TMOV.CODVEN1 AND T.CODTMV = TMOV.CODTMV) QTD_CLIENTE,

	                                                                                        (SELECT 
		                                                                                        COUNT(DISTINCT(F.CODCFO)) 
	                                                                                        FROM 
		                                                                                        FCFODEF F 
	                                                                                        WHERE 
			                                                                                        F.CODCOLIGADA = TMOV.CODCOLIGADA
		                                                                                        AND F.CODVEN = TMOV.CODVEN1) QTD_CLIENTE_CARTEIRA,

	                                                                                        CAST(SUM(TMOV.VALORLIQUIDOORIG) AS MONEY) VALOR
                                                                                        FROM 
	                                                                                        TMOV (NOLOCK)
                                                                                        WHERE 
	                                                                                        TMOV.CODCOLIGADA = ?
                                                                                        AND TMOV.CODTMV IN ('2.1.10')
                                                                                        AND TMOV.STATUS <> 'C'
                                                                                        AND TMOV.CODVEN1 NOT LIKE '0001'
                                                                                        AND TMOV.DATAEMISSAO >= ?
                                                                                        AND TMOV.DATAEMISSAO <= ?
                                                                                        AND TMOV.CODVEN1 LIKE '%'
                                                                                        GROUP BY	
	                                                                                        TMOV.CODCOLIGADA,
	                                                                                        TMOV.CODVEN1,
	                                                                                        TMOV.CODTMV
                                                                                        ORDER BY
	                                                                                        VENDEDOR", new object[] 
                                                                                         {
                                                                                             DataInicial, 
                                                                                             DataFinal,
                                                                                             DataInicial,
                                                                                             DataFinal,
                                                                                             AppLib.Context.Empresa,
                                                                                             DataInicial,
                                                                                             DataFinal
                                                                                         });

				// 2.1.15
				dtHistoricoVendas_2 = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
	                                                                                        TMOV.CODVEN1 AS COD_VENDEDOR,

	                                                                                        (SELECT 
		                                                                                        NOME 
	                                                                                        FROM 
		                                                                                        TVEN 
	                                                                                        WHERE 
		                                                                                        CODCOLIGADA = TMOV.CODCOLIGADA 
	                                                                                        AND CODVEN = TMOV.CODVEN1) AS VENDEDOR,

	                                                                                        (SELECT 
		                                                                                        COUNT(T.NUMEROMOV) 
	                                                                                        FROM 
		                                                                                        TMOV T 
	                                                                                        WHERE 
		                                                                                        T.CODCOLIGADA = TMOV.CODCOLIGADA 
	                                                                                        AND T.DATAEMISSAO >= ?
	                                                                                        AND T.DATAEMISSAO <= ?
	                                                                                        AND T.CODVEN1 = TMOV.CODVEN1 
	                                                                                        AND T.CODTMV = TMOV.CODTMV) QTD_PEDIDO,


	                                                                                        (SELECT 
		                                                                                        COUNT(DISTINCT(F.CODCFO)) 
	                                                                                        FROM 
		                                                                                        TMOV T
		                                                                                        INNER JOIN FCFO F 
		                                                                                        ON F.CODCOLIGADA = T.CODCOLCFO AND F.CODCFO = T.CODCFO 
		                                                                                        AND T.DATAEMISSAO >= ?
		                                                                                        AND T.DATAEMISSAO <= ?
		                                                                                        AND T.CODVEN1 = TMOV.CODVEN1 AND T.CODTMV = TMOV.CODTMV) QTD_CLIENTE,

	                                                                                        (SELECT 
		                                                                                        COUNT(DISTINCT(F.CODCFO)) 
	                                                                                        FROM 
		                                                                                        FCFODEF F 
	                                                                                        WHERE 
			                                                                                        F.CODCOLIGADA = TMOV.CODCOLIGADA
		                                                                                        AND F.CODVEN = TMOV.CODVEN1) QTD_CLIENTE_CARTEIRA,

	                                                                                        CAST(SUM(TMOV.VALORLIQUIDOORIG) AS MONEY) VALOR
                                                                                        FROM 
	                                                                                        TMOV (NOLOCK)
                                                                                        WHERE 
	                                                                                        TMOV.CODCOLIGADA = ?
                                                                                        AND TMOV.CODTMV IN ('2.1.15')
                                                                                        AND TMOV.STATUS <> 'C'
                                                                                        AND TMOV.CODVEN1 NOT LIKE '0001'
                                                                                        AND TMOV.DATAEMISSAO >= ?
                                                                                        AND TMOV.DATAEMISSAO <= ?
                                                                                        AND TMOV.CODVEN1 LIKE '%'
                                                                                        GROUP BY	
	                                                                                        TMOV.CODCOLIGADA,
	                                                                                        TMOV.CODVEN1,
	                                                                                        TMOV.CODTMV
                                                                                        ORDER BY
	                                                                                        VENDEDOR", new object[]
																						 {
																							 DataInicial,
																							 DataFinal,
																							 DataInicial,
																							 DataFinal,
																							 AppLib.Context.Empresa,
																							 DataInicial,
																							 DataFinal
																						 });
			}
            else
            {
                // Filtra o vendedor selecionado dentro do período informado.
				// 2.1.10
                dtHistoricoVendas = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
	                                                                                        TMOV.CODVEN1 AS COD_VENDEDOR,

	                                                                                        (SELECT 
		                                                                                        NOME 
	                                                                                        FROM 
		                                                                                        TVEN 
	                                                                                        WHERE 
		                                                                                        CODCOLIGADA = ?
	                                                                                        AND CODVEN = ?) AS VENDEDOR,

	                                                                                        (SELECT 
		                                                                                        COUNT(T.NUMEROMOV) 
	                                                                                        FROM 
		                                                                                        TMOV T 
	                                                                                        WHERE 
		                                                                                        T.CODCOLIGADA = ?
	                                                                                        AND T.DATAEMISSAO >= ?
	                                                                                        AND T.DATAEMISSAO <= ?
	                                                                                        AND T.CODVEN1 = ?
	                                                                                        AND T.CODTMV = TMOV.CODTMV) QTD_PEDIDO,


	                                                                                        (SELECT 
		                                                                                        COUNT(DISTINCT(F.CODCFO)) 
	                                                                                        FROM 
		                                                                                        TMOV T
		                                                                                        INNER JOIN FCFO F 
		                                                                                        ON F.CODCOLIGADA = T.CODCOLCFO AND F.CODCFO = T.CODCFO 
		                                                                                        AND T.DATAEMISSAO >= ?
		                                                                                        AND T.DATAEMISSAO <= ?
		                                                                                        AND T.CODVEN1 = TMOV.CODVEN1 AND T.CODTMV = TMOV.CODTMV) QTD_CLIENTE,

	                                                                                        (SELECT 
		                                                                                        COUNT(DISTINCT(F.CODCFO)) 
	                                                                                        FROM 
		                                                                                        FCFODEF F 
	                                                                                        WHERE 
			                                                                                        F.CODCOLIGADA = TMOV.CODCOLIGADA
		                                                                                        AND F.CODVEN = TMOV.CODVEN1) QTD_CLIENTE_CARTEIRA,

	                                                                                        CAST(SUM(TMOV.VALORLIQUIDOORIG) AS MONEY) VALOR
                                                                                        FROM 
	                                                                                        TMOV (NOLOCK)
                                                                                        WHERE 
	                                                                                        TMOV.CODCOLIGADA = ?
                                                                                        AND TMOV.CODTMV IN ('2.1.10')
                                                                                        AND TMOV.STATUS <> 'C'
                                                                                        AND TMOV.CODVEN1 NOT LIKE '0001'
                                                                                        AND TMOV.DATAEMISSAO >= ?
                                                                                        AND TMOV.DATAEMISSAO <= ?
                                                                                        AND TMOV.CODVEN1 = ?
                                                                                        GROUP BY	
	                                                                                        TMOV.CODCOLIGADA,
	                                                                                        TMOV.CODVEN1,
	                                                                                        TMOV.CODTMV
                                                                                        ORDER BY
	                                                                                        VENDEDOR", new object[]
                                                                                         {
                                                                                             AppLib.Context.Empresa,
                                                                                             CodVendedor,
                                                                                             AppLib.Context.Empresa,
                                                                                             DataInicial,
                                                                                             DataFinal,
                                                                                             CodVendedor,
                                                                                             DataInicial,
                                                                                             DataFinal,
                                                                                             AppLib.Context.Empresa,
                                                                                             DataInicial,
                                                                                             DataFinal,
                                                                                             CodVendedor }
                                                                                         );

				// 2.1.15
				dtHistoricoVendas_2 = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
	                                                                                        TMOV.CODVEN1 AS COD_VENDEDOR,

	                                                                                        (SELECT 
		                                                                                        NOME 
	                                                                                        FROM 
		                                                                                        TVEN 
	                                                                                        WHERE 
		                                                                                        CODCOLIGADA = ?
	                                                                                        AND CODVEN = ?) AS VENDEDOR,

	                                                                                        (SELECT 
		                                                                                        COUNT(T.NUMEROMOV) 
	                                                                                        FROM 
		                                                                                        TMOV T 
	                                                                                        WHERE 
		                                                                                        T.CODCOLIGADA = ?
	                                                                                        AND T.DATAEMISSAO >= ?
	                                                                                        AND T.DATAEMISSAO <= ?
	                                                                                        AND T.CODVEN1 = ?
	                                                                                        AND T.CODTMV = TMOV.CODTMV) QTD_PEDIDO,


	                                                                                        (SELECT 
		                                                                                        COUNT(DISTINCT(F.CODCFO)) 
	                                                                                        FROM 
		                                                                                        TMOV T
		                                                                                        INNER JOIN FCFO F 
		                                                                                        ON F.CODCOLIGADA = T.CODCOLCFO AND F.CODCFO = T.CODCFO 
		                                                                                        AND T.DATAEMISSAO >= ?
		                                                                                        AND T.DATAEMISSAO <= ?
		                                                                                        AND T.CODVEN1 = TMOV.CODVEN1 AND T.CODTMV = TMOV.CODTMV) QTD_CLIENTE,

	                                                                                        (SELECT 
		                                                                                        COUNT(DISTINCT(F.CODCFO)) 
	                                                                                        FROM 
		                                                                                        FCFODEF F 
	                                                                                        WHERE 
			                                                                                        F.CODCOLIGADA = TMOV.CODCOLIGADA
		                                                                                        AND F.CODVEN = TMOV.CODVEN1) QTD_CLIENTE_CARTEIRA,

	                                                                                        CAST(SUM(TMOV.VALORLIQUIDOORIG) AS MONEY) VALOR
                                                                                        FROM 
	                                                                                        TMOV (NOLOCK)
                                                                                        WHERE 
	                                                                                        TMOV.CODCOLIGADA = ?
                                                                                        AND TMOV.CODTMV IN ('2.1.15')
                                                                                        AND TMOV.STATUS <> 'C'
                                                                                        AND TMOV.CODVEN1 NOT LIKE '0001'
                                                                                        AND TMOV.DATAEMISSAO >= ?
                                                                                        AND TMOV.DATAEMISSAO <= ?
                                                                                        AND TMOV.CODVEN1 = ?
                                                                                        GROUP BY	
	                                                                                        TMOV.CODCOLIGADA,
	                                                                                        TMOV.CODVEN1,
	                                                                                        TMOV.CODTMV
                                                                                        ORDER BY
	                                                                                        VENDEDOR", new object[]
																						 {
																							 AppLib.Context.Empresa,
																							 CodVendedor,
																							 AppLib.Context.Empresa,
																							 DataInicial,
																							 DataFinal,
																							 CodVendedor,
																							 DataInicial,
																							 DataFinal,
																							 AppLib.Context.Empresa,
																							 DataInicial,
																							 DataFinal,
																							 CodVendedor }
																						 );
			}

            this.DetailReport.DataSource = dtHistoricoVendas;
            this.DetailReport2.DataSource = dtHistoricoVendas_2;

            // 2.1.10
            xrVendedor.DataBindings.Add("Text", null, "VENDEDOR");
            xrPedidos.DataBindings.Add("Text", null, "QTD_PEDIDO");
            xrClientesAtendidos.DataBindings.Add("Text", null, "QTD_CLIENTE");
            xrClientesCarteira.DataBindings.Add("Text", null, "QTD_CLIENTE_CARTEIRA");
            xrTotal.DataBindings.Add("Text", null, "VALOR", "{0:n2}");

			// 2.1.15
			xrVendedor2.DataBindings.Add("Text", null, "VENDEDOR");
			xrPedidos2.DataBindings.Add("Text", null, "QTD_PEDIDO");
			xrClientesAtendidos2.DataBindings.Add("Text", null, "QTD_CLIENTE");
			xrClientesCarteira2.DataBindings.Add("Text", null, "QTD_CLIENTE_CARTEIRA");
			xrTotal2.DataBindings.Add("Text", null, "VALOR", "{0:n2}");
		}

        private void CarregaTotalGrupos()
        {
			// 2.1.10
            decimal totalGeral = 0;

            DataTable dt = DetailReport.DataSource as DataTable;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                totalGeral += Convert.ToDecimal(dt.Rows[i]["VALOR"]);
            }

			TotalPedidos = totalGeral;

            xrTotalPedidos.Text = totalGeral.ToString("C");

			// 2.1.15
			decimal totalGeral2 = 0;

			DataTable dt2 = DetailReport2.DataSource as DataTable;

			for (int i = 0; i < dt2.Rows.Count; i++)
			{
				totalGeral2 += Convert.ToDecimal(dt2.Rows[i]["VALOR"]);
			}

			TotalCotacoes = totalGeral2;

			xrTotalCotacoes.Text = totalGeral2.ToString("C");
		}

		private void CarregaTotalGeral()
        {
			xrTotalGeral.Text = (TotalPedidos + TotalCotacoes).ToString("C");
		}

        #endregion
    }
}
