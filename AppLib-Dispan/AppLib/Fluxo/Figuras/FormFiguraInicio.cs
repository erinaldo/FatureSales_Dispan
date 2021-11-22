using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormFiguraInicio : Form
    {
        public FiguraInicio fig { get; set; }

        public FormFiguraInicio(FiguraInicio _fig, Object[] ListaDestino)
        {
            InitializeComponent();

            fig = _fig;
            textBoxNOME.Text = fig.Nome;

            comboBoxDESTINO.Text = fig.Destino;
            comboBoxDESTINO.Items.AddRange(ListaDestino);
        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {
            fig.Nome = textBoxNOME.Text;
            fig.Destino = comboBoxDESTINO.Text;
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

    }
}
