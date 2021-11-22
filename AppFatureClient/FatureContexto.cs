using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFatureClient
{
    public static class FatureContexto
    {
        public static bool Remoto { get; set; }
        public static wsLib.Service1SoapClient ServiceSoapClient { get; set; }
        public static AppInterop.AppInteropServer ServiceClient { get; set; }
    }
}
