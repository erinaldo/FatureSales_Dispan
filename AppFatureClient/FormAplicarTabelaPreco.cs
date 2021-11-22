using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormAplicarTabelaPreco : Form
    {
        public Boolean ConexaoDefault = true;
        public String TabelaPreco { get; set; }

        public FormAplicarTabelaPreco()
        {
            InitializeComponent();
        }

        private void FormAplicarTabelaPreco_Load(object sender, EventArgs e)
        {
            Valida v = new Valida();
            if (v.IsRepresentante())
            {
                campoLookup1.Enabled = false;
            }
            else
            {
                campoLookup1.Enabled = true;
            }

            campoLookup1.textBoxCODIGO.Text = "1";
            campoLookup1.textBox1_Leave(this, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TabelaPreco = campoLookup1.Get();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            String Consulta = @"
SELECT TABELA, NOME FROM (
SELECT CODCOLIGADA, 1 TABELA, NOMEDOPRECOPROD1 NOME FROM TPARPRODUTO
UNION ALL
SELECT CODCOLIGADA, 2 TABELA, NOMEDOPRECOPROD2 FROM TPARPRODUTO
UNION ALL
SELECT CODCOLIGADA, 3 TABELA, NOMEDOPRECOPROD3 FROM TPARPRODUTO
UNION ALL
SELECT CODCOLIGADA, 4 TABELA, NOMEDOPRECOPROD4 FROM TPARPRODUTO
UNION ALL
SELECT CODCOLIGADA, 5 TABELA, NOMEDOPRECOPROD5 FROM TPARPRODUTO
) X
WHERE CODCOLIGADA = ?";

            if ( ! ConexaoDefault)
            {
                campoLookup1.Conexao = "Conn2";
                Consulta = Consulta.Replace("TPARPRODUTO", "ZTPARPRODUTO");
            }

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1, Consulta, new Object[] { AppLib.Context.Empresa });
        }

        private void campoLookup1_SetDescricao(object sender, EventArgs e)
        {
            String Consulta = @"
SELECT NOME FROM (
SELECT CODCOLIGADA, 1 TABELA, NOMEDOPRECOPROD1 NOME FROM TPARPRODUTO
UNION ALL
SELECT CODCOLIGADA, 2 TABELA, NOMEDOPRECOPROD2 FROM TPARPRODUTO
UNION ALL
SELECT CODCOLIGADA, 3 TABELA, NOMEDOPRECOPROD3 FROM TPARPRODUTO
UNION ALL
SELECT CODCOLIGADA, 4 TABELA, NOMEDOPRECOPROD4 FROM TPARPRODUTO
UNION ALL
SELECT CODCOLIGADA, 5 TABELA, NOMEDOPRECOPROD5 FROM TPARPRODUTO
) X
WHERE CODCOLIGADA = ? AND TABELA = ?";

            if (!ConexaoDefault)
            {
                campoLookup1.Conexao = "Conn2";
                Consulta = Consulta.Replace("TPARPRODUTO", "ZTPARPRODUTO");
            }
            campoLookup1.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup1.Conexao).ExecGetField("", Consulta, new object[] { AppLib.Context.Empresa, campoLookup1.Get() }).ToString();
            //String Tabela = "TPARPRODUTO";

            //if (!ConexaoDefault)
            //{
            //    campoLookup1.Conexao = "Conn2";
            //    Tabela = Tabela.Replace("TPARPRODUTO", "ZTPARPRODUTO");
            //}

            //AppLib.Windows.CampoLookup.Descricao(campoLookup1, Tabela);
            //campoLookup1.setDescricao();
        }
    }
}
