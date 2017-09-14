using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jangi.Models
{
    public class Tag
    {
        public virtual int id { get; set; }
        public virtual string tag { get; set; }
        public virtual User author { get; set; }
    }

    public class TagMap : ClassMapping<Tag>
    {
        public TagMap()
        {
            Table("tags");

            Id(x => x.id, x => x.Generator(Generators.Identity));
            Property(x => x.tag, x => x.NotNullable(true));

            ManyToOne(x => x.author, map =>
            {
                map.Column("Author");
            });
        }
    }
}