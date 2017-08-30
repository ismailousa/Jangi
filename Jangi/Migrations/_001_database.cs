using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Jangi.Migrations
{
    [Migration(1)]
    public class _001_database : Migration
    {
        public override void Down()
        {
            Delete.Table("Users");
            Delete.Table("Posts");
            Delete.Table("Tags");
            Delete.Table("Post_Tags");
            Delete.Table("CommentReplies");
            Delete.Table("Comments");
            Delete.Table("VoteType");
            Delete.Table("Vote");
        }

        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Pseudo").AsString(30)
                .WithColumn("Email").AsString(256)
                .WithColumn("BirthDate").AsDate()
                .WithColumn("Password").AsString(128);

            Create.Table("Posts")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Content").AsCustom("Text")
                .WithColumn("Date").AsDateTime()
                .WithColumn("Author").AsInt32().ForeignKey("Users", "id").OnDelete(Rule.Cascade);

            Create.Table("Tags")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Tag").AsString(30)
                .WithColumn("Author").AsInt32().ForeignKey("Users", "id").OnDelete(Rule.None);

            Create.Table("Post_Tags")
                .WithColumn("postID").AsInt32().ForeignKey("Posts", "id")
                .WithColumn("tagID").AsInt32().ForeignKey("Tags", "id");

            Create.Table("Comments")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Content").AsCustom("Text")
                .WithColumn("Date").AsDateTime()
                .WithColumn("Author").AsInt32().ForeignKey("Users", "id")
                .WithColumn("Post").AsInt32().ForeignKey("Posts", "id");

            Create.Table("CommentReplies")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Content").AsCustom("Text")
                .WithColumn("Date").AsDateTime()
                .WithColumn("Author").AsInt32().ForeignKey("Users", "id")
                .WithColumn("Comment").AsInt32().ForeignKey("Comments", "id");

            Create.Table("Types")
               .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Name").AsString(30);

            Create.Table("Vote")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Type").AsInt32().ForeignKey("Types", "id")
                .WithColumn("Voter").AsInt32().ForeignKey("Users", "id")
                .WithColumn("VoteType").AsInt32().ForeignKey("Types", "id")
                .WithColumn("Voted").AsInt32();
        }
    }
}