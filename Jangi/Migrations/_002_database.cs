using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jangi.Migrations
{
    [Migration(2)]
    public class _002_database : Migration
    {
        public override void Down()
        {
            Delete.Column("Title").FromTable("posts");
        }

        public override void Up()
        {
            Alter.Table("posts")
                .AddColumn("Title").AsString(50);
        }
    }
}