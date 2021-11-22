using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Util
{
    public static class Log
    {
        public static void Escrever(String texto)
        {
            //Escrever("c:\\Log.txt", texto);
            Escrever("Log.txt", texto);
        }

        public static void Escrever(String arquivoLog, String texto)
        {
            if (!System.IO.File.Exists(arquivoLog))
            {
                System.IO.File.WriteAllText(arquivoLog, "");
            }

            System.IO.File.AppendAllText(arquivoLog, DateTime.Now.ToString() + ": " + texto + "\r\n");
        }

        public static void Limpar()
        {
            // System.IO.File.Delete("C:\\log.txt");
            System.IO.File.Delete("log.txt");
        }

        public static void Limpar(String arquivoLog)
        {
            System.IO.File.Delete(arquivoLog);
        }
    }
}
