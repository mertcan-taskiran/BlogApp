using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index(int sayi = 1)
        {
            var blog = db.Bloglar
                .Where(i => i.Onay == true && i.Anasayfa == true)
                .Select(i=>new BlogModel(){
                    Id = i.Id,
                    Baslik = i.Baslik,
                    KullaniciAdi = i.KullaniciAdi,
                    Resim = i.Resim,
                    EklenmeTarihi = i.EklenmeTarihi,
                    Onay = i.Onay,
                    Anasayfa = i.Anasayfa,
                    Goruntulenme = i.Goruntulenme,
                    Yorum = i.Yorum,
                    Aciklama = i.Aciklama,
                    Icerik = i.Icerik     
                })
                .OrderByDescending(i => i.EklenmeTarihi).ToList().ToPagedList(sayi, 9);

            return View(blog);
        }

        public ActionResult BlogListesi(int ? id)
        {
            var blog = db.Bloglar.Where(i => i.Onay == true).OrderByDescending(i => i.EklenmeTarihi).AsQueryable();
            if (id != null)
            {
                blog = blog.Where(i => i.KategoriId == id);
            }
            return View(blog.ToList());
        }

        public ActionResult Ara(string q)
        {
            var ara = db.Bloglar.Where(i => i.Onay == true && i.Baslik.Contains(q) || i.Aciklama.Contains(q)).OrderByDescending(i => i.EklenmeTarihi).ToList();
            return View(ara);
        }

        public PartialViewResult EnCokOkunan()
        {
            var encok = db.Bloglar.Where(i => i.Onay == true).OrderByDescending(i => i.Goruntulenme).Take(5).ToList();
            return PartialView(encok);
        }

        public ActionResult Detay(int id)
        {
            var yorumlar = db.Yorumlar.Where(y => y.BlogId == id).OrderByDescending(y => y.Tarih).ToList();
            ViewBag.yorumlar = yorumlar;

            var sonuc = (from ortalama in db.Yorumlar where ortalama.BlogId == id select ortalama.Puan).DefaultIfEmpty(0).Average();
            ViewBag.ortalama = Math.Round(sonuc);

            var blog = db.Bloglar.Find(id);
            ViewBag.blog = blog;

            var sayi = db.Bloglar.ToList().Find(x => x.Id == id);
            sayi.Goruntulenme += 1;
            db.SaveChanges();

            ViewBag.yorumsayisi = db.Yorumlar.ToList().Where(i => i.BlogId == id).Count();

            var yorum = new Yorum()
            {
                BlogId = blog.Id
            };

            return View("Detay", yorum);
        }

        public ActionResult YorumGonder(Yorum yorum, int rating)
        {
            yorum.KullaniciId = User.Identity.Name;
            yorum.Tarih = DateTime.Now;
            yorum.Puan = Convert.ToInt32(rating);
            db.Yorumlar.Add(yorum);
            db.SaveChanges();

            return RedirectToAction("Detay", "Home", new { id = yorum.BlogId });
        }

    }
}