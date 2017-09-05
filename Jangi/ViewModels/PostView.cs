using Jangi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jangi.ViewModels
{
    public class NewPost
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
        public User author { get; set; }
        public IList<Tag> tags { get; set; }
        public IList<Comment> comments { get; set; }
    }
}