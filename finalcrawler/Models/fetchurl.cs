//using AngleSharp.Html.Dom;
//using HtmlAgilityPack;
using mshtml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace finalcrawler.Models
{
    public class fetchurl
    {
        public static Queue<string> q = new Queue<string>();
        public static crawler_Context li = new crawler_Context();
        
        public static void fet(string URL)
        {
            if (li.weburis.Count() == 3000)
                return;
            var b = li.weburis.Select(s => s.url);
            string rString = "";
            

            try
            {
                WebRequest myWebRequest = WebRequest.Create(URL);

                WebResponse myWebResponse = myWebRequest.GetResponse();

                Stream streamResponse = myWebResponse.GetResponseStream();

                StreamReader sReader = new StreamReader(streamResponse);
                rString = sReader.ReadToEnd(); streamResponse.Close();
                string language = "lang=\"en";
                Regex regex = new Regex(@"[a-z]|[A-Z]");
                HTMLDocument doc = new HTMLDocument();
                IHTMLDocument2 doc2 = (IHTMLDocument2)doc;
                doc2.write(rString);
                string elements3 = doc2.title;
                Match match = regex.Match(elements3);
                if ((rString.Split(new string[] { "<head>" }, StringSplitOptions.None)[0].Contains(language)|| match.Success)&&!b.Contains(URL) )//||Regex.IsMatch(rString.Split(new string[] { "<head>" }, StringSplitOptions.None)[1].Split(' ')[0],text2))
                {
                    weburi dd = new weburi();
                    dd.url = URL;
                    
                    li.weburis.Add(dd);
                    li.SaveChanges();
                    FileStream f = new FileStream("D:\\fincrawler\\" + li.weburis.Count() + ".txt", FileMode.CreateNew);
                    StreamWriter w = new StreamWriter(f);
                    w.WriteLine(URL);
                    w.Write(rString);
                    w.Close();
                    f.Close();
                    if (li.weburis.Count() == 3000)
                        return;
                }
                sReader.Close();
                myWebResponse.Close();
            
            
            IHTMLElementCollection elements = doc2.links;
            foreach (IHTMLElement el in elements)
            {
                string link = (string)el.getAttribute("href", 0);

                if (b.Contains(link))
                    continue;

                if (Uri.IsWellFormedUriString(link, UriKind.Absolute))
                    if (link.StartsWith("https://") || link.StartsWith("http://"))
                    {
                        q.Enqueue(link);
                    }

            }
            if (li.weburis.Count() >= 3000 || q.Count == 0)
                return;
            fet(q.Dequeue());
            }
            catch (Exception e)
            {
                if (q.Count != 0)
                    fet(q.Dequeue());
                return;
            }
        }
    }
}