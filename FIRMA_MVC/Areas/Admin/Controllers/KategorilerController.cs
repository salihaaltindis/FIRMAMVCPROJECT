using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    public class KategorilerController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();

        // GET: Kategoriler
        public ActionResult Index(string arama)
        {
            List<KATEGORI> liste = new List<KATEGORI>();
            if (arama==null)
            {
                arama = "";
                liste = db.KATEGORIs.ToList();
            }
            else
            {
                liste = db.KATEGORIs.Where(k => k.KATEGORI_ADI.Contains(arama)).ToList();// List<KATEGORI> liste = db.KATEGORIs.ToList(); satırı ile aynı
            }
            ViewData["veri"] = arama;
            //return View("Index", liste);//Index.cshtml view e listeyi yolla. liste bilgisi yollanıyor.
            //return View();
            //return View("Index");
            return View(liste);
        }


        public ActionResult Delete(int id)
        {
            KATEGORI k = db.KATEGORIs.Find(id);
            if (k!=null)
            {
                db.KATEGORIs.Remove(k);
                db.SaveChanges();
            }
            //List<KATEGORI> liste = db.KATEGORIs.ToList();
            //return View("Index",liste);
            return RedirectToAction("Index");//Yukardaki satırlar ile aynı ama bu daha temiz.
        }

        [HttpGet]//yeni ve guncelleme işlemi
        public ActionResult Create(int? id)//bu gosterim id null olabilir. hem create hem edit sayflarını ortak yaptıgımız için id yolladıgımızda yollamadıgımızda olacak.
        {
            if (id==null)
            {
                KATEGORI k1 = new KATEGORI();
                return View(k1);
            }
            KATEGORI k = db.KATEGORIs.Find(id);
            return View(k);
        }

        [HttpPost]//Kayıt
        public ActionResult Create(KATEGORI k)
        {
            if (k.KATEGORI_REFNO == 0)
            {
                db.KATEGORIs.Add(k);
            }
            else
            {
                db.Entry(k).State = System.Data.Entity.EntityState.Modified;//txtKATEGORI_ADI.Text = k.KATEGORI_ADI;  demek
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Search(string txtAra)
        {
            //return View("Index",liste);//View e yönlendirir
            return RedirectToAction("Index", "Kategoriler", new { arama = txtAra });
        }
    }
}