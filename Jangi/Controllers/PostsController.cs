using Jangi.Models;
using Jangi.ViewModels;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jangi.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Categories()
        {
            var model = Database.Session.Query<Tag>().OrderBy(x => x.tag).ToList();
            return PartialView(model);
        }

        public ActionResult New()
        {
            return View(new NewPost()
            {
                tags = Database.Session.Query<Tag>().Select(
                    tag => new TagCheckBox()
                    {
                        Id = tag.id,
                        Name = tag.tag
                    }).ToList()
            }
            );
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult New(NewPost form)
        {
            if (!ModelState.IsValid)
            {
                form.tags = Database.Session.Query<Tag>().Select(
                    tag => new TagCheckBox()
                    {
                        Id = tag.id,
                        Name = tag.tag
                    }).ToList();
                return View(form);
            }

            var post = new Post
            {
                title = form.title,
                author = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == User.Identity.Name),
                date = DateTime.Now,
                content = form.content
            };

            //SyncTag(form.tags, post.tags);
            Database.Session.Save(post);
            Database.Session.Flush();
            return RedirectToAction("Index");
        }

        public ActionResult Publications()
        {
            var model = Database.Session.Query<Post>().OrderByDescending(x => x.date).ToList();
            return PartialView("_AllPosts",model);
        }
    }
}