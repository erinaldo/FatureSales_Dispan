using AppFatureClient.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFatureClient
{
    public class Util
    {
        public AppInterop.Message ConvertToMessage(AppFatureClient.wsLib.Message message)
        {
            AppInterop.Message msg = new AppInterop.Message();
            msg.Retorno = message.Retorno;
            msg.Mensagem = message.Mensagem;
            return msg;
        }

        public static Boolean ChecaPermissão(String usuario, String processo)
        {
            try
            {
                MetodosSQL.CS = AppLib.Context.poolConnection.Get().ConnectionString;

                string sql = String.Format(@"select COUNT(1) as 'Contador' from ZPROCESSOPERFIL ZPP

                                         inner join ZUSUARIO ZU
                                         on ZU.CODPERFIL = ZPP.CODPERFIL

                                         where USUARIO = '{0}'
                                         and ZPP.CODPROCESSO = '{1}'", usuario, processo);

                if (!(Convert.ToInt32(MetodosSQL.GetField(sql, "Contador")) >= 1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
        }

        public AppFatureClient.wsLib.MovMovimentoPar ConvertToWSMovMovimentoPar(AppInterop.MovMovimentoPar movimento)
        {
            AppFatureClient.wsLib.MovMovimentoPar mov = new AppFatureClient.wsLib.MovMovimentoPar();
            string er = string.Empty;
            try
            {
                mov.CodColigada = movimento.CodColigada;
                mov.IdMov = movimento.IdMov;
                mov.GuidId = movimento.GuidId;
                mov.CodTMv = movimento.CodTMv;
                mov.CodFilial = movimento.CodFilial;
                mov.CodLoc = movimento.CodLoc;
                mov.CodColCfo = movimento.CodColCfo;
                mov.CodCfo = movimento.CodCfo;
                mov.Serie = movimento.Serie;
                mov.DataEmissao = movimento.DataEmissao;
                mov.DataEntrega = movimento.DataEntrega;
                mov.PrazoEntrega = movimento.PrazoEntrega;
                mov.CodRepresentante = movimento.CodRepresentante;
                mov.FreteCIFouFOB = movimento.FreteCIFouFOB;
                mov.CodTra = movimento.CodTra;
                mov.CodCondicaoPagamento = movimento.CodCondicaoPagamento;
                mov.PercentualFrete = movimento.PercentualFrete;
                mov.ValorFrete = movimento.ValorFrete;
                mov.PercentualDesc = movimento.PercentualDesc;
                mov.ValorDesc = movimento.ValorDesc;
                mov.PercentualDesp = movimento.PercentualDesp;
                mov.ValorDesp = movimento.ValorDesp;
                mov.PercentualSeguro = movimento.PercentualSeguro;
                mov.ValorSeguro = movimento.ValorSeguro;
                mov.PercentualExtra1 = movimento.PercentualExtra1;
                mov.ValorExtra1 = movimento.ValorExtra1;
                mov.Observacao = movimento.Observacao;
                mov.CampoLivre1 = movimento.CampoLivre1;
                mov.HistoricoLongo = movimento.HistoricoLongo;
                mov.NumeroOrdem = movimento.NumeroOrdem;
                mov.SegundoNumero = movimento.SegundoNumero;
                mov.CodUsuario = movimento.CodUsuario;
                mov.CodUsuarioLogado = movimento.CodUsuarioLogado;
                mov.DataCriacao = movimento.DataCriacao;
                mov.DataMovimento = movimento.DataMovimento;
                mov.IdMovCfo = movimento.IdMovCfo;
                mov.MSGFORNEC = movimento.MSGFORNEC;
                mov.CONDPGTO = movimento.CONDPGTO;
                mov.TPCULTURA = movimento.TPCULTURA;
                mov.FINANCIADO = movimento.FINANCIADO;
                mov.DEMONSBRINDE = movimento.DEMONSBRINDE;
                mov.PARAFINANC = movimento.PARAFINANC;
                mov.CLASSREQ = movimento.CLASSREQ;
                mov.TIPOVENDA = movimento.TIPOVENDA;
            }
            catch (Exception ex)
            {
                er = "Erro: " + ex.Message + "\n\n" + ex.ToString();
            }

            return mov;
        }

        public AppFatureClient.wsLib.MovMovimentoFatPar ConvertToWSMovMovimentoFatPar(AppInterop.MovMovimentoFatPar faturamento)
        {
            AppFatureClient.wsLib.MovMovimentoFatPar fat = new AppFatureClient.wsLib.MovMovimentoFatPar();
            fat.CodColigada = faturamento.CodColigada;
            fat.CodTmvOrigem = faturamento.CodTmvOrigem;
            fat.CodUsuario = faturamento.CodUsuario;
            fat.realizaBaixaPedido = faturamento.realizaBaixaPedido;
            fat.TipoFaturamento = faturamento.TipoFaturamento;
            fat.CodSistema = faturamento.CodSistema;
            fat.CodTmvDestino = faturamento.CodTmvDestino;
            fat.IdExercicioFiscal = faturamento.IdExercicioFiscal;
            fat.numeroMov = faturamento.numeroMov;
            fat.IdMov = new int[1];
            fat.IdMov[0] = faturamento.IdMov[0];

            fat.listaMovItemFatAutomatico = new AppFatureClient.wsLib.MovMovimentoItemFatPar[faturamento.listaMovItemFatAutomatico.Count];
            for (int j = 0; j < faturamento.listaMovItemFatAutomatico.Count; j++)
            {
                AppFatureClient.wsLib.MovMovimentoItemFatPar item = new AppFatureClient.wsLib.MovMovimentoItemFatPar();
                item.Checked = 1;
                item.CodColigada = faturamento.listaMovItemFatAutomatico[j].CodColigada;
                item.IdMov = faturamento.listaMovItemFatAutomatico[j].IdMov;
                item.NSeqItmMov = faturamento.listaMovItemFatAutomatico[j].NSeqItmMov;
                item.Quantidade = faturamento.listaMovItemFatAutomatico[j].Quantidade;

                fat.listaMovItemFatAutomatico[j] = item;
            }

            return fat;
        }

        public AppFatureClient.wsLib.MovExclusaoPar ConvertToWSMovExclusaoPar(AppInterop.MovExclusaoPar exclusao)
        {
            AppFatureClient.wsLib.MovExclusaoPar exc = new AppFatureClient.wsLib.MovExclusaoPar();
            exc.CodColigada = exclusao.CodColigada;
            exc.CodSistemaLogado = exclusao.CodSistemaLogado;
            exc.CodUsuarioLogado = exclusao.CodUsuarioLogado;
            exc.IdMov = exclusao.IdMov;
            return exc;
        }

        public AppFatureClient.wsLib.MovCancelamentoPar ConvertToWSMovCancelamentoPar(AppInterop.MovCancelamentoPar cancelamento)
        {
            AppFatureClient.wsLib.MovCancelamentoPar canc = new AppFatureClient.wsLib.MovCancelamentoPar();
            canc.CodColigada = cancelamento.CodColigada;
            canc.ApagarMovRelac = cancelamento.ApagarMovRelac;
            canc.CodSistemaLogado = cancelamento.CodSistemaLogado;
            canc.CodUsuarioLogado = cancelamento.CodUsuarioLogado;
            canc.DataCancelamento = cancelamento.DataCancelamento;
            canc.IdExercicioFiscal = cancelamento.IdExercicioFiscal;
            canc.IdMov = cancelamento.IdMov;
            canc.MotivoCancelamento = cancelamento.MotivoCancelamento;
            return canc;
        }

        public AppFatureClient.wsLib.FinCliForPar ConvertToWSFinCliForPar(AppInterop.FinCliForPar cliente)
        {
            AppFatureClient.wsLib.FinCliForPar cli = new AppFatureClient.wsLib.FinCliForPar();

            cli.CodColigada = cliente.CodColigada;
            cli.CodCfo = cliente.CodCfo;
            cli.NomeFantasia = cliente.NomeFantasia;
            cli.Nome = cliente.Nome;
            cli.CGCCFO = cliente.CGCCFO;
            cli.InscrEstadual = cliente.InscrEstadual;
            cli.InscrMunicipal = cliente.InscrMunicipal;
            cli.PagRec = cliente.PagRec;
            cli.PessoaFisOuJur = cliente.PessoaFisOuJur;
            cli.Rua = cliente.Rua;
            cli.Numero = cliente.Numero;
            cli.Complemento = cliente.Complemento;
            cli.Bairro = cliente.Bairro;
            cli.CODETD = cliente.CODETD;
            cli.CEP = cliente.CEP;
            cli.Telefone = cliente.Telefone;
            cli.Telex = cliente.Telex;
            cli.FAX = cliente.FAX;
            cli.Contato = cliente.Contato;
            cli.Email = cliente.Email;
            cli.RuaPgto = cliente.RuaPgto;
            cli.NumeroPgto = cliente.NumeroPgto;
            cli.ComplementoPgto = cliente.ComplementoPgto;
            cli.BairroPgto = cliente.BairroPgto;
            cli.CODETDPgto = cliente.CODETDPgto;
            cli.CEPPgto = cliente.CEPPgto;
            cli.TelefonePgto = cliente.TelefonePgto;
            cli.FAXPgto = cliente.FAXPgto;
            cli.ContatoPgto = cliente.ContatoPgto;
            cli.EmailPgto = cliente.EmailPgto;
            cli.RuaEntrega = cliente.RuaEntrega;
            cli.NumeroEntrega = cliente.NumeroEntrega;
            cli.ComplemEntrega = cliente.ComplemEntrega;
            cli.BairroEntrega = cliente.BairroEntrega;
            cli.CODETDEntrega = cliente.CODETDEntrega;
            cli.CEPEntrega = cliente.CEPEntrega;
            cli.TelefoneEntrega = cliente.TelefoneEntrega;
            cli.FAXEntrega = cliente.FAXEntrega;
            cli.ContatoEntrega = cliente.ContatoEntrega;
            cli.EmailEntrega = cliente.EmailEntrega;
            cli.CodMunicipio = cliente.CodMunicipio;
            cli.CodMunicipioPgto = cliente.CodMunicipioPgto;
            cli.CodMunicipioEntrega = cliente.CodMunicipioEntrega;
            cli.Cidade = cliente.Cidade;
            cli.CidadePgto = cliente.CidadePgto;
            cli.CidadeEntrega = cliente.CidadeEntrega;
            cli.IDPais = cliente.IDPais;
            cli.IDPaisPgto = cliente.IDPaisPgto;
            cli.IDPaisEntrega = cliente.IDPaisEntrega;
            cli.TipoRua = cliente.TipoRua;
            cli.TipoBairro = cliente.TipoBairro;
            cli.TipoRuaPgto = cliente.TipoRuaPgto;
            cli.TipoBairroPgto = cliente.TipoBairroPgto;
            cli.TipoRuaEntrega = cliente.TipoRuaEntrega;
            cli.TipoBairroEntrega = cliente.TipoBairroEntrega;
            cli.RamoAtiv = cliente.RamoAtiv;
            cli.LimiteCredito = cliente.LimiteCredito;
            cli.Suframa = cliente.Suframa;
            cli.Ativo = cliente.Ativo;
            cli.UsuarioCriacao = cliente.UsuarioCriacao;
            cli.UsuarioAlteracao = cliente.UsuarioAlteracao;
            cli.HISTORICOATUAL = cliente.HISTORICOATUAL;
            cli.CodRpr = cliente.CodRpr;
            cli.ContribuinteICMS = cliente.ContribuinteICMS;
            cli.Nacionalidade = cliente.Nacionalidade;
            cli.DocumentosEstrangeiro = cliente.DocumentosEstrangeiro;
            return cli;
        }

        public static string Escrever_Valor_Extenso(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));
                if (a == 1) montagem += (b + c == 0) ? "cem" : "cento";
                else if (a == 2) montagem += "duzentos";
                else if (a == 3) montagem += "trezentos";
                else if (a == 4) montagem += "quatrocentos";
                else if (a == 5) montagem += "quientos";
                else if (a == 6) montagem += "seiscentos";
                else if (a == 7) montagem += "setecentos";
                else if (a == 8) montagem += "oitocentos";
                else if (a == 9) montagem += "novecentos";
                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "dez";
                    else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "onze";
                    else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "doze";
                    else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "treze";
                    else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "quatorze";
                    else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "quinze";
                    else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "dezesseis";
                    else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "dezessete";
                    else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "dezoito";
                    else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "dezenove";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "vinte";
                else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "trinta";
                else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "quarenta";
                else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "cinquenta";
                else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "sessenta";
                else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "setenta";
                else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "oitenta";
                else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "noventa";
                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";
                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "um";
                    else if (c == 2) montagem += "dois";
                    else if (c == 3) montagem += "três";
                    else if (c == 4) montagem += "quatro";
                    else if (c == 5) montagem += "cinco";
                    else if (c == 6) montagem += "seis";
                    else if (c == 7) montagem += "sete";
                    else if (c == 8) montagem += "oito";
                    else if (c == 9) montagem += "nove";
                return montagem;
            }
        }
    }
}
