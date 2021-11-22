using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormOpcoesGerais : Form
    {
        public FormOpcoesGerais()
        {
            InitializeComponent();
        }

        private void FormOpcoesGerais_Load(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        public void CarregarGrid()
        {
            String Comando = @"SELECT NOME FROM ZFLUXOBIBLIOTECA ORDER BY 1";
            dataGridView1.DataSource = AppLib.Context.poolConnection.Get().ExecQuery(Comando, new Object[] { });

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void buttonINSERIR_Click(object sender, EventArgs e)
        {
            // PRIMEIRAMENTE REMOVE
            this.buttonEXCLUIR_Click(this, null);

            // DEPOIS INCLUI
            String Comando = @"INSERT INTO ZFLUXOBIBLIOTECA (NOME) VALUES (?)";
            AppLib.Context.poolConnection.Get().ExecTransaction(Comando, new Object[] { textBoxBIBLIOTECAEXT.Text });

            this.CarregarGrid();
        }

        private void buttonEXCLUIR_Click(object sender, EventArgs e)
        {
            String Comando = @"DELETE ZFLUXOBIBLIOTECA WHERE NOME = ?";
            AppLib.Context.poolConnection.Get().ExecTransaction(Comando, new Object[] { textBoxBIBLIOTECAEXT.Text });

            this.CarregarGrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow dr = ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.CurrentRow.Index];
            textBoxBIBLIOTECAEXT.Text = dr["NOME"].ToString();
        }

        private void buttonFECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
