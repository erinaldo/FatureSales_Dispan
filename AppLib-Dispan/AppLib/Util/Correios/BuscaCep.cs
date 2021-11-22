using System;
using System.Text;
using System.Threading.Tasks;

namespace AppLib.Util.Correios
{
    public class BuscaCep
    {

        /// <summary>
        /// Realiza a busca do endereço a partir do cep no site dos correios
        /// </summary>
        /// <param name="zip">Cep utilizado para busca</param>
        /// <param name="timeout">Timeout em milisegundos.</param>
        /// <returns> Endereço </returns>
        public static Endereco GetEndereco(string cep, int timeout = 10000)
        {
            const string url = "http://m.correios.com.br/movel/buscaCepConfirma.do";
            string dataToPost = "cepEntrada=" + cep + "&tipoCep=&cepTemp=&metodo=buscarCep";
            const string method = "POST";
            const string contentType = "application/x-www-form-urlencoded";

            var request = new Request(url, dataToPost, method, contentType);

            Response response = request.Send(timeout);
            return response.ToEndereco();
        }

    }
}
