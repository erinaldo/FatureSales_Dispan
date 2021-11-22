using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormFiguraFim : Form
    {
        public FiguraFim fig { get; set; }
        public PropriedadeFluxo Propriedade { get; set; }

        public FormFiguraFim(FiguraFim _fig, PropriedadeFluxo _Propriedade)
        {
            InitializeComponent();

            fig = _fig;
            Propriedade = _Propriedade;

            textBoxNOME.Text = fig.Nome;

            // PREENCHE O COMBO
            comboBoxRETORNO.Items.Clear();
            for (int i = 0; i < Propriedade.Variaveis.Count; i++)
            {
                String temp = Propriedade.Variaveis[i].Variavel;
                comboBoxRETORNO.Items.Add(temp);
            }

            // SETA A PROPRIEDADE
            comboBoxRETORNO.Text = fig.Retorno;
        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {
            fig.Nome = textBoxNOME.Text;
            fig.Retorno = comboBoxRETORNO.Text;
            this.Close();
        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLIMPARRETORNO_Click(object sender, EventArgs e)
        {
            comboBoxRETORNO.Text = "";
        }
    }
}
