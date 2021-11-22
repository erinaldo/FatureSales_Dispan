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
    public partial class FormAprovarFinanceiro : Form
    {
        public DataRow Movimento { get; set; }

        // João Pedro Luchiari - 28-12-2017
        public DataRowCollection Movimentos { get; set; }
        public List<string> Codigos = new List<string>();

        public FormAprovarFinanceiro()
        {
            InitializeComponent();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAprovarFinanceiro_Load(object sender, EventArgs e)
        {
            CarregaGrid();
        }


        private string Validacao()
        {
            if (Movimentos.Count >= 1)
            {
                for (int i = 0; i < Movimentos.Count; i++)
                {
                    string Comando = string.Empty;
                    int codcoligada = AppLib.Context.Empresa;
                    int idmov = int.Parse(Movimentos[i]["IDMOV"].ToString());
                    Comando = @"SELECT dbo.ZFCFINANCEIRO(TMOV.CODCOLIGADA, TMOV.IDMOV) CRITICA FROM TMOV
                        LEFT JOIN FCFO ON FCFO.CODCOLIGADA = TMOV.CODCOLCFO AND FCFO.CODCFO = TMOV.CODCFO
                        WHERE TMOV.CODCOLIGADA = ?
                        AND TMOV.IDMOV = ?";

                    string critica = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, Comando, codcoligada, idmov).ToString();

                    if (critica.Equals("FINANCEIRO") || critica.Equals("VALOR DIVERGENTE"))
                    {
                        Codigos.Add(idmov.ToString());
                    }
                    else
                    {
                        
                    }
                }
                return Codigos.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Aprovar o movimentos selecionados ?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                for (int i = 0; i < Movimentos.Count; i++)
                {
                    if (Movimentos[i]["STATUS"].ToString() != "A FATURAR")
                    {
                        MessageBox.Show("Apenas movimentos A FATURAR podem ser APROVADOS.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Comentar bloco de código abaixo. João Pedro Luchiari - 29/12/2017
                    string Comando = string.Empty;
                    int codcoligada = AppLib.Context.Empresa;
                    String usuarioAprovacao = AppLib.Context.Usuario;
                    //int idmov = int.Parse(Movimento["IDMOV"].ToString());
                    int idmov = int.Parse(Movimentos[i]["IDMOV"].ToString());

                    Comando = @"SELECT dbo.ZFCFINANCEIRO(TMOV.CODCOLIGADA, TMOV.IDMOV) CRITICA FROM TMOV
                        LEFT JOIN FCFO ON FCFO.CODCOLIGADA = TMOV.CODCOLCFO AND FCFO.CODCFO = TMOV.CODCFO
                        WHERE TMOV.CODCOLIGADA = ?
                        AND TMOV.IDMOV = ?";

                    string critica = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, Comando, codcoligada, idmov).ToString();

                    if (critica.Equals("FINANCEIRO") || critica.Equals("VALOR DIVERGENTE"))
                    {
                        Comando = @"SELECT CODCOLIGADA, IDMOV, NUMEROMOV, TMOV.VALORLIQUIDO FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?";

                        Comando = AppLib.Context.poolConnection.Get("Start").ParseCommand(Comando, new Object[] { codcoligada, idmov });
                        System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(Comando, new Object[] { });

                        if (dt.Rows.Count == 1)
                        {
                            AppLib.ORM.Jit x = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "ZTMOVAPROVAFIN");
                            x.Set("CODCOLIGADA", codcoligada);
                            x.Set("IDMOV", idmov);
                            x.Set("NUMEROMOV", dt.Rows[0]["NUMEROMOV"]);
                            x.Set("USUARIOAPROVACAO", usuarioAprovacao);
                            x.Set("DATAHORAAPROVACAO", DateTime.Now);
                            x.Set("VALORLIQUIDO", dt.Rows[0]["VALORLIQUIDO"]);
                            int temp = x.Save();

                            if (temp == 1)
                            {
                                MessageBox.Show("Movimento aprovado com sucesso.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                throw new Exception("Erro ao salvar aprovação do movimento: " + dt.Rows[0]["NUMEROMOV"].ToString());
                            }
                        }
                        else
                        {
                            throw new Exception("Erro ao obter movimento.");
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(critica))
                        {
                            //MessageBox.Show("Verificar : " + critica, "Mensagem.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            string numeromov = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NUMEROMOV FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?", codcoligada, idmov).ToString();
                            MessageBox.Show("Movimento não necessita de aprovação financeira : " + numeromov, "Mensagem.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void CarregaGrid()
        {
            try
            {
                int CodColigada = AppLib.Context.Empresa;
                /*tring IdMov = Movimento["IDMOV"].ToString();*/
                string IdMov = string.Empty;

                for (int i = 0; i < Movimentos.Count; i++)
                {
                    if (i == 0 && Movimentos.Count > 1)
                        IdMov = Movimentos[i]["IDMOV"].ToString() + ",";
                    else
                    {
                        if (i == (Movimentos.Count - 1))
                            IdMov = IdMov + Movimentos[i]["IDMOV"].ToString();
                        else
                            IdMov = IdMov + Movimentos[i]["IDMOV"].ToString() + ",";
                    }
                }

                if (Validacao() != string.Empty)
                {
                    for (int i = 0; i < Codigos.Count; i++)
                    {

                        if (i == 0)
                        {
                            if (Codigos.Count > 1)
                            {
                                IdMov = Codigos[i].ToString() + ",";
                            }
                            else
                            {
                                IdMov = Codigos[i].ToString();
                            }
                        }
                        else
                        {
                            if (i == (Codigos.Count - 1))
                            {
                                IdMov = IdMov + Codigos[i].ToString();
                            }
                            else
                            {
                                IdMov = IdMov + Codigos[i].ToString() + ",";
                            }
                        }
                    }
                    //for (int i = 0; i < Codigos.Count; i++)
                    //{

                    //    if (i == 0 && Codigos.Count > 1)
                    //    {
                    //        IdMov = Codigos[i].ToString() + ",";
                    //    }
                    //    else
                    //    {
                    //        if (i == (Codigos.Count - 1))
                    //        {
                    //            IdMov = IdMov + Codigos[i].ToString();
                    //        }
                    //        else
                    //        {
                    //            IdMov = IdMov + Codigos[i].ToString() + ",";
                    //        }
                    //    }
                    //}

                    string sSql = @"
SELECT 
TMOV.NUMEROMOV,
dbo.ZFCFINANCEIRO(TMOV.CODCOLIGADA, TMOV.IDMOV) CRITICA,
TMOV.CODCOLCFO,
FCFO.CODCFO,
FCFO.CONTRIBUINTE,
FCFO.NOMEFANTASIA NOME_CLIENTE,
TMOV.VALORLIQUIDO,
TMOV.DATAEMISSAO,
TMOV.DATAENTREGA

FROM TMOV
LEFT JOIN FCFO ON FCFO.CODCOLIGADA = TMOV.CODCOLCFO AND FCFO.CODCFO = TMOV.CODCFO

WHERE TMOV.CODCOLIGADA = ?
AND TMOV.IDMOV IN ( " + IdMov + " )";

                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { CodColigada });
                    dataGridView2.DataSource = dt;

                    dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    if (dataGridView2.Columns.Count > 1)
                    {
                        dataGridView2.Columns["CRITICA"].HeaderText = "Critica";
                        dataGridView2.Columns["NUMEROMOV"].HeaderText = "Numero";
                        dataGridView2.Columns["CODCOLCFO"].Visible = false;
                        dataGridView2.Columns["CONTRIBUINTE"].Visible = false;
                        dataGridView2.Columns["CODCFO"].HeaderText = "Cliente";
                        dataGridView2.Columns["NOME_CLIENTE"].HeaderText = "Nome Cliente";
                        dataGridView2.Columns["VALORLIQUIDO"].HeaderText = "Valor Liquido";
                        dataGridView2.Columns["DATAEMISSAO"].HeaderText = "Emissão";
                        dataGridView2.Columns["DATAENTREGA"].HeaderText = "Entrega";

                    }

                    dataGridView2_CellClick(this, null);
                }
                else
                {
                    MessageBox.Show("Não existem movimentos a serem aprovados.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
            catch
            { }
        }

        public void CarregaLancamentos(int CodColigada, string CodCfo)
        {
            try
            {
                string sSql = @"
SELECT NUMERODOCUMENTO, DATAEMISSAO, DATAVENCIMENTO, VALORORIGINAL, VALORBAIXADO
FROM FLAN
WHERE FLAN.CODCOLIGADA = ?
AND FLAN.CODCFO = ?
AND FLAN.PAGREC = 1
AND FLAN.STATUSLAN IN (0,4)
AND FLAN.CODTDO NOT IN ('PRVV', 'PRVC', 'ADF','DAC')
AND FLAN.DATAVENCIMENTO < = GETDATE()- 2";

                System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { CodColigada, CodCfo });
                dataGridView1.DataSource = dt;

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (dataGridView1.Columns.Count > 1)
                {
                    dataGridView1.Columns["NUMERODOCUMENTO"].HeaderText = "Documento";
                    dataGridView1.Columns["DATAEMISSAO"].HeaderText = "Emissão";
                    dataGridView1.Columns["DATAVENCIMENTO"].HeaderText = "Vencimento";
                    dataGridView1.Columns["VALORORIGINAL"].HeaderText = "Valor Original";
                    dataGridView1.Columns["VALORBAIXADO"].HeaderText = "Valor Baixado";
                }
            }
            catch
            { }
        }

        public void CarregaCadastro(int CodColigada, string CodCfo, int Contribuinte)
        {
            try
            {
                string sSql = string.Empty;

                if (Contribuinte == 2 || Contribuinte == 0)
                {

                    sSql = @"
SELECT 
RUA, BAIRRO, CEP, NUMERO, EMAIL, CODETD, CGCCFO, IDPAIS, TIPORUA, TIPOBAIRRO, CODMUNICIPIO,
RUAPGTO, BAIRROPGTO, CEPPGTO, NUMEROPGTO, CODETDPGTO, IDPAISPGTO, CODMUNICIPIOPGTO, TIPORUAPGTO, TIPOBAIRROPGTO,
RUAENTREGA, BAIRROENTREGA, CEPENTREGA, NUMEROENTREGA, CODETDENTREGA, IDPAISENTREGA, CODMUNICIPIOENTREGA, TIPORUAENTREGA, TIPOBAIRROENTREGA
FROM FCFO 
WHERE CODCOLIGADA = ? AND CODCFO = ?
";
                }
                else
                {
                    sSql = @"
SELECT 
RUA, BAIRRO, CEP, NUMERO, EMAIL, CODETD, CGCCFO, INSCRESTADUAL, IDPAIS, TIPORUA, TIPOBAIRRO, CODMUNICIPIO,
RUAPGTO, BAIRROPGTO, CEPPGTO, NUMEROPGTO, CODETDPGTO, IDPAISPGTO, CODMUNICIPIOPGTO, TIPORUAPGTO, TIPOBAIRROPGTO,
RUAENTREGA, BAIRROENTREGA, CEPENTREGA, NUMEROENTREGA, CODETDENTREGA, IDPAISENTREGA, CODMUNICIPIOENTREGA, TIPORUAENTREGA, TIPOBAIRROENTREGA
FROM FCFO 
WHERE CODCOLIGADA = ? AND CODCFO = ?
";
                }

                System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { CodColigada, CodCfo });
                dataGridView3.DataSource = dt;

                dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch
            { }
        }

        public void CarregaLimiteCredito(int CodColigada, string CodCfo, decimal ValorPedido)
        {
            string sSql = "SELECT ISNULL(LIMITECREDITO,1) FROM FCFO WHERE CODCOLIGADA = ? AND CODCFO = ?";
            lblLimiteCredito.Text = string.Format("{0:n}", Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(null, sSql, new object[] { CodColigada, CodCfo })));

            sSql = @"SELECT ISNULL(X.VALORABERTO,0) VALORABERTO
FROM
(
SELECT 
SUM(ISNULL(L.VALORORIGINAL,0)) - SUM(ISNULL(L.VALORBAIXADO,0))  VALORABERTO
FROM   FLAN L (NOLOCK), FTDO D (NOLOCK), FCFO F
WHERE  L.STATUSLAN IN (0, 4) 
AND L.NFOUDUP <> 1
AND L.CODCOLIGADA = D.CODCOLIGADA 
AND L.CODTDO = D.CODTDO 
AND D.EDEVOLUCAO <> 4
AND D.EDEVOLUCAO <> 2
AND L.CODCOLCFO = ?
AND L.CODCFO = ?
AND L.PAGREC = 1
AND F.CODCOLIGADA = L.CODCOLCFO
AND F.CODCFO = L.CODCFO
)X";

            Decimal? ValorAberto = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(CodColigada, sSql, 0, CodCfo));
            Decimal? LimiteCredito = Convert.ToDecimal(lblLimiteCredito.Text);

            lblValorAberto.Text = string.Format("{0:n}", ValorAberto);
            lblValorPedido.Text = string.Format("{0:n}", ValorPedido);
            lblTotal.Text = string.Format("{0:n}", ValorAberto + ValorPedido);

            lblSaldo.Text = string.Format("{0:n}", LimiteCredito - (ValorAberto + ValorPedido));
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                //Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CODCOLIGADA"].Value
                CarregaLancamentos(1, dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CODCFO"].Value.ToString());
                CarregaLimiteCredito(Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CODCOLCFO"].Value),
                    dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CODCFO"].Value.ToString(),
                    Convert.ToDecimal(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["VALORLIQUIDO"].Value));
                CarregaCadastro(Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CODCOLCFO"].Value),
                                    dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CODCFO"].Value.ToString(),
                                    Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CONTRIBUINTE"].Value));
            }
        }
    }
}
