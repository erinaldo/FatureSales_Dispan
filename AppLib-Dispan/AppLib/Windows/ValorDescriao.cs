using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Windows
{
    public class ValorDescricao
    {
        public Object Valor { get; set; }
        public String Descricao { get; set; }

        public ValorDescricao() { }

        public ValorDescricao(String valor, String descricao)
        {
            Valor = valor;
            Descricao = descricao;
        }
    }
}
