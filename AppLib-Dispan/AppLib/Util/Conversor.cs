using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AppLib.Util
{
    public static class Conversor
    {
        #region DATA E HORA

        public static String DateTimeToString(DateTime DataHora)
        {
            String ano = DataHora.Year.ToString("0000");
            String mes = DataHora.Month.ToString("00");
            String dia = DataHora.Day.ToString("00");

            String hora = DataHora.Hour.ToString("00");
            String minuto = DataHora.Minute.ToString("00");
            String segundo = DataHora.Second.ToString("00");
            String milisegundo = DataHora.Millisecond.ToString("000");

            String result = ano + "-" + mes + "-" + dia + " " + hora + ":" + minuto + ":" + segundo + "." + milisegundo;

            return result;
        }

        public static DateTime GetDate(DateTime DataHora)
        {
            return new DateTime(DataHora.Year, DataHora.Month, DataHora.Day);
        }

        public static int GetMinutosCentesimal(DateTime DataHora)
        {
            int TotalMinutos = DataHora.Hour * 60;
            TotalMinutos += DataHora.Minute;
            return TotalMinutos;
        }

        public static decimal GetHorasDecimal(DateTime DataHora)
        {
            decimal baseHora = 60;
            decimal result = ((DataHora.Hour * baseHora) + DataHora.Minute) / baseHora;
            return result;
        }

        public static int GetHoras(int MinutosCentesimal)
        {
            int horas = (MinutosCentesimal / 60);
            return horas;
        }

        public static int GetHoras(DateTime DataHora)
        {
            return GetHoras(GetMinutosCentesimal(DataHora));
        }

        public static int GetMinutos(int MinutosCentesimal)
        {
            int minutos = MinutosCentesimal - (GetHoras(MinutosCentesimal) * 60);
            return minutos;
        }

        public static int GetMinutos(DateTime DataHora)
        {
            return GetMinutos(GetMinutosCentesimal(DataHora));
        }

        #endregion

        #region ARQUIVO EM BASE64

        public static String ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                // teste
                // format = System.Drawing.Imaging.ImageFormat.Bmp;
                // fim teste

                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static System.Drawing.Image Base64ToImage(String base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        public static String FileToBase64(String NomeArquivo)
        {
            byte[] toEncodeAsBytes = System.IO.File.ReadAllBytes(NomeArquivo);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static Boolean Base64ToFile(String NomeArquivo, String base64String)
        {
            try
            {
                byte[] encodedDataAsBytes = System.Convert.FromBase64String(base64String);
                System.IO.File.WriteAllBytes(NomeArquivo, encodedDataAsBytes);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static String EncodeTo64(String toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static String Base64Decode(String base64String)
        {
            return Base64Decode(base64String, Encoding.ASCII);
        }

        public static String Base64Decode(String base64String, Encoding encoding)
        {
            string returnValue = string.Empty;
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(base64String);

            if (encoding == Encoding.ASCII)
                returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            if (encoding == Encoding.UTF8)
                returnValue = Encoding.UTF8.GetString(encodedDataAsBytes);

            if (encoding == Encoding.Unicode)
                returnValue = Encoding.Unicode.GetString(encodedDataAsBytes);

            return returnValue;
        }

        #endregion

        #region ARRAY

        public static int[] Distinct(int[] array)
        {
            return array.ToList().Distinct().ToArray();
        }

        public static object[,] DataTableToArray(System.Data.DataTable data)
        {
            var result = Array.CreateInstance(typeof(object), data.Rows.Count, data.Columns.Count) as object[,];

            for (var i = 0; i < data.Rows.Count; i++)
                for (var j = 0; j < data.Columns.Count; j++)
                    result[i, j] = data.Rows[i][j];

            return result;
        }

        #endregion

    }
}
