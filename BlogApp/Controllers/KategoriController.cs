using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;

namespace BlogApp.Controllers
{
    //[Authorize(Roles = "admin")]
    public class KategoriController : Controller
    {
        private DataContext db = new DataContext();

        [Authorize(Roles = "admin")]
        // GET: Kategori
        public ActionResult Index()
        {
            var kategoriler = db.Kategoriler.Select(i => new CategoryModel()
            {
                Id = i.Id,
                KategoriAdi = i.KategoriAdi,
                BlogSayisi = i.Bloglar.Count()               
        });
            
            return View(kategoriler.ToList());
        }

        public PartialViewResult KategoriListesi()
        {
            var kategori = db.Kategoriler.ToList();
            return PartialView(kategori);
        }

        [Authorize(Roles = "admin")]
        // GET: Kategori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategoriler.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        [Authorize(Roles = "admin")]
        // GET: Kategori/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        // POST: Kategori/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,KategoriAdi")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                db.Kategoriler.Add(kategori);
                db.SaveChanges();
                //TempData["KategoriCreate"] = kategori;
                return RedirectToAction("Index");
            }

            return View(kategori);
        }

        [Authorize(Roles = "admin")]
        // GET: Kategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategoriler.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        [Authorize(Roles = "admin")]
        // POST: Kategori/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KategoriAdi")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategori).State = EntityState.Modified;
                db.SaveChanges();
                //TempData["KategoriEdit"] = kategori;
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        [Authorize(Roles = "admin")]
        // GET: Kategori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategoriler.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        [Authorize(Roles = "admin")]
        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori kategori = db.Kategoriler.Find(id);
            db.Kategoriler.Remove(kategori);
            db.SaveChanges();
            //TempData["KategoriDelete"] = kategori;
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
