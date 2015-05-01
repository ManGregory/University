namespace University.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        RecordBookNumber = c.Long(nullable: false),
                        GroupId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Group", t => t.GroupId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Specialization = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.ControlType",
                c => new
                    {
                        ControlTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ControlTypeId);
            
            CreateTable(
                "dbo.Journal",
                c => new
                    {
                        JournalId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Mark = c.String(),
                        ControlTypeId = c.Int(),
                        SubjectId = c.Int(),
                        TeacherId = c.Int(),
                        StudentId = c.Int(),
                    })
                .PrimaryKey(t => t.JournalId)
                .ForeignKey("dbo.ControlType", t => t.ControlTypeId)
                .ForeignKey("dbo.Subject", t => t.SubjectId)
                .ForeignKey("dbo.Teacher", t => t.TeacherId)
                .ForeignKey("dbo.Student", t => t.StudentId)
                .Index(t => t.ControlTypeId)
                .Index(t => t.SubjectId)
                .Index(t => t.TeacherId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Journal", new[] { "StudentId" });
            DropIndex("dbo.Journal", new[] { "TeacherId" });
            DropIndex("dbo.Journal", new[] { "SubjectId" });
            DropIndex("dbo.Journal", new[] { "ControlTypeId" });
            DropIndex("dbo.Student", new[] { "GroupId" });
            DropForeignKey("dbo.Journal", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Journal", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.Journal", "SubjectId", "dbo.Subject");
            DropForeignKey("dbo.Journal", "ControlTypeId", "dbo.ControlType");
            DropForeignKey("dbo.Student", "GroupId", "dbo.Group");
            DropTable("dbo.Teacher");
            DropTable("dbo.Journal");
            DropTable("dbo.ControlType");
            DropTable("dbo.Group");
            DropTable("dbo.Student");
            DropTable("dbo.Subject");
            DropTable("dbo.UserProfile");
        }
    }
}
