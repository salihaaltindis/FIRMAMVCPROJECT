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
        public ActionResult Index(string arama)
        {
            List<SLIDER> liste = new List<SLIDER>();
            if (arama == null)
            {
                arama = "";
                liste = db.SLIDERs.ToList();
            }
            else
            {
                liste = db.SLIDERs.Where(k => k.BASLIK.Contains(arama)).ToList();
            }
            ViewData["veri"] = arama;
            return View(liste);
        }

        public ActionResult Delete(int? id)
        {

            if (id != null)//id istek içerisinde varsa
            {
                SLIDER s = db.SLIDERs.Find(id);
                if (s != null)//id SLİDERLARDA varsa
                {
                    db.SLIDERs.Remove(s);
                    db.SaveChanges();//listeden databaseden silinmesini sağlıyor.
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create(int? id)
        {
            SLIDER s = new SLIDER();
            if (id != null)
            {
                s = db.SLIDERs.Find(id);//slider bulunuyor
                if (s == null)//slider yoksa
                {
                    s = new SLIDER();
                }
            }
            return View(s);//model binding
        }

        [HttpPost]
        public ActionResult Create(SLIDER slider)
        {
            if (ModelState.IsValid)
            {
                if (slider.SLIDER_REFNO == 0)
                {
                    db.SLIDERs.Add(slider);
                }
                else
                {
                    db.Entry(slider).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");//listeleme yapılıyor.
            }
            //hata var kayıt ekranı acılacak
            return View(slider);//model binding
        }

        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Sliderlar", new { arama = txtAra });
        }
    }
}
