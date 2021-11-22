using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using RM.Lib;
using RM.Lib.Server;
using System.Windows.Forms;

namespace AppFatureClient.Classes
{
    static class RateioNFOP
    {
        public static void Rateia(String NUMNF)
        {
            try
            {
                #region Query NF
                MetodosSQL.CS = MetodosSQL.CS + ";Connection Timeout=40000";
                string sql = String.Format(@"SELECT TMOV.IDMOV, TMOV.NUMEROMOV, TITMMOV.QUANTIDADE, TMOV.DATAEMISSAO, TITMMOV.IDPRD FROM TMOV 

                                        inner JOIN TITMMOV
                                        on TITMMOV.CODCOLIGADA = TMOV.CODCOLIGADA
                                        and TITMMOV.IDMOV = TMOV.IDMOV

                                        where CODTMV = '1.2.65' 
                                        and CODCFO = '00918' 
                                        and TITMMOV.IDPRD in ('39996','41523','40002','42289', '39999','45449')
                                        and TMOV.IDMOV not in (select IDMOV from ZOPRATEADAS)
                                        and NUMEROMOV = '{0}'", NUMNF);


                decimal qtdNF = (decimal)MetodosSQL.GetObject(sql, "QUANTIDADE");
                int idMOV = (int)MetodosSQL.GetObject(sql, "IDMOV");
                string numeroMov = (string)MetodosSQL.GetObject(sql, "NUMEROMOV");
                DateTime dataNF = (DateTime)MetodosSQL.GetObject(sql, "DATAEMISSAO");

                int IDPRODUTO = 0;

                if ((int)MetodosSQL.GetObject(sql, "IDPRD") == 41523)
                {
                    IDPRODUTO = 81725;
                }
                else if ((int)MetodosSQL.GetObject(sql, "IDPRD") == 40002)
                {
                    IDPRODUTO = 82181;
                }
                else if ((int)MetodosSQL.GetObject(sql, "IDPRD") == 42289)
                {
                    IDPRODUTO = 81725;
                }
                else if ((int)MetodosSQL.GetObject(sql, "IDPRD") == 39996)
                {
                    IDPRODUTO = 83805;
                }
                else if ((int)MetodosSQL.GetObject(sql, "IDPRD") == 39999)
                {
                    IDPRODUTO = 83950;
                }
                else if ((int)MetodosSQL.GetObject(sql, "IDPRD") == 45449)
                {
                    IDPRODUTO = 83916;
                }
                else
                {
                    throw new Exception("Não foi possivel definir um IDPRODUTO!");
                }
                #endregion

                #region Query OP
                sql = String.Format(@"select * from (
                                        select KAO.CODORDEM, 
                                               KAO.IDPRODUTO, 
                                               KAO.CODESTRUTURA,
                                               KAO.IDATIVIDADE,
                                               (select top 1 QUANTIDADE from KATVORDEMMP where CODORDEM = KAO.CODORDEM order by QUANTIDADE desc) as 'QUANTIDADE',
                                               (select top 1 QUANTIDADE*0.15 from KATVORDEMMP where CODORDEM = KAO.CODORDEM order by QUANTIDADE desc) as 'QTDFINAL',
                                               (select max(DATAHORA) as 'DATAHORA' from KLOGPRODUCAO where CODORDEM = KAO.CODORDEM group by CODCOLIGADA, CODORDEM) as 'DATAAPONTAMENTO',
                                               (select count(1) from KATVORDEMMP where IDPRODUTO in (81725,82181,83805,83950,83916) and CODORDEM = KAO.CODORDEM) as 'RATEADA'
                                        from (select CODCOLIGADA, CODORDEM, max(IDPRODUTO) as 'IDPRODUTO', CODESTRUTURA, IDATIVIDADE from KATVORDEMMP where EFETIVADO = 1
                                                 group by CODCOLIGADA, CODORDEM, CODESTRUTURA, IDATIVIDADE) KAO

                                        inner join (select CODCOLIGADA, CODORDEM, max(DATAHORA) as 'DATAHORA' from KLOGPRODUCAO group by CODCOLIGADA, CODORDEM) KLP
                                        on KLP.CODCOLIGADA = KAO.CODCOLIGADA
                                        and KLP.CODORDEM = KAO.CODORDEM

                                        inner join (select CODCOLPRD, IDPRD, CODIGOPRD, NOMEFANTASIA from TPRODUTO) TP
                                        on TP.CODCOLPRD = KAO.CODCOLIGADA
                                        and TP.IDPRD = KAO.IDPRODUTO

                                        where KAO.CODCOLIGADA = 1 
                                        and TP.CODIGOPRD LIKE '01.%' AND TP.NOMEFANTASIA LIKE 'CHAPA%'
                                        and CONVERT(DATE, KLP.DATAHORA ,103) >= CONVERT(datetime, '{0}', 103)
                                        and KAO.CODORDEM not in (select CODORDEM from ZOPRATEADAS)) Y

                                        where Y.RATEADA = 0
                                        order by Y.CODORDEM", MetodosSQL.GetObject(sql, "DATAEMISSAO"));

                DataTable dt = MetodosSQL.GetDT(sql);
                #endregion

                #region Inserts
                AppInterop.Util util = new AppInterop.Util();
                AppLib.Util.Alias alias = util.GetAlias(New.Class.EnviromentHelper.IndexAlias);
                string conn = string.Concat("Server=", alias.ServerName, ";Database=", alias.DbName, ";User Id=", alias.UserName, ";Password=", alias.Password, ";");
                RM.Lib.Data.DbServices DBS = new RM.Lib.Data.DbServices(RM.Lib.Data.ProviderType.SqlClient, conn);

                RMSAutoInc autoInc = new RMSAutoInc(DBS);

                #region KATVORDEMMP
                sql = @"INSERT INTO KATVORDEMMP VALUES (/*IDATVORDEMMATERIAPRIMA*/ {0},
                                /*CODCOLIGADA*/ 1, 
                                /*CODORDEM*/ '{1}', 
                                /*CODESTRUTURA*/ '{2}', 
                                /*CODMODELO*/ 'Base', 
                                /*IDATIVIDADE*/ {7}, 
                                /*DATAAPONTAMENTO*/ convert(datetime, '{3}', 103), 
                                /*IDPRODUTO*/ {4}, 
                                /*NSEQITMGRD*/ null, 
                                /*QUANTIDADE*/ {5}, 
                                /*EFETIVADO*/ 1, 
                                /*IDLOTE*/ null, 
                                /*CODFILIAL*/ 1, 
                                /*QTDUNITARIA*/ 0.00, 
                                /*QTFIXA*/ 0, 
                                /*ESTOQUE*/ 0.00,
                                /*RECCREATEDBY*/ '{6}',
                                /*RECCREATEDON*/ convert(datetime, '{8}', 103),
                                /*RECMODIFIEDBY*/ '{6}',
                                /*RECMODIFIEDON*/ convert(datetime, '{8}', 103),
                                /*ESTOQUETERCEIRO*/ null,
                                /*CODCOLCFO*/ null, 
                                /*CODCFO*/ null,
                                /*CODFILIALMP*/ null,
                                /*CODLOCAL*/ null)";
                #endregion

                #region KESTQDETALHEQTDE
                string sqlKESTQDETALHEQTDE = @"INSERT INTO KESTQDETALHEQTDE(CODCOLIGADA, CODFILIAL, CODPOSTO, IDPRODUTO, IDAUTOINC, 
                                                CODESTRUTURA, CODMODELO, CODORDEM, QUANTIDADE, USAGRADE, NSEQITMGRD, IDATVORDEM, CUSTO,
                                                RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON)                               
                                         VALUES(/*CODCOLIGADA*/1, 
                                                /*CODFILIAL*/ 1, 
                                                /*CODPOSTO*/ '01', 
                                                /*IDPRODUTO*/ {0}, 
                                                /*IDAUTOINC*/ {1}, 
                                                /*CODESTRUTURA*/ '{2}', 
                                                /*CODMODELO*/ 'Base', 
                                                /*CODORDEM*/ '{3}', 
                                                /*QUANTIDADE*/ -{4}, 
                                                /*USAGRADE*/ null, 
                                                /*NSEQITMGRD*/ null, 
                                                /*IDATVORDEM*/ {5}, 
                                                /*CUSTO*/ 0,
                                                /*RECCREATEDBY*/ '{6}', 
                                                /*RECCREATEDON*/ convert(datetime, '{7}', 103), 
                                                /*RECMODIFIEDBY*/ '{6}', 
                                                /*RECMODIFIEDON*/ convert(datetime, '{7}', 103))";
                #endregion

                #region KESTQDETALHE
                string sqlKESTQDETALHE = @"INSERT INTO KESTQDETALHE(CODCOLIGADA, CODFILIAL, CODPOSTO, IDPRODUTO, IDAUTOINC, 
                                                IDATVORDEM, CODESTRUTURA, CODMODELO, CODORDEM, QUANTIDADE, QTDEORIGINAL,
                                                USAGRADE, NSEQITMGRD, DATANECESSIDADE, RECCREATEDBY, RECCREATEDON, RECMODIFIEDBY, RECMODIFIEDON)                               
                                         VALUES(/*CODCOLIGADA*/ 1, 
                                                /*CODFILIAL*/ 1, 
                                                /*CODPOSTO*/ '01', 
                                                /*IDPRODUTO*/ {0}, 
                                                /*IDAUTOINC*/ {1}, 
                                                /*IDATVORDEM*/ {2}, 
                                                /*CODESTRUTURA*/ '{3}', 
                                                /*CODMODELO*/ 'Base', 
                                                /*CODORDEM*/ '{4}', 
                                                /*QUANTIDADE*/ +{5}, 
                                                /*QTDEORIGINAL*/ {6}, 
                                                /*USAGRADE*/ null, 
                                                /*NSEQITMGRD*/ null, 
                                                /*DATANECESSIDADE*/ convert(datetime, '{7}', 103),
                                                /*RECCREATEDBY*/ '{8}', 
                                                /*RECCREATEDON*/ convert(datetime, '{7}', 103), 
                                                /*RECMODIFIEDBY*/ '{8}',
                                                /*RECMODIFIEDON*/ convert(datetime, '{7}', 103))";
                #endregion

