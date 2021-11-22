using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class FiguraFim
    {
        public String Nome { get; set; }
        public String Retorno { get; set; }

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

        public FiguraFim(CaixaTexto Caixa1, System.Drawing.Graphics graph)
        {
            Caixa = Caixa1;

            Carregar();

            Graphic = graph;
            this.Desenhar(Graphic);
        }

        private int quadradoAX;
        private int quadradoAY;
        private int quadradoBX;
        private int quadradoBY;
        private int quadradoCX;
        private int quadradoCY;
        private int quadradoDX;
        private int quadradoDY;

        private System.Drawing.Point a;
        private System.Drawing.Point b;
        private System.Drawing.Point c;
        private System.Drawing.Point d;

        private System.Drawing.Point[] Pontos = new System.Drawing.Point[4];

        private void Calcular()
        {
            Carregar();

            int divisor = 13;
            int multiplicador = 8;

            quadradoAX = PontoX - ((Largura / divisor) * multiplicador);
            quadradoAY = PontoY + ((Altura / divisor) * multiplicador);
            quadradoBX = PontoX + ((Largura / divisor) * multiplicador);
            quadradoBY = PontoY + ((Altura / divisor) * multiplicador);
            quadradoCX = PontoX + ((Largura / divisor) * multiplicador);
            quadradoCY = PontoY - ((Altura / divisor) * multiplicador);
            quadradoDX = PontoX - ((Largura / divisor) * multiplicador);
            quadradoDY = PontoY - ((Altura / divisor) * multiplicador);

            a = new System.Drawing.Point(quadradoAX, quadradoAY);
            b = new System.Drawing.Point(quadradoBX, quadradoBY);
            c = new System.Drawing.Point(quadradoCX, quadradoCY);
            d = new System.Drawing.Point(quadradoDX, quadradoDY);

            Pontos[0] = a;
            Pontos[1] = b;
            Pontos[2] = c;
            Pontos[3] = d;

            int margemSelecao = 10;

            selecaoAX = quadradoAX - margemSelecao;
            selecaoAY = quadradoAY + margemSelecao;
            selecaoBX = quadradoBX + margemSelecao;
            selecaoBY = quadradoBY + margemSelecao;
            selecaoCX = quadradoCX + margemSelecao;
            selecaoCY = quadradoCY - margemSelecao;
            selecaoDX = quadradoDX - margemSelecao;
            selecaoDY = quadradoDY - margemSelecao;

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

            graph.FillPolygon(pincelFundo, Pontos);
            graph.DrawPolygon(canetaBorda, Pontos);

            if (Selecionado)
            {
                System.Drawing.Pen canetaSelecao = new System.Drawing.Pen(System.Drawing.Color.Black, 1);
                canetaSelecao.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                graph.DrawPolygon(canetaSelecao, selecao);
            }

            Caixa.Desenhar(graph);
        }

        public void Editar(PropriedadeFluxo Propriedade)
        {
            FormFiguraFim f = new FormFiguraFim(this, Propriedade);
            f.ShowDialog();
        }

        public FiguraFim Copiar()
        {
            CaixaTexto caixaTextoTemp = new CaixaTexto();
            caixaTextoTemp.CopiarCaixaTexto(this.Caixa, caixaTextoTemp);

            FiguraFim objDestino = new FiguraFim(caixaTextoTemp, Graphic);

            objDestino.Nome = this.Nome;
            objDestino.Retorno = this.Retorno;

            return objDestino;
        }

    }
}
