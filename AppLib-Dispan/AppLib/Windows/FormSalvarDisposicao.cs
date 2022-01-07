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
    public partial class FormSalvarDisposicao : DevExpress.XtraEditors.XtraForm
    {
        public String Conexao { get; set; }
        public String NomeGrid { get; set; }
        public String Usuario { get; set; }
        public String NomeFiltro { get; set; }

        public DevExpress.XtraGrid.Views.Grid.GridView gridView1 { get; set; }

        private String PadraoBotaoTodos = "[BOTÃO TODOS]";

        private List<GridProps> listGridProps = new List<GridProps>();

        public FormSalvarDisposicao()
        {
            InitializeComponent();
        }

        private void FormSalvarDisposicao_Load(object sender, EventArgs e)
        {
            String consulta = @"
SELECT NOME
FROM ZFILTRO
WHERE GRID = ?
  AND (USUARIO IS NULL OR USUARIO = ?)";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(consulta, new Object[] { NomeGrid, Usuario });

            checkedListBox1.Items.Add(PadraoBotaoTodos, false);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String NOME = dt.Rows[i]["NOME"].ToString();
                checkedListBox1.Items.Add(NOME, NOME.Equals(NomeFiltro));
            }
        }

        private void checkButtonTODOS_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, checkButtonTODOS.Checked);
            }

            if (checkButtonTODOS.Checked)
            {
                checkButtonTODOS.Text = "NENHUM";
            }
            else
            {
                checkButtonTODOS.Text = "TODOS";
            }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedIndices.Count > 0)
            {
                splashScreenManager1.ShowWaitForm();

                this.MontarListaConfiguracoes();
                this.SalvarFiltros();

                splashScreenManager1.CloseWaitForm();

                this.Close();
            }
            else
            {
                AppLib.Windows.FormMessageDefault.ShowError("Selecione o(s) filtro(s).");
            }
        }

        private void simpleButtonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void MontarListaConfiguracoes()
        {
            List<GridProps> listGridPropsVisivel = new List<GridProps>();
            List<GridProps> listGridPropsGrupo = new List<GridProps>();
            List<GridProps> listGridPropsInvisivel = new List<GridProps>();

            for (int iColumn = 0; iColumn < gridView1.Columns.Count; iColumn++)
            {
                GridProps gridProps1 = new GridProps();

                gridProps1.Coluna = gridView1.Columns[iColumn].FieldName;
                gridProps1.Sequencia = gridView1.Columns[iColumn].VisibleIndex;
                
                if (gridView1.Columns[iColumn].GroupIndex >= 0)
                {
                    gridProps1.Agrupar = true;
                    gridProps1.Sequencia = gridView1.Columns[iColumn].GroupIndex;
                }
                else
                {
                    gridProps1.Agrupar = false;
                }

                gridProps1.Visivel = gridView1.Columns[iColumn].Visible;
                gridProps1.Largura = gridView1.Columns[iColumn].Width;

                #region ALINHAMENTO

                gridProps1.Alinhamento = Alinhamento.Esquerda;

                if (gridView1.Columns[iColumn].AppearanceHeader.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Far)
                {
                    gridProps1.Alinhamento = Alinhamento.Direita;
                }

                if (gridView1.Columns[iColumn].AppearanceHeader.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Center)
                {
                    gridProps1.Alinhamento = Alinhamento.Centro;
                }

                #endregion

                #region FORMATAÇÃO

                gridProps1.Formato = Formato.Nenhum;

                if (gridView1.Columns[iColumn].DisplayFormat.FormatString.Equals("dd/MM/yyyy"))
                {
                    gridProps1.Formato = Formato.Data;
                }

                if (gridView1.Columns[iColumn].DisplayFormat.FormatString.Equals("dd/MM/yyyy HH:mm"))
                {
                    gridProps1.Formato = Formato.DataHora;
                }

                if (gridView1.Columns[iColumn].DisplayFormat.FormatString.Equals("dd/MM/yyyy HH:mm:ss"))
                {
                    gridProps1.Formato = Formato.DataHoraSegundos;
                }

                if (gridView1.Columns[iColumn].DisplayFormat.FormatString.Equals("dd/MM/yyyy HH:mm:ss.fff"))
                {
                    gridProps1.Formato = Formato.DataHoraMilisegundos;
                }

                if (gridView1.Columns[iColumn].DisplayFormat.FormatString.Equals("f2"))
                {
                    gridProps1.Formato = Formato.Decimal2;
                }

                if (gridView1.Columns[iColumn].DisplayFormat.FormatString.Equals("f4"))
                {
                    gridProps1.Formato = Formato.Decimal4;
                }

                #endregion

                // Encontra a lista temporária apropriada
                if (gridProps1.Agrupar)
                {
                    listGridPropsGrupo.Add(gridProps1);
                }
                else
                {
                    if (gridProps1.Visivel)
                    {
                        listGridPropsVisivel.Add(gridProps1);
                    }
                    else
                    {
                        listGridPropsInvisivel.Add(gridProps1);
                    }
                }
            }

            // Despeja as listas na sequencia correta
            for (int i = 0; i < listGridPropsGrupo.Count; i++)
            {
                listGridProps.Add(this.getItemList(listGridPropsGrupo, i));
            }

            for (int i = 0; i < listGridPropsVisivel.Count; i++)
            {
                listGridProps.Add(this.getItemList(listGridPropsVisivel, i));
            }

            for (int i = 0; i < listGridPropsInvisivel.Count; i++)
            {
                listGridProps.Add(listGridPropsInvisivel[i]);
            }

            // Previne erro de FK
            for (int i = 0; i < listGridProps.Count; i++)
            {
                listGridProps[i].Sequencia = i;
            }
        }

        public GridProps getItemList(List<GridProps> lista, int sequencia)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Sequencia == sequencia)
                {
                    return lista[i];
                }
            }

            return null;
        }

        public void SalvarFiltros()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    this.SalvarFiltro(checkedListBox1.Items[i].ToString());
                }
            }
        }

        public Boolean SalvarFiltro(String nomeFiltro)
        {
            if (nomeFiltro.Equals(PadraoBotaoTodos))
            {
                nomeFiltro = "";
            }

            if (this.LimparConfiguracaoAnterior(nomeFiltro) >= 0)
            {
                for (int i = 0; i < listGridProps.Count; i++)
                {
                    /*
                     * ALTERADO O MÉTODO DEVIDO LENTIDÃO
                     * 
                    AppLib.ORM.Jit ZVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), "ZVISAOUSUARIO");
                    ZVISAOUSUARIO.Set("GRID", NomeGrid);
                    ZVISAOUSUARIO.Set("USUARIO", Usuario);
                    ZVISAOUSUARIO.Set("FILTRO", nomeFiltro);
                    ZVISAOUSUARIO.Set("SEQUENCIA", listGridProps[i].Sequencia);
                    ZVISAOUSUARIO.Set("COLUNA", listGridProps[i].Coluna);

                    if (listGridProps[i].Agrupar)
                    {
                        ZVISAOUSUARIO.Set("AGRUPAR", 1);
                    }
                    else
                    {
                        ZVISAOUSUARIO.Set("AGRUPAR", 0);
                    }

                    if (listGridProps[i].Visivel)
                    {
                        ZVISAOUSUARIO.Set("VISIVEL", 1);
                    }
                    else
                    {
                        ZVISAOUSUARIO.Set("VISIVEL", 0);
                    }

                    ZVISAOUSUARIO.Set("LARGURA", listGridProps[i].Largura);
                    ZVISAOUSUARIO.Set("ALINHAMENTO", listGridProps[i].Alinhamento.ToString().Substring(0, 1));
                    ZVISAOUSUARIO.Set("FORMATO", listGridProps[i].Formato);

                    if (ZVISAOUSUARIO.Insert() > 0)
                    {
                        // OK
                    }
                    else
                    {
                        AppLib.Windows.FormMessageDefault.ShowError("Erro ao limpar configurações grid " + nomeFiltro + " usuario " + Usuario + " filtro " + nomeFiltro + " sequencia " + i);
                        return false;
                    }
                    */

                    string sSql = @"INSERT INTO ZVISAOUSUARIO (GRID,
USUARIO,
FILTRO,
SEQUENCIA,
COLUNA,
AGRUPAR,
VISIVEL,
LARGURA,
ALINHAMENTO,
FORMATO) VALUES(?,?,?,?,?,?,?,?,?,?)";

                    int retorno = AppLib.Context.poolConnection.Get(Conexao).ExecTransaction(sSql, NomeGrid, Usuario, nomeFiltro, listGridProps[i].Sequencia, listGridProps[i].Coluna,
                        ((listGridProps[i].Agrupar) ? 1 : 0),
                        ((listGridProps[i].Visivel) ? 1 : 0),
                        listGridProps[i].Largura,
                        listGridProps[i].Alinhamento.ToString().Substring(0, 1),
                        listGridProps[i].Formato);

                    if (retorno > 0)
                    {
                        // OK
                    }
                    else
                    {
                        AppLib.Windows.FormMessageDefault.ShowError("Erro ao limpar configurações grid " + nomeFiltro + " usuario " + Usuario + " filtro " + nomeFiltro + " sequencia " + i);
                        return false;
                    }
                }

                return true;
            }
            else
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao limpar configurações grid "+ nomeFiltro +" usuario "+ Usuario +" filtro" + nomeFiltro);
                return false;
            }
        }

        public int LimparConfiguracaoAnterior(String nomeFiltro)
        {
            return AppLib.Context.poolConnection.Get(Conexao).ExecTransaction("DELETE ZVISAOUSUARIO WHERE GRID = ? AND USUARIO = ? AND FILTRO = ?", new Object[] { NomeGrid, Usuario, nomeFiltro });
        }

    }
}
