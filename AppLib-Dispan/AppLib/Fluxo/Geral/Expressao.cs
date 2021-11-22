using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class Expressao
    {
        public String Variavel { get; set; }
        public String Atribuicao { get; set; }
        public String Texto { get; set; }
        public String ExpressaoXML { get; set; }

        public String TextoCompleto()
        {
            String result = Variavel + " " + Atribuicao + " " + Texto;
            return result;
        }

        public Expressao CopiarExpressao(Expressao ExpressaoBase, Expressao ExpressaoReceptora)
        {
            ExpressaoReceptora.Variavel = ExpressaoBase.Variavel;
            ExpressaoReceptora.Atribuicao = ExpressaoBase.Atribuicao;
            ExpressaoReceptora.Texto = ExpressaoBase.Texto;

            return ExpressaoReceptora;
        }
    }
}
