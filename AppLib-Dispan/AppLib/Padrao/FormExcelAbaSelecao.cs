using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Padrao
{
    public partial class FormExcelAbaSelecao : DevExpress.XtraEditors.XtraForm
    {
        public List<String> listaAbas { get; set; }
        public List<String> listaSQL { get; set; }
        public AppLib.Global.Types.Confirmacao confirmacao = Global.Types.Confirmacao.Cancelar;
        public String resultAbas = "IS NOT NULL";
        public String resultSQL = "IS NOT NULL";

        public FormExcelAbaSelecao()
        {
            InitializeComponent();
        }

        private void FormExcelAbaSelecao_Load(object sender, EventArgs e)
        {
            checkedListBoxAba.Items.AddRange(listaAbas.ToArray());
            checkedListBoxSQL.Items.AddRange(listaSQL.ToArray());
        }

        private void checkButtonTODASABAS_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxAba.Items.Count; i++)
            {
                checkedListBoxAba.SetItemChecked(i, checkButtonTODASABAS.Checked);
            }

            if (checkButtonTODASABAS.Checked)
            {
                checkButtonTODASABAS.Text = "NENHUM";
            }
            else
            {
                checkButtonTODASABAS.Text = "TODOS";
            }
        }

        private void simpleButtonOKABA_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.OK;
            this.montaResultAbas();
            resultSQL = "IS NOT NULL";
            this.Close();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.Cancelar;
            this.Close();
        }

        private void checkButtonTODASSQL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxSQL.Items.Count; i++)
            {
                checkedListBoxSQL.SetItemChecked(i, checkButtonTODASSQL.Checked);
            }

            if (checkButtonTODASSQL.Checked)
            {
                checkButtonTODASSQL.Text = "NENHUM";
            }
            else
            {
                checkButtonTODASSQL.Text = "TODOS";
            }
        }

        private void simpleButtonOKSQL_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.OK;
            this.montaResultSQL();
            resultAbas = "IS NOT NULL";
            this.Close();
        }

        public void montaResultAbas()
        {
            if (checkedListBoxAba.CheckedIndices.Count > 0)
            {
                resultAbas = "IN (";

                for (int i = 0; i < checkedListBoxAba.Items.Count; i++)
                {
                    if (checkedListBoxAba.GetItemChecked(i))
                    {
                        resultAbas += "'" + checkedListBoxAba.Items[i].ToString() + "', ";
                    }
                }

                resultAbas = resultAbas.Substring(0, resultAbas.Length - 2) + ")";
            }
            else
            {
                resultAbas = "IS NULL";
            }
        }

        public void montaResultSQL()
        {
            if (checkedListBoxSQL.CheckedIndices.Count > 0)
            {
                resultSQL = "IN (";

                for (int i = 0; i < checkedListBoxSQL.Items.Count; i++)
                {
                    if (checkedListBoxSQL.GetItemChecked(i))
                    {
                        String[] temp = checkedListBoxSQL.Items[i].ToString().Split(' ');
                        resultSQL += temp[0] + ", ";
                    }
                }

                resultSQL = resultSQL.Substring(0, resultSQL.Length - 2) + ")";
            }
            else
            {
                resultSQL = "IS NULL";
            }
        }


    }
}
