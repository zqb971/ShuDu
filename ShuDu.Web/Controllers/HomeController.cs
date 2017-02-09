using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShuDu.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string nd,string y ,string  m,string d )
        {
            
            ViewBag.ShuDuHtml = new MvcHtmlString(GetShuDuHtml(nd, y, m,d));
            return View();
        }

        private string GetShuDuHtml(string nd, string y, string m, string d)
        {
            var temp = HttpHelper.GetRequest("http://cn.sudokupuzzle.org/online2.php?nd=" + nd + "&y=" + y + "&m=" + m + "&d=" + d);
            var index = temp.IndexOf("<link");
            temp = temp.Substring(index, temp.Length - index);
            index = temp.LastIndexOf("</script>");
            temp = temp.Substring(0, index + 9);
            temp = temp.Replace("online2.php", "");
            return temp;
        }
    }
}