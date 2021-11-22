using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppLib.Windows
{
    public partial class FormCampoImagem : DevExpress.XtraEditors.XtraForm
    {
        private OpcaoCampoImagem opcaoCampoImagem { get; set; }
        public String Nome { get; set; }

        public FormCampoImagem()
        {
            InitializeComponent();
            opcaoCampoImagem = OpcaoCampoImagem.Cancelar;
        }

        private void FormCampoImagem_Load(object sender, EventArgs e)
        {

        }

        public OpcaoCampoImagem Mostrar()
        {
            this.ShowDialog();
            return opcaoCampoImagem;
        }

        private void simpleButtonIMPORTAR_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;

            String filtro = "";
            filtro += "Jpg|*.jpg|";
            filtro += "Png|*.png|";
            filtro += "Bitmap|*.bmp|";
            filtro += "Gif|*.gif|";
            filtro += "Icone|*.ico|";
            filtro += "Tiff|*.tif|";

            ofd.Filter = filtro.Substring(0, filtro.Length - 1);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                opcaoCampoImagem = OpcaoCampoImagem.Importar;
                Nome = ofd.FileName;
                this.Close();
            }
        }

        private void simpleButtonEXPORTAR_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                opcaoCampoImagem = OpcaoCampoImagem.Exportar;
                Nome = fbd.SelectedPath;
                this.Close();
            }
        }

        private void simpleButtonAPAGAR_Click(object sender, EventArgs e)
        {
            opcaoCampoImagem = OpcaoCampoImagem.Apagar;
            this.Close();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            opcaoCampoImagem = OpcaoCampoImagem.Cancelar;
            this.Close();
        }

    }

    public enum OpcaoCampoImagem { Importar, Exportar, Apagar, Cancelar }

}