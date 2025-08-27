namespace BarnManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UseDateTime2_AndProductProducedAt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animals", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Barns", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Products", "ProducedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Sales", "SoldAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "SoldAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "ProducedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Barns", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Animals", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
