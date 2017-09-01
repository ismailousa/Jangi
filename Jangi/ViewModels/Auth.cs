using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jangi.ViewModels
{
    public class AuthRegister
    {
        [Required]
        public string pseudo { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required,DataType(DataType.Date)]
        public DateTime birthDate { get; set; }
        [Required]
        public string password { get; set; }
    }
}