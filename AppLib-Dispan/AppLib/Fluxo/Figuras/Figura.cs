using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class Figura
    {
        public String Tipo { get; set; }
        public int PontoX { get; set; }
        public int PontoY { get; set; }
        public String Nome { get; set; }
        public String Texto { get; set; }
        public String Destino { get; set; }
        public String DestinoVerdadeiro { get; set; }
        public String DestinoFalso { get; set; }
        public String Variavel { get; set; }
        public String Atribuicao { get; set; }
        public String Expressao { get; set; }
        public String Retorno { get; set; }
        
        public String Subprocesso { get; set; }
        public List<VariaveisSubprocesso> Parametros = new List<VariaveisSubprocesso>();

    }
}
