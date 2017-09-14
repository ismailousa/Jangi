using Jangi.Models;
using Jangi.ViewModels;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Jangi.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            var user = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == User.Identity.Name);

            var a = Database.Session.Query<Post>().Select(x => x.author == user).ToList().Count();
            var profile = new ProfileInfo
            {
                id = user.id,
                pseudo = user.pseudo,
                email = user.email,
                birthDate = user.birthDate,
                numberOfPosts = Database.Session.Query<Post>().Select(x => x.author == user).ToList().Count()
            };
            return View(profile);
        }

        [HttpPost]
        public ActionResult Index(ProfileInfo form)
        {
            var toLogin = false;
            var user = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == User.Identity.Name);
            var existUser = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == form.pseudo && x.pseudo != User.Identity.Name);
            if (existUser != null)
                ModelState.AddModelError("Existe", "Ce pseudo n'est pas disponible");
            if (!ModelState.IsValid)
            {
                return View(new ProfileInfo
                {
                    id = user.id,
                    pseudo = user.pseudo,
                    email = user.email,
                    birthDate = user.birthDate,
                    numberOfPosts = Database.Session.Query<Post>().Select(x => x.author == user).ToList().Count()
                });
            }
            if (form.pseudo != "" && form.pseudo != user.pseudo)
                toLogin = true;
            if (form.pseudo != "")
                user.pseudo = form.pseudo;
            if (form.email != "")
                user.email = form.email;
            if (form.birthDate != null)
                user.birthDate = form.birthDate;

            Database.Session.Update(user);
            Database.Session.Flush();
            if (toLogin)
            {
                FormsAuthentication.SignOut();
                return RedirectToRoute("login");
            }
            return RedirectToRoute("Posts");
        }

        public ActionResult Password()
        {
            return View(new newPassword());
        }

        [HttpPost]
        public ActionResult Password(newPassword form)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == User.Identity.Name);
            if (form.password == null)
                form.password = "";
            if (!user.CheckPassword(form.password))
                ModelState.AddModelError("Mot de Passe", "Le mot de passe est incorrect");
            if (form.passwordConfirm != form.passwordNew)
                ModelState.AddModelError("Mot de Passe", "Les deux mot de passe ne sont pas identique");
            if (form.passwordConfirm == "" || form.passwordNew == "" || form.password == "")
                ModelState.AddModelError("Mot de Passe", "Tout les champs sont requis");
            if (!ModelState.IsValid)
                return View(form);

            user.SetPassword(form.passwordNew);
            Database.Session.Update(user);
            Database.Session.Flush();

            FormsAuthentication.SignOut();
            return RedirectToRoute("login");

        }

        public ActionResult Publication()
        {
            var user = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == User.Identity.Name);
            var posts = user.posts.OrderByDescending(x => x.date).ToList();
            return View(posts);
        }

        public ActionResult DeletePost(int id, string returnUrl)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == User.Identity.Name);
            var post = Database.Session.Load<Post>(id);

            if(post.author == user)
            {
                Database.Session.Delete(post);
                Database.Session.Flush();
            }

            return RedirectToAction("Publication");
        }
    }
}