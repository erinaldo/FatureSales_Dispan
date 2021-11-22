using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Windows
{
    public class DadosCadastro
    {
        public String TabelaPrincipal { get; set; }
        public String Tabela { get; set; }
        public String Campo { get; set; }

        public DadosCadastro() { }

        public DadosCadastro(String _TelaCadastro, String _Tabela, String _Campo)
        {
            TabelaPrincipal = _TelaCadastro;
            Tabela = _Tabela;
            Campo = _Campo;
        }
    }
}
