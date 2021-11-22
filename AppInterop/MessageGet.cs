using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AppInterop
{
    [Serializable]
    public class MessageGet
    {
        public DataSet Retorno;
        public string Mensagem;

        public MessageGet()
        { 
        
        }
    }
}
