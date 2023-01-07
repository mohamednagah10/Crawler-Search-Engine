using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace finalcrawler.Models
{
    public class crawler_Context:DbContext
    {
        public DbSet<weburi> weburis { get; set; }
        public DbSet<dic> dics { get; set; }
        public DbSet<beste> bestes { get; set; }
        public DbSet<posting> postings { get; set; }
        public DbSet<postion> postions { get; set; }

    }
}