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
            return View(new NewPost());
        }
    }
}