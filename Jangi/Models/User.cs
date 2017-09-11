using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jangi.Models
{
    public class User
    {
        public virtual int id { get; set; }
        public virtual string pseudo { get; set; }
        public virtual string email { get; set; }
        public virtual DateTime birthDate { get; set; }
        public virtual string password { get; set; }
        public virtual IList<Post> posts { get; set; }
        public virtual IList<Comment> comments { get; set; }
        public virtual IList<CommentReply> commentReplies { get; set; }
        public virtual IList<Vote> votes { get; set; }
        public virtual IList<Tag> tags { get; set; }

        public virtual void SetPassword(string password)
        {
            this.password = BCrypt.Net.BCrypt.HashPassword(password, 13);
        }

        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, this.password);
        }

        public virtual void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", 13);
        }
    }

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("users");

            Id(x => x.id, x => x.Generator(Generators.Identity));
            Property(x => x.pseudo, x => x.NotNullable(true));
            Property(x => x.email, x => x.NotNullable(true));
            Property(x => x.birthDate, x => x.NotNullable(true));
            Property(x => x.password, x => x.NotNullable(true));

            Bag(x => x.posts, x =>
            {
                x.Table("posts");
                x.Inverse(true);
                x.Cascade(Cascade.All);
                x.Key(y => y.Column("Author"));
            }, x => x.OneToMany());

            Bag(x => x.comments, x =>
            {
                x.Table("Comments");
                x.Inverse(true);
                x.Cascade(Cascade.All);
                x.Key(y => y.Column("Author"));
            }, x => x.OneToMany());

            Bag(x => x.posts, x =>
            {
                x.Table("CommentReplies");
                x.Inverse(true);
                x.Cascade(Cascade.All);
                x.Key(y => y.Column("Author"));
            }, x => x.OneToMany());

            Bag(x => x.votes, x =>
            {
                x.Table("Vote");
                x.Inverse(true);
                x.Cascade(Cascade.All);
                x.Key(y => y.Column("Voter"));
            }, x => x.OneToMany());

            Bag(x => x.tags, x =>
            {
                x.Table("tags");
                x.Inverse(true);
                x.Cascade(Cascade.None);
                x.Key(y => y.Column("Author"));
            }, x => x.OneToMany());
        }
    }
}