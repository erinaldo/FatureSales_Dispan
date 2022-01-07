using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AppFatureClient.Classes;
using System.Linq;

namespace AppFatureClient
{
    public partial class FormPedidoCadastro : AppLib.Windows.FormCadastroObject
    {
        // João Pedro Luchiari - 03/01/2018

        public DataRow row;

        public Boolean CopiaDeMovimento = false;
        private List<TITMMOV> Itens = new List<TITMMOV>();
        private List<TITMMOV> ItensBanco = new List<TITMMOV>();
        private Boolean ExcluiuTudo = false;
        private string status;

        public FormPedidoCadastro()
        {
            InitializeComponent();




            this.grid21.GetProcessos().Add("Expandir todos itens", null, ExpandirTodosItens);
            this.grid21.GetProcessos().Add("Aplicar tabela de preço", null, AplicarTabelaPreco);
            //this.GetAnexos().Add("Motivo", null, MostrarMotivo);
            //this.GetAnexos().Add("Fechar Anexo", null, FecharAnexo);

            SetConsulta();
            tabControl1.TabPages.Remove(tabPage3);
        }

        private void FecharAnexo(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage7);
        }

        private void MostrarMotivo(object sender, EventArgs e)
        {

            if (!tabControl1.TabPages.Contains(tabPage7))
            {
                tabControl1.TabPages.Add(tabPage7);
            }
        }

