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
    [Authorize(Roles = "admin")]
    public class YetenekController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Yetenek
        public ActionResult Index()
        {
            return View(db.Yeteneks.ToList());
        }

        // GET: Yetenek/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yetenek yetenek = db.Yeteneks.Find(id);
            if (yetenek == null)
            {
                return HttpNotFound();
            }
            return View(yetenek);
        }

        // GET: Yetenek/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yetenek/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,YetenekAdi,Progress")] Yetenek yetenek)
        {
            if (ModelState.IsValid)
            {
                db.Yeteneks.Add(yetenek);
                db.SaveChanges();
                TempData["YetenekCreate"] = yetenek;
                return RedirectToAction("Index");
            }

            return View(yetenek);
        }

        // GET: Yetenek/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yetenek yetenek = db.Yeteneks.Find(id);
            if (yetenek == null)
            {
                return HttpNotFound();
            }
            return View(yetenek);
        }

        // POST: Yetenek/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,YetenekAdi,Progress")] Yetenek yetenek)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yetenek).State = EntityState.Modified;
                db.SaveChanges();
                TempData["YetenekEdit"] = yetenek;
                return RedirectToAction("Index");
            }
            return View(yetenek);
        }

        // GET: Yetenek/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yetenek yetenek = db.Yeteneks.Find(id);
            if (yetenek == null)
            {
                return HttpNotFound();
            }
            return View(yetenek);
        }

        // POST: Yetenek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yetenek yetenek = db.Yeteneks.Find(id);
            db.Yeteneks.Remove(yetenek);
            db.SaveChanges();
            TempData["YetenekDelete"] = yetenek;
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
