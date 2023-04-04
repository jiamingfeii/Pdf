using iText.IO.Font.Constants;
using iText.Kernel.Font;

namespace pdf.Utils
{
    public class DocumentUtils
    {
        public static PdfFont SetFont() 
        {
            return PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
    }
}
}
