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


namespace AppFatureClient.Consultas
{
    public partial class frmConsultaCNPJ : Form
    {
        public frmConsultaCNPJ()
        {
            InitializeComponent();
        }
        public bool copiar = false;
        private bool pesquisa = false;

        private void frmConsultaCNPJ_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/cnpjreva_solicitacao2.asp");
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            maskedTextBox1.Enabled = false;
            txtCaptcha.Enabled = false;
            btnConsultar.Enabled = false;
            btnAtualiza.Enabled = false;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            pesquisa = true;

            webBrowser1.Document.GetElementById("cnpj").InnerText = maskedTextBox1.Text.Replace(".", "").Replace("-", "").Replace("/", "");
            webBrowser1.Document.GetElementById("txtTexto_captcha_serpro_gov_br").InnerText = txtCaptcha.Text;

            webBrowser1.Document.GetElementById("submit1").InvokeMember("click");
        }

        private void atualizaCaptcha()
        {
            try
            {
                mshtml.HTMLWindow2 w2 = (mshtml.HTMLWindow2)webBrowser1.Document.Window.DomWindow;
                w2.execScript("var ctrlRange = document.body.createControlRange();ctrlRange.add(document.getElementById('imgCaptcha'));ctrlRange.execCommand('Copy');", "javascript");
                pictureBox1.Image = Clipboard.GetImage();
                Clipboard.Clear();

                txtCaptcha.Enabled = true;
                btnConsultar.Enabled = true;
                txtCaptcha.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possivel concluir a operação.", "Informação do Sistema.", MessageBoxButtons.OK ,MessageBoxIcon.Warning);
                return;
            }
            
        }

        private void btnAtualiza_Click(object sender, EventArgs e)
        {
            atualizaCaptcha();

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsoluteUri.StartsWith("http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/cnpjreva_solicitacao2.asp"))
            {
                mshtml.HTMLWindow2 w2 = (mshtml.HTMLWindow2)webBrowser1.Document.Window.DomWindow;
                w2.execScript("var ctrlRange = document.body.createControlRange();ctrlRange.add(document.getElementById('imgCaptcha'));ctrlRange.execCommand('Copy');", "javascript");
                pictureBox1.Image = Clipboard.GetImage();
                Clipboard.Clear();

                txtCaptcha.Enabled = true;
                btnConsultar.Enabled = true;
                txtCaptcha.Clear();
            }

            if (e.Url.AbsoluteUri.StartsWith("http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/Cnpjreva_Comprovante.asp"))
            {
                //string[] SplitString;
                List<string> linhas = new List<string>();

                string[] lines = Regex.Split(webBrowser1.Document.Body.InnerText, "\r\n");
                //string[] lines = Regex.Split(webBrowser1.Document.GetElementById("content-core").OuterText, "\r\n");

                foreach (string line in lines)
                {
                    linhas.Add(line);
                }
                for (int i = 18; i < linhas.Count; i++)
                {
                    switch (linhas[i].ToString())
                    {
                        case "NÚMERO DE INSCRIÇÃO ":
                            txtNumeroInscricao.Text = linhas[i + 1].ToString();
                            txtMatriz.Text = linhas[i + 2].ToString();
                            txtDataAbertura.Text = linhas[i + 4].ToString();
                            i = i + 4;
                            break;
                        case "NOME EMPRESARIAL ":
                            txtRazaoSocial.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "TÍTULO DO ESTABELECIMENTO (NOME DE FANTASIA) ":
                            txtNomeFantasia.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "CÓDIGO E DESCRIÇÃO DA ATIVIDADE ECONÔMICA PRINCIPAL ":
                            txtCodigoDescricaoAEP.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "CÓDIGO E DESCRIÇÃO DAS ATIVIDADES ECONÔMICAS SECUNDÁRIAS ":
                            txtCodigoDescricaoAES.Text = linhas[i + 1].ToString();
                            if (!string.IsNullOrEmpty(linhas[i + 2].ToString()))
                            {
                                txtCodigoDescricaoAES.Text = txtCodigoDescricaoAES.Text + "\r\n" + linhas[i + 2].ToString();
                                if (!string.IsNullOrEmpty(linhas[i + 3].ToString()))
                                {
                                    txtCodigoDescricaoAES.Text = txtCodigoDescricaoAES.Text + "\r\n" + linhas[i + 3].ToString();
                                    if (!string.IsNullOrEmpty(linhas[i + 4].ToString()))
                                    {
                                        txtCodigoDescricaoAES.Text = txtCodigoDescricaoAES.Text + "\r\n" + linhas[i + 4].ToString();
                                    }
                                }
                            }
                            i = i + 1;
                            break;
                        case "CÓDIGO E DESCRIÇÃO DA NATUREZA JURÍDICA ":
                            txtCodigoDescricaoNaturezaJuridica.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "LOGRADOURO ":
                            txtLogr.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "NÚMERO ":
                            txtNumero.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "COMPLEMENTO ":
                            txtCompl.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "CEP ":
                            txtCep.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "BAIRRO/DISTRITO ":
                            txtBairro.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "MUNICÍPIO ":
                            txtMunicipio.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "UF ":
                            txtUF.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "ENDEREÇO ELETRÔNICO ":
                            txtEmail.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "TELEFONE ":
                            txtTelefone.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "ENTE FEDERATIVO RESPONSÁVEL (EFR) ":
                            txtEFR.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "SITUAÇÃO CADASTRAL ":
                            txtSituacaoCadastral.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "DATA DA SITUAÇÃO CADASTRAL ":
                            txtDataSituacao.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "MOTIVO DE SITUAÇÃO CADASTRAL ":
                            txtMotivoSituacao.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "SITUAÇÃO ESPECIAL ":
                            txtSituacaoEspecial.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        case "DATA DA SITUAÇÃO ESPECIAL ":
                            txtDataSituacaoEspecial.Text = linhas[i + 1].ToString();
                            i = i + 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            if (pesquisa == true)
            {
                if (string.IsNullOrEmpty(txtNumeroInscricao.Text))
                {
                    MessageBox.Show("Imagem digitada não confere. Favor verificar.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnAtualiza.Enabled = true;
                    atualizaCaptcha();
                }
            }

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            if (btnConsultar.Enabled == false)
            {
                if (MessageBox.Show("Deseja copiar as informações para o cadastro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    copiar = true;
                }
            }
            this.Close();
        }
    }
}
