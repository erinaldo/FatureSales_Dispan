using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class VariaveisSubprocesso
    {
        public String Parametro { get; set; }
        public String Valor { get; set; }

        public VariaveisSubprocesso Copiar()
        {
            VariaveisSubprocesso objDestino = new VariaveisSubprocesso();

            objDestino.Parametro = this.Parametro;
            objDestino.Valor = this.Valor;

            return objDestino;
        }
    }
}
