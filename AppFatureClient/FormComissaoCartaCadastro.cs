using AppFatureClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFatureClient.Classes;
using AppLib;
using AppLib.Windows;
using System.IO;
using System.Data.SqlClient;

namespace AppLib.Windows
{
    public partial class FormComissaoCartaCadastro : DevExpress.XtraEditors.XtraForm
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Registro da grid")]
        private DataRow dr;

        [Category("_APP"), Description("Registro em edição")]
        public AppLib.ORM.Jit x { get; set; }

        [Category("_APP"), Description("Tabela principal")]
        public String TabelaPrincipal { get; set; }

        [Category("_APP"), Description("Demais tabelas")]
        private List<String> Tabelas { get; set; }

        [Category("_APP"), Description("Nome da conexão")]
        public String Conexao { get; set; }

        [Category("_APP"), Description("Querys de edição")]
        public Query[] Querys { get; set; }

        [Category("_APP"), Description("Habilitar botão novo")]
        public Boolean BotaoNovo { get; set; }

        [Category("_APP"), Description("Habilitar botão excluir")]
        public Boolean BotaoExcluir { get; set; }

        [Category("_APP"), Description("Ação que acionou o form")]
        public Global.Types.Acao Acao { get; set; }

        #endregion

        #region CONSTRUTORES

        string CODREPRESENTANTE = String.Empty;
        string CARTA = String.Empty;
        bool UPDT = false;

        byte[] ConteudoCarta;
        byte[] ConteudoNFe;

        DataTable dt = new DataTable("Débitos");
        DataTable dt2 = new DataTable("Créditos");

        double VDebitoAnterior = 0;

