using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.ORM
{
    public class Engine
    {
        public static String SelectMetadados(AppLib.Data.Connection connection)
        {
            String Query = "";

            #region SETA A QUERY PARA BANCO SQL SERVER

            if ((connection.Database == Global.Types.Database.SqlClient) ||
                (connection.Database == Global.Types.Database.SqlWebService) ||
                (connection.Database == Global.Types.Database.SqlLocalDb))
            {
                Query = @"
SELECT * FROM (

SELECT 

(SELECT NAME
FROM sys.sysobjects
WHERE id = SYS.all_columns.object_id) NOMETABELA,

sys.all_columns.name NOMECAMPO,

(SELECT NAME
FROM sys.types
WHERE system_type_id = SYS.all_columns.system_type_id
  AND user_type_id = SYS.all_columns.system_type_id) TIPOCAMPO,

MAX_LENGTH TAMANHO,
PRECISION PRECISAO,
SCALE ESCALA,

CASE WHEN((SELECT COUNT(*)
FROM sys.index_columns, sys.indexes
WHERE sys.index_columns.object_id = sys.all_columns.object_id
  AND sys.index_columns.column_id = sys.all_columns.column_id
  
  AND sys.index_columns.object_id = SYS.indexes.object_id
  AND SYS.index_columns.index_id = SYS.indexes.index_id
    
  AND sys.indexes.name IN ( SELECT name
							FROM sys.key_constraints
							WHERE TYPE = 'PK'
							AND parent_object_id = SYS.all_columns.object_id)) > 0) THEN 'S' ELSE 'N' END CHAVE,

CASE WHEN(IS_IDENTITY = 1) THEN 'S' ELSE 'N' END AUTOINCREMENTO,
CASE WHEN(IS_NULLABLE = 1) THEN 'S' ELSE 'N' END NULO,
CASE WHEN(IS_COMPUTED = 1) THEN 'S' ELSE 'N' END VIRTUAL

FROM SYS.all_columns

inner join sys.objects
on sys.objects.object_id = sys.all_columns.object_id

where sys.all_columns.name not in ('LOGID','AUDITACTION','AUDITID','LOGAPP','LOGUSER','PARENTLOGID')
and sys.objects.schema_id = (select schema_id from sys.schemas where name = 'dbo')


) X WHERE NOMETABELA = ?

ORDER BY NOMETABELA, CHAVE DESC, NOMECAMPO";
            }
            
            #endregion

            #region SETA A QUERY PARA BANCO ORACLE

            if ((connection.Database == Global.Types.Database.OracleClient) ||
                (connection.Database == Global.Types.Database.OracleWebService))
            {
                Query = @"
SELECT
TABLE_NAME NOMETABELA,
COLUMN_NAME NOMECAMPO,

DATA_TYPE TIPOCAMPO,
NVL(DATA_LENGTH,0) TAMANHO,
NVL(DATA_PRECISION,0) PRECISAO,
NVL(DATA_SCALE,0) ESCALA,

CASE WHEN((SELECT COUNT(*)

FROM ALL_CONSTRAINTS, ALL_IND_COLUMNS

WHERE ALL_CONSTRAINTS.TABLE_NAME = ALL_IND_COLUMNS.TABLE_NAME
  AND ALL_CONSTRAINTS.CONSTRAINT_NAME = ALL_IND_COLUMNS.INDEX_NAME

  AND ALL_CONSTRAINTS.CONSTRAINT_TYPE = 'P'
  AND ALL_CONSTRAINTS.TABLE_NAME = ALL_TAB_COLS.TABLE_NAME
  AND ALL_IND_COLUMNS.COLUMN_NAME = ALL_TAB_COLS.COLUMN_NAME) > 0) THEN 'S' ELSE 'N' END CHAVE,

( SELECT CASE WHEN ( COUNT(*) > 0 ) THEN 'S' ELSE 'N' END FROM SYS.ALL_SEQUENCES WHERE SEQUENCE_NAME = TABLE_NAME || '_' || COLUMN_NAME || '_SEQ' ) AUTOINCREMENTO,
CASE WHEN (NULLABLE = 'Y') THEN 'S' ELSE 'N' END NULO,
CASE WHEN(VIRTUAL_COLUMN <> 'NO') THEN 'S' ELSE 'N' END VIRTUAL

FROM ALL_TAB_COLS

WHERE TABLE_NAME = ?

ORDER BY NOMETABELA, CHAVE DESC, NOMECAMPO";
            }

            #endregion            
            
            return Query;
        }

        public static String SelectPK(AppLib.Data.Connection connection)
        {
            String Query = "";

            #region SQL

            if (connection.Database == Global.Types.Database.SqlClient)
            {
                Query = @"
SELECT * FROM (

SELECT sys.foreign_keys.name CONSTRAINTNAME,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.parent_object_id ) TABLEPK,

( SELECT NAME
FROM SYS.all_columns
WHERE SYS.all_columns.object_id = SYS.foreign_key_columns.parent_object_id
  AND SYS.all_columns.column_id = SYS.foreign_key_columns.parent_column_id ) COLUMNPK,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.referenced_object_id ) TABLEFK,

( SELECT NAME
FROM SYS.all_columns
WHERE SYS.all_columns.object_id = SYS.foreign_key_columns.referenced_object_id
  AND SYS.all_columns.column_id = SYS.foreign_key_columns.referenced_column_id ) COLUMNFK


FROM SYS.foreign_key_columns, sys.foreign_keys

WHERE SYS.foreign_key_columns.constraint_object_id = sys.foreign_keys.object_id

) X

WHERE TABLEPK = ?

ORDER BY 1, 2, 3, 4, 5";
            }
            
            #endregion

            #region ORACLE

            if (connection.Database == Global.Types.Database.OracleClient)
            {
                Query = @"
SELECT
ALL_CONSTRAINTS.CONSTRAINT_NAME CONSTRAINTNAME,
ALL_CONS_COLUMNS.TABLE_NAME TABLEPK,
ALL_CONS_COLUMNS.COLUMN_NAME COLUMNPK,
ALL_CONS_COLUMNS2.TABLE_NAME TABLEFK,
ALL_CONS_COLUMNS2.COLUMN_NAME COLUMNFK

FROM SYS.ALL_CONS_COLUMNS, SYS.ALL_CONSTRAINTS, SYS.ALL_CONSTRAINTS ALL_CONSTRAINTS2, SYS.ALL_CONS_COLUMNS ALL_CONS_COLUMNS2

WHERE ALL_CONS_COLUMNS.OWNER = ALL_CONSTRAINTS.OWNER
  AND ALL_CONS_COLUMNS.CONSTRAINT_NAME = ALL_CONSTRAINTS.CONSTRAINT_NAME
  AND ALL_CONSTRAINTS.CONSTRAINT_TYPE = 'R'
  AND ALL_CONS_COLUMNS.TABLE_NAME = ALL_CONSTRAINTS.TABLE_NAME
  
  AND ALL_CONSTRAINTS.R_OWNER = ALL_CONSTRAINTS2.OWNER
  AND ALL_CONSTRAINTS.R_CONSTRAINT_NAME = ALL_CONSTRAINTS2.CONSTRAINT_NAME
  AND ALL_CONSTRAINTS2.CONSTRAINT_TYPE = 'P'
  
  AND ALL_CONSTRAINTS2.OWNER = ALL_CONS_COLUMNS2.OWNER
  AND ALL_CONSTRAINTS2.CONSTRAINT_NAME = ALL_CONS_COLUMNS2.CONSTRAINT_NAME
  AND ALL_CONSTRAINTS2.TABLE_NAME = ALL_CONS_COLUMNS2.TABLE_NAME
  
  AND ALL_CONS_COLUMNS.POSITION = ALL_CONS_COLUMNS2.POSITION
  
  AND ALL_CONS_COLUMNS.TABLE_NAME = ?
  
ORDER BY 1, 2, 3, 4, 5";
            }
            
            #endregion

            #region SQLLOCALDB

            if (connection.Database == Global.Types.Database.SqlLocalDb)
            {
                Query = @"
SELECT * FROM (

SELECT sys.foreign_keys.name CONSTRAINTNAME,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.parent_object_id ) TABLEPK,

( SELECT NAME
FROM SYS.all_columns
WHERE SYS.all_columns.object_id = SYS.foreign_key_columns.parent_object_id
  AND SYS.all_columns.column_id = SYS.foreign_key_columns.parent_column_id ) COLUMNPK,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.referenced_object_id ) TABLEFK,

( SELECT NAME
FROM SYS.all_columns
WHERE SYS.all_columns.object_id = SYS.foreign_key_columns.referenced_object_id
  AND SYS.all_columns.column_id = SYS.foreign_key_columns.referenced_column_id ) COLUMNFK


FROM SYS.foreign_key_columns, sys.foreign_keys

WHERE SYS.foreign_key_columns.constraint_object_id = sys.foreign_keys.object_id

) X

