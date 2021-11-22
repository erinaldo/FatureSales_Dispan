using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Expressao
{
    public partial class FormExpressaoDesigner : Form
    {
        #region PROPRIEDADES

        public Global.Types.Confirmacao confirmacao { get; set; }
        public AppLib.Fluxo.Expressao ExpressaoFigura { get; set; }
        public Boolean Atribuicao { get; set; }
        public String Fluxo { get; set; }
        public Arvore Arvore { get; set; }
        public String XML { get; set; }
        public String Expressao { get; set; }
        
        #endregion

        #region CONSTRUTOR E LOAD

        public FormExpressaoDesigner()
        {
            InitializeComponent();

            Atribuicao = true;
            confirmacao = Global.Types.Confirmacao.Cancelar;
        }

        private void FormExpressaoDesigner_Load(object sender, EventArgs e)
        {
            if (Atribuicao)
            {
                labelControlATRIBUICAO.Text = "Tipo de Atribuição";
                comboBoxATRIBUICAO.Items.Clear();
                comboBoxATRIBUICAO.Items.Add("=");
                comboBoxATRIBUICAO.Items.Add("++");
                comboBoxATRIBUICAO.Items.Add("--");
                comboBoxATRIBUICAO.Items.Add("+=");
                comboBoxATRIBUICAO.Items.Add("-=");
                comboBoxATRIBUICAO.Items.Add("*=");
                comboBoxATRIBUICAO.Items.Add("/=");
            }
            else
            {
                labelControlATRIBUICAO.Text = "Tipo de Comparação";
                comboBoxATRIBUICAO.Items.Clear();
                comboBoxATRIBUICAO.Items.Add("==");
                comboBoxATRIBUICAO.Items.Add("!=");
                comboBoxATRIBUICAO.Items.Add("<");
                comboBoxATRIBUICAO.Items.Add("<=");
                comboBoxATRIBUICAO.Items.Add(">");
                comboBoxATRIBUICAO.Items.Add(">=");
            }

            if (ExpressaoFigura.Variavel != null)
            {
                campoLookupVARIAVEL.Set(ExpressaoFigura.Variavel);
            }

            if (ExpressaoFigura.Atribuicao != null)
            {
                comboBoxATRIBUICAO.Text = ExpressaoFigura.Atribuicao;
            }

            if ( ! ExpressaoFigura.ExpressaoXML.Equals(""))
            {
                XML = ExpressaoFigura.ExpressaoXML;
                this.AtualizarForm();
            }
        }
        
        #endregion
        
        #region EVENTOS

        private void toolStripButtonNOVO_Click(object sender, EventArgs e)
        {
            this.Novo();
        }

        private void toolStripButtonEDITAR_Click(object sender, EventArgs e)
        {
            this.Editar();
        }

        private void toolStripButtonEXCLUIR_Click(object sender, EventArgs e)
        {
            this.Excluir();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.Editar();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.Salvar();
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            confirmacao = Global.Types.Confirmacao.Cancelar;
            this.Close();
        }

        #endregion

        #region MÉTODOS

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta = @"SELECT VARIAVEL, TIPOVARIAVEL, TIPODADO FROM ZFLUXOVARIAVEL WHERE FLUXO = ?";
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupVARIAVEL, consulta, new Object[] { Fluxo });
        }

        private void campoLookup1_SetDescricao(object sender, EventArgs e)
        {
            String consulta = @"SELECT TIPODADO FROM ZFLUXOVARIAVEL WHERE FLUXO = ? AND VARIAVEL = ?";
            AppLib.Windows.CampoLookup.Descricao(campoLookupVARIAVEL, consulta, new Object[] { campoLookupVARIAVEL.Get() });
        }

        public void Novo()
        {
            if (AppLib.Windows.FormMessageDefault.ShowQuestion("Quer construir uma nova expressão?") == System.Windows.Forms.DialogResult.Yes)
            {
                treeView1.Nodes.Clear();

                TreeNode item = new TreeNode("[Vazio]");
                item.ImageKey = "Parametro";
                item.SelectedImageKey = item.ImageKey;
                treeView1.Nodes.Add(item);
            }
        }

        public void Editar()
        {
            TreeNode node = treeView1.SelectedNode;

            FormExpressaoSelecao f = new FormExpressaoSelecao();
            f.Fluxo = this.Fluxo;
            f.ShowDialog();

            if (f.confirmacao == Global.Types.Confirmacao.OK)
            {
                if (node.ImageKey.Equals(Global.Types.TipoExpressao.Fluxo.ToString()))
                {
                    node.Nodes.Clear();
                }

                if (f.tipoExpressao == Global.Types.TipoExpressao.ValorFixo)
                {
                    node.Text = f.Result;
                    node.ImageKey = f.tipoExpressao.ToString();
                    node.SelectedImageKey = node.ImageKey;
                }

                if (f.tipoExpressao == Global.Types.TipoExpressao.Variavel)
                {
                    node.Text = f.Result;
                    node.ImageKey = f.tipoExpressao.ToString();
                    node.SelectedImageKey = node.ImageKey;
                }

                if (f.tipoExpressao == Global.Types.TipoExpressao.Fluxo)
                {
                    node.Text = f.Result;
                    node.ImageKey = f.tipoExpressao.ToString();
                    node.SelectedImageKey = node.ImageKey;

                    #region OBTÉM PARÂMETROS DO FLUXO

                    String consulta = @"SELECT * FROM ZFLUXOVARIAVEL WHERE FLUXO = ? AND TIPOVARIAVEL = 'Parâmetro'";
                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(consulta, new Object[] { f.Result });

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TreeNode item = new TreeNode();
                        item.Text = dt.Rows[i]["VARIAVEL"].ToString();
                        item.ImageKey = "Parametro";
                        item.SelectedImageKey = item.ImageKey;

                        node.Nodes.Add(item);
                    }

                    node.Expand();

                    #endregion
                }
            }        
        }

        public void Excluir()
        {
            TreeNode node = treeView1.SelectedNode;

            if (node != null)
            {
                if (node.Parent == null)
                {
                    node.Remove();
                }
                else
                {
                    if (node.Parent.ImageKey.Equals(Global.Types.TipoExpressao.Fluxo.ToString()))
                    {
                        node.Nodes.Clear();

                        node.ImageKey = "Parametro";
                        node.SelectedImageKey = node.ImageKey;
                        node.Text = "[Vazio]";
                    }
                    else
                    {
                        node.Remove();
                    }
                }
            }
        }

        public Boolean ValidarExpressao(TreeNode node)
        {
            if (node.ImageKey.Equals("Parametro"))
            {
                return false;
            }

            for (int i = 0; i < node.Nodes.Count; i++)
            {
                return ValidarExpressao(node.Nodes[i]);
            }

            return true;
        }

        public Boolean Validar()
        {
            if ( campoLookupVARIAVEL.Get() == null)
            {
                if ( ! comboBoxATRIBUICAO.Text.Equals("") )
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Selecione a variável que irá receber a atribuição.");
                    return false;
                }
            }

            if (campoLookupVARIAVEL.Get() != null)
            {
                if (comboBoxATRIBUICAO.Text.Equals(""))
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Selecione o tipo de atribuição.");
                    return false;
                }
            }

            if (treeView1.Nodes.Count > 0)
            {
                Boolean ValidouExpressao = this.ValidarExpressao(treeView1.Nodes[0]);
                if ( ! ValidouExpressao)
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Expressão incompleta.");
                    return false;
                }
            }

            return true;
        }
        
        public void Salvar()
        {
            Arvore = new Arvore();
            XML = String.Empty;
            Expressao = String.Empty;

            if (this.Validar())
            {
                this.AtualizarObjeto();
                this.GerarXml();
                this.GerarExpressao();

                confirmacao = Global.Types.Confirmacao.OK;
                this.Close();
            }
        }

        public void AtualizarObjeto()
        {
            Arvore = new Expressao.Arvore(treeView1);
        }

        public void GerarXml()
        {
            AppLib.Util.ObjetoXML oXML = new Util.ObjetoXML();
            XML = oXML.Escrever(Arvore);

            ExpressaoFigura.ExpressaoXML = XML;
        }

        public void GetExpressao(System.Windows.Forms.TreeNode treeNode1)
        {
            Expressao += treeNode1.Text;

            if (treeNode1.Nodes.Count > 0)
            {
                Expressao += "(";

                for (int i = 0; i < treeNode1.Nodes.Count; i++)
                {
                    this.GetExpressao(treeNode1.Nodes[i]);
                    Expressao += ", ";
                }

                Expressao = Expressao.Substring(0, Expressao.Length - 2);

                Expressao += ")";
            }
        }

        public void GerarExpressao()
        {
            if (Expressao == String.Empty)
            {
                if ( campoLookupVARIAVEL.Get() != null)
                {
                    ExpressaoFigura.Variavel = campoLookupVARIAVEL.Get();
                    ExpressaoFigura.Atribuicao = comboBoxATRIBUICAO.Text;

                    // Expressao += campoLookupVARIAVEL.Get() + " ";
                    // Expressao += comboBoxATRIBUICAO.Text + " ";
                }
            }

            if (treeView1.Nodes.Count > 0)
            {
                this.GetExpressao(treeView1.Nodes[0]);
            }

            ExpressaoFigura.Texto = Expressao;
        }

        public void SetExpressao(TreeNode no, Item item)
        {
            TreeNode node = new TreeNode();
            node.Text = item.Texto;
            node.ImageKey = item.Tipo;
            node.SelectedImageKey = node.ImageKey;

            if (no == null)
            {
                treeView1.Nodes.Add(node);
            }
            else
            {
                no.Nodes.Add(node);
            }            

            for (int i = 0; i < item.itens.Count; i++)
            {
                SetExpressao(node, item.itens[i]);
            }
        }

        public void AtualizarForm()
        {
            AppLib.Util.ObjetoXML oXML = new Util.ObjetoXML();
            XML = XML.Replace("<xml version=\"1.0\" encoding=\"utf-16\">", "");
            Arvore = new AppLib.Expressao.Arvore();
            Arvore = (Arvore)oXML.Ler(XML, new Arvore());
            this.SetExpressao(null, Arvore.itens[0]);
            treeView1.ExpandAll();
        }
        
        #endregion

        

        
    }
}
