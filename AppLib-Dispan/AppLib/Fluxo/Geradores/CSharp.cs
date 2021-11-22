using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class CSharp
    {
        private Fluxo fluxo { get; set; }
        public String CodigoFonte = "";
        private int Tabulacao = 0;

        public CSharp(Fluxo _fluxo)
        {
            fluxo = _fluxo;
        }

        public void BibliotecaExt()
        {
            String Comando = "SELECT NOME FROM ZFLUXOBIBLIOTECA ORDER BY NOME";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(Comando, new Object[] { });

            CodigoFonte = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CodigoFonte += "using " + dt.Rows[i]["NOME"].ToString() + ";";
                CodigoFonte += "\r\n";
            }

            CodigoFonte += "\r\n";
        }

        public void Biblioteca()
        {
            this.BibliotecaExt();

            CodigoFonte += "namespace " + fluxo.Propriedades.Biblioteca;
            CodigoFonte += "\r\n{";

            Tabulacao++;
            Classe();
            Tabulacao--;

            CodigoFonte += "\r\n}";
        }

        public void Classe()
        {
            Tabular();
            CodigoFonte += "public class " + fluxo.Propriedades.Classe;
            Tabular();
            CodigoFonte += "{";

            Tabulacao++;
            Metodo();
            Tabulacao--;

            Tabular();
            CodigoFonte += "}";
        }

        public void Tabular()
        {
            int temp = Tabulacao;

            CodigoFonte += "\r\n";

            while (temp != 0)
            {
                // CodigoFonte += "\t\t\t";
                CodigoFonte += "\t";
                temp--;
            }
        }

        public String ConverterTipo(String TipoAlgoritmo)
        {
            String result = TipoAlgoritmo;

            if (TipoAlgoritmo.Equals("Inteiro")) return "int";

            if (TipoAlgoritmo.Equals("Decimal")) return "decimal";

            if (TipoAlgoritmo.Equals("Data e Hora")) return "DateTime";

            if (TipoAlgoritmo.Equals("Texto")) return "String";

            if (TipoAlgoritmo.Equals("Verdadeiro ou Falso")) return "Boolean";

            if (TipoAlgoritmo.Equals("Grid de Visão")) return "AppLib.Windows.GridData";

            if (TipoAlgoritmo.Equals("Tela de Cadastro")) return "System.Windows.Forms.Control.ControlCollection";

            if (TipoAlgoritmo.Equals("Componente")) return "System.Windows.Forms.Control";

            if (TipoAlgoritmo.Equals("Objeto")) return "System.Object";

            if (TipoAlgoritmo.Equals("Array de Objetos")) return "System.Object[]";

            if (TipoAlgoritmo.Equals("Lista de Objetos")) return "System.Collections.Generic.List<System.Object>";

            if (TipoAlgoritmo.Equals("Tabela")) return "System.Data.DataTable";

            if (TipoAlgoritmo.Equals("DataSet de Tabelas")) return "System.Data.DataTable";

            return result;
        }

        public void Metodo()
        {
            // COMENTÁRIO MÉTODO
            Tabular();
            CodigoFonte += "\t\t/// <summary>";
            Tabular();

            String descricao = fluxo.Propriedades.Descricao;
            descricao = descricao.Replace("\r\n", "\r\n/// ");
            CodigoFonte += "\t\t/// " + descricao;

            Tabular();
            CodigoFonte += "\t\t/// </summary>";

            // COMENTÁRIO PARÂMETROS
            for (int i = 0; i < fluxo.Propriedades.Variaveis.Count; i++ )
            {
                String Variavel = fluxo.Propriedades.Variaveis[i].Variavel;
                String TipoDado = fluxo.Propriedades.Variaveis[i].TipoDado;
                String TipoVariavel = fluxo.Propriedades.Variaveis[i].TipoVariavel;

                if (TipoVariavel.Equals("Parâmetro"))
                {
                    Tabular();
                    CodigoFonte += "\t\t/// <param name=\"" + Variavel + "\">";
                    CodigoFonte += TipoDado;
                    CodigoFonte += "</param>";
                }
            }

            // MÉTODO
            Tabular();
            CodigoFonte += "\t\tpublic static ";

            if (fluxo.Propriedades.TipoRetorno.Equals("") || fluxo.Propriedades.TipoRetorno.Equals("Sem Retorno"))
            {
                CodigoFonte += "void ";
            }
            else
            {
                CodigoFonte += ConverterTipo(fluxo.Propriedades.TipoRetorno) + " ";
            }

            CodigoFonte += fluxo.Propriedades.Nome + "(";

            // PARAMETROS
            String variavel = "";
            String tipoDado = "";
            String tipoVariavel = "";

            int contParametros = 0;

            for (int i = 0; i < fluxo.Propriedades.Variaveis.Count; i++)
            {
                variavel = fluxo.Propriedades.Variaveis[i].Variavel;
                tipoDado = fluxo.Propriedades.Variaveis[i].TipoDado;
                tipoVariavel = fluxo.Propriedades.Variaveis[i].TipoVariavel;

                if (tipoVariavel.Equals("Parâmetro"))
                {
                    CodigoFonte += ConverterTipo(tipoDado) + " " + variavel + ", ";
                    contParametros++;
                }
            }

            if (contParametros > 0)
            {
                CodigoFonte = CodigoFonte.Substring(0, CodigoFonte.Length - 2);
            }

            CodigoFonte += ")";

            Tabular();
            CodigoFonte += "\t\t{";

            Tabulacao++;
            Atributos();
            Tabulacao--;

            Tabular();
            CodigoFonte += "\t\t}";
        }

        public void Atributos()
        {
            String variavel = "";
            String tipoDado = "";
            String tipoVariavel = "";

            for (int i = 0; i < fluxo.Propriedades.Variaveis.Count; i++)
            {
                variavel = fluxo.Propriedades.Variaveis[i].Variavel;
                tipoDado = fluxo.Propriedades.Variaveis[i].TipoDado;
                tipoVariavel = fluxo.Propriedades.Variaveis[i].TipoVariavel;

                if (tipoVariavel.Equals("Variável"))
                {
                    Tabular();
                    CodigoFonte += ConverterTipo(tipoDado) + " " + variavel + ";";
                }
            }

            Tabular();
            BuscarInicio();

        }

        public void BuscarInicio()
        {
            for (int i = 0; i < fluxo.Figuras.Count; i++)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];

                    if (!(fig.Destino.Equals("")))
                    {
                        BuscarDestino(fig.Destino);
                    }
                    
                    i = fluxo.Figuras.Count;
                }
            }
        }

        public void BuscarDestino(String FiguraDestino)
        {
            for (int i = 0; i < fluxo.Figuras.Count; i++)
            {
                if (i < fluxo.Figuras.Count)
                {
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                    {
                        FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                        if (fig.Nome.Equals(FiguraDestino))
                        {
                            Tabular();
                            CodigoFonte += fig.Expressao.TextoCompleto() + ";";
                            BuscarDestino(fig.Destino);
                            i = fluxo.Figuras.Count;
                        }
                    }
                }

                if (i < fluxo.Figuras.Count)
                {
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                    {
                        FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                        if (fig.Nome.Equals(FiguraDestino))
                        {
                            Tabular();
                            CodigoFonte += "if (" + fig.Expressao.TextoCompleto() + ")";
                            Tabular();
                            CodigoFonte += "{";

                            Tabulacao++;
                            BuscarDestino(fig.DestinoVerdadeiro);
                            Tabulacao--;

                            Tabular();
                            CodigoFonte += "}";

                            Tabular();
                            CodigoFonte += "else";

                            Tabular();
                            CodigoFonte += "{";

                            Tabulacao++;
                            BuscarDestino(fig.DestinoFalso);
                            Tabulacao--;

                            Tabular();
                            CodigoFonte += "}";

                            Tabular();
                            BuscarDestino(fig.Destino);

                            i = fluxo.Figuras.Count;
                        }
                    }
                }


                if (i < fluxo.Figuras.Count)
                {
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                    {
                        FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                        if (fig.Nome.Equals(FiguraDestino))
                        {
                            Tabular();
                            CodigoFonte += "AppLib.Context.poolConnection.Get().BeginTransaction();";
                            Tabular();
                            CodigoFonte += "try";
                            Tabular();
                            CodigoFonte += "{";

                            Tabulacao++;
                            BuscarDestino(fig.DestinoVerdadeiro);
                            Tabular();
                            CodigoFonte += "AppLib.Context.poolConnection.Get().Commit();";
                            Tabulacao--;

                            Tabular();
                            CodigoFonte += "}";
                            Tabular();
                            CodigoFonte += "catch (Exception exception1)";
                            Tabular();
                            CodigoFonte += "{";

                            Tabulacao++;
                            Tabular();
                            CodigoFonte += "AppLib.Context.poolConnection.Get().Rollback();";
                            BuscarDestino(fig.DestinoFalso);
                            Tabulacao--;

                            Tabular();
                            CodigoFonte += "}";

                            Tabular();
                            BuscarDestino(fig.Destino);

                            i = fluxo.Figuras.Count;
                        }
                    }
                }

                if (i < fluxo.Figuras.Count)
                {
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                    {
                        FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                        if (fig.Nome.Equals(FiguraDestino))
                        {
                            Tabular();
                            CodigoFonte += "while (" + fig.Expressao.TextoCompleto() + ")";
                            Tabular();
                            CodigoFonte += "{";

                            Tabulacao++;
                            BuscarDestino(fig.DestinoVerdadeiro);
                            Tabulacao--;

                            Tabular();
                            CodigoFonte += "}";

                            BuscarDestino(fig.DestinoFalso);
                            i = fluxo.Figuras.Count;
                        }
                    }
                }

                if (i < fluxo.Figuras.Count)
                {
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                    {
                        FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                        if (fig.Nome.Equals(FiguraDestino))
                        {
                            Tabular();
                            CodigoFonte += fig.Subprocesso + "(";

                            for (int j = 0; j < fig.Variaveis.Count; j++)
                            {
                                CodigoFonte += fig.Variaveis[j].Valor + ", ";
                            }

                            if (fig.Variaveis.Count > 0)
                            {
                                CodigoFonte = CodigoFonte.Substring(0, CodigoFonte.Length - 2);
                            }

                            CodigoFonte += ");";

                            BuscarDestino(fig.Destino);
                            i = fluxo.Figuras.Count;
                        }
                    }
                }

                if (i < fluxo.Figuras.Count)
                {
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                    {
                        FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                        if (fig.Nome.Equals(FiguraDestino))
                        {
                            if (!(fig.Retorno.Equals("")))
                            {
                                Tabular();
                                CodigoFonte += "return " + fig.Retorno + ";";
                            }

                            i = fluxo.Figuras.Count;
                        }
                    }
                }

            }
        }

    }
}
