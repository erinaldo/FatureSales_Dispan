using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public partial class FiguraCondicao
    {
        public String Nome { get; set; }
        public String DestinoVerdadeiro { get; set; }
        public String DestinoFalso { get; set; }
        public String Destino { get; set; }

        public Expressao Expressao = new Expressao();
        public CaixaTexto Caixa = new CaixaTexto();

        private int PontoX { get; set; }
        private int PontoY { get; set; }
        private int Largura { get; set; }
        private int Altura { get; set; }

        private System.Drawing.Color CorFundo { get; set; }
        private System.Drawing.Color CorBorda { get; set; }

        public Boolean Selecionado { get; set; }

        private int selecaoAX;
        private int selecaoAY;
        private int selecaoBX;
        private int selecaoBY;
        private int selecaoCX;
        private int selecaoCY;
        private int selecaoDX;
        private int selecaoDY;

        private System.Drawing.Point selecaoA;
        private System.Drawing.Point selecaoB;
        private System.Drawing.Point selecaoC;
        private System.Drawing.Point selecaoD;

        private System.Drawing.Point[] selecao = new System.Drawing.Point[4];

        private System.Drawing.Graphics Graphic;

        private void Carregar()
        {
            PontoX = Caixa.PontoX;
            PontoY = Caixa.PontoY;
            Largura = Caixa.Largura;
            Altura = Caixa.Altura;

            CorFundo = Caixa.CorFundo;
            CorBorda = Caixa.CorBorda;
        }

        public FiguraCondicao(CaixaTexto Caixa1, System.Drawing.Graphics graph)
        {
            Caixa = Caixa1;

            Carregar();

            Graphic = graph;
            this.Desenhar(Graphic);
        }

        private int retanguloAX;
        private int retanguloAY;
        private int retanguloBX;
        private int retanguloBY;
        private int retanguloCX;
        private int retanguloCY;
        private int retanguloDX;
        private int retanguloDY;

        private System.Drawing.Point a;
        private System.Drawing.Point b;
        private System.Drawing.Point c;
        private System.Drawing.Point d;

        private System.Drawing.Point[] pontos = new System.Drawing.Point[4];

        private void Calcular()
        {
            Carregar();

            retanguloAX = PontoX;
            retanguloAY = PontoY + Altura;
            retanguloBX = PontoX + Largura;
            retanguloBY = PontoY;
            retanguloCX = PontoX;
            retanguloCY = PontoY - Altura;
            retanguloDX = PontoX - Largura;
            retanguloDY = PontoY;

            a = new System.Drawing.Point(retanguloAX, retanguloAY);
            b = new System.Drawing.Point(retanguloBX, retanguloBY);
            c = new System.Drawing.Point(retanguloCX, retanguloCY);
            d = new System.Drawing.Point(retanguloDX, retanguloDY);

            pontos[0] = a;
            pontos[1] = b;
            pontos[2] = c;
            pontos[3] = d;

            int margemSelecao = 5;

            selecaoAX = retanguloDX - margemSelecao;
            selecaoAY = retanguloAY + margemSelecao;
            selecaoBX = retanguloBX + margemSelecao;
            selecaoBY = retanguloAY + margemSelecao;
            selecaoCX = selecaoBX;
            selecaoCY = retanguloCY - margemSelecao;
            selecaoDX = selecaoAX;
            selecaoDY = selecaoCY;

            selecaoA = new System.Drawing.Point(selecaoAX, selecaoAY);
            selecaoB = new System.Drawing.Point(selecaoBX, selecaoBY);
            selecaoC = new System.Drawing.Point(selecaoCX, selecaoCY);
            selecaoD = new System.Drawing.Point(selecaoDX, selecaoDY);

            selecao[0] = selecaoA;
            selecao[1] = selecaoB;
            selecao[2] = selecaoC;
            selecao[3] = selecaoD;
        }

        public void Desenhar(System.Drawing.Graphics graph)
        {
            this.Calcular();

            System.Drawing.SolidBrush pincelFundo = new System.Drawing.SolidBrush(CorFundo);
            System.Drawing.Pen canetaBorda = new System.Drawing.Pen(CorBorda, 2);

            graph.FillPolygon(pincelFundo, pontos);
            graph.DrawPolygon(canetaBorda, pontos);

            if (Selecionado)
            {
                System.Drawing.Pen canetaSelecao = new System.Drawing.Pen(System.Drawing.Color.Black, 1);
                canetaSelecao.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                graph.DrawPolygon(canetaSelecao, selecao);
            }

            Caixa.Desenhar(graph);
        }

        public void Editar(Object[] ListaDestino, String NomeFluxo)
        {
            FormFiguraCondicao f = new FormFiguraCondicao(this, ListaDestino, NomeFluxo);
            f.ShowDialog();
        }

        public FiguraCondicao Copiar()
        {
            CaixaTexto caixaTextoTemp = new CaixaTexto();
            caixaTextoTemp.CopiarCaixaTexto(this.Caixa, caixaTextoTemp);

            FiguraCondicao objDestino = new FiguraCondicao(caixaTextoTemp, Graphic);

            objDestino.Nome = this.Nome;
            objDestino.DestinoVerdadeiro = this.DestinoVerdadeiro;
            objDestino.DestinoFalso = this.DestinoFalso;
            objDestino.Expressao = this.Expressao;
            objDestino.Destino = this.Destino;

            return objDestino;
        }

    }
}
