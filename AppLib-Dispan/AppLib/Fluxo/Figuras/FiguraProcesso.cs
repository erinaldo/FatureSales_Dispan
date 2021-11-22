using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class FiguraProcesso
    {
        public String Nome { get; set; }
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

        public FiguraProcesso(CaixaTexto Caixa1, System.Drawing.Graphics graph)
        {
            Caixa = Caixa1;

            Carregar();

            Graphic = graph;
            this.Desenhar(Graphic);
        }

        private int pontoAX;
        private int pontoAY;
        private int pontoBX;
        private int pontoBY;
        private int pontoCX;
        private int pontoCY;
        private int pontoDX;
        private int pontoDY;

        private System.Drawing.Point a;
        private System.Drawing.Point b;
        private System.Drawing.Point c;
        private System.Drawing.Point d;

        private System.Drawing.Point[] pontos = new System.Drawing.Point[4];

        private void Calcular()
        {
            Carregar();

            int divisor1 = 2;
            int divisor2 = 20;

            pontoAX = PontoX - (Largura / divisor1) - (Largura / divisor2);
            pontoAY = PontoY - (Altura / divisor1);
            pontoBX = PontoX + (Largura / divisor1) + (Largura / divisor2);
            pontoBY = PontoY - (Altura / divisor1);
            pontoCX = PontoX + (Largura / divisor1) + (Largura / divisor2);
            pontoCY = PontoY + (Altura / divisor1) + 1;
            pontoDX = PontoX - (Largura / divisor1) - (Largura / divisor2);
            pontoDY = PontoY + (Altura / divisor1) + 1;

            a = new System.Drawing.Point(pontoAX, pontoAY);
            b = new System.Drawing.Point(pontoBX, pontoBY);
            c = new System.Drawing.Point(pontoCX, pontoCY);
            d = new System.Drawing.Point(pontoDX, pontoDY);

            pontos[0] = a;
            pontos[1] = b;
            pontos[2] = c;
            pontos[3] = d;

            int margemSelecao = 10;

            selecaoAX = pontoAX - margemSelecao;
            selecaoAY = pontoAY - margemSelecao;
            selecaoBX = pontoBX + margemSelecao;
            selecaoBY = pontoBY - margemSelecao;
            selecaoCX = pontoCX + margemSelecao;
            selecaoCY = pontoCY + margemSelecao;
            selecaoDX = pontoDX - margemSelecao;
            selecaoDY = pontoDY + margemSelecao;

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
            FormFiguraProcesso f = new FormFiguraProcesso(this, ListaDestino, NomeFluxo);
            f.ShowDialog();
        }

        public FiguraProcesso Copiar()
        {
            CaixaTexto caixaTextoTemp = new CaixaTexto();
            caixaTextoTemp.CopiarCaixaTexto(this.Caixa, caixaTextoTemp);

            FiguraProcesso objDestino = new FiguraProcesso(caixaTextoTemp, Graphic);

            objDestino.Nome = this.Nome;
            objDestino.Destino = this.Destino;
            objDestino.Expressao = this.Expressao;

            return objDestino;
        }

    }
}
