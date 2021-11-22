using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormAbrir : Form
    {
        public Principal principal = new Principal(new System.Windows.Forms.PictureBox());

        public FormAbrir()
        {
            InitializeComponent();
        }

        public FormAbrir(String NomeFluxo)
        {
            InitializeComponent();
            comboBoxFLUXO.Text = NomeFluxo;
            buttonOK_Click(this, null);
        }

        private void FormAbrir_Load(object sender, EventArgs e)
        {
            carregaBiblioteca();
        }

        public void carregaBiblioteca()
        {
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery("SELECT DISTINCT BIBLIOTECA FROM ZFLUXO", new object[] { });

            comboBoxBIBLIOTECA.Items.Clear();
            comboBoxBIBLIOTECA.Items.Add("Todos");
            comboBoxBIBLIOTECA.SelectedIndex = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxBIBLIOTECA.Items.Add(dt.Rows[i][0].ToString());
            }

            carregaClasse();
        }

        public void carregaClasse()
        {
            String Biblioteca = "";

            if (comboBoxBIBLIOTECA.Text.Equals("Todos"))
            {
                Biblioteca = "%";
            }
            else
            {
                Biblioteca = comboBoxBIBLIOTECA.Text;
            }

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery("SELECT DISTINCT CLASSE FROM ZFLUXO WHERE BIBLIOTECA LIKE ?", new object[] { Biblioteca });

            comboBoxCLASSE.Items.Clear();
            comboBoxCLASSE.Items.Add("Todos");
            comboBoxCLASSE.SelectedIndex = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxCLASSE.Items.Add(dt.Rows[i][0].ToString());
            }

            carregaFluxo();
        }

        public void carregaFluxo()
        {
            String Biblioteca = "";
            String Classe = "";

            if (comboBoxBIBLIOTECA.Text.Equals("Todos"))
            {
                Biblioteca = "%";
            }
            else
            {
                Biblioteca = comboBoxBIBLIOTECA.Text;
            }

            if (comboBoxCLASSE.Text.Equals("Todos"))
            {
                Classe = "%";
            }
            else
            {
                Classe = comboBoxCLASSE.Text;
            }

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery("SELECT DISTINCT NOME FROM ZFLUXO WHERE BIBLIOTECA LIKE ? AND CLASSE LIKE ?", new object[] { Biblioteca, Classe });

            comboBoxFLUXO.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxFLUXO.Items.Add(dt.Rows[i][0].ToString());
            }

            comboBoxFLUXO.SelectedIndex = 0;

            carregaDescricao();
        }

        public void carregaDescricao()
        {
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery("SELECT DESCRICAO FROM ZFLUXO WHERE NOME = ?", new object[] { comboBoxFLUXO.Text });
            textBoxDESCRICAO.Text = dt.Rows[0][0].ToString();
        }

        private void comboBoxBIBLIOTECA_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregaClasse();
        }

        private void comboBoxCLASSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregaFluxo();
        }

        private void comboBoxFLUXO_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregaDescricao();
        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dtFLUXO = AppLib.Context.poolConnection.Get().ExecQuery("SELECT * FROM ZFLUXO WHERE NOME = ?", new object[]{ comboBoxFLUXO.Text });
            System.Data.DataTable dtVARIAVEL = AppLib.Context.poolConnection.Get().ExecQuery("SELECT * FROM ZFLUXOVARIAVEL WHERE FLUXO = ?", new object[]{ comboBoxFLUXO.Text });
            System.Data.DataTable dtFIGURA = AppLib.Context.poolConnection.Get().ExecQuery("SELECT * FROM ZFLUXOFIGURA WHERE FLUXO = ?", new object[]{ comboBoxFLUXO.Text });

            #region CARREGAR PROPRIEDADES

            principal.fluxo.Propriedades.Biblioteca = dtFLUXO.Rows[0]["BIBLIOTECA"].ToString();
            principal.fluxo.Propriedades.Classe = dtFLUXO.Rows[0]["CLASSE"].ToString();
            principal.fluxo.Propriedades.Nome = dtFLUXO.Rows[0]["NOME"].ToString();
            principal.fluxo.Propriedades.Descricao = dtFLUXO.Rows[0]["DESCRICAO"].ToString();
            principal.fluxo.Propriedades.TipoRetorno = dtFLUXO.Rows[0]["TIPORETORNO"].ToString();
            principal.fluxo.Propriedades.Contador = int.Parse(dtFLUXO.Rows[0]["CONTADOR"].ToString());

            #endregion

            #region CARREGAR VARIAVEIS

            for (int i = 0; i < dtVARIAVEL.Rows.Count; i++)
            {
                VariaveisFluxo v = new VariaveisFluxo();
                v.Variavel = dtVARIAVEL.Rows[i]["VARIAVEL"].ToString();
                v.TipoDado = dtVARIAVEL.Rows[i]["TIPODADO"].ToString();
                v.TipoVariavel = dtVARIAVEL.Rows[i]["TIPOVARIAVEL"].ToString();
                principal.fluxo.Propriedades.Variaveis.Add(v);
            }

            #endregion

            #region CARREGAR FIGURAS

            for (int i = 0; i < dtFIGURA.Rows.Count; i++)
            {
                System.Data.DataRow dr = dtFIGURA.Rows[i];
                String Tipo = dr["TIPOFIGURA"].ToString();
                String Nome = dr["Nome"].ToString();
                int PontoX = int.Parse(dr["X"].ToString());
                int PontoY = int.Parse(dr["Y"].ToString());
                String Texto = dr["TEXTO"].ToString();
                String Destino = dr["DESTINO"].ToString();
                String DestinoVerdadeiro = dr["DESTINOVERDADEIRO"].ToString();
                String DestinoFalso = dr["DESTINOFALSO"].ToString();
                String Variavel = dr["VARIAVEL"].ToString();
                String Atribuicao = dr["ATRIBUICAO"].ToString();
                String Expressao = dr["EXPRESSAO"].ToString();
                String ExpressaoXML = dr["EXPRESSAOXML"].ToString();
                String Retorno = dr["RETORNO"].ToString();
                String Subprocesso = dr["SUBPROCESSO"].ToString();

                if (Tipo.Equals("Inicio"))
                {
                    principal.Inicio();
                    principal.Inserir(PontoX, PontoY);
                    FiguraInicio fig = (FiguraInicio)principal.fluxo.Figuras[principal.fluxo.Figuras.Count - 1];
                    fig.Nome = Nome;
                    fig.Caixa.Texto = Texto;
                    fig.Destino = Destino;
                }

                if (Tipo.Equals("Fim"))
                {
                    principal.Fim();
                    principal.Inserir(PontoX, PontoY);
                    FiguraFim fig = (FiguraFim)principal.fluxo.Figuras[principal.fluxo.Figuras.Count - 1];
                    fig.Nome = Nome;
                    fig.Caixa.Texto = Texto;
                    fig.Retorno = Retorno;
                }

                if (Tipo.Equals("Processo"))
                {
                    principal.Processo();
                    principal.Inserir(PontoX, PontoY);
                    FiguraProcesso fig = (FiguraProcesso)principal.fluxo.Figuras[principal.fluxo.Figuras.Count - 1];
                    fig.Nome = Nome;
                    fig.Caixa.Texto = Texto;
                    fig.Destino = Destino;
                    fig.Expressao.Variavel = Variavel;
                    fig.Expressao.Atribuicao = Atribuicao;
                    fig.Expressao.Texto = Expressao;
                    fig.Expressao.ExpressaoXML = ExpressaoXML;
                }

                if (Tipo.Equals("Condicao"))
                {
                    principal.Condicao();
                    principal.Inserir(PontoX, PontoY);
                    FiguraCondicao fig = (FiguraCondicao)principal.fluxo.Figuras[principal.fluxo.Figuras.Count - 1];
                    fig.Nome = Nome;
                    fig.Caixa.Texto = Texto;
                    fig.Destino = Destino;
                    fig.DestinoVerdadeiro = DestinoVerdadeiro;
                    fig.DestinoFalso = DestinoFalso;
                    fig.Expressao.Variavel = Variavel;
                    fig.Expressao.Atribuicao = Atribuicao;
                    fig.Expressao.Texto = Expressao;
                    fig.Expressao.ExpressaoXML = ExpressaoXML;
                }

                if (Tipo.Equals("Transacao"))
                {
                    principal.Transacao();
                    principal.Inserir(PontoX, PontoY);
                    FiguraTransacao fig = (FiguraTransacao)principal.fluxo.Figuras[principal.fluxo.Figuras.Count - 1];
                    fig.Nome = Nome;
                    fig.Caixa.Texto = Texto;
                    fig.Destino = Destino;
                    fig.DestinoVerdadeiro = DestinoVerdadeiro;
                    fig.DestinoFalso = DestinoFalso;
                }

                if (Tipo.Equals("Repeticao"))
                {
                    principal.Repeticao();
                    principal.Inserir(PontoX, PontoY);
                    FiguraRepeticao fig = (FiguraRepeticao)principal.fluxo.Figuras[principal.fluxo.Figuras.Count - 1];
                    fig.Nome = Nome;
                    fig.Caixa.Texto = Texto;
                    fig.DestinoVerdadeiro = DestinoVerdadeiro;
                    fig.DestinoFalso = DestinoFalso;
                    fig.Expressao.Variavel = Variavel;
                    fig.Expressao.Atribuicao = Atribuicao;
                    fig.Expressao.Texto = Expressao;
                    fig.Expressao.ExpressaoXML = ExpressaoXML;
                }

                if (Tipo.Equals("Subprocesso"))
                {
                    principal.Subprocesso();
                    principal.Inserir(PontoX, PontoY);
                    FiguraSubprocesso fig = (FiguraSubprocesso)principal.fluxo.Figuras[principal.fluxo.Figuras.Count - 1];
                    fig.Nome = Nome;
                    fig.Caixa.Texto = Texto;
                    fig.Destino = Destino;
                    fig.Subprocesso = Subprocesso;

                    #region CARREGA PARAMETROS

                    System.Data.DataTable dtPARAMETRO = AppLib.Context.poolConnection.Get().ExecQuery("SELECT * FROM ZFLUXOSUBFLUXO WHERE FLUXO = ? AND FIGURA = ?", new object[] { comboBoxFLUXO.Text, fig.Nome });

                    for (int j = 0; j < dtPARAMETRO.Rows.Count; j++)
                    {
                        VariaveisSubprocesso var = new VariaveisSubprocesso();
                        var.Parametro = dtPARAMETRO.Rows[j]["NOME"].ToString();
                        var.Valor = dtPARAMETRO.Rows[j]["VALOR"].ToString();

                        fig.Variaveis.Add(var);
                    }

                    #endregion
                }
            }

            #endregion

            buttonCANCELAR_Click(this, null);
        }
    }
}