                #endregion

                List<String> insert = new List<string>();

                string insertLog = @"insert into ZOPRATEADAS values (1, 1, '{0}', {1}, '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9})";

                decimal quantidadeSomada = 0;
                bool saiLoop = false;
                bool executouUltimo = false;
                string todosInser = String.Empty;
                string todasSomas = String.Empty;

                foreach (DataRow dr in dt.Rows)
                {
                    
                    decimal qtdFinal = (decimal)dr["QTDFINAL"];

                    if ((quantidadeSomada + qtdFinal) > qtdNF)
                    {
                        qtdFinal = (qtdNF - quantidadeSomada);
                        quantidadeSomada += qtdFinal;
                        saiLoop = true;
                    }
                    else
                    {
                        quantidadeSomada += qtdFinal;
                    }

                    decimal porcentagem = (qtdFinal * 100) / (decimal)dr["QUANTIDADE"];

                    if (!executouUltimo)
                    {
                        int IDKAO = autoInc.GetNewValue("K", 0, "IDATVORDEMMATERIAPRIMA", null, null);
                        int IDKED = autoInc.GetNewValue("K", 0, "IDSEQKESTQDETALHEQTDE", null, null);
                        int IDKEDQ = autoInc.GetNewValue("K", 0, "IDSEQKESTQDETALHEQTDE", null, null);

                        #region KATVORDEMMP
                        insert.Add(String.Format(sql,
                                                /*IDATVORDEMMATERIAPRIMA*/  IDKAO,
                                                /*CODORDEM*/ (String)dr["CODORDEM"],
                                                /*CODESTRUTURA*/ (String)dr["CODESTRUTURA"],
                                                /*DATAAPONTAMENTO*/ (DateTime)dr["DATAAPONTAMENTO"],
                                                /*IDPRODUTO*/ IDPRODUTO,
                                                /*QUANTIDADE*/ qtdFinal.ToString().Replace(",", "."),
                                                /*RECCREATEDBY*/ AppLib.Context.Usuario,
                                                /*IDATIVIDADE*/ (int)dr["IDATIVIDADE"],
                                                /*RECON*/ dataNF));
                        #endregion

                        #region KESTQDETALHE
                        insert.Add(String.Format(sqlKESTQDETALHE,
                                                /*IDPRODUTO*/ IDPRODUTO,
                                                /*IDAUTOINC*/ IDKED,
                                                /*IDATVORDEM*/ (int)dr["IDATIVIDADE"],
                                                /*CODESTRUTURA*/ (String)dr["CODESTRUTURA"],
                                                /*CODORDEM*/ (String)dr["CODORDEM"],
                                                /*QUANTIDADE*/ qtdFinal.ToString().Replace(",", "."),
                                                /*QTDEORIGINAL*/ qtdFinal.ToString().Replace(",", "."),
                                                /*RECON*/ dataNF,
                                                /*RECCREATEDBY*/ AppLib.Context.Usuario));
                        #endregion

                        #region KESTQDETALHEQTDE
                        insert.Add(String.Format(sqlKESTQDETALHEQTDE,
                                                /*IDPRODUTO*/ IDPRODUTO,
                                                /*IDAUTOINC*/ IDKEDQ,
                                                /*CODESTRUTURA*/ (String)dr["CODESTRUTURA"], 
                                                /*CODORDEM*/ (String)dr["CODORDEM"],
                                                /*QUANTIDADE*/ qtdFinal.ToString().Replace(",", "."), 
                                                /*IDATVORDEM*/ (int)dr["IDATIVIDADE"], 
                                                /*RECCREATEDBY*/ AppLib.Context.Usuario,
                                                /*RECON*/ dataNF));
                        #endregion

                        #region ZOPRATEADAS
                        insert.Add(String.Format(insertLog, (String)dr["CODORDEM"],
                                                             idMOV,
                                                             numeroMov,
                                                             qtdNF.ToString().Replace(",", "."),
                                                             dr["QUANTIDADE"].ToString().Replace(",", "."),
                                                             qtdFinal.ToString().Replace(",", "."),
                                                             porcentagem.ToString().Replace(",", "."),
                                                             IDKAO,
                                                             IDKED,
                                                             IDKEDQ));
                        #endregion

                        if (saiLoop)
                        {
                            executouUltimo = true;
                        }
                    }

                }

