using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jangi.Models
{
    public class Post
    {
        public virtual int id { get; set; }
        public virtual string title { get; set; }
        public virtual string content { get; set; }
        public virtual DateTime date { get; set; }
        //[ForeignKey("users")]
        //public virtual int author { get; set; }
        public virtual User author { get; set; }
        public virtual IList<Tag> tags { get; set; }
        public virtual IList<Comment> comments { get; set; }
    }

    public class PostMap : ClassMapping<Post>
    {
        public PostMap()
        {
            Table("posts");

            Id(x => x.id, x => x.Generator(Generators.Identity));
            Property(x => x.content, x => x.NotNullable(true));
            Property(x => x.title, x => x.NotNullable(true));
            Property(x => x.date, x => x.NotNullable(true));

            ManyToOne(x => x.author, map =>
            {
                map.Column("Author");
            });

            Bag(x => x.tags, x => {
                x.Table("post_tags");
                x.Key(y => y.Column("postID"));
            }, x => x.ManyToMany(y => y.Column("tagID")));

            Bag(x => x.comments, x =>
            {
                x.Table("Comments");
                x.Inverse(true);
                x.Cascade(Cascade.All);
                x.Key(y => y.Column("Post"));
            }, x => x.OneToMany());
        }
    }
}