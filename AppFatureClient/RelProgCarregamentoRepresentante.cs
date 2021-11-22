using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelProgCarregamentoRepresentante : DevExpress.XtraReports.UI.XtraReport
    {
        DateTime dataInicial;
        DateTime dataFinal;
        string codrpr, caminhao, cliente;
        int qtdeCaminhao = 0;
        
        public RelProgCarregamentoRepresentante(DateTime dataini, DateTime datafin, string CODRPR, string _cliente)
        {
            InitializeComponent();
            dataInicial = dataini;
            dataFinal = datafin;
            codrpr = CODRPR;
            cliente = _cliente;
        }
        private void header()
        {
            txtDataIni.Text = string.Format("Período de {0: dd/MMM/yyyy} a {1: dd/MMM/yyyy}", dataInicial, dataFinal);
        }
        public RelProgCarregamentoRepresentante()
        {
            InitializeComponent();
        }
        private void detalhe()
        {
            string sql;
           
                sql = @"SELECT ZPROGRAMACAOCARREGAMENTO.CAMINHAO, FCFO.NOMEFANTASIA CLIENTE, TMOV.NUMEROMOV, ('REPRESENTANTE: ' + TRPR.NOMEFANTASIA) REPRESENTANTE, TPRODUTO.CODIGOAUXILIAR, TPRODUTO.DESCRICAO, ZPROGRAMACAOCARREGAMENTO.QTDE, ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA, ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA 
FROM
ZPROGRAMACAOCARREGAMENTO,
FCFO,
TITMMOV,
TPRODUTO,
TMOV,
TRPR
WHERE
ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV
AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
AND TITMMOV.IDMOV = TMOV.IDMOV
AND TMOV.CODCFO = FCFO.CODCFO
AND TITMMOV.IDPRD = TPRODUTO.IDPRD
AND TMOV.CODCOLIGADA = TRPR.CODCOLIGADA
AND TMOV.CODRPR = TRPR.CODRPR
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA >= ?
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA <= ?

";
                if (!string.IsNullOrEmpty(codrpr))
                {
                    sql = sql + " AND TMOV.CODRPR = " + codrpr;
                }
                if (!string.IsNullOrEmpty(cliente))
                {
                    sql = sql + " AND FCFO.NOMEFANTASIA LIKE '% "+ cliente + " %'"; 
                }
                sql = sql + " ORDER BY CAMINHAO, ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO";
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dataInicial, dataFinal});
                this.DetailReport.DataSource = dt;
                txtCliente.DataBindings.Add("Text", null, "CLIENTE");
                txtPedido.DataBindings.Add("Text", null, "NUMEROMOV");
                txtCodigoAuxiliar.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
                txtProduto.DataBindings.Add("Text", null, "DESCRICAO");
                txtQtd.DataBindings.Add("Text", null, "QTDE");
                xrLabel18.DataBindings.Add("Text", null, "QTDCARREGADA");
                txtData.DataBindings.Add("Text", null, "DATAPROGRAMADA", "{0: dddd -- dd/MM/yyyy}");
                txtRepresentante.DataBindings.Add("Text", null, "REPRESENTANTE");
                xrLabel15.DataBindings.Add("Text", null, "CAMINHAO");
                //Criando o grupo
                GroupField representante = new GroupField("REPRESENTANTE");
                GroupHeader2.GroupFields.Add(representante);
                GroupField data = new GroupField("DATAPROGRAMADA");
                GroupHeader1.GroupFields.Add(data);
            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("Favor fornecer uma data válida.", "Informação do Sistema.", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            header();
        }

        private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql;
            if (string.IsNullOrEmpty(codrpr))
            {
                sql = @"SELECT CODIGOAUXILIAR, TPRODUTO.DESCRICAO, SUM(ZPROGRAMACAOCARREGAMENTO.QTDE) QUANTIDADE, SUM(ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA) QUANTIDADECARREGADA, TRPR.NOMEFANTASIA AS REPRESENTANTE FROM ZPROGRAMACAOCARREGAMENTO,
FCFO,
TITMMOV,
TPRODUTO,
TMOV,
TRPR
WHERE
ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV
AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
AND TITMMOV.IDMOV = TMOV.IDMOV
AND TMOV.CODCFO = FCFO.CODCFO
AND TITMMOV.IDPRD = TPRODUTO.IDPRD
AND TMOV.CODRPR = TRPR.CODRPR
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA >= ?
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA <= ?

";
                try
                {
                    if (!string.IsNullOrEmpty(codrpr))
                    {
                        sql = sql + " AND CODRPR = " + codrpr;
                    }
                    if (!string.IsNullOrEmpty(cliente))
                    {
                        sql = sql + " AND FCFO.NOMEFANTASIA LIKE '% " + cliente + " %'";
                    }
                    sql = sql + " GROUP BY CODIGOAUXILIAR, DESCRICAO, TRPR.NOMEFANTASIA";
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dataInicial, dataFinal });
                    this.DetailReport1.DataSource = dt;
                    xrLabel6.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
                    xrLabel12.DataBindings.Add("Text", null, "DESCRICAO");
                    xrLabel9.DataBindings.Add("Text", null, "QUANTIDADE");
                    xrLabel19.DataBindings.Add("Text", null, "QUANTIDADECARREGADA");
                    txtRepresentanteResumo.DataBindings.Add("Text", null, "REPRESENTANTE");
                    GroupField representante = new GroupField("REPRESENTANTE");
                    GroupHeader3.GroupFields.Add(representante);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                sql = @"SELECT CODIGOAUXILIAR, TPRODUTO.DESCRICAO, SUM(ZPROGRAMACAOCARREGAMENTO.QTDE) QUANTIDADE, SUM(ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA) QUANTIDADECARREGADA, TRPR.NOMEFANTASIA AS REPRESENTANTE FROM ZPROGRAMACAOCARREGAMENTO,
