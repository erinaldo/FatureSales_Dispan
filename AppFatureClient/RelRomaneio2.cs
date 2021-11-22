using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class RelRomaneio2 : DevExpress.XtraReports.UI.XtraReport
    {

        DateTime dataInicial;
        DateTime dataFinal;
        string codigoAuxiliar = string.Empty;

        public RelRomaneio2(DateTime dataini, DateTime datafin, string _codigoAuxiliar)
        {
            InitializeComponent();
            dataInicial = dataini;
            dataFinal = datafin;
            codigoAuxiliar = _codigoAuxiliar;
            txtDataIni.Text = string.Format("Período de {0: dd/MMM/yyyy} a {1: dd/MMM/yyyy}", dataInicial, dataFinal);
            lblFiltro.Text = string.Format("Filtro: {0}", codigoAuxiliar);
        }

        private void carrega()
        {
            string query = @"select TTM.IDMOV ,
	   TTM.NSEQITMMOV,
	   TTMC.OBSPRDPADRAO,
	   TTMC.SEQUENCIAL,
	   TM.NUMEROMOV,
	   TMC.TPCULTURA,
	   FCFO.NOMEFANTASIA as 'Cliente',
	   TRPR.NOMEFANTASIA as 'Representante', 
       ZC.DATAAGENDAMENTO,
       ZPC.DATAPROGRAMADA as 'DTAGENDAMENTO',
       ZC.NUMERO,
	   ZPC.DATAPROGRAMADA,
	   ZPC.QTDE,
	   ZPC.QTDCARREGADA,
	   ZPC.DATAALTERACAO,
	   ZPC.CAMINHAO,
	   ZPC.OBSROMANEIO,
	   ZPC.IDCARREGAMENTO,
	   ZPC.SEMANA,
	   TP.CODIGOPRD,
	   (select 
	          case
			  when (TP.CODIGOPRD like '01.001.%' or TP.CODIGOAUXILIAR like 'PA%' or TP.CODIGOAUXILIAR like 'CAIXA PA-SE%' or TP.CODIGOAUXILIAR like 'PÉ %') then
	          case
			  when X.Contador > 0 and Y.Contador = 0 then 'MONOF'
			  when X.Contador = 0 and Y.Contador > 0 then 'TRIF'
			  when X.Contador > 0 and Y.Contador > 0 and (TP.NOMEFANTASIA like 'DESC%CONJ%' or TP.CODIGOPRD like '01.001.024.%' or (TP.CODIGOPRD like '01.001.068.%' or TP.CODIGOPRD like '01.001.206.%')) then 'MONOF'
	          when X.Contador > 0 and Y.Contador > 0 then 'MONOF/TRIF'
			  when X.Contador = 0 and Y.Contador = 0 then 'S/MOTOR'
			  end
			  else ''
			  end
	    from

	   (select  count(1) as 'Contador' from TITMMOV TTMS (NOLOCK)
		where TTMS.IDMOV = TTM.IDMOV
		and TTMS.IDPRDCOMPOSTO = TTM.IDPRDCOMPOSTO
		and TTMS.IDPRD in (select IDPRD from TPRODUTO TPS where TPS.NOMEFANTASIA like 'MOTOR MONOF.%')) as X,

	   (select count(1) as 'Contador'  from TITMMOV TTMS (NOLOCK)
		where TTMS.IDMOV = TTM.IDMOV
		and TTMS.IDPRDCOMPOSTO = TTM.IDPRDCOMPOSTO
		and TTMS.IDPRD in (select IDPRD from TPRODUTO TPS where TPS.NOMEFANTASIA like 'MOTOR TRIF.%')) as Y) as 'Fase',

		(case isnumeric(replace(TP.CODIGOAUXILIAR,'.',''))
		 when 1 then TP.DESCRICAO
		 when 0 then TP.CODIGOAUXILIAR
		 end
		) as 'Auxiliar',
		COUNT(1) over() as 'Total',
		(select count(1) from
			(SELECT CONVERT(INT, CAMINHAO) as 'CAMINHAO' 
			FROM ZPROGRAMACAOCARREGAMENTO 
			WHERE DATAPROGRAMADA between ? 
			and ? AND CODCOLIGADA = 1 
			and CAMINHAO is not null 
			GROUP BY CAMINHAO) X) as 'Total CAM'

from TITMMOV TTM (NOLOCK)

inner join TITMMOVCOMPL TTMC (NOLOCK)
on TTMC.IDMOV = TTM.IDMOV
and TTMC.NSEQITMMOV = TTM.NSEQITMMOV
and TTMC.CODCOLIGADA = 1


inner join TMOV TM (NOLOCK)
on TM.IDMOV = TTM.IDMOV
and TM.CODCOLIGADA = 1

inner join TMOVCOMPL TMC (NOLOCK)
on TMC.IDMOV = TM.IDMOV
and TMC.CODCOLIGADA = 1

inner join ZPROGRAMACAOCARREGAMENTO ZPC (NOLOCK)
on ZPC.IDMOV = TTM.IDMOV
and ZPC.NSEQITMMOV = TTM.NSEQITMMOV
and ZPC.CODCOLIGADA = 1

left join ZCARREGAMENTO ZC (NOLOCK)
on ZC.IDCARREGAMENTO = ZPC.IDCARREGAMENTO


inner join TPRODUTO TP (NOLOCK)
on TP.IDPRD = TTM.IDPRD

inner join FCFO (NOLOCK)
on FCFO.CODCFO = TM.CODCFO

inner join TRPR (NOLOCK)
on TRPR.CODRPR = TM.CODRPR

where (ZC.STATUS <> 'Concluido' or ZC.STATUS is null) 
and ZPC.DATAPROGRAMADA between ? and ?
and TP.CODIGOAUXILIAR like ?

order by FCFO.NOMEFANTASIA";

            AppLib.Data.SqlClient ssql = new AppLib.Data.SqlClient();
            ssql.Timeout = 3600;

            DataTable dt = new DataTable();


            try
            {
                dt = AppLib.Context.poolConnection.Get().ExecQuery(query, new Object[] { dataInicial, dataFinal, dataInicial, dataFinal, codigoAuxiliar });
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            



            if (dt.Rows.Count > 0)
            {
                
                this.DetailReport.DataSource = dt;
                txtCodPrd.DataBindings.Add("Text", null, "Representante");
                txtCodigoAuxiliar.DataBindings.Add("Text", null, "Auxiliar");
                lblMotor.DataBindings.Add("Text", null, "Fase");
                txtProduto.DataBindings.Add("Text", null, "OBSROMANEIO");
                txtQtd.DataBindings.Add("Text", null, "QTDE");
                xrLabel9.DataBindings.Add("Text", null, "QTDCARREGADA");
                txtData.DataBindings.Add("Text", null, "DATAAGENDAMENTO", "{0: dddd -- dd/MM/yyyy}");
                xrLabel35.DataBindings.Add("Text", null, "DTAGENDAMENTO", "{0: dd/MM/yyyy}");
                xrLabel17.DataBindings.Add("Text", null, "Cliente");
                xrLabel11.DataBindings.Add("Text", null, "SEQUENCIAL");
                xrLabel18.DataBindings.Add("Text", null, "NUMEROMOV");
                xrLabel27.DataBindings.Add("Text", null, "TPCULTURA");
                txtCaminhao.DataBindings.Add("Text", null, "CAMINHAO");
                txtData.DataBindings.Add("Text", null, "DATAPROGRAMADA", "{0:dd/MM/yyyy}");
                xrLabel26.DataBindings.Add("Text", null, "DATAALTERACAO", "{0:dd/MM/yyyy}");
                //xrLabel30.DataBindings.Add("Text", null, "OBSPRDPADRAO");
                //xrLabel11.DataBindings.Add("Text", null, "SEMANA");
                xrLabel24.DataBindings.Add("Text", null, "NUMERO");
                xrLabel16.DataBindings.Add("Text", null, "Total CAM");
                //Criando o grupo
                GroupField grupo = new GroupField("DATAPROGRAMADA");
                GroupHeader1.GroupFields.Add(grupo);
                GroupField grupoCliente = new GroupField("Cliente");
                GroupHeader2.GroupFields.Add(grupoCliente);
                GroupField grupoPedido = new GroupField("NUMEROMOV");
                GroupHeader3.GroupFields.Add(grupoPedido);

                query = @"select  TP.CODIGOAUXILIAR,TP.DESCRICAO, SUM(isnull(ZPC.QTDE,0)+isnull(ZPC.QTDCARREGADA,0)) as 'Total'

from TITMMOV TTM (NOLOCK)

inner join TITMMOVCOMPL TTMC (NOLOCK)
on TTMC.IDMOV = TTM.IDMOV
and TTMC.NSEQITMMOV = TTM.NSEQITMMOV
and TTMC.CODCOLIGADA = 1


inner join TMOV TM (NOLOCK)
on TM.IDMOV = TTM.IDMOV
and TM.CODCOLIGADA = 1

inner join TMOVCOMPL TMC (NOLOCK)
on TMC.IDMOV = TM.IDMOV
and TMC.CODCOLIGADA = 1

inner join ZPROGRAMACAOCARREGAMENTO ZPC (NOLOCK)
on ZPC.IDMOV = TTM.IDMOV
and ZPC.NSEQITMMOV = TTM.NSEQITMMOV
and ZPC.CODCOLIGADA = 1

left join ZCARREGAMENTO ZC (NOLOCK)
on ZC.IDCARREGAMENTO = ZPC.IDCARREGAMENTO


inner join TPRODUTO TP (NOLOCK)
on TP.IDPRD = TTM.IDPRD

inner join FCFO (NOLOCK)
on FCFO.CODCFO = TM.CODCFO

inner join TRPR (NOLOCK)
on TRPR.CODRPR = TM.CODRPR

where (ZC.STATUS <> 'Concluido' or ZC.STATUS is null) 
and ZPC.DATAPROGRAMADA between ? and ?
and TP.CODIGOAUXILIAR like ?

group by TP.CODIGOAUXILIAR,TP.DESCRICAO

order by TP.DESCRICAO";

                try
                {
                    dt = AppLib.Context.poolConnection.Get().ExecQuery(query, new Object[] { dataInicial, dataFinal, codigoAuxiliar });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
                if (dt.Rows.Count > 0)
                {
                    this.DetailReport1.DataSource = dt;
                    xrLabel20.DataBindings.Add("Text", null, "CODIGOAUXILIAR");
                    xrLabel21.DataBindings.Add("Text", null, "DESCRICAO");
                    xrLabel22.DataBindings.Add("Text", null, "Total");
                }
            }
            else
            {
                MessageBox.Show(String.Format("Não há registros para serem exibidos no periodo de {0: dd/MMM/yyyy} a {1: dd/MMM/yyyy} com filtro {2}", dataInicial, dataFinal, codigoAuxiliar), "Informativo");
                this.CloseRibbonPreview();
            }

            
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            carrega();
        }
    }
}
