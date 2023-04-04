
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using pdf.Service;
using System.Diagnostics;
using pdf.Model;
using System.Collections.Generic;
using static pdf.Service.PDFGenerator;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Pdfa;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using Microsoft.AspNetCore.Mvc.RazorPages;
using iText.Kernel.Pdf.Extgstate;
using iText.Kernel.Pdf.Layer;
using iText.Commons.Utils;
using iText.Html2pdf.Resolver.Font;
using iText.Layout.Font;
using iText.Layout.Borders;
using iText.IO.Font.Constants;

namespace pdf.Controllers
{
	[ApiController]
	[Route("controller/[action]")]
	public class HomeController : Controller
	{
		[HttpPost]
		public IActionResult pdf()
		{
			string json = @"[[{""账号"":""1090577014""},{""金额"":""1,381,302.92""},{""货币"":""USD""},{""银行名称"":""CITIBANK""},{""银行参考"":""3402643867""},{""起息日"":""12/29/2022""},{""分行名称"":""CITIBANK N.A. HONG KONG""},{""分行号"":""712""},{""进入日期"":""12/29/2022""},{""账户名"":""HONG KONG XINYUN LOGISTICS""},{""声明日期"":""12/29/2022""},{""IBAN号码"":""--""},{""客户名称"":""HONG KONG XINYUN LOGISTICS""},{""客户号"":""090577""},{""客户参考"":""202OL22122902912""},{""分行传真ID号"":""--""}],[{""产品类型"":""汇款""},{""付款细节"":""/SPRO/01""},{""汇款银行名称/地址"":""STANDARD CHARTERED BANK (HONG KONG)""},{""汇款银行名称/地址"":""FLOOR 32, STANDARD CHARTERED BANKBU""},{""汇款银行名称/地址"":""ILDING 4-4A DES VOEUX ROAD""},{""汇款银行名称/地址"":""CENTRAL""},{""出票人账户/识别号"":""000044719208419""},{""出票人名称/地址"":""STILL QUALITY LIMITED""},{""出票人名称/地址"":""RM 512, 5/F, NEW MANDARIN PLAZA TOW""},{""出票人名称/地址"":""ER B, 14 SCIENCE MUSEUM ROAD, TSIM""},{""出票人名称/地址"":""SHA TSUI EAST,/HONG KONG""},{""受益人账户/识别号"":""1090577014""},{""受益人名称/地址"":""HONG KONG XINYUN LOGISTICS TRADING""},{""受益人名称/地址"":""LIMITED""},{""受益人名称/地址"":""/HONG KONG""},{""原始金额"":""1,381,302.92""},{""原始货币"":""USD""},{""日期填迟时间"":""15:40:13""}]]";

			List<List<Dictionary<string, string>>> result = JsonConvert.DeserializeObject<List<List<Dictionary<string, string>>>>(json);

			List<Dictionary<string, string>> collection1 = new List<Dictionary<string, string>>();
			List<Dictionary<string, string>> collection2 = new List<Dictionary<string, string>>();

			foreach (var innerList in result)
			{
				if (innerList == result[0])
				{
					collection1.AddRange(innerList);
				}
				else if (innerList == result[1])
				{
					collection2.AddRange(innerList);
				}
			}


			string dest = "output.pdf";

			var writer = new PdfWriter(dest, new WriterProperties().AddXmpMetadata());

			//设置字体
			var font = PdfFontFactory.CreateFont("C:/Windows/Fonts/msyh.ttc,0");

			PdfDocument pdf = new PdfDocument(writer);
			//设置尺寸25.4cm*29.7cm
			var pageSize = new PageSize(7.1998f * 100, 8.41865f * 100);
			pdf.SetDefaultPageSize(pageSize);

			Document document = new Document(pdf);
			//设置页面边距
			document.SetMargins(20, 20, 20, 20);

			Paragraph title = new Paragraph("交易细节")
				.SetFont(font)
				.SetTextAlignment(TextAlignment.LEFT)
				.SetFontSize(14)
				.SetMargins(46, 0, 6, 0);


			LineSeparator line = new LineSeparator(new SolidLine(1))
		   .SetHorizontalAlignment(HorizontalAlignment.CENTER);

			Paragraph p = new Paragraph("交易细节 For 1090577014")
				.SetFont(font)
				.SetTextAlignment(TextAlignment.LEFT)
				.SetFontSize(8)
				.SetFontColor(new DeviceRgb(0x00, 0x58, 0xa5))
				.SetMargins(0, 0, 0, 0);

            // 创建一个三列表格，无边框
            Table table = new Table(new float[] { 0.3f, 0.4f, 0.3f })
                .SetWidth(UnitValue.CreatePercentValue(85))
                .SetFixedLayout()
                .SetFontSize(9)
                .SetBorder(Border.NO_BORDER)
                .SetFont(font)
				.SetMarginTop(14); 

            // 将数据添加到表格中
            foreach (var pair in collection1)
            {
                var cell = new Cell().Add(new Paragraph($"{pair.Keys.ToArray()[0]}\n{pair.Values.ToArray()[0]}")
												.SetMultipliedLeading(1f))
                                      .SetFont(font)
                                      .SetFontSize(9)
                                      .SetBorder(Border.NO_BORDER)
                                      .SetHeight(28);
                table.AddCell(cell);
            }

            foreach (Cell cell in table.GetChildren())
            {
                cell.SetBorder(Border.NO_BORDER);
				cell.SetMarginTop(20);
                cell.SetBackgroundColor(ColorConstants.WHITE);
            }

			table.SetMarginBottom(20);

            Paragraph p1 = new Paragraph("交易细节 For 1090577014")
                .SetFont(font)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(8)
                .SetFontColor(new DeviceRgb(0x00, 0x58, 0xa5))
                .SetMargins(0, 0, 0, 0);

			// 创建一个两列表格，无边框
			Table table1 = new Table(new float[] { 1f, 1f })
					.SetWidth(UnitValue.CreatePercentValue(100))
					.SetFixedLayout()
					.SetFontSize(9)
					.SetFont(font)
					.SetMarginTop(18)
					.SetBorder(Border.NO_BORDER)
                    .SetKeepTogether(false);

            table1.AddCell(new Cell().Add(new Paragraph("字段名称").SetBold()).SetBackgroundColor(new DeviceRgb(0xe8, 0xe8, 0xe8))
                              .SetVerticalAlignment(VerticalAlignment.MIDDLE)
							  .SetBold()
							  .SetPaddingLeft(6));
            table1.AddCell(new Cell().Add(new Paragraph("值").SetBold()).SetBackgroundColor(new DeviceRgb(0xe8, 0xe8, 0xe8))
                              .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                              .SetBold()
                              .SetPaddingLeft(6));

            // 将数据添加到表格中
            foreach (var pair in collection2)
            {
                var cell = new Cell().Add(new Paragraph($"{pair.Keys.ToArray()[0]}"))
                                      .SetFont(font)
                                      .SetFontSize(9)
                                      .SetHeight(22)
                                      .SetVerticalAlignment(VerticalAlignment.MIDDLE)
									  .SetPaddingLeft(6);
                var cel2 = new Cell().Add(new Paragraph($"{pair.Values.ToArray()[0]}"))
                                     .SetFont(font)
                                     .SetFontSize(9)
                                     .SetHeight(22)
                                     .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                     .SetPaddingLeft(6);
                table1.AddCell(cell);
                table1.AddCell(cel2);
            }

            document.Add(title);
			document.Add(line);
			document.Add(p);
			document.Add(line);
            document.Add(table);
            document.Add(line);
            document.Add(p1);
            document.Add(line);
            document.Add(table1);






   //         //创建背景图片
   //         ImageData imageData = ImageDataFactory.Create("Resource/2.png");
			////为每个页面添加背景图片
			//for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
			//{

			//	PdfPage page = pdf.GetPage(i);
			//	PdfCanvas canvas = new PdfCanvas(page);
			//	canvas.SaveState();
			//	canvas.BeginLayer(new PdfLayer("Background", pdf));
			//	canvas.SetExtGState(new PdfExtGState().SetFillOpacity(0.5f));
			//	canvas.AddImageAt(imageData, 0, page.GetPageSize().GetTop() - imageData.GetHeight(), false);
			//	canvas.EndLayer();
			//	canvas.RestoreState();
			//}

			//获取总页数
			int pageCount = pdf.GetNumberOfPages();
			//为每个页面添加页码
			for (int i = 1; i <= pageCount; i++)
			{
				//获取当前页面
				PdfPage page = pdf.GetPage(i);
				//创建Canvas对象
				PdfCanvas canvas = new PdfCanvas(page);
				//保存当前状态
				canvas.SaveState();
				//开始一个新的文本对象
				canvas.BeginText();
				//设置字体和字号
				canvas.SetFontAndSize(font, 10);
				//将原点移动到页面的左下角
				canvas.MoveText(20, 8);
				//添加文本
				canvas.ShowText($"页码:{i}/{pageCount}");
				//结束文本对象
				canvas.EndText();
				//恢复之前的状态
				canvas.RestoreState();
			}

			document.Close();
			return File("application/pdf", "using-iText7.pdf");
        }
	}
}
