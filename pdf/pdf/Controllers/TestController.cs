
using Microsoft.AspNetCore.Mvc;
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
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Kernel.Pdf.Layer;
using iText.Layout.Borders;
using pdf.Service;
using System;

namespace pdf.Controllers
{
    [ApiController]
    [Route("test/[action]")]
    public class TestController : Controller
    {
        PDFGenerator pdfGenerator =new PDFGenerator();
        [HttpPost]
        public IActionResult xjbb()
        {
            string json = @"[[{""账号"":""1090577014""},{""声明日期"":""12/29/2022""},{""总信用额"":""1,381,302.92""},{""分行名称"":""CITIBANK N.A. HONG KONG""},{""客户名"":""HK XINYUN LOGISTICS TRADING LTD""},{""当前/关闭可用余额"":""1,397,307.79""},{""分行号"":""712""},{""借记计数"":""0""},{""打开总帐余额"":""3,402,643,867""},{""账户名"":""HONG KONG XINYUN LOGISTICS""},{""信用计数"":""1""},{""打开可用余额"":""3,402,643,867""},{""银行名称"":""CITIBANK""},{""计算余额"":""否""},{""当前/关闭总帐余额"":""1,397,307.79""},{""客户名称"":""HONG KONG XINYUN LOGISTICS""},{""IBAN号码"":""--""},{""总借方金额"":""0.00""},{""客户号"":""090577""},{"""":""""},{""净金额"":""1,397,307.79""}],[{""交易参考 "":""20221229135123319290"",""客户参考"":""202OL22122902912"",""起息日"":""12/29/2022"",""进入日期"":""12/29/2022"",""货币"":""USD"",""金额"":""100,000,000"",""产品类型"":""汇款""}]]";
            pdfGenerator.GetXJBB(json, @"TempDocument/" + Guid.NewGuid().ToString()+".pdf");
            return File("application/pdf", "using-iText7.pdf");
        }

        [HttpPost]
        public IActionResult jyxj()
        {
            string json = @"[[{""账号"":""1090577014""},{""金额"":""1,381,302.92""},{""货币"":""USD""},{""银行名称"":""CITIBANK""},{""银行参考"":""3402643867""},{""起息日"":""12/29/2022""},{""分行名称"":""CITIBANK N.A. HONG KONG""},{""分行号"":""712""},{""进入日期"":""12/29/2022""},{""账户名"":""HONG KONG XINYUN LOGISTICS""},{""声明日期"":""12/29/2022""},{""IBAN号码"":""--""},{""客户名称"":""HONG KONG XINYUN LOGISTICS""},{""客户号"":""090577""},{""客户参考"":""202OL22122902912""},{""分行传真ID号"":""--""},{"""":""""},{""账户类型"":""Current Account""}],[{""产品类型"":""汇款""},{""付款细节"":""/SPRO/01""},{""汇款银行名称/地址"":""STANDARD CHARTERED BANK (HONG KONG)""},{""汇款银行名称/地址"":""FLOOR 32, STANDARD CHARTERED BANKBU""},{""汇款银行名称/地址"":""ILDING 4-4A DES VOEUX ROAD""},{""汇款银行名称/地址"":""CENTRAL""},{""出票人账户/识别号"":""000044719208419""},{""出票人名称/地址"":""STILL QUALITY LIMITED""},{""出票人名称/地址"":""RM 512, 5/F, NEW MANDARIN PLAZA TOW""},{""出票人名称/地址"":""ER B, 14 SCIENCE MUSEUM ROAD, TSIM""},{""出票人名称/地址"":""SHA TSUI EAST,/HONG KONG""},{""受益人账户/识别号"":""1090577014""},{""受益人名称/地址"":""HONG KONG XINYUN LOGISTICS TRADING""},{""受益人名称/地址"":""LIMITED""},{""受益人名称/地址"":""/HONG KONG""},{""原始金额"":""1,381,302.92""},{""原始货币"":""USD""},{""日期填迟时间"":""15:40:13""}]]";
            pdfGenerator.GetJYXJ(json, @"TempDocument/" + Guid.NewGuid().ToString() + ".pdf");
            return File("application/pdf", "using-iText7.pdf");
        }

        [HttpPost]
        public IActionResult dzd()
        {
            string json = @"[[{""您的邮寄地址"":""HONG KONG XINYUN LOGISTICS \n HK XINYUN LOGISTICS TRADING LTD\nRM501 BLKA TOWER4 SHENZHEN\nSOFTWARE INDUSTRIAL BASE NANSHAN\nSHENZHEN CHINA 518000""},{""从"":""12/26/2022""},{""期初余额"":""USD 16,004.87""},{""至"":""03/10/2023""},{""期末余额"":""USD 296,758.43""},{""账号"":""1090577014""},{""货币"":""USD""},{""账户类型"":""USDNIB""}],[{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""},{""日期"":""12/29/2022"",""明细描述"":""INCOMING RTGS TRANSFER\n参考序号:3402643867\n您的参考序号:202OL22122902912\n起息日:12/29/2022\nB/O STILL QUALITY LIMITEDRM 512, 5/F, N"",""借记"":""100,00,000"",""贷记"":""100,000,000"",""期末余额"":""100,000,000""}]]";
            pdfGenerator.GetDZD(json, @"TempDocument/" + Guid.NewGuid().ToString() + ".pdf");
            try
            {
                _ = File("application/pdf", "using-iText7.pdf");
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
