using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class CampoLookup : UserControl
    {
        #region ATRIBUTOS
        
        [Category("_APP"), Description("Nome da conexão")]
        public String Conexao { get; set; }

        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo")]
        public String Campo { get; set; }

        [Category("_APP"), Description("Coluna de código")]
        public String ColunaCodigo { get; set; }

        [Category("_APP"), Description("Coluna de identificador")]
        public String ColunaIdentificador { get; set; }

        [Category("_APP"), Description("Coluna de descrição")]
        public String ColunaDescricao { get; set; }

        [Category("_APP"), Description("Tabela de pesquisa")]
        public String ColunaTabela { get; set; }

        [Category("_APP"), Description("Nome da grid")]
        public String NomeGrid { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Valor padrão")]
        public String Default { get; set; }

        [Category("_APP"), Description("Número Máximo de Caracteres Permitido")]
        public int? MaximoCaracteres { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }

        [Category("_APP"), Description("Datarow selecionado")]
        public System.Data.DataRow dr;

        [Category("_APP"), Description("Situação do campo")]
        public Boolean carregando = false;

        [Category("_APP"), Description("Edita o lookup")]
        public Boolean EditaLookup { get; set; }

        #endregion

        #region CONSTRUTOR E LOAD

        public CampoLookup()
        {
            InitializeComponent();

            Conexao = "Start";
            ColunaCodigo = "CODIGO";
            ColunaDescricao = "DESCRICAO";

            Query = 0;

            if (MaximoCaracteres != null)
            {
                textBoxCODIGO.Properties.MaxLength = (int)MaximoCaracteres;
            }

            Edita = true;
            EditaLookup = false;
        }

        private void Lookup_Load(object sender, EventArgs e)
        {
            textBoxCODIGO.Properties.ReadOnly = !Edita;
            buttonSELECIONAR.Enabled = Edita;

            if (EditaLookup)
            {
                textBoxDESCRICAO.ForeColor = Color.Blue;
                textBoxDESCRICAO.BackColor = Color.WhiteSmoke;
                textBoxDESCRICAO.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                textBoxDESCRICAO.ReadOnly = true;
            }
        }

        private void Lookup_Resize(object sender, EventArgs e)
        {
            textBoxDESCRICAO.Width = (this.Width - 140) - 3;
        }

        private void textBoxDESCRICAO_TextChanged(object sender, EventArgs e)
        {
            if (EditaLookup)
            {
                textBoxDESCRICAO.Cursor = Cursors.Hand;

                System.Drawing.Font font = new System.Drawing.Font(textBoxDESCRICAO.Font, FontStyle.Underline);
                textBoxDESCRICAO.Font = font;

                textBoxDESCRICAO.ForeColor = Color.Blue;
            }
        }

        #endregion

        #region MÉTODOS EXTERNOS

        public String Get()
        {
            if (this.ColunaIdentificador != null)
            {
                if (this.ColunaIdentificador.Length > 0)
                {
                    if (this.dr != null)
                    {
                        return this.dr[this.ColunaIdentificador].ToString();
                    }
                }
            }

            if (this.textBoxCODIGO.Text.Equals(""))
            {
                return null;
            }
            else
            {
                return this.textBoxCODIGO.Text;
            }
        }

        public void Set(String valor)
        {
            if ( ! this.DesignMode)
            {
                if (valor != null)
                {
                    if (!valor.Equals(""))
                    {
                        try
                        {
                            try
                            {
                                this.SetDescricao(this, null);
                            }
                            catch
                            {
                                this.textBoxCODIGO.Text = valor;
                                this.setDescricao();
                            }
                        }
                        catch
                        {
                            if (this.SetFormConsulta != null)
                            {
                                if (this.SetFormConsulta(this, null))
                                {
                                    // incluir tratamento caso necessário
                                }
                            }
                        }
                        /*
                        try
                        {
                            this.AposSelecao(this, null);
                        }
                        catch { }
                        */
                    }
                }

                try
                {
                    this.AposSelecao(this, null);
                }
                catch { }
            }
        }

        public void Clear()
        {
            textBoxCODIGO.Text = "";
            textBoxDESCRICAO.Text = "";
        }

        public void Select()
        {
            String codigo = textBoxCODIGO.Text;
            String descricao = textBoxDESCRICAO.Text;

            textBoxCODIGO.Text = "";
            textBoxDESCRICAO.Text = "";

            try
            {
                if (this.SetFormConsulta(this, null))
                {
                    try
                    {
                        this.AposSelecao(this, null);
                    }
                    catch { }
                }
                else
                {
                    textBoxCODIGO.Text = codigo;
                    textBoxDESCRICAO.Text = descricao;
                }
            }
            catch (Exception)
            {
                if (this.SetFormConsultaPadrao(this, null))
                {
                    try
                    {
                        this.AposSelecao(this, null);
                    }
                    catch { }
                }
                else
                {
                    textBoxCODIGO.Text = codigo;
                    textBoxDESCRICAO.Text = descricao;
                }
            }

            
        }

        private bool SetFormConsultaPadrao(object sender, EventArgs e)
        {
            try
            {
                String consulta1 = @"SELECT * FROM " + ColunaTabela;
                AppLib.Windows.FormVisao f = new AppLib.Windows.FormVisao();
                f.grid1.NomeGrid = this.NomeGrid;
                return f.MostrarLookup(this, consulta1, new Object[] { });
            }
            catch
            {
                return false;
            }
        }

        public Boolean modeloNovo()
        {
            Boolean result = false;

            if (this.ColunaIdentificador != null)
            {
                if (this.ColunaIdentificador.Length > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public void setDescricao()
        {
            if (carregando)
            {
                String Consulta = "SELECT * FROM " + this.ColunaTabela + " WHERE " + this.ColunaCodigo + " = ?";

                if (this.ColunaIdentificador != null)
                {
                    if (this.ColunaIdentificador.Length > 0)
                    {
                        Consulta = "SELECT * FROM " + this.ColunaTabela + " WHERE " + this.ColunaIdentificador + " = ?";
                    }
                }

                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Consulta, new Object[] { this.textBoxCODIGO.Text });

                if (dt.Rows.Count == 1)
                {
                    this.dr = dt.Rows[0];

                    if (this.ColunaIdentificador != null)
                    {
                        if (this.ColunaIdentificador.Length > 0)
                        {
                            this.textBoxCODIGO.Text = this.dr[this.ColunaCodigo].ToString();
                        }
                    }

                    this.textBoxDESCRICAO.Text = this.dr[this.ColunaDescricao].ToString();
                }
            }
            else
            {

                if (this.ColunaIdentificador == null)
                {
                    String Consulta = "SELECT DISTINCT " + this.ColunaDescricao + " FROM " + this.ColunaTabela + " WHERE " + this.ColunaCodigo + " = ?";

                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Consulta, new Object[] { this.textBoxCODIGO.Text });

                    if (dt.Rows.Count == 1)
                    {
                        this.dr = dt.Rows[0];
                        this.textBoxDESCRICAO.Text = this.dr[this.ColunaDescricao].ToString();
                    }
                }
                else
                {
                    String Consulta = "SELECT DISTINCT " + this.ColunaDescricao + ", " + this.ColunaIdentificador + " FROM " + this.ColunaTabela + " WHERE " + this.ColunaCodigo + " = ?";

                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get(this.Conexao).ExecQuery(Consulta, new Object[] { this.textBoxCODIGO.Text });

                    if (dt.Rows.Count == 1)
                    {
                        this.dr = dt.Rows[0];
                        this.textBoxDESCRICAO.Text = this.dr[this.ColunaDescricao].ToString();
                    }
                }
            }
        }

        public static void Descricao(CampoLookup campoLookup1, String command, params Object[] parameters)
        {
            //ANTES
            //String result = Context.poolConnection.Get(campoLookup1.Conexao).ExecGetField(null, command, parameters).ToString();
            //campoLookup1.textBoxDESCRICAO.Text = result;

            //DEPOIS
            System.Data.DataTable dt = Context.poolConnection.Get(campoLookup1.Conexao).ExecQuery(command, parameters);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    campoLookup1.dr = dt.Rows[0];
                    campoLookup1.textBoxDESCRICAO.Text = campoLookup1.dr[campoLookup1.ColunaDescricao].ToString();
                }
            }
        }
        
        #endregion

        #region MÉTODOS INTERNOS

        public void textBox1_Leave(object sender, EventArgs e)
        {
            textBoxDESCRICAO.Text = "";
            this.Set(textBoxCODIGO.Text);
        }

        private void buttonSELECIONAR_Click(object sender, EventArgs e)
        {
            try
            {
                this.Select();
            }catch(Exception ex)
            {

            }
            
        }

        private void textBoxDESCRICAO_Click(object sender, EventArgs e)
        {
            try
            {
                if (EditaLookup)
                {
                    this.SetFormCadastro(this, null);
                    this.setDescricao();
                }
            }
            catch { }
        }
        
        #endregion

        #region MÉTODOS CUSTOMIZADOS

        public delegate Boolean SetFormConsultaHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método setar o formulário de consulta"), DefaultValue(false)]
        public event SetFormConsultaHandler SetFormConsulta;

        public delegate void SetDescricaoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método de busca rápida de descrição"), DefaultValue(false)]
        public event SetDescricaoHandler SetDescricao;

        public delegate void AposSelecaoHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método executado após seleção do valor do lookup"), DefaultValue(false)]
        public event AposSelecaoHandler AposSelecao;

        public delegate void SetFormCadastroHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método setar o formulário de cadastro"), DefaultValue(false)]
        public event SetFormCadastroHandler SetFormCadastro;

        #endregion

    }
}
