using Jangi.Models;
using Jangi.ViewModels;
using NHibernate.Criterion;
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

        [Authorize]
        public ActionResult New()
        {
            return View("Form",new NewPost()
            {
                tags = Database.Session.Query<Tag>().Select(
                    tag => new TagCheckBox()
                    {
                        Id = tag.id,
                        Name = tag.tag
                    }).ToList(),
                isNew = true
            }
            );
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var post = Database.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            return View("Form", new NewPost()
            {
                tags = Database.Session.Query<Tag>().Select(
                    tag => new TagCheckBox()
                    {
                        Id = tag.id,
                        Name = tag.tag,
                        IsChecked = post.tags.Contains(tag)
                    }).ToList(),
                isNew = false,
                title = post.title,
                content = post.content,
                date = post.date,
                comments = post.comments,
                id = post.id,
                author = post.author
            }
            );
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Form(NewPost form)
        {
            form.isNew = form.id == 0;
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
            Post post;
            if (form.isNew)
            {
                post = new Post
                {
                    title = form.title,
                    author = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == User.Identity.Name),
                    date = DateTime.Now,
                    content = form.content,
                    tags = new List<Tag>()
                };
            }
            else
            {
                post = Database.Session.Load<Post>(form.id);
                post.title = form.title;
                post.content = form.content;
            }
           

            SyncTag(form.tags, post.tags);
            Database.Session.SaveOrUpdate(post);
            Database.Session.Flush();
            return RedirectToAction("Index");
        }

        public void SyncTag(IList<TagCheckBox> checkBoxes, IList<Tag> tags)
        {
            var selectedTags = new List<Tag>();

            foreach(var tag in Database.Session.Query<Tag>())
            {
                var checkBox = checkBoxes.Single(t => t.Id == tag.id);

                if (checkBox.IsChecked)
                    selectedTags.Add(tag);
            }

            foreach (var toadd in selectedTags.Where(t => !tags.Contains(t)))
                tags.Add(toadd);
            foreach (var toremove in tags.Where(t => !selectedTags.Contains(t)).ToList())
                tags.Remove(toremove);

        }

        public ActionResult Publications()
        {
            var model = Database.Session.Query<Post>().OrderByDescending(x => x.date).ToList();
            return PartialView("_AllPosts",model);
        }

        public ActionResult List(string cat)
        {
            posts posts = new posts();
            switch(cat)
            {
                case "Toutes":
                    posts.postList = Database.Session.Query<Post>().OrderBy(x => x.title).ToList();
                    break;
                case "Populaires":
                    posts.postList = Database.Session.Query<Post>().OrderByDescending(x => x.comments.Count).ToList();
                    break;
                default: var tag = Database.Session.Query<Tag>().FirstOrDefault(x => x.tag == cat);
                    posts.postList = Database.Session.Query<Post>().Where(x => x.tags.Contains(tag)).OrderByDescending(x => x.date).ToList();
                    break;
            }
            posts.Category = cat;
            //ViewBag.Title = cat;
            return View(posts);
        }

        public ActionResult Display(int id)
        {
            var post = Database.Session.Load<Post>(id);
            if (post == null)
                return RedirectToAction("index");
            return View(new NewPost
            {
                id = post.id,
                title = post.title,
                comments = post.comments,
                tagList = post.tags,
                author = post.author,
                date = post.date,
                content = post.content
            });
        }

        [Authorize]
        public ActionResult NewTag()
        {
            return View(new tagViewModel());
        }

        [HttpPost, Authorize]
        public ActionResult NewTag(tagViewModel form)
        {
            var tag = Database.Session.Query<Tag>().FirstOrDefault(t => t.tag == form.Categorie);
            if (tag != null)
                ModelState.AddModelError("Categorie", "Cette categorie existe deja");
            if (!ModelState.IsValid)
                return View(form);

            tag = new Tag();
            tag.tag = form.Categorie;
            tag.author = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == User.Identity.Name);

            Database.Session.Save(tag);
            Database.Session.Flush();

            return RedirectToAction("New");
        }

        [Authorize,HttpPost, ValidateInput(false)]
        public ActionResult Comment(NewPost form)
        {
            var post = Database.Session.Load<Post>(form.id);
            if (post == null)
                return HttpNotFound();

            var comment = new Comment()
            {
                post = post,
                author = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == User.Identity.Name),
                content = form.newComment,
                date = DateTime.Now
            };

            if(comment.content != null)
            {
                Database.Session.Save(comment);
                Database.Session.Flush();
            }

            return RedirectToAction("display", new { id = post.id });
        }

        [Authorize, HttpPost, ValidateInput(false)]
        public ActionResult Reply(NewPost form)
        {
            string val = Convert.ToString(Request.Params["CmtId"]);
            var comment = Database.Session.Load<Comment>(Int32.Parse(val));
            if (comment == null)
                return HttpNotFound();

            var reply = new CommentReply()
            {
                comment = comment,
                author = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == User.Identity.Name),
                content = form.newReply,
                date = DateTime.Now
            };

            if (reply.content != null)
            {
                Database.Session.Save(reply);
                Database.Session.Flush();
            }

            return RedirectToAction("display", new { id = comment.post.id });
        }

        [Authorize, HttpPost, ValidateInput(false)]
        public ActionResult EditReply(NewPost form)
        {
            string val = Convert.ToString(Request.Params["replyid"]);
            var reply = Database.Session.Load<CommentReply>(Int32.Parse(val));
            if (reply == null)
                return HttpNotFound();

            reply.content = form.newReply;

            if (reply.content != null)
            {
                Database.Session.Update(reply);
                Database.Session.Flush();
            }

            return RedirectToAction("display", new { id = reply.comment.post.id });
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Search(string query)
        {

            return View(new PostSearch
            {
                posts = Database.Session.Query<Post>().Where(p => p.title.Like(query)).ToList(),
                users = Database.Session.Query<User>().Where(u => u.pseudo.Like(query)).ToList(),
                tags = Database.Session.Query<Tag>().Where(u => u.tag.Like(query)).ToList()
            });
        }

        public ActionResult UserPost(string pseudo)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.pseudo == pseudo);
            if (user == null)
                return HttpNotFound();

            return View(new userResult
            {
                name = user.pseudo,
                posts = user.posts
            });
        }
    }
}