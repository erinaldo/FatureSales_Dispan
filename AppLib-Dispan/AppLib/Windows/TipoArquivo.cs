using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Windows
{
    public class TipoArquivo
    {
        public String NomeArquivo { get; set; }
        public String ExtensaoArquivo { get; set; }

        public TipoArquivo() { }

        public TipoArquivo(String nomeArquivo, String extensaoArquivo)
        {
            NomeArquivo = nomeArquivo;
            ExtensaoArquivo = extensaoArquivo;
        }
    }
}
