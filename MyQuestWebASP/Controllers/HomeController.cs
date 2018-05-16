using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using System.Data.Entity;
using System.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyQuestWebASP.Models;

namespace MyQuestWebASP.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            IList<string> roles = new List<string> { "Роль  не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            ApplicationUser user = userManager.FindByName(User.Identity.Name);
            if (user != null)
            {
                roles = userManager.GetRoles(user.Id);
            }

            ViewBag.Roles = roles;
            return View(roles);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}