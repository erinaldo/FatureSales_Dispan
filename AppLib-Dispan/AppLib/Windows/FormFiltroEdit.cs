using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class FormFiltroEdit : DevExpress.XtraEditors.XtraForm
    {
        public String Conexao { get; set; }
        public List<String> Colunas { get; set; }

        public String NomeGrid { get; set; }
        public String NomeFiltro {get;set;}
        public String Usuario { get; set; }
        public Boolean Padrao { get; set; }

        public FormFiltroEdit()
        {
            InitializeComponent();
        }

        private void FormFiltro2_Load(object sender, EventArgs e)
        {
            // PASSO 1 - CRIA AS COLUNAS
            DataGridViewComboBoxColumn Logico = new DataGridViewComboBoxColumn();
            Logico.Items.Add("E");
            Logico.Items.Add("Ou");

            DataGridViewComboBoxColumn Campo = new DataGridViewComboBoxColumn();
            for (int i = 0; i < Colunas.Count; i++)
            {
                Campo.Items.Add(Colunas[i]);
            }

            DataGridViewComboBoxColumn Comparador = new DataGridViewComboBoxColumn();
            Comparador.Items.Add("Igual");
            Comparador.Items.Add("Diferente");
            Comparador.Items.Add("Maior");
            Comparador.Items.Add("Maior ou igual");
            Comparador.Items.Add("Menor");
            Comparador.Items.Add("Menor ou igual");
            Comparador.Items.Add("Contém");
            Comparador.Items.Add("Não contém");
            Comparador.Items.Add("É nulo");
            Comparador.Items.Add("Não é nulo");
            Comparador.Items.Add("Pertence");
            Comparador.Items.Add("Não pertence");

            DataGridViewTextBoxColumn Valor = new DataGridViewTextBoxColumn();

            // PASSO 2 - ADICIONA AS COLUNAS NA GRID
            dataGridView1.Columns.Add(Logico);
            dataGridView1.Columns.Add(Campo);
            dataGridView1.Columns.Add(Comparador);
            dataGridView1.Columns.Add(Valor);

            // PASSO 3 - NOMEIA AS COLUNAS
            dataGridView1.Columns[0].Name = "LOGICO";
            dataGridView1.Columns[0].HeaderText = "Lógico";

            dataGridView1.Columns[1].Name = "CAMPO";
            dataGridView1.Columns[1].HeaderText = "Campo";

            dataGridView1.Columns[2].Name = "COMPARADOR";
            dataGridView1.Columns[2].HeaderText = "Comparador";

            dataGridView1.Columns[3].Name = "VALOR";
            dataGridView1.Columns[3].HeaderText = "Valor";

            // PASSO 4 - DIMENSIONA AS COLUNAS
            dataGridView1.Columns["LOGICO"].Width = 60;
            dataGridView1.Columns["CAMPO"].Width = 160;
            dataGridView1.Columns["COMPARADOR"].Width = 100;
            dataGridView1.Columns["VALOR"].Width = 220;

            // PASSO 5 - DEMAIS FORMATAÇÕES
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // SE NOVO
            if (NomeFiltro == null)
            {
                this.simpleButtonNOVO_Click(this, null);
            }
            else
            {
                // SE EDITAR
                String SQLFiltroCondicao = @"
SELECT LOGICO, CAMPO, COMPARADOR, VALOR
FROM ZFILTROCONDICAO
WHERE GRID = ?
  AND NOME = ?
ORDER BY SEQUENCIA";

                SQLFiltroCondicao = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(SQLFiltroCondicao, new Object[] { NomeGrid, NomeFiltro });
                System.Data.DataTable dtCondicao = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(SQLFiltroCondicao, new Object[] { });

                for (int i = 0; i < dtCondicao.Rows.Count; i++)
                {
                    String logico = dtCondicao.Rows[i]["LOGICO"].ToString();
                    String campo = dtCondicao.Rows[i]["CAMPO"].ToString();
                    String comparador = dtCondicao.Rows[i]["COMPARADOR"].ToString();
                    String valor= dtCondicao.Rows[i]["VALOR"].ToString();

                    toolStripButtonADICIONAR_Click(this, null);

                    dataGridView1.Rows[i].Cells["LOGICO"].Value = logico;
                    dataGridView1.Rows[i].Cells["CAMPO"].Value = campo;
                    dataGridView1.Rows[i].Cells["COMPARADOR"].Value = comparador;
                    dataGridView1.Rows[i].Cells["VALOR"].Value = valor;
                }
            }
        }

        private void toolStripButtonADICIONAR_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
        }

        private void toolStripButtonREMOVER_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
        }

        private void toolStripButtonSUBIR_Click(object sender, EventArgs e)
        {
            DataGridView grid = dataGridView1;
            try
            {
                int totalRows = grid.Rows.Count;
                int idx = grid.SelectedCells[0].OwningRow.Index;
                if (idx == 0)
                    return;
                int col = grid.SelectedCells[0].OwningColumn.Index;
                DataGridViewRowCollection rows = grid.Rows;
                DataGridViewRow row = rows[idx];
                rows.Remove(row);
                rows.Insert(idx - 1, row);
                grid.ClearSelection();
                grid.Rows[idx - 1].Cells[col].Selected = true;
            }
            catch { }
        }

        private void toolStripButtonDESCER_Click(object sender, EventArgs e)
        {
            DataGridView grid = dataGridView1;
            try
            {
                int totalRows = grid.Rows.Count;
                int idx = grid.SelectedCells[0].OwningRow.Index;
                if (idx == totalRows - 1)
                    return;
                int col = grid.SelectedCells[0].OwningColumn.Index;
                DataGridViewRowCollection rows = grid.Rows;
                DataGridViewRow row = rows[idx];
                rows.Remove(row);
                rows.Insert(idx + 1, row);
                grid.ClearSelection();
                grid.Rows[idx + 1].Cells[col].Selected = true;
            }
            catch { }
        }

        private void simpleButtonNOVO_Click(object sender, EventArgs e)
        {
            textEditNOME.Text = "";
            checkEditCOMPARTILHAR.Checked = true;
            dataGridView1.Rows.Clear();
            toolStripButtonADICIONAR_Click(this, null);
            textEditNOME.Focus();
        }

        public Boolean Validar()
        {
            if (textEditNOME.Text.Length <= 0)
            {
                MessageBox.Show("Informe o nome do filtro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            String Consulta1 = "SELECT * FROM ZFILTRO WHERE GRID = ? AND NOME = ? AND NATIVO = 1";
            if ( AppLib.Context.poolConnection.Get(Conexao).ExecHasRows(Consulta1, new Object[]{ NomeGrid, textEditNOME.Text }) )
            {
                MessageBox.Show("Já existe um filtro nativo do sistema com este nome.\r\nDica: Utilize um outro nome para o seu filtro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++ )
            {
                if (dataGridView1.Rows[i].Cells["LOGICO"].Value == null)
                {
                    MessageBox.Show("Condição inválida. Escolha o operador \"lógico\" na linha " + (i + 1) + " do filtro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (dataGridView1.Rows[i].Cells["CAMPO"].Value == null)
                {
                    MessageBox.Show("Condição inválida. Escolha o \"campo\" na linha " + (i + 1) + " do filtro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (dataGridView1.Rows[i].Cells["COMPARADOR"].Value == null)
                {
                    MessageBox.Show("Condição inválida. Escolha o operador de \"comparação\" na linha " + (i + 1) + " do filtro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        public void TrataRenomear()
        {
            if (this.NomeFiltro != null)
            {
                if (!this.NomeFiltro.Equals(textEditNOME.Text))
                {
                    String deleteZFILTROCONDICAO = @"DELETE ZFILTROCONDICAO WHERE GRID = ? AND NOME = ?";

                    if (AppLib.Context.poolConnection.Get().ExecTransaction(deleteZFILTROCONDICAO, new Object[] { this.NomeGrid, this.NomeFiltro }) >= 0)
                    {
                        String deleteZFILTRO = @"DELETE ZFILTRO WHERE GRID = ? AND NOME = ?";

                        if (AppLib.Context.poolConnection.Get().ExecTransaction(deleteZFILTRO, new Object[] { this.NomeGrid, this.NomeFiltro }) >= 0)
                        {
                            // ok
                        }
                        else
                        {
                            AppLib.Windows.FormMessageDefault.ShowError("Erro ao excluir filtro " + this.NomeFiltro + " na tabela ZFILTRO.");
                        }
                    }
                    else
                    {
                        AppLib.Windows.FormMessageDefault.ShowError("Erro ao excluir condição do filtro " + this.NomeFiltro + " na tabela ZFILTROCONDICAO.");
                    }
                }
            }
        }

        private void simpleButtonSALVAR_Click(object sender, EventArgs e)
        {
            if (this.Validar())
            {
                // REMOVE O FILTRO ANTIGO CASO NECESSÁRIO
                this.TrataRenomear();

                // INSERE O FILTRO
                AppLib.ORM.Jit x = new ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), "ZFILTRO");
                x.Set("GRID", NomeGrid);
                x.Set("NOME", textEditNOME.Text);

                if (checkEditCOMPARTILHAR.Checked)
                {
                    x.Set("USUARIO", null);
                }
                else
                {
                    x.Set("USUARIO", AppLib.Context.Usuario);
                }

                x.Set("NATIVO", 0);

                if ( x.Save() > 0 )
                {
                    String sqlLimpar = AppLib.Context.poolConnection.Get(Conexao).ParseCommand("DELETE ZFILTROCONDICAO WHERE GRID = ? AND NOME = ?", new Object[]{ NomeGrid, NomeFiltro });
                    int contLimpar = AppLib.Context.poolConnection.Get(Conexao).ExecTransaction(sqlLimpar, new Object[] { });

                    if (contLimpar >= 0)
                    {
                        x = new ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), "ZFILTROCONDICAO");
                        x.Set("GRID", NomeGrid);
                        x.Set("NOME", textEditNOME.Text);

                        // INSERE NOVAS CONDIÇÕES DO FILTRO
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            x.Set("SEQUENCIA", (i + 1));
                            x.Set("LOGICO", dataGridView1.Rows[i].Cells["LOGICO"].Value.ToString());
                            x.Set("CAMPO", dataGridView1.Rows[i].Cells["CAMPO"].Value.ToString());
                            x.Set("COMPARADOR", dataGridView1.Rows[i].Cells["COMPARADOR"].Value.ToString());

                            if (dataGridView1.Rows[i].Cells["VALOR"].Value == null)
                            {
                                x.Set("VALOR", null);
                            }
                            else
                            {
                                x.Set("VALOR", dataGridView1.Rows[i].Cells["VALOR"].Value.ToString());
                            }

                            int contCondicoes = x.Save();

                            if (contCondicoes <= 0)
                            {
                                MessageBox.Show("Erro inesperado salvar novas condições do filtro. Condição número "+ (i+1) +".", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro inesperado remover possíveis antigas condições do filtro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Erro inesperado ao salvar o filtro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (this.Validar())
            {
                this.simpleButtonSALVAR_Click(this, null);
                this.simpleButtonCANCELAR_Click(this, null);
            }
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
