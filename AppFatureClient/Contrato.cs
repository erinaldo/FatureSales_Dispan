using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFatureClient
{
    public class Contrato
    {
        public String IDPRODUTO { get; set; }
        public String IDPROCESSO { get; set; }
        public String QUANTIDADE { get; set; }
        public String VALIDADE { get; set; }
        public String COMPLEMENTO { get; set; }

        public Contrato() { }

        public Contrato(String _IDPRODUTO, String _IDPROCESSO, String _QUANTIDADE, String _VALIDADE, String _COMPLEMENTO)
        {
            IDPRODUTO = _IDPRODUTO;
            IDPROCESSO = _IDPROCESSO;
            QUANTIDADE = _QUANTIDADE;
            VALIDADE = _VALIDADE;
            COMPLEMENTO = _COMPLEMENTO;
        }
    }
}
