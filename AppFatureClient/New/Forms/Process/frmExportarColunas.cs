using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient.New.Forms.Process
{
    public partial class frmExportarColunas : Form
    {
        #region Variáveis

        public DataTable dtColunasItens = new DataTable();

        #endregion

        public frmExportarColunas()
        {
            InitializeComponent();
        }

        private void frmExportarColunas_Load(object sender, EventArgs e)
        {
            gcColunasItens.DataSource = ConverterDataTable();
            gvColunasItens.BestFitColumns();
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            if (gvColunasItens.SelectedRowsCount > 0)
            {
                string valoresColunas = "";

                DataTable dtColunasSelecionadas = new DataTable();
                DataTable dtItens = gcColunasItens.DataSource as DataTable;

                DataRow[] rowCollection = new DataRow[30];
                DataRow dr = null;

                DataView dv = dtColunasItens.DefaultView;

                int[] linhasSelecionadas = new int[30];
                int count = 0;

                foreach (DataRow row in dtItens.Rows)
                {
                    if (Convert.ToBoolean(row[0]))
                    {
                        rowCollection.SetValue(row, count);
                        count++;
                    }
                }

                rowCollection = rowCollection.Where(x => x != null).ToArray();
                linhasSelecionadas = linhasSelecionadas.Where(x => x != 0).ToArray();

                count = 0;

                string[] listNomeColunas = new string[30];

                foreach (DataRow row1 in rowCollection)
                {
                    foreach (DataColumn dc in row1.Table.Columns)
                    {
                        listNomeColunas.SetValue(dc.Caption, count);
                        count++;
                    }
                }

                listNomeColunas = listNomeColunas.Where(x => x != "").ToArray();

                for (int i = 0; i < dtItens.Rows.Count; i++)
                {
                    if (rowCollection.Count() > 1)
                    {
                        dtColunasSelecionadas = new DataView(dtColunasItens).ToTable(false, listNomeColunas);
                        break;
                    }
                    else
                    {
                        dr = gvColunasItens.GetDataRow(gvColunasItens.GetSelectedRows()[0]);

                        if (dr == gvColunasItens.GetDataRow(i))
                        {
                            dtColunasSelecionadas = new DataView(dtColunasItens).ToTable(false, dr["COLUNAS"].ToString());
                            break;
                        }
                    }
                }

                for (int i = 0; i < dtColunasSelecionadas.Rows.Count; i++)
                {
                    if (i != dtColunasSelecionadas.Rows.Count)
                    {
                        valoresColunas += dtColunasSelecionadas.Rows[i][dr["COLUNAS"].ToString()].ToString() + "\r";
                    }
                }

                Clipboard.SetText(valoresColunas);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos

        private DataTable ConverterDataTable()
        {
            DataTable dtTemp = new DataTable();

            dtTemp.Columns.Add("SELECAO", typeof(bool));
            dtTemp.Columns.Add("COLUNAS", typeof(string));

            for (int i = 0; i < dtColunasItens.Columns.Count; i++)
            {
                dtTemp.Rows.Add(false, dtColunasItens.Columns[i].ColumnName);
            }

            return dtTemp;
        }

        #endregion
    }
}