WHERE TABLEPK = ?

ORDER BY 1, 2, 3, 4, 5";
            }

            #endregion

            return Query;
        }

        public static String SelectFK(AppLib.Data.Connection connection)
        {
            String Query = "";

            #region SQL

            if (connection.Database == Global.Types.Database.SqlClient)
            {
                Query = @"
SELECT DISTINCT X.* FROM (

SELECT sys.foreign_keys.name CONSTRAINTNAME,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.parent_object_id ) TABLEPK,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.referenced_object_id ) TABLEFK

FROM SYS.foreign_key_columns, sys.foreign_keys

WHERE SYS.foreign_key_columns.constraint_object_id = sys.foreign_keys.object_id

) X

WHERE TABLEFK = ?

ORDER BY 1, 2, 3";
            }

            #endregion

            #region ORACLE

            if (connection.Database == Global.Types.Database.OracleClient)
            {
                Query = @"
SELECT DISTINCT
ALL_CONSTRAINTS.CONSTRAINT_NAME CONSTRAINTNAME,
ALL_CONS_COLUMNS.TABLE_NAME TABLEPK,
ALL_CONS_COLUMNS2.TABLE_NAME TABLEFK

FROM SYS.ALL_CONS_COLUMNS, SYS.ALL_CONSTRAINTS, SYS.ALL_CONSTRAINTS ALL_CONSTRAINTS2, SYS.ALL_CONS_COLUMNS ALL_CONS_COLUMNS2

WHERE ALL_CONS_COLUMNS.OWNER = ALL_CONSTRAINTS.OWNER
  AND ALL_CONS_COLUMNS.CONSTRAINT_NAME = ALL_CONSTRAINTS.CONSTRAINT_NAME
  AND ALL_CONSTRAINTS.CONSTRAINT_TYPE = 'R'
  AND ALL_CONS_COLUMNS.TABLE_NAME = ALL_CONSTRAINTS.TABLE_NAME
  
  AND ALL_CONSTRAINTS.R_OWNER = ALL_CONSTRAINTS2.OWNER
  AND ALL_CONSTRAINTS.R_CONSTRAINT_NAME = ALL_CONSTRAINTS2.CONSTRAINT_NAME
  AND ALL_CONSTRAINTS2.CONSTRAINT_TYPE = 'P'
  
  AND ALL_CONSTRAINTS2.OWNER = ALL_CONS_COLUMNS2.OWNER
  AND ALL_CONSTRAINTS2.CONSTRAINT_NAME = ALL_CONS_COLUMNS2.CONSTRAINT_NAME
  AND ALL_CONSTRAINTS2.TABLE_NAME = ALL_CONS_COLUMNS2.TABLE_NAME
  
  AND ALL_CONS_COLUMNS.POSITION = ALL_CONS_COLUMNS2.POSITION
  
  AND ALL_CONS_COLUMNS2.TABLE_NAME = ?
  
ORDER BY 1, 2, 3";
            }

            #endregion

            #region SQLLOCALDB

            if (connection.Database == Global.Types.Database.SqlLocalDb)
            {
                Query = @"
SELECT DISTINCT X.* FROM (

SELECT sys.foreign_keys.name CONSTRAINTNAME,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.parent_object_id ) TABLEPK,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.referenced_object_id ) TABLEFK

