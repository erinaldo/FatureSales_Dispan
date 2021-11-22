﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppFatureClient
{
    public partial class FormPerfilCadastro : AppLib.Windows.FormCadastroData
    {
        public FormPerfilCadastro()
        {
            InitializeComponent();
        }

        private void FormPerfilCadastro_Load(object sender, EventArgs e)
        {

        }

        private void FormPerfilCadastro_AntesSalvar(object sender, EventArgs e)
        {
            if (campoDATACRIACAO.Get() == null)
            {
                campoDATACRIACAO.Set(DateTime.Now);
                campoUSUARIOCRIACAO.Set(AppLib.Context.Usuario);
            }

            campoDATAALTERACAO.Set(DateTime.Now);
            campoUSUARIOALTERACAO.Set(AppLib.Context.Usuario);

        }
    }
}
