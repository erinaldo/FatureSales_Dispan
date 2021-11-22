using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient.Classes
{
    public static class MetodosEmail
    {
        public static string EnderecoEmail;
        public static int PortaEmail;
        public static string UsuarioEmail;
        public static string SenhaEmail;
        public static bool SSL;
        public static string EnviarComo;
        public static string EnviarComoDisplay;
        public static string EnviarPara;
        public static string Enderecos;
        public static string Assunto;

        private static bool SendMessage(string Mensagem, string Anexo)
        {
            try
            {
                AppLib.Util.Email email = new AppLib.Util.Email();
                email.Host = EnderecoEmail;
                email.Porta = PortaEmail;
                email.Usuario = UsuarioEmail;
                email.Senha = SenhaEmail;
                email.UsaSSL = SSL;
                email.De = EnviarComo;
                email.DeDisplay = EnviarComoDisplay;
                email.Para = Enderecos;
                email.Assunto = Assunto;
                email.Mensagem = Mensagem;
                email.Html = true;
                email.Anexo = Anexo;

                bool enviou = email.Enviar();

                if (enviou)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Erro ao Enviar E-mail");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static void EnviarEmailOrcamentoPedido(DataRow dr, string con)
        {

            string reportPath = String.Empty;

            try
            {
                string sql = $@"select CODTMV from TMOV where CODCOLIGADA = {AppLib.Context.Empresa} and IDMOV = {dr["IDMOV"]}";

                dynamic rel = null;

                if(MetodosSQL.GetField(sql, "CODTMV") == "2.1.10")
                {
                    RelPedidoDispan relatorio = new RelPedidoDispan();
                    relatorio.SelectRow = dr;
                    relatorio.Conexao = con;

                    rel = relatorio;
                }
                else
                {
                    RelOrcamentoDispan relatorio = new RelOrcamentoDispan();
                    relatorio.SelectRow = dr;
                    relatorio.Conexao = con;

                    rel = relatorio;
                }         

                Assunto = String.Format(@"{0}", dr["NUMEROMOV"]);

                formEmail frm = new formEmail(dr["IDMOV"].ToString(), Assunto);
                frm.ShowDialog();
                 
                reportPath = AppDomain.CurrentDomain.BaseDirectory + String.Format(@"pdf\{0}.pdf", frm.Assunto);

                var stream = new MemoryStream();
                rel.ExportToPdf(stream);

                using (var fs = new FileStream(reportPath, FileMode.OpenOrCreate))
                {
                    stream.WriteTo(fs);

                    fs.Close();
                }

                sql = String.Format(@"select top 1 ENDERECOEMAIL, PORTAEMAIL, USUARIOEMAIL, SENHAEMAIL, SSLEMAIL, ENVIARCOMO, ENVIARCOMODISPLAY from ZPARAMFATURE
                                where MOTIVOEMAIL = 'ORCAMENTOPEDIDO'");

                DataTable dt = MetodosSQL.GetDT(sql);

                string emailVendedor = MetodosSQL.GetField(String.Format(@"select EMAIL from GUSUARIO where CODUSUARIO = '{0}'", AppLib.Context.Usuario), "EMAIL");

                foreach (DataRow row in dt.Rows)
                {
                    EnderecoEmail = row["ENDERECOEMAIL"].ToString();
                    PortaEmail = (int)(row["PORTAEMAIL"]);
                    UsuarioEmail = row["USUARIOEMAIL"].ToString();
                    SenhaEmail = row["SENHAEMAIL"].ToString();
                    EnviarComo = emailVendedor;//row["ENVIARCOMO"].ToString();
                    EnviarComoDisplay = row["ENVIARCOMODISPLAY"].ToString();
                    SSL = row["SSLEMAIL"].ToString() == "0" ? false : true;
                }

                string sMsg = string.Empty;

                

                if (frm.Enviar)
                {
                    Assunto = frm.Assunto;
                    sMsg = frm.Email;

                    foreach (String TO in frm.Para)
                    {
                        Enderecos = TO;
                        if (SendMessage(sMsg, reportPath))
                        {
                            sql = String.Format(@"insert into ZMAILSEND
                                            (CODCOLIGADA, CODFILIAL, IDMOV, CODUSUARIO, SENDER, SENDERADDR, TOADDR, CCADDR, BCCADDR, SUBJECT, BODY, MSGTYPE, SENDTIME, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON)
                                            values (/*CODCOLIGADA*/ '1',
                                                    /*CODFILIAL*/ '1',
                                                    /*IDMOV*/ '{8}',
                                                    /*CODUSUARIO*/ '{0}',
		                                            /*SENDER*/ '{1}',
		                                            /*SENDERADDR*/ '{2}',
		                                            /*TOADDR*/ '{3}',
		                                            /*CCADDR*/ null,
		                                            /*BCCADDR*/ null,
		                                            /*SUBJECT*/ '{4}',
		                                            /*BODY*/ '{5}',
		                                            /*MSGTYPE*/ '{6}',
		                                            /*SENDTIME*/ GETDATE(),
		                                            /*RECCREATEDBY*/ '{7}',
		                                            /*RECCREATREDON*/ GETDATE(),
		                                            /*RECMODIFIEDBY*/ NULL,
		                                            /*RECMODIFIEDON*/ NULL)",
                                                            AppLib.Context.Usuario,
                                                            "DISPAN INDUSTRIA E COMERCIO LTDA",
                                                            emailVendedor,
                                                            TO,
                                                            EnviarComoDisplay,
                                                            sMsg,
                                                            "1",
                                                            AppLib.Context.Usuario,
                                                            dr["IDMOV"].ToString());

                            MetodosSQL.ExecQuery(sql);
                        }
                    }


                    MessageBox.Show("e-Mail enviado com sucesso!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }
    }
}
