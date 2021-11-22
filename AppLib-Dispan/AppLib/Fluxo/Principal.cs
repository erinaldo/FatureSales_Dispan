using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Fluxo
{
    public class Principal
    {
        public AppLib.Util.DesfazerRefazer desrefazer = new AppLib.Util.DesfazerRefazer();

        #region VARIAVEIS

        public Fluxo fluxo = new Fluxo();
        public CaixaTexto caixa = new CaixaTexto();
        public Ligacao linha = new Ligacao();

        public System.Windows.Forms.PictureBox pictureBox;
        public System.Drawing.Bitmap bmp;
        public System.Drawing.Graphics graph;

        public Boolean Ponteiro { get; set; }

        public int refSelecaoX = 0;
        public int refSelecaoY = 0;

        public String Clique { get; set; }

        public enum OpcoesClique
        {
            Esquerdo, Direito
        }

        public Principal(System.Windows.Forms.PictureBox _pictureBox)
        {
            pictureBox = _pictureBox;
            
            bmp = new System.Drawing.Bitmap(pictureBox.Width, pictureBox.Height);
            graph = System.Drawing.Graphics.FromImage(bmp);

            Ponteiro = true;

            Novo(false);
            fluxo.Propriedades.Contador = 0;
        }

        #endregion

        #region ARQUIVO

        public void Novo(Boolean Salvar)
        {
            if (Salvar)
            {
                SalvarComo();
            }

            fluxo = new Fluxo();
            caixa = new CaixaTexto();
            linha = new Ligacao();

            bmp = new System.Drawing.Bitmap(pictureBox.Width, pictureBox.Height);
            graph = System.Drawing.Graphics.FromImage(bmp);

            Ponteiro = true;

            Desenhar();
        }

        public void Abrir()
        {
            Novo(true);

            FormAbrir fa = new FormAbrir();
            fa.ShowDialog();
            this.fluxo = fa.principal.fluxo;

            Desenhar();
        }

        public void Abrir(String NomeFluxo)
        {
            FormAbrir fa = new FormAbrir(NomeFluxo);
            this.fluxo = fa.principal.fluxo;
        }

        public void SalvarComo()
        {
            // PERGUNTA ANTES DE SALVAR
            if (System.Windows.Forms.MessageBox.Show("Salvar o fluxo atual?", "Pergunta", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes )
            {
                System.Data.DataTable dt1 = AppLib.Context.poolConnection.Get().ExecQuery("SELECT COUNT(*) FROM ZFLUXO WHERE NOME = '" + fluxo.Propriedades.Nome + "'", new object[] { });

                if (int.Parse(dt1.Rows[0][0].ToString()) > 0)
                {
                    // DELETE ANTES DO INSERT
                    AppLib.Context.poolConnection.Get().ExecTransaction("DELETE ZFLUXOSUBFLUXO WHERE FLUXO = ?", new object[] { fluxo.Propriedades.Nome });
                    AppLib.Context.poolConnection.Get().ExecTransaction("DELETE ZFLUXOFIGURA WHERE FLUXO = ?", new object[] { fluxo.Propriedades.Nome });
                    AppLib.Context.poolConnection.Get().ExecTransaction("DELETE ZFLUXOVARIAVEL WHERE FLUXO = ?", new object[] { fluxo.Propriedades.Nome });
                    AppLib.Context.poolConnection.Get().ExecTransaction("DELETE ZFLUXO WHERE NOME = ?", new object[] { fluxo.Propriedades.Nome });
                }

                // INSERT DO FLUXO
                String insertFluxo = "INSERT INTO ZFLUXO (BIBLIOTECA, CLASSE, NOME, DESCRICAO, TIPORETORNO, CONTADOR) VALUES (?, ?, ?, ?, ?, ?)";
                int contFluxo = AppLib.Context.poolConnection.Get().ExecTransaction(insertFluxo, new object[] { fluxo.Propriedades.Biblioteca, fluxo.Propriedades.Classe, fluxo.Propriedades.Nome, fluxo.Propriedades.Descricao, fluxo.Propriedades.TipoRetorno, fluxo.Propriedades.Contador });

                // INSERT DAS VARIAVEIS
                String insertVariaveis = "INSERT INTO ZFLUXOVARIAVEL (FLUXO, VARIAVEL, TIPOVARIAVEL, TIPODADO) VALUES (?, ?, ?, ?)";
                int contVariaveis = 0;
                for (int i = 0; i < fluxo.Propriedades.Variaveis.Count; i++)
                {
                    contVariaveis += AppLib.Context.poolConnection.Get().ExecTransaction(insertVariaveis, new object[] { fluxo.Propriedades.Nome, fluxo.Propriedades.Variaveis[i].Variavel, fluxo.Propriedades.Variaveis[i].TipoVariavel, fluxo.Propriedades.Variaveis[i].TipoDado });
                }

                // INSERT DAS FIGURAS
                String insertFiguras = "";
                int contFiguras = 0;
                int contParametros = 0;
                for (int i = 0; i < fluxo.Figuras.Count; i++)
                {
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                    {
                        FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                        String tipo = "Inicio";
                        insertFiguras = "INSERT INTO ZFLUXOFIGURA (FLUXO, TIPOFIGURA, NOME, X, Y, TEXTO, DESTINO) VALUES (?, ?, ?, ?, ?, ?, ?)";
                        contFiguras += AppLib.Context.poolConnection.Get().ExecTransaction(insertFiguras, new object[] { fluxo.Propriedades.Nome, tipo, fig.Nome, fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Caixa.Texto, fig.Destino });
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                    {
                        FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                        String tipo = "Fim";
                        insertFiguras = "INSERT INTO ZFLUXOFIGURA (FLUXO, TIPOFIGURA, NOME, X, Y, TEXTO, RETORNO) VALUES (?, ?, ?, ?, ?, ?, ?)";
                        contFiguras += AppLib.Context.poolConnection.Get().ExecTransaction(insertFiguras, new object[] { fluxo.Propriedades.Nome, tipo, fig.Nome, fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Caixa.Texto, fig.Retorno });
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                    {
                        FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                        String tipo = "Processo";
                        insertFiguras = "INSERT INTO ZFLUXOFIGURA (FLUXO, TIPOFIGURA, NOME, X, Y, TEXTO, DESTINO, VARIAVEL, ATRIBUICAO, EXPRESSAO, EXPRESSAOXML) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                        contFiguras += AppLib.Context.poolConnection.Get().ExecTransaction(insertFiguras, new object[] { fluxo.Propriedades.Nome, tipo, fig.Nome, fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Caixa.Texto, fig.Destino, fig.Expressao.Variavel, fig.Expressao.Atribuicao, fig.Expressao.Texto, fig.Expressao.ExpressaoXML });
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                    {
                        FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                        String tipo = "Condicao";
                        insertFiguras = "INSERT INTO ZFLUXOFIGURA (FLUXO, TIPOFIGURA, NOME, X, Y, TEXTO, DESTINO, DESTINOVERDADEIRO, DESTINOFALSO, VARIAVEL, ATRIBUICAO, EXPRESSAO, EXPRESSAOXML) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                        contFiguras += AppLib.Context.poolConnection.Get().ExecTransaction(insertFiguras, new object[] { fluxo.Propriedades.Nome, tipo, fig.Nome, fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Caixa.Texto, fig.Destino, fig.DestinoVerdadeiro, fig.DestinoFalso, fig.Expressao.Variavel, fig.Expressao.Atribuicao, fig.Expressao.Texto, fig.Expressao.ExpressaoXML });
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                    {
                        FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                        String tipo = "Transacao";
                        insertFiguras = "INSERT INTO ZFLUXOFIGURA (FLUXO, TIPOFIGURA, NOME, X, Y, TEXTO, DESTINO, DESTINOVERDADEIRO, DESTINOFALSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                        contFiguras += AppLib.Context.poolConnection.Get().ExecTransaction(insertFiguras, new object[] { fluxo.Propriedades.Nome, tipo, fig.Nome, fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Caixa.Texto, fig.Destino, fig.DestinoVerdadeiro, fig.DestinoFalso });
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                    {
                        FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                        String tipo = "Repeticao";
                        insertFiguras = "INSERT INTO ZFLUXOFIGURA (FLUXO, TIPOFIGURA, NOME, X, Y, TEXTO, DESTINOVERDADEIRO, DESTINOFALSO, VARIAVEL, ATRIBUICAO, EXPRESSAO, EXPRESSAOXML) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                        contFiguras += AppLib.Context.poolConnection.Get().ExecTransaction(insertFiguras, new object[] { fluxo.Propriedades.Nome, tipo, fig.Nome, fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Caixa.Texto, fig.DestinoVerdadeiro, fig.DestinoFalso, fig.Expressao.Variavel, fig.Expressao.Atribuicao, fig.Expressao.Texto, fig.Expressao.ExpressaoXML });
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                    {
                        FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                        String tipo = "Subprocesso";
                        insertFiguras = "INSERT INTO ZFLUXOFIGURA (FLUXO, TIPOFIGURA, NOME, X, Y, TEXTO, DESTINO, SUBPROCESSO) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                        contFiguras += AppLib.Context.poolConnection.Get().ExecTransaction(insertFiguras, new object[] { fluxo.Propriedades.Nome, tipo, fig.Nome, fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Caixa.Texto, fig.Destino, fig.Subprocesso });

                        // INSERT DOS PARAMETROS
                        String insertParametros = "INSERT INTO ZFLUXOSUBFLUXO (FLUXO, FIGURA, NOME, VALOR) VALUES (?, ?, ?, ?)";

                        for (int j = 0; j < fig.Variaveis.Count; j++ )
                        {
                            contParametros += AppLib.Context.poolConnection.Get().ExecTransaction(insertParametros, new Object[] { fluxo.Propriedades.Nome, fig.Nome, fig.Variaveis[j].Parametro, fig.Variaveis[j].Valor });
                        }
                    }
                }

                // TRATA SALVAMENTO DO FLUXO
                if (contFluxo > 0)
                {
                    if (contVariaveis == fluxo.Propriedades.Variaveis.Count)
                    {
                        if (contFiguras == fluxo.Figuras.Count)
                        {
                            // OK
                            // System.Windows.Forms.MessageBox.Show("Fluxo salvo com sucesso.");
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Erro ao salvar figuras do fluxo.");
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Erro ao salvar variáveis do fluxo.");
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Erro ao salvar o fluxo.");
                }
            }

        }

        public void ExportarImagem()
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "(*.bmp)|*.bmp";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int maiorX = 0;
                int maiorY = 0;
                int margem = 100;

                #region BUSCA MAIOR POSIÇÃO X e Y

                for (int i = 0; i < fluxo.Figuras.Count; i++)
                {
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                    {
                        FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                        if (fig.Caixa.PontoX > maiorX)
                        {
                            maiorX = fig.Caixa.PontoX;
                        }
                        if (fig.Caixa.PontoY > maiorY)
                        {
                            maiorY = fig.Caixa.PontoY;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                    {
                        FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                        if (fig.Caixa.PontoX > maiorX)
                        {
                            maiorX = fig.Caixa.PontoX;
                        }
                        if (fig.Caixa.PontoY > maiorY)
                        {
                            maiorY = fig.Caixa.PontoY;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                    {
                        FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                        if (fig.Caixa.PontoX > maiorX)
                        {
                            maiorX = fig.Caixa.PontoX;
                        }
                        if (fig.Caixa.PontoY > maiorY)
                        {
                            maiorY = fig.Caixa.PontoY;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                    {
                        FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                        if (fig.Caixa.PontoX > maiorX)
                        {
                            maiorX = fig.Caixa.PontoX;
                        }
                        if (fig.Caixa.PontoY > maiorY)
                        {
                            maiorY = fig.Caixa.PontoY;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                    {
                        FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                        if (fig.Caixa.PontoX > maiorX)
                        {
                            maiorX = fig.Caixa.PontoX;
                        }
                        if (fig.Caixa.PontoY > maiorY)
                        {
                            maiorY = fig.Caixa.PontoY;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                    {
                        FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                        if (fig.Caixa.PontoX > maiorX)
                        {
                            maiorX = fig.Caixa.PontoX;
                        }
                        if (fig.Caixa.PontoY > maiorY)
                        {
                            maiorY = fig.Caixa.PontoY;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                    {
                        FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                        if (fig.Caixa.PontoX > maiorX)
                        {
                            maiorX = fig.Caixa.PontoX;
                        }
                        if (fig.Caixa.PontoY > maiorY)
                        {
                            maiorY = fig.Caixa.PontoY;
                        }
                    }
                } 

                #endregion

                maiorX += margem;
                maiorY += margem;

                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(maiorX, maiorY);
                pictureBox.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, maiorX, maiorY));
                bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }

        public void ExportarFluxo()
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FluxoExpImp fei = new FluxoExpImp();
                fei.Exportar(fbd.SelectedPath + "\\" + fluxo.Propriedades.Nome + ".xml", fluxo);
            }
        }

        public void ImportarFluxo()
        {
            Novo(true);

            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "(*.xml)|*.xml";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FluxoExpImp fei = new FluxoExpImp();
                this.fluxo = fei.Importar(ofd.FileName);
                this.Desenhar();
            }
        }

        public void Sair()
        {
            SalvarComo();
        }

        #endregion

        #region EDITAR

        public void Desfazer()
        {
            if (desrefazer.ListaDesfazer.Count > 0)
            {
                Object obj = desrefazer.Desfazer();

                if (obj.GetType() == typeof(Fluxo))
                {
                    Fluxo f = (Fluxo)obj;
                    this.fluxo = f;
                    this.Desenhar();
                }
            }
        }

        public void Refazer()
        {
            if (desrefazer.ListaRefazer.Count > 0)
            {
                this.fluxo = (Fluxo)desrefazer.Refazer();
                this.Desenhar();
            }
        }

        public void Recortar()
        {
            this.Copiar();
            this.Excluir();
        }

        public void Copiar()
        {
            fluxo.FigurasTransf.Clear();

            for (int i = fluxo.Figuras.Count - 1; i != -1; i--)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fluxo.FigurasTransf.Add(null);
                        fluxo.FigurasTransf[fluxo.FigurasTransf.Count - 1] = fig.Copiar();
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fluxo.FigurasTransf.Add(null);
                        fluxo.FigurasTransf[fluxo.FigurasTransf.Count - 1] = fig.Copiar();
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fluxo.FigurasTransf.Add(null);
                        fluxo.FigurasTransf[fluxo.FigurasTransf.Count - 1] = fig.Copiar();
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fluxo.FigurasTransf.Add(null);
                        fluxo.FigurasTransf[fluxo.FigurasTransf.Count - 1] = fig.Copiar();
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fluxo.FigurasTransf.Add(null);
                        fluxo.FigurasTransf[fluxo.FigurasTransf.Count - 1] = fig.Copiar();
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fluxo.FigurasTransf.Add(null);
                        fluxo.FigurasTransf[fluxo.FigurasTransf.Count - 1] = fig.Copiar();
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fluxo.FigurasTransf.Add(null);
                        fluxo.FigurasTransf[fluxo.FigurasTransf.Count - 1] = fig.Copiar();
                    }
                }
            }
        }

        public void Colar()
        {
            for (int i = fluxo.FigurasTransf.Count - 1; i != -1; i--)
            {
                fluxo.Propriedades.Contador++;

                if (fluxo.FigurasTransf[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio figTrans = (FiguraInicio)fluxo.FigurasTransf[i];
                    fluxo.Figuras.Add(null);
                    fluxo.Figuras[fluxo.Figuras.Count - 1] = figTrans.Copiar();

                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[fluxo.Figuras.Count - 1];
                    fig.Nome = "Inicio" + fluxo.Propriedades.Contador;
                    fig.Destino = "";
                }

                if (fluxo.FigurasTransf[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim figTrans = (FiguraFim)fluxo.FigurasTransf[i];
                    fluxo.Figuras.Add(null);
                    fluxo.Figuras[fluxo.Figuras.Count - 1] = figTrans.Copiar();

                    FiguraFim fig = (FiguraFim)fluxo.Figuras[fluxo.Figuras.Count - 1];
                    fig.Nome = "Fim" + fluxo.Propriedades.Contador;
                }

                if (fluxo.FigurasTransf[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso figTrans = (FiguraProcesso)fluxo.FigurasTransf[i];
                    fluxo.Figuras.Add(null);
                    fluxo.Figuras[fluxo.Figuras.Count - 1] = figTrans.Copiar();

                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[fluxo.Figuras.Count - 1];
                    fig.Nome = "Processo" + fluxo.Propriedades.Contador;
                    fig.Destino = "";
                }

                if (fluxo.FigurasTransf[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao figTrans = (FiguraCondicao)fluxo.FigurasTransf[i];
                    fluxo.Figuras.Add(null);
                    fluxo.Figuras[fluxo.Figuras.Count - 1] = figTrans.Copiar();

                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[fluxo.Figuras.Count - 1];
                    fig.Nome = "Condição" + fluxo.Propriedades.Contador;
                    fig.DestinoVerdadeiro = "";
                    fig.DestinoFalso = "";
                }

                if (fluxo.FigurasTransf[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao figTrans = (FiguraTransacao)fluxo.FigurasTransf[i];
                    fluxo.Figuras.Add(null);
                    fluxo.Figuras[fluxo.Figuras.Count - 1] = figTrans.Copiar();

                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[fluxo.Figuras.Count - 1];
                    fig.Nome = "Transação" + fluxo.Propriedades.Contador;
                    fig.DestinoVerdadeiro = "";
                    fig.DestinoFalso = "";
                }

                if (fluxo.FigurasTransf[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao figTrans = (FiguraRepeticao)fluxo.FigurasTransf[i];
                    fluxo.Figuras.Add(null);
                    fluxo.Figuras[fluxo.Figuras.Count - 1] = figTrans.Copiar();

                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[fluxo.Figuras.Count - 1];
                    fig.Nome = "Repetição" + fluxo.Propriedades.Contador;
                    fig.DestinoVerdadeiro = "";
                    fig.DestinoFalso = "";
                }

                if (fluxo.FigurasTransf[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso figTrans = (FiguraSubprocesso)fluxo.FigurasTransf[i];
                    fluxo.Figuras.Add(null);
                    fluxo.Figuras[fluxo.Figuras.Count - 1] = figTrans.Copiar();

                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[fluxo.Figuras.Count - 1];
                    fig.Nome = "Subprocesso" + fluxo.Propriedades.Contador;
                    fig.Destino = "";
                }
            }

            this.Desenhar();
        }

        public void Excluir()
        {
            int todos = 0;

            do
            {

                todos = 0;

                for (int i = fluxo.Figuras.Count - 1; i != -1; i--)
                {
                    if (todos == 0)
                    {
                        if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                        {
                            FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                            if (fig.Selecionado)
                            {
                                fluxo.Figuras.RemoveAt(i);
                                todos++;
                            }
                        }
                    }

                    if (todos == 0)
                    {
                        if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                        {
                            FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                            if (fig.Selecionado)
                            {
                                fluxo.Figuras.RemoveAt(i);
                                todos++;
                            }
                        }
                    }

                    if (todos == 0)
                    {
                        if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                        {
                            FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                            if (fig.Selecionado)
                            {
                                fluxo.Figuras.RemoveAt(i);
                                todos++;
                            }
                        }
                    }

                    if (todos == 0)
                    {
                        if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                        {
                            FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                            if (fig.Selecionado)
                            {
                                fluxo.Figuras.RemoveAt(i);
                                todos++;
                            }
                        }
                    }

                    if (todos == 0)
                    {
                        if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                        {
                            FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                            if (fig.Selecionado)
                            {
                                fluxo.Figuras.RemoveAt(i);
                                todos++;
                            }
                        }
                    }

                    if (todos == 0)
                    {
                        if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                        {
                            FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                            if (fig.Selecionado)
                            {
                                fluxo.Figuras.RemoveAt(i);
                                todos++;
                            }
                        }
                    }

                    if (todos == 0)
                    {
                        if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                        {
                            FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                            if (fig.Selecionado)
                            {
                                fluxo.Figuras.RemoveAt(i);
                                todos++;
                            }
                        }
                    }
                }

            } while (todos != 0);

            Desenhar();
            LimparSelecao();
        }

        public void SelecionarTudo()
        {
            Selecionar2(true);
            Desenhar();
        }

        public void InverterSelecao()
        {
            for (int i = fluxo.Figuras.Count - 1; i != -1; i--)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    fig.Selecionado = !fig.Selecionado;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    fig.Selecionado = !fig.Selecionado;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    fig.Selecionado = !fig.Selecionado;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    fig.Selecionado = !fig.Selecionado;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    fig.Selecionado = !fig.Selecionado;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    fig.Selecionado = !fig.Selecionado;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    fig.Selecionado = !fig.Selecionado;
                }
            }

            Desenhar();
        }

        public void LimparSelecao()
        {
            Selecionar2(false);
        }

        public void Selecionar2(Boolean b)
        {
            for (int i = fluxo.Figuras.Count - 1; i != -1; i--)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    fig.Selecionado = b;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    fig.Selecionado = b;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    fig.Selecionado = b;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    fig.Selecionado = b;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    fig.Selecionado = b;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    fig.Selecionado = b;
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    fig.Selecionado = b;
                }
            }

        }

        #endregion

        #region INSERIR

        public void Inicio()
        {
            caixa.Largura = 35;
            caixa.Altura = 35;
            caixa.CorFundo = System.Drawing.Color.DarkSeaGreen;
            caixa.CorBorda = System.Drawing.Color.SeaGreen;
            caixa.UsaBorda = false;
            caixa.CorFonte = System.Drawing.Color.Black;
            caixa.Fonte = CaixaTexto.OpcoesFonte.Arial.ToString().Replace("_", " ");
            caixa.TamanhoFonte = 7;
            caixa.EstiloFonte = CaixaTexto.OpcoesEstilo.Normal.ToString();
            caixa.AlinhamentoHorizontal = CaixaTexto.OpcoesAlinhamentoHorizontal.Centralizado.ToString();
            caixa.AlinhamentoVertical = CaixaTexto.OpcoesAlinhamentoVertical.Centralizado.ToString();
            caixa.Texto = "Inicio";
            Ponteiro = false;
        }

        public void Fim()
        {
            caixa.Largura = 40;
            caixa.Altura = 40;
            caixa.CorFundo = System.Drawing.Color.Gray;
            caixa.CorBorda = System.Drawing.Color.DimGray;
            caixa.UsaBorda = false;
            caixa.CorFonte = System.Drawing.Color.Black;
            caixa.Fonte = CaixaTexto.OpcoesFonte.Arial.ToString().Replace("_", " ");
            caixa.TamanhoFonte = 7;
            caixa.EstiloFonte = CaixaTexto.OpcoesEstilo.Normal.ToString();
            caixa.AlinhamentoHorizontal = CaixaTexto.OpcoesAlinhamentoHorizontal.Centralizado.ToString();
            caixa.AlinhamentoVertical = CaixaTexto.OpcoesAlinhamentoVertical.Centralizado.ToString();
            caixa.Texto = "Fim";
            Ponteiro = false;
        }

        public void Processo()
        {
            caixa.Largura = 96;
            caixa.Altura = 60;
            caixa.CorFundo = System.Drawing.Color.Gainsboro;
            caixa.CorBorda = System.Drawing.Color.Silver;
            caixa.UsaBorda = false;
            caixa.CorFonte = System.Drawing.Color.Black;
            caixa.Fonte = CaixaTexto.OpcoesFonte.Arial.ToString().Replace("_", " ");
            caixa.TamanhoFonte = 7;
            caixa.EstiloFonte = CaixaTexto.OpcoesEstilo.Normal.ToString();
            caixa.AlinhamentoHorizontal = CaixaTexto.OpcoesAlinhamentoHorizontal.Centralizado.ToString();
            caixa.AlinhamentoVertical = CaixaTexto.OpcoesAlinhamentoVertical.Centralizado.ToString();
            caixa.Texto = "Processo";
            Ponteiro = false;
        }

        public void Condicao()
        {
            caixa.Largura = 50;
            caixa.Altura = 50;
            caixa.CorFundo = System.Drawing.Color.Gold;
            caixa.CorBorda = System.Drawing.Color.Orange;
            caixa.UsaBorda = false;
            caixa.CorFonte = System.Drawing.Color.Black;
            caixa.Fonte = CaixaTexto.OpcoesFonte.Arial.ToString().Replace("_", " ");
            caixa.TamanhoFonte = 7;
            caixa.EstiloFonte = CaixaTexto.OpcoesEstilo.Normal.ToString();
            caixa.AlinhamentoHorizontal = CaixaTexto.OpcoesAlinhamentoHorizontal.Centralizado.ToString();
            caixa.AlinhamentoVertical = CaixaTexto.OpcoesAlinhamentoVertical.Centralizado.ToString();
            caixa.Texto = "Condição";
            Ponteiro = false;
        }

        public void Transacao()
        {
            caixa.Largura = 50;
            caixa.Altura = 50;
            caixa.CorFundo = System.Drawing.Color.SandyBrown;
            caixa.CorBorda = System.Drawing.Color.Brown;
            caixa.UsaBorda = false;
            caixa.CorFonte = System.Drawing.Color.Black;
            caixa.Fonte = CaixaTexto.OpcoesFonte.Arial.ToString().Replace("_", " ");
            caixa.TamanhoFonte = 7;
            caixa.EstiloFonte = CaixaTexto.OpcoesEstilo.Normal.ToString();
            caixa.AlinhamentoHorizontal = CaixaTexto.OpcoesAlinhamentoHorizontal.Centralizado.ToString();
            caixa.AlinhamentoVertical = CaixaTexto.OpcoesAlinhamentoVertical.Centralizado.ToString();
            caixa.Texto = "Transação";
            Ponteiro = false;
        }

        public void Repeticao()
        {
            caixa.Largura = 60;
            caixa.Altura = 60;
            caixa.CorFundo = System.Drawing.Color.LightSteelBlue;
            caixa.CorBorda = System.Drawing.Color.LightSlateGray;
            caixa.UsaBorda = false;
            caixa.CorFonte = System.Drawing.Color.Black;
            caixa.Fonte = CaixaTexto.OpcoesFonte.Arial.ToString().Replace("_", " ");
            caixa.TamanhoFonte = 7;
            caixa.EstiloFonte = CaixaTexto.OpcoesEstilo.Normal.ToString();
            caixa.AlinhamentoHorizontal = CaixaTexto.OpcoesAlinhamentoHorizontal.Centralizado.ToString();
            caixa.AlinhamentoVertical = CaixaTexto.OpcoesAlinhamentoVertical.Centralizado.ToString();
            caixa.Texto = "Repetição";
            Ponteiro = false;
        }

        public void Subprocesso()
        {
            caixa.Largura = 80;
            caixa.Altura = 60;
            caixa.CorFundo = System.Drawing.Color.SandyBrown;
            caixa.CorBorda = System.Drawing.Color.Brown;
            caixa.UsaBorda = true;
            caixa.CorFonte = System.Drawing.Color.Black;
            caixa.Fonte = CaixaTexto.OpcoesFonte.Arial.ToString().Replace("_", " ");
            caixa.TamanhoFonte = 7;
            caixa.EstiloFonte = CaixaTexto.OpcoesEstilo.Normal.ToString();
            caixa.AlinhamentoHorizontal = CaixaTexto.OpcoesAlinhamentoHorizontal.Centralizado.ToString();
            caixa.AlinhamentoVertical = CaixaTexto.OpcoesAlinhamentoVertical.Centralizado.ToString();
            caixa.Texto = "Subprocesso";
            Ponteiro = false;
        }

        #endregion

        #region FORMATAR

        public void AlinharHorizontal()
        {
            for (int i = 0; i < fluxo.Figuras.Count; i++)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoY = refSelecaoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoY = refSelecaoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoY = refSelecaoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoY = refSelecaoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoY = refSelecaoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoY = refSelecaoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoY = refSelecaoY;
                    }
                }
            }

            Desenhar();
        }

        public void AlinharVertical()
        {
            for (int i = 0; i < fluxo.Figuras.Count; i++)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoX = refSelecaoX;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoX = refSelecaoX;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoX = refSelecaoX;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoX = refSelecaoX;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoX = refSelecaoX;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoX = refSelecaoX;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    if (fig.Selecionado)
                    {
                        fig.Caixa.PontoX = refSelecaoX;
                    }
                }
            }

            Desenhar();
        }

        #endregion

        #region EXIBIR

        public Boolean PropriedadesFluxo()
        {
            fluxo.Propriedades.Editar();
            return true;
        }

        public void OpcoesGerais()
        {
            new FormOpcoesGerais().ShowDialog();
        }

        #endregion

        #region FERRAMENTAS

        public String GerarCodigoFonte()
        {
            String CodigoFonte = "";
            String BibliotecaPadrao = "AppLibJit";
            String ClassePadrao = "AppClassJit";
            
            // ESCREVE AS BIBLIOTECAS DO FRAMEWORK
            System.Data.DataTable dtUsing = AppLib.Context.poolConnection.Get().ExecQuery("SELECT DISTINCT NOME FROM ZFLUXOBIBLIOTECA ORDER BY NOME", new Object[] { });
            for (int k = 0; k < dtUsing.Rows.Count; k++)
            {
                CodigoFonte += "using " + dtUsing.Rows[k]["NOME"].ToString() + ";\r\n";
            }
            CodigoFonte += "\r\n";

            // ABRE O NAMESPACE PADRÃO
            CodigoFonte += "namespace " + BibliotecaPadrao + "\r\n";
            CodigoFonte += "{\r\n\n";

            // ABRE A CLASSE PADRÃO
            CodigoFonte += "\tpublic class " + ClassePadrao + "\r\n";
            CodigoFonte += "\t{\r\n\n";

            CodigoFonte += "\t\tpublic static void Main(){}\r\n\n";

            // LOOP DE BIBLIOTECAS ORBITUS
            System.Data.DataTable dtBiblitecas = AppLib.Context.poolConnection.Get().ExecQuery("SELECT DISTINCT BIBLIOTECA FROM ZFLUXO ORDER BY 1", new Object[] { });
            for (int i = 0; i < dtBiblitecas.Rows.Count; i++)
            {
                String Biblioteca = dtBiblitecas.Rows[i]["BIBLIOTECA"].ToString();
                CodigoFonte += "\t\t#region BIBLIOTECA " + Biblioteca + "\r\n\n";

                // LOOP DE CLASSES ORBITUS
                System.Data.DataTable dtClasses = AppLib.Context.poolConnection.Get().ExecQuery("SELECT DISTINCT CLASSE FROM ZFLUXO WHERE BIBLIOTECA = ? ORDER BY 1", new Object[] { Biblioteca });
                for (int j = 0; j < dtClasses.Rows.Count; j++)
                {
                    String Classe = dtClasses.Rows[j]["CLASSE"].ToString();
                    CodigoFonte += "\t\t#region CLASSE " + Classe + "\r\n";

                    // LOOP DE FLUXOS
                    System.Data.DataTable dtFluxos = AppLib.Context.poolConnection.Get().ExecQuery("SELECT NOME FROM ZFLUXO WHERE BIBLIOTECA = ? AND CLASSE = ? AND NOME IN (SELECT FLUXO FROM ZFLUXOEXT WHERE ATIVO = 1) ORDER BY 1", new Object[] { Biblioteca, Classe });

                    for (int k = 0; k < dtFluxos.Rows.Count; k++)
                    {
                        String Fluxo = dtFluxos.Rows[k]["NOME"].ToString();

                        System.Windows.Forms.PictureBox pb = new System.Windows.Forms.PictureBox();
                        AppLib.Fluxo.Principal principal = new AppLib.Fluxo.Principal(pb);
                        principal.Abrir(Fluxo);
                        CSharp cs = new CSharp(principal.fluxo);
                        cs.Metodo();
                        CodigoFonte += cs.CodigoFonte + "\r\n";
                    }
                    CodigoFonte += "\r\n";

                    // FECHA LOOP CLASSES ORBITUS
                    CodigoFonte += "\t\t#endregion\r\n\n";
                }

                // FECHA LOOP BIBLIOTECA ORBITUS
                CodigoFonte += "\t\t#endregion\r\n\n";
            }

            // FECHA A CLASSE
            CodigoFonte += "\t}\r\n\n";

            // FECHA O NAMESPACE
            CodigoFonte += "}\r\n";

            return CodigoFonte;
        }

        public void ExportarCodigoFonte()
        {
            // SELECIONA DIRETÓRIO
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String Diretorio = fbd.SelectedPath;
                String ClassePadrao = "AppClassJit";
                String Conteudo = this.GerarCodigoFonte();

                // GERA O ARQUIVO
                AppLib.Util.Arquivo a = new AppLib.Util.Arquivo();
                String arquivo = Diretorio + "\\" + ClassePadrao + ".cs";
                a.Escrever(arquivo, Conteudo);
            }
        }

        #endregion

        #region OUTROS

        public Boolean Selecionar(int xDown, int yDown)
        {
            Boolean b = false;

            #region SELECIONA FIGURA PELA POSIÇÃO

            for (int i = fluxo.Figuras.Count - 1; i != -1; i--)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        fig.Selecionado = !fig.Selecionado;
                        if (fig.Selecionado)
                        {
                            refSelecaoX = fig.Caixa.PontoX;
                            refSelecaoY = fig.Caixa.PontoY;
                        }
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        fig.Selecionado = !fig.Selecionado;
                        if (fig.Selecionado)
                        {
                            refSelecaoX = fig.Caixa.PontoX;
                            refSelecaoY = fig.Caixa.PontoY;
                        }
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        fig.Selecionado = !fig.Selecionado;
                        if (fig.Selecionado)
                        {
                            refSelecaoX = fig.Caixa.PontoX;
                            refSelecaoY = fig.Caixa.PontoY;
                        }
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        fig.Selecionado = !fig.Selecionado;
                        if (fig.Selecionado)
                        {
                            refSelecaoX = fig.Caixa.PontoX;
                            refSelecaoY = fig.Caixa.PontoY;
                        }
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        fig.Selecionado = !fig.Selecionado;
                        if (fig.Selecionado)
                        {
                            refSelecaoX = fig.Caixa.PontoX;
                            refSelecaoY = fig.Caixa.PontoY;
                        }
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        fig.Selecionado = !fig.Selecionado;
                        if (fig.Selecionado)
                        {
                            refSelecaoX = fig.Caixa.PontoX;
                            refSelecaoY = fig.Caixa.PontoY;
                        }
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        fig.Selecionado = !fig.Selecionado;
                        if (fig.Selecionado)
                        {
                            refSelecaoX = fig.Caixa.PontoX;
                            refSelecaoY = fig.Caixa.PontoY;
                        }
                        return true;
                    }
                }
            }
            // FIM DO LOOP

            #endregion

            #region SE NÃO SELECIONOU PORQUE É PONTEIRO, LIMPAR SELEÇÃO

            if (!b)
            {
                if (Ponteiro)
                {
                    LimparSelecao();
                    return true;
                }
            }

            #endregion

            return false;
        }

        public Boolean Inserir(int xDown, int yDown)
        {
            fluxo.Propriedades.Contador++;

            CaixaTexto caixaTemp = new CaixaTexto();
            caixaTemp = caixaTemp.CopiarCaixaTexto(caixa, caixaTemp);
            caixaTemp.PontoX = xDown;
            caixaTemp.PontoY = yDown;

            if (caixa.Texto.Equals("Inicio"))
            {
                FiguraInicio figura = new FiguraInicio(caixaTemp, graph);
                figura.Nome = caixaTemp.Texto + fluxo.Propriedades.Contador;
                fluxo.Figuras.Add(figura);
                Ponteiro = true;
                return true;
            }

            if (caixa.Texto.Equals("Fim"))
            {
                FiguraFim figura = new FiguraFim(caixaTemp, graph);
                figura.Nome = caixaTemp.Texto + fluxo.Propriedades.Contador;
                fluxo.Figuras.Add(figura);
                Ponteiro = true;
                return true;
            }

            if (caixa.Texto.Equals("Processo"))
            {
                FiguraProcesso figura = new FiguraProcesso(caixaTemp, graph);
                figura.Nome = caixaTemp.Texto + fluxo.Propriedades.Contador;
                fluxo.Figuras.Add(figura);
                Ponteiro = true;
                return true;
            }

            if (caixa.Texto.Equals("Condição"))
            {
                FiguraCondicao figura = new FiguraCondicao(caixaTemp, graph);
                figura.Nome = caixaTemp.Texto + fluxo.Propriedades.Contador;
                fluxo.Figuras.Add(figura);
                Ponteiro = true;
                return true;
            }

            if (caixa.Texto.Equals("Transação"))
            {
                FiguraTransacao figura = new FiguraTransacao(caixaTemp, graph);
                figura.Nome = caixaTemp.Texto + fluxo.Propriedades.Contador;
                fluxo.Figuras.Add(figura);
                Ponteiro = true;
                return true;
            }

            if (caixa.Texto.Equals("Repetição"))
            {
                FiguraRepeticao figura = new FiguraRepeticao(caixaTemp, graph);
                figura.Nome = caixaTemp.Texto + fluxo.Propriedades.Contador;
                fluxo.Figuras.Add(figura);
                Ponteiro = true;
                return true;
            }

            if (caixa.Texto.Equals("Subprocesso"))
            {
                FiguraSubprocesso figura = new FiguraSubprocesso(caixaTemp, graph);
                figura.Nome = caixaTemp.Texto + fluxo.Propriedades.Contador;
                fluxo.Figuras.Add(figura);
                Ponteiro = true;
                return true;
            }

            return false;
        }

        public Boolean PropriedadesFigura(int xDown, int yDown)
        {
            Boolean b = false;

            for (int i = fluxo.Figuras.Count - 1; i != -1; i--)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        fig.Editar(new FluxoUtil().ListaDestino(fluxo));
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        fig.Editar(fluxo.Propriedades);
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        fig.Editar(new FluxoUtil().ListaDestino(fluxo), fluxo.Propriedades.Nome);
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        fig.Editar(new FluxoUtil().ListaDestino(fluxo), fluxo.Propriedades.Nome);
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        fig.Editar(new FluxoUtil().ListaDestino(fluxo), fluxo.Propriedades.Nome);
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        fig.Editar(new FluxoUtil().ListaDestino(fluxo), fluxo.Propriedades.Nome);
                        return true;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        fig.Editar(new FluxoUtil().ListaDestino(fluxo), fluxo.Propriedades.Variaveis);
                        return true;
                    }
                }
            }

            return b;
        }

        public Boolean Mover(int xDown, int yDown, int xUp, int yUp)
        {
            Boolean b = false;

            int moverX = 0;
            int moverY = 0;

            #region OBTER COORDENADA A MOVER

            for (int i = fluxo.Figuras.Count - 1; i != -1; i--)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        moverX = xUp - xDown;
                        moverY = yUp - yDown;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        moverX = xUp - xDown;
                        moverY = yUp - yDown;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        moverX = xUp - xDown;
                        moverY = yUp - yDown;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        moverX = xUp - xDown;
                        moverY = yUp - yDown;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        moverX = xUp - xDown;
                        moverY = yUp - yDown;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        moverX = xUp - xDown;
                        moverY = yUp - yDown;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        moverX = xUp - xDown;
                        moverY = yUp - yDown;
                    }
                }
            }

            #endregion

            #region MOVER DE FATO (TODOS SELECIONADOS)

            if ((moverX != 0) && (moverY != 0))
            {
                for (int i = fluxo.Figuras.Count - 1; i != -1; i--)
                {
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                    {
                        FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                        if (fig.Selecionado)
                        {
                            fig.Caixa.PontoX += moverX;
                            fig.Caixa.PontoY += moverY;
                            b = true;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                    {
                        FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                        if (fig.Selecionado)
                        {
                            fig.Caixa.PontoX += moverX;
                            fig.Caixa.PontoY += moverY;
                            b = true;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                    {
                        FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                        if (fig.Selecionado)
                        {
                            fig.Caixa.PontoX += moverX;
                            fig.Caixa.PontoY += moverY;
                            b = true;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                    {
                        FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                        if (fig.Selecionado)
                        {
                            fig.Caixa.PontoX += moverX;
                            fig.Caixa.PontoY += moverY;
                            b = true;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                    {
                        FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                        if (fig.Selecionado)
                        {
                            fig.Caixa.PontoX += moverX;
                            fig.Caixa.PontoY += moverY;
                            b = true;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                    {
                        FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                        if (fig.Selecionado)
                        {
                            fig.Caixa.PontoX += moverX;
                            fig.Caixa.PontoY += moverY;
                            b = true;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                    {
                        FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                        if (fig.Selecionado)
                        {
                            fig.Caixa.PontoX += moverX;
                            fig.Caixa.PontoY += moverY;
                            b = true;
                        }
                    }
                }
            }

            #endregion

            return b;
        }

        public Boolean Ligar(int xDown, int yDown, int xUp, int yUp)
        {
            Boolean b = false;

            String FiguraDestino = "";

            #region BUSCA DESTINO

            for (int i = fluxo.Figuras.Count - 1; i != -1; i--)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    if ((xUp >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xUp <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yUp >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yUp <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        FiguraDestino = fig.Nome;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    if ((xUp >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xUp <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yUp >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yUp <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        FiguraDestino = fig.Nome;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    if ((xUp >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xUp <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yUp >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yUp <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        FiguraDestino = fig.Nome;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    if ((xUp >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xUp <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yUp >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yUp <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        FiguraDestino = fig.Nome;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    if ((xUp >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xUp <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yUp >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yUp <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        FiguraDestino = fig.Nome;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    if ((xUp >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xUp <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                        &&
                        (yUp >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yUp <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                    {
                        i = 0;
                        FiguraDestino = fig.Nome;
                    }
                }
            }

            #endregion

            #region SE ENCONTROU O DESTINO, BUSCA ORIGEM

            if (!(FiguraDestino.Equals("")))
            {
                for (int i = fluxo.Figuras.Count - 1; i != -1; i--)
                {
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                    {
                        FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                        if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                            &&
                            (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                        {
                            i = 0;
                            if (fig.Nome != FiguraDestino)
                            {
                                fig.Destino = FiguraDestino;
                                return true;
                            }
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                    {
                        FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                        if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                            &&
                            (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                        {
                            i = 0;
                            if (fig.Nome != FiguraDestino)
                            {
                                fig.Destino = FiguraDestino;
                                return true;
                            }
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                    {
                        FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                        if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                            &&
                            (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                        {
                            i = 0;
                            if (fig.Nome != FiguraDestino)
                            {
                                FormOpcaoCondicao f = new FormOpcaoCondicao();
                                f.ShowDialog();

                                if (f.Result == Result.DestinoVerdadeiro)
                                {
                                    fig.DestinoVerdadeiro = FiguraDestino;
                                    return true;
                                }

                                if (f.Result == Result.DestinoFalso)
                                {
                                    fig.DestinoFalso = FiguraDestino;
                                    return true;
                                }

                                if (f.Result == Result.Destino)
                                {
                                    fig.Destino = FiguraDestino;
                                    return true;
                                }

                                return false;
                            }
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                    {
                        FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                        if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                            &&
                            (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                        {
                            i = 0;
                            if (fig.Nome != FiguraDestino)
                            {
                                FormOpcaoCondicao f = new FormOpcaoCondicao();
                                f.ShowDialog();

                                if (f.Result == Result.DestinoVerdadeiro)
                                {
                                    fig.DestinoVerdadeiro = FiguraDestino;
                                    return true;
                                }

                                if (f.Result == Result.DestinoFalso)
                                {
                                    fig.DestinoFalso = FiguraDestino;
                                    return true;
                                }

                                if (f.Result == Result.Destino)
                                {
                                    fig.Destino = FiguraDestino;
                                    return true;
                                }

                                return false;
                            }
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                    {
                        FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                        if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                            &&
                            (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                        {
                            i = 0;
                            if (fig.Nome != FiguraDestino)
                            {
                                if (Clique.Equals(OpcoesClique.Esquerdo.ToString()))
                                {
                                    fig.DestinoVerdadeiro = FiguraDestino;
                                    return true;
                                }
                                if (Clique.Equals(OpcoesClique.Direito.ToString()))
                                {
                                    fig.DestinoFalso = FiguraDestino;
                                    return true;
                                }
                            }
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                    {
                        FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                        if ((xDown >= fig.Caixa.PontoX - fig.Caixa.Largura / 2) && (xDown <= (fig.Caixa.PontoX + fig.Caixa.Largura / 2))
                            &&
                            (yDown >= fig.Caixa.PontoY - fig.Caixa.Altura / 2) && (yDown <= (fig.Caixa.PontoY + fig.Caixa.Altura / 2)))
                        {
                            i = 0;
                            if (fig.Nome != FiguraDestino)
                            {
                                fig.Destino = FiguraDestino;
                                return true;
                            }
                        }
                    }

                }
            }

            #endregion

            return b;
        }

        public void RecalcularPictureBox()
        {
            int maiorX = 0;
            int maiorY = 0;
            int margem = 200;

            #region BUSCA MAIOR POSIÇÃO X e Y

            for (int i = 0; i < fluxo.Figuras.Count; i++)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    if (fig.Caixa.PontoX > maiorX)
                    {
                        maiorX = fig.Caixa.PontoX;
                    }
                    if (fig.Caixa.PontoY > maiorY)
                    {
                        maiorY = fig.Caixa.PontoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    if (fig.Caixa.PontoX > maiorX)
                    {
                        maiorX = fig.Caixa.PontoX;
                    }
                    if (fig.Caixa.PontoY > maiorY)
                    {
                        maiorY = fig.Caixa.PontoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    if (fig.Caixa.PontoX > maiorX)
                    {
                        maiorX = fig.Caixa.PontoX;
                    }
                    if (fig.Caixa.PontoY > maiorY)
                    {
                        maiorY = fig.Caixa.PontoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    if (fig.Caixa.PontoX > maiorX)
                    {
                        maiorX = fig.Caixa.PontoX;
                    }
                    if (fig.Caixa.PontoY > maiorY)
                    {
                        maiorY = fig.Caixa.PontoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    if (fig.Caixa.PontoX > maiorX)
                    {
                        maiorX = fig.Caixa.PontoX;
                    }
                    if (fig.Caixa.PontoY > maiorY)
                    {
                        maiorY = fig.Caixa.PontoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    if (fig.Caixa.PontoX > maiorX)
                    {
                        maiorX = fig.Caixa.PontoX;
                    }
                    if (fig.Caixa.PontoY > maiorY)
                    {
                        maiorY = fig.Caixa.PontoY;
                    }
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    if (fig.Caixa.PontoX > maiorX)
                    {
                        maiorX = fig.Caixa.PontoX;
                    }
                    if (fig.Caixa.PontoY > maiorY)
                    {
                        maiorY = fig.Caixa.PontoY;
                    }
                }
            }

            #endregion

            maiorX += margem;
            maiorY += margem;

            if (maiorX > 1500)
            {
                pictureBox.Width = maiorX;
            }

            if (maiorY > 1500)
            {
                pictureBox.Height = maiorY;
            }
        }

        public void Desenhar()
        {
            bmp = new System.Drawing.Bitmap(pictureBox.Width, pictureBox.Height);
            graph = System.Drawing.Graphics.FromImage(bmp);
            pictureBox.Image = bmp;

            RecalcularPictureBox();

            #region DESENHAR LIGAÇÃO

            for (int i = 0; i < fluxo.Figuras.Count; i++)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    DesenharLinhas(fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Destino);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    DesenharLinhas(fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Destino);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    DesenharLinhas(fig.Caixa.PontoX, fig.Caixa.PontoY, fig.DestinoVerdadeiro, System.Drawing.Color.Blue);
                    DesenharLinhas(fig.Caixa.PontoX, fig.Caixa.PontoY, fig.DestinoFalso, System.Drawing.Color.Red);
                    DesenharLinhas(fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Destino);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    DesenharLinhas(fig.Caixa.PontoX, fig.Caixa.PontoY, fig.DestinoVerdadeiro, System.Drawing.Color.Blue);
                    DesenharLinhas(fig.Caixa.PontoX, fig.Caixa.PontoY, fig.DestinoFalso, System.Drawing.Color.Red);
                    DesenharLinhas(fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Destino);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    DesenharLinhas(fig.Caixa.PontoX, fig.Caixa.PontoY, fig.DestinoVerdadeiro, System.Drawing.Color.Blue);
                    DesenharLinhas(fig.Caixa.PontoX, fig.Caixa.PontoY, fig.DestinoFalso, System.Drawing.Color.Red);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    DesenharLinhas(fig.Caixa.PontoX, fig.Caixa.PontoY, fig.Destino);
                }
            }

            #endregion

            #region DESENHAR FIGURAS

            for (int i = 0; i < fluxo.Figuras.Count; i++)
            {
                if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                {
                    FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                    fig.Desenhar(graph);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                {
                    FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                    fig.Desenhar(graph);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                {
                    FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                    fig.Desenhar(graph);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                {
                    FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                    fig.Desenhar(graph);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                {
                    FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                    fig.Desenhar(graph);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                {
                    FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                    fig.Desenhar(graph);
                }

                if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                {
                    FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                    fig.Desenhar(graph);
                }
            }

            #endregion

        }

        public void DesenharLinhas(int OrigemX, int OrigemY, String NomeDestino)
        {
            this.DesenharLinhas(OrigemX, OrigemY, NomeDestino, System.Drawing.Color.Black);
        }

        public void DesenharLinhas(int OrigemX, int OrigemY, String NomeDestino, System.Drawing.Color Cor)
        {
            int DestinoX = 0;
            int DestinoY = 0;
            int Desenhar = 0;

            // SE TIVER DESTINO
            if (NomeDestino != null)
            {
                // ENCONTRAR O DESTINO
                for (int i = fluxo.Figuras.Count - 1; i != -1; i--)
                {
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraInicio))
                    {
                        FiguraInicio fig = (FiguraInicio)fluxo.Figuras[i];
                        if (fig.Nome.Equals(NomeDestino))
                        {
                            i = 0;
                            Desenhar++;
                            DestinoX = fig.Caixa.PontoX;
                            DestinoY = fig.Caixa.PontoY;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraFim))
                    {
                        FiguraFim fig = (FiguraFim)fluxo.Figuras[i];
                        if (fig.Nome.Equals(NomeDestino))
                        {
                            i = 0;
                            Desenhar++;
                            DestinoX = fig.Caixa.PontoX;
                            DestinoY = fig.Caixa.PontoY;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraProcesso))
                    {
                        FiguraProcesso fig = (FiguraProcesso)fluxo.Figuras[i];
                        if (fig.Nome.Equals(NomeDestino))
                        {
                            i = 0;
                            Desenhar++;
                            DestinoX = fig.Caixa.PontoX;
                            DestinoY = fig.Caixa.PontoY;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraCondicao))
                    {
                        FiguraCondicao fig = (FiguraCondicao)fluxo.Figuras[i];
                        if (fig.Nome.Equals(NomeDestino))
                        {
                            i = 0;
                            Desenhar++;
                            DestinoX = fig.Caixa.PontoX;
                            DestinoY = fig.Caixa.PontoY;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraTransacao))
                    {
                        FiguraTransacao fig = (FiguraTransacao)fluxo.Figuras[i];
                        if (fig.Nome.Equals(NomeDestino))
                        {
                            i = 0;
                            Desenhar++;
                            DestinoX = fig.Caixa.PontoX;
                            DestinoY = fig.Caixa.PontoY;
                        }
                    }
                    
                    if (fluxo.Figuras[i].GetType() == typeof(FiguraRepeticao))
                    {
                        FiguraRepeticao fig = (FiguraRepeticao)fluxo.Figuras[i];
                        if (fig.Nome.Equals(NomeDestino))
                        {
                            i = 0;
                            Desenhar++;
                            DestinoX = fig.Caixa.PontoX;
                            DestinoY = fig.Caixa.PontoY;
                        }
                    }

                    if (fluxo.Figuras[i].GetType() == typeof(FiguraSubprocesso))
                    {
                        FiguraSubprocesso fig = (FiguraSubprocesso)fluxo.Figuras[i];
                        if (fig.Nome.Equals(NomeDestino))
                        {
                            i = 0;
                            Desenhar++;
                            DestinoX = fig.Caixa.PontoX;
                            DestinoY = fig.Caixa.PontoY;
                        }
                    }
                }
            }

            if (Desenhar == 1)
            {
                Ligacao lig = new Ligacao();
                lig.PontoX1 = OrigemX;
                lig.PontoY1 = OrigemY;
                lig.PontoX2 = DestinoX;
                lig.PontoY2 = DestinoY;
                lig.CorLinha = Cor;

                lig.Espessura = 1.5f;
                lig.Estilo = Ligacao.OpcoesEstilo.Normal.ToString();
                lig.Desenhar(graph);
            }
        }

        #endregion

    }
}
