using Jangi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jangi.ViewModels
{
    public class TagCheckBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsChecked { get; set; }
    }

    public class NewPost
    {
        public bool isNew { get; set; }
        public int id { get; set; }
        [Required, MaxLength(50)]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        public DateTime date { get; set; }
        public User author { get; set; }
        public IList<TagCheckBox> tags { get; set; }
        public IList<Comment> comments { get; set; }
        public IList<Tag> tagList { get; set; }
    }

    public class tagViewModel
    {
        public int id { get; set; }
        [Required, MaxLength(30)]
        public string Categorie { get; set; }
    }
}