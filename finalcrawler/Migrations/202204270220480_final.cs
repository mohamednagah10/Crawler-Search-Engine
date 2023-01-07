namespace finalcrawler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.postings", "dic_dic_id", "dbo.dics");
            DropForeignKey("dbo.postings", "weburi_id", "dbo.weburis");
            DropForeignKey("dbo.postions", "posting_id", "dbo.postings");
            DropIndex("dbo.postings", new[] { "dic_dic_id" });
            DropIndex("dbo.postings", new[] { "weburi_id" });
            DropIndex("dbo.postions", new[] { "posting_id" });
            AlterColumn("dbo.postings", "dic_dic_id", c => c.Int(nullable: false));
            AlterColumn("dbo.postings", "weburi_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.postings", "weburi_id", c => c.Int());
            AlterColumn("dbo.postings", "dic_dic_id", c => c.Int());
            CreateIndex("dbo.postions", "posting_id");
            CreateIndex("dbo.postings", "weburi_id");
            CreateIndex("dbo.postings", "dic_dic_id");
            AddForeignKey("dbo.postions", "posting_id", "dbo.postings", "id", cascadeDelete: true);
            AddForeignKey("dbo.postings", "weburi_id", "dbo.weburis", "id");
            AddForeignKey("dbo.postings", "dic_dic_id", "dbo.dics", "dic_id");
        }
    }
}
