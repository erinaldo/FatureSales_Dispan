using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class Fluxo
    {
        public PropriedadeFluxo Propriedades = new PropriedadeFluxo();

        public List<Object> Figuras = new List<Object>();

        public List<Object> FigurasTransf = new List<Object>();

        public Fluxo Copiar()
        {
            Fluxo objDestino = new Fluxo();

            objDestino.Propriedades = this.Propriedades.Copiar();

            objDestino.Figuras.Clear();

            for (int i = 0; i < this.Figuras.Count; i++)
            {
                objDestino.Figuras.Add(null);

                if (this.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)this.Figuras[i];
                    objDestino.Figuras[i] = fig.Copiar();
                }

                if (this.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)this.Figuras[i];
                    objDestino.Figuras[i] = fig.Copiar();
                }

                if (this.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)this.Figuras[i];
                    objDestino.Figuras[i] = fig.Copiar();
                }

                if (this.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)this.Figuras[i];
                    objDestino.Figuras[i] = fig.Copiar();
                }

                if (this.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)this.Figuras[i];
                    objDestino.Figuras[i] = fig.Copiar();
                }

                if (this.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)this.Figuras[i];
                    objDestino.Figuras[i] = fig.Copiar();
                }
            }

            return objDestino;
        }

    }
}
