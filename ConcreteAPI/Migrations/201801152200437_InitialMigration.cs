namespace ConcreteAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Ddd = c.String(nullable: false, maxLength: 3, unicode: false),
                        Number = c.String(nullable: false, maxLength: 9, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => new { t.UserId, t.Ddd, t.Number }, unique: true, name: "unq_Phone");
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 255, unicode: false),
                        Password = c.String(nullable: false, maxLength: 8000, unicode: false),
                        CreationDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        LastLoginDate = c.DateTime(),
                        Token = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true, name: "unq_Users_Email");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phones", "UserId", "dbo.Users");
            DropIndex("dbo.Users", "unq_Users_Email");
            DropIndex("dbo.Phones", "unq_Phone");
            DropTable("dbo.Users");
            DropTable("dbo.Phones");
        }
    }
}