                if(quantidadeSomada == qtdNF)
                {
                    MetodosSQL.ExecMultiple(insert);
                }
                else
                {
                    throw new Exception("Quantidade de OP's insuficientes para realizar o reteio!");
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static void AlterarDataMovimentacao(DataRowCollection drc)
        {
            try
            {
                #region Updates
                string sqlTMOV = @"UPDATE TMOV 
	                                    SET 
		                                    DATAMOVIMENTO = convert(date, '{0}', 103), 
		                                    DATAEMISSAO = convert(date, '{0}', 103),
		                                    DATASAIDA = convert(date, '{0}', 103), 
		                                    DATACRIACAO = convert(date, '{0}', 103)
	                                    WHERE
		                                    CODCOLIGADA = 1
	                                    AND IDMOV IN (
				                                    SELECT 
					                                    IDMOV 
				                                    FROM 
					                                    KMOVESTOQUE  
				                                    WHERE 
					                                    CODCOLIGADA = 1 
				                                    AND CODORDEM = '{1}' 
				                                    AND IDPRODUTO IN (81725,82181,83805,83950,83916))";

                string sqlTITMMOV = @"UPDATE TITMMOV 
	                                    SET 
		                                    DATAEMISSAO = convert(date, '{0}', 103)
	                                    WHERE
		                                    CODCOLIGADA = 1
	                                    AND IDMOV IN (
				                                    SELECT 
					                                    IDMOV 
				                                    FROM 
					                                    KMOVESTOQUE  
				                                    WHERE 
					                                    CODCOLIGADA = 1 
				                                    AND CODORDEM = '{1}' 
				                                    AND IDPRODUTO IN (81725,82181,83805,83950,83916))";

                string sqlKMOVESTOQUE = @"UPDATE KMOVESTOQUE
	                                    SET 
		                                    DATAHORA = convert(date, '{0}', 103)
	                                    WHERE 
		                                    CODCOLIGADA = 1 
	                                    AND CODORDEM = '{1}'
	                                    AND IDPRODUTO IN (81725,82181,83805,83950,83916)";

                string sqlDATA = @"select DATAAPONTAMENTO from KATVORDEMMP where IDATVORDEMMATERIAPRIMA = {0}";
                #endregion

                List<string> update = new List<string>();

                foreach (DataRow dr in drc)
                {
                    if (!dr.IsNull("DATABAIXA") && !dr.IsNull("CODORDEM"))
                    {
                        DateTime dta = (DateTime)MetodosSQL.GetObject(String.Format(sqlDATA, dr["IDKAO"]), "DATAAPONTAMENTO");

                        update.Add(String.Format(sqlTMOV, dta, (String)dr["CODORDEM"]));
                        update.Add(String.Format(sqlTITMMOV, dta, (String)dr["CODORDEM"]));
                        update.Add(String.Format(sqlKMOVESTOQUE, dta, (String)dr["CODORDEM"]));
                    }
                }

                MetodosSQL.ExecMultiple(update);

                MessageBox.Show("Processo Concluído com Sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static void DeletarRateio(int IDMOV)
        {
            try
            {
                string sql = String.Format(@"select count(1) as 'TOTAL'
                                    from ZOPRATEADAS ZOR

                                    left
                                    join (select CODCOLIGADA, CODORDEM, IDMOV, DATAHORA from KMOVESTOQUE where IDPRODUTO in (81725,82181,83805,83950,83916)) KME
                                on KME.CODCOLIGADA = ZOR.CODCOLIGADA
                                    and KME.CODORDEM = ZOR.CODORDEM
                                where KME.IDMOV is not null
                                and ZOR.IDMOV = {0}", IDMOV);

                if(int.Parse(MetodosSQL.GetField(sql, "TOTAL")) > 0)
                {
                    throw new Exception("Não é permitido excluir registros de NFs com OPs baixadas!");
                }


                sql = String.Format(@"select * from ZOPRATEADAS WHERE CODCOLIGADA = 1 and CODFILIAL = 1 and IDMOV = '{0}'", IDMOV);
                DataTable dt = MetodosSQL.GetDT(sql);

                string sqlZOR = @"delete from ZOPRATEADAS WHERE CODCOLIGADA = 1 and CODFILIAL = 1 and IDRATEIO = {0}";
                string sqlKAO = @"delete from KATVORDEMMP where IDATVORDEMMATERIAPRIMA = {0}";
                string sqlKED = @"delete from KESTQDETALHE where IDAUTOINC = {0}";
                string sqlKEDQ = @"delete from KESTQDETALHEQTDE where IDAUTOINC = {0}";

                List<string> delete = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    delete.Add(String.Format(sqlKAO, (int)row["IDKAO"]));
                    delete.Add(String.Format(sqlKED, (int)row["IDKED"]));
                    delete.Add(String.Format(sqlKEDQ, (int)row["IDKEDQ"]));
                    delete.Add(String.Format(sqlZOR, (int)row["IDRATEIO"]));
                }

                MetodosSQL.ExecMultiple(delete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
