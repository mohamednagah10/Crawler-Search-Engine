using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace finalcrawler.Models
{
    public class dic
    {
        [Key]
        public int dic_id { get; set; }
        public string term { get; set; }
        public int ndoc { get; set; }
        public int freq { get; set; }
        
    }
}