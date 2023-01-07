namespace finalcrawler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.weburis",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        url = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            

        }
        
        public override void Down()
        {
            DropTable("dbo.weburis");
        }
    }
}
