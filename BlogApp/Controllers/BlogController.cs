using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Blog
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var blog = db.Bloglar
                .Where(i => i.KullaniciAdi == username)
                .Select(i => new BlogModel()
                {
                    Id = i.Id,
                    Baslik = i.Baslik,
                    KullaniciAdi = i.KullaniciAdi,              
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

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi");
            return View();
        }

        // POST: Blog/Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                blog.KullaniciAdi = User.Identity.Name;
                blog.EklenmeTarihi = DateTime.Now;

                // image upload

                string yol = Path.Combine("/Content/images/" + File.FileName);
                File.SaveAs(Server.MapPath(yol));
                blog.Resim = File.FileName.ToString();

                db.Bloglar.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.KategoriId);
            return View(blog);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.KategoriId);
            return View(blog);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KullaniciAdi,Baslik,Aciklama,Resim,Icerik,Goruntulenme,Onay,Anasayfa,KategoriId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.KullaniciAdi = User.Identity.Name;
                blog.EklenmeTarihi = DateTime.Now;

                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.KategoriId);
            return View(blog);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Bloglar.Find(id);
            db.Bloglar.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
