using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jangi.ViewModels;
using Jangi.Models;

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
    }
}