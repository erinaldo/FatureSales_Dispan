using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Global
{
    public class Types
    {
        public enum Database { SqlClient, OracleClient, SqlLocalDb, SqlWebService, OracleWebService }

        public enum Confirmacao { OK, Cancelar }

        public enum TipoFiltro { Selecionar, Todos, Nenhum }

        public enum TipoAtualizacao { Expandir, Encolher, Nenhum }

        public enum Acao { Novo, Editar, Excluir }

        public enum TipoExpressao { ValorFixo, Variavel, Fluxo }

        public enum TipoCampoCheck { Booleano, Inteiro, String }

    }
}
