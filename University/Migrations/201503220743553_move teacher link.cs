namespace University.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moveteacherlink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Journal", "TeacherId", "dbo.Teacher");
            DropIndex("dbo.Journal", new[] { "TeacherId" });
            AddColumn("dbo.Subject", "TeacherId", c => c.Int());
            AddForeignKey("dbo.Subject", "TeacherId", "dbo.Teacher", "TeacherId");
            CreateIndex("dbo.Subject", "TeacherId");
            DropColumn("dbo.Journal", "TeacherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Journal", "TeacherId", c => c.Int());
            DropIndex("dbo.Subject", new[] { "TeacherId" });
            DropForeignKey("dbo.Subject", "TeacherId", "dbo.Teacher");
            DropColumn("dbo.Subject", "TeacherId");
            CreateIndex("dbo.Journal", "TeacherId");
            AddForeignKey("dbo.Journal", "TeacherId", "dbo.Teacher", "TeacherId");
        }
    }
}
