using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelRelacaoCarregamentoCompleto : DevExpress.XtraReports.UI.XtraReport
    {
        DateTime dataInicial;
        DateTime dataFinal;
        string caminhao;
        int qtdeCaminhao = 0;
        string codigoAuxiliar = string.Empty;
        public RelRelacaoCarregamentoCompleto(DateTime dataini, DateTime datafin, string _codigoAuxiliar)
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
            string sql = @"SELECT 
ZCARREGAMENTO.IDCARREGAMENTO,
ZPROGRAMACAOCARREGAMENTO.IDMOV, 
(CAST(FCFO.CODCFO AS VARCHAR(10)) +' - ' + FCFO.NOMEFANTASIA) AS CLIENTE, 
(CAST(ZCARREGAMENTO.CODTRA AS VARCHAR(10)) + ' - ' + TTRA.NOMEFANTASIA) AS TRANSPORTE, 
TPRODUTO.CODIGOAUXILIAR, 
TPRODUTO.NOMEFANTASIA, 
ZCARREGAMENTO.DATABAIXA,
ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA,
ZCARREGAMENTO.NUMERO,
TMOV.NUMEROMOV,
TMOVCOMPL.TPCULTURA,
TRPR.NOMEFANTASIA REPRESENTANTE,
ZCARREGAMENTO.DATA,
ZCARREGAMENTO.DATAAGENDAMENTO
FROM 
ZPROGRAMACAOCARREGAMENTO  
INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD AND TITMMOV.CODCOLIGADA = TPRODUTO.CODCOLPRD
INNER JOIN ZCARREGAMENTO ON ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO = ZCARREGAMENTO.IDCARREGAMENTO
INNER JOIN TMOV ON TITMMOV.CODCOLIGADA = TMOV.CODCOLIGADA AND TITMMOV.IDMOV = TMOV.IDMOV
INNER JOIN TMOVCOMPL ON TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA AND TMOV.IDMOV = TMOVCOMPL.IDMOV
INNER JOIN TRPR ON TMOV.CODRPR = TRPR.CODRPR AND TMOV.CODCOLIGADA = TRPR.CODCOLIGADA
INNER JOIN TTRA ON ZCARREGAMENTO.CODTRA = TTRA.CODTRA AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TTRA.CODCOLIGADA
INNER JOIN FCFO ON TMOV.CODCFO = FCFO.CODCFO 
WHERE Convert(date,ZCARREGAMENTO.DATA) >= ? AND convert(date, ZCARREGAMENTO.DATA) <= ? AND ZCARREGAMENTO.STATUS = 'CONCLUÍDO' AND TPRODUTO.CODIGOAUXILIAR LIKE ? ORDER BY ZPROGRAMACAOCARREGAMENTO.IDMOV
";
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dataInicial, dataFinal, codigoAuxiliar });
            if (dt.Rows.Count > 0)
            {
                this.DetailReport.DataSource = dt;
                txtIdCarregamento.DataBindings.Add("Text", null, "IDCARREGAMENTO");
                txtIdMov.DataBindings.Add("Text", null, "IDMOV");

                txtTransportadora.DataBindings.Add("Text", null, "TRANSPORTE");
                txtCodigoAuxiliar.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
                txtProduto.DataBindings.Add("Text", null, "NOMEFANTASIA");
                txtDataAutorizacao.DataBindings.Add("Text", null, "DATA", "{0:dd/MM/yyyy}");
                txtDataBaixa.DataBindings.Add("Text", null, "DATABAIXA", "{0:dd/MM/yyyy}");

                txtQtd.DataBindings.Add("Text", null, "QTDCARREGADA");
                xrLabel17.DataBindings.Add("Text", null, "NUMERO");
                //xrLabel19.DataBindings.Add("Text", null, "SEQUENCIAL");
                //Criando o grupo
                GroupField grupo = new GroupField("DATAAGENDAMENTO", XRColumnSortOrder.Ascending);
                GroupHeader1.GroupFields.Add(grupo);
                txtData.DataBindings.Add("Text", null, "DATAAGENDAMENTO", "{0:dd/MM/yyyy}");

                GroupField grupoCliente = new GroupField("CLIENTE");
                GroupHeader2.GroupFields.Add(grupoCliente);
                xrLabel7.DataBindings.Add("Text", null, "CLIENTE");

                GroupField grupoPedido = new GroupField("NUMEROMOV");
                GroupHeader3.GroupFields.Add(grupoPedido);
                xrLabel19.DataBindings.Add("Text", null, "NUMEROMOV");
                xrLabel27.DataBindings.Add("Text", null, "TPCULTURA");

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
            string sql = @"SELECT CODIGOAUXILIAR, TPRODUTO.NOMEFANTASIA, SUM(ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA)  + SUM(ZPROGRAMACAOCARREGAMENTO.QTDE)  QTDE FROM ZCARREGAMENTO
INNER JOIN ZPROGRAMACAOCARREGAMENTO ON ZPROGRAMACAOCARREGAMENTO.IDCARREGAMENTO = ZCARREGAMENTO.IDCARREGAMENTO
INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV  AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
INNER JOIN TPRODUTO ON TITMMOV.IDPRD = TPRODUTO.IDPRD AND TITMMOV.CODCOLIGADA = TPRODUTO.CODCOLPRD
WHERE CONVERT(DATE, ZCARREGAMENTO.DATAAGENDAMENTO) >= ? AND CONVERT(DATE, ZCARREGAMENTO.DATAAGENDAMENTO) <= ? 
AND TPRODUTO.CODIGOAUXILIAR LIKE ? AND ZCARREGAMENTO.STATUS = 'CONCLUÍDO'
GROUP BY CODIGOAUXILIAR, NOMEFANTASIA ORDER BY CODIGOAUXILIAR
";
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dataInicial, dataFinal, codigoAuxiliar });
                if (dt.Rows.Count > 0)
                {
                    this.DetailReport1.DataSource = dt;
                    xrLabel6.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
                    xrLabel12.DataBindings.Add("Text", null, "NOMEFANTASIA");
                    xrLabel9.DataBindings.Add("Text", null, "QTDE");    
                }
                
            }
            catch (Exception)
            {
            }
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {

        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                if (!txtIdCarregamento.Text.Equals(caminhao))
                {
                    if (!string.IsNullOrEmpty(txtIdCarregamento.Text))
                    {
                        qtdeCaminhao = qtdeCaminhao + 1;
                        caminhao = txtIdCarregamento.Text;
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
    }
}
