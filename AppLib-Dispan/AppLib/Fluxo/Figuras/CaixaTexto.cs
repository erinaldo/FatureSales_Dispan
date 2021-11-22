using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class CaixaTexto
    {
        public int PontoX { get; set; }
        public int PontoY { get; set; }
        public int Largura { get; set; }
        public int Altura { get; set; }

        public System.Drawing.Color CorFundo { get; set; }
        public System.Drawing.Color CorBorda { get; set; }
        public Boolean UsaBorda { get; set; }

        public System.Drawing.Color CorFonte { get; set; }
        public String Fonte { get; set; }
        public int TamanhoFonte { get; set; }
        public String EstiloFonte { get; set; }

        public String AlinhamentoHorizontal { get; set; }
        public String AlinhamentoVertical { get; set; }

        public String Texto { get; set; }

        private int CaixaX { get; set; }
        private int CaixaY { get; set; }

        public enum OpcoesFonte
        {
            Times_New_Roman, Arial, Verdana
        }

        public enum OpcoesEstilo
        {
            Normal, Negrito, Itálico
        }

        public enum OpcoesAlinhamentoHorizontal
        {
            Esquerda, Centralizado, Direita
        }

        public enum OpcoesAlinhamentoVertical
        {
            Superior, Centralizado, Inferior
        }

        public void Calcular()
        {
            CaixaX = PontoX - (Largura / 2);
            CaixaY = PontoY - (Altura / 2);
        }

        public void Desenhar(System.Drawing.Graphics graph)
        {
            this.Calcular();

            System.Drawing.SolidBrush PincelFundo = new System.Drawing.SolidBrush(CorFundo);

            System.Drawing.Pen CanetaBorda;

            if (UsaBorda)
            {
                CanetaBorda = new System.Drawing.Pen(CorBorda);
            }
            else
            {
                CanetaBorda = new System.Drawing.Pen(CorFundo);
            }

            System.Drawing.SolidBrush PincelTexto = new System.Drawing.SolidBrush(CorFonte);

            System.Drawing.Font Letra = null;
            if (EstiloFonte.Equals("Negrito")) Letra = new System.Drawing.Font(Fonte, TamanhoFonte, System.Drawing.FontStyle.Bold);
            else if (EstiloFonte.Equals("Itálico")) Letra = new System.Drawing.Font(Fonte, TamanhoFonte, System.Drawing.FontStyle.Italic);
            else Letra = new System.Drawing.Font(Fonte, TamanhoFonte);

            System.Drawing.StringFormat Formatacao = new System.Drawing.StringFormat();
            if (AlinhamentoHorizontal.Equals("Esquerda")) Formatacao.Alignment = System.Drawing.StringAlignment.Near;
            if (AlinhamentoHorizontal.Equals("Centralizado")) Formatacao.Alignment = System.Drawing.StringAlignment.Center;
            if (AlinhamentoHorizontal.Equals("Direita")) Formatacao.Alignment = System.Drawing.StringAlignment.Far;

            if (AlinhamentoVertical.Equals("Superior")) Formatacao.LineAlignment = System.Drawing.StringAlignment.Near;
            if (AlinhamentoVertical.Equals("Centralizado")) Formatacao.LineAlignment = System.Drawing.StringAlignment.Center;
            if (AlinhamentoVertical.Equals("Inferior")) Formatacao.LineAlignment = System.Drawing.StringAlignment.Far;

            System.Drawing.Rectangle Caixa = new System.Drawing.Rectangle(CaixaX + 1, CaixaY + 1, Largura - 2, Altura - 2);

            graph.FillRectangle(PincelFundo, Caixa);
            graph.DrawRectangle(CanetaBorda, Caixa);
            graph.DrawString(Texto, Letra, PincelTexto, Caixa, Formatacao);
        }

        public CaixaTexto CopiarCaixaTexto(CaixaTexto CaixaBase, CaixaTexto CaixaReceptora)
        {
            CaixaReceptora.PontoX = CaixaBase.PontoX;
            CaixaReceptora.PontoY = CaixaBase.PontoY;
            CaixaReceptora.Largura = CaixaBase.Largura;
            CaixaReceptora.Altura = CaixaBase.Altura;

            CaixaReceptora.CorFundo = CaixaBase.CorFundo;
            CaixaReceptora.CorBorda = CaixaBase.CorBorda;
            CaixaReceptora.UsaBorda = CaixaBase.UsaBorda;

            CaixaReceptora.CorFonte = CaixaBase.CorFonte;
            CaixaReceptora.Fonte = CaixaBase.Fonte;
            CaixaReceptora.TamanhoFonte = CaixaBase.TamanhoFonte;
            CaixaReceptora.EstiloFonte = CaixaBase.EstiloFonte;

            CaixaReceptora.AlinhamentoHorizontal = CaixaBase.AlinhamentoHorizontal;
            CaixaReceptora.AlinhamentoVertical = CaixaBase.AlinhamentoVertical;

            CaixaReceptora.Texto = CaixaBase.Texto;

            return CaixaReceptora;
        }
    }
}
