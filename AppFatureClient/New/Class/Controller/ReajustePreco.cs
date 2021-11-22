using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFatureClient.New.Class.Controller
{
    public class ReajustePreco
    {
        #region Variáveis

        public Models.ReajustePreco reajustePreco = new Models.ReajustePreco();
        public List<Models.ReajustePreco> listReajustePreco = new List<Models.ReajustePreco>();

        private DataTable dtReajuste = new DataTable();

        private Nullable<DateTime> dataNula;

        #endregion

        #region Construtor

        public ReajustePreco() { }

        #endregion

        #region Métodos

        public DataTable CriarSchemaTabelaReajuste()
        {
            dtReajuste.Columns.Add("IDPRD", typeof(int));
            dtReajuste.Columns.Add("CODIGOAUXILIAR", typeof(string));
            dtReajuste.Columns.Add("NOMEFANTASIA", typeof(string));
            dtReajuste.Columns.Add("PRECOFIXO", typeof(decimal));
            dtReajuste.Columns.Add("PRECOACABADO", typeof(decimal));
            dtReajuste.Columns.Add("PRECOREVENDA", typeof(decimal));
            dtReajuste.Columns.Add("VALORREAJUSTADO", typeof(decimal));

            // Colunas referentes ao histórico do Reajuste de Preço
            dtReajuste.Columns.Add("PRECO1", typeof(decimal));
            dtReajuste.Columns.Add("DATAALTERACAO1", typeof(DateTime));
            dtReajuste.Columns.Add("USUARIOALTERACAO1", typeof(string));

            dtReajuste.Columns.Add("PRECO2", typeof(decimal));
            dtReajuste.Columns.Add("DATAALTERACAO2", typeof(DateTime));
            dtReajuste.Columns.Add("USUARIOALTERACAO2", typeof(string));

            dtReajuste.Columns.Add("PRECO3", typeof(decimal));
            dtReajuste.Columns.Add("DATAALTERACAO3", typeof(DateTime));
            dtReajuste.Columns.Add("USUARIOALTERACAO3", typeof(string));

            return dtReajuste;
        }

        public List<Models.ReajustePreco> PreencherPropriedades(int IDPRD)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT TOP 3 * FROM ZTPRODUTOREAJUSTEPRECO WHERE CODCOLIGADA = ? AND IDPRD = ?", new object[] { AppLib.Context.Empresa, IDPRD });

            List<Models.ReajustePreco> reajustes = new List<Models.ReajustePreco>();
            reajustePreco = new Models.ReajustePreco();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        reajustePreco = new Models.ReajustePreco();

                        reajustePreco.CodColigada = dt.Rows[i]["CODCOLIGADA"] == DBNull.Value ? 1 : Convert.ToInt32(dt.Rows[i]["CODCOLIGADA"]);
                        reajustePreco.IDPrd = dt.Rows[i]["IDPRD"] == DBNull.Value ? IDPRD : Convert.ToInt32(dt.Rows[i]["IDPRD"]);
                        reajustePreco.Preco = dt.Rows[i]["PRECO"] == DBNull.Value ? 0 : Convert.ToDecimal(dt.Rows[i]["PRECO"]);
                        reajustePreco.DataAlteracao = dt.Rows[i]["DATAALTERACAO"] == DBNull.Value ? dataNula : Convert.ToDateTime(dt.Rows[i]["DATAALTERACAO"]);
                        reajustePreco.UsuarioAlteracao = dt.Rows[i]["USUARIOALTERACAO"] == DBNull.Value ? AppLib.Context.Usuario : dt.Rows[i]["USUARIOALTERACAO"].ToString();

                        reajustes.Add(reajustePreco);
                    }
                }
                else
                {
                    reajustePreco.CodColigada = 1;
                    reajustePreco.IDPrd = IDPRD;
                    reajustePreco.Preco = 0;
                    reajustePreco.DataAlteracao = dataNula;
                    reajustePreco.UsuarioAlteracao = AppLib.Context.Usuario;

                    reajustes.Add(reajustePreco);
                }
            }

            return reajustes;
        }

        public void ReajustarPreco(Models.ReajustePreco reajuste)
        {
            if (reajuste != null)
            {
                AtualizarPreco(reajuste);
            }
        }

        private void AtualizarPreco(Models.ReajustePreco reajuste)
        {
            try
            {
                AtualizarReajustePreco(reajuste);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AtualizarReajustePreco(Models.ReajustePreco reajuste)
        {
            try
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO ZTPRODUTOREAJUSTEPRECO VALUES (?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, reajuste.IDPrd, reajuste.Preco, DateTime.Now, AppLib.Context.Usuario });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
