using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace AppLib.Sync
{
    public partial class SyncTool : Component
    {
        #region PROPRIEDADES

        [Category("_APP"), Description("Itens da lista")]
        public AppLib.Sync.Base[] Item { get; set; }

        [Category("_APP"), Description("Intervalor de sincronização")]
        public int Intervalo { get; set; }

        [Category("_APP"), Description("Status da sincronização")]
        public String Status { get; set; }

        [Category("_APP"), Description("Gerar Log")]
        public Boolean Log { get; set; }

        #endregion

        #region CONSTRUTOR

        public SyncTool()
        {
            InitializeComponent();

            Intervalo = (5  * 1000); // 5 SEGUNDOS = 5 MILISEGUNDOS
            Status = "";
            Log = false;
        }

        public SyncTool(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        
        #endregion

        #region EVENTOS

        public delegate void SetNewParametrosHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método setar parametros da consulta"), DefaultValue(false)]
        public event SetNewParametrosHandler SetParametros;

        #endregion

        #region MÉTODOS

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Log)
            {
                AppLib.Util.Log.Escrever("Iniciando sincronização");
            }

            // Varre as lista de sincronização
            int i = 0;

            while (i < Item.Length)
            {
                if (Item[i].Tipo == Tipo.Receber)
                {
                    #region RECEBER

                    String Comando = "";
                    System.Data.DataTable dt = new System.Data.DataTable("Table1");

                    try
                    {
                        this.SetParametros(this, null);
                    }
                    catch (Exception) { }

                    try
                    {
                        // OBTÉM DADOS DA CONEXÃO
                        String name = AppLib.Context.poolConnection.Get(Item[i].ConexaoRemota).Name;
                        AppLib.Global.Types.Database database = AppLib.Context.poolConnection.Get(Item[i].ConexaoRemota).Database;
                        String connectionString = AppLib.Context.poolConnection.Get(Item[i].ConexaoRemota).ConnectionString;

                        // CRIA UMA NOVA CONEXÃO PARA EVITAR PROBLEMAS DE CONCORRÊNCIA
                        AppLib.Data.Connection conexaoRemota = new Data.Connection(name, database, connectionString);

                        // MONTA E EXECUTA O COMANDO
                        Comando = conexaoRemota.ParseCommand(Item[i].GetConsulta(), Item[i].Parametros);
                        dt = conexaoRemota.ExecQuery(Comando, new Object[] { });
                    }
                    catch (Exception ex)
                    {
                        if (Log)
                        {
                            AppLib.Util.Log.Escrever(Comando + " - " + ex.Message);
                        }
                    }

                    if (dt.Rows.Count == 0)
                    {
                        i++;
                    }
                    else
                    {
                        // OBTÉM DADOS DA CONEXÃO
                        String name = AppLib.Context.poolConnection.Get(Item[i].ConexaoLocal).Name;
                        AppLib.Global.Types.Database database = AppLib.Context.poolConnection.Get(Item[i].ConexaoLocal).Database;
                        String connectionString = AppLib.Context.poolConnection.Get(Item[i].ConexaoLocal).ConnectionString;

                        // CRIA UMA NOVA CONEXÃO PARA EVITAR PROBLEMAS DE CONCORRÊNCIA
                        AppLib.Data.Connection conexaoLocal = new Data.Connection(name, database, connectionString);

                        // Cria ORM
                        AppLib.ORM.Jit x = new ORM.Jit(conexaoLocal, Item[i].Tabela);

                        Status = "Recebendo tabela " + Item[i].Tabela;

                        if (Log)
                        {
                            AppLib.Util.Log.Escrever(Status);
                        }

                        // Varre os dados
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            x.Clear();

                            Status = "Recebendo tabela " + Item[i].Tabela + " (" + (j + 1) + "/" + dt.Rows.Count + ")";

                            if (Log)
                            {
                                AppLib.Util.Log.Escrever(Status);
                            }

                            // Varre as coluna
                            for (int m = 0; m < dt.Columns.Count; m++)
                            {
                                // Seta todos os campos
                                String coluna = dt.Columns[m].ColumnName;
                                Object valor = dt.Rows[j][m];

                                x.Set(coluna, valor);
                            }

                            // Salva (insert/update - automático)
                            int salvou = x.Save();

                            if (Log)
                            {
                                if (salvou == 1)
                                {
                                    AppLib.Util.Log.Escrever("sucesso ao sincronizar registro");
                                }
                                else
                                {
                                    AppLib.Util.Log.Escrever("erro ao sincronizar registro");
                                }
                            }
                            
                        }

                        Status = "";
                    }

                    #endregion
                }
                else
                {
                    #region ENVIAR

                    String Comando = "";
                    System.Data.DataTable dt = new System.Data.DataTable("Table1");

                    try
                    {
                        this.SetParametros(this, null);
                    }
                    catch (Exception) { }

                    try
                    {
                        // OBTÉM DADOS DA CONEXÃO
                        String name = AppLib.Context.poolConnection.Get(Item[i].ConexaoLocal).Name;
                        AppLib.Global.Types.Database database = AppLib.Context.poolConnection.Get(Item[i].ConexaoLocal).Database;
                        String connectionString = AppLib.Context.poolConnection.Get(Item[i].ConexaoLocal).ConnectionString;

                        // CRIA UMA NOVA CONEXÃO PARA EVITAR PROBLEMAS DE CONCORRÊNCIA
                        AppLib.Data.Connection conexaoLocal = new Data.Connection(name, database, connectionString);

                        // MONTA E EXECUTA O COMANDO
                        Comando = conexaoLocal.ParseCommand(Item[i].GetConsulta(), Item[i].Parametros);
                        dt = conexaoLocal.ExecQuery(Comando, new Object[] { });
                    }
                    catch (Exception ex)
                    {
                        if (Log)
                        {
                            AppLib.Util.Log.Escrever(Comando + " - " + ex.Message);
                        }
                    }

                    if (dt.Rows.Count == 0)
                    {
                        i++;
                    }
                    else
                    {
                        // OBTÉM DADOS DA CONEXÃO
                        String name = AppLib.Context.poolConnection.Get(Item[i].ConexaoRemota).Name;
                        AppLib.Global.Types.Database database = AppLib.Context.poolConnection.Get(Item[i].ConexaoRemota).Database;
                        String connectionString = AppLib.Context.poolConnection.Get(Item[i].ConexaoRemota).ConnectionString;

                        // CRIA UMA NOVA CONEXÃO PARA EVITAR PROBLEMAS DE CONCORRÊNCIA
                        AppLib.Data.Connection conexaoRemota = new Data.Connection(name, database, connectionString);

                        // Cria ORM
                        AppLib.ORM.Jit x = new ORM.Jit(conexaoRemota, Item[i].Tabela);

                        Status = "Enviando tabela " + Item[i].Tabela;

                        if (Log)
                        {
                            AppLib.Util.Log.Escrever(Status);
                        }

                        // Varre os dados
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            x.Clear();

                            Status = "Enviando tabela " + Item[i].Tabela + " (" + (j + 1) + "/" + dt.Rows.Count + ")";

                            if (Log)
                            {
                                AppLib.Util.Log.Escrever(Status);
                            }

                            // Varre as coluna
                            for (int m = 0; m < dt.Columns.Count; m++)
                            {
                                // Seta todoS os campos
                                x.Set(dt.Columns[m].ColumnName, dt.Rows[j][m]);
                            }

                            // Salva (insert/update - automático)
                            int salvou = x.Save();

                            if (Log)
                            {
                                if (salvou == 1)
                                {
                                    AppLib.Util.Log.Escrever("sucesso ao sincronizar registro");
                                }
                                else
                                {
                                    AppLib.Util.Log.Escrever("erro ao sincronizar registro");
                                }
                            }
                        }

                        Status = "";
                    }

                    #endregion
                }
            }

            if (Log)
            {
                AppLib.Util.Log.Escrever("Sincronização concluída");
            }
        }

        public void Start()
        {
            timer1.Start();
        }

        public void Stop()
        {
            timer1.Stop();
        }
        
        #endregion

    }
}
