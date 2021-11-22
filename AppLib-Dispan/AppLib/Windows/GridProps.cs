using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AppLib.Windows
{
    public class GridProps
    {
        [Category("_APP"), Description("Sequência")]
        public int Sequencia { get; set; }

        [Category("_APP"), Description("Nome da coluna")]
        public String Coluna { get; set; }

        [Category("_APP"), Description("Agrupar")]
        public Boolean Agrupar { get; set; }

        [Category("_APP"), Description("Visível")]
        public Boolean Visivel { get; set; }

        [Category("_APP"), Description("Largura da coluna")]
        public int Largura { get; set; }

        [Category("_APP"), Description("Alinhamento da coluna")]
        public Alinhamento Alinhamento { get; set; }

        [Category("_APP"), Description("Formatação da coluna")]
        public Formato Formato { get; set; }

        public GridProps()
        {
            Coluna = "";
            Largura = 50;
            Alinhamento = Windows.Alinhamento.Esquerda;
            Formato = Windows.Formato.Nenhum;
            Visivel = true;
        }

    }

    public enum Alinhamento { Esquerda, Direita, Centro }

    public enum Formato { Nenhum, Data, Hora, DataHora, DataHoraSegundos, DataHoraMilisegundos, Decimal2, Decimal4 }

}
