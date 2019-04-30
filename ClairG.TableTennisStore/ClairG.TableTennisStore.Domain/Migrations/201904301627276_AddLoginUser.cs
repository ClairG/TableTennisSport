namespace ClairG.TableTennisStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoginUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginUsers",
                c => new
                    {
                        LoginUserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Passsord = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LoginUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoginUsers");
        }
    }
}
