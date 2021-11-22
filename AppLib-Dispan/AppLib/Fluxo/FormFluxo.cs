using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormFluxo : Form
    {

        #region VARIAVEIS E OBJETOS

        public AppLib.Fluxo.Principal f;

        int xDown { get; set; }
        int yDown { get; set; }
        int xUp { get; set; }
        int yUp { get; set; }

        String Acao { get; set; }

        #endregion

        #region CONSTRUTOR E LOAD

        public FormFluxo()
        {
            InitializeComponent();

            // SESSION BÁSICO
            // AppLib.Util.Contexto.TipoConexao = _TipoConexao;
            // AppLib.Util.Contexto.StringConexao = _StringConexao;
            // AppLib.Util.Contexto.Usuario = AppLib.Context.Usuario;

            // CRIA OBJETO FLUXO
            f = new AppLib.Fluxo.Principal(pictureBox1);
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            // JANELA
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.WindowState = FormWindowState.Maximized;
            // pictureBox1.Dock = DockStyle.None;

            // RESOLUÇÃO
            pictureBox1.Width = 1500;
            pictureBox1.Height = 1500;

            // RECALCULAR
            this.f.Desenhar();
        }

        #endregion

        #region EVENTOS DO PAINEL

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // POSIÇÃO INICIAL
            xDown = e.X;
            yDown = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // POSIÇÃO FINAL
            xUp = e.X;
            yUp = e.Y;

            // TRATA INTERAÇÃO
            if ((xDown == xUp) && (yDown == yUp))
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (!f.Selecionar(xDown, yDown))
                    {
                        f.Inserir(xDown, yDown);

                        AppLib.Fluxo.Fluxo fluxoTemp = new AppLib.Fluxo.Fluxo();
                        fluxoTemp = f.fluxo.Copiar();
                        f.desrefazer.NovaAcao("Figura " + f.caixa.Texto, fluxoTemp);
                    }
                }

                if (e.Button == MouseButtons.Right)
                {
                    if (f.PropriedadesFigura(xDown, yDown))
                    {
                        AppLib.Fluxo.Fluxo fluxoTemp = new AppLib.Fluxo.Fluxo();
                        fluxoTemp = f.fluxo.Copiar();
                        f.desrefazer.NovaAcao("Propriedades da figura", fluxoTemp);
                    }
                    else
                    {
                        f.PropriedadesFluxo();

                        AppLib.Fluxo.Fluxo fluxoTemp = new AppLib.Fluxo.Fluxo();
                        fluxoTemp = f.fluxo.Copiar();
                        f.desrefazer.NovaAcao("Propriedades do fluxo", fluxoTemp);
                    }
                }
            }
            else
            {
                if (f.Mover(xDown, yDown, xUp, yUp))
                {
                    AppLib.Fluxo.Fluxo fluxoTemp = new AppLib.Fluxo.Fluxo();
                    fluxoTemp = f.fluxo.Copiar();
                    f.desrefazer.NovaAcao("Mover", fluxoTemp);
                }
                else
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        f.Clique = AppLib.Fluxo.Principal.OpcoesClique.Esquerdo.ToString();
                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        f.Clique = AppLib.Fluxo.Principal.OpcoesClique.Direito.ToString();
                    }

                    f.Ligar(xDown, yDown, xUp, yUp);

                    AppLib.Fluxo.Fluxo fluxoTemp = new AppLib.Fluxo.Fluxo();
                    fluxoTemp = f.fluxo.Copiar();
                    f.desrefazer.NovaAcao("Ligar", fluxoTemp);
                }
            }

            f.Desenhar();
            this.AtualizarDesfazerRefazer();
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            f.Desenhar();
        }

        public void AtualizarDesfazerRefazer()
        {
            DesfazerToolStripSplitButton.DropDownItems.Clear();

            for (int i = 0; i < f.desrefazer.AcaoDesfazer.Count; i++)
            {
                DesfazerToolStripSplitButton.DropDownItems.Add(f.desrefazer.AcaoDesfazer[i]);
            }

            RefazerToolStripSplitButton.DropDownItems.Clear();

            for (int i = 0; i < f.desrefazer.AcaoRefazer.Count; i++)
            {
                RefazerToolStripSplitButton.DropDownItems.Add(f.desrefazer.AcaoRefazer[i]);
            }
        }

        #endregion

        #region BARRA DE MENU

        private void NovoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Novo(true);
        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Abrir();
        }

        private void SalvarComotoolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.SalvarComo();
        }

        private void ImagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.ExportarImagem();
        }

        private void ExportarFluxoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.ExportarFluxo();
        }

        private void ImportarFluxoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.ImportarFluxo();
        }

        private void SairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Sair();
        }

        private void DesfazerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Desfazer();
            this.AtualizarDesfazerRefazer();
        }

        private void RefazerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Refazer();
            this.AtualizarDesfazerRefazer();
        }

        private void RecortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Recortar();
        }

        private void CopiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Copiar();
        }

        private void ColarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Colar();
        }

        private void ExcluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Excluir();
        }

        private void SelecionarStripMenuItem2_Click(object sender, EventArgs e)
        {
            f.SelecionarTudo();
        }

        private void InverterStripMenuItem3_Click(object sender, EventArgs e)
        {
            f.InverterSelecao();
        }

        private void LimparStripMenuItem4_Click(object sender, EventArgs e)
        {
            f.LimparSelecao();
        }

        private void InicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Inicio();
        }

        private void FimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Fim();
        }

        private void ProcessoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Processo();
        }

        private void CondiçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Condicao();
        }

        private void transaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Transacao();
        }

        private void RepetiçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Repeticao();
        }

        private void SubprocessoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.Subprocesso();
        }

        private void HorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.AlinharHorizontal();
        }

        private void VerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.AlinharVertical();
        }

        private void PropriedadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.PropriedadesFluxo();
        }

        private void OpcoesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.OpcoesGerais();
        }

        private void GerarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f.ExportarCodigoFonte();
        }

        #endregion

        #region BARRA DE ICONES PEQUENOS

        private void NovoToolStripButton_Click(object sender, EventArgs e)
        {
            f.Novo(true);
        }

        private void AbrirToolStripButton_Click(object sender, EventArgs e)
        {
            f.Abrir();
        }

        private void SalvarToolStripButton_Click(object sender, EventArgs e)
        {
            f.SalvarComo();
        }

        private void ImagemToolStripButton_Click(object sender, EventArgs e)
        {
            f.ExportarImagem();
        }

        private void ExportarToolStripButton1_Click(object sender, EventArgs e)
        {
            f.ExportarFluxo();
        }

        private void ImportarToolStripButton_Click(object sender, EventArgs e)
        {
            f.ImportarFluxo();
        }

        private void RecortarToolStripButton_Click(object sender, EventArgs e)
        {
            f.Recortar();
        }

        private void CopiarToolStripButton_Click(object sender, EventArgs e)
        {
            f.Copiar();
        }

        private void ColarToolStripButton_Click(object sender, EventArgs e)
        {
            f.Colar();
        }

        private void ExcluirToolStripButton_Click(object sender, EventArgs e)
        {
            f.Excluir();
        }

        private void DesfazerToolStripSplitButton_ButtonClick(object sender, EventArgs e)
        {
            f.Desfazer();
            this.AtualizarDesfazerRefazer();
        }

        private void RefazerToolStripSplitButton_ButtonClick(object sender, EventArgs e)
        {
            f.Refazer();
            this.AtualizarDesfazerRefazer();
        }

        private void HorizontalToolStripButton_Click(object sender, EventArgs e)
        {
            f.AlinharHorizontal();
        }

        private void VerticalToolStripButton_Click(object sender, EventArgs e)
        {
            f.AlinharVertical();
        }

        private void PropriedadesToolStripButton_Click(object sender, EventArgs e)
        {
            f.PropriedadesFluxo();
        }

        private void GerarToolStripButton_Click(object sender, EventArgs e)
        {
            f.ExportarCodigoFonte();
        }

        #endregion

        #region BARRA DE ICONES GRANDES

        private void toolStripButtonINICIO_Click(object sender, EventArgs e)
        {
            f.Inicio();
        }

        private void toolStripButtonFIM_Click(object sender, EventArgs e)
        {
            f.Fim();
        }

        private void toolStripButtonPROCESSO_Click(object sender, EventArgs e)
        {
            f.Processo();
        }

        private void toolStripButtonCONDICAO_Click(object sender, EventArgs e)
        {
            f.Condicao();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            f.Transacao();
        }

        private void toolStripButtonREPETICAO_Click(object sender, EventArgs e)
        {
            f.Repeticao();
        }

        private void toolStripButtonSUBPROCESSO_Click(object sender, EventArgs e)
        {
            f.Subprocesso();
        }

        #endregion

    }
}
