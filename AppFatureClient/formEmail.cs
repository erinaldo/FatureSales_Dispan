using AppFatureClient.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class formEmail : Form
    {
        public String[] Para { get; set; }
        public String Assunto { get; set; }
        public String Email { get; set; }
        String sql = String.Empty;
        String IDMOV = String.Empty;
        public bool Enviar { get; set; }

        string CorpoEmail;
        string LogoSite;
        string Rodape;
        string Tipo;

        public formEmail(String _IDMOV, String _Assunto)
        {
            InitializeComponent();
            IDMOV = _IDMOV;
            Assunto = _Assunto;

            Tipo = MetodosSQL.GetField($"select CODTMV from TMOV where IDMOV = {IDMOV} and CODCOLIGADA = {AppLib.Context.Empresa}", "CODTMV") == "2.1.05" ? "ORC" : "PED";
        }



        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            Regex rSplit = new Regex(";");
            Para = rSplit.Split(txtPara.Get());
            Assunto = txtAssunto.Get();
            Enviar = true;
            this.Close();
        }

        private void MudaPreview()
        {
            string emailEdit = txtEmail.Text;

            emailEdit = emailEdit.Replace(Environment.NewLine, "<br />");

            Email = String.Format(CorpoEmail, emailEdit, MetodosSQL.GetField(sql, "NOME"), MetodosSQL.GetField(sql, "EMAIL"), LogoSite, Rodape);

            wbPreview.DocumentText = Email;
        }

        private void formEmail_Load(object sender, EventArgs e)
        {
            try
            {
                sql = String.Format(@"select CONTATOCOM, EMAILCOM from TMOVCOMPL
                                        where IDMOV = '{0}'", IDMOV);

                string Cliente = MetodosSQL.GetField(sql, "CONTATOCOM");

                txtPara.Set(MetodosSQL.GetField(sql, "EMAILCOM"));

                

                if(Tipo == "ORC")
                {
                    Assunto = $"Proposta Comercial Numero {Assunto}";


                    txtEmail.Text = String.Format(@"Olá {0}!" 
                                        + "\r\n\r\n" + "Conforme solicitado, segue anexo o orçamento dos produtos requisitados." 
                                        + "\r\n\r\n" + "Antes da confirmação, favor conferir a descrição dos itens, suas respectivas quantidades e o prazo de fabricação." 
                                        + "\r\n\r\n" + "Mediante confirmação, os materiais serão fabricados sob medida de acordo com o pedido. Portanto, não poderão ser cancelados, trocados ou devolvidos." 
                                        + "\r\n\r\n" + "Nos colocamos à disposição para qualquer esclarecimento que se fizer necessário." 
                                        + "\r\n\r\n" + "Lembrando que nosso horário de funcionamento é de Segunda a Quinta-feira das 8h às 18h, Sexta-feira das 8h às 17h." 
                                        + "\r\n\r\n" + "Atenciosamente, ", Cliente);
                }
                else
                {
                    Assunto = $"Pedido Interno Numero {Assunto}";

                    txtEmail.Text = String.Format(@"Olá {0}!"
                                                  + "\r\n\r\n" + "Segue anexo o pedido interno Dispan." 
                                                  + "\r\n\r\n" + "Antes da confirmação, favor conferir a descrição dos itens, suas respectivas quantidades e o prazo de fabricação."
                                                  + "\r\n\r\n" + "Mediante confirmação, os materiais serão fabricados sob medida de acordo com o pedido. Portanto, não poderão ser cancelados, trocados ou devolvidos."
                                                  + "\r\n\r\n" + "Nos colocamos à disposição para qualquer esclarecimento que se fizer necessário."
                                                  + "\r\n\r\n" + "Atenciosamente,", Cliente);                                                   
                }

                txtAssunto.Set(Assunto);

                sql = @"select top 1 CORPOEMAIL from ZPARAMFATURE where MOTIVOEMAIL = 'ORCAMENTOPEDIDO'";

                CorpoEmail = MetodosSQL.GetField(sql, "CORPOEMAIL");

                sql = String.Format(@"select * from GUSUARIO where CODUSUARIO = '{0}'", AppLib.Context.Usuario);

                //LogoSite = AppDomain.CurrentDomain.BaseDirectory + @"logo_site.gif";
                //Rodape = AppDomain.CurrentDomain.BaseDirectory + @"rodape_assinatura.gif";

                LogoSite = "http://www.itinit.com.br/cliente/dispan/logo_site.gif";
                Rodape = "http://www.itinit.com.br/cliente/dispan/rodape_assinatura.gif";

                Email = String.Format(CorpoEmail, txtEmail.Text.Replace(Environment.NewLine, "<br />"), MetodosSQL.GetField(sql, "NOME"), MetodosSQL.GetField(sql, "EMAIL"), LogoSite, Rodape);

                wbPreview.DocumentText = Email;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            Enviar = false;
            this.Close();
        }

        private void campoMemoHISTORICOLONGO_Load(object sender, EventArgs e)
        {

        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            MudaPreview();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(CorpoEmail))
            {
                MudaPreview();
            }
            
        }
    }
}
