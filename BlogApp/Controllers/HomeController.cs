using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            var blog = db.Bloglar.Where(i => i.Onay == true && i.Anasayfa == true).OrderByDescending(i => i.EklenmeTarihi);

            return View(blog);
        }
        public ActionResult Detay()
        {
            return View();
        }

    }
}