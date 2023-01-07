using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace finalcrawler.Models
{
    public class postion
    {

        public int id { get; set; }
        // [Key]
        // [Column(Order =1)]
        [Required]
        public int pos { get; set; }
        // [Key]
        //[Column(Order = 2)]
        [ForeignKey("posting")]
        public int posting_id { get; set; }
        //[Required]
        public posting posting { get; set; }
        
    }
}