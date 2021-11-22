using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AppLib.Windows
{
    public class GridCuboProps
    {
        [Category("_APP"), Description("Sequência")]
        public int Sequencia { get; set; }

        [Category("_APP"), Description("Nome da coluna")]
        public String Coluna { get; set; }

        [Category("_APP"), Description("Area")]
        public Area Area { get; set; }

        [Category("_APP"), Description("Largura da coluna")]
        public int Largura { get; set; }

        [Category("_APP"), Description("Alinhamento da coluna")]
        public Alinhamento Alinhamento { get; set; }

        [Category("_APP"), Description("Formatação da coluna")]
        public Formato Formato { get; set; }

        public GridCuboProps()
        {
            Coluna = "";
            Area = Windows.Area.Filtro;
            Largura = 50;
            Alinhamento = Windows.Alinhamento.Esquerda;
            Formato = Windows.Formato.Nenhum;
        }

        public static GridCuboProps Clone(GridCuboProps origem)
        {
            GridCuboProps result = new GridCuboProps();
            result.Sequencia = origem.Sequencia;
            result.Coluna = origem.Coluna;
            result.Area = origem.Area;
            result.Largura = origem.Largura;
            result.Alinhamento = origem.Alinhamento;
            result.Formato = origem.Formato;
            return result;
        }

    }

    public enum Area { Linha, Coluna, Dados, Filtro, Oculta }

}
