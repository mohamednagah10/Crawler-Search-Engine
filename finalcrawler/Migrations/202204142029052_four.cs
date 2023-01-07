namespace finalcrawler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class four : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.postings", "dic_dic_id", "dbo.dics");
            DropForeignKey("dbo.postings", "weburi_id", "dbo.weburis");
            DropForeignKey("dbo.postions", "posting_id", "dbo.postings");
            DropIndex("dbo.postings", new[] { "dic_dic_id" });
            DropIndex("dbo.postings", new[] { "weburi_id" });
            DropIndex("dbo.postions", new[] { "posting_id" });
            DropTable("dbo.dics");
            DropTable("dbo.postings");
            DropTable("dbo.postions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.postions",
                c => new
                    {
                        pos = c.Int(nullable: false, identity: true),
                        posting_id = c.Int(),
                    })
                .PrimaryKey(t => t.pos);
            
            CreateTable(
                "dbo.postings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        freq = c.Int(nullable: false),
                        dic_dic_id = c.Int(),
                        weburi_id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.dics",
                c => new
                    {
                        dic_id = c.Int(nullable: false, identity: true),
                        ndoc = c.Int(nullable: false),
                        freq = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.dic_id);
            
            CreateIndex("dbo.postions", "posting_id");
            CreateIndex("dbo.postings", "weburi_id");
            CreateIndex("dbo.postings", "dic_dic_id");
            AddForeignKey("dbo.postions", "posting_id", "dbo.postings", "id");
            AddForeignKey("dbo.postings", "weburi_id", "dbo.weburis", "id");
            AddForeignKey("dbo.postings", "dic_dic_id", "dbo.dics", "dic_id");
        }
    }
}
