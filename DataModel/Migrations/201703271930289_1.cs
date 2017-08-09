namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PenegakHukums", "Agama", c => c.String());
            AddColumn("dbo.PenegakHukums", "Pendidikan", c => c.String());
            AddColumn("dbo.PenegakHukums", "TempatLahir", c => c.String());
            AddColumn("dbo.Pengacaras", "Agama", c => c.String());
            AddColumn("dbo.Pengacaras", "Pendidikan", c => c.String());
            AddColumn("dbo.Pengacaras", "TempatLahir", c => c.String());
            AddColumn("dbo.Pihaks", "Pekerjaan", c => c.String());
            AddColumn("dbo.Pihaks", "Kebangsaan", c => c.String());
            AddColumn("dbo.Pihaks", "Alias", c => c.String());
            AddColumn("dbo.Pihaks", "Agama", c => c.String());
            AddColumn("dbo.Pihaks", "Pendidikan", c => c.String());
            AddColumn("dbo.Pihaks", "TempatLahir", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pihaks", "TempatLahir");
            DropColumn("dbo.Pihaks", "Pendidikan");
            DropColumn("dbo.Pihaks", "Agama");
            DropColumn("dbo.Pihaks", "Alias");
            DropColumn("dbo.Pihaks", "Kebangsaan");
            DropColumn("dbo.Pihaks", "Pekerjaan");
            DropColumn("dbo.Pengacaras", "TempatLahir");
            DropColumn("dbo.Pengacaras", "Pendidikan");
            DropColumn("dbo.Pengacaras", "Agama");
            DropColumn("dbo.PenegakHukums", "TempatLahir");
            DropColumn("dbo.PenegakHukums", "Pendidikan");
            DropColumn("dbo.PenegakHukums", "Agama");
        }
    }
}
