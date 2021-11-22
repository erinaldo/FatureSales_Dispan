using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormFiguraTransacao : Form
    {
        public FiguraTransacao fig { get; set; }
        public String NomeFluxo { get; set; }

        public FormFiguraTransacao(FiguraTransacao _fig, Object[] ListaDestino, String _NomeFluxo)
        {
            InitializeComponent();

            fig = _fig;
            textBoxNOME.Text = fig.Nome;
            textBoxTEXTO.Text = fig.Caixa.Texto;

            comboBoxVERDADEIRO.Text = fig.DestinoVerdadeiro;
            comboBoxVERDADEIRO.Items.AddRange(ListaDestino);

            comboBoxFALSO.Text = fig.DestinoFalso;
            comboBoxFALSO.Items.AddRange(ListaDestino);

            comboBoxDESTINO.Text = fig.Destino;
            comboBoxDESTINO.Items.AddRange(ListaDestino);

            // textBoxEXPRESSAO.Text = fig.Expressao.TextoCompleto();
            NomeFluxo = _NomeFluxo;
        }

        private void FormFiguraTransacao_Load(object sender, EventArgs e)
        {

        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {
            fig.Nome = textBoxNOME.Text;
            fig.Caixa.Texto = textBoxTEXTO.Text;
            fig.DestinoVerdadeiro = comboBoxVERDADEIRO.Text;
            fig.DestinoFalso = comboBoxFALSO.Text;
            fig.Destino = comboBoxDESTINO.Text;
            this.Close();
        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLIMPARDESTINOV_Click(object sender, EventArgs e)
        {
            comboBoxVERDADEIRO.Text = "";
        }

        private void buttonLIMPARDESTINOF_Click(object sender, EventArgs e)
        {
            comboBoxFALSO.Text = "";
        }

        private void buttonLIMPARDESTINO_Click(object sender, EventArgs e)
        {
            comboBoxDESTINO.Text = "";
        }

        private void buttonEDITOR_Click(object sender, EventArgs e)
        {

        }
    }
}
