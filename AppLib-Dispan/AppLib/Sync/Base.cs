using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AppLib.Sync
{
    public class Base
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Nome da conexão local")]
        public String ConexaoLocal { get; set; }

        [Category("_APP"), Description("Nome da conexão remota")]
        public String ConexaoRemota { get; set; }

        [Category("_APP"), Description("Consulta da grid")]
        public String[] Consulta { get; set; }

        public Object[] Parametros = new Object[] { };

        [Category("_APP"), Description("Tabela que recebe os dados")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Tipo da Sincronização")]
        public Tipo Tipo { get; set; }
        
        #endregion        

        #region CONSTRUTOR

        public Base()
        {
            ConexaoLocal = "Conn2";
            ConexaoRemota = "Start";
        }
        
        #endregion

        #region MÉTODOS

        public String GetConsulta()
        {
            String result = "";

            for (int i = 0; i < Consulta.Length; i++)
            {
                result += Consulta[i] + "\r\n";
            }

            return result;
        }

        #endregion

    }

    public enum Tipo { Receber, Enviar }

}
