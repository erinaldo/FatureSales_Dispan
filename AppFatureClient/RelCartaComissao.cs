using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using AppFatureClient.Classes;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class RelCartaComissao : DevExpress.XtraReports.UI.XtraReport
    {
        double valorTotaleDeb = 0.0;
        double valorTotaleCred = 0.0;
        string _Carta = String.Empty;

        public RelCartaComissao(string Carta)
        {
            InitializeComponent();

            try
            {
                _Carta = Carta;

                lblCartaPG.Text = String.Format("Carta {0}/{1}", Carta, DateTime.Now.Year); 

                MetodosSQL.CS = AppLib.Context.poolConnection.Get().ConnectionString;
                
                string sql = String.Format(@"select FCFO.NOME as 'CLIENTE', 
                                                TM.NUMEROMOV as 'PEDIDO', 
	                                            REPLACE(REPLACE(REPLACE(CONVERT(varchar, CAST(TM.VALORLIQUIDOORIG AS money), 1),',', '_'), '.', ','), '_', '.') as 'VALORPEDIDO', 
	                                            ZTC.PERCENTUALCOMISSAO as 'PERCENTUALCOMISSAO', 
	                                            REPLACE(REPLACE(REPLACE(CONVERT(varchar, CAST(TM.VALORLIQUIDOORIG * (ZTC.PERCENTUALCOMISSAO / 100) AS money), 1),',', '_'), '.', ','), '_', '.') as 'COMISSAO'
	
                                         from ZTMOVCOMISSAO ZTC

                                         inner join TMOV TM
                                         on TM.IDMOV = ZTC.IDMOV

                                         inner join FCFO
                                         on FCFO.CODCFO = TM.CODCFO

                                         where ZTC.TIPOPAG = 'CRÉDITO' 
                                         and ZTC.NCARTA = '{0}'

                                         order by FCFO.NOME", Carta);

                this.DetailReport.DataSource = MetodosSQL.GetDT(sql);

                lblCliente.DataBindings.Add("Text", null, "CLIENTE");
                lblPedido.DataBindings.Add("Text", null, "PEDIDO");
                lblValor.DataBindings.Add("Text", null, "VALORPEDIDO");
                lblPercent.DataBindings.Add("Text", null, "PERCENTUALCOMISSAO");
                lblComissao.DataBindings.Add("Text", null, "COMISSAO");
                
                sql = String.Format(@"select FCFO.NOME as 'CLIENTE', 
                                         TM.NUMEROMOV as 'PEDIDO',
	                                     (SELECT top 1 REPLACE(REPLACE(REPLACE(CONVERT(varchar, CAST(FLAN.VALORORIGINAL AS money), 1),',', '_'), '.', ','), '_', '.') FROM FLAN

										  left join FLANBAIXA 
										  on FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
										  and FLANBAIXA.IDLAN = FLAN.IDLAN

										  WHERE FLAN.IDMOV in (SELECT 
																  IDMOVDESTINO 
															  FROM 
																  TMOVRELAC 
															  WHERE 
																  CODCOLORIGEM = 1
															  AND IDMOVORIGEM = (select IDMOV from TMOV where CODCOLIGADA = 1 and CODTMV = '2.1.20' and NUMEROMOV = TM.NUMEROMOV)
															  AND IDMOVDESTINO NOT IN (SELECT IDMOV FROM TMOV WHERE CODTMV IN ('2.1.40')))
										  AND FLAN.CODCOLIGADA = 1
										  AND FLAN.CODTDO <> 'ADC') as 'VALOR',
										 (SELECT top 1 FLAN.NUMERODOCUMENTO FROM FLAN

										  left join FLANBAIXA 
										  on FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
										  and FLANBAIXA.IDLAN = FLAN.IDLAN

										  WHERE FLAN.IDMOV in (SELECT 
																  IDMOVDESTINO 
															  FROM 
																  TMOVRELAC 
															  WHERE 
																  CODCOLORIGEM = 1
															  AND IDMOVORIGEM = (select IDMOV from TMOV where CODCOLIGADA = 1 and CODTMV = '2.1.20' and NUMEROMOV = TM.NUMEROMOV)
															  AND IDMOVDESTINO NOT IN (SELECT IDMOV FROM TMOV WHERE CODTMV IN ('2.1.40')))
										  AND FLAN.CODCOLIGADA = 1
										  AND FLAN.CODTDO <> 'ADC') as 'DOCUMENTO'
	
                                  from ZTMOVCOMISSAO ZTC

                                  inner join TMOV TM
                                  on TM.IDMOV = ZTC.IDMOV

                                  inner join FCFO
                                  on FCFO.CODCFO = TM.CODCFO

                                  where ZTC.TIPOPAG = 'DÉBITO' 
                                  and ZTC.NCARTA = '{0}'
                                
                                  order by FCFO.NOME", Carta);

                this.DetailReport2.DataSource = MetodosSQL.GetDT(sql);

                lblClienteDebito.DataBindings.Add("Text", null, "CLIENTE");
                lblPedidoDebito.DataBindings.Add("Text", null, "PEDIDO");
                lblNFe.DataBindings.Add("Text", null, "DOCUMENTO");
                lblValorDebito.DataBindings.Add("Text", null, "VALOR");

                //sql = String.Format(@"select cast(sum(TM.VALORLIQUIDOORIG*(ZTC.PERCENTUALCOMISSAO/100))  as numeric(10,2)) as 'VALOR'
	
                //                  from ZTMOVCOMISSAO ZTC

                //                  inner join TMOV TM
                //                  on TM.IDMOV = ZTC.IDMOV

                //                  inner join ZCOMISSAOCARTA ZCC
                //                  on ZCC.CODCARTA = ZTC.NCARTA

                //                  where ZTC.TIPOPAG = 'CRÉDITO' 
                //                  and ZTC.NCARTA = '{0}'", Carta);

                //lblTotalCredito.Text = String.Format("  R$ {0}", MetodosSQL.GetField(sql, "VALOR"));

                //sql = String.Format(@"select cast(sum(TM.VALORLIQUIDOORIG)  as numeric(10,2)) as 'VALOR'
	
                //                  from ZTMOVCOMISSAO ZTC

                //                  inner join TMOV TM
                //                  on TM.IDMOV = ZTC.IDMOV

                //                  inner join ZCOMISSAOCARTA ZCC
                //                  on ZCC.CODCARTA = ZTC.NCARTA

                //                  where ZTC.TIPOPAG = 'DÉBITO' 
                //                  and ZTC.NCARTA = '{0}'", Carta);

                //lblTotalDebito.Text = String.Format("  R$ {0}", MetodosSQL.GetField(sql, "VALOR"));

                //sql = String.Format(@"select cast(VALORCREDITOCOMPL as numeric(10,2)) as 'VALORCREDITOCOMPL', cast(VALORDEBITOCOMPL as numeric(10,2)) as 'VALORDEBITOCOMPL' from ZCOMISSAOCARTA where CODCARTA = {0}", Carta);

                sql = String.Format(@"select REPLACE(REPLACE(REPLACE(CONVERT(varchar, CAST(VALORCREDITOCOMPL AS money), 1),',', '_'), '.', ','), '_', '.') as 'VALORCREDITOCOMPL', 
                                             REPLACE(REPLACE(REPLACE(CONVERT(varchar, CAST(VALORDEBITOCOMPL AS money), 1),',', '_'), '.', ','), '_', '.') as 'VALORDEBITOCOMPL' 
                                             from ZCOMISSAOCARTA where CODCARTA = {0}", Carta);

                lblCredCpl.Text = String.Format("  R$ {0}", MetodosSQL.GetField(sql, "VALORCREDITOCOMPL"));

                lblDebCpl.Text = String.Format("  R$ {0}", MetodosSQL.GetField(sql, "VALORDEBITOCOMPL"));

                sql = String.Format(@"select OBSERVACAOCREDITOCOMPL,OBSERVACAODEBITOCOMPL from ZCOMISSAOCARTA where CODCARTA = {0}", Carta);

                lblCredObs.Text = " " + MetodosSQL.GetField(sql, "OBSERVACAOCREDITOCOMPL");

                lblDebObs.Text = " " + MetodosSQL.GetField(sql, "OBSERVACAODEBITOCOMPL");

                sql = String.Format(@"select NOME, CGC from TRPR

                                  inner join ZCOMISSAOCARTA ZCC
                                  on ZCC.CODRPR = TRPR.CODRPR

                                  where ZCC.CODCARTA = '{0}'", Carta);

                lblRepresentante.Text = MetodosSQL.GetField(sql, "NOME");
                lblDocumentoRPR.Text = MetodosSQL.GetField(sql, "CGC");


                sql = @"SELECT IMAGEM FROM GIMAGEM WHERE ID = (SELECT IDIMAGEM FROM GCOLIGADA WHERE CODCOLIGADA = ?)";
                sql = AppLib.Context.poolConnection.Get().ParseCommand(sql, new Object[] { AppLib.Context.Empresa });



                byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get().ExecGetField(null, sql, new Object[] { });
                System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
                xrPictureBox1.Image = Image.FromStream(ms);

                lblInicioCarta.Text = String.Format(@"Á {0}.", lblRepresentante.Text);
                lblRef.Text = "REF: COMISSAO SOBRE VENDAS.";
                lblATT.Text = String.Format("ATT: Sr(a) {0}.", lblRepresentante.Text);

                lblCarta.Text = lblCartaPG.Text;

                lblPrezado.Text = String.Format(@"Prezado(a) {0}, vimos por meio desta, comunicar a V.S.as o valor da comissão a receber referente aos pedidos abaixo mencionados:", lblRepresentante.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void lblValorDebito_SummaryRowChanged(object sender, EventArgs e)
        {
            
        }

        private void lblValorDebito_TextChanged(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(lblValorDebito.Text))
            {
                valorTotaleDeb += 0;
            }
            else
            {
                valorTotaleDeb += Double.Parse(lblValorDebito.Text.Replace("R$", ""));
            }
            
            lblTotalDebito.Text = String.Format("  R$ {0}", valorTotaleDeb.ToString("n2"));
            double total = (Double.Parse(lblTotalCredito.Text.Replace("  R$", "")) + Double.Parse(lblCredCpl.Text.Replace("  R$", ""))) - (Double.Parse(lblTotalDebito.Text.Replace("  R$", "")) + Double.Parse(lblDebCpl.Text.Replace("  R$", "")));

            lblTotal.Text = String.Format("  R$ {0}", total.ToString("n2"));
        }

        private void lblComissao_TextChanged(object sender, EventArgs e)
        {
            valorTotaleCred += Double.Parse(lblComissao.Text.Replace("R$", ""));
            lblTotalCredito.Text = String.Format("  R$ {0}", valorTotaleCred.ToString("n2"));
        }

        private void txtDebitoAnterior_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sql = String.Format(@"select top 1 case when cast((ZCC.VALORCREDITO + ZCC.VALORCREDITOCOMPL) - (ZCC.VALORDEBITO + ZCC.VALORDEBITOCOMPL) as numeric(10,2)) < 0 

                                                       then cast((ZCC.VALORCREDITO + ZCC.VALORCREDITOCOMPL) - (ZCC.VALORDEBITO + ZCC.VALORDEBITOCOMPL) as numeric(10, 2))
			                                           else 0

                                                       end as 'TOTAL',
                                                       ZCC.CODCARTA

                                          from ZCOMISSAOCARTA ZCC
                                          where ZCC.CODRPR = (select CODRPR from ZCOMISSAOCARTA where CODCARTA = '{0}')
                                          and CODCARTA < {0}
                                          order by CODCARTA desc", _Carta);

            if(String.IsNullOrWhiteSpace(MetodosSQL.GetField(sql, "TOTAL")))
            {
                txtDebitoAnterior.Text = String.Format("  R$ 0,00", 0);
            }
            else
            {
                txtDebitoAnterior.Text = String.Format("  R$ {0}", MetodosSQL.GetField(sql, "TOTAL"));
            }
            

            sql = String.Format(@"select cast((ZCC.VALORCREDITO + ZCC.VALORCREDITOCOMPL) - (ZCC.VALORDEBITO + ZCC.VALORDEBITOCOMPL) as numeric(10,2)) as 'TOTAL'
                                  from ZCOMISSAOCARTA ZCC
                                  where CODCARTA = '{0}'", _Carta);

            lblTotal.Text = String.Format("  R$ {0}", MetodosSQL.GetField(sql, "TOTAL"));
        }

        private void lblSomaCred_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lblSomaCred.Text = String.Format("  R$ {0}", (Double.Parse(lblTotalCredito.Text.Replace("  R$", "")) - Double.Parse(lblCredCpl.Text.Replace("  R$", ""))).ToString("n2"));
            }
            catch (Exception)
            {

            }
        }

        private void lblSomaDeb_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lblSomaDeb.Text = String.Format("  R$ {0}", (Double.Parse(lblTotalDebito.Text.Replace("  R$", "")) - Double.Parse(lblDebCpl.Text.Replace("  R$", ""))).ToString("n2")); 
            }
            catch (Exception)
            {

            }
        }
    }
}