        public FormComissaoCartaCadastro(string _CODREPRESENTANTE, string _carta)
        {
            InitializeComponent();

            BotaoNovo = false;
            BotaoExcluir = false;

            Conexao = "Start";
            CODREPRESENTANTE = _CODREPRESENTANTE;

            if (_carta != null)
            {
                CARTA = _carta;
                UPDT = true;
                lblNCarta.Text = CARTA;
                string query = String.Format(@"select VALORCREDITOCOMPL, 
                                                      OBSERVACAOCREDITOCOMPL, 
                                                      VALORDEBITOCOMPL, 
                                                      OBSERVACAODEBITOCOMPL, 
                                                      STATUS from ZCOMISSAOCARTA where CODCARTA = '{0}'", CARTA);

                if (MetodosSQL.GetField(query, "STATUS") == "CONCLUIDO")
                {
                    simpleButtonSALVAR.Enabled = false;
                    simpleButtonOK.Enabled = false;

                    gridData1.Visible = false;
                    gridData2.Visible = false;
                    gridData1.Enabled = false;
                    gridData2.Enabled = false;

                    gridData3.Dock = System.Windows.Forms.DockStyle.Fill;
                    gridData4.Dock = System.Windows.Forms.DockStyle.Fill;

                    btnSelecionaCarta.Enabled = false;
                    btnSelecionaNFe.Enabled = false;
                    btnVisualizaCarta.Visible = true;
                    btnVisualizaNFe.Visible = true;

                    pdfViewer1.Visible = true;
                }

                txtVCred.Set(decimal.Parse(MetodosSQL.GetField(query, "VALORCREDITOCOMPL")));
                txtVDeb.Set(decimal.Parse(MetodosSQL.GetField(query, "VALORDEBITOCOMPL")));

                txtObsCred.Set(MetodosSQL.GetField(query, "OBSERVACAOCREDITOCOMPL"));
                txtObsDeb.Set(MetodosSQL.GetField(query, "OBSERVACAODEBITOCOMPL"));

                //string sql = String.Format(@"select top 1 cast((ZCC.VALORCREDITO + ZCC.VALORCREDITOCOMPL) - (ZCC.VALORDEBITO + ZCC.VALORDEBITOCOMPL) as numeric(10,2)) as 'TOTAL', CODCARTA

                //                         from ZCOMISSAOCARTA ZCC

                //                         where (ZCC.VALORCREDITO + ZCC.VALORCREDITOCOMPL) - (ZCC.VALORDEBITO + ZCC.VALORDEBITOCOMPL) < 0
                //                         and ZCC.CODRPR = '{0}'
                //                         and ZCC.STATUS = 'CONCLUIDO'
                //                         and (ZCC.CODCARTA < {1})
                //                         and 0 = (select COUNT(1) from ZCOMISSAOCARTA 
                //                            where STATUS = 'CONCLUIDO'
                //                            and CODCARTA > ZCC.CODCARTA
                //                                  and CODCARTA <> {1}
                //                            and CODRPR = '{0}')

                //                         order by ZCC.CODCARTA desc", CODREPRESENTANTE,CARTA);


                string sql = String.Empty;


                if (UPDT)
                {
                    sql = String.Format(@"select top 1 case when cast((ZCC.VALORCREDITO + ZCC.VALORCREDITOCOMPL) - (ZCC.VALORDEBITO + ZCC.VALORDEBITOCOMPL) as numeric(10,2)) < 0 
			                                           then cast((ZCC.VALORCREDITO + ZCC.VALORCREDITOCOMPL) - (ZCC.VALORDEBITO + ZCC.VALORDEBITOCOMPL) as numeric(10,2))
			                                           else 0
			                                           end as 'TOTAL',
			                                           ZCC.CODCARTA

                                          from ZCOMISSAOCARTA ZCC 
                                          where ZCC.CODRPR = '{0}'
                                          and CODCARTA < {1}
                                          order by CODCARTA desc", CODREPRESENTANTE, CARTA);
                }
                else
                {
                    sql = String.Format(@"select top 1 case when cast((ZCC.VALORCREDITO + ZCC.VALORCREDITOCOMPL) - (ZCC.VALORDEBITO + ZCC.VALORDEBITOCOMPL) as numeric(10,2)) < 0 
			                                              then cast((ZCC.VALORCREDITO + ZCC.VALORCREDITOCOMPL) - (ZCC.VALORDEBITO + ZCC.VALORDEBITOCOMPL) as numeric(10,2))
			                                              else 0
			                                              end as 'TOTAL',
                                                          ZCC.CODCARTA

                                             from ZCOMISSAOCARTA ZCC where CODRPR = '{0}' order by CODCARTA desc", CODREPRESENTANTE);
                }

                if (!String.IsNullOrWhiteSpace(MetodosSQL.GetField(sql, "TOTAL")))
                {
                    VDebitoAnterior = Double.Parse(MetodosSQL.GetField(sql, "TOTAL"));
                }

                campoDecimalDEBITOANTERIOR.Set(Convert.ToDecimal(VDebitoAnterior));

                txtCARTAANTERIOR.Set(MetodosSQL.GetField(sql, "CODCARTA"));

                query = String.Format(@"select ANEXOCARTA from ZCOMISSAOCARTA where CODCARTA = {0}", CARTA);

                try
                {
                    ConteudoCarta = (byte[])MetodosSQL.ExecScalar(query);
                }
                catch
                {

                }

                query = String.Format(@"select ANEXONFE from ZCOMISSAOCARTA where CODCARTA = {0}", CARTA);

                try
                {
                    ConteudoNFe = (byte[])MetodosSQL.ExecScalar(query);
                }
                catch
                {

                }

                if (ConteudoCarta != null)
                {
                    btnVisualizaCarta.Visible = true;
                    btnVisualizaCarta.Enabled = true;
                    btnSalvarCarta.Visible = true;
                    btnDeletarCarta.Visible = true;
                }

                if (ConteudoNFe != null)
                {
                    btnVisualizaNFe.Visible = true;
                    btnVisualizaNFe.Enabled = true;
                    btnSalvarNFe.Visible = true;
                    btnDeletarNF.Visible = true;
                }
            }
            else
            {
                UPDT = false;
            }



            dt.Columns.Add("IDMOV", typeof(String));
            dt.Columns.Add("NUMEROMOV", typeof(String));
            dt.Columns.Add("CLIENTE", typeof(String));
            dt.Columns.Add("REPRESENTANTE", typeof(String));
            dt.Columns.Add("VALORLIQUIDOORIG", typeof(String));
            dt.Columns.Add("VALORBRUTOORIG", typeof(String));
            dt.Columns.Add("STATUS", typeof(String));

            dt2.Columns.Add("IDMOV", typeof(String));
            dt2.Columns.Add("NUMEROMOV", typeof(String));
            dt2.Columns.Add("CODCFO", typeof(String));
            dt2.Columns.Add("NOMEFANTASIA", typeof(String));
            dt2.Columns.Add("CODRPR", typeof(String));
            dt2.Columns.Add("NOME", typeof(String));
            dt2.Columns.Add("STATUS", typeof(String));
            dt2.Columns.Add("BAIXA", typeof(String));
            dt2.Columns.Add("VALORLIQUIDOORIG", typeof(String));
            dt2.Columns.Add("PERCENTUALCOMISSAO", typeof(String));

            gridData1.GetProcessos().Add("Adiciona Multiplos", null, AdicionaMultiplos);
            gridData2.GetProcessos().Add("Adiciona Multiplos", null, AdicionaMultiplos2);

            gridData2.GetProcessos().Add("Alterar comissao", null, AlterarComissao);

            MetodosSQL.CS = AppLib.Context.poolConnection.Get().ConnectionString;



        }



        private void FormCadastro2_Load(object sender, EventArgs e)
        {

            if (!this.DesignMode)
            {
                try
                {
                    if (BotaoNovo)
                    {
                        toolStripButtonNOVO.Enabled = true;
                    }
                    else
                    {
                        toolStripButtonNOVO.Enabled = false;
                    }
                }
                catch (Exception) { }

                try
                {
                    if (BotaoExcluir)
                    {
                        toolStripButtonEXCLUIR.Enabled = true;
                    }
                    else
                    {
                        toolStripButtonEXCLUIR.Enabled = false;
                    }
                }
                catch (Exception) { }

            }
        }

        #endregion

        #region EVENTOS

        private void toolStripButtonNOVO_Click(object sender, EventArgs e)
        {
            Acao = Global.Types.Acao.Novo;
            if (new AppLib.Security.Access().Cadastrar(this.Conexao, this.TabelaPrincipal, AppLib.Context.Perfil))
            {
                if (BotaoNovo)
                {
                    try
                    {
                        this.AntesNovo(this, null);
                    }
                    catch { }

                    this.LimparTela(this.Controls);

                    try
                    {
                        this.AposNovo(this, null);
                    }
                    catch { }
                }
                else
                {
                    // MessageBox.Show("Botão desabilitado.");
                }
            }
            else
            {
                MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso para salvar este cadastro (" + this.TabelaPrincipal + ").", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripButtonEXCLUIR_Click(object sender, EventArgs e)
        {
            if (BotaoExcluir)
            {
                Boolean excluiu;

                try
                {
                    excluiu = this.ValidarExcluir(this, null);
                }
                catch
                {
                    excluiu = true;
                }

                if (excluiu)
                {
                    try
                    {
                        try
                        {
                            this.AntesExcluir(this, null);
                        }
                        catch { }

                        this.ExcluirObject(this, null);

                        try
                        {
                            this.AposExcluir(this, null);
                        }
                        catch { }
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao excluir registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // MessageBox.Show("Botão desabilitado.");
            }
        }

        private void toolStripButtonPRIMEIRO_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveFirst();
            System.Data.DataRowView drv = (System.Data.DataRowView)bindingSource1.Current;
            this.Editar(drv.Row, false);
        }

        private void toolStripButtonANTERIOR_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
            System.Data.DataRowView drv = (System.Data.DataRowView)bindingSource1.Current;
            this.Editar(drv.Row, false);
        }

        private void toolStripButtonPROXIMO_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
            System.Data.DataRowView drv = (System.Data.DataRowView)bindingSource1.Current;
            this.Editar(drv.Row, false);
        }

        private void toolStripButtonULTIMO_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveLast();
            System.Data.DataRowView drv = (System.Data.DataRowView)bindingSource1.Current;
            this.Editar(drv.Row, false);
        }



        public void simpleButtonSALVAR_Click(object sender, EventArgs e)
        {
            //Boolean validou;

            //try
            //{
            //    validou = this.ValidarSalvar(this, null);
            //}
            //catch
            //{
            //    validou = true;
            //}

            //if (validou)
            //{
            //    try
            //    {
            //        this.AntesSalvar(this, null);
            //    }
            //    catch { }

            //    try
            //    {
            //        this.SalvarObject(this, null);

            //        try
            //        {
            //            this.AposSalvar(this, null);
            //        }
            //        catch { }
            //    }
            //    catch 
            //    {
            //        MessageBox.Show("Erro ao salvar o registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}


            UPDT = SalvarCarta(UPDT);
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            //Boolean validou;

            //try
            //{
            //    validou = this.ValidarSalvar(this, null);
            //}
            //catch
            //{
            //    validou = true;
            //}

            //if (validou)
            //{
            //    try
            //    {
            //        this.AntesSalvar(this, null);
            //    }
            //    catch { }

            //    try
            //    {
            //        if (this.SalvarObject(this, null))
            //        {

            //            Acao = Global.Types.Acao.Editar;

            //            try
            //            {
            //                this.AposSalvar(this, null);
            //            }
            //            catch { }

            //            this.Close();
            //        }
            //    }
            //    catch { }
            //}

            if (SalvarCarta(UPDT))
            {
                this.Close();
            }
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCadastroObject_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManager.ReleaseUnusedMemory(false);
        }

        #endregion

        #region MÉTODOS EXTERNOS

        public void Novo()
        {
            Acao = Global.Types.Acao.Novo;
            toolStripButtonNOVO_Click(this, null);
            this.ShowDialog();
        }

        public void Editar(System.Windows.Forms.BindingSource bs)
        {
            Acao = Global.Types.Acao.Editar;
            if (bs.Count > 0)
            {
                try
                {
                    this.AntesEditar(this, null);
                }

                catch { }
                bindingSource1 = bs;
                System.Data.DataRowView registro = (System.Data.DataRowView)bindingSource1.Current;
                this.Editar(registro.Row, true);

                try
                {
                    this.AposEditar(this, null);
                }
                catch { }
            }
        }

        public void Excluir(System.Data.DataRowCollection registros)
        {
            Acao = Global.Types.Acao.Excluir;
            if (registros != null)
            {
                for (int i = 0; i < registros.Count; i++)
                {
                    this.Editar(registros[i], false);
                    toolStripButtonEXCLUIR_Click(this, null);
                }
            }
        }

        public System.Windows.Forms.ToolStripItemCollection GetAnexos()
        {
            return this.toolStripButtonANEXOS.DropDownItems;
        }

        public System.Windows.Forms.ToolStripItemCollection GetProcessos()
        {
            return this.toolStripButtonPROCESSOS.DropDownItems;
        }

        #endregion

        #region MÉTODOS INTERNOS

        private void LimparTela(System.Windows.Forms.Control.ControlCollection componentes)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                LimparTela(componentes[i]);
            }
        }

        private void LimparTela(System.Windows.Forms.Control componente)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                LimparTela(campo.Controls);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                LimparTela(campo.Controls);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                LimparTela(campo.Controls);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                LimparTela(campo.Controls);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                LimparTela(campo.Controls);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                LimparTela(campo.Controls);
            }

