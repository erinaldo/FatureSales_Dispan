using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormParametroSubprocesso : Form
    {
        // OBJETOS

        // PROPRIEDADES
        public String Parametro { get; set; }
        public String Valor { get; set; }
        public String NovoValor { get; set; }
        public List<VariaveisFluxo> ListaVariaveis { get; set; }

        // METODOS
        public FormParametroSubprocesso()
        {
            InitializeComponent();
        }

        private void FormParametroSubprocesso_Load(object sender, EventArgs e)
        {
            textBoxPARAMETRO.Text = Parametro;
            comboBoxVALOR.Text = Valor;
            NovoValor = Valor;

            carregaLista();
        }

        public void carregaLista()
        {
            comboBoxVALOR.Items.Clear();

            for (int i = 0; i < ListaVariaveis.Count; i++)
            {
                comboBoxVALOR.Items.Add(ListaVariaveis[i].Variavel);
            }
        }

        public void apagarValor()
        {
            comboBoxVALOR.Text = "";
        }

        public void salvar()
        {
            NovoValor = comboBoxVALOR.Text;
        }

        public void fechar()
        {
            this.Close();
        }

        // EVENTOS
        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {
            fechar();
        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {
            salvar();
            fechar();
        }

    }
}
