using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ShuDu.Web.Controllers
{
    public class HttpHelper
    {
        public static string PostRequest(string url, string data)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            using (HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse())
            {
                Stream s = response.GetResponseStream();
                StreamReader Reader = new StreamReader(s);
                return Reader.ReadToEnd();
            }
        }

        public static string GetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            using (HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse())
            {
                Stream s = response.GetResponseStream();
                StreamReader Reader = new StreamReader(s);
                return Reader.ReadToEnd();
            }
        }

        public static string GetShuDuHtml(string nd, string y, string m, string d)
        {
            var temp = GetRequest("http://cn.sudokupuzzle.org/online2.php?nd=" + nd + "&y=" + y + "&m=" + m + "&d=" + d);
            var index = temp.IndexOf("<link");
            temp = temp.Substring(index, temp.Length - index);
            index = temp.LastIndexOf("</script>");
            temp = temp.Substring(0, index + 9);
            temp = temp.Replace("online2.php", "");
            return temp;
        }
    }
}