            #endregion

            #region COMPONENTES NOVOS

            if (componente.GetType() == typeof(CampoInteiro))
            {
                CampoInteiro campo = (CampoInteiro)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoDecimal))
            {
                CampoDecimal campo = (CampoDecimal)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoData))
            {
                CampoData campo = (CampoData)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoHora))
            {
                CampoHora campo = (CampoHora)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoDataHora))
            {
                CampoDataHora campo = (CampoDataHora)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoTexto))
            {
                CampoTexto campo = (CampoTexto)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoMemo))
            {
                CampoMemo campo = (CampoMemo)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoLista))
            {
                CampoLista campo = (CampoLista)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoLookup))
            {
                CampoLookup campo = (CampoLookup)componente;
                campo.textBoxCODIGO.Text = campo.Default;
                campo.textBox1_Leave(this, null);
            }

            if (componente.GetType() == typeof(CampoImagem))
            {
                CampoImagem campo = (CampoImagem)componente;
                campo.Clear();
            }

            #endregion
        }

        public void Editar(System.Data.DataRow registro, Boolean mostrarForm)
        {
            if (registro != null)
            {
                dr = registro;

                try
                {
                    this.AntesEditar(this, null);
                }
                catch { }

                // Carrega as querys
                for (int i = 0; i < this.Querys.Length; i++)
                {
                    Query q = this.Querys[i];

                    String consulta = "";

                    try
                    {
                        consulta = AppLib.Context.poolConnection.Get(q.Conexao).ParseCommand(q.GetConsulta(), this.MontarParametros(q.Parametros));
                        q.dt = AppLib.Context.poolConnection.Get(q.Conexao).ExecQuery(consulta, new Object[] { });

                        // Carrega os campos
                        this.CarregarForm(i, q.dt, this.Controls);
                    }
                    catch (Exception ex)
                    {
                        new FormExceptionSQL().Mostrar("Erro na montagem da query", consulta, ex);
                    }
                }

                try
                {
                    this.AposEditar(this, null);
                }
                catch { }

                if (mostrarForm)
                {
                    this.ShowDialog();
                }
            }
        }

        #endregion

        #region MÉTODOS DE CARGA

        private void CarregarForm(int posicaoQuery, System.Data.DataTable dt, System.Windows.Forms.Control.ControlCollection componentes)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                CarregarForm(posicaoQuery, dt, componentes[i]);
            }
        }

        private void CarregarForm(int posicaoQuery, System.Data.DataTable dt, System.Windows.Forms.Control componente)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            #endregion

            #region COMPONENTES LIB

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                String coluna = dt.Columns[i].ColumnName;
                Object valor = dt.Rows[0][i];

                if (componente.GetType() == typeof(CampoInteiro))
                {
                    CampoInteiro campo = (CampoInteiro)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(null);
                            }
                            else
                            {
                                campo.Set(int.Parse(valor.ToString()));
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoDecimal))
                {
                    CampoDecimal campo = (CampoDecimal)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(null);
                            }
                            else
                            {
                                campo.Set(decimal.Parse(valor.ToString()));
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoData))
                {
                    CampoData campo = (CampoData)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(null);
                            }
                            else
                            {
                                campo.Set(DateTime.Parse(valor.ToString()));
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoHora))
                {
                    CampoHora campo = (CampoHora)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(null);
                            }
                            else
                            {
                                campo.Set(DateTime.Parse(valor.ToString()));
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoDataHora))
                {
                    CampoDataHora campo = (CampoDataHora)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(null);
                            }
                            else
                            {
                                campo.Set(DateTime.Parse(valor.ToString()));
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoTexto))
                {
                    CampoTexto campo = (CampoTexto)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(String.Empty);
                            }
                            else
                            {
                                campo.Set(valor.ToString());
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoMemo))
                {
                    CampoMemo campo = (CampoMemo)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(String.Empty);
                            }
                            else
                            {
                                campo.Set(valor.ToString());
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoLista))
                {
                    CampoLista campo = (CampoLista)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(String.Empty);
                            }
                            else
                            {
                                campo.Set(valor.ToString());
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoLookup))
                {
                    CampoLookup campo = (CampoLookup)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.Set(String.Empty);
                            }
                            else
                            {
                                campo.textBoxCODIGO.Text = valor.ToString();
                                campo.textBox1_Leave(this, null);
                            }
                        }
                    }
                }

                if (componente.GetType() == typeof(CampoImagem))
                {
                    CampoImagem campo = (CampoImagem)componente;
                    if (campo.Query == posicaoQuery)
                    {
                        // nome imagem
                        if (coluna.ToUpper().Equals(campo.ColunaNomeImagem.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.SetNomeImagem(null);
                            }
                            else
                            {
                                campo.SetNomeImagem(valor.ToString()); ;
                            }
                        }

                        // imagem
                        if (coluna.ToUpper().Equals(campo.ColunaImagem.ToUpper()))
                        {
                            if (valor.ToString().Equals(""))
                            {
                                campo.SetImagem(null);
                            }
                            else
                            {
                                campo.SetImagem(valor.ToString()); ;
                            }
                        }
                    }
                }

            } // fim do for

            #endregion

            #region GRID LIB

            if (componente.GetType() == typeof(GridData))
            {
                GridData campo = (GridData)componente;
                campo.Atualizar(false, false, null);
            }

            if (componente.GetType() == typeof(GridObject))
            {
                GridObject campo = (GridObject)componente;
                campo.toolStripButtonATUALIZAR_Click(this, null);
            }

            #endregion
        }

        /// <summary>
        /// Chamado somnete na ocasião de auto incremento
        /// </summary>
        /// <param name="tabela">Nome da tabela</param>
        /// <param name="componentes">Collections de compoenntes do form</param>
        /// <param name="incremento">Valor do incremento</param>
        private void CarregarForm(String tabela, System.Windows.Forms.Control.ControlCollection componentes, int incremento)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                CarregarForm(tabela, componentes[i], incremento);
            }
        }

        private void CarregarForm(String tabela, System.Windows.Forms.Control componente, int incremento)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                CarregarForm(tabela, componente.Controls, incremento);
            }

            #endregion

            #region COMPONENTES LIB

            // O AUTO INCREMENTO PODE OCORRER SOMENTE EM CAMPOS INTEIRO
            // POR ESTA RAZÃO OS DEMAIS TIPOS FORAM IGNORADOS
            if (componente.GetType() == typeof(CampoInteiro))
            {
                CampoInteiro campo = (CampoInteiro)componente;
                if (this.EhAutoIncremento(campo.Tabela, campo.Campo))
                {
                    campo.Set(incremento);
                }
            }

            #endregion
        }

        private void CarregarObjeto(String tabela, System.Windows.Forms.Control.ControlCollection componentes)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                CarregarObjeto(tabela, componentes[i]);
            }
        }

        private void CarregarObjeto(String tabela, System.Windows.Forms.Control componente)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            #endregion

            #region COMPONENTES NOVOS

            if (componente.GetType() == typeof(CampoInteiro))
            {
                CampoInteiro campo = (CampoInteiro)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoDecimal))
            {
                CampoDecimal campo = (CampoDecimal)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoData))
            {
                CampoData campo = (CampoData)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoHora))
            {
                CampoHora campo = (CampoHora)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoDataHora))
            {
                CampoDataHora campo = (CampoDataHora)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoTexto))
            {
                CampoTexto campo = (CampoTexto)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoMemo))
            {
                CampoMemo campo = (CampoMemo)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoLista))
            {
                CampoLista campo = (CampoLista)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoLookup))
            {
                CampoLookup campo = (CampoLookup)componente;
                x.Set(campo.Campo, campo.Get());
            }

            if (componente.GetType() == typeof(CampoImagem))
            {
                CampoImagem campo = (CampoImagem)componente;
                x.Set(campo.ColunaNomeImagem, campo.GetNomeImagem());
                x.Set(campo.ColunaImagem, campo.GetImagem());
            }

            #endregion
        }

        #endregion

        #region MÉTODOS DE APOIO

        public Object[] MontarParametros(String[] parametros)
        {
            Object[] result = new Object[parametros.Length];

            for (int p = 0; p < parametros.Length; p++)
            {
                String coluna = parametros[p];

                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    if (dr.Table.Columns[i].ColumnName.ToUpper().Equals(coluna.ToUpper()))
                    {
                        result[p] = dr[i].ToString();
                        i = dr.Table.Columns.Count;
                    }
                }
            }

            return result;
        }

        public List<String> getTabelas()
        {
            Tabelas = new List<String>();
            getTabelas(this.Controls);
            return Tabelas;
        }

        private void getTabelas(System.Windows.Forms.Control.ControlCollection componentes)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                getTabelas(componentes[i]);
            }
        }

        private void getTabelas(System.Windows.Forms.Control componente)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                getTabelas(campo.Controls);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                getTabelas(campo.Controls);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                getTabelas(campo.Controls);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                getTabelas(campo.Controls);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                getTabelas(campo.Controls);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                getTabelas(campo.Controls);
            }

            #endregion

            #region COMPONENTES NOVOS

            if (componente.GetType() == typeof(CampoInteiro))
            {
                CampoInteiro campo = (CampoInteiro)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoDecimal))
            {
                CampoDecimal campo = (CampoDecimal)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoData))
            {
                CampoData campo = (CampoData)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoHora))
            {
                CampoHora campo = (CampoHora)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoDataHora))
            {
                CampoDataHora campo = (CampoDataHora)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoTexto))
            {
                CampoTexto campo = (CampoTexto)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoMemo))
            {
                CampoMemo campo = (CampoMemo)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoLista))
            {
                CampoLista campo = (CampoLista)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoLookup))
            {
                CampoLookup campo = (CampoLookup)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            if (componente.GetType() == typeof(CampoImagem))
            {
                CampoImagem campo = (CampoImagem)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            #endregion
        }

        private Boolean TabelaExiste(String tabela)
        {
            for (int i = 0; i < Tabelas.Count; i++)
            {
                if (Tabelas[i].ToString().ToUpper().Equals(tabela.ToUpper()))
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean EhAutoIncremento(String tabela, String campo)
        {
            // Se estou na tabela correta
            if (x.Metadados[0].NomeTabela.ToUpper().Equals(tabela.ToUpper()))
            {
                // Varre os campos
                for (int i = 0; i < x.Metadados.Count; i++)
                {
                    // Se encontrei o campo
                    if (x.Metadados[i].NomeCampo.ToUpper().Equals(campo.ToUpper()))
                    {
                        // Se é auto incremento
                        if (x.Metadados[i].AutoIncremento)
                        {
                            // Sim, interrompe o look e retorna verdadeiro
                            i = x.Metadados.Count;
                            return true;
                        }
                        else
                        {
                            // Não é auto incremento
                            return false;
                        }
                    }
                }
            }

            // Não sendo da tabela retorna falso
            return false;
        }

        #endregion

        #region MÉTODOS CUSTOMIZADOS

        public delegate Boolean Salvar2Handler(object sender, EventArgs e);
        [Category("_APP"), Description("Método botão salvar"), DefaultValue(false)]
        public event Salvar2Handler SalvarObject;

        public delegate void Excluir2Handler(object sender, EventArgs e);
        [Category("_APP"), Description("Método botão excluir"), DefaultValue(false)]
        public event Excluir2Handler ExcluirObject;

        public delegate Boolean ValidarSalvarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método de validação executado antes de salvar"), DefaultValue(false)]
        public event ValidarSalvarHandler ValidarSalvar;

        public delegate Boolean ValidarExcluirHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método de validação executado antes de excluir"), DefaultValue(false)]
        public event ValidarExcluirHandler ValidarExcluir;

        public delegate void AntesNovoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de limpar o form"), DefaultValue(false)]
        public event AntesNovoHandler AntesNovo;

        public delegate void AposNovoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado depois de limpar o form"), DefaultValue(false)]
        public event AposNovoHandler AposNovo;

        public delegate void AntesEditarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de editar um registro"), DefaultValue(false)]
        public event AntesEditarHandler AntesEditar;

        public delegate void AposEditarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado depois de editar um registro"), DefaultValue(false)]
        public event AposEditarHandler AposEditar;

        public delegate void PrepararHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de salvar"), DefaultValue(false)]
        public event PrepararHandler AntesSalvar;

        public delegate void AposSalvarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado após salvar"), DefaultValue(false)]
        public event AposSalvarHandler AposSalvar;

        public delegate void AntesExcluirHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de excluir"), DefaultValue(false)]
        public event AntesExcluirHandler AntesExcluir;

        public delegate void AposExcluirHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado depois de excluir"), DefaultValue(false)]
        public event AposExcluirHandler AposExcluir;



        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void gridData2_Load(object sender, EventArgs e)
        {
            //            string sql = String.Format(@"select TMOV.NUMEROMOV, TMOV.CODRPR, case when W.Baixa >= W.VALORLIQUIDOORIG then 'Finalizado' else 'Aberto' end as 'Status', isnull(W.Baixa,0) as 'Baixa' , W.VALORLIQUIDOORIG from (
            //Select SUM(isnull(Y.VALORBAIXADO,0)) + SUM(isnull(Y.VALORNOTACREDITOADIANTAMENTO,0)) as 'Baixa', TM.NUMEROMOV, TM.VALORLIQUIDOORIG, TM.CODTMV
            //		  from (

            //SELECT
            //	FLAN.VALORBAIXADO,
            //	FLAN.IDMOV,
            //	FLANBAIXA.VALORNOTACREDITOADIANTAMENTO

            //FROM FLAN

            //left join FLANBAIXA 
            //on FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
            //and FLANBAIXA.IDLAN = FLAN.IDLAN

            //WHERE FLAN.IDMOV in (SELECT 
            //						IDMOVDESTINO 
            //					FROM 
            //						TMOVRELAC 
            //					WHERE 
            //						CODCOLORIGEM = 1
            //					AND IDMOVDESTINO NOT IN (SELECT IDMOV FROM TMOV WHERE CODTMV IN ('2.1.40')))
            //AND FLAN.CODCOLIGADA = 1
            //AND FLAN.CODTDO <> 'ADC') Y

            //inner join TMOVRELAC TMR
            //on TMR.IDMOVDESTINO = Y.IDMOV

            //inner join TMOV TM
            //on TM.IDMOV = TMR.IDMOVORIGEM
            //and TM.CODTMV = '2.1.20'

            //where Y.IDMOV is not null
            //and CODRPR = '{0}'

            //group by NUMEROMOV, VALORLIQUIDOORIG, TM.CODTMV) W

            //inner join TMOV
            //on TMOV.NUMEROMOV = W.NUMEROMOV
            //and TMOV.CODTMV = '2.1.20'

            //where TMOV.CODCOLIGADA = 1
            //and W.Baixa < W.VALORLIQUIDOORIG", CODREPRESENTANTE);

            //            gridData2.gridControl1.DataSource = AppLib.Context.poolConnection.Get().ExecQuery(sql, new Object[] { });

            //            sql = String.Format(@"select TM.IDMOV, TM.NUMEROMOV, FCFO.NOMEFANTASIA, TRPR.NOMEFANTASIA, TM.VALORLIQUIDOORIG, TM.VALORBRUTOORIG from TMOV TM

            //inner join FCFO 
            //on FCFO.CODCFO = TM.CODCFO

            //inner join TRPR
            //on TRPR.CODRPR = TM.CODRPR

            //where TM.CODCOLIGADA = 1
            //and TM.CODTMV = '2.1.20'
            //and TM.CODRPR = '{0}'", CODREPRESENTANTE);

            //            gridData1.Consulta = sql.Split(' ');
        }

        private void gridData2_SetParametros(object sender, EventArgs e)
        {
            gridData2.Parametros = new Object[] { CODREPRESENTANTE };
            gridData1.Parametros = new Object[] { CODREPRESENTANTE, CARTA };
        }

        private void gridData1_Load(object sender, EventArgs e)
        {


            //string sql = string.Format(@" select TM.IDMOV, TM.NUMEROMOV, FCFO.NOMEFANTASIA, TRPR.NOMEFANTASIA, TM.VALORLIQUIDOORIG, TM.VALORBRUTOORIG from TMOV TM
            // inner join FCFO
            // on FCFO.CODCFO = TM.CODCFO
            // inner join TRPR
            // on TRPR.CODRPR = TM.CODRPR
            // where TM.CODCOLIGADA = 1
            // and TM.CODTMV = '2.1.20'
            // and TM.CODRPR = ? ");



        }



        private void gridData1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridData1_AposSelecao(object sender, EventArgs e)
        {

        }

        private void gridData1_Editar(object sender, EventArgs e)
        {
            try
            {
                if (UPDT)
                {

                    DataRow SelectedRow = gridData1.GetDataRow();

                    if (dt2.Rows.Cast<DataRow>().Where(x => x["NUMEROMOV"].ToString() == SelectedRow["NUMEROMOV"].ToString()).Count() == 0)
                    {
                        string sql = String.Format(@"select count(1) as 'Cont' from ZCOMISSAOCARTA ZCC

                                                     inner join ZTMOVCOMISSAO ZTC
                                                     on ZTC.NCARTA = ZCC.CODCARTA

                                                     where ZTC.IDMOV = (select IDMOV from TMOV where CODCOLIGADA = 1 and CODTMV = '2.1.10' and NUMEROMOV = '{0}')",
                                                     SelectedRow["NUMEROMOV"].ToString());


                        if (MetodosSQL.GetField(sql, "Cont") == "0")
                        {
                            sql = String.Format(@"update ZTMOVCOMISSAO
                                              set NCARTA = '{0}',
                                              TIPOPAG = 'DÉBITO'
                                              where IDMOV = (select TM.IDMOV from TMOV TM where TM.CODCOLIGADA = 1 and TM.CODTMV = '2.1.10' and TM.NUMEROMOV = '{1}')",
                                              CARTA, SelectedRow["NUMEROMOV"].ToString());
                            MetodosSQL.ExecQuery(sql);
                            gridData3.Atualizar();
                            gridData1.Atualizar();
                        }
                        else
                        {
                            MessageBox.Show(SelectedRow["NUMEROMOV"].ToString() + " já foi adicionado");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este item já foi adicionado");
                    }

                }
                else
                {
                    DataRow SelectedRow = gridData1.GetDataRow();

                    if (dt.Rows.Cast<DataRow>().Where(x => x["IDMOV"].ToString() == SelectedRow["IDMOV"].ToString()).Count() == 0)
                    {
                        dt.Rows.Add(SelectedRow["IDMOV"].ToString(),
                                SelectedRow["NUMEROMOV"].ToString(),
                                SelectedRow["NOMEFANTASIA"].ToString(),
                                SelectedRow["REPRESENTANTE"].ToString(),
                                SelectedRow["VALORLIQUIDOORIG"].ToString(),
                                SelectedRow["VALORBRUTOORIG"].ToString(),
                                SelectedRow["STATUS"].ToString());

                        gridData3.gridControl1.DataSource = dt;
                        gridData3.gridView1.BestFitColumns();
                    }
                    else
                    {
                        MessageBox.Show("Este item já foi adicionado");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void gridData2_Editar(object sender, EventArgs e)
        {
            try
            {

                if (UPDT)
                {

                    DataRow SelectedRow = gridData2.GetDataRow();

                    if (dt2.Rows.Cast<DataRow>().Where(x => x["NUMEROMOV"].ToString() == SelectedRow["NUMEROMOV"].ToString()).Count() == 0)
                    {
                        string sql = String.Format(@"select count(1) as 'Cont' from ZCOMISSAOCARTA ZCC

                                                     inner join ZTMOVCOMISSAO ZTC
                                                     on ZTC.NCARTA = ZCC.CODCARTA

                                                     where ZTC.IDMOV = (select IDMOV from TMOV where CODCOLIGADA = 1 and CODTMV = '2.1.10' and NUMEROMOV = '{0}')",
                                                     SelectedRow["NUMEROMOV"].ToString());


                        if (MetodosSQL.GetField(sql, "Cont") == "0")
                        {
                            sql = String.Format(@"update ZTMOVCOMISSAO
                                              set NCARTA = '{0}',
                                              TIPOPAG = 'CRÉDITO'
                                              where IDMOV = (select TM.IDMOV from TMOV TM where TM.CODCOLIGADA = 1 and TM.CODTMV = '2.1.10' and TM.NUMEROMOV = '{1}')",
                                              CARTA, SelectedRow["NUMEROMOV"].ToString());
                            MetodosSQL.ExecQuery(sql);
                            gridData4.Atualizar();
                            gridData2.Atualizar();
                        }
                        else
                        {
                            MessageBox.Show(SelectedRow["NUMEROMOV"].ToString() + " já foi adicionado");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este item já foi adicionado");
                    }

                }
                else
                {
                    DataRow SelectedRow = gridData2.GetDataRow();

                    if (dt2.Rows.Cast<DataRow>().Where(x => x["NUMEROMOV"].ToString() == SelectedRow["NUMEROMOV"].ToString()).Count() == 0)
                    {
                        dt2.Rows.Add(SelectedRow["IDMOV"].ToString(),
                            SelectedRow["NUMEROMOV"].ToString(),
                            SelectedRow["CODCFO"].ToString(),
                            SelectedRow["NOMEFANTASIA"].ToString(),
                            SelectedRow["CODRPR"].ToString(),
                            SelectedRow["NOME"].ToString(),
                            SelectedRow["STATUS"].ToString(),
                            SelectedRow["BAIXA"].ToString(),
                            SelectedRow["VALORLIQUIDOORIG"].ToString(),
                            SelectedRow["PERCENTUALCOMISSAO"].ToString());

                        gridData4.gridControl1.DataSource = dt2;
                        gridData4.gridView1.BestFitColumns();
                    }
                    else
                    {
                        MessageBox.Show("Este item já foi adicionado");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdicionaMultiplos(object sender, EventArgs e)
        {
            try
            {
                if (UPDT)
                {
                    DataRowCollection drc = gridData1.GetDataRows();



                    foreach (DataRow SelectedRow in drc)
                    {

                        string sql = String.Format(@"select count(1) as 'Cont' from ZCOMISSAOCARTA ZCC

                                                     inner join ZTMOVCOMISSAO ZTC
                                                     on ZTC.NCARTA = ZCC.CODCARTA

                                                     where ZTC.IDMOV = (select IDMOV from TMOV where CODCOLIGADA = 1 and CODTMV = '2.1.10' and NUMEROMOV = '{0}')",
                                                     SelectedRow["NUMEROMOV"].ToString());


                        if (MetodosSQL.GetField(sql, "Cont") == "0")
                        {
                            sql = String.Format(@"update ZTMOVCOMISSAO
                                              set NCARTA = '{0}',
                                              TIPOPAG = 'DÉBITO'
                                              where IDMOV = (select TM.IDMOV from TMOV TM where TM.CODCOLIGADA = 1 and TM.CODTMV = '2.1.10' and TM.NUMEROMOV = '{1}')",
                                              CARTA, SelectedRow["NUMEROMOV"].ToString());
                            MetodosSQL.ExecQuery(sql);
                            gridData3.Atualizar();
                            gridData1.Atualizar();
                        }
                        else
                        {
                            MessageBox.Show(SelectedRow["NUMEROMOV"].ToString() + " já foi adicionado");
                        }
                    }
                }
                else
                {
                    DataRowCollection drc = gridData1.GetDataRows();

                    foreach (DataRow SelectedRow in drc)
                    {
                        if (dt.Rows.Cast<DataRow>().Where(x => x["IDMOV"].ToString() == SelectedRow["IDMOV"].ToString()).Count() == 0)
                        {
                            dt.Rows.Add(SelectedRow["IDMOV"].ToString(),
                                SelectedRow["NUMEROMOV"].ToString(),
                                SelectedRow["NOMEFANTASIA"].ToString(),
                                SelectedRow["REPRESENTANTE"].ToString(),
                                SelectedRow["VALORLIQUIDOORIG"].ToString(),
                                SelectedRow["VALORBRUTOORIG"].ToString(),
                                SelectedRow["STATUS"].ToString());
                        }
                        else
                        {
                            MessageBox.Show(SelectedRow["NUMEROMOV"].ToString() + " já foi adicionado");
                        }
                    }

                    gridData3.gridControl1.DataSource = dt;
                    gridData3.gridView1.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AdicionaMultiplos2(object sender, EventArgs e)
        {
            try
            {


                if (UPDT)
                {
                    DataRowCollection drc = gridData2.GetDataRows();



                    foreach (DataRow SelectedRow in drc)
                    {

                        string sql = String.Format(@"select count(1) as 'Cont' from ZCOMISSAOCARTA ZCC

                                                     inner join ZTMOVCOMISSAO ZTC
                                                     on ZTC.NCARTA = ZCC.CODCARTA

                                                     where ZTC.IDMOV = (select IDMOV from TMOV where CODCOLIGADA = 1 and CODTMV = '2.1.10' and NUMEROMOV = '{0}')",
                                                     SelectedRow["NUMEROMOV"].ToString());


                        if (MetodosSQL.GetField(sql, "Cont") == "0")
                        {
                            sql = String.Format(@"update ZTMOVCOMISSAO
                                              set NCARTA = '{0}',
                                              TIPOPAG = 'CRÉDITO'
                                              where IDMOV = (select TM.IDMOV from TMOV TM where TM.CODCOLIGADA = 1 and TM.CODTMV = '2.1.10' and TM.NUMEROMOV = '{1}')",
                                              CARTA, SelectedRow["NUMEROMOV"].ToString());
                            MetodosSQL.ExecQuery(sql);
                            gridData4.Atualizar();
                            gridData2.Atualizar();
                        }
                        else
                        {
                            MessageBox.Show(SelectedRow["NUMEROMOV"].ToString() + " já foi adicionado");
                        }
                    }
                }
                else
                {
                    DataRowCollection drc = gridData2.GetDataRows();



                    foreach (DataRow SelectedRow in drc)
                    {
                        if (dt2.Rows.Cast<DataRow>().Where(x => x["IDMOV"].ToString() == SelectedRow["IDMOV"].ToString()).Count() == 0)
                        {
                            dt2.Rows.Add(SelectedRow["IDMOV"].ToString(),
                                SelectedRow["NUMEROMOV"].ToString(),
                                SelectedRow["CODCFO"].ToString(),
                                SelectedRow["NOMEFANTASIA"].ToString(),
                                SelectedRow["CODRPR"].ToString(),
                                SelectedRow["NOME"].ToString(),
                                SelectedRow["STATUS"].ToString(),
                                SelectedRow["BAIXA"].ToString(),
                                SelectedRow["VALORLIQUIDOORIG"].ToString(),
                                SelectedRow["PERCENTUALCOMISSAO"].ToString());

                        }
                        else
                        {
                            MessageBox.Show(SelectedRow["NUMEROMOV"].ToString() + " já foi adicionado");
                        }
                    }

                    gridData4.gridControl1.DataSource = dt2;
                    gridData4.gridView1.BestFitColumns();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private Boolean SalvarCarta(Boolean update)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtVCred.textEdit1.Text))
                {
                    txtVCred.textEdit1.Text = "0,0";
                }

                if (String.IsNullOrWhiteSpace(txtVDeb.textEdit1.Text))
                {
                    txtVDeb.textEdit1.Text = "0,0";
                }

                object NCarta = CARTA;
                string sql = String.Empty;

                if (!update)
                {
                    sql = (String.Format(@"insert into ZCOMISSAOCARTA (CODRPR,STATUS,USUARIOCRIACAO,DATACRIACAO) 
                                              values ('{0}','ABERTA','{1}',getdate()) select SCOPE_IDENTITY()", CODREPRESENTANTE, AppLib.Context.Usuario));

                    NCarta = MetodosSQL.ExecScalar(sql);
                }


                if (NCarta != null)
                {

                    lblNCarta.Text = NCarta.ToString();

                    double VCred = 0.0;
                    double VDeb = -VDebitoAnterior;
                    string IDMOV = String.Empty;

                    for (int i = 0; i < gridData4.gridView1.DataRowCount; i++)
                    {


                        sql = String.Format("select TM.IDMOV from TMOV TM where TM.CODCOLIGADA = 1 and TM.CODTMV = '2.1.10' and TM.NUMEROMOV = '{0}'", gridData4.gridView1.GetRowCellValue(i, "NUMEROMOV").ToString());

                        IDMOV = MetodosSQL.GetField(sql, "IDMOV");


                        sql = String.Format(@"update ZTMOVCOMISSAO
                                              set NCARTA = '{0}',
                                              TIPOPAG = 'CRÉDITO',
											  PERCENTUALCOMISSAO = {2}
                                              where IDMOV = '{1}'",
                                              NCarta.ToString(), IDMOV, gridData4.gridView1.GetRowCellValue(i, "PERCENTUALCOMISSAO").ToString().Replace(",", "."));

                        MetodosSQL.ExecQuery(sql);


                    }

                    sql = String.Format(@"select sum(cast(TM.VALORLIQUIDOORIG*(ZTC.PERCENTUALCOMISSAO/100)  as  numeric(10,2))) as 'TOTAL' from ZTMOVCOMISSAO ZTC

                                          inner join TMOV TM
                                          on TM.IDMOV = ZTC.IDMOV

                                          where ZTC.TIPOPAG = 'CRÉDITO'
                                          and ZTC.NCARTA = {0}", NCarta);

                    VCred = Double.Parse(MetodosSQL.GetField(sql, "TOTAL"));

                    for (int i = 0; i < gridData3.gridView1.DataRowCount; i++)
                    {
                        string NMOV = gridData3.gridView1.GetRowCellValue(i, "NUMEROMOV").ToString();

                        sql = String.Format(@"update ZTMOVCOMISSAO
                                              set NCARTA = '{0}',
                                              TIPOPAG = 'DÉBITO'
                                              where IDMOV = (select TM.IDMOV from TMOV TM where TM.CODCOLIGADA = 1 and TM.CODTMV = '2.1.10' and TM.NUMEROMOV = '{1}')",
                                              NCarta.ToString(), NMOV);

                        MetodosSQL.ExecQuery(sql);

                        sql = String.Format(@"SELECT top 1 cast(FLAN.VALORORIGINAL as numeric(10,2)) as 'VALOR' FROM FLAN

										  left join FLANBAIXA 
										  on FLANBAIXA.CODCOLIGADA = FLAN.CODCOLIGADA
										  and FLANBAIXA.IDLAN = FLAN.IDLAN

										  WHERE FLAN.IDMOV in (SELECT 
																  IDMOVDESTINO 
															  FROM 
																  TMOVRELAC 
															  WHERE 
																  CODCOLORIGEM = 1
															  AND IDMOVORIGEM = (select IDMOV from TMOV where CODCOLIGADA = 1 and CODTMV = '2.1.20' and NUMEROMOV = '{0}')
															  AND IDMOVDESTINO NOT IN (SELECT IDMOV FROM TMOV WHERE CODTMV IN ('2.1.40')))
										  AND FLAN.CODCOLIGADA = 1
										  AND FLAN.CODTDO <> 'ADC'", NMOV);

                        VDeb += Double.Parse(MetodosSQL.GetField(sql, "VALOR"));
                    }

                    sql = String.Format(@"update ZCOMISSAOCARTA
                                          set VALORCREDITO = '{0}',
                                           VALORDEBITO = '{1}',
                                           VALORCREDITOCOMPL = {2},
                                           VALORDEBITOCOMPL = {3},
                                           OBSERVACAOCREDITOCOMPL = '{4}',
                                           OBSERVACAODEBITOCOMPL = '{5}'
                                          where CODCARTA = '{6}'",
                                          VCred.ToString().Replace(",", "."),
                                          VDeb.ToString().Replace(",", "."),
                                          txtVCred.Get().ToString().Replace(",", "."),
                                          txtVDeb.Get().ToString().Replace(",", "."),
                                          txtObsCred.Get(),
                                          txtObsDeb.Get(),
                                          NCarta.ToString());
                    

                    MetodosSQL.ExecQuery(sql);

                    CARTA = NCarta.ToString();

                    SqlConnection connection = MetodosSQL.GetConnection();

                    try
                    {
                        connection.Open();

                        if (ConteudoCarta != null)
                        {
                            SqlCommand cmd = new SqlCommand(String.Format(@"update ZCOMISSAOCARTA set ANEXOCARTA = @Carta where CODCARTA = {0}", CARTA), connection);
                            cmd.Parameters.Add("@Carta", SqlDbType.VarBinary).Value = ConteudoCarta;


                            cmd.ExecuteNonQuery();
                        }

                        if (ConteudoNFe != null)
                        {
                            SqlCommand cmd = new SqlCommand(String.Format(@"update ZCOMISSAOCARTA set ANEXONFE = @Nfe where CODCARTA = {0}", CARTA), connection);
                            cmd.Parameters.Add("@Nfe", SqlDbType.VarBinary).Value = ConteudoNFe;


                            cmd.ExecuteNonQuery();
                        }

                        if (ConteudoCarta != null && ConteudoNFe != null)
                        {
                            if (DialogResult.Yes == MessageBox.Show("Deseja finalizar a carta?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                sql = String.Format(@"select STATUS from ZCOMISSAOCARTA where CODCARTA = {0}", CARTA);

                                if (MetodosSQL.GetField(sql, "STATUS") == "ABERTA")
                                {
                                    sql = String.Format(@"update ZCOMISSAOCARTA
                                                          set STATUS = 'CONCLUIDO'
                                                          where CODCARTA = {0}", CARTA);
                                    MetodosSQL.ExecQuery(sql);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex);
                    }
                    finally
                    {
                        connection.Close();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        private Boolean RemoveDebito(Boolean update)
        {
            try
            {
                string sql = String.Empty;

                DataRowCollection drc = gridData3.GetDataRows();


                foreach (DataRow SelectedRow in drc)
                {

                    if (update)
                    {
                        sql = String.Format(@"update ZTMOVCOMISSAO
                                              set NCARTA = null,
											  TIPOPAG = ''
                                              where NCARTA = '{0}'
                                              and IDMOV = (select IDMOV from TMOV where CODTMV = '2.1.10' and NUMEROMOV = '{1}')",
                                              CARTA, SelectedRow["NUMEROMOV"].ToString());
                        MetodosSQL.ExecQuery(sql);
                    }
                    else
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["IDMOV"].ToString() == SelectedRow["IDMOV"].ToString())
                            {
                                row.Delete();
                                break;
                            }
                        }

                        gridData3.gridControl1.DataSource = dt;

                        //for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        //{
                        //    DataRow dr = dt.Rows[i];
                        //    if (dr["IDMOV"].ToString() == SelectedRow["IDMOV"].ToString())
                        //        dr.Delete();
                        //}
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private Boolean RemoveCredito(Boolean update)
        {
            try
            {
                string sql = String.Empty;

                DataRowCollection drc = gridData4.GetDataRows();


                foreach (DataRow SelectedRow in drc)
                {

                    if (update)
                    {
                        sql = String.Format(@"update ZTMOVCOMISSAO
                                              set NCARTA = null,
											  TIPOPAG = ''
                                              where NCARTA = '{0}'
                                              and IDMOV = (select IDMOV from TMOV where CODTMV = '2.1.10' and NUMEROMOV = '{1}')",
                                              CARTA, SelectedRow["NUMEROMOV"].ToString());
                        MetodosSQL.ExecQuery(sql);
                    }
                    else
                    {

                        foreach (DataRow row in dt2.Rows)
                        {
                            if (row["IDMOV"].ToString() == SelectedRow["IDMOV"].ToString())
                            {
                                row.Delete();
                                break;
                            }
                        }

                        gridData4.gridControl1.DataSource = dt2;

                        //for (int i = dt2.Rows.Count - 1; i >= 0; i--)
                        //{
                        //    DataRow dr = dt2.Rows[i];
                        //    if (dr["IDMOV"].ToString() == SelectedRow["IDMOV"].ToString())
                        //    {
                        //        dr.Delete();
                        //    }
                        //}
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void gridData4_SetParametros(object sender, EventArgs e)
        {
            gridData4.Parametros = new Object[] { CODREPRESENTANTE, CARTA };
            gridData3.Parametros = new Object[] { CODREPRESENTANTE, CARTA };
            gridData3.Atualizar();
        }

        private void gridData3_Excluir(object sender, EventArgs e)
        {
            if (RemoveDebito(UPDT))
            {
                gridData3.Atualizar();
                gridData1.Atualizar();
            }

        }

        private void gridData4_Excluir(object sender, EventArgs e)
        {
            if (RemoveCredito(UPDT))
            {
                gridData4.Atualizar();
                gridData2.Atualizar();
            }
        }

        private void btnSelecionaCarta_Click(object sender, EventArgs e)
        {
            if (ofdComissao.ShowDialog() == DialogResult.OK)
            {
                FileInfo f = new FileInfo(ofdComissao.FileName);

                if (f.Extension.ToUpper() != ".PDF")
                {
                    MessageBox.Show("Somente arquivos PDF são aceitos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ConteudoCarta = File.ReadAllBytes(ofdComissao.FileName);

                    btnVisualizaCarta.Visible = true;
                    btnVisualizaCarta.Enabled = true;
                }
            }
        }

        private void btnSelecionaNFe_Click(object sender, EventArgs e)
        {
            if (ofdComissao.ShowDialog() == DialogResult.OK)
            {
                FileInfo f = new FileInfo(ofdComissao.FileName);

                if (f.Extension.ToUpper() != ".PDF")
                {
                    MessageBox.Show("Somente arquivos PDF são aceitos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ConteudoNFe = File.ReadAllBytes(ofdComissao.FileName);

                    btnVisualizaNFe.Visible = true;
                    btnVisualizaNFe.Enabled = true;
                }
            }
        }

        private void AlterarComissao(object sender, EventArgs e)
        {
            try
            {
                FormComissaoCartaValorComissao f = new FormComissaoCartaValorComissao();
                f.ShowDialog();

                DataRowCollection drc = gridData2.GetDataRows();

                string sql = String.Empty;

                foreach (DataRow row in drc)
                {
                    sql = String.Format(@"update ZTMOVCOMISSAO
                                         set PERCENTUALCOMISSAO = {0}
                                         where IDMOV = (select IDMOV from TMOV where NUMEROMOV = {1} and CODTMV = '2.1.10')",
                                         f.porcentagem.Replace(",", "."), row["NUMEROMOV"]);
                    MetodosSQL.ExecQuery(sql);
                }

                gridData2.Atualizar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnVisualizaCarta_Click(object sender, EventArgs e)
        {
            pdfViewer1.Visible = true;

            Stream stream = new MemoryStream(ConteudoCarta);

            pdfViewer1.LoadDocument(stream);
        }

        private void btnVisualizaNFe_Click(object sender, EventArgs e)
        {
            pdfViewer1.Visible = true;

            Stream stream = new MemoryStream(ConteudoNFe);

            pdfViewer1.LoadDocument(stream);
        }

        private void btnSalvarCarta_Click(object sender, EventArgs e)
        {
            fbdComissao.ShowDialog();

            FileStream fs = new FileStream(fbdComissao.SelectedPath + @"\Carta " + CARTA + " - Carta.pdf", FileMode.Create);
            fs.Write(ConteudoCarta, 0, ConteudoCarta.Length);
            fs.Close();
        }

        private void btnSalvarNFe_Click(object sender, EventArgs e)
        {
            fbdComissao.ShowDialog();

            FileStream fs = new FileStream(fbdComissao.SelectedPath + @"\Carta " + CARTA + " - NFe.pdf", FileMode.Create);
            fs.Write(ConteudoNFe, 0, ConteudoNFe.Length);
            fs.Close();
        }

        private void gridData4_AposAtualizar(object sender, EventArgs e)
        {
            if (!UPDT)
            {
                gridData4.gridControl1.DataSource = dt2;
            }
        }

        private void gridData3_AposAtualizar(object sender, EventArgs e)
        {
            if (!UPDT)
            {
                gridData3.gridControl1.DataSource = dt;
            }
        }

        private void btnDeletarCarta_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = String.Format(@"update ZCOMISSAOCARTA
                                         set ANEXOCARTA = null
                                         where CODCARTA = {0}", CARTA);
                MetodosSQL.ExecQuery(sql);

                btnVisualizaCarta.Visible = false;
                btnVisualizaCarta.Enabled = false;
                btnSalvarCarta.Visible = false;
                btnDeletarCarta.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDeletarNF_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = String.Format(@"update ZCOMISSAOCARTA
                                         set ANEXONFE = null
                                         where CODCARTA = {0}", CARTA);
                MetodosSQL.ExecQuery(sql);

                btnVisualizaNFe.Visible = false;
                btnVisualizaNFe.Enabled = false;
                btnSalvarNFe.Visible = false;
                btnDeletarNF.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridData2_AposAtualizar(object sender, EventArgs e)
        {
            gridData2.gridView1.RowStyle += PintaGrid4;
        }

        private void PintaGrid4(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView View = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["IDMOV"]);


                foreach (DataRow row in dt2.Rows)
                {
                    if (category == row["IDMOV"].ToString())
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                }
            }
        }

        private void gridData1_AposAtualizar(object sender, EventArgs e)
        {
            gridData1.gridView1.RowStyle += PintaGrid3;
        }

        private void PintaGrid3(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView View = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["IDMOV"]);


                foreach (DataRow row in dt.Rows)
                {
                    if (category == row["IDMOV"].ToString())
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                }
            }
        }
    }
}
