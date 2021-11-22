using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormPrintPreviewSettings : Form
    {
        public FormPrintPreviewSettings()
        {
            InitializeComponent();
        }

        private void FormPrintPreviewSettings_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = 1;
            rbTudo.Checked = true;

            int index = -1;
            int cont = -1;
            foreach (string impressora in PrinterSettings.InstalledPrinters)
            {
                cont++;
                if (new PrinterSettings().PrinterName == impressora)
                    index = cont;

                comboBox1.Items.Add(impressora);
            }
            comboBox1.SelectedIndex = index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrinterSettings parametros = new PrinterSettings();
            parametros.PrinterName = comboBox1.SelectedItem.ToString();
            parametros.Copies = (short)numericUpDown1.Value;
            parametros.Collate = checkBox1.Checked;

            if (rbTudo.Checked)
                parametros.PrintRange = PrintRange.AllPages;
            if (rbAtual.Checked)
                parametros.PrintRange = PrintRange.CurrentPage;
            if (rbSelecao.Checked)
                parametros.PrintRange = PrintRange.Selection;
            if (rbPaginas.Checked)
                parametros.PrintRange = PrintRange.SomePages;

            PrintDocument impressao = new PrintDocument();
            impressao.PrinterSettings = parametros;
            impressao.Print();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 1)
                checkBox1.Enabled = true;
            else
                checkBox1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
