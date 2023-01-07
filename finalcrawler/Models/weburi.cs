using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace finalcrawler.Models
{
    public class weburi
    {
       [Required]
        public int id { get; set; }
        [Required]
        public string url { get; set; }
    }
}