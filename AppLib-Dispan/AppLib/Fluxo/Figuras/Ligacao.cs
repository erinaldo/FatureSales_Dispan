using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class Ligacao
    {
        public int PontoX1 { get; set; }
        public int PontoY1 { get; set; }
        public int PontoX2 { get; set; }
        public int PontoY2 { get; set; }

        public System.Drawing.Color CorLinha { get; set; }
        public float Espessura { get; set; }
        public String Estilo { get; set; }

        public enum OpcoesEstilo
        {
            Normal, Pontilhada, Traço, TraçoTraço, TraçoPonto
        }

        public Ligacao() { }

        private System.Drawing.Point a;
        private System.Drawing.Point b;

        private System.Drawing.Point[] pontos = new System.Drawing.Point[2];

        private void Calcular()
        {
            a = new System.Drawing.Point(PontoX1, PontoY1);
            b = new System.Drawing.Point(PontoX2, PontoY2);

            pontos[0] = a;
            pontos[1] = b;
        }

        public void Desenhar(System.Drawing.Graphics graph)
        {
            this.Calcular();

            System.Drawing.Pen caneta = new System.Drawing.Pen(CorLinha, Espessura);

            if (Estilo.Equals("Normal")) caneta.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            if (Estilo.Equals("Pontilhada")) caneta.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            if (Estilo.Equals("Traço")) caneta.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (Estilo.Equals("TraçoTraço")) caneta.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            if (Estilo.Equals("TraçoPonto")) caneta.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;

            graph.DrawPolygon(caneta, pontos);
        }
    }
}
