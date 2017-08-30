using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jangi.Models
{
    public class Comment
    {
        public virtual int id { get; set; }
        public virtual string content { get; set; }
        public virtual DateTime date { get; set; }
        public virtual User author { get; set; }
        public virtual Post post { get; set; }
        public virtual IList<CommentReply> commentReplies { get; set; }
    }

    public class CommentMap : ClassMapping<Comment>
    {
        public CommentMap()
        {
            Table("Comments");

            Id(x => x.id, x => x.Generator(Generators.Identity));
            Property(x => x.content, x => x.NotNullable(true));
            Property(x => x.date, x => x.NotNullable(true));

            ManyToOne(x => x.author, map =>
            {
                map.Column("Author");
            });

            ManyToOne(x => x.post, map =>
            {
                map.Column("Post");
            });

            Bag(x => x.commentReplies, x =>
            {
                x.Table("CommentReplies");
                x.Inverse(true);
                x.Cascade(Cascade.All);
                x.Key(y => y.Column("Comment"));
            }, x => x.OneToMany());
        }
    }
}