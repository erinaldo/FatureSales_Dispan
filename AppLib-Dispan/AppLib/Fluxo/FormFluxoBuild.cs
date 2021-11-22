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
    public partial class FormFluxoBuild : Form
    {
        public String CodigoFonte { get; set; }
        public System.CodeDom.Compiler.CompilerResults results { get; set; }

        public FormFluxoBuild()
        {
            InitializeComponent();
        }

        private void FormFluxoBuild_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.RichTextBox richTextBox1 = new RichTextBox();
            richTextBox1.Text = CodigoFonte;

            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                listBoxCODIGOFONTE.Items.Add(richTextBox1.Lines[i]);
            }

            for (int i = 0; i < results.Errors.Count; i++)
            {
                listBoxERROS.Items.Add(results.Errors[i].ErrorText);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int linha = results.Errors[listBoxERROS.SelectedIndex].Line;
                listBoxCODIGOFONTE.SelectedIndex = (linha - 1);
                listBoxCODIGOFONTE.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
    }
}
