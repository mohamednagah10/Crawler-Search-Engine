using finalcrawler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mshtml;
using System.Text.RegularExpressions;

namespace finalcrawler.Controllers
{
    public class HomeController : Controller
    {
        string[] str = {  "i", "me", "my", "myself", "we", "our", "ours", "ourselves",
                              "you", "your", "yours", "yourself", "yourselves", "he", "him", "his",
                               "himself", "she", "her", "hers", "herself", "it", "its", "itself",
                            "they", "them", "their", "theirs", "themselves", "what", "which", "who",
                            "whom", "this", "that", "these", "those", "am", "is", "are", "was",
                            "were", "be", "been", "being", "have", "has", "had", "having", "do",
                            "does", "did", "doing", "a", "an", "the", "and", "but", "if", "or",
                            "because", "as", "until", "while", "of", "at", "by", "for", "with",
                            "about", "against", "between", "into", "through", "during", "before",
                            "after", "above", "below", "to", "from", "up", "down", "in", "out", "on",
                            "off", "over", "under", "again", "further", "then", "once", "here",
                            "there", "when", "where", "why", "how", "all", "any", "both", "each",
                            "few", "more", "most", "other", "some", "such", "no", "nor", "not",
                            "only", "own", "same", "so", "than", "too", "very", "s", "t", "can",
                            "will", "just", "don", "should", "now","."};
        Regex regex = new Regex(@"[a-z]|[A-Z]|[1-9]");
        Regex rgx = new Regex("[^a-zA-Z0-9 ,]");
        public ActionResult disp()
        {

            int i = 1;
            List<weburi> oo = new List<weburi>();

            foreach (weburi rr in fetchurl.li.weburis.ToList())
            {

                if (rr.id < 2950)
                    continue;
                // if(rr.id<26)
                //   continue;
                FileStream f1 = new FileStream("D:\\fincrawler\\" + rr.id + ".txt", FileMode.Open);
                StreamReader w1 = new StreamReader(f1);
                string q1 = ""; w1.ReadLine();
                q1 = w1.ReadToEnd();
                HTMLDocument doc = new HTMLDocument();
                IHTMLDocument2 doc2 = (IHTMLDocument2)doc;
                doc2.write(q1);
                //[^ph]
                //Regex hc = new Regex("</[ph][^>]*>");
                Regex ho = new Regex(" ");//<[ph][^>]*>");
                Regex hoh = new Regex("</?body[^>]*>");
                //Regex hoh1 = new Regex("</body[^>]*>");
                Regex hoh1h = new Regex("((<script[^>]*>[^<]*</script>)|(<style[^>]*>[^<]*</style>))");
                Regex hg = new Regex("</?[^>]*>");


                //Regex htmlRegex = new Regex("<+^p|(h[1-6]?)+[^>]*>");
                string ssw = "";//htmlRegex.Replace(q1, " ");
                string k = "";
                IHTMLElementCollection elements = doc.getElementsByTagName("P");
                foreach (IHTMLElement el in elements)
                {
                    k += el.innerText + " ";
                }
                //q1 = k;
                //if (hoh.Match(q1).Success)
                //{
                //string[] ff = hoh.Split(q1);
                //q1 = ff[ff.Length-2];
                // q1 = hoh1.Split(q1)[0];
                //q1 = hoh1h.Replace(q1, " ");
                //q1 = hg.Replace(q1," ");
                //string[] ff = ho.Split(q1);

                //foreach (IHTMLElement el in elements)
                //k += ff.Length;
                // for (int z = 0; z < ff.Length; z++)
                //{
                //ssw = hg.Replace(q1, " ");
                //ssw = q1;//ff[z]; //hc.Split(ff[z])[0];

                //    Match match = regex.Match("" + ssw);
                //  if (match.Success)
                //{
                //ssw = ssw.Replace(".","");
                //ssw=ssw.Replace(str, " @ ");
                //@@k += q1;// + " ";//ssw + " ";
                //}
                //break;
                //}
                k = rgx.Replace(k, "");
                List<string> wa = k.ToLower().Split(new char[] { ',', ' ' }).ToList();

                //wa.RemoveAll(x=> str.Contains(x)||x.Length==0);
                ssw = "";


                for (int j = 0; j < wa.Count; j++)
                {
                    if (j > 250)
                        break;
                    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    //if(wa[j].EndsWith(new string[] {"Fff"}))
                    Porter2 stemmer = new Porter2();
                    string outputstem = stemmer.stem(wa[j]);
                    if (str.Contains(outputstem) || wa[j].Length == 0)
                    {
                        continue;
                    }

                    weburi fff = new weburi();
                    fff.id = j;
                    fff.url = outputstem;
                    oo.Add(fff);
                    dic d = fetchurl.li.dics.SingleOrDefault(ds => ds.term == outputstem);
                    posting p;
                    if (d == null)
                    {
                        fetchurl.li.bestes.Add(new beste { Name = outputstem });
                        d = new dic();
                        d.term = outputstem;
                        d.freq = 1;
                        d.ndoc = 0;
                        fetchurl.li.dics.Add(d);
                        fetchurl.li.SaveChanges();
                        d = fetchurl.li.dics.Single(ds => ds.term == outputstem);
                    }
                    else
                    {
                        d.freq += 1;
                        fetchurl.li.SaveChanges();
                    }
                    p = fetchurl.li.postings.
                            SingleOrDefault(ds => ds.dic_dic_id == d.dic_id && ds.weburi_id == rr.id);
                    if (p == null)
                    {
                        d.ndoc += 1;
                        fetchurl.li.SaveChanges();
                        p = new posting();
                        p.freq = 1;
                        p.weburi_id = rr.id;
                        p.dic_dic_id = d.dic_id;
                        fetchurl.li.postings.Add(p);
                        fetchurl.li.SaveChanges();
                        p = fetchurl.li.postings.
                            Single(ds => ds.dic_dic_id == d.dic_id && ds.weburi_id == rr.id);

                    }
                    else
                    {
                        p.freq += 1;
                        fetchurl.li.SaveChanges();
                    }
                    postion poss = new postion();
                    poss.pos = j;
                    poss.posting_id = p.id;
                    fetchurl.li.postions.Add(poss);
                    fetchurl.li.SaveChanges();
                    ssw += outputstem + ' ';
                    //  }
                }
                if (true)
                {


                    FileStream f11 = new FileStream("D:\\finstem\\" + rr.id + ".txt", FileMode.CreateNew);
                    StreamWriter qq = new StreamWriter(f11);
                    qq.Write(ssw);
                    qq.Close();
                    f11.Close();
                    //fff = new weburi();
                    //fff.id = rr.id;
                    // fff.url = hc.Split(ff[1])[0];
                    // oo.Add(fff);
                }
                w1.Close();
                f1.Close();
                //break;
            }
            return View(oo);
        }
        public ActionResult Index()
        {


            foreach (weburi rr in fetchurl.li.weburis.ToList())
            {
                FileStream f1 = new FileStream("D:\\fincrawler\\" + rr.id + ".txt", FileMode.Open);
                StreamReader w1 = new StreamReader(f1);
                string q1 = ""; w1.ReadLine();
                q1 = w1.ReadToEnd();
                w1.Close();
                f1.Close();
                HTMLDocument doc = new HTMLDocument();
                IHTMLDocument2 doc2 = (IHTMLDocument2)doc;
                doc2.write(q1);

                string k = "";
                IHTMLElementCollection elements = doc.getElementsByTagName("P");
                foreach (IHTMLElement el in elements)
                {
                    k += el.innerText + " ";
                }
                k = rgx.Replace(k, "");
                FileStream ff = new FileStream("D:\\fincrawler\\beforesteaming\\" + rr.id + ".txt", FileMode.CreateNew);
                StreamWriter ww = new StreamWriter(ff);
                ww.Write(k);
                ww.Close();
                ff.Close();

            }




            // foreach (weburi s in fetchurl.li.weburis)
            //{
            //  fetchurl.q.Enqueue(s.url);
            //}
            //fetchurl.fet(fetchurl.q.Dequeue());
            return View();
        }

