using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib
{
    public static class Context
    {
        public static AppLib.Data.PoolConnection poolConnection = new Data.PoolConnection();

        public static Boolean ControlarAcessos { get; set; }
        public static String ProdutoITINIT { get; set; }
        public static String Usuario { get; set; }
        public static String Senha { get; set; }
        public static String Perfil { get; set; }
        public static int Empresa { get; set; }
        public static int Filial { get; set; }
    }
}
