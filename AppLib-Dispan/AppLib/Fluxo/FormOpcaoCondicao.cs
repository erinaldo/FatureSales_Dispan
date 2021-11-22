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
    public partial class FormOpcaoCondicao : Form
    {
        public Result Result { get; set; }

        public FormOpcaoCondicao()
        {
            InitializeComponent();
        }

        private void FormOpcaoCondicao_Load(object sender, EventArgs e)
        {
            Result = AppLib.Fluxo.Result.Nenhum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result = AppLib.Fluxo.Result.DestinoVerdadeiro;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result = AppLib.Fluxo.Result.DestinoFalso;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Result = AppLib.Fluxo.Result.Destino;
            this.Close();
        }
    }

    public enum Result { Nenhum, DestinoVerdadeiro, DestinoFalso, Destino }

}