FROM SYS.foreign_key_columns, sys.foreign_keys

WHERE SYS.foreign_key_columns.constraint_object_id = sys.foreign_keys.object_id

) X

WHERE TABLEFK = ?

ORDER BY 1, 2, 3";
            }

            #endregion

            return Query;
        }

        public static String SelectFKRef(AppLib.Data.Connection connection)
        {
            String Query = "";

            #region SQL

            if (connection.Database == Global.Types.Database.SqlClient)
            {
                Query = @"
SELECT * FROM (

SELECT sys.foreign_keys.name CONSTRAINTNAME,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.parent_object_id ) TABLEPK,

( SELECT NAME
FROM SYS.all_columns
WHERE SYS.all_columns.object_id = SYS.foreign_key_columns.parent_object_id
  AND SYS.all_columns.column_id = SYS.foreign_key_columns.parent_column_id ) COLUMNPK,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.referenced_object_id ) TABLEFK,

( SELECT NAME
FROM SYS.all_columns
WHERE SYS.all_columns.object_id = SYS.foreign_key_columns.referenced_object_id
  AND SYS.all_columns.column_id = SYS.foreign_key_columns.referenced_column_id ) COLUMNFK


FROM SYS.foreign_key_columns, sys.foreign_keys

WHERE SYS.foreign_key_columns.constraint_object_id = sys.foreign_keys.object_id

) X

