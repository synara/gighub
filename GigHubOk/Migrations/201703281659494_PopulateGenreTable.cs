namespace GigHubOk.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name) VALUES ('Jazz')");
            Sql("INSERT INTO Genres (Name) VALUES ('Blues')");
            Sql("INSERT INTO Genres (Name) VALUES ('Rock')");
            Sql("INSERT INTO Genres (Name) VALUES ('Country')");

        }

        public override void Down()
        {
            Sql("DELETE FROM Genres");
        }
    }
}
