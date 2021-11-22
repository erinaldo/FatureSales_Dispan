using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormFiguraSubprocesso : Form
    {
        public FiguraSubprocesso fig { get; set; }
        public List<VariaveisFluxo> Variaveis { get; set; }

        public FormFiguraSubprocesso(FiguraSubprocesso _fig, Object[] ListaDestino, List<VariaveisFluxo> ListaVariaveis)
        {
            InitializeComponent();

            fig = _fig;
            textBoxNOME.Text = fig.Nome;
            textBoxTEXTO.Text = fig.Caixa.Texto;

            comboBoxDESTINO.Text = fig.Destino;
            comboBoxDESTINO.Items.AddRange(ListaDestino);

            Variaveis = ListaVariaveis;

            carregaBiblioteca();

            comboBoxFLUXO.Text = fig.Subprocesso;
        }

        private void FormFiguraSubprocesso_Load(object sender, EventArgs e)
        {
            if (fig.Variaveis.Count > 0)
            {
                dataGridView1.DataSource = fig.Variaveis;
            }
            else
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Parametro");
                dt.Columns.Add("Valor");
                dataGridView1.DataSource = dt;
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {
            fig.Nome = textBoxNOME.Text;
            fig.Caixa.Texto = textBoxTEXTO.Text;
            fig.Destino = comboBoxDESTINO.Text;
            fig.Subprocesso = comboBoxFLUXO.Text;

            fig.Variaveis.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                VariaveisSubprocesso vs = new VariaveisSubprocesso();
                vs.Parametro = dataGridView1.Rows[i].Cells[0].Value.ToString();
                vs.Valor = dataGridView1.Rows[i].Cells[1].Value.ToString();

                fig.Variaveis.Add(vs);
            }

            this.Close();
        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLIMPARDESTINO_Click(object sender, EventArgs e)
        {
            comboBoxDESTINO.Text = "";
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
            if (dt.Rows.Count > 0)
            {
                textBoxDESCRICAO.Text = dt.Rows[0][0].ToString();
            }
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

        private void buttonINCLUIR_Click(object sender, EventArgs e)
        {
            fig.Subprocesso = comboBoxFLUXO.Text;

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery("SELECT VARIAVEL Parametro, '' Valor FROM ZFLUXOVARIAVEL WHERE FLUXO = '" + comboBoxFLUXO.Text + "' AND TIPOVARIAVEL = 'Parâmetro'", new Object[] { });
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                VariaveisSubprocesso var = new VariaveisSubprocesso();
                var.Parametro = dt.Rows[i]["Parametro"].ToString();
                var.Valor = dt.Rows[i]["Valor"].ToString();

                fig.Variaveis.Add(var);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String Parametro = dataGridView1.Rows[e.RowIndex].Cells["Parametro"].Value.ToString();
            String Valor = dataGridView1.Rows[e.RowIndex].Cells["Valor"].Value.ToString();

            FormParametroSubprocesso fps = new FormParametroSubprocesso();
            fps.Parametro = Parametro;
            fps.Valor = Valor;
            fps.ListaVariaveis = Variaveis;

            fps.ShowDialog();

            dataGridView1.Rows[e.RowIndex].Cells["Valor"].Value = fps.NovoValor;
        }

    }
}