        public ActionResult About()
        {


            Dictionary<int, Dictionary<int, List<int>>> d = new Dictionary<int, Dictionary<int, List<int>>>();



            int xx = 1;
            foreach (dic v in fetchurl.li.dics.ToList())
            {
                //FileStream f1 = new FileStream("C:\\Users\\moham\\OneDrive\\Desktop\\New folder (3)\\" + v.dic_id + "nnnn.txt", FileMode.Create);
                //f1.Close();
                d.Add(v.dic_id, new Dictionary<int, List<int>>());
                d[v.dic_id] = new Dictionary<int, List<int>>();
                foreach (posting p in fetchurl.li.postings.Where(x => x.dic_dic_id == v.dic_id).ToList())
                {

                    //FileStream f11 = new FileStream("C:\\Users\\moham\\OneDrive\\Desktop\\11.txt", FileMode.Append);
                    //StreamWriter qq = new StreamWriter(f11);
                    //qq.Write(p.weburi_id);
                    //qq.Close();
                    //f11.Close();
                    d[v.dic_id].Add(p.weburi_id, new List<int>());


                    foreach (postion pos in fetchurl.li.postions.Where(x => x.posting_id == p.id).ToList())
                    {
                        d[v.dic_id][p.weburi_id].Add(pos.pos);
                    }
                }
                //break;
                /*if (xx==3)
                {
                    
                }
                xx++ ;*/
            }

            /*FileStream f1 = new FileStream("C:\\Users\\moham\\OneDrive\\Desktop\\New folder (3)\\" + 0 + ".txt", FileMode.Create);
                f1.Close();
            int c=0;
            int ii = 0;
            foreach (dic v in fetchurl.li.dics.ToList())
            {
                
                c = 0;
                for (int i = 0; i < d[v.dic_id].Count; i++)
                {
                    c += d[v.dic_id].ElementAt(i).Value.Count;
                }
                v.ndoc = d[v.dic_id].Count;
                v.freq = c;
                ii++;
                fetchurl.li.SaveChanges();
               // break;
            } 
            ViewBag.Message = ii+"  Your application description page.   @@@"+c+"  "+ d[3].Count;
            */
            return View();
        }

