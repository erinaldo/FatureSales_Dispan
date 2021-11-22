using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class Editor : Form
    {
        #region START

        public Expressao Expressao { get; set; }
        public String NomeFluxo { get; set; }

        public Editor(Expressao _Expressao, String _NomeFluxo)
        {
            InitializeComponent();

            Expressao = _Expressao;
            NomeFluxo = _NomeFluxo;

            CarregarVariaveis();
            CarregarBibliotecas();

            comboBoxVariavelRecebe.Text = _Expressao.Variavel;
            comboBoxAtribuicao.Text = _Expressao.Atribuicao;
            richTextBox1.Text = _Expressao.Texto;
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font("Consolas", 10, FontStyle.Regular);
        }

        #endregion

        #region METODOS

        public void Salvar()
        {
            Expressao.Variavel = comboBoxVariavelRecebe.Text;
            Expressao.Atribuicao = comboBoxAtribuicao.Text;
            Expressao.Texto = richTextBox1.Text;

            Cancelar();
        }

        public void Cancelar()
        {
            this.Close();
        }

        public void CarregarVariaveis()
        {
            String Comando = @"SELECT VARIAVEL FROM ZFLUXOVARIAVEL WHERE FLUXO = ? ORDER BY 1";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(Comando, new object[] { NomeFluxo });

            comboBoxVariavelRecebe.Items.Clear();
            comboBoxVariavel.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxVariavelRecebe.Items.Add(dt.Rows[i]["VARIAVEL"].ToString());
                comboBoxVariavel.Items.Add(dt.Rows[i]["VARIAVEL"].ToString());
            }
        }

        public void CarregarBibliotecas()
        {
            String Comando = @"SELECT DISTINCT BIBLIOTECA FROM ZFLUXO ORDER BY BIBLIOTECA";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(Comando, new object[] { });

            comboBoxBiblioteca.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxBiblioteca.Items.Add(dt.Rows[i][0].ToString());
            }

            CarregarClasses();
        }

        public void CarregarClasses()
        {
            String Comando = @"SELECT DISTINCT CLASSE FROM ZFLUXO WHERE BIBLIOTECA = ? ORDER BY CLASSE";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(Comando, new object[] { comboBoxBiblioteca.Text });

            comboBoxClasse.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxClasse.Items.Add(dt.Rows[i][0].ToString());
            }

            CarregarFuncoes();
        }

        public void CarregarFuncoes()
        {
            String Comando = @"SELECT NOME FROM ZFLUXO WHERE BIBLIOTECA = ? AND CLASSE = ? ORDER BY NOME";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(Comando, new object[] { comboBoxBiblioteca.Text, comboBoxClasse.Text });

            comboBoxFuncao.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxFuncao.Items.Add(dt.Rows[i]["NOME"].ToString());
            }
        }

        public void CarregarTextoFuncao()
        {
            String Comando = @"SELECT TOP 1 DESCRICAO FROM ZFLUXO WHERE NOME = ?";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(Comando, new object[] { comboBoxFuncao.Text });

            if (dt.Rows.Count > 0)
            {
                textBoxTexto.Text = dt.Rows[0]["DESCRICAO"].ToString();
            }
            else
            {
                textBoxTexto.Text = "";
            }
        }

        public void Adicionar(String Texto)
        {
            this.Adicionar(Texto, "");
        }

        public void Adicionar(String Texto1, String Texto2)
        {
            int inicio = richTextBox1.SelectionStart;
            int tamanho = richTextBox1.SelectionLength;

            int tamanhoTexto1 = Texto1.Length;
            int tamanhoTexto2 = Texto2.Length;

            String Antes = richTextBox1.Text.Substring(0, inicio);
            String Selecao = richTextBox1.Text.Substring(inicio, tamanho);
            String Depois = richTextBox1.Text.Substring((inicio + tamanho));

            String Novo = Antes + Texto1 + Selecao + Texto2 + Depois;

            richTextBox1.Text = Novo;
            richTextBox1.SelectionStart = inicio + tamanhoTexto1 + tamanho;
            richTextBox1.Focus();
        }

        #endregion

        #region EVENTOS

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void buttonLimparVariavelRecebe_Click(object sender, EventArgs e)
        {
            comboBoxVariavelRecebe.Text = "";
        }

        private void buttonLimparAtribuicao_Click(object sender, EventArgs e)
        {
            comboBoxAtribuicao.Text = "";
        }

        private void comboBoxBiblioteca_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarClasses();
        }

        private void comboBoxClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarFuncoes();
        }

        private void comboBoxFuncao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarTextoFuncao();
        }

        private void buttonIncluirVariavel_Click(object sender, EventArgs e)
        {
            Adicionar(comboBoxVariavel.Text);
        }

        private void buttonIncluirFuncao_Click(object sender, EventArgs e)
        {
            Adicionar(comboBoxFuncao.Text + "(", ")");
        }

        #endregion

        #region EVENTOS ABA EDITOR

        private void buttonMenorIgual_Click(object sender, EventArgs e)
        {
            Adicionar("<=");
        }

        private void buttonMaiorIgual_Click(object sender, EventArgs e)
        {
            Adicionar(">=");
        }

        private void buttonMenor_Click(object sender, EventArgs e)
        {
            Adicionar("<");
        }

        private void buttonMaior_Click(object sender, EventArgs e)
        {
            Adicionar(">");
        }

        private void buttonIgualIgual_Click(object sender, EventArgs e)
        {
            Adicionar("==");
        }

        private void buttonDiferente_Click(object sender, EventArgs e)
        {
            Adicionar("<>");
        }

        private void buttonE_Click(object sender, EventArgs e)
        {
            Adicionar("&&");
        }

        private void buttonOu_Click(object sender, EventArgs e)
        {
            Adicionar("||");
        }

        private void buttonSete_Click(object sender, EventArgs e)
        {
            Adicionar("7");
        }

        private void buttonOito_Click(object sender, EventArgs e)
        {
            Adicionar("8");
        }

        private void buttonNove_Click(object sender, EventArgs e)
        {
            Adicionar("9");
        }

        private void buttonDividir_Click(object sender, EventArgs e)
        {
            Adicionar("/");
        }

        private void buttonQuatro_Click(object sender, EventArgs e)
        {
            Adicionar("4");
        }

        private void buttonCinco_Click(object sender, EventArgs e)
        {
            Adicionar("5");
        }

        private void buttonSeis_Click(object sender, EventArgs e)
        {
            Adicionar("6");
        }

        private void buttonVezes_Click(object sender, EventArgs e)
        {
            Adicionar("*");
        }

        private void buttonUm_Click(object sender, EventArgs e)
        {
            Adicionar("1");
        }

        private void buttonDois_Click(object sender, EventArgs e)
        {
            Adicionar("2");
        }

        private void buttonTres_Click(object sender, EventArgs e)
        {
            Adicionar("3");
        }

        private void buttonMenos_Click(object sender, EventArgs e)
        {
            Adicionar("-");
        }

        private void buttonZero_Click(object sender, EventArgs e)
        {
            Adicionar("0");
        }

        private void buttonVirgula_Click(object sender, EventArgs e)
        {
            Adicionar(",");
        }

        private void buttonMais_Click(object sender, EventArgs e)
        {
            Adicionar("+");
        }

        private void buttonEntreParenteses_Click(object sender, EventArgs e)
        {
            Adicionar("(", ")");
        }

        private void buttonPonto_Click(object sender, EventArgs e)
        {
            Adicionar(".");
        }

        private void buttonPercentual_Click(object sender, EventArgs e)
        {
            Adicionar("%");
        }

        private void buttonDivididoIgual_Click(object sender, EventArgs e)
        {
            Adicionar("/=");
        }

        private void buttonVezesIgual_Click(object sender, EventArgs e)
        {
            Adicionar("*=");
        }

        private void buttonMenosIgual_Click(object sender, EventArgs e)
        {
            Adicionar("-=");
        }

        private void buttonMenosMenos_Click(object sender, EventArgs e)
        {
            Adicionar("--");
        }

        private void buttonMaisIgual_Click(object sender, EventArgs e)
        {
            Adicionar("+=");
        }

        private void buttonMaisMais_Click(object sender, EventArgs e)
        {
            Adicionar("++");
        }

        private void buttonIgual_Click(object sender, EventArgs e)
        {
            Adicionar("=");
        }

        private void buttonEntreChaves_Click(object sender, EventArgs e)
        {
            Adicionar("[", "]");
        }

        private void buttonEntreColchetes_Click(object sender, EventArgs e)
        {
            Adicionar("{", "}");
        }

        private void buttonEntreAspasSimples_Click(object sender, EventArgs e)
        {
            Adicionar("'", "'");
        }

        private void buttonEntreAspasDupla_Click(object sender, EventArgs e)
        {
            Adicionar("\"", "\"");
        }

        private void buttonEntreMais_Click(object sender, EventArgs e)
        {
            Adicionar("+", "+");
        }

        #endregion

    }
}
