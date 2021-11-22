using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AppFatureClient
{
    public partial class RelRelacaoParcial : DevExpress.XtraReports.UI.XtraReport
    {
        DateTime dataInicial;
        DateTime dataFinal;
        string caminhao;
        int qtdeCaminhao = 0;
        string codigoAuxiliar = string.Empty;
        public RelRelacaoParcial(DateTime dataini, DateTime datafin, string _codigoAuxiliar)
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
            string sql = @"SELECT ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO, ZPROGRAMACAOCARREGAMENTO.CAMINHAO, FCFO.NOMEFANTASIA CLIENTE, TMOV.NUMEROMOV, TRPR.NOMEFANTASIA REPRESENTANTE, TPRODUTO.CODIGOAUXILIAR, TPRODUTO.DESCRICAO, ISNULL(ZPROGRAMACAOCARREGAMENTO.QTDE, 0) QTDE, ISNULL(ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA, 0) QTDCARREGADA, ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA, TMOVCOMPL.TPCULTURA
FROM
ZPROGRAMACAOCARREGAMENTO,
FCFO,
TITMMOV,
TPRODUTO,
TMOV,
TRPR,
TMOVCOMPL
WHERE
ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV
AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
AND TITMMOV.IDMOV = TMOV.IDMOV
AND TMOV.CODCFO = FCFO.CODCFO
AND TMOV.IDMOV = TMOVCOMPL.IDMOV
AND TMOV.CODCOLIGADA = TMOVCOMPL.CODCOLIGADA
AND TITMMOV.IDPRD = TPRODUTO.IDPRD
AND TMOV.CODCOLIGADA = TRPR.CODCOLIGADA
AND TMOV.CODRPR = TRPR.CODRPR
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA >= ?
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA <= ?
AND TPRODUTO.CODIGOAUXILIAR LIKE ?
ORDER BY CAMINHAO, ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO
";
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dataInicial, dataFinal, codigoAuxiliar });
                this.DetailReport.DataSource = dt;
                txtCliente.DataBindings.Add("Text", null, "CLIENTE");
                txtPedido.DataBindings.Add("Text", null, "NUMEROMOV");
                txtCodPrd.DataBindings.Add("Text", null, "REPRESENTANTE");
                txtCodigoAuxiliar.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
                txtProduto.DataBindings.Add("Text", null, "DESCRICAO");
                txtQtd.DataBindings.Add("Text", null, "QTDE");
                xrLabel17.DataBindings.Add("Text", null, "QTDCARREGADA");
                txtData.DataBindings.Add("Text", null, "DATAPROGRAMADA", "{0: dddd -- dd/MM/yyyy}");
                txtCaminhao.DataBindings.Add("Text", null, "CAMINHAO");
                //Criando o grupo
                GroupField grupo = new GroupField("DATAPROGRAMADA");
                GroupHeader1.GroupFields.Add(grupo);
                GroupField grupoCliente = new GroupField("CLIENTE");
                GroupHeader2.GroupFields.Add(grupoCliente);
                GroupField grupoPedido = new GroupField("NUMEROMOV");
                GroupHeader3.GroupFields.Add(grupoPedido);
                xrLabel22.DataBindings.Add("Text", null, "NUMEROMOV");
                xrLabel27.DataBindings.Add("Text", null, "TPCULTURA");
            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("Favor fornecer uma data válida.", "Informação do Sistema.", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
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
            string sql = @"SELECT CODIGOAUXILIAR, TPRODUTO.DESCRICAO, (SUM(ZPROGRAMACAOCARREGAMENTO.QTDE) + SUM(ZPROGRAMACAOCARREGAMENTO.QTDCARREGADA)) QUANTIDADE FROM ZPROGRAMACAOCARREGAMENTO,
FCFO,
TITMMOV,
TPRODUTO,
TMOV
WHERE
ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV
AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV
AND TITMMOV.IDMOV = TMOV.IDMOV
AND TMOV.CODCFO = FCFO.CODCFO
AND TITMMOV.IDPRD = TPRODUTO.IDPRD
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA >= ?
AND ZPROGRAMACAOCARREGAMENTO.DATAPROGRAMADA <= ?
AND TPRODUTO.CODIGOAUXILIAR LIKE ?
GROUP BY CODIGOAUXILIAR, DESCRICAO
";
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { dataInicial, dataFinal, codigoAuxiliar });
                this.DetailReport1.DataSource = dt;
                xrLabel6.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
                xrLabel12.DataBindings.Add("Text", null, "DESCRICAO");
                xrLabel9.DataBindings.Add("Text", null, "QUANTIDADE");
            }
            catch (Exception)
            {
            }
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
            xrLabel16.Text = string.Format("QUANTIDADE DE CAMINHÕES PROGRAMADOS POR DIA: {0}", (qtdeCaminhao));
            qtdeCaminhao = 0;
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
        }
    }
}
