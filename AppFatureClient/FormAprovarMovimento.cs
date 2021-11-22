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
    public partial class FormAprovarMovimento : Form
    {
        public DataRowCollection Movimentos { get; set; }

        public FormAprovarMovimento()
        {
            InitializeComponent();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAprovarMovimento_Load(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Aprovar os movimentos selecionados ?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                for (int i = 0; i < Movimentos.Count; i++)
                {
                    if (Movimentos[i]["STATUS"].ToString() != "A FATURAR")
                    {
                        MessageBox.Show("Apenas movimentos A FATURAR podem ser APROVADOS.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int codcoligada = AppLib.Context.Empresa;
                    String usuarioAprovacao = AppLib.Context.Usuario;
                    int idmov = int.Parse(Movimentos[i]["IDMOV"].ToString());

                    String Comando = @"SELECT TM.CODCOLCFO, 
                                               TM.CODCFO, 
                                               case TMC.TIPODESC
                                               when 0 then TM.PERCENTUALDESC
                                               when 1 then TMC.VALORDESCRAT
                                               end as 'PERCENTUALDESC', 
                                               TM.VALORLIQUIDOORIG as 'VALORLIQUIDO'
                                        FROM TMOV TM

                                        INNER JOIN TMOVCOMPL TMC
                                        on TMC.CODCOLIGADA = TM.CODCOLIGADA
                                        and TMC.IDMOV = TM.IDMOV

                                        WHERE TM.CODCOLIGADA = ?
                                        AND TM.IDMOV = ?";

                    try
                    {
                        Comando = AppLib.Context.poolConnection.Get("Start").ParseCommand(Comando, new Object[] { codcoligada, idmov });
                        System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(Comando, new Object[] { });

                        if (dt.Rows.Count == 1)
                        {
                            int CODCOLCFO = int.Parse(dt.Rows[0]["CODCOLCFO"].ToString());
                            String CODCFO = dt.Rows[0]["CODCFO"].ToString();
                            decimal PERCENTUALDESC = decimal.Parse(dt.Rows[0]["PERCENTUALDESC"].ToString());
                            decimal VALORLIQUIDO = decimal.Parse(dt.Rows[0]["VALORLIQUIDO"].ToString());

                            AppLib.ORM.Jit x = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "ZTMOVAPROVA");
                            x.Set("CODCOLIGADA", codcoligada);
                            x.Set("IDMOV", idmov);
                            x.Set("CODCOLCFO", CODCOLCFO);
                            x.Set("CODCFO", CODCFO);
                            x.Set("PERCENTUALDESC", PERCENTUALDESC);
                            x.Set("VALORLIQUIDO", VALORLIQUIDO);
                            x.Set("USUARIOAPROVACAO", usuarioAprovacao);
                            x.Set("DATAHORAAPROVACAO", DateTime.Now);
                            int temp = x.Save();

                            //if (temp == 1)
                            //{
                            //    // ok
                            //    MessageBox.Show("Aprovação realizada com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    this.Close();
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Erro ao salvar aprovação do movimento: " + idmov + " - coligada: " + codcoligada, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //}
                        }
                        else
                        {
                            MessageBox.Show("Erro ao obter movimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        new AppLib.Windows.FormExceptionSQL().Mostrar("Erro ao aprovar orçamento.", Comando, ex);
                    }
                }
                MessageBox.Show("Aprovação realizada com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CarregaGrid()
        {
            try
            {
                int CodColigada = AppLib.Context.Empresa;
                string IdMov = string.Empty;


                for (int i = 0; i < Movimentos.Count; i++)
                {
                    if (i == 0 && Movimentos.Count > 1)
                        IdMov = Movimentos[i]["IDMOV"].ToString() + ",";
                    else
                    { 
                        if(i == (Movimentos.Count - 1))
                            IdMov = IdMov + Movimentos[i]["IDMOV"].ToString();
                        else
                            IdMov = IdMov + Movimentos[i]["IDMOV"].ToString() + ",";                    
                    }                    
                }

                string sSql = @"
SELECT TMOV.NUMEROMOV,
FCFO.CODCFO,
FCFO.NOMEFANTASIA NOME_CLIENTE,
case TMC.TIPODESC
when 0 then TMOV.PERCENTUALDESC
when 1 then TMC.VALORDESCRAT
end as 'DESC_ORCAMENTO',
TMOV.VALORLIQUIDO

FROM TMOV

inner join TMOVCOMPL TMC on TMC.CODCOLIGADA = TMOV.CODCOLIGADA and TMC.IDMOV = TMOV.IDMOV
LEFT JOIN FCFO ON FCFO.CODCOLIGADA = TMOV.CODCOLCFO AND FCFO.CODCFO = TMOV.CODCFO
LEFT JOIN TRPR ON TRPR.CODCOLIGADA = TMOV.CODCOLIGADA AND TRPR.CODRPR = TMOV.CODRPR
LEFT JOIN TRPRCOMPL ON TRPR.CODCOLIGADA = TRPRCOMPL.CODCOLIGADA AND TRPR.CODRPR = TRPRCOMPL.CODRPR

WHERE TMOV.CODCOLIGADA = ?
AND TMOV.IDMOV IN ( " + IdMov + " )";

                System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sSql, new object[] { CodColigada });
                dataGridView2.DataSource = dt;

                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (dataGridView2.Columns.Count > 1)
                {
                    dataGridView2.Columns["NUMEROMOV"].HeaderText = "NUMERO";
                    dataGridView2.Columns["CODCFO"].HeaderText = "CLIENTE";
                    dataGridView2.Columns["NOME_CLIENTE"].HeaderText = "NOME CLIENTE";
                    dataGridView2.Columns["DESC_REPRESETANTE"].HeaderText = "Desconto Representante";
                    dataGridView2.Columns["DESC_ORCAMENTO"].HeaderText = "Desconto Orçamento";
                    dataGridView2.Columns["VALORLIQUIDO"].HeaderText = "Valor Liquido";
                }
            }
            catch
            { }
        }
    }
}
