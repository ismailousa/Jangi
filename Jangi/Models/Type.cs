using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jangi.Models
{
    public class Type
    {
        public virtual int id { get; set; }
        public virtual string name { get; set; }
    }

    public class TypeMap : ClassMapping<Type>
    {
        public TypeMap()
        {
            Table("types");

            Id(x => x.id, x => x.Generator(Generators.Identity));
            Property(x => x.name, x => x.NotNullable(true));
        }
    }
}