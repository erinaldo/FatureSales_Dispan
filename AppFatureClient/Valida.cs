using System;
using System.Collections.Generic;
using System.Text;

namespace AppFatureClient
{
    public class Valida
    {
        public bool ValidaAcessoMovimento(int CodColigada, string CodUsuario, string CodTmv, string Valor)
        {
            string sSql = @"SELECT ? FROM TUSRTMV WHERE CODCOLIGADA = ? AND CODUSUARIO = ? AND CODTMV = ? ";
            
            try
            {
                sSql = AppLib.Context.poolConnection.Get("Start").ParseCommand(sSql, new Object[] { Valor, CodColigada, CodUsuario, CodTmv });
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new Object[] { });
                if (dt.Rows.Count > 0)
                {
                    return (int.Parse(dt.Rows[0]["Valor"].ToString()) == 0) ? false : true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                new AppLib.Windows.FormExceptionSQL().Mostrar("Erro ao validar acesso do usuário á movimentos.", sSql, ex);
                return false;
            }
        }

        public Boolean ValidaCNPJCPF(String documento)
        {
            if (string.IsNullOrEmpty(documento))
                return true;

            if (documento.Length == 14)
                return ValidaCPF(documento);

            if (documento.Length == 18)
                return ValidaCNPJ(documento);

            return false;        
        }

        // Método de validação do CPF
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

        // Método de validação do CNPJ
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

        public Boolean ConsisteOffLineCNPJ(String cnpj, string idoff)
        {
            String Comando = @"SELECT COUNT(CODCFO) CODCFO FROM ZFCFO WHERE CGCCFO = ?";

            try
            {
                Comando = AppLib.Context.poolConnection.Get("Conn2").ParseCommand(Comando, new Object[] { cnpj });
                int contador = (int)AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, Comando, new Object[] { });

                if (contador <= 0)
                {
                    Comando = @"SELECT COUNT(IDOFF) IDOFF FROM ZFCFOX WHERE CGCCFO = ? AND IDOFF <> ?";
                    Comando = AppLib.Context.poolConnection.Get("Conn2").ParseCommand(Comando, new Object[] { cnpj, idoff });
                    contador = (int)AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, Comando, new Object[] { });

                    if (contador <= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }                     
        }

        // Método de validação de e-mail
        public Boolean ValidaEmail(String email)
        {
            //// Corrigir o antes de enviar para este método
            //email = email.Replace(" ", "");

            String caracterarroba = "@";
            String ponto = ".";

            int cont = 0;
            int arroba = 0;

            // Ter no mínimo 7 caracteres
            if (email.Length < 7)
            {
                return false;
            }

            // Uma arroba no meio da String
            for (int i = 0; i < (email.Length); i++)
            {
                if (caracterarroba.Equals(email[i].ToString()))
                {
                    cont++;
                    if (arroba == 0)
                    {
                        arroba = i;
                    }
                }
            }
            if (cont != 1)
            {
                return false;
            }
            else
            {
                cont = 0;
            }

            // No mínimo um ponto depois da arroba
            for (int i = arroba; i < (email.Length); i++)
            {
                if (ponto.Equals(email[i].ToString()))
                {
                    cont++;
                }
            }
            if (cont == 0)
            {
                return false;
            }

            // O ultimo caracter não pode ser um ponto
            if (ponto.Equals(email[email.Length - 1].ToString()))
            {
                return false;
            }

            // Se passar pelos testes retornar verdadeiro
            return true;
        }

        public Boolean AcessoSupervisor(String CONEXAO, int CODCOLIGADA, String CODSISTEMA, String CODUSUARIO)
        {
            String Comando = @"
SELECT COUNT(*)
FROM GPERMIS
WHERE CODCOLIGADA = ?
  AND CODSISTEMA = ?
  AND CODUSUARIO = ?
  AND SUPERVISOR = 1";

            try
            {
                Comando = AppLib.Context.poolConnection.Get(CONEXAO).ParseCommand(Comando, new Object[] { CODCOLIGADA, CODSISTEMA, CODUSUARIO });
                int contador = (int)AppLib.Context.poolConnection.Get(CONEXAO).ExecGetField(null, Comando, new Object[] { });

                if (contador == 1)
                {
                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("O seu usuário não possui permissão de supervisor para aprovação de movimentos.", "Erro", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                new AppLib.Windows.FormExceptionSQL().Mostrar("Erro na validação do supervisor.", Comando, ex);
                return false;
            }
        }

        public Boolean AcessoMenu(String CODPERFIL)
        {
            String Comando = @"SELECT COUNT(CODPERFIL) CONTADOR FROM GUSRPERFIL WHERE CODCOLIGADA = 1 AND CODSISTEMA = 'T' AND CODPERFIL = ? AND CODUSUARIO = ?";

            try
            {
                Comando = AppLib.Context.poolConnection.Get("Start").ParseCommand(Comando, new Object[] { CODPERFIL, AppLib.Context.Usuario });
                int contador = (int)AppLib.Context.poolConnection.Get("Start").ExecGetField(null, Comando, new Object[] { });

                if (contador > 0)
                {
                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Acesso não permitido.", "Erro", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                new AppLib.Windows.FormExceptionSQL().Mostrar("Erro na validação do acesso.", Comando, ex);
                return false;
            }
        }

        public Boolean IsRepresentante()
        {
            String Comando = @"SELECT COUNT(CODUSUARIO) CONTADOR FROM ZTRPR WHERE CODCOLIGADA = 1 AND CODUSUARIO = ?";

            try
            {
                Comando = AppLib.Context.poolConnection.Get("Start").ParseCommand(Comando, new Object[] { AppLib.Context.Usuario });
                int contador = (int)AppLib.Context.poolConnection.Get("Start").ExecGetField(null, Comando, new Object[] { });

                if (contador > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
