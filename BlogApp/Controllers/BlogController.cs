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
    [Authorize(Roles = "admin")]
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
                    Baslik = i.Baslik.Length > 30 ? i.Baslik.Substring(0, 30) + ("...") : i.Baslik,
                    KullaniciAdi = i.KullaniciAdi,
                    KategoriAdi = i.Kategori.KategoriAdi,
                    Resim = i.Resim,
                    EklenmeTarihi = i.EklenmeTarihi,
                    Onay = i.Onay,
                    Anasayfa = i.Anasayfa,
                    Goruntulenme = i.Goruntulenme,
                    Yorum = i.Yorum,
                    Aciklama = i.Aciklama.Length > 30 ? i.Aciklama.Substring(0, 30) + ("...") : i.Aciklama,
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
        [ValidateInput(false)]
        public ActionResult Create(Blog blog, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                blog.KullaniciAdi = User.Identity.Name;
                blog.EklenmeTarihi = DateTime.Now;

                // image upload
                if (file != null && file.ContentLength > 0)
                {
                    var extensition = Path.GetExtension(file.FileName);
                    if (extensition == ".jpg" || extensition == ".png" || extensition == ".jpeg")
                    {
                        // Rastgele isim oluşturma
                        string randomName = Guid.NewGuid().ToString("N").Substring(0, 10);
                        string fileName = randomName + extensition;

                        string yol = Path.Combine("~/Content/images/", fileName);
                        file.SaveAs(Server.MapPath(yol));
                        blog.Resim = fileName;
                    }
                }               

                db.Bloglar.Add(blog);
                TempData["BlogCreate"] = blog;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KullaniciAdi,Baslik,Aciklama,Resim,Icerik,Goruntulenme,Onay,Anasayfa,KategoriId")] Blog blog, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var existingBlog = db.Bloglar.Find(blog.Id);
                if (existingBlog == null)
                {
                    return HttpNotFound();
                }

                existingBlog.KullaniciAdi = User.Identity.Name;
                existingBlog.Baslik = blog.Baslik;
                existingBlog.Aciklama = blog.Aciklama;
                existingBlog.Icerik = blog.Icerik;
                existingBlog.KategoriId = blog.KategoriId;
                existingBlog.Onay = blog.Onay;
                existingBlog.Anasayfa = blog.Anasayfa;

                if (file != null && file.ContentLength > 0)
                {
                    var extensition = Path.GetExtension(file.FileName);
                    if (extensition == ".jpg" || extensition == ".png" || extensition == ".jpeg")
                    {
                        string randomName = Guid.NewGuid().ToString("N").Substring(0, 10);
                        string fileName = randomName + extensition;

                        string yol = Path.Combine("~/Content/images/", fileName);
                        file.SaveAs(Server.MapPath(yol));
                        existingBlog.Resim = fileName;
                    }
                }

                db.SaveChanges();
                TempData["BlogEdit"] = existingBlog;
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
            TempData["BlogDelete"] = blog;
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
