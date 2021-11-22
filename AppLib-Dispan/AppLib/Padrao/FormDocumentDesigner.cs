using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AppLib.Padrao
{
    public partial class FormDocumentDesigner : DevExpress.XtraEditors.XtraForm
    {
        public FormDocumentDesigner()
        {
            InitializeComponent();
        }

        private void FormDocumentDesigner_Load(object sender, EventArgs e)
        {

        }

        public void Mostrar()
        {
            this.WindowState = FormWindowState.Maximized;
            this.ShowDialog();
        }

        public void AddText(String texto, DevExpress.XtraRichEdit.API.Native.ParagraphStyle paragraphStyle1, DevExpress.XtraRichEdit.API.Native.CharacterStyle characterStyle1, int teclaEnter)
        {
            DevExpress.XtraRichEdit.API.Native.DocumentRange range;

            // using (var model = new DevExpress.XtraRichEdit.Model.DocumentModel())
            using (var model = new DevExpress.XtraPrinting.Native.RichText.SimpleDocumentModel(DevExpress.XtraRichEdit.Internal.RichEditDocumentFormatsDependecies.CreateDocumentFormatsDependecies()))
            {
                var rtfConverter = new DevExpress.XtraEditors.Controls.Rtf.StringEditValueToDocumentModelConverter(DevExpress.XtraRichEdit.DocumentFormat.PlainText, Encoding.Default);
                var stringConverter = new DevExpress.XtraEditors.Controls.Rtf.DocumentModelToStringConverter(DevExpress.XtraRichEdit.DocumentFormat.Rtf, Encoding.Default);

                for (int i = 0; i < teclaEnter; i++)
                {
                    texto += "\r\n";
                }

                rtfConverter.ConvertToDocumentModel(model, texto);
                string rtfSomeText = stringConverter.ConvertToEditValue(model) as string;
                range = this.richEditControl1.Document.InsertRtfText(this.richEditControl1.Document.Range.End, rtfSomeText);
            }

            DevExpress.XtraRichEdit.API.Native.ParagraphProperties paragraphProperties = this.richEditControl1.Document.BeginUpdateParagraphs(range);
            DevExpress.XtraRichEdit.API.Native.CharacterProperties characterProperties = this.richEditControl1.Document.BeginUpdateCharacters(range);

            paragraphProperties.Alignment = paragraphStyle1.Alignment;
            paragraphProperties.ContextualSpacing = paragraphStyle1.ContextualSpacing;
            paragraphProperties.FirstLineIndent = paragraphStyle1.FirstLineIndent;
            paragraphProperties.FirstLineIndentType = paragraphStyle1.FirstLineIndentType;
            paragraphProperties.KeepLinesTogether = paragraphStyle1.KeepLinesTogether;
            paragraphProperties.LeftIndent = paragraphStyle1.LeftIndent;
            paragraphProperties.LineSpacing = paragraphStyle1.LineSpacing;
            paragraphProperties.LineSpacingMultiplier = paragraphStyle1.LineSpacingMultiplier;
            paragraphProperties.LineSpacingType = paragraphStyle1.LineSpacingType;
            paragraphProperties.OutlineLevel = paragraphStyle1.OutlineLevel;
            paragraphProperties.PageBreakBefore = paragraphStyle1.PageBreakBefore;
            paragraphProperties.RightIndent = paragraphStyle1.RightIndent;
            paragraphProperties.SpacingAfter = paragraphStyle1.SpacingAfter;
            paragraphProperties.SpacingBefore = paragraphStyle1.SpacingBefore;
            paragraphProperties.SuppressHyphenation = paragraphStyle1.SuppressHyphenation;
            paragraphProperties.SuppressLineNumbers = paragraphStyle1.SuppressLineNumbers;

            characterProperties.AllCaps = characterStyle1.AllCaps;
            characterProperties.BackColor = characterStyle1.BackColor;
            characterProperties.Bold = characterStyle1.Bold;
            characterProperties.FontName = characterStyle1.FontName;
            characterProperties.FontSize = characterStyle1.FontSize;
            characterProperties.ForeColor = characterStyle1.ForeColor;
            characterProperties.Hidden = characterStyle1.Hidden;
            characterProperties.Italic = characterStyle1.Italic;
            characterProperties.Language = characterStyle1.Language;
            characterProperties.NoProof = characterStyle1.NoProof;
            characterProperties.Strikeout = characterStyle1.Strikeout;
            characterProperties.Subscript = characterStyle1.Subscript;
            characterProperties.Superscript = characterStyle1.Superscript;
            characterProperties.Underline = characterStyle1.Underline;
            characterProperties.UnderlineColor = characterStyle1.UnderlineColor;

            this.richEditControl1.Document.EndUpdateParagraphs(paragraphProperties);
            this.richEditControl1.Document.EndUpdateCharacters(characterProperties);
        }

        public void AddLine(DevExpress.XtraRichEdit.API.Native.ParagraphStyle paragraphStyle1, DevExpress.XtraRichEdit.API.Native.CharacterStyle characterStyle1, int teclaEnter)
        {
            this.AddText("", paragraphStyle1, characterStyle1, teclaEnter);
        }

        public void AddImage(System.Drawing.Image image1)
        {
            this.richEditControl1.Document.InsertImage(this.richEditControl1.Document.CaretPosition, image1);
        }

    }
}