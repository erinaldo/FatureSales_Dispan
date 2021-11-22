using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormPropriedadeFluxo : Form
    {
        public PropriedadeFluxo Propriedade { get; set; }

        public FormPropriedadeFluxo(PropriedadeFluxo _Propriedade)
        {
            InitializeComponent();

            // ALIMENTA AS LISTAS
            comboBoxTIPORETORNO.DataSource = AppLib.Fluxo.Opcoes.TipoRetorno();

            comboBoxTIPOVARIAVEL.Items.Add("Variável");
            comboBoxTIPOVARIAVEL.Items.Add("Parâmetro");

            comboBoxTIPODADO.DataSource = AppLib.Fluxo.Opcoes.TipoDado();

            // SETA OS VALORES
            Propriedade = _Propriedade;

            textBoxBIBLIOTECA.Text = Propriedade.Biblioteca;
            textBoxCLASSE.Text = Propriedade.Classe;
            textBoxNOME.Text = Propriedade.Nome;
            textBoxDESCRICAO.Text = Propriedade.Descricao;
            comboBoxTIPORETORNO.Text = Propriedade.TipoRetorno;
            dataGridViewVARIAVEIS.DataSource = Propriedade.Variaveis;
        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PropriedadeWorkflow_Load(object sender, EventArgs e)
        {
            if (Propriedade.Variaveis.Count > 0)
            {
                dataGridViewVARIAVEIS.DataSource = Propriedade.Variaveis;
            }
            else
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("TipoVariavel");
                dt.Columns.Add("Variavel");
                dt.Columns.Add("TipoDado");
                dataGridViewVARIAVEIS.DataSource = dt;
            }

            dataGridViewVARIAVEIS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewVARIAVEIS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {
            Propriedade.Biblioteca = textBoxBIBLIOTECA.Text;
            Propriedade.Classe = textBoxCLASSE.Text;
            Propriedade.Nome = textBoxNOME.Text;
            Propriedade.Descricao = textBoxDESCRICAO.Text;
            Propriedade.TipoRetorno = comboBoxTIPORETORNO.Text;

            if (dataGridViewVARIAVEIS.Rows.Count > 0)
            {
                Propriedade.Variaveis = (List<VariaveisFluxo>)dataGridViewVARIAVEIS.DataSource;
            }

            this.Close();
        }

        private void buttonINCLUIR_Click(object sender, EventArgs e)
        {
            Boolean salvar = false;

            if ( Propriedade.Variaveis.Count == 0 )
            {
                salvar = true;
            }

            VariaveisFluxo varfluxo = new VariaveisFluxo();
            varfluxo.TipoVariavel = comboBoxTIPOVARIAVEL.Text;
            varfluxo.Variavel = textBoxVARIAVEL.Text;
            varfluxo.TipoDado = comboBoxTIPODADO.Text;

            Propriedade.Variaveis.Add(varfluxo);

            dataGridViewVARIAVEIS.DataSource = null;
            dataGridViewVARIAVEIS.DataSource = Propriedade.Variaveis;

            textBoxVARIAVEL.Text = "";
            textBoxVARIAVEL.Focus();

            if (salvar)
            {
                buttonSALVAR_Click(this, null);
            }
        }

        private void dataGridViewVARIAVEIS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Propriedade.Variaveis = (List<VariaveisFluxo>)dataGridViewVARIAVEIS.DataSource;

            if (dataGridViewVARIAVEIS.Rows.Count > 0)
            {
                comboBoxTIPOVARIAVEL.Text = Propriedade.Variaveis[dataGridViewVARIAVEIS.CurrentRow.Index].TipoVariavel;
                textBoxVARIAVEL.Text = Propriedade.Variaveis[dataGridViewVARIAVEIS.CurrentRow.Index].Variavel;
                comboBoxTIPODADO.Text = Propriedade.Variaveis[dataGridViewVARIAVEIS.CurrentRow.Index].TipoDado;
            }
            else
            {
                comboBoxTIPOVARIAVEL.Text = "";
                textBoxVARIAVEL.Text = "";
                comboBoxTIPODADO.Text = "";
            }
        }

        private void buttonEXCLUIR_Click(object sender, EventArgs e)
        {
            Propriedade.Variaveis = (List<VariaveisFluxo>)dataGridViewVARIAVEIS.DataSource;

            for (int i = 0; i < Propriedade.Variaveis.Count; i++)
            {
                String varPropriedade = Propriedade.Variaveis[i].Variavel;

                if (varPropriedade.Equals(textBoxVARIAVEL.Text))
                {
                    Propriedade.Variaveis.RemoveAt(i);
                    dataGridViewVARIAVEIS.DataSource = null;
                    dataGridViewVARIAVEIS.DataSource = Propriedade.Variaveis;

                    dataGridViewVARIAVEIS_CellClick(this, null);
                }
            }
        }
    }
}
