using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jangi.Models
{
    public class Vote
    {
        public virtual int id { get; set; }
        public virtual Type type { get; set; }
        public virtual User voter { get; set; }
        public virtual Type voteType { get; set; }
        public virtual int voted { get; set; }
    }

    public class VoteMap : ClassMapping<Vote>
    {
        public VoteMap()
        {
            Table("Vote");

            Id(x => x.id, x => x.Generator(Generators.Identity));
            Property(x => x.voted, x => x.NotNullable(true));

            ManyToOne(x => x.type, x =>
            {
                x.Column("Type");
            });

            ManyToOne(x => x.voteType, x =>
            {
                x.Column("VoteType");
            });

            ManyToOne(x => x.voter, x =>
            {
                x.Column("Voter");
            });
        }
    }
}