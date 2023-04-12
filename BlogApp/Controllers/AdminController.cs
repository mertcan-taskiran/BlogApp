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

        public ActionResult AdminBlogs()
        {
            var blog = db.Bloglar.Select(i => new BlogModel()
            {
                Id = i.Id,
                Baslik = i.Baslik.Length > 20 ? i.Baslik.Substring(0, 20) + ("...") : i.Baslik,
                KullaniciAdi = i.KullaniciAdi,
                KategoriAdi = i.Kategori.KategoriAdi,
                Resim = i.Resim,
                EklenmeTarihi = i.EklenmeTarihi,
                Onay = i.Onay,
                Anasayfa = i.Anasayfa,
                Goruntulenme = i.Goruntulenme,
                Yorum = i.Yorum,
                Aciklama = i.Aciklama.Length > 20 ? i.Aciklama.Substring(0, 20) + ("...") : i.Aciklama,
                Icerik = i.Icerik
            })
            .OrderByDescending(i => i.EklenmeTarihi).ToList();  

            return View(blog);
        }

        public ActionResult OnayListesi()
        {
            var blog = db.Bloglar.Where(i => i.Onay == true).Select(i => new BlogModel()
            {
                Id = i.Id,
                Baslik = i.Baslik.Length > 20 ? i.Baslik.Substring(0, 20) + ("...") : i.Baslik,
                KullaniciAdi = i.KullaniciAdi,
                KategoriAdi = i.Kategori.KategoriAdi,
                Resim = i.Resim,
                EklenmeTarihi = i.EklenmeTarihi,
                Onay = i.Onay,
                Anasayfa = i.Anasayfa,
                Goruntulenme = i.Goruntulenme,
                Yorum = i.Yorum,
                Aciklama = i.Aciklama.Length > 20 ? i.Aciklama.Substring(0, 20) + ("...") : i.Aciklama,
                Icerik = i.Icerik
            })
            .OrderByDescending(i => i.EklenmeTarihi).ToList();

            return View(blog);
        }

        public ActionResult OnaysizListesi()
        {
            var blog = db.Bloglar.Where(i => i.Onay == false).Select(i => new BlogModel()
            {
                Id = i.Id,
                Baslik = i.Baslik.Length > 20 ? i.Baslik.Substring(0, 20) + ("...") : i.Baslik,
                KullaniciAdi = i.KullaniciAdi,
                KategoriAdi = i.Kategori.KategoriAdi,
                Resim = i.Resim,
                EklenmeTarihi = i.EklenmeTarihi,
                Onay = i.Onay,
                Anasayfa = i.Anasayfa,
                Goruntulenme = i.Goruntulenme,
                Yorum = i.Yorum,
                Aciklama = i.Aciklama.Length > 20 ? i.Aciklama.Substring(0, 20) + ("...") : i.Aciklama,
                Icerik = i.Icerik
            })
            .OrderByDescending(i => i.EklenmeTarihi).ToList();

            return View(blog);
        }

    }
}