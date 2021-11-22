using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Util
{
    public static class Format
    {
        public static String dataSql(DateTime dataHora)
        {
            return "CONVERT(DATETIME, '" + Util.Conversor.DateTimeToString(dataHora) + "', 121)";
        }

        public static String dataOracle(DateTime dataHora)
        {
            return "TO_TIMESTAMP('" + Util.Conversor.DateTimeToString(dataHora) + "', 'YYYY-MM-DD HH24:MI:SS.FF')";
        }

        public static String CompletarZeroEsquerda(int TamanhoMaximo, String Valor)
        {
            while (Valor.Length < TamanhoMaximo)
            {
                Valor = "0" + Valor;
            }

            return Valor;
        }

        public static String CompletarEspacoDireita(int TamanhoMaximo, String Valor)
        {
            while (Valor.Length < TamanhoMaximo)
            {
                Valor += " ";
            }

            return Valor;
        }

        public static String RemoveCharSpeciais(String Valor)
        {
            // Cria três matrizes, uma para acentos, sem acentos e caracteres especiais
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°", ".", "/" };
            
            //Replace em todos os acentos
            for (int i = 0; i < acentos.Length; i++)
            {
                Valor = Valor.Replace(acentos[i], semAcento[i]);
            }
            
            //Replace em todos os caracteres especiais           
            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                Valor = Valor.Replace(caracteresEspeciais[i], "");
            }
            
            //Replace nos espaços iniciais
            Valor = Valor.Replace("^\\s+", "");
            Valor = Valor.Replace("\\s+$", "");
            
            //Replace nos espaço duplos e tabulãções
            Valor = Valor.Replace("\\s+", " ");
            return Valor;
        }

        public static String RemoveCharSpeciaisSVirgula(String Valor)
        {
            // Cria três matrizes, uma para acentos, sem acentos e caracteres especiais
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };
            string[] caracteresEspeciais = { "\\.", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°", ".", "/" };

            //Replace em todos os acentos
            for (int i = 0; i < acentos.Length; i++)
            {
                Valor = Valor.Replace(acentos[i], semAcento[i]);
            }

            //Replace em todos os caracteres especiais           
            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                Valor = Valor.Replace(caracteresEspeciais[i], "");
            }

            //Replace nos espaços iniciais
            Valor = Valor.Replace("^\\s+", "");
            Valor = Valor.Replace("\\s+$", "");

            //Replace nos espaço duplos e tabulãções
            Valor = Valor.Replace("\\s+", " ");
            return Valor;
        }

        public static String DataAA(DateTime data)
        {
            String result = "";

            if ( data.Day < 10 )
            {
                result = "0";
            }

            result += data.Day;

            if (data.Month < 10)
            {
                result += "0";
            }

            result += data.Month;

            result += data.Year.ToString().Substring(2);

            return result;
        }

        public static String DataAAAA(DateTime data)
        {
            String result = "";

            if (data.Day < 10)
            {
                result = "0";
            }

            result += data.Day;

            if (data.Month < 10)
            {
                result += "0";
            }

            result += data.Month;

            result += data.Year;

            return result;
        }

        public static String Decimal(int Decimais, Decimal Valor)
        {
            return Convert.ToInt32(Valor * 100) + "";
        }

        public static String Direita(int TamanhoMaximo, String Valor)
        {
            if (TamanhoMaximo >= Valor.Length)
            {
                return Valor;
            }
            else
            {
                return Valor.Substring(Valor.Length - TamanhoMaximo);
            }
        }

        public static String Esquerda(int TamanhoMaximo, String Valor)
        {
            if (Valor.Length <= TamanhoMaximo)
            {
                return Valor;
            }
            else
            {
                return Valor.Substring(0, TamanhoMaximo);
            }
            
        }


    }
}
