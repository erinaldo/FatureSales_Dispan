using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormLancamentoFinanceiro : Form
    {
        public DataRow Movimento { get; set; }
        public string CodTmvOrigem { get; set; }

        public FormLancamentoFinanceiro()
        {
            InitializeComponent();
        }

        private void FormLancamentoFinanceiro_Load(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        public void CarregaGrid()
        { 
            int CodColigada = Convert.ToInt32(Movimento["CODCOLIGADA"]);
            int IdMov = Convert.ToInt32(Movimento["IDMOV"]);

            //string sSql = "SELECT CODCOLIGADA, IDLAN, VALORORIGINAL, DATAVENCIMENTO FROM FLAN WHERE CODCOLIGADA = ? AND IDMOV = ?";
            string sSql = "SELECT CODCOLIGADA, IDMOV, NUMEROPARCELA, VALOR, VENCIMENTO FROM ZPARCELAS WHERE CODCOLIGADA = ? AND IDMOV = ?";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { CodColigada, IdMov });
            dataGridView2.DataSource = dt;

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            if (dataGridView2.Columns.Count > 1)
            {
                dataGridView2.Columns["CODCOLIGADA"].HeaderText = "Coligada";
                dataGridView2.Columns["IDMOV"].HeaderText = "ID Movimento.";
                dataGridView2.Columns["NUMEROPARCELA"].HeaderText = "Número da Parcela";
                dataGridView2.Columns["VALOR"].HeaderText = "Valor Original";
                dataGridView2.Columns["VENCIMENTO"].HeaderText = "Vencimento";

                dataGridView2.Columns["VALOR"].DefaultCellStyle.Format = "C2";
                dataGridView2.Columns["VALOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            dataGridView2_CellClick(this, null);
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja aplicar os ajustes realizados ?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (dataGridView2.Rows.Count <= 0)
                {
                    throw new Exception("Movimento não possui lançamentos.");
                }

                if (dataGridView2.Rows.Count > 0)
                {
                    int CodColigada = Convert.ToInt32(Movimento["CODCOLIGADA"]);
                    int IdMov = Convert.ToInt32(Movimento["IDMOV"]);
                    string sSql = "SELECT SUM(VALOR) VALORTOTAL FROM ZPARCELAS WHERE CODCOLIGADA = ? AND IDMOV = ?";
                    decimal ValorTotal = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { CodColigada, IdMov }));
                    decimal ValorDistribuido = 0;
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(dataGridView2.Rows[i].Cells["VALOR"].Value) <= 0)
                        {
                            throw new Exception("Não é permitido lançamentos com valor zero ou negativo.");
                        }

                        ValorDistribuido = ValorDistribuido + Convert.ToDecimal(dataGridView2.Rows[i].Cells["VALOR"].Value);
                    }

                    if (Convert.ToDecimal(string.Format("{0:n2}", ValorTotal)) != Convert.ToDecimal(string.Format("{0:n2}", ValorDistribuido)))
                    {
                        throw new Exception("Valor informado diferente do valor total.");
                    }

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        sSql = "UPDATE ZPARCELAS SET VALOR = ?, VENCIMENTO = ?, EDITADO = 1 WHERE CODCOLIGADA = ? AND IDMOV = ? AND NUMEROPARCELA = ?";
                        AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, 
                            new object[] {  dataGridView2.Rows[i].Cells["VALOR"].Value, 
                                            dataGridView2.Rows[i].Cells["VENCIMENTO"].Value, 
                                            dataGridView2.Rows[i].Cells["CODCOLIGADA"].Value, 
                                            dataGridView2.Rows[i].Cells["IDMOV"].Value, 
                                            dataGridView2.Rows[i].Cells["NUMEROPARCELA"].Value } );
                    }
                }

                this.Cursor = Cursors.Default;
                this.Close();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                campoTexto1.Set(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["IDMOV"].Value.ToString());
                campoDecimal1.Set(Convert.ToDecimal(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["VALOR"].Value));
                campoData1.Set(Convert.ToDateTime(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["VENCIMENTO"].Value));
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            dataGridView2_CellClick(this, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count <= 0)
                {
                    throw new Exception("Movimento não possui lançamentos.");
                }

                if (dataGridView2.Rows.Count == 1)
                {
                    if (campoDecimal1.Get() != Convert.ToDecimal(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["VALOR"].Value))
                        throw new Exception("Valor informado diferente que o valor total.");
                    else
                    {
                        dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["VENCIMENTO"].Value = campoData1.Get();
                    }
                }

                if (dataGridView2.Rows.Count > 1)
                {
                    DateTime dataOriginal = Convert.ToDateTime(dataGridView2.Rows[0].Cells["VENCIMENTO"].Value);
                    dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["VALOR"].Value = campoDecimal1.Get();
                    dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["VENCIMENTO"].Value = campoData1.Get();

                    int CodColigada = Convert.ToInt32(Movimento["CODCOLIGADA"]);
                    int IdMov = Convert.ToInt32(Movimento["IDMOV"]);

                    string sSql = "SELECT SUM(VALOR) VALORTOTAL FROM ZPARCELAS WHERE CODCOLIGADA = ? AND IDMOV = ?";
                    decimal ValorTotal = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { CodColigada, IdMov }));
                    decimal ValorDistribuido = 0;
                    for (int i = 0; i <= dataGridView2.CurrentRow.Index; i++)
                    {
                        ValorDistribuido = ValorDistribuido + Convert.ToDecimal(dataGridView2.Rows[i].Cells["VALOR"].Value);
                    }

                    decimal ValorDistribuir = ValorTotal - ValorDistribuido;

                    if (ValorDistribuir < 0)
                        throw new Exception("Não é permitido lançamentos com valor zero ou negativo.");

                    
                    
                    if((dataGridView2.Rows.Count - (dataGridView2.CurrentRow.Index + 1)) > 0)
                    {
                        decimal ValorParcela = ValorDistribuir / (dataGridView2.Rows.Count - (dataGridView2.CurrentRow.Index + 1));
                        if (checkEdit1.Checked == true)
                        {
                            DateTime dataValor = Convert.ToDateTime(campoData1.Get());
                            for (int i = dataGridView2.CurrentRow.Index + 1; i < dataGridView2.Rows.Count; i++)
                            {
                                //if (i > 1)
                                //{
                                //    dataOriginal = Convert.ToDateTime(dataGridView2.Rows[i].Cells["VENCIMENTO"].Value);    
                                //}

                                TimeSpan a =  Convert.ToDateTime(dataGridView2.Rows[i].Cells["VENCIMENTO"].Value) - dataOriginal;
                                dataOriginal = Convert.ToDateTime(dataGridView2.Rows[i].Cells["VENCIMENTO"].Value);
                                dataValor = dataValor.Add(a);
                                dataGridView2.Rows[i].Cells["VENCIMENTO"].Value = dataValor;
                                
                            }      
                        }
                        for (int i = dataGridView2.CurrentRow.Index + 1; i < dataGridView2.Rows.Count; i++)
                        {
                            dataGridView2.Rows[i].Cells["VALOR"].Value = ValorParcela;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            }
        }
    }
}
