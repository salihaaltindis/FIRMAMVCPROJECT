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
    public class SliderlarController : Controller
    {
        private FIRMAMODEL db = new FIRMAMODEL();

        // GET: Admin/Sliderlar
        public ActionResult Index()
        {
            return View(db.SLIDERs.ToList());
        }

        // GET: Admin/Sliderlar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SLIDER sLIDER = db.SLIDERs.Find(id);
            if (sLIDER == null)
            {
                return HttpNotFound();
            }
            return View(sLIDER);
        }

        // GET: Admin/Sliderlar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sliderlar/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SLIDER_REFNO,BASLIK,LINK,RESIM,DURUMU")] SLIDER sLIDER)
        {
            if (ModelState.IsValid)
            {
                db.SLIDERs.Add(sLIDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sLIDER);
        }

        // GET: Admin/Sliderlar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SLIDER sLIDER = db.SLIDERs.Find(id);
            if (sLIDER == null)
            {
                return HttpNotFound();
            }
            return View(sLIDER);
        }

        // POST: Admin/Sliderlar/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SLIDER_REFNO,BASLIK,LINK,RESIM,DURUMU")] SLIDER sLIDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sLIDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sLIDER);
        }

        // GET: Admin/Sliderlar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SLIDER sLIDER = db.SLIDERs.Find(id);
            if (sLIDER == null)
            {
                return HttpNotFound();
            }
            return View(sLIDER);
        }

        // POST: Admin/Sliderlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SLIDER sLIDER = db.SLIDERs.Find(id);
            db.SLIDERs.Remove(sLIDER);
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
