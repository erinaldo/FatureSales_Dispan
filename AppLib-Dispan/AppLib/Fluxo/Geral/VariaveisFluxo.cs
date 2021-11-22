using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class VariaveisFluxo
    {

        public String Variavel { get; set; }
        public String TipoDado { get; set; }
        public String TipoVariavel { get; set; }

        public VariaveisFluxo Copiar()
        {
            VariaveisFluxo objDestino = new VariaveisFluxo();

            objDestino.Variavel = this.Variavel;
            objDestino.TipoDado = this.TipoDado;
            objDestino.TipoVariavel = this.TipoVariavel;

            return objDestino;
        }
    }
}
