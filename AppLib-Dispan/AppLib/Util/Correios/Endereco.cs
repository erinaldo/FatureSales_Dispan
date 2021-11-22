using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppLib.Util.Correios
{
    public class Endereco
    {
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string estado { get; set; }
        public string logradouro { get; set; }
        private string _cep;

        /// <summary>
        ///    Valida o cep com oito ou nove digitos,
        ///    com ou sem máscara.
        /// </summary>
        public string Cep
        {
            get { return _cep; }
            set
            {
                if (value.Length != 8 && value.Length != 9)
                {
                    throw new FormatException("O CEP informado é inválido.");
                }

                int count = value.Count(Char.IsDigit);

                if (count != 8)
                {
                    throw new FormatException("O CEP informado é inválido.");
                }
                _cep = value;
            }
        }
        /// <summary>
        ///     True quando for um cep único para toda a cidade.
        /// </summary>
        public Boolean CepUnico { get; set; }
    }
}
