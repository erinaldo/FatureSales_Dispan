using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace AppFatureClient
{
    public partial class FormRomaneioVisao : AppLib.Windows.FormVisao
    {
        private static FormRomaneioVisao _instance = null;
        public static FormRomaneioVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormRomaneioVisao();
            return _instance;
        }

        public FormRomaneioVisao()
        {
            InitializeComponent();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            try
            {
                DataRow dr;
                dr = grid1.GetDataRow();
                //Verifica se o campo IDCARREGAMENTO está vazio
                if (!string.IsNullOrEmpty(dr["IDCARREGAMENTO"].ToString()))
                {
                    string status = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT STATUS FROM ZCARREGAMENTO WHERE IDCARREGAMENTO = ?", new object[] { dr["IDCARREGAMENTO"] }).ToString();
                    if (status.Equals("COMPLETO"))
                    {
                        MessageBox.Show("Não é possível editar programação associada ao carregamento.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                AppLib.Windows.FormMessagePrompt prt = new AppLib.Windows.FormMessagePrompt();
                prt.textBox1.Text = dr["OBSROMANEIO"].ToString();
                prt.Text = "Digite a Observação";
                prt.Mostrar("Favor Digitar a Observação");
                if (prt.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                {
                    DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                    Calendar cal = dfi.Calendar;

                    AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE ZPROGRAMACAOCARREGAMENTO SET OBSROMANEIO = ?, ROMANEIO = 'S'  WHERE IDPROGRAMACAOCARREGAMENTO = ?", new object[] { prt.textBox1.Text.ToUpper(), dr["IDPROGRAMACAOCARREGAMENTO"] });

                    MessageBox.Show("Observação Cadastrada com Sucesso.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível cadastrar a observação no item.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            grid1.Atualizar(false);

        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            DataRowCollection drc = null;
            try
            {
                drc = grid1.GetDataRows();
            }
            catch (Exception)
            {
            }
            if (drc == null)
            {
                return;
            }

            if (MessageBox.Show("Deseja realmente exluir esse(s) romaneio(s)?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                for (int i = 0; i < drc.Count; i++)
                {
                    //Altera o status do romaneio na programação
                    AppLib.Context.poolConnection.Get().BeginTransaction();
                    AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE ZPROGRAMACAOCARREGAMENTO SET ROMANEIO = 'N' 
FROM ZPROGRAMACAOCARREGAMENTO INNER JOIN ZROMANEIOITEMS ON ZPROGRAMACAOCARREGAMENTO.IDPROGRAMACAOCARREGAMENTO = ZROMANEIOITEMS.IDPROGRAMACAOCARREGAMENTO
WHERE ZROMANEIOITEMS.IDROMANEIO = ?", new object[] { drc[i]["IDROMANEIO"].ToString() });
                    //Apagar os itens da ZCARREGAMENTOITENS E ZCARREGAMENTOITEMS
                    AppLib.Context.poolConnection.Get().ExecTransaction("DELETE ZROMANEIOITEMS WHERE IDROMANEIO = ?", drc[i]["IDROMANEIO"].ToString());
                    AppLib.Context.poolConnection.Get().ExecTransaction("DELETE ZROMANEIO WHERE IDROMANEIO = ?", drc[i]["IDROMANEIO"].ToString());
                    AppLib.Context.poolConnection.Get().Commit();

                }
                MessageBox.Show("Romaneio(s) excluído(s) com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            grid1.Atualizar(false);

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormRomaneioCadastro f = new FormRomaneioCadastro();
            f.Novo();
        }

        private void FormRomaneioVisao_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Imprimir Romaneio", null, ImprimirRomaneio);
            grid1.GetProcessos().Add("Limpar Romaneio", null, ExcluirRomaneio);
            grid1.GetProcessos().Add("Baixar Carregamento", null, baixaCarregamento);
            grid1.GetProcessos().Add("Marcar Romaneio", null, MarcarRomaneio);
        }

        private void MarcarRomaneio(object sender, EventArgs e)
        {
            try
            {              

                try
                {
                    DataRowCollection DR = grid1.GetDataRows();

                    

                    foreach (DataRow x in DR)
                    {
                        if (!string.IsNullOrEmpty(x["IDCARREGAMENTO"].ToString()))
                        {
                            string status = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT STATUS FROM ZCARREGAMENTO WHERE IDCARREGAMENTO = ?", new object[] { x["IDCARREGAMENTO"] }).ToString();
                            if (status.Equals("COMPLETO"))
                            {
                                MessageBox.Show("Não é possível editar programação associada ao carregamento.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                            Calendar cal = dfi.Calendar;

                            AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE ZPROGRAMACAOCARREGAMENTO SET ROMANEIO = 'S'  WHERE IDPROGRAMACAOCARREGAMENTO = ?", new object[] { x["IDPROGRAMACAOCARREGAMENTO"] });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível cadastrar a observação no item.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            grid1.Atualizar(false);
        }

        private void baixaCarregamento(object sender, EventArgs e)
        {
            int sim = 0, nao = 0;

            DataRowCollection drc = null;
            string status = string.Empty;
            int idcarregamento = 0;
            string IdProgramacaoCarregamanto = string.Empty;

            if (new AppLib.Security.Access().Processo("Start", "APP8034", AppLib.Context.Perfil))
            {

                if (MessageBox.Show("Deseja efetuar baixa dos itens selecionados?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    drc = grid1.GetDataRows();
                    idcarregamento = Convert.ToInt32(drc[0]["IDCARREGAMENTO"]);
                    for (int i = 0; i < drc.Count; i++)
                    {
                        IdProgramacaoCarregamanto = drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString();

                        if (idcarregamento != Convert.ToInt32(drc[i]["IDCARREGAMENTO"]))
                        {
                            MessageBox.Show("Favor selecionar somente itens de um único carregamento.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT * FROM ZPROGRAMACAOCARREGAMENTO WHERE IDCARREGAMENTO = ? AND DATABAIXA IS NULL ", new object[] { idcarregamento });
                    if (drc.Count < dt.Rows.Count)
                    {
                        status = "PARCIAL";
                    }
                    else
                    {
                        status = "COMPLETO";
                    }

                    for (int i = 0; i < drc.Count; i++)
                    {
                        if (drc[i]["STATUSBAIXA"].ToString() != "CONCLUÍDO")
                        {
                            if (!string.IsNullOrEmpty(drc[i]["IDCARREGAMENTO"].ToString()))
                            {

                                AppLib.ORM.Jit ZPROGRAMACAOCARREGAMENTO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZPROGRAMACAOCARREGAMENTO");
                                if (!string.IsNullOrEmpty(drc[i]["QTDCARREGADA"].ToString()))
                                {
                                    ZPROGRAMACAOCARREGAMENTO.Set("IDPROGRAMACAOCARREGAMENTO", int.Parse(drc[i]["IDPROGRAMACAOCARREGAMENTO"].ToString()));
                                    ZPROGRAMACAOCARREGAMENTO.Set("DATABAIXA", AppLib.Context.poolConnection.Get().GetDateTime());
                                    ZPROGRAMACAOCARREGAMENTO.Set("STATUSBAIXA", "CONCLUÍDO");
                                    ZPROGRAMACAOCARREGAMENTO.Save();
                                }
                                sim++;
                            }
                            else
                            {
                                nao++;
                            }
                        }
                        else
                        {
                            nao++;
                        }
                    }
                    DataTable dt1 = new DataTable();
                    dt1 = AppLib.Context.poolConnection.Get().ExecQuery(@"SELECT * FROM ZPROGRAMACAOCARREGAMENTO WHERE IDCARREGAMENTO = ? AND DATABAIXA IS NULL ", new object[] { idcarregamento });
                    for (int ii = 0; ii < dt1.Rows.Count; ii++)
                    {
                        AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE ZPROGRAMACAOCARREGAMENTO SET CARREGAMENTO = 'N', CAMINHAO = NULL, TIPOCAMINHAO = NULL, REPROGRAMAR = 1, QTDCARREGADA = 0, IDCARREGAMENTOANTERIOR = ?, IDCARREGAMENTO = NULL, QTDE = ? WHERE IDPROGRAMACAOCARREGAMENTO = ?", new object[] { Convert.ToInt32(dt1.Rows[ii]["IDCARREGAMENTO"]), Convert.ToDecimal(dt1.Rows[ii]["QTDCARREGADA"]), dt1.Rows[ii]["IDPROGRAMACAOCARREGAMENTO"] });
                    }
                    AppLib.Context.poolConnection.Get().ExecTransaction(@"UPDATE ZCARREGAMENTO SET STATUSCARREGADO = ?, STATUS = ?, DATABAIXA = ? WHERE IDCARREGAMENTO = ?", new object[] { status, "CONCLUÍDO", AppLib.Context.poolConnection.Get().GetDateTime(), idcarregamento });

                }
                MessageBox.Show(string.Format("Qtd. de baixa realizada com sucesso: {0}\n\rQtd. de baixa não realizada com sucesso: {1}", sim, nao), "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                grid1.Atualizar(false);
            }
        }
        private void ExcluirRomaneio(object sender, EventArgs e)
        {
            DataRowCollection drc = null;
            try
            {
                drc = grid1.GetDataRows();
            }
            catch (Exception)
            {
            }
            if (drc == null)
            {
                return;
            }
            for (int i = 0; i < drc.Count; i++)
            {
                AppLib.Context.poolConnection.Get().ExecQuery(@"UPDATE ZPROGRAMACAOCARREGAMENTO SET OBSROMANEIO = '', SEMANA = '', ANO = '', ROMANEIO = 'N' WHERE IDPROGRAMACAOCARREGAMENTO = ? ", new object[] { drc[i]["IDPROGRAMACAOCARREGAMENTO"] });
            }
            grid1.Atualizar(false);


        }
        private void ImprimirRomaneio(object sender, EventArgs e)
        {
            try
            {
                if (new AppLib.Security.Access().Processo("Start", "APP8034", AppLib.Context.Perfil))
                {
                    FormRelacaoParcial f = new FormRelacaoParcial("Romaneio");
                    f.Text = "Romaneio";
                    f.ShowDialog();
                }
            }
            catch (Exception)
            {

            }

        }

        private void FormRomaneioVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void grid1_AposAtualizar(object sender, EventArgs e)
        {
            grid1.gridView1.RowStyle += gridView1_RowStyle;
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView View = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["ROMANEIO"]);

                if (category == "S")
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }
    }
}
