using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.ORM
{
    public class Metadados
    {
        public String NomeTabela { get; set; }
        public String NomeCampo { get; set; }

        public String TipoCampo { get; set; }
        public int Tamanho { get; set; }
        public int Precisao { get; set; }
        public int Escala { get; set; }

        public Boolean Chave { get; set; }
        public Boolean AutoIncremento { get; set; }
        public Boolean Nulo { get; set; }
        public Boolean Virtual { get; set; }
    }
}
