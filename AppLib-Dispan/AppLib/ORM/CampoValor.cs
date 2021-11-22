using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.ORM
{
    public class CampoValor
    {
        public String Campo { get; set; }
        public Object Valor { get; set; }

        public CampoValor(String campo, Object valor)
        {
            Campo = campo;
            Valor = valor;
        }
    }
}
