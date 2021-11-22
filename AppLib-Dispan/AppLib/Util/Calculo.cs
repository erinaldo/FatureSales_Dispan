using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Util
{
    public static class Calculo
    {
        public static int DateDiff(DateTime DataFinal, DateTime DataInicial)
        {
            System.TimeSpan timeSpan1 = DataFinal - DataInicial;
            return timeSpan1.Days;
        }
    }
}
