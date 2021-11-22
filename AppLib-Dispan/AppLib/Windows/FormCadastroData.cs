using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class FormCadastroData : DevExpress.XtraEditors.XtraForm
    {
        #region PROPRIEDADES APP

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

        [Category("_APP"), Description("Habilitar barra de navegação")]
        public Boolean BarraNavegacao { get; set; }

        [Category("_APP"), Description("Habilitar botão salvar")]
        public Boolean BotaoSalvar { get; set; }

        [Category("_APP"), Description("Habilitar botão salvar como")]
        public Boolean BotaoSalvarComo { get; set; }

        [Category("_APP"), Description("Habilitar botão OK")]
        public Boolean BotaoOK { get; set; }

        #endregion

        #region PROPRIEDADES PADRÕES

        [Category("_padrao"), Description("Ação que acionou o form")]
        public Global.Types.Acao Acao { get; set; }

        [Category("_padrao"), Description("Registro da grid")]
        private DataRow dr;

        [Category("_padrao"), Description("Registro em edição")]
        public AppLib.ORM.Jit x = new ORM.Jit();

        [Category("_padrao"), Description("Chave estrangeira")]
        public List<AppLib.ORM.CampoValor> Chave = new List<ORM.CampoValor>();

        #endregion

        #region CONSTRUTORES

        public FormCadastroData()
        {
            InitializeComponent();

            BotaoNovo = true;
            BotaoExcluir = true;
            BotaoSalvar = true;
            BotaoSalvarComo = true;
            BotaoOK = true;
            BarraNavegacao = false;

            //Conexao = "Start";
            Conexao = "Start";
        }

        private void FormCadastro_Load(object sender, EventArgs e)
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

                try
                {
                    if (BotaoSalvar)
                    {
                        barButtonItemSalvar.Enabled = true;
                    }
                    else
                    {
                        barButtonItemSalvar.Enabled = false;
                    }
                }
                catch (Exception) { }

                try
                {
                    if (BotaoSalvarComo)
                    {
                        barButtonItemSalvarComo.Enabled = true;
                    }
                    else
                    {
                        barButtonItemSalvarComo.Enabled = false;
                    }
                }
                catch (Exception) { }

                try
                {
                    if ((BotaoSalvar) && (BotaoSalvarComo))
                    {
                        dropDownButtonSalvar.Enabled = true;
                    }

                    if ((!BotaoSalvar) && (!BotaoSalvarComo))
                    {
                        dropDownButtonSalvar.Enabled = false;
                    }
                }
                catch (Exception) { }

                try
                {
                    if (BotaoOK)
                    {
                        simpleButtonOK.Enabled = true;
                    }
                    else
                    {
                        simpleButtonOK.Enabled = false;
                    }
                }
                catch (Exception) { }

                try
                {
                    if (BarraNavegacao)
                    {
                        toolStripButtonPRIMEIRO.Enabled = true;
                        toolStripButtonANTERIOR.Enabled = true;
                        toolStripButtonPROXIMO.Enabled = true;
                        toolStripButtonULTIMO.Enabled = true;
                    }
                    else
                    {
                        toolStripButtonPRIMEIRO.Enabled = false;
                        toolStripButtonANTERIOR.Enabled = false;
                        toolStripButtonPROXIMO.Enabled = false;
                        toolStripButtonULTIMO.Enabled = false;
                    }
                }
                catch (Exception) { }

            }
        }

        private void FormCadastroData_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManager.ReleaseUnusedMemory(false);
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
                    this.CarregarChave();

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

        public void toolStripButtonEXCLUIR_Click(object sender, EventArgs e)
        {
            if (BotaoExcluir)
            {
                if (new Security.Access().Excluir(this.Conexao, this.TabelaPrincipal, AppLib.Context.Perfil))
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

                            this.Excluir();

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
                    MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso para excluir este cadastro (" + this.TabelaPrincipal + ").", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (BotaoOK)
            {

                if (new Security.Access().Cadastrar(this.Conexao, this.TabelaPrincipal, AppLib.Context.Perfil))
                {
                    Boolean validou;

                    try
                    {
                        validou = this.ValidarSalvar(this, null);
                    }
                    catch
                    {
                        validou = true;
                    }

                    if (validou)
                    {
                        try
                        {
                            this.AntesOK(this, null);
                        }
                        catch { }

                        try
                        {
                            this.AntesSalvar(this, null);
                        }
                        catch { }

                        try
                        {
                            splashScreenManager1.ShowWaitForm();

                            if (this.Salvar())
                            {
                                try
                                {
                                    this.AposSalvar(this, null);

                                    splashScreenManager1.CloseWaitForm();
                                }
                                catch
                                {
                                    splashScreenManager1.CloseWaitForm();
                                }

                                try
                                {
                                    this.AposOK(this, null);
                                }
                                catch { }

                                this.Close();
                            }
                        }
                        catch 
                        {
                            splashScreenManager1.CloseWaitForm();
                        }                     
                    }
                }
                else
                {
                    MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso para salvar este cadastro (" + this.TabelaPrincipal + ").", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void dropDownButtonSalvar_Click(object sender, EventArgs e)
        {
            if (BotaoSalvar)
            {
                if (new Security.Access().Cadastrar(this.Conexao, this.TabelaPrincipal, AppLib.Context.Perfil))
                {
                    Boolean validou;

                    try
                    {
                        validou = this.ValidarSalvar(this, null);
                    }
                    catch
                    {
                        validou = true;
                    }

                    if (validou)
                    {
                        try
                        {
                            this.AntesSalvar(this, null);
                        }
                        catch { }

                        try
                        {
                            splashScreenManager1.ShowWaitForm();

                            if (this.Salvar())
                            {
                                try
                                {
                                    this.AposSalvar(this, null);

                                    splashScreenManager1.CloseWaitForm();
                                }
                                catch
                                {
                                    splashScreenManager1.CloseWaitForm();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Erro ao salvar o registro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso para salvar este cadastro (" + this.TabelaPrincipal + ").", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void barButtonItemSalvar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dropDownButtonSalvar_Click(this, null);
        }

        private void barButtonItemSalvarComo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (BotaoSalvarComo)
            {
                if (new Security.Access().Cadastrar(this.Conexao, this.TabelaPrincipal, AppLib.Context.Perfil))
                {

                    try
                    {
                        // adiciona tabela principal
                        List<String> tabelas = new List<string>();
                        tabelas.Add(TabelaPrincipal);

                        // e demais tabelas
                        List<String> tabelasTemp = this.getTabelas();
                        for (int i = 0; i < tabelasTemp.Count; i++)
                        {
                            if (!tabelas.Contains(tabelasTemp[i]))
                            {
                                tabelas.Add(tabelasTemp[i]);
                            }
                        }

                        for (int i = 0; i < tabelas.Count; i++)
                        {
                            x = new ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), tabelas[i]);
                            this.CarregarObjeto(tabelas[i], this.Controls);
                            x.Select(); // PARA CARREGAR ATRAVÉS DAS CHAVES TODOS OS CAMPOS DO BANCO
                        }

                        this.LimparChave();

                        try
                        {
                            this.AntesSalvarComo(this, null);
                        }
                        catch { }

                        this.dropDownButtonSalvar_Click(this, null);

                        try
                        {
                            this.AposSalvarComo(this, null);
                        }
                        catch { }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro do método salvar como:\r\n\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Seu perfil (" + AppLib.Context.Perfil + ") não possui acesso para salvar este cadastro (" + this.TabelaPrincipal + ").", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
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
                this.CarregarChave();

                try
                {
                    this.AposEditar(this, null);
                }
                catch { }
            }
        }

        public void Editar(AppLib.Windows.GridData grid1)
        {
            this.Editar(grid1.bs);
        }

        public void Excluir(System.Data.DataRowCollection registros)
        {
            Acao = Global.Types.Acao.Excluir;
            if (registros != null)
            {
                for (int i = 0; i < registros.Count; i++)
                {
                    this.Editar(registros[i], false);
                    this.CarregarChave();
                    toolStripButtonEXCLUIR_Click(this, null);
                }
            }
        }

        public void Excluir(AppLib.Windows.GridData grid1)
        {
            this.Excluir(grid1.GetDataRows());
        }

        public System.Windows.Forms.ToolStripItemCollection GetAnexos()
        {
            return this.toolStripButtonANEXOS.DropDownItems;
        }

        public System.Windows.Forms.ToolStripItemCollection GetProcessos()
        {
            return this.toolStripButtonPROCESSOS.DropDownItems;
        }

        public void Atualizar()
        {
            this.Editar(dr, false);
        }

        #endregion

        #region MÉTODOS INTERNOS

        public void CarregarChave()
        {
            for (int i = 0; i < Chave.Count; i++)
            {
                String campo = Chave[i].Campo;
                Object valor = Chave[i].Valor;

                this.CarregarChave(this.Controls, campo, valor);
            }
        }

        private void CarregarChave(System.Windows.Forms.Control.ControlCollection componentes, String Campo, Object Valor)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                this.CarregarChave(componentes[i], Campo, Valor);
            }
        }

        private void CarregarChave(System.Windows.Forms.Control componente, String Campo, Object Valor)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                CarregarChave(componente.Controls, Campo, Valor);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                CarregarChave(componente.Controls, Campo, Valor);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                CarregarChave(componente.Controls, Campo, Valor);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                CarregarChave(componente.Controls, Campo, Valor);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                CarregarChave(componente.Controls, Campo, Valor);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                CarregarChave(componente.Controls, Campo, Valor);
            }

            #endregion

            #region COMPONENTES LIB

            if (componente.GetType() == typeof(CampoInteiro))
            {
                CampoInteiro campo = (CampoInteiro)componente;
                if (campo.Campo.ToUpper().Equals(Campo))
                {
                    if (Valor.ToString().Equals(""))
                    {
                        campo.Set(null);
                    }
                    else
                    {
                        campo.Set(int.Parse(Valor.ToString()));
                    }
                }
            }

            if (componente.GetType() == typeof(CampoDecimal))
            {
                CampoDecimal campo = (CampoDecimal)componente;
                if (campo.Campo.ToUpper().Equals(Campo))
                {
                    if (Valor.ToString().Equals(""))
                    {
                        campo.Set(null);
                    }
                    else
                    {
                        campo.Set(decimal.Parse(Valor.ToString()));
                    }
                }
            }

            if (componente.GetType() == typeof(CampoData))
            {
                CampoData campo = (CampoData)componente;
                if (campo.Campo.ToUpper().Equals(Campo))
                {
                    if (Valor.ToString().Equals(""))
                    {
                        campo.Set(null);
                    }
                    else
                    {
                        campo.Set(DateTime.Parse(Valor.ToString()));
                    }
                }
            }

            if (componente.GetType() == typeof(CampoHora))
            {
                CampoHora campo = (CampoHora)componente;
                if (campo.Campo.ToUpper().Equals(Campo))
                {
                    if (Valor.ToString().Equals(""))
                    {
                        campo.Set(null);
                    }
                    else
                    {
                        campo.Set(DateTime.Parse(Valor.ToString()));
                    }
                }
            }

            if (componente.GetType() == typeof(CampoDataHora))
            {
                CampoDataHora campo = (CampoDataHora)componente;
                if (campo.Campo.ToUpper().Equals(Campo))
                {
                    if (Valor.ToString().Equals(""))
                    {
                        campo.Set(null);
                    }
                    else
                    {
                        campo.Set(DateTime.Parse(Valor.ToString()));
                    }
                }
            }

            if (componente.GetType() == typeof(CampoTexto))
            {
                CampoTexto campo = (CampoTexto)componente;
                if (campo.Campo.ToUpper().Equals(Campo))
                {
                    if (Valor.ToString().Equals(""))
                    {
                        campo.Set(null);
                    }
                    else
                    {
                        campo.Set(Valor.ToString());
                    }
                }
            }

            if (componente.GetType() == typeof(CampoMemo))
            {
                CampoMemo campo = (CampoMemo)componente;
                if (campo.Campo.ToUpper().Equals(Campo))
                {
                    if (Valor.ToString().Equals(""))
                    {
                        campo.Set(null);
                    }
                    else
                    {
                        campo.Set(Valor.ToString());
                    }
                }
            }

            if (componente.GetType() == typeof(CampoLista))
            {
                CampoLista campo = (CampoLista)componente;
                if (campo.Campo.ToUpper().Equals(Campo))
                {
                    if (Valor.ToString().Equals(""))
                    {
                        campo.Set(null);
                    }
                    else
                    {
                        campo.Set(Valor.ToString());
                    }
                }
            }

            if (componente.GetType() == typeof(CampoLookup))
            {
                CampoLookup campo = (CampoLookup)componente;
                if (campo.Campo.ToUpper().Equals(Campo))
                {
                    if (Valor == null)
                    {
                        campo.Set(null);
                    }
                    else
                    {
                        if (Valor.ToString().Equals(""))
                        {
                            campo.Set(null);
                        }
                        else
                        {
                            campo.textBoxCODIGO.Text = Valor.ToString();
                            campo.textBox1_Leave(this, null);
                        }
                    }
                }
            }

            if (componente.GetType() == typeof(CampoImagem))
            {
                CampoImagem campo = (CampoImagem)componente;

                // nome imagem
                if (campo.ColunaNomeImagem.ToUpper().Equals(Campo))
                {
                    if (Valor.ToString().Equals(""))
                    {
                        campo.SetNomeImagem(null);
                    }
                    else
                    {
                        campo.SetNomeImagem(Valor.ToString());
                    }
                }

                // imagem
                if (campo.ColunaImagem.ToUpper().Equals(Campo))
                {
                    if (Valor.ToString().Equals(""))
                    {
                        campo.SetImagem(null);
                    }
                    else
                    {
                        campo.SetImagem(Valor.ToString());
                    }
                }
            }

            if (componente.GetType() == typeof(CampoArquivo))
            {
                CampoArquivo campo = (CampoArquivo)componente;
                if (campo.Campo.ToUpper().Equals(Campo))
                {
                    if (Valor == null)
                    {
                        campo.Set(null);
                    }
                    else
                    {
                        if (Valor.ToString().Equals(""))
                        {
                            campo.Set(null);
                        }
                        else
                        {
                            campo.textEditIDENTIFICADOR.Text = Valor.ToString();
                            campo.textEditIDENTIFICADOR_Leave(this, null);
                        }
                    }
                }
            }

            #endregion
        }

        private void LimparChave()
        {
            this.LimparChave(this.Controls);
        }

        private void LimparChave(System.Windows.Forms.Control.ControlCollection componentes)
        {
            for (int i = 0; i < componentes.Count; i++)
            {
                this.LimparChave(componentes[i]);
            }
        }

        private void LimparChave(System.Windows.Forms.Control componente)
        {
            #region COMPONENTES NATIVO

            if (componente.GetType() == typeof(TabControl))
            {
                TabControl campo = (TabControl)componente;
                LimparChave(componente.Controls);
            }

            if (componente.GetType() == typeof(TabPage))
            {
                TabPage campo = (TabPage)componente;
                LimparChave(componente.Controls);
            }

            if (componente.GetType() == typeof(GroupBox))
            {
                GroupBox campo = (GroupBox)componente;
                LimparChave(componente.Controls);
            }

            if (componente.GetType() == typeof(Panel))
            {
                Panel campo = (Panel)componente;
                LimparChave(componente.Controls);
            }

            if (componente.GetType() == typeof(SplitContainer))
            {
                SplitContainer campo = (SplitContainer)componente;
                LimparChave(componente.Controls);
            }

            if (componente.GetType() == typeof(SplitterPanel))
            {
                SplitterPanel campo = (SplitterPanel)componente;
                LimparChave(componente.Controls);
            }

            #endregion

            #region COMPONENTES LIB

            if (componente.GetType() == typeof(CampoInteiro))
            {
                CampoInteiro campo = (CampoInteiro)componente;
                if (x.CampoChave(campo.Campo))
                {
                    campo.Set(null);
                }
            }

            if (componente.GetType() == typeof(CampoDecimal))
            {
                CampoDecimal campo = (CampoDecimal)componente;
                if (x.CampoChave(campo.Campo))
                {
                    campo.Set(null);
                }
            }

            if (componente.GetType() == typeof(CampoData))
            {
                CampoData campo = (CampoData)componente;
                if (x.CampoChave(campo.Campo))
                {
                    campo.Set(null);
                }
            }

            if (componente.GetType() == typeof(CampoHora))
            {
                CampoHora campo = (CampoHora)componente;
                if (x.CampoChave(campo.Campo))
                {
                    campo.Set(null);
                }
            }

            if (componente.GetType() == typeof(CampoDataHora))
            {
                CampoDataHora campo = (CampoDataHora)componente;
                if (x.CampoChave(campo.Campo))
                {
                    campo.Set(null);
                }
            }

            if (componente.GetType() == typeof(CampoTexto))
            {
                CampoTexto campo = (CampoTexto)componente;
                if (x.CampoChave(campo.Campo))
                {
                    campo.Set(null);
                }
            }

            if (componente.GetType() == typeof(CampoMemo))
            {
                CampoMemo campo = (CampoMemo)componente;
                if (x.CampoChave(campo.Campo))
                {
                    campo.Set(null);
                }
            }

            if (componente.GetType() == typeof(CampoLista))
            {
                CampoLista campo = (CampoLista)componente;
                if (x.CampoChave(campo.Campo))
                {
                    campo.Set(null);
                }
            }

            if (componente.GetType() == typeof(CampoLookup))
            {
                CampoLookup campo = (CampoLookup)componente;
                if (x.CampoChave(campo.Campo))
                {
                    campo.Set(null);
                }
            }

            if (componente.GetType() == typeof(CampoImagem))
            {
                CampoImagem campo = (CampoImagem)componente;
                if (x.CampoChave(campo.ColunaNomeImagem))
                {
                    campo.SetNomeImagem(null);
                    campo.SetImagem(null);
                }
            }

            if (componente.GetType() == typeof(CampoArquivo))
            {
                CampoArquivo campo = (CampoArquivo)componente;
                if (x.CampoChave(campo.Campo))
                {
                    campo.Set(null);
                }
            }

            #endregion
        }

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

            #region COMPONENTES DEV

            if (componente.GetType() == typeof(DevExpress.XtraTab.XtraTabControl))
            {
                DevExpress.XtraTab.XtraTabControl campo = (DevExpress.XtraTab.XtraTabControl)componente;
                LimparTela(campo.Controls);
            }

            if (componente.GetType() == typeof(DevExpress.XtraTab.XtraTabPage))
            {
                DevExpress.XtraTab.XtraTabPage campo = (DevExpress.XtraTab.XtraTabPage)componente;
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

            if (componente.GetType() == typeof(CampoArquivo))
            {
                CampoArquivo campo = (CampoArquivo)componente;
                campo.Clear();
            }

            if (componente.GetType() == typeof(CampoCheck))
            {
                CampoCheck campo = (CampoCheck)componente;
                campo.Set(campo.Default);
            }

            if (componente.GetType() == typeof(CampoCheckList))
            {
                CampoCheckList campo = (CampoCheckList)componente;
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

                if (this.Querys == null)
                {
                    Query q1 = new Query();
                    q1.Conexao = this.Conexao;

                    AppLib.ORM.Jit jit = new ORM.Jit(AppLib.Context.poolConnection.Get(this.Conexao), this.TabelaPrincipal);

                    String consultaTemp = "SELECT * FROM " + this.TabelaPrincipal + " WHERE ";
                    List<String> parametrosTemp = new List<String>();

                    for (int i = 0; i < jit.Metadados.Count; i++)
                    {
                        if (jit.Metadados[i].Chave)
                        {
                            consultaTemp += jit.Metadados[i].NomeCampo + " = ? ";
                            parametrosTemp.Add(jit.Metadados[i].NomeCampo);
                        }
                    }

                    q1.Consulta = new String[] { consultaTemp };
                    q1.Parametros = parametrosTemp.ToArray();

                    this.Querys = new Query[] { q1 };
                }

                // Carrega as querys
                for (int i = 0; i < this.Querys.Length; i++)
                {
                    Query q = this.Querys[i];

                    String consulta = "";

                    try
                    {
                        consulta = AppLib.Context.poolConnection.Get(q.Conexao).ParseCommand(q.GetConsulta(), this.MontarParametros(q.Parametros));
                        q.dt = AppLib.Context.poolConnection.Get(q.Conexao).ExecQuery(consulta, new Object[] { });

                        if (q.dt.Rows.Count > 0)
                        {
                            // Carrega os campos
                            this.CarregarForm(i, q.dt, this.Controls);
                        }
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

        public void Editar(System.Data.DataTable dt)
        {
            this.Editar(dt.Rows[0], true);
        }

        public virtual void Excluir()
        {
            try
            {
                AppLib.Context.poolConnection.Get(Conexao).BeginTransaction();

                // adiciona tabela principal
                List<String> tabelas = new List<string>();
                tabelas.Add(TabelaPrincipal);

                // e demais tabelas
                List<String> tabelasTemp = this.getTabelas();
                for (int i = 0; i < tabelasTemp.Count; i++)
                {
                    if (!tabelas.Contains(tabelasTemp[i]))
                    {
                        tabelas.Add(tabelasTemp[i]);
                    }
                }

                tabelas.Remove(null);

                for (int i = (tabelas.Count - 1); i != -1; i--)
                {
                    x = new ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), tabelas[i]);
                    this.CarregarObjeto(tabelas[i], this.Controls);
                    int temp = x.Delete();

                    if (temp < 0)
                    {
                        throw new Exception("Erro ao excluir tabela " + tabelas[i] + ".");
                    }
                }

                AppLib.Context.poolConnection.Get(Conexao).Commit();
            }
            catch (Exception ex)
            {
                AppLib.Context.poolConnection.Get(Conexao).Rollback();
                MessageBox.Show("Erro do método excluir:\r\n\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (this.Visible)
            {
                this.Close();
            }
        }

        public Boolean Salvar()
        {
            try
            {
                AppLib.Context.poolConnection.Get(Conexao).BeginTransaction();

                // adiciona tabela principal
                List<String> tabelas = new List<string>();
                tabelas.Add(TabelaPrincipal);

                // e demais tabelas
                List<String> tabelasTemp = this.getTabelas();
                for (int i = 0; i < tabelasTemp.Count; i++)
                {
                    if (!tabelas.Contains(tabelasTemp[i]))
                    {
                        tabelas.Add(tabelasTemp[i]);
                    }
                }

                tabelas.Remove(null);

                for (int i = 0; i < tabelas.Count; i++)
                {
                    x = new ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), tabelas[i]);
                    this.CarregarObjeto(tabelas[i], this.Controls);
                    x.Select(); // PARA CARREGAR ATRAVÉS DAS CHAVES TODOS OS CAMPOS DO BANCO

                    this.CarregarObjeto(tabelas[i], this.Controls); // PARA CARREGAR OS DEMAIS CAMPOS DO FORM

                    try
                    {
                        this.DuranteSalvar(this, null);
                    }
                    catch { }

                    int temp = x.Save();

                    if (temp >= 1)
                    {
                        // CARREGA INCREMENTO
                        int? incremento = AppLib.Context.poolConnection.Get(Conexao).GetIncrement();

                        if (incremento != null)
                        {
                            this.CarregarForm(tabelas[i], this.Controls, (int)incremento);
                            this.CarregarObjeto(tabelas[i], this.Controls);
                        }
                    }
                    else
                    {
                        throw new Exception("Erro ao salvar tabela " + tabelas[i] + ".");
                    }
                }

                AppLib.Context.poolConnection.Get(Conexao).Commit();

                // CARREGA COLUNAS COMPUTADAS
                /*for (int i = 0; i < tabelas.Count; i++)
                {
                    System.Data.DataTable dt = x.Select();

                    if (dt.Rows.Count > 0)
                    {
                        this.CarregarForm(i, dt, this.Controls);
                    }
                }*/

                Acao = Global.Types.Acao.Editar;
                return true;
            }
            catch (Exception ex)
            {
                AppLib.Context.poolConnection.Get(Conexao).Rollback();
                MessageBox.Show("Erro do método salvar:\r\n\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // return false;
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

            #region COMPONENTES DEV

            if (componente.GetType() == typeof(DevExpress.XtraTab.XtraTabControl))
            {
                DevExpress.XtraTab.XtraTabControl campo = (DevExpress.XtraTab.XtraTabControl)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            if (componente.GetType() == typeof(DevExpress.XtraTab.XtraTabPage))
            {
                DevExpress.XtraTab.XtraTabPage campo = (DevExpress.XtraTab.XtraTabPage)componente;
                CarregarForm(posicaoQuery, dt, componente.Controls);
            }

            #endregion

            #region COMPONENTES LIB

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                try
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
                    else if (componente.GetType() == typeof(CampoDecimal))
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
                    else if (componente.GetType() == typeof(CampoData))
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
                    else if (componente.GetType() == typeof(CampoHora))
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
                    else if (componente.GetType() == typeof(CampoDataHora))
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
                    else if (componente.GetType() == typeof(CampoTexto))
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
                    else if (componente.GetType() == typeof(CampoMemo))
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
                    else if (componente.GetType() == typeof(CampoLista))
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
                    else if (componente.GetType() == typeof(CampoLookup))
                    {
                        CampoLookup campo = (CampoLookup)componente;
                        if (campo.Query == posicaoQuery)
                        {
                            if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                            {
                                campo.carregando = true;

                                if (valor.ToString().Equals(""))
                                {
                                    campo.Set(String.Empty);
                                }
                                else
                                {
                                    campo.textBoxCODIGO.Text = valor.ToString();
                                    campo.textBox1_Leave(this, null);
                                }

                                campo.carregando = false;
                            }
                        }
                    }
                    else if (componente.GetType() == typeof(CampoImagem))
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
                                    campo.SetNomeImagem(valor.ToString());
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
                                    campo.SetImagem(valor.ToString());
                                }
                            }
                        }
                    }
                    else if (componente.GetType() == typeof(CampoArquivo))
                    {
                        CampoArquivo campo = (CampoArquivo)componente;
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
                                    campo.textEditIDENTIFICADOR.Text = valor.ToString();
                                    campo.textEditIDENTIFICADOR_Leave(this, null);
                                }
                            }
                        }
                    }
                    else if (componente.GetType() == typeof(CampoCheck))
                    {
                        CampoCheck campo = (CampoCheck)componente;
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
                                    campo.Set(valor);
                                }
                            }
                        }
                    }
                    else if (componente.GetType() == typeof(CampoCheckList))
                    {
                        CampoCheckList campo = (CampoCheckList)componente;
                        if (campo.Query == posicaoQuery)
                        {
                            if (coluna.ToUpper().Equals(campo.Campo.ToUpper()))
                            {
                                if (valor.ToString().Equals(""))
                                {
                                    campo.Clear();
                                }
                                else
                                {
                                    campo.Set(int.Parse(valor.ToString()));
                                }
                            }
                        }
                    }
                }
                catch { }

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
        /// Chamado somente na ocasião de auto incremento
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

            #region COMPONENTES DEV

            if (componente.GetType() == typeof(DevExpress.XtraTab.XtraTabControl))
            {
                DevExpress.XtraTab.XtraTabControl campo = (DevExpress.XtraTab.XtraTabControl)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            if (componente.GetType() == typeof(DevExpress.XtraTab.XtraTabPage))
            {
                DevExpress.XtraTab.XtraTabPage campo = (DevExpress.XtraTab.XtraTabPage)componente;
                CarregarObjeto(tabela, campo.Controls);
            }

            #endregion

            #region COMPONENTES NOVOS

            if (componente.GetType() == typeof(CampoInteiro))
            {
                CampoInteiro campo = (CampoInteiro)componente;
                if (campo.Campo != null)
                {
                    if (campo.Name == "campoCodColPrd")
                    {

                    }

                    x.Set(campo.Campo, campo.Get());
                }
            }

            if (componente.GetType() == typeof(CampoDecimal))
            {
                CampoDecimal campo = (CampoDecimal)componente;
                if (campo.Campo != null)
                {
                    x.Set(campo.Campo, campo.Get());
                }
            }

            if (componente.GetType() == typeof(CampoData))
            {
                CampoData campo = (CampoData)componente;
                if (campo.Campo != null)
                {
                    x.Set(campo.Campo, campo.Get());
                }
            }

            if (componente.GetType() == typeof(CampoHora))
            {
                CampoHora campo = (CampoHora)componente;
                if (campo.Campo != null)
                {
                    x.Set(campo.Campo, campo.Get());
                }
            }

            if (componente.GetType() == typeof(CampoDataHora))
            {
                CampoDataHora campo = (CampoDataHora)componente;
                if (campo.Campo != null)
                {
                    x.Set(campo.Campo, campo.Get());
                }
            }

            if (componente.GetType() == typeof(CampoTexto))
            {
                CampoTexto campo = (CampoTexto)componente;
                if (campo.Campo != null)
                {
                    x.Set(campo.Campo, campo.Get());
                }
            }

            if (componente.GetType() == typeof(CampoMemo))
            {
                CampoMemo campo = (CampoMemo)componente;
                if (campo.Campo != null)
                {
                    x.Set(campo.Campo, campo.Get());
                }
            }

            if (componente.GetType() == typeof(CampoLista))
            {
                CampoLista campo = (CampoLista)componente;
                if (campo.Campo != null)
                {
                    x.Set(campo.Campo, campo.Get());
                }
            }

            if (componente.GetType() == typeof(CampoLookup))
            {
                CampoLookup campo = (CampoLookup)componente;
                if (campo.Campo != null)
                {
                    x.Set(campo.Campo, campo.Get());
                }
            }

            if (componente.GetType() == typeof(CampoImagem))
            {
                CampoImagem campo = (CampoImagem)componente;
                if (campo.ColunaNomeImagem != null)
                {
                    x.Set(campo.ColunaNomeImagem, campo.GetNomeImagem());
                    x.Set(campo.ColunaImagem, campo.GetImagem());
                }
            }

            if (componente.GetType() == typeof(CampoArquivo))
            {
                CampoArquivo campo = (CampoArquivo)componente;
                if (campo.Campo != null)
                {
                    x.Set(campo.Campo, campo.Get());
                }
            }

            if (componente.GetType() == typeof(CampoCheck))
            {
                CampoCheck campo = (CampoCheck)componente;
                if (campo.Campo != null)
                {
                    x.Set(campo.Campo, campo.Get());
                }
            }

            if (componente.GetType() == typeof(CampoCheckList))
            {
                CampoCheckList campo = (CampoCheckList)componente;
                if (campo.Campo != null)
                {
                    x.Set(campo.Campo, campo.Get());
                }
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

            #region COMPONENTES LIB

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

            if (componente.GetType() == typeof(CampoArquivo))
            {
                CampoArquivo campo = (CampoArquivo)componente;
                if (!TabelaExiste(campo.Tabela))
                {
                    Tabelas.Add(campo.Tabela);
                }
            }

            #endregion
        }

        private Boolean TabelaExiste(String tabela)
        {
            if (tabela != null)
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
            else
            {
                return true;
            }
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

        public delegate void AntesSalvarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de salvar"), DefaultValue(false)]
        public event AntesSalvarHandler AntesSalvar;

        public delegate void DuranteSalvarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado durante o salvar"), DefaultValue(false)]
        public event DuranteSalvarHandler DuranteSalvar;

        public delegate void AposSalvarHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado depois de salvar"), DefaultValue(false)]
        public event AposSalvarHandler AposSalvar;

        public delegate void AntesSalvarComoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de salvar como"), DefaultValue(false)]
        public event AntesSalvarComoHandler AntesSalvarComo;

        public delegate void AposSalvarComoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado depois de salvar como"), DefaultValue(false)]
        public event AposSalvarComoHandler AposSalvarComo;

        public delegate void AntesOKHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes do OK"), DefaultValue(false)]
        public event AntesOKHandler AntesOK;

        public delegate void AposOKHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado depois do OK"), DefaultValue(false)]
        public event AposOKHandler AposOK;

        public delegate void AntesExcluirHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado antes de excluir"), DefaultValue(false)]
        public event AntesExcluirHandler AntesExcluir;

        public delegate void AposExcluirHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado depois de excluir"), DefaultValue(false)]
        public event AposExcluirHandler AposExcluir;

        #endregion

    }
}
