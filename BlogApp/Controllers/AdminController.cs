using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {       
        DataContext db = new DataContext();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Onaysayi = db.Bloglar.Where(i => i.Onay == true).Count();
            ViewBag.Onaysizsayi = db.Bloglar.Where(i => i.Onay == false).Count();
            ViewBag.Blogsayi = db.Bloglar.Count();
            return View();
        }

        public ActionResult BlogListesi()
        {
            var blog = db.Bloglar.ToList();  
            return View(blog);
        }

        public ActionResult OnayListesi()
        {
            var blog = db.Bloglar.Where(i => i.Onay == true).ToList();
            return View(blog);
        }

        public ActionResult OnaysizListesi()
        {
            var blog = db.Bloglar.Where(i => i.Onay == false).ToList();
            return View(blog);
        }

    }
}