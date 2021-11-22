using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInterop
{
    [Serializable]
    public class Message
    {
        public object Retorno;
        public string Mensagem;

        public Message()
        { 
        
        }
    }
}