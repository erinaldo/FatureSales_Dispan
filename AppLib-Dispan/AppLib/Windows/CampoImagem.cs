using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class CampoImagem : UserControl
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo arquivo")]
        public String ColunaNomeImagem { get; set; }

        [Category("_APP"), Description("Nome do campo imagem")]
        public String ColunaImagem { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }

        #endregion

        #region CONSTRUTOR

        public CampoImagem()
        {
            InitializeComponent();
            Query = 0;

            ColunaNomeImagem = "NOMEIMAGEM";
            ColunaImagem = "IMAGEM";
            Edita = true;
        }

        private void CampoImagem_Load(object sender, EventArgs e)
        {
            pictureEdit1.Properties.ReadOnly = !Edita;
        }

        #endregion        

        public String GetNomeImagem()
        {
            return label1.Text;
        }

        public String GetImagem()
        {
            if (label1.Text.ToString().Equals(""))
            {
                return null;
            }
            else
            {
                return AppLib.Util.Conversor.ImageToBase64(pictureEdit1.Image, AppLib.Util.Classificacao.getImageFormat(label1.Text));
            }
        }

        public void SetNomeImagem(String StringNomeArquivo)
        {
            label1.Text = StringNomeArquivo;
        }

        public void SetImagem(String StringImagemBase64)
        {
            if (StringImagemBase64 == null)
            {
                pictureEdit1.Image = null;
            }
            else
            {
                pictureEdit1.Image = AppLib.Util.Conversor.Base64ToImage(StringImagemBase64);
            }
        }

        public void Selecionar(object sender, EventArgs e)
        {
            FormCampoImagem f = new FormCampoImagem();
            OpcaoCampoImagem opcaoCampoImagem  = f.Mostrar();

            if (opcaoCampoImagem == OpcaoCampoImagem.Importar)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(f.Nome);

                label1.Text = fi.Name;

                PictureBox pictureBox1 = new PictureBox();
                pictureBox1.Load(f.Nome);

                pictureEdit1.Image = pictureBox1.Image;
            }

            if (opcaoCampoImagem == OpcaoCampoImagem.Exportar)
            {
                try
                {
                    pictureEdit1.Image.Save(f.Nome + "\\" + label1.Text);
                    MessageBox.Show("Imagem exportada com sucesso.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao exportar imagem.\r\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }

            if (opcaoCampoImagem == OpcaoCampoImagem.Apagar)
            {
                this.Clear();
            }

        }

        public void Clear()
        {
            label1.Text = null;
            pictureEdit1.Image = null;
        }



    }
}
