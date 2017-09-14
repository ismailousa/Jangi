using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jangi.ViewModels
{
    public class AuthRegister
    {
        [Required, MaxLength(30)]
        public string pseudo { get; set; }
        [Required, MaxLength(256),DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required,DataType(DataType.Date)]
        public DateTime birthDate { get; set; }
        [Required, MaxLength(128), DataType(DataType.Password)]
        public string password { get; set; }
    }

    public class AuthLogin
    {
        [Required, MaxLength(256)]
        public string pseudoORmail { get; set; }
        [Required, MaxLength(128), DataType(DataType.Password)]
        public string password { get; set; }
    }
}