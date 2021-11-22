using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormFiguraProcesso : Form
    {
        public FiguraProcesso fig { get; set; }
        public String NomeFluxo { get; set; }

        public FormFiguraProcesso(FiguraProcesso _fig, Object[] ListaDestino, String _NomeFluxo)
        {
            InitializeComponent();

            fig = _fig;
            textBoxNOME.Text = fig.Nome;
            textBoxTEXTO.Text = fig.Caixa.Texto;

            comboBoxDESTINO.Text = fig.Destino;
            comboBoxDESTINO.Items.AddRange(ListaDestino);

            textBoxEXPRESSAO.Text = fig.Expressao.TextoCompleto();
            NomeFluxo = _NomeFluxo;
        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {
            fig.Nome = textBoxNOME.Text;
            fig.Caixa.Texto = textBoxTEXTO.Text;
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

        private void buttonEDITOR_Click(object sender, EventArgs e)
        {
            // new Editor(fig.Expressao, NomeFluxo).ShowDialog();
            // textBoxEXPRESSAO.Text = fig.Expressao.TextoCompleto();

            AppLib.Expressao.FormExpressaoDesigner f = new AppLib.Expressao.FormExpressaoDesigner();
            f.ExpressaoFigura = fig.Expressao;
            f.Atribuicao = true;
            f.Fluxo = NomeFluxo;
            f.ShowDialog();

            if (f.confirmacao == Global.Types.Confirmacao.OK)
            {
                textBoxEXPRESSAO.Text = f.Expressao;
            }
        }


    }
}
