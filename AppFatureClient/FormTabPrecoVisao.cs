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
    public partial class FormTabPrecoVisao : AppLib.Windows.FormVisao
    {
        private static FormTabPrecoVisao _instance = null;
        public bool TemInativo = false;

        public static FormTabPrecoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormTabPrecoVisao();
            return _instance;
        }

        public FormTabPrecoVisao()
        {
            InitializeComponent();

            grid1.gridView1.RowStyle += gridView1_RowStyle;

            grid1.GetProcessos().Add("Inativar Tabela de Preço", null, InativarRegra);
            grid1.GetProcessos().Add("Reativar Tabela de Preço", null, AtivarRegra);
            grid1.GetProcessos().Add("Copiar Tabela de Preço", null, CopiarRegra);
        }

        public void InativarRegra(object sender, EventArgs e)
        {
            try
            {
                DataRowCollection drc = grid1.GetDataRows();
                String Update = @"update ZTPRODUTOTABPRECO
                                    set INATIVO = 1
                                    where ID = {0}";
                List<String> sql = new List<String>();

                foreach (DataRow row in drc)
                {
                    sql.Add(String.Format(Update, row["ID"]));
                }

                MetodosSQL.ExecMultiple(sql);
                grid1.Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AtivarRegra(object sender, EventArgs e)
        {
            try
            {
                DataRowCollection drc = grid1.GetDataRows();
                String Update = @"update ZTPRODUTOTABPRECO
                                    set INATIVO = 0
                                    where ID = {0}";
                List<String> sql = new List<String>();

                foreach (DataRow row in drc)
                {
                    sql.Add(String.Format(Update, row["ID"]));
                }

                MetodosSQL.ExecMultiple(sql);
                grid1.Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CopiarRegra(object sender, EventArgs e)
        {
            try
            {
                FormTabPrecoCadastro frm = new FormTabPrecoCadastro();
                frm.codCopiar = grid1.GetDataRow()["ID"].ToString();
                frm.ShowDialog();
                grid1.Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormTabPrecoCadastro frm = new FormTabPrecoCadastro();
            frm.cod = grid1.GetDataRow()["ID"].ToString();
            frm.ShowDialog();
            //grid1.Atualizar();
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Deseja mesmo excluir o registro?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                List<String> sql = new List<string>();

                sql.Add(String.Format(@"delete from ZTPRODUTOTABPRECOCOMPL where ID = {0}", grid1.GetDataRow()["ID"].ToString()));
                sql.Add(String.Format(@"delete from ZTPRODUTOTABPRECO where ID = {0}", grid1.GetDataRow()["ID"].ToString()));


                MetodosSQL.ExecMultiple(sql);
                grid1.Atualizar();
            }
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            New.Forms.Register.frmCadastroTabelaPreco frmCadastroTabelaPreco = new New.Forms.Register.frmCadastroTabelaPreco();
            frmCadastroTabelaPreco.ShowDialog();

            //FormTabPrecoCadastro frm = new FormTabPrecoCadastro();
            //frm.ShowDialog();
            grid1.Atualizar();
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void FormTabPrecoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView View = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["VIGENTE"]);

                if (category == "SIM")
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
        }

        private void grid1_AposAtualizar(object sender, EventArgs e)
        {

        }

        private void FormTabPrecoVisao_Load(object sender, EventArgs e)
        {

        }
    }
}