FCFO,
TITMMOV,
TPRODUTO,
TMOV,
TRPR
WHERE
ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV
AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
AND TITMMOV.IDMOV = TMOV.IDMOV
AND TMOV.CODCFO = FCFO.CODCFO
AND TITMMOV.IDPRD = TPRODUTO.IDPRD
AND TMOV.CODRPR = TRPR.CODRPR
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA >= ?
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA <= ?
AND TRPR.CODRPR = ?

";
                try
                {
                    if (!string.IsNullOrEmpty(codrpr))
                    {
                        sql = sql + " AND CODRPR = " + codrpr;
                    }
                    if (!string.IsNullOrEmpty(cliente))
                    {
                        sql = sql + " AND FCFO.NOMEFANTASIA LIKE '% " + cliente + " %'";
                    }
                    sql = sql + " GROUP BY CODIGOAUXILIAR, DESCRICAO, TRPR.NOMEFANTASIA";
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dataInicial, dataFinal, codrpr});
                    this.DetailReport1.DataSource = dt;
                    xrLabel6.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
                    xrLabel12.DataBindings.Add("Text", null, "DESCRICAO");
                    xrLabel9.DataBindings.Add("Text", null, "QUANTIDADE");
                    xrLabel19.DataBindings.Add("Text", null, "QUANTIDADECARREGADA");
                    txtRepresentanteResumo.DataBindings.Add("Text", null, "REPRESENTANTE");
                    GroupField representante = new GroupField("REPRESENTANTE");
                    GroupHeader3.GroupFields.Add(representante);
                }
                catch (Exception)
                {
                }
            }
            
        }
        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            detalhe();
        }

        private void Detail1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                if (!xrLabel15.Text.Equals(caminhao))
                {
                    if (!string.IsNullOrEmpty(xrLabel15.Text))
                    {
                        qtdeCaminhao = qtdeCaminhao + 1;
                        caminhao = xrLabel15.Text;
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
            xrLabel16.Text = string.Format("QUANTIDADE DE CAMINHÕES PROGRAMADOS POR DIA: {0}", (qtdeCaminhao));
            qtdeCaminhao = 0;
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            try
            {
                if (!xrLabel15.Text.Equals(caminhao))
                {

                    if (!string.IsNullOrEmpty(xrLabel15.Text))
                    {
                        qtdeCaminhao = qtdeCaminhao + 1;
                        caminhao = xrLabel15.Text;
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
