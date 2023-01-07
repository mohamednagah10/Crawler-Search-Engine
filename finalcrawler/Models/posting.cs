using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace finalcrawler.Models
{
    public class posting
    {
        public int id { get; set; }
        [ForeignKey("dic")]
        public int dic_dic_id { get; set; }
        public dic dic { get; set; }
        [ForeignKey("weburi")]
        public int weburi_id { get; set; }
        public weburi weburi { get; set; }
        
        public int freq { get; set; }
       
    }
}