using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;

namespace AppFatureClient
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBoxUSUARIO.Text = string.Empty;
            textBoxSENHA.Text = string.Empty;
            textBoxUSUARIO.Focus();
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


        private void textBoxUSUARIO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxSENHA.Focus();
            }
        }

        private void textBoxSENHA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pictureBoxOK_Click(this, null);
            }
        }

        private void pictureBoxSAIR_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxCONFIG_Click(object sender, EventArgs e)
        {
            //new FormLicenca().ShowDialog();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.itinit.com.br");
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.itinit.com.br/atendimento.html");
        }

        public Boolean Validar()
        {
            // implementar
            return false;
        }

        public void Preparar()
        {
            // implementar
        }

        public Boolean VerValidadeContrato()
        {
            return true;
            /*
            Properties.Settings sett = new Properties.Settings();

            if (sett.Contrato.Equals(""))
            {
                new FormLicenca().ShowDialog();
            }

            List<Contrato> contratos = new List<Contrato>();
            AppLib.Util.ObjetoXML oxml = new AppLib.Util.ObjetoXML();
            contratos = (List<Contrato>)oxml.Ler(sett.Contrato, contratos);

            String chave = "merbibuziberbibonzi";

            // ENCONTRAR O PRODUTO
            for (int i = 0; i < contratos.Count; i++)
            {
                contratos[i].IDPRODUTO = new AppLib.Util.Criptografia().Decoder(AppLib.Util.Criptografia.OpcoesEncoder.TripleDES, contratos[i].IDPRODUTO, chave);
                contratos[i].VALIDADE = new AppLib.Util.Criptografia().Decoder(AppLib.Util.Criptografia.OpcoesEncoder.TripleDES, contratos[i].VALIDADE, chave);

                if (contratos[i].IDPRODUTO.Equals("1"))
                {
                    DateTime Validade = DateTime.Parse(contratos[i].VALIDADE);

                    if (Validade >= DateTime.Now)
                    {
                        return true;
                    }
                }
            }

            return false;
            */
        }

        private void pictureBoxOK_Click(object sender, EventArgs e)
        {
            if (this.VerValidadeContrato())
            {
                if (string.IsNullOrEmpty(textBoxUSUARIO.Text))
                {
                    MessageBox.Show("Informe o usuário.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(textBoxSENHA.Text))
                {
                    MessageBox.Show("Informe a senha.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AppInterop.Message msg = new AppInterop.Message();

                String endereco = string.Empty;
                if (comboBox1.SelectedIndex == 0)
                {
                    FatureContexto.ServiceClient = new AppInterop.AppInteropServer();
                    FatureContexto.Remoto = false;
                    AppLib.Util.Alias alias = new AppInterop.Util().GetAlias(New.Class.EnviromentHelper.IndexAlias);
                    AppLib.Context.poolConnection.Add(
                        "Start",
                        AppLib.Global.Types.Database.SqlClient,
                        new AppLib.Data.AssistantConnection().SqlClient(alias.ServerName, alias.DbName, alias.UserName, alias.Password));
                }
                else
                {
                    FatureContexto.Remoto = true;
                    endereco = Properties.Settings.Default.Remoto + "?WSDL";
                    //FatureContexto.ServiceSoapClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(Properties.Settings.Default.Remoto);

                    AppLib.Context.poolConnection.Add(
                        "Start",
                        AppLib.Global.Types.Database.SqlWebService,
                        new AppLib.Data.AssistantConnection().WebService(textBoxUSUARIO.Text, textBoxSENHA.Text, endereco));

                    FatureContexto.ServiceSoapClient = new WebService().GetWebService(AppLib.Context.poolConnection.Get("Start").ConnectionString);
                }

                FormPrincipal frmPrincipal = new FormPrincipal();
                frmPrincipal.Versao = lblVersao.Text;
                frmPrincipal.ConexaoDefault = false;
                frmPrincipal.ConexaoConn2 = false;

                #region CONEXÃO PRINCIPAL

                try
                {
                    frmPrincipal.ConexaoDefault = AppLib.Context.poolConnection.Get("Start").Test();
                }
                catch
                {
                    frmPrincipal.ConexaoDefault = false;
                }

                #endregion

                #region CONEXÃO BASE LOCAL

                //Verifica se existe o serviço do SQL
                //ServiceController service = new ServiceController("MSSQLSERVER");
                ServiceController service = new ServiceController("MSSQL$SQLEXPRESS");
                //MessageBox.Show(service.Status.ToString());
                try
                {
                    //Verifica se está rodando
                    if (service.Status.ToString().Equals("Running"))
                    {
                        AppLib.Context.poolConnection.Add("Conn3", AppLib.Global.Types.Database.SqlClient, new AppLib.Data.AssistantConnection().SqlClient("localhost\\sqlexpress", "master", "sa", "123456"));
                        // AppLib.Context.poolConnection.Add("Conn3", AppLib.Global.Types.Database.SqlClient, new AppLib.Data.AssistantConnection().SqlClient("localhost", "master", "sa", "123456"));
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
                        msg = new Util().ConvertToMessage(FatureContexto.ServiceSoapClient.Autenticar2(textBoxUSUARIO.Text, textBoxSENHA.Text));
                    }
                    else
                    {
                        msg = FatureContexto.ServiceClient.Autenticar2(textBoxUSUARIO.Text, textBoxSENHA.Text);
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

                        System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Conn2").ExecQuery("SELECT * FROM ZUSUARIO WHERE USUARIO = ?", new Object[] { textBoxUSUARIO.Text });

                        if (dt.Rows.Count > 0)
                        {
                            int STATUS = int.Parse(dt.Rows[0]["ATIVO"].ToString());
                            String SENHA = dt.Rows[0]["SENHA"].ToString();

                            if (STATUS == 1)
                            {
                                if (new RM.Lib.Criptografia.RMSCriptografia().CheckPassword(SENHA, textBoxSENHA.Text))
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
                                msg.Mensagem = "Usuário " + textBoxUSUARIO.Text + " inativo.";
                            }
                        }
                        else
                        {
                            msg.Retorno = false;
                            msg.Mensagem = "Usuário " + textBoxUSUARIO.Text + " não encontrado.";
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
                    AppLib.Context.Usuario = textBoxUSUARIO.Text;
                    AppLib.Context.Senha = textBoxSENHA.Text;
                    AppLib.Context.ControlarAcessos = true;

                    if (frmPrincipal.ConexaoDefault)
                    {
                        AppLib.Context.Usuario = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODUSUARIO FROM GUSUARIO WHERE CODUSUARIO = ?", new Object[] { textBoxUSUARIO.Text }).ToString();
                        AppLib.Context.Perfil = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPERFIL FROM ZUSUARIO WHERE USUARIO = ?", new Object[] { textBoxUSUARIO.Text }).ToString();

                        int Flag = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, "SELECT EXIGEVERSAO FROM ZPARAMFATURE WHERE CODCOLIGADA = ? ", new object[] { AppLib.Context.Empresa }));
                        if (Flag == 1)
                        {
                            string VersaoBanco = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT VERSAOSERVER FROM ZPARAMFATURE WHERE CODCOLIGADA = ?", new Object[] { AppLib.Context.Empresa }).ToString();
                            if (VersaoBanco != lblVersao.Text)
                            {
                                MessageBox.Show("ATENÇÃO !!!.\n\rVersão do Aplicativo [" + lblVersao.Text + "] diferente da versão exigida [" + VersaoBanco + "].\nPara atualizar o aplicativo. clique em OK e execute a atualização.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                System.Diagnostics.Process.Start("AppUpdate.exe");
                                Application.Exit();
                            }
                        }
                    }

                    if (frmPrincipal.ConexaoConn2)
                    {
                        AppLib.Context.Usuario = AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, "SELECT USUARIO FROM ZUSUARIO WHERE USUARIO = ?", new Object[] { textBoxUSUARIO.Text }).ToString();
                        AppLib.Context.Perfil = AppLib.Context.poolConnection.Get("Conn2").ExecGetField(null, "SELECT CODPERFIL FROM ZUSUARIO WHERE USUARIO = ?", new Object[] { textBoxUSUARIO.Text }).ToString();
                    }

                    if (comboBox1.SelectedIndex == 0)
                    {
                        if (AppLib.Context.Perfil.Equals("APPRVD") || AppLib.Context.Perfil.Equals("APPRPR"))
                        {
                            MessageBox.Show("ATENÇÃO !!!.\n\rUsuário [" + AppLib.Context.Usuario + "], perfil [" + AppLib.Context.Perfil + "].\n\rNão é permitido o acesso ao sistema através do alias Local.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    

                    frmPrincipal.ShowDialog();

                    textBoxSENHA.Text = "";
                    this.Visible = true;
                    textBoxSENHA.Focus();
                }
                else
                {
                    MessageBox.Show(msg.Mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #endregion
            }
            else
            {
                this.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.itinit.com.br");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.itinit.com.br/atendimento.html");
        }
    }
}
