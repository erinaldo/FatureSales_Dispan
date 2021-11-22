using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppLib;
using AppLib.Windows;
using AppFatureClient.Classes;

namespace AppFatureClient
{
    public partial class FormComissaoVisao : DevExpress.XtraEditors.XtraForm
    {
        public Boolean Selecionou { get; set; }
        string Anexo = String.Empty;
        hideTabPage hide = new hideTabPage();
        string SQLLancamentoFinanceiros = String.Empty;
        Boolean LancamentosFinanceiros = false;

        public FormComissaoVisao()
        {
            InitializeComponent();

            
        }


        private void FormConsulta_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Adicionar ADC", null, abrirADC);
            grid1.GetAnexos().Add("ADC + NFe sem vinculo", null, itemMovimentoSemVinculo); //GridData2
            grid1.GetAnexos().Add("Lançamentos financeiros", null, itemMovimento); //GridData1
            grid1.GetAnexos().Add("Fechar anexos", null, FecharAnexos);

            gridData2.GetProcessos().Add("Associar pedido", null, AssociarPedido);
            gridData1.GetProcessos().Add("Desassociar ADC", null, DesassociarPedido);
            gridData1.GetProcessos().Add("Editar ADC", null, EditarADC);


            hide.HideTabPage(tabPage1, tabControl1);
            hide.HideTabPage(tabPage2, tabControl1);
        }

        private void AssociarPedido(object sender, EventArgs e)
        {
            try
            {

                DataRowCollection row = gridData2.GetDataRows();
                string Pedido = grid1.GetDataRow()["Pedido"].ToString();

                foreach (DataRow dr in row)
                {
                    string IDLAN = dr["IDLAN"].ToString();

                    string sql = String.Format(@"update FLAN set CAMPOALFAOP3 = '{0}' where IDLAN = '{1}'", Pedido, IDLAN);

                    AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { });
                }

                //string Pedido = grid1.GetDataRow()["Pedido"].ToString();
                //string IDLAN = gridData2.GetDataRow()["IDLAN"].ToString();

                //string sql = String.Format(@"update FLAN set CAMPOALFAOP3 = '{0}' where IDLAN = '{1}'", Pedido, IDLAN);

                //AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { });

                gridData2.Atualizar();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }

