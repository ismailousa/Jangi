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
        }
    }
}