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
    public partial class CampoBusca : UserControl
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Default auto ajuste")]
        public Boolean AutoAjuste { get; set; }

        [Category("_APP"), Description("Nome da grid")]
        public String NomeGrid { get; set; }

        [Category("_APP"), Description("Nome da conexão")]
        public String Conexao { get; set; }

        //[Category("_APP"), Description("Nome da tabela")]
        //public String Tabela { get; set; }

        //[Category("_APP"), Description("Nome dos campos")]
        //public String[] Campo { get; set; }

        [Category("_APP"), Description("Colunas de descrição")]
        public String[] ColunaDescricao { get; set; }

        [Category("_APP"), Description("Separador das descrições")]
        public String Separador { get; set; }

        [Category("_APP"), Description("Consulta da grid")]
        public String[] Consulta { get; set; }

        public Object[] Parametros = new Object[] { };

        [Category("_APP"), Description("Propriedades das Colunas")]
        public GridProps[] Formatacao { get; set; }

        [Category("_APP"), Description("Colunas para Agrupar a grid")]
        public String[] ColunaAgrupar { get; set; }

        private System.Windows.Forms.BindingSource bs = new BindingSource();
        private FormBusca f;
        private System.Data.DataRow dr;

        #endregion

        #region CONSTRUTOR E LOAD
        
        public CampoBusca()
        {
            InitializeComponent();

            Conexao = "Start";
            AutoAjuste = true;
            Separador = " - ";
        }

        private void CampoBusca_Load(object sender, EventArgs e)
        {
            textBox1.Font = this.Font;
        }

        #endregion

        #region DELEGATE

        public delegate void SetNewParametrosHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método setar parametros da consulta"), DefaultValue(false)]
        public event SetNewParametrosHandler SetParametros;

        public delegate void AposSelecaoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado após seleção do valor do lookup"), DefaultValue(false)]
        public event AposSelecaoHandler AposSelecao;

        #endregion
        
        #region EVENTOS

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Lines.Length > 1)
            {
                String temp = "";

                for (int i = 0; i < textBox1.Lines.Length; i++)
                {
                    temp += textBox1.Lines[i].ToString();
                }

                textBox1.Text = temp;

                SendKeys.Send("{END}");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Buscar();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            f = new FormBusca();
            f.AutoAjuste = this.AutoAjuste;
            f.NomeGrid = this.NomeGrid;
            f.Conexao = this.Conexao;
            f.Consulta = this.Consulta;

            try
            {
                this.SetParametros(this, null);
            }
            catch { }

            f.Parametros = this.Parametros;
            f.Formatacao = this.Formatacao;
            f.ColunaAgrupar = this.ColunaAgrupar;

            f.Filtrar();
            f.ShowDialog();

            if (f.Selecionou)
            {
                dr = f.dr;
                this.MontaDescricao();
            }
        }

        #endregion

        #region MÉTODOS

        private System.Data.DataRowCollection BuscarDataRowCollection()
        {
            f = new FormBusca();
            f.AutoAjuste = this.AutoAjuste;
            f.NomeGrid = this.NomeGrid;
            f.Conexao = this.Conexao;
            f.Consulta = this.Consulta;

            try
            {
                this.SetParametros(this, null);
            }
            catch { }

            f.Parametros = this.Parametros;
            f.Formatacao = this.Formatacao;
            f.ColunaAgrupar = this.ColunaAgrupar;

            f.textEdit1.Text = textBox1.Text;
            f.Filtrar();

            f.SelecionarTudo();
            return f.GetDataRows();
        }

        private void Buscar()
        {
            System.Data.DataRowCollection drc = this.BuscarDataRowCollection();

            if (drc == null)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Registro não encontrado.");
            }
            else
            {
                if (drc.Count == 1)
                {
                    this.dr = drc[0];
                    this.MontaDescricao();
                }

                if (drc.Count > 1)
                {
                    f.ShowDialog();
                    if (f.Selecionou)
                    {
                        this.dr = f.dr;
                        this.MontaDescricao();
                    }
                }
            }
        }

        private void MontaDescricao()
        {
            String temp = "";

            for (int i = 0; i < ColunaDescricao.Length; i++)
            {
                temp += dr[ColunaDescricao[i]].ToString() + Separador;
            }

            temp = temp.Substring(0, temp.Length - Separador.Length);

            textBox1.Text = temp;

            try
            {
                this.AposSelecao(this, null);
            }
            catch { }
        }

        public void Clear()
        {
            textBox1.Text = "";
        }

        public void Buscar(String valor)
        {
            textBox1.Text = valor;
            this.Buscar();
        }

        public void Set(String[] Parametros, Object[] Valores)
        {
            System.Data.DataRowCollection drc = this.BuscarDataRowCollection();

            int indice = -1;

            for (int i = 0; i < drc.Count; i++)
            {
                System.Data.DataRow dr = drc[i];
                int achou = 0;

                for (int coluna = 0; coluna < Parametros.Length; coluna++)
                {
                    Object CP1 = dr[Parametros[coluna]];
                    Object CP2 = Valores[coluna].ToString();

                    if (CP1.Equals(CP2))
                    {
                        achou++;
                    }

                    if (achou == Parametros.Length)
                    {
                        indice = i;
                        i = drc.Count;
                    }
                }
            }

            if (indice != -1)
            {
                this.dr = drc[indice];
                this.MontaDescricao();
            }
        }

        public System.Data.DataRow GetDataRow()
        {
            return dr;
        }
        
        #endregion

    }
}
