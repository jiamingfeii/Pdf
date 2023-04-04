using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Layout.Borders;
using iText.IO.Font;
using System;
using iText.Layout.Font;
using System.Text.RegularExpressions;
using pdf.Utils;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Layer;
using iText.IO.Image;
using iText.Kernel.Pdf.Extgstate;

namespace pdf.Service
{
    public class PDFGenerator1
    {
        public static List<List<Dictionary<string, string>>> ParseJson(string json)
        {
            return JsonConvert.DeserializeObject<List<List<Dictionary<string, string>>>>(json);
        }

        public static string font11 = "Resource/font.ttc,0";
        public static string font22 = "Resource/simhei.ttf";
        public static string[] fontFamily = { font11 };
        public static ImageData imageData = ImageDataFactory.Create("Resource/2.png");
        public static PdfFont font = PdfFontFactory.CreateFont("C:/Windows/Fonts/msyh.ttc,0");
        FontUtils fontUtils = new FontUtils();
        //现金报表pdf
        public  void GetXJBB(List<Dictionary<string, string>> collection1, List<Dictionary<string, string>> collection2, string dest)
        {
            var writer = new PdfWriter(dest, new WriterProperties().AddXmpMetadata());
            PdfDocument pdf = new PdfDocument(writer);
            //1f*100=3.5278cm
            var pageSize = new PageSize(10.8f * 100, 5.95f * 100);
            pdf.SetDefaultPageSize(pageSize);
            Document document = new Document(pdf);
            document.SetMargins(20, 20, 20, 20);
            PdfFontFactory.Register(@"Resource/font.ttc,0", "font");
            FontProvider fontProvider = new FontProvider();
            fontProvider.AddFont(font11);
            fontProvider.AddFont(font22);
            document.SetFontProvider(fontProvider);

            document.Add(fontUtils.GetTitle("现金报表"));
            document.Add(fontUtils.GetDescribe("账号现金报表 1090577014 - 12/29/2022").SetMargins(14, 0, 0, 0));
            document.Add(FontUtils.GetLine(""));
            document.Add(fontUtils.GetBlueTitle("客户细节"));
            document.Add(FontUtils.GetLine(""));

            document.Close();


        }
        //交易细节pdf
        public static void GetJYXJ(List<Dictionary<string, string>> collection1, List<Dictionary<string, string>> collection2, string dest)
        {
            var writer = new PdfWriter(dest, new WriterProperties().AddXmpMetadata());
            PdfDocument pdf = new PdfDocument(writer);
            var pageSize = new PageSize(7.1998f * 100, 8.41865f * 100);
            pdf.SetDefaultPageSize(pageSize);
            Document document = new Document(pdf);
            document.SetMargins(20, 20, 20, 20);
            PdfFontFactory.Register(@"Resource/font.ttc,0", "font");
            FontProvider fontProvider = new FontProvider();
            fontProvider.AddFont(font11);
            fontProvider.AddFont(font22);
            document.SetFontProvider(fontProvider);

            Paragraph title = FontUtils.GetParagraphFont("交易细节")
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(14)
                            .SetMargins(46, 0, 6, 0)
                            .SetFontFamily(fontFamily);

            LineSeparator line = new LineSeparator(new SolidLine(1))
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            Paragraph p = FontUtils.GetParagraphFont("交易细节 For 1090577014")
                .SetFontFamily(fontFamily)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(8)
                .SetFontColor(new DeviceRgb(0x00, 0x58, 0xa5))
                .SetMargins(0, 0, 0, 0);


            Table table = new Table(new float[] { 1f, 1f, 1f })
                .SetWidth(UnitValue.CreatePercentValue(88))
                .SetFixedLayout()
                .SetFontSize(9)
                .SetBorder(Border.NO_BORDER)
                .SetFontFamily(fontFamily)
                .SetMarginTop(14);

            foreach (var pair in collection1)
            {
                var cell = new Cell()
                    .SetFontSize(9)
                    .SetBorder(Border.NO_BORDER)
                    .SetHeight(26)
                    .Add(FontUtils.GetParagraphFont(pair.Keys.ToArray()[0]).SetMultipliedLeading(1f))
                    .Add(FontUtils.GetParagraphFont(pair.Values.ToArray()[0]).SetMultipliedLeading(1f));
                table.AddCell(cell);
            }

            foreach (Cell cell in table.GetChildren())
            {
                cell.SetBorder(Border.NO_BORDER);
                cell.SetMarginTop(20);
                cell.SetBackgroundColor(ColorConstants.WHITE);
            }

            table.SetMarginBottom(20);

            Paragraph p1 = FontUtils.GetParagraphFont("附加细节")
                .SetFontFamily(fontFamily)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(8)
                .SetFontColor(new DeviceRgb(0x00, 0x58, 0xa5))
                .SetMargins(0, 0, 0, 0);

            Table table1 = new Table(new float[] { 1f, 1f })
                .SetWidth(UnitValue.CreatePercentValue(100))
                .SetFixedLayout()
                .SetFontSize(9)
                .SetFontFamily(fontFamily)
                .SetBorder(Border.NO_BORDER)
                .SetKeepTogether(false)
                .SetMarginTop(18)
                .SetBorder(new SolidBorder(ColorConstants.WHITE, 0));

            table1.AddCell(new Cell().Add(FontUtils.GetParagraphFont("字段名称"))
                    .SetBackgroundColor(new DeviceRgb(0xe8, 0xe8, 0xe8))
                    .SetFontFamily(fontFamily)
                    .SetBold()
                    .SetFontSize(9)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                    .SetPaddingLeft(6)
                    .SetBorder(new SolidBorder(new DeviceRgb(0xe8, 0xe8, 0xe8), 1)));

            table1.AddCell(new Cell().Add(new Paragraph("值"))
                    .SetBackgroundColor(new DeviceRgb(0xe8, 0xe8, 0xe8))
                    .SetFontFamily(fontFamily)
                    .SetBold()
                    .SetFontSize(9)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                    .SetPaddingLeft(6)
                    .SetBorder(new SolidBorder(new DeviceRgb(0xe8, 0xe8, 0xe8), 1)));

            foreach (var pair in collection2)
            {
                var cell = new Cell().Add(FontUtils.GetParagraphFont(pair.Keys.ToArray()[0]))
                    .SetFontSize(9)
                    .SetHeight(22)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                    .SetPaddingLeft(6)
                    .SetBorder(new SolidBorder(new DeviceRgb(0xdf, 0xdf, 0xdf), 1));

                var cel2 = new Cell().Add(FontUtils.GetParagraphFont(pair.Values.ToArray()[0]))
                    .SetFontSize(9)
                    .SetHeight(22)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                    .SetPaddingLeft(6)
                    .SetBorder(new SolidBorder(new DeviceRgb(0xdf, 0xdf, 0xdf), 1));

                table1.AddCell(cell);
                table1.AddCell(cel2);
            }
            Paragraph p2 = FontUtils.GetParagraphFont("法律文本\n我们已经贷记您的账户。")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(8)
                .SetFontColor(new DeviceRgb(0x00, 0x58, 0xa5))
                .SetMargins(0, 0, 0, 0);

            document.Add(title);
            document.Add(line);
            document.Add(p);
            document.Add(line);
            document.Add(table);
            document.Add(line);
            document.Add(p1);
            document.Add(line);
            document.Add(table1);
            document.Add(p2);

            int pageCount = pdf.GetNumberOfPages();
            for (int i = 1; i <= pageCount; i++)
            {
                PdfPage page = pdf.GetPage(i);
                PdfCanvas canvas = new PdfCanvas(page);
                canvas.SaveState();
                canvas.BeginLayer(new PdfLayer("Background", pdf));
                canvas.SetExtGState(new PdfExtGState().SetFillOpacity(0.5f));
                canvas.AddImageAt(imageData, 0, page.GetPageSize().GetTop() - imageData.GetHeight(), false);
                canvas.EndLayer();
                canvas.RestoreState();
            }

            for (int i = 1; i <= pageCount; i++)
            {
                PdfPage page = pdf.GetPage(i);
                PdfCanvas canvas = new PdfCanvas(page);
                canvas.SaveState();
                canvas.BeginText();
                canvas.SetFontAndSize(font, 10);
                canvas.MoveText(20, 8);
                canvas.ShowText($"页码:{i}/{pageCount}");
                canvas.EndText();
                canvas.RestoreState();
            }

            document.Close();
        }

    }
}
