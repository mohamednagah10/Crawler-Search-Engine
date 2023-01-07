using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalcrawler.Models
{
    public class searclass
    {
        public string query { get; set; }
        public List<weburi> weburis { get; set; }
         public Dictionary<int,int> diss (Dictionary<int, Dictionary<int, List<int>>> d, List<int> set)
        {

            Dictionary<int,int> rr = new Dictionary<int, int>();
            List<int> sum = new List<int>();
            foreach(var t in set)
            {
                int i = 0;
                int j = 0;
                List<int> first = new List<int>();
                List<int> last;
                last = new List<int>();
                bool tr = true;
                foreach (var d2 in d)
                {
                    i = 0;j = 0;
                    sum = new List<int>(new int[d2.Value[t].Count]);
                    
                    if (first.Count == 0)
                    {
                        last= new List<int>(new int[sum.Count]);
                        first = d2.Value[t];
                        
                        continue;
                    }
                    if (tr)
                    {
                        for(int q=0;q<sum.Count;q++)
                            sum[q] = int.MaxValue;
                       // tr = false;
                    }
                    bool fa = false;
                    while (i < first.Count&&j<d2.Value[t].Count)
                    {
                        
                        if (first[i] < d2.Value[t][j])
                        {
                            sum[j]=Math.Min(sum[j],Math.Abs(first[i]-d2.Value[t][j])+last[i]);
                            i++;
                            fa=true;
                        }
                        else
                        {
                            if(!fa)
                                sum[j] = Math.Min(sum[j], Math.Abs(first[i] - d2.Value[t][j]) + last[i]);
                            fa = false;
                            j++;
                        }

                    }
                    if (j < d2.Value[t].Count)
                    {
                        
                        while (j < d2.Value[t].Count)
                        {
                            sum[j] = Math.Abs(first[i - 1] - d2.Value[t][j]);
                            j++;
                        }
                    }
                    last = sum;
                    first = d2.Value[t];
                }
                rr.Add(t,sum.Min());
            }
            return rr;
        }

    }
}