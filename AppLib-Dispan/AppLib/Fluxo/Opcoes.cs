using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Fluxo
{
    public static class Opcoes
    {
        public static List<String> TipoRetorno()
        {
            List<String> lista = new List<String>();
            lista.Add("Sem Retorno");
            lista.Add("Inteiro");
            lista.Add("Decimal");
            lista.Add("Data e Hora");
            lista.Add("Texto");
            lista.Add("Verdadeiro ou Falso");
            lista.Add("Grid de Visão");
            lista.Add("Tela de Cadastro");
            lista.Add("Componente");
            lista.Add("Objeto");
            lista.Add("Array de Objetos");
            lista.Add("Lista de Objetos");
            lista.Add("Tabela");
            lista.Add("DataSet de Tabelas");
            return lista;
        }

        public static List<String> TipoDado()
        {
            List<String> lista = TipoRetorno();
            lista.RemoveAt(0);
            return lista;
        }

    }
}
