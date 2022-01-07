using System;
using System.ServiceProcess;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormNewLogin : Form
    {
        public FormNewLogin()
        {
            InitializeComponent();
        }

        private void FormNewLogin_Load(object sender, EventArgs e)
        {
            cb_Alias.SelectedIndex = 0;

            SetUser();
            tb_Senha.Text = string.Empty;

            if (!string.IsNullOrEmpty(tb_Usuario.Text))
            {
                tb_Senha.Focus();
                tb_Senha.Select();
            }

            lblVersao.Text = GetVersao();
        }

        public string GetVersao()
        {
            string[] versao = Application.ProductVersion.Split('.');

            DateTime data = new DateTime(2000, 1, 1);
            data = data.AddDays(int.Parse(versao[2]));
            data = data.AddSeconds(int.Parse(versao[3]) * 2);

            string result = string.Format("{0:yyyy.MM.dd}", data);
            result += "." + int.Parse(versao[3]);
            return result;
        }

        private void tb_Usuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tb_Senha.Focus();
            }
        }

        private void tb_Senha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                panel_Login_Click(this, null);
            }
        }

        private void panel_Sair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel_Suporte_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://itinit.com.br/");
        }

        public Boolean VerValidadeContrato()
        {
            return true;
        }

        private void panel_Login_Click(object sender, EventArgs e)
        {
            if (this.VerValidadeContrato())
            {
                if (string.IsNullOrEmpty(tb_Usuario.Text))
                {
                    MessageBox.Show("Informe o usuário.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(tb_Senha.Text))
                {
                    MessageBox.Show("Informe a senha.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AppInterop.Message msg = new AppInterop.Message();

                String endereco = string.Empty;

                FatureContexto.ServiceClient = new AppInterop.AppInteropServer();
                FatureContexto.Remoto = false;
                AppLib.Util.Alias alias = new AppInterop.Util().GetAlias(New.Class.EnviromentHelper.IndexAlias);
                AppLib.Context.poolConnection.Add(
                    "Start",
                    AppLib.Global.Types.Database.SqlClient,
                    new AppLib.Data.AssistantConnection().SqlClient(alias.ServerName, alias.DbName, alias.UserName, alias.Password));

                SaveUser(tb_Usuario.Text);

                FormPrincipal frmPrincipal = new FormPrincipal();
                frmPrincipal.Versao = lblVersao.Text;
                frmPrincipal.ConexaoDefault = false;
                frmPrincipal.ConexaoConn2 = false;

                #region CONEXÃO PRINCIPAL

                try
                {
                    frmPrincipal.ConexaoDefault = AppLib.Context.poolConnection.Get("Start").Test();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    frmPrincipal.ConexaoDefault = false;
                }

                #endregion

                #region CONEXÃO BASE LOCAL

                //Verifica se existe o serviço do SQL
                ServiceController service = new ServiceController("MSSQL$SQLEXPRESS");
                try
                {
                    //Verifica se está rodando
                    if (service.Status.ToString().Equals("Running"))
                    {
                        AppLib.Context.poolConnection.Add("Conn3", AppLib.Global.Types.Database.SqlClient, new AppLib.Data.AssistantConnection().SqlClient("localhost\\sqlexpress", "master", "sa", "123456"));                     
                        //Verifica se existe o Banco
                        string sql = @"SELECT NAME FROM SYS.DATABASES WHERE NAME = ?";
                        if (AppLib.Context.poolConnection.Get("Conn3").ExecHasRows(sql, new object[] { "FaturePalini" }))
                        {
                            //Cria a conexão 
                            if (AppLib.Context.poolConnection.Add("Conn2", AppLib.Global.Types.Database.SqlClient, new AppLib.Data.AssistantConnection().SqlClient("localhost\\sqlexpress", "FaturePalini", "sa", "123456")))
                            {
                                frmPrincipal.ConexaoConn2 = true;
                            }
                            else
                            {
                                frmPrincipal.ConexaoConn2 = false;
                            }
                        }
                        else
                        {
                            //Fim
                            frmPrincipal.ConexaoConn2 = false;
                        }
                    }
                    else
                    {
                        //Fim
                        frmPrincipal.ConexaoConn2 = false;
                    }
                }
                catch (Exception)
                {
                    frmPrincipal.ConexaoConn2 = false;
                }
                #endregion

                #region MENSAGENS DE CONEXÕES

                if (frmPrincipal.ConexaoDefault)
                {
                    if (FatureContexto.Remoto)
                    {
                        msg = new Util().ConvertToMessage(FatureContexto.ServiceSoapClient.Autenticar2(tb_Usuario.Text, tb_Senha.Text));
                    }
                    else
                    {
                        msg = FatureContexto.ServiceClient.Autenticar2(tb_Usuario.Text, tb_Senha.Text);
                    }

                    if (frmPrincipal.ConexaoConn2)
                    {
                        #region ATUALIZA TABELA DE PERFIL

                        System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT
CODPERFIL,
NOME, 
STATUS ATIVO,
RECCREATEDBY USUARIOCRIACAO,
RECCREATEDON DATACRIACAO,
RECMODIFIEDBY USUARIOALTERACAO,
RECMODIFIEDON DATAALTERACAO

FROM GPERFIL

WHERE CODSISTEMA = 'T' AND CODPERFIL LIKE 'APP%'

ORDER BY RECMODIFIEDON", new Object[] { });

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AppLib.ORM.Jit row = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Conn2"), "ZPERFIL");

                            String CODPERFIL = dt.Rows[i]["CODPERFIL"].ToString();

                            row.Set("CODPERFIL", CODPERFIL);
                            row.Set("NOME", dt.Rows[i]["NOME"].ToString());
                            row.Set("ATIVO", int.Parse(dt.Rows[i]["ATIVO"].ToString()));

                            if (!dt.Rows[i]["USUARIOCRIACAO"].ToString().Equals(""))
                            {
                                row.Set("USUARIOCRIACAO", dt.Rows[i]["USUARIOCRIACAO"].ToString());
                            }

                            if (!dt.Rows[i]["DATACRIACAO"].ToString().Equals(""))
                            {
                                row.Set("DATACRIACAO", DateTime.Parse(dt.Rows[i]["DATACRIACAO"].ToString()));
                            }

                            if (!dt.Rows[i]["USUARIOALTERACAO"].ToString().Equals(""))
                            {
                                row.Set("USUARIOALTERACAO", dt.Rows[i]["USUARIOALTERACAO"].ToString());
                            }

                            row.Set("DATAALTERACAO", DateTime.Parse(dt.Rows[i]["DATAALTERACAO"].ToString()));

                            if (row.Save() != 1)
                            {
                                MessageBox.Show("Erro ao atualizar tabela ZPERFIL registro " + CODPERFIL, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        #endregion

                        #region ATUALIZA TABELA DE USUÁRIOS

                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT
CODUSUARIO USUARIO,
SENHA,
(SELECT TOP 1 CODPERFIL FROM GUSRPERFIL WHERE CODUSUARIO = GUSUARIO.CODUSUARIO AND CODSISTEMA = 'T' AND CODPERFIL LIKE 'APP%') CODPERFIL,
STATUS ATIVO,
RECCREATEDBY USUARIOCRIACAO,
RECCREATEDON DATACRIACAO,
RECMODIFIEDBY USUARIOALTERACAO,
RECMODIFIEDON DATAALTERACAO

FROM GUSUARIO
WHERE NOT((SELECT TOP 1 CODPERFIL FROM GUSRPERFIL WHERE CODUSUARIO = GUSUARIO.CODUSUARIO AND CODSISTEMA = 'T' AND CODPERFIL LIKE 'APP%') IS NULL)", new Object[] { });

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AppLib.ORM.Jit row = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Conn2"), "ZUSUARIO");

                            String CODUSUARIO = dt.Rows[i]["USUARIO"].ToString();

                            row.Set("USUARIO", CODUSUARIO);
                            row.Set("SENHA", dt.Rows[i]["SENHA"].ToString());
                            row.Set("CODPERFIL", dt.Rows[i]["CODPERFIL"].ToString());
                            row.Set("ATIVO", int.Parse(dt.Rows[i]["ATIVO"].ToString()));

                            if (!dt.Rows[i]["USUARIOCRIACAO"].ToString().Equals(""))
                            {
                                row.Set("USUARIOCRIACAO", dt.Rows[i]["USUARIOCRIACAO"].ToString());
                            }

                            if (!dt.Rows[i]["DATACRIACAO"].ToString().Equals(""))
                            {
                                row.Set("DATACRIACAO", DateTime.Parse(dt.Rows[i]["DATACRIACAO"].ToString()));
                            }

                            if (!dt.Rows[i]["USUARIOALTERACAO"].ToString().Equals(""))
                            {
                                row.Set("USUARIOALTERACAO", dt.Rows[i]["USUARIOALTERACAO"].ToString());
                            }

                            row.Set("DATAALTERACAO", DateTime.Parse(dt.Rows[i]["DATAALTERACAO"].ToString()));

                            if (row.Save() != 1)
                            {
                                MessageBox.Show("Erro ao atualizar tabela ZUSUARIO registro " + CODUSUARIO, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        if (Properties.Settings.Default.AcessoOFF == "1")
                        {
                            //MessageBox.Show("Não foi possível conectar-se a um banco de dados local (modo off-line). Somente as operações remotas (modo on-line) estarão disponíveis.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    if (!frmPrincipal.ConexaoConn2)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        //MessageBox.Show("Não foi possível conectar-se remotamente (modo on-line). Somente as operações locais (modo off-line) estarão disponíveis.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        #region REALIZA A CONEXAO LOCAL

                        System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Conn2").ExecQuery("SELECT * FROM ZUSUARIO WHERE USUARIO = ?", new Object[] { tb_Usuario.Text });

                        if (dt.Rows.Count > 0)
                        {
                            int STATUS = int.Parse(dt.Rows[0]["ATIVO"].ToString());
                            String SENHA = dt.Rows[0]["SENHA"].ToString();

                            if (STATUS == 1)
                            {
                                if (new RM.Lib.Criptografia.RMSCriptografia().CheckPassword(SENHA, tb_Senha.Text))
                                {
                                    msg.Retorno = true;
                                }
                                else
                                {
                                    msg.Retorno = false;
                                    msg.Mensagem = "Erro de autenticação de usuário.";
                                }
                            }
                            else
                            {
                                msg.Retorno = false;
                                msg.Mensagem = "Usuário " + tb_Usuario.Text + " inativo.";
                            }
                        }
                        else
                        {
                            msg.Retorno = false;
                            msg.Mensagem = "Usuário " + tb_Usuario.Text + " não encontrado.";
                        }

                        #endregion
                    }
                }

                #endregion

                #region RETORNO

                if ((Boolean)msg.Retorno)
                {
                    this.Visible = false;

                    AppLib.Context.Empresa = 1;
                    AppLib.Context.Usuario = tb_Usuario.Text;
                    AppLib.Context.Senha = tb_Senha.Text;
                    AppLib.Context.ControlarAcessos = true;

                    if (frmPrincipal.ConexaoDefault)
                    {
                        AppLib.Context.Usuario = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODUSUARIO FROM GUSUARIO WHERE CODUSUARIO = ?", new Object[] { tb_Usuario.Text }).ToString();
                        AppLib.Context.Perfil = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPERFIL FROM ZUSUARIO WHERE USUARIO = ?", new Object[] { tb_Usuario.Text }).ToString();

                        int Flag = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, "SELECT EXIGEVERSAO FROM ZPARAMFATURE WHERE CODCOLIGADA = ? ", new object[] { AppLib.Context.Empresa }));
                        if (Flag == 1)
                        {
                            string VersaoClient = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT VERSAOCLIENT FROM ZPARAMFATURE WHERE CODCOLIGADA = ?", new Object[] { AppLib.Context.Empresa }).ToString();

                            string VersaoAtual = lblVersao.Text;

                            string sVersaoClient = VersaoClient.Replace(".", "");
                            string sVersaoAtual = VersaoAtual.Replace(".", "");

                            if (sVersaoClient.Length > sVersaoAtual.Length)
                            {
                                sVersaoClient = sVersaoClient.Substring(0, sVersaoAtual.Length);
                            }
                            else if (sVersaoClient.Length < sVersaoAtual.Length)
                            {
                                sVersaoAtual = sVersaoAtual.Substring(0, sVersaoClient.Length);
                            }

                            Int64 iVersaoClient = Convert.ToInt64(sVersaoClient);
                            Int64 iVersaoAtual = Convert.ToInt64(sVersaoAtual);

                            if (iVersaoClient > iVersaoAtual)
                            {
                                if (MessageBox.Show("ATENÇÃO !!!.\n\rVersão do Aplicativo [" + lblVersao.Text + "] diferente da versão exigida [" + VersaoClient + "].", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                                {
                                    System.Diagnostics.Process.Start(".\\Update\\Update.exe");
                                }
                                else
                                {
                                    Application.Exit();
                                }
                            }
                        }
                    }

                    if (frmPrincipal.ConexaoConn2)
                    {
                        AppLib.Context.Usuario = AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, "SELECT USUARIO FROM ZUSUARIO WHERE USUARIO = ?", new Object[] { tb_Usuario.Text }).ToString();
                        AppLib.Context.Perfil = AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, "SELECT CODPERFIL FROM ZUSUARIO WHERE USUARIO = ?", new Object[] { tb_Usuario.Text }).ToString();
                    }

                    if (cb_Alias.SelectedIndex == 0)
                    {
                        if (AppLib.Context.Perfil.Equals("APPRVD") || AppLib.Context.Perfil.Equals("APPRPR"))
                        {
                            MessageBox.Show("ATENÇÃO !!!.\n\rUsuário [" + AppLib.Context.Usuario + "], perfil [" + AppLib.Context.Perfil + "].\n\rNão é permitido o acesso ao sistema através do alias Local.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    AppLib.Util.Alias alias1 = new AppInterop.Util().GetAlias(New.Class.EnviromentHelper.IndexAlias);

                    if (alias1.DbName != "CorporeRM")
                    {
                        if (new AppLib.Security.Access().Processo("Start", "APP8046", AppLib.Context.Perfil))
                        {
                            if (MessageBox.Show("Conexão com base teste realizada com sucesso!\r\nTem certeza que deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                frmPrincipal.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        frmPrincipal.ShowDialog();
                    }

                    tb_Senha.Text = "";
                    this.Visible = true;
                    tb_Senha.Focus();
                }
                else
                {
                    MessageBox.Show(msg.Mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tb_Senha.Text = "";
                }

                #endregion
            }
            else
            {
                this.Close();
            }
        }

        private void SetUser()
        {
            tb_Usuario.Text = Properties.Settings.Default.User;
        }

        private void SaveUser(string _user)
        {
            Properties.Settings.Default.User = _user;
            Properties.Settings.Default.Save();
        }

        private void cb_Alias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Alias.SelectedIndex == 0)
            {
                New.Class.EnviromentHelper.IndexAlias = 0;
                AppInterop.EnviromentHelper.IndexAlias = 0;
            }
            else
            {
                New.Class.EnviromentHelper.IndexAlias = 1;
                AppInterop.EnviromentHelper.IndexAlias = 1;
            }
        }

        private void tb_Usuario_Leave(object sender, EventArgs e)
        {
            int supervisor = Convert.ToInt32(Properties.Settings.Default["Supervisor"]);

            if (supervisor != 1)
            {
                if (cb_Alias.Items.Count > 1)
                {
                    // Trocar validação
                    //cb_Alias.Items.RemoveAt(1);
                    //cb_Alias.SelectedIndex = 0;
                }
            }
        }
    }
}
