using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jangi.Models
{
    public class CommentReply
    {
        public virtual int id { get; set; }
        public virtual string content { get; set; }
        public virtual DateTime date { get; set; }
        public virtual User author { get; set; }
        public virtual Comment comment { get; set; }
    }

    public class CommentReplyMap : ClassMapping<CommentReply>
    {
        public CommentReplyMap()
        {
            Table("CommentReplies");

            Id(x => x.id, x => x.Generator(Generators.Identity));
            Property(x => x.content, x => x.NotNullable(true));
            Property(x => x.date, x => x.NotNullable(true));

            ManyToOne(x => x.author, map =>
            {
                map.Column("Author");
            });

            ManyToOne(x => x.comment, map =>
            {
                map.Column("Comment");
            });
        }
    }
}