using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class FormListaPrompt : Form
    {
        public Boolean PrimeiroItemNulo { get; set; }
        public String DescricaoPrimeiroItem { get; set; }

        public AppLib.Global.Types.Confirmacao confirmacao { get; set; }

        public FormListaPrompt()
        {
            InitializeComponent();

            PrimeiroItemNulo = true;
            DescricaoPrimeiroItem = "Selecione";

            confirmacao = Global.Types.Confirmacao.Cancelar;
        }

        private void FormListaPrompt_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (PrimeiroItemNulo)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Selecione uma opção.");
                }
                else
                {
                    confirmacao = Global.Types.Confirmacao.OK;
                    this.Close();
                }
            }
            else
            {
                confirmacao = Global.Types.Confirmacao.OK;
                this.Close();
            }
        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.Cancelar;
            this.Close();
        }

        public Object Mostrar(String Pergunta, List<ValorDescricao> Opcoes)
        {
            richTextBox1.Text = Pergunta;

            comboBox1.DataSource = null;
            comboBox1.DataSource = Opcoes;
            comboBox1.ValueMember = "VALOR";
            comboBox1.DisplayMember = "DESCRICAO";

            this.ShowDialog();

            if (comboBox1 == null)
            {
                return null;
            }
            else
            {
                return comboBox1.SelectedValue;
            }
        }

        public Object Mostrar(String Pergunta, System.Data.DataTable dt, String ColunaValor, String ColunaDescricao)
        {
            List<ValorDescricao> lista = new List<ValorDescricao>();
            lista.Clear();

            if ( PrimeiroItemNulo )
            {
                lista.Add(new ValorDescricao(null, DescricaoPrimeiroItem));
            }

            for (int i = 0; i < dt.Rows.Count; i++)
			{
                ValorDescricao vd = new ValorDescricao();
                vd.Valor  = dt.Rows[i][ColunaValor].ToString();
                vd.Descricao = dt.Rows[i][ColunaDescricao].ToString();
                lista.Add(vd);
			}

            return this.Mostrar(Pergunta, lista);
        }

        public Object Mostrar(String Pergunta, System.Data.DataTable dt)
        {
            String colunaValor = dt.Columns[0].ColumnName;
            String colunaDescricao = dt.Columns[1].ColumnName;
            return this.Mostrar(Pergunta, dt, colunaValor, colunaDescricao);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(this, null);
            }

            if (e.KeyCode == Keys.Escape)
            {
                buttonCANCELAR_Click(this, null);
            }
        }
    }
}
