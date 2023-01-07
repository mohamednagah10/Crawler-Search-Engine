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
            DropIndex("dbo.postings", new[] { "dic_dic_id" });
            DropIndex("dbo.postings", new[] { "weburi_id" });
            AlterColumn("dbo.postings", "dic_dic_id", c => c.Int(nullable: false));
            AlterColumn("dbo.postings", "weburi_id", c => c.Int(nullable: false));
            CreateIndex("dbo.postings", "dic_dic_id");
            CreateIndex("dbo.postings", "weburi_id");
            AddForeignKey("dbo.postings", "dic_dic_id", "dbo.dics", "dic_id", cascadeDelete: true);
            AddForeignKey("dbo.postings", "weburi_id", "dbo.weburis", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.postings", "weburi_id", "dbo.weburis");
            DropForeignKey("dbo.postings", "dic_dic_id", "dbo.dics");
            DropIndex("dbo.postings", new[] { "weburi_id" });
            DropIndex("dbo.postings", new[] { "dic_dic_id" });
            AlterColumn("dbo.postings", "weburi_id", c => c.Int());
            AlterColumn("dbo.postings", "dic_dic_id", c => c.Int());
            CreateIndex("dbo.postings", "weburi_id");
            CreateIndex("dbo.postings", "dic_dic_id");
            AddForeignKey("dbo.postings", "weburi_id", "dbo.weburis", "id");
            AddForeignKey("dbo.postings", "dic_dic_id", "dbo.dics", "dic_id");
        }
    }
}
