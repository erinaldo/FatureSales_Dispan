using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class FluxoExpImp
    {
        public PropriedadeFluxo Propriedades = new PropriedadeFluxo();
        public List<Figura> Figuras = new List<Figura>();

        public void Exportar(String Arquivo, Fluxo fluxo)
        {
            Propriedades = fluxo.Propriedades;

            #region TRANSFERE A LISTA DE FIGURAS DO TIPO OBJETO PARA A CLASSE FIGURA

            for (int i = 0; i < fluxo.Figuras.Count; i++)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    Figura f = new Figura();
                    f.Tipo = "Inicio";
                    f.PontoX = fig.Caixa.PontoX;
                    f.PontoY = fig.Caixa.PontoY;
                    f.Nome = fig.Nome;
                    f.Texto = fig.Caixa.Texto;
                    f.Destino = fig.Destino;
                    Figuras.Add(f);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    Figura f = new Figura();
                    f.Tipo = "Fim";
                    f.PontoX = fig.Caixa.PontoX;
                    f.PontoY = fig.Caixa.PontoY;
                    f.Nome = fig.Nome;
                    f.Texto = fig.Caixa.Texto;
                    f.Retorno = fig.Retorno;
                    Figuras.Add(f);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    Figura f = new Figura();
                    f.Tipo = "Processo";
                    f.PontoX = fig.Caixa.PontoX;
                    f.PontoY = fig.Caixa.PontoY;
                    f.Nome = fig.Nome;
                    f.Texto = fig.Caixa.Texto;
                    f.Destino = fig.Destino;
                    f.Variavel = fig.Expressao.Variavel;
                    f.Atribuicao = fig.Expressao.Atribuicao;
                    f.Expressao = fig.Expressao.Texto;
                    Figuras.Add(f);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    Figura f = new Figura();
                    f.Tipo = "Condicao";
                    f.PontoX = fig.Caixa.PontoX;
                    f.PontoY = fig.Caixa.PontoY;
                    f.Nome = fig.Nome;
                    f.Texto = fig.Caixa.Texto;
                    f.Destino = fig.Destino;
                    f.DestinoVerdadeiro = fig.DestinoVerdadeiro;
                    f.DestinoFalso = fig.DestinoFalso;
                    f.Variavel = fig.Expressao.Variavel;
                    f.Atribuicao = fig.Expressao.Atribuicao;
                    f.Expressao = fig.Expressao.Texto;

                    Figuras.Add(f);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    Figura f = new Figura();
                    f.Tipo = "Transacao";
                    f.PontoX = fig.Caixa.PontoX;
                    f.PontoY = fig.Caixa.PontoY;
                    f.Nome = fig.Nome;
                    f.Texto = fig.Caixa.Texto;
                    f.Destino = fig.Destino;
                    f.DestinoVerdadeiro = fig.DestinoVerdadeiro;
                    f.DestinoFalso = fig.DestinoFalso;

                    Figuras.Add(f);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    Figura f = new Figura();
                    f.Tipo = "Repeticao";
                    f.PontoX = fig.Caixa.PontoX;
                    f.PontoY = fig.Caixa.PontoY;
                    f.Nome = fig.Nome;
                    f.Texto = fig.Caixa.Texto;
                    f.DestinoVerdadeiro = fig.DestinoVerdadeiro;
                    f.DestinoFalso = fig.DestinoFalso;
                    f.Variavel = fig.Expressao.Variavel;
                    f.Atribuicao = fig.Expressao.Atribuicao;
                    f.Expressao = fig.Expressao.Texto;

                    Figuras.Add(f);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    Figura f = new Figura();
                    f.Tipo = "Subprocesso";
                    f.PontoX = fig.Caixa.PontoX;
                    f.PontoY = fig.Caixa.PontoY;
                    f.Nome = fig.Nome;
                    f.Texto = fig.Caixa.Texto;
                    f.Destino = fig.Destino;

                    f.Subprocesso = fig.Subprocesso;
                    f.Parametros = fig.Variaveis;

                    Figuras.Add(f);
                }
            }

            #endregion

            AppLib.Util.ObjetoXML objXML = new AppLib.Util.ObjetoXML();
            String conteudo = objXML.Escrever(this);

            AppLib.Util.Arquivo arqTXT = new AppLib.Util.Arquivo();
            arqTXT.Escrever(Arquivo, conteudo);

            System.Windows.Forms.MessageBox.Show("Exportação conluída");
        }

        public Fluxo Importar(String Arquivo)
        {
            AppLib.Util.Arquivo arqTXT = new AppLib.Util.Arquivo();
            String conteudo = arqTXT.Ler(Arquivo);

            AppLib.Util.ObjetoXML objXML = new AppLib.Util.ObjetoXML();
            FluxoExpImp fei = new FluxoExpImp();
            fei = (FluxoExpImp)objXML.Ler(conteudo, new FluxoExpImp());

            this.Figuras = fei.Figuras;

            #region TRANSFERE DESTA CLASSE PARA O OBJETO FLUXO DO PARAMETRO

            System.Windows.Forms.PictureBox pb = new System.Windows.Forms.PictureBox();
            Principal p = new Principal(pb);

            for (int i = 0; i < this.Figuras.Count; i++)
            {
                Figura f = this.Figuras[i];

                if (f.Tipo.Equals("Inicio"))
                {
                    p.Inicio();
                    p.Inserir(f.PontoX, f.PontoY);
                    FiguraInicio fig = (FiguraInicio)p.fluxo.Figuras[p.fluxo.Figuras.Count - 1];
                    fig.Nome = f.Nome;
                    fig.Caixa.Texto = f.Texto;
                    fig.Destino = f.Destino;
                }

                if (f.Tipo.Equals("Fim"))
                {
                    p.Fim();
                    p.Inserir(f.PontoX, f.PontoY);
                    FiguraFim fig = (FiguraFim)p.fluxo.Figuras[p.fluxo.Figuras.Count - 1];
                    fig.Nome = f.Nome;
                    fig.Caixa.Texto = f.Texto;
                    fig.Retorno = f.Retorno;
                }

                if (f.Tipo.Equals("Processo"))
                {
                    p.Processo();
                    p.Inserir(f.PontoX, f.PontoY);
                    FiguraProcesso fig = (FiguraProcesso)p.fluxo.Figuras[p.fluxo.Figuras.Count - 1];
                    fig.Nome = f.Nome;
                    fig.Caixa.Texto = f.Texto;
                    fig.Destino = f.Destino;
                    fig.Expressao.Variavel = f.Variavel;
                    fig.Expressao.Atribuicao = f.Atribuicao;
                    fig.Expressao.Texto = f.Expressao;
                }

                if (f.Tipo.Equals("Condicao"))
                {
                    p.Condicao();
                    p.Inserir(f.PontoX, f.PontoY);
                    FiguraCondicao fig = (FiguraCondicao)p.fluxo.Figuras[p.fluxo.Figuras.Count - 1];
                    fig.Nome = f.Nome;
                    fig.Caixa.Texto = f.Texto;
                    fig.Destino = f.Destino;
                    fig.DestinoVerdadeiro = f.DestinoVerdadeiro;
                    fig.DestinoFalso = f.DestinoFalso;
                    fig.Expressao.Variavel = f.Variavel;
                    fig.Expressao.Atribuicao = f.Atribuicao;
                    fig.Expressao.Texto = f.Expressao;
                }

                if (f.Tipo.Equals("Transacao"))
                {
                    p.Transacao();
                    p.Inserir(f.PontoX, f.PontoY);
                    FiguraTransacao fig = (FiguraTransacao)p.fluxo.Figuras[p.fluxo.Figuras.Count - 1];
                    fig.Nome = f.Nome;
                    fig.Caixa.Texto = f.Texto;
                    fig.Destino = f.Destino;
                    fig.DestinoVerdadeiro = f.DestinoVerdadeiro;
                    fig.DestinoFalso = f.DestinoFalso;
                }

                if (f.Tipo.Equals("Repeticao"))
                {
                    p.Repeticao();
                    p.Inserir(f.PontoX, f.PontoY);
                    FiguraRepeticao fig = (FiguraRepeticao)p.fluxo.Figuras[p.fluxo.Figuras.Count - 1];
                    fig.Nome = f.Nome;
                    fig.Caixa.Texto = f.Texto;
                    fig.DestinoVerdadeiro = f.DestinoVerdadeiro;
                    fig.DestinoFalso = f.DestinoFalso;
                    fig.Expressao.Variavel = f.Variavel;
                    fig.Expressao.Atribuicao = f.Atribuicao;
                    fig.Expressao.Texto = f.Expressao;
                }

                if (f.Tipo.Equals("Subprocesso"))
                {
                    p.Subprocesso();
                    p.Inserir(f.PontoX, f.PontoY);
                    FiguraSubprocesso fig = (FiguraSubprocesso)p.fluxo.Figuras[p.fluxo.Figuras.Count - 1];
                    fig.Nome = f.Nome;
                    fig.Caixa.Texto = f.Texto;
                    fig.Destino = f.Destino;
                    fig.Subprocesso = f.Subprocesso;
                    fig.Variaveis = f.Parametros;
                }
            }

            #endregion

            p.fluxo.Propriedades = fei.Propriedades;
            return p.fluxo;
        }

    }
}
