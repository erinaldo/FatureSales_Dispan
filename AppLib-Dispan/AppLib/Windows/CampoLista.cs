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
    public partial class CampoLista : UserControl
    {
        #region ATRIBUTOS

        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo")]
        public String Campo { get; set; }

        [Category("_APP"), Description("Itens da lista")]
        public CodigoNome[] Lista { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Valor padrão")]
        public String Default { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }

        #endregion

        #region CONSTRUTOR E LOAD

        public CampoLista()
        {
            InitializeComponent();

            this.Width = 100;
            Query = 0;
            Edita = true;
        }

        private void CampoLista_Load(object sender, EventArgs e)
        {
            comboBox1.Enabled = Edita;
        }

        #endregion

        #region EVENTOS

        private void CampoLista3_Resize(object sender, EventArgs e)
        {
            comboBox1.Height = this.Height;
            comboBox1.Width = this.Width;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(this.AposSelecao != null)
                    this.AposSelecao(sender, e);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region MÉTODOS

        public String Get()
        {
            if (comboBox1.Text.Equals("System.Data.DataRowView"))
            {
                return null;
            }

            if (comboBox1.Text.Equals(""))
            {
                return null;
            }

            return comboBox1.SelectedValue.ToString();
        }

        public void Set(String valor)
        {
            // limpa e carrega  a lista
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.DataColumn dc1 = new System.Data.DataColumn("CODIGO", typeof(String));
            dt.Columns.Add(dc1);

            System.Data.DataColumn dc2 = new System.Data.DataColumn("NOME", typeof(String));
            dt.Columns.Add(dc2);

            for (int i = 0; i < Lista.Length; i++)
            {
                System.Data.DataRow dr = dt.NewRow();

                dr["CODIGO"] = Lista[i].Codigo;
                dr["NOME"] = Lista[i].Codigo + " - " + Lista[i].Nome;

                dt.Rows.Add(dr);
            }

            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "CODIGO";
            comboBox1.DisplayMember = "NOME";

            if (valor != null)
            {
                comboBox1.SelectedValue = valor;
            }
        }

        #endregion

        #region DELEATE

        public delegate void AposSelecaoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado após seleção do valor da lista"), DefaultValue(false)]
        public event AposSelecaoHandler AposSelecao;

        #endregion

    }
}
