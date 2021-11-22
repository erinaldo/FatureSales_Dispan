using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Security
{
    public class Access
    {
        #region TELAS DE VISÃO E CADASTRO

        public String AutomatizaCadastroMenu(String Conexao, String TabelaPrincipal)
        {
            try
            {
                String consultaMenu = "SELECT * FROM ZMENU WHERE TELACADASTRO = ?";
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(consultaMenu, new Object[] { TabelaPrincipal });
                if ( dt.Rows.Count > 0 )
                {
                    return dt.Rows[0]["CODMENU"].ToString();
                }
                else
                {
                    AppLib.ORM.Jit ZMENU = new ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), "ZMENU");
                    ZMENU.Set("CODMENU", TabelaPrincipal);
                    ZMENU.Set("NOME", "Tabela " + TabelaPrincipal);
                    ZMENU.Set("TELACONSULTA", TabelaPrincipal);
                    ZMENU.Set("TELACADASTRO", TabelaPrincipal);
                    ZMENU.Set("ATIVO", 1);
                    DateTime agora = AppLib.Context.poolConnection.Get(Conexao).GetDateTime();
                    ZMENU.Set("DATACRIACAO", agora);
                    ZMENU.Set("USUARIOCRIACAO", "admin");
                    ZMENU.Set("DATAALTERACAO", agora);
                    ZMENU.Set("USUARIOALTERACAO", "admin");

                    if (ZMENU.Save() == 1)
                    {
                        return TabelaPrincipal;
                    }
                    else
                    {
                        throw new Exception("Erro no comanodo save do ORM.");
                    }
                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao inclir menu " + TabelaPrincipal + ".\r\nDetalhe técnico: " + ex.Message);
                return String.Empty;
            }
        }

        public Boolean AutomatizaPermissoesMenu(String Conexao, String CodMenu, String CodPerfil)
        {
            String consultaPermissao = "SELECT * FROM ZMENUPERFIL WHERE CODPERFIL = ? AND CODMENU = ?";
            Boolean temPermissao = AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(consultaPermissao, new Object[] { CodPerfil, CodMenu });

            if (temPermissao)
            {
                return true;
            }
            else
            {
                String consultaAdmin = "SELECT * FROM ZMENUPERFIL WHERE CODPERFIL = ? AND CODMENU = 'MENUPERFIL'";
                Boolean temAdmin = AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(consultaAdmin, new Object[] { CodPerfil });

                if (temAdmin)
                {
                    if (AppLib.Windows.FormMessageDefault.ShowQuestion("Identificamos que você não possui permissão para executar ações nesta tela. Mas também identificamos que seu usuário possui permissões para administrar acessos.\r\nGostaria de conceder agora este acesso ao seu perfil? Vale lembrar que todos os usuários com o mesmo perfil que o seu também estarão recebendo este acesso.") == System.Windows.Forms.DialogResult.Yes)
                    {
                        AppLib.ORM.Jit ZMENUPERFIL = new ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), "ZMENUPERFIL");
                        ZMENUPERFIL.Set("CODMENU", CodMenu);
                        ZMENUPERFIL.Set("CODPERFIL", CodPerfil);
                        ZMENUPERFIL.Set("CONSULTAR", 1);
                        ZMENUPERFIL.Set("CADASTRAR", 1);
                        ZMENUPERFIL.Set("EXCLUIR", 1);
                        ZMENUPERFIL.Set("ATIVO", 1);
                        DateTime agora = AppLib.Context.poolConnection.Get(Conexao).GetDateTime();
                        ZMENUPERFIL.Set("DATACRIACAO", agora);
                        ZMENUPERFIL.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                        ZMENUPERFIL.Set("DATAALTERACAO", agora);
                        ZMENUPERFIL.Set("USUARIOALTERACAO", AppLib.Context.Usuario);

                        if (ZMENUPERFIL.Save() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            AppLib.Windows.FormMessageDefault.ShowError("Erro ao conceder permissão.");
                            return false;
                        }
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
        }

        public Boolean Consultar(String Conexao, String TelaConsulta, String CodPerfil)
        {
            String CodMenu = this.AutomatizaCadastroMenu(Conexao, TelaConsulta);

            if (CodMenu != String.Empty)
            {
                this.AutomatizaPermissoesMenu(Conexao, CodMenu, CodPerfil);
            }

            if (AppLib.Context.ControlarAcessos)
            {
                String Comando = @"
SELECT 1 EXISTE
FROM ZMENUPERFIL, ZMENU
WHERE ZMENUPERFIL.CODMENU = ZMENU.CODMENU
  AND ZMENU.TELACONSULTA = ?
  AND ZMENUPERFIL.CODPERFIL = ?
  AND ZMENUPERFIL.CONSULTAR = 1";

                Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { TelaConsulta, CodPerfil });
                return AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(Comando, new Object[] { });
            }
            else
            {
                return true;
            }
        }

        public Boolean Cadastrar(String Conexao, String TabelaPrincipal, String CodPerfil)
        {
            String CodMenu = this.AutomatizaCadastroMenu(Conexao, TabelaPrincipal);

            if (CodMenu != String.Empty)
            {
                this.AutomatizaPermissoesMenu(Conexao, CodMenu, CodPerfil);
            }

            if (AppLib.Context.ControlarAcessos)
            {
                String Comando = @"
SELECT 1 EXISTE
FROM ZMENUPERFIL, ZMENU
WHERE ZMENUPERFIL.CODMENU = ZMENU.CODMENU
  AND ZMENU.TELACADASTRO = ?
  AND ZMENUPERFIL.CODPERFIL = ?
  AND ZMENUPERFIL.CADASTRAR = 1";

                Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { TabelaPrincipal, CodPerfil });
                return AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(Comando, new Object[] { });
            }
            else
            {
                return true;
            }
        }

        public Boolean Excluir(String Conexao, String TabelaPrincipal, String CodPerfil)
        {
            String CodMenu = this.AutomatizaCadastroMenu(Conexao, TabelaPrincipal);

            if (CodMenu != String.Empty)
            {
                this.AutomatizaPermissoesMenu(Conexao, CodMenu, CodPerfil);
            }

            if (AppLib.Context.ControlarAcessos)
            {
                String Comando = @"
SELECT 1 EXISTE
FROM ZMENUPERFIL, ZMENU
WHERE ZMENUPERFIL.CODMENU = ZMENU.CODMENU
  AND ZMENU.TELACADASTRO = ?
  AND ZMENUPERFIL.CODPERFIL = ?
  AND ZMENUPERFIL.EXCLUIR = 1";

                Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { TabelaPrincipal, CodPerfil });
                return AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(Comando, new Object[] { });
            }
            else
            {
                return true;
            }
        }
        
        #endregion

        #region PROCESSOS

        public String AutomatizaCadastroProcesso(String Conexao, String CodProcesso)
        {
            try
            {
                String consultaMenu = "SELECT * FROM ZPROCESSO WHERE CODPROCESSO = ?";
                if (AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(consultaMenu, new Object[] { CodProcesso }))
                {
                    return CodProcesso;
                }
                else
                {
                    AppLib.ORM.Jit ZPROCESSO = new ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), "ZPROCESSO");
                    ZPROCESSO.Set("CODPROCESSO", CodProcesso);
                    ZPROCESSO.Set("NOME", "Processo " + CodProcesso);
                    ZPROCESSO.Set("ATIVO", 1);
                    DateTime agora = AppLib.Context.poolConnection.Get(Conexao).GetDateTime();
                    ZPROCESSO.Set("DATACRIACAO", agora);
                    ZPROCESSO.Set("USUARIOCRIACAO", "admin");
                    ZPROCESSO.Set("DATAALTERACAO", agora);
                    ZPROCESSO.Set("USUARIOALTERACAO", "admin");

                    if (ZPROCESSO.Save() == 1)
                    {
                        return CodProcesso;
                    }
                    else
                    {
                        throw new Exception("Erro no comanodo save do ORM.");
                    }
                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao inclir processo " + CodProcesso + ".\r\nDetalhe técnico: " + ex.Message);
                return String.Empty;
            }
        }

        public Boolean AutomatizaPermissoesProcesso(String Conexao, String CodProcesso, String CodPerfil)
        {
            String consultaPermissao = "SELECT * FROM ZPROCESSOPERFIL WHERE CODPERFIL = ? AND CODPROCESSO = ?";
            Boolean temPermissao = AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(consultaPermissao, new Object[] { CodPerfil, CodProcesso });

            if (temPermissao)
            {
                return true;
            }
            else
            {
                String consultaAdmin = "SELECT * FROM ZMENUPERFIL WHERE CODPERFIL = ? AND CODMENU = 'PROCESSOPERFIL'";
                Boolean temAdmin = AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(consultaAdmin, new Object[] { CodPerfil });

                if (temAdmin)
                {
                    if (AppLib.Windows.FormMessageDefault.ShowQuestion("Identificamos que você não possui permissão para executar este processo. Mas também identificamos que seu usuário possui permissões para administrar acessos.\r\nGostaria de conceder agora este acesso ao seu perfil? Vale lembrar que todos os usuários com o mesmo perfil que o seu também estarão recebendo este acesso.") == System.Windows.Forms.DialogResult.Yes)
                    {
                        AppLib.ORM.Jit ZPROCESSOPERFIL = new ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), "ZPROCESSOPERFIL");
                        ZPROCESSOPERFIL.Set("CODPROCESSO", CodProcesso);
                        ZPROCESSOPERFIL.Set("CODPERFIL", CodPerfil);
                        ZPROCESSOPERFIL.Set("ATIVO", 1);
                        DateTime agora = AppLib.Context.poolConnection.Get(Conexao).GetDateTime();
                        ZPROCESSOPERFIL.Set("DATACRIACAO", agora);
                        ZPROCESSOPERFIL.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                        ZPROCESSOPERFIL.Set("DATAALTERACAO", agora);
                        ZPROCESSOPERFIL.Set("USUARIOALTERACAO", AppLib.Context.Usuario);

                        if (ZPROCESSOPERFIL.Save() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            AppLib.Windows.FormMessageDefault.ShowError("Erro ao conceder permissão.");
                            return false;
                        }
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
        }

        public Boolean Processo(String Conexao, String CodProcesso, String CodPerfil)
        {
            if (this.AutomatizaCadastroProcesso(Conexao, CodProcesso) != String.Empty)
            {
                this.AutomatizaPermissoesProcesso(Conexao, CodProcesso, CodPerfil);
            }

            if (AppLib.Context.ControlarAcessos)
            {
                String Comando = @"
SELECT 1 EXISTE
FROM ZPROCESSOPERFIL
WHERE CODPROCESSO = ?
  AND CODPERFIL = ?
  AND ATIVO = 1";

                Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { CodProcesso, CodPerfil });
                Boolean result = AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(Comando, new Object[] { });

                if (result == false)
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Seu perfil (" + CodPerfil + ") não possui permissão para executar o processo (" + CodProcesso + ").");
                }

                return result;
            }
            else
            {
                return true;
            }
        }
        
        #endregion

        #region BUSINESS INTELLIGENCE

        public Boolean ReportVisualizar(String Conexao, int IDREPORT, String CodPerfil)
        {
            if (AppLib.Context.ControlarAcessos)
            {
                String Comando = @"
SELECT 1 EXISTE
FROM ZREPORTPERFIL
WHERE IDREPORT = ?
  AND CODPERFIL = ?
  AND ATIVO = 1
  AND VISUALIZAR = 1";

                Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { IDREPORT, CodPerfil });
                Boolean result = AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(Comando, new Object[] { });

                if (result == false)
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Seu perfil (" + CodPerfil + ") não possui permissão para visualizar o report (" + IDREPORT + ").");
                }

                return result;
            }
            else
            {
                return true;
            }
        }

        public Boolean ReportFormatar(String Conexao, int IDREPORT, String CodPerfil)
        {
            if (AppLib.Context.ControlarAcessos)
            {
                String Comando = @"
SELECT 1 EXISTE
FROM ZREPORTPERFIL
WHERE IDREPORT = ?
  AND CODPERFIL = ?
  AND ATIVO = 1
  AND FORMATAR = 1";

                Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { IDREPORT, CodPerfil });
                Boolean result = AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(Comando, new Object[] { });

                if (result == false)
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Seu perfil (" + CodPerfil + ") não possui permissão para formatar o report (" + IDREPORT + ").");
                }

                return result;
            }
            else
            {
                return true;
            }
        }

        public Boolean PlanilhaProcessar(String Conexao, int IDPLANILHA, String CodPerfil)
        {
            if (AppLib.Context.ControlarAcessos)
            {
                String Comando = @"
SELECT 1 EXISTE
FROM ZPLANILHAPERFIL
WHERE IDPLANILHA = ?
  AND CODPERFIL = ?
  AND ATIVO = 1
  AND PROCESSAR = 1";

                Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { IDPLANILHA, CodPerfil });
                Boolean result = AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(Comando, new Object[] { });

                if (result == false)
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Seu perfil (" + CodPerfil + ") não possui permissão para processar a planilha (" + IDPLANILHA + ").");
                }

                return result;
            }
            else
            {
                return true;
            }
        }

        public Boolean DashboardVisualizar(String Conexao, int IDDASHBOARD, String CodPerfil)
        {
            if (AppLib.Context.ControlarAcessos)
            {
                String Comando = @"
SELECT 1 EXISTE
FROM ZDASHBOARDPERFIL
WHERE IDDASHBOARD = ?
  AND CODPERFIL = ?
  AND ATIVO = 1
  AND VISUALIZAR = 1";

                Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { IDDASHBOARD, CodPerfil });
                Boolean result = AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(Comando, new Object[] { });

                if (result == false)
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Seu perfil (" + CodPerfil + ") não possui permissão para visualizar o dashboard (" + IDDASHBOARD + ").");
                }

                return result;
            }
            else
            {
                return true;
            }
        }

        public Boolean DashboardFormatar(String Conexao, int IDDASHBOARD, String CodPerfil)
        {
            if (AppLib.Context.ControlarAcessos)
            {
                String Comando = @"
SELECT 1 EXISTE
FROM ZDASHBOARDPERFIL
WHERE IDDASHBOARD = ?
  AND CODPERFIL = ?
  AND ATIVO = 1
  AND FORMATAR = 1";

                Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { IDDASHBOARD, CodPerfil });
                Boolean result = AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(Comando, new Object[] { });

                if (result == false)
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Seu perfil (" + CodPerfil + ") não possui permissão para formatar o dashboard (" + IDDASHBOARD + ").");
                }

                return result;
            }
            else
            {
                return true;
            }
        }

        public Boolean CuboVisualizar(String Conexao, int IDCUBO, String CodPerfil)
        {
            if (AppLib.Context.ControlarAcessos)
            {
                String Comando = @"
SELECT 1 EXISTE
FROM ZCUBOPERFIL
WHERE IDCUBO = ?
  AND CODPERFIL = ?
  AND ATIVO = 1
  AND VISUALIZAR = 1";

                Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { IDCUBO, CodPerfil });
                Boolean result = AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(Comando, new Object[] { });

                if (result == false)
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Seu perfil (" + CodPerfil + ") não possui permissão para visualizar o cubo (" + IDCUBO + ").");
                }

                return result;
            }
            else
            {
                return true;
            }
        }

        #endregion

    }

}
