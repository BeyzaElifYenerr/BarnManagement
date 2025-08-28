using System.Data.Entity.Migrations;

namespace BarnManagement.Migrations
{
    public partial class MultiTenantBarn : DbMigration
    {
        public override void Up()
        {
            // 1) Yeni kolonları önce NULLABLE ekle (eski veriye uyum için)
            AddColumn("dbo.Barns", "OwnerUserId", c => c.Int()); // şimdilik nullable
            AddColumn("dbo.Animals", "BarnId", c => c.Int());    // nullable
            AddColumn("dbo.Products", "BarnId", c => c.Int());   // nullable

            // 2) Backfill: En az bir Barn olsun; tüm hayvan/ürünler ona bağlansın
            Sql(@"
DECLARE @barnId INT;

SELECT TOP 1 @barnId = Id FROM dbo.Barns ORDER BY Id;

IF @barnId IS NULL
BEGIN
    INSERT INTO dbo.Barns (Capacity, CurrentAnimalCount, Balance, UpdatedAt, OwnerUserId)
    VALUES (50, 0, 1000, GETUTCDATE(), NULL);
    SET @barnId = SCOPE_IDENTITY();
END

UPDATE dbo.Animals
SET BarnId = @barnId
WHERE BarnId IS NULL OR BarnId = 0;

IF COL_LENGTH('dbo.Products', 'BarnId') IS NOT NULL
BEGIN
    UPDATE dbo.Products
    SET BarnId = @barnId
    WHERE BarnId IS NULL OR BarnId = 0;
END

UPDATE dbo.Barns
SET CurrentAnimalCount = (SELECT COUNT(*) FROM dbo.Animals WHERE BarnId = dbo.Barns.Id)
WHERE Id = @barnId;
");

            // 3) Artık NOT NULL yapabiliriz
            AlterColumn("dbo.Animals", "BarnId", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "BarnId", c => c.Int(nullable: false));

            // 4) Index + FK (cascade delete KAPALI)
            CreateIndex("dbo.Animals", "BarnId");
            AddForeignKey("dbo.Animals", "BarnId", "dbo.Barns", "Id", cascadeDelete: false);

            CreateIndex("dbo.Products", "BarnId");
            AddForeignKey("dbo.Products", "BarnId", "dbo.Barns", "Id", cascadeDelete: false);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Products", "BarnId", "dbo.Barns");
            DropIndex("dbo.Products", new[] { "BarnId" });

            DropForeignKey("dbo.Animals", "BarnId", "dbo.Barns");
            DropIndex("dbo.Animals", new[] { "BarnId" });

            AlterColumn("dbo.Products", "BarnId", c => c.Int());
            AlterColumn("dbo.Animals", "BarnId", c => c.Int());

            DropColumn("dbo.Products", "BarnId");
            DropColumn("dbo.Animals", "BarnId");
            DropColumn("dbo.Barns", "OwnerUserId");
        }
    }
}
