using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public partial class FiguraRepeticao
    {
        public String Nome { get; set; }
        public String DestinoVerdadeiro { get; set; }
        public String DestinoFalso { get; set; }

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

        public FiguraRepeticao(CaixaTexto Caixa1, System.Drawing.Graphics graph)
        {
            Caixa = Caixa1;

            Carregar();

            Graphic = graph;
            this.Desenhar(Graphic);
        }

        private int n = 0;
        private int n2 = 0;
        private int n4 = 0;
        private int circuloX = 0;
        private int circuloY = 0;
        private int circuloLargura = 0;
        private int circuloAltura = 0;

        private void Calcular()
        {
            Carregar();

            n = 0;
            if (Altura > Largura) n = Altura;
            else n = Largura;

            n2 = n / 2;
            n4 = n2 / 2;

            circuloX = PontoX - n2 - n4;
            circuloY = PontoY - n2 - n4;
            circuloLargura = n + (n / 2);
            circuloAltura  = n + (n / 2);

            int margemSelecao = 5;

            selecaoAX = PontoX - (circuloLargura / 2) - margemSelecao;
            selecaoAY = PontoY - (circuloAltura / 2) - margemSelecao;
            selecaoBX = PontoX + (circuloLargura / 2) + margemSelecao;
            selecaoBY = selecaoAY;
            selecaoCX = selecaoBX;
            selecaoCY = PontoY + (circuloAltura / 2) + margemSelecao;
            selecaoDX = selecaoAX;
            selecaoDY = PontoY + (circuloAltura / 2) + margemSelecao;

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

            graph.FillEllipse(pincelFundo, circuloX, circuloY, circuloAltura, circuloLargura);
            graph.DrawEllipse(canetaBorda, circuloX, circuloY, circuloAltura, circuloLargura);

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
            FormFiguraRepeticao f = new FormFiguraRepeticao(this, ListaDestino, NomeFluxo);
            f.ShowDialog();
        }

        public FiguraRepeticao Copiar()
        {
            CaixaTexto caixaTextoTemp = new CaixaTexto();
            caixaTextoTemp.CopiarCaixaTexto(this.Caixa, caixaTextoTemp);

            FiguraRepeticao objDestino = new FiguraRepeticao(caixaTextoTemp, Graphic);

            objDestino.Nome = this.Nome;
            objDestino.DestinoVerdadeiro = this.DestinoVerdadeiro;
            objDestino.DestinoFalso = this.DestinoFalso;
            objDestino.Expressao = this.Expressao;

            return objDestino;
        }

    }
}
