using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    public class ProjelerController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Projeler
        public ActionResult Index(string arama)
        {
            List<PROJE> liste = new List<PROJE>();
            if (arama == null)
            {
                arama = "";
                liste = db.PROJEs.ToList();
            }
            else
            {
                liste = db.PROJEs.Where(k => k.ADI.Contains(arama)).ToList();
            }
            ViewData["veri"] = arama;
            return View(liste);
        }

        public ActionResult Delete(int? id)
        {

            if (id != null)//id istek içerisinde varsa
            {
                PROJE p = db.PROJEs.Find(id);
                if (p != null)//id projelerde varsa
                {
                    db.PROJEs.Remove(p);
                    db.SaveChanges();//listeden databaseden silinmesini sağlıyor.
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create(int? id)
        {
            PROJE p = new PROJE();
            if (id != null)
            {
                p = db.PROJEs.Find(id);//proje bulunuyor
                if (p == null)//proje yoksa
                {
                    p = new PROJE();
                }
            }
            return View(p);//model binding
        }

        [HttpPost]
        public ActionResult Create(PROJE proje, HttpPostedFileBase RESIM)
        {
            if (ModelState.IsValid)
            {
                if (RESIM != null)
                {
                    proje.RESIM = RESIM.FileName;
                }
                if (proje.PROJE_REFNO == 0)
                {
                    proje.ACIKLAMA = HttpUtility.HtmlDecode(proje.ACIKLAMA);
                    db.PROJEs.Add(proje);
                }
                else
                {
                    proje.ACIKLAMA = HttpUtility.HtmlDecode(proje.ACIKLAMA);
                    db.Entry(proje).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                if (RESIM != null)
                {
                    RESIM.SaveAs(Request.PhysicalApplicationPath + "/images/" + RESIM.FileName); //resim kaydediliyor.
                }
                
            }
            else
            {
                //hata var kayıt ekranı acılacak
                return View(proje);//model binding
            }
            return RedirectToAction("Index");//listeleme yapılıyor.
        }

        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Projeler", new { arama = txtAra });
        }
    }
}