        private void AtualizaValorTributo()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(campoInteiroIDMOV.Get().ToString()))
                {
                    string sql = String.Format(@"SELECT	SUM(VALOR) as 'IPI'
                                                FROM	TTRBMOV
                                                WHERE	TTRBMOV.CODCOLIGADA = 1
                                                AND	TTRBMOV.IDMOV = {0}
                                                AND	TTRBMOV.CODTRB = 'IPI'", campoInteiroIDMOV.Get());
                    decimalIPI.Set(Convert.ToDecimal(MetodosSQL.GetField(sql, "IPI")));

                    sql = String.Format(@"SELECT 
	                                            CASE 
		                                            WHEN (ISNULL(ST.VALOR, 0) - ISNULL(ICMS.VALOR, 0)) < 0 
		                                            THEN 0 
		                                            ELSE (ISNULL(ST.VALOR, 0) - ISNULL(ICMS.VALOR, 0)) 
	                                            END as 'ICMSST'
                                            FROM TMOV,

                                            (SELECT	SUM (TTRBMOV.VALOR) AS VALOR
                                                 FROM	TTRBMOV
                                                 WHERE	TTRBMOV.CODCOLIGADA = 1
                                                 AND	TTRBMOV.IDMOV = {0}
                                                 AND	TTRBMOV.CODTRB = 'ICMSST') ST,

                                            (SELECT	SUM (TTRBMOV.VALOR) AS VALOR
                                                 FROM	TTRBMOV
                                                 WHERE	TTRBMOV.CODCOLIGADA = 1
                                                 AND	TTRBMOV.IDMOV = {0}
                                                 AND	TTRBMOV.CODTRB = 'ICMS') ICMS

                                            WHERE CODCOLIGADA = 1
                                            AND   IDMOV = {0}", campoInteiroIDMOV.Get());
                    decimalICMSST.Set(Convert.ToDecimal(MetodosSQL.GetField(sql, "ICMSST")));
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetConsulta()
        {
            clLocalEstoque.ColunaCodigo = "CODLOC";
            clLocalEstoque.ColunaDescricao = "NOME";
            string sql = String.Format(@"(select CODLOC, NOME from TLOC 
                                                            where CODFILIAL = '1'
                                                            and CODCOLIGADA = '{1}') W", AppLib.Context.Filial, AppLib.Context.Empresa);
            clLocalEstoque.ColunaTabela = sql;

            clLocalEstoque.Set("001");
            clLocalEstoque.textBox1_Leave(null, null);

            clSerie.ColunaCodigo = "SERIE";
            clSerie.ColunaDescricao = "SERIE";
            clSerie.ColunaTabela = String.Format(@"(SELECT 
	                                                    SERIE 
                                                    FROM 
	                                                    TTMVSERIE 
                                                    WHERE 
	                                                    CODCOLIGADA = {0}
                                                    AND CODTMV = '2.1.05') W", AppLib.Context.Empresa);

            sql = String.Format(@"SELECT 
	                                SERIE
                                FROM 
	                                TTMVSERIE 
                                WHERE 
	                                CODCOLIGADA = {0}
                                AND CODTMV = '2.1.05'
                                AND PRINCIPAL = 1", AppLib.Context.Empresa);

            if (String.IsNullOrWhiteSpace(clSerie.textBoxCODIGO.Text))
            {
                clSerie.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "SERIE");
            }

            clSerie.textBox1_Leave(null, null);

            clVendedorInterno.ColunaCodigo = "CODVEN";
            clVendedorInterno.ColunaDescricao = "NOME";
            clVendedorInterno.ColunaTabela = String.Format(@"(SELECT 
	                                                                CODVEN,
	                                                                NOME
                                                                FROM 
	                                                                TVEN 
                                                                WHERE 
	                                                                CODCOLIGADA = {0}
                                                                AND CODFILIAL = 1) W", AppLib.Context.Empresa);
            if (!(this.Acao == AppLib.Global.Types.Acao.Editar))
            {
                sql = String.Format(@"SELECT 
	                                                                COUNT(1) as 'TOTAL'
                                                                FROM 
	                                                                TVEN 
                                                                WHERE 
	                                                                CODCOLIGADA = {0}
                                                                AND CODFILIAL = 1
																and CODVEN = '{1}'", AppLib.Context.Empresa, AppLib.Context.Usuario);
                if (MetodosSQL.GetField(sql, "TOTAL") != "0")
                {
                    clVendedorInterno.textBoxCODIGO.Text = AppLib.Context.Usuario;
                }


            }

            clVendedorInterno.textBox1_Leave(null, null);

            clCondigaoPagamento.ColunaCodigo = "CODCPG";
            clCondigaoPagamento.ColunaDescricao = "NOME";
            clCondigaoPagamento.ColunaTabela = String.Format(@"(SELECT 
	                                                                CODCPG, 
	                                                                NOME 
                                                                FROM 
	                                                                TCPG 
                                                                WHERE 
	                                                                CODCOLIGADA = {0} 
                                                                AND PLANOVENDA = 1 
                                                                AND INATIVO = 0) W", AppLib.Context.Empresa);
        }

        private void FormPedidoCadastro_Load(object sender, EventArgs e)
        {
            try
            {
                campoDecimalVALORFRETE.textEdit1.Leave += ((object sender2, EventArgs e2) => { campoDecimalPERCENTUALFRETE.Set(0); });
                campoDecimalVALORDESC.textEdit1.Leave += ((object sender2, EventArgs e2) => { campoDecimalPERCENTUALDESC.Set(0); });
                campoDecimalVALORDESP.textEdit1.Leave += ((object sender2, EventArgs e2) => { campoDecimalPERCENTUALDESP.Set(0); });

                campoDecimalPERCENTUALFRETE.textEdit1.Leave += ((object sender2, EventArgs e2) => { campoDecimalVALORFRETE.Set(0); });
                campoDecimalPERCENTUALDESC.textEdit1.Leave += ((object sender2, EventArgs e2) => { campoDecimalVALORDESC.Set(0); });
                campoDecimalPERCENTUALDESP.textEdit1.Leave += ((object sender2, EventArgs e2) => { campoDecimalVALORDESP.Set(0); });


                tabControl1.TabPages.Remove(tabPage7);
                tabControl1.TabPages.Remove(tabPage6);

                DataTable dt = MetodosSQL.GetDT("select distinct CODETD from ZREGRACFOP order by CODETD");

                AppLib.Windows.CodigoNome[] a = new AppLib.Windows.CodigoNome[dt.Rows.Count];


                int i = 0;
                foreach (DataRow prod in dt.Rows)
                {
                    a[i] = new AppLib.Windows.CodigoNome(prod["CODETD"].ToString(), prod["CODETD"].ToString());
                    i++;
                }

                campoLista2.Lista = a;

                grid21_Atualizar(this, null);

                if (this.Acao == AppLib.Global.Types.Acao.Editar)
                {

                    if (string.IsNullOrEmpty(campoListaFINANCIADO.Get()))
                    {
                        //campoListaFINANCIADO.comboBox1.SelectedIndex = 0;
                    }
                    if (string.IsNullOrEmpty(campoListaDEMONSBRINDE.Get()))
                    {
                        //campoListaDEMONSBRINDE.comboBox1.SelectedIndex = 0;
                    }
                    if (string.IsNullOrEmpty(campoListaPARAFINANC.Get()))
                    {
                        //campoListaPARAFINANC.comboBox1.SelectedIndex = 0;
                    }
                    status = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT STATUS FROM TMOV WHERE IDMOV = ? AND CODCOLIGADA = ?", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Empresa }).ToString();

                    string sql = String.Format(@"select TIPOVENDA, APLICPROD, NAOCONTRIB, CODETDORC from TMOVCOMPL
                                                  where CODCOLIGADA = {0}
                                                  and IDMOV = {1}", AppLib.Context.Empresa, campoInteiroIDMOV.Get());
                    campoLista1.Set(MetodosSQL.GetField(sql, "TIPOVENDA"));
                    campoListaAplicacao.Set(MetodosSQL.GetField(sql, "APLICPROD"));

                    if (campoLookupCODCFO.Get() == "00002")
                    {
                        campoLista2.Set(MetodosSQL.GetField(sql, "CODETDORC"));
                        cbConsumidorFinal.Checked = MetodosSQL.GetField(sql, "CODETDORC") == "0" ? true : false;
                    }
                    else
                    {
                        sql = String.Format(@"select CODETD from FCFO where CODCFO = '{0}'", campoLookupCODCFO.Get());
                        campoLista2.Set(MetodosSQL.GetField(sql, "CODETD"));
                    }
                }

                if (campoDataEMISSAO.Get() == null)
                    campoDataEMISSAO.Set(DateTime.Now);

                //if (campoListaFRETECIFOUFOB.Get() == null)
                //campoListaFRETECIFOUFOB.Set("2");

                //if (campoLookupCODCPG.Get() == null)
                //campoLookupCODCPG.Set("F001");

                AtualizaValorTributo();

                gridData1.GetProcessos().Add("Editar Item Banco", null, LiberaOrcamentoBanco);

                string campolivre = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT CAMPOLIVRE1 FROM TMOV WHERE IDMOV = ? AND CODCOLIGADA  = ?", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Empresa }).ToString();
                if (string.IsNullOrEmpty(campolivre))
                {
                    //Busca o campo VALPROPOSTA na tabela 
                    //campoTextoCAMPOLIVRE1.Set(AppLib.Context.poolConnection.Get().ExecGetField(null, "SELECT VALPROPOSTA FROM ZPARAMFATURE WHERE CODCOLIGADA = ?", new object[] { AppLib.Context.Empresa }).ToString());
                }


                //DataTable dtBanco = AppLib.Context.poolConnection.Get().ExecQuery("SELECT * FROM ZORCAMENTOBANCO WHERE CODCOLIGADA = ? AND IDMOV = ?", new object[] { AppLib.Context.Empresa, campoInteiroIDMOV.Get() });
                //if (dtBanco.Rows.Count > 0)
                //{
                //    campoDataEmissaoBanco.Set(Convert.ToDateTime(dtBanco.Rows[0]["DATAEMISSAO"]));
                //    campoListaFinanciadoBanco.Set(dtBanco.Rows[0]["FINANCIADO"].ToString());
                //    campoMemoPagamentoBanco.Set(dtBanco.Rows[0]["CONDPGTO"].ToString());
                //}
                //else
                //{
                //    campoDataEmissaoBanco.Set(campoDataEMISSAO.Get());
                //    //campoListaFinanciadoBanco.comboBox1.SelectedIndex = 2;
                //    //campoListaFinanciadoBanco.Set("SIM - SIM");
                //    campoMemoPagamentoBanco.Set(string.Empty);
                //}

                SetConsulta();

                if (CopiaDeMovimento)
                {
                    campoInteiroIDMOV.Set(null);
                    campoDataEMISSAO.Set(DateTime.Now);
                    campoTextoNUMEROMOV.Set(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void LiberaOrcamentoBanco(object sender, EventArgs e)
        {
            if (status != "A")
            {
                if (new AppLib.Security.Access().Processo(gridData1.Conexao, "APP8035", AppLib.Context.Perfil))
                {
                    System.Data.DataRow dr = gridData1.GetDataRow();

                    if (dr == null)
                    {
                        return;
                    }

                    if (MessageBox.Show("Deseja desbloquear a edição dos itens de orçamento?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //AppLib.Windows.FormMessagePrompt frm = new AppLib.Windows.FormMessagePrompt();
                        //frm.ShowDialog();

                        string ValorMotivo = string.Empty;

                        frmMensagemDesbloqueio frm = new frmMensagemDesbloqueio();
                        frm.ShowDialog();
                        ValorMotivo = frm.Motivo;

                        //if (frm.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                        //{
                        AppLib.Context.poolConnection.Get().ExecTransaction("INSERT INTO ZLIBERACAOORCAMENTOBANCO (IDMOV, USUARIO, MOTIVO) VALUES (?, ?, ?)", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Usuario, /*frm.textBox1.Text*/ValorMotivo });
                        status = "A";
                        FormOrcamentoItem f = new FormOrcamentoItem();
                        TITMMOV reg = new TITMMOV();
                        reg.ALIQUOTAIPI = Convert.ToDecimal(dr["ALIQUOTAIPI"]);
                        //reg.CODIGOPRD = dr["CODIGOPRD"].ToString();
                        reg.HISTORICOLONGO = dr["HISTORICOLONGO"].ToString();
                        reg.IDPRD = Convert.ToInt32(dr["IDPRD"]);
                        reg.CODIGOPRD = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT CODIGOPRD FROM TPRODUTO WHERE CODCOLPRD = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(dr["IDPRD"]) }).ToString();
                        reg.PRODUTO = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT NOMEFANTASIA FROM TPRODUTO WHERE CODCOLPRD = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(dr["IDPRD"]) }).ToString();

                        reg.PRECOUNITARIO = Convert.ToDecimal(dr["PRECOUNITARIO"]);

                        reg.QUANTIDADE = Convert.ToDecimal(dr["QUANTIDADE"]);
                        reg.UNIDADE = dr["CODUND"].ToString();

                        f.xTITMMOV = reg;
                        f.AtualizarForm();
                        f.ShowDialog();

                        if (f.acao == AcaoForcada.Salvar)
                        {
                            AppLib.ORM.Jit ZORCAMENTOITEMBANCO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZORCAMENTOITEMBANCO");

                            ZORCAMENTOITEMBANCO.Set("CODCOLIGADA", AppLib.Context.Empresa);
                            ZORCAMENTOITEMBANCO.Set("IDMOV", campoInteiroIDMOV.Get());
                            ZORCAMENTOITEMBANCO.Set("NSEQITMMOV", gridData1.GetDataRow()["NSEQITMMOV"]);
                            ZORCAMENTOITEMBANCO.Set("IDPRD", f.xTITMMOV.IDPRD);
                            ZORCAMENTOITEMBANCO.Set("CODUND", f.xTITMMOV.UNIDADE);
                            ZORCAMENTOITEMBANCO.Set("QUANTIDADE", f.xTITMMOV.QUANTIDADE);
                            ZORCAMENTOITEMBANCO.Set("PRECOUNITARIO", f.xTITMMOV.PRECOUNITARIO);
                            ZORCAMENTOITEMBANCO.Set("ALIQUOTAIPI", f.xTITMMOV.ALIQUOTAIPI);
                            ZORCAMENTOITEMBANCO.Set("HISTORICOLONGO", f.xTITMMOV.HISTORICOLONGO);
                            ZORCAMENTOITEMBANCO.Save();
                        }
                    }
                    //}
                }
            }
        }

        private void campoInteiroPRAZOENTREGA_Leave(object sender, EventArgs e)
        {
            int prazodias = Convert.ToInt32(campoInteiroPRAZOENTREGA.Get());
            campoDataENTREGA.Set(Convert.ToDateTime(campoDataEMISSAO.Get()).AddDays(prazodias));
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

        #region CAMPOS LOOKUP

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            //            Valida v = new Valida();
            //            if (v.IsRepresentante())
            //            {
            //                //return new FormClienteVisao().MostrarLookup(campoLookupCODCFO);
            //                String consulta1 = @"
            //                SELECT CODCFO, ISNULL(NOMEFANTASIA, NOME) NOMEFANTASIA, CGCCFO, CODETD, CIDADE
            //                FROM FCFO
            //                WHERE CODCOLIGADA IN (0, ?)
            //                  AND PAGREC IN (1, 3)
            //                  AND ATIVO = 1";
            //                return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCFO, consulta1, new Object[] { AppLib.Context.Empresa });
            //            }
            //            else
            //            {
            //                //return new FormClienteVisao().MostrarLookup(campoLookupCODCFO);
            //                String consulta1 = @"
            //                SELECT CODCFO, ISNULL(NOMEFANTASIA, NOME) NOMEFANTASIA, CGCCFO, CODETD, CIDADE
            //                FROM FCFO
            //                WHERE CODCOLIGADA IN (0, ?)
            //                  AND PAGREC IN (1, 3)
            //                  AND ATIVO = 1";
            //                return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCFO, consulta1, new Object[] { AppLib.Context.Empresa });
            //            }


            FormClienteVisao f = new FormClienteVisao();
            f.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Todos;
            return f.MostrarLookup(campoLookupCODCFO, "  AND PAGREC IN (1, 3) AND ATIVO = 1 ");
        }

        //private bool campoLookup2_SetFormConsulta(object sender, EventArgs e)
        //{
        //            String consulta1 = @"
        //SELECT CODCPG, NOME
        //FROM TCPG
        //WHERE CODCOLIGADA = ?
        //  AND PLANOVENDA = 1
        //  AND INATIVO = 0";

        //            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCPG, consulta1, new Object[] { AppLib.Context.Empresa });
        //}

        private bool campoLookup3_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"
SELECT CODRPR, ISNULL(NOMEFANTASIA, NOME) NOMEFANTASIA, SIGLA, CGC, CODETD, CIDADE, BAIRRO
FROM TRPR
WHERE CODCOLIGADA = ?
  AND INATIVO = 0";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODRPR, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookup4_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"
SELECT CODTRA, NOME, CGC, CODETD, CIDADE, BAIRRO
FROM TTRA
WHERE CODCOLIGADA = ?
  AND INATIVO = 0";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODTRA, consulta1, new Object[] { AppLib.Context.Empresa });
        }

        private bool campoLookupTPCULTURA_SetFormConsulta(object sender, EventArgs e)
        {
            String Consulta = @"
SELECT CODCLIENTE, DESCRICAO
FROM GCONSIST
WHERE CODCOLIGADA = ?
  AND APLICACAO = 'T'
  AND CODTABELA = 'TPCULTURA'";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupTPCULTURA, Consulta, new Object[] { AppLib.Context.Empresa });
        }

        private void campoLookupTPCULTURA_SetDescricao(object sender, EventArgs e)
        {
            String consulta1 = @"SELECT DESCRICAO FROM GCONSIST WHERE CODCOLIGADA = ? AND APLICACAO = 'T' AND CODTABELA = 'TPCULTURA' AND CODCLIENTE = ?";
            campoLookupTPCULTURA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(Conexao).ExecGetField(null, consulta1, new Object[] { AppLib.Context.Empresa, campoLookupTPCULTURA.textBoxCODIGO.Text }).ToString();
        }

        #endregion

        #region MÉTODOS DA GRID

        private void grid21_Novo(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(campoLookupCODCFO.Get()))
            {
                MessageBox.Show("Selecione um cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (String.IsNullOrWhiteSpace(campoListaAplicacao.Get()))
            {
                MessageBox.Show("Selecione uma aplicação.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                FormOrcamentoItem f = new FormOrcamentoItem();
                f.CODCFO = campoLookupCODCFO.Get();
                f.AplicaProd = campoListaAplicacao.Get();
                f.consumidorFinal = cbConsumidorFinal.Checked;
                f.CODETD = campoLista2.Get();
                //f.Itens = this.Itens;
                f.ShowDialog();

                if (f.acao == AcaoForcada.Salvar)
                {
                    Itens.Add(f.xTITMMOV);
                    this.grid21_Atualizar(this, null);
                }
                this.grid21_Atualizar(this, null);
                if (f.acao == AcaoForcada.Excluir)
                {
                    // ignorar o registro novo
                }
            }

        }

        private void grid21_Editar(object sender, EventArgs e)
        {
            int indice = 0;

            FormOrcamentoItem f = new FormOrcamentoItem();
            System.Data.DataRow dr = grid21.GetDataRow();
            TITMMOV reg = (TITMMOV)grid21.GetObjectRow();
            indice = Itens.IndexOf(reg);
            f.tipoVenda = campoLista1.Get();
            f.edita = true;
            f.CODCFO = campoLookupCODCFO.Get();
            f.CODETD = campoLista2.Get();
            f.AplicaProd = campoListaAplicacao.Get();
            f.xTITMMOV = reg;
            f.AtualizarForm();
            f.IDMOV = Convert.ToInt32(campoInteiroIDMOV.Get());
            f.NSEQITEMMOV = reg.NSEQITEMMOV;
            f.consumidorFinal = cbConsumidorFinal.Checked;
            f.ShowDialog();

            if (f.acao == AcaoForcada.Salvar)
            {
                Itens.RemoveAt(indice);
                Itens.Insert(indice, f.xTITMMOV);




                //Itens.Remove(reg);
                //Itens.Add(f.xTITMMOV);
                grid21_Atualizar(this, null);
            }

            if (f.acao == AcaoForcada.Excluir)
            {
                Itens.Remove(reg);

                if (Itens.Count == 0)
                {
                    ExcluiuTudo = true;
                }

                grid21_Atualizar(this, null);
            }
        }

        private void grid21_Excluir(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir registro selecionado?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }


            List<Object> lista = grid21.GetObjectRows();

            if (MessageBox.Show("A Exclusão deste item removerá toda sua composição.\nDeseja excluir?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    TITMMOV temp = (TITMMOV)lista[i];
                    List<TITMMOV> listRemove = new List<TITMMOV>();



                    Itens.Remove(temp);
                }
            }
            if (Itens.Count == 0)
            {
                ExcluiuTudo = true;
            }

            grid21_Atualizar(this, null);
        }

        private void grid21_Atualizar(object sender, EventArgs e)
        {
            if (!ExcluiuTudo)
            {
                // CARREGAR OS ITENS
                //if (grid21.gridControl1.Views[0].DataRowCount == 0)
                if (Itens.Count == 0)
                {
                    String Comando = "";

                    try
                    {
                        /*
                                                Comando = @"
                        SELECT
                        TITMMOV.NSEQITMMOV,
                        TITMMOV.IDPRD,
                        TITMMOV.IDPRDCOMPOSTO,
                        TPRD.CODIGOPRD,
                        ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
                        TITMMOV.CODUND,
                        TITMMOV.QUANTIDADEORIGINAL QUANTIDADE,
                        TITMMOV.PRECOUNITARIO,
                        TPRD.NUMEROCCF,

                        CASE 
                        WHEN TITMMOVCOMPL.PRODPRICIPAL = 'NÃO' THEN 0
                        WHEN TITMMOVCOMPL.PRODPRICIPAL = 'SIM' THEN 1
                        ELSE NULL
                        END PRODPRICIPAL,

                        TITMMOVCOMPL.PRODPRICIPAL PRODUTO_COMPOSTO,

                        CASE 
                        WHEN TITMMOVCOMPL.TIPOMAT = 0 THEN 'SIM'
                        WHEN TITMMOVCOMPL.TIPOMAT = 1 THEN 'NÃO'
                        ELSE NULL
                        END MATERIAL_INTERLIGACAO,

                        TITMMOVCOMPL.SEQUENCIAL,
                        TITMMOVCOMPL.TIPOMAT,
                        TITMMOVCOMPL.COMPRIMENTO,
                        TITMMOVCOMPL.QTDPAREDECOMP,
                        TITMMOVCOMPL.LARGURA,
                        TITMMOVCOMPL.QTDPAREDELARG,
                        TITMMOVCOMPL.ALTURA,
                        TTRBMOV.ALIQUOTA,
                        TITMMOVHISTORICO.HISTORICOLONGO

                        FROM TITMMOV
                        LEFT JOIN TTRBMOV ON TITMMOV.CODCOLIGADA = TTRBMOV.CODCOLIGADA 
                          AND TITMMOV.IDMOV = TTRBMOV.IDMOV
                          AND TITMMOV.NSEQITMMOV = TTRBMOV.NSEQITMMOV
                          AND TTRBMOV.CODTRB = 'IPI'
                        LEFT JOIN TITMMOVCOMPL ON TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA
                          AND TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV
                          AND TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV
                        LEFT JOIN TITMMOVHISTORICO ON TITMMOV.CODCOLIGADA = TITMMOVHISTORICO.CODCOLIGADA
                          AND TITMMOV.IDMOV = TITMMOVHISTORICO.IDMOV
                          AND TITMMOV.NSEQITMMOV = TITMMOVHISTORICO.NSEQITMMOV,
                          TPRD

                        WHERE TITMMOV.CODCOLIGADA = TPRD.CODCOLIGADA
                          AND TITMMOV.IDPRD = TPRD.IDPRD
                          AND TITMMOV.CODCOLIGADA = ?
                          AND TITMMOV.IDMOV = ?";
                        */
                        //                        Comando = @"
                        //SELECT
                        //TITMMOV.NSEQITMMOV,
                        //TITMMOV.IDPRD,
                        //TITMMOV.IDPRDCOMPOSTO,
                        //TPRD.CODIGOPRD,
                        //ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
                        //TITMMOV.CODUND,
                        //CAST(TITMMOV.QUANTIDADETOTAL AS NUMERIC (15,2)) QUANTIDADE,
                        //CAST(TITMMOV.PRECOUNITARIO AS NUMERIC(15,2)) PRECOUNITARIO,
                        //TPRD.NUMEROCCF,
                        //
                        //CASE 
                        //WHEN TITMMOVCOMPL.PRODPRICIPAL = 'NÃO' THEN 0
                        //WHEN TITMMOVCOMPL.PRODPRICIPAL = 'SIM' THEN 1
                        //ELSE NULL
                        //END PRODPRICIPAL,
                        //
                        //TITMMOVCOMPL.PRODPRICIPAL PRODUTO_COMPOSTO,
                        //
                        //CASE 
                        //WHEN TITMMOVCOMPL.TIPOMAT = 0 THEN 'SIM'
                        //WHEN TITMMOVCOMPL.TIPOMAT = 1 THEN 'NÃO'
                        //ELSE NULL
                        //END MATERIAL_INTERLIGACAO,
                        //
                        //TITMMOVCOMPL.SEQUENCIAL,
                        //TITMMOVCOMPL.TIPOMAT,
                        //TITMMOVCOMPL.COMPRIMENTO,
                        //TITMMOVCOMPL.QTDPAREDECOMP,
                        //TITMMOVCOMPL.LARGURA,
                        //TITMMOVCOMPL.QTDPAREDELARG,
                        //TITMMOVCOMPL.ALTURA,
                        //CASE TITMMOVCOMPL.PRDPADRAO WHEN 1 THEN 'SIM' WHEN 2 THEN 'NÃO' END PRDPADRAO,
                        //TITMMOVCOMPL.OBSPRDPADRAO,
                        //CAST(ISNULL(TTRBMOV.ALIQUOTA,0) AS NUMERIC (15,2)) ALIQUOTA,
                        //TITMMOVHISTORICO.HISTORICOLONGO
                        //
                        //FROM TITMMOV
                        //LEFT JOIN TTRBMOV ON TITMMOV.CODCOLIGADA = TTRBMOV.CODCOLIGADA 
                        //  AND TITMMOV.IDMOV = TTRBMOV.IDMOV
                        //  AND TITMMOV.NSEQITMMOV = TTRBMOV.NSEQITMMOV
                        //  AND TTRBMOV.CODTRB = 'IPI'
                        //LEFT JOIN TITMMOVCOMPL ON TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA
                        //  AND TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV
                        //  AND TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV
                        //LEFT JOIN TITMMOVHISTORICO ON TITMMOV.CODCOLIGADA = TITMMOVHISTORICO.CODCOLIGADA
                        //  AND TITMMOV.IDMOV = TITMMOVHISTORICO.IDMOV
                        //  AND TITMMOV.NSEQITMMOV = TITMMOVHISTORICO.NSEQITMMOV,
                        //  TPRD
                        //
                        //WHERE TITMMOV.CODCOLIGADA = TPRD.CODCOLIGADA
                        //  AND TITMMOV.IDPRD = TPRD.IDPRD
                        //  AND TITMMOV.CODCOLIGADA = ?
                        //  AND TITMMOV.IDMOV = ?";

                        Comando = @"SELECT
TITMMOV.NSEQITMMOV,
TITMMOV.IDPRD,
TPRD.CODIGOAUXILIAR,
TPRD.CODIGOPRD,
ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
TITMMOV.CODUND,
CAST(TITMMOV.QUANTIDADETOTAL AS NUMERIC (15,2)) QUANTIDADE,
CAST(TITMMOV.PRECOUNITARIO AS NUMERIC(15,2)) PRECOUNITARIO,
TPRD.NUMEROCCF,



CAST(ISNULL(TTRBMOV.ALIQUOTA,0) AS NUMERIC (15,2)) ALIQUOTA,
TITMMOVHISTORICO.HISTORICOLONGO,
TITMMOV.PRECOUNITARIOSELEC,

TITMMOVCOMPL.APLICPROD,
TITMMOV.IDNAT,
TITMMOV.DATAENTREGA

FROM TITMMOV
LEFT JOIN TTRBMOV ON TITMMOV.CODCOLIGADA = TTRBMOV.CODCOLIGADA 
  AND TITMMOV.IDMOV = TTRBMOV.IDMOV
  AND TITMMOV.NSEQITMMOV = TTRBMOV.NSEQITMMOV
  AND TTRBMOV.CODTRB = 'IPI'
LEFT JOIN TITMMOVCOMPL ON TITMMOV.CODCOLIGADA = TITMMOVCOMPL.CODCOLIGADA
  AND TITMMOV.IDMOV = TITMMOVCOMPL.IDMOV
  AND TITMMOV.NSEQITMMOV = TITMMOVCOMPL.NSEQITMMOV
LEFT JOIN TITMMOVHISTORICO ON TITMMOV.CODCOLIGADA = TITMMOVHISTORICO.CODCOLIGADA
  AND TITMMOV.IDMOV = TITMMOVHISTORICO.IDMOV
  AND TITMMOV.NSEQITMMOV = TITMMOVHISTORICO.NSEQITMMOV,
  TPRD

WHERE TITMMOV.CODCOLIGADA = TPRD.CODCOLIGADA
  AND TITMMOV.IDPRD = TPRD.IDPRD
  AND TITMMOV.CODCOLIGADA = ?
  AND TITMMOV.IDMOV = ?";

                        Comando = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(Comando, new Object[] { AppLib.Context.Empresa, campoInteiroIDMOV.Get() });
                        System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Comando, new Object[] { });

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TITMMOV titmmov = new TITMMOV();

                            titmmov.IDPRD = int.Parse(dt.Rows[i]["IDPRD"].ToString());

                            titmmov.CODIGOPRD = dt.Rows[i]["CODIGOPRD"].ToString();
                            titmmov.CODAUXILIAR = dt.Rows[i]["CODIGOAUXILIAR"].ToString();
                            titmmov.PRODUTO = dt.Rows[i]["PRODUTO"].ToString();
                            titmmov.UNIDADE = dt.Rows[i]["CODUND"].ToString();
                            titmmov.QUANTIDADE = decimal.Parse(dt.Rows[i]["QUANTIDADE"].ToString());
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[i]["PRECOUNITARIO"].ToString());
                            titmmov.VALORTOTAL = Convert.ToDecimal(string.Format("{0:n2}", titmmov.QUANTIDADE * titmmov.PRECOUNITARIO));
                            titmmov.ALIQUOTAIPI = decimal.Parse(dt.Rows[i]["ALIQUOTA"].ToString());
                            titmmov.HISTORICOLONGO = dt.Rows[i]["HISTORICOLONGO"].ToString();

                            titmmov.NUMEROCCF = dt.Rows[i]["NUMEROCCF"].ToString();
                            //titmmov.PRODUTO_COMPOSTO = dt.Rows[i]["PRODUTO_COMPOSTO"].ToString();
                            //titmmov.MATERIAL_INTERLIGACAO = dt.Rows[i]["MATERIAL_INTERLIGACAO"].ToString();
                            //titmmov.PRDPADRAO = dt.Rows[i]["PRDPADRAO"].ToString();
                            //titmmov.PRDREVENDA = dt.Rows[i]["PRDREVENDA"].ToString();
                            //titmmov.OBSPRDPADRAO = dt.Rows[i]["OBSPRDPADRAO"].ToString();
                            titmmov.PRECOUNITARIOSELEC = dt.Rows[i]["PRECOUNITARIOSELEC"].ToString();
                            titmmov.APLICACAO = dt.Rows[i]["APLICPROD"].ToString();
                            if (!String.IsNullOrWhiteSpace(dt.Rows[i]["IDNAT"].ToString()))
                            {
                                titmmov.IDNAT = int.Parse(dt.Rows[i]["IDNAT"].ToString());
                            }

                            titmmov.NSEQITEMMOV = Convert.ToInt32(dt.Rows[i]["NSEQITMMOV"]);
                            if (!String.IsNullOrWhiteSpace(dt.Rows[i]["DATAENTREGA"].ToString()))
                            {
                                titmmov.DATAENTREGA = (DateTime)dt.Rows[i]["DATAENTREGA"];
                            }

                            Comando = String.Format(@"select * from ZORCAMENTOITEMCOMPOSTO 

                                                        inner join TPRODUTO TP
                                                        on TP.IDPRD = ZORCAMENTOITEMCOMPOSTO.IDPRD

                                                        where CODCOLIGADA = 1 and CODFILIAL = 1 and CODCOLPRD = 1 and IDMOV = '{0}' and NSEQ = '{1}'",
                                                        campoInteiroIDMOV.Get(), titmmov.NSEQITEMMOV);

                            DataTable itemComposicao = MetodosSQL.GetDT(Comando);
                            List<ItemComposicao> it = new List<ItemComposicao>();

                            if (itemComposicao.Rows.Count > 0)
                            {
                                foreach (DataRow itc in itemComposicao.Rows)
                                {
                                    int o;
                                    ItemComposicao itemComposto = new ItemComposicao();
                                    itemComposto.CODCOLIGADA = 1;
                                    itemComposto.CODFILIAL = 1;
                                    itemComposto.IDMOV = int.TryParse(itc["IDMOV"].ToString(), out o) ? int.Parse(itc["IDMOV"].ToString()) : 0;
                                    itemComposto.CODIGOPRD = (String)itc["CODIGOPRD"];
                                    itemComposto.CODIGOAUXILIAR = (String)itc["CODIGOAUXILIAR"];
                                    itemComposto.IDPRD = (int)itc["IDPRD"];
                                    itemComposto.NOMEFANTASIA = (String)itc["NOMEFANTASIA"];
                                    itemComposto.NSEQ = (int)itc["NSEQ"];
                                    itemComposto.QUANTIDADE = (decimal)itc["QUANTIDADE"];
                                    itemComposto.PRECOUNITARIO = (decimal)itc["PRECOUNITARIO"];
                                    //itemComposto.UNIDADEMEDIDA = (String)itc["UNIDADEMEDIDA"];

                                    it.Add(itemComposto);
                                }
                            }


                            titmmov.COMPOSICAO = it;

                            //if (!dt.Rows[i]["PRODPRICIPAL"].ToString().Equals(""))
                            //{
                            //    titmmov.PRODUTOCOMPOSTO = (dt.Rows[i]["PRODPRICIPAL"].ToString() == "NÃO" || dt.Rows[i]["PRODPRICIPAL"].ToString() == "0") ? 0 : 1;
                            //}

                            //titmmov.PROJETO = dt.Rows[i]["SEQUENCIAL"].ToString();

                            //if (!dt.Rows[i]["TIPOMAT"].ToString().Equals(""))
                            //{
                            //    titmmov.MATERIALINTERLIGACAO = int.Parse(dt.Rows[i]["TIPOMAT"].ToString());
                            //}

                            Itens.Add(titmmov);
                        }
                    }
                    catch (Exception ex)
                    {
                        new AppLib.Windows.FormExceptionSQL().Mostrar("Erro ao atualizar a grid. Processo de carregar itens do movimento para a grid de edição.", Comando, ex);
                    }
                }
            }

            grid21.gridControl1.DataSource = null;
            grid21.gridControl1.DataSource = Itens;

            // TRATAMENTO ESPECIAL PARA ESTE OBTER COMPONENTE HERDADO DE UM USER CONTROL
            DevExpress.XtraGrid.Views.Grid.GridView gridView1 = (DevExpress.XtraGrid.Views.Grid.GridView)grid21.gridControl1.Views[0];
            // FIM DO TRATAMENTO ESPECIAL

            // Tratamentos para a grid
            // Grid somente leitura
            gridView1.OptionsBehavior.Editable = false;

            // Grid com seleção de multiplos registros
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

            // Grid com scroll horizontal
            gridView1.OptionsView.ColumnAutoWidth = false;

            // Grid com auto ajuste do melhor tamanho das colunas
            gridView1.BestFitMaxRowCount = 800;
            gridView1.BestFitColumns();

            // Grid mostrar coluna de filtro
            gridView1.OptionsView.ShowAutoFilterRow = true;

            //Desabilita Colunas PRODUTOCOMPOSTO e MATERIALINTERLIGACAO
            DevExpress.XtraGrid.Views.Grid.GridView oGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grid21.gridControl1.Views[0];


            // Grid sempre mostrar filtro no rodapé
            // gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;

            // Grid zebrada
            // gridView1.OptionsView.EnableAppearanceEvenRow = true;
            // gridView1.Appearance.EvenRow.BackColor = Color.Thistle;

        }

        #endregion

        #region PROCESSOS

        public void AplicarTabelaPreco(object sender, EventArgs e)
        {
            FormAplicarTabelaPreco f = new FormAplicarTabelaPreco();
            f.ShowDialog();

            if (f.TabelaPreco == null)
            {
                // ignorar
            }
            else
            {
                for (int i = 0; i < grid21.GetObjectRows().Count; i++)
                {
                    TITMMOV titmmov = (TITMMOV)grid21.GetObjectRows()[i];

                    String Consulta = @"
SELECT
ISNULL(PRECO1, 0) PRECO1,
ISNULL(PRECO2, 0) PRECO2,
ISNULL(PRECO3, 0) PRECO3,
ISNULL(PRECO4, 0) PRECO4,
ISNULL(PRECO5, 0) PRECO5
FROM TPRD
WHERE CODCOLIGADA = ?
  AND IDPRD = ?";

                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Consulta, new Object[] { AppLib.Context.Empresa, titmmov.IDPRD });

                    if (dt.Rows.Count > 0)
                    {
                        if (f.TabelaPreco.Equals("1"))
                        {
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO1"].ToString());
                            titmmov.VALORTOTAL = titmmov.QUANTIDADE * titmmov.PRECOUNITARIO;
                            titmmov.PRECOUNITARIOSELEC = "1";
                        }

                        if (f.TabelaPreco.Equals("2"))
                        {
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO2"].ToString());
                            titmmov.VALORTOTAL = titmmov.QUANTIDADE * titmmov.PRECOUNITARIO;
                            titmmov.PRECOUNITARIOSELEC = "2";
                        }

                        if (f.TabelaPreco.Equals("3"))
                        {
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO3"].ToString());
                            titmmov.VALORTOTAL = titmmov.QUANTIDADE * titmmov.PRECOUNITARIO;
                            titmmov.PRECOUNITARIOSELEC = "3";
                        }

                        if (f.TabelaPreco.Equals("4"))
                        {
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO4"].ToString());
                            titmmov.VALORTOTAL = titmmov.QUANTIDADE * titmmov.PRECOUNITARIO;
                            titmmov.PRECOUNITARIOSELEC = "4";
                        }

                        if (f.TabelaPreco.Equals("5"))
                        {
                            titmmov.PRECOUNITARIO = decimal.Parse(dt.Rows[0]["PRECO5"].ToString());
                            titmmov.VALORTOTAL = titmmov.QUANTIDADE * titmmov.PRECOUNITARIO;
                            titmmov.PRECOUNITARIOSELEC = "5";
                        }
                    }
                }

                grid21_Atualizar(this, null);
            }
        }

        public void ExpandirTodosItens(object sender, EventArgs e)
        {
            // varre os itens
            for (int i = 0; i < Itens.Count; i++)
            {
                TITMMOV titmmov = Itens[i];

            }

            this.grid21_Atualizar(this, null);
        }

        private System.Data.DataTable composicaoProduto(int idprd)
        {
            String Consulta = @"
SELECT
TPRDCOMPOSTO.IDPRDCOMPONENTE IDPRD,
TPRDCOMPOSTO.IDPRD IDPRDCOMPOSTO,
TPRD.CODIGOPRD,
ISNULL(TPRD.DESCRICAO, TPRD.NOMEFANTASIA) PRODUTO,
TPRDCOMPOSTO.CODUND,
TPRDCOMPOSTO.QUANTIDADE,
ISNULL(TTRBPRD.ALIQUOTA, 0) ALIQUOTA,
TPRD.NUMEROCCF,
'' as PRECOUNITARIOSELEC

FROM TPRDCOMPOSTO
LEFT JOIN TTRBPRD ON TPRDCOMPOSTO.CODCOLIGADA = TTRBPRD.CODCOLIGADA 
  AND TPRDCOMPOSTO.IDPRDCOMPONENTE = TTRBPRD.IDPRD
  AND TTRBPRD.CODTRB = 'IPI', 
  TPRD

WHERE TPRDCOMPOSTO.CODCOLIGADA = TPRD.CODCOLIGADA
  AND TPRDCOMPOSTO.IDPRDCOMPONENTE = TPRD.IDPRD

  AND TPRDCOMPOSTO.CODCOLIGADA = ?
  AND TPRDCOMPOSTO.IDPRD = ?

ORDER BY TPRDCOMPOSTO.NUMEROSEQUENCIAL";

            return AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Consulta, new Object[] { AppLib.Context.Empresa, idprd });
        }

        private Boolean produtoAcabado(String ProdutoDigitado, String ProdutoComposicao)
        {
            if (ProdutoDigitado.Substring(4).Equals(ProdutoComposicao.Substring(4)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region MANIPULAÇÃO DO REGISTRO

        public Boolean Validar()
        {
            if (campoLookupCODCFO.Get() == null)
            {
                MessageBox.Show("Campo Cliente obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoDataEMISSAO.Get() == null)
            {
                MessageBox.Show("Data de Emissão obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoDataENTREGA.Get() == null)
            {
                MessageBox.Show("Data de Entrega obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoInteiroPRAZOENTREGA.Get() == null)
            {
                MessageBox.Show("Prazo de Entrega obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoInteiroPRAZOENTREGA.Get() < 0)
            {
                MessageBox.Show("Data de Entrega deve ser maior ou igual a Data de Emissão.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //if (campoLookupCODRPR.Get() == null)
            //{
            //    if (string.IsNullOrEmpty(campoLookupCODRPR.Get()))
            //    {
            //        MessageBox.Show("Representante obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return false;
            //    }
            //}

            if (campoLista1.Get() == null)
            {
                MessageBox.Show("Tipo de Venda obrigatório.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (campoTextoNUMEROMOV.Get() != null)
            {
                if (campoTextoNUMEROMOV.MaximoCaracteres != null)
                {
                    if (campoTextoNUMEROMOV.Get().Length > campoTextoNUMEROMOV.MaximoCaracteres)
                    {
                        MessageBox.Show("Campo Númedo do Movimento excedeu o limite de " + campoTextoNUMEROMOV.MaximoCaracteres + " caracteres.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            if (campoTextoNORDEM.Get() != null)
            {
                if (campoTextoNORDEM.MaximoCaracteres != null)
                {
                    if (campoTextoNORDEM.Get().Length > campoTextoNORDEM.MaximoCaracteres)
                    {
                        MessageBox.Show("Campo Pedido do Cliente excedeu o limite de " + campoTextoNORDEM.MaximoCaracteres + " caracteres.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            if (campoTexto1.Get() != null)
            {
                if (campoTexto1.MaximoCaracteres != null)
                {
                    if (campoTexto1.Get().Length > campoTexto1.MaximoCaracteres)
                    {
                        MessageBox.Show("Campo Segundo Número excedeu o limite de " + campoTexto1.MaximoCaracteres + " caracteres.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            if (campoTextoOBSERVACAO.Get() != null)
            {
                if (campoTextoOBSERVACAO.MaximoCaracteres != null)
                {
                    if (campoTextoOBSERVACAO.Get().Length > campoTextoOBSERVACAO.MaximoCaracteres)
                    {
                        MessageBox.Show("Campo Observação excedeu o limite de " + campoTextoOBSERVACAO.MaximoCaracteres + " caracteres.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            if (campoMemoPAGAMENTO.Get() != null)
            {
                if (campoMemoPAGAMENTO.MaximoCaracteres != null)
                {
                    if (campoMemoPAGAMENTO.Get().Length > campoMemoPAGAMENTO.MaximoCaracteres)
                    {
                        MessageBox.Show("Campo Pagamento excedeu o limite de " + campoMemoPAGAMENTO.MaximoCaracteres + " caracteres.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            //if (String.IsNullOrEmpty(campoListaFINANCIADO.Get()))
            //{
            //    MessageBox.Show("Selecione uma opção para o campo Financiado.");
            //    return false;
            //}

            //if (String.IsNullOrEmpty(campoListaDEMONSBRINDE.Get()))
            //{
            //    MessageBox.Show("Selecione uma opção para o campo Demonstração ou Brinde.");
            //    return false;
            //}

            //if (String.IsNullOrEmpty(campoListaPARAFINANC.Get()))
            //{
            //    MessageBox.Show("Selecione uma opção para o campo Orçamento para Financiamento.");
            //    return false;
            //}

            for (int i = 0; i < Itens.Count; i++)
            {
                TITMMOV item = Itens[i];



            }

            if (Itens.Count <= 0)
            {
                MessageBox.Show("Movimento não possui itens.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int cont = 0;
            for (int i = 0; i < Itens.Count; i++)
            {

            }
            if (cont > 1)
            {
                MessageBox.Show("Existe mais de um item com o padrão de revenda.");
                return false;
            }

            if (campoLista1.Get().ToString() == "")
            {
                MessageBox.Show("Selecione uma opção para o campo Tipo de Venda.");
                return false;
            }

            //int retorno = Convert.ToInt32(AppLib.Context.poolConnection.Get().ExecGetField(0, "SELECT COUNT(*) FROM TMOVCOMPL INNER JOIN TMOV ON TMOVCOMPL.IDMOV = TMOV.IDMOV AND TMOVCOMPL.CODCOLIGADA = TMOV.CODCOLIGADA WHERE TMOV.CODCFO = ? AND TMOV.IDMOV <> ?", new object[] { campoTexto2.Get(), campoLookupCODCFO.Get(), campoInteiroIDMOV.Get() }));
            //if (retorno > 0)
            //{
            //    MessageBox.Show("Não é permitido incluir orçamento com código de produto repetido para a revenda selecionada!");
            //    return false;
            //}

            string ItemInativo = String.Empty;
            foreach (TITMMOV x in Itens)
            {
                string sql = String.Format(@"select INATIVO from TPRODUTO where CODCOLPRD = '1' and IDPRD = '{0}'", x.IDPRD);
                bool Inativo = MetodosSQL.GetField(sql, "INATIVO") == "1";

                if (Inativo)
                {
                    ItemInativo += String.Format(@"{0} - {1} {2}", x.CODIGOPRD, x.PRODUTO, Environment.NewLine);
                }
            }

            if (!String.IsNullOrWhiteSpace(ItemInativo))
            {
                string msg = String.Format(@"Existen itens inativos inseridos no orçamento. {0}{1}{2}", Environment.NewLine, Environment.NewLine, ItemInativo);
                MessageBox.Show(msg, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (row["STATUS"].ToString() == "FATURADO")
            {
                MessageBox.Show("Não é permitido editar orçamento 'faturado'.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //this.BotaoSalvar = false;
                //this.BotaoOK = false;
                return false;
            }



            return true;
        }

        private bool FormPedidoCadastro_Validar(object sender, EventArgs e)
        {
            return this.Validar();
        }

        private void FormPedidoCadastro_Preparar(object sender, EventArgs e)
        {
            this.ExpandirTodosItens(this, null);
        }

        private void FormPedidoCadastro_Excluir2(object sender, EventArgs e)
        {
            MessageBox.Show("Operação não permitida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private Boolean FormPedidoCadastro_Salvar2(object sender, EventArgs e)
        {

            string sPoint = string.Empty;


            try
            {


                this.Cursor = Cursors.WaitCursor;

                Guid oGuid = Guid.NewGuid();
                string sSql = string.Empty;

                AppInterop.MovMovimentoPar movimento = new AppInterop.MovMovimentoPar();

                sPoint = "Camada: [Client], Campo: [Código da Coligada]";
                movimento.CodColigada = AppLib.Context.Empresa;

                sPoint = "Camada: [Client], Campo: [Identificador do Movimento]";
                if (campoInteiroIDMOV.Get() != null)
                {
                    movimento.IdMov = int.Parse(campoInteiroIDMOV.Get().ToString());
                }

                sPoint = "Camada: [Client], Campo: [GuidId]";
                movimento.GuidId = oGuid.ToString();
                sPoint = "Camada: [Client], Campo: [Tipo de Movimento]";
                movimento.CodTMv = "2.1.03";
                sPoint = "Camada: [Client], Campo: [Filial]";
                movimento.CodFilial = 1;
                sPoint = "Camada: [Client], Campo: [Local de Estoque]";
                movimento.CodLoc = "001";
                sPoint = "Camada: [Client], Campo: [Coligada do Cliente]";
                movimento.CodColCfo = 0;
                sPoint = "Camada: [Client], Campo: [Código do Cliente]";
                movimento.CodCfo = campoLookupCODCFO.Get();
                sPoint = "Camada: [Client], Campo: [Local de estoque]";
                movimento.CodLoc = clLocalEstoque.Get();
                sPoint = "Camada: [Client], Campo: [Aplicação]";
                movimento.APLICPROD = campoListaAplicacao.Get();
                sPoint = "Camada: [Client], Campo: [Série]";
                movimento.Serie = clSerie.Get();
                sPoint = "Camada: [Client], Campo: [Vendedor interno]";
                movimento.CodVen1 = clVendedorInterno.Get();
                sPoint = "Camada: [Client], Campo: [Condição de pagamento]";
                movimento.CodCondicaoPagamento = clCondigaoPagamento.Get();
                sPoint = "Camada: [Client], Campo: [Data de Emissão]";
                movimento.DataEmissao = campoDataEMISSAO.Get();
                sPoint = "Camada: [Client], Campo: [Data de Entrega]";
                movimento.DataEntrega = campoDataENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [Prazo de Entrega]";
                movimento.PrazoEntrega = campoInteiroPRAZOENTREGA.Get();
                sPoint = "Camada: [Client], Campo: [Representante]";
                movimento.CodRepresentante = campoLookupCODRPR.Get();
                sPoint = "Camada: [Client], Campo: [Frete]";
                movimento.FreteCIFouFOB = int.Parse(campoListaFRETECIFOUFOB.Get());
                //= int.Parse(campoListaFRETECIFOUFOB.Get());
                sPoint = "Camada: [Client], Campo: [Transportadora]";
                movimento.CodTra = campoLookupCODTRA.Get();
                sPoint = "Camada: [Client], Campo: [Condição de Pagamento]";
                movimento.CodCondicaoPagamento = "F001";
                sPoint = "Camada: [Client], Campo: [Tabelas de Faturamento]";
                movimento.CodTb1Opcional = clTabelaFaturamento.Get();
                sPoint = "Camada: [Client], Campo: [UF Cliente]";
                movimento.UFORCAMENTO = campoLista2.Get();
                sPoint = "Camada: [Client], Campo: [Consumidor Final]";
                movimento.NCONTRIB = cbConsumidorFinal.Checked ? 0 : 1;


                sPoint = "Camada: [Client], Campo: [Percentual de Frete]";
                movimento.PercentualFrete = decimal.Parse(campoDecimalPERCENTUALFRETE.Get().ToString());
                sPoint = "Camada: [Client], Campo: [Valor de Frete]";
                movimento.ValorFrete = decimal.Parse(campoDecimalVALORFRETE.Get().ToString());
                sPoint = "Camada: [Client], Campo: [Percentual de Desconto]";
                movimento.PercentualDesc = decimal.Parse(campoDecimalPERCENTUALDESC.Get().ToString());
                sPoint = "Camada: [Client], Campo: [Valor de Desconto]";
                movimento.ValorDesc = decimal.Parse(campoDecimalVALORDESC.Get().ToString());
                sPoint = "Camada: [Client], Campo: [Percentual de Despesa]";
                movimento.PercentualDesp = decimal.Parse(campoDecimalPERCENTUALDESP.Get().ToString());
                sPoint = "Camada: [Client], Campo: [Valor de Despesa]";
                movimento.ValorDesp = decimal.Parse(campoDecimalVALORDESP.Get().ToString());
                //sPoint = "Camada: [Client], Campo: [Percentual de Seguro]";
                //movimento.PercentualSeguro = decimal.Parse(campoDecimalPERCENTUALSEGURO.Get().ToString());
                //sPoint = "Camada: [Client], Campo: [Valor do Seguro]";
                //movimento.ValorSeguro = decimal.Parse(campoDecimalVALORSEGURO.Get().ToString());
                //sPoint = "Camada: [Client], Campo: [Percentual Extra 1]";
                //movimento.PercentualExtra1 = decimal.Parse(campoDecimalPERCENTUALEXTRA1.Get().ToString());
                //sPoint = "Camada: [Client], Campo: [Valor do Extra 1]";
                //movimento.ValorExtra1 = decimal.Parse(campoDecimalVALOREXTRA1.Get().ToString());

                sPoint = "Camada: [Client], Campo: [Observação]";
                movimento.Observacao = campoTextoOBSERVACAO.Get();
                sPoint = "Camada: [Client], Campo: [Campo Livre 1]";
                movimento.CampoLivre1 = campoTextoCAMPOLIVRE1.Get();
                sPoint = "Camada: [Client], Campo: [Histórico Longo]";
                movimento.HistoricoLongo = campoMemoHISTORICOLONGO.Get();
                sPoint = "Camada: [Client], Campo: [Numero da Ordem]";
                movimento.NumeroOrdem = campoTextoNORDEM.Get();
                sPoint = "Camada: [Client], Campo: [Segundo Numero]";
                movimento.SegundoNumero = campoTexto1.Get();
                sPoint = "Camada: [Client], Campo: [Usuário]";
                movimento.CodUsuario = AppLib.Context.Usuario;
                sPoint = "Camada: [Client], Campo: [Usuário Logado]";
                movimento.CodUsuarioLogado = AppLib.Context.Usuario;
                sPoint = "Camada: [Client], Campo: [Data de Criação]";
                movimento.DataCriacao = DateTime.Now;
                sPoint = "Camada: [Client], Campo: [Data do Movimento]";
                movimento.DataMovimento = DateTime.Now;

                sPoint = string.Concat("Identificador do Movimento do Cliente]");
                sSql = @"SELECT MAX(IDHISTORICO) IDMOVCFO FROM FCFOHISTORICO WHERE CODCOLIGADA = ? AND CODCFO = ?";
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { movimento.CodColCfo, movimento.CodCfo });

                if (dt.Rows.Count > 0)
                    movimento.IdMovCfo = Convert.ToInt32(dt.Rows[0]["IDMOVCFO"].ToString());
                else
                    movimento.IdMovCfo = null;

                //Campos Complementares
                sPoint = "Camada: [Client], Campo: [Compl Mensagem do Fornecedor]";
                movimento.MSGFORNEC = campoMemoMSGFORNEC.Get();
                sPoint = "Camada: [Client], Campo: [Compl Condição de Pagamento]";
                movimento.CONDPGTO = campoMemoPAGAMENTO.Get();
                sPoint = "Camada: [Client], Campo: [Compl Tipo de Cultura]";
                movimento.TPCULTURA = campoLookupTPCULTURA.Get();
                sPoint = "Camada: [Client], Campo: [Compl Financiado]";
                movimento.FINANCIADO = campoListaFINANCIADO.Get();
                sPoint = "Camada: [Client], Campo: [Compl Demonstração/Brinde]";
                movimento.DEMONSBRINDE = campoListaDEMONSBRINDE.Get();
                sPoint = "Camada: [Client], Campo: [Compl Orçamento para Financiamento]";
                movimento.PARAFINANC = campoListaPARAFINANC.Get();
                sPoint = "Camada: [Client], Campo: [Compl Tipo Venda]";
                movimento.TIPOVENDA = campoLista1.Get();

                //movimento.DESCRICAOPRDREVENDA = campoTexto3.textEdit1.Text;


                List<String> queryItemComposto = new List<string>();

                queryItemComposto.Add(String.Format(@"delete from ZORCAMENTOITEMCOMPOSTO where CODCOLIGADA = 1 and CODFILIAL = 1 and IDMOV = '{0}'", String.IsNullOrWhiteSpace(campoInteiroIDMOV.Get().ToString()) ? oGuid.ToString() : campoInteiroIDMOV.Get().ToString()));

                List<AppInterop.MovMovimentoItemPar> lMovMovimentoItemPar = new List<AppInterop.MovMovimentoItemPar>();
                for (int i = 0; i < Itens.Count; i++)
                {
                    TITMMOV item = new TITMMOV();
                    item = Itens[i];



                    AppInterop.MovMovimentoItemPar itmmov = new AppInterop.MovMovimentoItemPar();

                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Código da Coligada");
                    itmmov.CodColigada = movimento.CodColigada;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Seq.");
                    itmmov.NSeqItmMov = (i + 1);
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Identificador do Produto");
                    itmmov.IdPrd = item.IDPRD;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Unidade de Medida");
                    itmmov.CodUnd = item.UNIDADE;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Quantidade");
                    itmmov.Quantidade = item.QUANTIDADE;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Preço Unitário");
                    itmmov.PrecoUnitario = item.PRECOUNITARIO;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Tabela de preço");
                    itmmov.PRECOUNITARIOSELEC = item.PRECOUNITARIOSELEC;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Aliquota do IPI");
                    itmmov.AliquotaIPI = item.ALIQUOTAIPI;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Histórico Longo");
                    itmmov.HistoricoLongo = item.HISTORICOLONGO;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " IDNAT");
                    item.IDNAT = item.IDNAT == 0 ? ItensOrcamento.getIDNAT(ItensOrcamento.getCFOP(campoLookupCODCFO.Get(), item.APLICACAO, item.CODIGOPRD, cbConsumidorFinal.Checked ? "1" : "0")) : item.IDNAT;
                    itmmov.IDNAT = item.IDNAT;
                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Aplicação do produto");
                    itmmov.APLICAPROD = item.APLICACAO;

                    //Campos Complementares

                    sPoint = string.Concat("Camada: [Client], Campo: [Item ", (i + 1), " Data Entrega");
                    itmmov.DATAENTREGA = item.DATAENTREGA;
                    sPoint = string.Concat("Item ", (i + 1), " Registra ZTITMMOVORCTEMP");
                    sSql = @"INSERT INTO ZTITMMOVORCTEMP(ROWID, CODCOLIGADA, NSEQITMMOV, IDPRD, IDPRDCOMPOSTO, CODUND, QUANTIDADE, PRECOUNITARIO, ALIQUOTAIPI, 
                                HISTORICOLONGO, PRODPRICIPAL, SEQUENCIAL, TIPOMAT, PRDPADRAO, OBSPRDPADRAO, PRDREVENDA, PRECOUNITARIOSELEC, DATAENTREGA, IDNAT, APLICPROD) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?, ?,?, ?, ?, ?)";

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(sSql, new object[] { oGuid, itmmov.CodColigada, itmmov.NSeqItmMov, itmmov.IdPrd,
                        itmmov.IdPrdComposto, itmmov.CodUnd, itmmov.Quantidade, itmmov.PrecoUnitario, itmmov.AliquotaIPI, itmmov.HistoricoLongo,
                        itmmov.PRODPRICIPAL, itmmov.SEQUENCIAL, itmmov.TIPOMAT, itmmov.PRDPADRAO, itmmov.OBSPRDPADRAO, itmmov.PRDREVENDA, itmmov.PRECOUNITARIOSELEC, item.DATAENTREGA, itmmov.IDNAT, itmmov.APLICAPROD});




                    foreach (ItemComposicao itmComposicao in item.COMPOSICAO)
                    {
                        sSql = String.Format(@"insert into ZORCAMENTOITEMCOMPOSTO values (/*CODCOLIGADA*/ {0},
	                                                                                           /*CODFILIAL*/ {1}, 
	                                                                                           /*IDMOV*/ '{2}',
	                                                                                           /*NSEQ*/ {3},
	                                                                                           /*IDPRD*/ {4},
	                                                                                           /*QUANTIDADE*/ {5},
	                                                                                           /*UNIDADEMEDIDA*/ '{6}',
	                                                                                           /*PRECOUNITARIO*/ {7})",
                                                                                               /*CODCOLIGADA*/ itmComposicao.CODCOLIGADA,
                                                                                               /*CODFILIAL*/ itmComposicao.CODFILIAL,
                                                                                               /*IDMOV*/ oGuid,
                                                                                               /*NSEQ*/ itmmov.NSeqItmMov,
                                                                                               /*IDPRD*/ itmComposicao.IDPRD,
                                                                                               /*QUANTIDADE*/ itmComposicao.QUANTIDADE.ToString().Replace(",", "."),
                                                                                               /*UNIDADEMEDIDA itmComposicao.UNIDADEMEDIDA,*/
                                                                                               /*PRECOUNITARIO*/ itmComposicao.PRECOUNITARIO.ToString().Replace(",", "."));
                        queryItemComposto.Add(sSql);
                    }

                    movimento.ItemMovimento.Add(itmmov);
                }

                sPoint = string.Concat("Camada: [Client]: Gera IDNAT do movimento.");
                List<string> GeraIDNAT = new List<string>();
                int Indice = 2;
                int j = 0;
                string sql = String.Empty;
                string CODNAT = String.Empty;

                while (j < Itens.Count)
                {
                    sql = String.Format(@"select CODNAT from DCFOP where CODCOLIGADA = 1 and IDNAT = '{0}'", Itens[j].IDNAT);
                    string[] IDSPLIT = MetodosSQL.GetField(sql, "CODNAT").Split('.');

                    if (!GeraIDNAT.Contains(IDSPLIT[Indice]))
                    {
                        GeraIDNAT.Add(IDSPLIT[Indice]);

                        for (int k = 0; k <= Indice; k++)
                        {
                            CODNAT += IDSPLIT[k] + ".";
                        }

                        if (GeraIDNAT.Count > 1 && Indice > 0)
                        {
                            GeraIDNAT.Clear();
                            Indice--;
                            CODNAT = String.Empty;
                            j = 0;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    else
                    {
                        j++;
                    }


                }

                sPoint = "Camada: [Client], Campo: [IDNAT]";
                sql = String.Format(@"select IDNAT from DCFOP where CODCOLIGADA = 1 and CODNAT = '{0}'", CODNAT.Remove(CODNAT.Length - 1, 1));
                movimento.IdNat = int.Parse(MetodosSQL.GetField(sql, "IDNAT")); //PASSANDO FIXO APENAS PARA TESTE



                sPoint = string.Concat("Camada: [Client], Rotina: [Executa Método OrcamentoSave");

                AppInterop.Message mensagem;
                if (FatureContexto.Remoto)
                {
                    mensagem = new Util().ConvertToMessage(FatureContexto.ServiceSoapClient.OrcamentoSave(AppLib.Context.Usuario, AppLib.Context.Senha, new Util().ConvertToWSMovMovimentoPar(movimento)));
                }
                else
                {
                    mensagem = FatureContexto.ServiceClient.OrcamentoSave(AppLib.Context.Usuario, AppLib.Context.Senha, movimento);
                }

                sPoint = string.Concat("Camada: [Client], Rotina: [Verifica Retorno");
                if (int.Parse(mensagem.Retorno.ToString()) > 0)
                {
                    sPoint = string.Concat("Camada: [Client], Rotina: [Grava Consumidor Final]");
                    int ConsuFinal = cbConsumidorFinal.Checked ? 1 : 0;

                    MetodosSQL.ExecMultiple(queryItemComposto);

                    sql = String.Format(@"update TMOVFISCAL
                                            set OPERACAOCONSUMIDORFINAL = '{0}'
                                            where CODCOLIGADA = 1 
                                            AND IDMOV = '{1}'", ConsuFinal, mensagem.Retorno);
                    MetodosSQL.ExecQuery(sql);

                    sPoint = string.Concat("Camada: [Client], Rotina: [Retorno do Identificador do Movimento");
                    campoInteiroIDMOV.Set(int.Parse(mensagem.Retorno.ToString()));

                    sSql = String.Format(@"update ZORCAMENTOITEMCOMPOSTO
                                                    set IDMOV = {0}
                                                    where CODCOLIGADA = 1
                                                    and CODFILIAL = 1
                                                    and IDMOV = '{1}'", campoInteiroIDMOV.Get(), oGuid);
                    MetodosSQL.ExecQuery(sSql);

                    #region Atualiza o IPI dos produtos ao salvar
                    try
                    {
                        string Update = String.Format(@"update TTRBMOV set ALIQUOTA = TTRBPRD.ALIQUOTA from TTRBMOV

                                      inner join TITMMOV TTM
                                      on TTRBMOV.IDMOV = TTM.IDMOV
                                      and TTRBMOV.NSEQITMMOV = TTM.NSEQITMMOV
                                      and TTRBMOV.CODCOLIGADA = TTM.CODCOLIGADA

                                      inner join TMOV TM
                                      on TM.CODCOLIGADA = TTM.CODCOLIGADA
                                      and TM.IDMOV = TTM.IDMOV

                                      inner join TTRBPRD 
                                      on TTRBPRD.IDPRD = TTM.IDPRD
                                      and TTRBPRD.CODCOLIGADA = TTM.CODCOLIGADA

                                      where TM.CODTMV = '2.1.03'
                                      and TTRBPRD.CODTRB = 'IPI'
                                      and TTRBMOV.CODTRB = 'IPI'
                                      and TTM.CODCOLIGADA = 1
                                      and TM.IDMOV = {0}", (int)campoInteiroIDMOV.Get());

                        AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Update);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao atualizar IPI: " + ex.Message);
                    }
                    #endregion

                    try
                    {
                        sSql = @"SELECT NUMEROMOV FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?";
                        sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, (int)campoInteiroIDMOV.Get() });
                        System.Data.DataTable dt1 = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                        if (dt.Rows.Count > 0)
                        {
                            campoTextoNUMEROMOV.Set(dt1.Rows[0]["NUMEROMOV"].ToString());
                        }
                    }
                    catch (Exception)
                    {
                        sSql = String.Format(@"delete from ZORCAMENTOITEMCOMPOSTO where CODCOLIGADA = 1 and CODFILIAL = 1 and IDMOV = '{0}'", String.IsNullOrWhiteSpace(campoInteiroIDMOV.Get().ToString()) ? oGuid.ToString() : campoInteiroIDMOV.Get().ToString());
                        this.Cursor = Cursors.Default;
                        sPoint = "Camada: [Client], Rotina: [Busca Numero do Movimento";
                        throw new Exception(mensagem.Mensagem);
                    }

                    sPoint = string.Concat("Camada: [Client], Rotina: [Busca dados do movimento");
                    sSql = @"SELECT * FROM TMOV WHERE CODCOLIGADA = ? AND IDMOV = ?";
                    sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, (int)campoInteiroIDMOV.Get() });
                    System.Data.DataTable dt2 = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });



                    if (dt2.Rows.Count > 0)
                    {
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Bruto");
                        campoDecimalVALORBRUTO.Set((dt2.Rows[0]["VALORBRUTO"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALORBRUTO"]));
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Outros");
                        campoDecimalVALOROUTROS.Set((dt2.Rows[0]["VALOROUTROS"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALOROUTROS"]));
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Liquido");
                        campoDecimalVALORLIQUIDO.Set((dt2.Rows[0]["VALORLIQUIDO"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALORLIQUIDO"]));

                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Frete");
                        campoDecimalVALORFRETE.Set((dt2.Rows[0]["VALORFRETE"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALORFRETE"]));
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Percentual Frete");
                        campoDecimalPERCENTUALFRETE.Set((dt2.Rows[0]["PERCENTUALFRETE"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["PERCENTUALFRETE"]));
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Desconto");
                        campoDecimalVALORDESC.Set((dt2.Rows[0]["VALORDESC"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALORDESC"]));
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Percentual Desconto");
                        campoDecimalPERCENTUALDESC.Set((dt2.Rows[0]["PERCENTUALDESC"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["PERCENTUALDESC"]));
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Despesa");
                        campoDecimalVALORDESP.Set((dt2.Rows[0]["VALORDESP"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALORDESP"]));
                        sPoint = string.Concat("Camada: [Client], Campo: [Carrega Percentual Despesa");
                        campoDecimalPERCENTUALDESP.Set((dt2.Rows[0]["PERCENTUALDESP"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["PERCENTUALDESP"]));
                        //sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Seguro");
                        //campoDecimalVALORSEGURO.Set((dt2.Rows[0]["VALORSEGURO"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALORSEGURO"]));
                        //sPoint = string.Concat("Camada: [Client], Campo: [Carrega Percentual Seguro");
                        //campoDecimalPERCENTUALSEGURO.Set((dt2.Rows[0]["PERCENTUALSEGURO"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["PERCENTUALSEGURO"]));
                        //sPoint = string.Concat("Camada: [Client], Campo: [Carrega Valor Extra1");
                        //campoDecimalVALOREXTRA1.Set((dt2.Rows[0]["VALOREXTRA1"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["VALOREXTRA1"]));
                        //sPoint = string.Concat("Camada: [Client], Campo: [Carrega Percentual Extra1");
                        //campoDecimalPERCENTUALEXTRA1.Set((dt2.Rows[0]["PERCENTUALEXTRA1"] == DBNull.Value) ? null : (decimal?)Convert.ToDecimal(dt2.Rows[0]["PERCENTUALEXTRA1"]));
                    }

                    this.Cursor = Cursors.Default;
                    // MessageBox.Show(mensagem.Mensagem);
                    return true;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    sPoint = "Camada: [Server], ";
                    throw new Exception(mensagem.Mensagem);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(string.Concat(sPoint, " - ", ex.Message), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void FormPedidoCadastro_AposNovo(object sender, EventArgs e)
        {
            Itens.Clear();
            grid21_Atualizar(this, null);
            campoListaFINANCIADO.Set("NÃO");
            campoListaDEMONSBRINDE.Set("NÃO");
            campoListaPARAFINANC.Set("NÃO");
            if (campoDataEMISSAO.Get() == null)
                campoDataEMISSAO.Set(DateTime.Now);

            if (campoListaFRETECIFOUFOB.Get() == null)
                campoListaFRETECIFOUFOB.Set("2");

            if (campoDecimalVALORDESC.Get() == null)
                campoDecimalVALORDESC.Set(0);

            if (campoDecimalPERCENTUALDESC.Get() == null)
                campoDecimalPERCENTUALDESC.Set(0);

            if (campoDecimalVALORDESP.Get() == null)
                campoDecimalVALORDESP.Set(0);

            if (campoDecimalPERCENTUALDESP.Get() == null)
                campoDecimalPERCENTUALDESP.Set(0);

            //if (campoDecimalVALOREXTRA1.Get() == null)
            //    campoDecimalVALOREXTRA1.Set(0);

            //if (campoDecimalPERCENTUALEXTRA1.Get() == null)
            //    campoDecimalPERCENTUALEXTRA1.Set(0);

            //if (campoDecimalVALORFRETE.Get() == null)
            //    campoDecimalVALORFRETE.Set(0);

            //if (campoDecimalPERCENTUALFRETE.Get() == null)
            //    campoDecimalPERCENTUALFRETE.Set(0);

            //if (campoDecimalVALORSEGURO.Get() == null)
            //    campoDecimalVALORSEGURO.Set(0);

            //if (campoDecimalPERCENTUALSEGURO.Get() == null)
            //    campoDecimalPERCENTUALSEGURO.Set(0);

            //if (campoLookupCODCPG.Get() == null)
            //campoLookupCODCPG.Set("F001");

            Valida v = new Valida();
            if (v.IsRepresentante())
            {
                string sSql = @"select CODRPR FROM ztrpr WHERE CODCOLIGADA = ? AND CODUSUARIO = ?";
                sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, AppLib.Context.Usuario });
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                if (dt.Rows.Count > 0)
                {
                    if (AppLib.Context.Perfil.Substring(0, 6) == "APPRPR")
                    {
                        campoLookupCODRPR.textBoxCODIGO.Text = dt.Rows[0]["CODRPR"].ToString();
                    }
                    else
                    {
                        if (AppLib.Context.Perfil.Substring(0, 6) == "APPRVD")
                        {
                            campoLookupCODRPR.textBoxCODIGO.Text = "00033";
                        }
                    }
                    //campoLookupCODRPR_SetDescricao(this, null);
                    campoLookupCODRPR.setDescricao();
                    campoLookupCODRPR.Enabled = false;
                }
                else
                {
                    campoLookupCODRPR.Enabled = true;
                }
            }
            else
            {
                campoLookupCODRPR.Enabled = true;
            }
        }

        private void FormPedidoCadastro_AntesEditar(object sender, EventArgs e)
        {
            Itens.Clear();
            ExcluiuTudo = false;
        }

        private void FormPedidoCadastro_AposEditar(object sender, EventArgs e)
        {
            Valida v = new Valida();
            if (v.IsRepresentante())
            {
                /*
                string sSql = @"SELECT CODRPR FROM TRPR WHERE CODCOLIGADA = ? AND CODUSUARIO = ?";
                sSql = AppLib.Context.poolConnection.Get(this.Conexao).ParseCommand(sSql, new Object[] { AppLib.Context.Empresa, AppLib.Context.Usuario });
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(sSql, new Object[] { });

                if (dt.Rows.Count > 0)
                {
                    if (AppLib.Context.Perfil.Substring(0, 6) == "APPRPR")
                    {
                        campoLookupCODRPR.textBoxCODIGO.Text = dt.Rows[0]["CODRPR"].ToString();
                    }
                    else
                    {
                        if (AppLib.Context.Perfil.Substring(0, 6) == "APPRVD")
                        {
                            campoLookupCODRPR.textBoxCODIGO.Text = "00033";
                        }                    
                    }
                    campoLookupCODRPR_SetDescricao(this, null);
                    campoLookupCODRPR.Enabled = false;
                }
                else
                {
                    campoLookupCODRPR.Enabled = true;
                }
                */

                campoLookupCODRPR.Enabled = false;
            }
            else
            {
                campoLookupCODRPR.Enabled = true;
            }
        }

        #endregion

        private void campoLookupCODCFO_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM ZVWFCFO WHERE CODCOLIGADA in (0,?) AND CODCFO = ?";
            campoLookupCODCFO.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODCFO.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODCFO.Get() }).ToString();
        }

        private void campoLookupCODRPR_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOMEFANTASIA FROM TRPR WHERE CODCOLIGADA = ? AND CODRPR = ?";
            campoLookupCODRPR.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODRPR.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODRPR.Get() }).ToString();
        }

        private void campoLookupCODTRA_SetDescricao(object sender, EventArgs e)
        {
            string sql = @"SELECT NOME FROM TTRA WHERE CODCOLIGADA = ? AND CODTRA = ?";
            campoLookupCODTRA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupCODTRA.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupCODTRA.Get() }).ToString();
        }

        private void campoLookupTPCULTURA_SetDescricao_1(object sender, EventArgs e)
        {
            string sql = @"SELECT DESCRICAO FROM GCONSIST WHERE CODCOLIGADA = ? AND CODCLIENTE = ?";
            campoLookupTPCULTURA.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get(campoLookupTPCULTURA.Conexao).ExecGetField("", sql, new object[] { AppLib.Context.Empresa, campoLookupTPCULTURA.Get() }).ToString();
        }

        private void gridData1_SetParametros(object sender, EventArgs e)
        {
            gridData1.Parametros = new Object[] { AppLib.Context.Empresa, campoInteiroIDMOV.Get() };
        }

        private void gridData1_Novo(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status != "A")
                {
                    MessageBox.Show("Movimento encontra-se bloqueado. ", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            FormOrcamentoItem f = new FormOrcamentoItem();
            f.banco = true;
            f.ShowDialog();
            if (f.acao == AcaoForcada.Salvar)
            {
                try
                {
                    AppLib.ORM.Jit ZORCAMENTOITEMBANCO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZORCAMENTOITEMBANCO");

                    ZORCAMENTOITEMBANCO.Set("CODCOLIGADA", AppLib.Context.Empresa);
                    ZORCAMENTOITEMBANCO.Set("IDMOV", campoInteiroIDMOV.Get());
                    ZORCAMENTOITEMBANCO.Set("NSEQITMMOV", gridData1.gridView1.RowCount + 1);
                    ZORCAMENTOITEMBANCO.Set("IDPRD", f.xTITMMOV.IDPRD);
                    ZORCAMENTOITEMBANCO.Set("CODUND", f.xTITMMOV.UNIDADE);
                    ZORCAMENTOITEMBANCO.Set("QUANTIDADE", f.xTITMMOV.QUANTIDADE);
                    ZORCAMENTOITEMBANCO.Set("PRECOUNITARIO", f.xTITMMOV.PRECOUNITARIO);
                    ZORCAMENTOITEMBANCO.Set("ALIQUOTAIPI", f.xTITMMOV.ALIQUOTAIPI);
                    ZORCAMENTOITEMBANCO.Set("HISTORICOLONGO", f.xTITMMOV.HISTORICOLONGO);
                    ZORCAMENTOITEMBANCO.Insert();
                }
                catch (Exception)
                {


                }

            }
        }

        private void gridData1_Excluir(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status != "A")
                {
                    MessageBox.Show("Movimento encontra-se bloqueado. ", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            for (int i = 0; i < gridData1.GetDataRows().Count; i++)
            {
                AppLib.ORM.Jit ZORCAMENTOITEMBANCO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZORCAMENTOITEMBANCO");

                ZORCAMENTOITEMBANCO.Set("CODCOLIGADA", AppLib.Context.Empresa);
                ZORCAMENTOITEMBANCO.Set("IDMOV", campoInteiroIDMOV.Get());
                ZORCAMENTOITEMBANCO.Set("NSEQITMMOV", gridData1.GetDataRows()[i]["NSEQITMMOV"]);
                ZORCAMENTOITEMBANCO.Delete();
            }
        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            //Verifica se o Status é == faturado
            //Verifica se existe o movimento 2.1.20
            string statusMov = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT STATUS FROM TMOV WHERE NUMEROMOV = ? AND CODTMV = '2.1.20'", new object[] { campoTextoNUMEROMOV.Get() }).ToString();
            if (!string.IsNullOrEmpty(statusMov))
            {
                if (statusMov != "A")
                {
                    MessageBox.Show("Movimento encontra-se bloqueado. ", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(status))
            {
                if (status != "A")
                {
                    AppLib.Windows.FormMessagePrompt frm = new AppLib.Windows.FormMessagePrompt();
                    frm.Text = "Motivo.";
                    frm.richTextBox1.Text = "Informe o Motivo.";
                    frm.ShowDialog();
                    if (frm.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                    {
                        AppLib.Context.poolConnection.Get().ExecTransaction("INSERT INTO ZLIBERACAOORCAMENTOBANCO (IDMOV, USUARIO, MOTIVO) VALUES (?, ?, ?)", new object[] { campoInteiroIDMOV.Get(), AppLib.Context.Usuario, frm.textBox1.Text });
                        status = "A";
                    }
                }
            }


            FormOrcamentoItem f = new FormOrcamentoItem();
            System.Data.DataRow dr = gridData1.GetDataRow();
            TITMMOV reg = new TITMMOV();
            reg.ALIQUOTAIPI = Convert.ToDecimal(dr["ALIQUOTAIPI"]);
            //reg.CODIGOPRD = dr["CODIGOPRD"].ToString();
            reg.HISTORICOLONGO = dr["HISTORICOLONGO"].ToString();
            reg.IDPRD = Convert.ToInt32(dr["IDPRD"]);
            reg.CODIGOPRD = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT CODIGOPRD FROM TPRODUTO WHERE CODCOLPRD = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(dr["IDPRD"]) }).ToString();
            reg.PRODUTO = AppLib.Context.poolConnection.Get().ExecGetField(string.Empty, "SELECT NOMEFANTASIA FROM TPRODUTO WHERE CODCOLPRD = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(dr["IDPRD"]) }).ToString();

            reg.PRECOUNITARIO = Convert.ToDecimal(dr["PRECOUNITARIO"]);
            reg.QUANTIDADE = Convert.ToDecimal(dr["QUANTIDADE"]);
            reg.UNIDADE = dr["CODUND"].ToString();

            f.xTITMMOV = reg;
            f.AtualizarForm();
            f.ShowDialog();

            if (f.acao == AcaoForcada.Salvar)
            {
                AppLib.ORM.Jit ZORCAMENTOITEMBANCO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZORCAMENTOITEMBANCO");

                ZORCAMENTOITEMBANCO.Set("CODCOLIGADA", AppLib.Context.Empresa);
                ZORCAMENTOITEMBANCO.Set("IDMOV", campoInteiroIDMOV.Get());
                ZORCAMENTOITEMBANCO.Set("NSEQITMMOV", gridData1.GetDataRow()["NSEQITMMOV"]);
                ZORCAMENTOITEMBANCO.Set("IDPRD", f.xTITMMOV.IDPRD);
                ZORCAMENTOITEMBANCO.Set("CODUND", f.xTITMMOV.UNIDADE);
                ZORCAMENTOITEMBANCO.Set("QUANTIDADE", f.xTITMMOV.QUANTIDADE);
                ZORCAMENTOITEMBANCO.Set("PRECOUNITARIO", f.xTITMMOV.PRECOUNITARIO);
                ZORCAMENTOITEMBANCO.Set("ALIQUOTAIPI", f.xTITMMOV.ALIQUOTAIPI);
                ZORCAMENTOITEMBANCO.Set("HISTORICOLONGO", f.xTITMMOV.HISTORICOLONGO);
                ZORCAMENTOITEMBANCO.Save();
            }
        }

        private void gridData1_AposAtualizar(object sender, EventArgs e)
        {
            //decimal valor = 0;

            //DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery("SELECT QUANTIDADE, PRECOUNITARIO FROM ZORCAMENTOITEMBANCO WHERE ZORCAMENTOITEMBANCO.CODCOLIGADA = ? AND IDMOV = ? ", new object[] { AppLib.Context.Empresa, campoInteiroIDMOV.Get() });

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    valor = valor + (Convert.ToDecimal(dt.Rows[i]["QUANTIDADE"]) * Convert.ToDecimal(dt.Rows[i]["PRECOUNITARIO"]));
            //}
            //txtSubTotalBanco.Text = string.Format("{0:n2}", valor);
            //txtValorBrutoBanco.Text = txtSubTotalBanco.Text;
            //txtValorLiquidoBanco.Text = txtSubTotalBanco.Text;
        }

        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            gridData2.Parametros = new Object[] { campoInteiroIDMOV.Get() };
        }

        private void FormPedidoCadastro_AposSalvar(object sender, EventArgs e)
        {
            AtualizaValorTributo();
        }

        private void btnSalvarOrcamentoBanco_Click(object sender, EventArgs e)
        {
            AppLib.ORM.Jit ZORCAMENTOBANCO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZORCAMENTOBANCO");
            ZORCAMENTOBANCO.Set("CODCOLIGADA", AppLib.Context.Empresa);
            ZORCAMENTOBANCO.Set("IDMOV", campoInteiroIDMOV.Get());
            ZORCAMENTOBANCO.Set("FINANCIADO", campoListaFinanciadoBanco.Get());
            ZORCAMENTOBANCO.Set("DATAEMISSAO", campoDataEmissaoBanco.Get());
            ZORCAMENTOBANCO.Set("CONDPGTO", campoMemoPagamentoBanco.Get());
            ZORCAMENTOBANCO.Save();
        }

        private void campoLista1_AposSelecao(object sender, EventArgs e)
        {
            //if (campoLista1.comboBox1.SelectedValue == "T")
            //{
            //    campoTexto2.Enabled = true;
            //    campoTexto3.Enabled = true;
            //}
            //else
            //{
            //    campoTexto2.Enabled = false;
            //    campoTexto3.Enabled = false;
            //}
        }

        private void gridData1_Load(object sender, EventArgs e)
        {

        }


        private void grid21_StyleChanged(object sender, EventArgs e)
        {

        }

        private void campoLookupCODCFO_AposSelecao(object sender, EventArgs e)
        {
            DataTable dt = MetodosSQL.GetDT("select distinct CODETD from ZREGRACFOP order by CODETD");

            AppLib.Windows.CodigoNome[] a = new AppLib.Windows.CodigoNome[dt.Rows.Count];


            int i = 0;
            foreach (DataRow prod in dt.Rows)
            {
                a[i] = new AppLib.Windows.CodigoNome(prod["CODETD"].ToString(), prod["CODETD"].ToString());
                i++;
            }

            campoLista2.Lista = a;

            if (campoLookupCODCFO.Get() == "00002")
            {
                cbConsumidorFinal.Enabled = true;
                campoLista2.Enabled = true;

                campoLista2.Set("SP");

                if (this.Acao == AppLib.Global.Types.Acao.Editar)
                {
                    string sql = String.Format(@"select NCONTRIB, UFORCAMENTO from TMOVCOMPL
                                                  where CODCOLIGADA = {0}
                                                  and IDMOV = {1}", AppLib.Context.Empresa, campoInteiroIDMOV.Get());
                    campoLista2.Set(MetodosSQL.GetField(sql, "UFORCAMENTO"));
                    cbConsumidorFinal.Checked = MetodosSQL.GetField(sql, "NCONTRIB") == "0" ? true : false;
                }
            }
            else
            {
                string sql = String.Format(@"SELECT CONTRIBUINTE, CODETD FROM FCFO WHERE CODCFO = '{0}'", campoLookupCODCFO.textBoxCODIGO.Text);

                cbConsumidorFinal.Checked = MetodosSQL.GetField(sql, "CONTRIBUINTE") == "0" ? true : false;
                campoLista2.Set(MetodosSQL.GetField(sql, "CODETD"));
                cbConsumidorFinal.Enabled = false;
                campoLista2.Enabled = false;
            }

        }

        private void cbConsumidorFinal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {


                if (cbConsumidorFinal.Checked)
                {
                    AppLib.Windows.CodigoNome[] a = new AppLib.Windows.CodigoNome[2];

                    a[0] = new AppLib.Windows.CodigoNome("1", "Uso/Consumo");
                    a[1] = new AppLib.Windows.CodigoNome("4", "Ativo Imobilizado");

                    campoListaAplicacao.Lista = a;
                    campoListaAplicacao.Set(null);
                }
                else
                {
                    AppLib.Windows.CodigoNome[] a = new AppLib.Windows.CodigoNome[2];

                    a[0] = new AppLib.Windows.CodigoNome("1", "Uso/Consumo");
                    a[1] = new AppLib.Windows.CodigoNome("2", "Revender");
                    a[2] = new AppLib.Windows.CodigoNome("3", "Industrializar");
                    a[3] = new AppLib.Windows.CodigoNome("4", "Ativo Imobilizado");

                    campoListaAplicacao.Lista = a;
                    campoListaAplicacao.Set(null);

                    campoListaAplicacao.Enabled = true;
                }


            }
            catch (Exception ex)
            {
                //Do nothing
            }
        }
    }
}
