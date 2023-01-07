namespace finalcrawler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bestes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.dics",
                c => new
                    {
                        dic_id = c.Int(nullable: false, identity: true),
                        term = c.String(),
                        ndoc = c.Int(nullable: false),
                        freq = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.dic_id);
            
            CreateTable(
                "dbo.postings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        freq = c.Int(nullable: false),
                        dic_dic_id = c.Int(),
                        weburi_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.dics", t => t.dic_dic_id)
                .ForeignKey("dbo.weburis", t => t.weburi_id)
                .Index(t => t.dic_dic_id)
                .Index(t => t.weburi_id);
            
            CreateTable(
                "dbo.postions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        pos = c.Int(nullable: false),
                        posting_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.postings", t => t.posting_id, cascadeDelete: true)
                .Index(t => t.posting_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.postions", "posting_id", "dbo.postings");
            DropForeignKey("dbo.postings", "weburi_id", "dbo.weburis");
            DropForeignKey("dbo.postings", "dic_dic_id", "dbo.dics");
            DropIndex("dbo.postions", new[] { "posting_id" });
            DropIndex("dbo.postings", new[] { "weburi_id" });
            DropIndex("dbo.postings", new[] { "dic_dic_id" });
            DropTable("dbo.postions");
            DropTable("dbo.postings");
            DropTable("dbo.dics");
            DropTable("dbo.bestes");
        }
    }
}