WHERE TABLEFK = ?
  AND CONSTRAINTNAME = ?

ORDER BY 1, 2, 3, 4, 5";
            }

            #endregion

            #region ORACLE

            if (connection.Database == Global.Types.Database.OracleClient)
            {
                Query = @"
SELECT
ALL_CONSTRAINTS.CONSTRAINT_NAME CONSTRAINTNAME,
ALL_CONS_COLUMNS.TABLE_NAME TABLEPK,
ALL_CONS_COLUMNS.COLUMN_NAME COLUMNPK,
ALL_CONS_COLUMNS2.TABLE_NAME TABLEFK,
ALL_CONS_COLUMNS2.COLUMN_NAME COLUMNFK

FROM SYS.ALL_CONS_COLUMNS, SYS.ALL_CONSTRAINTS, SYS.ALL_CONSTRAINTS ALL_CONSTRAINTS2, SYS.ALL_CONS_COLUMNS ALL_CONS_COLUMNS2

WHERE ALL_CONS_COLUMNS.OWNER = ALL_CONSTRAINTS.OWNER
  AND ALL_CONS_COLUMNS.CONSTRAINT_NAME = ALL_CONSTRAINTS.CONSTRAINT_NAME
  AND ALL_CONSTRAINTS.CONSTRAINT_TYPE = 'R'
  AND ALL_CONS_COLUMNS.TABLE_NAME = ALL_CONSTRAINTS.TABLE_NAME
  
  AND ALL_CONSTRAINTS.R_OWNER = ALL_CONSTRAINTS2.OWNER
  AND ALL_CONSTRAINTS.R_CONSTRAINT_NAME = ALL_CONSTRAINTS2.CONSTRAINT_NAME
  AND ALL_CONSTRAINTS2.CONSTRAINT_TYPE = 'P'
  
  AND ALL_CONSTRAINTS2.OWNER = ALL_CONS_COLUMNS2.OWNER
  AND ALL_CONSTRAINTS2.CONSTRAINT_NAME = ALL_CONS_COLUMNS2.CONSTRAINT_NAME
  AND ALL_CONSTRAINTS2.TABLE_NAME = ALL_CONS_COLUMNS2.TABLE_NAME
  
  AND ALL_CONS_COLUMNS.POSITION = ALL_CONS_COLUMNS2.POSITION
  
  AND ALL_CONS_COLUMNS2.TABLE_NAME = ?
  AND ALL_CONSTRAINTS.CONSTRAINT_NAME = ?
  
ORDER BY 1, 2, 3, 4, 5";
            }

            #endregion

            #region SQLLOCALDB

            if (connection.Database == Global.Types.Database.SqlLocalDb)
            {
                Query = @"
SELECT * FROM (

SELECT sys.foreign_keys.name CONSTRAINTNAME,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.parent_object_id ) TABLEPK,

( SELECT NAME
FROM SYS.all_columns
WHERE SYS.all_columns.object_id = SYS.foreign_key_columns.parent_object_id
  AND SYS.all_columns.column_id = SYS.foreign_key_columns.parent_column_id ) COLUMNPK,

( SELECT NAME
FROM SYS.sysobjects
WHERE SYS.sysobjects.id = SYS.foreign_key_columns.referenced_object_id ) TABLEFK,

( SELECT NAME
FROM SYS.all_columns
WHERE SYS.all_columns.object_id = SYS.foreign_key_columns.referenced_object_id
  AND SYS.all_columns.column_id = SYS.foreign_key_columns.referenced_column_id ) COLUMNFK


FROM SYS.foreign_key_columns, sys.foreign_keys

WHERE SYS.foreign_key_columns.constraint_object_id = sys.foreign_keys.object_id

) X

WHERE TABLEFK = ?
  AND CONSTRAINTNAME = ?

