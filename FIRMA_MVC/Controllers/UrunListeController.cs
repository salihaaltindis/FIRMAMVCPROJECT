using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Controllers
{
    public class UrunListeController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Urunler
        public ActionResult Index(int? kategoriid)
        {
            ViewData["kategori"] = db.KATEGORIs.ToList(); //modelbinding yapmıyoruz kayıt formu olmadığı için.

            if (kategoriid == null)
            {
                kategoriid = db.KATEGORIs.ToList()[0].KATEGORI_REFNO;
            }

            ViewData["urun"] = db.URUNs.Where(s => s.KATEGORI_REFNO == kategoriid).ToList();

            return View();
        }

        public ActionResult Detay(int id=0)
        {
            URUN urun = db.URUNs.Find(id);
            if (urun == null)
            {
                TempData["m"] = "Urun bulunamadı";
                //Goster metot Mesaj controller
                return RedirectToAction("Goster","Mesaj");
            }
            return View(urun);
        }
    }
}
