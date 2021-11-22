using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Windows
{
    public class CodigoNome
    {
        public String Codigo { get; set; }
        public String Nome { get; set; }

        public CodigoNome() { }

        public CodigoNome(String codigo, String nome)
        {
            Codigo = codigo;
            Nome = nome;
        }
    }
}
