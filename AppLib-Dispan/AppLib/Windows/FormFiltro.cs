using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppLib.Windows
{
    public partial class FormFiltro : DevExpress.XtraEditors.XtraForm
    {
        public String NomeGrid { get; set; }
        public String Conexao { get; set; }
        public List<String> Colunas { get; set; }
        public System.Data.DataTable dtSchema { get; set; }
        public Boolean Selecionou = false;
        public String Filtro { get; set; }

        public FormFiltro()
        {
            InitializeComponent();

            // seta depois de carregar o componente
            grid1.ModoFiltro = true;
            grid1.FormPai = this;
        }

        private void FormFiltro_Load(object sender, EventArgs e)
        {
            grid1.toolStripButtonPESQUISAR.Visible = false;
            grid1.toolStripButtonAGRUPAR.Visible = false;
            grid1.toolStripButtonFILTROS.Visible = false;
            grid1.toolStripButtonANEXOS.Visible = false;

            grid1.GetProcessos().Add("Copiar filtro", null, CopiarFiltro);
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormFiltroEdit f = new FormFiltroEdit();
            f.Conexao = this.Conexao;
            f.Colunas = this.Colunas;
            f.NomeGrid = this.NomeGrid;
            f.ShowDialog();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            try
            {
                FormFiltroEdit f = new FormFiltroEdit();
                f.Conexao = this.Conexao;
                f.Colunas = this.Colunas;
                f.NomeGrid = this.NomeGrid;
                f.NomeFiltro = grid1.GetDataRow()["NOME"].ToString();
                f.textEditNOME.Text = f.NomeFiltro;

                // CARREGAR OPÇÕES BÁSICAS DO FILTRO
                String SQLFiltro = "SELECT * FROM ZFILTRO WHERE GRID = ? AND NOME = ?";
                SQLFiltro = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(SQLFiltro, new Object[] { NomeGrid, f.textEditNOME.Text });
                System.Data.DataTable dtFiltro = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(SQLFiltro, new Object[] { });

                if (dtFiltro.Rows.Count > 0)
                {
                    String usuario = dtFiltro.Rows[0]["USUARIO"].ToString();
                    if (usuario.Equals(""))
                    {
                        f.checkEditCOMPARTILHAR.Checked = true;
                    }
                    else
                    {
                        f.checkEditCOMPARTILHAR.Checked = false;
                    }

                    int nativo = int.Parse(dtFiltro.Rows[0]["NATIVO"].ToString());

                    if (nativo == 0)
                    {
                        f.ShowDialog();
                    }
                    else
                    {
                        AppLib.Windows.FormMessageDefault.ShowError("Este filtro é nativo do sistema e não permite alterações.");
                    }
                }
            }
            catch (Exception) { }
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            String nomeFiltro = grid1.GetDataRow()["NOME"].ToString();
            String Comando = "";

            try
            {
                Comando = "SELECT NATIVO FROM ZFILTRO WHERE GRID = ? AND NOME = ?";
                int NATIVO = int.Parse(AppLib.Context.poolConnection.Get().ExecGetField(0, Comando, new Object[] { NomeGrid, nomeFiltro }).ToString());

                if (NATIVO == 0)
                {
                    Comando = "DELETE ZFILTROCONDICAO WHERE GRID = ? AND NOME = ?";
                    Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { NomeGrid, nomeFiltro });
                    int contComando = AppLib.Context.poolConnection.Get(Conexao).ExecTransaction(Comando, new Object[] { });

                    if (contComando >= 0)
                    {
                        AppLib.ORM.Jit x = new ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), "ZFILTRO");
                        x.Set("GRID", NomeGrid);
                        x.Set("NOME", nomeFiltro);
                        int contDelete = x.Delete();

                        if (contDelete > 0)
                        {
                            this.grid1.Atualizar(false, false, null);
                        }
                        else
                        {
                            MessageBox.Show("Erro ao excluir filtro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    AppLib.Windows.FormMessageDefault.ShowError("Este filtro é nativo do sistema e não permite exclusão.");
                }                
            }
            catch (Exception ex)
            {
                new FormExceptionSQL().Mostrar("Erro ao excluir condições do filtro.", Comando, ex);
            }
        }

        public String getFiltro(String Conexao, String nomeGrid, String nomeFiltro)
        {
            String result = "";
            
            String consulta = @"SELECT LOGICO, CAMPO, COMPARADOR, VALOR FROM ZFILTROCONDICAO WHERE GRID = ? AND NOME = ? ORDER BY SEQUENCIA";
            consulta = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(consulta, new Object[] { nomeGrid, nomeFiltro });
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get(Conexao).ExecQuery(consulta, new Object[] { });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String logico = dt.Rows[i]["LOGICO"].ToString();
                String campo = dt.Rows[i]["CAMPO"].ToString();

                String tipo = "";
                int variavel = 0;

                for (int n = 0; n < dtSchema.Rows.Count; n++)
                {
                    if (dtSchema.Rows[n]["ColumnName"].ToString().Equals(campo))
                    {
                        if (AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.SqlClient ||
                            AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.SqlLocalDb ||
                            AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.SqlWebService)
                        {
                            tipo = dtSchema.Rows[n]["DataTypeName"].ToString();
                        }

                        if(AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.OracleClient ||
                            AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.OracleWebService)
                        {
                            tipo = dtSchema.Rows[n]["DataType"].ToString();

                        }
                    }
                }

                String comparador = dt.Rows[i]["COMPARADOR"].ToString();
                String valor = dt.Rows[i]["VALOR"].ToString();

                // LOGICO
                if (logico.Equals("E"))
                    result += "\r\n  AND ";

                if (logico.Equals("Ou"))
                    result += " OR ";

                // VALOR 
                if (valor.Length > 0)
                {
                    // PROMPT
                    if (valor.Substring(0, 1).Equals("["))
                    {
                        String pergunta = valor.Replace("[", "").Replace("]", "");
                        FormMessagePrompt fmp = new FormMessagePrompt();
                        valor = fmp.Mostrar(pergunta, "");

                        if (fmp.confirmacao == Global.Types.Confirmacao.Cancelar)
                        {
                            valor = "";
                        }
                    }

                    #region VARIAVEIS

                    if (valor.ToUpper().Equals("$ONTEM"))
                    {
                        valor = new AppLib.Data.Variaveis(Conexao).getOntem();
                        variavel++;
                    }

                    if (valor.ToUpper().Equals("$HOJE"))
                    {
                        valor = new AppLib.Data.Variaveis(Conexao).getHoje();
                        variavel++;
                    }

                    if (valor.ToUpper().Equals("$AMANHA"))
                    {
                        valor = new AppLib.Data.Variaveis(Conexao).getAmanha();
                        variavel++;
                    }

                    if (valor.ToUpper().Equals("$MES"))
                    {
                        valor = new AppLib.Data.Variaveis(Conexao).getMes();
                        variavel++;
                    }

                    // Mês Dinâmico
                    if (valor.ToUpper().Contains("$MES-"))
                    {
                        int mes = Convert.ToInt32(valor.Substring(5, 1));

                        valor = new AppLib.Data.Variaveis(Conexao).getMesByUser(mes);
                        variavel++;
                    }

                    if (valor.ToUpper().Equals("$ANO"))
                    {
                        valor = new AppLib.Data.Variaveis(Conexao).getAno();
                        variavel++;
                    }

                    if (valor.ToUpper().Equals("$USUARIO"))
                    {
                        valor = new AppLib.Data.Variaveis(Conexao).getUsuario();
                        variavel++;
                    }

                    if (valor.ToUpper().Equals("$EMPRESA"))
                    {
                        valor = new AppLib.Data.Variaveis(Conexao).getEmpresa().ToString();
                        variavel++;
                    }
                    
                    #endregion
                }

                // VALOR (TIPO)
                if (comparador.ToUpper().Contains("PERTENCE") == false)
                {
                    if (tipo.ToUpper().Equals("INT"))
                    {
                        int temp = 0;

                        try
                        {
                            temp = int.Parse(valor);
                        }
                        catch { }

                        valor = temp.ToString();
                    }

                    if (tipo.ToUpper().Equals("FLOAT"))
                    {
                        float temp = 0;

                        try
                        {
                            temp = float.Parse(valor);
                        }
                        catch { }

                        valor = temp.ToString();
                    }

                    if (tipo.ToUpper().Equals("DECIMAL"))
                    {
                        decimal temp = 0;

                        try
                        {
                            temp = decimal.Parse(valor);
                        }
                        catch { }

                        valor = temp.ToString();
                    }

                    if (tipo.ToUpper().Equals("CHAR"))
                    {
                        valor = "'" + valor + "'";
                    }

                    if (tipo.ToUpper().Equals("VARCHAR"))
                    {
                        valor = "'" + valor + "'";
                    }

                    if (tipo.ToUpper().Equals("DATETIME") || tipo.ToUpper().Equals("SYSTEM.DATETIME"))
                    {
                        if (variavel == 0)
                        {
                            DateTime dataHora = DateTime.Now;
                            String formato = "dd/MM/yyyy HH:mm:ss.fff";
                            formato = formato.Substring(0, valor.Length);

                            try
                            {
                                // System.Globalization.CultureInfo provider = System.Globalization.CultureInfo;
                                dataHora = DateTime.ParseExact(valor, formato, null);
                            }
                            catch { }

                            if ((AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.SqlClient) ||
                                (AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.SqlWebService) ||
                                (AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.SqlLocalDb))
                            {
                                campo = "CONVERT(DATETIME, SUBSTRING(CONVERT(VARCHAR, " + campo + ", 121), 1, "+ valor.Length +"))";
                                valor = AppLib.Util.Format.dataSql(dataHora);
                            }

                            if ((AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.OracleClient) ||
                                (AppLib.Context.poolConnection.Get(Conexao).Database == Global.Types.Database.OracleWebService))
                            {
                                //campo = "TO_DATE(SUBSTR(TO_TIMESTAMP(" + campo + ", 'YYYY-MM-DD HH24:MI:SS.FF'), 1, " + valor.Length + "), 'YYYY-MM-DD HH24:MI:SS.FF')";
                                //valor = AppLib.Util.Format.dataOracle(dataHora);

                                campo = "TO_DATE(SUBSTR(TO_CHAR(" + campo + ", 'YYYY-MM-DD HH24:MI:SS') || '.000', 1, " + valor.Length + "), SUBSTR('YYYY-MM-DD HH24:MI:SS.FF', 1, " + valor.Length + "))";
                                valor = AppLib.Util.Format.dataOracle(dataHora);
                            }
                        }                        
                    }
                }


                // CAMPO (CONCATENAR)
                result += campo + " ";


                // COMPARADOR (CONCATENAR)
                if (comparador.Equals("Igual"))
                    result += "= ";

                if (comparador.Equals("Diferente"))
                    result += "<> ";

                if (comparador.Equals("Maior"))
                    result += "> ";

                if (comparador.Equals("Maior ou igual"))
                    result += ">= ";

                if (comparador.Equals("Menor"))
                    result += "< ";

                if (comparador.Equals("Menor ou igual"))
                    result += "<= ";

                if (comparador.Equals("Contém"))
                    result += "LIKE ";

                if (comparador.Equals("Não contém"))
                    result += "NOT LIKE ";

                if (comparador.Equals("É nulo"))
                    result += "IS NULL ";

                if (comparador.Equals("Não é nulo"))
                    result += "IS NOT NULL ";

                if (comparador.Equals("Pertence"))
                    result += "IN ";

                if (comparador.Equals("Não pertence"))
                    result += "NOT IN ";


                // VALOR (CONCATENAR)
                if (comparador.Equals("Igual"))
                    result += valor;

                if (comparador.Equals("Diferente"))
                    result += valor;

                if (comparador.Equals("Maior"))
                    result += valor;

                if (comparador.Equals("Maior ou igual"))
                    result += valor;

                if (comparador.Equals("Menor"))
                    result += valor;

                if (comparador.Equals("Menor ou igual"))
                    result += valor;

                if (comparador.Equals("Contém"))
                    result += valor;

                if (comparador.Equals("Não contém"))
                    result += valor;

                if (comparador.Equals("É nulo"))
                    result += "\r\n";

                if (comparador.Equals("Não é nulo"))
                    result += "\r\n";

                if (comparador.Equals("Pertence"))
                    result += "(" + valor + ")";

                if (comparador.Equals("Não pertence"))
                    result += "(" + valor + ")";
            }

            return result;
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { NomeGrid };
        }

        private void CopiarFiltro(object sender, EventArgs e)
        {
            String Comando = "";

            try
            {
                AppLib.ORM.Jit x = new ORM.Jit(AppLib.Context.poolConnection.Get(Conexao), "ZFILTRO");
                x.Set("GRID", NomeGrid);
                String nomeAtual = grid1.GetDataRow()["NOME"].ToString();
                x.Set("NOME", nomeAtual);
                x.Select();

                String novoNome = new FormMessagePrompt().Mostrar("Nome do novo filtro", null);
                if (novoNome != null)
                {
                    x.Set("NOME", novoNome);
                    if (x.Save() > 0)
                    {
                        Comando = @"INSERT INTO ZFILTROCONDICAO (GRID, NOME, SEQUENCIA, LOGICO, CAMPO, COMPARADOR, VALOR)
                                    SELECT GRID, ? NOME, SEQUENCIA, LOGICO, CAMPO, COMPARADOR, VALOR
                                    FROM ZFILTROCONDICAO
                                    WHERE GRID = ?
                                      AND NOME = ?";

                        Comando = AppLib.Context.poolConnection.Get(Conexao).ParseCommand(Comando, new Object[] { novoNome, NomeGrid, nomeAtual });
                        int contComando = AppLib.Context.poolConnection.Get(Conexao).ExecTransaction(Comando, new Object[]{ });

                        if ( contComando >= 0 )
                        {
                            this.grid1.Atualizar(false, false, null);
                        }
                        else
                        {
                            MessageBox.Show("Erro ao copiar condições do filtro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new FormExceptionSQL().Mostrar("Erro ao copiar condições do filtro", Comando, ex);
            }
        }

        private void FormFiltro_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemoryManager.ReleaseUnusedMemory(false);
        }
                      
    }
}