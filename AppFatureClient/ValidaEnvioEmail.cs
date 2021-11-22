using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Globalization;

namespace AppFatureClient
{
    public class ValidaEnvioEmail
    {
        public ValidaEnvioEmail() { }
        private DateTime dataProgramada;
        private string tipo;
        private DataRowCollection drc;
        private decimal quantidade;
        private string idProgamacaoCarregamento;
        public bool validaEnvio(DateTime _dataProgramada, DataRowCollection _drc, string _tipo, decimal _quantidade)
        {
            dataProgramada = _dataProgramada;
            tipo = _tipo;
            drc = _drc;
            quantidade = _quantidade;

            //Verifica se a alteração está na semana atual
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int semanaAtual = cal.GetWeekOfYear(DateTime.Today, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            semanaAtual = semanaAtual + 1 == 53 ? 1 : semanaAtual;


            int semanaProxima = cal.GetWeekOfYear(dataProgramada, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            

            string dataProgText = dataProgramada.ToString("ddd", new CultureInfo("pt-BR"));
            string dataAtual = DateTime.Now.ToString("ddd", new CultureInfo("pt-BR"));


            if (dataAtual.Equals("qui") || dataAtual.Equals("sex") || dataAtual.Equals("sáb"))
            {
                semanaAtual = semanaAtual + 1;
            }


            if (dataProgText.Equals("qui") || dataProgText.Equals("sex") || dataProgText.Equals("sáb"))
            {
                    semanaProxima = semanaProxima + 1;
            }

            semanaProxima = semanaProxima == 53 ? 1 : semanaProxima;
            
            //Enviar o e-mail
            if (semanaAtual.Equals(semanaProxima))
            {
                //verifica se a data programada esta entre (sex, sab, dom)
                //if (dataProgText.Equals("sex") || dataProgText.Equals("sáb") || dataProgText.Equals("dom"))
                //{
                //    //verifica se a data atual esta entre (seg, ter, qua, qui)
                //    if (dataAtual.Equals("seg") || dataAtual.Equals("ter") || dataAtual.Equals("qua"))
                //    {
                //        //não envia
                //        return false;
                //    }
                //    else
                //    {
                //        //envia
                //        return enviaEmailDataProgramada();
                //    }
                //}
                //else
                //{
                    //envia
                    return enviaEmailDataProgramada();
                //}
            }
            else if (semanaProxima.Equals(semanaAtual + 1))
            {
                if (dataProgText.Equals("qui"))
                {
                    return enviaEmailDataProgramada();
                }
                ////verifica se a data atual esta entre (seg, ter, qua, qui)
                //if (dataAtual.Equals("qui") || dataAtual.Equals("sex") || dataAtual.Equals("sáb"))
                //{
                //    //if (dataProgramada.Equals("sex"))
                //    //{
                        
                //    //}
                //    //envia
                //    return enviaEmailDataProgramada();
                //}
                ////verifica se a data programada esta entre (qui, sex, sab, dom)
                //if (dataProgText.Equals("dom") || dataProgText.Equals("seg") || dataProgText.Equals("ter") || dataProgText.Equals("qua") || dataProgText.Equals("qui"))
                //{
                //    //verifica se a data atual esta entre (seg, ter, qua)
                //    if (dataAtual.Equals("seg") || dataAtual.Equals("ter") || dataAtual.Equals("qua"))
                //    {
                //        //não envia
                //        return false;
                //    }
                //    else
                //    {
                //        //envia
                //        return enviaEmailDataProgramada();
                //    }
                //}
                //return enviaEmailDataProgramada();
            }
            return false;
        }
        private bool enviaEmailDataProgramada()
        {
            //Busca o cliente
            string cliente = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT (FCFO.CODCFO + ' - ' + NOMEFANTASIA) CLIENTE FROM FCFO INNER JOIN TMOV ON FCFO.CODCFO = TMOV.CODCFO WHERE IDMOV = ?", new object[] { drc[0]["IDMOV"] }).ToString();
            string usuario = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT USUARIOCRIACAO FROM TMOV WHERE NUMEROMOV = ? AND CODTMV = '2.1.03' AND CODCOLIGADA = ?", new object[] {drc[0]["NUMEROMOV"], AppLib.Context.Empresa }).ToString();
            //Cria o objeto e-mail
            AppLib.Util.Email email = new AppLib.Util.Email();
            if (tipo.Equals("data"))
            {
                #region Cria a mensagem do e-mail Data
                email.Mensagem = @"
<html>  
        <head>
            <title>
                ALERTA SOBRE ALTERAÇÃO DA PROGRAMAÇÃO DO CARREGAMENTO - PALINI & ALVES
            </title>
            <style type=""text/css"">
              body
              {
                font-family: monospace, serif, arial, times;
                color: black;
                background-color: #FFFFFF;
              }
              table
              {
                border-color:#F9F9F9;
              }
              </style>
        </head>
 <table align=""center"" border=""1"" style=""width:90%"" cellspacing=""0"" cellpadding=""12"">
    <tbody><tr>
  <td bgcolor=""#F1F1F1""><font face=""Verdana"" size=""4""><b>ALTERAÇÃO DO CARREGAMENTO</b></font></td>
    </tr>
    <tr>
      <td>
        <table>
			<tbody>
			 <tr><td style=" + "width:230px;" + "><b>PEDIDO:</b></td>	<td>" + drc[0]["NUMEROMOV"].ToString() + " </td></tr><tr><td><b>IDMOV:</b>			</td><td>" + drc[0]["IDMOV"].ToString() + "</td></tr><tr><td valign=" + "top" + " style=" + "width:100px;" + "><b>CLIENTE:</b></td><td>" + cliente + "<br></td></tr><tr><td><b>USUÁRIO CARREGAMENTO:</b></td><td>" + AppLib.Context.Usuario.ToUpper() + "</td></tr><tr><td><b>USUÁRIO MOVIMENTO:</b></td><td>" + usuario.ToUpper() + "</td></tr><tr></tbody></table></td></tr>";
                for (int i = 0; i < drc.Count; i++)
                {
                    email.Mensagem = email.Mensagem + @" <tr>
<td>
<table>
	<tbody>
		 <tr>
<td style=""width:230px;""><b>NUMERO DA PROGRAMAÇÃO:</b></td>
				<td>- " + drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString() + " </td></tr><tr><td valign=" + "top" + " style=" + "width:100px;" + "><b>PRODUTO:</b></td><td>- " + drc[i]["CODIGOPRD"].ToString() + " - " + drc[i]["PRODUTO"].ToString() + "<br></td></tr><tr><td valign=" + "top" + " style=" + "width:100px;" + "><b>DATA ANTIGA:</b></td><td>- " + Convert.ToDateTime(drc[i]["DATAPROGRAMADA"]).ToShortDateString() + " <br></td></tr><tr><td valign=" + "top" + " style=" + "width:150px;" + "><b>DATA NOVA:</b></td><td>- " + dataProgramada.ToShortDateString() + "<br></td></tr></tbody></table></td></tr>";
                }
                email.Mensagem = email.Mensagem + @"</tbody></table></body></html>";
                email.Assunto = "Alteração de programação do pedido de número " + drc[0]["NUMEROMOV"].ToString();
                #endregion
            }
            if (tipo.Equals("quantidade"))
            {
                #region Cria a mensagem do e-mail Quantidade
                email.Mensagem = @"
<html>  
        <head>
            <title>
                ALERTA SOBRE ALTERAÇÃO DA PROGRAMAÇÃO DO CARREGAMENTO - PALINI & ALVES
            </title>
            <style type=""text/css"">
              body
              {
                font-family: monospace, serif, arial, times;
                color: black;
                background-color: #FFFFFF;
              }
              table
              {
                border-color:#F9F9F9;
              }
              </style>
        </head>
 <table align=""center"" border=""1"" style=""width:90%"" cellspacing=""0"" cellpadding=""12"">
    <tbody><tr>
  <td bgcolor=""#F1F1F1""><font face=""Verdana"" size=""4""><b>ALTERAÇÃO DO CARREGAMENTO</b></font></td>
    </tr>
    <tr>
      <td>
        <table>
			<tbody>
			  <tr>
<td style=""width:230px;""><b>NUMERO DA PROGRAMAÇÃO:</b></td>
				<td>" + drc[0]["IDPROGRAMACAOCARREGAMENTO"].ToString() + " </td></tr><tr><td style=" + "width:230px;" + "><b>PEDIDO:</b></td>	<td>" + drc[0]["NUMEROMOV"].ToString() + " </td></tr><tr><td><b>IDMOV:</b>			</td><td>" + drc[0]["IDMOV"].ToString() + "</td></tr><tr><td valign=" + "top" + " style=" + "width:100px;" + "><b>CLIENTE:</b></td><td>" + cliente + "<br></td></tr><tr><td><b>USUÁRIO CARREGAMENTO:</b>			</td><td>" + AppLib.Context.Usuario.ToUpper() + "</td></tr><tr><td><b>USUÁRIO MOVIMENTO:</b>			</td><td>" + usuario.ToUpper() + "</td></tr><tr></tbody></table></td></tr>";
                for (int i = 0; i < drc.Count; i++)
                {
                    email.Mensagem = email.Mensagem + @" <tr>
<td>
<table>
	<tbody>
		<tr><td valign=" + "top" + " style=" + "width:100px;" + "><b>PRODUTO:</b></td><td>- " + drc[i]["CODIGOPRD"].ToString() + " - " + drc[i]["PRODUTO"].ToString() + "<br></td></tr><tr><td valign=" + "top" + " style=" + "width:100px;" + "><b>QUANTIDATE ANTIGA:</b></td><td>- " + drc[i]["QTDE"].ToString() + " <br></td></tr><tr><td valign=" + "top" + " style=" + "width:150px;" + "><b>QUANTIDADE NOVA:</b></td><td>- " + quantidade + "<br></td></tr></tbody></table></td></tr>";
                }
                email.Mensagem = email.Mensagem + @"</tbody></table></body></html>";
                email.Assunto = "Alteração de programação do pedido de número " + drc[0]["NUMEROMOV"].ToString();
                #endregion
            }
            if (tipo.Equals("inclusão"))
            {
                #region Cria a mensagem do e-mail Inclusão
                string oooooo ="0";
                for (int i = 0; i < drc.Count; i++)
                {
                    idProgamacaoCarregamento = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, @"SELECT TOP 1
MAX(ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO) IDPROGRAMACAOCARREGAMENTO
FROM 
ZPROGRAMACAOCARREGAMENTO 
INNER JOIN TITMMOV ON ZPROGRAMACAOCARREGAMENTO.IDMOV = TITMMOV.IDMOV AND ZPROGRAMACAOCARREGAMENTO.NSEQITMMOV = TITMMOV.NSEQITMMOV AND ZPROGRAMACAOCARREGAMENTO.CODCOLIGADA = TITMMOV.CODCOLIGADA
WHERE ZPROGRAMACAOCARREGAMENTO.IDMOV = ?", new object[] { drc[i]["IDMOV"].ToString() }).ToString();
                    if (oooooo != drc[i]["NUMEROMOV"].ToString())
                    {
                        if (oooooo != "0")
                        {
                             email.Mensagem = email.Mensagem + @"</tbody></table></body></html>";
                        }
                        email.Mensagem = email.Mensagem + @"
<html>  
        <head>
            <title>
                ALERTA SOBRE INCLUSÃO NA PROGRAMAÇÃO DO CARREGAMENTO - PALINI & ALVES
            </title>
            <style type=""text/css"">
              body
              {
                font-family: monospace, serif, arial, times;
                color: black;
                background-color: #FFFFFF;
              }
              table
              {
                border-color:#F9F9F9;
              }
              </style>
        </head>
 <table align=""center"" border=""1"" style=""width:90%"" cellspacing=""0"" cellpadding=""12"">
    <tbody><tr>
  <td bgcolor=""#F1F1F1""><font face=""Verdana"" size=""4""><b>INCLUSÃO DO CARREGAMENTO</b></font></td>
    </tr>
    <tr>
      <td>
        <table>
			<tbody>
			  <tr>
<td style=""width:230px;""><b>NUMERO DA PROGRAMAÇÃO:</b></td>
				<td>" + idProgamacaoCarregamento + " </td></tr><tr><td style=" + "width:230px;" + "><b>PEDIDO:</b></td>	<td>" + drc[i]["NUMEROMOV"].ToString() + " </td></tr><tr><td><b>IDMOV:</b>			</td><td>" + drc[i]["IDMOV"].ToString() + "</td></tr><tr><td valign=" + "top" + " style=" + "width:100px;" + "><b>CLIENTE:</b></td><td>" + cliente + "<br></td></tr><tr><td><b>USUÁRIO CARREGAMENTO:</b>			</td><td>" + AppLib.Context.Usuario.ToUpper() + "</td></tr><tr><td><b>USUÁRIO MOVIMENTO:</b>			</td><td>" + usuario.ToUpper() + "</td></tr><tr></tbody></table></td></tr>";
                        oooooo = drc[i]["NUMEROMOV"].ToString();
                    }
                    
                    //for (int iitens = 0; iitens < drc.Count; iitens++)
                    //{
                        email.Mensagem = email.Mensagem + @" <tr>
<td>
<table>
	<tbody>
		<tr><td valign=" + "top" + " style=" + "width:100px;" + "><b>PRODUTO:</b></td><td>- " + drc[i]["CODIGOPRD"].ToString() + " - " + drc[i]["DESCRICAO_DO_PRODUTO"].ToString() + "<br></td></tr><tr><td valign=" + "top" + " style=" + "width:100px;" + "><b>DATA PROGRAMADA:</b></td><td>- " + dataProgramada.ToShortDateString() + " <br></td></tr><tr><td valign=" + "top" + " style=" + "width:150px;" + "><b>QUANTIDADE:</b></td><td>- " + drc[i]["QTD_SALDO"].ToString() + "<br></td></tr></tbody></table></td></tr>";
                   // }
                        
                }
               
                email.Assunto = "Inclusão de programação do pedido de número ";// +drc[i]["NUMEROMOV"].ToString();
                #endregion
            }

            #region Configuração do e-mail
            System.Data.DataTable dtEmail = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT ENDERECOEMAIL, PORTAEMAIL, USUARIOEMAIL, SENHAEMAIL, ENVIARCOMO FROM ZPARAMFATURE WHERE CODCOLIGADA = ?", new object[] { AppLib.Context.Empresa });
            email.Host = dtEmail.Rows[0]["ENDERECOEMAIL"].ToString();
            email.Porta = Convert.ToInt32(dtEmail.Rows[0]["PORTAEMAIL"]);
            email.Usuario = dtEmail.Rows[0]["USUARIOEMAIL"].ToString();
            email.De = dtEmail.Rows[0]["ENVIARCOMO"].ToString();
            email.Senha = dtEmail.Rows[0]["SENHAEMAIL"].ToString();

            //Busca a lista de e-mails a serem enviados

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT EMAIL FROM ZENVIAEMAIL WHERE EVENTO = ? AND CODCOLIGADA = ?", new object[] { "ALTERAÇÃO CARREGAMENTO", AppLib.Context.Empresa });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                email.Para = dt.Rows[i]["EMAIL"].ToString();
                email.Enviar();
            }
            return true;
            #endregion
        }
    }
}
