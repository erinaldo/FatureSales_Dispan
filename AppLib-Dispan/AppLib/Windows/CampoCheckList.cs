using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Windows
{
    public partial class CampoCheckList : UserControl
    {
        #region PROPERTIES

        [Category("_APP"), Description("Nome da conexão")]
        public String Conexao { get; set; }

        [Category("_APP"), Description("Nome da tabela")]
        public String Tabela { get; set; }

        [Category("_APP"), Description("Nome do campo")]
        public String Campo { get; set; }

        [Category("_APP"), Description("Posição da query")]
        public int Query { get; set; }

        [Category("_APP"), Description("Coluna de código")]
        public String ColunaCodigo { get; set; }

        [Category("_APP"), Description("Coluna de identificador")]
        public String ColunaIdentificador { get; set; }

        [Category("_APP"), Description("Coluna de descrição")]
        public String ColunaDescricao { get; set; }

        [Category("_APP"), Description("Tabela de pesquisa")]
        public String ColunaTabela { get; set; }

        [Category("_APP"), Description("Coluna do order by")]
        public String ColunaOrdenacao { get; set; }

        [Category("_APP"), Description("Consulta da grid")]
        public String[] Consulta { get; set; }

        [Category("_APP"), Description("Parâmetros da consulta")]
        public Object[] Parametros = new Object[] { };

        [Category("_APP"), Description("Rótulo")]
        public String Rotulo { get; set; }

        [Category("_APP"), Description("Código do Checklist")]
        public String CodigoChecklist { get; set; }

        [Category("_APP"), Description("Edita o campo")]
        public Boolean Edita { get; set; }

        [Category("_APP"), Description("Dados do checklist")]
        public SequenciaListaItens dados { get; set; }
        
        #endregion

        #region CONSTRUTOR E LOAD

        public CampoCheckList()
        {
            InitializeComponent();

            dados = new SequenciaListaItens();
            Conexao = "Start";
            ColunaCodigo = "CODIGO";
            ColunaDescricao = "DESCRICAO";
            Query = 0;
            Edita = true;
        }

        private void CampoCheckList_Load(object sender, EventArgs e)
        {
            groupControl1.Text = Rotulo;
            checkedListBoxControl1.Enabled = Edita;

            if ( ! DesignMode)
            {
                this.Atualizar();
            }
        }
        
        #endregion

        #region EVENTOS

        private void CampoCheckList_Resize(object sender, EventArgs e)
        {
            groupControl1.Height = this.Height;
            groupControl1.Width = this.Width;
        }
        
        #endregion

        #region MÉTODOS

        public void Atualizar()
        {
            if (checkedListBoxControl1.Items.Count == 0)
            {
                try
                {
                    this.SetParametros(this, null);
                }
                catch (Exception) { }

                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(this.GetConsulta(), Parametros);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String IDENTIFICADOR = dt.Rows[i][ColunaCodigo].ToString();

                    if (ColunaIdentificador != null)
                    {
                        if (ColunaIdentificador.Length > 0)
                        {
                            IDENTIFICADOR = dt.Rows[i][ColunaIdentificador].ToString();
                        }
                    }

                    String CODIGO = dt.Rows[i][ColunaCodigo].ToString();
                    String DESCRICAO = dt.Rows[i][ColunaDescricao].ToString();

                    checkedListBoxControl1.Items.Add(IDENTIFICADOR, CODIGO + " - " + DESCRICAO);
                }
            }
        }

        public String GetConsulta()
        {
            String result = "";

            if (Consulta != null)
            {
                for (int i = 0; i < Consulta.Length; i++)
                {
                    result += Consulta[i] + "\r\n";
                }
            }
            else
            {
                result = "SELECT * FROM " + ColunaTabela;

                if ( ! String.IsNullOrEmpty(ColunaOrdenacao))
                {
                    result += " ORDER BY " + ColunaOrdenacao;
                }
                else
                {
                    result += " ORDER BY " + ColunaDescricao;
                }
            }

            return result;
        }

        public void Clear()
        {
            checkedListBoxControl1.UnCheckAll();
            dados = new SequenciaListaItens();
        }

        public int? Get()
        {
            dados.lista = new List<String>();

            for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
			{
                if ( checkedListBoxControl1.Items[i].CheckState == CheckState.Checked )
                {
                    dados.lista.Add(checkedListBoxControl1.Items[i].Value.ToString());
                }
            }

            #region SALVA NO BANCO DE DADOS

            // se já tem sequencial
            if (dados.Sequencia != null)
            {
                // limpa a tabela
                String comando1 = "DELETE ZCHECKLIST WHERE CODCHECKLIST = ? AND SEQUENCIA = ?";
                int temp1 = AppLib.Context.poolConnection.Get(Conexao).ExecTransaction(comando1, new Object[] { CodigoChecklist, dados.Sequencia });
            }

            // se não tem item na lista
            if (dados.lista.Count == 0)
            {
                // salvar null
                return null;
            }
            else
            {
                if (dados.Sequencia == null)
                {
                    // busca o próximo sequencial
                    String consulta2 = "SELECT ISNULL(MAX(SEQUENCIA),0)+1 FROM ZCHECKLIST WHERE CODCHECKLIST = ?";
                    dados.Sequencia = int.Parse(AppLib.Context.poolConnection.Get(Conexao).ExecGetField(1, consulta2, new Object[] { CodigoChecklist }).ToString());
                }

                for (int i = 0; i < dados.lista.Count; i++)
                {
                    String comando3 = "INSERT INTO ZCHECKLIST ( CODCHECKLIST, SEQUENCIA, IDITEM ) VALUES ( ?, ?, ? )";
                    int temp3 = AppLib.Context.poolConnection.Get(Conexao).ExecTransaction(comando3, new Object[] { CodigoChecklist, dados.Sequencia, dados.lista[i] });
                }

                return dados.Sequencia;
            }

            #endregion
        }

        public void Set(SequenciaListaItens valor)
        {
            dados = valor;

            if (dados.Sequencia != null)
            {
                checkedListBoxControl1.UnCheckAll();
                this.Atualizar();

                for (int i = 0; i < dados.lista.Count; i++)
                {
                    for (int x = 0; x < checkedListBoxControl1.Items.Count; x++)
                    {
                        if (dados.lista[i].Equals(checkedListBoxControl1.Items[x].Value.ToString()))
                        {
                            checkedListBoxControl1.Items[x].CheckState = CheckState.Checked;
                        }
                    }
                }
            }
            else
            {
                checkedListBoxControl1.UnCheckAll();
            }
        }

        public void Set(int? valor)
        {
            if (valor != null)
            {
                String consulta1 = "SELECT * FROM ZCHECKLIST WHERE CODCHECKLIST = ? AND SEQUENCIA = ?";
                System.Data.DataTable dt = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(consulta1, new Object[] { CodigoChecklist, valor });

                SequenciaListaItens temp = new SequenciaListaItens();
                temp.lista = new List<String>();
                temp.Sequencia = valor;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String IDITEM = dt.Rows[i]["IDITEM"].ToString();
                    temp.lista.Add(IDITEM);
                }

                this.Set(temp);
            }
        }

        #endregion

        #region MÉTODOS CUSTOMIZADOS

        public delegate void SetNewParametrosHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método setar parametros da consulta"), DefaultValue(false)]
        public event SetNewParametrosHandler SetParametros;

        #endregion

    }
}