        public ActionResult Contact()
        {
            int b = 0;
            int tot = 0;
            // foreach (posting p in fetchurl.li.postings.Where(r => r.dic.dic_id == 1).ToList()) {
            for (int i = 1; i <= 3000; i++)
            {
                try
                {

                    FileStream f11 = new FileStream("D:\\finstem\\" + i + ".txt", FileMode.Open);
                    StreamReader qq = new StreamReader(f11);
                    string w = qq.ReadToEnd();
                    List<string> arr = w.Split(' ').ToList();

                    int a = arr.Where(s => s == "site").Count();
                    if (a > 0)
                    {
                        tot += a;
                        b++;
                    }

                    qq.Close();
                    f11.Close();
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            ViewBag.Mee = b + "    " + tot;
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult search()
        {
            searclass s = new searclass();
            s.query = "";
            s.weburis = new List<weburi>();
            return View(s);
        }
       [HttpPost]
        public ActionResult search(searclass s)
        {


            if (s.query == null)
            {
                s = new searclass();
                s.query = "";
                s.weburis = new List<weburi>();
                return View(s);
            }
            if(s.query.StartsWith("\"")&& s.query.EndsWith("\""))
            {
                Dictionary<weburi, int> settt = new Dictionary<weburi, int>();
                s.weburis = new List<weburi>();
                foreach (weburi rr in fetchurl.li.weburis.ToList())
                {
                    FileStream f1 = new FileStream("D:\\fincrawler\\beforesteaming\\" + rr.id + ".txt", FileMode.Open);
                    StreamReader w1 = new StreamReader(f1);
                    string q1 = "";
                    q1 = w1.ReadToEnd();
                    w1.Close();
                    f1.Close();

                    if (q1.Contains(s.query.Substring(1, s.query.Length - 2)))
                    {
                        weburi ee = new weburi();
                        ee.id = rr.id;
                        
                        //s.weburis.Add(ee);

                        int n = q1.Split(new string[] { s.query.Substring(1, s.query.Length - 2) }, StringSplitOptions.RemoveEmptyEntries).Count();
                        ee.url = ee.id + "  " + rr.url + "  fff  "+n;settt.Add(ee, n);

                    }
                }
                s.weburis= (from entry in settt orderby entry.Value descending select entry.Key).ToList();
                
                return View(s);
            }
            List<string> wa = s.query.ToLower().Split(new char[] { ',', ' ' },StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> ww = new List<string>();


            for (int j = 0; j < wa.Count; j++)
            {
                Porter2 stemmer = new Porter2();
                string outputstem = stemmer.stem(wa[j]);
                if (str.Contains(outputstem) || wa[j].Length == 0)
                {
                    continue;
                }

                ww.Add(outputstem);
            }
                Dictionary<int, Dictionary<int, List<int>>> d = new Dictionary<int, Dictionary<int, List<int>>>();
            Dictionary<string, int> st = new Dictionary<string, int>();
            Dictionary<int,int> set = new Dictionary<int, int>();
            foreach (dic v in fetchurl.li.dics.ToList())
            {
                if (!ww.Contains(v.term)) continue;
                st.Add(v.term, v.dic_id);
                d.Add(v.dic_id, new Dictionary<int, List<int>>());
                d[v.dic_id] = new Dictionary<int, List<int>>();
                foreach (posting p in fetchurl.li.postings.Where(x => x.dic_dic_id == v.dic_id).ToList())
                {
                    d[v.dic_id].Add(p.weburi_id, new List<int>());

                    if (set.ContainsKey(p.weburi_id))
                    {
                        set[p.weburi_id]++;
                    }
                    else
                        set.Add(p.weburi_id, 1);
                    foreach (postion pos in fetchurl.li.postions.Where(x => x.posting_id == p.id).ToList())
                    {
                        d[v.dic_id][p.weburi_id].Add(pos.pos);
                    }
                }
            }List<int> lil = new List<int>();
            s.weburis = new List<weburi>();
            foreach(var b in set)
            {
                if (b.Value == ww.Count)
                {
                    lil.Add(b.Key);
                }

            }
            searclass c = new searclass();
            Dictionary<int,int> ew= c.diss(d, lil);
            List<int> y=(from entry in ew orderby entry.Value ascending select entry.Key).ToList();
            foreach(var i in y)
            {
                weburi ee = new weburi();
                weburi qw= fetchurl.li.weburis.SingleOrDefault(tt => tt.id == i);
                ee.id = qw.id;
                ee.url =qw.url+"   "+ee.id + "  fff  "+ew[i];
                s.weburis.Add(ee);
            }


            weburi eer = new weburi();
            eer.id = s.weburis.Count;
            eer.url = eer.id + "  fff";
            s.weburis.Add(eer);
            return View(s);
        }
    }
}