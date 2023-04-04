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
using System.Threading;
using iText.Kernel.Events;
using iText.IO.Font.Constants;

namespace pdf.Service
{
    public class PDFGenerator
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
        FontUtils fontUtils= new FontUtils();
        /// <summary>
        /// 获取现金报表
        /// </summary>
        /// <param name="json"></param>
        /// <param name="dest"></param>
        public  void GetXJBB(string json, string dest)
        {
            List<List<Dictionary<string, string>>> result = PDFGenerator.ParseJson(json);
            List<Dictionary<string, string>> collection1 = result[0];
            List<Dictionary<string, string>> collection2 = result[1];
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


            Table table = new Table(new float[] { 1f, 1f, 0.97f })
                .SetWidth(UnitValue.CreatePercentValue(100))
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

            Table table1 = new Table(new float[] { 2f, 2f, 1f, 1f, 0.5f, 1.1f, 1f })
                .SetWidth(UnitValue.CreatePercentValue(100))
                .SetFixedLayout()
                .SetFontSize(9)
                .SetFontFamily(fontFamily)
                .SetBorder(Border.NO_BORDER)
                .SetKeepTogether(false)
                .SetMarginTop(8)
                .SetBorder(new SolidBorder(ColorConstants.WHITE, 0));

            string[] keysArray = collection2.SelectMany(d => d.Keys).Distinct().ToArray();
            foreach (var item in keysArray)
            {
                table1.AddCell(fontUtils.GetCellTitle(item, true));
            }
            foreach (var pair in collection2)
            {
                foreach(var item in keysArray)
                {
                    string cellTxt = FontUtils.GetValueOrDefault(pair, item);
                    var cell = FontUtils.GetCellValue(cellTxt);
                    table1.AddCell(cell);
                }
            }

            document.Add(fontUtils.GetTitle("现金报表").SetMargins(50, 0, 6, 0));
            document.Add(fontUtils.GetDescribe("账号现金报表 1090577014 - 12/29/2022").SetMargins(8, 0, 4, 0));
            document.Add(FontUtils.GetParagraphLine().SetMarginBottom(-10));
            document.Add(fontUtils.GetParagraphFont());
            document.Add(FontUtils.GetParagraphLine().SetMarginTop(-14));
            document.Add(table);
            document.Add(FontUtils.GetLine("center").SetMarginTop(10));
            document.Add(fontUtils.GetBlueTitle("交易"));
            document.Add(FontUtils.GetLine("center"));
            document.Add(table1);
            document.Close();
            fontUtils.AddPageNumbersToFooter(dest);
        }
        /// <summary>
        /// 获取交易细节
        /// </summary>
        /// <param name="json"></param>
        /// <param name="dest"></param>
        public  void GetJYXJ(string json, string dest)
        {
            List<List<Dictionary<string, string>>> result = PDFGenerator.ParseJson(json);
            List<Dictionary<string, string>> collection1 = result[0];
            List<Dictionary<string, string>> collection2 = result[1];
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

            Table table1 = new Table(new float[] { 1f, 1f })
                .SetWidth(UnitValue.CreatePercentValue(100))
                .SetFixedLayout()
                .SetFontSize(9)
                .SetFontFamily(fontFamily)
                .SetBorder(Border.NO_BORDER)
                .SetKeepTogether(false)
                .SetMarginTop(18)
                .SetBorder(new SolidBorder(ColorConstants.WHITE, 0));

            table1.AddCell(fontUtils.GetCellTitle("字段名称",false));
            table1.AddCell(fontUtils.GetCellTitle("值", false));

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

            document.Add(fontUtils.GetTitle("交易细节"));
            document.Add(FontUtils.GetLine("center"));
            document.Add(fontUtils.GetBlueTitle("交易细节 For 1090577014"));
            document.Add(FontUtils.GetLine("center"));
            document.Add(table);
            document.Add(FontUtils.GetLine("center")); 
            document.Add(fontUtils.GetBlueTitle("附加细节"));
            document.Add(FontUtils.GetLine("center"));
            document.Add(table1);
            document.Add(p2);
            document.Close();
            fontUtils.AddPageNumbersToFooter(dest);
        }
        /// <summary>
        /// 获取对账单
        /// </summary>
        /// <param name="json"></param>
        /// <param name="dest"></param>
        public  void GetDZD(string json, string dest)
        {
            List<List<Dictionary<string, string>>> result = PDFGenerator.ParseJson(json);
            List<Dictionary<string, string>> collection1 = result[0];
            List<Dictionary<string, string>> collection2 = result[1];
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


            string key = "您的邮寄地址";
            string value = FontUtils.GetValueOrDefault(collection1[0], key);
            var contentSub1 = FontUtils.GetParagraphFont(value)
                .SetFontFamily(fontFamily)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(8)
                .SetPaddingLeft(6)
                .SetMargins(0, 0, 0, 0);

            //var contentSubNull = FontUtils.GetParagraphFont("")
            //  .SetFontFamily(fontFamily)
            //  .SetTextAlignment(TextAlignment.LEFT)
            //  .SetFontSize(8)
            //  .SetHeight(20)
            //  .SetMargins(0, 0, 0, 0);

            Table summarytable = new Table(new float[] { 1f, 1.2f, 7.8f })
                                 .SetWidth(UnitValue.CreatePercentValue(100))
                                 .SetFixedLayout()
                                 .SetFontSize(9)
                                 .SetFontFamily(fontFamily)
                                 .SetKeepTogether(false)
                                 .SetMarginTop(18)
                                 .SetBorder(new SolidBorder(ColorConstants.WHITE, 0));

            List<string> summaryList = new List<string>
            {
                "从      ：",
                FontUtils.GetValueOrDefault(collection1[1], "从"),
                " ",
                "期初余额：",
                FontUtils.GetValueOrDefault(collection1[2], "期初余额"),
                " ",
                "至      ：",
                FontUtils.GetValueOrDefault(collection1[3], "至"),
                " ",
                "期末余额：",
                FontUtils.GetValueOrDefault(collection1[4], "期末余额"),
                " ",
                " ",
                " ",
                " "
            };

            foreach (var item in summaryList)
            {
                summarytable.AddCell(new Cell().Add(FontUtils.GetParagraphFont(item))
                             .SetFontFamily(fontFamily)
                             .SetBorder(Border.NO_BORDER)
                             .SetFontSize(9)
                             .SetHeight(16)
                             .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                             .SetBorder(Border.NO_BORDER));
            }



            Table table2 = new Table(new float[] { 1f, 1f, 1f })
                          .SetWidth(UnitValue.CreatePercentValue(100))
                          .SetFixedLayout()
                          .SetFontSize(9)
                          .SetFontFamily(fontFamily)
                          .SetKeepTogether(false)
                          .SetMarginTop(18)
                          .SetBorder(new SolidBorder(ColorConstants.WHITE, 0));


            foreach (var pair in collection1.TakeLast(3))
            {
                var cell = new Cell()
                    .SetFontSize(9)
                    .SetBorder(Border.NO_BORDER)
                    .SetHeight(26)
                    .Add(FontUtils.GetParagraphFont(pair.Keys.ToArray()[0]).SetMultipliedLeading(1f))
                    .Add(FontUtils.GetParagraphFont(pair.Values.ToArray()[0]).SetMultipliedLeading(1f));
                table2.AddCell(cell);
            }

            Table table1 = new Table(new float[] { 1f, 2f, 1f, 1f, 1f })
                .SetWidth(UnitValue.CreatePercentValue(100))
                .SetFixedLayout()
                .SetFontSize(9)
                .SetFontFamily(fontFamily)
                .SetBorder(Border.NO_BORDER)
                .SetKeepTogether(false)
                .SetMarginTop(18)
                .SetBorder(new SolidBorder(ColorConstants.WHITE, 0));

            string[] keysArray = collection2.SelectMany(d => d.Keys).Distinct().ToArray();
            foreach (var item in keysArray)
            {
                table1.AddCell(fontUtils.GetCellTitle(item, true));
            }
            foreach (var pair in collection2)
            {
                foreach (var item in keysArray)
                {
                    string cellTxt = FontUtils.GetValueOrDefault(pair, item);
                    var cell = FontUtils.GetCellValue(cellTxt).SetHeight(item == "明细描述" ? 88 : 22);
                    table1.AddCell(cell);
                }
            }

            document.Add(fontUtils.GetTitle("现金对账单"));//标题
            document.Add(fontUtils.GetContentSubNull());//固定高度的空行
            document.Add(FontUtils.GetLine("left").SetWidth(pageSize.GetWidth() / 2.1f)); //分割线
            document.Add(fontUtils.GetBlueTitle("您的邮寄地址")); //您的邮寄地址
            document.Add(FontUtils.GetLine("left").SetWidth(pageSize.GetWidth() / 2.1f)); //分割线
            document.Add(fontUtils.GetContentSubNull()); //固定高度的空行
            document.Add(contentSub1);//您的邮寄地址内容
            document.Add(fontUtils.GetContentSubNull());//固定高度的空行

            document.Add(FontUtils.GetLine("left").SetWidth(pageSize.GetWidth() / 2.2f)); //分割线
            document.Add(fontUtils.GetBlueTitle("账单摘要"));
            document.Add(FontUtils.GetLine("left").SetWidth(pageSize.GetWidth() / 2.2f)); //分割线
            document.Add(summarytable); //账单摘要内容
            document.Add(fontUtils.GetContentSubNull());//固定高度的空行

            document.Add(FontUtils.GetLine("center"));//分割线
            document.Add(fontUtils.GetBlueTitle("账单明细"));
            document.Add(FontUtils.GetLine("center"));//分割线
            document.Add(table2);// 账单明细内容
            document.Add(fontUtils.GetContentSubNull());//固定高度的空行

            document.Add(FontUtils.GetLine("center"));//分割线
            document.Add(fontUtils.GetBlueTitle("交易"));
            document.Add(FontUtils.GetLine("center")); //分割线
            document.Add(table1); //交易内容
            document.Close();
            fontUtils.AddPageNumbersToFooter(dest,true,true);
            
        }
    }
}
