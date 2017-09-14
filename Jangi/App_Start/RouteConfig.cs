using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Jangi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Posts","", new { controller = "Posts", action = "Index" });
            routes.MapRoute("Register", "signup", new { controller = "Auth", action = "Register" });
            routes.MapRoute("Login", "login", new { controller = "Auth", action = "Login" });
            routes.MapRoute("Logout", "logout", new { controller = "Auth", action = "Logout" });
            routes.MapRoute("Layout", "posts/categories", new { controller = "Posts", action = "Categories" });
            routes.MapRoute("AllPost", "posts/allposts", new { controller = "Posts", action = "Publications" });
            routes.MapRoute("NewPost", "newpost", new { controller = "Posts", action = "New" });
            routes.MapRoute("NewTag", "newtag", new { controller = "Posts", action = "NewTag" });
            routes.MapRoute("MonProfile", "profile", new { controller = "Profile", action = "Index" });
            routes.MapRoute("ChangePass", "changepassword", new { controller = "Profile", action = "Password" });
            routes.MapRoute("MesPublications", "myposts", new { controller = "Profile", action = "Publication" });
            routes.MapRoute("Display", "post/{id}", new { controller = "Posts", action = "Display", id = UrlParameter.Optional });
            routes.MapRoute("DeletePost", "post/delete/{id}", new { controller = "Profile", action = "DeletePost", id = UrlParameter.Optional });
            routes.MapRoute("EditPost", "post/edit/{id}", new { controller = "Posts", action = "Edit", id = UrlParameter.Optional });
        }
    }
}