ORDER BY 1, 2, 3, 4, 5";
            }

            #endregion

            return Query;
        }

        public static Type BuscarTipo(AppLib.Data.Connection connection, String TipoBanco, Boolean Nulo)
        {
            String TipoLinguagem = "";

            #region SEARCH TYPE FOR DATABASE SQL SERVER

            if (connection.Database == Global.Types.Database.SqlClient)
            {
                if (TipoBanco.ToUpper().Equals("BIGINT")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("BINARY")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("BIT")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("CHAR")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("DATE")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("DATETIME")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("DATETIME2")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("DATETIMEOFFSET")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("DECIMAL")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("double")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("IMAGE")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("INT")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("MONEY")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("NCHAR")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("NTEXT")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("NUMERIC")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("NVARCHAR")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("REAL")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("SMALLDATETIME")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("SMALLINT")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("SMALLMONEY")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("SQL_VARIANT")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("TEXT")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("TIME")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TINYINT")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("UNIQUEIDENTIFIER")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("VARBINARY")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("VARCHAR")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("XML")) { TipoLinguagem = "String"; }
            }

            #endregion

            #region SEARCH TYPE FOR DATABASE ORACLE

            if (connection.Database == Global.Types.Database.OracleClient)
            {
                if (TipoBanco.ToUpper().Equals("BINARY_DOUBLE")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("BLOB")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("CHAR")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("CLOB")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("DATE")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("double")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("INTERVAL DAY(0) TO SECOND(0)")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("INTERVAL DAY(3) TO SECOND(0)")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("INTERVAL DAY(3) TO SECOND(2)")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("INTERVAL DAY(5) TO SECOND(1)")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("INTERVAL DAY(9) TO SECOND(6)")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("INTERVAL DAY(9) TO SECOND(9)")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("LONG RAW")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("LONG")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("MLSLABEL")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("NCHAR")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("NCLOB")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("NUMBER")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("NVARCHAR2")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("RAW")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("ROWID")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP(0) WITH TIME ZONE")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP(0)")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP(1)")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP(13) WITH TIME ZONE")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP(3) WITH TIME ZONE")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP(3)")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP(6) WITH TIME ZONE")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP(6)")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP(9) WITH TIME ZONE")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP(9)")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("UNDEFINED")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("UROWID")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("VARCHAR2")) { TipoLinguagem = "String"; }
            }

            #endregion

            #region SEARCH TYPE FOR DATABASE SQLLOCALDB

            if (connection.Database == Global.Types.Database.SqlLocalDb)
            {
                if (TipoBanco.ToUpper().Equals("BIGINT")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("BINARY")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("BIT")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("CHAR")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("DATE")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("DATETIME")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("DATETIME2")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("DATETIMEOFFSET")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("DECIMAL")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("double")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("IMAGE")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("INT")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("MONEY")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("NCHAR")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("NTEXT")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("NUMERIC")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("NVARCHAR")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("REAL")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("SMALLDATETIME")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("SMALLINT")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("SMALLMONEY")) { TipoLinguagem = "double?"; }
                if (TipoBanco.ToUpper().Equals("SQL_VARIANT")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("TEXT")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("TIME")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TIMESTAMP")) { TipoLinguagem = "DateTime?"; }
                if (TipoBanco.ToUpper().Equals("TINYINT")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("UNIQUEIDENTIFIER")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("VARBINARY")) { TipoLinguagem = "int?"; }
                if (TipoBanco.ToUpper().Equals("VARCHAR")) { TipoLinguagem = "String"; }
                if (TipoBanco.ToUpper().Equals("XML")) { TipoLinguagem = "String"; }
            }

            #endregion

            #region CHECK NULL

            if (Nulo)
            {
                if (TipoLinguagem.Equals("int?"))
                {
                    return typeof(int?);
                }

                if (TipoLinguagem.Equals("double?"))
                {
                    return typeof(double?);
                }

                if (TipoLinguagem.Equals("DateTime?"))
                {
                    return typeof(DateTime?);
                }

                if (TipoLinguagem.Equals("String"))
                {
                    return typeof(String);
                }
            }
            else
            {
                if (TipoLinguagem.Equals("int?"))
                {
                    return typeof(int);
                }

                if (TipoLinguagem.Equals("double?"))
                {
                    return typeof(double);
                }

                if (TipoLinguagem.Equals("DateTime?"))
                {
                    return typeof(DateTime);
                }

                if (TipoLinguagem.Equals("String"))
                {
                    return typeof(String);
                }
            }
            
            #endregion

            return null;
        }

    }
}
