﻿using BlogApp.Models;
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
                    Aciklama = i.Aciklama.Length>50?i.Aciklama.Substring(0,50)+("..."):i.Aciklama,
                    Icerik = i.Icerik     
                })
                .OrderByDescending(i => i.EklenmeTarihi).ToList();

            return View(blog);
        }
        public ActionResult Detay(int id)
        {
            var blog = db.Bloglar.Find(id);
            ViewBag.blog = blog;
            return View();
        }

    }
}