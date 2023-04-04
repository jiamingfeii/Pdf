using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf.Extgstate;
using iText.Kernel.Pdf.Layer;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using iText.Layout;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using iText.IO.Font.Constants;
using iText.Kernel.Geom;
using System.IO;

namespace pdf.Utils
{
    public class FontUtils
    {
        public static string font11 = "Resource/font.ttc,0";
        public static string font22 = "Resource/simhei.ttf";

        private readonly string[] fontFamily;
        private readonly ImageData imageData;
        private readonly PdfFont font;
        public FontUtils()
        {
            fontFamily = new[] { "Resource/font.ttc,0" };
            imageData = ImageDataFactory.Create("Resource/2.png");
            font = PdfFontFactory.CreateFont("C:/Windows/Fonts/msyh.ttc,0");
        }
        /// <summary>
        /// 获取大标题
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Paragraph GetTitle(string str)
        {
            return GetParagraphFont(str)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(14)
                .SetMargins(46, 0, 6, 0)
                .SetFontFamily(fontFamily);
        }
        /// <summary>
        /// 获取描述标题（大标题下面那行）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Paragraph GetDescribe(string str)
        {
            return GetParagraphFont("账号现金报表 1090577014 - 12/29/2022")
                .SetFontFamily(fontFamily)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(8);
        }
        /// <summary>
        /// 获取蓝色的标题
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Paragraph GetBlueTitle(string str)
        {
            return GetParagraphFont(str)
                .SetFontFamily(fontFamily)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(8)
                .SetFontColor(new DeviceRgb(0x00, 0x58, 0xa5))
                .SetMargins(0, 0, 0, 0);
        }
        /// <summary>
        /// 绘制一条横线
        /// </summary>
        /// <returns></returns>
        public static LineSeparator GetLine(string direction)
        {
            return new LineSeparator(new SolidLine(1))
                .SetHorizontalAlignment(string.IsNullOrEmpty(direction) ? HorizontalAlignment.CENTER : Enum.Parse<HorizontalAlignment>(direction.ToUpper()));
        }
        /// <summary>
        /// 在Paragrap中添加一条直线
        /// </summary>
        /// <returns></returns>
        public static Paragraph GetParagraphLine()
        {
            return new Paragraph().SetWidth(UnitValue.CreatePercentValue(100))
                .Add(GetLine("left").SetWidth(UnitValue.CreatePercentValue(65)))
                .Add(new Tab())
                .Add(GetLine("right").SetWidth(UnitValue.CreatePercentValue(31)));
        }
        /// <summary>
        /// 在Paragrap中添加文字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Paragraph GetParagraphFont()
        {
            return new Paragraph().SetWidth(UnitValue.CreatePercentValue(100))
                .Add(GetBlueTitle("客户细节").SetWidth(UnitValue.CreatePercentValue(65)))
                .Add(new Tab())
                .Add(GetBlueTitle("余额细节").SetWidth(UnitValue.CreatePercentValue(31)));
        }
        /// <summary>
        /// 获取一个cell
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Cell GetCellTitle(string str, bool isBlock)
        {
            return new Cell().Add(GetParagraphFont(str))
                     .SetBackgroundColor(new DeviceRgb(0xe8, 0xe8, 0xe8))
                     .SetFontFamily(fontFamily)
                     .SetFontSize(9)
                     .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                     .SetPaddingLeft(6)
                     .SetBorder(isBlock ? new SolidBorder(new DeviceRgb(0xdf, 0xdf, 0xdf), 1) : new SolidBorder(new DeviceRgb(0xe8, 0xe8, 0xe8), 1));
        }
        /// <summary>
        /// 获取一个cell
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Cell GetCellValue(string str)
        {
            return new Cell().Add(GetParagraphFont(str))
                    .SetFontSize(9)
                    .SetHeight(22)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                    .SetPaddingLeft(6)
                    .SetBorder(new SolidBorder(new DeviceRgb(0xdf, 0xdf, 0xdf), 1));
        }
        /// <summary>
        /// 固定高度的空行
        /// </summary>
        /// <returns></returns>
        public Paragraph GetContentSubNull()
        {
            return GetParagraphFont("")
                    .SetFontFamily(fontFamily)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(8)
                    .SetHeight(20)
                    .SetMargins(0, 0, 0, 0);
        }
        /// <summary>
        /// 设置每个字的字体
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Paragraph GetParagraphFont(string str)
        {
            var paragraph = new Paragraph();
            if (string.IsNullOrEmpty(str))
            {
                return paragraph;
            }
            var english = PdfFontFactory.CreateFont("Resource/bpg-glaho.ttf");
            string font11 = "Resource/font.ttc,0";
            string[] fontFamily = { font11 };
            var arr = str.ToCharArray().Select(c => c.ToString()).ToArray();
            foreach (var item in arr)
            {
                var isChinese = Regex.IsMatch(item, @"^[\u4e00-\u9fa5]+$");
                var isNumberOrEnglish = Regex.IsMatch(item, @"^[0-9a-zA-Z]+$");
                var text = new Text(item);
                if (isChinese)
                {
                    text.SetFontFamily(fontFamily);
                }
                else if (isNumberOrEnglish)
                {
                    text.SetFont(english);
                }
                paragraph.Add(text);
            }
            return paragraph;
        }
        /// <summary>
        /// 添加水印和页码
        /// </summary>
        /// <param name="pdf"></param>
        public void SetLogoAndPage(PdfDocument pdf)
        {
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
        }


        public void AddPageNumbersToFooter(string filePath, bool isNeedTime = false, bool isAddLogo = false)
        {
            DateTime now = DateTime.Now;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(now.ToUniversalTime(), cstZone);
            string output = cstTime.ToString("hh:mm:ss ' (GMT'zzz')'");
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(filePath), new PdfWriter(filePath.Substring(4)));
            Document doc = new Document(pdfDoc);
            Image logoImage = new Image(imageData).SetWidth(30).SetHeight(30);
            int totalPages = pdfDoc.GetNumberOfPages();

            for (int i = 1; i <= totalPages; i++)
            {
                Rectangle pageSize = pdfDoc.GetPage(i).GetPageSize();
                var footerTxt = $"页码:{i}/{totalPages}";
                if (isNeedTime)
                {
                    footerTxt +="                                                                                                               打印" + output + "Beijing, Chongqing, Hong Kong, Urumqi节的" + DateTime.Now.ToString("MM/dd/yyyy");
                }
                Paragraph footer = new Paragraph(footerTxt)
                    .SetFixedPosition(20, 6, pageSize.GetWidth())
                    .SetFont(font)
                    .SetFontSize(8);

                if (i > 1)
                {
                    doc.Add(new AreaBreak());
                }
                if (isAddLogo)
                {
                    doc.Add(logoImage.SetFixedPosition(i, 21, pageSize.GetHeight() - (i == 1 ? 60 : 30)));
                }
                doc.Add(footer);
            }
            doc.Close();
            File.Delete(filePath);

        }


        public static TValue GetValueOrDefault<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }
        public static TKey GetKeyOrDefault<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TValue value, TKey defaultKey = default)
        {
            foreach (var pair in dictionary)
            {
                if (EqualityComparer<TValue>.Default.Equals(pair.Value, value))
                {
                    return pair.Key;
                }
            }
            return defaultKey;
        }
    }
}
