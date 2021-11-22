using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Windows
{
    public class FormMessageDefault
    {
        /// <summary>
        /// Mostra um erro com botão OK
        /// </summary>
        /// <param name="text">Texto</param>
        /// <returns>System.Windows.Forms.MessageBoxButtons.OK</returns>
        public static System.Windows.Forms.DialogResult ShowError(string text)
        {
            return System.Windows.Forms.MessageBox.Show(text, "Erro", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        /// <summary>
        /// Mostra uma informação com botão OK
        /// </summary>
        /// <param name="text">Texto</param>
        /// <returns>System.Windows.Forms.MessageBoxButtons.OK</returns>
        public static System.Windows.Forms.DialogResult ShowInfo(string text)
        {
            return System.Windows.Forms.MessageBox.Show(text, "Informação", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        /// <summary>
        /// Mostra uma pergunta com botão YesNo
        /// </summary>
        /// <param name="text">Texto</param>
        /// <returns>System.Windows.Forms.MessageBoxButtons.YesNo</returns>
        public static System.Windows.Forms.DialogResult ShowQuestion(string text)
        {
            return System.Windows.Forms.MessageBox.Show(text, "Questão", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
        }
    }
}
