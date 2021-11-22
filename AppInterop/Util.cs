using System;
using System.Collections.Generic;
using System.Web;

namespace AppInterop
{
    public class Util
    {
        public AppLib.Util.Alias GetAlias(int? indexAlias = null)
        {
            try
            {              
                AppLib.Util.Arquivo arq = new AppLib.Util.Arquivo();
                AppLib.Util.ObjetoXML xml = new AppLib.Util.ObjetoXML();
                AppLib.Util.AliasList alias = new AppLib.Util.AliasList();

                string dir = "";

                if (indexAlias != null)
                {
                    if (indexAlias == 0)
                    {
                        dir = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Alias_Producao.xml");
                    }
                    else
                    {
                        dir = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Alias_Teste.xml");
                    }
                }
                else
                {
                    dir = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Alias_Producao.xml");
                }
      
                if (System.IO.File.Exists(dir))
                {
                    alias = (AppLib.Util.AliasList)xml.Ler(arq.Ler(dir), new AppLib.Util.AliasList());

                    RM.Lib.Criptografia.RMSCriptografia crip = new RM.Lib.Criptografia.RMSCriptografia();

                    alias.AliasGroup[0].Password = crip.Decrypt(alias.AliasGroup[0].Password, "BucPelLis");

                    if (!string.IsNullOrEmpty(alias.AliasGroup[0].DbType))
                    {
                        if (alias.AliasGroup[0].DbType == "SQL")
                        {
                            AppLib.Context.poolConnection.Add(
                                "Start",
                                AppLib.Global.Types.Database.SqlClient,
                                new AppLib.Data.AssistantConnection().SqlClient(alias.AliasGroup[0].ServerName, alias.AliasGroup[0].DbName,
                                    alias.AliasGroup[0].UserName, alias.AliasGroup[0].Password));
                        }
                        else
                        {
                            throw new Exception("O Fature permite apenas conexões do tipo [DbType=SQL], favor entrar em contato com o administrador.");                        
                        }
                    }
                    else
                    {
                        throw new Exception("A propriedade DbType do Alias é obrigatória, favor entrar em contato com o administrador.");
                    }

                    return alias.AliasGroup[0];
                }
                else
                {
                    throw new Exception("Arquivo Alias.xml não encontrado no diretório [" + AppDomain.CurrentDomain.BaseDirectory +"], favor entrar em contato com o administrador.");
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return null;
            }
        }

        public Boolean ValidaCNPJCPF(String documento)
        {
            if (documento.Length == 14)
                return ValidaCPF(documento);

            if (documento.Length == 18)
                return ValidaCNPJ(documento);

            return false;
        }

        public Boolean ValidaCPF(String cpf)
        {
            if (cpf.Equals("___.___.___-__")) { return true; }
            if (cpf.Equals("000.000.000-00")) { return true; }
            if (cpf.Equals("111.111.111-11")) { return true; }
            if (cpf.Equals("222.222.222-22")) { return true; }
            if (cpf.Equals("333.333.333-33")) { return true; }
            if (cpf.Equals("444.444.444-44")) { return true; }
            if (cpf.Equals("555.555.555-55")) { return true; }
            if (cpf.Equals("666.666.666-66")) { return true; }
            if (cpf.Equals("777.777.777-77")) { return true; }
            if (cpf.Equals("888.888.888-88")) { return true; }
            if (cpf.Equals("999.999.999-99")) { return true; }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11) return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++) soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito = resto.ToString();

            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++) soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public Boolean ValidaCNPJ(String cnpj)
        {
            if (cnpj.Equals("__.___.___/____-__")) { return true; }
            if (cnpj.Equals("00.000.000/0000-00")) { return true; }
            if (cnpj.Equals("11.111.111/1111-11")) { return true; }
            if (cnpj.Equals("22.222.222/2222-22")) { return true; }
            if (cnpj.Equals("33.333.333/3333-33")) { return true; }
            if (cnpj.Equals("44.444.444/4444-44")) { return true; }
            if (cnpj.Equals("55.555.555/5555-55")) { return true; }
            if (cnpj.Equals("66.666.666/6666-66")) { return true; }
            if (cnpj.Equals("77.777.777/7777-77")) { return true; }
            if (cnpj.Equals("88.888.888/8888-88")) { return true; }
            if (cnpj.Equals("99.999.999/9999-99")) { return true; }

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14) return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++) soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++) soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public decimal Arredonda(decimal? valor)
        {
            if (valor == null)
                return 0;
            else
                return Decimal.Round((decimal)valor, (int)2, MidpointRounding.AwayFromZero);
        }

        public class Log
        {
            public static void Escrever(String texto)
            {
                Console.WriteLine(texto);

                String arquivoLog = "c:\\ITINIT\\Log.txt";

                if (!System.IO.File.Exists(arquivoLog))
                {
                    System.IO.File.WriteAllText(arquivoLog, "");
                }

                System.IO.File.AppendAllText(arquivoLog, DateTime.Now.ToString() + ": " + texto + "\r\n");
            }
        }
    }
}