using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AppLib.Util
{

    public class Email
    {

        #region PROPRIEDADES

        public String Host { get; set; }
        public int Porta { get; set; }
        public Boolean UsaSSL { get; set; }
        public int Timeout { get; set; }

        public Boolean Autenticar { get; set; }
        public String Usuario { get; set; }
        public String Senha { get; set; }

        public String De { get; set; }
        public String DeDisplay { get; set; }
        public String Para { get; set; }
        public String ParaDisplay { get; set; }
        public String ResponderPara { get; set; }
        public String CC { get; set; }
        public String CCDisplay { get; set; }
        public String CCO { get; set; }
        public String CCODisplay { get; set; }

        public String Assunto { get; set; }
        public Boolean Html { get; set; }
        public String Mensagem { get; set; }
        public String Anexo { get; set; }
        public Stream AnexoStream { get; set; }

        #endregion

        public Email()
        {
            Host = "smtp.localhost.com.br";
            Porta = 25;
            UsaSSL = false;
            Timeout = 3600;
            Autenticar = true;

            Assunto = "";
            Html = true;
            Mensagem = "";
            Anexo = null;
            AnexoStream = Stream.Null;
        }

        public Boolean Enviar()
        {
            try
            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Host, Porta);
                client.Timeout = Timeout;
                client.UseDefaultCredentials = false;

                if (Autenticar)
                {
                    System.Net.NetworkCredential netcred = new System.Net.NetworkCredential(Usuario, Senha);
                    client.Credentials = netcred;
                }

                if (UsaSSL) client.EnableSsl = UsaSSL;

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(De, Para, Assunto, Mensagem);
                if (ResponderPara != null) message.ReplyToList.Add(ResponderPara);
                message.IsBodyHtml = Html;

                if (CC != null) message.CC.Add(CC);
                if (CCO != null) message.Bcc.Add(CCO);

                // PDF
                if (Anexo != null) message.Attachments.Add(new System.Net.Mail.Attachment(Anexo));

                client.Send(message);
                return true;
            }
            catch (System.Net.Mail.SmtpException e)
            {
                System.Console.WriteLine("Exception: " + e);
                return false;
            }

           
        }

    }

}