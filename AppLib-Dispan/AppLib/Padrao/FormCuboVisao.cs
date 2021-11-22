using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppLib.Padrao
{
    public partial class FormCuboVisao : AppLib.Windows.FormVisao
    {
        private static FormCuboVisao _instance = null;

        public static FormCuboVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormCuboVisao();

            return _instance;
        }

        private void FormCuboVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormCuboVisao()
        {
            InitializeComponent();
        }

        private void FormCuboVisao_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Visualizar", null, Visualizar);
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormCuboCadastro f = new FormCuboCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormCuboCadastro f = new FormCuboCadastro();
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormCuboCadastro f = new FormCuboCadastro();
            f.Excluir(grid1);
        }

        private void Visualizar(object sender, EventArgs e)
        {
            System.Data.DataRow dr = grid1.GetDataRow();

            if (dr != null)
            {
                int IDCUBO = int.Parse(dr["IDCUBO"].ToString());

                if (new AppLib.Security.Access().CuboVisualizar(grid1.Conexao, IDCUBO, AppLib.Context.Perfil))
                {
                    FormCuboViewer f = new FormCuboViewer();
                    f.gridCubo1.IDCUBO = IDCUBO;
                    f.gridCubo1.NomeGrid = "Cubo" + IDCUBO;
                    f.ShowDialog();
                }
            }
        }

    }
}
