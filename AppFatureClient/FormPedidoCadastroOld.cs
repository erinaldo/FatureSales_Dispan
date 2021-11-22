using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormPedidoCadastro : AppLib.Windows.FormCadastroData
    {

        // João Pedro Luchiari - 03/01/2018

        public DataRow row;

        public FormPedidoCadastro()
        {
            InitializeComponent();
            
        }

        private void FormPedidoCadastro_Load(object sender, EventArgs e)
        {
            //gridParcelas.GetProcessos().Add("Gerar Parcelas", null, GerarParcelas);
            gridParcelas.GetProcessos().Add("Alterar Valor Financeiro", null, AlterarFinanceiro);
            gridParcelas.GetProcessos().Add("Alterar Tipo de Cobrança", null, alteraTipoCobranca);

            campoLista14.comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            if (campoLista14.comboBox1.Text == "SIM - Sim")
            {
                txtPrazoMontagem.Visible = true;
                lblPrazoMontagem.Visible = true;
                txtPrazoMontagem.Text = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "select PRAZOMONT from TMOVCOMPL where IDMOV = ? and CODCOLIGADA = 1", new object[] { campoInteiro1.textEdit1.Text }).ToString();
            }
            else
            {
                txtPrazoMontagem.Visible = false;
                lblPrazoMontagem.Visible = false;
            }

            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Remove(tabPage5);
        }

        private void GerarParcelas(object sender, EventArgs e)
        {

            DateTime dtVencimento = new DateTime();
            decimal valorParcela = 0;

            //FormSelecaoDataBase frm = new FormSelecaoDataBase();
            //frm.ShowDialog();
            //if (frm.selecao == "Emissão")
            //{
            //    dtVencimento = Convert.ToDateTime(campoDataEMISSAO.Get());
            //}
            //else
            //{
            //    dtVencimento = Convert.ToDateTime(campoDataENTREGA.Get());
            //}

            // PARCELAS
            if (!string.IsNullOrEmpty(campoLookupCODCPG.textBoxCODIGO.Text))
            {
                DataTable dtComparacao = AppLib.Context.poolConnection.Get().ExecQuery("SELECT CODCPG, EDITADO FROM ZPARCELAS WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { campoInteiro1.textEdit1.Text, AppLib.Context.Empresa });
                if (dtComparacao.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtComparacao.Rows[0]["EDITADO"]) == true)
                    {
                        if (dtComparacao.Rows[0]["CODCPG"].ToString() == campoLookupCODCPG.textBoxCODIGO.Text)
                        {
                            return;
                        }
                    }
                }

                DataTable dtParcelas = AppLib.Context.poolConnection.Get().ExecQuery(@"

SELECT 
CODCPG, 

VALORPAGAMENTO1, 
QUANTASVEZES1, 
PERIODOEMDIAS1, 
PRAZO1, 

VALORPAGAMENTO2,
QUANTASVEZES2, 
PERIODOEMDIAS2, 
PRAZO2,  

VALORPAGAMENTO3,
QUANTASVEZES3, 
PERIODOEMDIAS3, 
PRAZO3, 

VALORPAGAMENTO4,
QUANTASVEZES4, 
PERIODOEMDIAS4, 
PRAZO4,

VALORPAGAMENTO5,
QUANTASVEZES5, 
PERIODOEMDIAS5, 
PRAZO5   

FROM TCPG WHERE 
CODCPG = ? AND CODCOLIGADA = ?", new object[] { campoLookupCODCPG.textBoxCODIGO.Text, AppLib.Context.Empresa });

                string financiado = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT FINANCIADO FROM TMOVCOMPL WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { campoInteiro1.textEdit1.Text, AppLib.Context.Empresa }).ToString();
                string CODTB2FLX = string.Empty;
                if (financiado == "SIM")
                {
                    CODTB2FLX = "02";
                }
                else
                {
                    CODTB2FLX = "01";
                }

            //

                if (dtParcelas.Rows.Count > 0)
                {
                    string codtmv = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT CODTMV FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, campoInteiro1.textEdit1.Text }).ToString();
                    AppLib.Context.poolConnection.Get().ExecTransaction("DELETE FROM ZPARCELAS WHERE CODCOLIGADA = ? AND IDMOV = ? AND NUMEROMOV = ? AND CODTMV = ?", new object[] { AppLib.Context.Empresa, campoInteiro1.textEdit1.Text, campoTextoNUMEROMOV.textEdit1.Text, codtmv });
                   
                    int numeroParcela = 0;
                    #region Condição 1

                    if (Convert.ToInt32(dtParcelas.Rows[0]["PRAZO1"]) == 0)
                    {
                        dtVencimento = Convert.ToDateTime(campoDataEMISSAO.Get());
                    }
                    else
                    {
                        dtVencimento = Convert.ToDateTime(campoDataENTREGA.Get());
                    }

                    for (int i1 = 0; i1 < Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES1"]); i1++)
                    {
                        //if (financiado == "SIM")
                        //{
                        //    if (numeroParcela > 1)
                        //    {
                        //        CODTB2FLX = "01";
                        //    }
                        //}
                       
                        dtVencimento = dtVencimento.AddDays(Convert.ToInt32(dtParcelas.Rows[0]["PRAZO1"]));
                        valorParcela = Convert.ToDecimal(campoDecimalVALORLIQUIDO.textEdit1.Text) * (Convert.ToDecimal(dtParcelas.Rows[0]["VALORPAGAMENTO1"]) / Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES1"])) / 100;
                        AppLib.Context.poolConnection.Get().ExecTransaction(@"INSERT INTO ZPARCELAS (CODCOLIGADA, IDMOV, NUMEROPARCELA, CODTMV, NUMEROMOV, VENCIMENTO, VALOR, CODCPG, CODTB2FLX) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, campoInteiro1.textEdit1.Text, ++numeroParcela, codtmv, campoTextoNUMEROMOV.Get(), dtVencimento, valorParcela, campoLookupCODCPG.textBoxCODIGO.Text, CODTB2FLX });
                        //numeroParcela = numeroParcela + 1;
                        valorParcela = 0;
                    }
                    #endregion

                    #region Condição 2
                    if (Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES2"]) > 0)
                    {
                        for (int i1 = 0; i1 < Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES2"]); i1++)
                        {
                            //if (financiado == "SIM")
                            //{
                            //    if (numeroParcela > 1)
                            //    {
                            //        CODTB2FLX = "01";
                            //    }
                            //}
                            dtVencimento = dtVencimento.AddDays(Convert.ToInt32(dtParcelas.Rows[0]["PRAZO2"]));
                            valorParcela = Convert.ToDecimal(campoDecimalVALORLIQUIDO.textEdit1.Text) * (Convert.ToDecimal(dtParcelas.Rows[0]["VALORPAGAMENTO2"]) / Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES2"])) / 100;
                            AppLib.Context.poolConnection.Get().ExecTransaction(@"INSERT INTO ZPARCELAS (CODCOLIGADA, IDMOV, NUMEROPARCELA, CODTMV, NUMEROMOV, VENCIMENTO, VALOR, CODCPG, CODTB2FLX) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, campoInteiro1.textEdit1.Text, ++numeroParcela, codtmv, campoTextoNUMEROMOV.Get(), dtVencimento, valorParcela, campoLookupCODCPG.textBoxCODIGO.Text, CODTB2FLX });
                            valorParcela = 0;
                        }
                    }


                    #endregion

                    #region Condição 3
                    if (Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES3"]) > 0)
                    {
                        for (int i1 = 0; i1 < Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES3"]); i1++)
                        {
                            //if (financiado == "SIM")
                            //{
                            //    if (numeroParcela > 1)
                            //    {
                            //        CODTB2FLX = "01";
                            //    }
                            //}
                            dtVencimento = dtVencimento.AddDays(Convert.ToInt32(dtParcelas.Rows[0]["PRAZO3"]));
                            valorParcela = Convert.ToDecimal(campoDecimalVALORLIQUIDO.textEdit1.Text) * (Convert.ToDecimal(dtParcelas.Rows[0]["VALORPAGAMENTO3"]) / Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES3"])) / 100;
                            AppLib.Context.poolConnection.Get().ExecTransaction(@"INSERT INTO ZPARCELAS (CODCOLIGADA, IDMOV, NUMEROPARCELA, CODTMV, NUMEROMOV, VENCIMENTO, VALOR, CODCPG, CODTB2FLX) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, campoInteiro1.textEdit1.Text, ++numeroParcela, codtmv, campoTextoNUMEROMOV.Get(), dtVencimento, valorParcela, campoLookupCODCPG.textBoxCODIGO.Text, CODTB2FLX });
                            valorParcela = 0;
                        }
                    }

                    #endregion

                    #region Condição 4
                    if (Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES4"]) > 0)
                    {
                        for (int i1 = 0; i1 < Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES4"]); i1++)
                        {
                            //if (financiado == "SIM")
                            //{
                            //    if (numeroParcela > 1)
                            //    {
                            //        CODTB2FLX = "01";
                            //    }
                            //}
                            dtVencimento = dtVencimento.AddDays(Convert.ToInt32(dtParcelas.Rows[0]["PRAZO4"]));
                            valorParcela = Convert.ToDecimal(campoDecimalVALORLIQUIDO.textEdit1.Text) * (Convert.ToDecimal(dtParcelas.Rows[0]["VALORPAGAMENTO4"]) / Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES4"])) / 100;
                            AppLib.Context.poolConnection.Get().ExecTransaction(@"INSERT INTO ZPARCELAS (CODCOLIGADA, IDMOV, NUMEROPARCELA, CODTMV, NUMEROMOV, VENCIMENTO, VALOR, CODCPG, CODTB2FLX) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, campoInteiro1.textEdit1.Text, ++numeroParcela, codtmv, campoTextoNUMEROMOV.Get(), dtVencimento, valorParcela, campoLookupCODCPG.textBoxCODIGO.Text, CODTB2FLX });
                            valorParcela = 0;
                        }
                    }
                    #endregion

                    #region Condição 5
                    if (Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES5"]) > 0)
                    {
                        for (int i1 = 0; i1 < Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES5"]); i1++)
                        {
                            //if (financiado == "SIM")
                            //{
                            //    if (numeroParcela > 1)
                            //    {
                            //        CODTB2FLX = "01";
                            //    }
                            //}
                            dtVencimento = dtVencimento.AddDays(Convert.ToInt32(dtParcelas.Rows[0]["PRAZO5"]));
                            valorParcela = Convert.ToDecimal(campoDecimalVALORLIQUIDO.textEdit1.Text) * (Convert.ToDecimal(dtParcelas.Rows[0]["VALORPAGAMENTO5"]) / Convert.ToInt32(dtParcelas.Rows[0]["QUANTASVEZES5"])) / 100;
                            AppLib.Context.poolConnection.Get().ExecTransaction(@"INSERT INTO ZPARCELAS (CODCOLIGADA, IDMOV, NUMEROPARCELA, CODTMV, NUMEROMOV, VENCIMENTO, VALOR, CODCPG, CODTB2FLX) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, campoInteiro1.textEdit1.Text, ++numeroParcela, codtmv, campoTextoNUMEROMOV.Get(), dtVencimento, valorParcela, campoLookupCODCPG.textBoxCODIGO.Text, CODTB2FLX });
                            valorParcela = 0;
                        }
                    }
                    #endregion

                }
            }
            gridParcelas.Atualizar(false);
            //}
            ///////////////////////////////////////////
        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            String Consulta = @"
SELECT CODCLIENTE, DESCRICAO
FROM GCONSIST
WHERE CODCOLIGADA = ?
  AND APLICACAO = 'T'
  AND CODTABELA = 'TIPOPAGTO'";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1, Consulta, new Object[] { AppLib.Context.Empresa });
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            new FormPedidoItem().Editar(grid1.bs);
        }

        private bool campoLookupCODCFO_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCFO, "SELECT CODCFO, NOMEFANTASIA FROM FCFO WHERE CODCOLIGADA IN (0, ?)", new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookupCODCPG_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"
SELECT CODCPG, NOME
FROM TCPG
WHERE CODCOLIGADA = ?
  AND PLANOVENDA = 1
  AND INATIVO = 0";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCPG, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new object[] { campoInteiro2.Get(), campoInteiro1.Get() };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string texto = @"- COMO ARRAS OU SINAL  DE PAGAMENTO, E VENCIDO EM [DATA] A IMPORTÂNCIA DE [VALOR], QUE EM CASO DE NÃO CUMPRIMENTO DESTE CONTRATO PERDERÁ O VALOR EM FAVOR DA VENDEDORA.
            //-1 PARCELA DE [VALOR] VENCIDA EM [DATA].";
           
            DataTable dtParcelas = AppLib.Context.poolConnection.Get().ExecQuery("SELECT * FROM ZPARCELAS WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, campoInteiro1.Get() });
            string texto = string.Empty;
            for (int i = 0; i < dtParcelas.Rows.Count; i++)
            {
                if (i == 0)
                {
                     texto = @"- COMO ARRAS OU SINAL DE PAGAMENTO, E A VENCER EM "+ string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dtParcelas.Rows[i]["VENCIMENTO"])) + ", A IMPORTÂNCIA DE R$ " + string.Format("{0:n2}", Convert.ToDecimal(dtParcelas.Rows[i]["VALOR"])) + ". O SALDO CONTRATUAL SERÁ PAGO EM " + (dtParcelas.Rows.Count - 1).ToString() + " PARCELA(S) NA SEGUINTE DATA E VALOR.";
                }
                else
                {
                    texto = texto + "\n DATA: " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dtParcelas.Rows[i]["VENCIMENTO"])) + " VALOR: R$ " + string.Format("{0:n2}", Convert.ToDecimal(dtParcelas.Rows[i]["VALOR"]));
                }
            }
            campoMemo1.Set(texto);

           

            //string texto1 = campoMemo1.Get();
            //if (texto1 == null || texto1 == "")
            //    campoMemo1.Set(texto);
            //else
            //    campoMemo1.Set(string.Concat(texto1, " ", texto));
        }
        
        private void campoDataEMISSAO_Leave(object sender, EventArgs e)
        {
            DateTime Emissao, Entrega;

            if (campoDataEMISSAO.Get() == null)
                return;
            else
                Emissao = (DateTime)campoDataEMISSAO.Get();


            if (campoDataENTREGA.Get() == null)
                return;
            else
                Entrega = (DateTime)campoDataENTREGA.Get();

            campoInteiroPRAZOENTREGA.Set((Entrega - Emissao).Days);
        }

        private void campoDataENTREGA_Leave(object sender, EventArgs e)
        {
            DateTime Emissao, Entrega;

            if (campoDataEMISSAO.Get() == null)
                return;
            else
                Emissao = (DateTime)campoDataEMISSAO.Get();


            if (campoDataENTREGA.Get() == null)
                return;
            else
                Entrega = (DateTime)campoDataENTREGA.Get();

            campoInteiroPRAZOENTREGA.Set((Entrega - Emissao).Days);
        }

        private void campoInteiroPRAZOENTREGA_Leave(object sender, EventArgs e)
        {
            int prazodias = Convert.ToInt32(campoInteiroPRAZOENTREGA.Get());
            campoDataENTREGA.Set(Convert.ToDateTime(campoDataEMISSAO.Get()).AddDays(prazodias));
        }

        private void campoLookupCODCFO_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM FCFO WHERE CODCOLIGADA = ? AND CODCFO = ?";
            campoLookupCODCFO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODCFO.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODCFO.Get() }).ToString();
        }

        private void campoLookupCODCPG_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM TCPG WHERE CODCOLIGADA = ? AND CODCPG = ?";
            campoLookupCODCPG.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODCPG.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODCPG.Get() }).ToString();
        }

        private void campoLookup1_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT CODCLIENTE, DESCRICAO FROM GCONSIST WHERE CODCOLIGADA = ?  AND APLICACAO = 'T'  AND CODTABELA = 'TIPOPAGTO' AND CODCLIENTE = ?";
            campoLookup1.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookup1.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookup1.Get() }).ToString();
        }

        private void FormPedidoCadastro_AntesSalvar(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(campoLista3.comboBox1.Text) || campoLista3.comboBox1.Text.Equals("System.Data.DataRowView"))
                {
                    campoLista3.comboBox1.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(campoLista4.comboBox1.Text))
                {
                    campoLista4.comboBox1.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(campoLista5.comboBox1.Text) || campoLista5.comboBox1.Text.Equals("System.Data.DataRowView"))
                {
                    campoLista5.comboBox1.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(campoLista6.comboBox1.Text) || campoLista6.comboBox1.Text.Equals("System.Data.DataRowView"))
                {
                    campoLista6.comboBox1.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(campoLista7.comboBox1.Text) || campoLista7.comboBox1.Text.Equals("System.Data.DataRowView"))
                {
                    campoLista7.comboBox1.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(campoLista8.comboBox1.Text) || campoLista8.comboBox1.Text.Equals("System.Data.DataRowView"))
                {
                    campoLista8.comboBox1.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(campoLista9.comboBox1.Text) || campoLista9.comboBox1.Text.Equals("System.Data.DataRowView"))
                {
                    campoLista9.comboBox1.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(campoLista10.comboBox1.Text) || campoLista10.comboBox1.Text.Equals("System.Data.DataRowView"))
                {
                    campoLista10.comboBox1.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(campoLista11.comboBox1.Text) || campoLista11.comboBox1.Text.Equals("System.Data.DataRowView"))
                {
                    campoLista11.comboBox1.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(campoLista12.comboBox1.Text) || campoLista12.comboBox1.Text.Equals("System.Data.DataRowView"))
                {
                    campoLista12.comboBox1.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(campoLista14.comboBox1.Text) || campoLista14.comboBox1.Text.Equals("System.Data.DataRowView"))
                {
                    campoLista14.comboBox1.SelectedIndex = 0;
                }

                if (campoLista14.comboBox1.Text == "SIM - Sim")
                {
                    try
                    {

                        string query = String.Format("update TMOVCOMPL set prazomont = {0} where IDMOV = {1} and CODCOLIGADA = 1", int.Parse(txtPrazoMontagem.Text), campoInteiro1.Get());
                        AppLib.Context.poolConnection.Get().ExecTransaction(query, new object[] { });
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex);
                    }
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível concluir a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }finally
            {
                
            }
        }

        private void gridParcelas_SetParametros(object sender, EventArgs e)
        {
            gridParcelas.Parametros = new object[] { campoInteiro2.Get(), campoInteiro1.Get() };
        }

        private bool FormPedidoCadastro_ValidarSalvar(object sender, EventArgs e)
        {
            try
            {
                if (campoLista14.comboBox1.Text == "SIM - Sim" && (String.IsNullOrWhiteSpace(txtPrazoMontagem.Text) || int.Parse(txtPrazoMontagem.Text) <= 0))
                {
                    MessageBox.Show("Preencha corretamente o prazo de montagem.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch
            {
                MessageBox.Show("Por favor, insira apenas numeros no prazo de montagem.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            

            if (campoLista15.Get() == null)
            {
                MessageBox.Show("Tipo de Venda obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            
            for (int i = 0; i < gridParcelas.gridView1.RowCount; i++)
            {
                DataRow row1 = gridParcelas.gridView1.GetDataRow(i);
                if (string.IsNullOrEmpty(row1["TIPOCOBRANCA"].ToString()))
                {
                    MessageBox.Show("Favor informar o tipo de cobrança para todas as parcelas", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (row["STATUS"].ToString() == "FATURADO")
            {
                MessageBox.Show("Não é permitido editar pedido 'faturado'.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //this.BotaoSalvar = false;
                //this.BotaoOK = false;
                return false;
            }

            return true;
        }

        private void alteraTipoCobranca(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataRowCollection drc = gridParcelas.GetDataRows();
                if (drc.Count > 0 )
                {
                    FormTipoCobranca frm = new FormTipoCobranca();
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.codTipoCobrana))
                    {
                        for (int i = 0; i < drc.Count; i++)
                        {
                            AppLib.Context.poolConnection.Get().ExecTransaction("UPDATE ZPARCELAS SET CODTB2FLX = ? WHERE CODCOLIGADA = ? AND NUMEROPARCELA = ? AND IDMOV = ?", new object[] { frm.codTipoCobrana, AppLib.Context.Empresa, drc[i]["NUMEROPARCELA"].ToString(), drc[i]["IDMOV"].ToString() });
                        }
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível completar a operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
         
          
            gridParcelas.Atualizar(false);
        }

        public void AlterarFinanceiro(object sender, EventArgs e)
        {
            if (new AppLib.Security.Access().Processo(gridParcelas.Conexao, "APP8018", AppLib.Context.Perfil))
            {
                System.Data.DataRowCollection drc = gridParcelas.GetDataRows();

                if (drc != null)
                {
                    if (gridParcelas.GetDataRows().Count > 1)
                    {
                        MessageBox.Show("Selecione apenas um registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    FormLancamentoFinanceiro f = new FormLancamentoFinanceiro();
                    f.Movimento = gridParcelas.GetDataRow();
                    f.ShowDialog();
                    gridParcelas.Atualizar(false);
                }
            }
        }

        private void campoLookupCODCPG_AposSelecao(object sender, EventArgs e)
        {
          
        }

        private void campoLookupCODCPG_Validating(object sender, CancelEventArgs e)
        {
            GerarParcelas(this, null);
        }

        private void campoLista4_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (campoLista14.comboBox1.Text == "SIM - Sim")
            {
                txtPrazoMontagem.Visible = true;
                lblPrazoMontagem.Visible = true;
            }
            else
            {
                txtPrazoMontagem.Visible = false;
                lblPrazoMontagem.Visible = false;
            }
        }
    }
}
