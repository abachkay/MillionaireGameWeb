namespace MillionaireGameWeb.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Thread = c.String(nullable: false, maxLength: 255),
                        Message = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAnswerLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionNumber = c.Int(nullable: false),
                        FirstAnswerCount = c.Int(nullable: false),
                        SecondAnswerCount = c.Int(nullable: false),
                        ThirdAnswerCount = c.Int(nullable: false),
                        FourthAnswerCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserAnswerLogs");
            DropTable("dbo.ExceptionLogs");
        }
    }
}
