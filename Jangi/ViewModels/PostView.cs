﻿using Jangi.Models;
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

        public int id { get; set; }
        [Required, MaxLength(50)]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        public DateTime date { get; set; }
        public User author { get; set; }
        public IList<Tag> tags { get; set; }
        public IList<TagCheckBox> tagCheck { get; set; }
        public IList<Comment> comments { get; set; }
    }
}