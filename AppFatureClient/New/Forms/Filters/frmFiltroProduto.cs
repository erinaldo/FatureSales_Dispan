using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFatureClient.New.Forms.Filters
{
    public partial class frmFiltroProduto : Form
    {
        public string condicao = "";
        public frmFiltroProduto()
        {
            InitializeComponent();
        }

        private void frmFiltroProduto_Load(object sender, EventArgs e)
        {
            CarregaLookUpTipoProduto();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            getCondicao();
            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            condicao = null;
            this.Dispose();
        }

        #region Eventos

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbTodos.Text);
            }
        }

        private void rbIdentificador_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIdentificador.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbIdentificador.Text);
            }
        }

        private void rbCodigoAuxiliar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCodigoAuxiliar.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbCodigoAuxiliar.Text);
            }
        }

        private void rbCodigoProduto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCodigoProduto.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbCodigoProduto.Text);
            }
        }

        private void rbNomeFantasia_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNomeFantasia.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbNomeFantasia.Text);
            }
        }

        private void rbTipoProduto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTipoProduto.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbTipoProduto.Text);
            }
        }

        private void rbInativo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStatus.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbStatus.Text);
            }
        }

        private void tbValorFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                getCondicao();
                this.Dispose();
            }
        }

        #endregion

        #region Métodos

        private void CarregaLookUpTipoProduto()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODTB2FAT AS 'Código', DESCRICAO AS 'Descrição' FROM TTB2 WHERE CODCOLIGADA = 1 ORDER BY CODTB2FAT ASC", new object[] { });

            lpTipoProduto.Properties.DataSource = dt;
            lpTipoProduto.Properties.ValueMember = dt.Columns["Código"].ToString();
            lpTipoProduto.Properties.DisplayMember = dt.Columns["Descrição"].ToString();
            lpTipoProduto.Properties.NullText = "Selecione...";

            lpTipoProduto.CalcBestSize();
        }

        private void ConfigurarVisibilidadeFiltros(string filtroSelecionado)
        {
            switch (filtroSelecionado)
            {
                case "Todos":

                    lbValorFiltro.Visible = false;
                    tbValorFiltro.Visible = false;

                    lpTipoProduto.Visible = false;

                    chkAtivo.Visible = false;

                    break;

                case "Identificador":

                    ConfiguraLocalizacaoValorFiltro(filtroSelecionado);

                    lbValorFiltro.Visible = true;
                    lbValorFiltro.Text = "Identificador";
                    tbValorFiltro.Visible = true;

                    lpTipoProduto.Visible = false;

                    chkAtivo.Visible = false;

                    break;

                case "Código Auxiliar":

                    ConfiguraLocalizacaoValorFiltro(filtroSelecionado);

                    lbValorFiltro.Visible = true;
                    lbValorFiltro.Text = "Código Auxiliar";
                    tbValorFiltro.Visible = true;

                    lpTipoProduto.Visible = false;

                    chkAtivo.Visible = false;

                    break;

                case "Código do Produto":

                    ConfiguraLocalizacaoValorFiltro(filtroSelecionado);

                    lbValorFiltro.Visible = true;
                    lbValorFiltro.Text = "Código do Produto";
                    tbValorFiltro.Visible = true;

                    lpTipoProduto.Visible = false;

                    chkAtivo.Visible = false;

                    break;

                case "Nome Fantasia":

                    ConfiguraLocalizacaoValorFiltro(filtroSelecionado);

                    lbValorFiltro.Visible = true;
                    lbValorFiltro.Text = "Nome Fantasia";
                    tbValorFiltro.Visible = true;

                    lpTipoProduto.Visible = false;

                    chkAtivo.Visible = false;

                    break;

                case "Tipo Produto":

                    ConfiguraLocalizacaoValorFiltro(filtroSelecionado);

                    lbValorFiltro.Visible = true;
                    lbValorFiltro.Text = "Tipo Produto";
                    lpTipoProduto.Visible = true;

                    tbValorFiltro.Visible = false;

                    chkAtivo.Visible = false;

                    break;

                case "Status":

                    ConfiguraLocalizacaoValorFiltro(filtroSelecionado);

                    chkAtivo.Visible = true;

                    lbValorFiltro.Visible = false;
                    lbValorFiltro.Text = "Tipo Produto";
                    lpTipoProduto.Visible = false;

                    tbValorFiltro.Visible = false;

                    break;              
                default:
                    break;
            }
        }

        private void ConfiguraLocalizacaoValorFiltro(string filtroSelecionado)
        {
            switch (filtroSelecionado)
            {
                case "Identificador":

                    tbValorFiltro.Location = new Point(40, 54);

                    break;

                case "Código Auxiliar":

                    tbValorFiltro.Location = new Point(40, 54);

                    break;

                case "Código do Produto":

                    tbValorFiltro.Location = new Point(40, 54);

                    break;

                case "Nome Fantasia":

                    tbValorFiltro.Location = new Point(40, 54);

                    break;

                case "Tipo Produto":

                    lpTipoProduto.Location = new Point(40, 54);

                    break;

                case "Status":

                    chkAtivo.Location = new Point(113, 32);

                    break;

                default:
                    break;
            }
        }

        private void getCondicao()
        {
            try
            {
                if (rbTodos.Checked)
                {
                    condicao = " WHERE CODCOLIGADA = " + AppLib.Context.Empresa + " AND CODCOLPRD = "+ AppLib.Context.Empresa +"";
                }

                else if (rbIdentificador.Checked)
                {
                    condicao = " WHERE IDPRD = " + Convert.ToInt32(tbValorFiltro.Text) + "";
                }

                else if (rbCodigoAuxiliar.Checked)
                {
                    condicao = " WHERE CODIGOAUXILIAR LIKE '"+ tbValorFiltro.Text +"%' AND INATIVO = 0";
                }

                else if (rbCodigoProduto.Checked)
                {
                    condicao = " WHERE CODIGOPRD LIKE '" + tbValorFiltro.Text + "%' AND INATIVO = 0";
                }

                else if (rbNomeFantasia.Checked)
                {
                    condicao = " WHERE NOMEFANTASIA LIKE '%"+ tbValorFiltro.Text + "%' AND INATIVO = 0";
                }

                else if (rbTipoProduto.Checked)
                {
                    condicao = " WHERE TIPOPROD = '" + lpTipoProduto.Text + "' AND INATIVO = 0";
                }

                else if (rbStatus.Checked)
                {
                    condicao = " WHERE INATIVO = " + ((chkAtivo.Checked)? 0 : 1) + "";
                }               
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
