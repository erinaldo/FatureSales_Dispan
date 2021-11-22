using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormRastreabilidade : Form
    {
        public DataRow Movimento { get; set; }

        public FormRastreabilidade()
        {
            InitializeComponent();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRastreabilidade_Load(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        public void CarregaGrid()
        {
            try
            {
                int CodColigada = Convert.ToInt32(Movimento["CODCOLIGADA"]);
                int IdMov = Convert.ToInt32(Movimento["IDMOV"]);

                string sSql = @"SELECT X.CODCOLIGADA, X.IDMOV, X.CODTMV, X.DESCRICAO, TMOV.NUMEROMOV, TMOV.SERIE, 
CASE 
WHEN TMOV.STATUS = 'N' THEN 'Normal' 
WHEN TMOV.STATUS = 'R' THEN 'Não Processado'
WHEN TMOV.STATUS = 'A' THEN 'A Faturar'
WHEN TMOV.STATUS = 'F' THEN 'Faturado'
WHEN TMOV.STATUS = 'P' THEN 'Parcialmente Quitado'
WHEN TMOV.STATUS = 'Q' THEN 'Quitado'
WHEN TMOV.STATUS = 'C' THEN 'Cancelado'
WHEN TMOV.STATUS = 'P' THEN 'Parcialmente Atendido'
WHEN TMOV.STATUS = 'D' THEN 'Perda'
WHEN TMOV.STATUS = 'I' THEN 'Inativo'
WHEN TMOV.STATUS = 'T' THEN 'Cotação'
WHEN TMOV.STATUS = 'B' THEN 'Baixado'
WHEN TMOV.STATUS = 'L' THEN 'Liberado'
WHEN TMOV.STATUS = 'U' THEN 'Em Faturamento'
ELSE ''
END STATUS,
FCFO.CODCFO,
FCFO.NOMEFANTASIA,
TMOV.DATAEMISSAO,
TMOV.DATAENTREGA

FROM [dbo].[FC_RASTREIAMOV] ( ?, ?) X, TMOV
LEFT JOIN FCFO ON FCFO.CODCOLIGADA = TMOV.CODCOLCFO AND FCFO.CODCFO = TMOV.CODCFO
WHERE X.CODCOLIGADA = TMOV.CODCOLIGADA
AND X.IDMOV = TMOV.IDMOV
ORDER BY X.CODCOLIGADA, X.IDMOV";
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { CodColigada, IdMov });
                dataGridView2.DataSource = dt;

                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (dataGridView2.Columns.Count > 1)
                {
                    dataGridView2.Columns["CODCOLIGADA"].HeaderText = "Coligada";
                    dataGridView2.Columns["IDMOV"].HeaderText = "Identificador";
                    dataGridView2.Columns["CODTMV"].HeaderText = "Tipo Mov.";
                    dataGridView2.Columns["DESCRICAO"].HeaderText = "Descrição";
                    dataGridView2.Columns["NUMEROMOV"].HeaderText = "Numero";
                    dataGridView2.Columns["SERIE"].HeaderText = "Série";
                    dataGridView2.Columns["STATUS"].HeaderText = "Status";
                    dataGridView2.Columns["CODCFO"].HeaderText = "Cliente";
                    dataGridView2.Columns["NOMEFANTASIA"].HeaderText = "Nome Fantasia";
                    dataGridView2.Columns["DATAEMISSAO"].HeaderText = "Emissão";
                    dataGridView2.Columns["DATAENTREGA"].HeaderText = "Entrega";
                }
            }
            catch
            { }
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                if (dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CODTMV"].Value.ToString() == "2.1.05")
                {
                    if (new AppLib.Security.Access().Consultar("Start", "ZVWORCAMENTO", AppLib.Context.Perfil))
                    {
                        DataTable dt = (DataTable)dataGridView2.DataSource;
                        new FormOrcamentoCadastro().Editar(dt.Rows[dataGridView2.CurrentRow.Index], true);
                    }
                    else
                    {
                        MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso a este menu (ZVWORCAMENTO).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }

                if (dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CODTMV"].Value.ToString() == "2.1.10")
                {
                    if (new AppLib.Security.Access().Consultar("Start", "ZVWPEDIDO", AppLib.Context.Perfil))
                    {

                        DataTable dt = (DataTable)dataGridView2.DataSource;
                        new FormOrcamentoCadastro().Editar(dt.Rows[dataGridView2.CurrentRow.Index], true);
                    }
                    else
                    {
                        MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso a este menu (ZVWPEDIDO).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }

                if (dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CODTMV"].Value.ToString() == "2.1.15")
                {
                    if (new AppLib.Security.Access().Consultar("Start", "ZVWPEDIDO", AppLib.Context.Perfil))
                    {

                        DataTable dt = (DataTable)dataGridView2.DataSource;
                        new FormOrcamentoCadastro().Editar(dt.Rows[dataGridView2.CurrentRow.Index], true);
                    }
                    else
                    {
                        MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso a este menu (ZVWPEDIDO).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }

                if (dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CODTMV"].Value.ToString() == "2.1.20")
                {
                    if (new AppLib.Security.Access().Consultar("Start", "ZVWORDEMPRODUCAO", AppLib.Context.Perfil))
                    {

                        DataTable dt = (DataTable)dataGridView2.DataSource;
                        new FormOrcamentoCadastro().Editar(dt.Rows[dataGridView2.CurrentRow.Index], true);
                    }
                    else
                    {
                        MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso a este menu (ZVWORDEMPRODUCAO).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView2.Rows[dataGridView2.CurrentRow.Index];

                int result = Convert.ToInt32(AppLib.Context.poolConnection.Get().ExecGetField(-1, @"SELECT * FROM TNFEESTADUAL WHERE CODCOLIGADA = ? AND IDMOV = ? AND STATUS = 'A'", new object[] { row.Cells["CODCOLIGADA"].Value, row.Cells["IDMOV"].Value }));

                if (result > 0)
                {
                    btnImpressaoDanfe.Visible = true;
                    btnImpressaoDanfe.Select();
                }
                else
                {
                    btnImpressaoDanfe.Visible = false;
                }
            }
        }

        private void btnImpressaoDanfe_Click(object sender, EventArgs e)
        {
            // Nota fiscal - DANFE
            DataTable dt = (DataTable)dataGridView2.DataSource;

            New.Reports.xrDanfe xrDanfe = new New.Reports.xrDanfe();
            xrDanfe.IDMOV = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[2].Value);

            ReportPrintTool printTool = new ReportPrintTool(xrDanfe);
            printTool.ShowPreview();
        }
    }
}

