using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFatureClient.Classes;
using AppLib.Windows;

namespace AppFatureClient
{
    public partial class FormRegraProdutoVisao : AppLib.Windows.FormVisao
    {
        private static FormRegraProdutoVisao _instance = null;
        public bool TemInativo = false;

        public static FormRegraProdutoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormRegraProdutoVisao();
            return _instance;
        }

        public FormRegraProdutoVisao()
        {
            InitializeComponent();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            DataRow SelectRow = grid1.GetDataRow();
            FormRegraProdutoCadastro frm = new FormRegraProdutoCadastro();
            frm.cod = SelectRow["CODDP"].ToString();
            frm.editar = true;
            frm.ShowDialog();
            //grid1.Atualizar();
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            try
            {
                DataRow SelectRow = grid1.GetDataRow();
                string sql = String.Format(@"delete from ZTPRODUTOREGRA where CODCOLIGADA = 1 and CODFILIAL = 1 and CODDP = '{0}'", SelectRow["CODDP"].ToString());
                MetodosSQL.ExecQuery(sql);
                grid1.Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormRegraProdutoCadastro frm = new FormRegraProdutoCadastro();
            frm.ShowDialog();
            grid1.Atualizar();
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void FormRegraProdutoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            
        }

        private void grid1_AposAtualizar(object sender, EventArgs e)
        {
            
        }
    }
}
