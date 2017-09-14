using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jangi.ViewModels
{
    public class ProfileInfo
    {
        public int id { get; set; }
        [Required, MaxLength(30)]
        public string pseudo { get; set; }
        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime birthDate { get; set; }
        public int numberOfPosts { get; set; }
    }

    public class newPassword
    {
        [Required, MaxLength(128), DataType(DataType.Password)]
        public string password { get; set; }
        [Required, MaxLength(128), DataType(DataType.Password)]
        public string passwordNew { get; set; }
        [Required, MaxLength(128), DataType(DataType.Password)]
        public string passwordConfirm { get; set; }
    }
}