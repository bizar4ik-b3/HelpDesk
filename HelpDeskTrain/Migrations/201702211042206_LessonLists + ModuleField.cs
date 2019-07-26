namespace HelpDeskTrain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LessonListsModuleField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LessonLists", "isModule", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {

        }
    }
}
