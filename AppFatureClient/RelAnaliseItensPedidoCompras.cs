using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using AppFatureClient.Classes;

namespace AppFatureClient
{
    public partial class RelAnaliseItensPedidoCompras : DevExpress.XtraReports.UI.XtraReport
    {
        DateTime? dataInicial;
        DateTime? dataFinal;

        public RelAnaliseItensPedidoCompras(DateTime? _datainicial, DateTime? _datafinal)
        {
            InitializeComponent();
            dataInicial = _datainicial;
            dataFinal = _datafinal;
        }

        private void CarregaLogo()
        {
            string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE ID = (SELECT IDIMAGEM FROM GCOLIGADA WHERE CODCOLIGADA = ?)";
            sSql = AppLib.Context.poolConnection.Get().ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });

            byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get().ExecGetField(null, sSql, new Object[] { });
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            xrPictureBox1.Image = Image.FromStream(ms);
        }

        private void detalhe()
        {
            //string sql = String.Format(@"select TP.CODIGOPRD, 
            //                                    TP.NOMEFANTASIA, 
            //                                 cast(sum(TTM.QUANTIDADEORIGINAL) as numeric(20,2)) as 'QUANTIDADE'

            //                             from (select * from TITMMOV where IDMOV in (select IDMOV from ZPROGRAMACAOCARREGAMENTO where DATAPROGRAMADA between CONVERT(Datetime, '{0}', 103) and CONVERT(Datetime, '{1}', 103))) TTM

            //                             inner join TPRODUTO TP
            //                             on TP.IDPRD = TTM.IDPRD

            //                             where SUBSTRING(TP.CODIGOPRD,1,10) in (select CODIGOPRD from ZPRODUTOANALISE where APLICACAO = 'V')
            //                               and TP.CODIGOPRD not in (select CODIGOPRD from ZPRODUTOANALISE where APLICACAO = 'E')
            //                               and TP.CODCOLPRD = 1

            //                             group by TP.CODIGOPRD, TP.NOMEFANTASIA

            //                             order by TP.NOMEFANTASIA", dataInicial, dataFinal);

            //string sql = String.Format(@"select TP.CODIGOPRD, 
            //                                    TP.NOMEFANTASIA, 
	           //                                 cast(sum(TTM.QUANTIDADEORIGINAL) as numeric(10,2)) as 'QUANTIDADE',
												//TP.CODUNDVENDA
	   
            //                             from (select IDPRD, QUANTIDADEORIGINAL, TITMMOV.NSEQITMMOV, TITMMOV.IDMOV, CODUND from TITMMOV (nolock)
												//	  inner join ZPROGRAMACAOCARREGAMENTO (nolock)
												//	  on ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA =  TITMMOV.CODCOLIGADA
												//	  and ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV
												//	  where DATAPROGRAMADA between CONVERT(Datetime, '{0}', 103) and CONVERT(Datetime, '{1}', 103)) TTM              

            //                             inner join (select TPRODUTO.IDPRD, TPRODUTO.CODIGOPRD, TPRODUTO.NOMEFANTASIA, TPRODUTO.CODCOLPRD, TPRODUTODEF.CODUNDVENDA from TPRODUTO (nolock)
												//	        inner join TPRODUTODEF (nolock)
												//	        on TPRODUTODEF.CODCOLIGADA = TPRODUTO.CODCOLPRD
												//	        and TPRODUTODEF.IDPRD = TPRODUTO.IDPRD) TP
            //                             on TP.IDPRD = TTM.IDPRD

            //                             where SUBSTRING(TP.CODIGOPRD,1,10) in (select CODIGOPRD from ZPRODUTOANALISE (nolock) where APLICACAO = 'V')
            //                             and TP.CODIGOPRD not in (select CODIGOPRD from ZPRODUTOANALISE (nolock) where APLICACAO = 'E')
            //                             and TP.CODCOLPRD = 1

            //                             group by TP.CODIGOPRD, TP.NOMEFANTASIA, TP.CODUNDVENDA", dataInicial, dataFinal);

            string sql = String.Format(@"select TP.CODIGOPRD, TTM.IDPRD, TP.NOMEFANTASIA, cast(sum(QUANTIDADEORIGINAL) as numeric(20,2)) as 'QUANTIDADE', TPD.CODUNDVENDA from TITMMOV TTM

										 inner join TPRODUTO TP
										 on TP.CODCOLPRD = TTM.CODCOLIGADA
										 and TP.IDPRD = TTM.IDPRD

										 left join TPRODUTODEF TPD
										 on TPD.CODCOLIGADA = TP.CODCOLPRD
										 and TPD.IDPRD = TP.IDPRD
										 
										 where IDMOV in (select IDMOV from ZPROGRAMACAOCARREGAMENTO 
														 where DATAPROGRAMADA between CONVERT(Datetime, '{0}', 103) and CONVERT(Datetime, '{1}', 103) 
														 group by IDMOV) 
									     and TTM.CODCOLIGADA = 1
										 and SUBSTRING(TP.CODIGOPRD,1,10) in (select CODIGOPRD from ZPRODUTOANALISE (nolock) where APLICACAO = 'V')
                                         and TP.CODIGOPRD not in (select CODIGOPRD from ZPRODUTOANALISE (nolock) where APLICACAO = 'E')
										 group by TP.CODIGOPRD, TTM.IDPRD, TP.NOMEFANTASIA, TPD.CODUNDVENDA", dataInicial, dataFinal);

            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = MetodosSQL.GetDT(sql);

                this.DetailReport.DataSource = dt;
                txtCodigo.DataBindings.Add("Text", null, "CODIGOPRD");
                txtNomeFantasia.DataBindings.Add("Text", null, "NOMEFANTASIA");
                txtQuantidade.DataBindings.Add("Text", null, "QUANTIDADE");
                txtCodUND.DataBindings.Add("Text", null, "CODUNDVENDA");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CarregaLogo();
            txtDataIni.Text = string.Format("Período de {0: dd/MMM/yyyy} a {1: dd/MMM/yyyy}", dataInicial, dataFinal);
        }

        private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            detalhe();
        }
    }
}
