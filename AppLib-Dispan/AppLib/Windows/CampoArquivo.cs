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
    public partial class CampoArquivo : UserControl
    {
        #region ATRIBUTOS

        [Category("_APP"), Description("Nome da conexão")]
        public String Conexao { get; set; }

        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo")]
        public String Campo { get; set; }

        [Category("_APP"), Description("Filtro de extensão")]
        public TipoArquivo[] Filtro { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }

        #endregion

        #region CONSTRUTOR

        public CampoArquivo()
        {
            InitializeComponent();

            Conexao = "Start";

            TipoArquivo tArquivo = new TipoArquivo("Todos", "*.*");
            Filtro = new TipoArquivo[] { tArquivo };

            Query = 0;
            Edita = true;
        }

        private void CampoArquivo_Load(object sender, EventArgs e)
        {
            textEditIDENTIFICADOR.Properties.ReadOnly = !Edita;
        }

        private void CampoArquivo_Resize(object sender, EventArgs e)
        {
            hyperLinkEditDESCRICAO.Width = (this.Width - 140) - 3;
        }
        
        #endregion

        #region MÉTODOS EXTERNO

        public int? Get()
        {
            if (textEditIDENTIFICADOR.Text.Equals(""))
            {
                return null;
            }
            else
            {
                return int.Parse(textEditIDENTIFICADOR.Text);
            }
        }

        public void Set(int? valor)
        {
            textEditIDENTIFICADOR.Text = valor.ToString();
            this.setDescricao();
        }

        public void Select()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;


            String filtroTemp = "";
            
            for (int i = 0; i < Filtro.Length; i++)
            {
                filtroTemp += Filtro[i].NomeArquivo + " ("+ Filtro[i].ExtensaoArquivo +")|" + Filtro[i].ExtensaoArquivo + "|";
            }

            if (filtroTemp.Length > 0)
            {
                filtroTemp = filtroTemp.Substring(0, filtroTemp.Length - 1);
                ofd.Filter = filtroTemp;
            }


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                int? IDARQUIVO = int.Parse(this.ImportarArquivo(ofd.FileName).ToString());

                if (IDARQUIVO != null)
                {
                    textEditIDENTIFICADOR.Text = IDARQUIVO.ToString();
                    this.setDescricao();
                }
            }
        }

        public void Clear()
        {
            textEditIDENTIFICADOR.Text = "";
            hyperLinkEditDESCRICAO.Text = "";
        }

        #endregion

        #region MÉTODOS INTERNO

        private void setDescricao()
        {
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery("SELECT NOMEARQUIVO FROM ZARQUIVO WHERE IDARQUIVO = ?", new Object[] { textEditIDENTIFICADOR.Text });

            if (dt.Rows.Count > 0)
            {
                hyperLinkEditDESCRICAO.Text = dt.Rows[0][0].ToString();
            }
        }

        public void textEditIDENTIFICADOR_Leave(object sender, EventArgs e)
        {
            hyperLinkEditDESCRICAO.Text = "";
            this.setDescricao();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Select();
        }

        private int? ImportarArquivo(String NomeArquivo)
        {
            try
            {
                AppLib.ORM.Jit reg = new ORM.Jit(AppLib.Context.poolConnection.Get(this.Conexao), "ZARQUIVO");

                System.IO.FileInfo fi = new System.IO.FileInfo(NomeArquivo);
                reg.Set("NOMEARQUIVO", fi.Name);

                String ConteudoArquivo = AppLib.Util.Conversor.FileToBase64(fi.FullName);
                reg.Set("ARQUIVO", ConteudoArquivo);

                reg.Set("ATIVO", 1);
                reg.Set("DATACRIACAO", DateTime.Now);
                reg.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                reg.Set("DATAALTERACAO", DateTime.Now);
                reg.Set("USUARIOALTERACAO", AppLib.Context.Usuario);

                if (reg.Insert() == 1)
                {
                    return AppLib.Context.poolConnection.Get(this.Conexao).GetIncrement();
                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao importar arquivo na tabela ZARQUIVO, motivo: " + ex.Message);
            }

            return null;
        }

        private void hyperLinkEditDESCRICAO_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                AppLib.ORM.Jit reg = new ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "ZARQUIVO");
                reg.Set("IDARQUIVO", textEditIDENTIFICADOR.Text);

                if (reg.Select().Rows.Count > 0)
                {
                    String NOMEARQUIVO = reg.Get("NOMEARQUIVO").ToString();
                    String ARQUIVO = reg.Get("ARQUIVO").ToString();

                    if ( ! System.IO.Directory.Exists("Temp"))
                    {
                        System.IO.Directory.CreateDirectory("Temp");
                    }

                    if (System.IO.File.Exists("Temp\\" + NOMEARQUIVO))
                    {
                        System.IO.File.Delete("TEMP\\" + NOMEARQUIVO);
                    }

                    AppLib.Util.Conversor.Base64ToFile("TEMP\\" + NOMEARQUIVO, ARQUIVO);
                    System.Diagnostics.Process.Start("TEMP\\" + NOMEARQUIVO);
                }
                else
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Arquivo não encontrado na tabela ZARQUIVO - Identificador: " + textEditIDENTIFICADOR.ToString());
                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro: " + ex.Message);
            }
        }
        
        #endregion
        
        #region EVENTOS CUSTOMIZADOS

        #endregion

    }
}