        private void DesassociarPedido(object sender, EventArgs e)
        {
            try
            {

                DataRowCollection row = gridData1.GetDataRows();

                foreach (DataRow dr in row)
                {
                    string IDLAN = dr["IDLAN"].ToString();

                    string sql = String.Format(@"update flan set CAMPOALFAOP3 = null where IDLAN = {0} and CODCOLIGADA = 1", IDLAN);

                    AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { });
                }

                gridData1.Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void EditarADC(object sender, EventArgs e)
        {

            try
            {
                DataRow SelectRow = gridData1.GetDataRow();

                if (gridData1.GetDataRows().Count == 1)
                {
                    if (SelectRow["STATUSLAN"].ToString() != "1")
                    {
                        FormComissaoCadastro f = new FormComissaoCadastro(SelectRow["IDLAN"].ToString(), true);
                        f.Show();
                    }
                    else
                    {
                        MessageBox.Show("Não é possivél editar ADC baixado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Selecione apenas um registro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
            }
        }
        

        private void abrirADC(object sender, EventArgs e)
        {
            try
            {
                if (grid1.GetDataRows().Count == 1)
                {
                    DataRow SelectRow = grid1.GetDataRow();
                    FormComissaoCadastro f = new FormComissaoCadastro(SelectRow["Pedido"].ToString(), false);
                    f.Show();
                }
                else
                {
                    MessageBox.Show("Selecione apenas um registro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
            }


        }

        private void itemMovimento(object sender, EventArgs e)
        {
            //FecharAnexos(null, null);

            hideTabPage hide = new hideTabPage();
            tabPage1.Text = "Lançamentos Financeiros";
            hide.ShowTabPage(tabPage1, tabControl1);

            try
            {
                if (grid1.GetDataRows().Count == 1)
                {
                    
                    Anexo = "LANCAMENTOSFINANCEIROS";
                    SQLLancamentoFinanceiros = @"SELECT
	FLAN.CODCOLIGADA,
	FLAN.IDMOV,
	FLAN.IDLAN,
	FLAN.CODTDO,
	FLAN.NUMERODOCUMENTO,
	FLAN.VALORORIGINAL,
	FLANBAIXA.VALORNOTACREDITOADIANTAMENTO,
	FLANBAIXA.VALORJUROS,
	FLANBAIXA.VALORDESCONTO,
	FLANBAIXA.VALORDEVOLUCAO,
	FLANBAIXA.VALORMULTA,
	FLANBAIXA.VALOROP5 TAXAFLAT,
	FLAN.VALORBAIXADO,
	FLAN.STATUSLAN

FROM FLAN

LEFT JOIN FLANBAIXA 
ON FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
AND FLANBAIXA.IDLAN = FLAN.IDLAN

WHERE FLAN.CODCOLIGADA = 1
AND FLAN.CODTDO = 'ADC'
AND FLAN.CAMPOALFAOP3 = {0}

UNION

SELECT
	FLAN.CODCOLIGADA,
	FLAN.IDMOV,
	FLAN.IDLAN,
	FLAN.CODTDO,
	FLAN.NUMERODOCUMENTO,
	FLAN.VALORORIGINAL,
	FLANBAIXA.VALORNOTACREDITOADIANTAMENTO,
	FLANBAIXA.VALORJUROS,
	FLANBAIXA.VALORDESCONTO,
	FLANBAIXA.VALORDEVOLUCAO,
	FLANBAIXA.VALORMULTA,
	FLANBAIXA.VALOROP5 TAXAFLAT,
	FLAN.VALORBAIXADO,
	FLAN.STATUSLAN

FROM FLAN

LEFT JOIN FLANBAIXA 
ON FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
AND FLANBAIXA.IDLAN = FLAN.IDLAN

WHERE FLAN.CODCOLIGADA = 1
AND FLAN.CODTDO = 'NF_e'
AND FLAN.CAMPOALFAOP3 = {0}

UNION

SELECT
	FLAN.CODCOLIGADA,
	FLAN.IDMOV,
	FLAN.IDLAN,
	FLAN.CODTDO,
	FLAN.NUMERODOCUMENTO,
	FLAN.VALORORIGINAL,
	FLANBAIXA.VALORNOTACREDITOADIANTAMENTO,
	FLANBAIXA.VALORJUROS,
	FLANBAIXA.VALORDESCONTO,
	FLANBAIXA.VALORDEVOLUCAO,
	FLANBAIXA.VALORMULTA,
	FLANBAIXA.VALOROP5 TAXAFLAT,
	FLAN.VALORBAIXADO,
	FLAN.STATUSLAN

FROM FLAN

LEFT JOIN FLANBAIXA 
ON FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
AND FLANBAIXA.IDLAN = FLAN.IDLAN

WHERE FLAN.IDMOV IN (SELECT 
						IDMOVDESTINO 
					FROM 
						TMOVRELAC 
					WHERE 
						CODCOLORIGEM = 1
					AND IDMOVORIGEM = (SELECT IDMOV FROM TMOV WHERE CODCOLIGADA = 1 AND CODTMV = '2.1.20' AND NUMEROMOV = {0} AND SERIE = 'OPG')
					AND IDMOVDESTINO NOT IN (SELECT IDMOV FROM TMOV WHERE CODCOLIGADA = 1 AND CODTMV IN ('2.1.40')))
AND FLAN.CODCOLIGADA = 1
AND FLAN.CODTDO <> 'ADC'";


                    string sql = String.Format(SQLLancamentoFinanceiros, grid1.GetDataRow()["Pedido"]);

                    gridData1.Consulta = sql.Split(' ');
                    gridData1.Parametros = new Object[] { };
                    splitContainer1.Panel2Collapsed = false;
                    gridData1.Atualizar();

                    LancamentosFinanceiros = true;

                }
                else
                {
                    MessageBox.Show("Selecione apenas um registro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

        }

        private void itemMovimentoSemVinculo(object sender, EventArgs e)
        {
            //FecharAnexos(null,null);

            hideTabPage hide = new hideTabPage();
            tabPage2.Text = "ADC + NFe sem vinculo";
            hide.ShowTabPage(tabPage2, tabControl1);

            try
            {
                    Anexo = "ADIANATAMENTOSSEMVINCULO";
                    string sql = @"select 
           F.CODCOLIGADA,
           F.IDLAN, 
           F.STATUSLAN,
           F.NUMERODOCUMENTO,
           FCFO.NOME,
           F.VALORORIGINAL,
           F.VALORBAIXADO,
           F.CODTDO,
           F.DATAVENCIMENTO,
           F.DATABAIXA,
           F.CAMPOALFAOP3 as 'PEDIDO',
		   FCFC1.CODCONTA as 'Conta 1',
	                      FCFC1.CLASSCONTA as 'Class Conta 1',
		   FCFC2.CODCONTA as 'Conta 2',
		   FCFC2.CLASSCONTA as 'Calss Conta 2'

from FLAN F

inner join FCFOCONT FCFC1
on FCFC1.CODCFO = F.CODCFO
and FCFC1.CODCONTA like '1.01.03.%'

inner join FCFOCONT FCFC2
on FCFC2.CODCFO = F.CODCFO
and FCFC2.CODCONTA like '2.01.04.%'

inner join FCFO
on FCFO.CODCFO = F.CODCFO

where F.CODTDO in ('ADC','NF_e')
and FCFC1.PAGREC = 1
and FCFC2.PAGREC = 1
and F.CAMPOALFAOP3 is null";
                    gridData2.Consulta = sql.Split(' ');
                    gridData2.Parametros = new Object[] { };
                    splitContainer1.Panel2Collapsed = false;

                    gridData2.Atualizar();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void FecharAnexos(object sender, EventArgs e)
        {
            hideTabPage hide = new hideTabPage();
            hide.HideTabPage(tabPage1, tabControl1);
            hide.HideTabPage(tabPage2, tabControl1);
            splitContainer1.Panel2Collapsed = true;
            LancamentosFinanceiros = false;
        }

        #region VISÃO

        public void Mostrar()
        {
            this.WindowState = FormWindowState.Maximized;

            if (new AppLib.Security.Access().Consultar(grid1.Conexao, grid1.NomeGrid, AppLib.Context.Perfil))
            {
                this.Show();
            }
            else
            {
                MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso a este menu (" + grid1.NomeGrid + ").", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        public void Mostrar(System.Windows.Forms.Form _MdiParent)
        {
            this.MdiParent = _MdiParent;
            this.Mostrar();
        }

        #endregion

        #region LOOKUP

        public Boolean MostrarLookup(CampoLookup campoLookup1)
        {
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;

            if (campoLookup1.textBoxCODIGO.Text.Equals(""))
            {
                this.grid1.panelLookup.Visible = true;
                this.grid1.FormPai = this;
                this.ShowDialog();

                if (this.grid1.Selecionou)
                {
                    campoLookup1.dr = grid1.GetDataRow();
                    campoLookup1.textBoxCODIGO.Text = campoLookup1.dr[campoLookup1.ColunaCodigo].ToString();
                    campoLookup1.textBoxDESCRICAO.Text = campoLookup1.dr[campoLookup1.ColunaDescricao].ToString();
                }
            }
            else
            {
                this.grid1.Atualizar(false, true, this);

                DataTable dt = ((DataView)grid1.gridControl1.Views[0].DataSource).Table;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][campoLookup1.ColunaCodigo].ToString().Equals(campoLookup1.textBoxCODIGO.Text))
                    {
                        campoLookup1.dr = dt.Rows[i];
                        campoLookup1.textBoxDESCRICAO.Text = dt.Rows[i][campoLookup1.ColunaDescricao].ToString();
                        Selecionou = true;
                        i = dt.Rows.Count;
                    }
                }
            }

            return grid1.Selecionou;
        }

        public Boolean MostrarLookup(CampoLookup campoLookup1, String consulta, Object[] parametros, String filtro)
        {
            this.grid1.Conexao = campoLookup1.Conexao;
            this.grid1.Consulta = new String[] { consulta += " " + filtro };
            this.grid1.Parametros = parametros;
            return this.MostrarLookup(campoLookup1);
        }

        public bool MostrarLookup(CampoLookup campoLookup1, String filtro)
        {
            this.grid1.Conexao = campoLookup1.Conexao;
            this.grid1.Consulta = new String[] { string.Join(" ", this.grid1.Consulta) + " " + filtro };
            return this.MostrarLookup(campoLookup1);
        }

        public Boolean MostrarLookup(CampoLookup campoLookup1, String consulta, Object[] parametros)
        {
            this.grid1.NomeGrid = campoLookup1.ColunaTabela + "_" + campoLookup1.ColunaCodigo;
            return this.MostrarLookup(campoLookup1, consulta, parametros, "");
        }

        #endregion

        private void FormVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManager.ReleaseUnusedMemory(false);
        }

        public void Pesquisar(String texto)
        {
            grid1.Pesquisar(texto);
        }

        private void grid1_AposSelecao(object sender, EventArgs e)
        {
            if (LancamentosFinanceiros)//splitContainer1.Panel2Collapsed.Equals(true))
            {
                try
                {
                    string sql = String.Format(SQLLancamentoFinanceiros, grid1.GetDataRow()["Pedido"]);

                    gridData1.Consulta = sql.Split(' ');
                        gridData1.Parametros = new Object[] { };
                        //gridData1.Parametros = new Object[] { grid1.GetDataRow()["Pedido"] };
                        
                }
                catch { }


                this.gridData1.Atualizar(false);
                //gridData2.Parametros = new Object[] { grid1.GetDataRow()["CODCFO"] };
                //this.gridData2.Atualizar(false);

            }

        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {

        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            try
            {
                if (grid1.GetDataRows().Count == 1)
                {
                    DataRow SelectRow = gridData1.GetDataRow();

                    DialogResult r = MessageBox.Show("Você deseja excluir o ADC [" + SelectRow["IDLAN"].ToString() + "]?", "Confirmação de exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (r == DialogResult.Yes)
                    {
                        Comissao.MetodosComissao.ExcluirADC(SelectRow["IDLAN"].ToString());
                    }

                }
                else
                {
                    MessageBox.Show("Selecione apenas um registro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
