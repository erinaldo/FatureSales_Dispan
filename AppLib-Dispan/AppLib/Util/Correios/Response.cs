using System;
using System.Text.RegularExpressions;

namespace AppLib.Util.Correios
{
    class Response
    {
        /// <summary>
        /// Texto de resposta recebido do servidor.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///  Construtor
        /// </summary>
        /// <param name="responseText">
        /// HTML retornado pelo servidor que será usado para construir o endereço
        /// </param>
        public Response(string responseText)
        {
            this.Text = responseText;
        }

        /// <summary>
        /// Método responsável por capturar um componente do endereço na resposta do servidor.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetValueByTag(string tag)
        {
            try
            {
                string value = this.Text;

                value = value.Replace("\r", "").Replace("\n", "");
                value = value.Substring(value.IndexOf("<span class=\"resposta\">" + tag));
                value = value.Substring(value.IndexOf("<span class=\"respostadestaque\">") + 31);

                string result = value.Substring(0, value.IndexOf("</span>"));
                return result.Trim();
            }
            catch (ArgumentOutOfRangeException)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Método responsável por realizar a conversão
        /// da resposta recebida do servidor para um objeto
        /// do tipo Address.
        /// </summary>
        /// <returns></returns>
        public Endereco ToEndereco()
        {
            if (IsInValidResponse())
            {
                return new Endereco
                {
                    logradouro = "Não encontrado",
                    cidade = "Não encontrado",
                };
            }

            Endereco end = new Endereco
            {
                logradouro = this.GetValueByTag("Logradouro"),
                bairro = this.GetValueByTag("Bairro"),
                cidade = Regex.Match(this.GetValueByTag("Localidade"), "^(.*?)   ").Groups[1].Value,
                estado = Regex.Match(this.GetValueByTag("Localidade"), "([A-Z]{2})$").Groups[1].Value,
                Cep = this.GetValueByTag("CEP")
            };

            if (end.logradouro == string.Empty)
            {
                end.CepUnico = true;
            }
            else if (end.logradouro.Contains(" -"))
            {
                end.logradouro = end.logradouro.Substring(0, end.logradouro.IndexOf(" -"));
            }
            return end;
        }

        /// <summary>
        /// Verifica se retornou algum erro
        /// </summary>
        /// <returns>
        /// True caso tenha recebido uma mensagem de erro
        /// </returns>
        private bool IsInValidResponse()
        {
            return this.Text.Contains("<div class=\"erro\">");
        }
    }
}
