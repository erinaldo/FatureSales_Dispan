using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using AppFatureClient.Classes;
using System.Text;

namespace AppFatureClient
{
    public partial class RelRequisicaoPecasPendentes : DevExpress.XtraReports.UI.XtraReport
    {
        public DateTime? DtaInicio { get; set; } 
        public DateTime? DtaFim { get; set; }
        public String Representante { get; set; }
        public String Cliente { get; set; }
        public String Status { get; set; }

        public RelRequisicaoPecasPendentes()
        {
            InitializeComponent();
        }

        private void CarregaLogo()
        {
            string sSql = String.Format(@"SELECT IMAGEM FROM GIMAGEM WHERE ID = (SELECT IDIMAGEM FROM GCOLIGADA WHERE CODCOLIGADA = ?)", AppLib.Context.Empresa);
            sSql = AppLib.Context.poolConnection.Get().ParseCommand(sSql, new Object[] { AppLib.Context.Empresa });

            byte[] arrayimagem = (byte[])AppLib.Context.poolConnection.Get().ExecGetField(null, sSql, new Object[] { });
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            xrPictureBox1.Image = Image.FromStream(ms);
        }

        private void detalhe()
        {
            if(Cliente == "%")
            {
                Cliente = String.Format(@"F.CODCFO like '{0}'", Cliente);
            }
            else
            {
                Cliente = String.Format(@"F.CODCFO in ({0})", Cliente);
            }

            string sql = String.Format(@"select R.NOMEFANTASIA as 'REPRESENTANTE', 
                                  F.NOMEFANTASIA as 'CLIENTE', 
	                              TM.NUMEROMOV,
                                  TM.NORDEM,
                                  TM.STATUS,
                                  TM.DATAEMISSAO,
	                              ZRD.QUANTIDADE-ZRD.QUANTIDADEBAIXADA as 'PENDENTE',
                                  SUBSTRING(ISNULL(TPC.DESCPADRAO,'') ,1,1000)+ '   '+SUBSTRING(ISNULL(TPC.DESCPADRAO,'') ,1001,2000)+ '   '+ ISNULL(CAST(ZRD.OBSERVACAO AS VARCHAR (5000)),'') as 'PRODUTO'
	   
                           from TMOV TM

                           inner join FCFO F
                           on F.CODCFO = TM.CODCFO

                           inner join TRPR R
                           on R.CODRPR =  TM.CODRPR

                           inner join ZREQDEVOLUCAO ZRD
                           on ZRD.IDREQ = TM.IDMOV

                           inner join TPRODUTO TP
                           on TP.IDPRD = ZRD.IDPRD

						   inner join TPRDCOMPL TPC
							on TPC.IDPRD = TP.IDPRD

                           where TM.CODTMV = '2.1.05'
                           and TM.CODCOLIGADA = 1
                           and ZRD.QUANTIDADEBAIXADA < ZRD.QUANTIDADE
                           and {0}
                           and R.CODRPR like '{1}'
                           and DATAEMISSAO between CONVERT(Datetime, '{2}', 103) and CONVERT(Datetime, '{3}', 103)
                           and TM.STATUS in ({4})", Cliente, Representante, DtaInicio, DtaFim, Status);
            

            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                Clipboard.SetText(sql);
                dt = MetodosSQL.GetDT(sql); //AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] {  });
                this.DetailReport.DataSource = dt;
                txtRepresentante.DataBindings.Add("Text", null, "REPRESENTANTE");
                txtNRequisicao.DataBindings.Add("Text", null, "NUMEROMOV");
                txtStatusReq.DataBindings.Add("Text", null, "STATUS");
                txtPedido.DataBindings.Add("Text", null, "NORDEM");
                txtRequisicaoCliente.DataBindings.Add("Text", null, "CLIENTE");
                txtQTDPendente.DataBindings.Add("Text", null, "PENDENTE");
                txtEquipamento.DataBindings.Add("Text", null, "PRODUTO");
                txtDataEmissao.DataBindings.Add("Text", null, "DATAEMISSAO", "Emissão: {0: dd/MM/yyyy}");
                //Criando o grupo
                GroupField grupoRepresentande = new GroupField("REPRESENTANTE");
                GroupHeader2.GroupFields.Add(grupoRepresentande);
                GroupField grupoRequisicao = new GroupField("NUMEROMOV");
                GroupHeader1.GroupFields.Add(grupoRequisicao);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CarregaLogo();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            detalhe();
        }
    }
}
