using FIRMA_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    public class MarkalarController : Controller
    {

        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Markalar
        public ActionResult Index(string arama)
        {
            List<MARKA> liste = new List<MARKA>();
            if (arama == null)
            {
                arama = "";
                liste = db.MARKAs.ToList();
            }
            else
            {
                liste = db.MARKAs.Where(k => k.MARKA_ADI.Contains(arama)).ToList();
            }
            ViewData["veri"] = arama;
            return View(liste);
        }

        public ActionResult Delete(int id)
        {
            MARKA m = db.MARKAs.Find(id);
            if (m != null)
            {
                db.MARKAs.Remove(m);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                MARKA m1 = new MARKA();
                return View(m1);
            }
            MARKA m = db.MARKAs.Find(id);
            return View(m);
        }

        [HttpPost]
        public ActionResult Create(MARKA m)
        {
            if (m.MARKA_REFNO == 0)
            {
                db.MARKAs.Add(m);
            }
            else
            {
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Markalar", new { arama = txtAra });
        }
    }
}