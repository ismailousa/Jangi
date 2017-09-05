using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Jangi.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/styles")
                .Include("~/Content/assets/css/main.css")
                .Include("~/Content/styles/bootstrap.css")
                );

            bundles.Add(new ScriptBundle("~/scripts")
                .Include("~/scripts/jquery-1.9.1.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/Content/assets/js/skel.min.js")
                .Include("~/Content/assets/js/util.js")
                .Include("~/Content/assets/js/main.js")
                );
        }
    }
}