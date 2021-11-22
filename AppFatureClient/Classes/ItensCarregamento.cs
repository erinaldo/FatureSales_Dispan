using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFatureClient.Classes
{
    public class ItensCarregamento
    {
        public int IDPROGRAMACAOCARREGAMENTO { get; set; }
        public int CODCOLIGADA { get; set; }
        public int IDMOV { get; set; }
        public int NSEQITMMOV { get; set; }
        public string NUMEROMOV { get; set; }
        public string CODIGOPRD { get; set; }
        public string STATUS { get; set; }
        public DateTime? DATAENTREGA { get; set; }
        public string DESCRICAO_DO_PRODUTO { get; set; }
        public int? IDPRDCOMPOSTO { get; set; }
        public decimal QTD_PEDIDO { get; set; }
        public decimal QTD_SALDO { get; set; }
        public decimal? QTD_PROG { get; set; }
        public decimal? QTD_CARREGADA { get; set; }
        public decimal? QTD_A_PROGRAMAR { get; set; }


    }
}
