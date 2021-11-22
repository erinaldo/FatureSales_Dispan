using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Data
{
    public class Variaveis
    {
        public String Conexao { get; set; }

        public Variaveis(String _Conexao)
        {
            Conexao = _Conexao;
        }

        public String getOntem()
        {
            int dia = DateTime.Now.Day;
            int mes = DateTime.Now.Month;
            int ano = DateTime.Now.Year;
            DateTime result = new DateTime(ano, mes, dia);

            result = result.AddDays(-1);

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.SQL)
            {
                return AppLib.Util.Format.dataSql(result);
            }

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.ORACLE)
            {
                return AppLib.Util.Format.dataOracle(result);
            }

            return "";
        }

        public String getHoje()
        {
            int dia = DateTime.Now.Day;
            int mes = DateTime.Now.Month;
            int ano = DateTime.Now.Year;
            DateTime result = new DateTime(ano, mes, dia);

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.SQL)
            {
                return AppLib.Util.Format.dataSql(result);
            }

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.ORACLE)
            {
                return AppLib.Util.Format.dataOracle(result);
            }

            return "";
        }

        public String getAmanha()
        {
            int dia = DateTime.Now.Day;
            int mes = DateTime.Now.Month;
            int ano = DateTime.Now.Year;
            DateTime result = new DateTime(ano, mes, dia);

            result = result.AddDays(1);

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.SQL)
            {
                return AppLib.Util.Format.dataSql(result);
            }

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.ORACLE)
            {
                return AppLib.Util.Format.dataOracle(result);
            }

            return "";
        }

        public String getMes()
        {
            int mes = DateTime.Now.Month;
            int ano = DateTime.Now.Year;
            DateTime result = new DateTime(ano, mes, 1);

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.SQL)
            {
                return AppLib.Util.Format.dataSql(result);
            }

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.ORACLE)
            {
                return AppLib.Util.Format.dataOracle(result);
            }

            return "";
        }

        public String getMesByUser(int mes)
        {
            int mesAtual = DateTime.Now.Month;
            int ano = DateTime.Now.Year;
            int mesSelecionado = mesAtual - mes;

            DateTime result = new DateTime(ano, mesSelecionado, 1);

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.SQL)
            {
                return AppLib.Util.Format.dataSql(result);
            }

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.ORACLE)
            {
                return AppLib.Util.Format.dataOracle(result);
            }

            return "";
        }

        public String getAno()
        {
            int ano = DateTime.Now.Year;
            DateTime result = new DateTime(ano, 1, 1);

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.SQL)
            {
                return AppLib.Util.Format.dataSql(result);
            }

            if (AppLib.Util.Classificacao.getSGBD(Conexao) == AppLib.Util.SGBD.ORACLE)
            {
                return AppLib.Util.Format.dataOracle(result);
            }

            return "";
        }

        public String getUsuario()
        {
            return AppLib.Context.Usuario;
        }

        public int getEmpresa()
        {
            return AppLib.Context.Empresa;
        }
    }
}
