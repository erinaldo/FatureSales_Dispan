using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Util
{
    public static class Classificacao
    {
        public static System.Drawing.Imaging.ImageFormat getImageFormat(String NomeArquivo)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(NomeArquivo);

            if (fi.Extension.ToUpper().Equals(".BMP"))
            {
                return System.Drawing.Imaging.ImageFormat.Bmp;
            }

            if (fi.Extension.ToUpper().Equals(".TIF"))
            {
                return System.Drawing.Imaging.ImageFormat.Tiff;
            }

            if (fi.Extension.ToUpper().Equals(".ICO"))
            {
                return System.Drawing.Imaging.ImageFormat.Icon;
            }

            if (fi.Extension.ToUpper().Equals(".GIF"))
            {
                return System.Drawing.Imaging.ImageFormat.Gif;
            }

            if (fi.Extension.ToUpper().Equals(".JPG") || fi.Extension.ToUpper().Equals(".JPEG"))
            {
                return System.Drawing.Imaging.ImageFormat.Jpeg;
            }

            if (fi.Extension.ToUpper().Equals(".PNG"))
            {
                return System.Drawing.Imaging.ImageFormat.Png;
            }

            return null;
        }

        public static SGBD? getSGBD(String Conexao)
        {
            if (AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.SqlClient)
            {
                return SGBD.SQL;
            }

            if (AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.SqlLocalDb)
            {
                return SGBD.SQL;
            }

            if (AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.SqlWebService)
            {
                return SGBD.SQL;
            }

            if (AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.OracleClient)
            {
                return SGBD.ORACLE;
            }

            if (AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.OracleWebService)
            {
                return SGBD.ORACLE;
            }

            return null;
        }
    }

    public enum SGBD { SQL, ORACLE }

}
