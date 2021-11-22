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
    public partial class FormSalvarDisposicaoCubo : Form
    {
        public String NomeGrid { get; set; }
        public String Usuario { get; set; }
        public String NomeFiltro { get; set; }

        public DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1 { get; set; }

        private String PadraoBotaoTodos = "[BOTÃO TODOS]";

        private List<GridCuboProps> listGridProps = new List<GridCuboProps>();
        private List<GridCuboProps> listGridOculta = new List<GridCuboProps>();

        public FormSalvarDisposicaoCubo()
        {
            InitializeComponent();
        }

        private void FormSalvarDisposicaoCubo_Load(object sender, EventArgs e)
        {
            String consulta = @"
SELECT NOME
FROM ZFILTRO
WHERE GRID = ?
  AND (USUARIO IS NULL OR USUARIO = ?)";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(consulta, new Object[] { NomeGrid, Usuario });

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

        public void IncluiItemLista(DevExpress.XtraPivotGrid.PivotGridField linha, Area area)
        {
            GridCuboProps props = new GridCuboProps();
            props.Sequencia = listGridProps.Count + 1;
            props.Coluna = linha.FieldName;
            props.Area = area;
            props.Largura = linha.Width;

            #region ALINHAMENTO

            props.Alinhamento = Alinhamento.Esquerda;

            if (linha.Appearance.Cell.HAlignment == DevExpress.Utils.HorzAlignment.Far)
            {
                props.Alinhamento = Alinhamento.Direita;
            }

            if (linha.Appearance.Cell.HAlignment == DevExpress.Utils.HorzAlignment.Center)
            {
                props.Alinhamento = Alinhamento.Centro;
            }

            #endregion

            #region FORMATAÇÃO

            props.Formato = Formato.Nenhum;

            if (linha.ValueFormat.FormatString.Equals("dd/MM/yyyy"))
            {
                props.Formato = Formato.Data;
            }

            if (linha.ValueFormat.FormatString.Equals("dd/MM/yyyy HH:mm"))
            {
                props.Formato = Formato.DataHora;
            }

            if (linha.ValueFormat.FormatString.Equals("dd/MM/yyyy HH:mm:ss"))
            {
                props.Formato = Formato.DataHoraSegundos;
            }

            if (linha.ValueFormat.FormatString.Equals("dd/MM/yyyy HH:mm:ss.fff"))
            {
                props.Formato = Formato.DataHoraMilisegundos;
            }

            if (linha.ValueFormat.FormatString.Equals("f2"))
            {
                props.Formato = Formato.Decimal2;
            }

            if (linha.ValueFormat.FormatString.Equals("f4"))
            {
                props.Formato = Formato.Decimal4;
            }

            #endregion

            if (linha.Visible)
            {
                listGridProps.Add(props);
            }
            else
            {
                props.Sequencia = 0;
                listGridOculta.Add(props);
            }
        }

        public void MontarListaConfiguracoes()
        {
            listGridProps.Clear();
            listGridOculta.Clear();

            #region Tratamento para as áreas do cubo

            List<DevExpress.XtraPivotGrid.PivotGridField> linhas = pivotGridControl1.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.RowArea);
            for (int i = 0; i < linhas.Count; i++)
            {
                IncluiItemLista(linhas[i], Area.Linha);
            }

            List<DevExpress.XtraPivotGrid.PivotGridField> colunas = pivotGridControl1.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.ColumnArea);
            for (int i = 0; i < colunas.Count; i++)
            {
                IncluiItemLista(colunas[i], Area.Coluna);
            }

            List<DevExpress.XtraPivotGrid.PivotGridField> dados = pivotGridControl1.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.DataArea);
            for (int i = 0; i < dados.Count; i++)
            {
                IncluiItemLista(dados[i], Area.Dados);
            }

            List<DevExpress.XtraPivotGrid.PivotGridField> filtro = pivotGridControl1.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.FilterArea);
            for (int i = 0; i < filtro.Count; i++)
            {
                IncluiItemLista(filtro[i], Area.Filtro);
            }

            for (int i = 0; i < pivotGridControl1.Fields.Count; i++)
			{
                if ( ! pivotGridControl1.Fields[i].Visible)
                {
                    IncluiItemLista(pivotGridControl1.Fields[i], Area.Oculta);
                }
			}
            
            #endregion

            #region Tratametno para área oculta

            for (int i = 0; i < listGridOculta.Count; i++)
            {
                GridCuboProps propTemp = GridCuboProps.Clone(listGridOculta[i]);
                propTemp.Sequencia = listGridProps.Count + 1;
                propTemp.Area = Area.Oculta;

                listGridProps.Add(propTemp);
            }

            #endregion
        }

        public GridCuboProps getItemList(List<GridCuboProps> lista, int sequencia)
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
                    AppLib.ORM.Jit ZVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get(), "ZCUBOVISAO");
                    ZVISAOUSUARIO.Set("GRID", NomeGrid);
                    ZVISAOUSUARIO.Set("USUARIO", Usuario);
                    ZVISAOUSUARIO.Set("FILTRO", nomeFiltro);
                    ZVISAOUSUARIO.Set("SEQUENCIA", listGridProps[i].Sequencia);
                    ZVISAOUSUARIO.Set("COLUNA", listGridProps[i].Coluna);
                    ZVISAOUSUARIO.Set("AREA", listGridProps[i].Area.ToString().Substring(0, 1));
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
                }

                return true;
            }
            else
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao limpar configurações grid " + nomeFiltro + " usuario " + Usuario + " filtro" + nomeFiltro);
                return false;
            }
        }

        public int LimparConfiguracaoAnterior(String nomeFiltro)
        {
            return AppLib.Context.poolConnection.Get().ExecTransaction("DELETE ZCUBOVISAO WHERE GRID = ? AND USUARIO = ? AND FILTRO = ?", new Object[] { NomeGrid, Usuario, nomeFiltro });
        }

    }
}
