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

namespace AppLib.Windows
{
    public partial class FormSelecionaItemComposicao : DevExpress.XtraEditors.XtraForm
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

        public int IDPRD { get; set; }
        public decimal QUANTIDADE { get; set; }
        public decimal PRECO { get; set; }
        public FormSelecionaItemComposicao()
        {
            InitializeComponent();

            BotaoNovo = false;
            BotaoExcluir = false;

            Conexao = "Start";

            decimalQuantidade.Set(1);
            decimalPreco.Set(0);
        }

        private void setConsulta()
        {
            clProduto.ColunaTabela = String.Format(@"(select IDPRD,
	                                                           CODIGOPRD,
	                                                           CODIGOAUXILIAR,
	                                                           NOMEFANTASIA
                                                        from TPRODUTO 
                                                        where CODCOLPRD = 1 
                                                        and INATIVO = 0 and idprd in (83093, 82749)) W");
        }

        private void FormCadastro2_Load(object sender, EventArgs e)
        {
            setConsulta();

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
            





        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(clProduto.Get()))
                {
                    throw new Exception("Favor informar o produto.");
                }

                string sql = String.Format(@"select IDPRD
                                                from TPRODUTO 
                                                where CODCOLPRD = 1 
                                                AND CODIGOPRD = '{0}'", clProduto.Get());

                DataTable dt = MetodosSQL.GetDT(sql);

                foreach (DataRow item in dt.Rows)
                {
                    
                    IDPRD = (int)item["IDPRD"];
                    QUANTIDADE = (decimal)decimalQuantidade.Get();
                    PRECO = (decimal)decimalPreco.Get();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void txtValorOriginal_Leave(object sender, EventArgs e)
        {

        }

        private void txtValorRateio_Leave(object sender, EventArgs e)
        {

        }

        private void txtValorRateio_Load(object sender, EventArgs e)
        {

        }

        private void CalculaPorcentagemRateio()
        {

        }



        private void gridData1_Excluir(object sender, EventArgs e)
        {

        }

        private void clCentroCusto_AposSelecao(object sender, EventArgs e)
        {

        }

        private void dtaVencimento_Validated(object sender, EventArgs e)
        {

        }

        private void dtaVencimento_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtaVencimento_MouseLeave(object sender, EventArgs e)
        {

        }

        private void dtaPrevBaixa_Validated(object sender, EventArgs e)
        {

        }
    }
}
