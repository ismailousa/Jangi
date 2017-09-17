using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jangi.ViewModels;
using Jangi.Models;
using NHibernate.Linq;
using System.Web.Security;

namespace Jangi.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Register()
        {
            return View(new AuthRegister());
        }

        [HttpPost]
        public ActionResult Register(AuthRegister form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var user = new User
            {
                pseudo = form.pseudo,
                email = form.email,
                birthDate = form.birthDate,
            };
            user.SetPassword(form.password);
            Database.Session.Save(user);
            Database.Session.Flush();

            return Content("User " + form.pseudo + " Registered");
        }

        public ActionResult Login()
        {
             return View(new AuthLogin());
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(x => x.pseudo == form.pseudoORmail 
            || x.email == form.pseudoORmail);

            if (user == null){
                //Jangi.Models.User.FakeHash();
            }
            if (user == null || !user.CheckPassword(form.password))
                ModelState.AddModelError("Pseudo", "Le pseudo, l'email, ou le mot de passe est incorrect");

            if (!ModelState.IsValid)
                return View(new AuthLogin());

            FormsAuthentication.SetAuthCookie(user.pseudo, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("Posts");
        }

        public ActionResult Logout(){
            FormsAuthentication.SignOut();
            return RedirectToRoute("Posts");
        }
    }
}