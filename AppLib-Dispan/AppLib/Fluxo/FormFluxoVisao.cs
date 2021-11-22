using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Fluxo
{
    public partial class FormFluxoVisao : AppLib.Windows.FormVisao
    {
        #region CONSTRUTOR E LOAD

        public String CodigoFonte { get; set; }

        private static FormFluxoVisao _instance = null;

        public static FormFluxoVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormFluxoVisao();

            return _instance;
        }

        private void FormFluxoVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormFluxoVisao()
        {
            InitializeComponent();
        }

        private void FormFluxoVisao_Load(object sender, EventArgs e)
        {
            // ANEXOS
            grid1.GetAnexos().Add("Bibliotecas", null, Bibliotecas);

            // PARÂMETROS
            grid1.GetProcessos().Add("Desenhar", null, Desenhar);
            grid1.GetProcessos().Add("Compilar Tudo", null, CompilarTudo);
            grid1.GetProcessos().Add("Executar", null, Executar);
            grid1.GetProcessos().Add("Debugar", null, Debugar).Enabled = false;
            grid1.GetProcessos().Add(new ToolStripSeparator());
            grid1.GetProcessos().Add("Versionar", null, Versionar).Enabled = false;
            grid1.GetProcessos().Add("Copiar", null, Copiar).Enabled = false;
            grid1.GetProcessos().Add("Exportar", null, Exportar).Enabled = false;
            grid1.GetProcessos().Add("Importar", null, Importar).Enabled = false;
        }
        
        #endregion

        #region EVENTOS

        private void Bibliotecas(object sender, EventArgs e)
        {
            this.Bibliotecas();
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormFluxoCadastro f = new FormFluxoCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormFluxoCadastro f = new FormFluxoCadastro();
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormFluxoCadastro f = new FormFluxoCadastro();
            f.Excluir(grid1);
        }

        private void Desenhar(object sender, EventArgs e)
        {
            this.Desenhar();
        }

        private void CompilarTudo(object sender, EventArgs e)
        {
            this.CompilarTudo();
        }

        private void Executar(object sender, EventArgs e)
        {
            this.Executar();
        }

        private void Debugar(object sender, EventArgs e)
        {
            this.Debugar();
        }

        private void Versionar(object sender, EventArgs e)
        {
            this.Versionar();
        }

        public void Copiar(object sender, EventArgs e)
        {
            this.Copiar();
        }

        public void Exportar(object sender, EventArgs e)
        {
            this.Exportar();
        }

        public void Importar(object sender, EventArgs e)
        {
            this.Importar();
        }
        
        #endregion

        #region MÉTODOS

        public void Bibliotecas()
        {
            FormFluxoBibliotecaVisao f = FormFluxoBibliotecaVisao.GetInstance();
            f.MdiParent = this.MdiParent;
            f.Mostrar();
        }

        public void Desenhar()
        {
            DataRow dr = grid1.GetDataRow(true);

            if (dr != null)
            {
                AppLib.Fluxo.FormFluxo f = new AppLib.Fluxo.FormFluxo();
                f.f.Abrir(dr["NOME"].ToString());
                f.ShowDialog();
            }
        }

        public Boolean Run(Modo Modo, String Fluxo, AppLib.Windows.GridData gridData1)
        {
            return this.Run(Modo, Fluxo, Origem.GridData, new Object[] { gridData1 });
        }

        public Boolean Run(Modo Modo, String Fluxo, AppLib.Windows.FormCadastroData formCadastroData1)
        {
            return this.Run(Modo, Fluxo, Origem.FormCadastroData, new Object[] { formCadastroData1 });
        }

        public Boolean Run(Modo Modo, String Fluxo, AppLib.Windows.CampoLookup campoLookup1)
        {
            return this.Run(Modo, Fluxo, Origem.CampoLookup, new Object[] { campoLookup1 });
        }

        public Boolean Run(Modo Modo, String Fluxo, Object[] Parametros)
        {
            return this.Run(Modo, Fluxo, Origem.Manual, Parametros);
        }

        private Boolean Run(Modo Modo, String Fluxo, Origem Origem, Object[] Parametros)
        {
            try
            {
                // GERA O CÓDIGO FONTE
                AppLib.Fluxo.FormFluxo f = new AppLib.Fluxo.FormFluxo();
                AppLib.Fluxo.Principal p = new Principal(f.pictureBox1);
                CodigoFonte = p.GerarCodigoFonte();
                
                // REPLACE NO MÉTODO MAIN CASO NECESSITE DE CHAMAR OUTRO MÉTODO INTERNO
                if ( ! Fluxo.Equals("Main"))
                {
                    if (Origem == AppLib.Fluxo.Origem.GridData)
                    {
                        CodigoFonte = CodigoFonte.Replace("Main(){}", "Main(AppLib.Windows.GridData gridData1){ " + Fluxo + "(gridData1); }");
                    }

                    if (Origem == AppLib.Fluxo.Origem.FormCadastroData)
                    {
                        CodigoFonte = CodigoFonte.Replace("Main(){}", "Main(AppLib.Windows.FormCadastroData formCadastroData1){ " + Fluxo + "(formCadastroData1); }");
                    }

                    if (Origem == AppLib.Fluxo.Origem.CampoLookup)
                    {
                        CodigoFonte = CodigoFonte.Replace("Main(){}", "Main(AppLib.Windows.CampoLookup campoLookup1){ " + Fluxo + "(campoLookup1); }");
                    }

                    if (Origem == AppLib.Fluxo.Origem.Manual)
                    {
                        CodigoFonte = CodigoFonte.Replace("Main(){}", "Main(Object[] parametros1){ " + Fluxo + "(parametros1); }");
                    }
                }

                // CARREGA AS BIBLIOTECAS
                Microsoft.CSharp.CSharpCodeProvider provider = new Microsoft.CSharp.CSharpCodeProvider();
                System.CodeDom.Compiler.CompilerParameters parameters = new System.CodeDom.Compiler.CompilerParameters();

                System.Data.DataTable dtAssembly = AppLib.Context.poolConnection.Get().ExecQuery("SELECT DISTINCT ASSEMBLY FROM ZFLUXOBIBLIOTECA", new Object[] { });
                for (int i = 0; i < dtAssembly.Rows.Count; i++)
                {
                    parameters.ReferencedAssemblies.Add(dtAssembly.Rows[i]["ASSEMBLY"].ToString());
                }

                // true = memory generation
                // false = external file generation
                parameters.GenerateInMemory = true;

                // true = exe file generation
                // false = dll file generation
                parameters.GenerateExecutable = false;

                // Ignrar avisos
                parameters.WarningLevel = 0;

                // COMPILA
                System.CodeDom.Compiler.CompilerResults results = provider.CompileAssemblyFromSource(parameters, CodigoFonte);

                if (results.Errors.Count > 0)
                {
                    if (AppLib.Windows.FormMessageDefault.ShowQuestion("Ocorreu erro na compilação.\r\nGostaria de ver os detalhes?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        FormFluxoBuild formFluxoBuild1 = new FormFluxoBuild();
                        formFluxoBuild1.StartPosition = FormStartPosition.CenterScreen;
                        formFluxoBuild1.WindowState = FormWindowState.Maximized;
                        formFluxoBuild1.CodigoFonte = CodigoFonte;
                        formFluxoBuild1.results = results;
                        formFluxoBuild1.ShowDialog();

                        return false;
                    }
                }

                // EXECUTAR
                System.Reflection.Assembly assembly = results.CompiledAssembly;
                Type type = assembly.GetType("AppLibJit.AppClassJit");
                object instance = System.Activator.CreateInstance(type);

                System.Reflection.MethodInfo method = type.GetMethod("Main");
                method.Invoke(instance, Parametros);

                if (Fluxo.Equals("Main"))
                {
                    if (AppLib.Windows.FormMessageDefault.ShowQuestion("Compilado com sucesso!\r\nGostaria de aplicar esta versão?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        AppLib.ORM.Jit ZFLUXOHIST = new ORM.Jit(AppLib.Context.poolConnection.Get(), "ZFLUXOHIST");
                        ZFLUXOHIST.Set("CODIGOFONTE", CodigoFonte);
                        if (ZFLUXOHIST.Insert() != 1)
                        {
                            AppLib.Windows.FormMessageDefault.ShowError("Erro ao aplicar versão.");
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao executar o fluxo " + Fluxo +".\r\n\nDetalhe técnico: " + ex.Message);
                return false;
            }
        }

        public void CompilarTudo()
        {
            this.Run(Modo.Compilar, "Main", Origem.Manual, new Object[] { });
        }

        public void Executar()
        {
            DataRow dr = grid1.GetDataRow(true);

            if (dr != null)
            {
                this.Run(Modo.Executar, dr["NOME"].ToString(), Origem.Manual, new Object[] { });
            }
        }

        public void Debugar()
        {
            DataRow dr = grid1.GetDataRow(true);

            if (dr != null)
            {
                this.Run(Modo.Debugar, dr["NOME"].ToString(), Origem.Manual, new Object[] { });
            }
        }       

        public void Versionar()
        {

        }

        public void Copiar()
        {

        }

        public void Exportar()
        {

        }

        public void Importar()
        {

        }
        
        #endregion

    }

    public enum Modo { Compilar, Executar, Debugar }

    public enum Origem { GridData, FormCadastroData, CampoLookup, Manual }

}
