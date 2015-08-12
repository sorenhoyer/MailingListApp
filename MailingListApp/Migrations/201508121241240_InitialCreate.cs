namespace MailingListApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailingLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        First = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubscriberMailingLists",
                c => new
                    {
                        Subscriber_Id = c.Int(nullable: false),
                        MailingList_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subscriber_Id, t.MailingList_Id })
                .ForeignKey("dbo.Subscribers", t => t.Subscriber_Id, cascadeDelete: true)
                .ForeignKey("dbo.MailingLists", t => t.MailingList_Id, cascadeDelete: true)
                .Index(t => t.Subscriber_Id)
                .Index(t => t.MailingList_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubscriberMailingLists", "MailingList_Id", "dbo.MailingLists");
            DropForeignKey("dbo.SubscriberMailingLists", "Subscriber_Id", "dbo.Subscribers");
            DropIndex("dbo.SubscriberMailingLists", new[] { "MailingList_Id" });
            DropIndex("dbo.SubscriberMailingLists", new[] { "Subscriber_Id" });
            DropTable("dbo.SubscriberMailingLists");
            DropTable("dbo.Subscribers");
            DropTable("dbo.MailingLists");
        }
    }
}
