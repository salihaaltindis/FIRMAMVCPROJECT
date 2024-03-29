﻿using FIRMA_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    public class UrunlerController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Urunler
        public ActionResult Index(string arama)
        {
            List<URUN> liste = new List<URUN>();
            if (arama == null)
            {
                arama = "";
                liste = db.URUNs.ToList();
            }
            else
            {
                liste = db.URUNs.Where(k => k.URUN_ADI.Contains(arama)).ToList();
            }
            ViewData["veri"] = arama;
            return View(liste);
        }

        public ActionResult Delete(int? id)//? id null gidebilir demek
        {
            if (id!=null)//olası hatalı bir link girdiğinde yada null deger girdiğinde
            {
                URUN u = db.URUNs.Find(id);
                if (u!=null)
                {
                    db.URUNs.Remove(u);
                    db.SaveChanges();
                }
            } 
            return RedirectToAction("Index");
        }

        
        public ActionResult Create(int? id)
        {//update create işlemi
            URUN urun = new URUN();
            if (id!=null)//guncelleme yapılacak
            {
                urun = db.URUNs.Find(id);
                if (urun==null)
                {
                    urun = new URUN();    
                }
                
            }
            ViewData["kategori"] = db.KATEGORIs.ToList();//hersey object olarak saklanır.
            ViewBag.marka = db.MARKAs.ToList();
            return View(urun);
        }

        [HttpPost]
        public ActionResult Create(URUN urun,HttpPostedFileBase RESIM1,HttpPostedFileBase RESIM2,
                                             HttpPostedFileBase RESIM3, HttpPostedFileBase RESIM4)
        {//kayıt işlemi

            if (ModelState.IsValid)//model validation yapıyor.
            {
                if (RESIM1!=null)
                {
                    urun.RESIM1 = RESIM1.FileName; 
                }
                if (RESIM2!=null)
                {
                    urun.RESIM2 = RESIM2.FileName; 
                }
                if (RESIM3!=null)
                {
                    urun.RESIM3 = RESIM3.FileName; 
                }
                if (RESIM4!=null)
                {
                    urun.RESIM4 = RESIM4.FileName; 
                }

                if (urun.URUN_REFNO == 0)
                {
                    db.URUNs.Add(urun);
                }
                else
                {
                    db.Entry(urun).State = System.Data.Entity.EntityState.Modified;//urun collection da durumunu değişstiriyot.
                }
                db.SaveChanges();//veritabanı burada kaydediyor.

                //RESIM dosyalarını yükler
                if (RESIM1 != null)
                {
                    RESIM1.SaveAs(Request.PhysicalApplicationPath + "/images/" + RESIM1.FileName); //resim kaydediliyor.
                }
                if (RESIM2 != null)
                {
                    RESIM2.SaveAs(Request.PhysicalApplicationPath + "/images/" + RESIM2.FileName); //resim kaydediliyor.
                }
                if (RESIM3 != null)
                {
                    RESIM3.SaveAs(Request.PhysicalApplicationPath + "/images/" + RESIM3.FileName); //resim kaydediliyor.
                }
                if (RESIM4 != null)
                {
                    RESIM4.SaveAs(Request.PhysicalApplicationPath + "/images/" + RESIM4.FileName); //resim kaydediliyor.
                }
            }
            else
            {
                //hataları kullanıcıya gosterdiğimiz yer.
                string hatalar = "";
                foreach (var item in ModelState.Values)
                {
                    for (int i = 0; i <item.Errors.Count; i++)
                    {
                        hatalar += item.Errors[i].ErrorMessage+" ";
                    }
                    
                }
                ViewData["hatalar"] = hatalar;
                ViewData["kategori"] = db.KATEGORIs.ToList();//hersey object olarak saklanır.
                ViewBag.marka = db.MARKAs.ToList();
                return View(urun);
            }
            return RedirectToAction("Index");

        }

        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Urunler", new { arama = txtAra });
        }

    }
}