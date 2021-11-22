using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class FluxoUtil
    {
        public Object[] ListaDestino(Fluxo fluxo)
        {
            int x = 0;
            Object[] lista = new Object[fluxo.Figuras.Count];

            for (int i = 0; i < fluxo.Figuras.Count; i++)
            {

                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    lista[x++] = fig.Nome;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    lista[x++] = fig.Nome;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    lista[x++] = fig.Nome;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    lista[x++] = fig.Nome;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    lista[x++] = fig.Nome;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    lista[x++] = fig.Nome;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    lista[x++] = fig.Nome;
                }

            }

            return lista;
        }
    }
}
