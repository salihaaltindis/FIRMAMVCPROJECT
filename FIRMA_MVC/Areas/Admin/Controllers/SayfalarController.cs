using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    public class SayfalarController : Controller
    {
        private FIRMAMODEL db = new FIRMAMODEL();

        // GET: Admin/Sayfalar
        public ActionResult Index()
        {
            return View(db.SAYFAs.ToList());
        }

        // GET: Admin/Sayfalar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAYFA sAYFA = db.SAYFAs.Find(id);
            if (sAYFA == null)
            {
                return HttpNotFound();
            }
            return View(sAYFA);
        }

        // GET: Admin/Sayfalar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sayfalar/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SAYFA_REFNO,BASLIK,ICERIK")] SAYFA sAYFA)
        {
            if (ModelState.IsValid)
            {
                db.SAYFAs.Add(sAYFA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sAYFA);
        }

        // GET: Admin/Sayfalar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAYFA sAYFA = db.SAYFAs.Find(id);
            if (sAYFA == null)
            {
                return HttpNotFound();
            }
            return View(sAYFA);
        }

        // POST: Admin/Sayfalar/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SAYFA_REFNO,BASLIK,ICERIK")] SAYFA sAYFA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sAYFA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sAYFA);
        }

        // GET: Admin/Sayfalar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAYFA sAYFA = db.SAYFAs.Find(id);
            if (sAYFA == null)
            {
                return HttpNotFound();
            }
            return View(sAYFA);
        }

        // POST: Admin/Sayfalar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SAYFA sAYFA = db.SAYFAs.Find(id);
            db.SAYFAs.Remove(sAYFA);
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
