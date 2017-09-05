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
            routes.MapRoute("NewPost", "newpost", new { controller = "Posts", action = "New" });
        }
    }
}

