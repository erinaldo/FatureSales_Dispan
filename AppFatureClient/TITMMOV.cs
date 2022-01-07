using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFatureClient.Classes;

namespace AppFatureClient
{
    public class TITMMOV
    {
        public int CODCOLIGADA { get; set; }
        public int IDMOV { get; set; }
        public int NSEQITEMMOV { get; set; }
        public int NUMEROSEQUENCIAL { get; set; }
        public string NUMITEMPEDIDO { get; set; }
        public string CODAUXILIAR { get; set; }
        public string PRODUTO { get; set; }
        public decimal QUANTIDADE { get; set; }
        public string UNIDADE { get; set; }
        public decimal PRECOUNITARIO { get; set; }
        public decimal VALORTOTAL { get; set; }
        public string NUMEROCCF { get; set; }
        public int IDPRD { get; set; }
        public string CODIGOPRD { get; set; }  
        public decimal ALIQUOTAIPI { get; set; }
        public string NUMEROCFOP { get; set; }
        public string HISTORICOLONGO { get; set; }
        public string PRECOUNITARIOSELEC { get; set; }
        public string DISTANCIAMENTO { get; set; }

        public DateTime? DATAENTREGA { get; set; }
        public int IDNAT { get; set; }
        public string APLICACAO { get; set; }

        public Decimal SALDO_FISICO { get; set; }
        public Decimal SALDO_PEDIDO { get; set; }
        public Decimal SALDO_DISPONIVEL { get; set; }

        public String NUMPEDIDO { get; set; }
        public String TIPOINOX { get; set; }

        public decimal VALORPINTURA { get; set; }

        public string TIPOPINTURA { get; set; }

        public string CORPINTURA { get; set; }

        public decimal VALORDESCORIGINAL { get; set; }
        public decimal VALORDESPORIGINAL { get; set; }

        public decimal AJUSTEVALOR { get; set; }

        public int? TIPOAJUSTEVALOR { get; set; }

        public decimal? PRECOTABELA { get; set; }

        public decimal RATEIOFRETE { get; set; }

        public List<ItemComposicao> COMPOSICAO { get; set; }
    }
}
