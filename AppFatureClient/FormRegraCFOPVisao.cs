using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFatureClient.Classes;
using AppLib;
using AppLib.Windows;


namespace AppFatureClient
{
    public partial class FormRegraCFOPVisao : AppLib.Windows.FormVisao
    {
        private static FormRegraCFOPVisao _instance = null;
        public bool TemInativo = false;

        public static FormRegraCFOPVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormRegraCFOPVisao();
            return _instance;
        }

        public FormRegraCFOPVisao()
        {
            InitializeComponent();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            DataRow SelectRow = grid1.GetDataRow();
            FormRegraCFOPCadastro frm = new FormRegraCFOPCadastro();
            frm.cod = SelectRow["ID"].ToString();
            frm.editar = true;
            frm.ShowDialog();
            //grid1.Atualizar();
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            try
            {
                DataRow SelectRow = grid1.GetDataRow();
                string sql = String.Format(@"delete from ZREGRACFOP where ID = {0}", SelectRow["ID"].ToString());
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
            FormRegraCFOPCadastro frm = new FormRegraCFOPCadastro();
            frm.ShowDialog();
            grid1.Atualizar();
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void FormRegraCFOPVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            
        }

        private void grid1_AposAtualizar(object sender, EventArgs e)
        {

        }

        private void FormRegraCFOPVisao_Load(object sender, EventArgs e)
        {

        }
    }
}
